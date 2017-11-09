using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTP_MM.Base
{
    public class StInputParam
    {
        public double InESpread;       // 异常价差变动   
        public double InEPrice;        // 异常最新价变动
        public double InMaxTN;         // 累计成交数量上限
        public double InMaxOpenN;      // 累计开仓数量上限
        public int InWaitTime;         // 下单/撤单等待时间，毫秒
        public int InMaxP;             // 仓位上限
        //public int InInitP;            // 初始仓位
        public int InVolUnit;          // 每次开仓/平仓数量
        public double InKC;            // 开仓价差
        public double InPC;            // 平仓价差
        public double InReorderAppend; // 补单附加点
        public int InMaxReroderTimes;  // 最大补单次数
        public string InIF1;           // 默认IF1买/IF2卖，不可随意互换
        public string InIF2;
        public double InIF1Append;     // 下单时的价格附加点
        public double InIF2Append;     // 下单时的价格附加点
    }
}
