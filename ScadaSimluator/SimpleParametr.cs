using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSimluator
{
    public class SimpleParametr
    {
        public string name { get; set; }
        public int cnlnum { get; set; }
        public double val { get; set; }

        public double _val;

        public string type { get; set; }//или задаваемый(set),расчетный( rated), итоговая сумма(sum)

        public SimpleParametr(int cnlnum,double val, string name,string type)
        {
            this.val = val;
            _val = val;
            this.cnlnum = cnlnum;
            this.name = name;
            this.type = type;
        }
        public void Random()
        {
            Random rnd = new Random();
            val = Math.Round(rnd.Next((int)(_val * 100 - 10), (int)(_val * 100 + 10)) * 0.01, 2);
        }
    }
}
