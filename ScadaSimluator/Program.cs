using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace ScadaSimluator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Коннектимся...");
            MySqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                Console.WriteLine("Соединение установлено");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            try
            {
                string sql = "INSERT INTO cnldata (datetime, cnlnum, val, stat) VALUES(@dateTime, @cnlNum, @val, @stat) ON DUPLICATE KEY UPDATE datetime = @dateTime,  val = @val";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;

                MySqlParameter dateTimeParam = new MySqlParameter("@dateTime", MySqlDbType.String);
                dateTimeParam.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //dataTimeParam.Value = "2012-10-12 22:22:22";
                cmd.Parameters.Add(dateTimeParam);

                MySqlParameter cnlNumParam = new MySqlParameter("@cnlNum", MySqlDbType.Int32);
                cnlNumParam.Value = 601;
                cmd.Parameters.Add(cnlNumParam);

                MySqlParameter valParam = new MySqlParameter("@val", MySqlDbType.Double);
                valParam.Value = 95;
                cmd.Parameters.Add(valParam);

                cmd.Parameters.Add("@stat", MySqlDbType.Int16).Value = 1;

                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
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

            Console.Read();



        }
    }
}
