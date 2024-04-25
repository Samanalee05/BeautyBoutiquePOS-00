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
        public Users()
        {
            InitializeComponent();

            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridView1);
            styles.RoundCornerPanels(panel1, 10);

            LoadUsers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new newUser(this).Show();
        }

        public void LoadUsers()
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string query = "SELECT * FROM users";

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
