namespace LaxPos.Inventory
{
    using Bunifu.Framework.UI;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class InventoryReports : BunifuForm
    {
        private IContainer components = null;

        public InventoryReports()
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
            base.Name = "InventoryReports";
            base.Size = new Size(0x3b9, 0x18b);
            base.ResumeLayout(false);
        }
    }
}

