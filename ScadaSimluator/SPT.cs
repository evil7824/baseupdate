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
        public Spt(int cnlnum, double t1, double t2, double p1, double p2, double g1, double g2, double q, double qsum)
        {
            this.cnlnum = cnlnum;
            this.t1 = t1;
            this.t2 = t2;
            this.p1 = p1;
            this.p2 = p2;
            this.g1 = g1;
            this.g2 = g2;
            this.q = q;
            this.qsum = qsum;
        }
    }
}
