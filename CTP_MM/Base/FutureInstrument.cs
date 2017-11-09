using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTP_MM.Base
{
    public class FutureInstrument : Instrument
    {
        //合约乘数
        public double Multiplier;

        // 标的资产
        public string UnderlyingIns;

        // 到期日
        public DateTime ExpirationDate;

        // 持仓信息
        public int BuyPosition;
        public int SellPosition;

        // 保证金
        public double Margin;
    }
}
