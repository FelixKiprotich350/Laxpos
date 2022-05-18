namespace LaxPos.Expenses
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class NewExpense : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox Btn_ToadysExpenses;
        private Label label11;
        private TextBox textBox3;
        private Label label2;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label3;
        private GroupBox groupBox2;
        private Button Btn_Add;
        private Button Btn_Resetform;
        private Label label8;
        private ComboBox comboBox3;
        private Label label6;
        private ComboBox comboBox2;
        private Label label7;
        private NumericUpDown numericUpDown1;
        private Button Btn_TodaysExpense;
        private Label label5;
        private TextBox textBox4;
        private Button Btn_SaveExpenses;
        private GroupBox groupBox3;
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

        public NewExpense()
        {
            this.InitializeComponent();
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Enter the price!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.textBox2.Focus();
            }
            else if (this.textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Enter the Description!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.textBox3.Focus();
            }
            else if (this.comboBox2.Text.Trim() == "")
            {
                MessageBox.Show("Enter the Unit/Measurement of the expense!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.comboBox2.Focus();
            }
            else if (this.comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("Enter the Expense Type!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.comboBox1.Focus();
            }
            else if (this.comboBox3.Text.Trim() == "")
            {
                MessageBox.Show("Enter the Paymethod Method of the expense!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.comboBox3.Focus();
            }
            else if (this.textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Enter the Expense Type!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.textBox1.Focus();
            }
            else if (this.numericUpDown1.Value <= 0M)
            {
                MessageBox.Show("The Quantity must be greater than Zero!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.numericUpDown1.Focus();
            }
            else
            {
                int num2;
                if (!int.TryParse(this.textBox2.Text, out num2))
                {
                    MessageBox.Show("The price cannot be validated!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.textBox2.Focus();
                }
                else
                {
                    try
                    {
                        int num = Convert.ToInt32(this.textBox2.Text);
                        int num3 = Convert.ToInt32(this.numericUpDown1.Value) * num;
                        object[] values = new object[9];
                        values[0] = this.textBox3.Text;
                        values[1] = this.comboBox2.Text;
                        values[2] = this.numericUpDown1.Value;
                        values[3] = this.textBox2.Text;
                        values[4] = num3;
                        values[5] = this.textBox1.Text;
                        values[6] = this.comboBox3.Text;
                        values[7] = this.comboBox1.Text;
                        values[8] = Program.CurrentDateTime().ToString("yyyy-MM-dd");
                        this.DataGrid_ExpenseItems.Rows.Add(values);
                        this.ResetForm();
                    }
                    catch (Exception exception1)
                    {
                        MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void Btn_Resetform_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard unsaved expenses?", "Message Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.ResetForm();
                this.DataGrid_ExpenseItems.Rows.Clear();
                this.textBox4.Text = "";
            }
        }

        private void Btn_SaveExpenses_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.DataGrid_ExpenseItems.Rows.Count <= 0)
                {
                    MessageBox.Show("You have no expenses to save!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        MySqlCommand command = new MySqlCommand("insert into expenses (expenseid, description, unit, qty, price, total, paidto, paymethod, exptype, expensedate, cashier, counter) values(@expenseid, @description, @unit, @qty, @price, @total, @paidto, @paymethod, @exptype, @expensedate, @cashier, @counter)", connection, transaction);
                        command.Parameters.Add("@expenseid", MySqlDbType.VarChar);
                        command.Parameters.Add("@description", MySqlDbType.VarChar);
                        command.Parameters.Add("@unit", MySqlDbType.VarChar);
                        command.Parameters.Add("@qty", MySqlDbType.Int32);
                        command.Parameters.Add("@price", MySqlDbType.Decimal);
                        command.Parameters.Add("@total", MySqlDbType.Decimal);
                        command.Parameters.Add("@paidto", MySqlDbType.VarChar);
                        command.Parameters.Add("@paymethod", MySqlDbType.VarChar);
                        command.Parameters.Add("@exptype", MySqlDbType.VarChar);
                        command.Parameters.Add("@expensedate", MySqlDbType.DateTime);
                        command.Parameters.Add("@cashier", MySqlDbType.VarChar);
                        command.Parameters.Add("@counter", MySqlDbType.VarChar);
                        int num = 0;
                        foreach (DataGridViewRow row in (IEnumerable) this.DataGrid_ExpenseItems.Rows)
                        {
                            command.Parameters["@expenseid"].Value = Program.CurrentDateTime().ToString("yyyyMMddHHmmffff");
                            command.Parameters["@description"].Value = row.Cells["Column1"].Value.ToString();
                            command.Parameters["@unit"].Value = row.Cells["Column5"].Value.ToString();
                            command.Parameters["@qty"].Value = row.Cells["Column2"].Value.ToString();
                            command.Parameters["@price"].Value = row.Cells["Column3"].Value.ToString();
                            command.Parameters["@total"].Value = row.Cells["Column4"].Value.ToString();
                            command.Parameters["@paidto"].Value = row.Cells["Column6"].Value.ToString();
                            command.Parameters["@paymethod"].Value = row.Cells["Column7"].Value.ToString();
                            command.Parameters["@exptype"].Value = row.Cells["Column8"].Value.ToString();
                            command.Parameters["@expensedate"].Value = row.Cells["Column9"].Value.ToString();
                            command.Parameters["@cashier"].Value = Program.CurrLoggedInUser.UserID;
                            command.Parameters["@counter"].Value = Program.LogInCounter;
                            int num2 = command.ExecuteNonQuery();
                            if (num2 > 0)
                            {
                                num++;
                            }
                        }
                        if (num != this.DataGrid_ExpenseItems.Rows.Count)
                        {
                            MessageBox.Show("Failed to save all expenses!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            transaction.Rollback();
                        }
                        else
                        {
                            transaction.Commit();
                            MessageBox.Show("Expenses saved successfully.", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.ResetForm();
                        }
                    }
                    catch (Exception exception1)
                    {
                        MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        transaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception exception3)
            {
                MessageBox.Show(exception3.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_TodaysExpense_Click(object sender, EventArgs e)
        {
            TodaysExpense expense = new TodaysExpense();
            expense.Text = expense.Text + " - " + Program.CurrentWorkPeriodDate.ToString();
            expense.ShowDialog(this);
        }

        private void DataGrid_ExpenseItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.FindTotal();
        }

        private void DataGrid_ExpenseItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.FindTotal();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FindTotal()
        {
            if (this.DataGrid_ExpenseItems.Rows.Count <= 0)
            {
                this.textBox4.Text = "0";
            }
            else
            {
                decimal num = 0M;
                foreach (DataGridViewRow row in (IEnumerable) this.DataGrid_ExpenseItems.Rows)
                {
                    decimal num2 = decimal.Parse(row.Cells["Column4"].Value.ToString());
                    num += num2;
                }
                this.textBox4.Text = num.ToString();
            }
        }

        private void InitializeComponent()
        {
            this.Btn_ToadysExpenses = new GroupBox();
            this.numericUpDown1 = new NumericUpDown();
            this.label8 = new Label();
            this.comboBox3 = new ComboBox();
            this.label6 = new Label();
            this.comboBox2 = new ComboBox();
            this.label7 = new Label();
            this.Btn_Add = new Button();
            this.comboBox1 = new ComboBox();
            this.label3 = new Label();
            this.label2 = new Label();
            this.textBox2 = new TextBox();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.label11 = new Label();
            this.textBox3 = new TextBox();
            this.groupBox2 = new GroupBox();
            this.Btn_TodaysExpense = new Button();
            this.label5 = new Label();
            this.textBox4 = new TextBox();
            this.Btn_SaveExpenses = new Button();
            this.Btn_Resetform = new Button();
            this.groupBox3 = new GroupBox();
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
            this.Btn_ToadysExpenses.SuspendLayout();
            this.numericUpDown1.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((ISupportInitialize) this.DataGrid_ExpenseItems).BeginInit();
            base.SuspendLayout();
            this.Btn_ToadysExpenses.BackColor = SystemColors.Control;
            this.Btn_ToadysExpenses.Controls.Add(this.numericUpDown1);
            this.Btn_ToadysExpenses.Controls.Add(this.label8);
            this.Btn_ToadysExpenses.Controls.Add(this.comboBox3);
            this.Btn_ToadysExpenses.Controls.Add(this.label6);
            this.Btn_ToadysExpenses.Controls.Add(this.comboBox2);
            this.Btn_ToadysExpenses.Controls.Add(this.label7);
            this.Btn_ToadysExpenses.Controls.Add(this.Btn_Add);
            this.Btn_ToadysExpenses.Controls.Add(this.comboBox1);
            this.Btn_ToadysExpenses.Controls.Add(this.label3);
            this.Btn_ToadysExpenses.Controls.Add(this.label2);
            this.Btn_ToadysExpenses.Controls.Add(this.textBox2);
            this.Btn_ToadysExpenses.Controls.Add(this.label1);
            this.Btn_ToadysExpenses.Controls.Add(this.textBox1);
            this.Btn_ToadysExpenses.Controls.Add(this.label11);
            this.Btn_ToadysExpenses.Controls.Add(this.textBox3);
            this.Btn_ToadysExpenses.Dock = DockStyle.Top;
            this.Btn_ToadysExpenses.Location = new Point(0, 0);
            this.Btn_ToadysExpenses.Name = "Btn_ToadysExpenses";
            this.Btn_ToadysExpenses.Size = new Size(0x42b, 0xb0);
            this.Btn_ToadysExpenses.TabIndex = 0;
            this.Btn_ToadysExpenses.TabStop = false;
            this.Btn_ToadysExpenses.Text = "Expense";
            this.numericUpDown1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.numericUpDown1.Location = new Point(0x25d, 0x29);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(120, 0x1a);
            this.numericUpDown1.TabIndex = 0x35;
            this.numericUpDown1.TextAlign = HorizontalAlignment.Center;
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label8.Location = new Point(0x25c, 0x12);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x3d, 0x11);
            this.label8.TabIndex = 0x34;
            this.label8.Text = "Quantity";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            object[] items = new object[] { "Cash", "Mpesa", "Cards" };
            this.comboBox3.Items.AddRange(items);
            this.comboBox3.Location = new Point(0x357, 0x29);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0x89, 0x1c);
            this.comboBox3.TabIndex = 0x33;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(0x357, 0x12);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x72, 0x11);
            this.label6.TabIndex = 50;
            this.label6.Text = "Payment Method";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.comboBox2.FormattingEnabled = true;
            object[] objArray2 = new object[] { "Pcs", "Packets", "Cartons", "Crates", "Items", "People" };
            this.comboBox2.Items.AddRange(objArray2);
            this.comboBox2.Location = new Point(0x1ca, 0x29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x79, 0x1c);
            this.comboBox2.TabIndex = 0x31;
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(0x1ca, 0x12);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x7b, 0x11);
            this.label7.TabIndex = 0x30;
            this.label7.Text = "Unit/Measurement";
            this.Btn_Add.Location = new Point(0x229, 140);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new Size(0xb6, 30);
            this.Btn_Add.TabIndex = 0x2f;
            this.Btn_Add.Text = "Add Expense";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new EventHandler(this.Btn_Add_Click);
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            object[] objArray3 = new object[] { "LocalExpense", "StockPurchase" };
            this.comboBox1.Items.AddRange(objArray3);
            this.comboBox1.Location = new Point(0x1ca, 0x60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 0x1c);
            this.comboBox1.TabIndex = 0x2c;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(0x1ca, 0x4c);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x62, 0x11);
            this.label3.TabIndex = 0x2b;
            this.label3.Text = "Expense Type";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(740, 0x12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(40, 0x11);
            this.label2.TabIndex = 0x29;
            this.label2.Text = "Price";
            this.textBox2.BackColor = SystemColors.ControlLightLight;
            this.textBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(740, 0x29);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(90, 0x1a);
            this.textBox2.TabIndex = 0x2a;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.textBox2.KeyPress += new KeyPressEventHandler(this.TextBox2_KeyPress);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x25f, 0x4c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x39, 0x11);
            this.label1.TabIndex = 0x27;
            this.label1.Text = "Paid To";
            this.textBox1.BackColor = SystemColors.ControlLightLight;
            this.textBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0x25f, 0x60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x181, 0x1a);
            this.textBox1.TabIndex = 40;
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label11.Location = new Point(12, 0x16);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x4f, 0x11);
            this.label11.TabIndex = 0x25;
            this.label11.Text = "Description";
            this.textBox3.BackColor = SystemColors.ControlLightLight;
            this.textBox3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(12, 0x2a);
            this.textBox3.MaxLength = 0x3e8;
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(420, 0x52);
            this.textBox3.TabIndex = 0x26;
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            this.groupBox2.BackColor = SystemColors.Control;
            this.groupBox2.Controls.Add(this.Btn_TodaysExpense);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.Btn_SaveExpenses);
            this.groupBox2.Controls.Add(this.Btn_Resetform);
            this.groupBox2.Dock = DockStyle.Bottom;
            this.groupBox2.Location = new Point(0, 0x176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x42b, 0x4c);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Expense Summary";
            this.Btn_TodaysExpense.Location = new Point(0x27e, 0x15);
            this.Btn_TodaysExpense.Name = "Btn_TodaysExpense";
            this.Btn_TodaysExpense.Size = new Size(0x88, 30);
            this.Btn_TodaysExpense.TabIndex = 0x36;
            this.Btn_TodaysExpense.Text = "Todays Expenes";
            this.Btn_TodaysExpense.UseVisualStyleBackColor = true;
            this.Btn_TodaysExpense.Click += new EventHandler(this.Btn_TodaysExpense_Click);
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(12, 0x20);
            this.label5.Name = "label5";
            this.label5.Size = new Size(40, 0x11);
            this.label5.TabIndex = 0x34;
            this.label5.Text = "Total";
            this.textBox4.BackColor = SystemColors.ControlLightLight;
            this.textBox4.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(0x41, 0x1d);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0x98, 30);
            this.textBox4.TabIndex = 0x35;
            this.textBox4.TextAlign = HorizontalAlignment.Center;
            this.Btn_SaveExpenses.Location = new Point(0x32f, 0x15);
            this.Btn_SaveExpenses.Name = "Btn_SaveExpenses";
            this.Btn_SaveExpenses.Size = new Size(0x88, 30);
            this.Btn_SaveExpenses.TabIndex = 0x31;
            this.Btn_SaveExpenses.Text = "Save Expenses";
            this.Btn_SaveExpenses.UseVisualStyleBackColor = true;
            this.Btn_SaveExpenses.Click += new EventHandler(this.Btn_SaveExpenses_Click);
            this.Btn_Resetform.Location = new Point(0x1cd, 0x15);
            this.Btn_Resetform.Name = "Btn_Resetform";
            this.Btn_Resetform.Size = new Size(0x88, 30);
            this.Btn_Resetform.TabIndex = 0x30;
            this.Btn_Resetform.Text = "Clear Expenses";
            this.Btn_Resetform.UseVisualStyleBackColor = true;
            this.Btn_Resetform.Click += new EventHandler(this.Btn_Resetform_Click);
            this.groupBox3.BackColor = SystemColors.Control;
            this.groupBox3.Controls.Add(this.DataGrid_ExpenseItems);
            this.groupBox3.Dock = DockStyle.Fill;
            this.groupBox3.Location = new Point(0, 0xb0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x42b, 0xc6);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Expense Items";
            this.DataGrid_ExpenseItems.AllowUserToAddRows = false;
            this.DataGrid_ExpenseItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGrid_ExpenseItems.BackgroundColor = SystemColors.Info;
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
            this.DataGrid_ExpenseItems.Location = new Point(3, 0x16);
            this.DataGrid_ExpenseItems.Name = "DataGrid_ExpenseItems";
            this.DataGrid_ExpenseItems.ReadOnly = true;
            this.DataGrid_ExpenseItems.RowHeadersVisible = false;
            this.DataGrid_ExpenseItems.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.DataGrid_ExpenseItems.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            this.DataGrid_ExpenseItems.Size = new Size(0x425, 0xad);
            this.DataGrid_ExpenseItems.TabIndex = 0;
            this.DataGrid_ExpenseItems.RowsAdded += new DataGridViewRowsAddedEventHandler(this.DataGrid_ExpenseItems_RowsAdded);
            this.DataGrid_ExpenseItems.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.DataGrid_ExpenseItems_RowsRemoved);
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
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x42b, 450);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.Btn_ToadysExpenses);
            this.Cursor = Cursors.Default;
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "NewExpense";
            this.Text = "NewExpense";
            this.Btn_ToadysExpenses.ResumeLayout(false);
            this.Btn_ToadysExpenses.PerformLayout();
            this.numericUpDown1.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((ISupportInitialize) this.DataGrid_ExpenseItems).EndInit();
            base.ResumeLayout(false);
        }

        private void ResetForm()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.comboBox1.SelectedItem = null;
            this.comboBox2.SelectedItem = null;
            this.comboBox3.SelectedItem = null;
            this.numericUpDown1.Value = 0M;
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(((e.KeyChar.ToString() == ".") || char.IsControl(e.KeyChar)) || char.IsNumber(e.KeyChar));
        }
    }
}

