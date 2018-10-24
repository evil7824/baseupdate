using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSimluator
{
    class SensorHeatGVS: Sensor
    {
        public SensorHeatGVS(string sensorname) : base(sensorname)
        {
            this.sensorname = sensorname;
        }
        public SensorHeatGVS(string sensorname, SimpleParametr parametr) : base(sensorname, parametr)
        {
            this.sensorname = sensorname;
            Parametrs.Add(parametr);
            if (parametr.type == "sum") cnlsumtotal++;
        }
        public SensorHeatGVS(SimpleParametr parametr) : base(parametr)
        {
            Parametrs.Add(parametr);
            if (parametr.type == "sum") cnlsumtotal++;
        }
        //расчет текущей нагрузки на отопление,гвс и прибавление к суммарным
        public void GetALLQ(int timeinterval)
        {
            Parametrs[12].val = (Parametrs[8].val - Parametrs[9].val) * (Parametrs[4].val) / 1000;
            Parametrs[13].val = (Parametrs[10].val - Parametrs[11].val) * (Parametrs[6].val - Parametrs[7].val) / 1000;
            Parametrs[14].val = Parametrs[14].val + Parametrs[12].val / 3600 * timeinterval / 1000;
            Parametrs[15].val = Parametrs[15].val + Parametrs[13].val / 3600 * timeinterval / 1000;
        }          
        public override void SensorUpdate(int timeinterval)
        {
            var updateparams = Parametrs.Where(x => x.type == "set");
            //Parametrs.FirstOrDefault(x => x.type == "set");
            foreach (SimpleParametr parametr in updateparams)
            {
                parametr.Random();
            }
            GetALLQ(timeinterval);
        }     
    }
}
