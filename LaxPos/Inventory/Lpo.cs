namespace LaxPos.Inventory
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Lpo : LaxcoForm
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public static int OrderNo;
        private readonly Random Rand = new Random();
        public static DateTime InsertionDate;
        public static string InsertionTime;
        private IContainer components = null;
        private TabControl Tab_Suppliers;
        private TabPage Tab_Page_OrdersList;
        private TabPage TabPage_OrderComplete;
        private DataGridView GridView_OrdersList;
        private Panel panel2;
        private Label label1;
        private TabPage TabPage_ViewOrder;
        private GroupBox ViewSuppliers_SearchGroupBox;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private Label label6;
        private TextBox TextBox_OrderId;
        private Button Btn_Vieworder;
        private Panel panel1;
        private DataGridView dataGridView3;
        private Panel panel5;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private TabPage TabPage_NewOrder;
        private TabPage TabPage_OrdersTrashed;
        private GroupBox GroupBox_NewOrdersDescription;
        private TextBox textBox8;
        private TextBox textBox24;
        private Button Btn_SearchProduct;
        private TextBox BtnOrders_Search;
        private Label label2;
        private GroupBox groupBox2;
        private TextBox textBox26;
        private TextBox textBox27;
        private TextBox textBox28;
        private TextBox textBox29;
        private Label label25;
        private Label label28;
        private Label label29;
        private Label label30;
        private TextBox textBox30;
        private Label label31;
        private Button BtnOrders_AddCart;
        private TextBox textBox31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Panel Panel_GridviewHolder;
        private DataGridView Order_Gridview;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private Panel Order_GridviewControls;
        private Button Btn_SaveOrder;
        private Button BtnOrders_ClearCart;
        private Button button5;
        private DataGridView Gridview_CompleteOrders;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private Panel panel3;
        private Label label3;
        private Button button1;
        private TextBox textBox1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private Button Btn_RemoveSupplier;
        private Button Btn_PickSupplier;

        public Lpo()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void Btn_PickSupplier_Click(object sender, EventArgs e)
        {
            PickSupplier supplier = new PickSupplier();
            if (supplier.ShowDialog(this) == DialogResult.OK)
            {
                this.textBox26.Text = supplier.textBox26.Text;
                this.textBox27.Text = supplier.textBox27.Text;
                this.textBox28.Text = supplier.textBox28.Text;
                this.textBox29.Text = supplier.textBox29.Text;
            }
        }

        private void Btn_RemoveSupplier_Click(object sender, EventArgs e)
        {
        }

        private void Btn_SaveOrder_Click(object sender, EventArgs e)
        {
            OrderNo = this.Rand.Next(0x2710, 0x15f90);
            if (((this.textBox26.Text != "") && ((this.textBox27.Text != "") && (this.textBox28.Text != ""))) && (this.textBox29.Text != ""))
            {
                this.InsertOrdeMaster(OrderNo);
            }
            else
            {
                MessageBox.Show("Incomplete Suppliers Details", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_SearchProduct_Click(object sender, EventArgs e)
        {
            this.GetProducts(this.BtnOrders_Search.Text);
        }

        private void Btn_Vieworder_Click(object sender, EventArgs e)
        {
            this.ViewSingleOrder(this.TextBox_OrderId.Text);
        }

        private void BtnOrders_AddCart_Click(object sender, EventArgs e)
        {
            if (((this.textBox31.Text == "") || ((this.textBox30.Text == "") || (this.textBox8.Text == ""))) || (this.textBox24.Text == ""))
            {
                MessageBox.Show("Incomplete Product Details !!", "Warning Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                int num = 0;
                int count = this.Order_Gridview.Rows.Count;
                int num3 = 0;
                while (true)
                {
                    if (num3 >= count)
                    {
                        if (num > 0)
                        {
                            MessageBox.Show("The same item exists in the Gridview !!\n" + this.textBox24.Text + "\nTry Adding the quantity in the cart", "Error Duplication", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                        {
                            object[] values = new object[] { this.textBox8.Text, this.textBox24.Text, this.textBox31.Text, this.textBox30.Text };
                            this.Order_Gridview.Rows.Add(values);
                        }
                        break;
                    }
                    if (this.Order_Gridview.Rows[num3].Cells[0].Value.ToString() == this.textBox8.Text.Trim())
                    {
                        num++;
                    }
                    num3++;
                }
            }
        }

        private void BtnOrders_ClearCart_Click(object sender, EventArgs e)
        {
            this.Order_Gridview.Rows.Clear();
        }

        public void CompleteOrdersList()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from ordermaster where DeliveryStatus=@status;";
                command.Parameters.AddWithValue("@status", "Delivered");
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No orders have been found !!", "Order Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.Gridview_CompleteOrders.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[9];
                        values[0] = reader[1].ToString();
                        values[1] = reader[2].ToString();
                        values[2] = reader[3].ToString();
                        values[3] = reader[4].ToString();
                        values[4] = reader[5].ToString();
                        values[5] = reader[6].ToString();
                        values[6] = reader[7].ToString();
                        values[7] = reader[8].ToString();
                        values[8] = reader[9].ToString();
                        this.Gridview_CompleteOrders.Rows.Add(values);
                    }
                }
                connection.Close();
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

        public void GetProducts(string ProductId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT a.ProductCode,a.Description From inventorymaster a where a.ProductCode=@Id";
                command.Parameters.AddWithValue("@Id", ProductId);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The product cannot be found !!", "SEARCH RESULTS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox8.Text = reader[0].ToString();
                        this.textBox24.Text = reader[1].ToString();
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
            this.Tab_Suppliers = new TabControl();
            this.TabPage_NewOrder = new TabPage();
            this.Panel_GridviewHolder = new Panel();
            this.Order_Gridview = new DataGridView();
            this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            this.Order_GridviewControls = new Panel();
            this.Btn_SaveOrder = new Button();
            this.BtnOrders_ClearCart = new Button();
            this.button5 = new Button();
            this.GroupBox_NewOrdersDescription = new GroupBox();
            this.textBox8 = new TextBox();
            this.textBox24 = new TextBox();
            this.groupBox2 = new GroupBox();
            this.Btn_RemoveSupplier = new Button();
            this.Btn_PickSupplier = new Button();
            this.textBox26 = new TextBox();
            this.textBox27 = new TextBox();
            this.textBox28 = new TextBox();
            this.textBox29 = new TextBox();
            this.label25 = new Label();
            this.label28 = new Label();
            this.label29 = new Label();
            this.label30 = new Label();
            this.Btn_SearchProduct = new Button();
            this.BtnOrders_Search = new TextBox();
            this.label2 = new Label();
            this.textBox30 = new TextBox();
            this.label31 = new Label();
            this.BtnOrders_AddCart = new Button();
            this.textBox31 = new TextBox();
            this.label32 = new Label();
            this.label33 = new Label();
            this.label34 = new Label();
            this.TabPage_ViewOrder = new TabPage();
            this.dataGridView3 = new DataGridView();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.panel1 = new Panel();
            this.label6 = new Label();
            this.Btn_Vieworder = new Button();
            this.TextBox_OrderId = new TextBox();
            this.Tab_Page_OrdersList = new TabPage();
            this.GridView_OrdersList = new DataGridView();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.Column15 = new DataGridViewTextBoxColumn();
            this.Column16 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.Column14 = new DataGridViewTextBoxColumn();
            this.panel2 = new Panel();
            this.ViewSuppliers_SearchGroupBox = new GroupBox();
            this.radioButton6 = new RadioButton();
            this.radioButton5 = new RadioButton();
            this.radioButton4 = new RadioButton();
            this.radioButton3 = new RadioButton();
            this.label1 = new Label();
            this.TabPage_OrderComplete = new TabPage();
            this.Gridview_CompleteOrders = new DataGridView();
            this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new DataGridViewTextBoxColumn();
            this.panel5 = new Panel();
            this.TabPage_OrdersTrashed = new TabPage();
            this.dataGridView2 = new DataGridView();
            this.dataGridViewTextBoxColumn21 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new DataGridViewTextBoxColumn();
            this.panel3 = new Panel();
            this.label3 = new Label();
            this.button1 = new Button();
            this.textBox1 = new TextBox();
            this.Tab_Suppliers.SuspendLayout();
            this.TabPage_NewOrder.SuspendLayout();
            this.Panel_GridviewHolder.SuspendLayout();
            ((ISupportInitialize) this.Order_Gridview).BeginInit();
            this.Order_GridviewControls.SuspendLayout();
            this.GroupBox_NewOrdersDescription.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TabPage_ViewOrder.SuspendLayout();
            ((ISupportInitialize) this.dataGridView3).BeginInit();
            this.panel1.SuspendLayout();
            this.Tab_Page_OrdersList.SuspendLayout();
            ((ISupportInitialize) this.GridView_OrdersList).BeginInit();
            this.panel2.SuspendLayout();
            this.ViewSuppliers_SearchGroupBox.SuspendLayout();
            this.TabPage_OrderComplete.SuspendLayout();
            ((ISupportInitialize) this.Gridview_CompleteOrders).BeginInit();
            this.TabPage_OrdersTrashed.SuspendLayout();
            ((ISupportInitialize) this.dataGridView2).BeginInit();
            this.panel3.SuspendLayout();
            base.SuspendLayout();
            this.Tab_Suppliers.Controls.Add(this.TabPage_NewOrder);
            this.Tab_Suppliers.Controls.Add(this.TabPage_ViewOrder);
            this.Tab_Suppliers.Controls.Add(this.Tab_Page_OrdersList);
            this.Tab_Suppliers.Controls.Add(this.TabPage_OrderComplete);
            this.Tab_Suppliers.Controls.Add(this.TabPage_OrdersTrashed);
            this.Tab_Suppliers.Dock = DockStyle.Fill;
            this.Tab_Suppliers.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Tab_Suppliers.Location = new Point(0, 0);
            this.Tab_Suppliers.Margin = new Padding(4);
            this.Tab_Suppliers.Name = "Tab_Suppliers";
            this.Tab_Suppliers.Padding = new Point(0x19, 3);
            this.Tab_Suppliers.SelectedIndex = 0;
            this.Tab_Suppliers.Size = new Size(0x4b0, 600);
            this.Tab_Suppliers.TabIndex = 9;
            this.Tab_Suppliers.Selected += new TabControlEventHandler(this.Tab_Suppliers_Selected);
            this.TabPage_NewOrder.Controls.Add(this.Panel_GridviewHolder);
            this.TabPage_NewOrder.Controls.Add(this.GroupBox_NewOrdersDescription);
            this.TabPage_NewOrder.Location = new Point(4, 0x19);
            this.TabPage_NewOrder.Name = "TabPage_NewOrder";
            this.TabPage_NewOrder.Padding = new Padding(3);
            this.TabPage_NewOrder.Size = new Size(0x4a8, 0x23b);
            this.TabPage_NewOrder.TabIndex = 5;
            this.TabPage_NewOrder.Text = "New Order";
            this.TabPage_NewOrder.UseVisualStyleBackColor = true;
            this.Panel_GridviewHolder.Controls.Add(this.Order_Gridview);
            this.Panel_GridviewHolder.Controls.Add(this.Order_GridviewControls);
            this.Panel_GridviewHolder.Dock = DockStyle.Fill;
            this.Panel_GridviewHolder.Location = new Point(3, 0x81);
            this.Panel_GridviewHolder.Name = "Panel_GridviewHolder";
            this.Panel_GridviewHolder.Size = new Size(0x4a2, 0x1b7);
            this.Panel_GridviewHolder.TabIndex = 3;
            this.Order_Gridview.AllowUserToAddRows = false;
            this.Order_Gridview.AllowUserToDeleteRows = false;
            this.Order_Gridview.AllowUserToResizeColumns = false;
            this.Order_Gridview.AllowUserToResizeRows = false;
            this.Order_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Order_Gridview.BackgroundColor = Color.White;
            this.Order_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.dataGridViewTextBoxColumn8, this.dataGridViewTextBoxColumn9, this.dataGridViewTextBoxColumn10, this.dataGridViewTextBoxColumn11 };
            this.Order_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Order_Gridview.Dock = DockStyle.Fill;
            this.Order_Gridview.EnableHeadersVisualStyles = false;
            this.Order_Gridview.Location = new Point(0, 0);
            this.Order_Gridview.Name = "Order_Gridview";
            this.Order_Gridview.RowHeadersVisible = false;
            this.Order_Gridview.Size = new Size(0x41a, 0x1b7);
            this.Order_Gridview.TabIndex = 2;
            this.dataGridViewTextBoxColumn8.HeaderText = "ProductCode";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.HeaderText = "Description";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.Order_GridviewControls.Controls.Add(this.Btn_SaveOrder);
            this.Order_GridviewControls.Controls.Add(this.BtnOrders_ClearCart);
            this.Order_GridviewControls.Controls.Add(this.button5);
            this.Order_GridviewControls.Dock = DockStyle.Right;
            this.Order_GridviewControls.Location = new Point(0x41a, 0);
            this.Order_GridviewControls.Name = "Order_GridviewControls";
            this.Order_GridviewControls.Size = new Size(0x88, 0x1b7);
            this.Order_GridviewControls.TabIndex = 0;
            this.Btn_SaveOrder.Location = new Point(6, 0x98);
            this.Btn_SaveOrder.Name = "Btn_SaveOrder";
            this.Btn_SaveOrder.Size = new Size(0x7b, 0x25);
            this.Btn_SaveOrder.TabIndex = 0x12;
            this.Btn_SaveOrder.Text = "Save Order";
            this.Btn_SaveOrder.UseVisualStyleBackColor = true;
            this.Btn_SaveOrder.Click += new EventHandler(this.Btn_SaveOrder_Click);
            this.BtnOrders_ClearCart.Location = new Point(6, 0x57);
            this.BtnOrders_ClearCart.Name = "BtnOrders_ClearCart";
            this.BtnOrders_ClearCart.Size = new Size(0x7b, 0x25);
            this.BtnOrders_ClearCart.TabIndex = 0x11;
            this.BtnOrders_ClearCart.Text = "Clear Cart";
            this.BtnOrders_ClearCart.UseVisualStyleBackColor = true;
            this.BtnOrders_ClearCart.Click += new EventHandler(this.BtnOrders_ClearCart_Click);
            this.button5.Location = new Point(6, 6);
            this.button5.Name = "button5";
            this.button5.Size = new Size(0x7b, 0x38);
            this.button5.TabIndex = 0x10;
            this.button5.Text = "Add Low Stock Items";
            this.button5.UseVisualStyleBackColor = true;
            this.GroupBox_NewOrdersDescription.BackColor = SystemColors.ButtonHighlight;
            this.GroupBox_NewOrdersDescription.Controls.Add(this.textBox8);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.textBox24);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.groupBox2);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.Btn_SearchProduct);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.BtnOrders_Search);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.label2);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.textBox30);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.label31);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.BtnOrders_AddCart);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.textBox31);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.label32);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.label33);
            this.GroupBox_NewOrdersDescription.Controls.Add(this.label34);
            this.GroupBox_NewOrdersDescription.Dock = DockStyle.Top;
            this.GroupBox_NewOrdersDescription.Location = new Point(3, 3);
            this.GroupBox_NewOrdersDescription.Name = "GroupBox_NewOrdersDescription";
            this.GroupBox_NewOrdersDescription.Size = new Size(0x4a2, 0x7e);
            this.GroupBox_NewOrdersDescription.TabIndex = 2;
            this.GroupBox_NewOrdersDescription.TabStop = false;
            this.GroupBox_NewOrdersDescription.Text = "Order Description";
            this.textBox8.BackColor = SystemColors.ButtonHighlight;
            this.textBox8.Location = new Point(7, 0x4c);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new Size(0x6a, 0x17);
            this.textBox8.TabIndex = 0x15;
            this.textBox24.BackColor = SystemColors.ButtonHighlight;
            this.textBox24.Location = new Point(0x77, 0x4c);
            this.textBox24.Name = "textBox24";
            this.textBox24.ReadOnly = true;
            this.textBox24.Size = new Size(0xca, 0x17);
            this.textBox24.TabIndex = 20;
            this.groupBox2.BackColor = SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.Btn_RemoveSupplier);
            this.groupBox2.Controls.Add(this.Btn_PickSupplier);
            this.groupBox2.Controls.Add(this.textBox26);
            this.groupBox2.Controls.Add(this.textBox27);
            this.groupBox2.Controls.Add(this.textBox28);
            this.groupBox2.Controls.Add(this.textBox29);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Location = new Point(0x242, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x25d, 0x6d);
            this.groupBox2.TabIndex = 0x10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Supplier Details";
            this.Btn_RemoveSupplier.Location = new Point(0x1d8, 0x3a);
            this.Btn_RemoveSupplier.Name = "Btn_RemoveSupplier";
            this.Btn_RemoveSupplier.Size = new Size(0x7b, 0x25);
            this.Btn_RemoveSupplier.TabIndex = 20;
            this.Btn_RemoveSupplier.Text = "Remove";
            this.Btn_RemoveSupplier.UseVisualStyleBackColor = true;
            this.Btn_RemoveSupplier.Click += new EventHandler(this.Btn_RemoveSupplier_Click);
            this.Btn_PickSupplier.Location = new Point(0x1d8, 14);
            this.Btn_PickSupplier.Name = "Btn_PickSupplier";
            this.Btn_PickSupplier.Size = new Size(0x7b, 0x25);
            this.Btn_PickSupplier.TabIndex = 0x13;
            this.Btn_PickSupplier.Text = "Pick Supplier";
            this.Btn_PickSupplier.UseVisualStyleBackColor = true;
            this.Btn_PickSupplier.Click += new EventHandler(this.Btn_PickSupplier_Click);
            this.textBox26.BackColor = SystemColors.ButtonHighlight;
            this.textBox26.Location = new Point(0x124, 0x41);
            this.textBox26.Name = "textBox26";
            this.textBox26.ReadOnly = true;
            this.textBox26.Size = new Size(0x9b, 0x17);
            this.textBox26.TabIndex = 7;
            this.textBox27.BackColor = SystemColors.ButtonHighlight;
            this.textBox27.Location = new Point(0x124, 0x1f);
            this.textBox27.Name = "textBox27";
            this.textBox27.ReadOnly = true;
            this.textBox27.Size = new Size(0x9b, 0x17);
            this.textBox27.TabIndex = 6;
            this.textBox28.BackColor = SystemColors.ButtonHighlight;
            this.textBox28.Location = new Point(0x3a, 0x41);
            this.textBox28.Name = "textBox28";
            this.textBox28.ReadOnly = true;
            this.textBox28.Size = new Size(0x9b, 0x17);
            this.textBox28.TabIndex = 5;
            this.textBox29.BackColor = SystemColors.ButtonHighlight;
            this.textBox29.Location = new Point(0x54, 0x22);
            this.textBox29.Name = "textBox29";
            this.textBox29.ReadOnly = true;
            this.textBox29.Size = new Size(130, 0x17);
            this.textBox29.TabIndex = 4;
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0xdb, 0x41);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x4c, 0x11);
            this.label25.TabIndex = 3;
            this.label25.Text = "Telephone";
            this.label28.AutoSize = true;
            this.label28.Location = new Point(7, 0x22);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x47, 0x11);
            this.label28.TabIndex = 2;
            this.label28.Text = "SupplierId";
            this.label29.AutoSize = true;
            this.label29.Location = new Point(220, 0x22);
            this.label29.Name = "label29";
            this.label29.Size = new Size(60, 0x11);
            this.label29.TabIndex = 1;
            this.label29.Text = "Address";
            this.label30.AutoSize = true;
            this.label30.Location = new Point(7, 0x41);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x2d, 0x11);
            this.label30.TabIndex = 0;
            this.label30.Text = "Name";
            this.Btn_SearchProduct.Location = new Point(0x135, 0x16);
            this.Btn_SearchProduct.Name = "Btn_SearchProduct";
            this.Btn_SearchProduct.Size = new Size(0x77, 0x1b);
            this.Btn_SearchProduct.TabIndex = 0x13;
            this.Btn_SearchProduct.Text = "Search Product";
            this.Btn_SearchProduct.UseVisualStyleBackColor = true;
            this.Btn_SearchProduct.Click += new EventHandler(this.Btn_SearchProduct_Click);
            this.BtnOrders_Search.Location = new Point(0x68, 0x19);
            this.BtnOrders_Search.Name = "BtnOrders_Search";
            this.BtnOrders_Search.Size = new Size(0xbb, 0x17);
            this.BtnOrders_Search.TabIndex = 0x12;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x1b);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x5e, 0x11);
            this.label2.TabIndex = 0x11;
            this.label2.Text = "Product Code";
            this.textBox30.Location = new Point(0x1bd, 0x4c);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Size(0x51, 0x17);
            this.textBox30.TabIndex = 12;
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x1bf, 0x38);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x3d, 0x11);
            this.label31.TabIndex = 11;
            this.label31.Text = "Quantity";
            this.BtnOrders_AddCart.Location = new Point(450, 0x16);
            this.BtnOrders_AddCart.Name = "BtnOrders_AddCart";
            this.BtnOrders_AddCart.Size = new Size(0x7a, 0x1b);
            this.BtnOrders_AddCart.TabIndex = 8;
            this.BtnOrders_AddCart.Text = "Add To Cart";
            this.BtnOrders_AddCart.UseVisualStyleBackColor = true;
            this.BtnOrders_AddCart.Click += new EventHandler(this.BtnOrders_AddCart_Click);
            this.textBox31.Location = new Point(0x153, 0x4c);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Size(0x59, 0x17);
            this.textBox31.TabIndex = 5;
            this.label32.AutoSize = true;
            this.label32.Location = new Point(0x150, 0x38);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x21, 0x11);
            this.label32.TabIndex = 4;
            this.label32.Text = "Unit";
            this.label33.AutoSize = true;
            this.label33.Location = new Point(0x74, 0x38);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x4f, 0x11);
            this.label33.TabIndex = 2;
            this.label33.Text = "Description";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(4, 0x38);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0x5e, 0x11);
            this.label34.TabIndex = 0;
            this.label34.Text = "Product Code";
            this.TabPage_ViewOrder.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_ViewOrder.Controls.Add(this.dataGridView3);
            this.TabPage_ViewOrder.Controls.Add(this.panel1);
            this.TabPage_ViewOrder.Location = new Point(4, 0x19);
            this.TabPage_ViewOrder.Name = "TabPage_ViewOrder";
            this.TabPage_ViewOrder.Padding = new Padding(3);
            this.TabPage_ViewOrder.Size = new Size(0x4a8, 0x23b);
            this.TabPage_ViewOrder.TabIndex = 4;
            this.TabPage_ViewOrder.Text = "View Order";
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.BackgroundColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn5 };
            this.dataGridView3.Columns.AddRange(columnArray2);
            this.dataGridView3.Dock = DockStyle.Left;
            this.dataGridView3.EnableHeadersVisualStyles = false;
            this.dataGridView3.Location = new Point(3, 60);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.Size = new Size(0x310, 0x1fc);
            this.dataGridView3.TabIndex = 4;
            this.dataGridViewTextBoxColumn1.HeaderText = "ProductCode";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn3.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn5.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.panel1.BackColor = SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.Btn_Vieworder);
            this.panel1.Controls.Add(this.TextBox_OrderId);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x4a2, 0x39);
            this.panel1.TabIndex = 3;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 0x11);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x63, 0x11);
            this.label6.TabIndex = 0;
            this.label6.Text = "Order Number";
            this.Btn_Vieworder.Location = new Point(0x138, 14);
            this.Btn_Vieworder.Name = "Btn_Vieworder";
            this.Btn_Vieworder.Size = new Size(0x88, 0x1b);
            this.Btn_Vieworder.TabIndex = 2;
            this.Btn_Vieworder.Text = "Find Order";
            this.Btn_Vieworder.UseVisualStyleBackColor = true;
            this.Btn_Vieworder.Click += new EventHandler(this.Btn_Vieworder_Click);
            this.TextBox_OrderId.Location = new Point(0x7e, 0x11);
            this.TextBox_OrderId.Name = "TextBox_OrderId";
            this.TextBox_OrderId.Size = new Size(0xa3, 0x17);
            this.TextBox_OrderId.TabIndex = 1;
            this.Tab_Page_OrdersList.Controls.Add(this.GridView_OrdersList);
            this.Tab_Page_OrdersList.Controls.Add(this.panel2);
            this.Tab_Page_OrdersList.Location = new Point(4, 0x19);
            this.Tab_Page_OrdersList.Name = "Tab_Page_OrdersList";
            this.Tab_Page_OrdersList.Padding = new Padding(3);
            this.Tab_Page_OrdersList.Size = new Size(0x4a8, 0x23b);
            this.Tab_Page_OrdersList.TabIndex = 0;
            this.Tab_Page_OrdersList.Text = "Orders List";
            this.Tab_Page_OrdersList.UseVisualStyleBackColor = true;
            this.GridView_OrdersList.AllowUserToAddRows = false;
            this.GridView_OrdersList.AllowUserToDeleteRows = false;
            this.GridView_OrdersList.AllowUserToResizeColumns = false;
            this.GridView_OrdersList.AllowUserToResizeRows = false;
            this.GridView_OrdersList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.GridView_OrdersList.BackgroundColor = SystemColors.ButtonHighlight;
            this.GridView_OrdersList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray3 = new DataGridViewColumn[9];
            columnArray3[0] = this.Column8;
            columnArray3[1] = this.Column9;
            columnArray3[2] = this.Column10;
            columnArray3[3] = this.Column11;
            columnArray3[4] = this.Column12;
            columnArray3[5] = this.Column15;
            columnArray3[6] = this.Column16;
            columnArray3[7] = this.Column13;
            columnArray3[8] = this.Column14;
            this.GridView_OrdersList.Columns.AddRange(columnArray3);
            this.GridView_OrdersList.Dock = DockStyle.Fill;
            this.GridView_OrdersList.EnableHeadersVisualStyles = false;
            this.GridView_OrdersList.Location = new Point(0xa6, 3);
            this.GridView_OrdersList.Name = "GridView_OrdersList";
            this.GridView_OrdersList.ReadOnly = true;
            this.GridView_OrdersList.RowHeadersVisible = false;
            this.GridView_OrdersList.Size = new Size(0x3ff, 0x235);
            this.GridView_OrdersList.TabIndex = 6;
            this.Column8.HeaderText = "OrderId";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column9.HeaderText = "OrderDate";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column10.HeaderText = "ItemsCount";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column11.HeaderText = "GrossAmount";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column12.HeaderText = "ApprovalStatus";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column15.HeaderText = "DateApproved";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column16.HeaderText = "ApprovedBy";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column13.HeaderText = "DeliveryStatus";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column14.HeaderText = "DeliveryId";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.panel2.BackColor = SystemColors.ButtonHighlight;
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.ViewSuppliers_SearchGroupBox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = DockStyle.Left;
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xa3, 0x235);
            this.panel2.TabIndex = 4;
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton6);
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton5);
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton4);
            this.ViewSuppliers_SearchGroupBox.Controls.Add(this.radioButton3);
            this.ViewSuppliers_SearchGroupBox.Location = new Point(4, 0x21);
            this.ViewSuppliers_SearchGroupBox.Name = "ViewSuppliers_SearchGroupBox";
            this.ViewSuppliers_SearchGroupBox.Size = new Size(0x9b, 0x99);
            this.ViewSuppliers_SearchGroupBox.TabIndex = 2;
            this.ViewSuppliers_SearchGroupBox.TabStop = false;
            this.ViewSuppliers_SearchGroupBox.Text = "Filter By";
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new Point(6, 0x1b);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new Size(60, 0x15);
            this.radioButton6.TabIndex = 3;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "None";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new Point(6, 0x75);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new Size(0x4a, 0x15);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Gender";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new Point(6, 0x57);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new Size(0x88, 0x15);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Registration Date";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new Point(6, 0x39);
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
            this.label1.Size = new Size(0xa1, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter Orders";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.TabPage_OrderComplete.Controls.Add(this.Gridview_CompleteOrders);
            this.TabPage_OrderComplete.Controls.Add(this.panel5);
            this.TabPage_OrderComplete.Location = new Point(4, 0x19);
            this.TabPage_OrderComplete.Name = "TabPage_OrderComplete";
            this.TabPage_OrderComplete.Padding = new Padding(3);
            this.TabPage_OrderComplete.Size = new Size(0x4a8, 0x23b);
            this.TabPage_OrderComplete.TabIndex = 3;
            this.TabPage_OrderComplete.Text = "Orders Complete";
            this.TabPage_OrderComplete.UseVisualStyleBackColor = true;
            this.Gridview_CompleteOrders.AllowUserToAddRows = false;
            this.Gridview_CompleteOrders.AllowUserToDeleteRows = false;
            this.Gridview_CompleteOrders.AllowUserToResizeColumns = false;
            this.Gridview_CompleteOrders.AllowUserToResizeRows = false;
            this.Gridview_CompleteOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Gridview_CompleteOrders.BackgroundColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.Gridview_CompleteOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray4 = new DataGridViewColumn[9];
            columnArray4[0] = this.dataGridViewTextBoxColumn12;
            columnArray4[1] = this.dataGridViewTextBoxColumn13;
            columnArray4[2] = this.dataGridViewTextBoxColumn14;
            columnArray4[3] = this.dataGridViewTextBoxColumn15;
            columnArray4[4] = this.dataGridViewTextBoxColumn16;
            columnArray4[5] = this.dataGridViewTextBoxColumn17;
            columnArray4[6] = this.dataGridViewTextBoxColumn18;
            columnArray4[7] = this.dataGridViewTextBoxColumn19;
            columnArray4[8] = this.dataGridViewTextBoxColumn20;
            this.Gridview_CompleteOrders.Columns.AddRange(columnArray4);
            this.Gridview_CompleteOrders.Dock = DockStyle.Fill;
            this.Gridview_CompleteOrders.EnableHeadersVisualStyles = false;
            this.Gridview_CompleteOrders.Location = new Point(0x9f, 3);
            this.Gridview_CompleteOrders.Name = "Gridview_CompleteOrders";
            this.Gridview_CompleteOrders.ReadOnly = true;
            this.Gridview_CompleteOrders.RowHeadersVisible = false;
            this.Gridview_CompleteOrders.Size = new Size(0x406, 0x235);
            this.Gridview_CompleteOrders.TabIndex = 7;
            this.dataGridViewTextBoxColumn12.HeaderText = "OrderId";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.HeaderText = "OrderDate";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.HeaderText = "ItemsCount";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.HeaderText = "GrossAmount";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.HeaderText = "ApprovalStatus";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.HeaderText = "DateApproved";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.HeaderText = "ApprovedBy";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.HeaderText = "DeliveryStatus";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.HeaderText = "DeliveryId";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.panel5.BackColor = SystemColors.ButtonHighlight;
            this.panel5.Dock = DockStyle.Left;
            this.panel5.Location = new Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x9c, 0x235);
            this.panel5.TabIndex = 6;
            this.TabPage_OrdersTrashed.Controls.Add(this.dataGridView2);
            this.TabPage_OrdersTrashed.Controls.Add(this.panel3);
            this.TabPage_OrdersTrashed.Location = new Point(4, 0x19);
            this.TabPage_OrdersTrashed.Name = "TabPage_OrdersTrashed";
            this.TabPage_OrdersTrashed.Padding = new Padding(3);
            this.TabPage_OrdersTrashed.Size = new Size(0x4a8, 0x23b);
            this.TabPage_OrdersTrashed.TabIndex = 7;
            this.TabPage_OrdersTrashed.Text = "Orders Trash";
            this.TabPage_OrdersTrashed.UseVisualStyleBackColor = true;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.BackgroundColor = Color.FromArgb(0xff, 0xff, 0xc0);
            this.dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray5 = new DataGridViewColumn[] { this.dataGridViewTextBoxColumn21, this.dataGridViewTextBoxColumn22, this.dataGridViewTextBoxColumn23, this.dataGridViewTextBoxColumn24, this.dataGridViewTextBoxColumn25, this.dataGridViewTextBoxColumn26, this.dataGridViewTextBoxColumn27 };
            this.dataGridView2.Columns.AddRange(columnArray5);
            this.dataGridView2.Dock = DockStyle.Fill;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new Point(3, 60);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new Size(0x4a2, 0x1fc);
            this.dataGridView2.TabIndex = 6;
            this.dataGridViewTextBoxColumn21.HeaderText = "ProductCode";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn22.HeaderText = "Description";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn23.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn24.HeaderText = "UnitPrice";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn25.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn26.HeaderText = "Total";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn27.HeaderText = "Comments";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.panel3.BackColor = SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = DockStyle.Top;
            this.panel3.Location = new Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x4a2, 0x39);
            this.panel3.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x11);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x63, 0x11);
            this.label3.TabIndex = 0;
            this.label3.Text = "Order Number";
            this.button1.Location = new Point(0x138, 14);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x88, 0x1b);
            this.button1.TabIndex = 2;
            this.button1.Text = "Find Order";
            this.button1.UseVisualStyleBackColor = true;
            this.textBox1.Location = new Point(0x7f, 0x11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0xa3, 0x17);
            this.textBox1.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x4b0, 600);
            base.Controls.Add(this.Tab_Suppliers);
            base.Name = "Lpo";
            base.Load += new EventHandler(this.Lpo_Load);
            this.Tab_Suppliers.ResumeLayout(false);
            this.TabPage_NewOrder.ResumeLayout(false);
            this.Panel_GridviewHolder.ResumeLayout(false);
            ((ISupportInitialize) this.Order_Gridview).EndInit();
            this.Order_GridviewControls.ResumeLayout(false);
            this.GroupBox_NewOrdersDescription.ResumeLayout(false);
            this.GroupBox_NewOrdersDescription.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TabPage_ViewOrder.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView3).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Tab_Page_OrdersList.ResumeLayout(false);
            ((ISupportInitialize) this.GridView_OrdersList).EndInit();
            this.panel2.ResumeLayout(false);
            this.ViewSuppliers_SearchGroupBox.ResumeLayout(false);
            this.ViewSuppliers_SearchGroupBox.PerformLayout();
            this.TabPage_OrderComplete.ResumeLayout(false);
            ((ISupportInitialize) this.Gridview_CompleteOrders).EndInit();
            this.TabPage_OrdersTrashed.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView2).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            base.ResumeLayout(false);
        }

        public void InsertOrdeMaster(int OrderNo)
        {
            MySqlTransaction transaction = null;
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                transaction = connection.BeginTransaction();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.Transaction = transaction;
                command.CommandText = "INSERT INTO ordermaster (OrderID,Orderdate,OrderItemsCount,SupplierId,TargetName,TargetAddress,TargetPhone,DeliveryStatus,DeliveryId) VALUES (@OrderID,@Orderdate,@OrderItemsCount,@SupplierId,@TargetName,@TargetAddress,@TargetPhone,@DeliveryStatus,@DeliveryId);";
                command.Parameters.AddWithValue("@OrderID", OrderNo);
                command.Parameters.AddWithValue("@Orderdate", Program.CurrentDateTime());
                command.Parameters.AddWithValue("@OrderItemsCount", this.Order_Gridview.Rows.Count);
                command.Parameters.AddWithValue("@SupplierId", this.textBox29.Text);
                command.Parameters.AddWithValue("@TargetName", this.textBox28.Text);
                command.Parameters.AddWithValue("@TargetAddress", this.textBox27.Text);
                command.Parameters.AddWithValue("@TargetPhone", this.textBox26.Text);
                command.Parameters.AddWithValue("@DeliveryStatus", "Undelivered");
                command.Parameters.AddWithValue("@DeliveryId", "");
                int num = command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.Dispose();
                if (num <= 0)
                {
                    MessageBox.Show("Failed to place the order", "Warning message", MessageBoxButtons.OK);
                }
                else
                {
                    int num2 = 0;
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= this.Order_Gridview.Rows.Count)
                        {
                            if (num2 == this.Order_Gridview.Rows.Count)
                            {
                                transaction.Commit();
                                MessageBox.Show("Successfully Placed the order!", "SUCCESS", MessageBoxButtons.OK);
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageBox.Show("Failed To Placed the order!", "WARNING MESSAGE", MessageBoxButtons.OK);
                            }
                            break;
                        }
                        command.CommandText = "INSERT INTO orderitems (OrderId,Orderdate,Pcode,Description,Unit,Quantity,DelStatus) VALUES (@OrderId,@Orderdate,@Pcode,@Description,@Unit,@Quantity,@Delstatus);";
                        command.Parameters.AddWithValue("@OrderId", OrderNo);
                        command.Parameters.AddWithValue("@Orderdate", Program.CurrentDateTime());
                        command.Parameters.AddWithValue("@Pcode", this.Order_Gridview.Rows[num3].Cells[0].Value);
                        command.Parameters.AddWithValue("@Description", this.Order_Gridview.Rows[num3].Cells[1].Value);
                        command.Parameters.AddWithValue("@Unit", this.Order_Gridview.Rows[num3].Cells[2].Value);
                        command.Parameters.AddWithValue("@Quantity", this.Order_Gridview.Rows[num3].Cells[3].Value);
                        command.Parameters.AddWithValue("@Delstatus", "0");
                        int num4 = command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        command.Dispose();
                        num2 += num4;
                        num3++;
                    }
                }
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                MessageBox.Show(exception.Message, "Error message", MessageBoxButtons.OK);
            }
        }

        private void Lpo_Load(object sender, EventArgs e)
        {
        }

        private void Tab_Suppliers_Selected(object sender, TabControlEventArgs e)
        {
            if (this.Tab_Suppliers.SelectedTab.Name == "TabPage_OrderComplete")
            {
                this.CompleteOrdersList();
            }
            if (this.Tab_Suppliers.SelectedTab.Name == "Tab_Page_OrdersList")
            {
                this.ViewOrdersList();
            }
        }

        public void ViewOrdersList()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from ordermaster where DeliveryStatus=@status;";
                command.Parameters.AddWithValue("@status", "UnDelivered");
                MySqlDataReader reader = command.ExecuteReader();
                this.GridView_OrdersList.Rows.Clear();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No orders have been found !!", "Order Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                        values[0] = reader[1].ToString();
                        values[1] = reader[2].ToString();
                        values[2] = reader[3].ToString();
                        values[3] = reader[4].ToString();
                        values[4] = reader[5].ToString();
                        values[5] = reader[6].ToString();
                        values[6] = reader[7].ToString();
                        values[7] = reader[8].ToString();
                        values[8] = reader[9].ToString();
                        this.GridView_OrdersList.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ViewSingleOrder(string OrderId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from orderitems a,ordermaster b where b.OrderID=@orderid and a.OrderId=b.OrderID;";
                command.Parameters.AddWithValue("@status", "Undelivered");
                command.Parameters.AddWithValue("@orderid", OrderId);
                MySqlDataReader reader = command.ExecuteReader();
                this.dataGridView3.Rows.Clear();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No orders have been found !!", "Order Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["Pcode"].ToString(), reader["Description"].ToString(), reader["Unit"].ToString(), reader["Quantity"].ToString() };
                        this.dataGridView3.Rows.Add(values);
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

