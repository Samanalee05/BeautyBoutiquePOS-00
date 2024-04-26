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
            InsertInventoryData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscount();
        }

        private void CalculateDiscount()
        {

            if (double.TryParse(costTextBox.Text, out double cost) &&
                double.TryParse(textBoxQty.Text, out double qty) &&
                double.TryParse(textBoxDiscount.Text, out double discountPercentage))
            {

                double totalCost = cost * qty;

                double discountAmount = totalCost * (discountPercentage / 100);

                double total = totalCost - discountAmount;

                textBoxTotal.Text = total.ToString();
            }
            else
            {
                textBoxTotal.Text = "Invalid input";
            }
        }

        private void InsertInventoryData()
        {
            int itemcode = Convert.ToInt32(textBoxItemCode.Text);
            double qty = Convert.ToDouble(textBoxQty.Text);
            double discount = Convert.ToDouble(textBoxDiscount.Text);
            float sellingPrice = float.Parse(textBoxSellingPrice.Text);
            float cost = float.Parse(costTextBox.Text);
            float total = float.Parse(textBoxTotal.Text);


            string query = "INSERT INTO inventory (itemcode, QTY, discount_percentage, selling_price, cost , total) VALUES (@ItemCode, @Qty, @Discount, @SellingPrice, @Cost , @Total)";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
 
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemCode", itemcode);
                    command.Parameters.AddWithValue("@Qty", qty);
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@Total", total);

                    try
                    {

                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            if (checkProductExists())
                            {
                                MessageBox.Show("Data inserted successfully.");
                                this.InventoryForm.LoadNewOrders();
                            }
                            else
                            {
                                MessageBox.Show("Failed to insert data.");
                            }

                        }

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void textBoxDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscount();
        }

        private void textBoxQty_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscount();
        }

        private void UpdateProductQuantity()
        {
            double qty = Convert.ToDouble(textBoxQty.Text);
            int itemcode = Convert.ToInt32(textBoxItemCode.Text);

            if (!decimal.TryParse(textBoxSellingPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid cost value.");
                return;
            }

            if (!decimal.TryParse(textBoxDiscount.Text, out decimal newDiscountPercentage))
            {
                MessageBox.Show("Invalid discount value.");
                return;
            }

            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string updateQuery = @"UPDATE products p INNER JOIN inventory i ON p.id = i.itemcode SET p.qty = p.qty + @Qty, p.discount_percentage = @newDiscountPercentage, p.price = @newPrice WHERE i.itemcode = @ItemCode;";

            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@newDiscountPercentage", newDiscountPercentage);
                    command.Parameters.AddWithValue("@newPrice", price);
                    command.Parameters.AddWithValue("@Qty", qty);
                    command.Parameters.AddWithValue("@ItemCode", itemcode);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //MessageBox.Show("Product quantities updated successfully.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No rows were updated.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error updating product quantities: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }



        private Boolean checkProductExists()
        {
            int itemcode = Convert.ToInt32(textBoxItemCode.Text);

            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string query = "SELECT * FROM   products WHERE id = @productId;";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@productId", itemcode);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UpdateProductQuantity();

                        return true;
                    } else
                    {
                        MessageBox.Show("Add Product To Product Table First!");

                        return false;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxItemCode.Text = "";
            textBoxDiscount.Text = "";
            textBoxQty.Text = "";
            textBoxSellingPrice.Text = "";
            costTextBox.Text = "";
            textBoxTotal.Text = "";
        }
    }
}
