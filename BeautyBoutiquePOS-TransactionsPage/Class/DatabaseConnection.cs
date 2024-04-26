using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBoutiquePOS_TransactionsPage.Class
{
    internal class DatabaseConnection
    {
        private const string Server = "mysql-15151bc9-samanala-e089.d.aivencloud.com";
        private const int Port = 11450;
        private const string Database = "defaultdb";
        private const string Username = "avnadmin";
        private const string Password = "AVNS_k-Wrpa4HswdWcem3LB_";


        public static string GetConnectionString()
        {
            return $"Server={Server};Port={Port};Database={Database};Uid={Username};Pwd={Password};";
        }
    }
}
