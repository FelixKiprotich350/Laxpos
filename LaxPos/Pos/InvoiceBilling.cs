namespace LaxPos.Pos
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvoiceBilling : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button Btn_SearchCustomer;
        private Label label14;
        private Label label15;
        private TextBox textBox15;
        private Button Btn_Close;
        private Button Btn_Accept;
        public TextBox textBox3;
        public TextBox textBox2;
        public TextBox textBox1;
        public TextBox textBox14;
        private Label label18;
        public TextBox textBox16;
        private Label label16;
        public TextBox textBox17;
        private Label label17;
        private Label label11;
        private TextBox textBox11;
        private Label label9;
        private TextBox textBox9;
        private Label label4;
        private TextBox textBox4;
        private Label label5;
        private TextBox textBox5;
        private Label label13;
        private TextBox textBox13;
        private Label label12;
        private TextBox textBox12;
        private GroupBox groupBox2;
        private Label label8;
        private TextBox textBox8;
        private Label label10;
        private TextBox textBox10;
        private GroupBox groupBox3;
        private GroupBox groupBox4;

        public InvoiceBilling(string InvNumber, string TenderAmount, string Discount)
        {
            this.InitializeComponent();
            this.textBox9.Text = InvNumber;
            this.textBox8.Text = Discount;
            this.textBox10.Text = TenderAmount;
        }

        private void Btn_Accept_Click(object sender, EventArgs e)
        {
            if ((this.textBox1.Text != null) && (this.textBox2.Text != null))
            {
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Incomplete Customers Details", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.No;
        }

        private void Btn_SearchCustomer_Click(object sender, EventArgs e)
        {
            this.ValidateUser();
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
            this.groupBox1 = new GroupBox();
            this.textBox16 = new TextBox();
            this.label16 = new Label();
            this.textBox17 = new TextBox();
            this.label17 = new Label();
            this.textBox14 = new TextBox();
            this.label14 = new Label();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.label18 = new Label();
            this.label15 = new Label();
            this.textBox15 = new TextBox();
            this.Btn_SearchCustomer = new Button();
            this.Btn_Close = new Button();
            this.Btn_Accept = new Button();
            this.label11 = new Label();
            this.textBox11 = new TextBox();
            this.label9 = new Label();
            this.textBox9 = new TextBox();
            this.label4 = new Label();
            this.textBox4 = new TextBox();
            this.label5 = new Label();
            this.textBox5 = new TextBox();
            this.label13 = new Label();
            this.textBox13 = new TextBox();
            this.label12 = new Label();
            this.textBox12 = new TextBox();
            this.groupBox2 = new GroupBox();
            this.label8 = new Label();
            this.textBox8 = new TextBox();
            this.label10 = new Label();
            this.textBox10 = new TextBox();
            this.groupBox3 = new GroupBox();
            this.groupBox4 = new GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.textBox16);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.textBox17);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.textBox14);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox1.Location = new Point(0, 0x37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(420, 0xa1);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer Details";
            this.textBox16.Location = new Point(0x11a, 0x18);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new Size(0x7e, 0x1a);
            this.textBox16.TabIndex = 14;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0xe2, 0x1b);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x2a, 20);
            this.label16.TabIndex = 13;
            this.label16.Text = "Limit";
            this.textBox17.Location = new Point(0x4b, 0x18);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new Size(0x8b, 0x1a);
            this.textBox17.TabIndex = 12;
            this.label17.AutoSize = true;
            this.label17.Location = new Point(3, 0x1b);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x2f, 20);
            this.label17.TabIndex = 11;
            this.label17.Text = "Id No";
            this.textBox14.Location = new Point(0x11a, 0x3a);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new Size(0x7e, 0x1a);
            this.textBox14.TabIndex = 8;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(220, 0x3d);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x3f, 20);
            this.label14.TabIndex = 7;
            this.label14.Text = "Gender";
            this.textBox3.Location = new Point(0x4b, 0x7a);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0x14d, 0x1a);
            this.textBox3.TabIndex = 6;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 0x5d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x33, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Name";
            this.textBox2.Location = new Point(0x4b, 90);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x14d, 0x1a);
            this.textBox2.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(3, 0x7d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email";
            this.textBox1.Location = new Point(0x4b, 0x3a);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x8b, 0x1a);
            this.textBox1.TabIndex = 9;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 0x3d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x49, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Contacts";
            this.label18.BorderStyle = BorderStyle.FixedSingle;
            this.label18.FlatStyle = FlatStyle.Flat;
            this.label18.Location = new Point(5, 0x2f);
            this.label18.Name = "label18";
            this.label18.Size = new Size(400, 2);
            this.label18.TabIndex = 15;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(15, 0x15);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x60, 20);
            this.label15.TabIndex = 10;
            this.label15.Text = "Customer Id";
            this.textBox15.Location = new Point(0x75, 0x12);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Size(0x8b, 0x1a);
            this.textBox15.TabIndex = 2;
            this.textBox15.TextChanged += new EventHandler(this.TextBox15_TextChanged);
            this.Btn_SearchCustomer.Location = new Point(0x106, 0x10);
            this.Btn_SearchCustomer.Name = "Btn_SearchCustomer";
            this.Btn_SearchCustomer.Size = new Size(0x6d, 30);
            this.Btn_SearchCustomer.TabIndex = 0;
            this.Btn_SearchCustomer.Text = "Search";
            this.Btn_SearchCustomer.UseVisualStyleBackColor = true;
            this.Btn_SearchCustomer.Click += new EventHandler(this.Btn_SearchCustomer_Click);
            this.Btn_Close.DialogResult = DialogResult.Cancel;
            this.Btn_Close.Location = new Point(0x3d, 0x1d1);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x76, 30);
            this.Btn_Close.TabIndex = 7;
            this.Btn_Close.Text = "Close Invoice";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            this.Btn_Accept.Location = new Point(0xe0, 0x1d1);
            this.Btn_Accept.Name = "Btn_Accept";
            this.Btn_Accept.Size = new Size(0x7b, 30);
            this.Btn_Accept.TabIndex = 6;
            this.Btn_Accept.Text = "Accept Invoice";
            this.Btn_Accept.UseVisualStyleBackColor = true;
            this.Btn_Accept.Click += new EventHandler(this.Btn_Accept_Click);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(12, 0x4f);
            this.label11.Name = "label11";
            this.label11.Size = new Size(110, 20);
            this.label11.TabIndex = 7;
            this.label11.Text = "Invoicing Date";
            this.textBox11.Location = new Point(12, 0x66);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new Size(110, 0x1a);
            this.textBox11.TabIndex = 8;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(12, 0x19);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x77, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "Invoice Number";
            this.textBox9.Location = new Point(12, 0x30);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new Size(0x98, 0x1a);
            this.textBox9.TabIndex = 12;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xae, 0x19);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x51, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Served By";
            this.textBox4.Location = new Point(0xae, 0x30);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0x7c, 0x1a);
            this.textBox4.TabIndex = 14;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x130, 0x19);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x42, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Counter";
            this.textBox5.Location = new Point(0x130, 0x30);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0x68, 0x1a);
            this.textBox5.TabIndex = 0x10;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x97, 0x4f);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x55, 20);
            this.label13.TabIndex = 0x11;
            this.label13.Text = "Valid From";
            this.textBox13.Location = new Point(0x8b, 0x66);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new Size(0x75, 0x1a);
            this.textBox13.TabIndex = 0x12;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x116, 0x4f);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x4e, 20);
            this.label12.TabIndex = 0x13;
            this.label12.Text = "Due Date";
            this.textBox12.Location = new Point(0x11a, 0x66);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new Size(0x84, 0x1a);
            this.textBox12.TabIndex = 20;
            this.groupBox2.Controls.Add(this.textBox12);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBox13);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox11);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Dock = DockStyle.Top;
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(0, 0xd8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(420, 0x95);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service Details";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x1d);
            this.label8.Name = "label8";
            this.label8.Size = new Size(130, 20);
            this.label8.TabIndex = 0x17;
            this.label8.Text = "Discount Offered";
            this.textBox8.Location = new Point(0xae, 0x1a);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new Size(0xc6, 0x1a);
            this.textBox8.TabIndex = 0x18;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(0x10, 0x3d);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x98, 20);
            this.label10.TabIndex = 0x19;
            this.label10.Text = "Amount Tendered";
            this.textBox10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox10.Location = new Point(0xae, 0x3a);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new Size(0xc6, 0x1a);
            this.textBox10.TabIndex = 0x1a;
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Dock = DockStyle.Top;
            this.groupBox3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox3.Location = new Point(0, 0x16d);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(420, 0x5e);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Summary Report";
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.Btn_SearchCustomer);
            this.groupBox4.Controls.Add(this.textBox15);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Dock = DockStyle.Top;
            this.groupBox4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox4.Location = new Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(420, 0x37);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search Box";
            base.AcceptButton = this.Btn_Accept;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            base.CancelButton = this.Btn_Close;
            base.ClientSize = new Size(420, 0x1f9);
            base.Controls.Add(this.Btn_Close);
            base.Controls.Add(this.Btn_Accept);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.groupBox4);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "InvoiceBilling";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Invoice Billing";
            base.Load += new EventHandler(this.InvoiceBilling_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            base.ResumeLayout(false);
        }

        private void InvoiceBilling_Load(object sender, EventArgs e)
        {
            this.textBox11.Text = Program.CurrentDateTime().ToShortDateString();
            this.textBox13.Text = Program.CurrentDateTime().ToShortDateString();
            this.textBox12.Text = Program.CurrentDateTime().AddMonths(1).ToShortDateString();
            this.textBox4.Text = Program.CurrLoggedInUser.UserFirstname;
            this.textBox5.Text = Program.LogInCounter;
            this.textBox15.Focus();
        }

        private void TextBox15_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox15.Focused)
            {
                base.AcceptButton = this.Btn_SearchCustomer;
            }
        }

        public int ValidateUser()
        {
            int num;
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("select * from customersinvoiced where IdNumber=@id", connection);
                command.Parameters.AddWithValue("@id", this.textBox15.Text);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No customer  has been found", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    num = 0;
                }
                else if (!reader.Read())
                {
                    num = 1;
                }
                else
                {
                    this.textBox1.Text = reader["Phone"].ToString();
                    this.textBox2.Text = reader["Name"].ToString();
                    this.textBox3.Text = reader["Email"].ToString();
                    this.textBox14.Text = reader["Gender"].ToString();
                    this.textBox16.Text = reader["MaxAmount"].ToString();
                    this.textBox17.Text = reader["IdNumber"].ToString();
                    num = 1;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                num = -1;
            }
            return num;
        }
    }
}

