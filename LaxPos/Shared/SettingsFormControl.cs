namespace LaxPos.Shared
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class SettingsFormControl : Form
    {
        private readonly CompanyProfile Client = new CompanyProfile();
        public string Title = "";
        public string Box = "";
        public string Email = "";
        public string Tel = "";
        public string Website = "";
        public string Pin = "";
        public string Footer1 = "";
        public string Footer2 = "";
        public string CounterName = Environment.MachineName;
        public int Center_X = 150;
        public double GrossTotal = 950.0;
        public double TaxPercentage = 14.0;
        public double TaxAmt = 0.0;
        public double AmountPaid = 1000.0;
        public double Change = 50.0;
        private IContainer components = null;
        private Panel TitlePanel;
        private Label Title_SalesControl;
        private TabPage Tabpage_Dashboard;
        private TabPage TabPage_Database;
        private TabPage TabPage_BusinessProfile;
        private TabControl Tab_SettingsHost;
        private TabPage TabPage_Receipt;
        public PrintDocument printDocument1;
        private OpenFileDialog openFileDialog1;
        private GroupBox groupBox1;
        private Button Btn_PreviewFromFile;
        private Button Btn_Loadfile;
        private TextBox textBox17;
        private Label label11;
        private Button Btn_SaveSettings;
        private Button Btn_CurrentSettings;
        private PrintPreviewControl Previewcontrol1;
        private Label label8;
        private TextBox textBox10;
        private Label label10;
        private TextBox textBox16;
        private Button Btn_ClearAll;
        private Label label3;
        private TextBox textBox4;
        private Button Btn_ViewAll;
        private Label label9;
        private Label label6;
        private Label label7;
        private Label label5;
        private Label label4;
        private TextBox textBox15;
        private TextBox textBox12;
        private TextBox textBox9;
        private TextBox textBox6;
        private Label label1;
        private TextBox textBox1;
        private Button Btn_OpenFile;
        private Button Btn_PrintPreview;

        public SettingsFormControl()
        {
            this.InitializeComponent();
        }

        public void AssignTextboxesValue()
        {
            this.textBox1.Text = this.Title;
            this.textBox6.Text = this.Box;
            this.textBox9.Text = this.Tel;
            this.textBox12.Text = this.Email;
            this.textBox15.Text = this.Pin;
            this.textBox4.Text = this.Footer1;
            this.textBox16.Text = this.Footer2;
            this.textBox10.Text = this.Website;
        }

        public void AssignValueToFields()
        {
            this.Title = this.textBox1.Text;
            this.Box = this.textBox6.Text;
            this.Email = this.textBox12.Text;
            this.Tel = this.textBox9.Text;
            this.Website = this.textBox10.Text;
            this.Pin = this.textBox15.Text;
            this.Footer1 = this.textBox4.Text;
            this.Footer2 = this.textBox16.Text;
        }

        private void Btn_CheckTitle_Click(object sender, EventArgs e)
        {
            this.PreviewReceipt();
        }

        private void Btn_ClearAll_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            this.textBox6.Text = "";
            this.textBox9.Text = "";
            this.textBox12.Text = "";
            this.textBox15.Text = "";
            this.textBox4.Text = "";
            this.textBox16.Text = "";
            this.textBox10.Text = "";
        }

        private void Btn_CurrentSettings_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadCurrentSettings();
                if (MessageBox.Show("Do you want to preview The previous Receipt ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    this.Btn_Preview_Click(sender, e);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void Btn_Preview_Click(object sender, EventArgs e)
        {
            this.AssignValueToFields();
            this.PreviewReceipt();
        }

        private void Btn_PreviewFromFile_Click(object sender, EventArgs e)
        {
            this.PreviewReceipt();
        }

        private void Btn_PrintPreview_Click(object sender, EventArgs e)
        {
            this.printDocument1.PrintController = new StandardPrintController();
            this.printDocument1.Print();
        }

        private void Btn_SaveSettings_Click(object sender, EventArgs e)
        {
            this.Client.ClientTitle = this.textBox1.Text;
            this.Client.ClientAddress = this.textBox6.Text;
            this.Client.ClientTel = this.textBox9.Text;
            this.Client.ClientEmail = this.textBox12.Text;
            this.Client.ClientPin = this.textBox15.Text;
            this.Client.Save();
            this.Client.Upgrade();
            this.Client.Reload();
            MessageBox.Show("Receipt Settings Saved !!", "", MessageBoxButtons.OK);
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
            this.TitlePanel = new Panel();
            this.Title_SalesControl = new Label();
            this.Tabpage_Dashboard = new TabPage();
            this.TabPage_Database = new TabPage();
            this.TabPage_BusinessProfile = new TabPage();
            this.printDocument1 = new PrintDocument();
            this.Tab_SettingsHost = new TabControl();
            this.TabPage_Receipt = new TabPage();
            this.Btn_PrintPreview = new Button();
            this.groupBox1 = new GroupBox();
            this.Btn_OpenFile = new Button();
            this.Btn_PreviewFromFile = new Button();
            this.Btn_Loadfile = new Button();
            this.textBox17 = new TextBox();
            this.label11 = new Label();
            this.Btn_SaveSettings = new Button();
            this.Btn_CurrentSettings = new Button();
            this.Previewcontrol1 = new PrintPreviewControl();
            this.label8 = new Label();
            this.textBox10 = new TextBox();
            this.label10 = new Label();
            this.textBox16 = new TextBox();
            this.Btn_ClearAll = new Button();
            this.label3 = new Label();
            this.textBox4 = new TextBox();
            this.Btn_ViewAll = new Button();
            this.label9 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.textBox15 = new TextBox();
            this.textBox12 = new TextBox();
            this.textBox9 = new TextBox();
            this.textBox6 = new TextBox();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.openFileDialog1 = new OpenFileDialog();
            this.TitlePanel.SuspendLayout();
            this.Tab_SettingsHost.SuspendLayout();
            this.TabPage_Receipt.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.TitlePanel.Controls.Add(this.Title_SalesControl);
            this.TitlePanel.Dock = DockStyle.Top;
            this.TitlePanel.Location = new Point(0, 0);
            this.TitlePanel.Margin = new Padding(6, 9, 6, 9);
            this.TitlePanel.Name = "TitlePanel";
            this.TitlePanel.Size = new Size(0x4a0, 0x1f);
            this.TitlePanel.TabIndex = 5;
            this.Title_SalesControl.BackColor = Color.NavajoWhite;
            this.Title_SalesControl.Dock = DockStyle.Fill;
            this.Title_SalesControl.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Title_SalesControl.Location = new Point(0, 0);
            this.Title_SalesControl.Margin = new Padding(4, 0, 4, 0);
            this.Title_SalesControl.Name = "Title_SalesControl";
            this.Title_SalesControl.Size = new Size(0x4a0, 0x1f);
            this.Title_SalesControl.TabIndex = 0;
            this.Title_SalesControl.Text = "Settings Form";
            this.Title_SalesControl.TextAlign = ContentAlignment.MiddleCenter;
            this.Tabpage_Dashboard.Location = new Point(4, 0x1d);
            this.Tabpage_Dashboard.Name = "Tabpage_Dashboard";
            this.Tabpage_Dashboard.Padding = new Padding(3);
            this.Tabpage_Dashboard.Size = new Size(0x4a8, 0x218);
            this.Tabpage_Dashboard.TabIndex = 8;
            this.Tabpage_Dashboard.Text = "Dashboard";
            this.Tabpage_Dashboard.UseVisualStyleBackColor = true;
            this.TabPage_Database.Location = new Point(4, 0x1d);
            this.TabPage_Database.Name = "TabPage_Database";
            this.TabPage_Database.Padding = new Padding(3);
            this.TabPage_Database.Size = new Size(0x4a8, 0x218);
            this.TabPage_Database.TabIndex = 5;
            this.TabPage_Database.Text = "Database Settings";
            this.TabPage_Database.UseVisualStyleBackColor = true;
            this.TabPage_BusinessProfile.Location = new Point(4, 0x1d);
            this.TabPage_BusinessProfile.Name = "TabPage_BusinessProfile";
            this.TabPage_BusinessProfile.Padding = new Padding(3);
            this.TabPage_BusinessProfile.Size = new Size(0x498, 0x1f1);
            this.TabPage_BusinessProfile.TabIndex = 7;
            this.TabPage_BusinessProfile.Text = "Business Profile";
            this.TabPage_BusinessProfile.UseVisualStyleBackColor = true;
            this.TabPage_BusinessProfile.Click += new EventHandler(this.TabPage_BusinessProfile_Click);
            this.Tab_SettingsHost.Controls.Add(this.TabPage_BusinessProfile);
            this.Tab_SettingsHost.Controls.Add(this.TabPage_Database);
            this.Tab_SettingsHost.Controls.Add(this.Tabpage_Dashboard);
            this.Tab_SettingsHost.Controls.Add(this.TabPage_Receipt);
            this.Tab_SettingsHost.Dock = DockStyle.Fill;
            this.Tab_SettingsHost.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Tab_SettingsHost.HotTrack = true;
            this.Tab_SettingsHost.ItemSize = new Size(100, 0x19);
            this.Tab_SettingsHost.Location = new Point(0, 0x1f);
            this.Tab_SettingsHost.Multiline = true;
            this.Tab_SettingsHost.Name = "Tab_SettingsHost";
            this.Tab_SettingsHost.Padding = new Point(0x19, 3);
            this.Tab_SettingsHost.SelectedIndex = 0;
            this.Tab_SettingsHost.Size = new Size(0x4a0, 530);
            this.Tab_SettingsHost.TabIndex = 7;
            this.TabPage_Receipt.Controls.Add(this.Btn_PrintPreview);
            this.TabPage_Receipt.Controls.Add(this.groupBox1);
            this.TabPage_Receipt.Controls.Add(this.Btn_SaveSettings);
            this.TabPage_Receipt.Controls.Add(this.Btn_CurrentSettings);
            this.TabPage_Receipt.Controls.Add(this.Previewcontrol1);
            this.TabPage_Receipt.Controls.Add(this.label8);
            this.TabPage_Receipt.Controls.Add(this.textBox10);
            this.TabPage_Receipt.Controls.Add(this.label10);
            this.TabPage_Receipt.Controls.Add(this.textBox16);
            this.TabPage_Receipt.Controls.Add(this.Btn_ClearAll);
            this.TabPage_Receipt.Controls.Add(this.label3);
            this.TabPage_Receipt.Controls.Add(this.textBox4);
            this.TabPage_Receipt.Controls.Add(this.Btn_ViewAll);
            this.TabPage_Receipt.Controls.Add(this.label9);
            this.TabPage_Receipt.Controls.Add(this.label6);
            this.TabPage_Receipt.Controls.Add(this.label7);
            this.TabPage_Receipt.Controls.Add(this.label5);
            this.TabPage_Receipt.Controls.Add(this.label4);
            this.TabPage_Receipt.Controls.Add(this.textBox15);
            this.TabPage_Receipt.Controls.Add(this.textBox12);
            this.TabPage_Receipt.Controls.Add(this.textBox9);
            this.TabPage_Receipt.Controls.Add(this.textBox6);
            this.TabPage_Receipt.Controls.Add(this.label1);
            this.TabPage_Receipt.Controls.Add(this.textBox1);
            this.TabPage_Receipt.Location = new Point(4, 0x1d);
            this.TabPage_Receipt.Name = "TabPage_Receipt";
            this.TabPage_Receipt.Padding = new Padding(3);
            this.TabPage_Receipt.Size = new Size(0x4a8, 0x218);
            this.TabPage_Receipt.TabIndex = 9;
            this.TabPage_Receipt.Text = "ReceiptDesigner";
            this.TabPage_Receipt.UseVisualStyleBackColor = true;
            this.TabPage_Receipt.Click += new EventHandler(this.TabPage_Receipt_Click);
            this.Btn_PrintPreview.Location = new Point(0x1dd, 0xef);
            this.Btn_PrintPreview.Margin = new Padding(3, 4, 3, 4);
            this.Btn_PrintPreview.Name = "Btn_PrintPreview";
            this.Btn_PrintPreview.Size = new Size(0xa6, 0x1c);
            this.Btn_PrintPreview.TabIndex = 0x85;
            this.Btn_PrintPreview.Text = "Print Preview";
            this.Btn_PrintPreview.UseVisualStyleBackColor = true;
            this.Btn_PrintPreview.Click += new EventHandler(this.Btn_PrintPreview_Click);
            this.groupBox1.Controls.Add(this.Btn_OpenFile);
            this.groupBox1.Controls.Add(this.Btn_PreviewFromFile);
            this.groupBox1.Controls.Add(this.Btn_Loadfile);
            this.groupBox1.Controls.Add(this.textBox17);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new Point(6, 0x15f);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x307, 0x79);
            this.groupBox1.TabIndex = 0x80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load From File";
            this.Btn_OpenFile.Location = new Point(0x297, 0x1a);
            this.Btn_OpenFile.Margin = new Padding(3, 4, 3, 4);
            this.Btn_OpenFile.Name = "Btn_OpenFile";
            this.Btn_OpenFile.Size = new Size(0x6a, 0x1c);
            this.Btn_OpenFile.TabIndex = 0x5b;
            this.Btn_OpenFile.Text = "Browse File";
            this.Btn_OpenFile.UseVisualStyleBackColor = true;
            this.Btn_PreviewFromFile.Location = new Point(0x199, 0x4a);
            this.Btn_PreviewFromFile.Margin = new Padding(3, 4, 3, 4);
            this.Btn_PreviewFromFile.Name = "Btn_PreviewFromFile";
            this.Btn_PreviewFromFile.Size = new Size(0xb2, 0x1c);
            this.Btn_PreviewFromFile.TabIndex = 90;
            this.Btn_PreviewFromFile.Text = "Preview Receipt";
            this.Btn_PreviewFromFile.UseVisualStyleBackColor = true;
            this.Btn_PreviewFromFile.Click += new EventHandler(this.Btn_PreviewFromFile_Click);
            this.Btn_Loadfile.Location = new Point(0xa6, 0x4a);
            this.Btn_Loadfile.Margin = new Padding(3, 4, 3, 4);
            this.Btn_Loadfile.Name = "Btn_Loadfile";
            this.Btn_Loadfile.Size = new Size(0xcd, 0x1c);
            this.Btn_Loadfile.TabIndex = 0x59;
            this.Btn_Loadfile.Text = "Load To Designer Box";
            this.Btn_Loadfile.UseVisualStyleBackColor = true;
            this.textBox17.Location = new Point(0x60, 0x1a);
            this.textBox17.Margin = new Padding(3, 4, 3, 4);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Size(0x231, 0x1a);
            this.textBox17.TabIndex = 0x58;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(6, 0x19);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x54, 20);
            this.label11.TabIndex = 0x57;
            this.label11.Text = "File lcation";
            this.Btn_SaveSettings.Location = new Point(0x36e, 0x16c);
            this.Btn_SaveSettings.Margin = new Padding(3, 4, 3, 4);
            this.Btn_SaveSettings.Name = "Btn_SaveSettings";
            this.Btn_SaveSettings.Size = new Size(0xa6, 0x1c);
            this.Btn_SaveSettings.TabIndex = 0x7f;
            this.Btn_SaveSettings.Text = "Save settings";
            this.Btn_SaveSettings.UseVisualStyleBackColor = true;
            this.Btn_SaveSettings.Click += new EventHandler(this.Btn_SaveSettings_Click);
            this.Btn_CurrentSettings.Location = new Point(0x1dd, 0xad);
            this.Btn_CurrentSettings.Margin = new Padding(3, 4, 3, 4);
            this.Btn_CurrentSettings.Name = "Btn_CurrentSettings";
            this.Btn_CurrentSettings.Size = new Size(0xa6, 0x1c);
            this.Btn_CurrentSettings.TabIndex = 0x7e;
            this.Btn_CurrentSettings.Text = "Current Settings";
            this.Btn_CurrentSettings.UseVisualStyleBackColor = true;
            this.Btn_CurrentSettings.Click += new EventHandler(this.Btn_CurrentSettings_Click);
            this.Previewcontrol1.AutoZoom = false;
            this.Previewcontrol1.Location = new Point(0x30e, 7);
            this.Previewcontrol1.Margin = new Padding(3, 4, 3, 4);
            this.Previewcontrol1.Name = "Previewcontrol1";
            this.Previewcontrol1.Size = new Size(0x147, 0x14f);
            this.Previewcontrol1.TabIndex = 0x7d;
            this.Previewcontrol1.Zoom = 1.0;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x15, 0x10c);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x41, 20);
            this.label8.TabIndex = 0x7b;
            this.label8.Text = "Footer2";
            this.textBox10.Location = new Point(0x84, 0x130);
            this.textBox10.Margin = new Padding(3, 4, 3, 4);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Size(280, 0x1a);
            this.textBox10.TabIndex = 0x79;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x15, 0xe5);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x41, 20);
            this.label10.TabIndex = 0x77;
            this.label10.Text = "Footer1";
            this.textBox16.Location = new Point(0x83, 0x10c);
            this.textBox16.Margin = new Padding(3, 4, 3, 4);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Size(280, 0x1a);
            this.textBox16.TabIndex = 0x75;
            this.Btn_ClearAll.Location = new Point(0x1dd, 0x35);
            this.Btn_ClearAll.Margin = new Padding(3, 4, 3, 4);
            this.Btn_ClearAll.Name = "Btn_ClearAll";
            this.Btn_ClearAll.Size = new Size(0xa6, 0x1c);
            this.Btn_ClearAll.TabIndex = 0x73;
            this.Btn_ClearAll.Text = "Clear All";
            this.Btn_ClearAll.UseVisualStyleBackColor = true;
            this.Btn_ClearAll.Click += new EventHandler(this.Btn_ClearAll_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x15, 190);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x44, 20);
            this.label3.TabIndex = 0x71;
            this.label3.Text = "KRA Pin";
            this.textBox4.Location = new Point(0x83, 0xe5);
            this.textBox4.Margin = new Padding(3, 4, 3, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(280, 0x1a);
            this.textBox4.TabIndex = 0x6f;
            this.Btn_ViewAll.Location = new Point(0x1dd, 0x70);
            this.Btn_ViewAll.Margin = new Padding(3, 4, 3, 4);
            this.Btn_ViewAll.Name = "Btn_ViewAll";
            this.Btn_ViewAll.Size = new Size(0xa6, 0x1c);
            this.Btn_ViewAll.TabIndex = 110;
            this.Btn_ViewAll.Text = "Preview Receipt";
            this.Btn_ViewAll.UseVisualStyleBackColor = true;
            this.Btn_ViewAll.Click += new EventHandler(this.Btn_Preview_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x15, 0x130);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x43, 20);
            this.label9.TabIndex = 0x68;
            this.label9.Text = "Website";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x15, 0x70);
            this.label6.Name = "label6";
            this.label6.Size = new Size(90, 20);
            this.label6.TabIndex = 0x67;
            this.label6.Text = "Tel Number";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x15, 0x92);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x30, 20);
            this.label7.TabIndex = 0x66;
            this.label7.Text = "Email";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x15, 0x49);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x44, 20);
            this.label5.TabIndex = 0x65;
            this.label5.Text = "Address";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x15, 0x22);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x61, 20);
            this.label4.TabIndex = 100;
            this.label4.Text = "Receipt Title";
            this.textBox15.Location = new Point(130, 190);
            this.textBox15.Margin = new Padding(3, 4, 3, 4);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Size(280, 0x1a);
            this.textBox15.TabIndex = 0x62;
            this.textBox12.Location = new Point(130, 0x97);
            this.textBox12.Margin = new Padding(3, 4, 3, 4);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Size(280, 0x1a);
            this.textBox12.TabIndex = 0x60;
            this.textBox9.Location = new Point(130, 0x70);
            this.textBox9.Margin = new Padding(3, 4, 3, 4);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Size(280, 0x1a);
            this.textBox9.TabIndex = 0x5e;
            this.textBox6.Location = new Point(130, 0x49);
            this.textBox6.Margin = new Padding(3, 4, 3, 4);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Size(280, 0x1a);
            this.textBox6.TabIndex = 0x5c;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0xbc, 7);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x70, 20);
            this.label1.TabIndex = 0x59;
            this.label1.Text = "Text Content";
            this.textBox1.Location = new Point(130, 0x22);
            this.textBox1.Margin = new Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(280, 0x1a);
            this.textBox1.TabIndex = 0x58;
            this.openFileDialog1.FileName = "openFileDialog1";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x4a0, 0x231);
            base.Controls.Add(this.Tab_SettingsHost);
            base.Controls.Add(this.TitlePanel);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SettingsFormControl";
            this.TitlePanel.ResumeLayout(false);
            this.Tab_SettingsHost.ResumeLayout(false);
            this.TabPage_Receipt.ResumeLayout(false);
            this.TabPage_Receipt.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        public void LoadCurrentSettings()
        {
            this.Title = this.Client.ClientTitle;
            this.Box = this.Client.ClientAddress;
            this.Email = this.Client.ClientEmail;
            this.Tel = this.Client.ClientTel;
            this.Pin = this.Client.ClientPin;
            this.AssignTextboxesValue();
        }

        public void PreviewReceipt()
        {
            this.printDocument1.PrintPage += new PrintPageEventHandler(this.ProvideContent);
            this.Previewcontrol1.Document = this.printDocument1;
        }

        public void ProvideContent(object sender, PrintPageEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Item");
            table.Columns.Add("Qty");
            table.Columns.Add("Price");
            table.Columns.Add("Total");
            object[] values = new object[] { "Item 1", "3", "100.00", "300.00" };
            table.Rows.Add(values);
            object[] objArray2 = new object[] { "Item 2", "5", "50.00", "250.00" };
            table.Rows.Add(objArray2);
            object[] objArray3 = new object[] { "Item 3", "2", "200.00", "400.00" };
            table.Rows.Add(objArray3);
            this.TaxAmt = (this.TaxPercentage * this.GrossTotal) / 100.0;
            Graphics graphics = e.Graphics;
            int num = 30;
            StringFormat format1 = new StringFormat();
            format1.LineAlignment = StringAlignment.Center;
            format1.Alignment = StringAlignment.Center;
            StringFormat format = format1;
            graphics.DrawString(this.Title, new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(this.Box, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(this.Tel, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(this.Email, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString(this.Pin, new Font("Palatino Linotype", 12f, FontStyle.Regular), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 20;
            graphics.DrawString("Sales Receipt", new Font("Palatino Linotype", 15f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            graphics.DrawString("____________", new Font("Palatino Linotype", 15f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
            num += 15;
            graphics.DrawString("BillNo :01010101 ", new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
            num += 20;
            graphics.DrawString("Date:" + Program.CurrentDateTime().ToShortDateString(), new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
            graphics.DrawString("Counter : " + this.CounterName, new Font("Arial", 10f), new SolidBrush(Color.Black), 120f, (float) num);
            num += 20;
            graphics.DrawString("Time : " + Program.CurrentDateTime().ToShortTimeString(), new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
            graphics.DrawString("Served By: " + Program.CurrLoggedInUser.UserFirstname, new Font("Arial", 10f), new SolidBrush(Color.Black), 120f, (float) num);
            num += 10;
            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
            num += 10;
            graphics.DrawString("Item                                   Qty Price    Total", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float) num);
            graphics.DrawString("______________________________________", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
            num += 20;
            int num2 = 0;
            while (true)
            {
                if (num2 >= table.Rows.Count)
                {
                    graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float) num);
                    num += 15;
                    graphics.DrawString("TOTAL :", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
                    graphics.DrawString(this.GrossTotal.ToString(), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 200f, (float) num);
                    num += 20;
                    graphics.DrawString("Amount Paid :", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
                    graphics.DrawString(this.AmountPaid.ToString(), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 200f, (float) num);
                    num += 20;
                    graphics.DrawString("Change", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
                    graphics.DrawString(this.Change.ToString(), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 200f, (float) num);
                    num += 10;
                    graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                    num += 15;
                    graphics.DrawString("Tax%        TaxAmt", new Font("Arial", 10f, FontStyle.Underline), new SolidBrush(Color.Black), 70f, (float) num);
                    num += 15;
                    graphics.DrawString(this.TaxPercentage.ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 80f, (float) num);
                    graphics.DrawString(this.TaxAmt.ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 140f, (float) num);
                    num += 10;
                    graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                    num += 20;
                    graphics.DrawString(this.Footer1, new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 20;
                    graphics.DrawString(this.Footer2, new Font("Arial", 10f, FontStyle.Italic), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 15;
                    graphics.DrawString(this.Website, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    return;
                }
                if (table.Rows[num2][0].ToString().Length <= 0x1f)
                {
                    graphics.DrawString(table.Rows[num2][0].ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                }
                else
                {
                    Array array = table.Rows[num2][0].ToString().ToCharArray(0, 30);
                    string s = "";
                    int index = 0;
                    while (true)
                    {
                        string text1;
                        if (index >= array.Length)
                        {
                            graphics.DrawString(s, new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                            break;
                        }
                        object obj1 = array.GetValue(index);
                        if (obj1 != null)
                        {
                            text1 = obj1.ToString();
                        }
                        else
                        {
                            object local1 = obj1;
                            text1 = null;
                        }
                        s = s + text1;
                        index++;
                    }
                }
                num += 15;
                string[] textArray1 = new string[] { "                                                           ", table.Rows[num2][1].ToString().Trim(), " *  ", table.Rows[num2][2].ToString(), "   ", table.Rows[num2][3].ToString() };
                graphics.DrawString(string.Concat(textArray1), new Font("Arial", 8f), new SolidBrush(Color.Black), 0f, (float) num);
                num += 15;
                num2++;
            }
        }

        private void TabPage_BusinessProfile_Click(object sender, EventArgs e)
        {
        }

        private void TabPage_Receipt_Click(object sender, EventArgs e)
        {
        }
    }
}

