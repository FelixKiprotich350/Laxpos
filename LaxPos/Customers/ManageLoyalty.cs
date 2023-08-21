namespace LaxPos.Customers
{ 
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ManageLoyalty : Form
    {
        private IContainer components = null;

        public ManageLoyalty()
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.Name = "ManageLoyalty";
            base.Size = new Size(0x4b0, 600);
            base.ResumeLayout(false);
        }
    }
}

