using CTP;
using CTP_STrader.Base;
using System;
using System.Diagnostics;
using System.Reflection;

namespace CTP_STrader.Biz
{
    public delegate void HandleCTPRawMarketData(ThostFtdcDepthMarketDataField pDepthMarketData);

    public class CTPMDClient
    {
        // 内部变量
        private FtdcMdAdapter mdApi;
        private int iRequestID = 0;

        // 外部输入参数
        public bool IsSubscribeAfterLogin;
        public string BROKER_ID;
        public string[] FrontAddrs;     // 前置机地址列表
        public string[] Instruments;    // 订阅代码

        // 回调处理函数
        public HandleLoginDelegate HandleLoginDel;
        public HandleStatusDelegate HandleStatusDel;
        public HandleErrorDelegate HandleErrorDel;

        public HandleMarketDataDelegate HandleMarketDataDel;
        public HandleCTPRawMarketData HandleRawMarketDataDel;

        public void Init()
        {
            if (FrontAddrs == null || FrontAddrs.Length == 0)
            {
                HandleErrorInternal("前置机地址不能为空!");
                return;
            }

            try
            {
                if (mdApi == null)
                {
                    mdApi = new FtdcMdAdapter(".\\md", false,false);                   // 创建md目录存放流文件，避免与交易流文件冲突
                    mdApi.OnFrontConnected += new FrontConnected(OnFrontConnected);
                    mdApi.OnFrontDisconnected += new FrontDisconnected(OnFrontDisconnected);
                    mdApi.OnRspUserLogout += new RspUserLogout(OnRspUserLogout);
                    mdApi.OnRspUserLogin += new RspUserLogin(OnRspUserLogin);
                    mdApi.OnRspSubMarketData += new RspSubMarketData(OnRspSubMarketData);
                    mdApi.OnRspUnSubMarketData += new RspUnSubMarketData(OnRspUnSubMarketData);
                    mdApi.OnRtnDepthMarketData += new RtnDepthMarketData(OnRtnDepthMarketData);
                    mdApi.OnRspError += new RspError(OnRspError);

                    mdApi.OnFrontEvent += MdApi_OnFrontEvent;
                    mdApi.OnRspEvent += MdApi_OnRspEvent;
                    mdApi.OnRtnEvent += MdApi_OnRtnEvent;

                }

                foreach (var addr in FrontAddrs)
                {
                    mdApi.RegisterFront(addr);  // 注册所有的可用地址
                }

                mdApi.Init();
            }
            catch (Exception ex)
            {
                HandleErrorInternal("CTPMD.Start()异常:" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void Release()
        {
            if (mdApi != null)
            {
                mdApi.Release();
                mdApi = null;
            }
        }

        /*
        * --------------------------------------------------------------
        * 通用部分：连接/登录/订阅/回调处理
        * --------------------------------------------------------------
        */
        private void MdApi_OnFrontEvent(object sender, OnFrontEventArgs e)
        {
            switch (e.EventType)
            {
                case EnumOnFrontType.OnFrontConnected:
                    {
                        __DEBUGPF__();
                        ReqUserLogin();
                    }
                    break;

                default:
                    __DEBUGPF__();
                    HandleStatusInternal("CTP前置机断开连接：nReason = " + e.Reason);
                    break;
            }
        }

        private void ReqUserLogin()
        {
            ThostFtdcReqUserLoginField reqUserLogin = new ThostFtdcReqUserLoginField();
            reqUserLogin.BrokerID = BROKER_ID;

            var ret = mdApi.ReqUserLogin(reqUserLogin, iRequestID++);
            HandleStatusInternal("CTP发送用户登录请求：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));

            // 通知登录结果
            if (HandleLoginDel != null && ret != 0)
            {
                HandleLoginDel(false);
            }
        }

        public void ReqUserLogout()
        {
            ThostFtdcUserLogoutField logoutField = new ThostFtdcUserLogoutField();
            logoutField.BrokerID = BROKER_ID;

            var ret = mdApi.ReqUserLogout(logoutField, iRequestID++);
            HandleStatusInternal("CTP发送用户登出请求：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        public void Subscribe(string[] instruments)
        {
            var ret = mdApi.SubscribeMarketData(instruments);
            HandleStatusInternal("CTP发送订阅请求：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        public void Subscribe()
        {
            var ret = mdApi.SubscribeMarketData(Instruments);
            HandleStatusInternal("CTP发送订阅请求：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        public void Unsubscribe(string[] instruments)
        {
            // 退订所需的行情
            var ret = mdApi.UnSubscribeMarketData(instruments);
            HandleStatusInternal("CTP发送取消订阅请求：" + (ret == 0 ? "成功" : "失败,返回代码" + ret));
        }

        private void MdApi_OnRspEvent(object sender, OnRspEventArgs e)
        {
            Console.WriteLine("MdApi_OnRspEvent " + e.EventType.ToString());
            bool err = IsError(e.RspInfo, e.EventType.ToString());

            switch (e.EventType)
            {
                case EnumOnRspType.OnRspUserLogin:
                    if (!err)
                        Console.WriteLine("登录成功");
                    break;
                case EnumOnRspType.OnRspSubMarketData:
                    {
                        var f = Conv.P2S<ThostFtdcSpecificInstrumentField>(e.Param);
                        Console.WriteLine("订阅成功:" + f.InstrumentID);
                    }
                    break;
                case EnumOnRspType.OnRspUnSubMarketData:
                    {
                        var f = Conv.P2S<ThostFtdcSpecificInstrumentField>(e.Param);
                        Console.WriteLine("退订成功:" + f.InstrumentID);
                    }
                    break;
            }
        }

        private void MdApi_OnRtnEvent(object sender, OnRtnEventArgs e)
        {
            //Console.WriteLine("DataApi_OnRtnEvent " + e.EventType.ToString());

            var fld = Conv.P2S<ThostFtdcDepthMarketDataField>(e.Param);

            Console.WriteLine("{0}.{1:D3} {2} {3}", fld.UpdateTime, fld.UpdateMillisec, fld.InstrumentID, fld.LastPrice);
        }

        private void OnRspUserLogin(ThostFtdcRspUserLoginField pRspUserLogin, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            if (bIsLast)
            {
                bool isLogin = !IsErrorRspInfo(pRspInfo);
                if (isLogin)
                {   // 订阅
                    Subscribe();
                }

                // 通知登录结果
                if (HandleLoginDel != null)
                {
                    HandleLoginDel(isLogin);
                }
            }
        }

        private void OnRspUserLogout(ThostFtdcUserLogoutField pUserLogout, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            IsErrorRspInfo(pRspInfo);
        }

        private void OnRspSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            if (bIsLast && !IsErrorRspInfo(pRspInfo))
            {
                HandleStatusInternal("CTP行情订阅成功");
            }
        }

        private void OnRspUnSubMarketData(ThostFtdcSpecificInstrumentField pSpecificInstrument, ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            if (bIsLast && !IsErrorRspInfo(pRspInfo))
            {
                HandleStatusInternal("CTP行情取消订阅成功");
            }
        }

        private void OnRspError(ThostFtdcRspInfoField pRspInfo, int nRequestID, bool bIsLast)
        {
            __DEBUGPF__();
            IsErrorRspInfo(pRspInfo);
        }

        private bool IsErrorRspInfo(ThostFtdcRspInfoField pRspInfo)
        {
            var flag = (pRspInfo != null && pRspInfo.ErrorID != 0);
            if (flag)
            {
                HandleStatusInternal("CPT返回错误:ErrorID=" + pRspInfo.ErrorID + ",ErrorMsg=" + pRspInfo.ErrorMsg);
            }
            return flag;
        }

        private void __DEBUGPF__()
        {
            StackTrace ss = new StackTrace(false);
            MethodBase mb = ss.GetFrame(1).GetMethod();
            string str = "--->>> " + mb.DeclaringType.Name + "." + mb.Name + "()";
            Debug.WriteLine(str);
            //UpdateLogInfo(str);
        }

        /*
         * --------------------------------------------------------------
         * 行情通知
         * --------------------------------------------------------------
         */
        private void OnRtnDepthMarketData(ThostFtdcDepthMarketDataField pDepthMarketData)
        {
            if (HandleRawMarketDataDel != null)
            {
                HandleRawMarketDataDel(pDepthMarketData);
            }

            if (HandleMarketDataDel != null)
            {
                CustomMarketData m = new CustomMarketData();
                m.StockCode = pDepthMarketData.InstrumentID;
                m.Exchange = PublicDefine.EXCHANGE_SYMBOL_EN[6];
                m.Time = int.Parse(pDepthMarketData.UpdateTime.Replace(":", ""));
                m.MilliSec = pDepthMarketData.UpdateMillisec;
                m.NewPrice = pDepthMarketData.LastPrice;
                m.Buy = new double[] { pDepthMarketData.BidPrice1, pDepthMarketData.BidPrice2, pDepthMarketData.BidPrice3, pDepthMarketData.BidPrice4, pDepthMarketData.BidPrice5 };
                m.BuyVol = new int[] { pDepthMarketData.BidVolume1, pDepthMarketData.BidVolume2, pDepthMarketData.BidVolume3, pDepthMarketData.BidVolume4, pDepthMarketData.BidVolume5 };
                m.Sell = new double[] { pDepthMarketData.AskPrice1, pDepthMarketData.AskPrice2, pDepthMarketData.AskPrice3, pDepthMarketData.AskPrice4, pDepthMarketData.AskPrice5 };
                m.SellVol = new int[] { pDepthMarketData.AskVolume1, pDepthMarketData.AskVolume2, pDepthMarketData.AskVolume3, pDepthMarketData.AskVolume4, pDepthMarketData.AskVolume5 };

                HandleMarketDataDel(m);
            }
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

        private void HandleErrorInternal(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
            if (HandleErrorDel != null)
            {
                HandleErrorDel(text);
            }
        }

    }
}
