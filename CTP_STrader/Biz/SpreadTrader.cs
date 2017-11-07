using CTP;
using CTP_STrader.Base;
using System;
using System.Threading;

namespace CTP_STrader.Biz
{
    enum Event { Timer1Timeout, Timer2Timeout, StartTrade, OnRtnOrder, OnErrRtnOrderCancel }
    enum State { Init, WaitPairTraded, WaitPairCanceled, WaitSingleTraded, WaitSingleCanceled, Error }

    public partial class SpreadTrader
    {
        #region
        protected static readonly int VALID_START_TIME_AM = 91600;
        protected static readonly int VALID_END_TIME_AM = 112956;
        protected static readonly int VALID_START_TIME_PM = 130100;
        protected static readonly int VALID_END_TIME_PM = 151456;
        protected static readonly double GOLDEN_SECTION_RATIO = 0.618;  // 黄金分割比例
        protected static readonly double TRADE_FEE_RETIO = 0.0000258;   // 手续费
        protected static readonly int IF_CONTRACT_FACTOR = 300;         // 股指期货合约乘数
        protected static readonly double DOUBLE_COMPARE_MARGIN = 1E-5;

        // 输入参数
        private StInputParam inParam;

        public HandleStatusDelegate HandleStatusDel;          // 状态更新代理函数
        public HandleErrorDelegate HandleErrorDel;            // 错误处理代理函数

        // 内部变量
        private string errorMsg = "-";
        protected bool enable = false;                            // 开始/停止 开关
        //protected int position = 0;
        //protected int totalTradeVol = 0;
        //protected int totalOpenVol = 0;
        protected CustomMarketData latestM1 = null;
        protected CustomMarketData latestM2 = null;
        protected SpreadInfo lastHQInfo = null;

        private object stEnterLocker = new object();
        private AutoResetEvent resetEvent = new AutoResetEvent(false);

        // 输出结果
        public TradeSummary TradeSummary = new TradeSummary();
        public TradeDetail TradeDetail_IF1BO = new TradeDetail();
        public TradeDetail TradeDetail_IF2SO = new TradeDetail();
        public TradeDetail TradeDetail_IF1SC = new TradeDetail();
        public TradeDetail TradeDetail_IF2BC = new TradeDetail();
        #endregion

        public void SetInputParam(StInputParam param)
        {
            this.inParam = param;

            // 设置超时时间
            /// MSDN: http://msdn.microsoft.com/zh-cn/library/system.timers.timer.interval(v=vs.100).aspx
            /// 如果将 Enabled 和 AutoReset 都设置为 false，并且以前已启用了计时器，则设置 Interval 属性会引发一次 Elapsed 事件，
            /// 就好象将 Enabled 属性设置为 true 一样。 若要设置间隔而不引发事件，可以暂时将 AutoReset 属性设置为 true。
            timer1.AutoReset = timer2.AutoReset = true;
            timer1.Interval = timer2.Interval = param.InWaitTime;
            timer1.AutoReset = timer2.AutoReset = false;

            // 设置成交明细所对应的代码
            TradeDetail_IF1BO.Stock = TradeDetail_IF1SC.Stock = param.InIF1;
            TradeDetail_IF2BC.Stock = TradeDetail_IF2SO.Stock = param.InIF2;

            TradeDetail_IF1BO.BS = TradeDetail_IF2BC.BS = PublicDefine.BUY_OR_SELL[0];
            TradeDetail_IF1SC.BS = TradeDetail_IF2SO.BS = PublicDefine.BUY_OR_SELL[1];
            TradeDetail_IF1BO.OC = TradeDetail_IF2SO.OC = PublicDefine.OPEN_OR_CLOSE[0];
            TradeDetail_IF1SC.OC = TradeDetail_IF2BC.OC = PublicDefine.OPEN_OR_CLOSE[1];
        }

        public void Enable(bool enable)
        {
            this.enable = enable;
        }

        public void Reset()
        {
            currentState = State.Init;
        }

        public void SetCurrentPosition(int position)
        {
            //this.position = position;
            TradeSummary.Position = position;
        }

        public void OnMarketData(CustomMarketData m)
        {
            if (inParam == null || m == null)
            { return; }

            // 更新本地行情缓存
            if (m.StockCode == inParam.InIF1)
            {
                latestM1 = m;
            }
            else if (m.StockCode == inParam.InIF2)
            {
                latestM2 = m;
            }
        }

        public void OnSpreadData(SpreadInfo spInfo)
        {
            // 判断是否到达交易条件
            if (CanSpreadTrade(spInfo))
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(StartSpreadTrade), spInfo);
            }
        }

        public void OnRtnTrade(CustomTrade pTrade)
        {
            if (!IsMyTrade(pTrade))
            { return; }

            if (pTrade.InstrumentID == inParam.InIF1)
            {
                if (pTrade.Direction == (int)BS_CODE.Buy) // 跨期自动交易的IF1和IF2不能随意变更，否则对成交汇总的计算会造成错误影响
                {
                    TradeDetail_IF1BO.TradeVol += pTrade.TradeVol;
                    TradeDetail_IF1BO.TradeAmount += pTrade.TradeVol * pTrade.TradePrice * IF_CONTRACT_FACTOR;

                    // 更新持仓（按对计算）
                    TradeSummary.Position += pTrade.TradeVol;
                }
                else
                {
                    TradeDetail_IF1SC.TradeVol += pTrade.TradeVol;
                    TradeDetail_IF1SC.TradeAmount += pTrade.TradeVol * pTrade.TradePrice * IF_CONTRACT_FACTOR;

                    // 更新持仓（按对计算）
                    TradeSummary.Position -= pTrade.TradeVol;
                }
            }
            else if (pTrade.InstrumentID == inParam.InIF2)
            {
                if (pTrade.Direction == BS_CODE.Sell)
                {
                    TradeDetail_IF2SO.TradeVol += pTrade.TradeVol;
                    TradeDetail_IF2SO.TradeAmount += pTrade.TradeVol * pTrade.TradePrice * IF_CONTRACT_FACTOR;
                }
                else
                {
                    TradeDetail_IF2BC.TradeVol += pTrade.TradeVol;
                    TradeDetail_IF2BC.TradeAmount += pTrade.TradeVol * pTrade.TradePrice * IF_CONTRACT_FACTOR;
                }
            }

            TradeSummary.TradeCount += pTrade.TradeVol;

            TradeSummary.OpenAvgPrice = TradeDetail_IF1BO.TradeAvgPrice - TradeDetail_IF2SO.TradeAvgPrice;
            TradeSummary.CloseAvgPrice = TradeDetail_IF1SC.TradeAvgPrice - TradeDetail_IF2BC.TradeAvgPrice;
            TradeSummary.RealProfit = (TradeSummary.CloseAvgPrice - TradeSummary.OpenAvgPrice) * Math.Min(TradeDetail_IF1BO.TradeVol, TradeDetail_IF1SC.TradeVol) * 300;
            //TradeSummary.FloatProfit = (lastHQInfo.CurrentSpread - TradeSummary.OpenAvgPrice) * TradeSummary.Position * 300;
            TradeSummary.Fee = (TradeDetail_IF1BO.TradeAmount + TradeDetail_IF1SC.TradeAmount + TradeDetail_IF2BC.TradeAmount + TradeDetail_IF2SO.TradeAmount) * TRADE_FEE_RETIO;
            TradeSummary.TotalProfit = TradeSummary.RealProfit + TradeSummary.FloatProfit - TradeSummary.Fee;
        }

        bool CanSpreadTrade(SpreadInfo spInfo)
        {
            // 判断当前自动单是否开启
            if (!enable)
            { return false; }

            // 是否在错误状态
            if (IsErrorState())
            { return false; }

            // 判断当前行情是否异常
            if (!HQTest(spInfo))
            { return false; }

            // 行情时间必须在设定的正常交易时间范围内
            if (!(spInfo.Time > VALID_START_TIME_AM && spInfo.Time < VALID_END_TIME_AM) &&
                !(spInfo.Time > VALID_START_TIME_PM && spInfo.Time < VALID_END_TIME_PM))
            { return false; }

            // 是否满足开仓条件
            if (spInfo.OpenSpread < inParam.InKC || Math.Abs(spInfo.OpenSpread - inParam.InKC) <= DOUBLE_COMPARE_MARGIN)
            {
                if (TradeSummary.Position < inParam.InMaxP && RiskTest(OC_CODE.Open))
                {
                    return true;
                }
            }

            // 是否满足平仓条件
            if (spInfo.CloseSpread > inParam.InPC || Math.Abs(spInfo.CloseSpread - inParam.InPC) <= DOUBLE_COMPARE_MARGIN)
            {
                if (TradeSummary.Position > 0 && RiskTest(OC_CODE.Close))
                {
                    return true;
                }
            }

            return false;
        }

        void StartSpreadTrade(object stateObject)
        {
            // 只允许同时运行一个
            if (Monitor.TryEnter(stEnterLocker))
            {
                // 更新价差相关信息
                curSpreadInfo = (SpreadInfo)stateObject;

                // 开始价差下单
                SpreadTrade(Event.StartTrade);

                // 等待执行完成
                resetEvent.WaitOne();

                // 错误处理
                if (IsErrorState() && HandleErrorDel != null)
                {
                    HandleErrorDel(errorMsg);
                }

                // 释放lock
                Monitor.Exit(stEnterLocker);
            }
        }

        bool UpdateOrderPrice(CustomOrder order)
        {
            var m = (order.InstrumentID == inParam.InIF1) ? latestM1 : latestM2;
            if (m == null || m.Buy == null || m.Sell == null)
            {
                errorMsg = order.InstrumentID + "当前无行情数据";
                return false;
            }

            var basePrice = (order.Direction == BS_CODE.Buy) ? m.Sell[0] : m.Buy[0];
            var append = (order.Direction == BS_CODE.Buy) ? inParam.InReorderAppend : (-1 * inParam.InReorderAppend);
            order.Price = basePrice + append;

            return true;
        }

        public bool RiskTest(OC_CODE oc)
        {
            if ((TradeSummary.TradeCount + inParam.InVolUnit) > inParam.InMaxTN)    // 当前累计成交数量+每次下单数量(对)
            {
                return false;
            }

            if (oc == OC_CODE.Open && (TradeSummary.OpenCount + inParam.InVolUnit * 2 + inParam.InVolUnit * inParam.InMaxReroderTimes) > inParam.InMaxOpenN) // 加上潜在的补单最大开仓数量
            {
                return false;
            }

            return true;
        }

        bool HQTest(SpreadInfo hqInfo)
        {
            // 行情信息不能缺失
            if (double.IsNaN(hqInfo.CurrentSpread) || double.IsNaN(hqInfo.OpenSpread) || double.IsNaN(hqInfo.CloseSpread))
            {
                errorMsg = "行情缺失：当前价差为NaN";
                return false;
            }

            // 判断是否异常行情
            if (Math.Abs(hqInfo.LastIF1Price - hqInfo.IF1Price) >= inParam.InEPrice)
            {
                errorMsg = "IF1最新价变动异常，超过设定最新价变动" + inParam.InEPrice + ",last:" + hqInfo.LastIF1Price + ",current:" + hqInfo.IF1Price;
                return false;
            }
            if (Math.Abs(hqInfo.LastIF2Price - hqInfo.IF2Price) >= inParam.InEPrice)
            {
                errorMsg = "IF2最新价变动异常，超过设定最新价变动" + inParam.InEPrice + ",last:" + hqInfo.LastIF2Price + ",current:" + hqInfo.IF2Price;
                return false;
            }
            if (Math.Abs(hqInfo.LastSpread - hqInfo.CurrentSpread) >= inParam.InESpread)
            {
                errorMsg = "价差变动超过设定异常价差变动值" + inParam.InESpread + ",last:" + hqInfo.LastSpread + ",current:" + hqInfo.CurrentSpread;
                return false;
            }

            return true;
        }

        bool IsMyTrade(CustomTrade pTrade)
        {
            return ((order0 != null && order0.tradeRef == pTrade.tradeRef) ||
                (order1 != null && order1.tradeRef == pTrade.tradeRef) ||
                (order2 != null && order2.tradeRef == pTrade.tradeRef));
        }

        void HandleStatusInternal(string text)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff  ") + text);
            if (HandleStatusDel != null)
            {
                HandleStatusDel(text);
            }
        }

        void HandleErrorInternal(string text)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff  ") + text);
            if (HandleErrorDel != null)
            {
                HandleErrorDel(text);
            }
        }
    }

    partial class SpreadTrader
    {
        private CustomOrder order0;
        private CustomOrder order1;
        private CustomOrder order2;

        private System.Timers.Timer timer1; // 报单计时
        private System.Timers.Timer timer2; // 撤单计时

        private int reOrderCount = 0;
        private SpreadInfo curSpreadInfo;
        private State currentState = State.Init;

        private IOrderMgr orderMgr;

        private object stInsideLocker = new object();

        public SpreadTrader(IOrderMgr orderMgr)
        {
            this.orderMgr = orderMgr;

            timer1 = new System.Timers.Timer();
            timer2 = new System.Timers.Timer();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(OnTimer1);
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(OnTimer2);

            // 每次超时只引发一次事件
            timer1.AutoReset = timer2.AutoReset = false;
        }

        public void OnRtnOrder(CustomOrder pOrder)
        {
            if (!IsMyOrder(pOrder))
            { return; }

            System.Diagnostics.Debug.WriteLine(string.Format("{3} IsMyOrder:{0},{1},{2}", pOrder.InstrumentID, pOrder.OrderNo, pOrder.OrderStatus,DateTime.Now.ToString("HH:mm:ss")));

            // 更新order
            order0 = (order0 != null && pOrder.OrderNo == order0.OrderNo) ? pOrder : order0;
            order1 = (order1 != null && pOrder.OrderNo == order1.OrderNo) ? pOrder : order1;
            order2 = (order2 != null && pOrder.OrderNo == order2.OrderNo) ? pOrder : order2;

            switch (pOrder.OrderStatus)
            {
                case ORDER_STATUS_CODE.全成:
                case ORDER_STATUS_CODE.已撤:
                case ORDER_STATUS_CODE.部成撤单:
                    SpreadTrade(Event.OnRtnOrder);
                    break;

                default:
                    break;
            }
        }

        public void OnErrRtnOrderCancel(CustomOrder pOrder, string errorMsg)
        {
            // 撤单返回错误
            if (IsMyOrder(pOrder))
            {
                this.errorMsg = errorMsg;
                SpreadTrade(Event.OnErrRtnOrderCancel);
            }
        }

        void OnTimer1(object obj, EventArgs e)
        {
            errorMsg = string.Format("下单后超时");
            HandleStatusInternal(errorMsg);
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff  ") + errorMsg);

            SpreadTrade(Event.Timer1Timeout);
        }

        void OnTimer2(object obj, EventArgs e)
        {
            errorMsg = "撤单后超时";
            HandleStatusInternal(errorMsg);
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff  ") + errorMsg);

            SpreadTrade(Event.Timer2Timeout);
        }

        void SpreadTrade(Event e)
        {
            lock (stInsideLocker)   // 控制多线程访问
            {
                switch (currentState)
                {
                    case State.Init:
                        S_Init_Func(e);
                        break;

                    case State.WaitPairTraded:
                        S_WaitPairTraded_Func(e);
                        break;

                    case State.WaitPairCanceled:
                        S_WaitPairCanceled_Func(e);
                        break;

                    case State.WaitSingleTraded:
                        S_WaitSingleTraded_Func(e);
                        break;

                    case State.WaitSingleCanceled:
                        S_WaitSingleCanceled_Func(e);
                        break;
                }
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss  ") + "当前状态:" +currentState);
            }
        }

        void S_Init_Func(Event e)
        {
            if (e == Event.StartTrade)
            {
                // 重置补单次数
                reOrderCount = 0;
                // 报单变量初始化
                order0 = order1 = order2 = null;

                // 满足开平仓条件,双边下单
                var param = GetSpreadTradeParam();
                if (!PairOrderInsert(param))
                {
                    currentState = State.Error;
                    Quit();
                }
                else
                {
                    timer1.Start();
                    currentState = State.WaitPairTraded;
                }
            }
        }

        void S_WaitPairTraded_Func(Event e)
        {
            // 全成
            if (e == Event.OnRtnOrder && order1.IsTraded && order2.IsTraded)
            {
                timer1.Stop();
                currentState = State.Init;
                Quit();
            }

            // 全撤,可能下单有问题(例如超过涨跌停),被动撤单
            else if (e == Event.OnRtnOrder && order1.IsCanceled && order2.IsCanceled)
            {
                timer1.Stop();
                errorMsg = "下单后被动撤单";
                currentState = State.Error;
                Quit();
            }

            // 超时
            else if (e == Event.Timer1Timeout)
            {
                // 单边未成交
                if ((order1.IsTraded && !order2.IsTraded) || (!order1.IsTraded && order2.IsTraded))
                {
                    bool bRet;
                    if (!order1.IsTraded)
                    { 
                        bRet = OrderCancel(order1);
                        order0 = order1;
                    }
                    else
                    { 
                        bRet = OrderCancel(order2);
                        order0 = order2;
                    }

                    if (!bRet)
                    {
                        currentState = State.Error;
                        Quit();
                    }
                    else
                    {
                        timer2.Start();
                        currentState = State.WaitSingleCanceled;
                    }
                }

                // 双边未成交
                else if (!order1.IsTraded && !order2.IsTraded)
                {
                    if (OrderCancel(order1) && OrderCancel(order2))
                    {
                        timer2.Start();
                        currentState = State.WaitPairCanceled;
                    }
                    else
                    {
                        currentState = State.Error;
                        Quit();
                    }
                }
            }
        }

        void S_WaitPairCanceled_Func(Event e)
        {
            if (e == Event.OnRtnOrder)
            {
                // 双边已全成
                if (order1.IsTraded && order2.IsTraded)
                {
                    timer2.Stop();
                    currentState = State.Init;
                    Quit();
                }

                // 双边撤单成功 或 一边撤单一边成交
                else if ((order1.IsCanceled && order2.IsCanceled) ||
                    (order1.IsCanceled && order2.IsTraded) ||
                    (order1.IsTraded && order2.IsCanceled) )
                {    // 双边撤单数量相等
                    if (order1.TradedVol == order2.TradedVol)
                    {
                        timer2.Stop();
                        currentState = State.Init;
                        Quit();
                    }
                    else
                    {   // 超过最大补单次数
                        if (IsExceedMaxReOrderTimes())
                        {
                            timer2.Stop();
                            currentState = State.Error;
                            Quit();
                        }
                        else
                        {
                            timer2.Stop();
                            // 补单
                            if (ReOrderInsert())
                            {
                                timer1.Start();
                                currentState = State.WaitSingleTraded;
                            }
                            // 补单失败
                            else
                            {
                                currentState = State.Error;
                                Quit();
                            }
                        } // end IsExceedMaxReOrderTimes()
                    } // end (order1.TradedVol == order2.TradedVol)
                }   // end (order1.IsCanceled && order2.IsCanceled)
            } // end (e == Event.OnRtnOrder)

            // 撤单超时
            else if (e == Event.Timer2Timeout)
            { // 继续等待
                timer2.Start();
                HandleStatusInternal("继续等待撤单回报");
                //currentState = State.Error;
                //Quit();
            }

            // 撤单返回错误
            else if (e == Event.OnErrRtnOrderCancel)
            {
                currentState = State.Error;
                Quit();
            }
        }

        void S_WaitSingleTraded_Func(Event e)
        {
            // 单边全成交
            if (e == Event.OnRtnOrder && order0.IsTraded)
            {
                timer1.Stop();
                currentState = State.Init;
                Quit();
            }

            // 单边被动撤单,可能下单有问题(例如超过涨跌停)
            else if (e == Event.OnRtnOrder && order0.IsCanceled)
            {
                timer1.Stop();
                errorMsg = "下单后被动撤单";
                currentState = State.Error;
                Quit();
            }

            // 超时
            else if (e == Event.Timer1Timeout)
            {
                //撤单
                if (OrderCancel(order0))
                {
                    timer2.Start();
                    currentState = State.WaitSingleCanceled;
                }
                else
                {//撤单失败
                    currentState = State.Error;
                    Quit();
                }
            }
        }

        void S_WaitSingleCanceled_Func(Event e)
        {
            // 单边已撤单
            if (e == Event.OnRtnOrder && order0.IsCanceled)
            {
                // 超过补单次数
                if (IsExceedMaxReOrderTimes())
                {
                    timer2.Stop();
                    currentState = State.Error;
                    Quit();
                }
                else
                {
                    timer2.Stop();
                    // 补单
                    if (ReOrderInsert())
                    {
                        timer1.Start();
                        currentState = State.WaitSingleTraded;
                    }
                    else
                    {
                        currentState = State.Error;
                        Quit();
                    }
                }
            }

            // 单边已成交
            else if (e == Event.OnRtnOrder && order0.IsTraded)
            {
                timer2.Stop();
                currentState = State.Init;
                Quit();
            }

            // 撤单超时
            else if (e == Event.Timer2Timeout)
            {
                timer2.Start();
                HandleStatusInternal("继续等待撤单回报");
                //currentState = State.Error;
                //Quit();
            }

            // 撤单返回错误
            else if (e == Event.OnErrRtnOrderCancel)
            {
                currentState = State.Error;
                Quit();
            }
        }

        bool PairOrderInsert(StInternalParam param)
        {
            // 开/平仓数量
            var vol = (int)((param.OpenOrClose == OC_CODE.Open) ?
                Math.Min(param.Vol, inParam.InMaxP - TradeSummary.Position) : Math.Min(param.Vol, TradeSummary.Position));

            // 更新开仓计数
            if (param.OpenOrClose == OC_CODE.Open)
            {
                TradeSummary.OpenCount += vol * 2;
            }

            // 报单录入
            order1 = new CustomOrder();
            order1.InstrumentID = inParam.InIF1;
            order1.OffsetFlag = param.OpenOrClose;
            order1.Direction = (param.OpenOrClose == OC_CODE.Open) ? BS_CODE.Buy : BS_CODE.Sell;
            order1.Price = param.Price1 + ((order1.Direction == BS_CODE.Buy) ? 1.0 : -1.0) * inParam.InIF1Append;
            order1.Volume = vol;

            order2 = new CustomOrder();
            order2.InstrumentID = inParam.InIF2;
            order2.OffsetFlag = param.OpenOrClose;
            order2.Direction = (param.OpenOrClose == OC_CODE.Open) ? BS_CODE.Sell : BS_CODE.Buy;
            order2.Price = param.Price2 + ((order2.Direction == BS_CODE.Buy) ? 1.0 : -1.0) * inParam.InIF2Append;
            order2.Volume = vol;

            var ret = orderMgr.OrderInsert(order1, out errorMsg) && orderMgr.OrderInsert(order2, out errorMsg);

            // 下单状态更新
            HandleStatusInternal(string.Format("下单：委托号:{0},代码:{1},数量:{2},价格:{3},方向:{4}",
               order1.OrderNo, order1.InstrumentID, order1.Volume, order1.Price, order1.OffsetFlag.ToString() + order1.Direction.ToString()));
            HandleStatusInternal(string.Format("下单：委托号:{0},代码:{1},数量:{2},价格:{3},方向:{4}",
               order2.OrderNo, order2.InstrumentID, order2.Volume, order2.Price, order2.OffsetFlag.ToString() + order2.Direction.ToString()));

            return ret;
        }

        bool ReOrderInsert()
        {
            // 双边撤单时补单情形
            if (currentState == State.WaitPairCanceled)
            {
                if (order1.TradedVol > order2.TradedVol)
                {
                    order0 = order2;
                    order0.Volume = order1.TradedVol - order2.TradedVol;
                }
                else if (order1.TradedVol < order2.TradedVol)
                {
                    order0 = order1;
                    order0.Volume = order2.TradedVol - order1.TradedVol;
                }
                else
                { return true; }
            }

            // 单边撤单时补单情形
            else if (currentState == State.WaitSingleCanceled)
            {
                order0.Volume -= order0.TradedVol;
            }
            else
            { return false; }

            // 获取最新补单价格
            if (!UpdateOrderPrice(order0))
            {
                errorMsg = "补单时无法获取最新价格:" + errorMsg;
                return false;
            }

            // 补单次数++
            reOrderCount++;
            
            // 下单
            var ret = orderMgr.OrderInsert(order0, out errorMsg);

            // 补单状态
            HandleStatusInternal(string.Format("-补单：委托号:{0},代码:{1},数量:{2},价格:{3},方向:{4}",
               order0.OrderNo, order0.InstrumentID, order0.Volume, order0.Price, order0.OffsetFlag.ToString() + order0.Direction.ToString()));

            return ret;
        }

        bool OrderCancel(CustomOrder order)
        {
            HandleStatusInternal(string.Format("(撤单：委托号:{0},代码:{1})", order.OrderNo, order.InstrumentID));

            return orderMgr.OrderCancel(order, out errorMsg);
        }

        void Quit()
        {
            // 价差交易结束
            resetEvent.Set();
        }

        StInternalParam GetSpreadTradeParam()
        {
            // 开仓时的参数
            if (curSpreadInfo.OpenSpread < inParam.InKC || Math.Abs(curSpreadInfo.OpenSpread - inParam.InKC) <= DOUBLE_COMPARE_MARGIN)
            {
                var openVol = (int)Math.Min(inParam.InVolUnit, Math.Round(curSpreadInfo.MaxOpenVol * GOLDEN_SECTION_RATIO));
                return new StInternalParam(OC_CODE.Open, curSpreadInfo.IF1OpenPrice, curSpreadInfo.IF2OpenPrice, openVol);
            }

            // 平仓时的参数
            else
            {
                var closeVol = (int)Math.Min(inParam.InVolUnit, Math.Round(curSpreadInfo.MaxCloseVol * GOLDEN_SECTION_RATIO));
                return new StInternalParam(OC_CODE.Close, curSpreadInfo.IF1ClosePrice, curSpreadInfo.IF2ClosePrice, closeVol);
            }
        }

        bool IsMyOrder(CustomOrder pOrder)
        {
            return (order0 != null && order0.InstrumentID == pOrder.InstrumentID && order0.OrderNo == pOrder.OrderNo) ||
                (order1 != null && order1.InstrumentID == pOrder.InstrumentID && order1.OrderNo == pOrder.OrderNo) ||
                (order2 != null && order2.InstrumentID == pOrder.InstrumentID && order2.OrderNo == pOrder.OrderNo);
        }

        bool IsExceedMaxReOrderTimes()
        {
            var ret = (reOrderCount >= inParam.InMaxReroderTimes);
            if (ret)
            {
                errorMsg = "超过补单次数上限" + reOrderCount + "次";
            }
            return ret;
        }

        bool IsErrorState()
        {
            return (currentState == State.Error);
        }

    }
}
