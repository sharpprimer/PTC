
namespace CTP_MM.Base
{
    public delegate void UpdateLogInfoDelegate(string text);

    public delegate void HandleLoginDelegate(bool isLogin);
    public delegate void HandleStatusDelegate(string text);
    public delegate void HandleErrorDelegate(string error);
    //public delegate void HandleErrorDelegate(int errorID, string errorMsg);

    public delegate void HandleMarketDataDelegate(CustomMarketData marketData);
    public delegate void HandleRtnOrderDelegate(CustomOrder order);
    public delegate void HandleRtnTradeDelegate(CustomTrade trade);
    public delegate void HandleErrRtnOrderCancelDelegate(CustomOrder order, string errorMsg);

    public static class PublicDefine
    {
        public static readonly string[] BUY_OR_SELL = { "买", "卖" };
        public static readonly string[] OPEN_OR_CLOSE = { "开", "平" };
        public static readonly string[] HOLD_TYPE = { "多仓", "空仓" };
        public static readonly string[] EXCHANGE_SYMBOL_ZH = { "", "上交所", "深交所", "上商所", "郑商所", "大商所", "中金所" };
        public static readonly string[] EXCHANGE_SYMBOL_EN = { "", "SH", "SZ", "SHFE", "CZCE", "DCE", "CF" };
        public static readonly string[] ORDER_STATUS = { "", "未报", "待报", "废单", "已报", "部分成交", "全部成交", "已报撤单", "部分成交撤单", "待撤", "未知" };

        public static readonly string STOCK_NA = "N/A";

    }

    public enum BS_CODE { Buy = 0, Sell = 1, }

    public enum OC_CODE { Open = 0, Close = 1, }

    public enum ORDER_STATUS_CODE { 未报 = 1, 待报 = 2, 废单 = 3, 已报 = 4, 部成 = 5, 全成 = 6, 已撤 = 7, 部成撤单 = 8, 待撤 = 9, }

    public enum EXCHANGE_CODE_ZH { 上交所 = 1, 深交所 = 2, 中金所 = 6, }

    public enum PRICE_LEVEL { 加1档 = 0, 最新价 = 1, 减一档 = 2, }

    public enum EnumOptionType { Call = 0, Put = 1 }
}

