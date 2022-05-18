namespace LaxPos.Pos
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class ReceiptSettings : Form
    {
        private readonly CompanyProfile Client = new CompanyProfile();
        public string Title = "";
        public string Box = "";
        public string Email = "";
        public string Tel = "";
        public string Text4 = "";
        public string Pin = "";
        public string Text1 = "";
        public string Text2 = "";
        public decimal TaxRate = 0M;
        public string Text3 = "";
        public string CounterName = Environment.MachineName;
        public int Center_X = 150;
        public decimal GrossTotal = 950M;
        public decimal TaxPercentage = 16M;
        public decimal TaxAmt = 0M;
        public decimal AmountPaid = 1000M;
        public decimal Change = 50M;
        private IContainer components = null;
        private Button Btn_PrintPreview;
        private Button Btn_CurrentSettings;
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
        private Label label5;
        private Label label4;
        private TextBox textBox15;
        private TextBox textBox12;
        private TextBox textBox9;
        private TextBox textBox6;
        private TextBox textBox1;
        private Button Btn_SaveSettings;
        private PrintPreviewControl Previewcontrol1;
        private PrintDocument printDocument1;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label7;

        public ReceiptSettings()
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
            this.textBox4.Text = this.Text1;
            this.textBox16.Text = this.Text2;
            this.textBox10.Text = this.Text3;
            this.textBox2.Text = this.TaxRate.ToString();
            this.textBox3.Text = this.Text4;
        }

        public void AssignValueToFields()
        {
            try
            {
                this.Title = this.textBox1.Text;
                this.Box = this.textBox6.Text;
                this.Email = this.textBox12.Text;
                this.Tel = this.textBox9.Text;
                this.Text3 = this.textBox10.Text;
                this.Pin = this.textBox15.Text;
                this.Text1 = this.textBox4.Text;
                this.Text2 = this.textBox16.Text;
                this.TaxRate = decimal.Parse(this.textBox2.Text);
                this.Text4 = this.textBox3.Text.ToString();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
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
            this.textBox3.Text = "";
            this.textBox2.Text = "";
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

        private void Btn_PrintPreview_Click(object sender, EventArgs e)
        {
            this.printDocument1.PrintController = new StandardPrintController();
            this.printDocument1.Print();
        }

        private void Btn_SaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                this.Client.ClientTitle = this.textBox1.Text;
                this.Client.ClientAddress = this.textBox6.Text;
                this.Client.ClientTel = this.textBox9.Text;
                this.Client.ClientEmail = this.textBox12.Text;
                this.Client.ClientPin = this.textBox15.Text;
                this.Client.ClientText1 = this.textBox4.Text;
                this.Client.ClientText2 = this.textBox16.Text;
                this.Client.ClientText3 = this.textBox10.Text;
                this.Client.ClientTaxRate = decimal.Parse(this.textBox2.Text);
                this.Client.ClientText4 = this.textBox3.Text;
                MessageBox.Show("Receipt Settings Saved !!", "", MessageBoxButtons.OK);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        private void InitializeComponent()
        {
            this.Btn_PrintPreview = new Button();
            this.Btn_CurrentSettings = new Button();
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
            this.label5 = new Label();
            this.label4 = new Label();
            this.textBox15 = new TextBox();
            this.textBox12 = new TextBox();
            this.textBox9 = new TextBox();
            this.textBox6 = new TextBox();
            this.textBox1 = new TextBox();
            this.Btn_SaveSettings = new Button();
            this.Previewcontrol1 = new PrintPreviewControl();
            this.printDocument1 = new PrintDocument();
            this.label1 = new Label();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox3 = new TextBox();
            this.label7 = new Label();
            base.SuspendLayout();
            this.Btn_PrintPreview.Location = new Point(0x1a8, 0xe1);
            this.Btn_PrintPreview.Margin = new Padding(3, 4, 3, 4);
            this.Btn_PrintPreview.Name = "Btn_PrintPreview";
            this.Btn_PrintPreview.Size = new Size(0xa6, 0x1c);
            this.Btn_PrintPreview.TabIndex = 0x98;
            this.Btn_PrintPreview.Text = "Print Preview";
            this.Btn_PrintPreview.UseVisualStyleBackColor = true;
            this.Btn_PrintPreview.Click += new EventHandler(this.Btn_PrintPreview_Click);
            this.Btn_CurrentSettings.Location = new Point(0x1a8, 0x9f);
            this.Btn_CurrentSettings.Margin = new Padding(3, 4, 3, 4);
            this.Btn_CurrentSettings.Name = "Btn_CurrentSettings";
            this.Btn_CurrentSettings.Size = new Size(0xa6, 0x1c);
            this.Btn_CurrentSettings.TabIndex = 0x97;
            this.Btn_CurrentSettings.Text = "Current Settings";
            this.Btn_CurrentSettings.UseVisualStyleBackColor = true;
            this.Btn_CurrentSettings.Click += new EventHandler(this.Btn_CurrentSettings_Click);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(20, 300);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x2b, 13);
            this.label8.TabIndex = 150;
            this.label8.Text = "Footer2";
            this.textBox10.Location = new Point(0x5f, 0x150);
            this.textBox10.Margin = new Padding(3, 4, 3, 4);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Size(280, 20);
            this.textBox10.TabIndex = 0x95;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(20, 0x105);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x2b, 13);
            this.label10.TabIndex = 0x94;
            this.label10.Text = "Footer1";
            this.textBox16.Location = new Point(0x5e, 300);
            this.textBox16.Margin = new Padding(3, 4, 3, 4);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Size(280, 20);
            this.textBox16.TabIndex = 0x93;
            this.Btn_ClearAll.Location = new Point(0x1a8, 0x27);
            this.Btn_ClearAll.Margin = new Padding(3, 4, 3, 4);
            this.Btn_ClearAll.Name = "Btn_ClearAll";
            this.Btn_ClearAll.Size = new Size(0xa6, 0x1c);
            this.Btn_ClearAll.TabIndex = 0x92;
            this.Btn_ClearAll.Text = "Clear All";
            this.Btn_ClearAll.UseVisualStyleBackColor = true;
            this.Btn_ClearAll.Click += new EventHandler(this.Btn_ClearAll_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x15, 0xba);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2f, 13);
            this.label3.TabIndex = 0x91;
            this.label3.Text = "KRA Pin";
            this.textBox4.Location = new Point(0x5e, 0x105);
            this.textBox4.Margin = new Padding(3, 4, 3, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(280, 20);
            this.textBox4.TabIndex = 0x90;
            this.Btn_ViewAll.Location = new Point(0x1a8, 0x62);
            this.Btn_ViewAll.Margin = new Padding(3, 4, 3, 4);
            this.Btn_ViewAll.Name = "Btn_ViewAll";
            this.Btn_ViewAll.Size = new Size(0xa6, 0x1c);
            this.Btn_ViewAll.TabIndex = 0x8f;
            this.Btn_ViewAll.Text = "Preview Receipt";
            this.Btn_ViewAll.UseVisualStyleBackColor = true;
            this.Btn_ViewAll.Click += new EventHandler(this.Btn_Preview_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(20, 0x150);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x2e, 13);
            this.label9.TabIndex = 0x8e;
            this.label9.Text = "Website";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x15, 0x6c);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3e, 13);
            this.label6.TabIndex = 0x8d;
            this.label6.Text = "Tel Number";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x15, 0x45);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2d, 13);
            this.label5.TabIndex = 140;
            this.label5.Text = "Address";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x15, 30);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x43, 13);
            this.label4.TabIndex = 0x8b;
            this.label4.Text = "Receipt Title";
            this.textBox15.Location = new Point(0x5e, 0xba);
            this.textBox15.Margin = new Padding(3, 4, 3, 4);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Size(280, 20);
            this.textBox15.TabIndex = 0x8a;
            this.textBox12.Location = new Point(0x5e, 0x93);
            this.textBox12.Margin = new Padding(3, 4, 3, 4);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Size(280, 20);
            this.textBox12.TabIndex = 0x89;
            this.textBox9.Location = new Point(0x5e, 0x6c);
            this.textBox9.Margin = new Padding(3, 4, 3, 4);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Size(280, 20);
            this.textBox9.TabIndex = 0x88;
            this.textBox6.Location = new Point(0x5e, 0x45);
            this.textBox6.Margin = new Padding(3, 4, 3, 4);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Size(280, 20);
            this.textBox6.TabIndex = 0x87;
            this.textBox1.Location = new Point(0x5e, 30);
            this.textBox1.Margin = new Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(280, 20);
            this.textBox1.TabIndex = 0x86;
            this.Btn_SaveSettings.Location = new Point(0x1a8, 300);
            this.Btn_SaveSettings.Margin = new Padding(3, 4, 3, 4);
            this.Btn_SaveSettings.Name = "Btn_SaveSettings";
            this.Btn_SaveSettings.Size = new Size(0xa6, 0x1c);
            this.Btn_SaveSettings.TabIndex = 0x9a;
            this.Btn_SaveSettings.Text = "Save settings";
            this.Btn_SaveSettings.UseVisualStyleBackColor = true;
            this.Btn_SaveSettings.Click += new EventHandler(this.Btn_SaveSettings_Click);
            this.Previewcontrol1.AutoZoom = false;
            this.Previewcontrol1.Location = new Point(0x25e, 13);
            this.Previewcontrol1.Margin = new Padding(3, 4, 3, 4);
            this.Previewcontrol1.Name = "Previewcontrol1";
            this.Previewcontrol1.Size = new Size(0x147, 0x14f);
            this.Previewcontrol1.TabIndex = 0x99;
            this.Previewcontrol1.Zoom = 1.0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x15, 0x93);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x20, 13);
            this.label1.TabIndex = 0x9b;
            this.label1.Text = "Email";
            this.textBox2.Location = new Point(0x5f, 0xe1);
            this.textBox2.Margin = new Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(280, 20);
            this.textBox2.TabIndex = 0x9d;
            this.textBox2.KeyPress += new KeyPressEventHandler(this.InputNumbers);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 0xe1);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 13);
            this.label2.TabIndex = 0x9c;
            this.label2.Text = "TaxRate";
            this.textBox3.Location = new Point(0x5e, 0x16d);
            this.textBox3.Margin = new Padding(3, 4, 3, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(280, 20);
            this.textBox3.TabIndex = 0x9f;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x13, 0x16d);
            this.label7.Name = "label7";
            this.label7.Size = new Size(60, 13);
            this.label7.TabIndex = 0x9e;
            this.label7.Text = "Till Number";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x38a, 450);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.Btn_SaveSettings);
            base.Controls.Add(this.Previewcontrol1);
            base.Controls.Add(this.Btn_PrintPreview);
            base.Controls.Add(this.Btn_CurrentSettings);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.textBox10);
            base.Controls.Add(this.label10);
            base.Controls.Add(this.textBox16);
            base.Controls.Add(this.Btn_ClearAll);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox4);
            base.Controls.Add(this.Btn_ViewAll);
            base.Controls.Add(this.label9);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBox15);
            base.Controls.Add(this.textBox12);
            base.Controls.Add(this.textBox9);
            base.Controls.Add(this.textBox6);
            base.Controls.Add(this.textBox1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Name = "ReceiptSettings";
            this.Text = "ReceiptSettings";
            base.Load += new EventHandler(this.ReceiptSettings_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InputNumbers(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || (e.KeyChar.ToString() == "."));
        }

        public void LoadCurrentSettings()
        {
            this.Title = this.Client.ClientTitle;
            this.Box = this.Client.ClientAddress;
            this.Email = this.Client.ClientEmail;
            this.Tel = this.Client.ClientTel;
            this.Text4 = this.Client.ClientText4;
            this.Pin = this.Client.ClientPin;
            this.Text2 = this.Client.ClientText2;
            this.Text3 = this.Client.ClientText3;
            this.TaxRate = this.Client.ClientTaxRate;
            this.Text1 = this.Client.ClientText1.ToString();
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
            this.TaxAmt = (this.TaxPercentage * this.GrossTotal) / 100M;
            int num = 10;
            Graphics graphics = e.Graphics;
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
                    graphics.DrawString(this.Text1, new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 20;
                    graphics.DrawString(this.Text2, new Font("Arial", 10f, FontStyle.Italic), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 15;
                    graphics.DrawString(this.Text3, new Font("Arial", 8f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 15;
                    graphics.DrawString(this.Text4, new Font("Arial", 8f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
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

        private void ReceiptSettings_Load(object sender, EventArgs e)
        {
        }
    }
}

