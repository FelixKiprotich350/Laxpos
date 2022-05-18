namespace LaxPos.Administration
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ViewPassword_Enabler : Form
    {
        private IContainer components = null;
        private Button Btn_Close;
        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private Label label3;
        private TextBox textBox3;
        private Label label4;
        private Button Btn_Update;
        public TextBox textBox1;
        public TextBox textBox2;

        public ViewPassword_Enabler()
        {
            this.InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Btn_Update_Click(object sender, EventArgs e)
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
            this.Btn_Close = new Button();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.comboBox1 = new ComboBox();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.label3 = new Label();
            this.textBox3 = new TextBox();
            this.label4 = new Label();
            this.Btn_Update = new Button();
            base.SuspendLayout();
            this.Btn_Close.BackColor = Color.FromArgb(0, 0xc0, 0);
            this.Btn_Close.FlatAppearance.BorderSize = 0;
            this.Btn_Close.FlatStyle = FlatStyle.Flat;
            this.Btn_Close.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Close.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Close.Location = new Point(260, 0x83);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x55, 0x21);
            this.Btn_Close.TabIndex = 0;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x40, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "User ID";
            this.textBox1.Location = new Point(12, 0x20);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(100, 0x1a);
            this.textBox1.TabIndex = 2;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            object[] items = new object[] { "Yes", "No" };
            this.comboBox1.Items.AddRange(items);
            this.comboBox1.Location = new Point(230, 0x61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 0x1c);
            this.comboBox1.TabIndex = 3;
            this.textBox2.Location = new Point(0x93, 0x20);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0xcc, 0x1a);
            this.textBox2.TabIndex = 5;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x93, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(80, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Full Name";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(230, 0x4a);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x76, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Working Status";
            this.textBox3.Location = new Point(12, 0x61);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0xb8, 0x1a);
            this.textBox3.TabIndex = 8;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x4a);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x71, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "New Password";
            this.Btn_Update.BackColor = Color.Red;
            this.Btn_Update.FlatAppearance.BorderSize = 0;
            this.Btn_Update.FlatStyle = FlatStyle.Flat;
            this.Btn_Update.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Update.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Update.Location = new Point(0x10, 0x83);
            this.Btn_Update.Name = "Btn_Update";
            this.Btn_Update.Size = new Size(0x60, 0x21);
            this.Btn_Update.TabIndex = 9;
            this.Btn_Update.Text = "Update";
            this.Btn_Update.UseVisualStyleBackColor = false;
            this.Btn_Update.Click += new EventHandler(this.Btn_Update_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x165, 0xae);
            base.Controls.Add(this.Btn_Update);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.Btn_Close);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ViewPassword_Enabler";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "UserName";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

