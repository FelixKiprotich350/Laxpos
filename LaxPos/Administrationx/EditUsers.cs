namespace LaxPos.Administration
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class EditUsers : Form
    {
        private IContainer components = null;

        public EditUsers()
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
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(800, 0x153);
            this.Cursor = Cursors.Default;
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "EditUsers";
            this.Text = "EditUsers";
            base.ResumeLayout(false);
        }
    }
}

