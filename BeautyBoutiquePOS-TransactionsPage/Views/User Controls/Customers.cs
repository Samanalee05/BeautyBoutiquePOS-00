using BeautyBoutiquePOS_TransactionsPage.Class;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views;
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

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls
{
    public partial class Customers : UserControl
    {
        public Customers()
        {
            InitializeComponent();

            LoadCustomers();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(customerGridView);
            styles.RoundCornerPanels(panel1, 10);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            customerGridView.Columns.Add(deleteButtonColumn);
            deleteButtonColumn.Width = 100;

            customerGridView.CellContentClick += dataGridViewCellContentClick;
        }

        private void dataGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == customerGridView.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                String nic = Convert.ToString(customerGridView.Rows[e.RowIndex].Cells["nic"].Value);

                Delete(nic);
            }
        }

        private void Delete(String nic)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());
            string deleteQuery = "DELETE FROM customers WHERE nic = @Nic";
            using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Nic", nic);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("customer deleted successfully.");
                        LoadCustomers();
                    }
                    else
                    {
                        MessageBox.Show("No rows were deleted.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error deleting customer: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newCustomer customerForm = new newCustomer(this);
            customerForm.ShowDialog();
        }

        public void LoadCustomers()
        {
            string query = "SELECT * FROM customers";

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

                        customerGridView.DataSource = dataTable;
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
