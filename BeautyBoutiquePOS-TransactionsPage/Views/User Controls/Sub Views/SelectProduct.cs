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
    public partial class SelectProduct : Form
    {

        DataTable dataTable1;
        DataView dataView1;
        private newOrder newOrder;
        private String formName;

        public SelectProduct(string form, newOrder newOrder1)
        {
            this.newOrder = newOrder1;
            this.formName = form;

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

        public void UpdateDataGridView() // load available product from product db to data grid view
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) // select btn click from data gird view
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == productGridView.Columns["SelectButton"].Index)
                {

                    DataGridViewRow row = productGridView.Rows[e.RowIndex];

                    Console.WriteLine(row.Cells[4].Value);


                    if (this.formName == "newOrder")
                    {
                        this.newOrder.refreshFormData(row.Cells[1].Value.ToString(), row.Cells[6].Value.ToString(), row.Cells[2].Value.ToString());

                        this.Close();
                    }
                    
                }
            }
        }



    }
}
