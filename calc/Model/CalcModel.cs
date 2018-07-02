using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc.Model
{
    class CalcModel
    {
        private bool ispercent;

        public CalcModel()
        {
            ispercent = false;
        }

        public bool isPercentUsed
        {
            get { return ispercent; }
            set { ispercent = value; }
        }

        //finds out can this number be shorten to integer (4,999999999 = 5)
        private bool isFrInt(string x)
        {
            bool fl = true;
            int n = x.IndexOf(',');
            if(n>0)
            {
                for(int i=n+2;i<x.Length-1;i++)
                {
                    if (x[n+1] != x[i])
                        fl = false;
                }
                if (x[n+2] != '9' || (x.Length - n + 1) <=10 )
                    fl = false;
            }
            return fl;
        }

        //Rounds number to integer or leaves 12 digits after comma
        private string roundOut(double x)
        {
            if (isFrInt(x.ToString()))
                x = Math.Round(x);
            else
                x = Math.Round(x, 12);
            return x.ToString();
        }


        //sqrt
        public string root (string x)
        {
            double n=-1;
            try
            {
               n  = Convert.ToDouble(x);
            }
            catch(Exception e)
            {
                return "Type overflow";
            }
            
            if (n >= 0)
            {
                n = Math.Sqrt(n);
                return roundOut(n);
            }    
            else
                return "Invalid input";
        }
        
        // /
        public string div (string x, string y)
        {
            double op1 = -1;
            try
            {
                op1 = Convert.ToDouble(x);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double op2 = -1;
            try
            {
                op2 = Convert.ToDouble(y);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            if (op2 == 0)
                return "Can't divide by zero";
            else
            {
                double n = op1 / op2;
                return roundOut(n);
            }   
        }

        // %
        public string percent (string x, string y)
        {
            double op1 = -1;
            try
            {
                op1 = Convert.ToDouble(x);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double op2 = -1;
            try
            {
                op2 = Convert.ToDouble(y);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double n = op1 * (op2 / 100);
            return roundOut(n);
        }

        //*
        public string mult (string x, string y)
        {
            double op1 = -1;
            try
            {
                op1 = Convert.ToDouble(x);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double op2 = -1;
            try
            {
                op2 = Convert.ToDouble(y);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double n = op1 * op2;
            return roundOut(n);
        }

        // 1/x
        public string inv (string x)
        {
            double n = Convert.ToDouble(x);
            if (n != 0)
            {
                n = 1 / n;
                return roundOut(n);
            }   
            else
                return "Can't divide by zero";
        }

        //-
        public string minus (string x, string y)
        {
            double op1 = -1;
            try
            {
                op1 = Convert.ToDouble(x);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double op2 = -1;
            try
            {
                op2 = Convert.ToDouble(y);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double n = op1 - op2;
            return roundOut(n);
        }

        //+
        public string plus (string x, string y)
        {
            double op1 = -1;
            try
            {
                op1 = Convert.ToDouble(x);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double op2 = -1;
            try
            {
                op2 = Convert.ToDouble(y);
            }
            catch (Exception e)
            {
                return "Type overflow";
            }
            double n = op1 + op2;
            return roundOut(n);
        }
    }
}
