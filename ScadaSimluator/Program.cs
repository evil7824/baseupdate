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
        //public static MySqlConnection conn = DBUtils.GetDBConnection();
        public static int timeInterval = 5000;
        static void Main(string[] args)
        {
            //Spt spt1 = new Spt(601, 90, 70, 5, 4, 6);
            //Console.WriteLine(spt1.ToList());
            //подключение к Базе данных

            // устанавливаем Параметры
            SimpleParametr spt1_t1 = new SimpleParametr(1, 90,"t1");
            SimpleParametr spt1_t2 = new SimpleParametr(2, 75, "t2");
            SimpleParametr spt1_p1 = new SimpleParametr(3, 5, "p1");
            SimpleParametr spt1_p2 = new SimpleParametr(4, 4, "p2");
            SimpleParametr spt1_g1 = new SimpleParametr(5, 8.5, "g1");
            SimpleParametr spt1_g2 = new SimpleParametr(6, 8.2, "g2");
            SimpleParametr spt1_qt = new SimpleParametr(7, 0, "q");
            SimpleParametr spt1_qsum = new SimpleParametr(8, 1, "qsum");

           // Console.WriteLine("Коннектимся...");
            SensorHeat spt1 = new SensorHeat("spt1");
            spt1.AddParametr(spt1_t1, spt1_t2, spt1_p1, spt1_p2,spt1_g1,spt1_g2,spt1_qt,spt1_qsum);
            

            ConsoleKeyInfo cki = new ConsoleKeyInfo();
           while (true)
            {
                Console.WriteLine("итерация");
                // conn = DBUtils.GetDBConnection();
                //  conn.Open();
                spt1.SensorUpdate(timeInterval);
                Console.WriteLine(spt1.SensorToString());

                // conn.Close();
                // conn.Dispose();
                // conn = null;
                Thread.Sleep(timeInterval);

                if (Console.KeyAvailable == true)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Q)
                    {
                        Console.WriteLine("Выход из потока");
                        break;

                    }
                }

            }
            



 /*           try
            {
               // conn.Open();
                Console.WriteLine("Соединение установлено");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            try
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
*/
            Console.Read();
        }



        //Старые методы для прошлой версии
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
            public static void ParamToDB(int cnlnum, double val)
            {/*
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

                    //Console.WriteLine("Row Count affected = " + rowCount);
                    */

            }
            public static double GetQsumm(Sensor sensor)
            {               
                string sql = "SELECT val FROM cnldata WHERE cnlnum=" + sensor.CnlnumSum();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                string name = cmd.ExecuteScalar().ToString();
                return Convert.ToDouble(cmd.ExecuteScalar().ToString());
            }

   /*         public static void SptTobBase(Spt spt)
            {
                int num = spt.cnlnum; 
                TestSql(num, spt.t1);
                TestSql(++num, spt.t2);
                TestSql(++num, spt.p1);
                TestSql(++num, spt.p2);
                TestSql(++num, spt.g1);
                TestSql(++num, spt.g2);
                TestSql(++num, spt.q);
                TestSql(++num, spt.qsum+spt.q/3600*timeInterval/1000);
            }

        */
    }
}
