using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSimluator
{
    public class Sensor
    {
        public int sensortimer { get; set; }
        public List<SimpleParametr> Parametrs = new List<SimpleParametr>();
        public string sensorname;
        public Sensor(string sensorname)
        {
            this.sensorname = sensorname;
        }
        public Sensor(string sensorname, SimpleParametr parametr)
        {
            this.sensorname = sensorname;
            Parametrs.Add(parametr);
        }
        public Sensor(SimpleParametr parametr)
        {
            Parametrs.Add(parametr);
        }
        public virtual void SensorUpdate(int timeinterval)
        {
            foreach (SimpleParametr parametr in Parametrs)
            {

                parametr.Random();
            }
        }
        public string SensorToString()
        {
            string sensor = sensorname;
            foreach (SimpleParametr parametr in Parametrs)
            {
                sensor += "\n" + parametr.name + " " + parametr.cnlnum + " " + parametr.val;             
            }
            return sensor;
        }
        public void AddParametr(params SimpleParametr[] parametrs)
        {
            for (int i = 0; i < parametrs.Length; i++)
            {
                Parametrs.Add(parametrs[i]);
            }
        }
        public string CnlnumSum()
        {
            return Parametrs[Parametrs.Count - 1].cnlnum.ToString();
        }
    }
}
