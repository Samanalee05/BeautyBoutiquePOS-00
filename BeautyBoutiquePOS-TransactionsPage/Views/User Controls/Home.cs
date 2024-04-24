using BeautyBoutiquePOS_TransactionsPage.Class;
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
            
        }

        private void LoadCheckoutData()
        {
            string query = "SELECT date, COUNT(*) AS checkoutCount FROM checkoutLine GROUP BY date ORDER BY date ASC";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        DataTable dataTable = new DataTable();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }

                        chart2.Series.Clear();

                        chart2.Series.Add("Sales");

                        chart2.Series["Sales"].XValueMember = "date";
                        chart2.Series["Sales"].YValueMembers = "checkoutCount";
                        chart2.DataSource = dataTable;

                        chart2.Series.Clear();
                        Series series = new Series("Sales");


                        series.ChartType = SeriesChartType.Column;

                        series["PixelPointWidth"] = "20";


                        series.XValueMember = "date";
                        series.YValueMembers = "checkoutCount";
                        chart2.DataSource = dataTable;
                        chart2.Series.Add(series);

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

    }
}
