﻿using BeautyBoutiquePOS_TransactionsPage.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

            LoadCheckoutData();
            LoadCustomerJoinData();
        }

        private void LoadCheckoutData()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            string query = "SELECT date, COUNT(*) AS checkoutCount " +
                           "FROM checkoutLine " +
                           "WHERE date >= @FirstDayOfMonth AND date <= @LastDayOfMonth " +
                           "GROUP BY date " +
                           "ORDER BY date ASC";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@FirstDayOfMonth", firstDayOfMonth);
                        command.Parameters.AddWithValue("@LastDayOfMonth", lastDayOfMonth);

                        connection.Open();

                        DataTable dataTable = new DataTable();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        chart2.Series.Clear();

                        Series series = new Series("Sales");
                        series.ChartType = SeriesChartType.Column;
                        series["PixelPointWidth"] = "17";
                        series.XValueMember = "date";
                        series.YValueMembers = "checkoutCount";
                        chart2.Series.Add(series);

                        chart2.DataSource = dataTable;
                        chart2.ChartAreas[0].AxisX.Title = "Date";
                        chart2.ChartAreas[0].AxisY.Title = "Checkout Count";
                        chart2.DataBind();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void LoadCustomerJoinData()
        {
            DateTime currentDate = DateTime.Now;
            string currentMonth = currentDate.ToString("yyyy-MM");

            string query = "SELECT date_join, COUNT(*) AS customerCount " +
                           "FROM customers " +
                           "WHERE DATE_FORMAT(date_join, '%Y-%m') = @CurrentMonth " +
                           "GROUP BY date_join " +
                           "ORDER BY date_join ASC";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@CurrentMonth", currentMonth);

                        connection.Open();

                        DataTable dataTable = new DataTable();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        chart1.Series.Clear();

                        Series series = new Series("Customers");
                        series.ChartType = SeriesChartType.Column;
                        series["PixelPointWidth"] = "17";
                        series.XValueMember = "date_join";
                        series.YValueMembers = "customerCount";
                        chart1.Series.Add(series);

                        chart1.DataSource = dataTable;
                        chart1.ChartAreas[0].AxisX.Title = "Date";
                        chart1.ChartAreas[0].AxisY.Title = "Customer Count";
                        chart1.DataBind();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }




    }
}
