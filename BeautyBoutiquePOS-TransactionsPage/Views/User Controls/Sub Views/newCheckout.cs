using BeautyBoutiquePOS_TransactionsPage.Class;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views.Payments;
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

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views
{
    public partial class newCheckout : Form
    {
        private decimal netGross;
        Cash cashForm1 = new Cash(0);

        public newCheckout()
        {
            InitializeComponent();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridView1);
        }

        private void newCheckout_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            addToCart productForm = new addToCart();
            productForm.ShowDialog();
        }

        public void RefreshDataGrid()
        {
            string query = "SELECT * FROM productsLine";
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }

            CalculateNetGrossAmount();
        }


        private void CalculateNetGrossAmount()
        {
            decimal netGrossAmount = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["total"].Value != null && decimal.TryParse(row.Cells["total"].Value.ToString(), out decimal totalPrice))
                {
                    netGrossAmount += totalPrice;
                }
            }

            netammountText.Text = netGrossAmount.ToString();

            netGross = netGrossAmount;
        }

        private void DeleteAllProductsLineData()
        {
            string query = "DELETE FROM productsLine";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("All data from productsLine table deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No data found in productsLine table.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textGross.Text = this.cashForm1.balance.ToString();
            DeleteAllProductsLineData();
        }

        private void cashBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cashBtn_Click(object sender, EventArgs e)
        {
            Cash cashForm = new Cash(netGross);
            cashForm.ShowDialog();
            this.cashForm1 = cashForm;
        }

        private void cardBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
