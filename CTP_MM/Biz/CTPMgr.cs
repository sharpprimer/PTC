
namespace CTP_STrader.Biz
{
    public static class CTPMgr
    {
        public static CTPMDClient CtpMdClient = new CTPMDClient();
        public static CTPTraderClient CtpTradeClient = new CTPTraderClient(CtpMdClient);
    }
}
