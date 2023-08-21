using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaxPos
{
    public partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
         public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
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
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1364, 718);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laxco Retail Software";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
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
    
        #endregion
        public System.Windows.Forms.NotifyIcon NotifyIcon1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Timer timer1;
        private System.Windows.Forms.Panel Navigation_Panel;
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
        private LaxExpanderPanel.LaxExpander CustomerSupport_Container;
        private LaxExpanderPanel.LaxExpander CashierServices_Container;
        private LaxExpanderPanel.LaxExpander Supervisor_Container;
        private LaxExpanderPanel.LaxExpander Inventory_Container;
        private LaxExpanderPanel.LaxExpander Accounting_Container;
        private LaxExpanderPanel.LaxExpander Administration_Container;
    }
}
