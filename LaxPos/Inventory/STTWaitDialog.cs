namespace LaxPos.Inventory
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class STTWaitDialog : Form
    {
        private IContainer components = null;
        private FlowLayoutPanel flowLayoutPanel1;
        public TextBox TextBox_Updated;
        private Label label2;
        public TextBox TextBox_Total;
        public ProgressBar progressBar1;
        private Label label1;
        private Button Btn_stop;

        public STTWaitDialog()
        {
            this.InitializeComponent();
        }

        private void Btn_stop_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Btn_stop_Click_1(object sender, EventArgs e)
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
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.TextBox_Updated = new TextBox();
            this.label2 = new Label();
            this.TextBox_Total = new TextBox();
            this.progressBar1 = new ProgressBar();
            this.label1 = new Label();
            this.Btn_stop = new Button();
            this.flowLayoutPanel1.SuspendLayout();
            base.SuspendLayout();
            this.flowLayoutPanel1.Controls.Add(this.TextBox_Updated);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.TextBox_Total);
            this.flowLayoutPanel1.Location = new Point(13, 0x40);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new Size(0x142, 0x1f);
            this.flowLayoutPanel1.TabIndex = 7;
            this.flowLayoutPanel1.UseWaitCursor = true;
            this.TextBox_Updated.Location = new Point(3, 3);
            this.TextBox_Updated.Name = "TextBox_Updated";
            this.TextBox_Updated.Size = new Size(0x90, 0x17);
            this.TextBox_Updated.TabIndex = 6;
            this.TextBox_Updated.TextAlign = HorizontalAlignment.Center;
            this.TextBox_Updated.UseWaitCursor = true;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(0x99, 2);
            this.label2.Margin = new Padding(3, 2, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x13, 0x19);
            this.label2.TabIndex = 7;
            this.label2.Text = "/";
            this.label2.UseWaitCursor = true;
            this.TextBox_Total.Location = new Point(0xb2, 3);
            this.TextBox_Total.Name = "TextBox_Total";
            this.TextBox_Total.Size = new Size(0x80, 0x17);
            this.TextBox_Total.TabIndex = 8;
            this.TextBox_Total.UseWaitCursor = true;
            this.progressBar1.Location = new Point(13, 30);
            this.progressBar1.Margin = new Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x1ad, 0x1b);
            this.progressBar1.TabIndex = 6;
            this.progressBar1.UseWaitCursor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 9);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x70, 0x11);
            this.label1.TabIndex = 5;
            this.label1.Text = "Saving Progress";
            this.label1.UseWaitCursor = true;
            this.Btn_stop.Location = new Point(0x156, 0x43);
            this.Btn_stop.Margin = new Padding(4);
            this.Btn_stop.Name = "Btn_stop";
            this.Btn_stop.Size = new Size(100, 0x1c);
            this.Btn_stop.TabIndex = 4;
            this.Btn_stop.Text = "Cancel";
            this.Btn_stop.UseVisualStyleBackColor = true;
            this.Btn_stop.UseWaitCursor = true;
            this.Btn_stop.Click += new EventHandler(this.Btn_stop_Click_1);
            base.AutoScaleDimensions = new SizeF(8f, 16f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1db, 0x8e);
            base.Controls.Add(this.flowLayoutPanel1);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.Btn_stop);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.Margin = new Padding(4);
            base.Name = "STTWaitDialog";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "STTWaitDialog";
            base.TopMost = true;
            base.UseWaitCursor = true;
            base.Load += new EventHandler(this.STTWaitDialog_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void STTWaitDialog_Load(object sender, EventArgs e)
        {
        }
    }
}

