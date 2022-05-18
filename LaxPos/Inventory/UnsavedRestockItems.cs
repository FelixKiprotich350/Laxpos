namespace LaxPos.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class UnsavedRestockItems : Form
    {
        public List<DataGridViewRow> UnsavedItems = new List<DataGridViewRow>();
        private IContainer components = null;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column2;
        public DataGridView UnsavedItems_Gridview;
        private Button BTN_oK;
        private Label label1;

        public UnsavedRestockItems(List<DataGridViewRow> Unsaved)
        {
            this.InitializeComponent();
            this.UnsavedItems = Unsaved;
        }

        private void BTN_oK_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists("UnsavedItems.csv"))
                {
                    File.Delete("UnsavedItems.csv");
                }
                int num = 0;
                while (true)
                {
                    if (num >= this.UnsavedItems_Gridview.RowCount)
                    {
                        base.Close();
                        break;
                    }
                    File.AppendAllText("UnsavedItems.csv", this.UnsavedItems_Gridview.Rows[num].Cells[1].Value.ToString() + "\n");
                    num++;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
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
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.panel2 = new Panel();
            this.BTN_oK = new Button();
            this.panel3 = new Panel();
            this.UnsavedItems_Gridview = new DataGridView();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize) this.UnsavedItems_Gridview).BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x275, 0x24);
            this.panel1.TabIndex = 0;
            this.label1.Dock = DockStyle.Fill;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x275, 0x24);
            this.label1.TabIndex = 0;
            this.label1.Text = "The following items have not been saved";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.panel2.Controls.Add(this.BTN_oK);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x16b);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x275, 0x24);
            this.panel2.TabIndex = 1;
            this.BTN_oK.Location = new Point(0x20c, 6);
            this.BTN_oK.Name = "BTN_oK";
            this.BTN_oK.Size = new Size(0x4b, 0x17);
            this.BTN_oK.TabIndex = 0;
            this.BTN_oK.Text = "Close";
            this.BTN_oK.UseVisualStyleBackColor = true;
            this.BTN_oK.Click += new EventHandler(this.BTN_oK_Click);
            this.panel3.Controls.Add(this.UnsavedItems_Gridview);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(0, 0x24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x275, 0x147);
            this.panel3.TabIndex = 2;
            this.UnsavedItems_Gridview.AllowUserToAddRows = false;
            this.UnsavedItems_Gridview.AllowUserToResizeColumns = false;
            this.UnsavedItems_Gridview.AllowUserToResizeRows = false;
            this.UnsavedItems_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.UnsavedItems_Gridview.BackgroundColor = Color.White;
            this.UnsavedItems_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column3, this.Column1, this.Column4, this.Column6, this.Column5, this.Column2 };
            this.UnsavedItems_Gridview.Columns.AddRange(dataGridViewColumns);
            this.UnsavedItems_Gridview.Dock = DockStyle.Fill;
            this.UnsavedItems_Gridview.EnableHeadersVisualStyles = false;
            this.UnsavedItems_Gridview.Location = new Point(0, 0);
            this.UnsavedItems_Gridview.Name = "UnsavedItems_Gridview";
            this.UnsavedItems_Gridview.RowHeadersVisible = false;
            this.UnsavedItems_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Palatino Linotype", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.UnsavedItems_Gridview.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.UnsavedItems_Gridview.Size = new Size(0x275, 0x147);
            this.UnsavedItems_Gridview.TabIndex = 0x15;
            this.Column3.HeaderText = "OrderId";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column1.HeaderText = "ProductId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column4.HeaderText = "Description";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column6.HeaderText = "Unit";
            this.Column6.Name = "Column6";
            this.Column5.HeaderText = "Quantity";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column2.HeaderText = "SupplierId";
            this.Column2.Name = "Column2";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.ClientSize = new Size(0x275, 0x18f);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UnsavedRestockItems";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "UnsavedRestockItems";
            base.Load += new EventHandler(this.UnsavedRestockItems_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((ISupportInitialize) this.UnsavedItems_Gridview).EndInit();
            base.ResumeLayout(false);
        }

        private void UnsavedRestockItems_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.UnsavedItems)
            {
                object[] values = new object[] { row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString() };
                this.UnsavedItems_Gridview.Rows.Add(values);
            }
        }
    }
}

