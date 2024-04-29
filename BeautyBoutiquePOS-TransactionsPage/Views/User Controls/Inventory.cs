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
            dataGridView2.Columns.Add(editButtonColumn);

            editButtonColumn.Width = 100;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView2.Columns["EditButton"].Index && e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[rowIndex];


                string[] rowDataArray = new string[selectedRow.Cells.Count];
                for (int i = 0; i < selectedRow.Cells.Count; i++)
                {
                    rowDataArray[i] = selectedRow.Cells[i].Value.ToString();
                }

                editForm editForm = new editForm(this, rowDataArray);
                editForm.ShowDialog();
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            newOrder newOrderForm = new newOrder(this, "in");
            newOrderForm.ShowDialog();
        }

        public void LoadNewOrders()
        {
            try
            {
                string queryString = "SELECT * FROM inventory";

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

                            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading new orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newOrder newOrderForm = new newOrder(this,"out");
            newOrderForm.ShowDialog();
        }
    }
}
