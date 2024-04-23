namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views
{
    partial class addToCart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textQTY = new System.Windows.Forms.TextBox();
            this.productGridView = new System.Windows.Forms.DataGridView();
            this.textProduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.productGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(691, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 31);
            this.button1.TabIndex = 34;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(488, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "QTY";
            // 
            // textQTY
            // 
            this.textQTY.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textQTY.Location = new System.Drawing.Point(535, 8);
            this.textQTY.Name = "textQTY";
            this.textQTY.Size = new System.Drawing.Size(250, 29);
            this.textQTY.TabIndex = 32;
            this.textQTY.Text = "1";
            // 
            // productGridView
            // 
            this.productGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.productGridView.Location = new System.Drawing.Point(0, 95);
            this.productGridView.Name = "productGridView";
            this.productGridView.Size = new System.Drawing.Size(800, 355);
            this.productGridView.TabIndex = 31;
            // 
            // textProduct
            // 
            this.textProduct.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textProduct.Location = new System.Drawing.Point(129, 9);
            this.textProduct.Name = "textProduct";
            this.textProduct.Size = new System.Drawing.Size(348, 29);
            this.textProduct.TabIndex = 30;
            this.textProduct.TextChanged += new System.EventHandler(this.textProduct_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Prduct Name Or Code";
            // 
            // addToCart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textQTY);
            this.Controls.Add(this.productGridView);
            this.Controls.Add(this.textProduct);
            this.Controls.Add(this.label1);
            this.Name = "addToCart";
            this.Text = "addToCart";
            ((System.ComponentModel.ISupportInitialize)(this.productGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textQTY;
        private System.Windows.Forms.DataGridView productGridView;
        private System.Windows.Forms.TextBox textProduct;
        private System.Windows.Forms.Label label1;
    }
}