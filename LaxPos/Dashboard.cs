using LaxExpanderPanel;
using LaxPos.Accounting;
using LaxPos.Administration;
using LaxPos.Customers;
using LaxPos.Expenses;
using LaxPos.Inventory;
using LaxPos.LaxPosFiles;
using LaxPos.Pos;
using LaxPos.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace LaxPos
{ 
    public partial class Dashboard : Form, IMessageFilter
    {
        //private const int WM_LBUTTONDOWN = 0x201;
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public readonly Login L;
        private Form lastopenform=null;
       
        //private IContainer components = null;
     

        public Dashboard()
        {
            this.InitializeComponent();
            Application.AddMessageFilter(this);
            this.Navigation_Panel.Width = 170;
            foreach (Control control in this.MainMenuPanel.Controls)
            {
                if (control.GetType() == typeof(SplitContainer))
                {
                    control.Height = 40;
                }
            }
            this.Administration_Container.HeaderText.Text = "Administration";
            this.Accounting_Container.HeaderText.Text = "Accounting";
            this.Inventory_Container.HeaderText.Text = "Inventory";
            this.Supervisor_Container.HeaderText.Text = "Supervisor";
            this.CashierServices_Container.HeaderText.Text = "CashierServices";
            this.CustomerSupport_Container.HeaderText.Text = "CustomerSupport";
            this.GenerateMenu();
        }

        private void UpdateLastTask()
        {
            Program.LastActionTime = DateTime.Now;
        }

        private void AutologoutTimer_Tick(object sender, EventArgs e)
        {
            this.CheckLoginStatus();
        }

        public bool BackupDB()
        {
            bool flag;
            try
            {
                string filePath = @"D:\LaxcoPosBackup.SQL";
                using (MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString() + "convertzerodatetime=true;"))
                {
                    using (MySqlCommand command = new MySqlCommand())
                    {
                        using (MySqlBackup backup = new MySqlBackup(command))
                        {
                            command.Connection = connection;
                            connection.Open();
                            backup.ExportToFile(filePath);
                            connection.Close();
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag = false;
            }
            return flag;
        }

        private void Btn_ExpandMenuPanel_Click(object sender, EventArgs e)
        {
            this.Navigation_Panel.Width = 170;
            this.Btn_ExpandMenuPanel.Visible = false;
            this.Btn_MinimizeMenuPanel.Visible = true;
        }

        private void Btn_Logout_Click(object sender, EventArgs e)
        {
            Program.LastActionTime = DateTime.Now;
            this.AutologoutTimer.Stop();
            DialogResult result = MessageBox.Show(this, "Do you want to keep Your Session?", "Message Box", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.LogoutProcess(false);
            }
            else if (result == DialogResult.No)
            {
                this.LogoutProcess(true);
            }
            else
            {
                Program.LastActionTime = DateTime.Now;
                this.AutologoutTimer.Start();
            }
        }

        private void Btn_MinimizeMenuPanel_Click(object sender, EventArgs e)
        {
            this.Btn_ExpandMenuPanel.Visible = true;
            this.Btn_MinimizeMenuPanel.Visible = false;
            this.Navigation_Panel.Width = 0x23;
        }

        private void CheckLoginStatus()
        {
            try
            {
                DateTime time = Program.LastActionTime.AddSeconds((double) Program.AutologoutTime_seconds);
                if (DateTime.Now > time)
                {
                    this.AutologoutTimer.Stop();
                    this.LogoutProcess(false);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.AutologoutTimer.Stop();
                Application.Exit();
            }
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.Shuttingdown)
            {
                DialogResult result = MessageBox.Show(this, "Do you want to Exit?", "Message Box", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    BackupDB();
                    Application.ExitThread(); 
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                Program.CurrLoginForm.Show();
            }
        }
        private void Dashboard_KeyDown(object sender, KeyEventArgs e)
        {
            this.UpdateLastTask();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                Home home1 = new Home
                {
                    MdiParent = this,
                    Dock = DockStyle.Fill
                };
                Home home = home1;
                this.AutologoutTimer.Start();
                this.label1.Text = "Role: " + Program.CurrLoggedInUser.UserRole;
                this.label2.Text = "User: " + Program.CurrLoggedInUser.UserID;
                this.label3.Text = "Counter: " + Program.LogInCounter;
                this.MainMenuPanel.AutoScroll = true;
                home.Show();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                this.NotifyIcon1.Visible = true;
                this.NotifyIcon1.ShowBalloonTip(0x7d0);
            }
        }

        private void Dashboard_Shown(object sender, EventArgs e)
        {
            Program.Shuttingdown = true;
            this.LoadAutocompleteProducts();
        }

       

        private void ExpIcon_Click(object sender, EventArgs e)
        {
            try
            {
                if ((sender as Control).Parent.Parent.GetType() == typeof(SplitContainer))
                {
                    SplitContainer parent = (sender as PictureBox).Parent.Parent as SplitContainer;
                    this.ToggleExpander(parent, sender as PictureBox);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK);
            }
        }

        private void GenerateMenu()
        {
            List<string> userRights = Program.CurrLoggedInUser.UserRights;
            userRights.Reverse();
            foreach (string x in userRights)
            {
                try
                {
                    if ((from m in Program.Fun_Rights
                        where (m.RightLevel == 1) && (m.RightID == x)
                        select m).Count<FunctionalityRight>() > 0)
                    {
                        Button button1 = new Button
                        {
                            Name = x,
                            BackColor = Color.White,
                            Cursor = Cursors.Hand,
                            DialogResult = DialogResult.None,
                            Dock = DockStyle.Top,
                            Font = new Font("Segoe UI Semibold", 12f),
                            ForeColor = Color.FromArgb(0x5e, 0, 0x12),
                            Location = new Point(0, 0),
                            Margin = new Padding(0),
                            Size = new Size(0xb6, 40),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = (from n in Program.Fun_Rights
                                    where n.RightID == x
                                    select n).First<FunctionalityRight>().RightShortName,
                            FlatStyle = FlatStyle.Flat
                        };
                        Button button = button1;
                        button.FlatAppearance.BorderColor = Color.White;
                        button.Click += new EventHandler(this.MenuItemClick);
                        if (x.Contains("A"))
                        {
                            this.Administration_Container.ContentPanel.Controls.Add(button);
                        }
                        else if (x.Contains("B"))
                        {
                            this.Accounting_Container.ContentPanel.Controls.Add(button);
                        }
                        else if (x.Contains("C"))
                        {
                            this.Inventory_Container.ContentPanel.Controls.Add(button);
                        }
                        else if (x.Contains("D"))
                        {
                            this.Supervisor_Container.ContentPanel.Controls.Add(button);
                        }
                        else if (x.Contains("E"))
                        {
                            this.CashierServices_Container.ContentPanel.Controls.Add(button);
                        }
                        else if (x.Contains("F"))
                        {
                            this.CustomerSupport_Container.ContentPanel.Controls.Add(button);
                        }
                    }
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            foreach (LaxExpander expander in this.MainMenuPanel.Controls)
            {
                if (expander.ContentPanel.Controls.Count == 0)
                {
                    expander.Visible = false;
                }
            }
        }

        public void GetCategories()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from categories";
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Database failed to retrieve data", "Initializing Datalist", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    do
                    {
                    }
                    while (reader.Read());
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        

        public void LoadAutocompleteProducts()
        {
            try
            {
                Program.MasterProductList.Clear();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT a.ProductCode,a.Description FROM inventorymaster a;";
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Products Have Been Found !!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        Program.MasterProductList.Add(reader["ProductCode"].ToString());
                        Program.MasterProductList.Add(reader["Description"].ToString());
                    }
                }
                command.Dispose();
                reader.Dispose();
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
            }
        }

        private void LogoutProcess(bool CloseDashboard)
        {
            try
            {
                Program.Lastuser = Program.CurrLoggedInUser.UserID;
                Program.CurrLoggedInUser = null;
                ReLogin login = new ReLogin();
                this.AutologoutTimer.Stop();
                if (CloseDashboard)
                {
                    Program.Lastuser = "";
                    Program.Shuttingdown = false;
                    this.Close();
                }
                else
                {
                    login.Show();
                    this.Hide();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            
            try
            {
                Form rform = null;
                string name = (sender as Button).Name;
                string str2 = (sender as Button).Parent.Parent.Name;//container panel
                if (str2 != "Administration_Container")
                {
                    if (str2 != "Accounting_Container")
                    {
                        
                        if (str2 != "Inventory_Container")
                        {
                            if (str2 != "Supervisor_Container")
                            {
                                if (str2 == "CashierServices_Container")
                                {
                                    string str11 = name;
                                    rform = (str11 == "E100") ? ((Form)new Sales()) : ((str11 == "E101") ? ((Form)new NewExpense()) : ((str11 == "E102") ? ((Form)new InvoicePay()) : ((str11 == "E103") ? ((Form)new ReprintReceipt()) : ((str11 == "E104") ? ((Form)new DailyReport()) : null))));
                                }
                                else if (str2 == "CustomerSupport_Container")
                                {
                                    string str13 = name;
                                    rform = (str13 == "F100") ? ((Form)new ManageCustomers()) :
                                        ((str13 == "F101") ? ((Form)new ManageLoyalty()) : 
                                        ((str13 == "F102") ? ((Form)new SalesRecords()) :
                                        ((str13 == "F103") ? ((Form)new SalesRecords()) :
                                        null)));
                                }
                            }
                            else
                            {                             
                                string str4 = name;
                                rform =
                                    (str4 == "A103") ? ((Form)new ManageUserRights()) : 
                                    ((str4 == "B100") ? ((Form)new WPAccounts()) :
                                    ((str4 == "B101") ? ((Form)new SalesRecords()) :
                                    ((str4 == "B102") ? ((Form)new WorkPeriods()) : 
                                    ((str4 == "B103") ? ((Form)new Payables()) :
                                    ((str4 == "C107") ? ((Form)new InventoryReports()) :
                                    ((str4 == "C108") ? ((Form)new InventorySettings()) :
                                    ((str4 == "E100") ? ((Form)new Sales()) :
                                    ((str4 == "E101") ? ((Form)new NewExpense()) :
                                    ((str4 == "E102") ? ((Form)new ReprintReceipt()) :
                                    ((str4 == "E103") ? ((Form)new InvoicePay()) :
                                    ((str4 == "E104") ? ((Form)new DailyReport()) : 
                                    null)))))))))));
                            }
                        }
                        else
                        {
                            string str4 = name;
                            rform = (str4 == "C100") ? ((Form)new MasterEntries()) : ((str4 == "C101") ? ((Form)new StockIn()) : ((str4 == "C102") ? ((Form)new Products()) : ((str4 == "C103") ? ((Form)new Lpo()) : ((str4 == "C104") ? ((Form)new StockManagement()) : 
                                ((str4 == "C106") ? ((Form)new Suppliers()) : ((str4 == "C107") ? ((Form)new InventoryReports()): ((str4 == "C108") ? ((Form)new PriceTags()) : null)))))));
                        }
                        
                    }
                    else
                    {
                        string str5 = name;
                        rform = (str5 == "B100") ? ((Form)new WPAccounts()) : ((str5 == "B101") ? ((Form)new Payables()) : ((str5 == "B102") ? ((Form)new SalesRecords()) : ((str5 == "B103") ? ((Form)new WorkPeriods()) : null)));
                    }
                }
                else
                {
                    string str3 = name;
                    rform = (str3 == "A100") ? ((Form)new ManageUsers()) : ((str3 == "A101") ? ((Form)new ManageUserRights()) : ((str3 == "A102") ? ((Form)new ViewUsers()) : ((str3 == "A103") ? ((Form)new EditUsers()) : null)));
                }
         //   TR_0004:
                if (rform != null)
                { 
                    this.ShowRightForm(rform);
                    lastopenform = rform;
                }
                else
                {
                    MessageBox.Show(this, "This feature does not Exist!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x201)
            {
                Program.LastActionTime = DateTime.Now;
            }
            return false;
        }

        private void ShowRightForm(Form Rform)
        {
            try
            {
                if ((from n in base.MdiChildren where n.GetType() == Rform.GetType() select n).Count<Form>() > 0)
                {
                    (from n in base.MdiChildren
                        where n.GetType() == Rform.GetType()
                        select n).First<Form>().Activate();
                }
                else
                { 
                    Rform.FormBorderStyle = FormBorderStyle.None;
                    Rform.MdiParent = this;
                    Rform.Dock = DockStyle.Fill;
                    Rform.Show();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Program.CurrLoggedInUser != null)
            {
                this.label5.Text = "Date: " + Program.CurrentDateTime().ToShortDateString();
                this.label6.Text = "Time: " + Program.CurrentDateTime().ToLongTimeString();
            }
        }

        private void ToggleExpander(SplitContainer sc, PictureBox pb)
        {
            try
            {
                if (!sc.Panel2Collapsed)
                {
                    pb.Image = Resources.expand;
                    sc.Height = 40;
                    sc.Panel2Collapsed = true;
                }
                else
                {
                    pb.Image = Resources.collapse;
                    sc.Panel2Collapsed = false;
                    int count = sc.Panel2.Controls.Count;
                    int num2 = ((count * 40) + 0x2a) - count;
                    sc.Height = num2;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
            }
        }

       
    }
}

