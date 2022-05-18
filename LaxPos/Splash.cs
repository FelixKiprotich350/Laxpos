namespace LaxPos
{
    using LaxPos.LaxPosFiles;
    using LaxPos.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Splash : Form
    {
        private CompanyDetails Company_Details = new CompanyDetails();
        private IContainer components = null;
        private Label label2;
        private Label label7;
        private Label label6;
        private PictureBox pictureBox1;
        private Label label1;
        public Timer timer1;
        private Label label8;
        private Label label3;
        private ProgressBar progressBar1;
        private Label label4;

        public Splash()
        {
            this.InitializeComponent();
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
            this.components = new Container();
            this.label2 = new Label();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label1 = new Label();
            this.timer1 = new Timer(this.components);
            this.label8 = new Label();
            this.label3 = new Label();
            this.progressBar1 = new ProgressBar();
            this.pictureBox1 = new PictureBox();
            this.label4 = new Label();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
            this.label2.FlatStyle = FlatStyle.Flat;
            this.label2.Font = new Font("Palatino Linotype", 15f, FontStyle.Underline | FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.ForeColor = Color.DarkBlue;
            this.label2.Location = new Point(0x9f, 0x4f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x126, 0x1c);
            this.label2.TabIndex = 0x29;
            this.label2.Text = "LAXCO INC TECHNOLOGY";
            this.label2.TextAlign = ContentAlignment.MiddleCenter;
            this.label7.FlatStyle = FlatStyle.Flat;
            this.label7.Font = new Font("Palatino Linotype", 20f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label7.ForeColor = Color.DarkBlue;
            this.label7.Location = new Point(0x9f, 12);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x126, 0x23);
            this.label7.TabIndex = 40;
            this.label7.Text = "Point Of Sales Sytem";
            this.label7.TextAlign = ContentAlignment.MiddleCenter;
            this.label6.FlatStyle = FlatStyle.Flat;
            this.label6.Font = new Font("Palatino Linotype", 15f, FontStyle.Italic | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(0xd0, 0x33);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0xb9, 0x1c);
            this.label6.TabIndex = 0x27;
            this.label6.Text = "Powered By";
            this.label6.TextAlign = ContentAlignment.MiddleCenter;
            this.label1.FlatStyle = FlatStyle.Flat;
            this.label1.Font = new Font("Palatino Linotype", 12f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x16, 160);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xd0, 0x19);
            this.label1.TabIndex = 0x25;
            this.label1.Text = "Email : info@laxcoinc.com";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.timer1.Interval = 0x3e8;
            this.timer1.Tick += new EventHandler(this.Timer1_Tick);
            this.label8.FlatStyle = FlatStyle.Flat;
            this.label8.Font = new Font("Perpetua Titling MT", 17f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label8.ForeColor = Color.DarkBlue;
            this.label8.Location = new Point(0xf8, 0x6d);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x9e, 0x23);
            this.label8.TabIndex = 0x2e;
            this.label8.Text = "00/00/0000";
            this.label8.TextAlign = ContentAlignment.MiddleLeft;
            this.label3.FlatStyle = FlatStyle.Flat;
            this.label3.Font = new Font("Palatino Linotype", 12f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(0x10c, 160);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xa1, 0x19);
            this.label3.TabIndex = 0x2b;
            this.label3.Text = "www.laxcoinc.com";
            this.label3.TextAlign = ContentAlignment.MiddleCenter;
            this.progressBar1.BackColor = Color.Blue;
            this.progressBar1.Location = new Point(9, 0x93);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x1bc, 10);
            this.progressBar1.Step = 5;
            this.progressBar1.TabIndex = 0x2a;
            this.pictureBox1.Image = Resources.logopng;
            this.pictureBox1.Location = new Point(9, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x8d, 0x77);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0x26;
            this.pictureBox1.TabStop = false;
            this.label4.FlatStyle = FlatStyle.Flat;
            this.label4.Font = new Font("Times New Roman", 17.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label4.ForeColor = Color.DarkBlue;
            this.label4.Location = new Point(0xb0, 0x6d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4e, 0x23);
            this.label4.TabIndex = 0x2f;
            this.label4.Text = "Date :";
            this.label4.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Coral;
            base.ClientSize = new Size(0x1cd, 0xbc);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.pictureBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.progressBar1);
            this.DoubleBuffered = true;
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Splash";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Splash";
            base.Load += new EventHandler(this.Splash_Load);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            this.label8.Text = Program.CurrentDateTime().Date.ToShortDateString().ToString();
            this.timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.Company_Details.GetCompanyDetails();
            if (this.progressBar1.Value < 100)
            {
                this.progressBar1.Value += 10;
            }
            else
            {
                base.DialogResult = DialogResult.OK;
            }
        }
    }
}

