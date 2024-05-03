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
        DataTable dataTable1;
        DataView dataView1;
        private string userType1;

        public Customers(string userType)
        {
            this.userType1 = userType;

            InitializeComponent();

            LoadCustomers();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(customerGridView);
            styles.RoundCornerPanels(panel1, 10);

            //Add delete button
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            customerGridView.Columns.Add(deleteButtonColumn);
            deleteButtonColumn.Width = 100;
            styles.RoundedBtn(button1);

            customerGridView.CellContentClick += dataGridViewCellContentClick;
        }

        private void filterData() // filter data grid view data 
        {
            string filter = richTextBox1.Text;
            if (!string.IsNullOrEmpty(filter))
            {
                DataView dv = new DataView(dataTable1);

                if (int.TryParse(filter, out int idFilter))
                {
                    dv.RowFilter = string.Format("nic LIKE '%{0}%'", idFilter);
                } 
                else
                {
                    dv.RowFilter = string.Format("name LIKE '%{0}%'", filter);
                }
                customerGridView.DataSource = dv;
                dataView1 = dv;
            }
            else
            {
                customerGridView.DataSource = dataTable1;
            }
        }

        private void dataGridViewCellContentClick(object sender, DataGridViewCellEventArgs e) // delete butoon click
        {
            if (e.ColumnIndex == customerGridView.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                String nic = Convert.ToString(customerGridView.Rows[e.RowIndex].Cells["nic"].Value);

                if (this.userType1 == "Admin")
                {
                    Delete(nic);
                }
                else
                {
                    MessageBox.Show("permission denied!");
                }

            }
        }

        private void Delete(String nic) // delete customer from database
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

        private void button1_Click(object sender, EventArgs e) // new customer button click 
        {
            newCustomer customerForm = new newCustomer(this);
            customerForm.ShowDialog();
        }

        public void LoadCustomers() // Load all customer from database to data grid view 
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
                        dataTable1 = dataTable;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) // search box text entered
        {
            filterData();
        }
    }
}
