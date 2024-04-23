using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls;

namespace BeautyBoutiquePOS_TransactionsPage
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;


            //For right side rounded menu buttons:
            btnHome.Paint += RoundButton_Paint;
            btnCheckout.Paint += RoundButton_Paint;
            btnProducts.Paint += RoundButton_Paint;
            btnCategories.Paint += RoundButton_Paint;
            btnInventory.Paint += RoundButton_Paint;
            btnCustomers.Paint += RoundButton_Paint;
            btnReports.Paint += RoundButton_Paint;
            btnUsers.Paint += RoundButton_Paint;
        }



        private void Form1_Load(object sender, EventArgs e)
        {





            /* Use to check client height:
             
            int clientHeight = this.ClientSize.Height;
            MessageBox.Show("Form's Client Area Height: " + clientHeight);*/
        }

        //Right Side Rounded Menu Buttons:
        private void RoundButton_Paint(object sender, PaintEventArgs e)
        {
            Button button = (Button)sender;
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 25; //change roundness here
                Rectangle bounds = button.ClientRectangle;
                bounds.Width--;
                bounds.Height--;

                path.AddLine(bounds.X, bounds.Y, bounds.Right - radius, bounds.Y);
                path.AddArc(bounds.Right - radius * 2, bounds.Y, radius * 2, radius * 2, 270, 90);
                path.AddLine(bounds.Right, bounds.Y + radius, bounds.Right, bounds.Bottom - radius);
                path.AddArc(bounds.Right - radius * 2, bounds.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddLine(bounds.Right - radius, bounds.Bottom, bounds.X, bounds.Bottom);
                path.CloseFigure();

                button.Region = new Region(path);
            }
        }
        private void ClearContentArea()
        {
            windowPnl.Controls.Clear();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ClearContentArea();
            var Product = new Product();
            windowPnl.Controls.Add(Product);
        }

        

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ClearContentArea();
            var Customers = new Customers();
            windowPnl.Controls.Add(Customers);
        }



    }
}
