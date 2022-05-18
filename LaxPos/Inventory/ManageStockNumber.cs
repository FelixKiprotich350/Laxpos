namespace LaxPos.Inventory
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ManageStockNumber : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Button Btn_GetAllStts;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private GroupBox groupBox2;
        private Button Btn_GetRecentStts;
        private TextBox textBox1;
        private Label label1;
        private Button Btn_CreateSTT;
        private TextBox textBox2;
        private Label label2;
        public TextBox textBox3;
        private Label label3;
        private Button Btn_Close;

        public ManageStockNumber()
        {
            this.InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Btn_CreateSTT_Click(object sender, EventArgs e)
        {
            try
            {
                this.GenerateSTT();
                this.InsertSTT();
                this.GenerateSTT();
                this.Btn_GetAllStts_Click(new object(), new EventArgs());
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_GetAllStts_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.Rows.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlDataReader reader = new MySqlCommand("SELECT * from stocktakingmaster", connection).ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Stock Taking Numbers Items Found", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["STTNo"].ToString(), reader["ItemsCount"].ToString(), reader.GetDateTime("CreationDate").ToShortDateString() };
                        this.dataGridView1.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void Btn_GetRecentStts_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GenerateSTT()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlDataReader reader = new MySqlCommand("SELECT max(Sno) as Sno from stocktakingmaster", connection).ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("You are generating the first Stock Taking Number", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.textBox1.Text = 10.ToString();
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox1.Text = (int.Parse(reader["Sno"].ToString()) + 1).ToString();
                    }
                }
                reader.Close();
                reader.Dispose();
                reader = new MySqlCommand("SELECT count(*) as Count FROM inventorymaster", connection).ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Count returned", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.textBox2.Text = "0";
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox2.Text = reader["Count"].ToString();
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.dataGridView1 = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Btn_GetAllStts = new Button();
            this.groupBox2 = new GroupBox();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.Btn_CreateSTT = new Button();
            this.Btn_GetRecentStts = new Button();
            this.Btn_Close = new Button();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = DockStyle.Left;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Margin = new Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(4);
            this.groupBox1.Size = new Size(0x15b, 0x1cf);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stock Taking Numbers";
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = SystemColors.Info;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3 };
            this.dataGridView1.Columns.AddRange(dataGridViewColumns);
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(4, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new Size(0x153, 0x1b7);
            this.dataGridView1.TabIndex = 1;
            this.Column1.HeaderText = "STT No";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "ItemsCount";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            this.Column3.HeaderText = "DateCreated";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Btn_GetAllStts.Location = new Point(0x187, 0x26);
            this.Btn_GetAllStts.Name = "Btn_GetAllStts";
            this.Btn_GetAllStts.Size = new Size(0x8e, 0x20);
            this.Btn_GetAllStts.TabIndex = 1;
            this.Btn_GetAllStts.Text = "Get All ";
            this.Btn_GetAllStts.UseVisualStyleBackColor = true;
            this.Btn_GetAllStts.Click += new EventHandler(this.Btn_GetAllStts_Click);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Btn_CreateSTT);
            this.groupBox2.Location = new Point(0x166, 0x5c);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x181, 0xd4);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create New STT Number";
            this.textBox3.Location = new Point(0x67, 0x54);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0x10d, 0x3f);
            this.textBox3.TabIndex = 8;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x11, 0x57);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x40, 0x11);
            this.label3.TabIndex = 7;
            this.label3.Text = "Remarks";
            this.textBox2.BackColor = Color.LightYellow;
            this.textBox2.Location = new Point(0x67, 0x37);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x10d, 0x17);
            this.textBox2.TabIndex = 6;
            this.textBox2.Text = "0";
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x11, 0x3a);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "Total Items";
            this.textBox1.BackColor = Color.LightYellow;
            this.textBox1.Location = new Point(0x67, 0x1a);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x10d, 0x17);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "100";
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x11, 0x1d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x40, 0x11);
            this.label1.TabIndex = 3;
            this.label1.Text = "STT NO:";
            this.Btn_CreateSTT.Location = new Point(0x7d, 0xa3);
            this.Btn_CreateSTT.Name = "Btn_CreateSTT";
            this.Btn_CreateSTT.Size = new Size(0xe9, 0x20);
            this.Btn_CreateSTT.TabIndex = 2;
            this.Btn_CreateSTT.Text = "Save STT Number";
            this.Btn_CreateSTT.UseVisualStyleBackColor = true;
            this.Btn_CreateSTT.Click += new EventHandler(this.Btn_CreateSTT_Click);
            this.Btn_GetRecentStts.Location = new Point(0x22b, 0x26);
            this.Btn_GetRecentStts.Name = "Btn_GetRecentStts";
            this.Btn_GetRecentStts.Size = new Size(0x8e, 0x20);
            this.Btn_GetRecentStts.TabIndex = 3;
            this.Btn_GetRecentStts.Text = "Get Recent STTs";
            this.Btn_GetRecentStts.UseVisualStyleBackColor = true;
            this.Btn_GetRecentStts.Click += new EventHandler(this.Btn_GetRecentStts_Click);
            this.Btn_Close.Location = new Point(520, 0x148);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x9c, 0x20);
            this.Btn_Close.TabIndex = 4;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2f7, 0x1cf);
            base.Controls.Add(this.Btn_Close);
            base.Controls.Add(this.Btn_GetRecentStts);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.Btn_GetAllStts);
            base.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Margin = new Padding(4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ManageStockNumber";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ManageStockNumber";
            base.Load += new EventHandler(this.ManageStockNumber_Load);
            this.groupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        public void InsertSTT()
        {
            List<SttItem> list = new List<SttItem>();
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand command = new MySqlCommand("INSERT INTO stocktakingmaster (STTNo, Auditor, CreationDate, ItemsCount, FirstEdit, LastEdit, Remarks) VALUES(@stt,@auditor,@creationdate,@itemscount,@firstedit,@lastedit,@remarks)", connection, transaction);
                command.Parameters.AddWithValue("@stt", this.textBox1.Text);
                command.Parameters.AddWithValue("@auditor", Program.CurrLoggedInUser.UserID);
                command.Parameters.AddWithValue("@creationdate", Program.CurrentDateTime());
                command.Parameters.AddWithValue("@itemscount", this.textBox2.Text);
                command.Parameters.AddWithValue("@firstedit", Program.CurrentDateTime());
                command.Parameters.AddWithValue("@lastedit", Program.CurrentDateTime());
                command.Parameters.AddWithValue("@remarks", this.textBox3.Text);
                if (command.ExecuteNonQuery() <= 0)
                {
                    transaction.Rollback();
                    MessageBox.Show("Failed to create the Stock Taking Number");
                }
                else
                {
                    int num2 = 0;
                    MySqlDataReader reader = new MySqlCommand("select * from inventorymaster", connection, transaction).ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                break;
                            }
                            SttItem item1 = new SttItem();
                            item1.SttNumber = this.textBox1.Text;
                            item1.Itemcode = reader["ProductCode"].ToString();
                            item1.ProductName = reader["Description"].ToString();
                            item1.Expected = int.Parse(reader["StockBalance"].ToString());
                            item1.Counted = 0;
                            item1.CountStatus = "Uncounted";
                            item1.DateCounted = Program.CurrentDateTime();
                            list.Add(item1);
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    MySqlCommand command2 = new MySqlCommand("Insert into stocktakingitems (STT, ItemCode, ItemName, Expected, Counted, CountStatus, DateCounted) values(@STT, @ItemCode, @ItemName, @Expected, @Counted, @CountStatus, @DateCounted)", connection, transaction);
                    command2.Parameters.Add("@STT", MySqlDbType.VarChar);
                    command2.Parameters.Add("@ItemCode", MySqlDbType.VarChar);
                    command2.Parameters.Add("@ItemName", MySqlDbType.VarChar);
                    command2.Parameters.Add("@Expected", MySqlDbType.Int32);
                    command2.Parameters.Add("@Counted", MySqlDbType.Int32);
                    command2.Parameters.Add("@CountStatus", MySqlDbType.VarChar);
                    command2.Parameters.Add("@DateCounted", MySqlDbType.DateTime);
                    foreach (SttItem item in list)
                    {
                        command2.Parameters["@STT"].Value = item.SttNumber;
                        command2.Parameters["@ItemCode"].Value = item.Itemcode;
                        command2.Parameters["@ItemName"].Value = item.ProductName;
                        command2.Parameters["@Expected"].Value = item.Expected;
                        command2.Parameters["@Counted"].Value = item.Counted;
                        command2.Parameters["@CountStatus"].Value = item.CountStatus;
                        command2.Parameters["@DateCounted"].Value = item.DateCounted;
                        int num3 = command2.ExecuteNonQuery();
                        if (num3 > 0)
                        {
                            num2++;
                        }
                    }
                    if (num2 >= int.Parse(this.textBox2.Text))
                    {
                        transaction.Commit();
                        MessageBox.Show("Stock Taking Order Successfully Created", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        transaction.Rollback();
                        MessageBox.Show("Failed to create the Stock Taking Order", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void ManageStockNumber_Load(object sender, EventArgs e)
        {
            this.GenerateSTT();
        }
    }
}

