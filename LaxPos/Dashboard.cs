namespace LaxPos
{
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

    public class Dashboard : Form, IMessageFilter
    {
        //private const int WM_LBUTTONDOWN = 0x201;
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public readonly Login L;
        bool Shuttingdown = true;
        private IContainer components = null;
        public NotifyIcon NotifyIcon1;
        private ToolTip toolTip1;
        private Timer timer1;
        private Panel Navigation_Panel;
        private Panel panel1;
        private Panel panel2;
        private Button Btn_Logout;
        private Panel panel3;
        private Label label6;
        private Label label5;
        private Label label3;
        private Label label2;
        private Label label1;
        public Timer AutologoutTimer;
        private Button Btn_MinimizeMenuPanel;
        private Button Btn_ExpandMenuPanel;
        private Panel MainMenuPanel;
        private LaxExpander CustomerSupport_Container;
        private LaxExpander CashierServices_Container;
        private LaxExpander Supervisor_Container;
        private LaxExpander Inventory_Container;
        private LaxExpander Accounting_Container;
        private LaxExpander Administration_Container;

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

        private void ActionOccured()
        {
            Program.LastActionTime = DateTime.Now;
        }

        private void AutologoutTimer_Tick(object sender, EventArgs e)
        {
            this.CheckLoginStatus();
        }

        public bool Backup()
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

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Shuttingdown)
            {
                DialogResult result = MessageBox.Show(this, "Do you want to Exit?", "Message Box", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
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
            this.ActionOccured();
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
            this.Shuttingdown = true;
            this.LoadAutocompleteProducts();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Navigation_Panel = new System.Windows.Forms.Panel();
            this.MainMenuPanel = new System.Windows.Forms.Panel();
            this.CustomerSupport_Container = new LaxExpanderPanel.LaxExpander();
            this.CashierServices_Container = new LaxExpanderPanel.LaxExpander();
            this.Supervisor_Container = new LaxExpanderPanel.LaxExpander();
            this.Inventory_Container = new LaxExpanderPanel.LaxExpander();
            this.Accounting_Container = new LaxExpanderPanel.LaxExpander();
            this.Administration_Container = new LaxExpanderPanel.LaxExpander();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_Logout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_MinimizeMenuPanel = new System.Windows.Forms.Button();
            this.Btn_ExpandMenuPanel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AutologoutTimer = new System.Windows.Forms.Timer(this.components);
            this.Navigation_Panel.SuspendLayout();
            this.MainMenuPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon1.BalloonTipText = "BaloonTxt:PoS";
            this.NotifyIcon1.BalloonTipTitle = "Title:LaxPosTitle";
            this.NotifyIcon1.Text = "Text:LaxPos";
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.Maroon;
            this.toolTip1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Menu";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Navigation_Panel
            // 
            this.Navigation_Panel.BackColor = System.Drawing.Color.LightGray;
            this.Navigation_Panel.Controls.Add(this.MainMenuPanel);
            this.Navigation_Panel.Controls.Add(this.panel2);
            this.Navigation_Panel.Controls.Add(this.panel1);
            this.Navigation_Panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Navigation_Panel.Location = new System.Drawing.Point(0, 0);
            this.Navigation_Panel.Name = "Navigation_Panel";
            this.Navigation_Panel.Size = new System.Drawing.Size(170, 749);
            this.Navigation_Panel.TabIndex = 0;
            // 
            // MainMenuPanel
            // 
            this.MainMenuPanel.BackColor = System.Drawing.Color.LightGray;
            this.MainMenuPanel.Controls.Add(this.CustomerSupport_Container);
            this.MainMenuPanel.Controls.Add(this.CashierServices_Container);
            this.MainMenuPanel.Controls.Add(this.Supervisor_Container);
            this.MainMenuPanel.Controls.Add(this.Inventory_Container);
            this.MainMenuPanel.Controls.Add(this.Accounting_Container);
            this.MainMenuPanel.Controls.Add(this.Administration_Container);
            this.MainMenuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMenuPanel.Location = new System.Drawing.Point(0, 31);
            this.MainMenuPanel.Name = "MainMenuPanel";
            this.MainMenuPanel.Size = new System.Drawing.Size(170, 666);
            this.MainMenuPanel.TabIndex = 7;
            // 
            // CustomerSupport_Container
            // 
            this.CustomerSupport_Container.Dock = System.Windows.Forms.DockStyle.Top;
            this.CustomerSupport_Container.HeadingText = null;
            this.CustomerSupport_Container.Location = new System.Drawing.Point(0, 150);
            this.CustomerSupport_Container.Name = "CustomerSupport_Container";
            this.CustomerSupport_Container.Padding = new System.Windows.Forms.Padding(1);
            this.CustomerSupport_Container.Size = new System.Drawing.Size(170, 30);
            this.CustomerSupport_Container.TabIndex = 11;
            // 
            // CashierServices_Container
            // 
            this.CashierServices_Container.Dock = System.Windows.Forms.DockStyle.Top;
            this.CashierServices_Container.HeadingText = null;
            this.CashierServices_Container.Location = new System.Drawing.Point(0, 120);
            this.CashierServices_Container.Name = "CashierServices_Container";
            this.CashierServices_Container.Padding = new System.Windows.Forms.Padding(1);
            this.CashierServices_Container.Size = new System.Drawing.Size(170, 30);
            this.CashierServices_Container.TabIndex = 7;
            // 
            // Supervisor_Container
            // 
            this.Supervisor_Container.Dock = System.Windows.Forms.DockStyle.Top;
            this.Supervisor_Container.HeadingText = null;
            this.Supervisor_Container.Location = new System.Drawing.Point(0, 90);
            this.Supervisor_Container.Name = "Supervisor_Container";
            this.Supervisor_Container.Padding = new System.Windows.Forms.Padding(1);
            this.Supervisor_Container.Size = new System.Drawing.Size(170, 30);
            this.Supervisor_Container.TabIndex = 10;
            // 
            // Inventory_Container
            // 
            this.Inventory_Container.Dock = System.Windows.Forms.DockStyle.Top;
            this.Inventory_Container.HeadingText = null;
            this.Inventory_Container.Location = new System.Drawing.Point(0, 60);
            this.Inventory_Container.Name = "Inventory_Container";
            this.Inventory_Container.Padding = new System.Windows.Forms.Padding(1);
            this.Inventory_Container.Size = new System.Drawing.Size(170, 30);
            this.Inventory_Container.TabIndex = 9;
            // 
            // Accounting_Container
            // 
            this.Accounting_Container.Dock = System.Windows.Forms.DockStyle.Top;
            this.Accounting_Container.HeadingText = null;
            this.Accounting_Container.Location = new System.Drawing.Point(0, 30);
            this.Accounting_Container.Name = "Accounting_Container";
            this.Accounting_Container.Padding = new System.Windows.Forms.Padding(1);
            this.Accounting_Container.Size = new System.Drawing.Size(170, 30);
            this.Accounting_Container.TabIndex = 8;
            // 
            // Administration_Container
            // 
            this.Administration_Container.Dock = System.Windows.Forms.DockStyle.Top;
            this.Administration_Container.HeadingText = "test";
            this.Administration_Container.Location = new System.Drawing.Point(0, 0);
            this.Administration_Container.Name = "Administration_Container";
            this.Administration_Container.Padding = new System.Windows.Forms.Padding(1);
            this.Administration_Container.Size = new System.Drawing.Size(170, 30);
            this.Administration_Container.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_Logout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 697);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(170, 52);
            this.panel2.TabIndex = 6;
            // 
            // Btn_Logout
            // 
            this.Btn_Logout.BackColor = System.Drawing.Color.Maroon;
            this.Btn_Logout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Logout.FlatAppearance.BorderSize = 0;
            this.Btn_Logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Logout.ForeColor = System.Drawing.Color.White;
            this.Btn_Logout.Location = new System.Drawing.Point(35, 6);
            this.Btn_Logout.Name = "Btn_Logout";
            this.Btn_Logout.Size = new System.Drawing.Size(100, 35);
            this.Btn_Logout.TabIndex = 0;
            this.Btn_Logout.Text = "Logout";
            this.Btn_Logout.UseVisualStyleBackColor = false;
            this.Btn_Logout.Click += new System.EventHandler(this.Btn_Logout_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.Btn_MinimizeMenuPanel);
            this.panel1.Controls.Add(this.Btn_ExpandMenuPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 31);
            this.panel1.TabIndex = 0;
            // 
            // Btn_MinimizeMenuPanel
            // 
            this.Btn_MinimizeMenuPanel.BackgroundImage = global::LaxPos.Properties.Resources.menu;
            this.Btn_MinimizeMenuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_MinimizeMenuPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_MinimizeMenuPanel.Location = new System.Drawing.Point(138, 0);
            this.Btn_MinimizeMenuPanel.Name = "Btn_MinimizeMenuPanel";
            this.Btn_MinimizeMenuPanel.Size = new System.Drawing.Size(32, 31);
            this.Btn_MinimizeMenuPanel.TabIndex = 2;
            this.Btn_MinimizeMenuPanel.UseVisualStyleBackColor = true;
            this.Btn_MinimizeMenuPanel.Click += new System.EventHandler(this.Btn_MinimizeMenuPanel_Click);
            // 
            // Btn_ExpandMenuPanel
            // 
            this.Btn_ExpandMenuPanel.BackgroundImage = global::LaxPos.Properties.Resources.menu;
            this.Btn_ExpandMenuPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_ExpandMenuPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_ExpandMenuPanel.Location = new System.Drawing.Point(0, 0);
            this.Btn_ExpandMenuPanel.Name = "Btn_ExpandMenuPanel";
            this.Btn_ExpandMenuPanel.Size = new System.Drawing.Size(32, 31);
            this.Btn_ExpandMenuPanel.TabIndex = 1;
            this.Btn_ExpandMenuPanel.UseVisualStyleBackColor = true;
            this.Btn_ExpandMenuPanel.Visible = false;
            this.Btn_ExpandMenuPanel.Click += new System.EventHandler(this.Btn_ExpandMenuPanel_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(170, 722);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1200, 27);
            this.panel3.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(727, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Time";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(569, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(354, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Counter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Department";
            // 
            // AutologoutTimer
            // 
            this.AutologoutTimer.Interval = 60000;
            this.AutologoutTimer.Tick += new System.EventHandler(this.AutologoutTimer_Tick);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Navigation_Panel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1364, 718);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laxco Retail Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Dashboard_FormClosed);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.Shown += new System.EventHandler(this.Dashboard_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dashboard_KeyDown);
            this.Resize += new System.EventHandler(this.Dashboard_Resize);
            this.Navigation_Panel.ResumeLayout(false);
            this.MainMenuPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

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
                    Shuttingdown = false;
                    base.Close();
                }
                else
                {
                    login.Show();
                    base.Hide();
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
                                    rform = (str13 == "F100") ? ((Form)new ManageCustomers()) : ((str13 == "F101") ? ((Form)new ManageLoyalty()) : ((str13 == "F102") ? ((Form)new SalesRecords()) : null));
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
                                ((str4 == "C106") ? ((Form)new Suppliers()) : ((str4 == "C107") ? ((Form)new InventoryReports()): ((str4 == "C108") ? ((Form)new InventorySettings()) : null)))))));
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
                if ((from n in base.MdiChildren
                    where n.GetType() == Rform.GetType()
                    select n).Count<Form>() > 0)
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

