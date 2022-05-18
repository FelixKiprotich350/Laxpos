namespace LaxPos.Pos
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReprintReceipt : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox GroupBox_BillItems;
        private Panel panel3;
        private DataGridView DataGridView_BillItems;
        private DataGridView dataGridView2;
        private Panel panel1;
        private GroupBox GroupBox_BillDetails;
        private Button Btn_ExportReceipt;
        private Button Btn_Print;
        private TextBox textBox6;
        private Label label6;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private GroupBox GroupBox_BillNumbers;
        private TextBox textBox1;
        private Label label1;
        private TextBox textBox7;
        private Label label7;
        private TextBox textBox8;
        private Label label8;
        private TextBox textBox9;
        private Label label9;
        private Button Btn_CloseBill;
        private DataGridView DataGridView_TransactionCodes;
        private Button Btn_SearchBill;
        private TextBox textBox10;
        private Label label10;
        private TextBox textBox11;
        private Label label11;
        private DataGridViewTextBoxColumn TransactionCode;
        private DataGridViewTextBoxColumn Amount;
        private DataGridViewTextBoxColumn Balance;
        private Panel panel2;
        private DataGridViewTextBoxColumn ProductCode;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Units;
        private DataGridViewTextBoxColumn UnitPrice;
        private DataGridViewTextBoxColumn TotalPrice;
        private DataGridViewTextBoxColumn DiscPercantage;
        private DataGridViewTextBoxColumn Tax;
        private DataGridViewTextBoxColumn Profit;
        private DataGridViewTextBoxColumn TotalProfit;
        private DataGridViewTextBoxColumn DiscAmount;

        public ReprintReceipt()
        {
            this.InitializeComponent();
        }

        private void Btn_CloseBill_Click(object sender, EventArgs e)
        {
            this.CloseBill(0);
        }

        private void Btn_ExportReceipt_Click(object sender, EventArgs e)
        {
        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
        }

        private void Btn_SearchBill_Click(object sender, EventArgs e)
        {
            try
            {
                this.CloseBill(1);
                this.GetBillDetails(this.textBox2.Text.Trim());
                this.GetBillItems(this.textBox2.Text.Trim());
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void CloseBill(int T)
        {
            try
            {
                if (T != 0)
                {
                    this.DataGridView_BillItems.Rows.Clear();
                    this.textBox11.Text = "";
                    this.textBox10.Text = "";
                    this.textBox3.Text = "";
                    this.textBox5.Text = "";
                    this.textBox6.Text = "";
                    this.textBox1.Text = "";
                    this.textBox7.Text = "";
                    this.textBox8.Text = "";
                    this.textBox9.Text = "";
                }
                else
                {
                    this.DataGridView_BillItems.Rows.Clear();
                    this.textBox11.Text = "";
                    this.textBox10.Text = "";
                    this.textBox2.Text = "";
                    this.textBox3.Text = "";
                    this.textBox5.Text = "";
                    this.textBox6.Text = "";
                    this.textBox1.Text = "";
                    this.textBox7.Text = "";
                    this.textBox8.Text = "";
                    this.textBox9.Text = "";
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DataGridView_TransactionCodes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                this.CloseBill(0);
                string billNo = this.DataGridView_TransactionCodes.Rows[e.RowIndex].Cells[0].Value.ToString();
                this.GetBillDetails(billNo);
                this.GetBillItems(billNo);
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

        private void GetBillDetails(string BillNo)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command1 = new MySqlCommand();
                command1.CommandType = CommandType.Text;
                command1.Connection = connection;
                command1.CommandText = "SELECT * from `p.o.s`.billmaster where Billno=@transno";
                MySqlCommand command = command1;
                command.Parameters.AddWithValue("@transno", BillNo);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Transaction Details Not Found...", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox11.Text = reader["Billno"].ToString();
                        this.textBox10.Text = reader["Cashier"].ToString();
                        this.textBox3.Text = Convert.ToDateTime(reader["BillDate"].ToString()).ToShortDateString();
                        this.textBox5.Text = Convert.ToDateTime(reader["BillDate"].ToString()).ToShortTimeString();
                        this.textBox6.Text = reader["Counter"].ToString();
                        this.textBox1.Text = reader["Totalpaid"].ToString();
                        this.textBox7.Text = reader["TaxAmount"].ToString();
                        this.textBox8.Text = reader["Gross"].ToString();
                        this.textBox9.Text = reader["Balance"].ToString();
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void GetBillItems(string BillNo)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command1 = new MySqlCommand();
                command1.CommandType = CommandType.Text;
                command1.Connection = connection;
                command1.CommandText = "SELECT * from `p.o.s`.`itemsales` where TransNo=@transno";
                MySqlCommand command = command1;
                command.Parameters.AddWithValue("@transno", BillNo);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Transaction Related Items Not Found...", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["ProductCode"].ToString(), reader["Description"].ToString(), reader["Quantity"].ToString() };
                        this.DataGridView_BillItems.Rows.Add(values);
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
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.GroupBox_BillItems = new GroupBox();
            this.panel3 = new Panel();
            this.DataGridView_BillItems = new DataGridView();
            this.dataGridView2 = new DataGridView();
            this.panel1 = new Panel();
            this.GroupBox_BillDetails = new GroupBox();
            this.panel2 = new Panel();
            this.label11 = new Label();
            this.label3 = new Label();
            this.textBox10 = new TextBox();
            this.textBox3 = new TextBox();
            this.label10 = new Label();
            this.label5 = new Label();
            this.textBox11 = new TextBox();
            this.textBox5 = new TextBox();
            this.label6 = new Label();
            this.textBox6 = new TextBox();
            this.textBox9 = new TextBox();
            this.label8 = new Label();
            this.label9 = new Label();
            this.textBox8 = new TextBox();
            this.textBox1 = new TextBox();
            this.label7 = new Label();
            this.label1 = new Label();
            this.textBox7 = new TextBox();
            this.Btn_SearchBill = new Button();
            this.Btn_CloseBill = new Button();
            this.Btn_ExportReceipt = new Button();
            this.Btn_Print = new Button();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.GroupBox_BillNumbers = new GroupBox();
            this.DataGridView_TransactionCodes = new DataGridView();
            this.TransactionCode = new DataGridViewTextBoxColumn();
            this.Amount = new DataGridViewTextBoxColumn();
            this.Balance = new DataGridViewTextBoxColumn();
            this.ProductCode = new DataGridViewTextBoxColumn();
            this.Description = new DataGridViewTextBoxColumn();
            this.Quantity = new DataGridViewTextBoxColumn();
            this.Units = new DataGridViewTextBoxColumn();
            this.UnitPrice = new DataGridViewTextBoxColumn();
            this.TotalPrice = new DataGridViewTextBoxColumn();
            this.DiscPercantage = new DataGridViewTextBoxColumn();
            this.Tax = new DataGridViewTextBoxColumn();
            this.Profit = new DataGridViewTextBoxColumn();
            this.TotalProfit = new DataGridViewTextBoxColumn();
            this.DiscAmount = new DataGridViewTextBoxColumn();
            this.GroupBox_BillItems.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize) this.DataGridView_BillItems).BeginInit();
            ((ISupportInitialize) this.dataGridView2).BeginInit();
            this.panel1.SuspendLayout();
            this.GroupBox_BillDetails.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GroupBox_BillNumbers.SuspendLayout();
            ((ISupportInitialize) this.DataGridView_TransactionCodes).BeginInit();
            base.SuspendLayout();
            this.GroupBox_BillItems.Controls.Add(this.panel3);
            this.GroupBox_BillItems.Dock = DockStyle.Fill;
            this.GroupBox_BillItems.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.GroupBox_BillItems.Location = new Point(0, 0xee);
            this.GroupBox_BillItems.Name = "GroupBox_BillItems";
            this.GroupBox_BillItems.Size = new Size(0x4b0, 0xd4);
            this.GroupBox_BillItems.TabIndex = 6;
            this.GroupBox_BillItems.TabStop = false;
            this.GroupBox_BillItems.Text = "Bill Items";
            this.panel3.Controls.Add(this.DataGridView_BillItems);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.panel3.Location = new Point(3, 0x13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x4aa, 190);
            this.panel3.TabIndex = 1;
            this.DataGridView_BillItems.AllowUserToAddRows = false;
            this.DataGridView_BillItems.AllowUserToDeleteRows = false;
            this.DataGridView_BillItems.AllowUserToResizeColumns = false;
            this.DataGridView_BillItems.AllowUserToResizeRows = false;
            this.DataGridView_BillItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_BillItems.BackgroundColor = SystemColors.ButtonHighlight;
            this.DataGridView_BillItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[11];
            dataGridViewColumns[0] = this.ProductCode;
            dataGridViewColumns[1] = this.Description;
            dataGridViewColumns[2] = this.Quantity;
            dataGridViewColumns[3] = this.Units;
            dataGridViewColumns[4] = this.UnitPrice;
            dataGridViewColumns[5] = this.TotalPrice;
            dataGridViewColumns[6] = this.DiscPercantage;
            dataGridViewColumns[7] = this.Tax;
            dataGridViewColumns[8] = this.Profit;
            dataGridViewColumns[9] = this.TotalProfit;
            dataGridViewColumns[10] = this.DiscAmount;
            this.DataGridView_BillItems.Columns.AddRange(dataGridViewColumns);
            this.DataGridView_BillItems.Dock = DockStyle.Fill;
            this.DataGridView_BillItems.EnableHeadersVisualStyles = false;
            this.DataGridView_BillItems.Location = new Point(0, 0);
            this.DataGridView_BillItems.Name = "DataGridView_BillItems";
            this.DataGridView_BillItems.ReadOnly = true;
            this.DataGridView_BillItems.RowHeadersVisible = false;
            this.DataGridView_BillItems.RowTemplate.Height = 30;
            this.DataGridView_BillItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_BillItems.Size = new Size(0x4aa, 190);
            this.DataGridView_BillItems.TabIndex = 0;
            this.dataGridView2.BackgroundColor = SystemColors.ButtonHighlight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = DockStyle.Fill;
            this.dataGridView2.Location = new Point(3, 0x13);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new Size(0xce, 0x8a);
            this.dataGridView2.TabIndex = 1;
            this.panel1.Controls.Add(this.GroupBox_BillDetails);
            this.panel1.Controls.Add(this.GroupBox_BillNumbers);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x4b0, 0xee);
            this.panel1.TabIndex = 7;
            this.GroupBox_BillDetails.Controls.Add(this.panel2);
            this.GroupBox_BillDetails.Controls.Add(this.Btn_SearchBill);
            this.GroupBox_BillDetails.Controls.Add(this.Btn_CloseBill);
            this.GroupBox_BillDetails.Controls.Add(this.Btn_ExportReceipt);
            this.GroupBox_BillDetails.Controls.Add(this.Btn_Print);
            this.GroupBox_BillDetails.Controls.Add(this.textBox2);
            this.GroupBox_BillDetails.Controls.Add(this.label2);
            this.GroupBox_BillDetails.Dock = DockStyle.Fill;
            this.GroupBox_BillDetails.Location = new Point(0x17f, 0);
            this.GroupBox_BillDetails.Name = "GroupBox_BillDetails";
            this.GroupBox_BillDetails.Size = new Size(0x331, 0xee);
            this.GroupBox_BillDetails.TabIndex = 10;
            this.GroupBox_BillDetails.TabStop = false;
            this.GroupBox_BillDetails.Text = "Bill Details";
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBox10);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.textBox11);
            this.panel2.Controls.Add(this.textBox5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox6);
            this.panel2.Controls.Add(this.textBox9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.textBox8);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBox7);
            this.panel2.Location = new Point(6, 0x35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x279, 0xb3);
            this.panel2.TabIndex = 40;
            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label11.Location = new Point(0x10, 13);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x7c, 0x11);
            this.label11.TabIndex = 0x23;
            this.label11.Text = "Transaction Code:";
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(0x10, 0x35);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2a, 0x11);
            this.label3.TabIndex = 0x11;
            this.label3.Text = "Date:";
            this.textBox10.BackColor = SystemColors.ControlLightLight;
            this.textBox10.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox10.Location = new Point(0x1be, 12);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new Size(0xb1, 0x17);
            this.textBox10.TabIndex = 0x26;
            this.textBox10.TextAlign = HorizontalAlignment.Center;
            this.textBox3.BackColor = SystemColors.ControlLightLight;
            this.textBox3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0x40, 0x35);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0x61, 0x17);
            this.textBox3.TabIndex = 0x12;
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(370, 13);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x4d, 0x11);
            this.label10.TabIndex = 0x25;
            this.label10.Text = "Served By:";
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(0xb3, 0x35);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2b, 0x11);
            this.label5.TabIndex = 0x15;
            this.label5.Text = "Time:";
            this.textBox11.BackColor = SystemColors.ControlLightLight;
            this.textBox11.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox11.Location = new Point(0x92, 13);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new Size(0xc9, 0x17);
            this.textBox11.TabIndex = 0x24;
            this.textBox11.TextAlign = HorizontalAlignment.Center;
            this.textBox5.BackColor = SystemColors.ControlLightLight;
            this.textBox5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox5.Location = new Point(0xe1, 0x35);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0x7a, 0x17);
            this.textBox5.TabIndex = 0x16;
            this.textBox5.TextAlign = HorizontalAlignment.Center;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(370, 0x35);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3e, 0x11);
            this.label6.TabIndex = 0x17;
            this.label6.Text = "Counter:";
            this.textBox6.BackColor = SystemColors.ControlLightLight;
            this.textBox6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox6.Location = new Point(0x1be, 0x35);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(0xb1, 0x17);
            this.textBox6.TabIndex = 0x18;
            this.textBox6.TextAlign = HorizontalAlignment.Center;
            this.textBox9.BackColor = SystemColors.ControlLightLight;
            this.textBox9.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox9.Location = new Point(0x1cf, 0x84);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new Size(160, 0x17);
            this.textBox9.TabIndex = 0x21;
            this.textBox9.TextAlign = HorizontalAlignment.Center;
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label8.Location = new Point(0x10, 0x5f);
            this.label8.Name = "label8";
            this.label8.Size = new Size(110, 0x11);
            this.label8.TabIndex = 0x1a;
            this.label8.Text = "Tender Amount:";
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label9.Location = new Point(370, 0x84);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x3f, 0x11);
            this.label9.TabIndex = 0x20;
            this.label9.Text = "Balance:";
            this.textBox8.BackColor = SystemColors.ControlLightLight;
            this.textBox8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox8.Location = new Point(0x80, 0x5d);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new Size(0xdb, 0x17);
            this.textBox8.TabIndex = 0x1b;
            this.textBox8.TextAlign = HorizontalAlignment.Center;
            this.textBox1.BackColor = SystemColors.ControlLightLight;
            this.textBox1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0x80, 0x84);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0xdb, 0x17);
            this.textBox1.TabIndex = 0x1f;
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(370, 0x5d);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x57, 0x11);
            this.label7.TabIndex = 0x1c;
            this.label7.Text = "Tax Amount:";
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x10, 0x84);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5c, 0x11);
            this.label1.TabIndex = 30;
            this.label1.Text = "Amount Paid:";
            this.textBox7.BackColor = SystemColors.ControlLightLight;
            this.textBox7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox7.Location = new Point(0x1cf, 0x5d);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new Size(160, 0x17);
            this.textBox7.TabIndex = 0x1d;
            this.textBox7.TextAlign = HorizontalAlignment.Center;
            this.Btn_SearchBill.BackColor = SystemColors.Control;
            this.Btn_SearchBill.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_SearchBill.Location = new Point(370, 20);
            this.Btn_SearchBill.Name = "Btn_SearchBill";
            this.Btn_SearchBill.Size = new Size(250, 30);
            this.Btn_SearchBill.TabIndex = 0x27;
            this.Btn_SearchBill.Text = "Search this Bill";
            this.Btn_SearchBill.UseVisualStyleBackColor = false;
            this.Btn_SearchBill.Click += new EventHandler(this.Btn_SearchBill_Click);
            this.Btn_CloseBill.BackColor = SystemColors.Control;
            this.Btn_CloseBill.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_CloseBill.Location = new Point(0x297, 0x42);
            this.Btn_CloseBill.Name = "Btn_CloseBill";
            this.Btn_CloseBill.Size = new Size(0x8e, 30);
            this.Btn_CloseBill.TabIndex = 0x22;
            this.Btn_CloseBill.Text = "Close Bill";
            this.Btn_CloseBill.UseVisualStyleBackColor = false;
            this.Btn_CloseBill.Click += new EventHandler(this.Btn_CloseBill_Click);
            this.Btn_ExportReceipt.BackColor = SystemColors.Control;
            this.Btn_ExportReceipt.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_ExportReceipt.Location = new Point(0x297, 0x73);
            this.Btn_ExportReceipt.Name = "Btn_ExportReceipt";
            this.Btn_ExportReceipt.Size = new Size(0x8e, 30);
            this.Btn_ExportReceipt.TabIndex = 0x19;
            this.Btn_ExportReceipt.Text = "Export Receipt";
            this.Btn_ExportReceipt.UseVisualStyleBackColor = false;
            this.Btn_ExportReceipt.Click += new EventHandler(this.Btn_ExportReceipt_Click);
            this.Btn_Print.BackColor = SystemColors.Control;
            this.Btn_Print.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Print.Location = new Point(0x297, 0xa4);
            this.Btn_Print.Name = "Btn_Print";
            this.Btn_Print.Size = new Size(0x8e, 30);
            this.Btn_Print.TabIndex = 14;
            this.Btn_Print.Text = "Print Receipt";
            this.Btn_Print.UseVisualStyleBackColor = false;
            this.Btn_Print.Click += new EventHandler(this.Btn_Print_Click);
            this.textBox2.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.textBox2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0x67, 0x18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0xf1, 0x17);
            this.textBox2.TabIndex = 0x10;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(13, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x54, 0x11);
            this.label2.TabIndex = 15;
            this.label2.Text = "Bill Number:";
            this.GroupBox_BillNumbers.Controls.Add(this.DataGridView_TransactionCodes);
            this.GroupBox_BillNumbers.Dock = DockStyle.Left;
            this.GroupBox_BillNumbers.Location = new Point(0, 0);
            this.GroupBox_BillNumbers.Name = "GroupBox_BillNumbers";
            this.GroupBox_BillNumbers.Size = new Size(0x17f, 0xee);
            this.GroupBox_BillNumbers.TabIndex = 9;
            this.GroupBox_BillNumbers.TabStop = false;
            this.GroupBox_BillNumbers.Text = "Recent Transactions";
            this.DataGridView_TransactionCodes.AllowUserToAddRows = false;
            this.DataGridView_TransactionCodes.AllowUserToDeleteRows = false;
            this.DataGridView_TransactionCodes.AllowUserToResizeColumns = false;
            this.DataGridView_TransactionCodes.AllowUserToResizeRows = false;
            this.DataGridView_TransactionCodes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_TransactionCodes.BackgroundColor = SystemColors.ButtonHighlight;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = Color.FromArgb(0xff, 0xc0, 0x80);
            style.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.DataGridView_TransactionCodes.ColumnHeadersDefaultCellStyle = style;
            this.DataGridView_TransactionCodes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[] { this.TransactionCode, this.Amount, this.Balance };
            this.DataGridView_TransactionCodes.Columns.AddRange(columnArray2);
            this.DataGridView_TransactionCodes.Dock = DockStyle.Fill;
            this.DataGridView_TransactionCodes.EnableHeadersVisualStyles = false;
            this.DataGridView_TransactionCodes.Location = new Point(3, 0x13);
            this.DataGridView_TransactionCodes.Name = "DataGridView_TransactionCodes";
            this.DataGridView_TransactionCodes.ReadOnly = true;
            this.DataGridView_TransactionCodes.RowHeadersVisible = false;
            this.DataGridView_TransactionCodes.RowTemplate.Height = 30;
            this.DataGridView_TransactionCodes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_TransactionCodes.Size = new Size(0x179, 0xd8);
            this.DataGridView_TransactionCodes.TabIndex = 1;
            this.DataGridView_TransactionCodes.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.DataGridView_TransactionCodes_CellMouseDoubleClick);
            this.TransactionCode.HeaderText = "BillNo";
            this.TransactionCode.Name = "TransactionCode";
            this.TransactionCode.ReadOnly = true;
            this.Amount.HeaderText = "AmountPaid";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            this.ProductCode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.ProductCode.HeaderText = "Code";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            this.ProductCode.Width = 150;
            this.Description.FillWeight = 58.14433f;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Quantity.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 80;
            this.Units.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Units.HeaderText = "Unit";
            this.Units.Name = "Units";
            this.Units.ReadOnly = true;
            this.UnitPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.UnitPrice.HeaderText = "Price";
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            this.UnitPrice.Width = 150;
            this.TotalPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.TotalPrice.HeaderText = "Total";
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.ReadOnly = true;
            this.TotalPrice.Width = 200;
            this.DiscPercantage.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.DiscPercantage.FillWeight = 20f;
            this.DiscPercantage.HeaderText = "Disc%";
            this.DiscPercantage.Name = "DiscPercantage";
            this.DiscPercantage.ReadOnly = true;
            this.DiscPercantage.Width = 50;
            this.Tax.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Tax.FillWeight = 20f;
            this.Tax.HeaderText = "Tax%";
            this.Tax.Name = "Tax";
            this.Tax.ReadOnly = true;
            this.Tax.Width = 50;
            this.Profit.HeaderText = "Profit";
            this.Profit.Name = "Profit";
            this.Profit.ReadOnly = true;
            this.Profit.Visible = false;
            this.TotalProfit.HeaderText = "TProfit";
            this.TotalProfit.Name = "TotalProfit";
            this.TotalProfit.ReadOnly = true;
            this.TotalProfit.Visible = false;
            this.DiscAmount.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.DiscAmount.HeaderText = "DiscAmount";
            this.DiscAmount.Name = "DiscAmount";
            this.DiscAmount.ReadOnly = true;
            this.DiscAmount.Visible = false;
            this.DiscAmount.Width = 0x6c;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x4b0, 450);
            base.Controls.Add(this.GroupBox_BillItems);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ReprintReceipt";
            this.Text = "ReprintReceipt";
            base.Load += new EventHandler(this.ReprintReceipt_Load);
            this.GroupBox_BillItems.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((ISupportInitialize) this.DataGridView_BillItems).EndInit();
            ((ISupportInitialize) this.dataGridView2).EndInit();
            this.panel1.ResumeLayout(false);
            this.GroupBox_BillDetails.ResumeLayout(false);
            this.GroupBox_BillDetails.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.GroupBox_BillNumbers.ResumeLayout(false);
            ((ISupportInitialize) this.DataGridView_TransactionCodes).EndInit();
            base.ResumeLayout(false);
        }

        private void ReprintReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command1 = new MySqlCommand();
                command1.CommandType = CommandType.Text;
                command1.Connection = connection;
                command1.CommandText = "SELECT * from `p.o.s`.billmaster where Cashier=@userid and Counter=@counter order by sno desc limit 10";
                MySqlCommand command = command1;
                command.Parameters.AddWithValue("@userid", Program.CurrLoggedInUser.UserID);
                command.Parameters.AddWithValue("@counter", Program.LogInCounter);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No recent transactions found...", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["Billno"].ToString(), reader["Totalpaid"].ToString(), reader["Balance"].ToString() };
                        this.DataGridView_TransactionCodes.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}

