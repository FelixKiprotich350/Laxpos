namespace LaxPos.Inventory
{
    using Bunifu.Framework.UI;
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class StockManagement : BunifuForm
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private TabControl Tab_StockManagement;
        private TabPage Tab_Page_StockTaking;
        private TabPage TabPage_stockAdjustment;
        private Button Btn_SearchSTT;
        public TextBox textBox6;
        private Label label9;
        private GroupBox groupBox5;
        private RadioButton RadioButton_Uncounted;
        private RadioButton RadioButton_All;
        private GroupBox groupBox7;
        private GroupBox groupBox6;
        private DataGridView DGView_StockTackingCount;
        private Label label11;
        private Button Btn_ManageStockTakingNo;
        private Button Btn_ExportSttItems;
        private DataGridViewTextBoxColumn STT_No;
        private DataGridViewTextBoxColumn ST_Itemcode;
        private DataGridViewTextBoxColumn ST_Name;
        private DataGridViewTextBoxColumn ST_Expected;
        private DataGridViewTextBoxColumn ST_Counted;
        private DataGridViewTextBoxColumn ST_Status;
        private DataGridViewTextBoxColumn STTDate;
        private Button Btn_RemoveAllItems;
        private TabPage TabPage_UploadSTT_Items;
        private Button Btn_UploadsttTabOpen;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private GroupBox groupBox2;

        public StockManagement()
        {
            this.InitializeComponent();
        }

        private unsafe void Btn_ExportSttItems_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog;
                bool flag2;
                if (this.DGView_StockTackingCount.Rows.Count <= 0)
                {
                    MessageBox.Show("No Record To Export !!!", "Info");
                    return;
                }
                else
                {
                    SaveFileDialog dialog1 = new SaveFileDialog();
                    dialog1.Filter = "CSV (*.csv)|*.csv";
                    dialog1.FileName = "Output.csv";
                    dialog = dialog1;
                    flag2 = false;
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                    {
                        return;
                    }
                    else if (File.Exists(dialog.FileName))
                    {
                        try
                        {
                            File.Delete(dialog.FileName);
                        }
                        catch (IOException exception)
                        {
                            flag2 = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + exception.Message);
                        }
                    }
                }
                if (!flag2)
                {
                    try
                    {
                        int count = this.DGView_StockTackingCount.Columns.Count;
                        string str = "";
                        string[] contents = new string[this.DGView_StockTackingCount.Rows.Count + 1];
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= count)
                            {
                                string* textPtr1 = contents;
                                textPtr1 = (string*) (((string) textPtr1) + str);
                                int index = 1;
                                while (true)
                                {
                                    if ((index - 1) >= this.DGView_StockTackingCount.Rows.Count)
                                    {
                                        File.WriteAllLines(dialog.FileName, contents, Encoding.UTF8);
                                        MessageBox.Show("Data Exported Successfully !!!", "Info");
                                        break;
                                    }
                                    int num4 = 0;
                                    while (true)
                                    {
                                        if (num4 >= count)
                                        {
                                            index++;
                                            break;
                                        }
                                        string* textPtr2 = &(contents[index]);
                                        textPtr2 = (string*) (((string) textPtr2) + this.DGView_StockTackingCount.Rows[index - 1].Cells[num4].Value.ToString() + ",");
                                        num4++;
                                    }
                                }
                                break;
                            }
                            str = str + this.DGView_StockTackingCount.Columns[num2].HeaderText.ToString() + ",";
                            num2++;
                        }
                    }
                    catch (Exception exception2)
                    {
                        MessageBox.Show("Error :" + exception2.Message);
                    }
                }
            }
            catch (Exception exception5)
            {
                MessageBox.Show(exception5.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_ManageStockTakingNo_Click(object sender, EventArgs e)
        {
            new ManageStockNumber().ShowDialog(this);
        }

        private void Btn_RemoveAllItems_Click(object sender, EventArgs e)
        {
        }

        private void Btn_SearchSTT_Click(object sender, EventArgs e)
        {
            if (this.textBox6.Text == "")
            {
                MessageBox.Show("Fill in the STN", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (this.RadioButton_All.Checked)
            {
                this.GetStockTakingItems(1);
            }
            else if (this.RadioButton_Uncounted.Checked)
            {
                this.GetStockTakingItems(2);
            }
            else
            {
                MessageBox.Show("Kindly Check one Item", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_UploadsttTabOpen_Click(object sender, EventArgs e)
        {
            this.Tab_StockManagement.SelectedTab = this.Tab_StockManagement.TabPages["TabPage_UploadSTT_Items"];
        }

        private void DataGridView_StockTackingCount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int columnIndex = e.ColumnIndex;
                if (((rowIndex >= 0) && (columnIndex == 1)) && (MessageBox.Show("Are you sure you want to delete this item?", "Message Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    this.DGView_StockTackingCount.Rows.RemoveAt(rowIndex);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DataGridView_StockTackingCount_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                int columnIndex = e.ColumnIndex;
                if (this.DGView_StockTackingCount.Rows[rowIndex].Cells[columnIndex].Value.ToString() == "")
                {
                    this.DGView_StockTackingCount.Rows[rowIndex].Cells[columnIndex].Value = 0;
                    this.DGView_StockTackingCount.UpdateCellValue(columnIndex, rowIndex);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void DataGridView_StockTackingCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        public void GetStockTakingItems(int Type)
        {
            try
            {
                this.DGView_StockTackingCount.Rows.Clear();
                string cmdText = "";
                if (Type == 0)
                {
                    cmdText = "SELECT * FROM stocktakingitems where ItemCode=@itemcode LIMIT 10";
                }
                else if (Type == 1)
                {
                    cmdText = "SELECT * FROM stocktakingitems where STT=@stt";
                }
                else if (Type == 2)
                {
                    cmdText = "SELECT * FROM stocktakingitems where STT=@stt AND CountStatus=@status";
                }
                else
                {
                    MessageBox.Show("You have not selected any Group Of Items", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand(cmdText, connection);
                command.Parameters.AddWithValue("@status", "1");
                command.Parameters.AddWithValue("@stt", this.textBox6.Text);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Items Found!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["STT"], reader["ItemCode"], reader["ItemName"], reader["Expected"], reader["Counted"], reader["CountStatus"], reader.GetDateTime("DateCounted").ToShortDateString(), "" };
                        this.DGView_StockTackingCount.Rows.Add(values);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            this.Tab_StockManagement = new TabControl();
            this.Tab_Page_StockTaking = new TabPage();
            this.groupBox7 = new GroupBox();
            this.DGView_StockTackingCount = new DataGridView();
            this.STT_No = new DataGridViewTextBoxColumn();
            this.ST_Itemcode = new DataGridViewTextBoxColumn();
            this.ST_Name = new DataGridViewTextBoxColumn();
            this.ST_Expected = new DataGridViewTextBoxColumn();
            this.ST_Counted = new DataGridViewTextBoxColumn();
            this.ST_Status = new DataGridViewTextBoxColumn();
            this.STTDate = new DataGridViewTextBoxColumn();
            this.groupBox6 = new GroupBox();
            this.Btn_RemoveAllItems = new Button();
            this.Btn_ExportSttItems = new Button();
            this.groupBox5 = new GroupBox();
            this.Btn_UploadsttTabOpen = new Button();
            this.Btn_ManageStockTakingNo = new Button();
            this.label11 = new Label();
            this.RadioButton_Uncounted = new RadioButton();
            this.RadioButton_All = new RadioButton();
            this.Btn_SearchSTT = new Button();
            this.label9 = new Label();
            this.textBox6 = new TextBox();
            this.TabPage_UploadSTT_Items = new TabPage();
            this.TabPage_stockAdjustment = new TabPage();
            this.groupBox1 = new GroupBox();
            this.groupBox2 = new GroupBox();
            this.groupBox3 = new GroupBox();
            this.dataGridView1 = new DataGridView();
            this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            this.Tab_StockManagement.SuspendLayout();
            this.Tab_Page_StockTaking.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((ISupportInitialize) this.DGView_StockTackingCount).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.TabPage_UploadSTT_Items.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            base.SuspendLayout();
            this.Tab_StockManagement.Controls.Add(this.Tab_Page_StockTaking);
            this.Tab_StockManagement.Controls.Add(this.TabPage_UploadSTT_Items);
            this.Tab_StockManagement.Controls.Add(this.TabPage_stockAdjustment);
            this.Tab_StockManagement.Dock = DockStyle.Fill;
            this.Tab_StockManagement.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Tab_StockManagement.HotTrack = true;
            this.Tab_StockManagement.Location = new Point(0, 0);
            this.Tab_StockManagement.Margin = new Padding(4);
            this.Tab_StockManagement.Name = "Tab_StockManagement";
            this.Tab_StockManagement.Padding = new Point(0x19, 3);
            this.Tab_StockManagement.SelectedIndex = 0;
            this.Tab_StockManagement.Size = new Size(0x3d3, 0x1a6);
            this.Tab_StockManagement.TabIndex = 7;
            this.Tab_StockManagement.Selected += new TabControlEventHandler(this.Tab_Suppliers_Selected);
            this.Tab_Page_StockTaking.BackColor = SystemColors.ButtonHighlight;
            this.Tab_Page_StockTaking.Controls.Add(this.groupBox7);
            this.Tab_Page_StockTaking.Controls.Add(this.groupBox6);
            this.Tab_Page_StockTaking.Controls.Add(this.groupBox5);
            this.Tab_Page_StockTaking.Location = new Point(4, 0x19);
            this.Tab_Page_StockTaking.Name = "Tab_Page_StockTaking";
            this.Tab_Page_StockTaking.Padding = new Padding(3);
            this.Tab_Page_StockTaking.Size = new Size(0x3cb, 0x189);
            this.Tab_Page_StockTaking.TabIndex = 2;
            this.Tab_Page_StockTaking.Text = "Stock Taking";
            this.groupBox7.Controls.Add(this.DGView_StockTackingCount);
            this.groupBox7.Dock = DockStyle.Fill;
            this.groupBox7.Location = new Point(3, 0x79);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new Size(0x3c5, 0xaf);
            this.groupBox7.TabIndex = 0x12;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Items List";
            this.DGView_StockTackingCount.AllowUserToAddRows = false;
            this.DGView_StockTackingCount.AllowUserToDeleteRows = false;
            this.DGView_StockTackingCount.AllowUserToResizeColumns = false;
            this.DGView_StockTackingCount.AllowUserToResizeRows = false;
            this.DGView_StockTackingCount.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.DGView_StockTackingCount.BackgroundColor = SystemColors.ButtonHighlight;
            this.DGView_StockTackingCount.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.STT_No, this.ST_Itemcode, this.ST_Name, this.ST_Expected, this.ST_Counted, this.ST_Status, this.STTDate };
            this.DGView_StockTackingCount.Columns.AddRange(dataGridViewColumns);
            this.DGView_StockTackingCount.Dock = DockStyle.Fill;
            this.DGView_StockTackingCount.Location = new Point(3, 0x13);
            this.DGView_StockTackingCount.Name = "DGView_StockTackingCount";
            this.DGView_StockTackingCount.RowHeadersVisible = false;
            this.DGView_StockTackingCount.Size = new Size(0x3bf, 0x99);
            this.DGView_StockTackingCount.TabIndex = 0;
            this.DGView_StockTackingCount.CellDoubleClick += new DataGridViewCellEventHandler(this.DataGridView_StockTackingCount_CellDoubleClick);
            this.DGView_StockTackingCount.CellValidated += new DataGridViewCellEventHandler(this.DataGridView_StockTackingCount_CellValidated);
            this.DGView_StockTackingCount.KeyPress += new KeyPressEventHandler(this.DataGridView_StockTackingCount_KeyPress);
            this.STT_No.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.STT_No.HeaderText = "STTN";
            this.STT_No.Name = "STT_No";
            this.STT_No.ReadOnly = true;
            this.ST_Itemcode.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.ST_Itemcode.HeaderText = "ItemCode";
            this.ST_Itemcode.Name = "ST_Itemcode";
            this.ST_Itemcode.ReadOnly = true;
            this.ST_Itemcode.Width = 150;
            this.ST_Name.HeaderText = "Name";
            this.ST_Name.Name = "ST_Name";
            this.ST_Name.ReadOnly = true;
            this.ST_Expected.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.ST_Expected.HeaderText = "Expected";
            this.ST_Expected.Name = "ST_Expected";
            this.ST_Expected.ReadOnly = true;
            this.ST_Counted.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            style.Format = "N0";
            style.NullValue = null;
            this.ST_Counted.DefaultCellStyle = style;
            this.ST_Counted.HeaderText = "Counted";
            this.ST_Counted.Name = "ST_Counted";
            this.ST_Counted.SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ST_Status.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.ST_Status.HeaderText = "Status";
            this.ST_Status.Name = "ST_Status";
            this.ST_Status.ReadOnly = true;
            this.STTDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.STTDate.HeaderText = "STT Date";
            this.STTDate.Name = "STTDate";
            this.STTDate.ReadOnly = true;
            this.STTDate.Width = 120;
            this.groupBox6.Controls.Add(this.Btn_RemoveAllItems);
            this.groupBox6.Controls.Add(this.Btn_ExportSttItems);
            this.groupBox6.Dock = DockStyle.Bottom;
            this.groupBox6.Location = new Point(3, 0x128);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x3c5, 0x5e);
            this.groupBox6.TabIndex = 0x11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "groupBox6";
            this.Btn_RemoveAllItems.Location = new Point(0x18f, 0x1b);
            this.Btn_RemoveAllItems.Name = "Btn_RemoveAllItems";
            this.Btn_RemoveAllItems.Size = new Size(0xa6, 0x29);
            this.Btn_RemoveAllItems.TabIndex = 0x1d;
            this.Btn_RemoveAllItems.Text = "Remove AllI tems";
            this.Btn_RemoveAllItems.UseVisualStyleBackColor = true;
            this.Btn_RemoveAllItems.Click += new EventHandler(this.Btn_RemoveAllItems_Click);
            this.Btn_ExportSttItems.Location = new Point(740, 0x1b);
            this.Btn_ExportSttItems.Name = "Btn_ExportSttItems";
            this.Btn_ExportSttItems.Size = new Size(0xa6, 0x29);
            this.Btn_ExportSttItems.TabIndex = 0x1c;
            this.Btn_ExportSttItems.Text = "Export Products";
            this.Btn_ExportSttItems.UseVisualStyleBackColor = true;
            this.Btn_ExportSttItems.Click += new EventHandler(this.Btn_ExportSttItems_Click);
            this.groupBox5.Controls.Add(this.Btn_UploadsttTabOpen);
            this.groupBox5.Controls.Add(this.Btn_ManageStockTakingNo);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.RadioButton_Uncounted);
            this.groupBox5.Controls.Add(this.RadioButton_All);
            this.groupBox5.Controls.Add(this.Btn_SearchSTT);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.textBox6);
            this.groupBox5.Dock = DockStyle.Top;
            this.groupBox5.Location = new Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x3c5, 0x76);
            this.groupBox5.TabIndex = 0x10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Items Search Box";
            this.Btn_UploadsttTabOpen.Location = new Point(0x2cb, 0x19);
            this.Btn_UploadsttTabOpen.Name = "Btn_UploadsttTabOpen";
            this.Btn_UploadsttTabOpen.Size = new Size(0x8b, 0x3e);
            this.Btn_UploadsttTabOpen.TabIndex = 0x23;
            this.Btn_UploadsttTabOpen.Text = "Upload STT Items";
            this.Btn_UploadsttTabOpen.UseVisualStyleBackColor = true;
            this.Btn_UploadsttTabOpen.Click += new EventHandler(this.Btn_UploadsttTabOpen_Click);
            this.Btn_ManageStockTakingNo.Location = new Point(0x1dc, 0x19);
            this.Btn_ManageStockTakingNo.Name = "Btn_ManageStockTakingNo";
            this.Btn_ManageStockTakingNo.Size = new Size(0xc3, 0x3e);
            this.Btn_ManageStockTakingNo.TabIndex = 0x22;
            this.Btn_ManageStockTakingNo.Text = "Manage Stock Taking Number";
            this.Btn_ManageStockTakingNo.UseVisualStyleBackColor = true;
            this.Btn_ManageStockTakingNo.Click += new EventHandler(this.Btn_ManageStockTakingNo_Click);
            this.label11.BorderStyle = BorderStyle.FixedSingle;
            this.label11.Location = new Point(0x1a6, 0x11);
            this.label11.Name = "label11";
            this.label11.Size = new Size(2, 0x57);
            this.label11.TabIndex = 0x21;
            this.label11.Text = "label11";
            this.RadioButton_Uncounted.AutoSize = true;
            this.RadioButton_Uncounted.Location = new Point(0x101, 0x17);
            this.RadioButton_Uncounted.Name = "RadioButton_Uncounted";
            this.RadioButton_Uncounted.Size = new Size(0x84, 0x15);
            this.RadioButton_Uncounted.TabIndex = 0x1d;
            this.RadioButton_Uncounted.TabStop = true;
            this.RadioButton_Uncounted.Text = "Uncounted Items";
            this.RadioButton_Uncounted.UseVisualStyleBackColor = true;
            this.RadioButton_All.AutoSize = true;
            this.RadioButton_All.Location = new Point(0xad, 0x17);
            this.RadioButton_All.Name = "RadioButton_All";
            this.RadioButton_All.Size = new Size(0x4e, 0x15);
            this.RadioButton_All.TabIndex = 0x1c;
            this.RadioButton_All.TabStop = true;
            this.RadioButton_All.Text = "All Items";
            this.RadioButton_All.UseVisualStyleBackColor = true;
            this.Btn_SearchSTT.Location = new Point(0x8d, 80);
            this.Btn_SearchSTT.Name = "Btn_SearchSTT";
            this.Btn_SearchSTT.Size = new Size(0x8b, 0x20);
            this.Btn_SearchSTT.TabIndex = 0x1b;
            this.Btn_SearchSTT.Text = "Search STT";
            this.Btn_SearchSTT.UseVisualStyleBackColor = true;
            this.Btn_SearchSTT.Click += new EventHandler(this.Btn_SearchSTT_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x15, 0x19);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x74, 0x11);
            this.label9.TabIndex = 14;
            this.label9.Text = "Stock Taking No:";
            this.textBox6.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textBox6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox6.Location = new Point(0x18, 0x30);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Size(0x17a, 0x1a);
            this.textBox6.TabIndex = 0x1a;
            this.TabPage_UploadSTT_Items.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_UploadSTT_Items.Controls.Add(this.groupBox3);
            this.TabPage_UploadSTT_Items.Controls.Add(this.groupBox2);
            this.TabPage_UploadSTT_Items.Controls.Add(this.groupBox1);
            this.TabPage_UploadSTT_Items.Location = new Point(4, 0x19);
            this.TabPage_UploadSTT_Items.Name = "TabPage_UploadSTT_Items";
            this.TabPage_UploadSTT_Items.Padding = new Padding(3);
            this.TabPage_UploadSTT_Items.Size = new Size(0x3cb, 0x189);
            this.TabPage_UploadSTT_Items.TabIndex = 5;
            this.TabPage_UploadSTT_Items.Text = "Upload STT";
            this.TabPage_stockAdjustment.BackColor = SystemColors.ButtonHighlight;
            this.TabPage_stockAdjustment.Location = new Point(4, 0x19);
            this.TabPage_stockAdjustment.Name = "TabPage_stockAdjustment";
            this.TabPage_stockAdjustment.Padding = new Padding(3);
            this.TabPage_stockAdjustment.Size = new Size(0x3cb, 0x189);
            this.TabPage_stockAdjustment.TabIndex = 4;
            this.TabPage_stockAdjustment.Text = "Stock Adjustments";
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3c5, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox2.Dock = DockStyle.Bottom;
            this.groupBox2.Location = new Point(3, 290);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x3c5, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Dock = DockStyle.Fill;
            this.groupBox3.Location = new Point(3, 0x67);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x3c5, 0xbb);
            this.groupBox3.TabIndex = 0x13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Items List";
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] columnArray2 = new DataGridViewColumn[] { this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6, this.dataGridViewTextBoxColumn7 };
            this.dataGridView1.Columns.AddRange(columnArray2);
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(3, 0x13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new Size(0x3bf, 0xa5);
            this.dataGridView1.TabIndex = 0;
            this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.HeaderText = "STTN";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.HeaderText = "ItemCode";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.HeaderText = "Expected";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            style2.Format = "N0";
            style2.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = style2;
            this.dataGridViewTextBoxColumn5.HeaderText = "Counted";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.HeaderText = "Status";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.HeaderText = "STT Date";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 120;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x3d3, 0x1a6);
            base.Controls.Add(this.Tab_StockManagement);
            base.Name = "StockManagement";
            base.Load += new EventHandler(this.StockManagement_Load);
            this.Tab_StockManagement.ResumeLayout(false);
            this.Tab_Page_StockTaking.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((ISupportInitialize) this.DGView_StockTackingCount).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.TabPage_UploadSTT_Items.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            base.ResumeLayout(false);
        }

        private void SaveSttitems(object sender, EventArgs e)
        {
            STTWaitDialog dialog = new STTWaitDialog();
            dialog.Show();
            int num = 0;
            int num2 = 0;
            try
            {
                dialog.progressBar1.Maximum = this.DGView_StockTackingCount.Rows.Count;
                dialog.TextBox_Total.Text = this.DGView_StockTackingCount.Rows.Count.ToString();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE stocktakingitems SET Counted=@counted,CountStatus=@status,DateCounted=@date WHERE STT=@stt AND ItemCode=@itemcode", connection);
                command.Parameters.Add("@status", MySqlDbType.VarChar);
                command.Parameters.Add("@counted", MySqlDbType.Int32);
                command.Parameters.Add("@stt", MySqlDbType.VarChar);
                command.Parameters.Add("@itemcode", MySqlDbType.VarChar);
                command.Parameters.Add("@date", MySqlDbType.DateTime);
                foreach (DataGridViewRow row in (IEnumerable) this.DGView_StockTackingCount.Rows)
                {
                    if (row.Cells[5].Value.ToString() != "Uncounted")
                    {
                        num++;
                    }
                    else
                    {
                        command.Parameters["@status"].Value = "Counted";
                        command.Parameters["@counted"].Value = row.Cells[4].Value.ToString();
                        command.Parameters["@stt"].Value = row.Cells[0].Value.ToString();
                        command.Parameters["@itemcode"].Value = row.Cells[1].Value.ToString();
                        command.Parameters["@date"].Value = Program.CurrentDateTime();
                        if (command.ExecuteNonQuery() > 0)
                        {
                            num++;
                        }
                        else
                        {
                            num++;
                            num2++;
                        }
                    }
                    dialog.progressBar1.Value = num;
                    dialog.TextBox_Updated.Text = num.ToString();
                }
                connection.Close();
                dialog.Close();
                if (num2 > 0)
                {
                    MessageBox.Show("Failed to Update : " + num2.ToString(), "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void StockManagement_Load(object sender, EventArgs e)
        {
            this.Tab_StockManagement.SelectedTab = this.Tab_StockManagement.TabPages["Tab_Page_StockTaking"];
        }

        private void Tab_Suppliers_Selected(object sender, TabControlEventArgs e)
        {
            if (this.Tab_StockManagement.SelectedTab.Name == "Tab_Page_StockTaking")
            {
                this.RadioButton_All.Checked = true;
            }
        }
    }
}

