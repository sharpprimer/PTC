using System.ComponentModel;

namespace CTP_STrader.Base
{
    public class TradeSummary : INotifyPropertyChanged
    {
        private int position;
        private int tradeCount;
        private int openCount;
        private double openAvgPrice;
        private double closeAvgPrice;
        private double fee;
        private double realProfit;
        private double floatProfit;
        private double totalProfit;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DisplayName("当前仓位(对)")]
        public int Position
        {
            get { return this.position; }
            set
            {
                if (value != this.position)
                {
                    this.position = value;
                    NotifyPropertyChanged("Position");
                }
            }
        }

        [DisplayName("累计成交数量(对)")]
        [Browsable(false)]
        public int TradeCount
        {
            get { return this.tradeCount; }
            set
            {
                if (value != this.tradeCount)
                {
                    this.tradeCount = value;
                    NotifyPropertyChanged("TradeCount");
                }
            }
        }

        [DisplayName("累计开仓数量(手)")]
        public int OpenCount
        {
            get { return this.openCount; }
            set
            {
                if (value != this.openCount)
                {
                    this.openCount = value;
                    NotifyPropertyChanged("OpenCount");
                }
            }
        }

        [DisplayName("开仓平均价差")]
        public double OpenAvgPrice
        {
            get { return this.openAvgPrice; }
            set
            {
                if (value != this.openAvgPrice)
                {
                    this.openAvgPrice = value;
                    NotifyPropertyChanged("OpenAvgPrice");
                }
            }
        }

        [DisplayName("平仓平均价差")]
        public double CloseAvgPrice
        {
            get { return this.closeAvgPrice; }
            set
            {
                if (value != this.closeAvgPrice)
                {
                    this.closeAvgPrice = value;
                    NotifyPropertyChanged("CloseAvgPrice");
                }
            }
        }

        [DisplayName("当前实现盈亏")]
        public double RealProfit
        {
            get { return this.realProfit; }
            set
            {
                if (value != this.realProfit)
                {
                    this.realProfit = value;
                    NotifyPropertyChanged("RealProfit");
                }
            }
        }

        [DisplayName("当前浮动盈亏")]
        public double FloatProfit
        {
            get { return this.floatProfit; }
            set
            {
                if (value != this.floatProfit)
                {
                    this.floatProfit = value;
                    NotifyPropertyChanged("FloatProfit");
                }
            }
        }

        [DisplayName("手续费")]
        public double Fee
        {
            get { return this.fee; }
            set
            {
                if (value != this.fee)
                {
                    this.fee = value;
                    NotifyPropertyChanged("Fee");
                }
            }
        }

        [DisplayName("总盈亏")]
        [Browsable(false)]
        public double TotalProfit
        {
            get { return this.totalProfit; }
            set
            {
                if (value != this.totalProfit)
                {
                    this.totalProfit = value;
                    NotifyPropertyChanged("TotalProfit");
                }
            }
        }
    }
}
