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
            styles.RoundCornerPanels(panel3, 10);
            //styles.RoundedBtn(button1);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridViewProducts.Columns.Add(deleteButtonColumn);
            deleteButtonColumn.Width = 100;


            dataGridViewProducts.CellContentClick += dataGridViewProducts_CellContentClick;
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

        private void dataGridViewProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewProducts.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                int productId = Convert.ToInt32(dataGridViewProducts.Rows[e.RowIndex].Cells["id"].Value);

                DeleteProduct(productId);
            }
        }

        private void DeleteProduct(int productId)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());
            string deleteQuery = "DELETE FROM products WHERE id = @ProductId";
            using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@ProductId", productId);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product deleted successfully.");
                        LoadProductData();
                    }
                    else
                    {
                        MessageBox.Show("No rows were deleted.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error deleting product: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
