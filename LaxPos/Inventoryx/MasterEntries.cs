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
    using System.IO;
    using System.Windows.Forms;

    public class MasterEntries : BunifuForm
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public static int TransactionNo = 0;
        public static DateTime InsertionDate = Program.CurrentDateTime();
        public static string InsertionTime;
        public static List<string> UnsavedItems = new List<string>();
        public static List<string> UnresolvedItems = new List<string>();
        public int X = 0;
        private IContainer components = null;
        private TabPage TabPage_Product;
        private GroupBox groupBox4;
        private TextBox textBox1;
        private Label label7;
        private TextBox textBox12;
        private Label label13;
        private Label label15;
        private Label label16;
        private TextBox textBox15;
        private TextBox textBox16;
        private Label label17;
        private Label label18;
        private Label label8;
        private TextBox textBox19;
        private Label label22;
        private Button Btn_AddCart;
        private ListBox listBox1;
        private Label label27;
        private Label label26;
        private Button Btn_Generate;
        private TabPage TabPage_Categories;
        private TabPage TabPage_Supplier;
        private Button Btn_AddSupplier;
        private Button Btn_NewSupplierClearForm;
        private GroupBox groupBox5;
        private ListBox Listbox_SupplyActivestatus;
        private Label label35;
        private Label label36;
        private Label label37;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox32;
        private GroupBox groupBox6;
        private TextBox textBox33;
        private Label label38;
        private TextBox textBox34;
        private Label label39;
        private TextBox textBox35;
        private Label label40;
        private TextBox textBox37;
        private Label label41;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private TextBox textBox38;
        private Label label44;
        private TextBox textBox39;
        private Label label45;
        private TextBox textBox40;
        private Label label46;
        private TextBox textBox41;
        private TextBox textBox42;
        private TextBox textBox43;
        private Label label47;
        private Label label48;
        private Label label49;
        private GroupBox groupBox9;
        private Label label50;
        private TextBox textBox46;
        private GroupBox groupBox13;
        private GroupBox groupBox12;
        private TextBox textBox18;
        private Label label23;
        private TextBox textBox14;
        private Label label20;
        private Label label51;
        private TextBox textBox25;
        private GroupBox groupBox14;
        private ComboBox ComboBox2;
        private Label label67;
        private GroupBox groupBox15;
        private ComboBox comboBox3;
        private Label label71;
        private GroupBox groupBox16;
        private Label label70;
        private TextBox textBox65;
        private Label label74;
        private TextBox textBox49;
        private Label label54;
        private TextBox textBox48;
        private Label label53;
        private TextBox textBox50;
        private Label label55;
        public TabControl Tab_MasterEntries;
        private Label label19;
        private Button Btn_ResetNewProduct;
        private TabPage Tabpage_BulkProducts;
        private Panel panel3;
        public DataGridView Products_Gridview;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private Panel panel7;
        private Button Btn_ClearGridview;
        private Button Btn_SaveCsvItems;
        private Button Btn_UploadCSVFile;
        private GroupBox groupBox10;
        private GroupBox groupBox11;
        private GroupBox groupBox17;
        private Button button10;
        private ComboBox ComboBox6;
        private ComboBox ComboBox5;
        private ComboBox ComboBox4;
        private TabPage TabPage_MeasurementsUnits;
        private Button Btn_SaveMainCategory;
        private Button Btn_SaveCategory1;
        private Button Btn_SaveCategory2;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Panel panel1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private GroupBox groupBox2;
        public ComboBox comboBox1;
        private ComboBox ComboBox8;
        private ComboBox ComboBox7;
        private Button Btn_RefreshCategories;

        public MasterEntries()
        {
            this.InitializeComponent();
            this.ListCategoriesForCategories(0);
        }

        private void Btn_AddSupplier_Click(object sender, EventArgs e)
        {
            if (this.CompleteInfo())
            {
                string supplid = "S" + this.textBox38.Text;
                if (this.CheckifIdDoesNotExist(supplid))
                {
                    string[] textArray1 = new string[] { "The Following Supplier Will be registered As:\n\nSUPPLIERID: ", supplid, "\n\nNAME:", this.textBox43.Text.ToUpper(), " ", this.textBox42.Text.ToUpper(), " ", this.textBox41.Text.ToUpper() };
                    if (MessageBox.Show(string.Concat(textArray1), "CONFRIMATION BOX", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        string gender = "";
                        if (this.radioButton1.Checked)
                        {
                            gender = "Male";
                        }
                        if (this.radioButton2.Checked)
                        {
                            gender = "Female";
                        }
                        this.InsertSupplier(supplid, gender);
                    }
                }
            }
        }

        private void Btn_ClearGridview_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the items list?", "Message Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Products_Gridview.Rows.Clear();
            }
        }

        private void Btn_Generate_Click(object sender, EventArgs e)
        {
            this.textBox16.Text = "**GENERATED**";
        }

        private void Btn_NewSupplierClearForm_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You are about to clear the Suppliers Details on the form...\nAre you sure To Continue ?", "Confirmation Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.ResetSuppliersForm();
            }
        }

        private void Btn_RefreshCategories_Click(object sender, EventArgs e)
        {
            this.ListCategoriesForCategories(0);
            this.PullDatabase();
        }

        private void Btn_ResetNewProduct_Click(object sender, EventArgs e)
        {
            this.textBox16.Text = "";
            this.textBox15.Text = "";
            this.textBox12.Text = "";
            this.textBox18.Text = "";
            this.textBox1.Text = "";
            this.textBox14.Text = "";
            this.textBox19.Text = "";
            this.textBox48.Text = "";
            this.textBox49.Text = "";
            this.textBox50.Text = "";
            this.listBox1.ResetText();
            this.ComboBox4.SelectedItem = null;
            this.ComboBox5.SelectedItem = null;
            this.ComboBox6.SelectedItem = null;
            this.ComboBox7.SelectedItem = null;
            this.ComboBox8.SelectedItem = null;
        }

        private void Btn_SaveCategory1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.textBox25.Text == "") || (this.comboBox1.Text == ""))
                {
                    MessageBox.Show("incomplete info");
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO categories (CatName,Category1,Category2,RegDate) values(@maincat,@cat1,@cat2,@date);", connection);
                    command.Parameters.AddWithValue("@cat1", this.textBox25.Text);
                    command.Parameters.AddWithValue("@cat2", "#");
                    command.Parameters.AddWithValue("@date", Program.CurrentDateTime());
                    command.Parameters.AddWithValue("@maincat", this.comboBox1.Text);
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("Category Cannot be saved");
                    }
                    else
                    {
                        MessageBox.Show("Category saved successfully");
                        this.textBox25.Text = "";
                        this.comboBox1.Text = "";
                        this.PullDatabase();
                        this.ListCategoriesForCategories(0);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_SaveCategory2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.textBox65.Text == "") || (this.ComboBox2.Text == ""))
                {
                    MessageBox.Show("incomplete info");
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("insert into categories (CatName,Category1,Category2,RegDate) values(@maincat,@cat1,@cat2,@date);", connection);
                    command.Parameters.AddWithValue("@cat1", this.comboBox3.Text);
                    command.Parameters.AddWithValue("@cat2", this.textBox65.Text);
                    command.Parameters.AddWithValue("@date", Program.CurrentDateTime());
                    command.Parameters.AddWithValue("@maincat", this.ComboBox2.Text);
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("Category Cannot be saved");
                    }
                    else
                    {
                        MessageBox.Show("Category saved successfully");
                        this.textBox25.Text = "";
                        this.comboBox1.Text = "";
                        this.PullDatabase();
                        this.ListCategoriesForCategories(0);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_SaveCsvItems_Click(object sender, EventArgs e)
        {
            if (this.Products_Gridview.Rows.Count > 0)
            {
                this.InsertMultipleProducts();
            }
            else
            {
                MessageBox.Show("No items to save!", "MESSAGEBox", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_SaveMainCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox46.Text == "")
                {
                    MessageBox.Show("Category name cannot be empty");
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO categories (CatName,Category1,Category2,RegDate) values(@name,@cat1,@cat2,@date);", connection);
                    command.Parameters.AddWithValue("@name", this.textBox46.Text);
                    command.Parameters.AddWithValue("@cat1", "#");
                    command.Parameters.AddWithValue("@cat2", "#");
                    command.Parameters.AddWithValue("@date", Program.CurrentDateTime());
                    if (command.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("Category Cannot be saved");
                    }
                    else
                    {
                        MessageBox.Show("Category saved successfully");
                        this.textBox46.Text = "";
                        this.PullDatabase();
                        this.ListCategoriesForCategories(0);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_SaveProduct_Click(object sender, EventArgs e)
        {
            if (((this.textBox16.Text == "") || ((this.textBox1.Text == "") || ((this.ComboBox4.Text == "") || ((this.textBox12.Text == "") || ((this.textBox15.Text == "") || ((this.ComboBox7.Text == "") || ((this.textBox19.Text == "") || ((this.listBox1.Text == "") || (this.textBox18.Text == ""))))))))) || (this.ComboBox8.Text == ""))
            {
                MessageBox.Show("Incomplete Details", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    int autoGenId = 0;
                    if (this.textBox16.Text == "**GENERATED**")
                    {
                        this.GenerateCode();
                        autoGenId = 1;
                    }
                    if ((this.textBox16.Text != "**GENERATED**") && (this.textBox16.Text != ""))
                    {
                        if (this.InsertSingleNewProduct(this.textBox16.Text, autoGenId) != 1)
                        {
                            MessageBox.Show("Failed to insert the products", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            this.Btn_ResetNewProduct_Click(new object(), new EventArgs());
                            MessageBox.Show("You have successfully inserted the products", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void Btn_UploadFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Products_Gridview.Rows.Count > 0)
                {
                    MessageBox.Show("You already Have items waiting to be saved. Kindly Save and try again !", "Uncompleted Process Detected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string path = "";
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        path = dialog.FileName;
                    }
                    List<string> list = new List<string>();
                    char[] separator = new char[] { '\n' };
                    foreach (string str3 in File.ReadAllText(path).Split(separator))
                    {
                        char[] chArray2 = new char[] { ',' };
                        object[] values = str3.Split(chArray2);
                        this.Products_Gridview.Rows.Add(values);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
            }
        }

        public void CategorieslistForProducts(int CategoryLevel)
        {
            try
            {
                if (CategoryLevel == 0)
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT distinct CatName FROM `p.o.s`.categories;";
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        this.ComboBox4.Items.Clear();
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                break;
                            }
                            this.ComboBox4.Items.Add(reader[0].ToString());
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
                if (CategoryLevel == 1)
                {
                    MySqlConnection connection2 = new MySqlConnection(this.Db.DBConnecString());
                    connection2.Open();
                    MySqlCommand command2 = connection2.CreateCommand();
                    command2.CommandType = CommandType.Text;
                    command2.CommandText = "SELECT DISTINCT Category1 FROM `p.o.s`.categories where CatName=@main;";
                    command2.Parameters.AddWithValue("@main", this.ComboBox4.Text);
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        this.ComboBox5.Items.Clear();
                        while (true)
                        {
                            if (!reader2.Read())
                            {
                                break;
                            }
                            this.ComboBox5.Items.Add(reader2[0].ToString());
                        }
                    }
                    reader2.Close();
                    connection2.Close();
                }
                if (CategoryLevel == 2)
                {
                    MySqlConnection connection3 = new MySqlConnection(this.Db.DBConnecString());
                    connection3.Open();
                    MySqlCommand command3 = connection3.CreateCommand();
                    command3.CommandType = CommandType.Text;
                    command3.CommandText = "SELECT DISTINCT Category2 FROM `p.o.s`.categories where CatName=@main and Category1=@cat1;";
                    command3.Parameters.AddWithValue("@main", this.ComboBox4.Text);
                    command3.Parameters.AddWithValue("@cat1", this.ComboBox5.Text);
                    MySqlDataReader reader3 = command3.ExecuteReader();
                    if (reader3.HasRows)
                    {
                        this.ComboBox6.Items.Clear();
                        while (true)
                        {
                            if (!reader3.Read())
                            {
                                break;
                            }
                            this.ComboBox6.Items.Add(reader3[0].ToString());
                        }
                    }
                    reader3.Close();
                    connection3.Close();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
            }
        }

        public bool CheckifIdDoesNotExist(string Supplid)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from suppliersdetails where SupId=@Id;";
                command.Parameters.AddWithValue("@Id", Supplid);
                MySqlDataReader reader = command.ExecuteReader();
                connection.Close();
                return !reader.HasRows;
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ComboBox2.SelectedIndex >= 0)
            {
                this.ListCategoriesForCategories(1);
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.ComboBox5.Items.Clear();
                this.ComboBox6.Items.Clear();
                this.ComboBox5.Text = null;
                this.ComboBox6.Text = null;
                this.CategorieslistForProducts(1);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK);
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CategorieslistForProducts(2);
        }

        public bool CompleteInfo()
        {
            bool flag3;
            if (((this.textBox38.Text == "") || ((this.textBox43.Text == "") || ((this.textBox42.Text == "") || ((this.textBox41.Text == "") || ((this.textBox40.Text == "") || ((this.textBox39.Text == "") || ((this.textBox33.Text == "") || ((this.textBox34.Text == "") || ((this.textBox35.Text == "") || ((this.textBox32.Text == "") || (this.textBox37.Text == ""))))))))))) || (this.dateTimePicker1.Text == ""))
            {
                MessageBox.Show("You have not provided The Minimum Information Required !!", "INCOMPLETE INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                flag3 = false;
            }
            else if ((this.radioButton1.Checked || this.radioButton2.Checked) ? (this.Listbox_SupplyActivestatus.SelectedIndex >= 0) : false)
            {
                flag3 = true;
            }
            else
            {
                MessageBox.Show("Select Suppliers Gender And Status !! ", "Gender And Status Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                flag3 = false;
            }
            return flag3;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GenerateCode()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT MAX(SNo) FROM `p.o.s`.inventorymaster where AG=1;";
                MySqlDataReader reader = command.ExecuteReader();
                string checkid = "";
                if (!reader.HasRows)
                {
                    MessageBox.Show("Cannot Identify The Previous Code", "Error Message", MessageBoxButtons.OK);
                    this.textBox16.Text = "";
                }
                else
                {
                    int num = 0;
                    bool flag2 = false;
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            reader.Dispose();
                            if (!flag2)
                            {
                                MessageBox.Show("Cannot Identify The Previous Code", "Error Message", MessageBoxButtons.OK);
                            }
                            else
                            {
                                command.CommandText = "select ProductCode from inventorymaster where Sno=@sno;";
                                command.Parameters.AddWithValue("@sno", num);
                                MySqlDataReader reader2 = command.ExecuteReader();
                                if (!reader2.HasRows)
                                {
                                    MessageBox.Show("The system Cannot generate a new code! Check The Last Code!", "WARNING MESSSAGE", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                                    this.textBox16.Text = "";
                                }
                                else
                                {
                                    while (true)
                                    {
                                        int num2;
                                        if (!reader2.Read())
                                        {
                                            break;
                                        }
                                        string s = reader2["ProductCode"].ToString().Remove(0, 1);
                                        if (!int.TryParse(s, out num2))
                                        {
                                            MessageBox.Show("The system Cannot generate a new code! Check The Last Code!", "WARNING MESSSAGE", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
                                            this.textBox16.Text = "";
                                            continue;
                                        }
                                        checkid = "G" + (num2 + 1).ToString();
                                        if (this.UniqueIdCheck(checkid) == 0)
                                        {
                                            this.textBox16.Text = checkid;
                                            continue;
                                        }
                                        if (this.UniqueIdCheck(checkid) == 1)
                                        {
                                            if (MessageBox.Show("An item with the same Code exists.Try generate a new code!", "WARNING MESSSAGE", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                                            {
                                                this.GenerateCode();
                                                continue;
                                            }
                                            this.textBox16.Text = "";
                                        }
                                    }
                                }
                            }
                            break;
                        }
                        flag2 = int.TryParse(reader[0].ToString(), out num);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.StackTrace, "ERROR MESSAGE", MessageBoxButtons.OK);
                this.textBox16.Text = "";
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            this.Tab_MasterEntries = new TabControl();
            this.TabPage_Product = new TabPage();
            this.button10 = new Button();
            this.Btn_ResetNewProduct = new Button();
            this.groupBox17 = new GroupBox();
            this.textBox50 = new TextBox();
            this.label55 = new Label();
            this.textBox49 = new TextBox();
            this.label54 = new Label();
            this.textBox48 = new TextBox();
            this.label53 = new Label();
            this.textBox14 = new TextBox();
            this.label20 = new Label();
            this.textBox19 = new TextBox();
            this.label22 = new Label();
            this.groupBox11 = new GroupBox();
            this.ComboBox8 = new ComboBox();
            this.ComboBox7 = new ComboBox();
            this.textBox1 = new TextBox();
            this.label19 = new Label();
            this.textBox18 = new TextBox();
            this.label8 = new Label();
            this.label23 = new Label();
            this.label17 = new Label();
            this.textBox12 = new TextBox();
            this.label15 = new Label();
            this.groupBox4 = new GroupBox();
            this.ComboBox6 = new ComboBox();
            this.ComboBox5 = new ComboBox();
            this.ComboBox4 = new ComboBox();
            this.Btn_Generate = new Button();
            this.label18 = new Label();
            this.label27 = new Label();
            this.textBox16 = new TextBox();
            this.textBox15 = new TextBox();
            this.label26 = new Label();
            this.listBox1 = new ListBox();
            this.label16 = new Label();
            this.label13 = new Label();
            this.label7 = new Label();
            this.Btn_AddCart = new Button();
            this.TabPage_Supplier = new TabPage();
            this.Btn_AddSupplier = new Button();
            this.Btn_NewSupplierClearForm = new Button();
            this.groupBox5 = new GroupBox();
            this.Listbox_SupplyActivestatus = new ListBox();
            this.label35 = new Label();
            this.label36 = new Label();
            this.label37 = new Label();
            this.dateTimePicker1 = new DateTimePicker();
            this.textBox32 = new TextBox();
            this.groupBox6 = new GroupBox();
            this.textBox33 = new TextBox();
            this.label38 = new Label();
            this.textBox34 = new TextBox();
            this.label39 = new Label();
            this.textBox35 = new TextBox();
            this.label40 = new Label();
            this.textBox37 = new TextBox();
            this.label41 = new Label();
            this.groupBox7 = new GroupBox();
            this.groupBox8 = new GroupBox();
            this.radioButton2 = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.textBox38 = new TextBox();
            this.label44 = new Label();
            this.textBox39 = new TextBox();
            this.label45 = new Label();
            this.textBox40 = new TextBox();
            this.label46 = new Label();
            this.textBox41 = new TextBox();
            this.textBox42 = new TextBox();
            this.textBox43 = new TextBox();
            this.label47 = new Label();
            this.label48 = new Label();
            this.label49 = new Label();
            this.TabPage_Categories = new TabPage();
            this.groupBox1 = new GroupBox();
            this.dataGridView1 = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.panel1 = new Panel();
            this.groupBox13 = new GroupBox();
            this.Btn_SaveCategory2 = new Button();
            this.groupBox2 = new GroupBox();
            this.label74 = new Label();
            this.textBox65 = new TextBox();
            this.groupBox15 = new GroupBox();
            this.comboBox3 = new ComboBox();
            this.label71 = new Label();
            this.groupBox14 = new GroupBox();
            this.ComboBox2 = new ComboBox();
            this.label67 = new Label();
            this.groupBox12 = new GroupBox();
            this.Btn_RefreshCategories = new Button();
            this.Btn_SaveCategory1 = new Button();
            this.groupBox16 = new GroupBox();
            this.comboBox1 = new ComboBox();
            this.label70 = new Label();
            this.textBox25 = new TextBox();
            this.label51 = new Label();
            this.groupBox9 = new GroupBox();
            this.Btn_SaveMainCategory = new Button();
            this.label50 = new Label();
            this.textBox46 = new TextBox();
            this.Tabpage_BulkProducts = new TabPage();
            this.panel3 = new Panel();
            this.Products_Gridview = new DataGridView();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new DataGridViewTextBoxColumn();
            this.panel7 = new Panel();
            this.Btn_ClearGridview = new Button();
            this.Btn_SaveCsvItems = new Button();
            this.Btn_UploadCSVFile = new Button();
            this.groupBox10 = new GroupBox();
            this.TabPage_MeasurementsUnits = new TabPage();
            this.Tab_MasterEntries.SuspendLayout();
            this.TabPage_Product.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.TabPage_Supplier.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.TabPage_Categories.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.groupBox13.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.Tabpage_BulkProducts.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize) this.Products_Gridview).BeginInit();
            this.panel7.SuspendLayout();
            base.SuspendLayout();
            this.Tab_MasterEntries.Controls.Add(this.TabPage_Product);
            this.Tab_MasterEntries.Controls.Add(this.TabPage_Supplier);
            this.Tab_MasterEntries.Controls.Add(this.TabPage_Categories);
            this.Tab_MasterEntries.Controls.Add(this.Tabpage_BulkProducts);
            this.Tab_MasterEntries.Controls.Add(this.TabPage_MeasurementsUnits);
            this.Tab_MasterEntries.Dock = DockStyle.Fill;
            this.Tab_MasterEntries.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Tab_MasterEntries.Location = new Point(0, 0);
            this.Tab_MasterEntries.Margin = new Padding(4);
            this.Tab_MasterEntries.Name = "Tab_MasterEntries";
            this.Tab_MasterEntries.Padding = new Point(0x19, 3);
            this.Tab_MasterEntries.SelectedIndex = 0;
            this.Tab_MasterEntries.Size = new Size(0x4b0, 600);
            this.Tab_MasterEntries.TabIndex = 8;
            this.Tab_MasterEntries.SelectedIndexChanged += new EventHandler(this.Tab_Suppliers_SelectedIndexChanged);
            this.Tab_MasterEntries.Selected += new TabControlEventHandler(this.Tab_Suppliers_Selected);
            this.TabPage_Product.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_Product.Controls.Add(this.button10);
            this.TabPage_Product.Controls.Add(this.Btn_ResetNewProduct);
            this.TabPage_Product.Controls.Add(this.groupBox17);
            this.TabPage_Product.Controls.Add(this.groupBox11);
            this.TabPage_Product.Controls.Add(this.groupBox4);
            this.TabPage_Product.Controls.Add(this.Btn_AddCart);
            this.TabPage_Product.Location = new Point(4, 0x19);
            this.TabPage_Product.Name = "TabPage_Product";
            this.TabPage_Product.Padding = new Padding(150, 30, 0, 3);
            this.TabPage_Product.Size = new Size(0x4a8, 0x23b);
            this.TabPage_Product.TabIndex = 3;
            this.TabPage_Product.Text = "Product";
            this.button10.BackColor = Color.Chocolate;
            this.button10.FlatStyle = FlatStyle.Flat;
            this.button10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.button10.ForeColor = SystemColors.ButtonHighlight;
            this.button10.Location = new Point(0x2cf, 0x188);
            this.button10.Margin = new Padding(8);
            this.button10.Name = "button10";
            this.button10.Size = new Size(170, 0x29);
            this.button10.TabIndex = 0x55;
            this.button10.Text = "Close";
            this.button10.UseVisualStyleBackColor = false;
            this.Btn_ResetNewProduct.BackColor = Color.Chocolate;
            this.Btn_ResetNewProduct.FlatStyle = FlatStyle.Flat;
            this.Btn_ResetNewProduct.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_ResetNewProduct.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_ResetNewProduct.Location = new Point(0xdd, 0x188);
            this.Btn_ResetNewProduct.Margin = new Padding(8);
            this.Btn_ResetNewProduct.Name = "Btn_ResetNewProduct";
            this.Btn_ResetNewProduct.Size = new Size(0x88, 0x29);
            this.Btn_ResetNewProduct.TabIndex = 0x52;
            this.Btn_ResetNewProduct.Text = "New Item";
            this.Btn_ResetNewProduct.UseVisualStyleBackColor = false;
            this.Btn_ResetNewProduct.Click += new EventHandler(this.Btn_ResetNewProduct_Click);
            this.groupBox17.Controls.Add(this.textBox50);
            this.groupBox17.Controls.Add(this.label55);
            this.groupBox17.Controls.Add(this.textBox49);
            this.groupBox17.Controls.Add(this.label54);
            this.groupBox17.Controls.Add(this.textBox48);
            this.groupBox17.Controls.Add(this.label53);
            this.groupBox17.Controls.Add(this.textBox14);
            this.groupBox17.Controls.Add(this.label20);
            this.groupBox17.Controls.Add(this.textBox19);
            this.groupBox17.Controls.Add(this.label22);
            this.groupBox17.Dock = DockStyle.Top;
            this.groupBox17.Location = new Point(150, 0x102);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new Size(0x412, 0x71);
            this.groupBox17.TabIndex = 0x54;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Product Prices";
            this.textBox50.Location = new Point(0x166, 0x36);
            this.textBox50.MaxLength = 20;
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new Size(0x3d, 0x17);
            this.textBox50.TabIndex = 0x4d;
            this.label55.AutoSize = true;
            this.label55.Location = new Point(0x16e, 0x22);
            this.label55.Name = "label55";
            this.label55.Size = new Size(0x27, 0x11);
            this.label55.TabIndex = 0x4c;
            this.label55.Text = "Tax3";
            this.textBox49.Location = new Point(0x11b, 0x36);
            this.textBox49.MaxLength = 20;
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new Size(0x39, 0x17);
            this.textBox49.TabIndex = 0x4b;
            this.label54.AutoSize = true;
            this.label54.Location = new Point(0x123, 0x22);
            this.label54.Name = "label54";
            this.label54.Size = new Size(0x1d, 0x11);
            this.label54.TabIndex = 0x4a;
            this.label54.Text = "Vat";
            this.textBox48.Location = new Point(0xce, 0x36);
            this.textBox48.MaxLength = 20;
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new Size(0x3f, 0x17);
            this.textBox48.TabIndex = 0x49;
            this.label53.AutoSize = true;
            this.label53.Location = new Point(0xd5, 0x22);
            this.label53.Name = "label53";
            this.label53.Size = new Size(0x25, 0x11);
            this.label53.TabIndex = 0x48;
            this.label53.Text = "Gsst";
            this.textBox14.Location = new Point(20, 0x36);
            this.textBox14.MaxLength = 20;
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Size(0x62, 0x17);
            this.textBox14.TabIndex = 0x47;
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0x1a, 0x22);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x35, 0x11);
            this.label20.TabIndex = 70;
            this.label20.Text = "B.Price";
            this.textBox19.Location = new Point(0x85, 0x36);
            this.textBox19.MaxLength = 20;
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Size(0x3f, 0x17);
            this.textBox19.TabIndex = 0x38;
            this.label22.AutoSize = true;
            this.label22.Location = new Point(130, 0x22);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x35, 0x11);
            this.label22.TabIndex = 0x33;
            this.label22.Text = "S.Price";
            this.groupBox11.Controls.Add(this.ComboBox8);
            this.groupBox11.Controls.Add(this.ComboBox7);
            this.groupBox11.Controls.Add(this.textBox1);
            this.groupBox11.Controls.Add(this.label19);
            this.groupBox11.Controls.Add(this.textBox18);
            this.groupBox11.Controls.Add(this.label8);
            this.groupBox11.Controls.Add(this.label23);
            this.groupBox11.Controls.Add(this.label17);
            this.groupBox11.Controls.Add(this.textBox12);
            this.groupBox11.Controls.Add(this.label15);
            this.groupBox11.Dock = DockStyle.Top;
            this.groupBox11.Location = new Point(150, 0x9e);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new Size(0x412, 100);
            this.groupBox11.TabIndex = 0x53;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Product Supply Info";
            this.ComboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox8.FormattingEnabled = true;
            this.ComboBox8.Location = new Point(0x1d, 0x38);
            this.ComboBox8.Name = "ComboBox8";
            this.ComboBox8.Size = new Size(0x75, 0x18);
            this.ComboBox8.TabIndex = 0x53;
            this.ComboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox7.FormattingEnabled = true;
            this.ComboBox7.Location = new Point(0x1cf, 0x38);
            this.ComboBox7.Name = "ComboBox7";
            this.ComboBox7.Size = new Size(0x80, 0x18);
            this.ComboBox7.TabIndex = 0x52;
            this.textBox1.Location = new Point(0x179, 0x38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x4c, 0x17);
            this.textBox1.TabIndex = 0x2f;
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x21, 0x25);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x66, 0x11);
            this.label19.TabIndex = 0x4e;
            this.label19.Text = "Suppliers Pack";
            this.textBox18.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.textBox18.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textBox18.Location = new Point(0xae, 0x38);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Size(0x62, 0x17);
            this.textBox18.TabIndex = 0x3a;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x1d9, 0x24);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x4f, 0x11);
            this.label8.TabIndex = 0x39;
            this.label8.Text = "Selling Unit";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0xab, 0x24);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x65, 0x11);
            this.label23.TabIndex = 50;
            this.label23.Text = "Opening Stock";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x127, 0x24);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x3f, 0x11);
            this.label17.TabIndex = 4;
            this.label17.Text = "Minimum";
            this.textBox12.Location = new Point(0x126, 0x38);
            this.textBox12.MaxLength = 20;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Size(0x49, 0x17);
            this.textBox12.TabIndex = 0x27;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x176, 0x24);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x42, 0x11);
            this.label15.TabIndex = 13;
            this.label15.Text = "Maximum";
            this.groupBox4.Controls.Add(this.ComboBox6);
            this.groupBox4.Controls.Add(this.ComboBox5);
            this.groupBox4.Controls.Add(this.ComboBox4);
            this.groupBox4.Controls.Add(this.Btn_Generate);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.textBox16);
            this.groupBox4.Controls.Add(this.textBox15);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.listBox1);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Dock = DockStyle.Top;
            this.groupBox4.Location = new Point(150, 30);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x412, 0x80);
            this.groupBox4.TabIndex = 0x11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Product Description";
            this.ComboBox6.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.ComboBox6.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.ComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox6.FormattingEnabled = true;
            this.ComboBox6.Location = new Point(0x217, 0x61);
            this.ComboBox6.Name = "ComboBox6";
            this.ComboBox6.Size = new Size(0x79, 0x18);
            this.ComboBox6.TabIndex = 0x48;
            this.ComboBox5.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.ComboBox5.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.ComboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox5.FormattingEnabled = true;
            this.ComboBox5.Location = new Point(0x187, 0x61);
            this.ComboBox5.Name = "ComboBox5";
            this.ComboBox5.Size = new Size(0x80, 0x18);
            this.ComboBox5.TabIndex = 0x47;
            this.ComboBox5.SelectedIndexChanged += new EventHandler(this.ComboBox5_SelectedIndexChanged);
            this.ComboBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.ComboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.ComboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox4.FormattingEnabled = true;
            this.ComboBox4.Location = new Point(0xfb, 0x61);
            this.ComboBox4.Name = "ComboBox4";
            this.ComboBox4.Size = new Size(0x79, 0x18);
            this.ComboBox4.TabIndex = 70;
            this.ComboBox4.SelectedIndexChanged += new EventHandler(this.ComboBox4_SelectedIndexChanged);
            this.Btn_Generate.BackColor = Color.Chocolate;
            this.Btn_Generate.FlatStyle = FlatStyle.Flat;
            this.Btn_Generate.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Generate.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Generate.Location = new Point(50, 0x4d);
            this.Btn_Generate.Margin = new Padding(8);
            this.Btn_Generate.Name = "Btn_Generate";
            this.Btn_Generate.Size = new Size(0x6a, 30);
            this.Btn_Generate.TabIndex = 0x45;
            this.Btn_Generate.Text = "Generate";
            this.Btn_Generate.UseVisualStyleBackColor = false;
            this.Btn_Generate.Click += new EventHandler(this.Btn_Generate_Click);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0xf8, 0x4d);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x63, 0x11);
            this.label18.TabIndex = 3;
            this.label18.Text = "Main Category";
            this.label27.AutoSize = true;
            this.label27.Location = new Point(0x217, 0x4d);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x4d, 0x11);
            this.label27.TabIndex = 0x43;
            this.label27.Text = "Category 2";
            this.textBox16.CharacterCasing = CharacterCasing.Upper;
            this.textBox16.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox16.Location = new Point(0x19, 0x31);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Size(180, 0x17);
            this.textBox16.TabIndex = 7;
            this.textBox15.CharacterCasing = CharacterCasing.Upper;
            this.textBox15.Location = new Point(0xf9, 0x31);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Size(0x134, 0x17);
            this.textBox15.TabIndex = 8;
            this.label26.AutoSize = true;
            this.label26.Location = new Point(0x178, 0x4d);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x4d, 0x11);
            this.label26.TabIndex = 0x41;
            this.label26.Text = "Category 1";
            this.listBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            object[] items = new object[] { "On", "Off" };
            this.listBox1.Items.AddRange(items);
            this.listBox1.Location = new Point(0x233, 0x30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x5d, 0x18);
            this.listBox1.TabIndex = 0x3f;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0xf6, 0x1f);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x4f, 0x11);
            this.label16.TabIndex = 10;
            this.label16.Text = "Description";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(560, 0x1c);
            this.label13.Name = "label13";
            this.label13.Size = new Size(80, 0x11);
            this.label13.TabIndex = 0x22;
            this.label13.Text = "Sale Status";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x1a, 0x1c);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x5e, 0x11);
            this.label7.TabIndex = 40;
            this.label7.Text = "Product Code";
            this.Btn_AddCart.BackColor = Color.Chocolate;
            this.Btn_AddCart.FlatStyle = FlatStyle.Flat;
            this.Btn_AddCart.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_AddCart.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_AddCart.Location = new Point(0x1a5, 0x188);
            this.Btn_AddCart.Margin = new Padding(8);
            this.Btn_AddCart.Name = "Btn_AddCart";
            this.Btn_AddCart.Size = new Size(170, 0x29);
            this.Btn_AddCart.TabIndex = 0x3e;
            this.Btn_AddCart.Text = "Save Item";
            this.Btn_AddCart.UseVisualStyleBackColor = false;
            this.Btn_AddCart.Click += new EventHandler(this.Btn_SaveProduct_Click);
            this.TabPage_Supplier.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_Supplier.Controls.Add(this.Btn_AddSupplier);
            this.TabPage_Supplier.Controls.Add(this.Btn_NewSupplierClearForm);
            this.TabPage_Supplier.Controls.Add(this.groupBox5);
            this.TabPage_Supplier.Controls.Add(this.groupBox6);
            this.TabPage_Supplier.Controls.Add(this.groupBox7);
            this.TabPage_Supplier.Location = new Point(4, 0x19);
            this.TabPage_Supplier.Name = "TabPage_Supplier";
            this.TabPage_Supplier.Size = new Size(0x4a8, 0x23b);
            this.TabPage_Supplier.TabIndex = 5;
            this.TabPage_Supplier.Text = "Supplier";
            this.Btn_AddSupplier.Location = new Point(0x21f, 0x18f);
            this.Btn_AddSupplier.Name = "Btn_AddSupplier";
            this.Btn_AddSupplier.Size = new Size(0x8b, 0x1f);
            this.Btn_AddSupplier.TabIndex = 9;
            this.Btn_AddSupplier.Text = "AddSupplier";
            this.Btn_AddSupplier.UseVisualStyleBackColor = true;
            this.Btn_AddSupplier.Click += new EventHandler(this.Btn_AddSupplier_Click);
            this.Btn_NewSupplierClearForm.Location = new Point(0x131, 0x18f);
            this.Btn_NewSupplierClearForm.Name = "Btn_NewSupplierClearForm";
            this.Btn_NewSupplierClearForm.Size = new Size(0x8b, 0x1f);
            this.Btn_NewSupplierClearForm.TabIndex = 8;
            this.Btn_NewSupplierClearForm.Text = "New Supplier";
            this.Btn_NewSupplierClearForm.UseVisualStyleBackColor = true;
            this.Btn_NewSupplierClearForm.Click += new EventHandler(this.Btn_NewSupplierClearForm_Click);
            this.groupBox5.Controls.Add(this.Listbox_SupplyActivestatus);
            this.groupBox5.Controls.Add(this.label35);
            this.groupBox5.Controls.Add(this.label36);
            this.groupBox5.Controls.Add(this.label37);
            this.groupBox5.Controls.Add(this.dateTimePicker1);
            this.groupBox5.Controls.Add(this.textBox32);
            this.groupBox5.Dock = DockStyle.Top;
            this.groupBox5.Location = new Point(0, 0x121);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x4a8, 0x5b);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Supply Details";
            this.Listbox_SupplyActivestatus.FormattingEnabled = true;
            this.Listbox_SupplyActivestatus.ItemHeight = 0x10;
            object[] objArray2 = new object[] { "Active", "Inactive" };
            this.Listbox_SupplyActivestatus.Items.AddRange(objArray2);
            this.Listbox_SupplyActivestatus.Location = new Point(0x1c0, 0x2d);
            this.Listbox_SupplyActivestatus.Name = "Listbox_SupplyActivestatus";
            this.Listbox_SupplyActivestatus.Size = new Size(120, 20);
            this.Listbox_SupplyActivestatus.TabIndex = 0x1d;
            this.label35.AutoSize = true;
            this.label35.Location = new Point(0x1bc, 0x16);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x5f, 0x11);
            this.label35.TabIndex = 0x1c;
            this.label35.Text = "Supply Status";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(630, 20);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x76, 0x11);
            this.label36.TabIndex = 0x1a;
            this.label36.Text = "Registration Date";
            this.label37.Location = new Point(6, 0x1b);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0x69, 0x2f);
            this.label37.TabIndex = 0x19;
            this.label37.Text = "Supply Items Description";
            this.dateTimePicker1.Format = DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new Point(0x27a, 0x2b);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new Size(200, 0x17);
            this.dateTimePicker1.TabIndex = 0x18;
            this.textBox32.Location = new Point(0x72, 0x10);
            this.textBox32.MaxLength = 500;
            this.textBox32.Multiline = true;
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Size(0x128, 0x45);
            this.textBox32.TabIndex = 12;
            this.groupBox6.Controls.Add(this.textBox33);
            this.groupBox6.Controls.Add(this.label38);
            this.groupBox6.Controls.Add(this.textBox34);
            this.groupBox6.Controls.Add(this.label39);
            this.groupBox6.Controls.Add(this.textBox35);
            this.groupBox6.Controls.Add(this.label40);
            this.groupBox6.Controls.Add(this.textBox37);
            this.groupBox6.Controls.Add(this.label41);
            this.groupBox6.Dock = DockStyle.Top;
            this.groupBox6.Location = new Point(0, 0xac);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x4a8, 0x75);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Locational Details";
            this.textBox33.Location = new Point(0x1d4, 0x16);
            this.textBox33.MaxLength = 200;
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new Size(0xc7, 0x17);
            this.textBox33.TabIndex = 0x17;
            this.label38.AutoSize = true;
            this.label38.Location = new Point(0x167, 0x1a);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x59, 0x11);
            this.label38.TabIndex = 0x16;
            this.label38.Text = "Constituency";
            this.textBox34.Location = new Point(0x1d4, 0x48);
            this.textBox34.MaxLength = 200;
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new Size(0xc7, 0x17);
            this.textBox34.TabIndex = 11;
            this.label39.AutoSize = true;
            this.label39.Location = new Point(0x178, 0x4e);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x3b, 0x11);
            this.label39.TabIndex = 10;
            this.label39.Text = "P.O Box";
            this.textBox35.Location = new Point(0x83, 0x48);
            this.textBox35.MaxLength = 200;
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new Size(0xc7, 0x17);
            this.textBox35.TabIndex = 0x15;
            this.label40.AutoSize = true;
            this.label40.Location = new Point(0x16, 0x4e);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x34, 0x11);
            this.label40.TabIndex = 20;
            this.label40.Text = "County";
            this.textBox37.Location = new Point(0x83, 0x19);
            this.textBox37.MaxLength = 200;
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new Size(0xc7, 0x17);
            this.textBox37.TabIndex = 0x13;
            this.label41.AutoSize = true;
            this.label41.Location = new Point(0x16, 0x1d);
            this.label41.Name = "label41";
            this.label41.Size = new Size(0x39, 0x11);
            this.label41.TabIndex = 0x12;
            this.label41.Text = "Country";
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Controls.Add(this.textBox38);
            this.groupBox7.Controls.Add(this.label44);
            this.groupBox7.Controls.Add(this.textBox39);
            this.groupBox7.Controls.Add(this.label45);
            this.groupBox7.Controls.Add(this.textBox40);
            this.groupBox7.Controls.Add(this.label46);
            this.groupBox7.Controls.Add(this.textBox41);
            this.groupBox7.Controls.Add(this.textBox42);
            this.groupBox7.Controls.Add(this.textBox43);
            this.groupBox7.Controls.Add(this.label47);
            this.groupBox7.Controls.Add(this.label48);
            this.groupBox7.Controls.Add(this.label49);
            this.groupBox7.Dock = DockStyle.Top;
            this.groupBox7.Location = new Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0x4a8, 0xac);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Personal Information";
            this.groupBox8.Controls.Add(this.radioButton2);
            this.groupBox8.Controls.Add(this.radioButton1);
            this.groupBox8.Location = new Point(360, 0x5b);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new Size(0x11f, 0x35);
            this.groupBox8.TabIndex = 0x1c;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Gender";
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x58, 0x17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x48, 0x15);
            this.radioButton2.TabIndex = 20;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Female";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new Point(10, 0x17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x38, 0x15);
            this.radioButton1.TabIndex = 0x13;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Male";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.textBox38.Location = new Point(110, 0x16);
            this.textBox38.MaxLength = 200;
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new Size(0xd3, 0x17);
            this.textBox38.TabIndex = 0x18;
            this.label44.AutoSize = true;
            this.label44.Location = new Point(0x10, 0x16);
            this.label44.Name = "label44";
            this.label44.Size = new Size(0x4b, 0x11);
            this.label44.TabIndex = 0x17;
            this.label44.Text = "ID Number";
            this.textBox39.Location = new Point(0x1c0, 0x38);
            this.textBox39.MaxLength = 200;
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new Size(0xc7, 0x17);
            this.textBox39.TabIndex = 9;
            this.label45.AutoSize = true;
            this.label45.Location = new Point(0x165, 0x38);
            this.label45.Name = "label45";
            this.label45.Size = new Size(0x4c, 0x11);
            this.label45.TabIndex = 8;
            this.label45.Text = "Telephone";
            this.textBox40.Location = new Point(0x194, 0x10);
            this.textBox40.MaxLength = 200;
            this.textBox40.Name = "textBox40";
            this.textBox40.Size = new Size(0xf3, 0x17);
            this.textBox40.TabIndex = 7;
            this.label46.AutoSize = true;
            this.label46.Location = new Point(0x164, 0x13);
            this.label46.Name = "label46";
            this.label46.Size = new Size(0x2a, 0x11);
            this.label46.TabIndex = 6;
            this.label46.Text = "Email";
            this.textBox41.Location = new Point(110, 0x7c);
            this.textBox41.MaxLength = 200;
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new Size(0xd3, 0x17);
            this.textBox41.TabIndex = 5;
            this.textBox42.Location = new Point(110, 90);
            this.textBox42.MaxLength = 200;
            this.textBox42.Name = "textBox42";
            this.textBox42.Size = new Size(0xd3, 0x17);
            this.textBox42.TabIndex = 4;
            this.textBox43.Location = new Point(110, 0x38);
            this.textBox43.MaxLength = 200;
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new Size(0xd3, 0x17);
            this.textBox43.TabIndex = 3;
            this.label47.AutoSize = true;
            this.label47.Location = new Point(0x10, 0x7c);
            this.label47.Name = "label47";
            this.label47.Size = new Size(0x4c, 0x11);
            this.label47.TabIndex = 2;
            this.label47.Text = "Last Name";
            this.label48.AutoSize = true;
            this.label48.Location = new Point(0x10, 0x59);
            this.label48.Name = "label48";
            this.label48.Size = new Size(90, 0x11);
            this.label48.TabIndex = 1;
            this.label48.Text = "Middle Name";
            this.label49.AutoSize = true;
            this.label49.Location = new Point(15, 0x36);
            this.label49.Name = "label49";
            this.label49.Size = new Size(0x4c, 0x11);
            this.label49.TabIndex = 0;
            this.label49.Text = "First Name";
            this.TabPage_Categories.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_Categories.Controls.Add(this.groupBox1);
            this.TabPage_Categories.Controls.Add(this.groupBox13);
            this.TabPage_Categories.Controls.Add(this.groupBox12);
            this.TabPage_Categories.Controls.Add(this.groupBox9);
            this.TabPage_Categories.Location = new Point(4, 0x19);
            this.TabPage_Categories.Name = "TabPage_Categories";
            this.TabPage_Categories.Size = new Size(0x4a8, 0x23b);
            this.TabPage_Categories.TabIndex = 4;
            this.TabPage_Categories.Text = "Categories";
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0x116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x4a8, 0x125);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Categories List";
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 };
            this.dataGridView1.Columns.AddRange(dataGridViewColumns);
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new Point(3, 0x13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new Size(0x405, 0x10f);
            this.dataGridView1.TabIndex = 0x13;
            this.Column1.FillWeight = 20f;
            this.Column1.HeaderText = "Code";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.HeaderText = "CategoryName";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column3.HeaderText = "SubCategory1";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column4.HeaderText = "SubCategory2";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.panel1.Dock = DockStyle.Right;
            this.panel1.Location = new Point(0x408, 0x13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x9d, 0x10f);
            this.panel1.TabIndex = 20;
            this.groupBox13.Controls.Add(this.Btn_SaveCategory2);
            this.groupBox13.Controls.Add(this.groupBox2);
            this.groupBox13.Controls.Add(this.groupBox15);
            this.groupBox13.Controls.Add(this.groupBox14);
            this.groupBox13.Dock = DockStyle.Top;
            this.groupBox13.Location = new Point(0, 0xb2);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new Size(0x4a8, 100);
            this.groupBox13.TabIndex = 0x12;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "SubCategory 2";
            this.Btn_SaveCategory2.Location = new Point(650, 0x1b);
            this.Btn_SaveCategory2.Name = "Btn_SaveCategory2";
            this.Btn_SaveCategory2.Size = new Size(0x7f, 50);
            this.Btn_SaveCategory2.TabIndex = 60;
            this.Btn_SaveCategory2.Text = "Add SubCategory2";
            this.Btn_SaveCategory2.UseVisualStyleBackColor = true;
            this.Btn_SaveCategory2.Click += new EventHandler(this.Btn_SaveCategory2_Click);
            this.groupBox2.Controls.Add(this.label74);
            this.groupBox2.Controls.Add(this.textBox65);
            this.groupBox2.Dock = DockStyle.Left;
            this.groupBox2.Location = new Point(0x1b3, 0x13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xc0, 0x4e);
            this.groupBox2.TabIndex = 0x3d;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SubCategory 1";
            this.label74.AutoSize = true;
            this.label74.Location = new Point(8, 0x15);
            this.label74.Name = "label74";
            this.label74.Size = new Size(0x8f, 0x11);
            this.label74.TabIndex = 0x36;
            this.label74.Text = "Sub Category2 Name";
            this.textBox65.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox65.Location = new Point(11, 0x29);
            this.textBox65.Name = "textBox65";
            this.textBox65.Size = new Size(0xa7, 0x1a);
            this.textBox65.TabIndex = 0x39;
            this.groupBox15.Controls.Add(this.comboBox3);
            this.groupBox15.Controls.Add(this.label71);
            this.groupBox15.Dock = DockStyle.Left;
            this.groupBox15.Location = new Point(0xe7, 0x13);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new Size(0xcc, 0x4e);
            this.groupBox15.TabIndex = 50;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "SubCategory 1";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new Point(7, 40);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0xbf, 0x1c);
            this.comboBox3.TabIndex = 0x3d;
            this.label71.AutoSize = true;
            this.label71.Location = new Point(10, 0x15);
            this.label71.Name = "label71";
            this.label71.Size = new Size(0x83, 0x11);
            this.label71.TabIndex = 60;
            this.label71.Text = "SubCategory Name";
            this.groupBox14.Controls.Add(this.ComboBox2);
            this.groupBox14.Controls.Add(this.label67);
            this.groupBox14.Dock = DockStyle.Left;
            this.groupBox14.Location = new Point(3, 0x13);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new Size(0xe4, 0x4e);
            this.groupBox14.TabIndex = 0x31;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Main Category";
            this.ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Location = new Point(10, 40);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new Size(0xbb, 0x1c);
            this.ComboBox2.TabIndex = 0x3d;
            this.ComboBox2.SelectedIndexChanged += new EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.label67.AutoSize = true;
            this.label67.Location = new Point(10, 0x15);
            this.label67.Name = "label67";
            this.label67.Size = new Size(140, 0x11);
            this.label67.TabIndex = 60;
            this.label67.Text = "Main Category Name";
            this.groupBox12.Controls.Add(this.Btn_RefreshCategories);
            this.groupBox12.Controls.Add(this.Btn_SaveCategory1);
            this.groupBox12.Controls.Add(this.groupBox16);
            this.groupBox12.Controls.Add(this.textBox25);
            this.groupBox12.Controls.Add(this.label51);
            this.groupBox12.Dock = DockStyle.Top;
            this.groupBox12.Location = new Point(0, 0x4e);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new Size(0x4a8, 100);
            this.groupBox12.TabIndex = 0x11;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "SubCategory 1";
            this.Btn_RefreshCategories.Location = new Point(650, 0x1c);
            this.Btn_RefreshCategories.Name = "Btn_RefreshCategories";
            this.Btn_RefreshCategories.Size = new Size(0x7f, 50);
            this.Btn_RefreshCategories.TabIndex = 0x3e;
            this.Btn_RefreshCategories.Text = "Refresh Categories";
            this.Btn_RefreshCategories.UseVisualStyleBackColor = true;
            this.Btn_RefreshCategories.Click += new EventHandler(this.Btn_RefreshCategories_Click);
            this.Btn_SaveCategory1.Location = new Point(0x1d4, 0x1c);
            this.Btn_SaveCategory1.Name = "Btn_SaveCategory1";
            this.Btn_SaveCategory1.Size = new Size(0x7f, 50);
            this.Btn_SaveCategory1.TabIndex = 0x37;
            this.Btn_SaveCategory1.Text = "Add SubCategory1";
            this.Btn_SaveCategory1.UseVisualStyleBackColor = true;
            this.Btn_SaveCategory1.Click += new EventHandler(this.Btn_SaveCategory1_Click);
            this.groupBox16.Controls.Add(this.comboBox1);
            this.groupBox16.Controls.Add(this.label70);
            this.groupBox16.Dock = DockStyle.Left;
            this.groupBox16.Location = new Point(3, 0x13);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new Size(0xfe, 0x4e);
            this.groupBox16.TabIndex = 0x36;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Main Category";
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(8, 0x27);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xbb, 0x1c);
            this.comboBox1.TabIndex = 0x3d;
            this.label70.AutoSize = true;
            this.label70.Location = new Point(10, 0x13);
            this.label70.Name = "label70";
            this.label70.Size = new Size(140, 0x11);
            this.label70.TabIndex = 60;
            this.label70.Text = "Main Category Name";
            this.textBox25.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox25.Location = new Point(0x10a, 0x30);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Size(0xa5, 0x1a);
            this.textBox25.TabIndex = 50;
            this.label51.AutoSize = true;
            this.label51.Location = new Point(0x111, 0x1c);
            this.label51.Name = "label51";
            this.label51.Size = new Size(0x8f, 0x11);
            this.label51.TabIndex = 0x2d;
            this.label51.Text = "Sub Category1 Name";
            this.groupBox9.Controls.Add(this.Btn_SaveMainCategory);
            this.groupBox9.Controls.Add(this.label50);
            this.groupBox9.Controls.Add(this.textBox46);
            this.groupBox9.Dock = DockStyle.Top;
            this.groupBox9.Location = new Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new Size(0x4a8, 0x4e);
            this.groupBox9.TabIndex = 0x10;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Main Category";
            this.Btn_SaveMainCategory.Location = new Point(0x1d4, 20);
            this.Btn_SaveMainCategory.Name = "Btn_SaveMainCategory";
            this.Btn_SaveMainCategory.Size = new Size(0x7f, 50);
            this.Btn_SaveMainCategory.TabIndex = 0x2b;
            this.Btn_SaveMainCategory.Text = "Add MainCategory";
            this.Btn_SaveMainCategory.UseVisualStyleBackColor = true;
            this.Btn_SaveMainCategory.Click += new EventHandler(this.Btn_SaveMainCategory_Click);
            this.label50.AutoSize = true;
            this.label50.Location = new Point(8, 0x20);
            this.label50.Name = "label50";
            this.label50.Size = new Size(0x6a, 0x11);
            this.label50.TabIndex = 10;
            this.label50.Text = "Category Name";
            this.textBox46.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox46.Location = new Point(0x90, 0x20);
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Size(0x11d, 0x1a);
            this.textBox46.TabIndex = 8;
            this.Tabpage_BulkProducts.BackColor = SystemColors.ButtonHighlight;
            this.Tabpage_BulkProducts.Controls.Add(this.panel3);
            this.Tabpage_BulkProducts.Controls.Add(this.panel7);
            this.Tabpage_BulkProducts.Controls.Add(this.groupBox10);
            this.Tabpage_BulkProducts.Location = new Point(4, 0x19);
            this.Tabpage_BulkProducts.Name = "Tabpage_BulkProducts";
            this.Tabpage_BulkProducts.Padding = new Padding(3);
            this.Tabpage_BulkProducts.Size = new Size(0x4a8, 0x23b);
            this.Tabpage_BulkProducts.TabIndex = 9;
            this.Tabpage_BulkProducts.Text = "BulkProducts";
            this.panel3.Controls.Add(this.Products_Gridview);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(3, 0x37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x4a2, 0x1dd);
            this.panel3.TabIndex = 0x43;
            this.Products_Gridview.AllowUserToAddRows = false;
            this.Products_Gridview.AllowUserToResizeRows = false;
            style.BackColor = SystemColors.ButtonHighlight;
            style.Font = new Font("Palatino Linotype", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = Color.Black;
            this.Products_Gridview.AlternatingRowsDefaultCellStyle = style;
            this.Products_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Products_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            this.Products_Gridview.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            style2.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style2.ForeColor = Color.FromArgb(0, 0, 0x40);
            style2.SelectionBackColor = Color.LightSalmon;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.Products_Gridview.ColumnHeadersDefaultCellStyle = style2;
            this.Products_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[15];
            columnArray2[0] = this.dataGridViewTextBoxColumn5;
            columnArray2[1] = this.dataGridViewTextBoxColumn10;
            columnArray2[2] = this.dataGridViewTextBoxColumn11;
            columnArray2[3] = this.dataGridViewTextBoxColumn12;
            columnArray2[4] = this.dataGridViewTextBoxColumn13;
            columnArray2[5] = this.dataGridViewTextBoxColumn14;
            columnArray2[6] = this.dataGridViewTextBoxColumn15;
            columnArray2[7] = this.dataGridViewTextBoxColumn16;
            columnArray2[8] = this.dataGridViewTextBoxColumn17;
            columnArray2[9] = this.dataGridViewTextBoxColumn18;
            columnArray2[10] = this.dataGridViewTextBoxColumn19;
            columnArray2[11] = this.dataGridViewTextBoxColumn20;
            columnArray2[12] = this.dataGridViewTextBoxColumn21;
            columnArray2[13] = this.dataGridViewTextBoxColumn22;
            columnArray2[14] = this.dataGridViewTextBoxColumn23;
            this.Products_Gridview.Columns.AddRange(columnArray2);
            this.Products_Gridview.Dock = DockStyle.Fill;
            this.Products_Gridview.EditMode = DataGridViewEditMode.EditOnEnter;
            this.Products_Gridview.EnableHeadersVisualStyles = false;
            this.Products_Gridview.Location = new Point(0, 0);
            this.Products_Gridview.Name = "Products_Gridview";
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = Color.Sienna;
            style3.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style3.ForeColor = SystemColors.WindowText;
            style3.NullValue = "X";
            style3.SelectionBackColor = SystemColors.Highlight;
            style3.SelectionForeColor = SystemColors.HighlightText;
            style3.WrapMode = DataGridViewTriState.True;
            this.Products_Gridview.RowHeadersDefaultCellStyle = style3;
            this.Products_Gridview.RowHeadersWidth = 20;
            this.Products_Gridview.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            style4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style4.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style4.NullValue = null;
            this.Products_Gridview.RowsDefaultCellStyle = style4;
            this.Products_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 10f);
            this.Products_Gridview.RowTemplate.Height = 20;
            this.Products_Gridview.Size = new Size(0x4a2, 0x1dd);
            this.Products_Gridview.TabIndex = 0x43;
            style5.BackColor = Color.White;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = style5;
            this.dataGridViewTextBoxColumn5.HeaderText = "ProductId";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.HeaderText = "Description";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            style6.NullValue = null;
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = style6;
            this.dataGridViewTextBoxColumn11.HeaderText = "MainCat";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn12.HeaderText = "Cat1";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn13.HeaderText = "Cat2";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn14.HeaderText = "MinL";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.HeaderText = "MaxL";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.HeaderText = "Unit";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.HeaderText = "OpennigStock";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn18.HeaderText = "Bprice";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn19.HeaderText = "Sprice";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn20.HeaderText = "Gsst";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn21.HeaderText = "Vat";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn22.HeaderText = "Tax3";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn23.HeaderText = "Status";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.panel7.Controls.Add(this.Btn_ClearGridview);
            this.panel7.Controls.Add(this.Btn_SaveCsvItems);
            this.panel7.Controls.Add(this.Btn_UploadCSVFile);
            this.panel7.Dock = DockStyle.Bottom;
            this.panel7.Location = new Point(3, 0x214);
            this.panel7.Name = "panel7";
            this.panel7.Size = new Size(0x4a2, 0x24);
            this.panel7.TabIndex = 0x42;
            this.Btn_ClearGridview.BackColor = Color.Chocolate;
            this.Btn_ClearGridview.FlatStyle = FlatStyle.Flat;
            this.Btn_ClearGridview.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_ClearGridview.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_ClearGridview.Location = new Point(0x25, 4);
            this.Btn_ClearGridview.Margin = new Padding(8);
            this.Btn_ClearGridview.Name = "Btn_ClearGridview";
            this.Btn_ClearGridview.Size = new Size(0xdf, 0x1d);
            this.Btn_ClearGridview.TabIndex = 0x41;
            this.Btn_ClearGridview.Text = "Clear Form";
            this.Btn_ClearGridview.UseVisualStyleBackColor = false;
            this.Btn_ClearGridview.Click += new EventHandler(this.Btn_ClearGridview_Click);
            this.Btn_SaveCsvItems.BackColor = Color.Chocolate;
            this.Btn_SaveCsvItems.FlatStyle = FlatStyle.Flat;
            this.Btn_SaveCsvItems.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_SaveCsvItems.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_SaveCsvItems.Location = new Point(0x260, 4);
            this.Btn_SaveCsvItems.Margin = new Padding(8);
            this.Btn_SaveCsvItems.Name = "Btn_SaveCsvItems";
            this.Btn_SaveCsvItems.Size = new Size(0xdf, 0x1d);
            this.Btn_SaveCsvItems.TabIndex = 0x35;
            this.Btn_SaveCsvItems.Text = "Save Products";
            this.Btn_SaveCsvItems.UseVisualStyleBackColor = false;
            this.Btn_SaveCsvItems.Click += new EventHandler(this.Btn_SaveCsvItems_Click);
            this.Btn_UploadCSVFile.BackColor = Color.FromArgb(0xc0, 0xc0, 0);
            this.Btn_UploadCSVFile.FlatStyle = FlatStyle.Flat;
            this.Btn_UploadCSVFile.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_UploadCSVFile.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_UploadCSVFile.Location = new Point(0x130, 3);
            this.Btn_UploadCSVFile.Margin = new Padding(8);
            this.Btn_UploadCSVFile.Name = "Btn_UploadCSVFile";
            this.Btn_UploadCSVFile.Size = new Size(0x113, 30);
            this.Btn_UploadCSVFile.TabIndex = 0x40;
            this.Btn_UploadCSVFile.Text = "Upload From File";
            this.Btn_UploadCSVFile.UseVisualStyleBackColor = false;
            this.Btn_UploadCSVFile.Click += new EventHandler(this.Btn_UploadFromFile_Click);
            this.groupBox10.Dock = DockStyle.Top;
            this.groupBox10.Location = new Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new Size(0x4a2, 0x34);
            this.groupBox10.TabIndex = 0x11;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Product Information";
            this.TabPage_MeasurementsUnits.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_MeasurementsUnits.Location = new Point(4, 0x19);
            this.TabPage_MeasurementsUnits.Name = "TabPage_MeasurementsUnits";
            this.TabPage_MeasurementsUnits.Padding = new Padding(3);
            this.TabPage_MeasurementsUnits.Size = new Size(0x4a8, 0x23b);
            this.TabPage_MeasurementsUnits.TabIndex = 10;
            this.TabPage_MeasurementsUnits.Text = "Measurement Units";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x4b0, 600);
            base.Controls.Add(this.Tab_MasterEntries);
            this.DoubleBuffered = true;
            base.Name = "MasterEntries";
            base.Load += new EventHandler(this.MasterEntries_Load);
            this.Tab_MasterEntries.ResumeLayout(false);
            this.TabPage_Product.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.TabPage_Supplier.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.TabPage_Categories.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.Tabpage_BulkProducts.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((ISupportInitialize) this.Products_Gridview).EndInit();
            this.panel7.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public int InsertMultipleProducts()
        {
            MySqlTransaction transaction = null;
            int num3;
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                transaction = connection.BeginTransaction();
                int num = 0;
                while (true)
                {
                    if (num >= this.Products_Gridview.Rows.Count)
                    {
                        transaction.Commit();
                        num3 = 1;
                        break;
                    }
                    MySqlCommand command = new MySqlCommand("Insert into inventorymaster (ProductCode,Description,CatMain,Cat1,Cat2,InsertionDate,MinLevel,MaxLevel,LastRestock,SellStatus,StockBalance,SuppliersPack,AG) values(@ProductCode,@Description,@CatMain,@cat1,@cat2,@InsertionDate,@MinLevel,@MaxLevel,@LastRestock,@SellStatus,@StockBalance,@SuppliersPack,@AG)", connection, transaction);
                    command.Parameters.AddWithValue("@ProductCode", this.Products_Gridview.Rows[num].Cells[0].Value.ToString());
                    command.Parameters.AddWithValue("@Description", this.Products_Gridview.Rows[num].Cells[1].Value.ToString());
                    command.Parameters.AddWithValue("@CatMain", this.Products_Gridview.Rows[num].Cells[2].Value.ToString());
                    command.Parameters.AddWithValue("@cat1", this.Products_Gridview.Rows[num].Cells[3].Value.ToString());
                    command.Parameters.AddWithValue("@cat2", this.Products_Gridview.Rows[num].Cells[4].Value.ToString());
                    command.Parameters.AddWithValue("@InsertionDate", InsertionDate);
                    command.Parameters.AddWithValue("@MinLevel", this.Products_Gridview.Rows[num].Cells[5].Value.ToString());
                    command.Parameters.AddWithValue("@MaxLevel", this.Products_Gridview.Rows[num].Cells[6].Value.ToString());
                    command.Parameters.AddWithValue("@LastRestock", InsertionDate);
                    command.Parameters.AddWithValue("@SellStatus", this.Products_Gridview.Rows[num].Cells[14].Value.ToString().Trim());
                    command.Parameters.AddWithValue("@StockBalance", this.Products_Gridview.Rows[num].Cells[8].Value.ToString());
                    command.Parameters.AddWithValue("@SuppliersPack", "N/A");
                    command.Parameters.AddWithValue("@AG", "0");
                    int num2 = command.ExecuteNonQuery();
                    command.Dispose();
                    MySqlCommand command2 = new MySqlCommand("Insert into productprice (ProductCode,SellingUnit,Pprice,SellingUnitPrice,Disc,GSST,VAT,TAX3) values(@ProductCode,@SellingUnit,@Pprice,@SellingUnitPrice,@Disc,@GSST,@VAT,@TAX3)", connection, transaction);
                    command2.Parameters.AddWithValue("@ProductCode", this.Products_Gridview.Rows[num].Cells[0].Value.ToString());
                    command2.Parameters.AddWithValue("@Pprice", this.Products_Gridview.Rows[num].Cells[9].Value.ToString());
                    command2.Parameters.AddWithValue("@SellingUnit", this.Products_Gridview.Rows[num].Cells[7].Value.ToString());
                    command2.Parameters.AddWithValue("@SellingUnitPrice", this.Products_Gridview.Rows[num].Cells[10].Value);
                    command2.Parameters.AddWithValue("@Disc", 0);
                    command2.Parameters.AddWithValue("@GSST", this.Products_Gridview.Rows[num].Cells[11].Value);
                    command2.Parameters.AddWithValue("@VAT", this.Products_Gridview.Rows[num].Cells[12].Value);
                    command2.Parameters.AddWithValue("@TAX3", this.Products_Gridview.Rows[num].Cells[13].Value);
                    command2.ExecuteNonQuery();
                    command2.Dispose();
                    num++;
                }
            }
            catch (Exception exception1)
            {
                transaction.Rollback();
                int num4 = 0;
                MessageBox.Show(exception1.Message, "Error message", MessageBoxButtons.OK);
                num3 = num4;
            }
            return num3;
        }

        public int InsertSingleNewProduct(string Itemid, int AutoGenId)
        {
            MySqlTransaction transaction = null;
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                transaction = connection.BeginTransaction();
                MySqlCommand command = new MySqlCommand("Insert into inventorymaster (ProductCode,Description,CatMain,Cat1,Cat2,InsertionDate,MinLevel,MaxLevel,LastRestock,SellStatus,StockBalance,SuppliersPack,AG) values(@ProductCode,@Description,@CatMain,@cat1,@cat2,@InsertionDate,@MinLevel,@MaxLevel,@LastRestock,@SellStatus,@StockBalance,@SuppliersPack,@AG)", connection, transaction);
                command.Parameters.AddWithValue("@ProductCode", Itemid);
                command.Parameters.AddWithValue("@Description", this.textBox15.Text.ToString());
                command.Parameters.AddWithValue("@CatMain", this.ComboBox4.Text.ToString());
                command.Parameters.AddWithValue("@cat1", this.ComboBox5.Text.ToString());
                command.Parameters.AddWithValue("@cat2", this.ComboBox6.Text.ToString());
                command.Parameters.AddWithValue("@InsertionDate", InsertionDate);
                command.Parameters.AddWithValue("@MinLevel", this.textBox12.Text.ToString());
                command.Parameters.AddWithValue("@MaxLevel", this.textBox1.Text.ToString());
                command.Parameters.AddWithValue("@LastRestock", InsertionDate);
                command.Parameters.AddWithValue("@SellStatus", this.listBox1.Text.ToString());
                command.Parameters.AddWithValue("@StockBalance", this.textBox18.Text.ToString());
                command.Parameters.AddWithValue("@SuppliersPack", this.ComboBox8.Text.ToString());
                command.Parameters.AddWithValue("@AG", AutoGenId);
                int num = command.ExecuteNonQuery();
                command.Dispose();
                MySqlCommand command2 = new MySqlCommand("Insert into productprice (ProductCode,SellingUnit,Pprice,SellingUnitPrice,Disc,GSST,VAT,TAX3) values(@ProductCode,@SellingUnit,@Pprice,@SellingUnitPrice,@Disc,@GSST,@VAT,@TAX3)", connection, transaction);
                command2.Parameters.AddWithValue("@ProductCode", Itemid);
                command2.Parameters.AddWithValue("@Pprice", this.textBox14.Text);
                command2.Parameters.AddWithValue("@SellingUnit", this.ComboBox7.Text.ToString());
                command2.Parameters.AddWithValue("@SellingUnitPrice", this.textBox19.Text);
                command2.Parameters.AddWithValue("@Disc", 0);
                command2.Parameters.AddWithValue("@GSST", this.textBox48.Text);
                command2.Parameters.AddWithValue("@VAT", this.textBox49.Text);
                command2.Parameters.AddWithValue("@TAX3", this.textBox50.Text);
                command2.ExecuteNonQuery();
                command2.Dispose();
                transaction.Commit();
                return 1;
            }
            catch (Exception exception1)
            {
                transaction.Rollback();
                MessageBox.Show(exception1.Message, "Error message", MessageBoxButtons.OK);
                return 0;
            }
        }

        public void InsertSupplier(string SupplID, string Gender)
        {
            DateTimePicker picker1 = new DateTimePicker();
            picker1.Value = Program.CurrentDateTime();
            picker1.Format = DateTimePickerFormat.Short;
            DateTimePicker picker = picker1;
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO  suppliersdetails (`SupId`, `IdNumber`, `FirstName`, `MiddleName`, `LastName`, `Gender`, `MobileNo`, `Email`, `Country`, `County`, `Constituency`, `BoxAddress`,`SupplyStatus`, `RegDate`,`SupplyDescription`) values(@SupId,@IdNumber,@FirstName,@MiddleName,@LastName,@Gender,@MobileNo,@Email,@Country,@County,@Constituency,@BoxAddress,@SupplyStatus,@RegDate,@Description);";
                command.Parameters.AddWithValue("@SupId", SupplID);
                command.Parameters.AddWithValue("@IdNumber", this.textBox38.Text.ToString());
                command.Parameters.AddWithValue("@FirstName", this.textBox43.Text.ToString());
                command.Parameters.AddWithValue("@MiddleName", this.textBox42.Text.ToString());
                command.Parameters.AddWithValue("@LastName", this.textBox41.Text.ToString());
                command.Parameters.AddWithValue("@Gender", Gender);
                command.Parameters.AddWithValue("@MobileNo", this.textBox39.Text.ToString());
                command.Parameters.AddWithValue("@Email", this.textBox40.Text.ToString());
                command.Parameters.AddWithValue("@Country", this.textBox37.Text.ToString());
                command.Parameters.AddWithValue("@County", this.textBox35.Text.ToString());
                command.Parameters.AddWithValue("@Constituency", this.textBox33.Text.ToString());
                command.Parameters.AddWithValue("@BoxAddress", this.textBox34.Text.ToString());
                command.Parameters.AddWithValue("SupplyStatus", this.Listbox_SupplyActivestatus.SelectedItem.ToString());
                command.Parameters.AddWithValue("@RegDate", picker.Value);
                command.Parameters.AddWithValue("@Description", this.textBox32.Text);
                if (command.ExecuteNonQuery() <= 0)
                {
                    MessageBox.Show("Failed To Register The Supplier !!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show("The Supplier has been successfully rregistered", "SUCCESS MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.ResetSuppliersForm();
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ListCategoriesForCategories(int Level)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                if (Level == 0)
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM `p.o.s`.categories where Category1=@cat1 AND Category2=@cat2;";
                    command.Parameters.AddWithValue("@cat1", "#");
                    command.Parameters.AddWithValue("@cat2", "#");
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("No MainCategories Found", "Loading Categories", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        this.comboBox1.Items.Clear();
                        this.ComboBox2.Items.Clear();
                        this.ComboBox4.Items.Clear();
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                break;
                            }
                            this.comboBox1.Items.Add(reader["CatName"].ToString());
                            this.ComboBox2.Items.Add(reader["CatName"].ToString());
                            this.ComboBox4.Items.Add(reader["CatName"].ToString());
                        }
                    }
                }
                else if (Level == 1)
                {
                    MySqlCommand command2 = connection.CreateCommand();
                    command2.CommandType = CommandType.Text;
                    command2.CommandText = "SELECT * FROM `p.o.s`.categories where CatName=@main AND Category1!=@cat1 AND Category2=@cat2;";
                    command2.Parameters.AddWithValue("@main", this.ComboBox2.Text);
                    command2.Parameters.AddWithValue("@cat1", "#");
                    command2.Parameters.AddWithValue("@cat2", "#");
                    MySqlDataReader reader2 = command2.ExecuteReader();
                    if (!reader2.HasRows)
                    {
                        MessageBox.Show("No Category1 Items Found", "Loading Categories", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.comboBox3.Items.Clear();
                    }
                    else
                    {
                        this.comboBox3.Items.Clear();
                        while (true)
                        {
                            if (!reader2.Read())
                            {
                                break;
                            }
                            this.comboBox3.Items.Add(reader2["Category1"].ToString());
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void LoadUnits()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Unitname FROM `p.o.s`.units;";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    this.ComboBox7.Items.Clear();
                    this.ComboBox8.Items.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.ComboBox7.Items.Add(reader[0].ToString());
                        this.ComboBox8.Items.Add(reader[0].ToString());
                    }
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
            }
        }

        public void MasterEntries_Load(object sender, EventArgs e)
        {
            this.PullDatabase();
            this.LoadUnits();
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

        private void Products_Gridview_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete?", "Message Box", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        public void PullDatabase()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM `p.o.s`.categories;";
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No categories found", "Initializing Datalist", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.dataGridView1.Rows.Clear();
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString() };
                        this.dataGridView1.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ResetSuppliersForm()
        {
            this.textBox32.Text = "";
            this.textBox33.Text = "";
            this.textBox34.Text = "";
            this.textBox35.Text = "";
            this.textBox41.Text = "";
            this.textBox37.Text = "";
            this.textBox38.Text = "";
            this.textBox39.Text = "";
            this.textBox40.Text = "";
            this.textBox42.Text = "";
            this.textBox43.Text = "";
            this.Listbox_SupplyActivestatus.SelectedIndex = 0;
            this.dateTimePicker1.Value = Program.CurrentDateTime();
            this.radioButton1.Checked = false;
            this.radioButton2.Checked = false;
        }

        private void Tab_Suppliers_Selected(object sender, TabControlEventArgs e)
        {
            if (this.Tab_MasterEntries.SelectedTab.Name == "TabPage_Categories")
            {
                this.PullDatabase();
            }
            if (this.Tab_MasterEntries.SelectedTab.Name == "TabPage_Product")
            {
                this.CategorieslistForProducts(0);
            }
        }

        private void Tab_Suppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.ParentForm.AcceptButton = null;
        }

        public int UniqueIdCheck(string Checkid)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from inventorymaster where ProductCode=@id";
                command.Parameters.AddWithValue("@id", Checkid);
                return (!command.ExecuteReader().HasRows ? 0 : 1);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
                return -1;
            }
        }
    }
}

