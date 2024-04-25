using BeautyBoutiquePOS_TransactionsPage.Class;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views
{
    public partial class editForm : Form
    {
        private Inventory inventory1;
        private double oldQty;
        private double sellValue;

        public editForm(Inventory inventory, string[] rowDataArray)
        {
            this.inventory1 = inventory;
            InitializeComponent();

            textBox1.ReadOnly = true;

            if (rowDataArray.Length > 0)
                textBox1.Text = rowDataArray[1]; 
            if (rowDataArray.Length > 1)
                textBoxQty.Text = rowDataArray[2];
            if (rowDataArray.Length > 2)
                textBoxSellingPrice.Text = rowDataArray[4];


            double parsedValue1;
            double oldSellValue;
            try
            {
                double.TryParse(textBoxQty.Text, out parsedValue1);
                oldQty = parsedValue1;

                double.TryParse(textBoxQty.Text, out oldSellValue);
                sellValue = oldSellValue;
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateProductQuantity(oldQty);

        }


        private void UpdateProductQuantity(double oldQty)
        {
            string productId = textBox1.Text;

            string query = "UPDATE products SET qty = qty - @OldQty WHERE id = @ProductId";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OldQty", oldQty);
                    command.Parameters.AddWithValue("@ProductId", productId);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            try
                            {
                                double newQty;

                                double.TryParse(textBoxQty.Text, out newQty);

                                UpdateProductNewQuantity(newQty);
                            } catch
                            {

                            }
                        }
                        else
                        {
                            MessageBox.Show("No product found with the specified ID.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void UpdateProductNewQuantity(double newQty)
        {
            string productId = textBox1.Text;
            double sellValue;

            if (!double.TryParse(textBoxSellingPrice.Text, out sellValue))
            {
                MessageBox.Show("Invalid selling price. Please enter a valid number.");
                return;
            }

            string query = "UPDATE products SET qty = qty + @NewQty, price = @SellingPrice WHERE id = @ProductId";


            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewQty", newQty);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@SellingPrice", sellValue);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product quantity and price updated successfully in products table.");
                            UpdateInventory(newQty);
                        }
                        else
                        {
                            MessageBox.Show("No product found with the specified ID in products table.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show
        ("Error updating product quantity: " + ex.Message);
                    }
                }


            }
        }

        private void UpdateInventory(double newQty)
        {

            string productId = textBox1.Text;
            double sellValue;

            if (!double.TryParse(textBoxSellingPrice.Text, out sellValue))
            {
                MessageBox.Show("Invalid selling price. Please enter a valid number.");
                return;
            }

            string inventoryQuery = "UPDATE inventory SET QTY = @NewQty, selling_price = @SellingPrice WHERE itemcode = @ProductId";
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {

                using (MySqlCommand command = new MySqlCommand(inventoryQuery, connection))
                {
                    command.Parameters.AddWithValue("@NewQty", newQty);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.Parameters.AddWithValue("@SellingPrice", sellValue);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product quantity and price updated successfully in inventory table.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No product found with the specified ID in inventory table.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error updating product quantity: " + ex.Message);
                    }
                }
            }




        }




        private void grForm_Load(object sender, EventArgs e)
        {

        }

    }
}
