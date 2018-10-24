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
        
        public int cnlsumtotal { get; set; }// общее количество сумм
        public Sensor(string sensorname)
        {
            this.sensorname = sensorname;
            
        }
        public Sensor(string sensorname, SimpleParametr parametr)
        {
            this.sensorname = sensorname;
            Parametrs.Add(parametr);
            if (parametr.type == "sum") cnlsumtotal++;
            
        }
        public Sensor(SimpleParametr parametr)
        {
            Parametrs.Add(parametr);
            if (parametr.type == "sum") cnlsumtotal++;
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
                if (parametrs[i].type == "sum") cnlsumtotal++;
            }
        }
        public string[] CnlnumSum()
        {
            var parametrs = Parametrs.Where(x => x.type == "sum");

            string[] s = new string[parametrs.Count()];
            int i = 0;
            foreach (SimpleParametr parametr in parametrs)
            {
                s[i] = parametr.cnlnum.ToString();
                i++;
            }
            return s;
        }
        public void SetSensorSum(double[] sensorssum)
        {
            var parametrs = Parametrs.Where(x => x.type == "sum");
            int i = 0;
            foreach (SimpleParametr parametr in parametrs)
            {
                parametr.val = sensorssum[i];
                i++;
            }
        }
    }
}
