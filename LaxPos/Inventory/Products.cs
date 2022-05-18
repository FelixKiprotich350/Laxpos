namespace LaxPos.Inventory
{
    using Bunifu.Framework.UI;
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Products : BunifuForm
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public static int TransactionNo;
        public static DateTime InsertionDate;
        public static string InsertionTime;
        public int UpdateStatus = 0;
        public int DeleteStatus = 0;
        private int x = 0;
        private IContainer components = null;
        private TabPage TabPage_Prices;
        private DataGridView Prices_ProductsGridview;
        private Panel Prices_ControlsPanel;
        private Button Prices_Btn_Search;
        private Label label15;
        private TabPage TabPage_Search;
        private TabControl Tab_Products;
        private TabPage TabPage_ViewProducts;
        private Panel panel8;
        private Panel panel7;
        private DataGridView Gridview_ProductList;
        private Label label11;
        private Panel panel4;
        private Label label12;
        private Panel panel5;
        private TextBox textBox15;
        private Label label17;
        private TextBox textBox13;
        private Label label14;
        private TextBox textBox11;
        private Label label13;
        private Label label20;
        private Button Btn_Delete;
        private Button Btn_DiscardNewInfo;
        private Button Btn_Refresh;
        private Button Btn_Update;
        private TabPage Tab_Page_SKU;
        private Panel panel6;
        private Panel panel13;
        private DataGridView Gridview_Sku;
        private Button Btn_Sku_searchProduct;
        private TextBox Txt_sEarchProductIdSKU;
        private Label label30;
        private GroupBox groupBox2;
        private RadioButton SKU_RadioButton_BelowLevel;
        private RadioButton SKU_RadioButton_AboveLevel;
        private RadioButton SKU_RadioButton_ViewAll;
        private TextBox textBox28;
        private Label label33;
        private TextBox textBox29;
        private Label label34;
        private TextBox textBox19;
        private Label label22;
        private TextBox textBox20;
        private Label label23;
        private Label label26;
        private TextBox textBox24;
        private Label label27;
        private TextBox textBox27;
        private Label label32;
        private TextBox textBox32;
        private Label label37;
        private Label label36;
        private DataGridViewTextBoxColumn Column30;
        private DataGridViewTextBoxColumn Column31;
        private DataGridViewTextBoxColumn Column37;
        private DataGridViewTextBoxColumn Column38;
        private DataGridViewTextBoxColumn Column32;
        private DataGridViewTextBoxColumn Column33;
        private DataGridViewTextBoxColumn Column35;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label7;
        private Label label6;
        private TextBox textBox6;
        private TextBox textBox5;
        private Label label9;
        private TextBox textBox8;
        private Button Btn_AddItem;
        private Button Btn_ClearTexts;
        private Label label10;
        private TextBox textBox10;
        private Label label35;
        private TextBox textBox25;
        private Label label31;
        private TextBox textBox12;
        private Label label43;
        private TextBox textBox36;
        private Label label38;
        private TextBox textBox26;
        private Label label1;
        private TextBox textBox7;
        private Label label8;
        private TextBox textBox9;
        private Button Btn_SaveItems;
        private Button Btn_ClearGridview;
        private DataGridView Products_Gridview;
        private DataGridViewTextBoxColumn Column34;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column42;
        private DataGridViewTextBoxColumn Column41;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column2;
        private TextBox Prices_TxtSearch;
        private Button Btn_SearchProduct;
        public TextBox Txt_SearchProductId;
        private TextBox textBox14;
        private Label label16;
        private Button Btn_SearchProductDetails;
        private TextBox textBox30;
        private Label label39;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column25;
        private DataGridViewTextBoxColumn Column21;
        private DataGridViewTextBoxColumn Column22;
        private DataGridViewTextBoxColumn Column36;
        private DataGridViewTextBoxColumn Column24;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column23;
        private DataGridViewTextBoxColumn Column17;
        private TextBox textBox34;
        private Label label41;
        private TextBox textBox33;
        private Label label40;
        private TextBox textBox35;
        private Label label42;
        private TextBox textBox37;
        private Label label44;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column20;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column26;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column27;
        private Timer timer1;
        private ListBox listBox1;
        private TextBox textBox16;
        private TextBox textBox39;
        private Label label47;
        private TextBox textBox38;
        private Label label46;
        private TextBox textBox23;
        private Label label45;
        private TextBox textBox22;
        private Label label24;
        private TextBox textBox18;
        private Label label19;
        private TextBox textBox31;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private TextBox textBox17;
        private Label label18;
        private TextBox textBox21;
        private Label label21;
        private TextBox textBox40;
        private Label label25;
        private TextBox textBox41;
        private Label label28;
        private TextBox textBox42;
        private Label label29;

        public Products()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.Txt_SearchProductId.AutoCompleteCustomSource = Program.MasterProductList;
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to DELETE The product ?", "CONFIRMATION BOX", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.DeleteStatus = 0;
                this.DeleteProduct(this.textBox11.Text);
                if (this.DeleteStatus == 1)
                {
                    this.ClearTexts();
                    MessageBox.Show("You have successfully Deleted The product !!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (this.DeleteStatus == 2)
                {
                    MessageBox.Show("The product Has been partialy Deleted. The price,unit,tax and discount could not be Deleted !! ", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Failed to delete the product !", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Btn_DiscardNewInfo_Click(object sender, EventArgs e)
        {
            this.ClearTexts();
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            this.GetProductsInfo(this.Txt_SearchProductId.Text);
        }

        private void Btn_SearchProduct_Click(object sender, EventArgs e)
        {
            if (this.Txt_SearchProductId.Text != "")
            {
                this.GetProductsInfo(this.Txt_SearchProductId.Text);
            }
        }

        private void Btn_SearchProductDetails_Click(object sender, EventArgs e)
        {
            if (this.textBox30.Text != "")
            {
                this.ViewProducts(" where a.ProductCode=@ProductCode;");
            }
        }

        private void Btn_Sku_searchProduct_Click(object sender, EventArgs e)
        {
            if (this.Txt_sEarchProductIdSKU.Text.ToString() != "")
            {
                this.GetSKUList(" where a.ProductCode=@id;", "SpecificProduct");
            }
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to update The product details ?", "CONFIRMATION BOX", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.UpdateStatus = 0;
                this.UpdateProduct(this.textBox11.Text.Trim());
                if (this.UpdateStatus == 1)
                {
                    this.GetProductsInfo(this.textBox11.Text.Trim());
                    MessageBox.Show("You have successfully updated The product Details", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (this.UpdateStatus == 2)
                {
                    MessageBox.Show("The product Has been partialy updated. The price,unit,tax and discount could not be updated", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("The product update failed", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        public void ClearTexts()
        {
            this.textBox11.Text = "";
            this.textBox13.Text = "";
            this.textBox33.Text = "";
            this.textBox34.Text = "";
            this.textBox37.Text = "";
            this.textBox35.Text = "";
            this.textBox15.Text = "";
            this.textBox32.Text = "";
            this.textBox14.Text = "";
            this.textBox16.Text = "";
            this.textBox18.Text = "";
            this.textBox29.Text = "";
            this.textBox28.Text = "";
            this.textBox24.Text = "";
            this.textBox27.Text = "";
            this.textBox31.Text = "";
            this.textBox23.Text = "";
            this.textBox22.Text = "";
            this.textBox20.Text = "";
            this.textBox19.Text = "";
            this.textBox38.Text = "";
            this.textBox39.Text = "";
            this.listBox1.Text = "On";
            this.textBox17.Text = "";
            this.textBox21.Text = "";
            this.textBox40.Text = "";
            this.textBox41.Text = "";
            this.textBox42.Text = "";
            this.Txt_SearchProductId.Text = "";
        }

        public void DeleteProduct(string ProductId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("delete from inventorymaster  where ProductCode=@ProductCode and CatMain=@Category;", connection);
                command.Parameters.AddWithValue("@ProductCode", ProductId);
                command.Parameters.AddWithValue("@Category", this.textBox15.Text);
                int num = command.ExecuteNonQuery();
                command.Dispose();
                if (num <= 0)
                {
                    this.DeleteStatus = 0;
                }
                else
                {
                    MySqlCommand command2 = new MySqlCommand("Delete from productprice where ProductCode=@PCode;", connection);
                    command2.Parameters.AddWithValue("@PCode", this.textBox11.Text);
                    int num2 = command2.ExecuteNonQuery();
                    command2.Dispose();
                    this.DeleteStatus = (num <= 0) ? 2 : 1;
                }
            }
            catch (Exception exception)
            {
                string text1;
                IDictionary data = exception.Data;
                if (data != null)
                {
                    text1 = data.ToString();
                }
                else
                {
                    IDictionary local1 = data;
                    text1 = null;
                }
                MessageBox.Show(exception.Message + "\n" + text1, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        public void GetProductsInfo(string ProductId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT a.ProductCode,a.Description,a.CatMain,a.Cat1,a.Cat2,a.StockBalance,a.MinLevel,a.MaxLevel,a.SellStatus,b.SellingUnit,b.SellingUnitPrice,b.Pprice,b.VAT,b.GSST,b.TAX3 FROM inventorymaster a, productprice b where (a.ProductCode=b.ProductCode and a.ProductCode=@Id) OR (a.ProductCode=b.ProductCode and a.Description=@Id)";
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
                        this.textBox11.Text = reader["ProductCode"].ToString();
                        this.textBox13.Text = reader["Description"].ToString();
                        this.textBox15.Text = reader["CatMain"].ToString();
                        this.textBox32.Text = reader["SellingUnit"].ToString();
                        this.textBox14.Text = reader["StockBalance"].ToString();
                        this.textBox29.Text = reader["MinLevel"].ToString();
                        this.textBox28.Text = reader["MaxLevel"].ToString();
                        this.textBox33.Text = reader["Cat1"].ToString();
                        this.textBox34.Text = reader["Cat2"].ToString();
                        this.textBox16.Text = reader["SellStatus"].ToString();
                        this.textBox18.Text = reader["Pprice"].ToString();
                        this.textBox23.Text = reader["SellingUnitPrice"].ToString();
                        this.textBox22.Text = reader["GSST"].ToString();
                        this.textBox38.Text = reader["VAT"].ToString();
                        this.textBox39.Text = reader["TAX3"].ToString();
                        this.textBox24.Text = reader["CatMain"].ToString();
                        this.textBox37.Text = reader["Cat1"].ToString();
                        this.textBox35.Text = reader["Cat2"].ToString();
                        this.textBox27.Text = reader["Description"].ToString();
                        this.textBox31.Text = reader["SellingUnit"].ToString();
                        this.textBox20.Text = reader["MinLevel"].ToString();
                        this.textBox19.Text = reader["MaxLevel"].ToString();
                        this.textBox17.Text = reader["TAX3"].ToString();
                        this.textBox21.Text = reader["VAT"].ToString();
                        this.textBox40.Text = reader["SellingUnitPrice"].ToString();
                        this.textBox41.Text = reader["GSST"].ToString();
                        this.textBox42.Text = reader["Pprice"].ToString();
                    }
                }
                command.Dispose();
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.Txt_SearchProductId.Text = "";
        }

        public void GetSKUList(string SearchParameter, string FilterType)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT a.ProductCode,a.Description,a.MinLevel,a.MaxLevel,a.LastRestock,a.StockBalance FROM inventorymaster a " + SearchParameter;
                if (FilterType != "")
                {
                    command.Parameters.AddWithValue("@id", this.Txt_sEarchProductIdSKU.Text.ToString());
                }
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    this.Gridview_Sku.Rows.Clear();
                    if (FilterType == "Below")
                    {
                        MessageBox.Show("No Products is below the stock minimum limit !!", "SKU Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("No Products has been found !!", "SKU Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    this.Gridview_Sku.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        string str = "";
                        bool flag3 = int.Parse(reader[5].ToString()) <= int.Parse(reader[2].ToString());
                        str = !flag3 ? (((int.Parse(reader[5].ToString()) <= int.Parse(reader[2].ToString())) || (int.Parse(reader[5].ToString()) >= int.Parse(reader[3].ToString()))) ? "High Stock Balance. Revisit The Product's History" : "Average Stock Balance") : "Low Stock. Order For It";
                        object[] values = new object[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[5].ToString(), Convert.ToDateTime(reader[4].ToString()).ToShortDateString(), str };
                        this.Gridview_Sku.Rows.Add(values);
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
            this.components = new Container();
            this.TabPage_Prices = new TabPage();
            this.Prices_ProductsGridview = new DataGridView();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.Column18 = new DataGridViewTextBoxColumn();
            this.Column20 = new DataGridViewTextBoxColumn();
            this.Column11 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.Column12 = new DataGridViewTextBoxColumn();
            this.Column26 = new DataGridViewTextBoxColumn();
            this.Column15 = new DataGridViewTextBoxColumn();
            this.Column27 = new DataGridViewTextBoxColumn();
            this.Prices_ControlsPanel = new Panel();
            this.Prices_Btn_Search = new Button();
            this.label15 = new Label();
            this.Prices_TxtSearch = new TextBox();
            this.TabPage_Search = new TabPage();
            this.panel5 = new Panel();
            this.groupBox3 = new GroupBox();
            this.textBox17 = new TextBox();
            this.label18 = new Label();
            this.textBox21 = new TextBox();
            this.label21 = new Label();
            this.textBox40 = new TextBox();
            this.label25 = new Label();
            this.textBox41 = new TextBox();
            this.label28 = new Label();
            this.textBox42 = new TextBox();
            this.label29 = new Label();
            this.label27 = new Label();
            this.label32 = new Label();
            this.label36 = new Label();
            this.listBox1 = new ListBox();
            this.textBox27 = new TextBox();
            this.textBox35 = new TextBox();
            this.textBox24 = new TextBox();
            this.label42 = new Label();
            this.label26 = new Label();
            this.textBox37 = new TextBox();
            this.label23 = new Label();
            this.label44 = new Label();
            this.textBox20 = new TextBox();
            this.textBox31 = new TextBox();
            this.label22 = new Label();
            this.textBox19 = new TextBox();
            this.groupBox1 = new GroupBox();
            this.textBox13 = new TextBox();
            this.textBox39 = new TextBox();
            this.label13 = new Label();
            this.label47 = new Label();
            this.textBox11 = new TextBox();
            this.textBox38 = new TextBox();
            this.label14 = new Label();
            this.label46 = new Label();
            this.label17 = new Label();
            this.textBox23 = new TextBox();
            this.textBox15 = new TextBox();
            this.label45 = new Label();
            this.textBox33 = new TextBox();
            this.textBox22 = new TextBox();
            this.label40 = new Label();
            this.label24 = new Label();
            this.textBox14 = new TextBox();
            this.textBox18 = new TextBox();
            this.label19 = new Label();
            this.label16 = new Label();
            this.label37 = new Label();
            this.textBox16 = new TextBox();
            this.textBox32 = new TextBox();
            this.label41 = new Label();
            this.textBox34 = new TextBox();
            this.label20 = new Label();
            this.label34 = new Label();
            this.textBox29 = new TextBox();
            this.label33 = new Label();
            this.textBox28 = new TextBox();
            this.Btn_Update = new Button();
            this.Btn_Delete = new Button();
            this.Btn_DiscardNewInfo = new Button();
            this.Btn_Refresh = new Button();
            this.panel4 = new Panel();
            this.Btn_SearchProduct = new Button();
            this.Txt_SearchProductId = new TextBox();
            this.label12 = new Label();
            this.Tab_Products = new TabControl();
            this.TabPage_ViewProducts = new TabPage();
            this.panel8 = new Panel();
            this.Gridview_ProductList = new DataGridView();
            this.Column19 = new DataGridViewTextBoxColumn();
            this.Column16 = new DataGridViewTextBoxColumn();
            this.Column25 = new DataGridViewTextBoxColumn();
            this.Column21 = new DataGridViewTextBoxColumn();
            this.Column22 = new DataGridViewTextBoxColumn();
            this.Column36 = new DataGridViewTextBoxColumn();
            this.Column24 = new DataGridViewTextBoxColumn();
            this.Column14 = new DataGridViewTextBoxColumn();
            this.Column23 = new DataGridViewTextBoxColumn();
            this.Column17 = new DataGridViewTextBoxColumn();
            this.panel7 = new Panel();
            this.Btn_SearchProductDetails = new Button();
            this.textBox30 = new TextBox();
            this.label39 = new Label();
            this.label11 = new Label();
            this.Tab_Page_SKU = new TabPage();
            this.panel13 = new Panel();
            this.Gridview_Sku = new DataGridView();
            this.Column30 = new DataGridViewTextBoxColumn();
            this.Column31 = new DataGridViewTextBoxColumn();
            this.Column37 = new DataGridViewTextBoxColumn();
            this.Column38 = new DataGridViewTextBoxColumn();
            this.Column32 = new DataGridViewTextBoxColumn();
            this.Column33 = new DataGridViewTextBoxColumn();
            this.Column35 = new DataGridViewTextBoxColumn();
            this.panel6 = new Panel();
            this.groupBox2 = new GroupBox();
            this.SKU_RadioButton_BelowLevel = new RadioButton();
            this.SKU_RadioButton_AboveLevel = new RadioButton();
            this.SKU_RadioButton_ViewAll = new RadioButton();
            this.Btn_Sku_searchProduct = new Button();
            this.Txt_sEarchProductIdSKU = new TextBox();
            this.label30 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.textBox3 = new TextBox();
            this.textBox4 = new TextBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.textBox6 = new TextBox();
            this.textBox5 = new TextBox();
            this.label9 = new Label();
            this.textBox8 = new TextBox();
            this.Btn_AddItem = new Button();
            this.Btn_ClearTexts = new Button();
            this.label10 = new Label();
            this.textBox10 = new TextBox();
            this.label35 = new Label();
            this.textBox25 = new TextBox();
            this.label31 = new Label();
            this.textBox12 = new TextBox();
            this.label43 = new Label();
            this.textBox36 = new TextBox();
            this.label38 = new Label();
            this.textBox26 = new TextBox();
            this.label1 = new Label();
            this.textBox7 = new TextBox();
            this.label8 = new Label();
            this.textBox9 = new TextBox();
            this.Btn_SaveItems = new Button();
            this.Btn_ClearGridview = new Button();
            this.Products_Gridview = new DataGridView();
            this.Column34 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column42 = new DataGridViewTextBoxColumn();
            this.Column41 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.timer1 = new Timer(this.components);
            this.TabPage_Prices.SuspendLayout();
            ((ISupportInitialize) this.Prices_ProductsGridview).BeginInit();
            this.Prices_ControlsPanel.SuspendLayout();
            this.TabPage_Search.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.Tab_Products.SuspendLayout();
            this.TabPage_ViewProducts.SuspendLayout();
            this.panel8.SuspendLayout();
            ((ISupportInitialize) this.Gridview_ProductList).BeginInit();
            this.panel7.SuspendLayout();
            this.Tab_Page_SKU.SuspendLayout();
            this.panel13.SuspendLayout();
            ((ISupportInitialize) this.Gridview_Sku).BeginInit();
            this.panel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.Products_Gridview).BeginInit();
            base.SuspendLayout();
            this.TabPage_Prices.Controls.Add(this.Prices_ProductsGridview);
            this.TabPage_Prices.Controls.Add(this.Prices_ControlsPanel);
            this.TabPage_Prices.Location = new Point(4, 0x1d);
            this.TabPage_Prices.Name = "TabPage_Prices";
            this.TabPage_Prices.Padding = new Padding(3);
            this.TabPage_Prices.Size = new Size(0x4a8, 0x269);
            this.TabPage_Prices.TabIndex = 5;
            this.TabPage_Prices.Text = "ViewProductPrices";
            this.TabPage_Prices.UseVisualStyleBackColor = true;
            this.Prices_ProductsGridview.AllowUserToAddRows = false;
            this.Prices_ProductsGridview.AllowUserToDeleteRows = false;
            this.Prices_ProductsGridview.AllowUserToResizeRows = false;
            this.Prices_ProductsGridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Prices_ProductsGridview.BackgroundColor = Color.White;
            this.Prices_ProductsGridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[9];
            dataGridViewColumns[0] = this.Column10;
            dataGridViewColumns[1] = this.Column18;
            dataGridViewColumns[2] = this.Column20;
            dataGridViewColumns[3] = this.Column11;
            dataGridViewColumns[4] = this.Column13;
            dataGridViewColumns[5] = this.Column12;
            dataGridViewColumns[6] = this.Column26;
            dataGridViewColumns[7] = this.Column15;
            dataGridViewColumns[8] = this.Column27;
            this.Prices_ProductsGridview.Columns.AddRange(dataGridViewColumns);
            this.Prices_ProductsGridview.Dock = DockStyle.Fill;
            this.Prices_ProductsGridview.EnableHeadersVisualStyles = false;
            this.Prices_ProductsGridview.Location = new Point(0xb7, 3);
            this.Prices_ProductsGridview.Name = "Prices_ProductsGridview";
            this.Prices_ProductsGridview.RowHeadersVisible = false;
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.BackColor = Color.White;
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            this.Prices_ProductsGridview.Size = new Size(0x3ee, 0x263);
            this.Prices_ProductsGridview.TabIndex = 0x1a;
            this.Column10.HeaderText = "Pcode";
            this.Column10.Name = "Column10";
            this.Column18.HeaderText = "Description";
            this.Column18.Name = "Column18";
            this.Column20.HeaderText = "Unit";
            this.Column20.Name = "Column20";
            this.Column11.HeaderText = "Selling Price";
            this.Column11.Name = "Column11";
            this.Column13.HeaderText = "Buying Price";
            this.Column13.Name = "Column13";
            this.Column12.HeaderText = "Gsst %";
            this.Column12.Name = "Column12";
            this.Column26.HeaderText = "VAT%";
            this.Column26.Name = "Column26";
            this.Column15.HeaderText = "Tax0";
            this.Column15.Name = "Column15";
            this.Column27.HeaderText = "Discount";
            this.Column27.Name = "Column27";
            this.Prices_ControlsPanel.BackColor = SystemColors.ButtonHighlight;
            this.Prices_ControlsPanel.Controls.Add(this.Prices_Btn_Search);
            this.Prices_ControlsPanel.Controls.Add(this.label15);
            this.Prices_ControlsPanel.Controls.Add(this.Prices_TxtSearch);
            this.Prices_ControlsPanel.Dock = DockStyle.Left;
            this.Prices_ControlsPanel.Location = new Point(3, 3);
            this.Prices_ControlsPanel.Name = "Prices_ControlsPanel";
            this.Prices_ControlsPanel.Size = new Size(180, 0x263);
            this.Prices_ControlsPanel.TabIndex = 0x19;
            this.Prices_Btn_Search.Location = new Point(14, 0x3a);
            this.Prices_Btn_Search.Name = "Prices_Btn_Search";
            this.Prices_Btn_Search.Size = new Size(0x8b, 0x20);
            this.Prices_Btn_Search.TabIndex = 0x1a;
            this.Prices_Btn_Search.Text = "Search Item";
            this.Prices_Btn_Search.UseVisualStyleBackColor = true;
            this.Prices_Btn_Search.Click += new EventHandler(this.Prices_Btn_Search_Click);
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x34, 3);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x44, 0x11);
            this.label15.TabIndex = 0x18;
            this.label15.Text = "ProductId";
            this.Prices_TxtSearch.Location = new Point(3, 0x1a);
            this.Prices_TxtSearch.Name = "Prices_TxtSearch";
            this.Prices_TxtSearch.Size = new Size(0xab, 0x17);
            this.Prices_TxtSearch.TabIndex = 0x1b;
            this.TabPage_Search.AutoScroll = true;
            this.TabPage_Search.Controls.Add(this.panel5);
            this.TabPage_Search.Controls.Add(this.panel4);
            this.TabPage_Search.Location = new Point(4, 0x1d);
            this.TabPage_Search.Name = "TabPage_Search";
            this.TabPage_Search.Padding = new Padding(3);
            this.TabPage_Search.Size = new Size(0x4a8, 0x269);
            this.TabPage_Search.TabIndex = 1;
            this.TabPage_Search.Text = "Search Product";
            this.TabPage_Search.UseVisualStyleBackColor = true;
            this.panel5.BackColor = SystemColors.ButtonHighlight;
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Controls.Add(this.Btn_Update);
            this.panel5.Controls.Add(this.Btn_Delete);
            this.panel5.Controls.Add(this.Btn_DiscardNewInfo);
            this.panel5.Controls.Add(this.Btn_Refresh);
            this.panel5.Dock = DockStyle.Fill;
            this.panel5.Location = new Point(3, 0x3e);
            this.panel5.Name = "panel5";
            this.panel5.Size = new Size(0x4a2, 0x228);
            this.panel5.TabIndex = 15;
            this.groupBox3.Controls.Add(this.textBox17);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.textBox21);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.textBox40);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.textBox41);
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.textBox42);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.label27);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.label36);
            this.groupBox3.Controls.Add(this.listBox1);
            this.groupBox3.Controls.Add(this.textBox27);
            this.groupBox3.Controls.Add(this.textBox35);
            this.groupBox3.Controls.Add(this.textBox24);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.textBox37);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.label44);
            this.groupBox3.Controls.Add(this.textBox20);
            this.groupBox3.Controls.Add(this.textBox31);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.textBox19);
            this.groupBox3.Dock = DockStyle.Left;
            this.groupBox3.Location = new Point(0x184, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x199, 0x228);
            this.groupBox3.TabIndex = 0x66;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Update Panel";
            this.textBox17.BorderStyle = BorderStyle.FixedSingle;
            this.textBox17.Location = new Point(0x83, 0x1b0);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Size(0xf6, 0x17);
            this.textBox17.TabIndex = 110;
            this.textBox17.TextAlign = HorizontalAlignment.Center;
            this.textBox17.KeyPress += new KeyPressEventHandler(this.InputNumbers);
            this.textBox17.Leave += new EventHandler(this.TextboxFocus_Lost);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x19, 430);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x27, 0x11);
            this.label18.TabIndex = 0x6d;
            this.label18.Text = "Tax3";
            this.textBox21.BorderStyle = BorderStyle.FixedSingle;
            this.textBox21.Location = new Point(0x83, 0x1cf);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Size(0xf6, 0x17);
            this.textBox21.TabIndex = 0x6c;
            this.textBox21.TextAlign = HorizontalAlignment.Center;
            this.textBox21.KeyPress += new KeyPressEventHandler(this.InputNumbers);
            this.textBox21.Leave += new EventHandler(this.TextboxFocus_Lost);
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x19, 0x1d1);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x23, 0x11);
            this.label21.TabIndex = 0x6b;
            this.label21.Text = "VAT";
            this.textBox40.BorderStyle = BorderStyle.FixedSingle;
            this.textBox40.Location = new Point(0x83, 0x173);
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Size(0xf6, 0x17);
            this.textBox40.TabIndex = 0x6a;
            this.textBox40.TextAlign = HorizontalAlignment.Center;
            this.textBox40.KeyPress += new KeyPressEventHandler(this.InputNumbers);
            this.textBox40.Leave += new EventHandler(this.TextboxFocus_Lost);
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0x19, 0x175);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x35, 0x11);
            this.label25.TabIndex = 0x69;
            this.label25.Text = "S.Price";
            this.textBox41.BorderStyle = BorderStyle.FixedSingle;
            this.textBox41.Location = new Point(0x83, 0x192);
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Size(0xf6, 0x17);
            this.textBox41.TabIndex = 0x68;
            this.textBox41.TextAlign = HorizontalAlignment.Center;
            this.textBox41.KeyPress += new KeyPressEventHandler(this.InputNumbers);
            this.textBox41.Leave += new EventHandler(this.TextboxFocus_Lost);
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0x19, 0x18f);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x2e, 0x11);
            this.label28.TabIndex = 0x67;
            this.label28.Text = "GSST";
            this.textBox42.BorderStyle = BorderStyle.FixedSingle;
            this.textBox42.Location = new Point(0x83, 0x156);
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Size(0xf6, 0x17);
            this.textBox42.TabIndex = 0x66;
            this.textBox42.TextAlign = HorizontalAlignment.Center;
            this.textBox42.KeyPress += new KeyPressEventHandler(this.InputNumbers);
            this.textBox42.Leave += new EventHandler(this.TextboxFocus_Lost);
            this.label29.AutoSize = true;
            this.label29.Location = new Point(0x19, 0x153);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x35, 0x11);
            this.label29.TabIndex = 0x65;
            this.label29.Text = "B.Price";
            this.label27.AutoSize = true;
            this.label27.Location = new Point(0x19, 0x25);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x63, 0x11);
            this.label27.TabIndex = 0x41;
            this.label27.Text = "Main Category";
            this.label32.AutoSize = true;
            this.label32.Location = new Point(0x19, 0x89);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x4f, 0x11);
            this.label32.TabIndex = 0x3d;
            this.label32.Text = "Description";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(0x19, 0xcd);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x21, 0x11);
            this.label36.TabIndex = 0x4d;
            this.label36.Text = "Unit";
            this.listBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            object[] items = new object[] { "On", "Off" };
            this.listBox1.Items.AddRange(items);
            this.listBox1.Location = new Point(0x83, 0xea);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x51, 0x18);
            this.listBox1.TabIndex = 0x59;
            this.textBox27.Location = new Point(0x83, 0x89);
            this.textBox27.MaxLength = 300;
            this.textBox27.Multiline = true;
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Size(0xf6, 0x39);
            this.textBox27.TabIndex = 0x3e;
            this.textBox35.BorderStyle = BorderStyle.FixedSingle;
            this.textBox35.Location = new Point(0x83, 0x61);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Size(0xf6, 0x17);
            this.textBox35.TabIndex = 0x58;
            this.textBox24.Location = new Point(0x83, 0x25);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Size(0xf6, 0x17);
            this.textBox24.TabIndex = 0x42;
            this.label42.AutoSize = true;
            this.label42.Location = new Point(0x19, 0x61);
            this.label42.Name = "label42";
            this.label42.Size = new Size(0x4d, 0x11);
            this.label42.TabIndex = 0x57;
            this.label42.Text = "Category 2";
            this.label26.AutoSize = true;
            this.label26.Location = new Point(0x19, 0xf1);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x30, 0x11);
            this.label26.TabIndex = 0x43;
            this.label26.Text = "Status";
            this.textBox37.BorderStyle = BorderStyle.FixedSingle;
            this.textBox37.Location = new Point(0x83, 0x42);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Size(0xf6, 0x17);
            this.textBox37.TabIndex = 0x56;
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x19, 0x10f);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x40, 0x11);
            this.label23.TabIndex = 0x49;
            this.label23.Text = "MinLevel";
            this.label44.AutoSize = true;
            this.label44.Location = new Point(0x19, 0x42);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x4d, 0x11);
            this.label44.TabIndex = 0x55;
            this.label44.Text = "Category 1";
            this.textBox20.Location = new Point(0x83, 0x10f);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Size(0xf6, 0x17);
            this.textBox20.TabIndex = 0x4a;
            this.textBox31.Location = new Point(0x83, 0xcd);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Size(0xf6, 0x17);
            this.textBox31.TabIndex = 0x4e;
            this.label22.AutoSize = true;
            this.label22.Location = new Point(0x19, 0x135);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x43, 0x11);
            this.label22.TabIndex = 0x4b;
            this.label22.Text = "MaxLevel";
            this.textBox19.Location = new Point(0x83, 0x134);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Size(0xf6, 0x17);
            this.textBox19.TabIndex = 0x4c;
            this.groupBox1.Controls.Add(this.textBox13);
            this.groupBox1.Controls.Add(this.textBox39);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label47);
            this.groupBox1.Controls.Add(this.textBox11);
            this.groupBox1.Controls.Add(this.textBox38);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label46);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.textBox23);
            this.groupBox1.Controls.Add(this.textBox15);
            this.groupBox1.Controls.Add(this.label45);
            this.groupBox1.Controls.Add(this.textBox33);
            this.groupBox1.Controls.Add(this.textBox22);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.textBox14);
            this.groupBox1.Controls.Add(this.textBox18);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.textBox16);
            this.groupBox1.Controls.Add(this.textBox32);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.textBox34);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.textBox29);
            this.groupBox1.Controls.Add(this.label33);
            this.groupBox1.Controls.Add(this.textBox28);
            this.groupBox1.Dock = DockStyle.Left;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x184, 0x228);
            this.groupBox1.TabIndex = 0x65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Produc Information";
            this.textBox13.BorderStyle = BorderStyle.FixedSingle;
            this.textBox13.Location = new Point(0x72, 0x39);
            this.textBox13.MaxLength = 300;
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new Size(0xf6, 0x39);
            this.textBox13.TabIndex = 0x13;
            this.textBox13.TextAlign = HorizontalAlignment.Center;
            this.textBox39.BorderStyle = BorderStyle.FixedSingle;
            this.textBox39.Location = new Point(0x72, 0x1b2);
            this.textBox39.Name = "textBox39";
            this.textBox39.ReadOnly = true;
            this.textBox39.Size = new Size(0x5c, 0x17);
            this.textBox39.TabIndex = 100;
            this.textBox39.TextAlign = HorizontalAlignment.Center;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(7, 20);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x48, 0x11);
            this.label13.TabIndex = 0x10;
            this.label13.Text = "Product Id";
            this.label47.AutoSize = true;
            this.label47.Location = new Point(0x30, 0x1b2);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x27, 0x11);
            this.label47.TabIndex = 0x63;
            this.label47.Text = "Tax3";
            this.textBox11.Location = new Point(0x72, 20);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new Size(0xf6, 0x17);
            this.textBox11.TabIndex = 0x11;
            this.textBox11.TextAlign = HorizontalAlignment.Center;
            this.textBox38.BorderStyle = BorderStyle.FixedSingle;
            this.textBox38.Location = new Point(0x10c, 0x195);
            this.textBox38.Name = "textBox38";
            this.textBox38.ReadOnly = true;
            this.textBox38.Size = new Size(0x5c, 0x17);
            this.textBox38.TabIndex = 0x62;
            this.textBox38.TextAlign = HorizontalAlignment.Center;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(7, 0x39);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x4f, 0x11);
            this.label14.TabIndex = 0x12;
            this.label14.Text = "Description";
            this.label46.AutoSize = true;
            this.label46.Location = new Point(0xd1, 0x196);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x23, 0x11);
            this.label46.TabIndex = 0x61;
            this.label46.Text = "VAT";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(7, 0x73);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x63, 0x11);
            this.label17.TabIndex = 0x16;
            this.label17.Text = "Main Category";
            this.textBox23.BorderStyle = BorderStyle.FixedSingle;
            this.textBox23.Location = new Point(0x10c, 0x178);
            this.textBox23.Name = "textBox23";
            this.textBox23.ReadOnly = true;
            this.textBox23.Size = new Size(0x5c, 0x17);
            this.textBox23.TabIndex = 0x60;
            this.textBox23.TextAlign = HorizontalAlignment.Center;
            this.textBox15.BorderStyle = BorderStyle.FixedSingle;
            this.textBox15.Location = new Point(0x72, 0x7b);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new Size(0xf6, 0x17);
            this.textBox15.TabIndex = 0x17;
            this.textBox15.TextAlign = HorizontalAlignment.Center;
            this.label45.AutoSize = true;
            this.label45.Location = new Point(0xd1, 0x179);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0x35, 0x11);
            this.label45.TabIndex = 0x5f;
            this.label45.Text = "S.Price";
            this.textBox33.BorderStyle = BorderStyle.FixedSingle;
            this.textBox33.Location = new Point(0x72, 0x9b);
            this.textBox33.Name = "textBox33";
            this.textBox33.ReadOnly = true;
            this.textBox33.Size = new Size(0xf6, 0x17);
            this.textBox33.TabIndex = 0x52;
            this.textBox33.TextAlign = HorizontalAlignment.Center;
            this.textBox22.BorderStyle = BorderStyle.FixedSingle;
            this.textBox22.Location = new Point(0x72, 0x195);
            this.textBox22.Name = "textBox22";
            this.textBox22.ReadOnly = true;
            this.textBox22.Size = new Size(0x5c, 0x17);
            this.textBox22.TabIndex = 0x5e;
            this.textBox22.TextAlign = HorizontalAlignment.Center;
            this.label40.AutoSize = true;
            this.label40.Location = new Point(7, 0x9a);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x4d, 0x11);
            this.label40.TabIndex = 0x51;
            this.label40.Text = "Category 1";
            this.label24.AutoSize = true;
            this.label24.Location = new Point(0x30, 0x194);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x2e, 0x11);
            this.label24.TabIndex = 0x5d;
            this.label24.Text = "GSST";
            this.textBox14.BorderStyle = BorderStyle.FixedSingle;
            this.textBox14.Location = new Point(0x72, 0xdb);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new Size(0xf6, 0x17);
            this.textBox14.TabIndex = 0x15;
            this.textBox14.TextAlign = HorizontalAlignment.Center;
            this.textBox18.BorderStyle = BorderStyle.FixedSingle;
            this.textBox18.Location = new Point(0x72, 0x178);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new Size(0x5c, 0x17);
            this.textBox18.TabIndex = 0x5c;
            this.textBox18.TextAlign = HorizontalAlignment.Center;
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x30, 0x177);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x35, 0x11);
            this.label19.TabIndex = 0x5b;
            this.label19.Text = "B.Price";
            this.label16.AutoSize = true;
            this.label16.Location = new Point(7, 0xd7);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x5e, 0x11);
            this.label16.TabIndex = 20;
            this.label16.Text = "StockBalance";
            this.label37.AutoSize = true;
            this.label37.Location = new Point(7, 0xfc);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0x21, 0x11);
            this.label37.TabIndex = 0x4f;
            this.label37.Text = "Unit";
            this.textBox16.BorderStyle = BorderStyle.FixedSingle;
            this.textBox16.Location = new Point(0x72, 0x11b);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new Size(0xf6, 0x17);
            this.textBox16.TabIndex = 90;
            this.textBox16.TextAlign = HorizontalAlignment.Center;
            this.textBox32.BorderStyle = BorderStyle.FixedSingle;
            this.textBox32.Location = new Point(0x72, 0xfb);
            this.textBox32.Name = "textBox32";
            this.textBox32.ReadOnly = true;
            this.textBox32.Size = new Size(0xf6, 0x17);
            this.textBox32.TabIndex = 80;
            this.textBox32.TextAlign = HorizontalAlignment.Center;
            this.label41.AutoSize = true;
            this.label41.Location = new Point(7, 0xbb);
            this.label41.Name = "label41";
            this.label41.Size = new Size(0x4d, 0x11);
            this.label41.TabIndex = 0x53;
            this.label41.Text = "Category 2";
            this.textBox34.BorderStyle = BorderStyle.FixedSingle;
            this.textBox34.Location = new Point(0x72, 0xbb);
            this.textBox34.Name = "textBox34";
            this.textBox34.ReadOnly = true;
            this.textBox34.Size = new Size(0xf6, 0x17);
            this.textBox34.TabIndex = 0x54;
            this.textBox34.TextAlign = HorizontalAlignment.Center;
            this.label20.AutoSize = true;
            this.label20.Location = new Point(7, 0x11b);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x30, 0x11);
            this.label20.TabIndex = 0x19;
            this.label20.Text = "Status";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(7, 0x13b);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0x40, 0x11);
            this.label34.TabIndex = 0x37;
            this.label34.Text = "MinLevel";
            this.textBox29.BorderStyle = BorderStyle.FixedSingle;
            this.textBox29.Location = new Point(0x72, 0x13b);
            this.textBox29.Name = "textBox29";
            this.textBox29.ReadOnly = true;
            this.textBox29.Size = new Size(0xf6, 0x17);
            this.textBox29.TabIndex = 0x38;
            this.textBox29.TextAlign = HorizontalAlignment.Center;
            this.label33.AutoSize = true;
            this.label33.Location = new Point(7, 0x15c);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x43, 0x11);
            this.label33.TabIndex = 0x39;
            this.label33.Text = "MaxLevel";
            this.textBox28.BorderStyle = BorderStyle.FixedSingle;
            this.textBox28.Location = new Point(0x72, 0x15b);
            this.textBox28.Name = "textBox28";
            this.textBox28.ReadOnly = true;
            this.textBox28.Size = new Size(0xf6, 0x17);
            this.textBox28.TabIndex = 0x3a;
            this.textBox28.TextAlign = HorizontalAlignment.Center;
            this.Btn_Update.Location = new Point(0x34d, 0xc0);
            this.Btn_Update.Name = "Btn_Update";
            this.Btn_Update.Size = new Size(0xa8, 0x21);
            this.Btn_Update.TabIndex = 0x24;
            this.Btn_Update.Text = "Update";
            this.Btn_Update.UseVisualStyleBackColor = true;
            this.Btn_Update.Click += new EventHandler(this.Btn_Update_Click);
            this.Btn_Delete.Location = new Point(0x34d, 0x57);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new Size(0xa8, 0x21);
            this.Btn_Delete.TabIndex = 0x21;
            this.Btn_Delete.Text = "Delete";
            this.Btn_Delete.UseVisualStyleBackColor = true;
            this.Btn_Delete.Click += new EventHandler(this.Btn_Delete_Click);
            this.Btn_DiscardNewInfo.Location = new Point(0x34d, 0x89);
            this.Btn_DiscardNewInfo.Name = "Btn_DiscardNewInfo";
            this.Btn_DiscardNewInfo.Size = new Size(0xa8, 0x21);
            this.Btn_DiscardNewInfo.TabIndex = 0x20;
            this.Btn_DiscardNewInfo.Text = "Discard";
            this.Btn_DiscardNewInfo.UseVisualStyleBackColor = true;
            this.Btn_DiscardNewInfo.Click += new EventHandler(this.Btn_DiscardNewInfo_Click);
            this.Btn_Refresh.Location = new Point(0x34d, 0x23);
            this.Btn_Refresh.Name = "Btn_Refresh";
            this.Btn_Refresh.Size = new Size(0xa8, 0x21);
            this.Btn_Refresh.TabIndex = 0x1f;
            this.Btn_Refresh.Text = "Refressh";
            this.Btn_Refresh.UseVisualStyleBackColor = true;
            this.Btn_Refresh.Click += new EventHandler(this.Btn_Refresh_Click);
            this.panel4.BackColor = SystemColors.ButtonHighlight;
            this.panel4.BorderStyle = BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.Btn_SearchProduct);
            this.panel4.Controls.Add(this.Txt_SearchProductId);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Dock = DockStyle.Top;
            this.panel4.Location = new Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x4a2, 0x3b);
            this.panel4.TabIndex = 14;
            this.Btn_SearchProduct.Location = new Point(0x19f, 9);
            this.Btn_SearchProduct.Name = "Btn_SearchProduct";
            this.Btn_SearchProduct.Size = new Size(0x8b, 0x20);
            this.Btn_SearchProduct.TabIndex = 0x1b;
            this.Btn_SearchProduct.Text = "Search Item";
            this.Btn_SearchProduct.UseVisualStyleBackColor = true;
            this.Btn_SearchProduct.Click += new EventHandler(this.Btn_SearchProduct_Click);
            this.Txt_SearchProductId.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.Txt_SearchProductId.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.Txt_SearchProductId.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Txt_SearchProductId.Location = new Point(100, 14);
            this.Txt_SearchProductId.Name = "Txt_SearchProductId";
            this.Txt_SearchProductId.Size = new Size(0x125, 0x1a);
            this.Txt_SearchProductId.TabIndex = 0x1a;
            this.Txt_SearchProductId.TextChanged += new EventHandler(this.Txt_SearchProductId_TextChanged);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x16, 0x11);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x48, 0x11);
            this.label12.TabIndex = 14;
            this.label12.Text = "Product Id";
            this.Tab_Products.Controls.Add(this.TabPage_Search);
            this.Tab_Products.Controls.Add(this.TabPage_ViewProducts);
            this.Tab_Products.Controls.Add(this.TabPage_Prices);
            this.Tab_Products.Controls.Add(this.Tab_Page_SKU);
            this.Tab_Products.Dock = DockStyle.Fill;
            this.Tab_Products.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Tab_Products.HotTrack = true;
            this.Tab_Products.ItemSize = new Size(100, 0x19);
            this.Tab_Products.Location = new Point(0, 0);
            this.Tab_Products.Multiline = true;
            this.Tab_Products.Name = "Tab_Products";
            this.Tab_Products.Padding = new Point(0x19, 3);
            this.Tab_Products.SelectedIndex = 0;
            this.Tab_Products.Size = new Size(0x4b0, 650);
            this.Tab_Products.TabIndex = 6;
            this.Tab_Products.Selecting += new TabControlCancelEventHandler(this.Tab_Products_Selecting);
            this.Tab_Products.Selected += new TabControlEventHandler(this.Tab_Products_Selected);
            this.TabPage_ViewProducts.Controls.Add(this.panel8);
            this.TabPage_ViewProducts.Controls.Add(this.panel7);
            this.TabPage_ViewProducts.Location = new Point(4, 0x1d);
            this.TabPage_ViewProducts.Name = "TabPage_ViewProducts";
            this.TabPage_ViewProducts.Padding = new Padding(3);
            this.TabPage_ViewProducts.Size = new Size(0x4a8, 0x269);
            this.TabPage_ViewProducts.TabIndex = 7;
            this.TabPage_ViewProducts.Text = "Products List";
            this.TabPage_ViewProducts.UseVisualStyleBackColor = true;
            this.panel8.Controls.Add(this.Gridview_ProductList);
            this.panel8.Dock = DockStyle.Fill;
            this.panel8.Location = new Point(0xdd, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new Size(0x3c8, 0x263);
            this.panel8.TabIndex = 1;
            this.Gridview_ProductList.AllowUserToAddRows = false;
            this.Gridview_ProductList.AllowUserToDeleteRows = false;
            this.Gridview_ProductList.AllowUserToResizeRows = false;
            this.Gridview_ProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Gridview_ProductList.BackgroundColor = Color.White;
            this.Gridview_ProductList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[10];
            columnArray2[0] = this.Column19;
            columnArray2[1] = this.Column16;
            columnArray2[2] = this.Column25;
            columnArray2[3] = this.Column21;
            columnArray2[4] = this.Column22;
            columnArray2[5] = this.Column36;
            columnArray2[6] = this.Column24;
            columnArray2[7] = this.Column14;
            columnArray2[8] = this.Column23;
            columnArray2[9] = this.Column17;
            this.Gridview_ProductList.Columns.AddRange(columnArray2);
            this.Gridview_ProductList.Dock = DockStyle.Fill;
            this.Gridview_ProductList.EnableHeadersVisualStyles = false;
            this.Gridview_ProductList.Location = new Point(0, 0);
            this.Gridview_ProductList.Name = "Gridview_ProductList";
            this.Gridview_ProductList.RowHeadersVisible = false;
            this.Gridview_ProductList.RowTemplate.DefaultCellStyle.BackColor = Color.White;
            this.Gridview_ProductList.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75f);
            this.Gridview_ProductList.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            this.Gridview_ProductList.Size = new Size(0x3c8, 0x263);
            this.Gridview_ProductList.TabIndex = 0;
            this.Column19.HeaderText = "ProductId";
            this.Column19.Name = "Column19";
            this.Column16.HeaderText = "Description";
            this.Column16.Name = "Column16";
            this.Column25.HeaderText = "MainCat";
            this.Column25.Name = "Column25";
            this.Column21.HeaderText = "Category1";
            this.Column21.Name = "Column21";
            this.Column22.HeaderText = "Category2";
            this.Column22.Name = "Column22";
            this.Column36.HeaderText = "LastRestock";
            this.Column36.Name = "Column36";
            this.Column24.HeaderText = "SellStatus";
            this.Column24.Name = "Column24";
            this.Column14.HeaderText = "Unit";
            this.Column14.Name = "Column14";
            this.Column23.HeaderText = "SellingPrice";
            this.Column23.Name = "Column23";
            this.Column17.HeaderText = "StockBalance";
            this.Column17.Name = "Column17";
            this.panel7.BackColor = SystemColors.ButtonHighlight;
            this.panel7.Controls.Add(this.Btn_SearchProductDetails);
            this.panel7.Controls.Add(this.textBox30);
            this.panel7.Controls.Add(this.label39);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Dock = DockStyle.Left;
            this.panel7.Location = new Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new Size(0xda, 0x263);
            this.panel7.TabIndex = 0;
            this.Btn_SearchProductDetails.Location = new Point(0x1c, 0x58);
            this.Btn_SearchProductDetails.Name = "Btn_SearchProductDetails";
            this.Btn_SearchProductDetails.Size = new Size(0x8b, 0x20);
            this.Btn_SearchProductDetails.TabIndex = 0x1d;
            this.Btn_SearchProductDetails.Text = "Search Item";
            this.Btn_SearchProductDetails.UseVisualStyleBackColor = true;
            this.Btn_SearchProductDetails.Click += new EventHandler(this.Btn_SearchProductDetails_Click);
            this.textBox30.Location = new Point(0x12, 0x38);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Size(0xa4, 0x17);
            this.textBox30.TabIndex = 30;
            this.label39.AutoSize = true;
            this.label39.Location = new Point(0x31, 0x21);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x44, 0x11);
            this.label39.TabIndex = 0x1c;
            this.label39.Text = "ProductId";
            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label11.Location = new Point(0x18, 9);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x7e, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Filter Products";
            this.Tab_Page_SKU.Controls.Add(this.panel13);
            this.Tab_Page_SKU.Controls.Add(this.panel6);
            this.Tab_Page_SKU.Location = new Point(4, 0x1d);
            this.Tab_Page_SKU.Name = "Tab_Page_SKU";
            this.Tab_Page_SKU.Padding = new Padding(3);
            this.Tab_Page_SKU.Size = new Size(0x4a8, 0x269);
            this.Tab_Page_SKU.TabIndex = 9;
            this.Tab_Page_SKU.Text = "S.K.U";
            this.Tab_Page_SKU.UseVisualStyleBackColor = true;
            this.panel13.Controls.Add(this.Gridview_Sku);
            this.panel13.Dock = DockStyle.Fill;
            this.panel13.Location = new Point(3, 0x45);
            this.panel13.Name = "panel13";
            this.panel13.Size = new Size(0x4a2, 0x221);
            this.panel13.TabIndex = 1;
            this.Gridview_Sku.AllowUserToAddRows = false;
            this.Gridview_Sku.AllowUserToDeleteRows = false;
            this.Gridview_Sku.AllowUserToResizeRows = false;
            this.Gridview_Sku.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Gridview_Sku.BackgroundColor = SystemColors.ButtonHighlight;
            this.Gridview_Sku.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray3 = new DataGridViewColumn[] { this.Column30, this.Column31, this.Column37, this.Column38, this.Column32, this.Column33, this.Column35 };
            this.Gridview_Sku.Columns.AddRange(columnArray3);
            this.Gridview_Sku.Dock = DockStyle.Fill;
            this.Gridview_Sku.EnableHeadersVisualStyles = false;
            this.Gridview_Sku.Location = new Point(0, 0);
            this.Gridview_Sku.Name = "Gridview_Sku";
            this.Gridview_Sku.ReadOnly = true;
            this.Gridview_Sku.RowHeadersVisible = false;
            this.Gridview_Sku.RowTemplate.DefaultCellStyle.BackColor = SystemColors.ButtonHighlight;
            this.Gridview_Sku.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Gridview_Sku.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
            this.Gridview_Sku.Size = new Size(0x4a2, 0x221);
            this.Gridview_Sku.TabIndex = 0;
            this.Column30.FillWeight = 50f;
            this.Column30.HeaderText = "ProductId";
            this.Column30.Name = "Column30";
            this.Column30.ReadOnly = true;
            this.Column31.FillWeight = 150f;
            this.Column31.HeaderText = "Description";
            this.Column31.Name = "Column31";
            this.Column31.ReadOnly = true;
            this.Column37.FillWeight = 30f;
            this.Column37.HeaderText = "MinLimit";
            this.Column37.Name = "Column37";
            this.Column37.ReadOnly = true;
            this.Column38.FillWeight = 30f;
            this.Column38.HeaderText = "MaxLimit";
            this.Column38.Name = "Column38";
            this.Column38.ReadOnly = true;
            this.Column32.FillWeight = 50f;
            this.Column32.HeaderText = "StockLevel";
            this.Column32.Name = "Column32";
            this.Column32.ReadOnly = true;
            this.Column33.FillWeight = 50f;
            this.Column33.HeaderText = "LastRestock";
            this.Column33.Name = "Column33";
            this.Column33.ReadOnly = true;
            this.Column35.FillWeight = 150f;
            this.Column35.HeaderText = "Suggestion";
            this.Column35.Name = "Column35";
            this.Column35.ReadOnly = true;
            this.panel6.BackColor = SystemColors.ButtonHighlight;
            this.panel6.Controls.Add(this.groupBox2);
            this.panel6.Controls.Add(this.Btn_Sku_searchProduct);
            this.panel6.Controls.Add(this.Txt_sEarchProductIdSKU);
            this.panel6.Controls.Add(this.label30);
            this.panel6.Dock = DockStyle.Top;
            this.panel6.Location = new Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new Size(0x4a2, 0x42);
            this.panel6.TabIndex = 0;
            this.groupBox2.Controls.Add(this.SKU_RadioButton_BelowLevel);
            this.groupBox2.Controls.Add(this.SKU_RadioButton_AboveLevel);
            this.groupBox2.Controls.Add(this.SKU_RadioButton_ViewAll);
            this.groupBox2.Location = new Point(10, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1a3, 0x39);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter By";
            this.SKU_RadioButton_BelowLevel.AutoSize = true;
            this.SKU_RadioButton_BelowLevel.Location = new Point(0x105, 0x19);
            this.SKU_RadioButton_BelowLevel.Name = "SKU_RadioButton_BelowLevel";
            this.SKU_RadioButton_BelowLevel.Size = new Size(0x88, 0x15);
            this.SKU_RadioButton_BelowLevel.TabIndex = 2;
            this.SKU_RadioButton_BelowLevel.TabStop = true;
            this.SKU_RadioButton_BelowLevel.Text = "Below StockLevel";
            this.SKU_RadioButton_BelowLevel.UseVisualStyleBackColor = true;
            this.SKU_RadioButton_BelowLevel.CheckedChanged += new EventHandler(this.SKU_RadioButton_BelowLevel_CheckedChanged);
            this.SKU_RadioButton_AboveLevel.AutoSize = true;
            this.SKU_RadioButton_AboveLevel.Location = new Point(0x65, 0x19);
            this.SKU_RadioButton_AboveLevel.Name = "SKU_RadioButton_AboveLevel";
            this.SKU_RadioButton_AboveLevel.Size = new Size(0x8b, 0x15);
            this.SKU_RadioButton_AboveLevel.TabIndex = 1;
            this.SKU_RadioButton_AboveLevel.TabStop = true;
            this.SKU_RadioButton_AboveLevel.Text = "Above StockLevel";
            this.SKU_RadioButton_AboveLevel.UseVisualStyleBackColor = true;
            this.SKU_RadioButton_AboveLevel.CheckedChanged += new EventHandler(this.SKU_RadioButton_AboveLevel_CheckedChanged);
            this.SKU_RadioButton_ViewAll.AutoSize = true;
            this.SKU_RadioButton_ViewAll.Location = new Point(6, 0x19);
            this.SKU_RadioButton_ViewAll.Name = "SKU_RadioButton_ViewAll";
            this.SKU_RadioButton_ViewAll.Size = new Size(0x4a, 0x15);
            this.SKU_RadioButton_ViewAll.TabIndex = 0;
            this.SKU_RadioButton_ViewAll.TabStop = true;
            this.SKU_RadioButton_ViewAll.Text = "View All";
            this.SKU_RadioButton_ViewAll.UseVisualStyleBackColor = true;
            this.SKU_RadioButton_ViewAll.CheckedChanged += new EventHandler(this.SKU_RadioButton_ViewAll_CheckedChanged);
            this.Btn_Sku_searchProduct.Location = new Point(0x332, 0x11);
            this.Btn_Sku_searchProduct.Name = "Btn_Sku_searchProduct";
            this.Btn_Sku_searchProduct.Size = new Size(0x8f, 30);
            this.Btn_Sku_searchProduct.TabIndex = 0x13;
            this.Btn_Sku_searchProduct.Text = "Search Product";
            this.Btn_Sku_searchProduct.UseVisualStyleBackColor = true;
            this.Btn_Sku_searchProduct.Click += new EventHandler(this.Btn_Sku_searchProduct_Click);
            this.Txt_sEarchProductIdSKU.Location = new Point(0x229, 0x13);
            this.Txt_sEarchProductIdSKU.Name = "Txt_sEarchProductIdSKU";
            this.Txt_sEarchProductIdSKU.Size = new Size(0xf6, 0x17);
            this.Txt_sEarchProductIdSKU.TabIndex = 0x12;
            this.Txt_sEarchProductIdSKU.TextChanged += new EventHandler(this.Txt_sEarchProductIdSKU_TextChanged);
            this.label30.AutoSize = true;
            this.label30.Location = new Point(0x1db, 0x16);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x48, 0x11);
            this.label30.TabIndex = 0x11;
            this.label30.Text = "Product Id";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x11, 0x13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x55, 20);
            this.label2.TabIndex = 1;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xb7, 0x12);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x43, 20);
            this.label3.TabIndex = 2;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x1eb, 0x13);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x6a, 20);
            this.label4.TabIndex = 3;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x2ba, 0x13);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x59, 20);
            this.label5.TabIndex = 4;
            this.textBox1.Location = new Point(0x11, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x98, 20);
            this.textBox1.TabIndex = 5;
            this.textBox2.Location = new Point(0xb7, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x94, 20);
            this.textBox2.TabIndex = 6;
            this.textBox3.Location = new Point(0x1eb, 40);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0xc6, 20);
            this.textBox3.TabIndex = 7;
            this.textBox4.Location = new Point(0x2ba, 40);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0x14f, 20);
            this.textBox4.TabIndex = 8;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x11, 70);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x6d, 20);
            this.label7.TabIndex = 9;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xb3, 0x45);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x36, 20);
            this.label6.TabIndex = 10;
            this.textBox6.Location = new Point(0xb3, 0x5c);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Size(0x98, 20);
            this.textBox6.TabIndex = 11;
            this.textBox5.Location = new Point(0x11, 0x5c);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(0x98, 20);
            this.textBox5.TabIndex = 12;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x161, 0x45);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x4e, 20);
            this.label9.TabIndex = 13;
            this.textBox8.Location = new Point(0x161, 0x5b);
            this.textBox8.MaxLength = 20;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Size(0x5f, 20);
            this.textBox8.TabIndex = 15;
            this.Btn_AddItem.Location = new Point(0, 0);
            this.Btn_AddItem.Name = "Btn_AddItem";
            this.Btn_AddItem.Size = new Size(0x4b, 0x17);
            this.Btn_AddItem.TabIndex = 0;
            this.Btn_ClearTexts.Location = new Point(0, 0);
            this.Btn_ClearTexts.Name = "Btn_ClearTexts";
            this.Btn_ClearTexts.Size = new Size(0x4b, 0x17);
            this.Btn_ClearTexts.TabIndex = 0;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x14d, 0x13);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x49, 20);
            this.label10.TabIndex = 0x12;
            this.textBox10.Location = new Point(0x151, 40);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Size(0x94, 20);
            this.textBox10.TabIndex = 0x13;
            this.label35.AutoSize = true;
            this.label35.Location = new Point(0x1d1, 0x44);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x22, 20);
            this.label35.TabIndex = 20;
            this.textBox25.Location = new Point(0x1d1, 0x5b);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Size(0x54, 20);
            this.textBox25.TabIndex = 0x15;
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x22b, 70);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x48, 20);
            this.label31.TabIndex = 0x16;
            this.textBox12.Location = new Point(0x22b, 0x5c);
            this.textBox12.MaxLength = 20;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Size(0x58, 20);
            this.textBox12.TabIndex = 0x17;
            this.label43.AutoSize = true;
            this.label43.Location = new Point(0x2ac, 70);
            this.label43.Name = "label43";
            this.label43.Size = new Size(0x2c, 20);
            this.label43.TabIndex = 0x22;
            this.textBox36.Location = new Point(0, 0);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new Size(100, 20);
            this.textBox36.TabIndex = 0;
            this.label38.Location = new Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new Size(100, 0x17);
            this.label38.TabIndex = 0;
            this.textBox26.Location = new Point(0, 0);
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Size(100, 20);
            this.textBox26.TabIndex = 0;
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(100, 0x17);
            this.label1.TabIndex = 0;
            this.textBox7.Location = new Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Size(100, 20);
            this.textBox7.TabIndex = 0;
            this.label8.Location = new Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new Size(100, 0x17);
            this.label8.TabIndex = 0;
            this.textBox9.Location = new Point(0, 0);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Size(100, 20);
            this.textBox9.TabIndex = 0;
            this.Btn_SaveItems.Location = new Point(0, 0);
            this.Btn_SaveItems.Name = "Btn_SaveItems";
            this.Btn_SaveItems.Size = new Size(0x4b, 0x17);
            this.Btn_SaveItems.TabIndex = 0;
            this.Btn_ClearGridview.Location = new Point(0, 0);
            this.Btn_ClearGridview.Name = "Btn_ClearGridview";
            this.Btn_ClearGridview.Size = new Size(0x4b, 0x17);
            this.Btn_ClearGridview.TabIndex = 0;
            this.Products_Gridview.Location = new Point(0, 0);
            this.Products_Gridview.Name = "Products_Gridview";
            this.Products_Gridview.Size = new Size(240, 150);
            this.Products_Gridview.TabIndex = 0;
            this.Column34.Name = "Column34";
            this.Column8.Name = "Column8";
            this.Column42.Name = "Column42";
            this.Column41.Name = "Column41";
            this.Column7.Name = "Column7";
            this.Column6.Name = "Column6";
            this.Column5.Name = "Column5";
            this.Column4.Name = "Column4";
            this.Column1.Name = "Column1";
            this.Column9.Name = "Column9";
            this.Column3.Name = "Column3";
            this.Column2.Name = "Column2";
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.Timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x4b0, 650);
            base.Controls.Add(this.Tab_Products);
            base.Name = "Products";
            base.Load += new EventHandler(this.Products_Load);
            this.TabPage_Prices.ResumeLayout(false);
            ((ISupportInitialize) this.Prices_ProductsGridview).EndInit();
            this.Prices_ControlsPanel.ResumeLayout(false);
            this.Prices_ControlsPanel.PerformLayout();
            this.TabPage_Search.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.Tab_Products.ResumeLayout(false);
            this.TabPage_ViewProducts.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((ISupportInitialize) this.Gridview_ProductList).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.Tab_Page_SKU.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            ((ISupportInitialize) this.Gridview_Sku).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((ISupportInitialize) this.Products_Gridview).EndInit();
            base.ResumeLayout(false);
        }

        private void InputNumbers(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || (e.KeyChar.ToString() == "."));
        }

        private void ParsePriceValue(TextBox M)
        {
            decimal num;
            M.Text = !decimal.TryParse(M.Text, out num) ? "0" : num.ToString("N2");
        }

        private void Prices_Btn_Search_Click(object sender, EventArgs e)
        {
            if (this.Prices_TxtSearch.Text != "")
            {
                this.ViewProductsPrice(" where a.ProductCode=@id ");
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
        }

        private void SKU_RadioButton_AboveLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SKU_RadioButton_AboveLevel.Checked)
            {
                this.GetSKUList(" where a.StockBalance>a.MaxLevel;", "Above");
            }
        }

        private void SKU_RadioButton_BelowLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SKU_RadioButton_BelowLevel.Checked)
            {
                this.GetSKUList(" where a.StockBalance<a.MinLevel;", "Below");
            }
        }

        private void SKU_RadioButton_ViewAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SKU_RadioButton_ViewAll.Checked)
            {
                this.GetSKUList(" ;", "All");
            }
        }

        private void Tab_Products_Selected(object sender, TabControlEventArgs e)
        {
            if (this.Tab_Products.SelectedTab.Name == "TabPage_ViewProducts")
            {
                this.ViewProducts(";");
            }
            if (this.Tab_Products.SelectedTab.Name == "TabPage_Prices")
            {
                this.ViewProductsPrice(" ");
            }
            if (this.Tab_Products.SelectedTab.Name == "TabPage_Search")
            {
                this.Txt_SearchProductId.Focus();
                base.ParentForm.AcceptButton = this.Btn_SearchProduct;
            }
            if (this.Tab_Products.SelectedTab.Name == "Tab_Page_SKU")
            {
                this.x = 0;
                this.timer1.Enabled = true;
                this.timer1.Start();
            }
        }

        private void Tab_Products_Selecting(object sender, TabControlCancelEventArgs e)
        {
            base.ParentForm.AcceptButton = null;
        }

        private void TextboxFocus_Lost(object sender, EventArgs e)
        {
            this.ParsePriceValue((TextBox) sender);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.x++;
            if (this.x == 2)
            {
                this.TriggerFunction();
            }
        }

        private void TriggerFunction()
        {
            this.GetSKUList(" ;", "All");
            this.Txt_sEarchProductIdSKU.Focus();
        }

        private void Txt_SearchProductId_TextChanged(object sender, EventArgs e)
        {
            base.ParentForm.AcceptButton = this.Btn_SearchProduct;
        }

        private void Txt_sEarchProductIdSKU_TextChanged(object sender, EventArgs e)
        {
            base.ParentForm.AcceptButton = this.Btn_Sku_searchProduct;
        }

        public void UpdateProduct(string ProductId)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("update inventorymaster SET Description=@Description,CatMain=@catmain,Cat1=@cat1,Cat2=@cat2,MinLevel=@MinLevel,MaxLevel=@MaxLevel where ProductCode=@ProductCode;", connection);
                command.Parameters.AddWithValue("@ProductCode", ProductId);
                command.Parameters.AddWithValue("@Description", this.textBox27.Text);
                command.Parameters.AddWithValue("@catmain", this.textBox24.Text);
                command.Parameters.AddWithValue("@cat1", this.textBox37.Text);
                command.Parameters.AddWithValue("@cat2", this.textBox35.Text);
                command.Parameters.AddWithValue("@status", this.listBox1.Text);
                command.Parameters.AddWithValue("@MinLevel", this.textBox20.Text);
                command.Parameters.AddWithValue("@MaxLevel", this.textBox19.Text);
                int num = command.ExecuteNonQuery();
                command.Dispose();
                if (num <= 0)
                {
                    this.UpdateStatus = 0;
                }
                else
                {
                    MySqlCommand command2 = new MySqlCommand("update productprice set SellingUnit=@Unit, Pprice=@bprice, SellingUnitPrice=@sprice, GSST=@gsst,VAT=@vat,TAX3=@tax3 where ProductCode=@PCode;", connection);
                    command2.Parameters.AddWithValue("@PCode", this.textBox11.Text);
                    command2.Parameters.AddWithValue("@Unit", this.textBox31.Text);
                    command2.Parameters.AddWithValue("@vat", this.textBox21.Text);
                    command2.Parameters.AddWithValue("@sprice", Convert.ToDecimal(this.textBox40.Text));
                    command2.Parameters.AddWithValue("@gsst", this.textBox41.Text);
                    command2.Parameters.AddWithValue("@bprice", Convert.ToDecimal(this.textBox42.Text));
                    command2.Parameters.AddWithValue("@tax3", this.textBox17.Text);
                    int num2 = command2.ExecuteNonQuery();
                    command2.Dispose();
                    this.UpdateStatus = (num <= 0) ? 2 : 1;
                }
            }
            catch (Exception exception)
            {
                string text1;
                IDictionary data = exception.Data;
                if (data != null)
                {
                    text1 = data.ToString();
                }
                else
                {
                    IDictionary local1 = data;
                    text1 = null;
                }
                MessageBox.Show(exception.Message + "\n" + text1, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ViewProducts(string SearchParameter)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select distinct a.ProductCode,a.Description,a.CatMain,a.Cat1,a.Cat2,a.LastRestock,a.SellStatus,b.SellingUnit,b.SellingUnitPrice,a.StockBalance from inventorymaster a left join productprice b on b.ProductCode=a.ProductCode " + SearchParameter;
                command.Parameters.AddWithValue("@ProductCode", this.textBox30.Text);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Products have been found !!", "Products Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.Gridview_ProductList.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[10];
                        values[0] = reader["ProductCode"].ToString();
                        values[1] = reader["Description"].ToString();
                        values[2] = reader["CatMain"].ToString();
                        values[3] = reader["Cat1"].ToString();
                        values[4] = reader["Cat2"].ToString();
                        values[5] = reader["LastRestock"].ToString();
                        values[6] = reader["SellStatus"].ToString();
                        values[7] = reader["SellingUnit"].ToString();
                        values[8] = reader["SellingUnitPrice"].ToString();
                        values[9] = reader["StockBalance"].ToString();
                        this.Gridview_ProductList.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ViewProductsPrice(string ParameterSearch)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from productprice a, inventorymaster b" + ParameterSearch + " where a.ProductCode=b.ProductCode";
                command.Parameters.AddWithValue("@id", this.Prices_TxtSearch.Text.Trim());
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No list of Products Prices have been found !!", "Products Prices Records", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.Prices_ProductsGridview.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[9];
                        values[0] = reader["ProductCode"].ToString();
                        values[1] = reader["Description"].ToString();
                        values[2] = reader["SellingUnit"].ToString();
                        values[3] = reader["SellingUnitPrice"].ToString();
                        values[4] = reader["Pprice"].ToString();
                        values[5] = reader["GSST"].ToString();
                        values[6] = reader["VAT"].ToString();
                        values[7] = reader["TAX3"].ToString();
                        values[8] = reader["Disc"].ToString();
                        this.Prices_ProductsGridview.Rows.Add(values);
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

