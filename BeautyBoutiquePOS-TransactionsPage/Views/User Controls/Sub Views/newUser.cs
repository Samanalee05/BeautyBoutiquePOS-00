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
    public partial class newUser : Form
    {
        private Users Users;
        public newUser(Users users1)
        {

            InitializeComponent();
            
            this.Users = users1;
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Manager");
            comboBox1.Items.Add("Cashier");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InsertUser();
        }

        private void InsertUser()
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string query = @"INSERT INTO users (nic, name, age, address, contact, type, username, password, date_join) 
                     VALUES (@NIC, @Name, @Age, @Address, @Contact, @Type, @Username, @Password, @JoinDate)";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@NIC", textBoxNIC.Text);
            command.Parameters.AddWithValue("@Name", textBoxName.Text);
            command.Parameters.AddWithValue("@Age", Convert.ToInt32(textBoxAge.Text));
            command.Parameters.AddWithValue("@Address", textBoxAddress.Text);
            command.Parameters.AddWithValue("@Contact", textBoxContact.Text);
            command.Parameters.AddWithValue("@Type", comboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@Username", usernameTextBox.Text);
            command.Parameters.AddWithValue("@Password", passwordTextBox.Text);
            command.Parameters.AddWithValue("@JoinDate", DateTime.Now);

            try
            {

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("User inserted successfully.");
                    this.Users.LoadUsers();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No rows were inserted.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error inserting user: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
