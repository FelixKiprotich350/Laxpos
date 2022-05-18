namespace LaxPos.Accounting
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class TransactionDetails : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private readonly string TransNo = "";
        private IContainer components = null;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox6;
        private Label label6;
        private GroupBox groupBox2;
        private TextBox textBox7;
        private Label label7;
        private TextBox textBox8;
        private Label label8;
        private Button Btn_Close;
        private DataGridView GridView_Payments;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private GroupBox groupBox3;
        private DataGridView Productslist_Gridview;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;

        public TransactionDetails(string Tr)
        {
            this.InitializeComponent();
            this.TransNo = Tr;
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GetTransDetails(string TransNo)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select a.TransactionNo,a.AccType,a.amount,a.PaymentMethod,a.refference,a.refference2,a.Date,a.Station,a.Balance,a.UserId,b.Gross,b.Totalpaid from accounts a,billmaster b where a.TransactionNo=@transactionno AND b.Billno=@transactionno";
                command.Parameters.AddWithValue("@transactionno", TransNo);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The transaction details cannot be found!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    int num = 0;
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        if (num == 0)
                        {
                            this.textBox1.Text = reader["TransactionNo"].ToString();
                            this.textBox2.Text = reader["AccType"].ToString();
                            this.textBox3.Text = reader["Gross"].ToString();
                            this.textBox4.Text = reader["Totalpaid"].ToString();
                            this.textBox5.Text = reader["Date"].ToString();
                            this.textBox6.Text = reader["Station"].ToString();
                            this.textBox7.Text = reader["Balance"].ToString();
                            this.textBox8.Text = reader["UserId"].ToString();
                        }
                        object[] values = new object[] { reader["Amount"].ToString(), reader["PaymentMethod"].ToString(), reader["Refference"].ToString(), reader["Refference2"].ToString() };
                        this.GridView_Payments.Rows.Add(values);
                        num++;
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void GetTransProducts(string TransNo)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from itemsales where TransNo=@transactionno";
                command.Parameters.AddWithValue("@transactionno", TransNo);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Related Products cannot be found!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["ProductCode"].ToString(), reader["Description"].ToString(), reader["Quantity"].ToString(), reader["UnitPrice"].ToString(), reader["Gross"].ToString() };
                        this.Productslist_Gridview.Rows.Add(values);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.textBox7 = new TextBox();
            this.label7 = new Label();
            this.textBox8 = new TextBox();
            this.label8 = new Label();
            this.textBox5 = new TextBox();
            this.label5 = new Label();
            this.textBox6 = new TextBox();
            this.label6 = new Label();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.textBox4 = new TextBox();
            this.label4 = new Label();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.Productslist_Gridview = new DataGridView();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Btn_Close = new Button();
            this.GridView_Payments = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((ISupportInitialize) this.Productslist_Gridview).BeginInit();
            ((ISupportInitialize) this.GridView_Payments).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox8);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Margin = new Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(4);
            this.groupBox1.Size = new Size(0x1eb, 0x85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.textBox7.BackColor = SystemColors.ButtonHighlight;
            this.textBox7.Location = new Point(340, 100);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new Size(0x91, 0x18);
            this.textBox7.TabIndex = 15;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x111, 0x67);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x3d, 0x12);
            this.label7.TabIndex = 14;
            this.label7.Text = "Balance";
            this.textBox8.BackColor = SystemColors.ButtonHighlight;
            this.textBox8.Location = new Point(0x152, 10);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new Size(0x93, 0x18);
            this.textBox8.TabIndex = 13;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(7, 0x65);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x4a, 0x12);
            this.label8.TabIndex = 12;
            this.label8.Text = "Total Paid";
            this.textBox5.BackColor = SystemColors.ButtonHighlight;
            this.textBox5.Location = new Point(340, 0x47);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0x91, 0x18);
            this.textBox5.TabIndex = 11;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x111, 0x4a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x27, 0x12);
            this.label5.TabIndex = 10;
            this.label5.Text = "Date";
            this.textBox6.BackColor = SystemColors.ButtonHighlight;
            this.textBox6.Location = new Point(340, 40);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(0x91, 0x18);
            this.textBox6.TabIndex = 9;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(7, 0x48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x6d, 0x12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Tender Amount";
            this.textBox3.BackColor = SystemColors.ButtonHighlight;
            this.textBox3.Location = new Point(0x7f, 0x47);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0x8d, 0x18);
            this.textBox3.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x111, 0x2b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3d, 0x12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Counter";
            this.textBox4.BackColor = SystemColors.ButtonHighlight;
            this.textBox4.Location = new Point(0x7f, 0x65);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0x8d, 0x18);
            this.textBox4.TabIndex = 5;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x111, 13);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 0x12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Cashier";
            this.textBox2.BackColor = SystemColors.ButtonHighlight;
            this.textBox2.Location = new Point(0x7f, 0x2b);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x8e, 0x18);
            this.textBox2.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(7, 0x2b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x62, 0x12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Account Type";
            this.textBox1.BackColor = SystemColors.ButtonHighlight;
            this.textBox1.Location = new Point(0x7f, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x8e, 0x18);
            this.textBox1.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(7, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x72, 0x12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Transaction No:";
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.Btn_Close);
            this.groupBox2.Controls.Add(this.GridView_Payments);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(0, 0x85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1eb, 0x14b);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Payments";
            this.groupBox3.Controls.Add(this.Productslist_Gridview);
            this.groupBox3.Dock = DockStyle.Top;
            this.groupBox3.Location = new Point(3, 0x77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1e5, 0xab);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Products Related";
            this.Productslist_Gridview.AllowUserToAddRows = false;
            this.Productslist_Gridview.AllowUserToDeleteRows = false;
            this.Productslist_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Productslist_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            this.Productslist_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.Column5, this.Column6 };
            this.Productslist_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Productslist_Gridview.Dock = DockStyle.Fill;
            this.Productslist_Gridview.Location = new Point(3, 20);
            this.Productslist_Gridview.Name = "Productslist_Gridview";
            this.Productslist_Gridview.ReadOnly = true;
            this.Productslist_Gridview.RowHeadersVisible = false;
            this.Productslist_Gridview.Size = new Size(0x1df, 0x94);
            this.Productslist_Gridview.TabIndex = 3;
            this.dataGridViewTextBoxColumn2.HeaderText = "Code";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.HeaderText = "Qty";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.Column5.HeaderText = "Price";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column6.HeaderText = "Total";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Btn_Close.Location = new Point(0xad, 0x128);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x9f, 0x1d);
            this.Btn_Close.TabIndex = 0;
            this.Btn_Close.Text = "Close Transaction";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            this.GridView_Payments.AllowUserToAddRows = false;
            this.GridView_Payments.AllowUserToDeleteRows = false;
            this.GridView_Payments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.GridView_Payments.BackgroundColor = SystemColors.ButtonHighlight;
            this.GridView_Payments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 };
            this.GridView_Payments.Columns.AddRange(columnArray2);
            this.GridView_Payments.Dock = DockStyle.Top;
            this.GridView_Payments.Location = new Point(3, 20);
            this.GridView_Payments.Name = "GridView_Payments";
            this.GridView_Payments.ReadOnly = true;
            this.GridView_Payments.RowHeadersVisible = false;
            this.GridView_Payments.Size = new Size(0x1e5, 0x63);
            this.GridView_Payments.TabIndex = 1;
            this.Column1.HeaderText = "Amount";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.HeaderText = "Method";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column3.HeaderText = "Refference";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column4.HeaderText = "Refference 2";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            base.AcceptButton = this.Btn_Close;
            base.AutoScaleDimensions = new SizeF(9f, 18f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1eb, 0x1d0);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            this.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.Margin = new Padding(4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TransactionDetails";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Transaction Details";
            base.TopMost = true;
            base.Load += new EventHandler(this.TransactionDetails_Load);
            base.Shown += new EventHandler(this.TransactionDetails_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((ISupportInitialize) this.Productslist_Gridview).EndInit();
            ((ISupportInitialize) this.GridView_Payments).EndInit();
            base.ResumeLayout(false);
        }

        private void TransactionDetails_Load(object sender, EventArgs e)
        {
            base.ActiveControl = this.Btn_Close;
        }

        private void TransactionDetails_Shown(object sender, EventArgs e)
        {
            this.GetTransDetails(this.TransNo);
            this.GetTransProducts(this.TransNo);
        }
    }
}

