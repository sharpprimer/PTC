
namespace CTP_STrader.Base
{
    public class CustomTrade
    {
        public int orderNo;
        public string tradeRef;

        public string InstrumentID;
        public BS_CODE Direction;
        public OC_CODE OffsetFlag;
        public double TradePrice;
        public int TradeVol;
        public string TradeTime;
    }
}
