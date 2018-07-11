using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace calc.Model
{
    public static class CalcOperations
    {
        //finds out can this number be shorten to integer (4,999999999 = 5)
        private static bool isFrInt(string x)
        {
            bool fl = true;
            int n = x.IndexOf(Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator));
            if (n>0)
            {
                for(int i=n+2;i<x.Length-1;i++)
                {
                    if (x[n+1] != x[i])
                        fl = false;
                }
                if (x[n+1] != '9' || (x.Length - n + 1) <=10 )
                    fl = false;
            }
            return fl;
        }

        //Rounds number to integer or leaves 12 digits after comma
        private static string roundOut(double x)
        {
            if (isFrInt(x.ToString()))
                x = Math.Round(x);
            else
                x = Math.Round(x, 12);
            string s = x.ToString();
            return s;
        }

        //gets double from string with different 
        public static double GetDouble(string value, double defaultValue, ref bool fl)
        {
            double result;
            //try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                //then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
                fl = true;
            }

            return result;
        }

        //sqrt
        public static string root (string x)
        {
            double n;
            bool isUnknownCulture = false;
            n = GetDouble(x, 0, ref isUnknownCulture);
            if(!isUnknownCulture)
            {
                if (n >= 0)
                {
                    n = Math.Sqrt(n);
                    return roundOut(n);
                }
                else
                    return "Invalid input";
            }
            else
            {
                return "Invalid input";
            }
        }

        // /
        public static string div (string x, string y)
        {
            double op1, op2;
            bool isUnknownCulture = false;
            op1 = GetDouble(x, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";
            isUnknownCulture = false;
            op2 = GetDouble(y, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";

            if (op2 == 0)
                return "Divide by 0";
            else
            {
                double n = op1 / op2;
                return roundOut(n);
            }   
        }

        // %
        public static string percent (string x, string y)
        {
            double op1, op2;
            bool isUnknownCulture = false;
            op1 = GetDouble(x, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";
            isUnknownCulture = false;
            op2 = GetDouble(y, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";

            double n = op1 * (op2 / 100.0);
            return roundOut(n);
        }

        //*
        public static string mult (string x, string y)
        {
            double op1, op2;
            bool isUnknownCulture = false;
            op1 = GetDouble(x, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";
            isUnknownCulture = false;
            op2 = GetDouble(y, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";

            double n = op1 * op2;
            return roundOut(n);
        }

        // 1/x
        public static string inv (string x)
        {
            double n;
            bool isUnknownCulture = false;
            n = GetDouble(x, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";
            
            if (n != 0)
            {
                n = 1 / n;
                return roundOut(n);
            }   
            return "Divide by 0";
        }

        //-
        public static string minus (string x, string y)
        {
            double op1, op2;
            bool isUnknownCulture = false;
            op1 = GetDouble(x, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";
            isUnknownCulture = false;
            op2 = GetDouble(y, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";

            double n = op1 - op2;
            return roundOut(n);
        }

        //+
        public static string plus (string x, string y)
        {
            double op1, op2;
            bool isUnknownCulture = false;
            op1 = GetDouble(x, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";
            isUnknownCulture = false;
            op2 = GetDouble(y, 0, ref isUnknownCulture);
            if (isUnknownCulture)
                return "Invalid input";

            double n = op1 + op2;
            return roundOut(n);
        }
    }
}
