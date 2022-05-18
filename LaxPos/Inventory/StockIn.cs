namespace LaxPos.Inventory
{
    using Bunifu.Framework.UI;
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class StockIn : BunifuForm
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public int X = 0;
        public static int TransactionNo = 0;
        private readonly Random Rand = new Random();
        public static DateTime InsertionDate = Program.CurrentDateTime();
        public static List<string> UnsavedItems = new List<string>();
        public static List<string> UnresolvedItems = new List<string>();
        private IContainer components = null;
        private TabControl Tab_Suppliers;
        private TabPage Tab_Page_UnorderedItemsRestock;
        private Panel panel5;
        private DataGridView Deliveries_Gridview;
        private GroupBox groupBox3;
        private TextBox textBox20;
        private Label label21;
        private Button Btn_ClearGridview;
        private Button Btn_SaveDeliveries;
        private Panel panel2;
        private GroupBox groupBox4;
        private Button Btn_Restock_SearchItem;
        public TextBox Txt_SearchProductId;
        private Label label7;
        private TabPage TabPage_ReceiveOrderItems;
        private Panel panel1;
        private GroupBox groupBox1;
        private Button Btn_SearchOrder;
        public TextBox Txt_OrderId;
        private Label label12;
        private Panel panel3;
        private DataGridView DataGridView_OrderItems;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private GroupBox groupBox2;
        private TextBox textBox1;
        private Label label1;
        private Button Btn_OrderClearItems;
        private Button Btn_OrderItemsSave;
        public TextBox Text_UnorderedSupplierId;
        private Label label2;
        private CheckBox checkBox1;
        public TextBox textBox3;
        private Label label4;
        public TextBox textBox2;
        private Label label3;
        public TextBox textBox4;
        private Label label5;
        public TextBox textBox5;
        private Label label6;
        private BunifuDatepicker bunifuDatepicker1;
        private Label label8;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column7;

        public StockIn()
        {
            this.InitializeComponent();
        }

        private void Btn_ClearGridview_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the items list?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Deliveries_Gridview.Rows.Clear();
            }
        }

        private void Btn_OrderClearItems_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the items list?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.DataGridView_OrderItems.Rows.Clear();
            }
        }

        private void Btn_OrderItemsSave_Click(object sender, EventArgs e)
        {
            this.InsertOrderedProduct();
        }

        private void Btn_Restock_SearchItem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime time1 = this.bunifuDatepicker1.Value;
                if (this.bunifuDatepicker1.Value <= Program.CurrentDateTime())
                {
                    MessageBox.Show("Invalid Expiry date", "Expiry date Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from inventorymaster where ProductCode=@id OR Description=@id;";
                    command.Parameters.AddWithValue("@id", this.Txt_SearchProductId.Text);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("The product does not exist", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                break;
                            }
                            if (this.Text_UnorderedSupplierId.Text == "")
                            {
                                object[] objArray1 = new object[] { "N/A", "N/A", reader["ProductCode"].ToString(), reader["Description"].ToString(), reader["SuppliersPack"].ToString(), "10", this.bunifuDatepicker1.Value };
                                this.Deliveries_Gridview.Rows.Add(objArray1);
                                continue;
                            }
                            object[] values = new object[] { "N/A", this.Text_UnorderedSupplierId.Text, reader["ProductCode"].ToString(), reader["Description"].ToString(), reader["SuppliersPack"].ToString(), "10", this.bunifuDatepicker1.Value };
                            this.Deliveries_Gridview.Rows.Add(values);
                        }
                    }
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    if (!this.checkBox1.Checked)
                    {
                        this.Text_UnorderedSupplierId.Text = "";
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void Btn_SaveDeliveries_Click(object sender, EventArgs e)
        {
            if (this.Deliveries_Gridview.Rows.Count <= 0)
            {
                MessageBox.Show("You have No Product Items To Restock !!", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    InsertionDate = Program.CurrentDateTime();
                    TransactionNo = this.Rand.Next(0x5f5e100, 0x35a4e900);
                    this.InsertUnorderedProduct();
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Btn_SearchOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Txt_OrderId.Text != "")
                {
                    this.OrderMaster(this.Txt_OrderId.Text);
                }
                else
                {
                    MessageBox.Show("The Order Number cannot be empty !!", "Search Results");
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ClearDeliveriesForm()
        {
            this.Deliveries_Gridview.Rows.Clear();
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
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.Tab_Suppliers = new TabControl();
            this.Tab_Page_UnorderedItemsRestock = new TabPage();
            this.panel5 = new Panel();
            this.Deliveries_Gridview = new DataGridView();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.groupBox3 = new GroupBox();
            this.textBox20 = new TextBox();
            this.label21 = new Label();
            this.Btn_ClearGridview = new Button();
            this.Btn_SaveDeliveries = new Button();
            this.panel2 = new Panel();
            this.groupBox4 = new GroupBox();
            this.bunifuDatepicker1 = new BunifuDatepicker();
            this.label8 = new Label();
            this.checkBox1 = new CheckBox();
            this.Text_UnorderedSupplierId = new TextBox();
            this.label2 = new Label();
            this.Btn_Restock_SearchItem = new Button();
            this.Txt_SearchProductId = new TextBox();
            this.label7 = new Label();
            this.TabPage_ReceiveOrderItems = new TabPage();
            this.panel3 = new Panel();
            this.DataGridView_OrderItems = new DataGridView();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.groupBox2 = new GroupBox();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.Btn_OrderClearItems = new Button();
            this.Btn_OrderItemsSave = new Button();
            this.panel1 = new Panel();
            this.textBox5 = new TextBox();
            this.label6 = new Label();
            this.textBox4 = new TextBox();
            this.label5 = new Label();
            this.textBox3 = new TextBox();
            this.label4 = new Label();
            this.textBox2 = new TextBox();
            this.label3 = new Label();
            this.groupBox1 = new GroupBox();
            this.Btn_SearchOrder = new Button();
            this.Txt_OrderId = new TextBox();
            this.label12 = new Label();
            this.Tab_Suppliers.SuspendLayout();
            this.Tab_Page_UnorderedItemsRestock.SuspendLayout();
            this.panel5.SuspendLayout();
            ((ISupportInitialize) this.Deliveries_Gridview).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.TabPage_ReceiveOrderItems.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize) this.DataGridView_OrderItems).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.Tab_Suppliers.Controls.Add(this.Tab_Page_UnorderedItemsRestock);
            this.Tab_Suppliers.Controls.Add(this.TabPage_ReceiveOrderItems);
            this.Tab_Suppliers.Dock = DockStyle.Fill;
            this.Tab_Suppliers.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Tab_Suppliers.HotTrack = true;
            this.Tab_Suppliers.Location = new Point(0, 0);
            this.Tab_Suppliers.Margin = new Padding(4);
            this.Tab_Suppliers.Name = "Tab_Suppliers";
            this.Tab_Suppliers.Padding = new Point(0x19, 3);
            this.Tab_Suppliers.SelectedIndex = 0;
            this.Tab_Suppliers.Size = new Size(0x3d3, 0x1a6);
            this.Tab_Suppliers.TabIndex = 7;
            this.Tab_Page_UnorderedItemsRestock.BackColor = SystemColors.ButtonHighlight;
            this.Tab_Page_UnorderedItemsRestock.Controls.Add(this.panel5);
            this.Tab_Page_UnorderedItemsRestock.Controls.Add(this.panel2);
            this.Tab_Page_UnorderedItemsRestock.Location = new Point(4, 0x19);
            this.Tab_Page_UnorderedItemsRestock.Name = "Tab_Page_UnorderedItemsRestock";
            this.Tab_Page_UnorderedItemsRestock.Padding = new Padding(3);
            this.Tab_Page_UnorderedItemsRestock.Size = new Size(0x3cb, 0x189);
            this.Tab_Page_UnorderedItemsRestock.TabIndex = 5;
            this.Tab_Page_UnorderedItemsRestock.Text = "Receive Items";
            this.panel5.Controls.Add(this.Deliveries_Gridview);
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Dock = DockStyle.Fill;
            this.panel5.Location = new Point(3, 0x56);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x3c5, 0x130);
            this.panel5.TabIndex = 0x15;
            this.Deliveries_Gridview.AllowUserToAddRows = false;
            this.Deliveries_Gridview.AllowUserToResizeColumns = false;
            this.Deliveries_Gridview.AllowUserToResizeRows = false;
            this.Deliveries_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Deliveries_Gridview.BackgroundColor = Color.White;
            this.Deliveries_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column3, this.Column2, this.Column1, this.Column4, this.Column6, this.Column5, this.Column7 };
            this.Deliveries_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Deliveries_Gridview.Dock = DockStyle.Fill;
            this.Deliveries_Gridview.EnableHeadersVisualStyles = false;
            this.Deliveries_Gridview.Location = new Point(0, 0);
            this.Deliveries_Gridview.Name = "Deliveries_Gridview";
            this.Deliveries_Gridview.RowHeadersWidth = 20;
            this.Deliveries_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Deliveries_Gridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.Deliveries_Gridview.Size = new Size(0x3c5, 0xed);
            this.Deliveries_Gridview.TabIndex = 20;
            this.Column3.HeaderText = "OrderId";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column2.HeaderText = "SupplierId";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column1.HeaderText = "ProductId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column4.HeaderText = "Description";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column6.HeaderText = "Unit";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column5.HeaderText = "Quantity";
            this.Column5.Name = "Column5";
            style.Format = "d";
            style.NullValue = null;
            this.Column7.DefaultCellStyle = style;
            this.Column7.HeaderText = "ExpDate";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.groupBox3.Controls.Add(this.textBox20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.Btn_ClearGridview);
            this.groupBox3.Controls.Add(this.Btn_SaveDeliveries);
            this.groupBox3.Dock = DockStyle.Bottom;
            this.groupBox3.Location = new Point(0, 0xed);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x3c5, 0x43);
            this.groupBox3.TabIndex = 0x15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Receive Goods";
            this.textBox20.Location = new Point(0x5e, 0x19);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Size(140, 0x17);
            this.textBox20.TabIndex = 0x37;
            this.label21.AutoSize = true;
            this.label21.Location = new Point(11, 0x1c);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x4d, 0x11);
            this.label21.TabIndex = 0x36;
            this.label21.Text = "Total Items";
            this.Btn_ClearGridview.Location = new Point(0x2b4, 0x16);
            this.Btn_ClearGridview.Name = "Btn_ClearGridview";
            this.Btn_ClearGridview.Size = new Size(0x76, 0x1f);
            this.Btn_ClearGridview.TabIndex = 0x33;
            this.Btn_ClearGridview.Text = "Clear Items List";
            this.Btn_ClearGridview.UseVisualStyleBackColor = false;
            this.Btn_ClearGridview.Click += new EventHandler(this.Btn_ClearGridview_Click);
            this.Btn_SaveDeliveries.Location = new Point(0x339, 0x15);
            this.Btn_SaveDeliveries.Name = "Btn_SaveDeliveries";
            this.Btn_SaveDeliveries.Size = new Size(0x74, 0x1f);
            this.Btn_SaveDeliveries.TabIndex = 50;
            this.Btn_SaveDeliveries.Text = "Save Items";
            this.Btn_SaveDeliveries.UseVisualStyleBackColor = true;
            this.Btn_SaveDeliveries.Click += new EventHandler(this.Btn_SaveDeliveries_Click);
            this.panel2.BackColor = SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3c5, 0x53);
            this.panel2.TabIndex = 0x11;
            this.groupBox4.BackColor = SystemColors.ButtonHighlight;
            this.groupBox4.Controls.Add(this.bunifuDatepicker1);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.Text_UnorderedSupplierId);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.Btn_Restock_SearchItem);
            this.groupBox4.Controls.Add(this.Txt_SearchProductId);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Dock = DockStyle.Fill;
            this.groupBox4.Location = new Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x3c5, 0x53);
            this.groupBox4.TabIndex = 0x12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Add Item";
            this.bunifuDatepicker1.BackColor = Color.White;
            this.bunifuDatepicker1.BorderRadius = 0;
            this.bunifuDatepicker1.BorderStyle = BorderStyle.Fixed3D;
            this.bunifuDatepicker1.ForeColor = SystemColors.WindowFrame;
            this.bunifuDatepicker1.Format = DateTimePickerFormat.Short;
            this.bunifuDatepicker1.FormatCustom = null;
            this.bunifuDatepicker1.Location = new Point(0x202, 0x23);
            this.bunifuDatepicker1.Margin = new Padding(4);
            this.bunifuDatepicker1.Name = "bunifuDatepicker1";
            this.bunifuDatepicker1.Size = new Size(0x94, 0x1d);
            this.bunifuDatepicker1.TabIndex = 0x15;
            this.bunifuDatepicker1.Value = new DateTime(0x7e5, 7, 0x15, 0x10, 0x30, 0x1a, 0x1bb);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(530, 14);
            this.label8.Name = "label8";
            this.label8.Size = new Size(80, 0x11);
            this.label8.TabIndex = 20;
            this.label8.Text = "Expiry Date";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x141, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x98, 0x15);
            this.checkBox1.TabIndex = 0x13;
            this.checkBox1.Text = "Remember Supplier";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.Text_UnorderedSupplierId.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.Text_UnorderedSupplierId.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.Text_UnorderedSupplierId.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Text_UnorderedSupplierId.Location = new Point(0xf3, 0x26);
            this.Text_UnorderedSupplierId.Name = "Text_UnorderedSupplierId";
            this.Text_UnorderedSupplierId.Size = new Size(230, 0x1a);
            this.Text_UnorderedSupplierId.TabIndex = 0x12;
            this.Text_UnorderedSupplierId.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(240, 0x12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4b, 0x11);
            this.label2.TabIndex = 0x11;
            this.label2.Text = "Supplier Id";
            this.Btn_Restock_SearchItem.BackColor = Color.LightGray;
            this.Btn_Restock_SearchItem.Location = new Point(0x2b4, 0x16);
            this.Btn_Restock_SearchItem.Name = "Btn_Restock_SearchItem";
            this.Btn_Restock_SearchItem.Size = new Size(0x8f, 0x2a);
            this.Btn_Restock_SearchItem.TabIndex = 0x10;
            this.Btn_Restock_SearchItem.Text = "Add Item";
            this.Btn_Restock_SearchItem.UseVisualStyleBackColor = false;
            this.Btn_Restock_SearchItem.Click += new EventHandler(this.Btn_Restock_SearchItem_Click);
            this.Txt_SearchProductId.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.Txt_SearchProductId.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.Txt_SearchProductId.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Txt_SearchProductId.Location = new Point(14, 0x27);
            this.Txt_SearchProductId.Name = "Txt_SearchProductId";
            this.Txt_SearchProductId.Size = new Size(0xd0, 0x1a);
            this.Txt_SearchProductId.TabIndex = 15;
            this.Txt_SearchProductId.TextAlign = HorizontalAlignment.Center;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x39, 0x13);
            this.label7.Name = "label7";
            this.label7.Size = new Size(90, 0x11);
            this.label7.TabIndex = 14;
            this.label7.Text = "Item Name/Id";
            this.TabPage_ReceiveOrderItems.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_ReceiveOrderItems.Controls.Add(this.panel3);
            this.TabPage_ReceiveOrderItems.Controls.Add(this.panel1);
            this.TabPage_ReceiveOrderItems.Location = new Point(4, 0x19);
            this.TabPage_ReceiveOrderItems.Name = "TabPage_ReceiveOrderItems";
            this.TabPage_ReceiveOrderItems.Padding = new Padding(3);
            this.TabPage_ReceiveOrderItems.Size = new Size(0x3cb, 0x189);
            this.TabPage_ReceiveOrderItems.TabIndex = 6;
            this.TabPage_ReceiveOrderItems.Text = "Receive Order";
            this.panel3.BackColor = SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.DataGridView_OrderItems);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(3, 0x45);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x3c5, 0x141);
            this.panel3.TabIndex = 0x16;
            this.DataGridView_OrderItems.AllowUserToAddRows = false;
            this.DataGridView_OrderItems.AllowUserToResizeColumns = false;
            this.DataGridView_OrderItems.AllowUserToResizeRows = false;
            this.DataGridView_OrderItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_OrderItems.BackgroundColor = Color.White;
            this.DataGridView_OrderItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6 };
            this.DataGridView_OrderItems.Columns.AddRange(columnArray2);
            this.DataGridView_OrderItems.Dock = DockStyle.Fill;
            this.DataGridView_OrderItems.EnableHeadersVisualStyles = false;
            this.DataGridView_OrderItems.Location = new Point(0, 0);
            this.DataGridView_OrderItems.Name = "DataGridView_OrderItems";
            this.DataGridView_OrderItems.RowHeadersVisible = false;
            this.DataGridView_OrderItems.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.DataGridView_OrderItems.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.DataGridView_OrderItems.Size = new Size(0x3c5, 0xfe);
            this.DataGridView_OrderItems.TabIndex = 20;
            this.dataGridViewTextBoxColumn1.HeaderText = "OrderId";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "ProductId";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn6.HeaderText = "SupplierId";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.groupBox2.BackColor = SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Btn_OrderClearItems);
            this.groupBox2.Controls.Add(this.Btn_OrderItemsSave);
            this.groupBox2.Dock = DockStyle.Bottom;
            this.groupBox2.Location = new Point(0, 0xfe);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x3c5, 0x43);
            this.groupBox2.TabIndex = 0x15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Receive Goods";
            this.textBox1.Location = new Point(0x5e, 0x19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(140, 0x17);
            this.textBox1.TabIndex = 0x37;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(11, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4d, 0x11);
            this.label1.TabIndex = 0x36;
            this.label1.Text = "Total Items";
            this.Btn_OrderClearItems.Location = new Point(0x256, 0x15);
            this.Btn_OrderClearItems.Name = "Btn_OrderClearItems";
            this.Btn_OrderClearItems.Size = new Size(0x76, 0x1f);
            this.Btn_OrderClearItems.TabIndex = 0x33;
            this.Btn_OrderClearItems.Text = "Clear Items List";
            this.Btn_OrderClearItems.UseVisualStyleBackColor = false;
            this.Btn_OrderClearItems.Click += new EventHandler(this.Btn_OrderClearItems_Click);
            this.Btn_OrderItemsSave.Location = new Point(0x2ff, 0x15);
            this.Btn_OrderItemsSave.Name = "Btn_OrderItemsSave";
            this.Btn_OrderItemsSave.Size = new Size(0x74, 0x1f);
            this.Btn_OrderItemsSave.TabIndex = 50;
            this.Btn_OrderItemsSave.Text = "Save Items";
            this.Btn_OrderItemsSave.UseVisualStyleBackColor = true;
            this.Btn_OrderItemsSave.Click += new EventHandler(this.Btn_OrderItemsSave_Click);
            this.panel1.BackColor = SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3c5, 0x42);
            this.panel1.TabIndex = 0;
            this.textBox5.BackColor = SystemColors.ButtonHighlight;
            this.textBox5.Location = new Point(0x355, 0x1d);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0x56, 0x17);
            this.textBox5.TabIndex = 0x1a;
            this.textBox5.TextAlign = HorizontalAlignment.Center;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(850, 9);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x52, 0x11);
            this.label6.TabIndex = 0x19;
            this.label6.Text = "Items Count";
            this.textBox4.BackColor = SystemColors.ButtonHighlight;
            this.textBox4.Location = new Point(0x2dd, 0x1d);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0x6a, 0x17);
            this.textBox4.TabIndex = 0x18;
            this.textBox4.TextAlign = HorizontalAlignment.Center;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x2e3, 9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x26, 0x11);
            this.label5.TabIndex = 0x17;
            this.label5.Text = "Date";
            this.textBox3.BackColor = SystemColors.ButtonHighlight;
            this.textBox3.Location = new Point(590, 0x1d);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0x7f, 0x17);
            this.textBox3.TabIndex = 0x16;
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x254, 9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 0x15;
            this.label4.Text = "Supplier";
            this.textBox2.BackColor = SystemColors.ButtonHighlight;
            this.textBox2.Location = new Point(0x1d0, 0x1d);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x6f, 0x17);
            this.textBox2.TabIndex = 20;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x1cd, 9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(60, 0x11);
            this.label3.TabIndex = 0x13;
            this.label3.Text = "Order Id";
            this.groupBox1.BackColor = SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.Btn_SearchOrder);
            this.groupBox1.Controls.Add(this.Txt_OrderId);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Dock = DockStyle.Left;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1c6, 0x42);
            this.groupBox1.TabIndex = 0x12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Order Items";
            this.Btn_SearchOrder.Location = new Point(0x141, 0x16);
            this.Btn_SearchOrder.Name = "Btn_SearchOrder";
            this.Btn_SearchOrder.Size = new Size(0x77, 30);
            this.Btn_SearchOrder.TabIndex = 0x10;
            this.Btn_SearchOrder.Text = "Search Order";
            this.Btn_SearchOrder.UseVisualStyleBackColor = true;
            this.Btn_SearchOrder.Click += new EventHandler(this.Btn_SearchOrder_Click);
            this.Txt_OrderId.Location = new Point(0x4e, 0x1a);
            this.Txt_OrderId.Name = "Txt_OrderId";
            this.Txt_OrderId.Size = new Size(0xed, 0x17);
            this.Txt_OrderId.TabIndex = 15;
            this.Txt_OrderId.TextAlign = HorizontalAlignment.Center;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(11, 0x1d);
            this.label12.Name = "label12";
            this.label12.Size = new Size(60, 0x11);
            this.label12.TabIndex = 14;
            this.label12.Text = "Order Id";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x3d3, 0x1a6);
            base.Controls.Add(this.Tab_Suppliers);
            base.Name = "StockIn";
            this.Tab_Suppliers.ResumeLayout(false);
            this.Tab_Page_UnorderedItemsRestock.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((ISupportInitialize) this.Deliveries_Gridview).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.TabPage_ReceiveOrderItems.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((ISupportInitialize) this.DataGridView_OrderItems).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void InsertBatch(MySqlConnection con_Parameter, DateTime ExpDate)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO batchstokin (Expirydate, Batchdate) VALUES (@Expirydate, @Batchdate) ON DUPLICATE KEY UPDATE Expirydate = @Expirydate; ", con_Parameter);
                command.Parameters.AddWithValue("@Expirydate", ExpDate);
                command.Parameters.AddWithValue("@Batchdate", Program.CurrentDateTime());
                int num = command.ExecuteNonQuery();
            }
            catch
            {
            }
        }

        public void InsertOrderedProduct()
        {
            MySqlTransaction transaction = null;
            try
            {
                UnresolvedItems.Clear();
                UnsavedItems.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                transaction = connection.BeginTransaction();
                this.X = 0;
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                MySqlCommand command2 = connection.CreateCommand();
                int num = 0;
                while (true)
                {
                    if (num >= this.DataGridView_OrderItems.Rows.Count)
                    {
                        if (this.X <= 0)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Failed To update Items", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if ((UnresolvedItems.Count <= 0) && (UnsavedItems.Count <= 0))
                        {
                            transaction.Commit();
                            MessageBox.Show("You have successfully Updated The product(s)", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            List<DataGridViewRow> unsaved = new List<DataGridViewRow>();
                            foreach (string str in UnsavedItems)
                            {
                                int num4 = 0;
                                while (true)
                                {
                                    if (num4 < this.Deliveries_Gridview.Rows.Count)
                                    {
                                        if (str != this.Deliveries_Gridview.Rows[num4].Cells[1].Value.ToString())
                                        {
                                            num4++;
                                            continue;
                                        }
                                        unsaved.Add(this.Deliveries_Gridview.Rows[num4]);
                                    }
                                    break;
                                }
                            }
                            foreach (string str2 in UnresolvedItems)
                            {
                                int num5 = 0;
                                while (true)
                                {
                                    if (num5 < this.Deliveries_Gridview.Rows.Count)
                                    {
                                        if (str2 != this.Deliveries_Gridview.Rows[num5].Cells[1].Value.ToString())
                                        {
                                            num5++;
                                            continue;
                                        }
                                        unsaved.Add(this.Deliveries_Gridview.Rows[num5]);
                                    }
                                    break;
                                }
                            }
                            transaction.Commit();
                            new UnsavedRestockItems(unsaved).ShowDialog(this);
                            this.ClearDeliveriesForm();
                        }
                        connection.Close();
                        break;
                    }
                    try
                    {
                        if (!this.ProductExist(this.DataGridView_OrderItems.Rows[num].Cells[1].Value.ToString()))
                        {
                            this.X++;
                            UnresolvedItems.Add(this.DataGridView_OrderItems.Rows[num].Cells[1].Value.ToString());
                        }
                        else if (this.UpdateInventoryMaster(connection, this.DataGridView_OrderItems.Rows[num].Cells[1].Value.ToString(), this.DataGridView_OrderItems.Rows[num].Cells[4].Value.ToString()) != 1)
                        {
                            UnsavedItems.Add(this.DataGridView_OrderItems.Rows[num].Cells[1].Value.ToString());
                        }
                        else
                        {
                            this.X++;
                            command2 = new MySqlCommand("update  orderitems set DelStatus=@status, DelQty=@qty, DelDate=@date where OrderID=@order and Pcode=@pcode", connection);
                            command2.Parameters.AddWithValue("@order", this.DataGridView_OrderItems.Rows[num].Cells[0].Value);
                            command2.Parameters.AddWithValue("@pcode", this.DataGridView_OrderItems.Rows[num].Cells[1].Value);
                            command2.Parameters.AddWithValue("@qty", this.DataGridView_OrderItems.Rows[num].Cells[4].Value);
                            command2.Parameters.AddWithValue("@status", "1");
                            command2.Parameters.AddWithValue("@date", Program.CurrentDateTime());
                            int num2 = command2.ExecuteNonQuery();
                            command2.Parameters.Clear();
                            command2 = new MySqlCommand("update  ordermaster set DeliveryStatus=@status where OrderID=@order", connection);
                            command2.Parameters.AddWithValue("@order", this.DataGridView_OrderItems.Rows[0].Cells[0].Value);
                            command2.Parameters.AddWithValue("@status", "Delivered");
                            int num3 = command2.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exception1)
                    {
                        MessageBox.Show(exception1.Message, "error");
                    }
                    command.Parameters.Clear();
                    command.Dispose();
                    num++;
                }
            }
            catch (Exception exception2)
            {
                transaction.Rollback();
                MessageBox.Show(exception2.Message);
            }
        }

        public void InsertUnorderedProduct()
        {
            MySqlTransaction transaction = null;
            try
            {
                UnresolvedItems.Clear();
                UnsavedItems.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                transaction = connection.BeginTransaction();
                this.X = 0;
                MySqlCommand command = connection.CreateCommand();
                MySqlCommand command2 = connection.CreateCommand();
                int num = 0;
                while (true)
                {
                    if (num >= this.Deliveries_Gridview.Rows.Count)
                    {
                        if (this.X <= 0)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Failed To update Items", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if ((UnresolvedItems.Count <= 0) && (UnsavedItems.Count <= 0))
                        {
                            transaction.Commit();
                            MessageBox.Show("You have successfully Updated The product(s)", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            List<DataGridViewRow> unsaved = new List<DataGridViewRow>();
                            foreach (string str in UnsavedItems)
                            {
                                int num2 = 0;
                                while (true)
                                {
                                    if (num2 < this.Deliveries_Gridview.Rows.Count)
                                    {
                                        if (str != this.Deliveries_Gridview.Rows[num2].Cells[2].Value.ToString())
                                        {
                                            num2++;
                                            continue;
                                        }
                                        unsaved.Add(this.Deliveries_Gridview.Rows[num2]);
                                    }
                                    break;
                                }
                            }
                            foreach (string str2 in UnresolvedItems)
                            {
                                int num3 = 0;
                                while (true)
                                {
                                    if (num3 < this.Deliveries_Gridview.Rows.Count)
                                    {
                                        if (str2 != this.Deliveries_Gridview.Rows[num3].Cells[2].Value.ToString())
                                        {
                                            num3++;
                                            continue;
                                        }
                                        unsaved.Add(this.Deliveries_Gridview.Rows[num3]);
                                    }
                                    break;
                                }
                            }
                            transaction.Commit();
                            new UnsavedRestockItems(unsaved).ShowDialog(this);
                        }
                        connection.Close();
                        break;
                    }
                    command.CommandType = CommandType.Text;
                    try
                    {
                        if (!this.ProductExist(this.Deliveries_Gridview.Rows[num].Cells[2].Value.ToString()))
                        {
                            this.X++;
                            UnresolvedItems.Add(this.Deliveries_Gridview.Rows[num].Cells[2].Value.ToString());
                        }
                        else if (this.UpdateInventoryMaster(connection, this.Deliveries_Gridview.Rows[num].Cells[2].Value.ToString(), this.Deliveries_Gridview.Rows[num].Cells[5].Value.ToString()) != 1)
                        {
                            UnsavedItems.Add(this.Deliveries_Gridview.Rows[num].Cells[2].Value.ToString());
                        }
                        else
                        {
                            this.InsertBatch(connection, Convert.ToDateTime(this.Deliveries_Gridview.Rows[num].Cells[6].Value.ToString()));
                            this.X++;
                        }
                    }
                    catch (Exception exception1)
                    {
                        MessageBox.Show(exception1.Message, "error");
                    }
                    command.Parameters.Clear();
                    command.Dispose();
                    num++;
                }
            }
            catch (Exception exception2)
            {
                transaction.Rollback();
                MessageBox.Show(exception2.Message);
            }
        }

        public void OrderItemsSearch(string OrderId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select a.OrderID,a.Pcode,a.Description,a.Unit,a.Quantity from orderitems a where a.OrderID=@id;";
                command.Parameters.AddWithValue("@id", OrderId);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The Order Items does not exist", "Order Items Search", MessageBoxButtons.OKCancel);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString() };
                        this.DataGridView_OrderItems.Rows.Add(values);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ItemSearch Error", MessageBoxButtons.OK);
            }
        }

        public void OrderMaster(string OrderId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select a.OrderID,a.SupplierId,a.Orderdate,a.OrderItemsCount from ordermaster a where a.OrderID=@id and a.DeliveryStatus='Undelivered';";
                command.Parameters.AddWithValue("@id", OrderId);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The order id does not exist", "Order Search Results", MessageBoxButtons.OKCancel);
                    connection.Close();
                    reader.Close();
                    command.Dispose();
                }
                else
                {
                    string orderId = "";
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            connection.Close();
                            reader.Close();
                            command.Dispose();
                            this.OrderItemsSearch(orderId);
                            break;
                        }
                        orderId = reader["OrderID"].ToString();
                        this.textBox2.Text = reader["OrderID"].ToString();
                        this.textBox3.Text = reader["SupplierId"].ToString();
                        this.textBox4.Text = reader["Orderdate"].ToString();
                        this.textBox5.Text = reader["OrderItemsCount"].ToString();
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Error message", MessageBoxButtons.OK);
            }
        }

        public bool ProductExist(string ProductId)
        {
            bool flag2;
            MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from inventorymaster where ProductCode=@id;";
            command.Parameters.AddWithValue("@id", ProductId);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                connection.Close();
                reader.Close();
                command.Dispose();
                flag2 = true;
            }
            else
            {
                connection.Close();
                reader.Close();
                command.Dispose();
                flag2 = false;
            }
            return flag2;
        }

        public int UpdateInventoryMaster(MySqlConnection con_Parameter, string ProductCode, string Quantity)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("UPDATE inventorymaster set StockBalance=StockBalance+@quantity where ProductCode=@ProductCode", con_Parameter);
                command.Parameters.AddWithValue("@ProductCode", ProductCode);
                command.Parameters.AddWithValue("@quantity", Quantity);
                int num = command.ExecuteNonQuery();
                command.Dispose();
                return ((num <= 0) ? 0 : 1);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Update Error message", MessageBoxButtons.OK);
                return 0;
            }
        }
    }
}

