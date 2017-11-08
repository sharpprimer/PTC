using CTP;
using CTP_STrader.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace CTP_STrader.Biz
{
    public class CTPTraderClient : IOrderMgr
    {
        static readonly string INSTRUMENT_ID = "IF";

        // 内部变量
        FtdcTdAdapter trader;
        int FRONT_ID, SESSION_ID;
        int iRequestID = 0;
        int iOrderRef;

        CTPMDClient mdClient;            // 订阅行情用

        // 外部输入参数
        public EnumHedgeFlagType HedgeFlagType;         // 下单类型
        public string[] FrontAddrs;                     // 前置机地址列表
        public string BROKER_ID, INVESTOR_ID, PASSWD;

        // 回调处理函数
        public HandleLoginDelegate HandleLoginDel;
        public HandleStatusDelegate HandleStatusDel;
        public HandleErrorDelegate HandleErrorDel;

        public HandleRtnOrderDelegate HandleRtnOrderDel;    // 委托回报
        public HandleRtnTradeDelegate HandleRtnTradeDel;    // 成交回报
        public HandleErrRtnOrderCancelDelegate HandleErrRtnOrderCancelDel;  // 撤单错误回报

        // 输出参数
        List<string> instruments = new List<string>();
        public string[] Instruments { get { return instruments.ToArray(); } }

        public CTPTraderClient()
        { }

        public CTPTraderClient(CTPMDClient mdClient)
        {
            this.mdClient = mdClient;
        }

        public void Init()
        {
            if (FrontAddrs == null || FrontAddrs.Length == 0)
            {
                HandleErrorInternal("CTP交易输入前置机地址不能为空！");
                return;
            }

            if (trader == null)
            { trader = new FtdcTdAdapter(".\\trade"); }  // 创建trade目录存放流文件，避免与行情流文件冲突

            // 回调函数
            trader.OnFrontEvent += TraderApi_OnFrontEvent;
            trader.OnRspEvent += TraderApi_OnRspEvent;
            trader.OnErrRtnEvent += TraderApi_OnErrRtnEvent;
            trader.OnRtnEvent += TraderApi_OnRtnEvent;

            // 订阅私有流
            trader.SubscribePrivateTopic(EnumTeResumeType.THOST_TERT_RESUME);
            // 订阅公共流
            trader.SubscribePublicTopic(EnumTeResumeType.THOST_TERT_RESUME);

            try
            {
                foreach (var frontAddr in FrontAddrs)
                { trader.RegisterFront(frontAddr); }

                trader.Init();
            }
            catch (Exception ex)
            {
                HandleErrorInternal("CTP连接前置机发生错误：" + ex.Message);
            }
        }

        public void Release()
        {
            if (trader != null)
            {
                trader.Release();
                trader = null;
            }
        }

        /*
         * --------------------------------------------------------------
         * 通用回应事件：连接/错误回应/错误返回/正常回应
         * --------------------------------------------------------------
         */
        private void TraderApi_OnFrontEvent(object sender, OnFrontEventArgs e)
        {
            switch (e.EventType)
            {
                case EnumOnFrontType.OnFrontConnected:
                    {
                        ReqUserLogin();
                    }
                    break;

                default:
                    {
                        HandleStatusInternal("CTP前置机断开连接：nReason = " + e.Reason);
                    }
                    break;
            }
        }

        private void TraderApi_OnRspEvent(object sender, OnRspEventArgs e)
        {
            if (!IsError(e.RspInfo, e.EventType.ToString()))
            { return; }

            switch (e.EventType)
            {
                case EnumOnRspType.OnRspUserLogin:
                    if (e.Param != IntPtr.Zero)
                    {
                        var fld = Conv.P2S<ThostFtdcRspUserLoginField>(e.Param);
                        OnRspUserLogin(fld, e.RspInfo, e.RequestID, e.IsLast);
                    }
                    else
                    { HandleStatusInternal("参数ThostFtdcRspUserLoginField为空"); }
                    break;

                case EnumOnRspType.OnRspSettlementInfoConfirm:
                    if (e.Param != IntPtr.Zero)
                    {
                        var fld = Conv.P2S<ThostFtdcSettlementInfoConfirmField>(e.Param);
                        OnRspSettlementInfoConfirm(fld, e.RspInfo, e.RequestID, e.IsLast);
                    }
                    else
                    { HandleStatusInternal("参数ThostFtdcSettlementInfoConfirmField为空"); }
                    break;

                case EnumOnRspType.OnRspQryInstrument:
                    if (e.Param != IntPtr.Zero)
                    {
                        var fld = Conv.P2S<ThostFtdcInstrumentField>(e.Param);
                        OnRspQryInstrument(fld, e.RspInfo, e.RequestID, e.IsLast);
                    }
                    else
                    { HandleStatusInternal("参数ThostFtdcInstrumentField为空"); }
                    break;

                case EnumOnRspType.OnRspQryTradingAccount:
                    if (e.Param != IntPtr.Zero)
                    {
                        var fld = Conv.P2S<ThostFtdcTradingAccountField>(e.Param);
                        OnRspQryTradingAccount(fld, e.RspInfo, e.RequestID, e.IsLast);
                    }
                    else
                    { HandleStatusInternal("参数ThostFtdcTradingAccountField为空"); }
                    break;

                case EnumOnRspType.OnRspQryInvestorPosition:
                    if (e.Param != IntPtr.Zero)
                    {
                        var fld = Conv.P2S<ThostFtdcInvestorPositionField>(e.Param);
                        OnRspQryInvestorPosition(fld, e.RspInfo, e.RequestID, e.IsLast);
                    }
                    else
                    { HandleStatusInternal("参数ThostFtdcInvestorPositionField为空"); }
                    break;

                case EnumOnRspType.OnRspOrderAction:
                    if (e.Param != IntPtr.Zero)
                    {
                        var fld = Conv.P2S<ThostFtdcInputOrderActionField>(e.Param);
                        OnRspOrderAction(fld, e.RspInfo, e.RequestID, e.IsLast);
                    }
                    else
                    { HandleStatusInternal("参数ThostFtdcInputOrderActionField为空"); }
                    break;
            }
        }

        private void TraderApi_OnErrRtnEvent(object sender, OnErrRtnEventArgs e)
        {
            IsError(e.RspInfo, e.EventType.ToString());
        }

        private void TraderApi_OnRtnEvent(object sender, OnRtnEventArgs e)
        {
            if (e.Param == IntPtr.Zero)
            {
                HandleStatusInternal("TraderApi_OnRtnEvent返回参数e.Param为空!");
                return;
            }

            switch(e.EventType)
            {
                case EnumOnRtnType.OnRtnOrder:
                    {
                        var order = Conv.P2S<ThostFtdcOrderField>(e.Param);
                        OnRtnOrder(order);
                    }
                    break;

                case EnumOnRtnType.OnRtnTrade:
                    {
                        var trade = Conv.P2S<ThostFtdcTradeField>(e.Param);
                        OnRtnTrade(trade);
                    }
                    break;

                case EnumOnRtnType.OnRtnForQuoteRsp:
                    break;

                case EnumOnRtnType.OnRtnQuote:
                    break;
            }
        }

        /*
         * --------------------------------------------------------------
         * 通用部分：连接/登录/结算确认/回调处理
         * --------------------------------------------------------------
         */
        private void ReqUserLogin()
        {
            // 构造登录请求
            ThostFtdcReqUserLoginField login = new ThostFtdcReqUserLoginField();
            login.BrokerID = BROKER_ID;
            login.UserID = INVESTOR_ID;
            login.Password = PASSWD;

            // 发送登录请求
            int ret = trader.ReqUserLogin(login, iRequestID++);
            HandleStatusInternal("CTP发送用户登录请求：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));

            // 登录回调
            if (HandleLoginDel != null && ret != 0)
            {
                HandleLoginDel(false);
            }
        }

        private void OnRspUserLogin(ThostFtdcRspUserLoginField pRspUserLogin, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();

            if (bIsLast)
            {
                // 保存会话参数
                FRONT_ID = pRspUserLogin.FrontID;
                SESSION_ID = pRspUserLogin.SessionID;
                int iNextOrderRef = 0;
                if (!string.IsNullOrEmpty(pRspUserLogin.MaxOrderRef))
                    iNextOrderRef = Convert.ToInt32(pRspUserLogin.MaxOrderRef);
                iNextOrderRef++;
                iOrderRef = iNextOrderRef;

                ///获取当前交易日
                HandleStatusInternal("CTP获取当前交易日 = " + trader.GetTradingDay());

                ///投资者结算结果确认
                ReqSettlementInfoConfirm();

                // 通知登录结果
                if (HandleLoginDel != null)
                {
                    HandleLoginDel(true);
                }
            }
        }

        private void ReqSettlementInfoConfirm()
        {
            Thread.Sleep(1000);

            ThostFtdcSettlementInfoConfirmField confirm = new ThostFtdcSettlementInfoConfirmField();
            confirm.BrokerID = BROKER_ID;
            confirm.InvestorID = INVESTOR_ID;

            int ret = trader.ReqSettlementInfoConfirm(confirm, iRequestID++);
            HandleStatusInternal("CTP投资者结算结果确认：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        private void OnRspSettlementInfoConfirm(ThostFtdcSettlementInfoConfirmField p, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            if (bIsLast)
            {
                HandleStatusInternal("CTP投资者结算结果已确认");

                // 获取合约信息
                ReqQryInstrument();
            }
        }

        private void ReqQryInstrument()
        {
            Thread.Sleep(1000);

            ThostFtdcQryInstrumentField req = new ThostFtdcQryInstrumentField();
            req.InstrumentID = INSTRUMENT_ID;

            int ret = trader.ReqQryInstrument(req, ++iRequestID);
            HandleStatusInternal("CTP请求查询合约：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        private void OnRspQryInstrument(ThostFtdcInstrumentField pInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();

            // 保存返回的合约代码
            instruments.Add(pInstrument.InstrumentID);

            if (bIsLast)
            {
                HandleStatusInternal("CTP合约代码已获取");

                if (mdClient != null)
                {
                    mdClient.Subscribe(instruments.ToArray());
                }

                //请求查询资金账户
                ReqQryTradingAccount();
            }
        }

        private void OnRspOrderInsert(ThostFtdcInputOrderField pInputOrder, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            IsErrorRspInfo(pRspInfo);
        }

        private void OnRspOrderAction(ThostFtdcInputOrderActionField pInputOrderAction, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            //IsErrorRspInfo(pRspInfo);

            if (HandleErrRtnOrderCancelDel != null)
            {
                var customOrder = new CustomOrder();
                // 原始报单信息
                customOrder.OrderNo = int.Parse(pInputOrderAction.OrderRef);
                customOrder.InstrumentID = pInputOrderAction.InstrumentID;
                customOrder.Price = pInputOrderAction.LimitPrice;
                // 更新委托回报信息
                customOrder.tradeRef = pInputOrderAction.ExchangeID + "_" + pInputOrderAction.OrderSysID;

                HandleErrRtnOrderCancelDel(customOrder, pRspInfo.ErrorMsg);
            }
        }

        /*
         * --------------------------------------------------------------
         * 查询函数：查持仓/查资金
         * --------------------------------------------------------------
         */
        public void ReqQryTradingAccount()
        {
            Thread.Sleep(1000);

            ThostFtdcQryTradingAccountField req = new ThostFtdcQryTradingAccountField();
            req.BrokerID = BROKER_ID;
            req.InvestorID = INVESTOR_ID;

            int ret = trader.ReqQryTradingAccount(req, ++iRequestID);
            HandleStatusInternal("CTP请求查询资金账户：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        void OnRspQryTradingAccount(ThostFtdcTradingAccountField pTradingAccount, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();

            //pTradingAccount.Available;
            HandleStatusInternal("当前账户可用资金：" + pTradingAccount.Available);
        }

        public void ReqQryInvestorPosition()
        {
            Thread.Sleep(1000);

            ThostFtdcQryInvestorPositionField req = new ThostFtdcQryInvestorPositionField();
            req.BrokerID = BROKER_ID;
            req.InvestorID = INVESTOR_ID;
            req.InstrumentID = INSTRUMENT_ID;

            int ret = trader.ReqQryInvestorPosition(req, ++iRequestID);
            HandleStatusInternal("CTP请求查询投资者持仓：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        void OnRspQryInvestorPosition(ThostFtdcInvestorPositionField pInvestorPosition, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            //pInvestorPosition.Position;
        }

        /*
         * --------------------------------------------------------------
         * 报单/撤单，委托回报/成交回报
         * --------------------------------------------------------------
         */
        public bool OrderInsert(CustomOrder order, out string errMsg)
        {
            if (!CheckOrder(order, out errMsg))
            { return false; }

            // 构造下单参数
            ThostFtdcInputOrderField inOrder = new ThostFtdcInputOrderField();
            inOrder.BrokerID = BROKER_ID;
            inOrder.InvestorID = INVESTOR_ID;

            inOrder.OrderPriceType = EnumOrderPriceTypeType.LimitPrice;

            inOrder.InstrumentID = order.InstrumentID;
            inOrder.OrderRef = iOrderRef.ToString();
            inOrder.LimitPrice = order.Price;
            inOrder.Direction = (order.Direction == BS_CODE.Buy) ? EnumDirectionType.Buy : EnumDirectionType.Sell;
            inOrder.CombOffsetFlag_0 = (order.OffsetFlag == OC_CODE.Open) ? EnumOffsetFlagType.Open : EnumOffsetFlagType.Close;
            inOrder.VolumeTotalOriginal = order.Volume;

            inOrder.VolumeCondition = EnumVolumeConditionType.AV;
            inOrder.MinVolume = 0;    // ??
            inOrder.ContingentCondition = EnumContingentConditionType.Immediately;
            inOrder.CombHedgeFlag_0 = HedgeFlagType;
            inOrder.TimeCondition = EnumTimeConditionType.GFD;
            inOrder.ForceCloseReason = EnumForceCloseReasonType.NotForceClose;
            inOrder.IsAutoSuspend = 0;
            inOrder.UserForceClose = 0;

            // 更新
            order.OrderNo = iOrderRef;
            iOrderRef++;

            // 下单
            var ret = trader.ReqOrderInsert(inOrder, iRequestID++);
            errMsg = (ret == 0) ? "-" : "CTP报单录入失败,返回值:" + ret;

            return (ret == 0);
        }

        public bool OrderCancel(CustomOrder order, out string errMsg)
        {
            if (order == null || string.IsNullOrEmpty(order.InstrumentID))
            {
                errMsg = "CTP撤单时Order参数不能为空,InstrumentID信息也不能为空";
                return false;
            }

            ThostFtdcInputOrderActionField inAction = new ThostFtdcInputOrderActionField();
            inAction.BrokerID = BROKER_ID;
            inAction.InvestorID = INVESTOR_ID;
            inAction.FrontID = FRONT_ID;
            inAction.SessionID = SESSION_ID;

            inAction.InstrumentID = order.InstrumentID;
            inAction.OrderRef = order.OrderNo.ToString();
            inAction.ActionFlag = EnumActionFlagType.Delete;

            // 撤单
            var ret = trader.ReqOrderAction(inAction, iRequestID++);
            errMsg = (ret == 0) ? "-" : "CTP撤单失败,返回值:" + ret;

            return (ret == 0);
        }

        ///报单通知
        private void OnRtnOrder(ThostFtdcOrderField pOrder)
        {
            //__DEBUGPF__();
            System.Diagnostics.Debug.WriteLine(string.Format("{2} OnRtnOrder: {0}, {1}, {3}",
                pOrder.OrderRef, pOrder.InstrumentID, DateTime.Now.ToString("HH:mm:ss"), pOrder.OrderStatus));

            if (IsMyOrder(pOrder))
            {
                var customOrder = new CustomOrder();
                // 原始报单信息
                customOrder.OrderNo = int.Parse(pOrder.OrderRef);
                customOrder.InstrumentID = pOrder.InstrumentID;
                customOrder.Direction = (pOrder.Direction == EnumDirectionType.Buy) ? BS_CODE.Buy : BS_CODE.Sell;
                customOrder.OffsetFlag = (pOrder.CombOffsetFlag_0 == EnumOffsetFlagType.Open) ? OC_CODE.Open : OC_CODE.Close;
                customOrder.Price = pOrder.LimitPrice;
                customOrder.Volume = pOrder.VolumeTotalOriginal;
                // 更新委托回报信息
                customOrder.tradeRef = pOrder.ExchangeID + "_" + pOrder.OrderSysID;         // 查找对应成交回报的依据
                customOrder.TradedVol = pOrder.VolumeTotalOriginal - pOrder.VolumeTotal;    // 已成交数量
                customOrder.OrderStatus = ConvertStatus(pOrder.OrderStatus);

                // 处理委托回报
                if (HandleRtnOrderDel != null)
                {
                    HandleRtnOrderDel(customOrder);
                    //HandleRtnOrderDel.BeginInvoke(customOrder, null, null);
                }
            }
        }

        ///成交通知
        private void OnRtnTrade(ThostFtdcTradeField pTrade)
        {
            __DEBUGPF__();

            // 更新成交回报信息
            CustomTrade customTrade = new CustomTrade();
            customTrade.tradeRef = pTrade.ExchangeID + "_" + pTrade.OrderSysID;             // 成交回报的唯一编号
            customTrade.InstrumentID = pTrade.InstrumentID;
            customTrade.Direction = (pTrade.Direction == EnumDirectionType.Buy) ? BS_CODE.Buy : BS_CODE.Sell;
            customTrade.OffsetFlag = (pTrade.OffsetFlag == EnumOffsetFlagType.Open) ? OC_CODE.Open : OC_CODE.Close;
            customTrade.TradePrice = pTrade.Price;
            customTrade.TradeVol = pTrade.Volume;
            customTrade.TradeTime = pTrade.TradeTime;

            // 处理成交回报
            if (HandleRtnTradeDel != null)
            {
                //HandleRtnTradeDel.BeginInvoke(customTrade, null, null);
                HandleRtnTradeDel(customTrade);
            }
        }

        /*
         * --------------------------------------------------------------
         * 询价/询价回应
         * --------------------------------------------------------------
         */

        private void OnRtnForQuoteRsp(CThostFtdcQuo)

        /*
         * --------------------------------------------------------------
         * 帮助函数
         * --------------------------------------------------------------
         */
        private bool CheckOrder(CustomOrder order, out string errorMsg)
        {
            if (order == null)
            {
                errorMsg = "CTP报单参数不能为空!";
                return false;
            }
            if (string.IsNullOrEmpty(order.InstrumentID))
            {
                errorMsg = "CTP报单时InstrumentID不能为空";
                return false;
            }

            errorMsg = "-";
            return true;
        }

        private bool IsMyOrder(ThostFtdcOrderField pOrder)
        {
            return (pOrder.FrontID == FRONT_ID &&
                pOrder.SessionID == SESSION_ID);
        }

        private ORDER_STATUS_CODE ConvertStatus(EnumOrderStatusType status)
        {
            switch (status)
            {
                case EnumOrderStatusType.AllTraded:
                    return ORDER_STATUS_CODE.全成;
                case EnumOrderStatusType.Canceled:
                    return ORDER_STATUS_CODE.已撤;
                case EnumOrderStatusType.NoTradeNotQueueing:
                    return ORDER_STATUS_CODE.未报;
                case EnumOrderStatusType.NoTradeQueueing:
                    return ORDER_STATUS_CODE.已报;
                case EnumOrderStatusType.PartTradedQueueing:
                    return ORDER_STATUS_CODE.部成;
                case EnumOrderStatusType.PartTradedNotQueueing:
                    return ORDER_STATUS_CODE.部成撤单;
                case EnumOrderStatusType.Unknown:
                    return ORDER_STATUS_CODE.待报;
                default:
                    return ORDER_STATUS_CODE.未报;
            }
        }

        private bool IsError(ThostFtdcRspInfoField rspinfo, string source)
        {
            if (rspinfo != null && rspinfo.ErrorID != 0)
            {
                HandleStatusInternal("CTP返回错误:ErrorID=" + rspinfo.ErrorID + ",ErrorMsg=" + rspinfo.ErrorMsg + ", 来源 " + source);
                return true;
            }

            return false;
        }

        private void __DEBUGPF__()
        {
#if (DEBUG)
            StackTrace ss = new StackTrace(false);
            MethodBase mb = ss.GetFrame(1).GetMethod();
            string str = "--->>> " + mb.DeclaringType.Name + "." + mb.Name + "()";
            Debug.WriteLine(str);
            //UpdateLogInfo(str);
#endif
        }

        /*
         * --------------------------------------------------------------
         * 内部处理函数
         * --------------------------------------------------------------
         */
        private void HandleStatusInternal(string text)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + text);
            if (HandleStatusDel != null)
            {
                HandleStatusDel(text);
            }
        }

        private void HandleErrorInternal(string errMsg)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + errMsg);
            if (HandleErrorDel != null)
            {
                HandleErrorDel(errMsg);
            }
        }
    }
}
