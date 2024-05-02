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
    public partial class newCategory : Form
    {
        private Categories categories1;

        public newCategory(Categories categories)
        {
            this.categories1 = categories;
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string categoryName = textBoxCategoryName.Text;
            string categoryDescription = textBoxCategoryDescription.Text;

            // Retrieve the last category ID from the database
            string lastCategoryId;
            string queryLastId = "SELECT MAX(SUBSTRING(id, 3)) FROM categories";
            using (MySqlConnection connectionLastId = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand commandLastId = new MySqlCommand(queryLastId, connectionLastId))
                {
                    try
                    {
                        connectionLastId.Open();
                        object result = commandLastId.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int lastId = Convert.ToInt32(result);
                            lastCategoryId = "CA" + (lastId + 1).ToString("D4");
                        }
                        else
                        {
                            lastCategoryId = "CA0001";
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error retrieving last category ID: " + ex.Message);
                        return;
                    }
                }
            }

            // Insert the new category with the generated ID into the database
            string query = "INSERT INTO categories (id, name, description) VALUES (@Id, @Name, @Description)";
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", lastCategoryId);
                    command.Parameters.AddWithValue("@Name", categoryName);
                    command.Parameters.AddWithValue("@Description", categoryDescription);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category added successfully!");
                            this.categories1.LoadCategories();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add category.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e) // cancel btn click
        {
            textBoxCategoryName.Text = "";
            textBoxCategoryDescription.Text = "";
        }
    }
}
