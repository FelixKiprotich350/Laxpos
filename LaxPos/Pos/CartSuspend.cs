namespace LaxPos.Pos
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class CartSuspend : Form
    {
        private readonly DataSet Cust = null;
        private readonly string TName = null;
        private IContainer components = null;
        private Panel TitlePanel;
        private Label Title_SalesControl;
        private Button Btn_Confirm;
        private Button Btn_Cancel;
        private Label label1;
        private Label CartIdLabel;
        private Panel panel1;
        public DataGridView Sus_Gridview;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;

        public CartSuspend(string Tablename, DataSet CustomerDataset)
        {
            this.InitializeComponent();
            this.Cust = CustomerDataset;
            this.TName = Tablename;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void Btn_Confirm_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        private void CartSuspend_Load(object sender, EventArgs e)
        {
            this.CartIdLabel.Text = this.TName;
            if (this.Cust.Tables[this.TName].Rows.Count <= 0)
            {
                MessageBox.Show("You Have no items to Cart!!", "Message Box", MessageBoxButtons.OK);
                base.Close();
            }
            else
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.Cust.Tables[this.TName].Rows.Count)
                    {
                        break;
                    }
                    object[] values = new object[10];
                    values[0] = this.Cust.Tables[this.TName].Rows[num][0];
                    values[1] = this.Cust.Tables[this.TName].Rows[num][1];
                    values[2] = this.Cust.Tables[this.TName].Rows[num][2];
                    values[3] = this.Cust.Tables[this.TName].Rows[num][3];
                    values[4] = this.Cust.Tables[this.TName].Rows[num][4];
                    values[5] = this.Cust.Tables[this.TName].Rows[num][5];
                    values[6] = this.Cust.Tables[this.TName].Rows[num][6];
                    values[7] = this.Cust.Tables[this.TName].Rows[num][7];
                    values[8] = this.Cust.Tables[this.TName].Rows[num][8];
                    values[9] = this.Cust.Tables[this.TName].Rows[num][9];
                    this.Sus_Gridview.Rows.Add(values);
                    num++;
                }
            }
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
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            this.TitlePanel = new Panel();
            this.Title_SalesControl = new Label();
            this.Btn_Confirm = new Button();
            this.Btn_Cancel = new Button();
            this.label1 = new Label();
            this.CartIdLabel = new Label();
            this.panel1 = new Panel();
            this.Sus_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.TitlePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.Sus_Gridview).BeginInit();
            base.SuspendLayout();
            this.TitlePanel.Controls.Add(this.Title_SalesControl);
            this.TitlePanel.Dock = DockStyle.Top;
            this.TitlePanel.Location = new Point(0, 0);
            this.TitlePanel.Margin = new Padding(8, 11, 8, 11);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new Size(0x2aa, 0x22);
            this.TitlePanel.TabIndex = 0x12;
            this.Title_SalesControl.BackColor = Color.NavajoWhite;
            this.Title_SalesControl.Dock = DockStyle.Fill;
            this.Title_SalesControl.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Title_SalesControl.Location = new Point(0, 0);
            this.Title_SalesControl.Margin = new Padding(5, 0, 5, 0);
            this.Title_SalesControl.Name = "Title_SalesControl";
            this.Title_SalesControl.Size = new Size(0x2aa, 0x22);
            this.Title_SalesControl.TabIndex = 0;
            this.Title_SalesControl.Text = "Cart Suspension Form";
            this.Title_SalesControl.TextAlign = ContentAlignment.MiddleCenter;
            this.Btn_Confirm.BackColor = Color.Maroon;
            this.Btn_Confirm.FlatStyle = FlatStyle.Flat;
            this.Btn_Confirm.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Confirm.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Confirm.Location = new Point(0x1ad, 12);
            this.Btn_Confirm.Margin = new Padding(11, 10, 11, 10);
            this.Btn_Confirm.Name = "Btn_Confirm";
            this.Btn_Confirm.Size = new Size(0xe9, 0x22);
            this.Btn_Confirm.TabIndex = 50;
            this.Btn_Confirm.Text = "Confirm Task";
            this.Btn_Confirm.UseVisualStyleBackColor = false;
            this.Btn_Confirm.Click += new EventHandler(this.Btn_Confirm_Click);
            this.Btn_Cancel.BackColor = Color.Maroon;
            this.Btn_Cancel.DialogResult = DialogResult.Cancel;
            this.Btn_Cancel.FlatStyle = FlatStyle.Flat;
            this.Btn_Cancel.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Cancel.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Cancel.Location = new Point(0xc4, 12);
            this.Btn_Cancel.Margin = new Padding(11, 10, 11, 10);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new Size(0xc3, 0x22);
            this.Btn_Cancel.TabIndex = 0x33;
            this.Btn_Cancel.Text = "Cancel Task";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new EventHandler(this.Btn_Cancel_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(5, 0x13);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x51, 0x19);
            this.label1.TabIndex = 0x34;
            this.label1.Text = "CartId : ";
            this.CartIdLabel.AutoSize = true;
            this.CartIdLabel.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.CartIdLabel.Location = new Point(80, 0x13);
            this.CartIdLabel.Margin = new Padding(4, 0, 4, 0);
            this.CartIdLabel.Name = "CartIdLabel";
            this.CartIdLabel.Size = new Size(0x24, 0x19);
            this.CartIdLabel.TabIndex = 0x35;
            this.CartIdLabel.Text = "00";
            this.panel1.Controls.Add(this.CartIdLabel);
            this.panel1.Controls.Add(this.Btn_Confirm);
            this.panel1.Controls.Add(this.Btn_Cancel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x12f);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x2aa, 0x3e);
            this.panel1.TabIndex = 0x36;
            this.Sus_Gridview.AllowUserToAddRows = false;
            this.Sus_Gridview.AllowUserToDeleteRows = false;
            this.Sus_Gridview.AllowUserToResizeColumns = false;
            this.Sus_Gridview.AllowUserToResizeRows = false;
            style.BackColor = SystemColors.ButtonHighlight;
            style.Font = new Font("Palatino Linotype", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = Color.Black;
            this.Sus_Gridview.AlternatingRowsDefaultCellStyle = style;
            this.Sus_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Sus_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            this.Sus_Gridview.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            style2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style2.ForeColor = Color.FromArgb(0, 0, 0x40);
            style2.SelectionBackColor = Color.LightSalmon;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.Sus_Gridview.ColumnHeadersDefaultCellStyle = style2;
            this.Sus_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[10];
            dataGridViewColumns[0] = this.Column1;
            dataGridViewColumns[1] = this.Column2;
            dataGridViewColumns[2] = this.Column9;
            dataGridViewColumns[3] = this.Column3;
            dataGridViewColumns[4] = this.Column4;
            dataGridViewColumns[5] = this.Column5;
            dataGridViewColumns[6] = this.Column7;
            dataGridViewColumns[7] = this.Column8;
            dataGridViewColumns[8] = this.Column10;
            dataGridViewColumns[9] = this.Column11;
            this.Sus_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Sus_Gridview.Dock = DockStyle.Fill;
            this.Sus_Gridview.EditMode = DataGridViewEditMode.EditOnEnter;
            this.Sus_Gridview.EnableHeadersVisualStyles = false;
            this.Sus_Gridview.Location = new Point(0, 0x22);
            this.Sus_Gridview.Name = "Sus_Gridview";
            this.Sus_Gridview.ReadOnly = true;
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.Sienna;
            style3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style3.ForeColor = SystemColors.WindowText;
            style3.NullValue = "X";
            style3.SelectionBackColor = SystemColors.Highlight;
            style3.SelectionForeColor = SystemColors.HighlightText;
            style3.WrapMode = DataGridViewTriState.True;
            this.Sus_Gridview.RowHeadersDefaultCellStyle = style3;
            this.Sus_Gridview.RowHeadersVisible = false;
            this.Sus_Gridview.RowHeadersWidth = 20;
            this.Sus_Gridview.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            style4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style4.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style4.NullValue = null;
            this.Sus_Gridview.RowsDefaultCellStyle = style4;
            this.Sus_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 14f);
            this.Sus_Gridview.RowTemplate.Height = 30;
            this.Sus_Gridview.Size = new Size(0x2aa, 0x10d);
            this.Sus_Gridview.TabIndex = 0x37;
            style5.BackColor = Color.White;
            this.Column1.DefaultCellStyle = style5;
            this.Column1.FillWeight = 30f;
            this.Column1.HeaderText = "ProductId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.FillWeight = 62.55304f;
            this.Column2.HeaderText = "Description";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column9.FillWeight = 20f;
            this.Column9.HeaderText = "Unit";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            style6.NullValue = null;
            this.Column3.DefaultCellStyle = style6;
            this.Column3.FillWeight = 20f;
            this.Column3.HeaderText = "Quantity";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column4.FillWeight = 20f;
            this.Column4.HeaderText = "UnitPrice";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column5.FillWeight = 25f;
            this.Column5.HeaderText = "Total";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column7.FillWeight = 15f;
            this.Column7.HeaderText = "Tax(%)";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column8.FillWeight = 15f;
            this.Column8.HeaderText = "Disc(%)";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column10.HeaderText = "Profit";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            this.Column11.HeaderText = "Tprofit";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Visible = false;
            base.AcceptButton = this.Btn_Confirm;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.Btn_Cancel;
            base.ClientSize = new Size(0x2aa, 0x16d);
            base.Controls.Add(this.Sus_Gridview);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.TitlePanel);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Margin = new Padding(4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "CartSuspend";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Suspend Cart Dialog Form";
            base.TopMost = true;
            base.Load += new EventHandler(this.CartSuspend_Load);
            this.TitlePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize) this.Sus_Gridview).EndInit();
            base.ResumeLayout(false);
        }
    }
}

