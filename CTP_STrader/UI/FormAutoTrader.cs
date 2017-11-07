using CTP;
using CTP_STrader.Base;
using CTP_STrader.Biz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace CTP_STrader
{
    internal delegate void UpdateCtrlBackColorDelegate(Control control, Color color);
    internal delegate void UpdateHQDelegate(SpreadInfo spInfo);

    public partial class FormAutoTrader : Form
    {
        private static readonly int TIMER_INTERVAL = 5000;
        private static readonly double OPEN_CLOSE_MIN_SPREAD = 0.2;   // 平仓价差必须比开仓价差大，防止输入错误

        private SpreadTrader spreadTrader;

        // 输入参数
        int inInitPosition, inMaxWarnTimes;
        StInputParam param;

        // 内部变量
        protected bool isFirstTimeClickStartBtn = true;
        protected int errorStateCount = 0;                        // 当前已出错次数
        protected CustomMarketData latestM1 = null;
        protected CustomMarketData latestM2 = null;
        protected SpreadInfo lastHQInfo = null;

        protected Control[] noResetCtrls = null;  // 只enable一次控件
        protected Control[] resetCtrls = null;    // 可以重置控件

        // 检查提醒风控指标
        //System.Timers.Timer timer = new System.Timers.Timer();

        public FormAutoTrader()
        {
            InitializeComponent();
        }

        // 载入窗体时初始化
        private void FormIFAutoTrader_Load(object sender, EventArgs e)
        {
            // 初始化价差交易
            spreadTrader = new SpreadTrader(CTPMgr.CtpTradeClient);
            spreadTrader.HandleStatusDel += new HandleStatusDelegate(UpdateLogInfo);
            spreadTrader.HandleErrorDel += new HandleErrorDelegate(HandleSpreadTradeError);

            // 初始化下单代码
            var codes = new List<string>(CTPMgr.CtpTradeClient.Instruments);
            cB_IF1.Items.AddRange(codes.ToArray());
            cB_IF2.Items.AddRange(codes.ToArray());
            cB_IF1.SelectedIndex = 0;
            cB_IF2.SelectedIndex = 1;

            // 初始化加减档位控件
            cB_IF1PriceLevel.Items.AddRange(Enum.GetNames(typeof(PRICE_LEVEL)));
            cB_IF2PriceLevel.Items.AddRange(Enum.GetNames(typeof(PRICE_LEVEL)));
            cB_IF1PriceLevel.SelectedIndex = cB_IF2PriceLevel.SelectedIndex = 0;

            // 初始化可重置及非可重置控件
            noResetCtrls = new Control[] { cB_IF1, cB_IF2, nUD_InitPosition };
            resetCtrls = new Control[] { rB_A,rB_H,rB_S, tB_ESpread, tB_EPrice,tB_MaxWarnTimes, tB_MaxTN, tB_MaxOpenN, tB_QueryWaitTime, tB_OpenSpread, tB_CloseSpread, 
                cB_IF1PriceLevel, cB_IF2PriceLevel, nUD_MaxPosition, nUD_HedgeVol, nUD_IF1, nUD_IF2, nUD_ReorderAppendPt, nUD_ReorderTimes };

            // 绑定成交结果集
            var detailList = new BindingList<TradeDetail>();
            detailList.Add(spreadTrader.TradeDetail_IF1BO);
            detailList.Add(spreadTrader.TradeDetail_IF2SO);
            detailList.Add(spreadTrader.TradeDetail_IF1SC);
            detailList.Add(spreadTrader.TradeDetail_IF2BC);

            var summaryList = new BindingList<TradeSummary>();
            summaryList.Add(spreadTrader.TradeSummary);

            dGV_Detail.DataSource = detailList;
            dGV_Summary.DataSource = summaryList;

            // 初始化风控提醒timer
            //timer.Elapsed += new ElapsedEventHandler(OnTimer);
            //timer.Interval = TIMER_INTERVAL;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (!PassCheckUIInput())
            { return; }

            // 下单类型参数
            CTPMgr.CtpTradeClient.HedgeFlagType = rB_S.Checked ? EnumHedgeFlagType.Speculation : (rB_A.Checked ? EnumHedgeFlagType.Arbitrage : EnumHedgeFlagType.Hedge);

            // 获取用户输入参数
            inInitPosition = (int)nUD_InitPosition.Value;
            inMaxWarnTimes = int.Parse(tB_MaxWarnTimes.Text);

            param = new StInputParam();
            param.InEPrice = double.Parse(tB_EPrice.Text);
            param.InESpread = double.Parse(tB_ESpread.Text);
            param.InMaxTN = int.Parse(tB_MaxTN.Text);
            param.InMaxOpenN = int.Parse(tB_MaxOpenN.Text);
            param.InWaitTime = int.Parse(tB_QueryWaitTime.Text);
            param.InMaxP = (int)nUD_MaxPosition.Value;
            param.InVolUnit = (int)nUD_HedgeVol.Value;
            param.InKC = double.Parse(tB_OpenSpread.Text);
            param.InPC = double.Parse(tB_CloseSpread.Text);
            param.InReorderAppend = (double)nUD_ReorderAppendPt.Value;
            param.InMaxReroderTimes = (int)nUD_ReorderTimes.Value;

            if (param.InMaxP >= 0)
            {// 正向开仓
                param.InIF1 = cB_IF1.Text;
                param.InIF2 = cB_IF2.Text;
                param.InIF1Append = (double)nUD_IF1.Value;
                param.InIF2Append = (double)nUD_IF2.Value;
            }
            else
            {// 反向开仓
                param.InMaxP = -1 * param.InMaxP;
                param.InIF2 = cB_IF1.Text;
                param.InIF1 = cB_IF2.Text;
                param.InIF2Append = (double)nUD_IF1.Value;
                param.InIF1Append = (double)nUD_IF2.Value;
            }

            // 设置spread trade的输入参数
            spreadTrader.SetInputParam(param);

            // 只接受第一次开始时输入的初始仓位参数
            if (isFirstTimeClickStartBtn)
            {
                isFirstTimeClickStartBtn = false;
                spreadTrader.SetCurrentPosition(inInitPosition);
            }

            // 冻结不允许改动的控件
            foreach (var c in noResetCtrls)
            { c.Enabled = false; }

            // 禁止再次点击开始，但可以停止
            ResetControls(false);

            // 开始价差交易
            spreadTrader.Enable(true);

            // 检查风控指标
            //timer.Start();
            //OnTimer(null, null);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            // 停止标志
            spreadTrader.Enable(false);

            // enable控件，供用户重新输入参数
            ResetControls(true);

            // 停止检查风控指标
            //timer.Stop();
        }

        private void ResetControls(bool enable)
        {
            btn_Start.Enabled = enable;
            btn_Stop.Enabled = !enable;
            foreach (var c in resetCtrls)
            { c.Enabled = enable; }
        }

        private bool PassCheckUIInput()
        {
            var IF1 = cB_IF1.Text;
            var IF2 = cB_IF2.Text;
            if (string.IsNullOrEmpty(IF1) || string.IsNullOrEmpty(IF2) ||
                (IF1 == PublicDefine.STOCK_NA && IF2 == PublicDefine.STOCK_NA))
            {
                ShowMsgBoxAndUpdateLogInfo("IF1或IF2代码不能为空！请选择合适的代码后才能进行开仓/平仓！");
                return false;
            }
            if (IF1 == IF2)
            {
                ShowMsgBoxAndUpdateLogInfo("IF1或IF2代码不能为相同代码！请重新选择");
                return false;
            }

            double value = 0;
            if (!double.TryParse(tB_ESpread.Text, out value) || value < 0)
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：异常行情价差");
                return false;
            }
            if (!double.TryParse(tB_EPrice.Text, out value) || value < 0)
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：异常最新价");
                return false;
            }
            if (!double.TryParse(tB_MaxTN.Text, out value) || value < 0)
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：累计成交数量上限");
                return false;
            }
            if (!double.TryParse(tB_MaxOpenN.Text, out value) || value < 0)
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：累计开仓数量上限");
                return false;
            }
            if (!double.TryParse(tB_QueryWaitTime.Text, out value) || value < 0)
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：查询等待 >= 0");
                return false;
            }
            if (!double.TryParse(tB_MaxWarnTimes.Text, out value) || value <= 0)
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：错误提醒次数上限 > 0");
                return false;
            }

            double v1, v2;
            if (!double.TryParse(tB_OpenSpread.Text, out v1))
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：开仓价差");
                return false;
            }
            if (!double.TryParse(tB_CloseSpread.Text, out v2))
            {
                ShowMsgBoxAndUpdateLogInfo("请输入合适的参数：平仓价差");
                return false;
            }
            if (Math.Round((v2 - v1), 2) <= OPEN_CLOSE_MIN_SPREAD)
            {
                ShowMsgBoxAndUpdateLogInfo("平仓价差 - 开仓价差 必须大于" + OPEN_CLOSE_MIN_SPREAD + "！请重新设置");
                return false;
            }

            return true;
        }

        public void OnMarketData(CustomMarketData m)
        {
            var del = new HandleMarketDataDelegate(OnMarketDataInternal);
            this.BeginInvoke(del, new object[] { m });
        }

        private void OnMarketDataInternal(CustomMarketData m)
        {
            spreadTrader.OnMarketData(m);

            // 更新本地行情缓存
            if (m != null && m.StockCode == cB_IF1.Text)
            {
                latestM1 = m;
            }
            else if (m != null && m.StockCode == cB_IF2.Text)
            {
                latestM2 = m;
            }
            else
            { return; }

            // 当前行情时间戳是否一致,一致时才进行开仓平仓判断
            if (latestM1 != null && latestM2 != null &&
                latestM1.StockCode == cB_IF1.Text && latestM2.StockCode == cB_IF2.Text &&
                latestM1.Time == latestM2.Time && latestM1.MilliSec == latestM2.MilliSec)
            {
                // 处理行情，计算价差
                var spInfo = ProcessHQ(latestM1, latestM2);

                spreadTrader.OnSpreadData(spInfo);

                UpdateHQ(spInfo);
            }
        }

        private SpreadInfo ProcessHQ(CustomMarketData m1, CustomMarketData m2)
        {
            SpreadInfo hqInfo = new SpreadInfo();
            hqInfo.Time = m1.Time;
            hqInfo.LastIF1Price = (lastHQInfo == null) ? 0 : lastHQInfo.IF1Price;
            hqInfo.LastIF2Price = (lastHQInfo == null) ? 0 : lastHQInfo.IF2Price;
            hqInfo.IF1Price = (m1 == null) ? double.NaN : m1.NewPrice;
            hqInfo.IF2Price = (m2 == null) ? double.NaN : m2.NewPrice;

            if (m1 == null || m2 == null || m1.Buy.Length <= 0 || m1.Sell.Length <= 0 || m2.Buy.Length <= 0 || m2.Sell.Length <= 0)
            {
                hqInfo.IF1OpenPrice = hqInfo.IF1ClosePrice = hqInfo.IF2OpenPrice = hqInfo.IF2ClosePrice = double.NaN;
                hqInfo.CurrentSpread = hqInfo.CloseSpread = hqInfo.OpenSpread = double.NaN;

                UpdateLogInfo(string.Format("当前行情异常:无法获得{0}的买卖盘口", (m1 == null || m1.Buy.Length <= 0 || m1.Sell.Length <= 0) ? param.InIF1 : param.InIF2));
            }
            else
            {
                hqInfo.IF1OpenPrice = m1.Sell[0];
                hqInfo.IF1ClosePrice = m1.Buy[0];
                hqInfo.IF2OpenPrice = m2.Buy[0];
                hqInfo.IF2ClosePrice = m2.Sell[0];
                hqInfo.MaxOpenVol = (int)Math.Min(m1.SellVol[0], m2.BuyVol[0]);
                hqInfo.MaxCloseVol = (int)Math.Min(m1.BuyVol[0], m2.SellVol[0]);

                hqInfo.LastSpread = (lastHQInfo == null) ? 0 : lastHQInfo.CurrentSpread;
                hqInfo.CurrentSpread = m1.NewPrice - m2.NewPrice;                           // 实时最新价差
                hqInfo.OpenSpread = hqInfo.IF1OpenPrice - hqInfo.IF2OpenPrice;              // 实时开仓价差
                hqInfo.CloseSpread = hqInfo.IF1ClosePrice - hqInfo.IF2ClosePrice;           // 实时平仓价差
            }

            lastHQInfo = hqInfo;
            return hqInfo;
        }

        public void OnRtnOrder(CustomOrder order)
        {
            var del = new HandleRtnOrderDelegate(OnRtnOrderInternal);
            this.BeginInvoke(del, new object[] { order });
        }

        private void OnRtnOrderInternal(CustomOrder order)
        {
            spreadTrader.OnRtnOrder(order);
        }

        public void OnRtnTrade(CustomTrade trade)
        {
            var del = new HandleRtnTradeDelegate(OnRtnTradeInternal);
            this.BeginInvoke(del, new object[] { trade });
        }

        private void OnRtnTradeInternal(CustomTrade trade)
        {
            spreadTrader.OnRtnTrade(trade);
        }

        public void OnErrRtnOrderCancel(CustomOrder order, string errorMsg)
        {
            var del = new HandleErrRtnOrderCancelDelegate(OnErrRtnOrderCancelInternal);
            this.BeginInvoke(del, new object[] { order, errorMsg });
        }

        private void OnErrRtnOrderCancelInternal(CustomOrder order, string errorMsg)
        {
            spreadTrader.OnErrRtnOrderCancel(order, errorMsg);
        }

        public void HandleFatalError(string errorMsg)
        {
            // 停止继续下单
            spreadTrader.Enable(false);

            // 提示
            UpdateLogInfo(errorMsg);
            UpdateLogInfo("程序已暂停运行。如要继续，请按“停止”-“开始”。");
        }

        private void HandleSpreadTradeError(string errorMsg)
        {
            errorStateCount++;
            UpdateLogInfo(errorMsg);

            if (errorStateCount < inMaxWarnTimes)
            {// 只弹出警告框，程序继续执行
                ShowErrorMsgBox(errorMsg, true);
            }
            else
            {// 弹出警告框并让用户选择，程序暂停
                ShowErrorMsgBox(errorMsg + "已达到警告次数上限!", false);
            }
        }

        // 更新界面日志
        public void UpdateLogInfo(string text)
        {
            if (listBox_Log.InvokeRequired)
            {
                var updateLog = new UpdateLogInfoDelegate(UpdateLogInfo);
                this.BeginInvoke(updateLog, new object[] { text });
            }
            else
            {
                var message = DateTime.Now.ToString("HH:mm:ss.fff  ") + text;
                listBox_Log.Items.Add(message);
                int visibleItems = listBox_Log.ClientSize.Height / listBox_Log.ItemHeight;
                listBox_Log.TopIndex = Math.Max(listBox_Log.Items.Count - visibleItems + 1, 0);
            }
        }

        private void UpdateHQ(SpreadInfo spInfo)
        {
            if (lb_Time1.InvokeRequired)
            {
                var updateHQ = new UpdateHQDelegate(UpdateHQ);
                this.BeginInvoke(updateHQ, new object[] { spInfo });
            }
            else
            {
                lb_IF1.Text = (latestM1 == null) ? "N/A" : latestM1.StockCode;
                lb_IF2.Text = (latestM2 == null) ? "N/A" : latestM2.StockCode;
                lb_Time1.Text = (latestM1 == null) ? "N/A" : latestM1.Time + "." + latestM1.MilliSec;
                lb_Time2.Text = (latestM2 == null) ? "N/A" : latestM2.Time + "." + latestM2.MilliSec;
                lb_IF1Price.Text = spInfo.IF1Price.ToString();
                lb_IF2Price.Text = spInfo.IF2Price.ToString();
                lb_Diff1.Text = spInfo.CurrentSpread.ToString("F2");
                lb_OpenSpread.Text = spInfo.OpenSpread.ToString("F2");
                lb_CloseSpread.Text = spInfo.CloseSpread.ToString("F2");
            }
        }

        private void ShowErrorMsgBox(string text, bool isBlock)
        {
            if (isBlock)
            {
                // 将当前错误状态重置，以便继续下单
                spreadTrader.Reset();
                // 只弹出警告框
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate { MessageBox.Show(text, "警告次数:" + errorStateCount, MessageBoxButtons.OK); }));
            }
            else
            {
                var caption = "错误";
                text = text + "\n如要继续，请按“是”，终止运行请按“否”";
                if (DialogResult.Yes == MessageBox.Show(text, caption, MessageBoxButtons.YesNo))
                { // 将当前错误状态重置，以便继续下单
                    spreadTrader.Reset();
                    errorStateCount = 0;
                    UpdateLogInfo("已达到警告次数上限，用户选择继续执行");
                }
                else
                { UpdateLogInfo("已达到警告次数上限，用户选择终止运行，如要继续请重开窗口"); }
            }
        }

        private void ShowMsgBoxAndUpdateLogInfo(string text, string caption = null)
        {
            UpdateLogInfo(text);
            if (string.IsNullOrEmpty(caption))
            {
                caption = "错误";
            }
            MessageBox.Show(text, caption);
        }

        private void dGV_Detail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dGV_Detail.Columns[e.ColumnIndex].Name == "TradeAmount")
            {
                e.CellStyle.Format = "N0";
            }
            else if (dGV_Detail.Columns[e.ColumnIndex].Name == "TradeAvgPrice")
            {
                e.CellStyle.Format = "F2";
            }
        }

        private void dGV_Summary_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dGV_Summary.Columns[e.ColumnIndex].Name == "RealProfit" ||
                dGV_Summary.Columns[e.ColumnIndex].Name == "FloatProfit" ||
                dGV_Summary.Columns[e.ColumnIndex].Name == "TotalProfit" ||
                dGV_Summary.Columns[e.ColumnIndex].Name == "Fee")
            {
                e.CellStyle.Format = "N2";
            }
            else if (dGV_Summary.Columns[e.ColumnIndex].Name == "OpenAvgPrice" ||
                dGV_Summary.Columns[e.ColumnIndex].Name == "CloseAvgPrice")
            {
                e.CellStyle.Format = "F2";
            }
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // 提醒风控指标
            //var origColor = Color.White;
            //var warnColor = Color.Coral;
            //UpdateCtrlBackColor(tB_MaxTN, (totalTradeVol + inN) > inMaxTN ? warnColor : origColor);
            //UpdateCtrlBackColor(tB_MaxOpenN, (totalOpenVol + inN * 2 + inN * inBDN) > inMaxOpenN ? warnColor : origColor);
            //UpdateCtrlBackColor(nUD_MaxPosition, (position >= inMaxP) ? warnColor : origColor);
        }

        private void UpdateCtrlBackColor(Control control, Color color)
        {
            if (control.InvokeRequired)
            {
                var updateDel = new UpdateCtrlBackColorDelegate(UpdateCtrlBackColor);
                this.BeginInvoke(updateDel, new object[] { control, color });
            }
            else
            {
                control.BackColor = color;
            }
        }

        private void FormIFAutoTrader_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (timer != null)
            //{
            //    timer.Stop();
            //    timer.Close();
            //}
        }

    }
}
