namespace CTP_MM.Base
{
    class Broker
    {
        public string BrokerID { get;set; }
        public string BrokerName{ get;set; }

        public string UserID;
        public string Password;

        public string[] MdFrontAddrs;
        public string[] TradeFrontAdds;
    }
}
