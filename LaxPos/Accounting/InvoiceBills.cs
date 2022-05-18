namespace LaxPos.Accounting
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InvoiceBills : Form
    {
        private IContainer components = null;

        public InvoiceBills()
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
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(800, 450);
            this.Text = "InvoiceBills";
        }
    }
}

