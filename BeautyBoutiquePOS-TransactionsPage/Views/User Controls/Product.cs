using BeautyBoutiquePOS_TransactionsPage.Class;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views;
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

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls
{
    public partial class Product : UserControl
    {
        public Product()
        {
            InitializeComponent();
            LoadProductData();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridViewProducts);
            styles.RoundCornerPanels(panel1, 10);
            //styles.RoundedBtn(button1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newProduct productForm = new newProduct(this);
            productForm.ShowDialog();
        }

        public void LoadProductData()
        {
            string query = "SELECT id, name, description, discount_percentage, price, category FROM products";

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

                        dataGridViewProducts.DataSource = dataTable;
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
