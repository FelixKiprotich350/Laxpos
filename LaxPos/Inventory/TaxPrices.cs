namespace LaxPos.Inventory
{ 
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TaxPrices : Form
    {
        private IContainer components = null;
        private TabControl tabControl1;
        private TabPage TabPage_Discounts;
        private TabPage tabPage_Prices;
        private TabPage tabPage4;
        private GroupBox groupBox2;
        private DataGridView Prices_ProductsGridview;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column20;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column26;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column27;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;

        public TaxPrices()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new TabControl();
            this.TabPage_Discounts = new TabPage();
            this.groupBox2 = new GroupBox();
            this.groupBox6 = new GroupBox();
            this.groupBox5 = new GroupBox();
            this.groupBox4 = new GroupBox();
            this.tabPage_Prices = new TabPage();
            this.Prices_ProductsGridview = new DataGridView();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column18 = new DataGridViewTextBoxColumn();
            this.Column20 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.Column26 = new DataGridViewTextBoxColumn();
            this.Column15 = new DataGridViewTextBoxColumn();
            this.Column27 = new DataGridViewTextBoxColumn();
            this.groupBox3 = new GroupBox();
            this.tabPage4 = new TabPage();
            this.tabControl1.SuspendLayout();
            this.TabPage_Discounts.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage_Prices.SuspendLayout();
            ((ISupportInitialize) this.Prices_ProductsGridview).BeginInit();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.TabPage_Discounts);
            this.tabControl1.Controls.Add(this.tabPage_Prices);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Font = new Font("Segoe UI", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x3dc, 430);
            this.tabControl1.TabIndex = 0;
            this.TabPage_Discounts.Controls.Add(this.groupBox2);
            this.TabPage_Discounts.Location = new Point(4, 0x1a);
            this.TabPage_Discounts.Name = "TabPage_Discounts";
            this.TabPage_Discounts.Padding = new Padding(3);
            this.TabPage_Discounts.Size = new Size(980, 400);
            this.TabPage_Discounts.TabIndex = 0;
            this.TabPage_Discounts.Text = "Discounts";
            this.TabPage_Discounts.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x3ce, 0x18a);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Discount Management";
            this.groupBox6.Dock = DockStyle.Left;
            this.groupBox6.Location = new Point(0x1df, 0x15);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x13b, 370);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Wholesale Discounts";
            this.groupBox5.Dock = DockStyle.Left;
            this.groupBox5.Location = new Point(0xe3, 0x15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0xfc, 370);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tied  Products";
            this.groupBox4.Dock = DockStyle.Left;
            this.groupBox4.Location = new Point(3, 0x15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xe0, 370);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Seasonal Discount";
            this.tabPage_Prices.Controls.Add(this.Prices_ProductsGridview);
            this.tabPage_Prices.Controls.Add(this.groupBox3);
            this.tabPage_Prices.Location = new Point(4, 0x1a);
            this.tabPage_Prices.Name = "tabPage_Prices";
            this.tabPage_Prices.Padding = new Padding(3);
            this.tabPage_Prices.Size = new Size(0x3e4, 0x1b7);
            this.tabPage_Prices.TabIndex = 1;
            this.tabPage_Prices.Text = "Product Prices";
            this.tabPage_Prices.UseVisualStyleBackColor = true;
            this.Prices_ProductsGridview.AllowUserToAddRows = false;
            this.Prices_ProductsGridview.AllowUserToDeleteRows = false;
            this.Prices_ProductsGridview.AllowUserToResizeRows = false;
            this.Prices_ProductsGridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Prices_ProductsGridview.BackgroundColor = Color.White;
            this.Prices_ProductsGridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[9];
            dataGridViewColumns[0] = this.Column10;
            dataGridViewColumns[1] = this.Column18;
            dataGridViewColumns[2] = this.Column20;
            dataGridViewColumns[3] = this.Column11;
            dataGridViewColumns[4] = this.Column13;
            dataGridViewColumns[5] = this.Column12;
            dataGridViewColumns[6] = this.Column26;
            dataGridViewColumns[7] = this.Column15;
            dataGridViewColumns[8] = this.Column27;
            this.Prices_ProductsGridview.Columns.AddRange(dataGridViewColumns);
            this.Prices_ProductsGridview.Dock = DockStyle.Fill;
            this.Prices_ProductsGridview.EnableHeadersVisualStyles = false;
            this.Prices_ProductsGridview.Location = new Point(3, 3);
            this.Prices_ProductsGridview.Name = "Prices_ProductsGridview";
            this.Prices_ProductsGridview.RowHeadersVisible = false;
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.BackColor = Color.White;
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            this.Prices_ProductsGridview.Size = new Size(990, 0x14d);
            this.Prices_ProductsGridview.TabIndex = 0x1c;
            this.Column10.HeaderText = "Pcode";
            this.Column10.Name = "Column10";
            this.Column18.HeaderText = "Description";
            this.Column18.Name = "Column18";
            this.Column20.HeaderText = "Unit";
            this.Column20.Name = "Column20";
            this.Column11.HeaderText = "Selling Price";
            this.Column11.Name = "Column11";
            this.Column13.HeaderText = "Buying Price";
            this.Column13.Name = "Column13";
            this.Column12.HeaderText = "Gsst %";
            this.Column12.Name = "Column12";
            this.Column26.HeaderText = "VAT%";
            this.Column26.Name = "Column26";
            this.Column15.HeaderText = "Tax0";
            this.Column15.Name = "Column15";
            this.Column27.HeaderText = "Discount";
            this.Column27.Name = "Column27";
            this.groupBox3.Dock = DockStyle.Bottom;
            this.groupBox3.Location = new Point(3, 0x150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(990, 100);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            this.tabPage4.Location = new Point(4, 0x1a);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(0x3e4, 0x1b7);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "TiedProducts";
            this.tabPage4.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3dc, 430);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "TaxPrices";
            this.tabControl1.ResumeLayout(false);
            this.TabPage_Discounts.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage_Prices.ResumeLayout(false);
            ((ISupportInitialize) this.Prices_ProductsGridview).EndInit();
            base.ResumeLayout(false);
        }
    }
}

