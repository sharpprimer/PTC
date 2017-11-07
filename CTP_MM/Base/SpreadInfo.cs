
namespace CTP_STrader.Base
{
    public class SpreadInfo
    {
        internal int Time;
        internal double IF1Price;
        internal double IF2Price;
        internal double LastIF1Price;
        internal double LastIF2Price;
        internal double LastSpread = double.NaN;
        internal double OpenSpread = double.NaN;
        internal double CurrentSpread = double.NaN;
        internal double CloseSpread = double.NaN;
        internal double IF1OpenPrice;
        internal double IF2OpenPrice;
        internal double IF1ClosePrice;
        internal double IF2ClosePrice;
        internal int MaxOpenVol;     // 盘口买一/卖一挂单支持的最大开仓手数
        internal int MaxCloseVol;    // 盘口卖一/买一挂单支持的最大平仓手数
    }
}
