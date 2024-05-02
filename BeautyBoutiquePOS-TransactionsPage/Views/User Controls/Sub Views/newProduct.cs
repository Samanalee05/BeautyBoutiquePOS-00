﻿using BeautyBoutiquePOS_TransactionsPage.Class;
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
    public partial class newProduct : Form
    {

        public Product productForm;
        public newProduct(Product productForm)
        {
            InitializeComponent();

            this.productForm = productForm;
            this.productForm = productForm;
            LoadCategories();
        }


        private void AddProductToDatabase() // add product to product tbl
        {

            string name = txtProductName.Text;
            string description = txtDescription.Text;
            double qty = 0;
            double discount = Convert.ToDouble(txtDiscount.Text);
            float price = float.Parse(txtPrice.Text);
            string category = txtCategory.SelectedItem.ToString();


            string query = "INSERT INTO products (name, description, qty, discount_percentage, price, category) VALUES (@Name, @Description, @Qty, @Discount, @Price, @Category)";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Qty", qty);
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Category", category);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product added successfully.");
                            this.productForm.LoadProductData();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to add product.");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void LoadCategories() //load categories data from db 
        {

            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string query = "SELECT name FROM categories;";

            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtCategory.Items.Add(reader.GetString("name"));
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e) // save btn click
        {
            AddProductToDatabase();
        }

        private void button1_Click(object sender, EventArgs e) // cancel btn click
        {
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtCategory.Text = "";
            txtDiscount.Text = "";
        }
    }
}
