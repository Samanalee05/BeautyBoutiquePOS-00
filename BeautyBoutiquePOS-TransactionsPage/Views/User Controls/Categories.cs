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
        public Categories()
        {
            InitializeComponent();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridViewCategories);
            styles.RoundCornerPanels(panel1, 10);
            LoadCategories();
        }

        public void LoadCategories()
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
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newCategory categoryForm = new newCategory(this);
            categoryForm.ShowDialog();
        }


    }
}
