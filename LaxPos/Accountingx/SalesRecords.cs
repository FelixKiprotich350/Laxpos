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
    using System.Windows.Forms;

    public class SalesRecords : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public string Report_Title = "";
        private IContainer components = null;
        private TabPage TransactionWise_Tabpage;
        private TabControl tabControl1;
        private TabPage ItemWise_Tabpage;
        private DataGridView Sales_Gridview;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column13;
        private Panel panel1;
        private TextBox Itemwise__Textbox_Cards;
        private Label label9;
        private TextBox Itemwise_Textbox_Cash;
        private Label label10;
        private Label label11;
        private TextBox Itemwise_TextboxTotalitems;
        private Panel panel4;
        private GroupBox groupBox2;
        private RadioButton Itemwise_RadioButton_All;
        private RadioButton Itemwise_RadioButton_PeriodicalI;
        private RadioButton Itemwise_RadioButton_Daily;
        private DateTimePicker Itemwise_DateTo;
        private Label label12;
        private Button Btn_ItemwiseSearch;
        private TextBox Itemwise_Txt_SearchBox;
        private Label label13;
        private DateTimePicker Itemwise_DateFrom;
        private Label label14;
        private Panel panel3;
        private DataGridView Transactionwise_Accounts_Gridview;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private Panel Panel_Controls;
        private ComboBox ComboBox_PaymentType;
        private Label label1;
        private GroupBox groupBox1;
        private RadioButton Transactionwise_RadioButtonAll;
        private RadioButton Transactionwise_RadioBtn_Periodical;
        private RadioButton Transactionwise_RadioButton_Daily;
        private DateTimePicker Transactionwise_DateTo;
        private Label label2;
        private Button Btn_TransactionSearch;
        private TextBox Transactionwise_Txt_CashierIdBox;
        private Label label3;
        private DateTimePicker Transactionwise_Datefrom;
        private Label label4;
        private GroupBox panel2;
        private TextBox Transactionwise_textBox_Mpesa;
        private Label label7;
        private Button Btn_TransactionwiseExport;
        private TextBox Transactionwise_textBoxTotal;
        private TextBox Transactionwise_textBox_Cards;
        private Label label8;
        private Label label6;
        private Label label5;
        private TextBox Transactionwise_textBox_Cash;
        private DataGridView SalesItems_Gridview;

        public SalesRecords()
        {
            this.InitializeComponent();
        }

        private void Accounts_Gridview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((this.Transactionwise_Accounts_Gridview.Rows.Count > 0) && (e.RowIndex >= 0))
                {
                    int rowIndex = e.RowIndex;
                    new TransactionDetails(this.Transactionwise_Accounts_Gridview.Rows[rowIndex].Cells[0].Value.ToString()).ShowDialog(this);
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
                foreach (DataGridViewRow row in (IEnumerable) this.Transactionwise_Accounts_Gridview.Rows)
                {
                    object[] values = new object[] { row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[7].Value };
                    set.Tables["DataTable1"].Rows.Add(values);
                }
                if (this.Transactionwise_textBoxTotal.Text != "")
                {
                    total = double.Parse(this.Transactionwise_textBoxTotal.Text);
                    cards = double.Parse(this.Transactionwise_textBox_Cards.Text);
                    mpesa = double.Parse(this.Transactionwise_textBox_Mpesa.Text);
                    cash = double.Parse(this.Transactionwise_textBox_Cash.Text);
                }
                MessageBox.Show("Generating Report...Please Wait!", "Message Box", MessageBoxButtons.OK);
                new ReportingForm(set, this.Report_Title, total, cash, cards, mpesa).ShowDialog(this);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_ItemwiseSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Itemwise_Txt_SearchBox.Text != string.Empty)
                {
                    if (this.Itemwise_RadioButton_All.Checked)
                    {
                        this.GetItemwise_SalesRecords(" where ReceivedBy=@Userid;");
                    }
                    if (this.Itemwise_RadioButton_Daily.Checked)
                    {
                        DateTime time1 = this.Itemwise_DateFrom.Value;
                        if (this.Itemwise_DateFrom.Value.ToShortDateString() == this.Itemwise_DateTo.Value.ToShortDateString())
                        {
                            this.GetItemwise_SalesRecords(" where ReceivedBy=@Userid and DATE_FORMAT(TransDate, '%Y-%m-%d')=@datefrom; ");
                        }
                        else
                        {
                            MessageBox.Show("To and From Date Must Be Equal !!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            this.Itemwise_DateFrom.Focus();
                        }
                    }
                    else if (this.Itemwise_RadioButton_PeriodicalI.Checked)
                    {
                        int num1;
                        if (this.Itemwise_DateFrom.Value >= this.Itemwise_DateTo.Value)
                        {
                            num1 = 0;
                        }
                        else
                        {
                            DateTime time2 = this.Itemwise_DateFrom.Value;
                            DateTime time3 = this.Itemwise_DateTo.Value;
                            num1 = 1;
                        }
                        if (num1 != 0)
                        {
                            this.GetItemwise_SalesRecords(" where ReceivedBy=@Userid and DATE_FORMAT(TransDate, '%Y-%m-%d')>=@datefrom AND DATE_FORMAT(TransDate, '%Y-%m-%d')<=@dateto;");
                        }
                        else
                        {
                            MessageBox.Show("The Start Date Cannot Be Greater Or Equal To The EndDate !!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    if (this.Itemwise_RadioButton_All.Checked)
                    {
                        this.GetItemwise_SalesRecords(";");
                    }
                    if (this.Itemwise_RadioButton_Daily.Checked)
                    {
                        DateTime time4 = this.Itemwise_DateFrom.Value;
                        if (this.Itemwise_DateFrom.Value.ToShortDateString() == this.Itemwise_DateTo.Value.ToShortDateString())
                        {
                            this.GetItemwise_SalesRecords("where DATE_FORMAT(TransDate, '%Y-%m-%d')=@datefrom;");
                        }
                        else
                        {
                            MessageBox.Show("To and From Date Must Be Equal !!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            this.Itemwise_DateFrom.Focus();
                        }
                    }
                    else if (this.Itemwise_RadioButton_PeriodicalI.Checked)
                    {
                        int num2;
                        if (this.Itemwise_DateFrom.Value >= this.Itemwise_DateTo.Value)
                        {
                            num2 = 0;
                        }
                        else
                        {
                            DateTime time5 = this.Itemwise_DateFrom.Value;
                            DateTime time6 = this.Itemwise_DateTo.Value;
                            num2 = 1;
                        }
                        if (num2 != 0)
                        {
                            this.GetItemwise_SalesRecords(" where DATE_FORMAT(TransDate, '%Y-%m-%d')>=@datefrom AND DATE_FORMAT(TransDate, '%Y-%m-%d')<=@dateto;");
                        }
                        else
                        {
                            MessageBox.Show("The Start Date Cannot Be Greater Or Equal To The EndDate !!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
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
                this.Transactionwise_textBox_Cash.Text = "0.00";
                this.Transactionwise_textBox_Mpesa.Text = "0.00";
                this.Transactionwise_textBox_Cards.Text = "0.00";
                this.Transactionwise_textBoxTotal.Text = "0.00";
                this.Transactionwise_Accounts_Gridview.Rows.Clear();
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
                int count = this.Transactionwise_Accounts_Gridview.Rows.Count;
                if (this.Transactionwise_Accounts_Gridview.Rows.Count > 0)
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
                            this.Transactionwise_textBox_Cash.Text = (num2 - num6).ToString("N2");
                            this.Transactionwise_textBox_Mpesa.Text = num3.ToString("N2");
                            this.Transactionwise_textBox_Cards.Text = num4.ToString("N2");
                            this.Transactionwise_textBoxTotal.Text = (num5 - num6).ToString("N2");
                            if (num7 > 0.0)
                            {
                                MessageBox.Show("The following amount cannot be determined!!!\n==>" + num7.ToString(), "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            break;
                        }
                        string item = this.Transactionwise_Accounts_Gridview.Rows[num8].Cells[0].Value.ToString();
                        double num9 = 0.0;
                        string str2 = this.Transactionwise_Accounts_Gridview.Rows[num8].Cells[3].Value.ToString();
                        double num10 = double.Parse(this.Transactionwise_Accounts_Gridview.Rows[num8].Cells[2].Value.ToString());
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
                        num9 = list.Contains(item) ? 0.0 : Convert.ToDouble(this.Transactionwise_Accounts_Gridview.Rows[num8].Cells[5].Value.ToString());
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

        public void GetItemwise_SalesRecords(string Parameter)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM itemsales " + Parameter;
                command.Parameters.AddWithValue("@Userid", this.Itemwise_Txt_SearchBox.Text);
                command.Parameters.AddWithValue("@datefrom", this.Itemwise_DateFrom.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@dateto", this.Itemwise_DateTo.Value.ToString("yyyy-MM-dd"));
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Sales Have Been Found !!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.SalesItems_Gridview.Rows.Clear();
                    this.Itemwise_Txt_SearchBox.Focus();
                }
                else
                {
                    this.SalesItems_Gridview.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[13];
                        values[0] = reader["ProductCode"];
                        values[1] = reader["Description"].ToString();
                        values[2] = reader["Quantity"];
                        values[3] = reader["Unit"].ToString();
                        values[4] = reader["UnitPrice"];
                        values[5] = reader["Gross"];
                        values[6] = reader["Discount"];
                        values[7] = reader["Profit"];
                        values[8] = reader["Balance"];
                        values[9] = reader.GetDateTime("TransDate").ToShortDateString();
                        values[10] = reader.GetDateTime("TransDate").ToLongTimeString();
                        values[11] = reader.GetString("ReceivedBy");
                        values[12] = reader.GetString("WorkStationID");
                        this.SalesItems_Gridview.Rows.Add(values);
                    }
                }
                command.Dispose();
                reader.Dispose();
                connection.Close();
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
                this.Transactionwise_Accounts_Gridview.Rows.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = Parameter;
                command.Parameters.AddWithValue("@cashier", this.Transactionwise_Txt_CashierIdBox.Text);
                command.Parameters.AddWithValue("@datefrom", this.Transactionwise_Datefrom.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@dateto", this.Transactionwise_DateTo.Value.ToString("yyyy-MM-dd"));
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    this.ClearGridview();
                    MessageBox.Show("No Records Have Been Found !!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Transactionwise_Txt_CashierIdBox.Focus();
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
                        this.Transactionwise_Accounts_Gridview.Rows.Add(values);
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
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            this.TransactionWise_Tabpage = new TabPage();
            this.panel3 = new Panel();
            this.Transactionwise_Accounts_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Sales_Gridview = new DataGridView();
            this.Panel_Controls = new Panel();
            this.ComboBox_PaymentType = new ComboBox();
            this.label1 = new Label();
            this.groupBox1 = new GroupBox();
            this.Transactionwise_RadioButtonAll = new RadioButton();
            this.Transactionwise_RadioBtn_Periodical = new RadioButton();
            this.Transactionwise_RadioButton_Daily = new RadioButton();
            this.Transactionwise_DateTo = new DateTimePicker();
            this.label2 = new Label();
            this.Btn_TransactionSearch = new Button();
            this.Transactionwise_Txt_CashierIdBox = new TextBox();
            this.label3 = new Label();
            this.Transactionwise_Datefrom = new DateTimePicker();
            this.label4 = new Label();
            this.panel2 = new GroupBox();
            this.Transactionwise_textBox_Mpesa = new TextBox();
            this.label7 = new Label();
            this.Btn_TransactionwiseExport = new Button();
            this.Transactionwise_textBoxTotal = new TextBox();
            this.Transactionwise_textBox_Cards = new TextBox();
            this.label8 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.Transactionwise_textBox_Cash = new TextBox();
            this.tabControl1 = new TabControl();
            this.ItemWise_Tabpage = new TabPage();
            this.SalesItems_Gridview = new DataGridView();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.panel1 = new Panel();
            this.Itemwise__Textbox_Cards = new TextBox();
            this.label9 = new Label();
            this.Itemwise_Textbox_Cash = new TextBox();
            this.label10 = new Label();
            this.label11 = new Label();
            this.Itemwise_TextboxTotalitems = new TextBox();
            this.panel4 = new Panel();
            this.groupBox2 = new GroupBox();
            this.Itemwise_RadioButton_All = new RadioButton();
            this.Itemwise_RadioButton_PeriodicalI = new RadioButton();
            this.Itemwise_RadioButton_Daily = new RadioButton();
            this.Itemwise_DateTo = new DateTimePicker();
            this.label12 = new Label();
            this.Btn_ItemwiseSearch = new Button();
            this.Itemwise_Txt_SearchBox = new TextBox();
            this.label13 = new Label();
            this.Itemwise_DateFrom = new DateTimePicker();
            this.label14 = new Label();
            this.TransactionWise_Tabpage.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize) this.Transactionwise_Accounts_Gridview).BeginInit();
            ((ISupportInitialize) this.Sales_Gridview).BeginInit();
            this.Panel_Controls.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ItemWise_Tabpage.SuspendLayout();
            ((ISupportInitialize) this.SalesItems_Gridview).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.TransactionWise_Tabpage.Controls.Add(this.panel3);
            this.TransactionWise_Tabpage.Controls.Add(this.Sales_Gridview);
            this.TransactionWise_Tabpage.Controls.Add(this.Panel_Controls);
            this.TransactionWise_Tabpage.Controls.Add(this.panel2);
            this.TransactionWise_Tabpage.Location = new Point(4, 0x1b);
            this.TransactionWise_Tabpage.Name = "TransactionWise_Tabpage";
            this.TransactionWise_Tabpage.Padding = new Padding(3);
            this.TransactionWise_Tabpage.Size = new Size(0x3d7, 0x1a3);
            this.TransactionWise_Tabpage.TabIndex = 0;
            this.TransactionWise_Tabpage.Text = "Transaction Wise";
            this.TransactionWise_Tabpage.UseVisualStyleBackColor = true;
            this.panel3.Controls.Add(this.Transactionwise_Accounts_Gridview);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(3, 0x41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x3d1, 0x10d);
            this.panel3.TabIndex = 30;
            this.Transactionwise_Accounts_Gridview.AllowUserToAddRows = false;
            this.Transactionwise_Accounts_Gridview.AllowUserToDeleteRows = false;
            this.Transactionwise_Accounts_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Transactionwise_Accounts_Gridview.BackgroundColor = SystemColors.Window;
            this.Transactionwise_Accounts_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.Transactionwise_Accounts_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Transactionwise_Accounts_Gridview.Dock = DockStyle.Fill;
            this.Transactionwise_Accounts_Gridview.EnableHeadersVisualStyles = false;
            this.Transactionwise_Accounts_Gridview.Location = new Point(0, 0);
            this.Transactionwise_Accounts_Gridview.Name = "Transactionwise_Accounts_Gridview";
            this.Transactionwise_Accounts_Gridview.ReadOnly = true;
            this.Transactionwise_Accounts_Gridview.RowHeadersVisible = false;
            this.Transactionwise_Accounts_Gridview.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.Transactionwise_Accounts_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_Accounts_Gridview.Size = new Size(0x3d1, 0x10d);
            this.Transactionwise_Accounts_Gridview.TabIndex = 0x18;
            this.Transactionwise_Accounts_Gridview.CellDoubleClick += new DataGridViewCellEventHandler(this.Accounts_Gridview_CellDoubleClick);
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
            this.Sales_Gridview.AllowUserToAddRows = false;
            this.Sales_Gridview.AllowUserToDeleteRows = false;
            this.Sales_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Sales_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.ControlText;
            style.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.ButtonHighlight;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.Sales_Gridview.ColumnHeadersDefaultCellStyle = style;
            this.Sales_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Sales_Gridview.Dock = DockStyle.Fill;
            this.Sales_Gridview.EnableHeadersVisualStyles = false;
            this.Sales_Gridview.Location = new Point(3, 0x41);
            this.Sales_Gridview.Name = "Sales_Gridview";
            this.Sales_Gridview.ReadOnly = true;
            this.Sales_Gridview.RowHeadersVisible = false;
            style2.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style2.ForeColor = Color.Black;
            this.Sales_Gridview.RowsDefaultCellStyle = style2;
            this.Sales_Gridview.Size = new Size(0x3d1, 0x10d);
            this.Sales_Gridview.TabIndex = 0x17;
            this.Panel_Controls.Controls.Add(this.ComboBox_PaymentType);
            this.Panel_Controls.Controls.Add(this.label1);
            this.Panel_Controls.Controls.Add(this.groupBox1);
            this.Panel_Controls.Controls.Add(this.Transactionwise_DateTo);
            this.Panel_Controls.Controls.Add(this.label2);
            this.Panel_Controls.Controls.Add(this.Btn_TransactionSearch);
            this.Panel_Controls.Controls.Add(this.Transactionwise_Txt_CashierIdBox);
            this.Panel_Controls.Controls.Add(this.label3);
            this.Panel_Controls.Controls.Add(this.Transactionwise_Datefrom);
            this.Panel_Controls.Controls.Add(this.label4);
            this.Panel_Controls.Dock = DockStyle.Top;
            this.Panel_Controls.Location = new Point(3, 3);
            this.Panel_Controls.Name = "Panel_Controls";
            this.Panel_Controls.Size = new Size(0x3d1, 0x3e);
            this.Panel_Controls.TabIndex = 0x1c;
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
            this.groupBox1.Controls.Add(this.Transactionwise_RadioButtonAll);
            this.groupBox1.Controls.Add(this.Transactionwise_RadioBtn_Periodical);
            this.groupBox1.Controls.Add(this.Transactionwise_RadioButton_Daily);
            this.groupBox1.Location = new Point(0xbf, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xee, 0x38);
            this.groupBox1.TabIndex = 0x12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sales Period";
            this.Transactionwise_RadioButtonAll.AutoSize = true;
            this.Transactionwise_RadioButtonAll.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_RadioButtonAll.Location = new Point(9, 0x18);
            this.Transactionwise_RadioButtonAll.Name = "Transactionwise_RadioButtonAll";
            this.Transactionwise_RadioButtonAll.Size = new Size(0x2f, 0x1a);
            this.Transactionwise_RadioButtonAll.TabIndex = 2;
            this.Transactionwise_RadioButtonAll.TabStop = true;
            this.Transactionwise_RadioButtonAll.Text = "All";
            this.Transactionwise_RadioButtonAll.UseVisualStyleBackColor = true;
            this.Transactionwise_RadioBtn_Periodical.AutoSize = true;
            this.Transactionwise_RadioBtn_Periodical.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_RadioBtn_Periodical.Location = new Point(0x8e, 0x18);
            this.Transactionwise_RadioBtn_Periodical.Name = "Transactionwise_RadioBtn_Periodical";
            this.Transactionwise_RadioBtn_Periodical.Size = new Size(0x5e, 0x1a);
            this.Transactionwise_RadioBtn_Periodical.TabIndex = 1;
            this.Transactionwise_RadioBtn_Periodical.TabStop = true;
            this.Transactionwise_RadioBtn_Periodical.Text = "Periodical";
            this.Transactionwise_RadioBtn_Periodical.UseVisualStyleBackColor = true;
            this.Transactionwise_RadioButton_Daily.AutoSize = true;
            this.Transactionwise_RadioButton_Daily.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_RadioButton_Daily.Location = new Point(0x47, 0x18);
            this.Transactionwise_RadioButton_Daily.Name = "Transactionwise_RadioButton_Daily";
            this.Transactionwise_RadioButton_Daily.Size = new Size(0x41, 0x1a);
            this.Transactionwise_RadioButton_Daily.TabIndex = 0;
            this.Transactionwise_RadioButton_Daily.TabStop = true;
            this.Transactionwise_RadioButton_Daily.Text = "Daily";
            this.Transactionwise_RadioButton_Daily.UseVisualStyleBackColor = true;
            this.Transactionwise_DateTo.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_DateTo.Format = DateTimePickerFormat.Short;
            this.Transactionwise_DateTo.Location = new Point(0x1f1, 0x22);
            this.Transactionwise_DateTo.Name = "Transactionwise_DateTo";
            this.Transactionwise_DateTo.Size = new Size(0x74, 0x17);
            this.Transactionwise_DateTo.TabIndex = 0x10;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label2.Location = new Point(0x10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x43, 0x16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Cashier";
            this.Btn_TransactionSearch.BackColor = Color.Maroon;
            this.Btn_TransactionSearch.FlatStyle = FlatStyle.Flat;
            this.Btn_TransactionSearch.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_TransactionSearch.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_TransactionSearch.Location = new Point(0x330, 11);
            this.Btn_TransactionSearch.Name = "Btn_TransactionSearch";
            this.Btn_TransactionSearch.Size = new Size(150, 0x29);
            this.Btn_TransactionSearch.TabIndex = 0x11;
            this.Btn_TransactionSearch.Text = "Search";
            this.Btn_TransactionSearch.UseVisualStyleBackColor = false;
            this.Btn_TransactionSearch.Click += new EventHandler(this.Btn_Search_Click);
            this.Transactionwise_Txt_CashierIdBox.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.Transactionwise_Txt_CashierIdBox.BorderStyle = BorderStyle.FixedSingle;
            this.Transactionwise_Txt_CashierIdBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_Txt_CashierIdBox.Location = new Point(7, 30);
            this.Transactionwise_Txt_CashierIdBox.Name = "Transactionwise_Txt_CashierIdBox";
            this.Transactionwise_Txt_CashierIdBox.Size = new Size(0xa9, 0x1a);
            this.Transactionwise_Txt_CashierIdBox.TabIndex = 12;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label3.Location = new Point(0x1b9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new Size(50, 0x16);
            this.label3.TabIndex = 13;
            this.label3.Text = "From";
            this.Transactionwise_Datefrom.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_Datefrom.Format = DateTimePickerFormat.Short;
            this.Transactionwise_Datefrom.Location = new Point(0x1f1, 6);
            this.Transactionwise_Datefrom.Name = "Transactionwise_Datefrom";
            this.Transactionwise_Datefrom.Size = new Size(0x74, 0x17);
            this.Transactionwise_Datefrom.TabIndex = 15;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label4.Location = new Point(0x1b9, 0x20);
            this.label4.Name = "label4";
            this.label4.Size = new Size(30, 0x16);
            this.label4.TabIndex = 14;
            this.label4.Text = "To";
            this.panel2.Controls.Add(this.Transactionwise_textBox_Mpesa);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.Btn_TransactionwiseExport);
            this.panel2.Controls.Add(this.Transactionwise_textBoxTotal);
            this.panel2.Controls.Add(this.Transactionwise_textBox_Cards);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.Transactionwise_textBox_Cash);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(3, 0x14e);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3d1, 0x52);
            this.panel2.TabIndex = 0x1d;
            this.panel2.TabStop = false;
            this.panel2.Text = "Total Summary";
            this.Transactionwise_textBox_Mpesa.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_textBox_Mpesa.Location = new Point(0xc1, 0x29);
            this.Transactionwise_textBox_Mpesa.Name = "Transactionwise_textBox_Mpesa";
            this.Transactionwise_textBox_Mpesa.Size = new Size(0xae, 0x1d);
            this.Transactionwise_textBox_Mpesa.TabIndex = 13;
            this.Transactionwise_textBox_Mpesa.TextAlign = HorizontalAlignment.Center;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(590, 0x12);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x2c, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Total";
            this.Btn_TransactionwiseExport.BackColor = Color.Maroon;
            this.Btn_TransactionwiseExport.FlatStyle = FlatStyle.Flat;
            this.Btn_TransactionwiseExport.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_TransactionwiseExport.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_TransactionwiseExport.Location = new Point(0x330, 0x1d);
            this.Btn_TransactionwiseExport.Name = "Btn_TransactionwiseExport";
            this.Btn_TransactionwiseExport.Size = new Size(150, 0x29);
            this.Btn_TransactionwiseExport.TabIndex = 11;
            this.Btn_TransactionwiseExport.Text = "Export ";
            this.Btn_TransactionwiseExport.UseVisualStyleBackColor = false;
            this.Btn_TransactionwiseExport.Click += new EventHandler(this.Btn_Export_Click);
            this.Transactionwise_textBoxTotal.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_textBoxTotal.Location = new Point(0x243, 0x29);
            this.Transactionwise_textBoxTotal.Name = "Transactionwise_textBoxTotal";
            this.Transactionwise_textBoxTotal.Size = new Size(0xd8, 0x1d);
            this.Transactionwise_textBoxTotal.TabIndex = 9;
            this.Transactionwise_textBoxTotal.TextAlign = HorizontalAlignment.Center;
            this.Transactionwise_textBox_Cards.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_textBox_Cards.Location = new Point(0x181, 0x29);
            this.Transactionwise_textBox_Cards.Name = "Transactionwise_textBox_Cards";
            this.Transactionwise_textBox_Cards.Size = new Size(0xae, 0x1d);
            this.Transactionwise_textBox_Cards.TabIndex = 7;
            this.Transactionwise_textBox_Cards.TextAlign = HorizontalAlignment.Center;
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
            this.Transactionwise_textBox_Cash.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Transactionwise_textBox_Cash.Location = new Point(7, 0x29);
            this.Transactionwise_textBox_Cash.Name = "Transactionwise_textBox_Cash";
            this.Transactionwise_textBox_Cash.Size = new Size(0xae, 0x1d);
            this.Transactionwise_textBox_Cash.TabIndex = 0;
            this.Transactionwise_textBox_Cash.TextAlign = HorizontalAlignment.Center;
            this.tabControl1.Controls.Add(this.TransactionWise_Tabpage);
            this.tabControl1.Controls.Add(this.ItemWise_Tabpage);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x3df, 450);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new TabControlEventHandler(this.TabControl1_Selected);
            this.ItemWise_Tabpage.Controls.Add(this.SalesItems_Gridview);
            this.ItemWise_Tabpage.Controls.Add(this.panel1);
            this.ItemWise_Tabpage.Controls.Add(this.panel4);
            this.ItemWise_Tabpage.Location = new Point(4, 0x1b);
            this.ItemWise_Tabpage.Name = "ItemWise_Tabpage";
            this.ItemWise_Tabpage.Padding = new Padding(3);
            this.ItemWise_Tabpage.Size = new Size(0x3d7, 0x1a3);
            this.ItemWise_Tabpage.TabIndex = 1;
            this.ItemWise_Tabpage.Text = "Item Wise";
            this.ItemWise_Tabpage.UseVisualStyleBackColor = true;
            this.SalesItems_Gridview.AllowUserToAddRows = false;
            this.SalesItems_Gridview.AllowUserToDeleteRows = false;
            this.SalesItems_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.SalesItems_Gridview.BackgroundColor = SystemColors.ControlLightLight;
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = SystemColors.Control;
            style3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style3.ForeColor = SystemColors.WindowText;
            style3.SelectionBackColor = SystemColors.Highlight;
            style3.SelectionForeColor = SystemColors.HighlightText;
            style3.WrapMode = DataGridViewTriState.True;
            this.SalesItems_Gridview.ColumnHeadersDefaultCellStyle = style3;
            this.SalesItems_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[13];
            columnArray2[0] = this.dataGridViewTextBoxColumn1;
            columnArray2[1] = this.dataGridViewTextBoxColumn2;
            columnArray2[2] = this.dataGridViewTextBoxColumn3;
            columnArray2[3] = this.dataGridViewTextBoxColumn4;
            columnArray2[4] = this.dataGridViewTextBoxColumn5;
            columnArray2[5] = this.dataGridViewTextBoxColumn6;
            columnArray2[6] = this.dataGridViewTextBoxColumn7;
            columnArray2[7] = this.dataGridViewTextBoxColumn8;
            columnArray2[8] = this.dataGridViewTextBoxColumn9;
            columnArray2[9] = this.Column11;
            columnArray2[10] = this.Column12;
            columnArray2[11] = this.Column10;
            columnArray2[12] = this.Column13;
            this.SalesItems_Gridview.Columns.AddRange(columnArray2);
            this.SalesItems_Gridview.Dock = DockStyle.Fill;
            this.SalesItems_Gridview.EnableHeadersVisualStyles = false;
            this.SalesItems_Gridview.Location = new Point(3, 0x41);
            this.SalesItems_Gridview.Name = "SalesItems_Gridview";
            this.SalesItems_Gridview.ReadOnly = true;
            this.SalesItems_Gridview.RowHeadersVisible = false;
            this.SalesItems_Gridview.RowTemplate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.SalesItems_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.SalesItems_Gridview.Size = new Size(0x3d1, 0x127);
            this.SalesItems_Gridview.TabIndex = 0x1d;
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "Price";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "Total";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.HeaderText = "Discount";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.HeaderText = "Profit";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn9.HeaderText = "Balance";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            this.Column11.HeaderText = "Date";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column12.HeaderText = "Time";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column10.HeaderText = "Cashier";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column13.HeaderText = "Counter";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.panel1.Controls.Add(this.Itemwise__Textbox_Cards);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.Itemwise_Textbox_Cash);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.Itemwise_TextboxTotalitems);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(3, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3d1, 0x38);
            this.panel1.TabIndex = 0x1c;
            this.Itemwise__Textbox_Cards.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise__Textbox_Cards.Location = new Point(0x275, 9);
            this.Itemwise__Textbox_Cards.Name = "Itemwise__Textbox_Cards";
            this.Itemwise__Textbox_Cards.ReadOnly = true;
            this.Itemwise__Textbox_Cards.Size = new Size(0x8a, 0x1d);
            this.Itemwise__Textbox_Cards.TabIndex = 15;
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label9.Location = new Point(0x205, 12);
            this.label9.Name = "label9";
            this.label9.Size = new Size(90, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Cards Total";
            this.Itemwise_Textbox_Cash.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_Textbox_Cash.Location = new Point(0x149, 9);
            this.Itemwise_Textbox_Cash.Name = "Itemwise_Textbox_Cash";
            this.Itemwise_Textbox_Cash.ReadOnly = true;
            this.Itemwise_Textbox_Cash.Size = new Size(0xb2, 0x1d);
            this.Itemwise_Textbox_Cash.TabIndex = 11;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(270, 12);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x2e, 20);
            this.label10.TabIndex = 10;
            this.label10.Text = "Cash";
            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label11.Location = new Point(3, 9);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x58, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "Total Items";
            this.Itemwise_TextboxTotalitems.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_TextboxTotalitems.Location = new Point(0x5c, 9);
            this.Itemwise_TextboxTotalitems.Name = "Itemwise_TextboxTotalitems";
            this.Itemwise_TextboxTotalitems.ReadOnly = true;
            this.Itemwise_TextboxTotalitems.Size = new Size(0xa3, 0x1d);
            this.Itemwise_TextboxTotalitems.TabIndex = 0;
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.Itemwise_DateTo);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.Btn_ItemwiseSearch);
            this.panel4.Controls.Add(this.Itemwise_Txt_SearchBox);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.Itemwise_DateFrom);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Dock = DockStyle.Top;
            this.panel4.Location = new Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x3d1, 0x3e);
            this.panel4.TabIndex = 0x1b;
            this.groupBox2.Controls.Add(this.Itemwise_RadioButton_All);
            this.groupBox2.Controls.Add(this.Itemwise_RadioButton_PeriodicalI);
            this.groupBox2.Controls.Add(this.Itemwise_RadioButton_Daily);
            this.groupBox2.Location = new Point(0xcb, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xee, 0x38);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sales Period";
            this.Itemwise_RadioButton_All.AutoSize = true;
            this.Itemwise_RadioButton_All.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_RadioButton_All.Location = new Point(9, 0x18);
            this.Itemwise_RadioButton_All.Name = "Itemwise_RadioButton_All";
            this.Itemwise_RadioButton_All.Size = new Size(0x2f, 0x1a);
            this.Itemwise_RadioButton_All.TabIndex = 2;
            this.Itemwise_RadioButton_All.TabStop = true;
            this.Itemwise_RadioButton_All.Text = "All";
            this.Itemwise_RadioButton_All.UseVisualStyleBackColor = true;
            this.Itemwise_RadioButton_PeriodicalI.AutoSize = true;
            this.Itemwise_RadioButton_PeriodicalI.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_RadioButton_PeriodicalI.Location = new Point(0x8e, 0x18);
            this.Itemwise_RadioButton_PeriodicalI.Name = "Itemwise_RadioButton_PeriodicalI";
            this.Itemwise_RadioButton_PeriodicalI.Size = new Size(0x5e, 0x1a);
            this.Itemwise_RadioButton_PeriodicalI.TabIndex = 1;
            this.Itemwise_RadioButton_PeriodicalI.TabStop = true;
            this.Itemwise_RadioButton_PeriodicalI.Text = "Periodical";
            this.Itemwise_RadioButton_PeriodicalI.UseVisualStyleBackColor = true;
            this.Itemwise_RadioButton_Daily.AutoSize = true;
            this.Itemwise_RadioButton_Daily.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_RadioButton_Daily.Location = new Point(0x47, 0x18);
            this.Itemwise_RadioButton_Daily.Name = "Itemwise_RadioButton_Daily";
            this.Itemwise_RadioButton_Daily.Size = new Size(0x41, 0x1a);
            this.Itemwise_RadioButton_Daily.TabIndex = 0;
            this.Itemwise_RadioButton_Daily.TabStop = true;
            this.Itemwise_RadioButton_Daily.Text = "Daily";
            this.Itemwise_RadioButton_Daily.UseVisualStyleBackColor = true;
            this.Itemwise_DateTo.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_DateTo.Format = DateTimePickerFormat.Short;
            this.Itemwise_DateTo.Location = new Point(0x1f7, 0x21);
            this.Itemwise_DateTo.Name = "Itemwise_DateTo";
            this.Itemwise_DateTo.Size = new Size(0x74, 0x17);
            this.Itemwise_DateTo.TabIndex = 8;
            this.label12.AutoSize = true;
            this.label12.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label12.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label12.Location = new Point(0x39, 5);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x43, 0x16);
            this.label12.TabIndex = 3;
            this.label12.Text = "Cashier";
            this.Btn_ItemwiseSearch.BackColor = Color.Maroon;
            this.Btn_ItemwiseSearch.FlatStyle = FlatStyle.Flat;
            this.Btn_ItemwiseSearch.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_ItemwiseSearch.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_ItemwiseSearch.Location = new Point(0x288, 10);
            this.Btn_ItemwiseSearch.Name = "Btn_ItemwiseSearch";
            this.Btn_ItemwiseSearch.Size = new Size(150, 0x29);
            this.Btn_ItemwiseSearch.TabIndex = 9;
            this.Btn_ItemwiseSearch.Text = "Search";
            this.Btn_ItemwiseSearch.UseVisualStyleBackColor = false;
            this.Btn_ItemwiseSearch.Click += new EventHandler(this.Btn_ItemwiseSearch_Click);
            this.Itemwise_Txt_SearchBox.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.Itemwise_Txt_SearchBox.BorderStyle = BorderStyle.FixedSingle;
            this.Itemwise_Txt_SearchBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_Txt_SearchBox.Location = new Point(12, 30);
            this.Itemwise_Txt_SearchBox.Name = "Itemwise_Txt_SearchBox";
            this.Itemwise_Txt_SearchBox.Size = new Size(0xae, 0x1a);
            this.Itemwise_Txt_SearchBox.TabIndex = 4;
            this.label13.AutoSize = true;
            this.label13.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label13.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label13.Location = new Point(0x1bf, 6);
            this.label13.Name = "label13";
            this.label13.Size = new Size(50, 0x16);
            this.label13.TabIndex = 5;
            this.label13.Text = "From";
            this.Itemwise_DateFrom.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Itemwise_DateFrom.Format = DateTimePickerFormat.Short;
            this.Itemwise_DateFrom.Location = new Point(0x1f7, 6);
            this.Itemwise_DateFrom.Name = "Itemwise_DateFrom";
            this.Itemwise_DateFrom.Size = new Size(0x74, 0x17);
            this.Itemwise_DateFrom.TabIndex = 7;
            this.label14.AutoSize = true;
            this.label14.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label14.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label14.Location = new Point(0x1bf, 0x21);
            this.label14.Name = "label14";
            this.label14.Size = new Size(30, 0x16);
            this.label14.TabIndex = 6;
            this.label14.Text = "To";
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3df, 450);
            base.ControlBox = false;
            base.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SalesRecords";
            base.ShowIcon = false;
            this.Text = "SalesRecords";
            base.Shown += new EventHandler(this.SalesRecords_Shown);
            this.TransactionWise_Tabpage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((ISupportInitialize) this.Transactionwise_Accounts_Gridview).EndInit();
            ((ISupportInitialize) this.Sales_Gridview).EndInit();
            this.Panel_Controls.ResumeLayout(false);
            this.Panel_Controls.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ItemWise_Tabpage.ResumeLayout(false);
            ((ISupportInitialize) this.SalesItems_Gridview).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void SalesRecords_Shown(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = this.tabControl1.TabPages[0];
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (ReferenceEquals(this.tabControl1.SelectedTab, this.tabControl1.TabPages["TransactionWise_Tabpage"]))
            {
                this.Transactionwise_RadioButton_Daily.Checked = true;
            }
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
            if (this.Transactionwise_RadioButton_Daily.Checked)
            {
                if (this.Transactionwise_Datefrom.Value.Date == this.Transactionwise_DateTo.Value.Date)
                {
                    str = " AND date_format(a.Date,'%Y-%m-%d')=@dateto ";
                    this.Report_Title = "Daily Sales Report for: " + this.Transactionwise_Datefrom.Value.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("From and To date must be Equal!!", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Transactionwise_RadioButtonAll.Checked = true;
                }
            }
            else if (this.Transactionwise_RadioBtn_Periodical.Checked)
            {
                if (this.Transactionwise_DateTo.Value.Date > this.Transactionwise_Datefrom.Value.Date)
                {
                    str = " AND date_format(a.Date,'%Y-%m-%d')>=@datefrom AND date_format(a.Date,'%Y-%m-%d')<=@dateto ";
                    this.Report_Title = "Sales Report from " + this.Transactionwise_Datefrom.Value.ToShortDateString() + " To " + this.Transactionwise_DateTo.Value.ToShortDateString();
                }
                else
                {
                    MessageBox.Show("Date range must be greater than one day!!", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Transactionwise_RadioButtonAll.Checked = true;
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
                return ((this.Transactionwise_Txt_CashierIdBox.Text != string.Empty) ? (((str2 == "") && (str3 == "")) ? ((str2 == "") ? ((str3 == "") ? "SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier;" : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier " + str3)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier " + str2)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' AND a.UserId=@cashier " + str3 + str2)) : (((str2 == "") && (str3 == "")) ? ((str2 == "") ? ((str3 == "") ? "SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND a.AccType='Receivable'" : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' " + str3)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' " + str2)) : ("SELECT a.TransactionNo,a.AccType,a.Amount,a.PaymentMethod, b.Billno,b.Totalpaid,b.Balance,b.BillDate,b.Cashier,b.Counter,b.Branch FROM `p.o.s`.billmaster b,`p.o.s`.accounts a where b.billno = a.TransactionNo AND  a.AccType='Receivable' " + str3 + str2)));
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return "";
            }
        }
    }
}

