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


namespace BeautyBoutiquePOS_TransactionsPage.Views
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();

            UserControlStyles styles = new UserControlStyles();
            styles.ApplyTextBoxStyles(textBox1);
            styles.ApplyTextBoxStyles(textBox2);
            textBox2.PasswordChar = '*';
        }

        private void btnLogin_Click(object sender, EventArgs e) //login btn press
        {
            login();
        }

        private void button2_Click(object sender, EventArgs e) // signup btn press
        {
            Signup signup = new Signup();
            signup.ShowDialog();
        }


        private void login() // check user by using username & password 
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            string userType = GetUserType(username, password);

            if (!string.IsNullOrEmpty(userType)) // if user exists login and load main form
            {
                if (userType == "Pending")
                {
                    MessageBox.Show("Contact Adming Befor Login!.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else
                {
                    Main mainForm = new Main(userType, username);
                    mainForm.ShowDialog();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /*private string GetUserType(string username, string password) // Remove on Release
        {

            return "Admin";
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Conatct Your Admin For Help To Reset Password!");
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private string GetUserType(string username, string password) // get user type from database
        {

            string userType = "";

            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string query = "SELECT type FROM users WHERE username = @Username AND password = @Password";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    userType = result.ToString();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error retrieving user type: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return userType;
        }


    }
}
