using BeautyBoutiquePOS_TransactionsPage.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BeautyBoutiquePOS_TransactionsPage.Views
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            signup();
        }


        private void signup()
        {
            string name = txtFirstName.Text + " " + txtLastName.Text;    

            string query = "INSERT INTO users (nic, name, contact, type, username, password, date_join) " +
                           "VALUES(@nic, @name, @contact, @type, @username, @password, @date_join)";

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nic", txtNic.Text);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@contact", txtContact.Text);
                cmd.Parameters.AddWithValue("@type", "Pending");
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@date_join", DateTime.Now);

                connection.Open();

                try
                {
                     if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Signup Sucsess! Please Login");
                        this.Close();
                    }else
                    {
                        MessageBox.Show("Try Again!");
                    }

                    
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
