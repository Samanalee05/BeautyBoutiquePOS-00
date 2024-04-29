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
        private String userType;

        public Main(string userType1)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            this.userType = userType1;


            //For right side rounded menu buttons:
            btnHome.Paint += RoundButton_Paint;
            btnCheckout.Paint += RoundButton_Paint;
            btnProducts.Paint += RoundButton_Paint;
            btnCategories.Paint += RoundButton_Paint;
            btnInventory.Paint += RoundButton_Paint;
            btnCustomers.Paint += RoundButton_Paint;
            btnReports.Paint += RoundButton_Paint;
            btnUsers.Paint += RoundButton_Paint;

            this.FormClosing += MainForm_FormClosing;
        }



        private void Form1_Load(object sender, EventArgs e)
        {


            ClearContentArea();
            var Home = new Home();
            windowPnl.Controls.Add(Home);


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
            if (this.userType == "Admin"  || this.userType == "Manager")
            {
                ClearContentArea();
                var Product = new Product(this.userType);
                windowPnl.Controls.Add(Product);
            } else
            {
                MessageBox.Show("permission denied!");
            }
        }

        

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            if (this.userType == "Admin" || this.userType == "Manager")
            {
                ClearContentArea();
                var Customers = new Customers(this.userType);
                windowPnl.Controls.Add(Customers);
            }
            else
            {
                MessageBox.Show("permission denied!");
            }
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {

            if (this.userType == "Admin" || this.userType == "Manager")
            {
                ClearContentArea();
                var Categories = new Categories(this.userType);
                windowPnl.Controls.Add(Categories);
            }
            else
            {
                MessageBox.Show("permission denied!");
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (this.userType == "Admin" || this.userType == "Cashier")
            {
                ClearContentArea();
                var Checkout = new Checkout(userType);
                windowPnl.Controls.Add(Checkout);
            }
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {

            if (this.userType == "Admin" || this.userType == "Manager")
            {
                ClearContentArea();
                var Inventory = new Inventory();
                windowPnl.Controls.Add(Inventory);
            }
            else
            {
                MessageBox.Show("permission denied!");
            }

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ClearContentArea();
            var Home = new Home();
            windowPnl.Controls.Add(Home);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            if (this.userType == "Admin" || this.userType == "Manager")
            {
                ClearContentArea();
                var Users = new Users(this.userType);
                windowPnl.Controls.Add(Users);
            }
            else
            {
                MessageBox.Show("permission denied!");
            }
        }

        private void btnHome_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnHome_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnCheckout_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnCheckout_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnProducts_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnProducts_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnCategories_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnCategories_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnInventory_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnInventory_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnCustomers_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnCustomers_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnReports_MouseEnter(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnReports_MouseLeave(object sender, EventArgs e)
        {
            pictureBox8.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnUsers_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void btnUsers_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (this.userType == "Admin" || this.userType == "Manager")
            {
                ClearContentArea();
                var Reports = new Reports();
                windowPnl.Controls.Add(Reports);
            }
            else
            {
                MessageBox.Show("permission denied!");
            }

        }
    }
}
