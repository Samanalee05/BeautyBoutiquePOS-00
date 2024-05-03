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
    public partial class Categories : UserControl
    {

        DataTable dataTable1;
        DataView dataView1;
        private string userType1;

        public Categories(string userType)
        {
            this.userType1 = userType;

            InitializeComponent();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridViewCategories);
            styles.RoundCornerPanels(panel1, 10);
            styles.RoundedBtn(button1);
            LoadCategories();

            //Add delete button
            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridViewCategories.Columns.Add(deleteButtonColumn);
            deleteButtonColumn.Width = 100;

            dataGridViewCategories.CellContentClick += dataGridViewCellContentClick;

            
        }


        private void filterData() // filter categories data grid view data
        {
            string filter = richTextBox1.Text;
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
                dataGridViewCategories.DataSource = dv;
                dataView1 = dv;
            }
            else
            {
                dataGridViewCategories.DataSource = dataTable1;
            }
        }

        public void LoadCategories() // load all categories data from db
        {
            string query = "SELECT id, name, description FROM categories";

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


                        dataGridViewCategories.DataSource = dataTable;
                        dataTable1 = dataTable;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void dataGridViewCellContentClick(object sender, DataGridViewCellEventArgs e) // data grid view delte btn click
        {
            if (e.ColumnIndex == dataGridViewCategories.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                String categoriesName = Convert.ToString(dataGridViewCategories.Rows[e.RowIndex].Cells["name"].Value);

                if (this.userType1 == "Admin")
                {
                    Delete(categoriesName);
                }
                else
                {
                    MessageBox.Show("permission denied!");
                }

            }
        }


        private void Delete(String categoriesName) // delete categorie from db
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());
            string deleteQuery = "DELETE FROM categories WHERE name = @CategoriesName";
            using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@CategoriesName", categoriesName);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Categorie deleted successfully.");
                        LoadCategories();
                    }
                    else
                    {
                        MessageBox.Show("No rows were deleted.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error deleting Categorie: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e) // new category btn click
        {
            newCategory categoryForm = new newCategory(this);
            categoryForm.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) // searchbox text entered
        {
            filterData();
        }

    }
}
