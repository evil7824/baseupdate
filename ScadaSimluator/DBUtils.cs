using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ScadaSimluator
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "scada_test";
            string username = "root";
            string password = "mavric";
            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
        
        
    }
}
