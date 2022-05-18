namespace LaxPos.Expenses
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TodaysExpense : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private Panel panel1;
        private DataGridView DataGrid_ExpenseItems;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private Panel panel2;
        private Button Btn_CloseX;
        private GroupBox groupBox1;
        private Button button3;
        private Button button2;
        private Label label2;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
        private Button Btn_Close;
        private Label label4;

        public TodaysExpense()
        {
            this.InitializeComponent();
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

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.DataGrid_ExpenseItems = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.panel2 = new Panel();
            this.Btn_CloseX = new Button();
            this.groupBox1 = new GroupBox();
            this.button3 = new Button();
            this.button2 = new Button();
            this.label2 = new Label();
            this.textBox2 = new TextBox();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.Btn_Close = new Button();
            this.label4 = new Label();
            this.panel1.SuspendLayout();
            ((ISupportInitialize) this.DataGrid_ExpenseItems).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DataGrid_ExpenseItems);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3c5, 0x148);
            this.panel1.TabIndex = 0;
            this.DataGrid_ExpenseItems.AllowUserToAddRows = false;
            this.DataGrid_ExpenseItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGrid_ExpenseItems.BackgroundColor = SystemColors.Control;
            this.DataGrid_ExpenseItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[9];
            dataGridViewColumns[0] = this.Column1;
            dataGridViewColumns[1] = this.Column5;
            dataGridViewColumns[2] = this.Column2;
            dataGridViewColumns[3] = this.Column3;
            dataGridViewColumns[4] = this.Column4;
            dataGridViewColumns[5] = this.Column6;
            dataGridViewColumns[6] = this.Column7;
            dataGridViewColumns[7] = this.Column8;
            dataGridViewColumns[8] = this.Column9;
            this.DataGrid_ExpenseItems.Columns.AddRange(dataGridViewColumns);
            this.DataGrid_ExpenseItems.Dock = DockStyle.Fill;
            this.DataGrid_ExpenseItems.EnableHeadersVisualStyles = false;
            this.DataGrid_ExpenseItems.Location = new Point(0, 0x1a);
            this.DataGrid_ExpenseItems.Name = "DataGrid_ExpenseItems";
            this.DataGrid_ExpenseItems.ReadOnly = true;
            this.DataGrid_ExpenseItems.RowHeadersVisible = false;
            this.DataGrid_ExpenseItems.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.DataGrid_ExpenseItems.Size = new Size(0x3c3, 0xeb);
            this.DataGrid_ExpenseItems.TabIndex = 5;
            this.Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 110f;
            this.Column1.HeaderText = "Description";
            this.Column1.MinimumWidth = 100;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column5.HeaderText = "Unit";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 90;
            this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "Qty";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 60;
            this.Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "Price";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 90;
            this.Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column4.HeaderText = "Total";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.FillWeight = 90f;
            this.Column6.HeaderText = "PaidTo";
            this.Column6.MinimumWidth = 100;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column7.HeaderText = "PaymentMethod";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column8.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column8.HeaderText = "ExpType";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column9.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column9.HeaderText = "ExpenseDate";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.panel2.BackColor = SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.Btn_CloseX);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3c3, 0x1a);
            this.panel2.TabIndex = 4;
            this.Btn_CloseX.BackColor = Color.Red;
            this.Btn_CloseX.Dock = DockStyle.Right;
            this.Btn_CloseX.FlatAppearance.BorderSize = 0;
            this.Btn_CloseX.FlatStyle = FlatStyle.Flat;
            this.Btn_CloseX.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_CloseX.Location = new Point(930, 0);
            this.Btn_CloseX.Margin = new Padding(1);
            this.Btn_CloseX.Name = "Btn_CloseX";
            this.Btn_CloseX.Size = new Size(0x21, 0x1a);
            this.Btn_CloseX.TabIndex = 1;
            this.Btn_CloseX.Text = "X";
            this.Btn_CloseX.UseVisualStyleBackColor = false;
            this.Btn_CloseX.Click += new EventHandler(this.Btn_Close_Click);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.Btn_Close);
            this.groupBox1.Dock = DockStyle.Bottom;
            this.groupBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox1.Location = new Point(0, 0x105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3c3, 0x41);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Summary";
            this.button3.Location = new Point(0x2f1, 0x15);
            this.button3.Name = "button3";
            this.button3.Size = new Size(0x5c, 30);
            this.button3.TabIndex = 6;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = true;
            this.button2.Location = new Point(640, 0x15);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x5c, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xf9, 0x15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2c, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Total";
            this.textBox2.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0x137, 0x15);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x92, 30);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextAlign = HorizontalAlignment.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x7e, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Expenses Count";
            this.textBox1.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0x8e, 0x15);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x65, 30);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextAlign = HorizontalAlignment.Right;
            this.Btn_Close.Location = new Point(0x35d, 0x15);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x5c, 30);
            this.Btn_Close.TabIndex = 0;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            this.label4.BackColor = Color.Transparent;
            this.label4.Dock = DockStyle.Fill;
            this.label4.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.ForeColor = Color.White;
            this.label4.Location = new Point(0, 0);
            this.label4.Margin = new Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new Size(930, 0x1a);
            this.label4.TabIndex = 3;
            this.label4.Text = "Current Work Period Expenses -  ";
            this.label4.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3c5, 0x148);
            base.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "TodaysExpense";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Current Work Period Expenses";
            base.Load += new EventHandler(this.TodaysExpense_Load);
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize) this.DataGrid_ExpenseItems).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void TodaysExpense_Load(object sender, EventArgs e)
        {
            decimal num = 0M;
            this.label4.Text = this.label4.Text + Program.CurrentWorkPeriodDate.ToString();
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM expenses where DATE_FORMAT(expensedate, '%Y-%m-%d')=@expensedate", connection);
                command.Parameters.AddWithValue("@expensedate", Program.CurrentWorkPeriodDate);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No expenses for this Period :: " + Program.CurrentWorkPeriodDate.ToString(), "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                        values[0] = reader.GetString("description");
                        values[1] = reader.GetString("unit");
                        values[2] = reader.GetString("qty");
                        values[3] = reader.GetString("price");
                        values[4] = reader.GetString("total");
                        values[5] = reader.GetString("paidto");
                        values[6] = reader.GetString("paymethod");
                        values[7] = reader.GetString("exptype");
                        values[8] = reader.GetDateTime("expensedate").ToShortDateString();
                        this.DataGrid_ExpenseItems.Rows.Add(values);
                        num += reader.GetDecimal("total");
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                num = 0M;
            }
            this.textBox1.Text = this.DataGrid_ExpenseItems.Rows.Count.ToString();
            this.textBox2.Text = num.ToString();
            base.ActiveControl = this.Btn_Close;
        }
    }
}

