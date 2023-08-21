namespace LaxPos.Inventory
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Products : LaxcoForm
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
            this.components = new System.ComponentModel.Container();
            this.TabPage_Prices = new System.Windows.Forms.TabPage();
            this.Prices_ProductsGridview = new System.Windows.Forms.DataGridView();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Prices_ControlsPanel = new System.Windows.Forms.Panel();
            this.Prices_Btn_Search = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.Prices_TxtSearch = new System.Windows.Forms.TextBox();
            this.TabPage_Search = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.Btn_Update = new System.Windows.Forms.Button();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.Btn_DiscardNewInfo = new System.Windows.Forms.Button();
            this.Btn_Refresh = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Btn_SearchProduct = new System.Windows.Forms.Button();
            this.Txt_SearchProductId = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Tab_Products = new System.Windows.Forms.TabControl();
            this.TabPage_ViewProducts = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
            this.Gridview_ProductList = new System.Windows.Forms.DataGridView();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.Btn_SearchProductDetails = new System.Windows.Forms.Button();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Tab_Page_SKU = new System.Windows.Forms.TabPage();
            this.panel13 = new System.Windows.Forms.Panel();
            this.Gridview_Sku = new System.Windows.Forms.DataGridView();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SKU_RadioButton_BelowLevel = new System.Windows.Forms.RadioButton();
            this.SKU_RadioButton_AboveLevel = new System.Windows.Forms.RadioButton();
            this.SKU_RadioButton_ViewAll = new System.Windows.Forms.RadioButton();
            this.Btn_Sku_searchProduct = new System.Windows.Forms.Button();
            this.Txt_sEarchProductIdSKU = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.Btn_AddItem = new System.Windows.Forms.Button();
            this.Btn_ClearTexts = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.Btn_SaveItems = new System.Windows.Forms.Button();
            this.Btn_ClearGridview = new System.Windows.Forms.Button();
            this.Products_Gridview = new System.Windows.Forms.DataGridView();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column42 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column41 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TabPage_Prices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Prices_ProductsGridview)).BeginInit();
            this.Prices_ControlsPanel.SuspendLayout();
            this.TabPage_Search.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.Tab_Products.SuspendLayout();
            this.TabPage_ViewProducts.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gridview_ProductList)).BeginInit();
            this.panel7.SuspendLayout();
            this.Tab_Page_SKU.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gridview_Sku)).BeginInit();
            this.panel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Products_Gridview)).BeginInit();
            this.SuspendLayout();
            // 
            // TabPage_Prices
            // 
            this.TabPage_Prices.Controls.Add(this.Prices_ProductsGridview);
            this.TabPage_Prices.Controls.Add(this.Prices_ControlsPanel);
            this.TabPage_Prices.Location = new System.Drawing.Point(4, 29);
            this.TabPage_Prices.Name = "TabPage_Prices";
            this.TabPage_Prices.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Prices.Size = new System.Drawing.Size(1192, 617);
            this.TabPage_Prices.TabIndex = 5;
            this.TabPage_Prices.Text = "ViewProductPrices";
            this.TabPage_Prices.UseVisualStyleBackColor = true;
            // 
            // Prices_ProductsGridview
            // 
            this.Prices_ProductsGridview.AllowUserToAddRows = false;
            this.Prices_ProductsGridview.AllowUserToDeleteRows = false;
            this.Prices_ProductsGridview.AllowUserToResizeRows = false;
            this.Prices_ProductsGridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Prices_ProductsGridview.BackgroundColor = System.Drawing.Color.White;
            this.Prices_ProductsGridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Prices_ProductsGridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column18,
            this.Column20,
            this.Column11,
            this.Column13,
            this.Column12,
            this.Column26,
            this.Column15,
            this.Column27});
            this.Prices_ProductsGridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Prices_ProductsGridview.EnableHeadersVisualStyles = false;
            this.Prices_ProductsGridview.Location = new System.Drawing.Point(183, 3);
            this.Prices_ProductsGridview.Name = "Prices_ProductsGridview";
            this.Prices_ProductsGridview.RowHeadersVisible = false;
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prices_ProductsGridview.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.Prices_ProductsGridview.Size = new System.Drawing.Size(1006, 611);
            this.Prices_ProductsGridview.TabIndex = 26;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Pcode";
            this.Column10.Name = "Column10";
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Description";
            this.Column18.Name = "Column18";
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Unit";
            this.Column20.Name = "Column20";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Selling Price";
            this.Column11.Name = "Column11";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Buying Price";
            this.Column13.Name = "Column13";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Gsst %";
            this.Column12.Name = "Column12";
            // 
            // Column26
            // 
            this.Column26.HeaderText = "VAT%";
            this.Column26.Name = "Column26";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Tax0";
            this.Column15.Name = "Column15";
            // 
            // Column27
            // 
            this.Column27.HeaderText = "Discount";
            this.Column27.Name = "Column27";
            // 
            // Prices_ControlsPanel
            // 
            this.Prices_ControlsPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Prices_ControlsPanel.Controls.Add(this.Prices_Btn_Search);
            this.Prices_ControlsPanel.Controls.Add(this.label15);
            this.Prices_ControlsPanel.Controls.Add(this.Prices_TxtSearch);
            this.Prices_ControlsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Prices_ControlsPanel.Location = new System.Drawing.Point(3, 3);
            this.Prices_ControlsPanel.Name = "Prices_ControlsPanel";
            this.Prices_ControlsPanel.Size = new System.Drawing.Size(180, 611);
            this.Prices_ControlsPanel.TabIndex = 25;
            // 
            // Prices_Btn_Search
            // 
            this.Prices_Btn_Search.Location = new System.Drawing.Point(14, 58);
            this.Prices_Btn_Search.Name = "Prices_Btn_Search";
            this.Prices_Btn_Search.Size = new System.Drawing.Size(139, 32);
            this.Prices_Btn_Search.TabIndex = 26;
            this.Prices_Btn_Search.Text = "Search Item";
            this.Prices_Btn_Search.UseVisualStyleBackColor = true;
            this.Prices_Btn_Search.Click += new System.EventHandler(this.Prices_Btn_Search_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(52, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 17);
            this.label15.TabIndex = 24;
            this.label15.Text = "ProductId";
            // 
            // Prices_TxtSearch
            // 
            this.Prices_TxtSearch.Location = new System.Drawing.Point(3, 26);
            this.Prices_TxtSearch.Name = "Prices_TxtSearch";
            this.Prices_TxtSearch.Size = new System.Drawing.Size(171, 23);
            this.Prices_TxtSearch.TabIndex = 27;
            // 
            // TabPage_Search
            // 
            this.TabPage_Search.AutoScroll = true;
            this.TabPage_Search.Controls.Add(this.panel5);
            this.TabPage_Search.Controls.Add(this.panel4);
            this.TabPage_Search.Location = new System.Drawing.Point(4, 29);
            this.TabPage_Search.Name = "TabPage_Search";
            this.TabPage_Search.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Search.Size = new System.Drawing.Size(1192, 617);
            this.TabPage_Search.TabIndex = 1;
            this.TabPage_Search.Text = "Search Product";
            this.TabPage_Search.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel5.Controls.Add(this.groupBox3);
            this.panel5.Controls.Add(this.groupBox1);
            this.panel5.Controls.Add(this.Btn_Update);
            this.panel5.Controls.Add(this.Btn_Delete);
            this.panel5.Controls.Add(this.Btn_DiscardNewInfo);
            this.panel5.Controls.Add(this.Btn_Refresh);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 62);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1186, 552);
            this.panel5.TabIndex = 15;
            // 
            // groupBox3
            // 
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
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(388, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 552);
            this.groupBox3.TabIndex = 102;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Update Panel";
            // 
            // textBox17
            // 
            this.textBox17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox17.Location = new System.Drawing.Point(131, 432);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(246, 23);
            this.textBox17.TabIndex = 110;
            this.textBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox17.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputNumbers);
            this.textBox17.Leave += new System.EventHandler(this.TextboxFocus_Lost);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(25, 430);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 17);
            this.label18.TabIndex = 109;
            this.label18.Text = "Tax3";
            // 
            // textBox21
            // 
            this.textBox21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox21.Location = new System.Drawing.Point(131, 463);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(246, 23);
            this.textBox21.TabIndex = 108;
            this.textBox21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox21.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputNumbers);
            this.textBox21.Leave += new System.EventHandler(this.TextboxFocus_Lost);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(25, 465);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(35, 17);
            this.label21.TabIndex = 107;
            this.label21.Text = "VAT";
            // 
            // textBox40
            // 
            this.textBox40.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox40.Location = new System.Drawing.Point(131, 371);
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new System.Drawing.Size(246, 23);
            this.textBox40.TabIndex = 106;
            this.textBox40.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox40.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputNumbers);
            this.textBox40.Leave += new System.EventHandler(this.TextboxFocus_Lost);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(25, 373);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 17);
            this.label25.TabIndex = 105;
            this.label25.Text = "S.Price";
            // 
            // textBox41
            // 
            this.textBox41.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox41.Location = new System.Drawing.Point(131, 402);
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new System.Drawing.Size(246, 23);
            this.textBox41.TabIndex = 104;
            this.textBox41.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox41.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputNumbers);
            this.textBox41.Leave += new System.EventHandler(this.TextboxFocus_Lost);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(25, 399);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(46, 17);
            this.label28.TabIndex = 103;
            this.label28.Text = "GSST";
            // 
            // textBox42
            // 
            this.textBox42.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox42.Location = new System.Drawing.Point(131, 342);
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new System.Drawing.Size(246, 23);
            this.textBox42.TabIndex = 102;
            this.textBox42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox42.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputNumbers);
            this.textBox42.Leave += new System.EventHandler(this.TextboxFocus_Lost);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(25, 339);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 17);
            this.label29.TabIndex = 101;
            this.label29.Text = "B.Price";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(25, 37);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(99, 17);
            this.label27.TabIndex = 65;
            this.label27.Text = "Main Category";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(25, 137);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(79, 17);
            this.label32.TabIndex = 61;
            this.label32.Text = "Description";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(25, 205);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(33, 17);
            this.label36.TabIndex = 77;
            this.label36.Text = "Unit";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Items.AddRange(new object[] {
            "On",
            "Off"});
            this.listBox1.Location = new System.Drawing.Point(131, 234);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(81, 24);
            this.listBox1.TabIndex = 89;
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(131, 137);
            this.textBox27.MaxLength = 300;
            this.textBox27.Multiline = true;
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(246, 57);
            this.textBox27.TabIndex = 62;
            // 
            // textBox35
            // 
            this.textBox35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox35.Location = new System.Drawing.Point(131, 97);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(246, 23);
            this.textBox35.TabIndex = 88;
            // 
            // textBox24
            // 
            this.textBox24.Location = new System.Drawing.Point(131, 37);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(246, 23);
            this.textBox24.TabIndex = 66;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(25, 97);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(77, 17);
            this.label42.TabIndex = 87;
            this.label42.Text = "Category 2";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(25, 241);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(48, 17);
            this.label26.TabIndex = 67;
            this.label26.Text = "Status";
            // 
            // textBox37
            // 
            this.textBox37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox37.Location = new System.Drawing.Point(131, 66);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(246, 23);
            this.textBox37.TabIndex = 86;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(25, 271);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 17);
            this.label23.TabIndex = 73;
            this.label23.Text = "MinLevel";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(25, 66);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(77, 17);
            this.label44.TabIndex = 85;
            this.label44.Text = "Category 1";
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(131, 271);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(246, 23);
            this.textBox20.TabIndex = 74;
            // 
            // textBox31
            // 
            this.textBox31.Location = new System.Drawing.Point(131, 205);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(246, 23);
            this.textBox31.TabIndex = 78;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(25, 309);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 17);
            this.label22.TabIndex = 75;
            this.label22.Text = "MaxLevel";
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(131, 308);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(246, 23);
            this.textBox19.TabIndex = 76;
            // 
            // groupBox1
            // 
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
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 552);
            this.groupBox1.TabIndex = 101;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Produc Information";
            // 
            // textBox13
            // 
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox13.Location = new System.Drawing.Point(114, 57);
            this.textBox13.MaxLength = 300;
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(246, 57);
            this.textBox13.TabIndex = 19;
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox39
            // 
            this.textBox39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox39.Location = new System.Drawing.Point(114, 434);
            this.textBox39.Name = "textBox39";
            this.textBox39.ReadOnly = true;
            this.textBox39.Size = new System.Drawing.Size(92, 23);
            this.textBox39.TabIndex = 100;
            this.textBox39.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 17);
            this.label13.TabIndex = 16;
            this.label13.Text = "Product Id";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(48, 434);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(39, 17);
            this.label47.TabIndex = 99;
            this.label47.Text = "Tax3";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(114, 20);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(246, 23);
            this.textBox11.TabIndex = 17;
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox38
            // 
            this.textBox38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox38.Location = new System.Drawing.Point(268, 405);
            this.textBox38.Name = "textBox38";
            this.textBox38.ReadOnly = true;
            this.textBox38.Size = new System.Drawing.Size(92, 23);
            this.textBox38.TabIndex = 98;
            this.textBox38.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "Description";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(209, 406);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(35, 17);
            this.label46.TabIndex = 97;
            this.label46.Text = "VAT";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 115);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 17);
            this.label17.TabIndex = 22;
            this.label17.Text = "Main Category";
            // 
            // textBox23
            // 
            this.textBox23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox23.Location = new System.Drawing.Point(268, 376);
            this.textBox23.Name = "textBox23";
            this.textBox23.ReadOnly = true;
            this.textBox23.Size = new System.Drawing.Size(92, 23);
            this.textBox23.TabIndex = 96;
            this.textBox23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox15
            // 
            this.textBox15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox15.Location = new System.Drawing.Point(114, 123);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new System.Drawing.Size(246, 23);
            this.textBox15.TabIndex = 23;
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(209, 377);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(53, 17);
            this.label45.TabIndex = 95;
            this.label45.Text = "S.Price";
            // 
            // textBox33
            // 
            this.textBox33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox33.Location = new System.Drawing.Point(114, 155);
            this.textBox33.Name = "textBox33";
            this.textBox33.ReadOnly = true;
            this.textBox33.Size = new System.Drawing.Size(246, 23);
            this.textBox33.TabIndex = 82;
            this.textBox33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox22
            // 
            this.textBox22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox22.Location = new System.Drawing.Point(114, 405);
            this.textBox22.Name = "textBox22";
            this.textBox22.ReadOnly = true;
            this.textBox22.Size = new System.Drawing.Size(92, 23);
            this.textBox22.TabIndex = 94;
            this.textBox22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(7, 154);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(77, 17);
            this.label40.TabIndex = 81;
            this.label40.Text = "Category 1";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(48, 404);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(46, 17);
            this.label24.TabIndex = 93;
            this.label24.Text = "GSST";
            // 
            // textBox14
            // 
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Location = new System.Drawing.Point(114, 219);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(246, 23);
            this.textBox14.TabIndex = 21;
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox18
            // 
            this.textBox18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox18.Location = new System.Drawing.Point(114, 376);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(92, 23);
            this.textBox18.TabIndex = 92;
            this.textBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(48, 375);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 17);
            this.label19.TabIndex = 91;
            this.label19.Text = "B.Price";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 215);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 17);
            this.label16.TabIndex = 20;
            this.label16.Text = "StockBalance";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(7, 252);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(33, 17);
            this.label37.TabIndex = 79;
            this.label37.Text = "Unit";
            // 
            // textBox16
            // 
            this.textBox16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox16.Location = new System.Drawing.Point(114, 283);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new System.Drawing.Size(246, 23);
            this.textBox16.TabIndex = 90;
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox32
            // 
            this.textBox32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox32.Location = new System.Drawing.Point(114, 251);
            this.textBox32.Name = "textBox32";
            this.textBox32.ReadOnly = true;
            this.textBox32.Size = new System.Drawing.Size(246, 23);
            this.textBox32.TabIndex = 80;
            this.textBox32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(7, 187);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(77, 17);
            this.label41.TabIndex = 83;
            this.label41.Text = "Category 2";
            // 
            // textBox34
            // 
            this.textBox34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox34.Location = new System.Drawing.Point(114, 187);
            this.textBox34.Name = "textBox34";
            this.textBox34.ReadOnly = true;
            this.textBox34.Size = new System.Drawing.Size(246, 23);
            this.textBox34.TabIndex = 84;
            this.textBox34.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 283);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 17);
            this.label20.TabIndex = 25;
            this.label20.Text = "Status";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(7, 315);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(64, 17);
            this.label34.TabIndex = 55;
            this.label34.Text = "MinLevel";
            // 
            // textBox29
            // 
            this.textBox29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox29.Location = new System.Drawing.Point(114, 315);
            this.textBox29.Name = "textBox29";
            this.textBox29.ReadOnly = true;
            this.textBox29.Size = new System.Drawing.Size(246, 23);
            this.textBox29.TabIndex = 56;
            this.textBox29.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(7, 348);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(67, 17);
            this.label33.TabIndex = 57;
            this.label33.Text = "MaxLevel";
            // 
            // textBox28
            // 
            this.textBox28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox28.Location = new System.Drawing.Point(114, 347);
            this.textBox28.Name = "textBox28";
            this.textBox28.ReadOnly = true;
            this.textBox28.Size = new System.Drawing.Size(246, 23);
            this.textBox28.TabIndex = 58;
            this.textBox28.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Btn_Update
            // 
            this.Btn_Update.Location = new System.Drawing.Point(845, 192);
            this.Btn_Update.Name = "Btn_Update";
            this.Btn_Update.Size = new System.Drawing.Size(168, 33);
            this.Btn_Update.TabIndex = 36;
            this.Btn_Update.Text = "Update";
            this.Btn_Update.UseVisualStyleBackColor = true;
            this.Btn_Update.Click += new System.EventHandler(this.Btn_Update_Click);
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.Location = new System.Drawing.Point(845, 87);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(168, 33);
            this.Btn_Delete.TabIndex = 33;
            this.Btn_Delete.Text = "Delete";
            this.Btn_Delete.UseVisualStyleBackColor = true;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // Btn_DiscardNewInfo
            // 
            this.Btn_DiscardNewInfo.Location = new System.Drawing.Point(845, 137);
            this.Btn_DiscardNewInfo.Name = "Btn_DiscardNewInfo";
            this.Btn_DiscardNewInfo.Size = new System.Drawing.Size(168, 33);
            this.Btn_DiscardNewInfo.TabIndex = 32;
            this.Btn_DiscardNewInfo.Text = "Discard";
            this.Btn_DiscardNewInfo.UseVisualStyleBackColor = true;
            this.Btn_DiscardNewInfo.Click += new System.EventHandler(this.Btn_DiscardNewInfo_Click);
            // 
            // Btn_Refresh
            // 
            this.Btn_Refresh.Location = new System.Drawing.Point(845, 35);
            this.Btn_Refresh.Name = "Btn_Refresh";
            this.Btn_Refresh.Size = new System.Drawing.Size(168, 33);
            this.Btn_Refresh.TabIndex = 31;
            this.Btn_Refresh.Text = "Refressh";
            this.Btn_Refresh.UseVisualStyleBackColor = true;
            this.Btn_Refresh.Click += new System.EventHandler(this.Btn_Refresh_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.Btn_SearchProduct);
            this.panel4.Controls.Add(this.Txt_SearchProductId);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1186, 59);
            this.panel4.TabIndex = 14;
            // 
            // Btn_SearchProduct
            // 
            this.Btn_SearchProduct.Location = new System.Drawing.Point(415, 9);
            this.Btn_SearchProduct.Name = "Btn_SearchProduct";
            this.Btn_SearchProduct.Size = new System.Drawing.Size(139, 32);
            this.Btn_SearchProduct.TabIndex = 27;
            this.Btn_SearchProduct.Text = "Search Item";
            this.Btn_SearchProduct.UseVisualStyleBackColor = true;
            this.Btn_SearchProduct.Click += new System.EventHandler(this.Btn_SearchProduct_Click);
            // 
            // Txt_SearchProductId
            // 
            this.Txt_SearchProductId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.Txt_SearchProductId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Txt_SearchProductId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_SearchProductId.Location = new System.Drawing.Point(100, 14);
            this.Txt_SearchProductId.Name = "Txt_SearchProductId";
            this.Txt_SearchProductId.Size = new System.Drawing.Size(293, 26);
            this.Txt_SearchProductId.TabIndex = 26;
            this.Txt_SearchProductId.TextChanged += new System.EventHandler(this.Txt_SearchProductId_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 17);
            this.label12.TabIndex = 14;
            this.label12.Text = "Product Id";
            // 
            // Tab_Products
            // 
            this.Tab_Products.Controls.Add(this.TabPage_Search);
            this.Tab_Products.Controls.Add(this.TabPage_ViewProducts);
            this.Tab_Products.Controls.Add(this.TabPage_Prices);
            this.Tab_Products.Controls.Add(this.Tab_Page_SKU);
            this.Tab_Products.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Products.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tab_Products.HotTrack = true;
            this.Tab_Products.ItemSize = new System.Drawing.Size(100, 25);
            this.Tab_Products.Location = new System.Drawing.Point(0, 0);
            this.Tab_Products.Multiline = true;
            this.Tab_Products.Name = "Tab_Products";
            this.Tab_Products.Padding = new System.Drawing.Point(25, 3);
            this.Tab_Products.SelectedIndex = 0;
            this.Tab_Products.Size = new System.Drawing.Size(1200, 650);
            this.Tab_Products.TabIndex = 6;
            this.Tab_Products.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.Tab_Products_Selecting);
            this.Tab_Products.Selected += new System.Windows.Forms.TabControlEventHandler(this.Tab_Products_Selected);
            // 
            // TabPage_ViewProducts
            // 
            this.TabPage_ViewProducts.Controls.Add(this.panel8);
            this.TabPage_ViewProducts.Controls.Add(this.panel7);
            this.TabPage_ViewProducts.Location = new System.Drawing.Point(4, 29);
            this.TabPage_ViewProducts.Name = "TabPage_ViewProducts";
            this.TabPage_ViewProducts.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_ViewProducts.Size = new System.Drawing.Size(1192, 617);
            this.TabPage_ViewProducts.TabIndex = 7;
            this.TabPage_ViewProducts.Text = "Products List";
            this.TabPage_ViewProducts.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.Gridview_ProductList);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(221, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(968, 611);
            this.panel8.TabIndex = 1;
            // 
            // Gridview_ProductList
            // 
            this.Gridview_ProductList.AllowUserToAddRows = false;
            this.Gridview_ProductList.AllowUserToDeleteRows = false;
            this.Gridview_ProductList.AllowUserToResizeRows = false;
            this.Gridview_ProductList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Gridview_ProductList.BackgroundColor = System.Drawing.Color.White;
            this.Gridview_ProductList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gridview_ProductList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column19,
            this.Column16,
            this.Column25,
            this.Column21,
            this.Column22,
            this.Column36,
            this.Column24,
            this.Column14,
            this.Column23,
            this.Column17});
            this.Gridview_ProductList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gridview_ProductList.EnableHeadersVisualStyles = false;
            this.Gridview_ProductList.Location = new System.Drawing.Point(0, 0);
            this.Gridview_ProductList.Name = "Gridview_ProductList";
            this.Gridview_ProductList.RowHeadersVisible = false;
            this.Gridview_ProductList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.Gridview_ProductList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Gridview_ProductList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.Gridview_ProductList.Size = new System.Drawing.Size(968, 611);
            this.Gridview_ProductList.TabIndex = 0;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "ProductId";
            this.Column19.Name = "Column19";
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Description";
            this.Column16.Name = "Column16";
            // 
            // Column25
            // 
            this.Column25.HeaderText = "MainCat";
            this.Column25.Name = "Column25";
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Category1";
            this.Column21.Name = "Column21";
            // 
            // Column22
            // 
            this.Column22.HeaderText = "Category2";
            this.Column22.Name = "Column22";
            // 
            // Column36
            // 
            this.Column36.HeaderText = "LastRestock";
            this.Column36.Name = "Column36";
            // 
            // Column24
            // 
            this.Column24.HeaderText = "SellStatus";
            this.Column24.Name = "Column24";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Unit";
            this.Column14.Name = "Column14";
            // 
            // Column23
            // 
            this.Column23.HeaderText = "SellingPrice";
            this.Column23.Name = "Column23";
            // 
            // Column17
            // 
            this.Column17.HeaderText = "StockBalance";
            this.Column17.Name = "Column17";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel7.Controls.Add(this.Btn_SearchProductDetails);
            this.panel7.Controls.Add(this.textBox30);
            this.panel7.Controls.Add(this.label39);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(218, 611);
            this.panel7.TabIndex = 0;
            // 
            // Btn_SearchProductDetails
            // 
            this.Btn_SearchProductDetails.Location = new System.Drawing.Point(28, 88);
            this.Btn_SearchProductDetails.Name = "Btn_SearchProductDetails";
            this.Btn_SearchProductDetails.Size = new System.Drawing.Size(139, 32);
            this.Btn_SearchProductDetails.TabIndex = 29;
            this.Btn_SearchProductDetails.Text = "Search Item";
            this.Btn_SearchProductDetails.UseVisualStyleBackColor = true;
            this.Btn_SearchProductDetails.Click += new System.EventHandler(this.Btn_SearchProductDetails_Click);
            // 
            // textBox30
            // 
            this.textBox30.Location = new System.Drawing.Point(18, 56);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(164, 23);
            this.textBox30.TabIndex = 30;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(49, 33);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(68, 17);
            this.label39.TabIndex = 28;
            this.label39.Text = "ProductId";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(24, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Filter Products";
            // 
            // Tab_Page_SKU
            // 
            this.Tab_Page_SKU.Controls.Add(this.panel13);
            this.Tab_Page_SKU.Controls.Add(this.panel6);
            this.Tab_Page_SKU.Location = new System.Drawing.Point(4, 29);
            this.Tab_Page_SKU.Name = "Tab_Page_SKU";
            this.Tab_Page_SKU.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Page_SKU.Size = new System.Drawing.Size(1192, 617);
            this.Tab_Page_SKU.TabIndex = 9;
            this.Tab_Page_SKU.Text = "S.K.U";
            this.Tab_Page_SKU.UseVisualStyleBackColor = true;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.Gridview_Sku);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 69);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(1186, 545);
            this.panel13.TabIndex = 1;
            // 
            // Gridview_Sku
            // 
            this.Gridview_Sku.AllowUserToAddRows = false;
            this.Gridview_Sku.AllowUserToDeleteRows = false;
            this.Gridview_Sku.AllowUserToResizeRows = false;
            this.Gridview_Sku.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Gridview_Sku.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Gridview_Sku.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gridview_Sku.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column30,
            this.Column31,
            this.Column37,
            this.Column38,
            this.Column32,
            this.Column33,
            this.Column35});
            this.Gridview_Sku.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gridview_Sku.EnableHeadersVisualStyles = false;
            this.Gridview_Sku.Location = new System.Drawing.Point(0, 0);
            this.Gridview_Sku.Name = "Gridview_Sku";
            this.Gridview_Sku.ReadOnly = true;
            this.Gridview_Sku.RowHeadersVisible = false;
            this.Gridview_Sku.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Gridview_Sku.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Gridview_Sku.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.Gridview_Sku.Size = new System.Drawing.Size(1186, 545);
            this.Gridview_Sku.TabIndex = 0;
            // 
            // Column30
            // 
            this.Column30.FillWeight = 50F;
            this.Column30.HeaderText = "ProductId";
            this.Column30.Name = "Column30";
            this.Column30.ReadOnly = true;
            // 
            // Column31
            // 
            this.Column31.FillWeight = 150F;
            this.Column31.HeaderText = "Description";
            this.Column31.Name = "Column31";
            this.Column31.ReadOnly = true;
            // 
            // Column37
            // 
            this.Column37.FillWeight = 30F;
            this.Column37.HeaderText = "MinLimit";
            this.Column37.Name = "Column37";
            this.Column37.ReadOnly = true;
            // 
            // Column38
            // 
            this.Column38.FillWeight = 30F;
            this.Column38.HeaderText = "MaxLimit";
            this.Column38.Name = "Column38";
            this.Column38.ReadOnly = true;
            // 
            // Column32
            // 
            this.Column32.FillWeight = 50F;
            this.Column32.HeaderText = "StockLevel";
            this.Column32.Name = "Column32";
            this.Column32.ReadOnly = true;
            // 
            // Column33
            // 
            this.Column33.FillWeight = 50F;
            this.Column33.HeaderText = "LastRestock";
            this.Column33.Name = "Column33";
            this.Column33.ReadOnly = true;
            // 
            // Column35
            // 
            this.Column35.FillWeight = 150F;
            this.Column35.HeaderText = "Suggestion";
            this.Column35.Name = "Column35";
            this.Column35.ReadOnly = true;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel6.Controls.Add(this.groupBox2);
            this.panel6.Controls.Add(this.Btn_Sku_searchProduct);
            this.panel6.Controls.Add(this.Txt_sEarchProductIdSKU);
            this.panel6.Controls.Add(this.label30);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1186, 66);
            this.panel6.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SKU_RadioButton_BelowLevel);
            this.groupBox2.Controls.Add(this.SKU_RadioButton_AboveLevel);
            this.groupBox2.Controls.Add(this.SKU_RadioButton_ViewAll);
            this.groupBox2.Location = new System.Drawing.Point(10, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 57);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter By";
            // 
            // SKU_RadioButton_BelowLevel
            // 
            this.SKU_RadioButton_BelowLevel.AutoSize = true;
            this.SKU_RadioButton_BelowLevel.Location = new System.Drawing.Point(261, 25);
            this.SKU_RadioButton_BelowLevel.Name = "SKU_RadioButton_BelowLevel";
            this.SKU_RadioButton_BelowLevel.Size = new System.Drawing.Size(136, 21);
            this.SKU_RadioButton_BelowLevel.TabIndex = 2;
            this.SKU_RadioButton_BelowLevel.TabStop = true;
            this.SKU_RadioButton_BelowLevel.Text = "Below StockLevel";
            this.SKU_RadioButton_BelowLevel.UseVisualStyleBackColor = true;
            this.SKU_RadioButton_BelowLevel.CheckedChanged += new System.EventHandler(this.SKU_RadioButton_BelowLevel_CheckedChanged);
            // 
            // SKU_RadioButton_AboveLevel
            // 
            this.SKU_RadioButton_AboveLevel.AutoSize = true;
            this.SKU_RadioButton_AboveLevel.Location = new System.Drawing.Point(101, 25);
            this.SKU_RadioButton_AboveLevel.Name = "SKU_RadioButton_AboveLevel";
            this.SKU_RadioButton_AboveLevel.Size = new System.Drawing.Size(139, 21);
            this.SKU_RadioButton_AboveLevel.TabIndex = 1;
            this.SKU_RadioButton_AboveLevel.TabStop = true;
            this.SKU_RadioButton_AboveLevel.Text = "Above StockLevel";
            this.SKU_RadioButton_AboveLevel.UseVisualStyleBackColor = true;
            this.SKU_RadioButton_AboveLevel.CheckedChanged += new System.EventHandler(this.SKU_RadioButton_AboveLevel_CheckedChanged);
            // 
            // SKU_RadioButton_ViewAll
            // 
            this.SKU_RadioButton_ViewAll.AutoSize = true;
            this.SKU_RadioButton_ViewAll.Location = new System.Drawing.Point(6, 25);
            this.SKU_RadioButton_ViewAll.Name = "SKU_RadioButton_ViewAll";
            this.SKU_RadioButton_ViewAll.Size = new System.Drawing.Size(74, 21);
            this.SKU_RadioButton_ViewAll.TabIndex = 0;
            this.SKU_RadioButton_ViewAll.TabStop = true;
            this.SKU_RadioButton_ViewAll.Text = "View All";
            this.SKU_RadioButton_ViewAll.UseVisualStyleBackColor = true;
            this.SKU_RadioButton_ViewAll.CheckedChanged += new System.EventHandler(this.SKU_RadioButton_ViewAll_CheckedChanged);
            // 
            // Btn_Sku_searchProduct
            // 
            this.Btn_Sku_searchProduct.Location = new System.Drawing.Point(818, 17);
            this.Btn_Sku_searchProduct.Name = "Btn_Sku_searchProduct";
            this.Btn_Sku_searchProduct.Size = new System.Drawing.Size(143, 30);
            this.Btn_Sku_searchProduct.TabIndex = 19;
            this.Btn_Sku_searchProduct.Text = "Search Product";
            this.Btn_Sku_searchProduct.UseVisualStyleBackColor = true;
            this.Btn_Sku_searchProduct.Click += new System.EventHandler(this.Btn_Sku_searchProduct_Click);
            // 
            // Txt_sEarchProductIdSKU
            // 
            this.Txt_sEarchProductIdSKU.Location = new System.Drawing.Point(553, 19);
            this.Txt_sEarchProductIdSKU.Name = "Txt_sEarchProductIdSKU";
            this.Txt_sEarchProductIdSKU.Size = new System.Drawing.Size(246, 23);
            this.Txt_sEarchProductIdSKU.TabIndex = 18;
            this.Txt_sEarchProductIdSKU.TextChanged += new System.EventHandler(this.Txt_sEarchProductIdSKU_TextChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(475, 22);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(72, 17);
            this.label30.TabIndex = 17;
            this.label30.Text = "Product Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(491, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(698, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(152, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(183, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(148, 20);
            this.textBox2.TabIndex = 6;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(491, 40);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(198, 20);
            this.textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(698, 40);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(335, 20);
            this.textBox4.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 20);
            this.label7.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 20);
            this.label6.TabIndex = 10;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(179, 92);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(152, 20);
            this.textBox6.TabIndex = 11;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(17, 92);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(152, 20);
            this.textBox5.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(353, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 20);
            this.label9.TabIndex = 13;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(353, 91);
            this.textBox8.MaxLength = 20;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(95, 20);
            this.textBox8.TabIndex = 15;
            // 
            // Btn_AddItem
            // 
            this.Btn_AddItem.Location = new System.Drawing.Point(0, 0);
            this.Btn_AddItem.Name = "Btn_AddItem";
            this.Btn_AddItem.Size = new System.Drawing.Size(75, 23);
            this.Btn_AddItem.TabIndex = 0;
            // 
            // Btn_ClearTexts
            // 
            this.Btn_ClearTexts.Location = new System.Drawing.Point(0, 0);
            this.Btn_ClearTexts.Name = "Btn_ClearTexts";
            this.Btn_ClearTexts.Size = new System.Drawing.Size(75, 23);
            this.Btn_ClearTexts.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(333, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 20);
            this.label10.TabIndex = 18;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(337, 40);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(148, 20);
            this.textBox10.TabIndex = 19;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(465, 68);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 20);
            this.label35.TabIndex = 20;
            // 
            // textBox25
            // 
            this.textBox25.Location = new System.Drawing.Point(465, 91);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(84, 20);
            this.textBox25.TabIndex = 21;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(555, 70);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(72, 20);
            this.label31.TabIndex = 22;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(555, 92);
            this.textBox12.MaxLength = 20;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(88, 20);
            this.textBox12.TabIndex = 23;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(684, 70);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(44, 20);
            this.label43.TabIndex = 34;
            // 
            // textBox36
            // 
            this.textBox36.Location = new System.Drawing.Point(0, 0);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(100, 20);
            this.textBox36.TabIndex = 0;
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(100, 23);
            this.label38.TabIndex = 0;
            // 
            // textBox26
            // 
            this.textBox26.Location = new System.Drawing.Point(0, 0);
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(100, 20);
            this.textBox26.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 0;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(0, 0);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 20);
            this.textBox9.TabIndex = 0;
            // 
            // Btn_SaveItems
            // 
            this.Btn_SaveItems.Location = new System.Drawing.Point(0, 0);
            this.Btn_SaveItems.Name = "Btn_SaveItems";
            this.Btn_SaveItems.Size = new System.Drawing.Size(75, 23);
            this.Btn_SaveItems.TabIndex = 0;
            // 
            // Btn_ClearGridview
            // 
            this.Btn_ClearGridview.Location = new System.Drawing.Point(0, 0);
            this.Btn_ClearGridview.Name = "Btn_ClearGridview";
            this.Btn_ClearGridview.Size = new System.Drawing.Size(75, 23);
            this.Btn_ClearGridview.TabIndex = 0;
            // 
            // Products_Gridview
            // 
            this.Products_Gridview.Location = new System.Drawing.Point(0, 0);
            this.Products_Gridview.Name = "Products_Gridview";
            this.Products_Gridview.Size = new System.Drawing.Size(240, 150);
            this.Products_Gridview.TabIndex = 0;
            // 
            // Column34
            // 
            this.Column34.Name = "Column34";
            // 
            // Column8
            // 
            this.Column8.Name = "Column8";
            // 
            // Column42
            // 
            this.Column42.Name = "Column42";
            // 
            // Column41
            // 
            this.Column41.Name = "Column41";
            // 
            // Column7
            // 
            this.Column7.Name = "Column7";
            // 
            // Column6
            // 
            this.Column6.Name = "Column6";
            // 
            // Column5
            // 
            this.Column5.Name = "Column5";
            // 
            // Column4
            // 
            this.Column4.Name = "Column4";
            // 
            // Column1
            // 
            this.Column1.Name = "Column1";
            // 
            // Column9
            // 
            this.Column9.Name = "Column9";
            // 
            // Column3
            // 
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.Name = "Column2";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1200, 650);
            this.Controls.Add(this.Tab_Products);
            this.Name = "Products";
            this.Load += new System.EventHandler(this.Products_Load);
            this.TabPage_Prices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Prices_ProductsGridview)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.Gridview_ProductList)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.Tab_Page_SKU.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gridview_Sku)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Products_Gridview)).EndInit();
            this.ResumeLayout(false);

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

