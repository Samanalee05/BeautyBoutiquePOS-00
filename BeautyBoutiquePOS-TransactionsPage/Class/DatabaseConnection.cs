using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBoutiquePOS_TransactionsPage.Class
{
    internal class DatabaseConnection
    {
        private const string Server = "localhost";
        private const string Database = "test";
        private const string Username = "root";
        private const string Password = "root1234";


        public static string GetConnectionString()
        {
            return $"Server={Server};Database={Database};Uid={Username};Pwd={Password};";
        }
    }
}
