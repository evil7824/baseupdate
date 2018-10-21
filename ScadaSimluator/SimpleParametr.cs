using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSimluator
{
    public class SimpleParametr
    {
        public string name;
        public int cnlnum { get; set; }
        public double val { get; set; }

        public double _val;

        public SimpleParametr(int cnlnum,double val, string name)
        {
            this.val = val;
            _val = val;
            this.cnlnum = cnlnum;
            this.name = name;
        }
        public void Random()
        {
            Random rnd = new Random();
            val = Math.Round(rnd.Next((int)(_val * 100 - 10), (int)(_val * 100 + 10)) * 0.01, 2);
        }
    }
}
