using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP_STrader.Base
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
