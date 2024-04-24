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
            string query = "SELECT date, COUNT(*) AS checkoutCount FROM checkoutLine GROUP BY date";

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

                        chart2.Series.Add("Checkouts");

                        chart2.Series["Checkouts"].XValueMember = "date";
                        chart2.Series["Checkouts"].YValueMembers = "checkoutCount";
                        chart2.DataSource = dataTable;
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
