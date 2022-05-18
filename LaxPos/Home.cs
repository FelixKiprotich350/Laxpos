namespace LaxPos
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Home : Form
    {
        private IContainer components = null;
        private Panel MainPanel;
        public Timer timer1;
        private Label label1;

        public Home()
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

        private void HomeControl_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.MainPanel = new Panel();
            this.label1 = new Label();
            this.timer1 = new Timer(this.components);
            this.MainPanel.SuspendLayout();
            base.SuspendLayout();
            this.MainPanel.BackColor = Color.Transparent;
            this.MainPanel.BackgroundImageLayout = ImageLayout.Stretch;
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Dock = DockStyle.Top;
            this.MainPanel.Location = new Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new Size(0x3d2, 0x2b);
            this.MainPanel.TabIndex = 3;
            this.label1.Dock = DockStyle.Top;
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3d2, 0x22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Laxco Administration";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x3d2, 0x2ed);
            base.Controls.Add(this.MainPanel);
            this.DoubleBuffered = true;
            this.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Margin = new Padding(4, 6, 4, 6);
            base.Name = "Home";
            base.Load += new EventHandler(this.HomeControl_Load);
            this.MainPanel.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked label");
        }

        private void Panel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked me");
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked picture");
        }
    }
}

