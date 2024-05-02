using BeautyBoutiquePOS_TransactionsPage.Class;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls
{
    public partial class Checkout : UserControl
    {
        private string userType;

        public Checkout(string userType1)
        {
            InitializeComponent();
            this.userType = userType1;
            LoadCheckoutRecordsForToday();
            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(checkoutGridView);
            styles.RoundCornerPanels(panel1, 10);


        }

        private void button1_Click(object sender, EventArgs e) // new checkout click
        {
            newCheckout checkoutForm = new newCheckout(this,this.userType);
            checkoutForm.ShowDialog();
        }

        public void LoadCheckoutRecordsForToday() // load all checkout records only for today
        {
            try
            {
                DateTime currentDate = DateTime.Now.Date;

                string queryString = "SELECT * FROM checkoutLine WHERE DATE(date) = @Date";

                using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@Date", currentDate);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();

                            adapter.Fill(dataTable);

                            checkoutGridView.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading checkout records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
