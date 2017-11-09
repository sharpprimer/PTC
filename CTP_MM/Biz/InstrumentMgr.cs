using CTP;
using CTP_MM.Base;
using System.Collections.Concurrent;

namespace CTP_MM.Biz
{
    public class InstrumentMgr
    {
        private ConcurrentDictionary<string, Instrument> Instruments = new ConcurrentDictionary<string, Instrument>();

        public void UpdateMarketData(CustomMarketData marketData)
        {
            Instruments[marketData.StockCode].MarketData = marketData;
        }

        public void UpdateOrder(CustomOrder order)
        {
        }

        public void UpdateTrade(CustomTrade trade)
        {
            var ins = Instruments[trade.InstrumentID] as FutureInstrument;

            // 更新仓位
            if (trade.OffsetFlag == EnumOffsetFlagType.Open)
            {
                if(trade.Direction == EnumDirectionType.Sell)
                {
                    ins.SellPosition++;
                }
                else
                {
                    ins.BuyPosition++;
                }
            }
            else
            {
                if (trade.Direction == EnumDirectionType.Buy)
                {
                    ins.SellPosition--;
                }
                else
                {
                    ins.BuyPosition--;
                }
            }
        }

        public void UpdateOptionGreeks()
        {
            foreach(var item in Instruments)
            {
                var option = item.Value as OptionInstrument;
                if (option != null)
                {
                    double S = Instruments[option.UnderlyingIns].MarketData.NewPrice;
                    option.Greeks.Delta = OptionCalculator.CalcDelta(option.OptionType, S, option.K, option.r, option.T, option.Sigma, option.q);
                    option.Greeks.Theta = OptionCalculator.CalcTheta(option.OptionType,S, option.K, option.r, option.T, option.Sigma, option.q);
                    option.Greeks.Gamma = OptionCalculator.CalcGamma(S, option.K, option.r, option.T, option.Sigma, option.q);
                    option.Greeks.Vega = OptionCalculator.CalcVega(S, option.K, option.r, option.T, option.Sigma, option.q);
                    option.Greeks.Rho = OptionCalculator.CalcRho(option.OptionType,S, option.K, option.r, option.T, option.Sigma, option.q);

                }
            }
        }
    }
}
