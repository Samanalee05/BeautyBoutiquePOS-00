using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBoutiquePOS_TransactionsPage.Class
{
    internal class mysql
    {
        public static MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());
        public static MySqlCommand command = new MySqlCommand();
        public static DataTable table;
        public static MySqlDataAdapter adapter;
        public static MySqlDataReader reader;
        public static DataTable DataTable;
        public static DataSet DataSet;

        public static DataTable GetData(string query)
        {
           DataTable dt = new DataTable();

            using(adapter = new MySqlDataAdapter(query,connection))
            {
               adapter.Fill(dt);
            }
            return dt;
        }

        public static DataSet Data(string query,string tbl)
        {
            DataSet ds = new DataSet();
            using (adapter = new MySqlDataAdapter(query,connection))
            {
                adapter.Fill(ds,tbl);
            }

            return ds;
        }
    }
}
