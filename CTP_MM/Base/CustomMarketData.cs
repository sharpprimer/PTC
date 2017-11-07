using System;

namespace CTP_STrader.Base
{
    public class CustomMarketData
    {
        public string StockCode;        // 代码
        public string Symbol;       // 名称
        public string Exchange;     // 交易所
        public string Date;         // 日期，交易所不推送，因此可不设置
        public int Time;
        public int MilliSec;        // 股指期货ms时间
        public double PreClose;
        public double NewPrice;
        public double OpenPrice;
        public double HighPrice;
        public double LowPrice;
        public double HighLimit;
        public double LowLimit;
        public double Volume;
        public double Amount;
        public double PrePosition;
        public double Position;
        public double PreSettle;

        public double IOPV;

        public double[] Buy;
        public double[] Sell;
        public int[] BuyVol;
        public int[] SellVol;
    }
}
