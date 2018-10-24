using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ScadaSimluator;
using System.Threading;


namespace ScadaSimluator
{
    class Program
    {
        public static MySqlConnection conn = DBUtils.GetDBConnection();
        public static int timeInterval = 5000;
        static void Main(string[] args)
        {
            conn = DBUtils.GetDBConnection();
            conn.Open();
     
            //создаем вычислитель
            SensorHeatGVS Spt1 = new SensorHeatGVS("spt1");
            // устанавливаем Параметры для счетчика
            SimpleParametr spt1_p1 = new SimpleParametr(1, 5, "p1","set");
            SimpleParametr spt1_p2 = new SimpleParametr(2, 4, "p2", "set");
            SimpleParametr spt1_p3 = new SimpleParametr(3, 2, "p3", "set");
            SimpleParametr spt1_p4 = new SimpleParametr(4, 2, "p4", "set");
            SimpleParametr spt1_g1 = new SimpleParametr(5, 8.5, "g1", "set");
            SimpleParametr spt1_g2 = new SimpleParametr(6, 8.2, "g2", "set");
            SimpleParametr spt1_g3 = new SimpleParametr(7, 3, "g3", "set");
            SimpleParametr spt1_g4 = new SimpleParametr(8, 1, "g4", "set");

            SimpleParametr spt1_t1 = new SimpleParametr(9, 90, "t1", "set");
            SimpleParametr spt1_t2 = new SimpleParametr(10, 75, "t2", "set");
            SimpleParametr spt1_t3 = new SimpleParametr(11, 65, "t3", "set");
            SimpleParametr spt1_t4 = new SimpleParametr(12, 62, "t4", "set");

         
            SimpleParametr spt1_q_ot = new SimpleParametr(13, 0, "q_ot","rated");
            SimpleParametr spt1_q_gvs = new SimpleParametr(14, 0, "q_gvs","rated");
            SimpleParametr spt1_Q_ot = new SimpleParametr(15, 0, "Q_ot", "sum");
            SimpleParametr spt1_Q_gvs = new SimpleParametr(16, 1, "Q_gvs", "sum");
            
            //добавляем параметры в счетчик
            Spt1.AddParametr(spt1_p1,spt1_p2, spt1_p3,spt1_p4, spt1_g1,spt1_g2,spt1_g3,spt1_g4,spt1_t1,spt1_t2,spt1_t3,spt1_t4,spt1_q_ot,spt1_q_gvs,spt1_Q_ot,spt1_Q_gvs);
            //получаем qsum из базы
            //SensorsToDB(Spt1);
            Spt1.SetSensorSum(GetQsumm(Spt1));


            conn.Close();
            conn.Dispose();
          

           ConsoleKeyInfo cki = new ConsoleKeyInfo();
           while (true)
           {
                conn.Open();
                Console.WriteLine("итерация");

                Spt1.SensorUpdate(timeInterval);
                SensorsToDB(Spt1);

                conn.Close();
                conn.Dispose();
               
                Console.WriteLine(Spt1.SensorToString());

                
                Thread.Sleep(timeInterval);

                if (Console.KeyAvailable == true)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Q)
                    {
                        Console.WriteLine("Выход из потока");
                        conn.Close();
                        conn.Dispose();
                        conn = null;
                        break;
                    }
                }
           }
          
            Console.Read();
        }

        //функиця получения текущего значения суммы
        public static double[] GetQsumm(Sensor sensor)
        {
            double[] d = new double[sensor.cnlsumtotal];
            for (int i = 0; i < sensor.cnlsumtotal; i++)
            {
                string sql = "SELECT val FROM cnldata WHERE cnlnum=" + sensor.CnlnumSum()[i];
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                string name = cmd.ExecuteScalar().ToString();
                d[i]= Convert.ToDouble(cmd.ExecuteScalar().ToString());
            }
            return d;
        }
        //Занесение данных сенсора в базу данных 
        public static void SensorsToDB(params Sensor[] sensors)
        {
                foreach (Sensor sensor in sensors)
                {
                    foreach (SimpleParametr parametr in sensor.Parametrs)
                    {
                        ParamToDB(parametr.cnlnum, parametr.val);
                    }
                }

        }
        //занесение каждого параметра в базу
        public static void ParamToDB(int cnlnum, double val)
            {
                    string sql = "INSERT INTO cnldata (datetime, cnlnum, val, stat) VALUES(@dateTime, @cnlNum, @val, @stat) ON DUPLICATE KEY UPDATE datetime = @dateTime,  val = @val";
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;

                    MySqlParameter dateTimeParam = new MySqlParameter("@dateTime", MySqlDbType.String);
                    dateTimeParam.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    cmd.Parameters.Add(dateTimeParam);

                    MySqlParameter cnlNumParam = new MySqlParameter("@cnlNum", MySqlDbType.Int32);
                    cnlNumParam.Value = cnlnum;
                    cmd.Parameters.Add(cnlNumParam);

                    MySqlParameter valParam = new MySqlParameter("@val", MySqlDbType.Double);
                    valParam.Value = val;
                    cmd.Parameters.Add(valParam);

                    cmd.Parameters.Add("@stat", MySqlDbType.Int16).Value = 1;

                    int rowCount = cmd.ExecuteNonQuery();
             
            }  
    }
}
