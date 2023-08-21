namespace LaxPos.Accounting
{ 
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class Receivables : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public string Report_Title = "";
        private IContainer components = null;
        private TextBox textBox4;
        private Button Btn_Export;
        private TextBox textBox3;
        private DataGridView Sales_Gridview;
        private Panel Panel_Controls;
        private GroupBox panel2;
        private Label label8;
        private Label label6;
        private Label label5;
        private TextBox textBox1;
        private Panel panel3;
        private GroupBox groupBox1;
        private RadioButton RadioButtonAll;
        private RadioButton RadioBtn_Periodical;
        private RadioButton RadioButton_Daily;
        private DateTimePicker dateTimePicker2;
        private Label label2;
        private Button Btn_Search;
        private TextBox Txt_CashierIdBox;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label4;
        private DataGridView Accounts_Gridview;
        private TextBox textBox2;
        private Label label7;
        private PrintPreviewDialog printPreviewDialog1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private ComboBox ComboBox_PaymentType;
        private Label label1;

        public Receivables()
        {
            this.InitializeComponent();
        }

        private void Accounts_Gridview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((this.Accounts_Gridview.Rows.Count > 0) && (e.RowIndex >= 0))
                {
                    int rowIndex = e.RowIndex;
                    new TransactionDetails(this.Accounts_Gridview.Rows[rowIndex].Cells[0].Value.ToString()).ShowDialog(this);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                double total = 0.0;
                double cash = 0.0;
                double cards = 0.0;
                double mpesa = 0.0;
                ReportingGlobals.Report_Dataset.DataTable1.Rows.Clear();
                DataSet set = new LaxposReportingDataset();
                foreach (DataGridViewRow row in (IEnumerable) this.Accounts_Gridview.Rows)
                {
                    object[] values = new object[] { row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[7].Value };
                    set.Tables["DataTable1"].Rows.Add(values);
                }
                if (this.textBox4.Text != "")
                {
                    total = double.Parse(this.textBox4.Text);
                    cards = double.Parse(this.textBox3.Text);
                    mpesa = double.Parse(this.textBox2.Text);
                    cash = double.Parse(this.textBox1.Text);
                }
                MessageBox.Show("Generating Report...Please Wait!", "Message Box", MessageBoxButtons.OK); 
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            this.Report_Title = "";
            string parameter = this.Trans_Query();
            if (parameter != "")
            {
                this.GetTransactionsRecords(parameter);
            }
            else
            {
                MessageBox.Show("System cannot evaluate your search!", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ClearGridview()
        {
            try
            {
                this.textBox1.Text = "0.00";
                this.textBox2.Text = "0.00";
                this.textBox3.Text = "0.00";
                this.textBox4.Text = "0.00";
                this.Accounts_Gridview.Rows.Clear();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        public void FindTotal()
        {
            try
            {
                List<string> list = new List<string>();
                int count = this.Accounts_Gridview.Rows.Count;
                if (this.Accounts_Gridview.Rows.Count > 0)
                {
                    list.Clear();
                    double num2 = 0.0;
                    double num3 = 0.0;
                    double num4 = 0.0;
                    double num5 = 0.0;
                    double num6 = 0.0;
                    double num7 = 0.0;
                    int num8 = 0;
                    while (true)
                    {
                        if (num8 >= count)
                        {
                            this.textBox1.Text = (num2 - num6).ToString("N2");
                            this.textBox2.Text = num3.ToString("N2");
                            this.textBox3.Text = num4.ToString("N2");
                            this.textBox4.Text = (num5 - num6).ToString("N2");
                            if (num7 > 0.0)
                            {
                                MessageBox.Show("The following amount cannot be determined!!!\n==>" + num7.ToString(), "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            break;
                        }
                        string item = this.Accounts_Gridview.Rows[num8].Cells[0].Value.ToString();
                        double num9 = 0.0;
                        string str2 = this.Accounts_Gridview.Rows[num8].Cells[3].Value.ToString();
                        double num10 = double.Parse(this.Accounts_Gridview.Rows[num8].Cells[2].Value.ToString());
                        if (str2 == "Cash")
                        {
                            num2 += num10;
                        }
                        else if (str2 == "Mpesa")
                        {
                            num3 += num10;
                        }
                        else if (str2.Contains("Card-"))
                        {
                            num4 += num10;
                        }
                        else
                        {
                            num7 += num10;
                        }
                        num9 = list.Contains(item) ? 0.0 : Convert.ToDouble(this.Accounts_Gridview.Rows[num8].Cells[5].Value.ToString());
                        num6 += num9;
                        num5 += num10;
                        list.Add(item);
                        num8++;
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void GetTransactionsRecords(string Parameter)
        {
            try
            {
                this.Accounts_Gridview.Rows.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = Parameter;
                command.Parameters.AddWithValue("@cashier", this.Txt_CashierIdBox.Text);
                command.Parameters.AddWithValue("@datefrom", this.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@dateto", this.dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    this.ClearGridview();
                    MessageBox.Show("No Records Have Been Found !!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Txt_CashierIdBox.Focus();
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[9];
                        values[0] = reader["TransactionNo"];
                        values[1] = reader["AccType"].ToString();
                        values[2] = reader["Amount"];
                        values[3] = reader["PaymentMethod"];
                        values[4] = reader["Totalpaid"];
                        values[5] = reader["Balance"];
                        values[6] = Convert.ToDateTime(reader["BillDate"].ToString()).ToShortDateString();
                        values[7] = reader["Cashier"];
                        values[8] = reader["Counter"].ToString();
                        this.Accounts_Gridview.Rows.Add(values);
                    }
                }
                command.Dispose();
                reader.Dispose();
                connection.Close();
                this.FindTotal();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Receivables));
            this.textBox4 = new TextBox();
            this.Btn_Export = new Button();
            this.textBox3 = new TextBox();
            this.Sales_Gridview = new DataGridView();
            this.Panel_Controls = new Panel();
            this.ComboBox_PaymentType = new ComboBox();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.RadioButtonAll = new RadioButton();
            this.RadioBtn_Periodical = new RadioButton();
            this.RadioButton_Daily = new RadioButton();
            this.dateTimePicker2 = new DateTimePicker();
            this.label2 = new Label();
            this.Btn_Search = new Button();
            this.Txt_CashierIdBox = new TextBox();
            this.label3 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.label4 = new Label();
            this.panel2 = new GroupBox();
            this.textBox2 = new TextBox();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.textBox1 = new TextBox();
            this.panel3 = new Panel();
            this.Accounts_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.printPreviewDialog1 = new PrintPreviewDialog();
            ((ISupportInitialize) this.Sales_Gridview).BeginInit();
            this.Panel_Controls.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize) this.Accounts_Gridview).BeginInit();
            base.SuspendLayout();
            this.textBox4.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(0x243, 0x29);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0xd8, 0x1d);
            this.textBox4.TabIndex = 9;
            this.textBox4.TextAlign = HorizontalAlignment.Center;
            this.Btn_Export.BackColor = Color.Maroon;
            this.Btn_Export.FlatStyle = FlatStyle.Flat;
            this.Btn_Export.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Export.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Export.Location = new Point(0x330, 0x1d);
            this.Btn_Export.Name = "Btn_Export";
            this.Btn_Export.Size = new Size(150, 0x29);
            this.Btn_Export.TabIndex = 11;
            this.Btn_Export.Text = "Export ";
            this.Btn_Export.UseVisualStyleBackColor = false;
            this.Btn_Export.Click += new EventHandler(this.Btn_Export_Click);
            this.textBox3.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0x181, 0x29);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0xae, 0x1d);
            this.textBox3.TabIndex = 7;
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            this.Sales_Gridview.AllowUserToAddRows = false;
            this.Sales_Gridview.AllowUserToDeleteRows = false;
            this.Sales_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Sales_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.ControlText;
            style.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.ButtonHighlight;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.Sales_Gridview.ColumnHeadersDefaultCellStyle = style;
            this.Sales_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Sales_Gridview.Dock = DockStyle.Fill;
            this.Sales_Gridview.EnableHeadersVisualStyles = false;
            this.Sales_Gridview.Location = new Point(0, 0);
            this.Sales_Gridview.Name = "Sales_Gridview";
            this.Sales_Gridview.ReadOnly = true;
            this.Sales_Gridview.RowHeadersVisible = false;
            style2.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style2.ForeColor = Color.Black;
            this.Sales_Gridview.RowsDefaultCellStyle = style2;
            this.Sales_Gridview.Size = new Size(0x4c3, 0x22b);
            this.Sales_Gridview.TabIndex = 0x13;
            this.Panel_Controls.Controls.Add(this.ComboBox_PaymentType);
            this.Panel_Controls.Controls.Add(this.label1);
            this.Panel_Controls.Controls.Add(this.groupBox1);
            this.Panel_Controls.Controls.Add(this.dateTimePicker2);
            this.Panel_Controls.Controls.Add(this.label2);
            this.Panel_Controls.Controls.Add(this.Btn_Search);
            this.Panel_Controls.Controls.Add(this.Txt_CashierIdBox);
            this.Panel_Controls.Controls.Add(this.label3);
            this.Panel_Controls.Controls.Add(this.dateTimePicker1);
            this.Panel_Controls.Controls.Add(this.label4);
            this.Panel_Controls.Dock = DockStyle.Top;
            this.Panel_Controls.Location = new Point(0, 0);
            this.Panel_Controls.Name = "Panel_Controls";
            this.Panel_Controls.Size = new Size(0x4c3, 0x3e);
            this.Panel_Controls.TabIndex = 0x11;
            this.ComboBox_PaymentType.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.ComboBox_PaymentType.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.ComboBox_PaymentType.FormattingEnabled = true;
            object[] items = new object[] { "All", "Cash", "Mpesa", "Card" };
            this.ComboBox_PaymentType.Items.AddRange(items);
            this.ComboBox_PaymentType.Location = new Point(0x283, 30);
            this.ComboBox_PaymentType.Name = "ComboBox_PaymentType";
            this.ComboBox_PaymentType.Size = new Size(0x89, 0x18);
            this.ComboBox_PaymentType.TabIndex = 20;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label1.Location = new Point(0x288, 5);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7a, 0x16);
            this.label1.TabIndex = 0x13;
            this.label1.Text = "Payment Mode";
            this.groupBox1.Controls.Add(this.RadioButtonAll);
            this.groupBox1.Controls.Add(this.RadioBtn_Periodical);
            this.groupBox1.Controls.Add(this.RadioButton_Daily);
            this.groupBox1.Location = new Point(0xbf, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xee, 0x38);
            this.groupBox1.TabIndex = 0x12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Frequency";
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
            this.dateTimePicker2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new Point(0x1f1, 0x22);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new Size(0x74, 0x17);
            this.dateTimePicker2.TabIndex = 0x10;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label2.Location = new Point(0x10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 0x16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Cashier";
            this.Btn_Search.BackColor = Color.Maroon;
            this.Btn_Search.FlatStyle = FlatStyle.Flat;
            this.Btn_Search.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Search.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Search.Location = new Point(0x330, 11);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new Size(150, 0x29);
            this.Btn_Search.TabIndex = 0x11;
            this.Btn_Search.Text = "Search";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new EventHandler(this.Btn_Search_Click);
            this.Txt_CashierIdBox.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.Txt_CashierIdBox.BorderStyle = BorderStyle.FixedSingle;
            this.Txt_CashierIdBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Txt_CashierIdBox.Location = new Point(7, 30);
            this.Txt_CashierIdBox.Name = "Txt_CashierIdBox";
            this.Txt_CashierIdBox.Size = new Size(0xa9, 0x1a);
            this.Txt_CashierIdBox.TabIndex = 12;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label3.Location = new Point(0x1b9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new Size(50, 0x16);
            this.label3.TabIndex = 13;
            this.label3.Text = "From";
            this.dateTimePicker1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new Point(0x1f1, 6);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0x74, 0x17);
            this.dateTimePicker1.TabIndex = 15;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label4.Location = new Point(0x1b9, 0x20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(30, 0x16);
            this.label4.TabIndex = 14;
            this.label4.Text = "To";
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.Btn_Export);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x22b);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x4c3, 0x52);
            this.panel2.TabIndex = 0x12;
            this.panel2.TabStop = false;
            this.panel2.Text = "Total Summary";
            this.textBox2.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0xc1, 0x29);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0xae, 0x1d);
            this.textBox2.TabIndex = 13;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(590, 0x12);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x2c, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Total";
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label8.Location = new Point(0xc6, 0x12);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x39, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "Mpesa";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(0x10, 0x12);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x2e, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Cash";
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(0x18b, 0x12);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x33, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Cards";
            this.textBox1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(7, 0x29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xae, 0x1d);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.panel3.Controls.Add(this.Accounts_Gridview);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(0, 0x3e);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x4c3, 0x1ed);
            this.panel3.TabIndex = 20;
            this.Accounts_Gridview.AllowUserToAddRows = false;
            this.Accounts_Gridview.AllowUserToDeleteRows = false;
            this.Accounts_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Accounts_Gridview.BackgroundColor = SystemColors.Window;
            this.Accounts_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[9];
            dataGridViewColumns[0] = this.Column1;
            dataGridViewColumns[1] = this.Column3;
            dataGridViewColumns[2] = this.Column5;
            dataGridViewColumns[3] = this.Column6;
            dataGridViewColumns[4] = this.Column9;
            dataGridViewColumns[5] = this.Column4;
            dataGridViewColumns[6] = this.Column2;
            dataGridViewColumns[7] = this.Column7;
            dataGridViewColumns[8] = this.Column8;
            this.Accounts_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Accounts_Gridview.Dock = DockStyle.Fill;
            this.Accounts_Gridview.Location = new Point(0, 0);
            this.Accounts_Gridview.Name = "Accounts_Gridview";
            this.Accounts_Gridview.ReadOnly = true;
            this.Accounts_Gridview.RowHeadersVisible = false;
            this.Accounts_Gridview.Size = new Size(0x4c3, 0x1ed);
            this.Accounts_Gridview.TabIndex = 0x18;
            this.Accounts_Gridview.CellDoubleClick += new DataGridViewCellEventHandler(this.Accounts_Gridview_CellDoubleClick);
            this.Column1.HeaderText = "TransactionNo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column3.HeaderText = "AccType";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column5.HeaderText = "SubAmount";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column6.HeaderText = "Method";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column9.HeaderText = "TotalPaid";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column4.HeaderText = "Balance";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column2.HeaderText = "Date";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column7.HeaderText = "Cashier";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column8.HeaderText = "Counter";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            this.printPreviewDialog1.ClientSize = new Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = (Icon) manager.GetObject("printPreviewDialog1.Icon");
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x4c3, 0x27d);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.Panel_Controls);
            base.Controls.Add(this.Sales_Gridview);
            base.Controls.Add(this.panel2);
            base.Name = "Receivables";
            base.Load += new EventHandler(this.Payables_Load);
            ((ISupportInitialize) this.Sales_Gridview).EndInit();
            this.Panel_Controls.ResumeLayout(false);
            this.Panel_Controls.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((ISupportInitialize) this.Accounts_Gridview).EndInit();
            base.ResumeLayout(false);
        }

        private void Payables_Load(object sender, EventArgs e)
        {
            this.RadioButtonAll.Checked = true;
        }

        private string Trans_GetPayMode()
        {
            string str = "";
            if (this.ComboBox_PaymentType.Text == "Cash")
            {
                str = " AND a.PaymentMethod='Cash' ";
            }
            else if (this.ComboBox_PaymentType.Text == "Mpesa")
            {
                str = " AND a.PaymentMethod='Mpesa' ";
            }
            else if (this.ComboBox_PaymentType.Text == "Card")
            {
                str = " AND a.PaymentMethod LIKE 'Card%' ";
            }
            return str;
        }

        private string Trans_GetPeriod()
        {
            string str = "";
            this.Report_Title = "General Sales Report";
            if (this.RadioButton_Daily.Checked)
            {
                if (this.dateTimePicker1.Value.Date == this.dateTimePicker2.Value.Date)
                {
                    str = " AND date_format(a.Date,'%Y-%m-%d')=@dateto ";
                    this.Report_Title = "Daily Sales Report for: " + this.dateTimePicker1.Value.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("From and To date must be Equal!!", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.RadioButtonAll.Checked = true;
                }
            }
            else if (this.RadioBtn_Periodical.Checked)
            {
                if (this.dateTimePicker2.Value.Date > this.dateTimePicker1.Value.Date)
                {
                    str = " AND date_format(a.Date,'%Y-%m-%d')>=@datefrom AND date_format(a.Date,'%Y-%m-%d')<=@dateto ";
                    this.Report_Title = "Sales Report from " + this.dateTimePicker1.Value.ToShortDateString() + " To " + this.dateTimePicker2.Value.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("Date range must be greater than one day!!", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.RadioButtonAll.Checked = true;
                }
            }
            return str;
        }

        private string Trans_Query()
        {
            try
            {
                string str2 = this.Trans_GetPayMode();
                string str3 = this.Trans_GetPeriod();
                return ((this.Txt_CashierIdBox.Text != string.Empty) ? (((str2 == "") && (str3 == "")) ? ((str2 == "") ? ((str3 == "") ? "SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier;" : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier " + str3)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier " + str2)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier " + str3 + str2)) : (((str2 == "") && (str3 == "")) ? ((str2 == "") ? ((str3 == "") ? "SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND a.AccType='Receivable'" : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' " + str3)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' " + str2)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' " + str3 + str2)));
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return "";
            }
        }

        private class BillMaster
        {
            public string TransCode { get; set; }

            public double Gross { get; set; }

            public double Totalpaid { get; set; }

            public double Balance { get; set; }
        }

        private class BillPayment
        {
            public string TransCode { get; set; }

            public string AccType { get; set; }

            public double SubAmount { get; set; }

            public string MethodOfPay { get; set; }

            public double TotalPaid { get; set; }

            public double Balance { get; set; }

            public DateTime Date { get; set; }

            public string Cashier { get; set; }

            public string Counter { get; set; }
        }
    }
}

