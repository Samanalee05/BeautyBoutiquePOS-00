using BeautyBoutiquePOS_TransactionsPage.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls
{
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
            PopulatePieChart();
            PopulateAreaChart();
        }

        private void PopulatePieChart()
        {
            chart1.Series.Clear();

            Series series = new Series("ProductQuantities");
            series.ChartType = SeriesChartType.Pie;
            series["PieLabelStyle"] = "Disabled";



            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                string query = "SELECT name, qty, id FROM products";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string productName = reader["name"].ToString();
                    int quantity = Convert.ToInt32(reader["qty"]);
                    series.Points.AddXY(productName, quantity);
                }
            }
            chart1.Series.Add(series);
        }

        private void PopulateAreaChart()
        {
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea("ChartArea");

            chart2.ChartAreas.Add(chartArea);

            Series series = new Series("Total By Day");
            series.ChartType = SeriesChartType.Area;

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                string query = "SELECT date, SUM(total) AS total FROM checkoutLine GROUP BY date";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string date = reader["date"].ToString();
                    double total = Convert.ToDouble(reader["total"]);
                    series.Points.AddXY(date, total);
                }
            }
            chart2.Series.Add(series);
        }
    }
}
