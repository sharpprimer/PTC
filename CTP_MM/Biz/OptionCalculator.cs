using CTP_MM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTP_MM.Biz
{
    public static class OptionCalculator
    {
        private static double dOne(double S, double K, double r, double T, double Sigma, double q)
        {
            return (Sigma != 0 && T != 0) ?
                (Math.Log(S / K) + ((r - q + Math.Pow(Sigma, 2.0)) / 2.0) * T) / (Sigma * Math.Sqrt(T)) :
                double.NaN;
        }

        private static double dTwo(double S, double K, double r, double T, double Sigma, double q)
        {
            return (Sigma != 0 && T != 0) ?
                (dOne(S, K, r, T, Sigma, q) - Sigma * Math.Sqrt(T)) :
                double.NaN;
        }

        // 累积正态分布函数
        // http://blog.csdn.net/norsd/article/details/52439056
        private static double CND(double d)
        {
            const double A1 = 0.31938153;
            const double A2 = -0.356563782;
            const double A3 = 1.781477937;
            const double A4 = -1.821255978;
            const double A5 = 1.330274429;
            const double RSQRT2PI = 0.39894228040143267793994605993438;

            double K = 1.0 / (1.0 + 0.2316419 * Math.Abs(d));

            double cnd = RSQRT2PI * Math.Exp(-0.5 * d * d) *
                  (K * (A1 + K * (A2 + K * (A3 + K * (A4 + K * A5)))));

            if (d > 0)
                cnd = 1.0 - cnd;

            return cnd;
        }

        // 概率密度函数
        private static double PDF(double d)
        {
            const double RSQRT2PI = 0.39894228040143267793994605993438;
            return RSQRT2PI * Math.Exp(-0.5 * d * d);
        }

        public static double CalcDelta(EnumOptionType OptionType, double S, double K, double r, double T, double Sigma, double q)
        {
            if (Sigma != 0 && T != 0)
            {
                double ndOne = CND(dOne(S, K, r, T, Sigma, q));
                return (OptionType == EnumOptionType.Call) ? ndOne : ndOne - 1;
            }
            else
            {
                return double.NaN;
            }
        }

        public static double CalcGamma(double S, double K, double r, double T, double Sigma, double q)
        {
            return (Sigma != 0 && T != 0) ?
                PDF(dOne(S, K, r, T, Sigma, q)) * Math.Exp(-q * T) / (S * Sigma * Math.Sqrt(T)) :
                double.NaN;

        }

        public static double CalcTheta(EnumOptionType OptionType, double S, double K, double r, double T, double Sigma, double q)
        {
            if (Sigma != 0 && T != 0)
            {
                double d1 = dOne(S, K, r, T, Sigma, q);
                double d2 = dTwo(S, K, r, T, Sigma, q);
                double ndOne = CND(d1);
                double ndTwo = CND(d2);

                if (OptionType == EnumOptionType.Call)
                {
                    return S * PDF(d1) * Sigma * Math.Exp(-q * T) / (2 * Math.Sqrt(T)) + q * S * CND(d1) * Math.Exp(-q * T) - r * K * Math.Exp(-r * T) * CND(d2);
                }
                else
                {
                    return -S * PDF(d1) * Sigma * Math.Exp(-q * T) / (2 * Math.Sqrt(T)) - q * S * CND(-d1) * Math.Exp(-q * T) + r * K * Math.Exp(-r * T) * CND(d2);
                }
            }
            else
            {
                return double.NaN;
            }

        }

        public static double CalcVega(double S, double K, double r, double T, double Sigma, double q)
        {
            return (Sigma != 0 && T != 0) ?
                S * Math.Sqrt(T) * PDF(dOne(S, K, r, T, Sigma, q)) * Math.Exp(-q * T) :
                double.NaN;
        }

        public static double CalcRho(EnumOptionType OptionType, double S, double K, double r, double T, double Sigma, double q)
        {
            if (Sigma != 0 && T != 0)
            {
                double d2 = dTwo(S, K, r, T, Sigma, q);

                return (OptionType == EnumOptionType.Call) ?
                    K * T * Math.Exp(-r * T) * CND(d2) :
                    -K * T * Math.Exp(-r * T) * CND(-d2);
            }
            else
            {
                return double.NaN;
            }
        }

        public static double OptionPrice(EnumOptionType OptionType, double S, double K, double r, double T, double Sigma, double q)
        {
            if (Sigma != 0 && T != 0)
            {
                double d1 = dOne(S, K, r, T, Sigma, q);
                double d2 = dTwo(S, K, r, T, Sigma, q);
                double exp_r = Math.Exp(-r * T);
                double exp_q = Math.Exp(-q * T);

                return (OptionType == EnumOptionType.Call) ?
                    S * exp_q * CND(d1) - K * exp_r * CND(d2) :
                    K * exp_r * CND(-d2) - S * exp_q * CND(-d1);
            }
            else
            {
                return double.NaN;
            }
        }
    }
}
