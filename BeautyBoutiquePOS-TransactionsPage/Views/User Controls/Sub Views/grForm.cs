using BeautyBoutiquePOS_TransactionsPage.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views
{
    public partial class grForm : Form
    {
        private Inventory inventory1;
        public grForm(Inventory inventory)
        {
            this.inventory1 = inventory;
            InitializeComponent();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertDataIntoGRTable();
        }

        private void grForm_Load(object sender, EventArgs e)
        {

        }

        private void InsertDataIntoGRTable()
        {
            decimal quantity = 0;
            decimal price = 0;

            try
            {
                DateTime receivedDate = DateTime.Now.Date;

                if (!string.IsNullOrEmpty(priceTextBox.Text) && decimal.TryParse(priceTextBox.Text, out price))
                {
                    
                }
                if (!string.IsNullOrEmpty(qtyTextBox.Text) && decimal.TryParse(qtyTextBox.Text, out quantity))
                {
                    
                }

                decimal total = quantity * price;


                string queryString = "INSERT INTO gr (product_name, quantity, price, total, vendor, received_date) " +
                                     "VALUES (@ProductName, @Quantity, @Price, @Total, @Vendor, @ReceivedDate)";

                using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
                {

                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", textProductName.Text);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Total", total);
                        command.Parameters.AddWithValue("@Vendor", vendorNameTextBox.Text);
                        command.Parameters.AddWithValue("@ReceivedDate", receivedDate);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Data inserted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.inventory1.LoadDataIntoDataGridView();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data into gr table: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
