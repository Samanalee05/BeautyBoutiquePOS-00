using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BeautyBoutiquePOS_TransactionsPage.Class
{
    internal class UserControlStyles
    {
        public void CustomizeDataGridView(DataGridView  dataGridView) // data grid view styles
        {
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView.BackgroundColor = Color.White;

            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView.RowTemplate.Height = 40;
        }

        public Button RoundedBtn(Button btn) // btn styles
        {
            Button button = btn;
            using (GraphicsPath GraphPath = new GraphicsPath())
            {
                button.BackColor = Color.FromArgb(149, 73, 158); // Background color
                button.ForeColor = Color.White; // Text color
                button.Font = new Font("Arial", 12, FontStyle.Bold); // Font
                button.FlatStyle = FlatStyle.Flat; // Flat style
                button.FlatAppearance.BorderSize = 1; // Border size
                button.FlatAppearance.BorderColor = Color.Black; // Border color
                button.Size = new Size(200, 50);

                Image icon = Image.FromFile(@"Resources\\add.png"); 

                // Resize the image
                int desiredWidth = 30; // Set your desired width
                int desiredHeight = 30; // Set your desired height
                Image resizedIcon = new Bitmap(icon, new Size(desiredWidth, desiredHeight));

                button.Image = resizedIcon;
                button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft; // Set icon alignment

                button.Padding = new Padding(10, 0, 0, 0);

                return button;
            }
        }


        public void RoundCornerPanels(Control control, int cornerRadius)// round corner syles for panels
        {

            GraphicsPath path = new GraphicsPath();

            int width = control.Width;
            int height = control.Height;
            int radius = cornerRadius;

            Rectangle arcRectTopLeft = new Rectangle(0, 0, radius * 2, radius * 2);
            Rectangle arcRectTopRight = new Rectangle(width - radius * 2, 0, radius * 2, radius * 2);
            Rectangle arcRectBottomLeft = new Rectangle(0, height - radius * 2, radius * 2, radius * 2);
            Rectangle arcRectBottomRight = new Rectangle(width - radius * 2, height - radius * 2, radius * 2, radius * 2);

            path.AddArc(arcRectTopLeft, 180, 90);
            path.AddArc(arcRectTopRight, 270, 90);
            path.AddArc(arcRectBottomRight, 0, 90);
            path.AddArc(arcRectBottomLeft, 90, 90);

            path.CloseFigure();
            control.Region = new Region(path);
        }

        public void ApplyTextBoxStyles(TextBox textBox) // text box styles
        {
            Color borderColor = Color.Gray;

            textBox.BackColor = Color.LightGray;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Arial", 10, FontStyle.Bold);
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.Paint += (sender, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, textBox.ClientRectangle,
                                        Color.DarkGray, 1, ButtonBorderStyle.Solid,
                                        Color.DarkGray, 1, ButtonBorderStyle.Solid,
                                        Color.DarkGray, 1, ButtonBorderStyle.Solid,
                                        Color.DarkGray, 1, ButtonBorderStyle.Solid);
            };
        }
    }
}
