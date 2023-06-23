using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace calc
{
    public class Calc
    {
        public string PercentSumDiff(string x, string y)
        {
            return (Convert.ToDouble(x) * Convert.ToDouble(y) / 100).ToString();
        }
        public string PercentMultDiv(string y)
        {
            return (Convert.ToDouble(y) / 100).ToString();
        }
        public string Sum(string x, string y)
        {
            return (Math.Round(Convert.ToDouble(x) + Convert.ToDouble(y), 14)).ToString();
        }
        public string Diff(string x, string y)
        {
            return (Math.Round(Convert.ToDouble(x) - Convert.ToDouble(y), 14)).ToString();
        }
        public string Mult(string x, string y)
        {
            return (Math.Round(Convert.ToDouble(x) * Convert.ToDouble(y), 14)).ToString();
        }
        public string Div(string x, string y)
        {
            return (Math.Round(Convert.ToDouble(x) / Convert.ToDouble(y), 14)).ToString();
        }
        public string Reverse(string x)
        {
            return (1 / Convert.ToDouble(x)).ToString();
        }
        public string Square(string x)
        {
            return (Convert.ToDouble(x) * Convert.ToDouble(x)).ToString();
        }
        public string Root(string x)
        {
            return Math.Sqrt(Convert.ToDouble(x)).ToString();
        }
        public string PosNeg(string x)
        {
            if (x[0] == '-')
            {
                return x.Substring(1);
            }
            else
            {
                return "-" + x;
            }
        }
    }
}
