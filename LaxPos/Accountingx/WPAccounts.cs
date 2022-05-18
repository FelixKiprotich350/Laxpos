namespace LaxPos.Accounting
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class WPAccounts : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private Panel Panel_Controls;
        private GroupBox groupBox1;
        private RadioButton RadioButtonAll;
        private RadioButton RadioBtn_Periodical;
        private RadioButton RadioButton_OneDay;
        private DateTimePicker dateTimePicker2;
        private Label label2;
        private Button Btn_Search;
        private TextBox Text_OpenedbyBox;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Label label4;
        private GroupBox panel2;
        private TextBox textBox2;
        private Label label7;
        private Button Btn_Export;
        private TextBox textBox4;
        private TextBox textBox3;
        private Label label8;
        private Label label6;
        private Label label5;
        private TextBox textBox1;
        private DataGridView WorkperiodAccounts_Gridview;
        private Label label9;
        private TextBox Text_Closedby;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column10;
        private Panel panel1;

        public WPAccounts()
        {
            this.InitializeComponent();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            string sqlstatement = "SELECT * FROM workperiods WHERE workingdate IS NOT NULL ";
            if (!((this.RadioButtonAll.Checked || this.RadioButton_OneDay.Checked) || this.RadioBtn_Periodical.Checked))
            {
                this.RadioButton_OneDay.Checked = true;
            }
            if (this.Text_OpenedbyBox.Text != "")
            {
                sqlstatement = sqlstatement + " and openedby=@openedby ";
            }
            if (this.Text_Closedby.Text != "")
            {
                sqlstatement = sqlstatement + " and closedby=@closedby ";
            }
            if (this.RadioButtonAll.Checked)
            {
                sqlstatement ??= "";
            }
            if (this.RadioButton_OneDay.Checked)
            {
                sqlstatement = sqlstatement + " and workingdate=@datefrom ";
            }
            if (this.RadioBtn_Periodical.Checked)
            {
                sqlstatement = sqlstatement + " and workingdate>=@datefrom and workingdate<=@dateto ";
            }
            if (!this.RadioButton_OneDay.Checked || (this.dateTimePicker1.Value.ToShortDateString() == this.dateTimePicker2.Value.ToShortDateString()))
            {
                if (this.RadioBtn_Periodical.Checked && (this.dateTimePicker1.Value.ToShortDateString() == this.dateTimePicker1.Value.ToShortDateString()))
                {
                    MessageBox.Show("The From and To dates must not be Equal!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.GetWP_Accounts(sqlstatement);
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

        public void GetWP_Accounts(string sqlstatement)
        {
            try
            {
                this.WorkperiodAccounts_Gridview.Rows.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = sqlstatement;
                command.Parameters.AddWithValue("@openedby", this.Text_OpenedbyBox.Text);
                command.Parameters.AddWithValue("@closedby", this.Text_OpenedbyBox.Text);
                command.Parameters.AddWithValue("@datefrom", this.dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@dateto", this.dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    this.WorkperiodAccounts_Gridview.Rows.Clear();
                    MessageBox.Show("No Records Have Been Found !!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Btn_Search.Focus();
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[10];
                        values[0] = reader.GetDateTime("workingdate").ToString("yyyy-MM-dd");
                        values[1] = reader.GetDateTime("prevperioddate").ToString("yyyy-MM-dd");
                        values[2] = reader.GetDecimal("openingcash");
                        values[3] = reader.GetDecimal("openingmpesa");
                        values[4] = reader.GetDecimal("openingcards");
                        values[5] = reader.GetString("openingtime");
                        values[6] = reader.GetDecimal("closingcash");
                        values[7] = reader.GetDecimal("closingmpesa");
                        values[8] = reader.GetDecimal("closingcards");
                        values[9] = reader.GetString("closingtime");
                        this.WorkperiodAccounts_Gridview.Rows.Add(values);
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
            this.Panel_Controls = new Panel();
            this.panel1 = new Panel();
            this.dateTimePicker2 = new DateTimePicker();
            this.label4 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.label3 = new Label();
            this.label9 = new Label();
            this.Text_Closedby = new TextBox();
            this.groupBox1 = new GroupBox();
            this.RadioButtonAll = new RadioButton();
            this.RadioBtn_Periodical = new RadioButton();
            this.RadioButton_OneDay = new RadioButton();
            this.label2 = new Label();
            this.Btn_Search = new Button();
            this.Text_OpenedbyBox = new TextBox();
            this.panel2 = new GroupBox();
            this.textBox2 = new TextBox();
            this.label7 = new Label();
            this.Btn_Export = new Button();
            this.textBox4 = new TextBox();
            this.textBox3 = new TextBox();
            this.label8 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.textBox1 = new TextBox();
            this.WorkperiodAccounts_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Panel_Controls.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((ISupportInitialize) this.WorkperiodAccounts_Gridview).BeginInit();
            base.SuspendLayout();
            this.Panel_Controls.Controls.Add(this.panel1);
            this.Panel_Controls.Controls.Add(this.label9);
            this.Panel_Controls.Controls.Add(this.Text_Closedby);
            this.Panel_Controls.Controls.Add(this.groupBox1);
            this.Panel_Controls.Controls.Add(this.label2);
            this.Panel_Controls.Controls.Add(this.Btn_Search);
            this.Panel_Controls.Controls.Add(this.Text_OpenedbyBox);
            this.Panel_Controls.Dock = DockStyle.Top;
            this.Panel_Controls.Location = new Point(0, 0);
            this.Panel_Controls.Name = "Panel_Controls";
            this.Panel_Controls.Size = new Size(0x3ee, 0x3e);
            this.Panel_Controls.TabIndex = 0x16;
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new Point(0x29c, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(220, 60);
            this.panel1.TabIndex = 0x18;
            this.dateTimePicker2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new Point(0x3a, 0x21);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new Size(0x94, 0x17);
            this.dateTimePicker2.TabIndex = 0x10;
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label4.Location = new Point(6, 30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(30, 0x16);
            this.label4.TabIndex = 14;
            this.label4.Text = "To";
            this.dateTimePicker1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new Point(0x3a, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(0x94, 0x17);
            this.dateTimePicker1.TabIndex = 15;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label3.Location = new Point(2, 5);
            this.label3.Name = "label3";
            this.label3.Size = new Size(50, 0x16);
            this.label3.TabIndex = 13;
            this.label3.Text = "From";
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label9.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label9.Location = new Point(0xcb, 5);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x55, 0x16);
            this.label9.TabIndex = 0x15;
            this.label9.Text = "Closed By";
            this.Text_Closedby.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.Text_Closedby.BorderStyle = BorderStyle.FixedSingle;
            this.Text_Closedby.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Text_Closedby.Location = new Point(0xcb, 0x1d);
            this.Text_Closedby.Name = "Text_Closedby";
            this.Text_Closedby.Size = new Size(0xa9, 0x1a);
            this.Text_Closedby.TabIndex = 0x16;
            this.groupBox1.Controls.Add(this.RadioButtonAll);
            this.groupBox1.Controls.Add(this.RadioBtn_Periodical);
            this.groupBox1.Controls.Add(this.RadioButton_OneDay);
            this.groupBox1.Location = new Point(0x183, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x10b, 0x38);
            this.groupBox1.TabIndex = 0x12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accounts Work Period";
            this.RadioButtonAll.AutoSize = true;
            this.RadioButtonAll.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.RadioButtonAll.Location = new Point(9, 0x18);
            this.RadioButtonAll.Name = "RadioButtonAll";
            this.RadioButtonAll.Size = new Size(0x2f, 0x1a);
            this.RadioButtonAll.TabIndex = 2;
            this.RadioButtonAll.TabStop = true;
            this.RadioButtonAll.Text = "All";
            this.RadioButtonAll.UseVisualStyleBackColor = true;
            this.RadioButtonAll.CheckedChanged += new EventHandler(this.RadioButtonAll_CheckedChanged);
            this.RadioBtn_Periodical.AutoSize = true;
            this.RadioBtn_Periodical.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.RadioBtn_Periodical.Location = new Point(0xa3, 0x18);
            this.RadioBtn_Periodical.Name = "RadioBtn_Periodical";
            this.RadioBtn_Periodical.Size = new Size(0x5e, 0x1a);
            this.RadioBtn_Periodical.TabIndex = 1;
            this.RadioBtn_Periodical.TabStop = true;
            this.RadioBtn_Periodical.Text = "Periodical";
            this.RadioBtn_Periodical.UseVisualStyleBackColor = true;
            this.RadioBtn_Periodical.CheckedChanged += new EventHandler(this.RadioBtn_Periodical_CheckedChanged);
            this.RadioButton_OneDay.AutoSize = true;
            this.RadioButton_OneDay.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.RadioButton_OneDay.Location = new Point(0x3e, 0x18);
            this.RadioButton_OneDay.Name = "RadioButton_OneDay";
            this.RadioButton_OneDay.Size = new Size(90, 0x1a);
            this.RadioButton_OneDay.TabIndex = 0;
            this.RadioButton_OneDay.TabStop = true;
            this.RadioButton_OneDay.Text = "One Day";
            this.RadioButton_OneDay.UseVisualStyleBackColor = true;
            this.RadioButton_OneDay.CheckedChanged += new EventHandler(this.RadioButton_Daily_CheckedChanged);
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.ForeColor = Color.FromArgb(0, 0, 0x40);
            this.label2.Location = new Point(15, 5);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x5d, 0x16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Opened By";
            this.Btn_Search.BackColor = Color.Maroon;
            this.Btn_Search.FlatStyle = FlatStyle.Flat;
            this.Btn_Search.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Search.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Search.Location = new Point(0x39e, 12);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Size = new Size(120, 0x29);
            this.Btn_Search.TabIndex = 0x11;
            this.Btn_Search.Text = "Search";
            this.Btn_Search.UseVisualStyleBackColor = false;
            this.Btn_Search.Click += new EventHandler(this.Btn_Search_Click);
            this.Text_OpenedbyBox.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.Text_OpenedbyBox.BorderStyle = BorderStyle.FixedSingle;
            this.Text_OpenedbyBox.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Text_OpenedbyBox.Location = new Point(15, 0x1d);
            this.Text_OpenedbyBox.Name = "Text_OpenedbyBox";
            this.Text_OpenedbyBox.Size = new Size(0xa9, 0x1a);
            this.Text_OpenedbyBox.TabIndex = 12;
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
            this.panel2.Location = new Point(0, 0x170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3ee, 0x52);
            this.panel2.TabIndex = 0x17;
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
            this.textBox4.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(0x243, 0x29);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0xd8, 0x1d);
            this.textBox4.TabIndex = 9;
            this.textBox4.TextAlign = HorizontalAlignment.Center;
            this.textBox3.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0x181, 0x29);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0xae, 0x1d);
            this.textBox3.TabIndex = 7;
            this.textBox3.TextAlign = HorizontalAlignment.Center;
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
            this.WorkperiodAccounts_Gridview.AllowUserToAddRows = false;
            this.WorkperiodAccounts_Gridview.AllowUserToDeleteRows = false;
            this.WorkperiodAccounts_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.WorkperiodAccounts_Gridview.BackgroundColor = SystemColors.Window;
            this.WorkperiodAccounts_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[10];
            dataGridViewColumns[0] = this.Column1;
            dataGridViewColumns[1] = this.Column3;
            dataGridViewColumns[2] = this.Column5;
            dataGridViewColumns[3] = this.Column6;
            dataGridViewColumns[4] = this.Column9;
            dataGridViewColumns[5] = this.Column4;
            dataGridViewColumns[6] = this.Column2;
            dataGridViewColumns[7] = this.Column7;
            dataGridViewColumns[8] = this.Column8;
            dataGridViewColumns[9] = this.Column10;
            this.WorkperiodAccounts_Gridview.Columns.AddRange(dataGridViewColumns);
            this.WorkperiodAccounts_Gridview.Dock = DockStyle.Fill;
            this.WorkperiodAccounts_Gridview.Location = new Point(0, 0x3e);
            this.WorkperiodAccounts_Gridview.Name = "WorkperiodAccounts_Gridview";
            this.WorkperiodAccounts_Gridview.ReadOnly = true;
            this.WorkperiodAccounts_Gridview.RowHeadersVisible = false;
            this.WorkperiodAccounts_Gridview.Size = new Size(0x3ee, 0x132);
            this.WorkperiodAccounts_Gridview.TabIndex = 0x19;
            this.WorkperiodAccounts_Gridview.CellDoubleClick += new DataGridViewCellEventHandler(this.WorkperiodAccounts_Gridview_CellDoubleClick);
            this.Column1.HeaderText = "WorkPeriod";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column3.HeaderText = "PrevPeriod";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column5.HeaderText = "Ocash";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column6.HeaderText = "Ompesa";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column9.HeaderText = "Ocards";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column4.HeaderText = "Otime";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column2.HeaderText = "Ccash";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column7.HeaderText = "Cmpesa";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column8.HeaderText = "Ccards";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column10.HeaderText = "Ctime";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = SystemColors.Control;
            base.ClientSize = new Size(0x3ee, 450);
            base.Controls.Add(this.WorkperiodAccounts_Gridview);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.Panel_Controls);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "WPAccounts";
            this.Text = "Work Period Accounts";
            this.Panel_Controls.ResumeLayout(false);
            this.Panel_Controls.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((ISupportInitialize) this.WorkperiodAccounts_Gridview).EndInit();
            base.ResumeLayout(false);
        }

        private void RadioBtn_Periodical_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = true;
            this.dateTimePicker1.Enabled = true;
            this.dateTimePicker2.Enabled = true;
            this.label3.Text = "From";
            this.label4.Visible = true;
            this.dateTimePicker2.Visible = true;
        }

        private void RadioButton_Daily_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = true;
            this.dateTimePicker1.Enabled = true;
            this.dateTimePicker2.Enabled = false;
            this.label3.Text = "Date";
            this.label4.Visible = false;
            this.dateTimePicker2.Visible = false;
        }

        private void RadioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = false;
            this.label3.Text = "From";
        }

        private void WorkperiodAccounts_Gridview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0)
                {
                    string str = this.WorkperiodAccounts_Gridview.Rows[rowIndex].Cells[0].Value.ToString();
                    new WorkPeriodDetails { textBox1 = { Text = str } }.Show(this);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}

