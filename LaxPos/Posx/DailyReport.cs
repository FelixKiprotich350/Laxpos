namespace LaxPos.Pos
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class DailyReport : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public static List<string> TransactionCodesList = new List<string>();
        private IContainer components = null;
        private Panel Panel_Controls;
        private GroupBox groupBox1;
        private RadioButton RadioButtonAll;
        private RadioButton RadioBtn_Periodical;
        private RadioButton RadioButton_Daily;
        private DateTimePicker dateTimePicker2;
        private Label label2;
        private Button Btn_Search;
        private TextBox Txt_SearchBox;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label4;
        private Label label8;
        private Label label6;
        private TextBox textBox1;
        private Panel panel2;
        private TextBox textBox3;
        private DataGridView Accounts_Gridview;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox4;
        private Label label5;
        private TextBox textBox5;
        private Label label7;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column9;
        private TextBox textBox6;
        private Label label9;

        public DailyReport()
        {
            this.InitializeComponent();
            this.RadioButtonAll.Checked = true;
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                TransactionCodesList.Clear();
                if (this.Txt_SearchBox.Text != string.Empty)
                {
                    if (this.RadioButtonAll.Checked)
                    {
                        this.GetTransactionCodes(" where Cashier=@Userid;");
                        this.GetFinalReport();
                    }
                    if (this.RadioButton_Daily.Checked)
                    {
                        DateTime time1 = this.dateTimePicker1.Value;
                        if (this.dateTimePicker1.Value.ToShortDateString() == this.dateTimePicker2.Value.ToShortDateString())
                        {
                            this.GetTransactionCodes(" where Cashier=@Userid and DATE_FORMAT(BillDate, '%Y-%m-%d')=@datefrom;");
                            this.GetFinalReport();
                        }
                        else
                        {
                            MessageBox.Show("To and From Date Must Be Equal !!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            this.dateTimePicker1.Focus();
                        }
                    }
                    else if (this.RadioBtn_Periodical.Checked)
                    {
                        int num1;
                        if (this.dateTimePicker1.Value >= this.dateTimePicker2.Value)
                        {
                            num1 = 0;
                        }
                        else
                        {
                            DateTime time2 = this.dateTimePicker1.Value;
                            DateTime time3 = this.dateTimePicker2.Value;
                            num1 = 1;
                        }
                        if (num1 == 0)
                        {
                            MessageBox.Show("The Start Date Cannot Be Greater Or Equal To The EndDate !!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            this.GetTransactionCodes(" where Cashier=@Userid and DATE_FORMAT(BillDate, '%Y-%m-%d')>=@datefrom AND DATE_FORMAT(BillDate, '%Y-%m-%d')<=@dateto;");
                            this.GetFinalReport();
                        }
                    }
                }
                else
                {
                    if (this.RadioButtonAll.Checked)
                    {
                        this.GetTransactionCodes(";");
                        this.GetFinalReport();
                    }
                    if (this.RadioButton_Daily.Checked)
                    {
                        DateTime time4 = this.dateTimePicker1.Value;
                        if (this.dateTimePicker1.Value.ToShortDateString() == this.dateTimePicker2.Value.ToShortDateString())
                        {
                            this.GetTransactionCodes(" where DATE_FORMAT(BillDate, '%Y-%m-%d')=@datefrom;");
                            this.GetFinalReport();
                        }
                        else
                        {
                            MessageBox.Show("To and From Date Must Be Equal !!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            this.dateTimePicker1.Focus();
                        }
                    }
                    else if (this.RadioBtn_Periodical.Checked)
                    {
                        int num2;
                        if (this.dateTimePicker1.Value >= this.dateTimePicker2.Value)
                        {
                            num2 = 0;
                        }
                        else
                        {
                            DateTime time5 = this.dateTimePicker1.Value;
                            DateTime time6 = this.dateTimePicker2.Value;
                            num2 = 1;
                        }
                        if (num2 == 0)
                        {
                            MessageBox.Show("The Start Date Cannot Be Greater Or Equal To The EndDate !!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            this.GetTransactionCodes(" where DATE_FORMAT(BillDate, '%Y-%m-%d')>=@datefrom AND DATE_FORMAT(BillDate, '%Y-%m-%d')<=@dateto;");
                            this.GetFinalReport();
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
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

        private void GetBillAccounts(List<string> BillNolist)
        {
            try
            {
                decimal num = 0M;
                decimal num2 = 0M;
                decimal num3 = 0M;
                decimal num4 = 0M;
                decimal num5 = 0M;
                decimal num6 = 0M;
                decimal num7 = 0M;
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                foreach (string str in BillNolist)
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from accounts where TransactionNo=@billno";
                    command.Parameters.AddWithValue("@billno", str);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("No Records Have Been Found !!", "Accounting Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.textBox3.Text = "0.00";
                        this.textBox4.Text = "0.00";
                        this.textBox5.Text = "0.00";
                        this.textBox6.Text = "0.00";
                    }
                    else
                    {
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                if (num4 > 0M)
                                {
                                    MessageBox.Show("You have unaccounted sum of: " + num4.ToString(), "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                                break;
                            }
                            if (reader["PaymentMethod"].ToString() == "Mpesa")
                            {
                                num2 += Convert.ToDecimal(reader["Amount"].ToString());
                                continue;
                            }
                            if (reader["PaymentMethod"].ToString() == "Cash")
                            {
                                num += Convert.ToDecimal(reader["Amount"].ToString());
                                continue;
                            }
                            if (reader["PaymentMethod"].ToString().Contains("Card-"))
                            {
                                num3 += Convert.ToDecimal(reader["Amount"].ToString());
                                continue;
                            }
                            num4 += Convert.ToDecimal(reader["Amount"].ToString());
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    command.Parameters.Clear();
                    command.CommandText = "select Balance,Profit from billmaster where Billno=@billno;";
                    command.Parameters.AddWithValue("@billno", str);
                    MySqlDataReader reader2 = command.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (true)
                        {
                            if (!reader2.Read())
                            {
                                break;
                            }
                            num6 += Convert.ToDecimal(reader2["Balance"].ToString());
                            num7 += Convert.ToDecimal(reader2["Profit"].ToString());
                        }
                    }
                    command.Dispose();
                    reader2.Close();
                    reader2.Dispose();
                }
                num5 = (((num + num2) + num3) + num4) - num6;
                this.textBox2.Text = num7.ToString("N2");
                this.textBox3.Text = num5.ToString("N2");
                this.textBox4.Text = (num - num6).ToString("N2");
                this.textBox5.Text = num2.ToString("N2");
                this.textBox6.Text = num3.ToString("N2");
                connection.Close();
            }
            catch (Exception exception)
            {
                this.textBox2.Text = "0.00";
                this.textBox3.Text = "0.00";
                this.textBox4.Text = "0.00";
                this.textBox5.Text = "0.00";
                this.textBox6.Text = "0.00";
                MessageBox.Show(exception.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void GetFinalReport()
        {
            try
            {
                if (TransactionCodesList.Count > 0)
                {
                    this.GetTransactionsRecords(TransactionCodesList);
                    this.GetBillAccounts(TransactionCodesList);
                }
            }
            catch
            {
            }
        }

        public void GetTransactionCodes(string Query_Clause)
        {
            try
            {
                TransactionCodesList.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Billno FROM billmaster " + Query_Clause;
                command.Parameters.AddWithValue("@Userid", this.Txt_SearchBox.Text);
                command.Parameters.AddWithValue("@datefrom", this.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@dateto", this.dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No transactions found...", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Accounts_Gridview.Rows.Clear();
                    this.textBox4.Text = "0.00";
                    this.textBox5.Text = "0.00";
                    this.textBox6.Text = "0.00";
                    this.textBox3.Text = "0.00";
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        TransactionCodesList.Add(reader["Billno"].ToString());
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void GetTransactionsRecords(List<string> BillNolist)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM itemsales where TransNo=@billno";
                this.Accounts_Gridview.Rows.Clear();
                foreach (string str in BillNolist)
                {
                    command.Parameters.AddWithValue("@billno", str);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Transaction Records Related to BillNo: =>" + str + " Has not been found.", "BillMessage Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                break;
                            }
                            object[] values = new object[] { reader["TransNo"], reader["Description"].ToString(), reader["Quantity"], reader["Unit"].ToString(), reader["UnitPrice"], reader["Gross"], reader["Discount"], reader["Profit"] };
                            this.Accounts_Gridview.Rows.Add(values);
                        }
                    }
                    command.Parameters.Clear();
                    command.Dispose();
                    reader.Dispose();
                }
                this.textBox1.Text = this.Accounts_Gridview.Rows.Count.ToString();
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            this.Panel_Controls = new Panel();
            this.groupBox1 = new GroupBox();
            this.RadioButtonAll = new RadioButton();
            this.RadioBtn_Periodical = new RadioButton();
            this.RadioButton_Daily = new RadioButton();
            this.dateTimePicker2 = new DateTimePicker();
            this.label2 = new Label();
            this.Btn_Search = new Button();
            this.Txt_SearchBox = new TextBox();
            this.label3 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.label4 = new Label();
            this.label8 = new Label();
            this.label6 = new Label();
            this.textBox1 = new TextBox();
            this.panel2 = new Panel();
            this.textBox6 = new TextBox();
            this.label9 = new Label();
            this.textBox5 = new TextBox();
            this.label7 = new Label();
            this.textBox4 = new TextBox();
            this.label5 = new Label();
            this.textBox2 = new TextBox();
            this.label1 = new Label();
            this.textBox3 = new TextBox();
            this.Accounts_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Panel_Controls.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((ISupportInitialize) this.Accounts_Gridview).BeginInit();
            base.SuspendLayout();
            this.Panel_Controls.Controls.Add(this.groupBox1);
            this.Panel_Controls.Controls.Add(this.dateTimePicker2);
            this.Panel_Controls.Controls.Add(this.label2);
            this.Panel_Controls.Controls.Add(this.Btn_Search);
            this.Panel_Controls.Controls.Add(this.Txt_SearchBox);
            this.Panel_Controls.Controls.Add(this.label3);
            this.Panel_Controls.Controls.Add(this.dateTimePicker1);
            this.Panel_Controls.Controls.Add(this.label4);
            this.Panel_Controls.Dock = DockStyle.Top;
            this.Panel_Controls.Location = new Point(0, 0);
            this.Panel_Controls.Name = "Panel_Controls";
            this.Panel_Controls.Size = new Size(0x4b3, 0x3e);
            this.Panel_Controls.TabIndex = 0x15;
            this.groupBox1.Controls.Add(this.RadioButtonAll);
            this.groupBox1.Controls.Add(this.RadioBtn_Periodical);
            this.groupBox1.Controls.Add(this.RadioButton_Daily);
            this.groupBox1.Location = new Point(0x184, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xee, 0x38);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Period";
            this.RadioButtonAll.AutoSize = true;
            this.RadioButtonAll.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.RadioButtonAll.Location = new Point(9, 0x18);
            this.RadioButtonAll.Name = "RadioButtonAll";
            this.RadioButtonAll.Size = new Size(0x2f, 0x1a);
            this.RadioButtonAll.TabIndex = 2;
            this.RadioButtonAll.TabStop = true;
            this.RadioButtonAll.Text = "All";
            this.RadioButtonAll.UseVisualStyleBackColor = true;
            this.RadioBtn_Periodical.AutoSize = true;
            this.RadioBtn_Periodical.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.RadioBtn_Periodical.Location = new Point(0x8e, 0x18);
            this.RadioBtn_Periodical.Name = "RadioBtn_Periodical";
            this.RadioBtn_Periodical.Size = new Size(0x5e, 0x1a);
            this.RadioBtn_Periodical.TabIndex = 1;
            this.RadioBtn_Periodical.TabStop = true;
            this.RadioBtn_Periodical.Text = "Periodical";
            this.RadioBtn_Periodical.UseVisualStyleBackColor = true;
            this.RadioButton_Daily.AutoSize = true;
            this.RadioButton_Daily.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.RadioButton_Daily.Location = new Point(0x47, 0x18);
            this.RadioButton_Daily.Name = "RadioButton_Daily";
            this.RadioButton_Daily.Size = new Size(0x41, 0x1a);
            this.RadioButton_Daily.TabIndex = 0;
            this.RadioButton_Daily.TabStop = true;
            this.RadioButton_Daily.Text = "Daily";
            this.RadioButton_Daily.UseVisualStyleBackColor = true;
            this.dateTimePicker2.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new Point(0x309, 30);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new Size(0x74, 0x1d);
            this.dateTimePicker2.TabIndex = 8;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label2.Location = new Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(80, 0x1b);
            this.label2.TabIndex = 3;
            this.label2.Text = "User Id";
            this.Btn_Search.BackColor = Color.Maroon;
            this.Btn_Search.FlatStyle = FlatStyle.Flat;
            this.Btn_Search.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Search.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Search.Location = new Point(0x383, 12);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new Size(150, 0x29);
            this.Btn_Search.TabIndex = 9;
            this.Btn_Search.Text = "Search";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new EventHandler(this.Btn_Search_Click);
            this.Txt_SearchBox.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.Txt_SearchBox.BorderStyle = BorderStyle.FixedSingle;
            this.Txt_SearchBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Txt_SearchBox.Location = new Point(0x60, 15);
            this.Txt_SearchBox.Name = "Txt_SearchBox";
            this.Txt_SearchBox.Size = new Size(0x11e, 0x1a);
            this.Txt_SearchBox.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label3.Location = new Point(0x28d, 1);
            this.label3.Name = "label3";
            this.label3.Size = new Size(60, 0x1b);
            this.label3.TabIndex = 5;
            this.label3.Text = "From";
            this.dateTimePicker1.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new Point(0x27e, 30);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0x74, 0x1d);
            this.dateTimePicker1.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label4.Location = new Point(810, 3);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x25, 0x1b);
            this.label4.TabIndex = 6;
            this.label4.Text = "To";
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label8.Location = new Point(3, 0x2d);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x70, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "Gross Amount";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x58, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Total Items";
            this.textBox1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0x79, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0xa3, 0x1d);
            this.textBox1.TabIndex = 0;
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x203);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x4b3, 0x53);
            this.panel2.TabIndex = 0x16;
            this.textBox6.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox6.Location = new Point(0x2b1, 6);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(0x8a, 0x1d);
            this.textBox6.TabIndex = 15;
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label9.Location = new Point(0x241, 9);
            this.label9.Name = "label9";
            this.label9.Size = new Size(90, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Cards Total";
            this.textBox5.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox5.Location = new Point(0x17a, 0x2d);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0xb2, 0x1d);
            this.textBox5.TabIndex = 13;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(0x13c, 0x2d);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x39, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Mpesa";
            this.textBox4.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(0x17a, 6);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0xb2, 0x1d);
            this.textBox4.TabIndex = 11;
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(0x13f, 9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2e, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cash";
            this.textBox2.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0x2b1, 0x2d);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x8a, 0x1d);
            this.textBox2.TabIndex = 9;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x24d, 0x2d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2e, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Profit";
            this.textBox3.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0x79, 0x2d);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0xa3, 0x1d);
            this.textBox3.TabIndex = 7;
            this.Accounts_Gridview.AllowUserToAddRows = false;
            this.Accounts_Gridview.AllowUserToDeleteRows = false;
            this.Accounts_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Accounts_Gridview.BackgroundColor = SystemColors.ControlLightLight;
            this.Accounts_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[9];
            dataGridViewColumns[0] = this.Column1;
            dataGridViewColumns[1] = this.Column4;
            dataGridViewColumns[2] = this.Column6;
            dataGridViewColumns[3] = this.Column5;
            dataGridViewColumns[4] = this.Column2;
            dataGridViewColumns[5] = this.Column7;
            dataGridViewColumns[6] = this.Column8;
            dataGridViewColumns[7] = this.Column3;
            dataGridViewColumns[8] = this.Column9;
            this.Accounts_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Accounts_Gridview.Dock = DockStyle.Fill;
            this.Accounts_Gridview.Location = new Point(0, 0x3e);
            this.Accounts_Gridview.Name = "Accounts_Gridview";
            this.Accounts_Gridview.ReadOnly = true;
            this.Accounts_Gridview.RowHeadersVisible = false;
            this.Accounts_Gridview.Size = new Size(0x4b3, 0x1c5);
            this.Accounts_Gridview.TabIndex = 0x17;
            this.Column1.HeaderText = "TransactionNo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column4.HeaderText = "Product";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column6.HeaderText = "Quantity";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column5.HeaderText = "Unit";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column2.HeaderText = "Price";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column7.HeaderText = "Total";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column8.HeaderText = "Discount";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column3.HeaderText = "Profit";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            this.Column9.HeaderText = "Balance";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x4b3, 0x256);
            base.Controls.Add(this.Accounts_Gridview);
            base.Controls.Add(this.Panel_Controls);
            base.Controls.Add(this.panel2);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "DailyReport";
            base.Load += new EventHandler(this.Receivables_Load);
            this.Panel_Controls.ResumeLayout(false);
            this.Panel_Controls.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((ISupportInitialize) this.Accounts_Gridview).EndInit();
            base.ResumeLayout(false);
        }

        private void Receivables_Load(object sender, EventArgs e)
        {
            this.RadioButtonAll.Checked = true;
            this.Txt_SearchBox.Focus();
            base.ParentForm.AcceptButton = this.Btn_Search;
        }
    }
}

