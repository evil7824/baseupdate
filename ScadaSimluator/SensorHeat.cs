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
        public void GetQtek()
        {
            Parametrs[Parametrs.Count - 2].val = (Parametrs[0].val - Parametrs[1].val) * (Parametrs[4].val) / 1000;
        }
        public void GetQsum(int timeinterval)
        {
            //TestSql(++num, spt.qsum + spt.q / 3600 * timeInterval / 1000);
            Parametrs[Parametrs.Count - 1].val = Parametrs[Parametrs.Count - 1].val + Parametrs[Parametrs.Count - 2].val / 3600 * timeinterval / 1000;
        }
        public override void SensorUpdate(int timeintevral)
        {
            for (int i = 0; i < Parametrs.Count - 2; i++)
            {
                Parametrs[i].Random();
            }
            GetQtek();
            GetQsum(timeintevral);
        }

    }
}
