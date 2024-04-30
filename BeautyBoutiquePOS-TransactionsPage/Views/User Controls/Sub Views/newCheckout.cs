using BeautyBoutiquePOS_TransactionsPage.Class;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Report_Views;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views.Payments;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views
{
    public partial class newCheckout : Form
    {
        public decimal balance;
        public decimal cash;
        public decimal totalDiscount;
        private decimal netGross;
        private Cash cashForm1;
        private Card cardForm1;
        private Checkout checkout1;
        private String userType;


        public newCheckout(Checkout checkout, string userType1)
        {


            InitializeComponent();

            this.userType = userType1;

            labelUserName.Text = userType;

            UserControlStyles styles = new UserControlStyles();
            styles.CustomizeDataGridView(dataGridView1);
            DateTime currentDate = DateTime.Today;
            labelDate.Text = currentDate.ToString("yyyy/MM/dd");
            this.checkout1 = checkout;


            RefreshDataGrid();
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "Delete";
            editButtonColumn.Text = "Delete";
            editButtonColumn.Name = "DeleteButton";
            editButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editButtonColumn);

            dataGridView1.CellContentClick += dataGridView1_CellContentClick;



        }

        private void newCheckout_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            addToCart productForm = new addToCart("checkout",this);
            productForm.ShowDialog();
        }

        public void RefreshDataGrid()
        {
            string query = "SELECT * FROM productsLine";
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }



            CalculateNetGrossAmount();
        }


        private void CalculateNetGrossAmount()
        {
            decimal netGrossAmount = 0;
            decimal totalPrice1 = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["price"].Value != null && decimal.TryParse(row.Cells["price"].Value.ToString(), out decimal totalPrice))
                {
                    netGrossAmount += totalPrice;
                }

                if (row.Cells["total"].Value != null && decimal.TryParse(row.Cells["total"].Value.ToString(), out decimal total))
                {
                    totalPrice1 += total;
                    textBoxTotalDiscount.Text = totalDiscount.ToString();
                }
            }

            netammountText.Text = totalPrice1.ToString();

            netGross = totalPrice1;
        }

        private void DeleteAllProductsLineDataAndUpdateProductQty()
        {

            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string updateQuery = @"UPDATE products p INNER JOIN productsLine pl ON p.id = pl.id SET p.qty = p.qty - pl.qty;";

                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection, transaction))
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            //MessageBox.Show("Product QTY updateed.");
                        }
                        else
                        {
                            MessageBox.Show("Product QTY update fail.");
                        }


                    }

                    string deleteQuery = "DELETE FROM productsLine";
                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection, transaction))
                    {
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            //MessageBox.Show("All data from productsLine table deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No data found in productsLine table.");
                        }
                    }

                    transaction.Commit();
                }
                catch (MySqlException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textGross.Text = balance.ToString();
            DeleteAllProductsLineDataAndUpdateProductQty();
            checkoutButton_Click(sender, e);
            this.checkout1.LoadCheckoutRecordsForToday();
        }

        private void cashBtn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cashBtn_Click(object sender, EventArgs e)
        {
            Cash cashForm = new Cash(netGross,this);
            cashForm.ShowDialog();
            this.cashForm1 = cashForm;
        }

        private void cardBtn_Click(object sender, EventArgs e)
        {
            Card cardForm = new Card(netGross);
            cardForm.ShowDialog();
            this.cardForm1 = cardForm;
        }

        private void checkoutButton_Click(object sender, EventArgs e)
        {

            double totalQty = 0;
            double discountTotal = 0;

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd");


            string customerName = labelCustomer.Text;




            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["discount"].Value != null) // Check if the cell value is not null
                {
                    double rowDiscount;
                    if (double.TryParse(row.Cells["discount"].Value.ToString(), out rowDiscount))
                    {
                        discountTotal += rowDiscount;
                    }
                }

                if (row.Cells["qty"].Value != null) // Check if the cell value is not null
                {
                    double rowQty;
                    if (double.TryParse(row.Cells["qty"].Value.ToString(), out rowQty))
                    {
                        totalQty += rowQty;
                    }
                }
            }


            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                connection.Open();
                string query = "INSERT INTO checkoutLine (date, customer, total, discount_percentage, itemQTY) VALUES (@Date, @Customer, @Total, @Discount, @ItemQty)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", formattedDate);
                    command.Parameters.AddWithValue("@Customer", customerName);
                    command.Parameters.AddWithValue("@Total", netGross);
                    command.Parameters.AddWithValue("@Discount", discountTotal);
                    command.Parameters.AddWithValue("@ItemQty", totalQty);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        {
                            MessageBox.Show("Checkout successfully.");
                            
                        }
                        else
                        {
                            MessageBox.Show("Error.");
                        }
                }

                string query1 = "INSERT INTO checkout (date, customer, total, discount, itemQTY) VALUES (@Date, @Customer, @Total, @Discount, @ItemQty)";
                using (MySqlCommand command = new MySqlCommand(query1, connection))
                {
                    command.Parameters.AddWithValue("@Date", formattedDate);
                    command.Parameters.AddWithValue("@Customer", customerName);
                    command.Parameters.AddWithValue("@Total", netGross);
                    command.Parameters.AddWithValue("@Discount", totalDiscount);
                    command.Parameters.AddWithValue("@ItemQty", totalQty);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        new newCheckoutReportView().ShowDialog();
                        //MessageBox.Show("Checkout successfully.");
                        this.Close();
                    }
                    else
                    {
                        //MessageBox.Show("Error.");
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DeleteButton"].Index)
            {

                try
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                    DeleteProductLine(id);

                } catch {
                    MessageBox.Show("Select Valid Row!");
                }
                


            }
        }

        private void DeleteProductLine(int id)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string query = "DELETE FROM productsLine WHERE id = @id;";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    //MessageBox.Show("Product line deleted successfully.");
                    RefreshDataGrid();
                    CalculateNetGrossAmount();
                }
                else
                {
                   // MessageBox.Show("No rows were deleted.");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error deleting product line: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString());

            string deleteQuery = "DELETE FROM productsLine";
            using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
            {
                connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    RefreshDataGrid();
                    //MessageBox.Show("All data from productsLine table deleted successfully.");
                }
                else
                {
                    MessageBox.Show("No data found in productsLine table.");
                }
            }
        }

        public void calculateTotalDiscount()
        {
            double totalQty = 0;
            double discountTotal = 0;

            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd");


            string customerName = labelCustomer.Text;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["discount"].Value != null) // Check if the cell value is not null
                {
                    double rowDiscount;
                    if (double.TryParse(row.Cells["discount"].Value.ToString(), out rowDiscount))
                    {
                        discountTotal += rowDiscount;
                        //textBoxTotalDiscount.Text = this.totalDiscount.ToString();
                        textBoxCashResived.Text = cash.ToString();

                    }
                }

                if (row.Cells["qty"].Value != null) // Check if the cell value is not null
                {
                    double rowQty;
                    if (double.TryParse(row.Cells["qty"].Value.ToString(), out rowQty))
                    {
                        totalQty += rowQty;
                    }
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new newCheckoutReportView().ShowDialog();
        }
    }
}
