namespace LaxPos.Inventory
{
    using Bunifu.Framework.UI;
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Suppliers : BunifuForm
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private TabPage Tab_PageSuppliersAccount;
        private TabPage Tab_PageViewSuppliers;
        private DataGridView Gridview_SuppliersDetails;
        private Panel panel2;
        private GroupBox ViewSuppliers_SearchGroupBox;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private Label label1;
        private TabControl Tab_Suppliers;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private TabPage TabPage_SupplyLogs;
        private GroupBox groupBox13;
        private DataGridView GridView_SupplyHistory;
        private Panel TabPage_Suppliers_FilterBox;
        private Panel panel3;
        private Button Btn_SuppliersList_Refresh;
        private Panel Panel_Search;
        private Label label11;
        private Button Btn_SuppliersAccount_FindSupplier;
        private TextBox textBox10;
        private Panel Panel_HoldSuppliersDetails;
        private Panel panel5;
        private Button Btn_SearchNewSupplier;
        private Button Btn_SuppliersAccount_Delete;
        private Button Btn_SuppliersAccount_Refresh;
        private Button Btn_SuppliersAccount_Update;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private GroupBox groupBox10;
        private Button Btn_SearchLogs_SupplierID;
        private TextBox textBox13;
        private GroupBox groupBox11;
        private Button Btn_SearchLogs_OrderID;
        private TextBox textBox16;
        private GroupBox groupBox12;
        private Button Btn_SearchLogs_Pcode;
        private TextBox textBox27;
        private GroupBox groupBox14;
        private Label label23;
        private Label label16;
        private Button Btn_SearchLogs_Date;
        private DateTimePicker dateTimePicker4;
        private DateTimePicker dateTimePicker3;
        private Button Btn_SupplyLogs_ClearFilters;
        private Panel panel1;
        private GroupBox groupBox5;
        private TextBox textBox21;
        private Label label25;
        private TextBox textBox14;
        private Label label17;
        private TextBox textBox17;
        private Label label20;
        private TextBox textBox19;
        private Label label22;
        private GroupBox groupBox6;
        private TextBox textBox15;
        private Label label32;
        private TextBox textBox12;
        private Label label45;
        private Label label19;
        private DateTimePicker dateTimePicker2;
        private GroupBox groupBox7;
        private RadioButton radioButton7;
        private RadioButton radioButton8;
        private Label label31;
        private TextBox textBox20;
        private Label label24;
        private TextBox textBox22;
        private Label label26;
        private TextBox textBox23;
        private Label label27;
        private TextBox textBox24;
        private TextBox textBox25;
        private TextBox textBox26;
        private Label label28;
        private Label label29;
        private Label label30;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column13;

        public Suppliers()
        {
            this.InitializeComponent();
        }

        private void Btn_SearchLogs_OrderID_Click(object sender, EventArgs e)
        {
            if (this.textBox16.Text == "")
            {
                MessageBox.Show("The OrderID is Required !", "Empty Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.GetSupplyHistoryLogs(" WHERE OrderId=@orderid;");
                this.ToggleSearchParameters(this.textBox16);
            }
        }

        private void Btn_SearchLogs_Pcode_Click(object sender, EventArgs e)
        {
            if (this.textBox27.Text == "")
            {
                MessageBox.Show("The ProductCode is Required !", "Empty Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.GetSupplyHistoryLogs(" WHERE PCode=@pcode;");
                this.ToggleSearchParameters(this.textBox27);
            }
        }

        private void Btn_SearchLogs_SupplierID_Click(object sender, EventArgs e)
        {
            if (this.textBox13.Text == "")
            {
                MessageBox.Show("The Supplier ID is Required !", "Empty Parameter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.GetSupplyHistoryLogs(" WHERE SupplierId=@SuppId;");
                this.ToggleSearchParameters(this.textBox13);
            }
        }

        private void Btn_SearchNewSupplier_Click(object sender, EventArgs e)
        {
            this.Panel_Search.Visible = true;
            this.Panel_HoldSuppliersDetails.Visible = false;
            this.ClearSupplierDetails();
            this.textBox10.Text = "";
        }

        private void Btn_SuppliersAccount_Delete_Click(object sender, EventArgs e)
        {
        }

        private void Btn_SuppliersAccount_FindSupplier_Click(object sender, EventArgs e)
        {
            if (this.textBox10.Text == "")
            {
                MessageBox.Show("Cannot Search Null Id !!", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string id = this.textBox10.Text.ToString();
                if (this.FindSupplier(id))
                {
                    this.Panel_Search.Visible = false;
                    this.Panel_HoldSuppliersDetails.Visible = true;
                }
                else
                {
                    this.Panel_Search.Visible = true;
                    this.Panel_HoldSuppliersDetails.Visible = false;
                }
            }
        }

        private void Btn_SuppliersAccount_Refresh_Click(object sender, EventArgs e)
        {
            this.FindSupplier(this.textBox15.Text.ToString());
        }

        private void Btn_SuppliersAccount_Update_Click(object sender, EventArgs e)
        {
        }

        private void Btn_SuppliersList_Refresh_Click(object sender, EventArgs e)
        {
            this.GetSuppliersList();
        }

        private void Btn_SupplyLogs_ClearFilters_Click(object sender, EventArgs e)
        {
            this.textBox13.Text = "";
            this.textBox16.Text = "";
            this.textBox27.Text = "";
            this.dateTimePicker3.Value = Program.CurrentDateTime();
            this.dateTimePicker4.Value = Program.CurrentDateTime();
            this.GetSupplyHistoryLogs(";");
        }

        public void ClearSupplierDetails()
        {
            this.textBox15.Text = "";
            this.textBox20.Text = "";
            this.textBox26.Text = "";
            this.textBox25.Text = "";
            this.textBox24.Text = "";
            this.textBox22.Text = "";
            this.textBox23.Text = "";
            this.textBox19.Text = "";
            this.textBox17.Text = "";
            this.textBox14.Text = "";
            this.textBox21.Text = "";
            this.textBox12.Text = "";
            this.dateTimePicker2.Value = Program.CurrentDateTime();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool FindSupplier(string Id)
        {
            bool flag3;
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from suppliersdetails where SupId=@Id;";
                command.Parameters.AddWithValue("@Id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The supplieId Provided does not exist !!", "Results Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    flag3 = false;
                }
                else
                {
                    this.ClearSupplierDetails();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            connection.Close();
                            flag3 = true;
                            break;
                        }
                        this.textBox15.Text = reader[1].ToString();
                        this.textBox20.Text = reader[2].ToString();
                        this.textBox26.Text = reader[3].ToString();
                        this.textBox25.Text = reader[4].ToString();
                        this.textBox24.Text = reader[5].ToString();
                        this.textBox22.Text = reader[7].ToString();
                        this.textBox23.Text = reader[8].ToString();
                        this.textBox19.Text = reader[9].ToString();
                        this.textBox17.Text = reader[10].ToString();
                        this.textBox14.Text = reader[11].ToString();
                        this.textBox21.Text = reader[12].ToString();
                        this.textBox12.Text = reader[13].ToString();
                        this.dateTimePicker2.Value = DateTime.Parse(reader[14].ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception.HelpLink, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag3 = false;
            }
            return flag3;
        }

        public void GetSuppliersList()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from suppliersdetails;";
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No list of suppliers have been found !!", "Suppliers Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.Gridview_SuppliersDetails.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[8].ToString() };
                        this.Gridview_SuppliersDetails.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void GetSupplyHistoryLogs(string SearchParameter)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from supplierdeliveries" + SearchParameter;
                command.Parameters.AddWithValue("@SuppId", this.textBox13.Text.ToString());
                command.Parameters.AddWithValue("@orderid", this.textBox16.Text.ToString());
                command.Parameters.AddWithValue("@pcode", this.textBox27.Text.ToString());
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Supply History has been found !!", "Supplies Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.GridView_SupplyHistory.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[10];
                        values[0] = reader[0].ToString();
                        values[1] = reader[1].ToString();
                        values[2] = reader[2].ToString();
                        values[3] = reader[3].ToString();
                        values[4] = reader[5].ToString();
                        values[5] = reader[6].ToString();
                        values[6] = reader[7].ToString();
                        values[7] = reader[8].ToString();
                        values[8] = reader[9].ToString();
                        values[9] = Convert.ToDateTime(reader[14].ToString()).ToShortDateString();
                        this.GridView_SupplyHistory.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            this.Tab_PageSuppliersAccount = new TabPage();
            this.Panel_HoldSuppliersDetails = new Panel();
            this.dataGridView1 = new DataGridView();
            this.Column19 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.panel5 = new Panel();
            this.Btn_SearchNewSupplier = new Button();
            this.Btn_SuppliersAccount_Delete = new Button();
            this.Btn_SuppliersAccount_Update = new Button();
            this.panel1 = new Panel();
            this.groupBox5 = new GroupBox();
            this.textBox21 = new TextBox();
            this.label25 = new Label();
            this.Btn_SuppliersAccount_Refresh = new Button();
            this.textBox14 = new TextBox();
            this.label17 = new Label();
            this.textBox17 = new TextBox();
            this.label20 = new Label();
            this.textBox19 = new TextBox();
            this.label22 = new Label();
            this.groupBox6 = new GroupBox();
            this.textBox15 = new TextBox();
            this.label32 = new Label();
            this.textBox12 = new TextBox();
            this.label45 = new Label();
            this.label19 = new Label();
            this.dateTimePicker2 = new DateTimePicker();
            this.groupBox7 = new GroupBox();
            this.radioButton7 = new RadioButton();
            this.radioButton8 = new RadioButton();
            this.label31 = new Label();
            this.textBox20 = new TextBox();
            this.label24 = new Label();
            this.textBox22 = new TextBox();
            this.label26 = new Label();
            this.textBox23 = new TextBox();
            this.label27 = new Label();
            this.textBox24 = new TextBox();
            this.textBox25 = new TextBox();
            this.textBox26 = new TextBox();
            this.label28 = new Label();
            this.label29 = new Label();
            this.label30 = new Label();
            this.Panel_Search = new Panel();
            this.label11 = new Label();
            this.Btn_SuppliersAccount_FindSupplier = new Button();
            this.textBox10 = new TextBox();
            this.Tab_PageViewSuppliers = new TabPage();
            this.Gridview_SuppliersDetails = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.panel2 = new Panel();
            this.panel3 = new Panel();
            this.Btn_SuppliersList_Refresh = new Button();
            this.ViewSuppliers_SearchGroupBox = new GroupBox();
            this.radioButton6 = new RadioButton();
            this.radioButton5 = new RadioButton();
            this.radioButton4 = new RadioButton();
            this.radioButton3 = new RadioButton();
            this.label1 = new Label();
            this.Tab_Suppliers = new TabControl();
            this.TabPage_SupplyLogs = new TabPage();
            this.groupBox13 = new GroupBox();
            this.GridView_SupplyHistory = new DataGridView();
            this.Column17 = new DataGridViewTextBoxColumn();
            this.Column14 = new DataGridViewTextBoxColumn();
            this.Column15 = new DataGridViewTextBoxColumn();
            this.Column16 = new DataGridViewTextBoxColumn();
            this.Column18 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.TabPage_Suppliers_FilterBox = new Panel();
            this.Btn_SupplyLogs_ClearFilters = new Button();
            this.groupBox14 = new GroupBox();
            this.dateTimePicker4 = new DateTimePicker();
            this.dateTimePicker3 = new DateTimePicker();
            this.label23 = new Label();
            this.label16 = new Label();
            this.Btn_SearchLogs_Date = new Button();
            this.groupBox12 = new GroupBox();
            this.Btn_SearchLogs_Pcode = new Button();
            this.textBox27 = new TextBox();
            this.groupBox11 = new GroupBox();
            this.Btn_SearchLogs_OrderID = new Button();
            this.textBox16 = new TextBox();
            this.groupBox10 = new GroupBox();
            this.Btn_SearchLogs_SupplierID = new Button();
            this.textBox13 = new TextBox();
            this.Tab_PageSuppliersAccount.SuspendLayout();
            this.Panel_HoldSuppliersDetails.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.Panel_Search.SuspendLayout();
            this.Tab_PageViewSuppliers.SuspendLayout();
            ((ISupportInitialize) this.Gridview_SuppliersDetails).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ViewSuppliers_SearchGroupBox.SuspendLayout();
            this.Tab_Suppliers.SuspendLayout();
            this.TabPage_SupplyLogs.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((ISupportInitialize) this.GridView_SupplyHistory).BeginInit();
            this.TabPage_Suppliers_FilterBox.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            base.SuspendLayout();
            this.Tab_PageSuppliersAccount.BackColor = SystemColors.ButtonHighlight;
            this.Tab_PageSuppliersAccount.Controls.Add(this.Panel_HoldSuppliersDetails);
            this.Tab_PageSuppliersAccount.Controls.Add(this.panel1);
            this.Tab_PageSuppliersAccount.Controls.Add(this.Panel_Search);
            this.Tab_PageSuppliersAccount.Location = new Point(4, 0x19);
            this.Tab_PageSuppliersAccount.Name = "Tab_PageSuppliersAccount";
            this.Tab_PageSuppliersAccount.Padding = new Padding(3);
            this.Tab_PageSuppliersAccount.Size = new Size(0x4a8, 0x24f);
            this.Tab_PageSuppliersAccount.TabIndex = 2;
            this.Tab_PageSuppliersAccount.Text = "Suppliers Account";
            this.Panel_HoldSuppliersDetails.Controls.Add(this.dataGridView1);
            this.Panel_HoldSuppliersDetails.Controls.Add(this.panel5);
            this.Panel_HoldSuppliersDetails.Dock = DockStyle.Fill;
            this.Panel_HoldSuppliersDetails.Location = new Point(3, 0x10d);
            this.Panel_HoldSuppliersDetails.Name = "Panel_HoldSuppliersDetails";
            this.Panel_HoldSuppliersDetails.Size = new Size(0x4a2, 0x13f);
            this.Panel_HoldSuppliersDetails.TabIndex = 10;
            this.dataGridView1.BackgroundColor = SystemColors.Control;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column19, this.Column13 };
            this.dataGridView1.Columns.AddRange(dataGridViewColumns);
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new Size(0x39c, 0x13f);
            this.dataGridView1.TabIndex = 0;
            this.Column19.HeaderText = "orderid";
            this.Column19.Name = "Column19";
            this.Column13.HeaderText = "invoiceno";
            this.Column13.Name = "Column13";
            this.panel5.Controls.Add(this.Btn_SearchNewSupplier);
            this.panel5.Controls.Add(this.Btn_SuppliersAccount_Delete);
            this.panel5.Controls.Add(this.Btn_SuppliersAccount_Update);
            this.panel5.Dock = DockStyle.Right;
            this.panel5.Location = new Point(0x39c, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x106, 0x13f);
            this.panel5.TabIndex = 0x25;
            this.Btn_SearchNewSupplier.Location = new Point(0x3b, 0x52);
            this.Btn_SearchNewSupplier.Name = "Btn_SearchNewSupplier";
            this.Btn_SearchNewSupplier.Size = new Size(170, 0x26);
            this.Btn_SearchNewSupplier.TabIndex = 0x22;
            this.Btn_SearchNewSupplier.Text = "Search New Supplier";
            this.Btn_SearchNewSupplier.UseVisualStyleBackColor = true;
            this.Btn_SearchNewSupplier.Click += new EventHandler(this.Btn_SearchNewSupplier_Click);
            this.Btn_SuppliersAccount_Delete.Location = new Point(0x3b, 0x8e);
            this.Btn_SuppliersAccount_Delete.Name = "Btn_SuppliersAccount_Delete";
            this.Btn_SuppliersAccount_Delete.Size = new Size(170, 0x26);
            this.Btn_SuppliersAccount_Delete.TabIndex = 0x20;
            this.Btn_SuppliersAccount_Delete.Text = "Delete";
            this.Btn_SuppliersAccount_Delete.UseVisualStyleBackColor = true;
            this.Btn_SuppliersAccount_Delete.Click += new EventHandler(this.Btn_SuppliersAccount_Delete_Click);
            this.Btn_SuppliersAccount_Update.Location = new Point(0x3b, 0x17);
            this.Btn_SuppliersAccount_Update.Name = "Btn_SuppliersAccount_Update";
            this.Btn_SuppliersAccount_Update.Size = new Size(170, 0x26);
            this.Btn_SuppliersAccount_Update.TabIndex = 30;
            this.Btn_SuppliersAccount_Update.Text = "Update";
            this.Btn_SuppliersAccount_Update.UseVisualStyleBackColor = true;
            this.Btn_SuppliersAccount_Update.Click += new EventHandler(this.Btn_SuppliersAccount_Update_Click);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(3, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x4a2, 0xd1);
            this.panel1.TabIndex = 11;
            this.groupBox5.Controls.Add(this.textBox21);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.Btn_SuppliersAccount_Refresh);
            this.groupBox5.Controls.Add(this.textBox14);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.textBox17);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.textBox19);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Dock = DockStyle.Bottom;
            this.groupBox5.Location = new Point(0, 0x87);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x4a2, 0x4a);
            this.groupBox5.TabIndex = 0x25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Locational Details";
            this.textBox21.Location = new Point(0x2a9, 0x2d);
            this.textBox21.MaxLength = 200;
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Size(0xc7, 0x17);
            this.textBox21.TabIndex = 0x1b;
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0x2aa, 0x16);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x3b, 0x11);
            this.label25.TabIndex = 0x1a;
            this.label25.Text = "P.O Box";
            this.Btn_SuppliersAccount_Refresh.Location = new Point(0x3c2, 0x19);
            this.Btn_SuppliersAccount_Refresh.Name = "Btn_SuppliersAccount_Refresh";
            this.Btn_SuppliersAccount_Refresh.Size = new Size(170, 0x26);
            this.Btn_SuppliersAccount_Refresh.TabIndex = 0x1f;
            this.Btn_SuppliersAccount_Refresh.Text = "Refresh";
            this.Btn_SuppliersAccount_Refresh.UseVisualStyleBackColor = true;
            this.Btn_SuppliersAccount_Refresh.Click += new EventHandler(this.Btn_SuppliersAccount_Refresh_Click);
            this.textBox14.Location = new Point(0x1bb, 0x2f);
            this.textBox14.MaxLength = 200;
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Size(0xc7, 0x17);
            this.textBox14.TabIndex = 0x17;
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x1bb, 0x16);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x4b, 0x11);
            this.label17.TabIndex = 0x16;
            this.label17.Text = "Subcounty";
            this.textBox17.Location = new Point(0xdd, 0x2d);
            this.textBox17.MaxLength = 200;
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Size(0xc7, 0x17);
            this.textBox17.TabIndex = 0x15;
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0xdd, 20);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x34, 0x11);
            this.label20.TabIndex = 20;
            this.label20.Text = "County";
            this.textBox19.Location = new Point(11, 0x2d);
            this.textBox19.MaxLength = 200;
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Size(0xc7, 0x17);
            this.textBox19.TabIndex = 0x13;
            this.label22.AutoSize = true;
            this.label22.Location = new Point(11, 0x16);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x39, 0x11);
            this.label22.TabIndex = 0x12;
            this.label22.Text = "Country";
            this.groupBox6.Controls.Add(this.textBox15);
            this.groupBox6.Controls.Add(this.label32);
            this.groupBox6.Controls.Add(this.textBox12);
            this.groupBox6.Controls.Add(this.label45);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.dateTimePicker2);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.label31);
            this.groupBox6.Controls.Add(this.textBox20);
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Controls.Add(this.textBox22);
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Controls.Add(this.textBox23);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.textBox24);
            this.groupBox6.Controls.Add(this.textBox25);
            this.groupBox6.Controls.Add(this.textBox26);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Controls.Add(this.label30);
            this.groupBox6.Dock = DockStyle.Top;
            this.groupBox6.Location = new Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x4a2, 0x88);
            this.groupBox6.TabIndex = 0x24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Personal Information";
            this.textBox15.Location = new Point(0x66, 0x17);
            this.textBox15.MaxLength = 200;
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new Size(0xc7, 0x17);
            this.textBox15.TabIndex = 0x21;
            this.label32.AutoSize = true;
            this.label32.Location = new Point(10, 0x17);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x4d, 0x11);
            this.label32.TabIndex = 0x20;
            this.label32.Text = "Supplier ID";
            this.textBox12.Location = new Point(0x336, 0x61);
            this.textBox12.MaxLength = 200;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Size(0x9c, 0x17);
            this.textBox12.TabIndex = 0x1f;
            this.label45.AutoSize = true;
            this.label45.Location = new Point(0x157, 0x67);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0x38, 0x11);
            this.label45.TabIndex = 0x1d;
            this.label45.Text = "Gender";
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x2af, 100);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x72, 0x11);
            this.label19.TabIndex = 30;
            this.label19.Text = "Supplier's Status";
            this.dateTimePicker2.Format = DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new Point(0x3ea, 0x33);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new Size(0x97, 0x17);
            this.dateTimePicker2.TabIndex = 0x18;
            this.groupBox7.Controls.Add(this.radioButton7);
            this.groupBox7.Controls.Add(this.radioButton8);
            this.groupBox7.Location = new Point(0x1bf, 0x57);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0xca, 0x2b);
            this.groupBox7.TabIndex = 0x1c;
            this.groupBox7.TabStop = false;
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new Point(0x58, 14);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new Size(0x48, 0x15);
            this.radioButton7.TabIndex = 20;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "Female";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new Point(10, 14);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new Size(0x38, 0x15);
            this.radioButton8.TabIndex = 0x13;
            this.radioButton8.TabStop = true;
            this.radioButton8.Text = "Male";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x3e6, 0x11);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x76, 0x11);
            this.label31.TabIndex = 0x17;
            this.label31.Text = "Registration Date";
            this.textBox20.Location = new Point(0x66, 0x40);
            this.textBox20.MaxLength = 200;
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Size(0xc7, 0x17);
            this.textBox20.TabIndex = 0x18;
            this.label24.AutoSize = true;
            this.label24.Location = new Point(10, 60);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x4b, 0x11);
            this.label24.TabIndex = 0x17;
            this.label24.Text = "ID Number";
            this.textBox22.Location = new Point(0x30b, 0x39);
            this.textBox22.MaxLength = 200;
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Size(0xc7, 0x17);
            this.textBox22.TabIndex = 9;
            this.label26.AutoSize = true;
            this.label26.Location = new Point(0x2aa, 0x3f);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x4c, 0x11);
            this.label26.TabIndex = 8;
            this.label26.Text = "Telephone";
            this.textBox23.Location = new Point(0x30b, 0x11);
            this.textBox23.MaxLength = 200;
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Size(0xc7, 0x17);
            this.textBox23.TabIndex = 7;
            this.label27.AutoSize = true;
            this.label27.Location = new Point(0x2aa, 20);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x2a, 0x11);
            this.label27.TabIndex = 6;
            this.label27.Text = "Email";
            this.textBox24.Location = new Point(450, 0x39);
            this.textBox24.MaxLength = 200;
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Size(0xc7, 0x17);
            this.textBox24.TabIndex = 5;
            this.textBox25.Location = new Point(450, 0x11);
            this.textBox25.MaxLength = 200;
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Size(0xc7, 0x17);
            this.textBox25.TabIndex = 4;
            this.textBox26.Location = new Point(0x66, 0x69);
            this.textBox26.MaxLength = 200;
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Size(0xc7, 0x17);
            this.textBox26.TabIndex = 3;
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0x157, 0x39);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x4c, 0x11);
            this.label28.TabIndex = 2;
            this.label28.Text = "Last Name";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(0x157, 0x11);
            this.label29.Name = "label29";
            this.label29.Size = new Size(90, 0x11);
            this.label29.TabIndex = 1;
            this.label29.Text = "Middle Name";
            this.label30.AutoSize = true;
            this.label30.Location = new Point(10, 0x69);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x4c, 0x11);
            this.label30.TabIndex = 0;
            this.label30.Text = "First Name";
            this.Panel_Search.Controls.Add(this.label11);
            this.Panel_Search.Controls.Add(this.Btn_SuppliersAccount_FindSupplier);
            this.Panel_Search.Controls.Add(this.textBox10);
            this.Panel_Search.Dock = DockStyle.Top;
            this.Panel_Search.Location = new Point(3, 3);
            this.Panel_Search.Name = "Panel_Search";
            this.Panel_Search.Size = new Size(0x4a2, 0x39);
            this.Panel_Search.TabIndex = 9;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(12, 0x11);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x4d, 0x11);
            this.label11.TabIndex = 0;
            this.label11.Text = "Supplier ID";
            this.Btn_SuppliersAccount_FindSupplier.Location = new Point(0x16d, 14);
            this.Btn_SuppliersAccount_FindSupplier.Name = "Btn_SuppliersAccount_FindSupplier";
            this.Btn_SuppliersAccount_FindSupplier.Size = new Size(0x88, 0x1b);
            this.Btn_SuppliersAccount_FindSupplier.TabIndex = 2;
            this.Btn_SuppliersAccount_FindSupplier.Text = "Find supplier";
            this.Btn_SuppliersAccount_FindSupplier.UseVisualStyleBackColor = true;
            this.Btn_SuppliersAccount_FindSupplier.Click += new EventHandler(this.Btn_SuppliersAccount_FindSupplier_Click);
            this.textBox10.Location = new Point(0x7f, 0x11);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Size(0xcf, 0x17);
            this.textBox10.TabIndex = 1;
            this.Tab_PageViewSuppliers.Controls.Add(this.Gridview_SuppliersDetails);
            this.Tab_PageViewSuppliers.Controls.Add(this.panel2);
            this.Tab_PageViewSuppliers.Location = new Point(4, 0x19);
            this.Tab_PageViewSuppliers.Margin = new Padding(4);
            this.Tab_PageViewSuppliers.Name = "Tab_PageViewSuppliers";
            this.Tab_PageViewSuppliers.Padding = new Padding(4);
            this.Tab_PageViewSuppliers.Size = new Size(0x4a8, 0x24f);
            this.Tab_PageViewSuppliers.TabIndex = 0;
            this.Tab_PageViewSuppliers.Text = "View Suppliers";
            this.Tab_PageViewSuppliers.UseVisualStyleBackColor = true;
            this.Gridview_SuppliersDetails.AllowUserToAddRows = false;
            this.Gridview_SuppliersDetails.AllowUserToDeleteRows = false;
            this.Gridview_SuppliersDetails.AllowUserToResizeColumns = false;
            this.Gridview_SuppliersDetails.AllowUserToResizeRows = false;
            this.Gridview_SuppliersDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Gridview_SuppliersDetails.BackgroundColor = SystemColors.ButtonHighlight;
            this.Gridview_SuppliersDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4, this.Column5, this.Column6, this.Column7 };
            this.Gridview_SuppliersDetails.Columns.AddRange(columnArray2);
            this.Gridview_SuppliersDetails.Dock = DockStyle.Fill;
            this.Gridview_SuppliersDetails.EnableHeadersVisualStyles = false;
            this.Gridview_SuppliersDetails.Location = new Point(0xa9, 4);
            this.Gridview_SuppliersDetails.Name = "Gridview_SuppliersDetails";
            this.Gridview_SuppliersDetails.ReadOnly = true;
            this.Gridview_SuppliersDetails.RowHeadersVisible = false;
            this.Gridview_SuppliersDetails.RowTemplate.DefaultCellStyle.BackColor = SystemColors.ButtonHighlight;
            this.Gridview_SuppliersDetails.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Gridview_SuppliersDetails.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            this.Gridview_SuppliersDetails.Size = new Size(0x3fb, 0x247);
            this.Gridview_SuppliersDetails.TabIndex = 3;
            this.Column1.HeaderText = "SupplierId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.HeaderText = "IdNumber";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column3.HeaderText = "FirstName";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column4.HeaderText = "MiddleName";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column5.HeaderText = "LastName";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column6.HeaderText = "Gender";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column7.HeaderText = "Email";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.panel2.BackColor = SystemColors.ButtonHighlight;
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.ViewSuppliers_SearchGroupBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = DockStyle.Left;
            this.panel2.Location = new Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xa5, 0x247);
            this.panel2.TabIndex = 1;
            this.panel3.Controls.Add(this.Btn_SuppliersList_Refresh);
            this.panel3.Dock = DockStyle.Top;
            this.panel3.Location = new Point(0, 0xc7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0xa3, 0x30);
            this.panel3.TabIndex = 2;
            this.Btn_SuppliersList_Refresh.Location = new Point(0x15, 6);
            this.Btn_SuppliersList_Refresh.Name = "Btn_SuppliersList_Refresh";
            this.Btn_SuppliersList_Refresh.Size = new Size(120, 0x21);
            this.Btn_SuppliersList_Refresh.TabIndex = 0;
            this.Btn_SuppliersList_Refresh.Text = "Refresh List";
            this.Btn_SuppliersList_Refresh.UseVisualStyleBackColor = true;
            this.Btn_SuppliersList_Refresh.Click += new EventHandler(this.Btn_SuppliersList_Refresh_Click);
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton6);
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton5);
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton4);
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton3);
            this.ViewSuppliers_SearchGroupBox.Dock = DockStyle.Top;
            this.ViewSuppliers_SearchGroupBox.Location = new Point(0, 40);
            this.ViewSuppliers_SearchGroupBox.Name = "ViewSuppliers_SearchGroupBox";
            this.ViewSuppliers_SearchGroupBox.Size = new Size(0xa3, 0x9f);
            this.ViewSuppliers_SearchGroupBox.TabIndex = 1;
            this.ViewSuppliers_SearchGroupBox.TabStop = false;
            this.ViewSuppliers_SearchGroupBox.Text = "Filter By";
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new Point(6, 0x20);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new Size(60, 0x15);
            this.radioButton6.TabIndex = 3;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "None";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new Point(6, 0x7a);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new Size(0x4a, 0x15);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Gender";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new Point(6, 0x5c);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new Size(0x88, 0x15);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Registration Date";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new Point(6, 0x3e);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new Size(0x65, 0x15);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Designation";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.label1.Dock = DockStyle.Top;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa3, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter Suppliers";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.Tab_Suppliers.Controls.Add(this.Tab_PageSuppliersAccount);
            this.Tab_Suppliers.Controls.Add(this.Tab_PageViewSuppliers);
            this.Tab_Suppliers.Controls.Add(this.TabPage_SupplyLogs);
            this.Tab_Suppliers.Dock = DockStyle.Fill;
            this.Tab_Suppliers.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Tab_Suppliers.HotTrack = true;
            this.Tab_Suppliers.Location = new Point(0, 0);
            this.Tab_Suppliers.Margin = new Padding(4);
            this.Tab_Suppliers.Name = "Tab_Suppliers";
            this.Tab_Suppliers.Padding = new Point(0x19, 3);
            this.Tab_Suppliers.SelectedIndex = 0;
            this.Tab_Suppliers.Size = new Size(0x4b0, 620);
            this.Tab_Suppliers.TabIndex = 6;
            this.Tab_Suppliers.Selected += new TabControlEventHandler(this.Tab_Suppliers_Selected);
            this.TabPage_SupplyLogs.Controls.Add(this.groupBox13);
            this.TabPage_SupplyLogs.Controls.Add(this.TabPage_Suppliers_FilterBox);
            this.TabPage_SupplyLogs.Location = new Point(4, 0x19);
            this.TabPage_SupplyLogs.Name = "TabPage_SupplyLogs";
            this.TabPage_SupplyLogs.Padding = new Padding(3);
            this.TabPage_SupplyLogs.Size = new Size(0x4a8, 0x24f);
            this.TabPage_SupplyLogs.TabIndex = 4;
            this.TabPage_SupplyLogs.Text = "Supply History";
            this.TabPage_SupplyLogs.UseVisualStyleBackColor = true;
            this.groupBox13.Controls.Add(this.GridView_SupplyHistory);
            this.groupBox13.Dock = DockStyle.Fill;
            this.groupBox13.Location = new Point(0xb5, 3);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new Size(0x3f0, 0x249);
            this.groupBox13.TabIndex = 0x2c;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Product-Supply History List";
            this.GridView_SupplyHistory.AllowUserToAddRows = false;
            this.GridView_SupplyHistory.AllowUserToDeleteRows = false;
            this.GridView_SupplyHistory.AllowUserToResizeColumns = false;
            this.GridView_SupplyHistory.AllowUserToResizeRows = false;
            this.GridView_SupplyHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.GridView_SupplyHistory.BackgroundColor = SystemColors.ButtonHighlight;
            this.GridView_SupplyHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray3 = new DataGridViewColumn[10];
            columnArray3[0] = this.Column17;
            columnArray3[1] = this.Column14;
            columnArray3[2] = this.Column15;
            columnArray3[3] = this.Column16;
            columnArray3[4] = this.Column18;
            columnArray3[5] = this.Column8;
            columnArray3[6] = this.Column10;
            columnArray3[7] = this.Column9;
            columnArray3[8] = this.Column11;
            columnArray3[9] = this.Column12;
            this.GridView_SupplyHistory.Columns.AddRange(columnArray3);
            this.GridView_SupplyHistory.Dock = DockStyle.Fill;
            this.GridView_SupplyHistory.EnableHeadersVisualStyles = false;
            this.GridView_SupplyHistory.Location = new Point(3, 0x13);
            this.GridView_SupplyHistory.Name = "GridView_SupplyHistory";
            this.GridView_SupplyHistory.ReadOnly = true;
            this.GridView_SupplyHistory.RowHeadersVisible = false;
            this.GridView_SupplyHistory.RowTemplate.DefaultCellStyle.BackColor = Color.White;
            this.GridView_SupplyHistory.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.GridView_SupplyHistory.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            this.GridView_SupplyHistory.Size = new Size(0x3ea, 0x233);
            this.GridView_SupplyHistory.TabIndex = 0x2c;
            this.Column17.HeaderText = "SNo";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column14.HeaderText = "SupID";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column15.HeaderText = "OrderID";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column16.HeaderText = "Pcode";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column18.HeaderText = "Description";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column8.HeaderText = "Unit";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column10.HeaderText = "Quantity";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column9.HeaderText = "Price";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column11.HeaderText = "Gross";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column12.HeaderText = "Date";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.TabPage_Suppliers_FilterBox.Controls.Add(this.Btn_SupplyLogs_ClearFilters);
            this.TabPage_Suppliers_FilterBox.Controls.Add(this.groupBox14);
            this.TabPage_Suppliers_FilterBox.Controls.Add(this.groupBox12);
            this.TabPage_Suppliers_FilterBox.Controls.Add(this.groupBox11);
            this.TabPage_Suppliers_FilterBox.Controls.Add(this.groupBox10);
            this.TabPage_Suppliers_FilterBox.Dock = DockStyle.Left;
            this.TabPage_Suppliers_FilterBox.Location = new Point(3, 3);
            this.TabPage_Suppliers_FilterBox.Name = "TabPage_Suppliers_FilterBox";
            this.TabPage_Suppliers_FilterBox.Size = new Size(0xb2, 0x249);
            this.TabPage_Suppliers_FilterBox.TabIndex = 0x2b;
            this.Btn_SupplyLogs_ClearFilters.Location = new Point(11, 0x1b9);
            this.Btn_SupplyLogs_ClearFilters.Name = "Btn_SupplyLogs_ClearFilters";
            this.Btn_SupplyLogs_ClearFilters.Size = new Size(0x95, 0x21);
            this.Btn_SupplyLogs_ClearFilters.TabIndex = 4;
            this.Btn_SupplyLogs_ClearFilters.Text = "Clear Filters";
            this.Btn_SupplyLogs_ClearFilters.UseVisualStyleBackColor = true;
            this.Btn_SupplyLogs_ClearFilters.Click += new EventHandler(this.Btn_SupplyLogs_ClearFilters_Click);
            this.groupBox14.Controls.Add(this.dateTimePicker4);
            this.groupBox14.Controls.Add(this.dateTimePicker3);
            this.groupBox14.Controls.Add(this.label23);
            this.groupBox14.Controls.Add(this.label16);
            this.groupBox14.Controls.Add(this.Btn_SearchLogs_Date);
            this.groupBox14.Dock = DockStyle.Top;
            this.groupBox14.Location = new Point(0, 0x120);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new Size(0xb2, 0x84);
            this.groupBox14.TabIndex = 3;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Search By Date";
            this.dateTimePicker4.Format = DateTimePickerFormat.Short;
            this.dateTimePicker4.Location = new Point(0x39, 0x39);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new Size(0x73, 0x17);
            this.dateTimePicker4.TabIndex = 7;
            this.dateTimePicker3.Format = DateTimePickerFormat.Short;
            this.dateTimePicker3.Location = new Point(0x39, 0x19);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.Size = new Size(0x73, 0x17);
            this.dateTimePicker3.TabIndex = 6;
            this.label23.AutoSize = true;
            this.label23.Location = new Point(7, 0x39);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x21, 0x11);
            this.label23.TabIndex = 5;
            this.label23.Text = "End";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(7, 0x19);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x26, 0x11);
            this.label16.TabIndex = 3;
            this.label16.Text = "Start";
            this.Btn_SearchLogs_Date.Location = new Point(0x23, 0x5d);
            this.Btn_SearchLogs_Date.Name = "Btn_SearchLogs_Date";
            this.Btn_SearchLogs_Date.Size = new Size(0x4b, 0x21);
            this.Btn_SearchLogs_Date.TabIndex = 2;
            this.Btn_SearchLogs_Date.Text = "Search";
            this.Btn_SearchLogs_Date.UseVisualStyleBackColor = true;
            this.groupBox12.Controls.Add(this.Btn_SearchLogs_Pcode);
            this.groupBox12.Controls.Add(this.textBox27);
            this.groupBox12.Dock = DockStyle.Top;
            this.groupBox12.Location = new Point(0, 0xc0);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new Size(0xb2, 0x60);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Search By Pcode";
            this.Btn_SearchLogs_Pcode.Location = new Point(0x23, 0x39);
            this.Btn_SearchLogs_Pcode.Name = "Btn_SearchLogs_Pcode";
            this.Btn_SearchLogs_Pcode.Size = new Size(0x4b, 0x21);
            this.Btn_SearchLogs_Pcode.TabIndex = 2;
            this.Btn_SearchLogs_Pcode.Text = "Search";
            this.Btn_SearchLogs_Pcode.UseVisualStyleBackColor = true;
            this.Btn_SearchLogs_Pcode.Click += new EventHandler(this.Btn_SearchLogs_Pcode_Click);
            this.textBox27.Location = new Point(6, 0x19);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Size(0x9a, 0x17);
            this.textBox27.TabIndex = 1;
            this.groupBox11.Controls.Add(this.Btn_SearchLogs_OrderID);
            this.groupBox11.Controls.Add(this.textBox16);
            this.groupBox11.Dock = DockStyle.Top;
            this.groupBox11.Location = new Point(0, 0x60);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new Size(0xb2, 0x60);
            this.groupBox11.TabIndex = 1;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Search By OrderID";
            this.Btn_SearchLogs_OrderID.Location = new Point(0x23, 0x39);
            this.Btn_SearchLogs_OrderID.Name = "Btn_SearchLogs_OrderID";
            this.Btn_SearchLogs_OrderID.Size = new Size(0x4b, 0x21);
            this.Btn_SearchLogs_OrderID.TabIndex = 2;
            this.Btn_SearchLogs_OrderID.Text = "Search";
            this.Btn_SearchLogs_OrderID.UseVisualStyleBackColor = true;
            this.Btn_SearchLogs_OrderID.Click += new EventHandler(this.Btn_SearchLogs_OrderID_Click);
            this.textBox16.Location = new Point(6, 0x19);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Size(0x9a, 0x17);
            this.textBox16.TabIndex = 1;
            this.groupBox10.Controls.Add(this.Btn_SearchLogs_SupplierID);
            this.groupBox10.Controls.Add(this.textBox13);
            this.groupBox10.Dock = DockStyle.Top;
            this.groupBox10.Location = new Point(0, 0);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new Size(0xb2, 0x60);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Search By SupplierID";
            this.Btn_SearchLogs_SupplierID.Location = new Point(0x23, 0x39);
            this.Btn_SearchLogs_SupplierID.Name = "Btn_SearchLogs_SupplierID";
            this.Btn_SearchLogs_SupplierID.Size = new Size(0x4b, 0x21);
            this.Btn_SearchLogs_SupplierID.TabIndex = 2;
            this.Btn_SearchLogs_SupplierID.Text = "Search";
            this.Btn_SearchLogs_SupplierID.UseVisualStyleBackColor = true;
            this.Btn_SearchLogs_SupplierID.Click += new EventHandler(this.Btn_SearchLogs_SupplierID_Click);
            this.textBox13.Location = new Point(6, 0x19);
            this.textBox13.MaxLength = 50;
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Size(0x9a, 0x17);
            this.textBox13.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x4b0, 620);
            base.Controls.Add(this.Tab_Suppliers);
            this.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.Margin = new Padding(4);
            base.Name = "Suppliers";
            base.Load += new EventHandler(this.Suppliers_Load);
            this.Tab_PageSuppliersAccount.ResumeLayout(false);
            this.Panel_HoldSuppliersDetails.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.Panel_Search.ResumeLayout(false);
            this.Panel_Search.PerformLayout();
            this.Tab_PageViewSuppliers.ResumeLayout(false);
            ((ISupportInitialize) this.Gridview_SuppliersDetails).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ViewSuppliers_SearchGroupBox.ResumeLayout(false);
            this.ViewSuppliers_SearchGroupBox.PerformLayout();
            this.Tab_Suppliers.ResumeLayout(false);
            this.TabPage_SupplyLogs.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            ((ISupportInitialize) this.GridView_SupplyHistory).EndInit();
            this.TabPage_Suppliers_FilterBox.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            base.ResumeLayout(false);
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            this.GetSuppliersList();
        }

        private void Tab_Suppliers_Selected(object sender, TabControlEventArgs e)
        {
            if (this.Tab_Suppliers.SelectedTab.Name == "Tab_PageViewSuppliers")
            {
                this.GetSuppliersList();
            }
            if (this.Tab_Suppliers.SelectedTab.Name == "Tab_PageSuppliersAccount")
            {
                base.ParentForm.AcceptButton = this.Btn_SuppliersAccount_FindSupplier;
                this.Panel_Search.Visible = true;
                this.Panel_HoldSuppliersDetails.Visible = false;
            }
            if (this.Tab_Suppliers.SelectedTab.Name == "TabPage_SupplyLogs")
            {
                this.GetSupplyHistoryLogs(";");
            }
        }

        public void ToggleSearchParameters(TextBox ParameterControl)
        {
            string text = ParameterControl.Text;
            this.textBox13.Text = "";
            this.textBox16.Text = "";
            this.textBox27.Text = "";
            ParameterControl.Text = text;
        }
    }
}

