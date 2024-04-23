using BeautyBoutiquePOS_TransactionsPage.Class;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views;
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
    public partial class Inventory : UserControl
    {
        public Inventory()
        {
            InitializeComponent();
            LoadNewOrders();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newOrder newOrderForm = new newOrder(this);
            newOrderForm.ShowDialog();
        }

        public void LoadNewOrders()
        {
            try
            {
                string queryString = "SELECT * FROM newOrder";

                using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
                {

                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(queryString, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {

                            DataTable dataTable = new DataTable();

                            adapter.Fill(dataTable);

                            dataGridView2.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading new orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
