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

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views
{
    public partial class newOrder : Form
    {
        private Inventory InventoryForm;
        public newOrder(Inventory inventory)
        {
            this.InventoryForm = inventory;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
 
                DateTime orderDate = DateTime.Now;

                string vendorName = vendorNameTextBox.Text;

                if (!double.TryParse(totalAmountTextBox.Text, out double totalAmount))
                {
                    MessageBox.Show("Invalid total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO newOrder (vendor, total, date) VALUES (@CustomerName, @TotalAmount, @OrderDate)";
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerName", vendorName);
                        command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                        command.Parameters.AddWithValue("@OrderDate", orderDate);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("New order saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.InventoryForm.LoadNewOrders();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving new order: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
