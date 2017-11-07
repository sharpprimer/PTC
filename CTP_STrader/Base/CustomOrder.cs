
namespace CTP_STrader.Base
{
    public class CustomOrder
    {
        // 报单时需要的信息
        public string InstrumentID;
        public BS_CODE Direction;
        public OC_CODE OffsetFlag;
        public int Volume;
        public double Price;

        // 报单后返回的信息
        public int OrderNo;
        public string tradeRef;
        public int TradedVol;
        public ORDER_STATUS_CODE OrderStatus;

        public bool IsTraded { get { return OrderStatus == ORDER_STATUS_CODE.全成; } }

        public bool IsCanceled { get { return OrderStatus == ORDER_STATUS_CODE.已撤; } }
    }
}
