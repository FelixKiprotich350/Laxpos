namespace LaxExpanderPanel
{
    using LaxExpanderPanel.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class LaxExpander : UserControl
    {
        public int ExpanderDefaultHeight = 30;
        private IContainer components = null;
        public Panel HeaderPanel;
        public PictureBox ExpIcon;
        public Panel ContentPanel;
        public Label HeaderText;

        public LaxExpander()
        {
            this.HeadingText = "HeadingText";
            this.InitializeComponent();
            this.ExpanderDefaultHeight = base.Height;
            this.HeaderText.Text = this.HeadingText;
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
            if (this.ContentPanel.Visible)
            {
               this.ExpIcon.Image = Resources.testimage;
                base.Height = this.ExpanderDefaultHeight;
                this.ContentPanel.Visible = false;
            }
            else
            {
                this.ExpIcon.Image = Resources.testimage;
                int height = this.HeaderPanel.Height;
                this.ContentPanel.Visible = true;
                foreach (Control control in this.ContentPanel.Controls)
                {
                    height = (height + control.Height) + 1;
                }
                base.Height = height;
            }
        }

        private void HeaderText_Click(object sender, EventArgs e)
        {
            this.ExpIcon_Click(this.ExpIcon, new EventArgs());
        }

        private void InitializeComponent()
        {
            this.HeaderText = new System.Windows.Forms.Label();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.ExpIcon = new System.Windows.Forms.PictureBox();
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.HeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExpIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // HeaderText
            // 
            this.HeaderText.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.HeaderText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HeaderText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HeaderText.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderText.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.HeaderText.Location = new System.Drawing.Point(32, 0);
            this.HeaderText.Name = "HeaderText";
            this.HeaderText.Size = new System.Drawing.Size(235, 30);
            this.HeaderText.TabIndex = 3;
            this.HeaderText.Text = "HeaderText";
            this.HeaderText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HeaderText.Click += new System.EventHandler(this.HeaderText_Click);
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Controls.Add(this.HeaderText);
            this.HeaderPanel.Controls.Add(this.ExpIcon);
            this.HeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeaderPanel.Location = new System.Drawing.Point(1, 1);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(267, 30);
            this.HeaderPanel.TabIndex = 2;
            // 
            // ExpIcon
            // 
            this.ExpIcon.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ExpIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.ExpIcon.Image = global::LaxExpanderPanel.Properties.Resources.testimage;
            this.ExpIcon.Location = new System.Drawing.Point(0, 0);
            this.ExpIcon.Name = "ExpIcon";
            this.ExpIcon.Size = new System.Drawing.Size(32, 30);
            this.ExpIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ExpIcon.TabIndex = 2;
            this.ExpIcon.TabStop = false;
            this.ExpIcon.Click += new System.EventHandler(this.ExpIcon_Click);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(1, 31);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(267, 0);
            this.ContentPanel.TabIndex = 3;
            this.ContentPanel.Visible = false;
            // 
            // LaxExpander
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.HeaderPanel);
            this.Name = "LaxExpander";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(269, 30);
            this.HeaderPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExpIcon)).EndInit();
            this.ResumeLayout(false);

        }

        [Description("HeadingText"), Category("HeadingText"), EditorBrowsable(EditorBrowsableState.Advanced), Browsable(true)]
        public string HeadingText { get; set; }
    }
}

