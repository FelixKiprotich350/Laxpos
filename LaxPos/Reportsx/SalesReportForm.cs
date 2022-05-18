namespace LaxPos.Reports
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class SalesReportForm : Form
    {
        public DataTable Dt = null;
        private IContainer components = null;

        public SalesReportForm(DataTable _Dt)
        {
            this.InitializeComponent();
            this.Dt = _Dt;
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
            base.ClientSize = new Size(0x360, 0x295);
            base.MinimizeBox = false;
            base.Name = "SalesReportForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "SalesReportForm";
            base.Load += new EventHandler(this.SalesReportForm_Load);
            base.ResumeLayout(false);
        }

        private void SalesReportForm_Load(object sender, EventArgs e)
        {
        }
    }
}

