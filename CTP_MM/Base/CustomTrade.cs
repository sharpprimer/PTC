
using CTP;

namespace CTP_MM.Base
{
    public class CustomTrade
    {
        public int orderNo;
        public string tradeRef;

        public string InstrumentID;
        public EnumDirectionType Direction;
        public EnumOffsetFlagType OffsetFlag;
        public double TradePrice;
        public int TradeVol;
        public string TradeTime;
    }
}
