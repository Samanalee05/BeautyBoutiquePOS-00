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
    public partial class addToCart : Form
    {
        DataTable dataTable1;
        DataView dataView1;
        private string form1;
        private newCheckout newCheckout1;

        public addToCart(string form, newCheckout newCheckout)
        {
            this.newCheckout1 = newCheckout;

            InitializeComponent();
            UpdateDataGridView();

            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "Select";
            editButtonColumn.Text = "Select";
            editButtonColumn.Name = "SelectButton";
            editButtonColumn.UseColumnTextForButtonValue = true;
            productGridView.Columns.Add(editButtonColumn);

            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(productGridView);
            this.form1 = form;

            productGridView.CellContentClick += dataGridView1_CellContentClick;
        }

        private void textProduct_TextChanged(object sender, EventArgs e)
        {
            string filter = textProduct.Text;
            if (!string.IsNullOrEmpty(filter))
            {
                DataView dv = new DataView(dataTable1);

                if (int.TryParse(filter, out int idFilter))
                {
                    dv.RowFilter = string.Format("id = {0}", idFilter);
                }
                else
                {
                    dv.RowFilter = string.Format("name LIKE '%{0}%'", filter);
                }
                productGridView.DataSource = dv;
                dataView1 = dv;
            }
            else
            {
                productGridView.DataSource = dataTable1;
            }
        }


        public void UpdateDataGridView()
        {
            string query = "SELECT * FROM products";

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

                        productGridView.DataSource = dataTable;
                        dataTable1 = dataTable;

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == productGridView.Columns["SelectButton"].Index)
                {

                    DataGridViewRow row = productGridView.Rows[e.RowIndex];

                    Console.WriteLine(row.Cells[4].Value);

                   CalculateDiscountPerItem(row.Cells[2].Value.ToString(), row.Cells[5].Value.ToString());

                    using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
                    {
                        connection.Open();

                        int quantity;

                        if (!int.TryParse(textQTY.Text, out quantity))
                        {
                            MessageBox.Show("Please enter a valid integer value for quantity.");
                            return;
                        }
                        else
                        {
                            quantity = Convert.ToInt32(textQTY.Text);
                        }

                        if (quantity <= 0)
                        {
                            MessageBox.Show("Please enter a valid quantity.");
                            return;
                        }



                        if (CheckStockQuantity(row.Cells[2].Value.ToString(), quantity))
                        {
                            decimal discountPrice = CalculateDiscountPrice(row.Cells[2].Value.ToString());
                            decimal totalPrice = discountPrice * quantity;


                            string query = "INSERT INTO productsLine (id ,name, description, qty, discount, price , total) VALUES (@Id ,@Name, @Description, @Qty, @Discount,@Price , @Total)";
                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Id", row.Cells[1].Value);
                                command.Parameters.AddWithValue("@Name", row.Cells[2].Value);
                                command.Parameters.AddWithValue("@Description", row.Cells[3].Value);
                                command.Parameters.AddWithValue("@Qty", Convert.ToInt32(textQTY.Text));
                                command.Parameters.AddWithValue("@Discount", row.Cells[5].Value);
                                command.Parameters.AddWithValue("@Price", row.Cells[6].Value);
                                command.Parameters.AddWithValue("@Total", totalPrice);
                                command.ExecuteNonQuery();
                                
                            }

                            if (Application.OpenForms["newCheckout"] is newCheckout checkoutForm)
                            {
                                checkoutForm.RefreshDataGrid();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("newCheckout form is not open.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Insufficient stock quantity for the selected product.");
                            return;
                        }

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataView1 != null)
            {
                List<object[]> filteredData = new List<object[]>();

                foreach (DataRowView rowView in dataView1)
                {
                    DataRow row = rowView.Row;
                    filteredData.Add(row.ItemArray);
                }

                using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
                {
                    connection.Open();
                    foreach (var rowData in filteredData)
                    {

                        int quantity;

                        if (!int.TryParse(textQTY.Text, out quantity))
                        {
                            MessageBox.Show("Please enter a valid integer value for quantity.");
                            return;
                        }
                        else
                        {
                            quantity = Convert.ToInt32(textQTY.Text);
                        }


                        if (string.IsNullOrEmpty(rowData[1].ToString()))
                        {
                            MessageBox.Show("Please select a product before adding.");
                            return;
                        }

                        if (quantity <= 0)
                        {
                            MessageBox.Show("Please enter a valid quantity.");
                            return;
                        }

                        if (CheckStockQuantity(rowData[1].ToString(), quantity))
                        {
                            decimal discountPrice = CalculateDiscountPrice(rowData[1].ToString());
                            decimal totalPrice = discountPrice * quantity;


                            //this.newCheckout1.netAmmount += Convert.ToDecimal(rowData[5].ToString());


                            string query = "INSERT INTO productsLine (id ,name, description, qty, discount, price , total) VALUES (@Id ,@Name, @Description, @Qty, @Discount,@Price , @Total)";
                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Id", rowData[0]);
                                command.Parameters.AddWithValue("@Name", rowData[1]);
                                command.Parameters.AddWithValue("@Description", rowData[2]);
                                command.Parameters.AddWithValue("@Qty", Convert.ToInt32(textQTY.Text));
                                command.Parameters.AddWithValue("@Discount", rowData[4]);
                                command.Parameters.AddWithValue("@Price", rowData[5]);
                                command.Parameters.AddWithValue("@Total", totalPrice);
                                command.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Insufficient stock quantity for the selected product.");
                            return;
                        }

                    }
                }

                if (Application.OpenForms["newCheckout"] is newCheckout checkoutForm)
                {
                    checkoutForm.RefreshDataGrid();
                }
                else
                {
                    MessageBox.Show("newCheckout form is not open.");
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("No filtered data available.");
            }
        }

        private bool CheckStockQuantity(string productName, int quantity)
        {
            bool hasEnoughStock = false;

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                string query = "SELECT qty FROM products WHERE name = @ProductName";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int stockQuantity = Convert.ToInt32(result);

                            if (stockQuantity >= quantity)
                            {
                                hasEnoughStock = true;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            return hasEnoughStock;
        }

        private decimal CalculateDiscountPrice(string productName)
        {
            decimal price = 0;
            decimal discount = 0;

            // Retrieve price and discount from the database
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                string query = "SELECT price, discount_percentage FROM products WHERE name = @ProductName";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                price = reader.GetDecimal(0);
                                discount = reader.GetDecimal(1);
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            decimal discountPrice = price - ((price/100) * discount) ;


            return discountPrice;
        }

        private decimal CalculateDiscountPerItem(string productName,string qty)
        {

            decimal price = 0;
            decimal discount = 0;
            decimal pqty = Convert.ToDecimal(qty);
            decimal discountTotal = 0;

            // Retrieve price and discount from the database
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                string query = "SELECT price, discount_percentage FROM products WHERE name = @ProductName";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                price = reader.GetDecimal(0);
                                discount = reader.GetDecimal(1);

                                decimal discountPrice = ((price / 100) * discount);

                                discountTotal = discountPrice * pqty;

                                this.newCheckout1.totalDiscount += discountTotal;
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }



            return discountTotal;
        }
    }
}
