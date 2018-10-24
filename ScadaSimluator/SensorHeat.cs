using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScadaSimluator
{
    public class SensorHeat: Sensor
    {
        public SensorHeat(string sensorname) : base(sensorname)
        {
            this.sensorname = sensorname;
        }
        public SensorHeat(string sensorname, SimpleParametr parametr) : base(sensorname,parametr)
        {
            this.sensorname = sensorname;
            Parametrs.Add(parametr);
        }
        public SensorHeat(SimpleParametr parametr):base(parametr)
        {
            Parametrs.Add(parametr);
        }
        //функция расчета текущего расхода
        public void GetALLQ(int timeinterval)
        {
            Parametrs[Parametrs.Count - 2].val = (Parametrs[0].val - Parametrs[1].val) * (Parametrs[4].val) / 1000;
            Parametrs[Parametrs.Count - 1].val = Parametrs[Parametrs.Count - 1].val + Parametrs[Parametrs.Count - 2].val / 3600 * timeinterval / 1000;
        }
        //функция расчета суммарного
       
        //функция обновления параметров кроме двух последних, которые расчитываются
        public override void SensorUpdate(int timeintevral)
        {
            for (int i = 0; i < Parametrs.Count - 2; i++)
            {
                Parametrs[i].Random();
            }           
            GetALLQ(timeintevral);
        }

    }
}
