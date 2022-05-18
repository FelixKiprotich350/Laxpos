namespace LaxPos.Pos
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class InvoicePay : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private readonly CompanyProfile Client = new CompanyProfile();
        private static string Title = "";
        private static string Box = "";
        private static string Email = "";
        private static string Tel = "";
        private static string Website = "";
        private static string Pin = "";
        private static string Footer1 = "";
        private static string Footer2 = "";
        public int Center_X = 150;
        public decimal TaxPercentage = 0M;
        public decimal TaxAmt = 0M;
        public decimal AmountPaid = 0M;
        public decimal Balance = 0M;
        public decimal GrossTotal = 0M;
        private IContainer components = null;
        private GroupBox panel1;
        private GroupBox panel2;
        private DataGridView Invoices_dataGridView;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private TextBox textBox3;
        private Label label1;
        private TextBox textBox6;
        private Label label4;
        private TextBox textBox5;
        private Label label3;
        private TextBox textBox8;
        private Label label6;
        private TextBox textBox9;
        private Label label7;
        private TextBox textBox10;
        private Label label8;
        private Button Btn_Checkout;
        private Label Txt_TransactionNo;
        private Label label5;
        private TextBox Tax_Text;
        private Label label9;
        private Panel panel3;
        private GroupBox groupBox1;
        private Button Btn_SearchByUser;
        private TextBox textBox2;
        private GroupBox groupBox2;
        private Button Btn_SearchByInvoice;
        private TextBox textBox1;
        private Button Btn_ViewAll_Invoices;
        private Button Btn_ClearForm;

        public InvoicePay()
        {
            this.InitializeComponent();
        }

        private void Btn_Checkout_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.textBox5.Text == "") || (this.textBox6.Text == ""))
                {
                    MessageBox.Show("Cannot process your request", "message box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    decimal totalAmount = Convert.ToDecimal(this.textBox6.Text);
                    Transactions transactions = new Transactions(totalAmount, new CustomerCart.Table_PaymentsDataTable());
                    if (transactions.ShowDialog(this) == DialogResult.OK)
                    {
                        this.GrossTotal = totalAmount;
                        this.AmountPaid = Convert.ToDecimal(transactions.Txt_AmountPaidTotal.Text.ToString());
                        this.Balance = Convert.ToDecimal(transactions.Txt_Balance.Text.ToString());
                        this.Tax_Text.Text = Convert.ToDecimal((decimal) ((this.Client.ClientTaxRate * this.GrossTotal) / 100M)).ToString();
                        this.InsertPayment();
                        this.PrintReceipt();
                        if (MessageBox.Show("Success", "Message Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                        {
                            this.ResetForm();
                            this.LoadInvoices("");
                        }
                        this.GrossTotal = 0M;
                        this.AmountPaid = 0M;
                        this.Balance = 0M;
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_ClearForm_Click(object sender, EventArgs e)
        {
            this.ResetForm();
        }

        private void Btn_SearchByInvoice_Click(object sender, EventArgs e)
        {
            this.SearchByInvoice(this.textBox1.Text);
        }

        private void Btn_SearchByUser_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text != "")
            {
                this.LoadInvoices(this.textBox2.Text);
            }
            else
            {
                MessageBox.Show("The customer Id should not be empty!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_ViewAll_Invoices_Click(object sender, EventArgs e)
        {
            this.LoadInvoices("");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GenerateId()
        {
            try
            {
                string str = Program.CurrentDateTime().ToString("ddHHmmssffff");
                this.Txt_TransactionNo.Text = str;
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR OCCURED", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            this.panel1 = new GroupBox();
            this.Invoices_dataGridView = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.panel3 = new Panel();
            this.groupBox1 = new GroupBox();
            this.Btn_SearchByUser = new Button();
            this.textBox2 = new TextBox();
            this.groupBox2 = new GroupBox();
            this.Btn_ViewAll_Invoices = new Button();
            this.Btn_SearchByInvoice = new Button();
            this.textBox1 = new TextBox();
            this.panel2 = new GroupBox();
            this.Btn_ClearForm = new Button();
            this.Tax_Text = new TextBox();
            this.label9 = new Label();
            this.Txt_TransactionNo = new Label();
            this.label5 = new Label();
            this.Btn_Checkout = new Button();
            this.textBox8 = new TextBox();
            this.label6 = new Label();
            this.textBox9 = new TextBox();
            this.label7 = new Label();
            this.textBox10 = new TextBox();
            this.label8 = new Label();
            this.textBox6 = new TextBox();
            this.label4 = new Label();
            this.textBox5 = new TextBox();
            this.label3 = new Label();
            this.textBox3 = new TextBox();
            this.label1 = new Label();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.Invoices_dataGridView).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.Invoices_dataGridView);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Margin = new Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(4);
            this.panel1.Size = new Size(0x213, 0x18b);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = false;
            this.panel1.Text = "Invoice SearchBox";
            this.Invoices_dataGridView.AllowUserToAddRows = false;
            this.Invoices_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Invoices_dataGridView.BackgroundColor = SystemColors.Control;
            this.Invoices_dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 };
            this.Invoices_dataGridView.Columns.AddRange(dataGridViewColumns);
            this.Invoices_dataGridView.Dock = DockStyle.Fill;
            this.Invoices_dataGridView.Location = new Point(4, 0xb3);
            this.Invoices_dataGridView.Margin = new Padding(4);
            this.Invoices_dataGridView.MultiSelect = false;
            this.Invoices_dataGridView.Name = "Invoices_dataGridView";
            this.Invoices_dataGridView.ReadOnly = true;
            this.Invoices_dataGridView.RowHeadersVisible = false;
            this.Invoices_dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Invoices_dataGridView.Size = new Size(0x20b, 0xd4);
            this.Invoices_dataGridView.TabIndex = 3;
            this.Invoices_dataGridView.DoubleClick += new EventHandler(this.Invoices_dataGridView_DoubleClick);
            this.Column1.HeaderText = "Customer";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.HeaderText = "InvoiceNo";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column3.HeaderText = "InvoiceDate";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column4.HeaderText = "Amount";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = DockStyle.Top;
            this.panel3.Location = new Point(4, 20);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x20b, 0x9f);
            this.panel3.TabIndex = 4;
            this.groupBox1.Controls.Add(this.Btn_SearchByUser);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Dock = DockStyle.Left;
            this.groupBox1.Location = new Point(0xfe, 0);
            this.groupBox1.Margin = new Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(4);
            this.groupBox1.Size = new Size(0x109, 0x9f);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search By CustomerId";
            this.Btn_SearchByUser.Location = new Point(0x1f, 0x3f);
            this.Btn_SearchByUser.Margin = new Padding(4);
            this.Btn_SearchByUser.Name = "Btn_SearchByUser";
            this.Btn_SearchByUser.Size = new Size(0xc0, 0x1c);
            this.Btn_SearchByUser.TabIndex = 3;
            this.Btn_SearchByUser.Text = "Search Customer";
            this.Btn_SearchByUser.UseVisualStyleBackColor = true;
            this.Btn_SearchByUser.Click += new EventHandler(this.Btn_SearchByUser_Click);
            this.textBox2.Location = new Point(9, 0x15);
            this.textBox2.Margin = new Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0xf5, 0x17);
            this.textBox2.TabIndex = 2;
            this.groupBox2.Controls.Add(this.Btn_ViewAll_Invoices);
            this.groupBox2.Controls.Add(this.Btn_SearchByInvoice);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Dock = DockStyle.Left;
            this.groupBox2.Location = new Point(0, 0);
            this.groupBox2.Margin = new Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(4);
            this.groupBox2.Size = new Size(0xfe, 0x9f);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search By Invoice Number";
            this.Btn_ViewAll_Invoices.Location = new Point(0x1f, 0x68);
            this.Btn_ViewAll_Invoices.Margin = new Padding(4);
            this.Btn_ViewAll_Invoices.Name = "Btn_ViewAll_Invoices";
            this.Btn_ViewAll_Invoices.Size = new Size(0xc0, 0x1c);
            this.Btn_ViewAll_Invoices.TabIndex = 4;
            this.Btn_ViewAll_Invoices.Text = "View All Invoices";
            this.Btn_ViewAll_Invoices.UseVisualStyleBackColor = true;
            this.Btn_ViewAll_Invoices.Click += new EventHandler(this.Btn_ViewAll_Invoices_Click);
            this.Btn_SearchByInvoice.Location = new Point(0x16, 0x3e);
            this.Btn_SearchByInvoice.Margin = new Padding(4);
            this.Btn_SearchByInvoice.Name = "Btn_SearchByInvoice";
            this.Btn_SearchByInvoice.Size = new Size(0xd3, 0x1c);
            this.Btn_SearchByInvoice.TabIndex = 1;
            this.Btn_SearchByInvoice.Text = "Search Invoice";
            this.Btn_SearchByInvoice.UseVisualStyleBackColor = true;
            this.Btn_SearchByInvoice.Click += new EventHandler(this.Btn_SearchByInvoice_Click);
            this.textBox1.Location = new Point(12, 0x17);
            this.textBox1.Margin = new Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xea, 0x17);
            this.textBox1.TabIndex = 0;
            this.panel2.Controls.Add(this.Btn_ClearForm);
            this.panel2.Controls.Add(this.Tax_Text);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.Txt_TransactionNo);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.Btn_Checkout);
            this.panel2.Controls.Add(this.textBox8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox10);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0x213, 0);
            this.panel2.Margin = new Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new Padding(4);
            this.panel2.Size = new Size(490, 0x18b);
            this.panel2.TabIndex = 1;
            this.panel2.TabStop = false;
            this.panel2.Text = "Invoice Details";
            this.Btn_ClearForm.BackColor = Color.Chocolate;
            this.Btn_ClearForm.FlatStyle = FlatStyle.Flat;
            this.Btn_ClearForm.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_ClearForm.ForeColor = Color.White;
            this.Btn_ClearForm.Location = new Point(0x12, 0x15c);
            this.Btn_ClearForm.Name = "Btn_ClearForm";
            this.Btn_ClearForm.Size = new Size(0xa9, 0x23);
            this.Btn_ClearForm.TabIndex = 0x13;
            this.Btn_ClearForm.Text = "ClearForm";
            this.Btn_ClearForm.UseVisualStyleBackColor = false;
            this.Btn_ClearForm.Click += new EventHandler(this.Btn_ClearForm_Click);
            this.Tax_Text.BackColor = SystemColors.ButtonHighlight;
            this.Tax_Text.Location = new Point(0x7b, 0xa2);
            this.Tax_Text.Margin = new Padding(4);
            this.Tax_Text.Name = "Tax_Text";
            this.Tax_Text.ReadOnly = true;
            this.Tax_Text.Size = new Size(0xbb, 0x17);
            this.Tax_Text.TabIndex = 0x12;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(14, 0xa2);
            this.label9.Margin = new Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1f, 0x11);
            this.label9.TabIndex = 0x11;
            this.label9.Text = "Tax";
            this.Txt_TransactionNo.AutoSize = true;
            this.Txt_TransactionNo.Location = new Point(0x7c, 0x13e);
            this.Txt_TransactionNo.Name = "Txt_TransactionNo";
            this.Txt_TransactionNo.Size = new Size(0, 0x11);
            this.Txt_TransactionNo.TabIndex = 0x10;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(14, 0x13e);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x69, 0x11);
            this.label5.TabIndex = 15;
            this.label5.Text = "Transaction No";
            this.Btn_Checkout.BackColor = Color.Chocolate;
            this.Btn_Checkout.Enabled = false;
            this.Btn_Checkout.FlatStyle = FlatStyle.Flat;
            this.Btn_Checkout.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Checkout.ForeColor = Color.White;
            this.Btn_Checkout.Location = new Point(0xc5, 0x15c);
            this.Btn_Checkout.Name = "Btn_Checkout";
            this.Btn_Checkout.Size = new Size(0xa9, 0x23);
            this.Btn_Checkout.TabIndex = 14;
            this.Btn_Checkout.Text = "CheckOut";
            this.Btn_Checkout.UseVisualStyleBackColor = false;
            this.Btn_Checkout.Click += new EventHandler(this.Btn_Checkout_Click);
            this.textBox8.BackColor = SystemColors.ButtonHighlight;
            this.textBox8.Location = new Point(0x7c, 0xf1);
            this.textBox8.Margin = new Padding(4);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new Size(0xba, 0x17);
            this.textBox8.TabIndex = 13;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(14, 0xeb);
            this.label6.Margin = new Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x56, 0x11);
            this.label6.TabIndex = 12;
            this.label6.Text = "Invoice Date";
            this.textBox9.BackColor = SystemColors.ButtonHighlight;
            this.textBox9.Location = new Point(0x7b, 0x115);
            this.textBox9.Margin = new Padding(4);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new Size(0xba, 0x17);
            this.textBox9.TabIndex = 11;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(14, 0x115);
            this.label7.Margin = new Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x52, 0x11);
            this.label7.TabIndex = 10;
            this.label7.Text = "Items Count";
            this.textBox10.BackColor = SystemColors.ButtonHighlight;
            this.textBox10.Location = new Point(0x7c, 0xc5);
            this.textBox10.Margin = new Padding(4);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new Size(0xba, 0x17);
            this.textBox10.TabIndex = 9;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(14, 0xc5);
            this.label8.Margin = new Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new Size(100, 0x11);
            this.label8.TabIndex = 8;
            this.label8.Text = "Expected Date";
            this.textBox6.BackColor = SystemColors.ButtonHighlight;
            this.textBox6.Location = new Point(0x7c, 0x7c);
            this.textBox6.Margin = new Padding(4);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(0xbb, 0x17);
            this.textBox6.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(14, 0x7c);
            this.label4.Margin = new Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x38, 0x11);
            this.label4.TabIndex = 6;
            this.label4.Text = "Amount";
            this.textBox5.BackColor = SystemColors.ButtonHighlight;
            this.textBox5.Location = new Point(0x7b, 0x55);
            this.textBox5.Margin = new Padding(4);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0xbb, 0x17);
            this.textBox5.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(14, 0x59);
            this.label3.Margin = new Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4a, 0x11);
            this.label3.TabIndex = 4;
            this.label3.Text = "Invoice No";
            this.textBox3.BackColor = SystemColors.ButtonHighlight;
            this.textBox3.Location = new Point(0x7b, 0x29);
            this.textBox3.Margin = new Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0xbb, 0x17);
            this.textBox3.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 0x2c);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x55, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer ID";
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x3fd, 0x18b);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            this.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Margin = new Padding(4);
            base.Name = "InvoicePay";
            this.Text = "Invoice Payment";
            base.Load += new EventHandler(this.InvoicePay_Load);
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.Invoices_dataGridView).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

        public void InsertPayment()
        {
            MySqlTransaction transaction = null;
            try
            {
                try
                {
                    if ((Program.CurrLoggedInUser.UserID == null) || (Program.CurrLoggedInUser.UserFirstname == null))
                    {
                        MessageBox.Show("UserId is null! Cannot complete transaction!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                        connection.Open();
                        if (this.AmountPaid < this.GrossTotal)
                        {
                            MessageBox.Show("The Amount Paid Is Less !!", "ERROR MESSAGE");
                        }
                        else
                        {
                            transaction = connection.BeginTransaction();
                            MySqlCommand command = new MySqlCommand();
                            command = new MySqlCommand("update invoicemaster set status=@status where InvoiceNo=@InvoiceNo;", connection, transaction);
                            command.Parameters.AddWithValue("@InvoiceNo", this.textBox5.Text);
                            command.Parameters.AddWithValue("@status", 1);
                            int num = command.ExecuteNonQuery();
                            command.Parameters.Clear();
                            command.Dispose();
                            if (num <= 0)
                            {
                                MessageBox.Show("Failed to complete your transaction!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                            {
                                transaction.Commit();
                                this.GenerateId();
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    MessageBox.Show("The following error occured:\n\n" + exception.Message, "ERROR OCCURED", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.GenerateId();
                }
            }
            finally
            {
            }
        }

        private void InvoicePay_Load(object sender, EventArgs e)
        {
            this.GenerateId();
            this.LoadReceiptSettings();
        }

        private void Invoices_dataGridView_DoubleClick(object sender, EventArgs e)
        {
            string invoiceNo = this.Invoices_dataGridView.CurrentRow.Cells[1].Value.ToString();
            this.SearchByInvoice(invoiceNo);
        }

        private void LoadInvoices(string CustRefference)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                string cmdText = "SELECT * FROM invoicemaster WHERE status=@status;";
                if (CustRefference != "")
                {
                    cmdText = "SELECT * FROM invoicemaster WHERE status=@status AND CustRef=@CustRef;";
                }
                MySqlCommand command = new MySqlCommand(cmdText, connection);
                command.Parameters.AddWithValue("@invoice", this.textBox1.Text.Trim());
                command.Parameters.AddWithValue("@status", "0");
                command.Parameters.AddWithValue("@CustRef", this.textBox2.Text);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("NO Invoices found", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.Invoices_dataGridView.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["CustRef"].ToString(), reader["InvoiceNo"].ToString(), reader["InvoiceDate"].ToString(), reader["Total"].ToString() };
                        this.Invoices_dataGridView.Rows.Add(values);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void LoadReceiptSettings()
        {
            Title = this.Client.ClientTitle;
            Box = this.Client.ClientAddress;
            Email = this.Client.ClientEmail;
            Tel = this.Client.ClientTel;
            Website = this.Client.ClientText4;
            Pin = this.Client.ClientPin;
            Footer1 = this.Client.ClientText2;
            Footer2 = this.Client.ClientText3;
            this.TaxPercentage = this.Client.ClientTaxRate;
        }

        public void PrintReceipt()
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += new PrintPageEventHandler(this.ProvideContent);
            document.PrintController = new StandardPrintController();
            document.Print();
        }

        public void ProvideContent(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            int num = 10;
            StringFormat format1 = new StringFormat();
            format1.LineAlignment = StringAlignment.Center;
            format1.Alignment = StringAlignment.Center;
            StringFormat format = format1;
            graphics.DrawString(Title, new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(Box, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(Tel, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(Email, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(Pin.ToUpper(), new Font("Arial", 12f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString("Sales Receipt", new Font("Palatino Linotype", 15f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            graphics.DrawString("____________", new Font("Palatino Linotype", 15f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 15;
            graphics.DrawString("BillNo:" + this.Txt_TransactionNo.Text, new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
            num += 20;
            graphics.DrawString("Date:" + Program.CurrentDateTime().ToShortDateString(), new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
            graphics.DrawString("Counter : " + Program.LogInCounter, new Font("Arial", 10f), new SolidBrush(Color.Black), 120f, (float) num);
            num += 20;
            graphics.DrawString("Time : " + Program.CurrentDateTime().ToShortTimeString(), new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
            graphics.DrawString("Served By: " + Program.CurrLoggedInUser.UserFirstname, new Font("Arial", 10f), new SolidBrush(Color.Black), 120f, (float) num);
            num += 10;
            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
            num += 10;
            graphics.DrawString("Item                              Qty Price    Total", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float) num);
            graphics.DrawString("______________________________________", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
            num += 20;
            graphics.DrawString("Invoice Payment : " + this.textBox5.Text, new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
            num += 15;
            string[] textArray1 = new string[] { "                                                     ", 1.ToString(), " *  ", this.GrossTotal.ToString(), "   ", this.GrossTotal.ToString() };
            graphics.DrawString(string.Concat(textArray1), new Font("Arial", 8f), new SolidBrush(Color.Black), 0f, (float) num);
            num += 20;
            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float) num);
            num += 15;
            graphics.DrawString("TOTAL :", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
            graphics.DrawString(this.GrossTotal.ToString(), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 200f, (float) num);
            num += 20;
            graphics.DrawString("Amount Paid :", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
            graphics.DrawString(this.AmountPaid.ToString(), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 200f, (float) num);
            num += 20;
            graphics.DrawString("Balance", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
            graphics.DrawString(this.Balance.ToString(), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 200f, (float) num);
            num += 10;
            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
            num += 15;
            graphics.DrawString("Tax%        TaxAmt", new Font("Arial", 10f, FontStyle.Underline), new SolidBrush(Color.Black), 70f, (float) num);
            num += 15;
            graphics.DrawString(this.TaxPercentage.ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 80f, (float) num);
            graphics.DrawString(this.Tax_Text.Text.ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 135f, (float) num);
            num += 10;
            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
            num += 20;
            graphics.DrawString(Footer1, new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(Footer2, new Font("Arial", 10f, FontStyle.Italic), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 15;
            graphics.DrawString(Website, new Font("Arial", 8f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
        }

        private void ResetForm()
        {
            this.textBox5.Text = "";
            this.textBox8.Text = "";
            this.textBox9.Text = "";
            this.textBox6.Text = "";
            this.textBox3.Text = "";
            this.textBox10.Text = "";
            this.Tax_Text.Text = "";
        }

        public void SearchByInvoice(string InvoiceNo)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM invoicemaster WHERE InvoiceNo=@invoice;", connection);
                command.Parameters.AddWithValue("@invoice", InvoiceNo);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    this.Btn_Checkout.Enabled = false;
                    MessageBox.Show("The invoice number does not exist!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.ResetForm();
                }
                else
                {
                    decimal @decimal = 0M;
                    int num2 = 0;
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            if (num2 <= 0)
                            {
                                this.Btn_Checkout.Enabled = false;
                            }
                            else
                            {
                                this.Btn_Checkout.Enabled = false;
                                MessageBox.Show("The invoice has been paid off", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                this.ResetForm();
                            }
                            break;
                        }
                        @decimal = reader.GetDecimal("Total");
                        this.textBox9.Text = reader["ItemsNo"].ToString();
                        this.textBox3.Text = reader["CustRef"].ToString();
                        this.textBox5.Text = reader["InvoiceNo"].ToString();
                        this.textBox6.Text = @decimal.ToString();
                        this.textBox8.Text = reader["InvoiceDate"].ToString();
                        this.textBox10.Text = reader["Vto"].ToString();
                        num2 = reader.GetInt32("status");
                        this.Tax_Text.Text = ((@decimal * this.Client.ClientTaxRate) / 100M).ToString();
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.ResetForm();
            }
        }
    }
}

