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
    public partial class Inventory : UserControl
    {
        public Inventory()
        {
            InitializeComponent();
            LoadNewOrders();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridView2);
            //styles.CustomizeDataGridView(dataGridView1);

            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "Edit";
            editButtonColumn.Text = "Edit";
            editButtonColumn.Name = "EditButton";
            editButtonColumn.UseColumnTextForButtonValue = true;
            //dataGridView2.Columns.Add(editButtonColumn);

            editButtonColumn.Width = 100;
        }

        private void button1_Click(object sender, EventArgs e) // in btn click 
        {
            newOrder newOrderForm = new newOrder(this, "in");
            newOrderForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) // out btn click
        {
            newOrder newOrderForm = new newOrder(this, "out");
            newOrderForm.ShowDialog();
        }

        public void LoadNewOrders() // load inventory trasaction data from db
        {
            try
            {
                string queryString = "SELECT id,name,QTY,cost,total,`function` FROM inventory";

                using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
                {

                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(queryString, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {

                            DataTable dataTable = new DataTable();

                            adapter.Fill(dataTable);

                            dataGridView2.DataSource = dataTable;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading new orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
