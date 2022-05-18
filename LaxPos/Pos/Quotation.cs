namespace LaxPos.Pos
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Quotation : Form
    {
        private IContainer components = null;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private GroupBox groupBox2;
        private TextBox textBox9;
        private Label label9;
        private TextBox textBox11;
        private Label label11;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox12;
        private Label label12;
        private TextBox textBox13;
        private Label label13;
        private GroupBox groupBox3;
        private TextBox textBox6;
        private Label label6;
        private TextBox textBox10;
        private Label label10;
        private TextBox textBox8;
        private Label label8;
        private TextBox textBox7;
        private Label label7;
        private Button Btn_Acceptquotation;
        private Button Btn_Close;
        public TextBox textBox1;

        public Quotation()
        {
            this.InitializeComponent();
        }

        private void Btn_Acceptquotation_Click(object sender, EventArgs e)
        {
            if ((this.textBox2.Text != "") && (this.textBox3.Text != ""))
            {
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Incomplete Customers Details", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.No;
            base.Close();
        }

        private void Btn_SearchCustomer_Click(object sender, EventArgs e)
        {
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
            this.groupBox1 = new GroupBox();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.textBox12 = new TextBox();
            this.label12 = new Label();
            this.textBox13 = new TextBox();
            this.label13 = new Label();
            this.textBox5 = new TextBox();
            this.label5 = new Label();
            this.textBox4 = new TextBox();
            this.label4 = new Label();
            this.textBox9 = new TextBox();
            this.label9 = new Label();
            this.textBox11 = new TextBox();
            this.label11 = new Label();
            this.groupBox3 = new GroupBox();
            this.textBox10 = new TextBox();
            this.label10 = new Label();
            this.textBox8 = new TextBox();
            this.label8 = new Label();
            this.textBox7 = new TextBox();
            this.label7 = new Label();
            this.textBox6 = new TextBox();
            this.label6 = new Label();
            this.Btn_Acceptquotation = new Button();
            this.Btn_Close = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x17a, 0x83);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Customer Details";
            this.textBox3.Location = new Point(0x5b, 90);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0x105, 0x1a);
            this.textBox3.TabIndex = 6;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x3a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x33, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Name";
            this.textBox2.Location = new Point(0x5b, 0x3a);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x105, 0x1a);
            this.textBox2.TabIndex = 4;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x30, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email";
            this.textBox1.Location = new Point(0x5b, 0x19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x105, 0x1a);
            this.textBox1.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x19);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x49, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Contacts";
            this.groupBox2.Controls.Add(this.textBox12);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBox13);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox11);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Dock = DockStyle.Top;
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox2.Location = new Point(0, 0x83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x17a, 0xc7);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service Details";
            this.textBox12.Location = new Point(220, 0xa7);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Size(0x85, 0x1a);
            this.textBox12.TabIndex = 20;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(220, 0x90);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x42, 20);
            this.label12.TabIndex = 0x13;
            this.label12.Text = "Valid To";
            this.textBox13.Location = new Point(0x10, 0xa7);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Size(0x9f, 0x1a);
            this.textBox13.TabIndex = 0x12;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x10, 0x90);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x55, 20);
            this.label13.TabIndex = 0x11;
            this.label13.Text = "Valid From";
            this.textBox5.Location = new Point(220, 110);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(0x85, 0x1a);
            this.textBox5.TabIndex = 0x10;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(220, 0x57);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x42, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Counter";
            this.textBox4.Location = new Point(0x10, 110);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0xc6, 0x1a);
            this.textBox4.TabIndex = 14;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x10, 0x57);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x51, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Served By";
            this.textBox9.Location = new Point(220, 0x34);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Size(0x85, 0x1a);
            this.textBox9.TabIndex = 12;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(220, 0x1d);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x44, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "Quot No";
            this.textBox11.Location = new Point(0x10, 0x34);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Size(0xc6, 0x1a);
            this.textBox11.TabIndex = 8;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x10, 0x1d);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x76, 20);
            this.label11.TabIndex = 7;
            this.label11.Text = "Quotation Date";
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Dock = DockStyle.Top;
            this.groupBox3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.groupBox3.Location = new Point(0, 330);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x17a, 0xa4);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Summary Report";
            this.textBox10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox10.Location = new Point(0xa1, 0x80);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Size(0xbf, 0x1a);
            this.textBox10.TabIndex = 0x1a;
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(0x10, 0x80);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x8b, 20);
            this.label10.TabIndex = 0x19;
            this.label10.Text = "Amount Payable";
            this.textBox8.Location = new Point(0xa1, 0x60);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Size(0xbf, 0x1a);
            this.textBox8.TabIndex = 0x18;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x60);
            this.label8.Name = "label8";
            this.label8.Size = new Size(130, 20);
            this.label8.TabIndex = 0x17;
            this.label8.Text = "Discount Offered";
            this.textBox7.Location = new Point(0xa1, 0x40);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Size(0xbf, 0x1a);
            this.textBox7.TabIndex = 0x16;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x10, 0x40);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x5e, 20);
            this.label7.TabIndex = 0x15;
            this.label7.Text = "Tax Amount";
            this.textBox6.Location = new Point(0xa1, 0x21);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Size(0xbf, 0x1a);
            this.textBox6.TabIndex = 20;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x10, 0x21);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x70, 20);
            this.label6.TabIndex = 0x13;
            this.label6.Text = "Gross Amount";
            this.Btn_Acceptquotation.Location = new Point(0xe0, 500);
            this.Btn_Acceptquotation.Name = "Btn_Acceptquotation";
            this.Btn_Acceptquotation.Size = new Size(0x80, 30);
            this.Btn_Acceptquotation.TabIndex = 3;
            this.Btn_Acceptquotation.Text = "Accept Quotation";
            this.Btn_Acceptquotation.UseVisualStyleBackColor = true;
            this.Btn_Acceptquotation.Click += new EventHandler(this.Btn_Acceptquotation_Click);
            this.Btn_Close.DialogResult = DialogResult.Cancel;
            this.Btn_Close.Location = new Point(0x40, 500);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x7b, 30);
            this.Btn_Close.TabIndex = 5;
            this.Btn_Close.Text = "Close Quotation";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            base.AcceptButton = this.Btn_Acceptquotation;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.Btn_Close;
            base.ClientSize = new Size(0x17a, 0x213);
            base.Controls.Add(this.Btn_Close);
            base.Controls.Add(this.Btn_Acceptquotation);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Quotation";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Quotation Form";
            base.Load += new EventHandler(this.Quotation_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

        private void Quotation_Load(object sender, EventArgs e)
        {
        }
    }
}

