namespace LaxPos.Reports
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class DailySales : UserControl
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private PrintPreviewDialog printPreviewDialog1;
        private Button Btn_PrintResults;
        private TextBox textBox3;
        private Panel panel2;
        private Label label8;
        private Label label6;
        private TextBox textBox1;
        private PrintDocument printDocument1;
        private DateTimePicker dateTimePicker2;
        private Label label2;
        private Button Btn_Search;
        private TextBox Txt_SearchBox;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private RadioButton RadioButtonAll;
        private RadioButton RadioBtn_Periodical;
        private RadioButton RadioButton_Daily;
        private GroupBox groupBox1;
        private Panel Panel_Controls;
        private Label label4;
        private DataGridView Sales_Gridview;
        private TextBox textBox2;
        private Label label1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column6;

        public DailySales()
        {
            this.InitializeComponent();
        }

        private void Accounts_Gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Txt_SearchBox.Text != string.Empty)
                {
                    if (this.RadioButtonAll.Checked)
                    {
                        this.GetTransactionsRecords(" where ReceivedBy=@Userid;");
                        this.FindTotal();
                    }
                    if (this.RadioButton_Daily.Checked)
                    {
                        DateTime time1 = this.dateTimePicker1.Value;
                        if (this.dateTimePicker1.Value.ToShortDateString() == this.dateTimePicker2.Value.ToShortDateString())
                        {
                            this.GetTransactionsRecords("where ReceivedBy=@Userid and TransDate=@datefrom;");
                            this.FindTotal();
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
                            this.GetTransactionsRecords(" where ReceivedBy=@Userid and TransDate>@datefrom AND TransDate<@dateto;");
                            this.FindTotal();
                        }
                    }
                }
                else
                {
                    if (this.RadioButtonAll.Checked)
                    {
                        this.GetTransactionsRecords(";");
                        this.FindTotal();
                    }
                    if (this.RadioButton_Daily.Checked)
                    {
                        DateTime time4 = this.dateTimePicker1.Value;
                        if (this.dateTimePicker1.Value.ToShortDateString() == this.dateTimePicker2.Value.ToShortDateString())
                        {
                            this.GetTransactionsRecords("where  TransDate=@datefrom;");
                            this.FindTotal();
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
                            this.GetTransactionsRecords(" where TransDate>@datefrom AND TransDate<@dateto;");
                            this.FindTotal();
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
            }
        }

        public void ClearGridview()
        {
            try
            {
                if ((this.Sales_Gridview.Rows.Count > 0) && (MessageBox.Show("Report period Changed. Clear The Previous Data On Grid ?", "CONFIRMATION BOX", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    this.Sales_Gridview.Rows.Clear();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DailySales_Load(object sender, EventArgs e)
        {
            base.Visible = true;
            this.RadioButtonAll.Checked = true;
            this.Txt_SearchBox.Focus();
            base.ParentForm.AcceptButton = this.Btn_Search;
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
                decimal num = 0M;
                decimal num2 = 0M;
                decimal num3 = 0M;
                decimal num4 = 0M;
                if (this.Sales_Gridview.Rows.Count > 0)
                {
                    int count = this.Sales_Gridview.Rows.Count;
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= count)
                        {
                            this.textBox3.Text = num.ToString();
                            this.textBox1.Text = this.Sales_Gridview.Rows.Count.ToString();
                            this.textBox2.Text = num4.ToString();
                            break;
                        }
                        num += Convert.ToDecimal(this.Sales_Gridview.Rows[num6].Cells[1].Value.ToString());
                        num2 += Convert.ToDecimal(this.Sales_Gridview.Rows[num6].Cells[2].Value.ToString());
                        num3 += Convert.ToDecimal(this.Sales_Gridview.Rows[num6].Cells[3].Value.ToString());
                        num4 += Convert.ToDecimal(this.Sales_Gridview.Rows[num6].Cells[7].Value.ToString());
                        num6++;
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
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select a.TransNo,a.Gross,a.AmountPaid,a.Balance,a.TransDate,a.WorkStationID,a.ReceivedBy,Profit from itemsales a " + Parameter;
                command.Parameters.Add("@Userid", MySqlDbType.VarChar).Value = this.Txt_SearchBox.Text.Trim();
                command.Parameters.Add("@datefrom", MySqlDbType.Date).Value = this.dateTimePicker1.Value;
                command.Parameters.AddWithValue("@dateto", this.dateTimePicker2.Value);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Records Have Been Found !!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.ClearGridview();
                    this.Txt_SearchBox.Focus();
                    this.RadioButtonAll.Checked = true;
                }
                else
                {
                    this.Sales_Gridview.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader[0], reader[1].ToString(), reader[2], reader[3].ToString(), reader[4], reader[6], reader[5], reader[7] };
                        this.Sales_Gridview.Rows.Add(values);
                    }
                }
                command.Dispose();
                reader.Dispose();
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(DailySales));
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.printPreviewDialog1 = new PrintPreviewDialog();
            this.Btn_PrintResults = new Button();
            this.textBox3 = new TextBox();
            this.panel2 = new Panel();
            this.textBox2 = new TextBox();
            this.label1 = new Label();
            this.label8 = new Label();
            this.label6 = new Label();
            this.textBox1 = new TextBox();
            this.printDocument1 = new PrintDocument();
            this.dateTimePicker2 = new DateTimePicker();
            this.label2 = new Label();
            this.Btn_Search = new Button();
            this.Txt_SearchBox = new TextBox();
            this.label3 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.RadioButtonAll = new RadioButton();
            this.RadioBtn_Periodical = new RadioButton();
            this.RadioButton_Daily = new RadioButton();
            this.groupBox1 = new GroupBox();
            this.Panel_Controls = new Panel();
            this.label4 = new Label();
            this.Sales_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Panel_Controls.SuspendLayout();
            ((ISupportInitialize) this.Sales_Gridview).BeginInit();
            base.SuspendLayout();
            this.printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            this.printPreviewDialog1.ClientSize = new Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = (Icon) manager.GetObject("printPreviewDialog1.Icon");
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            this.Btn_PrintResults.BackColor = Color.Maroon;
            this.Btn_PrintResults.FlatStyle = FlatStyle.Flat;
            this.Btn_PrintResults.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_PrintResults.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_PrintResults.Location = new Point(0x421, 9);
            this.Btn_PrintResults.Name = "Btn_PrintResults";
            this.Btn_PrintResults.Size = new Size(150, 0x29);
            this.Btn_PrintResults.TabIndex = 11;
            this.Btn_PrintResults.Text = "Print Report";
            this.Btn_PrintResults.UseVisualStyleBackColor = false;
            this.textBox3.BackColor = SystemColors.ButtonHighlight;
            this.textBox3.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0x166, 0x11);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0x8a, 0x1d);
            this.textBox3.TabIndex = 7;
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.Btn_PrintResults);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x240);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x4c3, 0x3d);
            this.panel2.TabIndex = 0x19;
            this.textBox2.BackColor = SystemColors.ButtonHighlight;
            this.textBox2.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0x236, 0x11);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x8a, 0x1d);
            this.textBox2.TabIndex = 13;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x202, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2e, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Profit";
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label8.Location = new Point(0xea, 0x11);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x70, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "Gross Amount";
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(3, 0x11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(100, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Transactions";
            this.textBox1.BackColor = SystemColors.ButtonHighlight;
            this.textBox1.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0x73, 0x11);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x60, 0x1d);
            this.textBox1.TabIndex = 0;
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
            this.label3.Location = new Point(0x28d, 0);
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
            this.groupBox1.Controls.Add(this.RadioButtonAll);
            this.groupBox1.Controls.Add(this.RadioBtn_Periodical);
            this.groupBox1.Controls.Add(this.RadioButton_Daily);
            this.groupBox1.Location = new Point(0x184, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xee, 0x38);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Period";
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
            this.Panel_Controls.Size = new Size(0x4c3, 0x3e);
            this.Panel_Controls.TabIndex = 0x18;
            this.Panel_Controls.Paint += new PaintEventHandler(this.Panel_Controls_Paint);
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label4.Location = new Point(810, 3);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x25, 0x1b);
            this.label4.TabIndex = 6;
            this.label4.Text = "To";
            this.Sales_Gridview.AllowUserToAddRows = false;
            this.Sales_Gridview.AllowUserToDeleteRows = false;
            this.Sales_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Sales_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            style.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.Sales_Gridview.ColumnHeadersDefaultCellStyle = style;
            this.Sales_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column1, this.Column3, this.Column5, this.Column4, this.Column2, this.Column7, this.Column8, this.Column6 };
            this.Sales_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Sales_Gridview.Dock = DockStyle.Fill;
            this.Sales_Gridview.EnableHeadersVisualStyles = false;
            this.Sales_Gridview.Location = new Point(0, 0x3e);
            this.Sales_Gridview.Name = "Sales_Gridview";
            this.Sales_Gridview.ReadOnly = true;
            this.Sales_Gridview.RowHeadersVisible = false;
            this.Sales_Gridview.Size = new Size(0x4c3, 0x202);
            this.Sales_Gridview.TabIndex = 0x1a;
            this.Sales_Gridview.CellContentClick += new DataGridViewCellEventHandler(this.Accounts_Gridview_CellContentClick);
            this.Column1.HeaderText = "TransactionNo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column3.HeaderText = "Gross";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column5.HeaderText = "AmountPaid";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column4.HeaderText = "Balance";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column2.HeaderText = "Date";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column7.HeaderText = "UserId";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column8.HeaderText = "Counter";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column6.HeaderText = "Profit";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.Sales_Gridview);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.Panel_Controls);
            base.Name = "DailySales";
            base.Size = new Size(0x4c3, 0x27d);
            base.Load += new EventHandler(this.DailySales_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Panel_Controls.ResumeLayout(false);
            this.Panel_Controls.PerformLayout();
            ((ISupportInitialize) this.Sales_Gridview).EndInit();
            base.ResumeLayout(false);
        }

        private void Panel_Controls_Paint(object sender, PaintEventArgs e)
        {
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x2000000;
                return createParams;
            }
        }
    }
}

