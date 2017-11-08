using CTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTP_MM.Base
{
    public class CustomQuote
    {
        // 询价回应时需要的信息
        public string InstrumentID;
        public double AskPrice;
        public double BidPrice;
        public int AskVolume;
        public int BidVolume;
        public EnumOffsetFlagType AskOffsetFlag;
        public EnumOffsetFlagType BidOffsetFlag;
        public EnumHedgeFlagType AskHedgeFlag;
        public EnumHedgeFlagType BidHedgeFlag;

    }
}
