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
        private String function;
        private Inventory InventoryForm;
        private string productName;
        public newOrder(Inventory inventory, string function1)
        {
            this.InventoryForm = inventory;
            this.function = function1;
            InitializeComponent();
            textBoxTotal.ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.function == "in")
            {
                InsertInventoryData("in");
                UpdateProductQuantity();

            } else if(this.function == "out")
            {
                InsertInventoryData("out");
                UpdateProductQuantityOut();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscount();
        }

        private void CalculateDiscount()
        {

            if (double.TryParse(costTextBox.Text, out double cost) &&
                double.TryParse(textBoxQty.Text, out double qty))
            {

                double totalCost = cost * qty;

                textBoxTotal.Text = totalCost.ToString();
            }
            else
            {
                textBoxTotal.Text = "Invalid input";
            }
        }
        private void InsertInventoryData(string function)
        {
            int itemcode = Convert.ToInt32(textBoxItemCode.Text);
            double qty = Convert.ToDouble(textBoxQty.Text);
            float cost = float.Parse(costTextBox.Text);
            float total = float.Parse(textBoxTotal.Text);

            string query = "INSERT INTO `inventory` (`itemcode`, `name`, `QTY`, `cost`, `total`, `function`) VALUES (@ItemCode, @Name, @Qty, @Cost, @Total,  @Function);";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemCode", itemcode);
                    command.Parameters.AddWithValue("@Name", productName);
                    command.Parameters.AddWithValue("@Qty", qty);
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@Total", total);
                    command.Parameters.AddWithValue("@Function", function);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            //MessageBox.Show("Data inserted successfully.");
                            this.InventoryForm.LoadNewOrders();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }



        private void UpdateProductQuantity()
        {
            double qty = Convert.ToDouble(textBoxQty.Text);
            int itemcode = Convert.ToInt32(textBoxItemCode.Text);


            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string updateQuery = @"UPDATE products p INNER JOIN inventory i ON p.id = i.itemcode SET p.qty = p.qty + @Qty WHERE i.itemcode = @ItemCode;";

            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
            {
                try
                {
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

        private void UpdateProductQuantityOut()
        {
            double qty = Convert.ToDouble(textBoxQty.Text);
            int itemcode = Convert.ToInt32(textBoxItemCode.Text);


            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string updateQuery = @"UPDATE products p  SET p.qty = p.qty - @Qty WHERE p.id = @ItemCode;";

            using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
            {
                try
                {
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
            textBoxQty.Text = "";
            costTextBox.Text = "";
            textBoxTotal.Text = "";
        }

        private void textBoxItemCode_MouseClick(object sender, MouseEventArgs e)
        {
            var selectProductForm = new SelectProduct("newOrder", this);
            selectProductForm.ShowDialog();
        }

        public void refreshFormData(String itemCode,String cost,String name)
        {
            textBoxItemCode.Text = itemCode.ToString();
            //textBoxQty.Text = QTY.ToString();
            costTextBox.Text = cost.ToString();
            productName = name;
        }

        private void newOrder_Load(object sender, EventArgs e)
        {

        }

        private void textBoxQty_TextChanged(object sender, EventArgs e)
        {
            CalculateDiscount();
        }
    }
}
