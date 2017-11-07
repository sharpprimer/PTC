using CTP_STrader.Base;
using CTP_STrader.Biz;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CTP_STrader.UI
{
    public partial class MainForm : Form
    {
        private object mdLocker = new object();
        private object orderLocker = new object();
        private object tradeLocker = new object();
        private object errRtnLocker = new object();
        private IList<FormAutoTrader> formList = new List<FormAutoTrader>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // 回调函数
            CTPMgr.CtpMdClient.HandleStatusDel += new HandleStatusDelegate(UpdateStatus);
            CTPMgr.CtpMdClient.HandleErrorDel += new HandleErrorDelegate(HandleCTPError);
            CTPMgr.CtpMdClient.HandleMarketDataDel += new HandleMarketDataDelegate(UpdateMarketData);

            CTPMgr.CtpTradeClient.HandleStatusDel += new HandleStatusDelegate(UpdateStatus);
            CTPMgr.CtpTradeClient.HandleErrorDel += new HandleErrorDelegate(HandleCTPError);
            CTPMgr.CtpTradeClient.HandleRtnOrderDel += new HandleRtnOrderDelegate(UpdateOrder);
            CTPMgr.CtpTradeClient.HandleRtnTradeDel += new HandleRtnTradeDelegate(UpdateTrade);
            CTPMgr.CtpTradeClient.HandleErrRtnOrderCancelDel += new HandleErrRtnOrderCancelDelegate(HandleErrRtnOrderCancel);

            // 订阅行情
            //CTPMgr.CtpMdClient.Instruments = CTPMgr.CtpTradeClient.Instruments;
            //CTPMgr.CtpMdClient.Subscribe();
        }

        private void ToolStripMenuItem_Quit_Click(object sender, EventArgs e)
        {
            ReleaseResources();
            Application.Exit();
        }

        private void ToolStripMenuItem_AutoTrader_Click(object sender, EventArgs e)
        {
            // 创建新的自动跨期下单窗口
            var f = new FormAutoTrader();
            lock (mdLocker)
            {
                lock (orderLocker)
                {
                    lock (tradeLocker)
                    {
                        lock (errRtnLocker)
                        {
                            formList.Add(f);
                        }
                    }
                }
            }

            f.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseResources();
        }

        private void UpdateMarketData(CustomMarketData marketData)
        {
            lock (mdLocker)
            {
                foreach (var f in formList)
                {
                    // 推送行情
                    if (!f.IsDisposed)
                    { f.OnMarketData(marketData); }
                }
            }
        }

        private void UpdateOrder(CustomOrder order)
        {
            lock (orderLocker)
            {
                foreach (var f in formList)
                {
                    // 推送委托回报
                    if (!f.IsDisposed)
                    { f.OnRtnOrder(order); }
                }
            }
        }

        private void UpdateTrade(CustomTrade trade)
        {
            lock (tradeLocker)
            {
                foreach (var f in formList)
                {
                    // 推送成交回报
                    if (!f.IsDisposed)
                    { f.OnRtnTrade(trade); }
                }
            }
        }

        private void HandleErrRtnOrderCancel(CustomOrder order, string errorMsg)
        {
            lock(errRtnLocker)
            {
                foreach (var f in formList)
                {
                    // 推送委托回报
                    if (!f.IsDisposed)
                    { f.OnErrRtnOrderCancel(order, errorMsg); }
                }
            }
        }

        private void UpdateStatus(string text)
        {
            if (listBox_Log.InvokeRequired)
            {
                var updateLog = new UpdateLogInfoDelegate(UpdateStatus);
                this.BeginInvoke(updateLog, new object[] { text });
            }
            else
            {
                var message = DateTime.Now.ToString("HH:mm:ss  ") + text;
                listBox_Log.Items.Add(message);
                int visibleItems = listBox_Log.ClientSize.Height / listBox_Log.ItemHeight;
                listBox_Log.TopIndex = Math.Max(listBox_Log.Items.Count - visibleItems + 1, 0);
            }
        }

        private void HandleCTPError(string text)
        {
            UpdateStatus(text);

            // 30:平仓量超过持仓量; 31:资金不足
            if (text.Contains("ErrorID=30") || text.Contains("ErrorID=31"))
            {
                foreach (var f in formList)
                {
                    if (!f.IsDisposed)
                    {
                        f.HandleFatalError(text);
                    }
                }
            }
        }

        private void ReleaseResources()
        {
            CTPMgr.CtpMdClient.Release();
            CTPMgr.CtpTradeClient.Release();
        }

    }
}
