using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyBoutiquePOS_TransactionsPage.Class
{
    internal class DatabaseMigration
    {
        public static void MigrateDatabase() //use to migrate db to another database
        {
            string[] tableCheckQueries = {
            "CREATE TABLE IF NOT EXISTS`categories`  (`id` int NOT NULL AUTO_INCREMENT,`name` varchar(2555) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`description` varchar(2555) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,PRIMARY KEY (`id`) USING BTREE) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS`checkout`  (`id` int NOT NULL AUTO_INCREMENT,`customer` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`total` double NULL DEFAULT NULL,`discount` double NULL DEFAULT NULL,PRIMARY KEY (`id`) USING BTREE) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS `checkoutLine`  (`id` int NOT NULL AUTO_INCREMENT,`date` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`customer` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`total` double NULL DEFAULT NULL,`discount_percentage` double NULL DEFAULT NULL,`itemQTY` double NULL DEFAULT NULL,PRIMARY KEY (`id`) USING BTREE) ENGINE = InnoDB AUTO_INCREMENT = 1003 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS `customers`  (`nic` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,`name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`age` double NULL DEFAULT NULL,`address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`contact` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`email` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`Career` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`date_join` date NULL DEFAULT NULL,PRIMARY KEY (`nic`) USING BTREE) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS`inventory`  (`id` int NOT NULL AUTO_INCREMENT,`itemcode` int NULL DEFAULT NULL,`QTY` double NULL DEFAULT 0,`discount_percentage` double NULL DEFAULT 0,`selling_price` float NULL DEFAULT NULL,`cost` float NULL DEFAULT NULL,`total` float NULL DEFAULT NULL,PRIMARY KEY (`id`) USING BTREE) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS `neworder`  (`id` int NOT NULL AUTO_INCREMENT,`vendor` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`total` double NULL DEFAULT NULL,`date` datetime NULL DEFAULT NULL,PRIMARY KEY (`id`) USING BTREE) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS `products`  (`id` int NOT NULL AUTO_INCREMENT,`name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`qty` double NULL DEFAULT 0,`discount_percentage` double NULL DEFAULT 0,`price` float NULL DEFAULT NULL,`category` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,PRIMARY KEY (`id`) USING BTREE) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS `productsLine`  (`productsLineId` int NOT NULL AUTO_INCREMENT,`id` int NULL DEFAULT NULL,`name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`qty` double NULL DEFAULT NULL,`discount` double NULL DEFAULT NULL,`price` float NULL DEFAULT NULL,`total` float NULL DEFAULT NULL,PRIMARY KEY (`productsLineId`) USING BTREE) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
            "CREATE TABLE IF NOT EXISTS `users`  (`id` int NOT NULL AUTO_INCREMENT,`nic` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`age` int NULL DEFAULT NULL,`address` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`contact` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`username` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`password` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,`date_join` date NULL DEFAULT NULL,PRIMARY KEY (`id`) USING BTREE) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;",
        };

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {

                connection.Open();

                foreach (string query in tableCheckQueries)
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        int rows = command.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            Console.WriteLine("DB Update !");
                        }
                    }
                }
            }
        }
    }

}
