using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP_MM.Base
{
    class StInternalParam
    {
        public OC_CODE OpenOrClose;
        public double Price1;
        public double Price2;
        public int Vol;

        public StInternalParam(OC_CODE oc, double p1, double p2, int vol)
        {
            OpenOrClose = oc;
            Price1 = p1;
            Price2 = p2;
            Vol = vol;
        }
    }
}
