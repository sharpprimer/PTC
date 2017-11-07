using System.ComponentModel;

namespace CTP_STrader.Base
{
    public class TradeDetail : INotifyPropertyChanged
    {
        private string stock;
        private string bs;
        private string oc;
        private int tradeVol;
        private double tradeAmount;
        //private double tradeAvgPrice;

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [DisplayName("代码")]
        public string Stock
        {
            get { return this.stock; }
            set
            {
                if (value != this.stock)
                {
                    this.stock = value;
                    NotifyPropertyChanged("Stock");
                }
            }
        }

        [DisplayName("买卖")]
        public string BS
        {
            get { return this.bs; }
            set
            {
                if (value != this.bs)
                {
                    this.bs = value;
                    NotifyPropertyChanged("BS");
                }
            }
        }

        [DisplayName("开平")]
        public string OC
        {
            get { return this.oc; }
            set
            {
                if (value != this.oc)
                {
                    this.oc = value;
                    NotifyPropertyChanged("OC");
                }
            }
        }

        [DisplayName("成交数量")]
        public int TradeVol
        {
            get { return this.tradeVol; }
            set
            {
                if (value != this.tradeVol)
                {
                    this.tradeVol = value;
                    NotifyPropertyChanged("TradeVol");
                    NotifyPropertyChanged("TradeAvgPrice");
                }
            }
        }

        [DisplayName("成交金额")]
        public double TradeAmount
        {
            get { return this.tradeAmount; }
            set
            {
                if (value != this.tradeAmount)
                {
                    this.tradeAmount = value;
                    NotifyPropertyChanged("TradeAmount");
                    NotifyPropertyChanged("TradeAvgPrice");
                }
            }
        }

        [DisplayName("成交均价")]
        public double TradeAvgPrice
        {
            get { return (tradeVol == 0) ? 0 : tradeAmount / tradeVol / 300; }
        }
    }
}
