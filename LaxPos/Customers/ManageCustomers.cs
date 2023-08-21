namespace LaxPos.Customers
{ 
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class ManageCustomers : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
       

        public ManageCustomers()
        {

            InitializeComponent();
        }

        private void Btn_AddCustomer_Click(object sender, EventArgs e)
        {
            if (((this.textBox1.Text != "") && ((this.textBox2.Text != "") && ((this.textBox3.Text != "") && ((this.textBox4.Text != "") && ((this.textBox5.Text != "") && (this.textBox6.Text != "")))))) && (this.radioButton1.Checked || this.radioButton2.Checked))
            {
                this.InsertCustomer();
            }
            else
            {
                MessageBox.Show("Incomplete Customer Details", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
       
        public void InsertCustomer()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                if (new MySqlCommand("INSERT INTO customersinvoiced (IdNumber, CustomerType, Name, Gender, MaxAmount, Phone, Email, Address, InvoiceStatus, RegDate, Counter, UserId) VALUES(@IdNumber, CustomerType, Name, Gender, MaxAmount, @Phone, @Email,@ Address, InvoiceStatus, RegDate, Counter, UserId);", connection).ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("You have successfully registered the Customer", "SUCCESS", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("failed to register the Customer", "Regitrastion failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message);
            }
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ManageCustomers
            // 
            this.ClientSize = new System.Drawing.Size(557, 279);
            this.Name = "ManageCustomers";
            this.ResumeLayout(false);

        }
    }
    partial class ManageCustomers 
    {
       
        #region

        private IContainer components = null;
        private TabControl TabControl_ManageCustomers;
        private TabPage TabPage_NewCustomer;
        private GroupBox groupBox1;
        private Label label5;
        private TextBox textBox4;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Button Btn_Clear;
        private Button Btn_AddCustomer;
        private GroupBox groupBox2;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label1;
        private Label label4;
        private TextBox textBox1;
        private Label label2;
        private TabPage TabPage_EditCustomer;
        private TabPage Tab_Page_CustomerAccount;
        private TextBox textBox5;
        private Label label6;
        private Panel panel3;
        private Panel panel2;
        private Button Btn_searchAccount;
        private Label label23;
        private TextBox textBox23;
        private TabPage TabPage_CustomersList;
        private TextBox textBox6;
        private GroupBox groupBox3;
        private Panel panel1;
        private Button Btn_Search;
        private Label label22;
        private TextBox textBox22;
        private TextBox textBox7;
        private Label label7;
        private TextBox textBox8;
        private Label label8;
        private TextBox textBox9;
        private TextBox textBox10;
        private Label label9;
        private TextBox textBox11;
        private GroupBox groupBox4;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private Label label10;
        private Label label11;
        private TextBox textBox12;
        private Label label12;
        private GroupBox groupBox9;
        private Button Btn_Cancel;
        private Button Btn_Update;
        private TextBox textBox13;
        private Label label13;
        private TextBox textBox14;
        private Label label14;
        private TextBox textBox15;
        private TextBox textBox16;
        private Label label15;
        private TextBox textBox17;
        private GroupBox groupBox5;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private Label label16;
        private Label label17;
        private GroupBox groupBox6;
        private TextBox textBox18;
        private Label label18;
        private TextBox textBox19;
        private Label label19;
        private TextBox textBox20;
        private TextBox textBox21;
        private Label label20;
        private TextBox textBox24;
        private GroupBox groupBox7;
        private RadioButton radioButton7;
        private RadioButton radioButton8;
        private Label label21;
        private Label label24;
        private TextBox textBox25;
        private Label label25;
        private DataGridView dataGridView1;
        private Panel panel6;
        private Panel panel5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column10;
       
        
        #endregion
    }
}

