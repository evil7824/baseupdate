using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSimluator
{
    class Spt
    {

        public int cnlnum {get;set;}
        public double t1 { get; set;}
        public double t2 { get; set;}
        public double p1 { get; set;}
        public double p2 { get; set;}
        public double g1 { get; set;}
        public double g2 { get; set;}
        public double q { get; set;}
        public double qsum { get; set;}
        public Spt (int cnlnum) { this.cnlnum = cnlnum;}
        public Spt(int cnlnum, double t1, double t2, double p1, double p2, double g1)
        {
            this.cnlnum = cnlnum;
            this.t1 = t1;
            this.t2 = t2;
            this.p1 = p1;
            this.p2 = p2;
            this.g1 = g1;
            this.g2 = g2;
        }
        public void Random()
        {
            Random rnd = new Random();
            t1 = Math.Round(rnd.Next((int)(t1 * 100 - 10), (int)(t1 * 100 + 10)) * 0.01,2);
            t2 = Math.Round(rnd.Next((int)(t2 * 100 - 10), (int)(t2 * 100 + 10)) * 0.01,2);
            p1 = Math.Round(rnd.Next((int)(p1 * 100 - 10), (int)(p1 * 100 + 10)) * 0.01,2);
            p2 = Math.Round(rnd.Next((int)(p2 * 100 - 10), (int)(p2 * 100 + 10)) * 0.01,2);
            g1 = Math.Round(rnd.Next((int)(g1 * 100 - 10), (int)(g1 * 100 + 10)) * 0.01,2);
            g2 = Math.Round(g1-0.1,2);
            q = Math.Round(g1 * (t1 - t2) / 1000, 3);
        }
        public string ToList()
        {
            return "clnum= " + cnlnum + "\nt1= " + t1 + "\nt2= " + t2 + "\np1= " + p1 + "\np2= " + p2 + "\ng1= " + g1 + "\ng2= " + g2 + "\nq= " + q + "\nqsum= "+qsum;
        }
        


    }
}
