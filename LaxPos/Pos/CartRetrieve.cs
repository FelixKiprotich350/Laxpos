namespace LaxPos.Pos
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class CartRetrieve : Form
    {
        private readonly DataSet Cust;
        private IContainer components = null;
        private Panel TitlePanel;
        private Label Title_SalesControl;
        private Panel panel1;
        private Label CartIdLabel;
        private Label label2;
        private Button Btn_Cancel;
        private Button Btn_Confirm;
        private DataGridView CartList_Gridview;
        private DataGridViewTextBoxColumn Column6;
        private Button Btn_DeleteCart;
        private Panel panel2;
        public DataGridView CartItems_Gridview;
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

        public CartRetrieve(DataSet Dset)
        {
            this.InitializeComponent();
            this.Cust = Dset;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void Btn_Confirm_Click(object sender, EventArgs e)
        {
            if (this.CartList_Gridview.Rows.Count <= 0)
            {
                MessageBox.Show("You have No Carts To Restore !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.CartList_Gridview.CurrentRow.Cells[0].Value.ToString() != this.CartIdLabel.Text)
            {
                MessageBox.Show("Select the cart to restore !", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.Cust.Tables[this.CartIdLabel.Text].TableName = "999";
                base.DialogResult = DialogResult.OK;
            }
        }

        private void Btn_DeleteCart_Click(object sender, EventArgs e)
        {
            if (this.CartList_Gridview.Rows.Count <= 0)
            {
                MessageBox.Show("You have No Carts To Remove !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if ((MessageBox.Show("Do you want to Delete the Suspended Cart ?", "Confirmation Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) && (this.CartList_Gridview.CurrentRow.Cells[0].Value.ToString() == this.CartIdLabel.Text))
            {
                this.Cust.Tables.Remove(this.CartIdLabel.Text);
                this.LoadItems(this.CartIdLabel.Text);
                this.CartIdLabel.Text = this.CartList_Gridview.Rows[0].Cells[0].Value.ToString();
                MessageBox.Show("You have successfully removed the cart.\nCartId Number :" + this.CartIdLabel.Text, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void CartList_Gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.LoadItems(this.CartList_Gridview.CurrentRow.Cells[0].Value.ToString());
            this.CartIdLabel.Text = this.CartList_Gridview.CurrentRow.Cells[0].Value.ToString();
        }

        private void CartRetrieve_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadCartList();
            }
            catch
            {
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
            DataGridViewCellStyle style7 = new DataGridViewCellStyle();
            DataGridViewCellStyle style8 = new DataGridViewCellStyle();
            this.TitlePanel = new Panel();
            this.Title_SalesControl = new Label();
            this.panel1 = new Panel();
            this.CartList_Gridview = new DataGridView();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.CartIdLabel = new Label();
            this.label2 = new Label();
            this.Btn_Cancel = new Button();
            this.Btn_Confirm = new Button();
            this.Btn_DeleteCart = new Button();
            this.panel2 = new Panel();
            this.CartItems_Gridview = new DataGridView();
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
            ((ISupportInitialize) this.CartList_Gridview).BeginInit();
            this.panel2.SuspendLayout();
            ((ISupportInitialize) this.CartItems_Gridview).BeginInit();
            base.SuspendLayout();
            this.TitlePanel.Controls.Add(this.Title_SalesControl);
            this.TitlePanel.Dock = DockStyle.Top;
            this.TitlePanel.Location = new Point(0, 0);
            this.TitlePanel.Margin = new Padding(11, 14, 11, 14);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new Size(790, 0x18);
            this.TitlePanel.TabIndex = 0x13;
            this.Title_SalesControl.BackColor = Color.NavajoWhite;
            this.Title_SalesControl.Dock = DockStyle.Fill;
            this.Title_SalesControl.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Title_SalesControl.Location = new Point(0, 0);
            this.Title_SalesControl.Margin = new Padding(7, 0, 7, 0);
            this.Title_SalesControl.Name = "Title_SalesControl";
            this.Title_SalesControl.Size = new Size(790, 0x18);
            this.Title_SalesControl.TabIndex = 0;
            this.Title_SalesControl.Text = "Cart Retrieval Form";
            this.Title_SalesControl.TextAlign = ContentAlignment.MiddleCenter;
            this.panel1.Controls.Add(this.CartList_Gridview);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0x18);
            this.panel1.Margin = new Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x86, 0x173);
            this.panel1.TabIndex = 20;
            this.CartList_Gridview.AllowUserToAddRows = false;
            this.CartList_Gridview.AllowUserToDeleteRows = false;
            this.CartList_Gridview.AllowUserToOrderColumns = true;
            this.CartList_Gridview.AllowUserToResizeColumns = false;
            this.CartList_Gridview.AllowUserToResizeRows = false;
            this.CartList_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.CartList_Gridview.ColumnHeadersDefaultCellStyle = style;
            this.CartList_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column6 };
            this.CartList_Gridview.Columns.AddRange(dataGridViewColumns);
            this.CartList_Gridview.Location = new Point(5, 4);
            this.CartList_Gridview.Margin = new Padding(4);
            this.CartList_Gridview.Name = "CartList_Gridview";
            this.CartList_Gridview.RowHeadersVisible = false;
            this.CartList_Gridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.CartList_Gridview.Size = new Size(0x7d, 0x16b);
            this.CartList_Gridview.TabIndex = 2;
            this.CartList_Gridview.CellClick += new DataGridViewCellEventHandler(this.CartList_Gridview_CellContentClick);
            this.CartList_Gridview.CellContentClick += new DataGridViewCellEventHandler(this.CartList_Gridview_CellContentClick);
            style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = style2;
            this.Column6.HeaderText = "CartID";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.CartIdLabel.AutoSize = true;
            this.CartIdLabel.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.CartIdLabel.Location = new Point(0x61, 12);
            this.CartIdLabel.Margin = new Padding(4, 0, 4, 0);
            this.CartIdLabel.Name = "CartIdLabel";
            this.CartIdLabel.Size = new Size(0x24, 0x19);
            this.CartIdLabel.TabIndex = 0x39;
            this.CartIdLabel.Text = "00";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(8, 12);
            this.label2.Margin = new Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x51, 0x19);
            this.label2.TabIndex = 0x38;
            this.label2.Text = "CartId : ";
            this.Btn_Cancel.BackColor = Color.Maroon;
            this.Btn_Cancel.DialogResult = DialogResult.Cancel;
            this.Btn_Cancel.FlatStyle = FlatStyle.Flat;
            this.Btn_Cancel.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Cancel.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Cancel.Location = new Point(0xa9, 7);
            this.Btn_Cancel.Margin = new Padding(11, 10, 11, 10);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new Size(0x72, 0x22);
            this.Btn_Cancel.TabIndex = 0x37;
            this.Btn_Cancel.Text = "Close";
            this.Btn_Cancel.UseVisualStyleBackColor = false;
            this.Btn_Cancel.Click += new EventHandler(this.Btn_Cancel_Click);
            this.Btn_Confirm.BackColor = Color.Maroon;
            this.Btn_Confirm.FlatStyle = FlatStyle.Flat;
            this.Btn_Confirm.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Confirm.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Confirm.Location = new Point(0x1e1, 7);
            this.Btn_Confirm.Margin = new Padding(11, 10, 11, 10);
            this.Btn_Confirm.Name = "Btn_Confirm";
            this.Btn_Confirm.Size = new Size(0x9b, 0x22);
            this.Btn_Confirm.TabIndex = 0x36;
            this.Btn_Confirm.Text = "Restore Cart";
            this.Btn_Confirm.UseVisualStyleBackColor = false;
            this.Btn_Confirm.Click += new EventHandler(this.Btn_Confirm_Click);
            this.Btn_DeleteCart.BackColor = Color.Maroon;
            this.Btn_DeleteCart.DialogResult = DialogResult.Cancel;
            this.Btn_DeleteCart.FlatStyle = FlatStyle.Flat;
            this.Btn_DeleteCart.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_DeleteCart.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_DeleteCart.Location = new Point(0x145, 7);
            this.Btn_DeleteCart.Margin = new Padding(11, 10, 11, 10);
            this.Btn_DeleteCart.Name = "Btn_DeleteCart";
            this.Btn_DeleteCart.Size = new Size(0x72, 0x22);
            this.Btn_DeleteCart.TabIndex = 0x38;
            this.Btn_DeleteCart.Text = "Remove Cart";
            this.Btn_DeleteCart.UseVisualStyleBackColor = false;
            this.Btn_DeleteCart.Click += new EventHandler(this.Btn_DeleteCart_Click);
            this.panel2.Controls.Add(this.Btn_Cancel);
            this.panel2.Controls.Add(this.Btn_Confirm);
            this.panel2.Controls.Add(this.Btn_DeleteCart);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.CartIdLabel);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0x86, 0x14c);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x290, 0x3f);
            this.panel2.TabIndex = 0x3a;
            this.CartItems_Gridview.AllowUserToAddRows = false;
            this.CartItems_Gridview.AllowUserToDeleteRows = false;
            this.CartItems_Gridview.AllowUserToResizeColumns = false;
            this.CartItems_Gridview.AllowUserToResizeRows = false;
            style3.BackColor = SystemColors.ButtonHighlight;
            style3.Font = new Font("Palatino Linotype", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style3.ForeColor = Color.Black;
            this.CartItems_Gridview.AlternatingRowsDefaultCellStyle = style3;
            this.CartItems_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.CartItems_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            this.CartItems_Gridview.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            style4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style4.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            style4.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style4.ForeColor = Color.FromArgb(0, 0, 0x40);
            style4.SelectionBackColor = Color.LightSalmon;
            style4.SelectionForeColor = SystemColors.HighlightText;
            style4.WrapMode = DataGridViewTriState.True;
            this.CartItems_Gridview.ColumnHeadersDefaultCellStyle = style4;
            this.CartItems_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[10];
            columnArray2[0] = this.Column1;
            columnArray2[1] = this.Column2;
            columnArray2[2] = this.Column9;
            columnArray2[3] = this.Column3;
            columnArray2[4] = this.Column4;
            columnArray2[5] = this.Column5;
            columnArray2[6] = this.Column7;
            columnArray2[7] = this.Column8;
            columnArray2[8] = this.Column10;
            columnArray2[9] = this.Column11;
            this.CartItems_Gridview.Columns.AddRange(columnArray2);
            this.CartItems_Gridview.Dock = DockStyle.Fill;
            this.CartItems_Gridview.EditMode = DataGridViewEditMode.EditOnEnter;
            this.CartItems_Gridview.EnableHeadersVisualStyles = false;
            this.CartItems_Gridview.Location = new Point(0x86, 0x18);
            this.CartItems_Gridview.Name = "CartItems_Gridview";
            this.CartItems_Gridview.ReadOnly = true;
            style5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style5.BackColor = Color.Sienna;
            style5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style5.ForeColor = SystemColors.WindowText;
            style5.NullValue = "X";
            style5.SelectionBackColor = SystemColors.Highlight;
            style5.SelectionForeColor = SystemColors.HighlightText;
            style5.WrapMode = DataGridViewTriState.True;
            this.CartItems_Gridview.RowHeadersDefaultCellStyle = style5;
            this.CartItems_Gridview.RowHeadersVisible = false;
            this.CartItems_Gridview.RowHeadersWidth = 20;
            this.CartItems_Gridview.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            style6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style6.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style6.NullValue = null;
            this.CartItems_Gridview.RowsDefaultCellStyle = style6;
            this.CartItems_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 14f);
            this.CartItems_Gridview.RowTemplate.Height = 30;
            this.CartItems_Gridview.Size = new Size(0x290, 0x134);
            this.CartItems_Gridview.TabIndex = 0x3b;
            style7.BackColor = Color.White;
            this.Column1.DefaultCellStyle = style7;
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
            style8.NullValue = null;
            this.Column3.DefaultCellStyle = style8;
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
            base.ClientSize = new Size(790, 0x18b);
            base.Controls.Add(this.CartItems_Gridview);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.TitlePanel);
            this.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Margin = new Padding(4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "CartRetrieve";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Retrieve Customer Cart";
            base.Load += new EventHandler(this.CartRetrieve_Load);
            this.TitlePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.CartList_Gridview).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((ISupportInitialize) this.CartItems_Gridview).EndInit();
            base.ResumeLayout(false);
        }

        public void LoadCartList()
        {
            this.CartList_Gridview.Rows.Clear();
            if (this.Cust.Tables.Count <= 0)
            {
                MessageBox.Show("You Have No Suspended items!!", "Warning Message Box", MessageBoxButtons.OK);
            }
            else
            {
                int num = 0;
                while (true)
                {
                    if (num >= this.Cust.Tables.Count)
                    {
                        this.LoadItems(this.CartList_Gridview.Rows[0].Cells[0].Value.ToString());
                        this.CartIdLabel.Text = this.CartList_Gridview.Rows[0].Cells[0].Value.ToString();
                        break;
                    }
                    object[] values = new object[] { this.Cust.Tables[num] };
                    this.CartList_Gridview.Rows.Add(values);
                    num++;
                }
            }
        }

        public void LoadItems(string Tname)
        {
            try
            {
                this.CartItems_Gridview.Rows.Clear();
                int num = 0;
                while (true)
                {
                    if (num >= this.Cust.Tables.Count)
                    {
                        break;
                    }
                    if (this.Cust.Tables[num].TableName == Tname)
                    {
                        string tableName = this.Cust.Tables[num].TableName;
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= this.Cust.Tables[num].Rows.Count)
                            {
                                break;
                            }
                            object[] values = new object[10];
                            values[0] = this.Cust.Tables[num].Rows[num2][0];
                            values[1] = this.Cust.Tables[num].Rows[num2][1];
                            values[2] = this.Cust.Tables[num].Rows[num2][2];
                            values[3] = this.Cust.Tables[num].Rows[num2][3];
                            values[4] = this.Cust.Tables[num].Rows[num2][4];
                            values[5] = this.Cust.Tables[num].Rows[num2][5];
                            values[6] = this.Cust.Tables[num].Rows[num2][6];
                            values[7] = this.Cust.Tables[num].Rows[num2][7];
                            values[8] = this.Cust.Tables[num].Rows[num2][8];
                            values[9] = this.Cust.Tables[num].Rows[num2][9];
                            this.CartItems_Gridview.Rows.Add(values);
                            num2++;
                        }
                    }
                    num++;
                }
            }
            catch
            {
            }
        }
    }
}

