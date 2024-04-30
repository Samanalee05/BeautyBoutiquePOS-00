using BeautyBoutiquePOS_TransactionsPage.Class;
using Microsoft.Reporting.WinForms;
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

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Report_Views
{
    public partial class newCheckoutReportView : Form
    {
        public newCheckoutReportView()
        {
            InitializeComponent();

            reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }

        private void newCheckoutReportView_Load(object sender, EventArgs e)
        {
            //Textbox8.Text = "inv00003";

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", mysql.Data($"select * from checkout", "checkout").Tables[0]));
            reportViewer1.LocalReport.ReportPath = $"C:\\Users\\samanla\\Documents\\GitHub\\BeautyBoutiquePOS-00\\BeautyBoutiquePOS-TransactionsPage\\Reports\\newCheckout.rdlc";
            reportViewer1.RefreshReport();
        }

        private void newCheckoutReportView_FormClosing(object sender, FormClosingEventArgs e)
        {
            

            // Create connection
            using (MySqlConnection connection = new MySqlConnection(DatabaseConnection.GetConnectionString()))
            {
                try
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM checkout";

                    MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} rows deleted successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
