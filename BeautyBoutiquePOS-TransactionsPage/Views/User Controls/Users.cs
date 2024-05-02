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
    public partial class Users : UserControl
    {

        private string userType1;
        public Users(string userType)
        {
            this.userType1 = userType;
            InitializeComponent();

            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridView1);
            styles.RoundCornerPanels(panel1, 10);

            LoadUsers();

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.Name = "Delete";
            deleteButtonColumn.HeaderText = "Delete";
            deleteButtonColumn.Text = "Delete";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteButtonColumn);
            deleteButtonColumn.Width = 100;

            dataGridView1.CellContentClick += dataGridViewCellContentClick;

            
        }

        private void dataGridViewCellContentClick(object sender, DataGridViewCellEventArgs e) // delete btn click 
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                String nic = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["nic"].Value);

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

        private void Delete(String nic) // delete user from db
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());
            string deleteQuery = "DELETE FROM users WHERE nic = @Nic";
            using (MySqlCommand command = new MySqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Nic", nic);
                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("user deleted successfully.");
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("No rows were deleted.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // new user btn click
        {
            new newUser(this).Show();
        }

        public void LoadUsers() // load user date from db without user name & password
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string query = "SELECT id,nic,name,address,contact,type,date_join  FROM users";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);

            DataSet dataSet = new DataSet();

            try
            {
                connection.Open();

                adapter.Fill(dataSet, "Users");

                dataGridView1.DataSource = dataSet.Tables["Users"];
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
