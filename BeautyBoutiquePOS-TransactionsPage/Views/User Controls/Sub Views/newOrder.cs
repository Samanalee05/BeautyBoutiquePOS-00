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

        private void btnSave_Click(object sender, EventArgs e) // save btn click
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

        private void InsertInventoryData(string function) // insert inventory transaction data to tbl
        {
            CalculateTotal();

            string itemcode = Convert.ToString(textBoxItemCode.Text);
            double qty = Convert.ToDouble(textBoxQty.Text);
            float cost = float.Parse(costTextBox.Text);

            

            string query = "INSERT INTO `inventory` (`id`, `name`, `QTY`, `cost`, `total`, `function`) VALUES (@ItemCode, @Name, @Qty, @Cost, @Total,  @Function);";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemCode", textBoxItemCode.Text);
                    command.Parameters.AddWithValue("@Name", productName);
                    command.Parameters.AddWithValue("@Qty", qty);
                    command.Parameters.AddWithValue("@Cost", cost);
                    command.Parameters.AddWithValue("@Total", textBoxTotal.Text);
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

        private void CalculateTotal()
        {
            double cost = Convert.ToDouble(costTextBox.Text);
            double qty = Convert.ToDouble(textBoxQty.Text);

            textBoxTotal.Text = Convert.ToString(cost * qty);
        }



        private void UpdateProductQuantity() // update product qty from product tble inside db after inventory in
        {
            double qty = Convert.ToDouble(textBoxQty.Text);
            string itemcode = Convert.ToString(textBoxItemCode.Text);


            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string updateQuery = @"UPDATE products p INNER JOIN inventory i ON p.id = i.id SET p.qty = p.qty + @Qty WHERE i.id = @ItemCode;";

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

        private void UpdateProductQuantityOut() // update product qty from product tble inside db after inventory out
        {
            double qty = Convert.ToDouble(textBoxQty.Text);
            string itemcode = Convert.ToString(textBoxItemCode.Text);


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


        private void button1_Click(object sender, EventArgs e) // cancel btn click
        {
            textBoxItemCode.Text = "";
            textBoxQty.Text = "";
            costTextBox.Text = "";
            textBoxTotal.Text = "";
        }

        private void textBoxItemCode_MouseClick(object sender, MouseEventArgs e) // textBoxItemCode click popup Select Product 
        {
            var selectProductForm = new SelectProduct("newOrder", this);
            selectProductForm.ShowDialog();
        }

        public void refreshFormData(String itemCode,String cost,String name) // set selected product data to text box
        {
            textBoxItemCode.Text = itemCode.ToString();
            //textBoxQty.Text = QTY.ToString();
            costTextBox.Text = cost.ToString();
            productName = name;
        }

    }
}
