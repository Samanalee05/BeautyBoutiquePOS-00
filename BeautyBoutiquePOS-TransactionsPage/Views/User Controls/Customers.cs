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
    public partial class Customers : UserControl
    {
        public Customers()
        {
            InitializeComponent();

            LoadCustomers();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(customerGridView);
            styles.RoundCornerPanels(panel1, 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newCustomer customerForm = new newCustomer(this);
            customerForm.ShowDialog();
        }

        public void LoadCustomers()
        {
            string query = "SELECT * FROM customers";

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

                        customerGridView.DataSource = dataTable;
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
