using System;

namespace CTP_MM.Base
{
    public class OptionInstrument : FutureInstrument
    {
        // 标的波动率
        public double Sigma;

        // 无风险利率
        public double r;

        // 标的分红率
        public double q;

        // 期权类型
        public EnumOptionType OptionType;

        // 行权价
        public double K;

        // 以年计的时间
        public double T
        {
            get
            {
                return (ExpirationDate - DateTime.Now).Days / 365;
            }
        }

        // 希腊字母
        public Greeks Greeks;

        /// <summary>
        /// 报价策略
        /// </summary>
        public Strategy MMStrategy;
    }
}
