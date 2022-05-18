namespace LaxPos.Inventory
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PickSupplier : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private Label label1;
        private Label label25;
        private Label label28;
        private Label label29;
        private GroupBox groupBox2;
        private DataGridView DataGridView_Supplierslist;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private Button Btn_Cancel;
        private Button Btn_Ok;
        public TextBox textBox26;
        public TextBox textBox27;
        public TextBox textBox28;
        public TextBox textBox29;

        public PickSupplier()
        {
            this.InitializeComponent();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

        private void DataGridView_Supplierslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.DataGridView_Supplierslist.SelectedRows.Count != 1)
            {
                MessageBox.Show("Select only one Row!", "Message Box", MessageBoxButtons.OK);
            }
            else
            {
                this.textBox26.Text = this.DataGridView_Supplierslist.SelectedRows[0].Cells[2].Value.ToString();
                this.textBox27.Text = this.DataGridView_Supplierslist.SelectedRows[0].Cells[3].Value.ToString();
                this.textBox28.Text = this.DataGridView_Supplierslist.SelectedRows[0].Cells[1].Value.ToString();
                this.textBox29.Text = this.DataGridView_Supplierslist.SelectedRows[0].Cells[0].Value.ToString();
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

        public void GetSuppliersList()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * From suppliersdetails ";
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("You have registered suppliers!", "Suppliers List", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["SupId"].ToString(), reader["FirstName"].ToString() + " " + reader["LastName"].ToString(), reader["MobileNo"].ToString(), reader["BoxAddress"].ToString() };
                        this.DataGridView_Supplierslist.Rows.Add(values);
                    }
                }
                command.Dispose();
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.groupBox1 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.Btn_Cancel = new Button();
            this.Btn_Ok = new Button();
            this.label1 = new Label();
            this.textBox26 = new TextBox();
            this.textBox27 = new TextBox();
            this.textBox28 = new TextBox();
            this.textBox29 = new TextBox();
            this.label25 = new Label();
            this.label28 = new Label();
            this.label29 = new Label();
            this.groupBox2 = new GroupBox();
            this.DataGridView_Supplierslist = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.DataGridView_Supplierslist).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(430, 0x51);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Box";
            this.groupBox3.Controls.Add(this.Btn_Cancel);
            this.groupBox3.Controls.Add(this.Btn_Ok);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.textBox26);
            this.groupBox3.Controls.Add(this.textBox27);
            this.groupBox3.Controls.Add(this.textBox28);
            this.groupBox3.Controls.Add(this.textBox29);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Dock = DockStyle.Bottom;
            this.groupBox3.Location = new Point(0, 0x156);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(430, 0x77);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Suppliers Details";
            this.Btn_Cancel.DialogResult = DialogResult.Cancel;
            this.Btn_Cancel.Location = new Point(0xed, 0x57);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new Size(0x4b, 0x17);
            this.Btn_Cancel.TabIndex = 0x19;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new EventHandler(this.Btn_Cancel_Click);
            this.Btn_Ok.Location = new Point(0x15a, 0x57);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new Size(0x4b, 0x17);
            this.Btn_Ok.TabIndex = 0x18;
            this.Btn_Ok.Text = "Ok";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new EventHandler(this.Btn_Ok_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x33);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 13);
            this.label1.TabIndex = 0x17;
            this.label1.Text = "Name";
            this.textBox26.BackColor = SystemColors.ButtonHighlight;
            this.textBox26.Location = new Point(0x10a, 0x39);
            this.textBox26.Name = "textBox26";
            this.textBox26.ReadOnly = true;
            this.textBox26.Size = new Size(0x9b, 20);
            this.textBox26.TabIndex = 0x16;
            this.textBox27.BackColor = SystemColors.ButtonHighlight;
            this.textBox27.Location = new Point(0x10a, 0x17);
            this.textBox27.Name = "textBox27";
            this.textBox27.ReadOnly = true;
            this.textBox27.Size = new Size(0x9b, 20);
            this.textBox27.TabIndex = 0x15;
            this.textBox28.BackColor = SystemColors.ButtonHighlight;
            this.textBox28.Location = new Point(0x40, 0x33);
            this.textBox28.Name = "textBox28";
            this.textBox28.ReadOnly = true;
            this.textBox28.Size = new Size(130, 20);
            this.textBox28.TabIndex = 20;
            this.textBox29.BackColor = SystemColors.ButtonHighlight;
            this.textBox29.Location = new Point(0x40, 0x17);
            this.textBox29.Name = "textBox29";
            this.textBox29.ReadOnly = true;
            this.textBox29.Size = new Size(130, 20);
            this.textBox29.TabIndex = 0x13;
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0xca, 0x3b);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x3a, 13);
            this.label25.TabIndex = 0x12;
            this.label25.Text = "Telephone";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(7, 0x17);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x36, 13);
            this.label28.TabIndex = 0x11;
            this.label28.Text = "SupplierId";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(0xca, 0x1a);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x2d, 13);
            this.label29.TabIndex = 0x10;
            this.label29.Text = "Address";
            this.groupBox2.Controls.Add(this.DataGridView_Supplierslist);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(0, 0x51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(430, 0x105);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Suppliers List";
            this.DataGridView_Supplierslist.AllowUserToAddRows = false;
            this.DataGridView_Supplierslist.AllowUserToDeleteRows = false;
            this.DataGridView_Supplierslist.AllowUserToResizeRows = false;
            this.DataGridView_Supplierslist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_Supplierslist.BackgroundColor = SystemColors.ControlLightLight;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = SystemColors.Control;
            style.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.WindowText;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.DataGridView_Supplierslist.ColumnHeadersDefaultCellStyle = style;
            this.DataGridView_Supplierslist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 };
            this.DataGridView_Supplierslist.Columns.AddRange(dataGridViewColumns);
            this.DataGridView_Supplierslist.Dock = DockStyle.Fill;
            this.DataGridView_Supplierslist.Location = new Point(3, 0x10);
            this.DataGridView_Supplierslist.Name = "DataGridView_Supplierslist";
            this.DataGridView_Supplierslist.ReadOnly = true;
            this.DataGridView_Supplierslist.RowHeadersVisible = false;
            this.DataGridView_Supplierslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_Supplierslist.Size = new Size(0x1a8, 0xf2);
            this.DataGridView_Supplierslist.TabIndex = 0;
            this.DataGridView_Supplierslist.CellDoubleClick += new DataGridViewCellEventHandler(this.DataGridView_Supplierslist_CellDoubleClick);
            this.Column1.HeaderText = "SupplierId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.HeaderText = "Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column3.HeaderText = "Phone";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column4.HeaderText = "Address";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.Btn_Cancel;
            base.ClientSize = new Size(430, 0x1cd);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PickSupplier";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "PickSupplier";
            base.TopMost = true;
            base.Load += new EventHandler(this.PickSupplier_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.DataGridView_Supplierslist).EndInit();
            base.ResumeLayout(false);
        }

        private void PickSupplier_Load(object sender, EventArgs e)
        {
            this.GetSuppliersList();
        }
    }
}

