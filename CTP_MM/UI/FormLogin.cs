using CTP_MM.Base;
using CTP_MM.Biz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace CTP_MM.UI
{
    public partial class FormLogin : Form
    {
        BackgroundWorker bgWorker = new BackgroundWorker();
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        bool isLogin = false;

        public FormLogin()
        {
            InitializeComponent();

            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            ReadConfig();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            var broker = (Broker)cB_ServerConfig.SelectedItem;
            broker.UserID = tB_User.Text;
            broker.Password = tB_Pass.Text;

            // 登录线程
            bgWorker.RunWorkerAsync(broker);

            btn_Login.Enabled = false;
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var broker = (Broker)e.Argument;

            MdClientLogin(broker);
            TraderClientLogin(broker);
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (isLogin)
            {
                // 保存用户名
                Global.Default.UserID = tB_User.Text;
                Global.Default.Save();

                // 隐藏当前登录窗，显示主窗口
                this.Hide();
                new MainForm().ShowDialog();
                this.Close();

                autoResetEvent.Dispose();
            }
            else
            {
                // 释放CTP连接，为下次重新登录准备
                CTPMgr.CtpMdClient.Release();
                CTPMgr.CtpTradeClient.Release();                
            }

            btn_Login.Enabled = true;
        }

        private void ReadConfig()
        {
            // 用户名
            tB_User.Text = Global.Default.UserID;

            // broker配置
            var doc = new XmlDocument();
            try
            {
                doc.Load(@".\broker.xml");

                var brokers = new List<Broker>();
                XmlNodeList brokerNodes = doc.SelectNodes(@"//broker");
                foreach (XmlNode brokerNode in brokerNodes)
                {
                    Broker broker = new Broker();
                    broker.BrokerName = brokerNode.Attributes["BrokerName"].Value;
                    broker.BrokerID = brokerNode.Attributes["BrokerID"].Value;

                    // 行情前置机
                    var mdFronts = new List<string>();
                    XmlNodeList mdFrontNodes = brokerNode.SelectNodes(@"//MarketData/item");
                    foreach (XmlNode front in mdFrontNodes)
                    {
                        mdFronts.Add(front.InnerText);
                    }
                    broker.MdFrontAddrs = mdFronts.ToArray();

                    // 交易前置机
                    var tradeFronts = new List<string>();
                    XmlNodeList tradeFrontNodes = brokerNode.SelectNodes(@"//Trading/item");
                    foreach (XmlNode front in tradeFrontNodes)
                    {
                        tradeFronts.Add(front.InnerText);
                    }
                    broker.TradeFrontAdds = tradeFronts.ToArray();

                    // broker添加到集合
                    brokers.Add(broker);
                }

                // 绑定控件
                cB_ServerConfig.DataSource = brokers;
                cB_ServerConfig.DisplayMember = "BrokerName";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("解析Broker配置文件出错："+ex.Message);
            }
        }

        private void MdClientLogin(Broker broker)
        {
            // CTP行情登录
            CTPMDClient mdClient = CTPMgr.CtpMdClient;
            mdClient.BROKER_ID = broker.BrokerID;
            mdClient.FrontAddrs = broker.MdFrontAddrs;
            mdClient.IsSubscribeAfterLogin = false;

            mdClient.HandleLoginDel += new HandleLoginDelegate(HandleLogin);
            mdClient.HandleStatusDel += new HandleStatusDelegate(UpdateStatus);
            mdClient.HandleErrorDel += new HandleErrorDelegate(UpdateStatus);

            // 初始化
            mdClient.Init();

            // 等待登录消息
            autoResetEvent.WaitOne();
        }

        private void TraderClientLogin(Broker broker)
        {
            // CTP交易登录
            CTPTraderClient tradeClient = CTPMgr.CtpTradeClient;
            tradeClient.BROKER_ID = broker.BrokerID;
            tradeClient.INVESTOR_ID = broker.UserID;
            tradeClient.PASSWD = broker.Password;
            tradeClient.FrontAddrs = broker.TradeFrontAdds;

            tradeClient.HandleLoginDel += new HandleLoginDelegate(HandleLogin);
            tradeClient.HandleStatusDel += new HandleStatusDelegate(UpdateStatus);
            tradeClient.HandleErrorDel += new HandleErrorDelegate(UpdateStatus);

            // 初始化
            tradeClient.Init();

            // 等待登录消息
            autoResetEvent.WaitOne();
        }

        private void HandleLogin(bool isLogin)
        {
            this.isLogin = isLogin;
            autoResetEvent.Set();
        }

        private void UpdateStatus(string text)
        {
            if (statusStrip.InvokeRequired)
            {
                HandleStatusDelegate updateStatus = new HandleStatusDelegate(UpdateStatus);
                this.BeginInvoke(updateStatus, new object[] { text });
            }
            else
            {
                toolStripStatusLabel1.Text = text;
            }
        }
    }
}
