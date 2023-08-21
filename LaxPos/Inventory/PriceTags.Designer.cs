
namespace LaxPos.Inventory
{
    partial class PriceTags
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceTags));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel_ProductSearch = new System.Windows.Forms.Panel();
            this.Btn_SelectAllItems = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.Btn_AddItem = new System.Windows.Forms.Button();
            this.Product_IdLabel = new System.Windows.Forms.Label();
            this.Panel_salesOperations = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_Print = new System.Windows.Forms.Button();
            this.Btn_ClearAll = new System.Windows.Forms.Button();
            this.Cart_Gridview = new System.Windows.Forms.DataGridView();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Panel_ProductSearch.SuspendLayout();
            this.Panel_salesOperations.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cart_Gridview)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_ProductSearch
            // 
            this.Panel_ProductSearch.Controls.Add(this.Btn_SelectAllItems);
            this.Panel_ProductSearch.Controls.Add(this.textBox4);
            this.Panel_ProductSearch.Controls.Add(this.Btn_AddItem);
            this.Panel_ProductSearch.Controls.Add(this.Product_IdLabel);
            this.Panel_ProductSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_ProductSearch.Location = new System.Drawing.Point(0, 0);
            this.Panel_ProductSearch.Name = "Panel_ProductSearch";
            this.Panel_ProductSearch.Size = new System.Drawing.Size(979, 43);
            this.Panel_ProductSearch.TabIndex = 19;
            // 
            // Btn_SelectAllItems
            // 
            this.Btn_SelectAllItems.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_SelectAllItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_SelectAllItems.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_SelectAllItems.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_SelectAllItems.Location = new System.Drawing.Point(663, 7);
            this.Btn_SelectAllItems.Name = "Btn_SelectAllItems";
            this.Btn_SelectAllItems.Size = new System.Drawing.Size(281, 30);
            this.Btn_SelectAllItems.TabIndex = 16;
            this.Btn_SelectAllItems.Text = "Select All Items";
            this.Btn_SelectAllItems.UseVisualStyleBackColor = false;
            this.Btn_SelectAllItems.Click += new System.EventHandler(this.Btn_SelectAllItems_Click);
            // 
            // textBox4
            // 
            this.textBox4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(103, 9);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(365, 26);
            this.textBox4.TabIndex = 15;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // Btn_AddItem
            // 
            this.Btn_AddItem.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_AddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_AddItem.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_AddItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_AddItem.Location = new System.Drawing.Point(474, 7);
            this.Btn_AddItem.Name = "Btn_AddItem";
            this.Btn_AddItem.Size = new System.Drawing.Size(144, 30);
            this.Btn_AddItem.TabIndex = 13;
            this.Btn_AddItem.Text = "Add Item";
            this.Btn_AddItem.UseVisualStyleBackColor = false;
            this.Btn_AddItem.Click += new System.EventHandler(this.Btn_AddItem_Click);
            // 
            // Product_IdLabel
            // 
            this.Product_IdLabel.AutoSize = true;
            this.Product_IdLabel.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Product_IdLabel.Location = new System.Drawing.Point(9, 11);
            this.Product_IdLabel.Name = "Product_IdLabel";
            this.Product_IdLabel.Size = new System.Drawing.Size(88, 22);
            this.Product_IdLabel.TabIndex = 11;
            this.Product_IdLabel.Text = "Item Code";
            // 
            // Panel_salesOperations
            // 
            this.Panel_salesOperations.BackColor = System.Drawing.Color.GhostWhite;
            this.Panel_salesOperations.Controls.Add(this.panel3);
            this.Panel_salesOperations.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_salesOperations.Location = new System.Drawing.Point(0, 396);
            this.Panel_salesOperations.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.Panel_salesOperations.Name = "Panel_salesOperations";
            this.Panel_salesOperations.Size = new System.Drawing.Size(979, 54);
            this.Panel_salesOperations.TabIndex = 20;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Btn_Close);
            this.panel3.Controls.Add(this.Btn_Print);
            this.panel3.Controls.Add(this.Btn_ClearAll);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(-106, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1085, 54);
            this.panel3.TabIndex = 62;
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Close.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Close.Location = new System.Drawing.Point(118, 5);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(10);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(160, 40);
            this.Btn_Close.TabIndex = 61;
            this.Btn_Close.Text = "Close [F1]";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_Print
            // 
            this.Btn_Print.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Print.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Print.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Print.Location = new System.Drawing.Point(910, 5);
            this.Btn_Print.Margin = new System.Windows.Forms.Padding(8);
            this.Btn_Print.Name = "Btn_Print";
            this.Btn_Print.Size = new System.Drawing.Size(160, 40);
            this.Btn_Print.TabIndex = 49;
            this.Btn_Print.Text = "Print";
            this.Btn_Print.UseVisualStyleBackColor = false;
            this.Btn_Print.Click += new System.EventHandler(this.Btn_Print_Click);
            // 
            // Btn_ClearAll
            // 
            this.Btn_ClearAll.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_ClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ClearAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ClearAll.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_ClearAll.Location = new System.Drawing.Point(507, 5);
            this.Btn_ClearAll.Margin = new System.Windows.Forms.Padding(10);
            this.Btn_ClearAll.Name = "Btn_ClearAll";
            this.Btn_ClearAll.Size = new System.Drawing.Size(188, 40);
            this.Btn_ClearAll.TabIndex = 50;
            this.Btn_ClearAll.Text = "Clear All";
            this.Btn_ClearAll.UseVisualStyleBackColor = false;
            // 
            // Cart_Gridview
            // 
            this.Cart_Gridview.AllowUserToAddRows = false;
            this.Cart_Gridview.AllowUserToResizeColumns = false;
            this.Cart_Gridview.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cart_Gridview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Cart_Gridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Cart_Gridview.BackgroundColor = System.Drawing.Color.White;
            this.Cart_Gridview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Cart_Gridview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Cart_Gridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Cart_Gridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column9,
            this.Column4,
            this.Column3});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Cart_Gridview.DefaultCellStyle = dataGridViewCellStyle6;
            this.Cart_Gridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cart_Gridview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.Cart_Gridview.EnableHeadersVisualStyles = false;
            this.Cart_Gridview.Location = new System.Drawing.Point(0, 43);
            this.Cart_Gridview.Name = "Cart_Gridview";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.NullValue = "X";
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Cart_Gridview.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.Cart_Gridview.RowHeadersVisible = false;
            this.Cart_Gridview.RowHeadersWidth = 20;
            this.Cart_Gridview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Palatino Linotype", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.NullValue = null;
            this.Cart_Gridview.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.Cart_Gridview.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cart_Gridview.RowTemplate.Height = 26;
            this.Cart_Gridview.Size = new System.Drawing.Size(979, 353);
            this.Cart_Gridview.TabIndex = 21;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // Column1
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.FillWeight = 25F;
            this.Column1.HeaderText = "Code";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.FillWeight = 62.55304F;
            this.Column2.HeaderText = "Description";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.FillWeight = 20F;
            this.Column9.HeaderText = "Unit";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column4
            // 
            dataGridViewCellStyle5.Format = "N2";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.FillWeight = 20F;
            this.Column4.HeaderText = "UnitPrice";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 50;
            // 
            // PriceTags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 450);
            this.Controls.Add(this.Cart_Gridview);
            this.Controls.Add(this.Panel_salesOperations);
            this.Controls.Add(this.Panel_ProductSearch);
            this.Name = "PriceTags";
            this.Text = "PriceTags";
            this.Shown += new System.EventHandler(this.PriceTags_Shown);
            this.Panel_ProductSearch.ResumeLayout(false);
            this.Panel_ProductSearch.PerformLayout();
            this.Panel_salesOperations.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Cart_Gridview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_ProductSearch;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button Btn_AddItem;
        private System.Windows.Forms.Label Product_IdLabel;
        private System.Windows.Forms.Panel Panel_salesOperations;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_Print;
        private System.Windows.Forms.Button Btn_ClearAll;
        public System.Windows.Forms.DataGridView Cart_Gridview;
        private System.Windows.Forms.Button Btn_SelectAllItems;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}