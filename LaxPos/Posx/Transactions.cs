namespace LaxPos.Pos
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Transactions : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private static decimal GrossAmount;
        private readonly DataTable PassedDt;
        private int CheckTrials = 0;
        private IContainer components = null;
        private Button Btn_CompleteTransaction;
        private Label label12;
        private Label label13;
        public TextBox Txt_AmountPaidTotal;
        public TextBox Txt_Balance;
        private DataGridView Payments_Gridview;
        private ContextMenuStrip ContextMenuStrip_paymentsGridview;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Button Btn_Close;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private Timer Timer1;
        public Button Btn_Cash;
        public Button Btn_Mpesa;
        public Button Btn_Card;
        private Label label1;
        public TextBox TextBox_CustomerPhone;
        private Label label2;
        public TextBox textBox2;
        private Label label3;
        public Button Btn_SearchCustomer;
        private Label label4;
        public TextBox textBox3;
        private Label label5;
        public TextBox textBox4;

        public Transactions(decimal TotalAmount, DataTable Dt)
        {
            this.InitializeComponent();
            GrossAmount = TotalAmount;
            this.PassedDt = Dt;
        }

        private void AddPayment(double M_Amount, string M_Phone, string M_Tcode)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("update mpesatrans set Status=@status where Transcode=@id and Phone=@phone;", connection);
                command.Parameters.AddWithValue("@id", M_Tcode);
                command.Parameters.AddWithValue("@phone", M_Phone);
                command.Parameters.AddWithValue("@status", 1);
                int num = command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                if (num != 1)
                {
                    MessageBox.Show("The payment cannot be processed", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    object[] values = new object[] { "Mpesa", M_Amount, M_Tcode, M_Phone };
                    this.Payments_Gridview.Rows.Add(values);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_Card_Click(object sender, EventArgs e)
        {
            try
            {
                CardPayment payment = new CardPayment();
                payment.ShowDialog(this);
                decimal amount = payment.Amount;
                string refference = payment.Refference;
                if ((amount != 0M) && (amount > 0M))
                {
                    string str2 = "C-" + Program.CurrentDateTime().ToString("yyyyMMddHHmmssff");
                    object[] values = new object[] { "Card-" + refference, amount, str2, "" };
                    this.Payments_Gridview.Rows.Add(values);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            base.ActiveControl = this.Btn_CompleteTransaction;
        }

        private void Btn_Cash_Click(object sender, EventArgs e)
        {
            try
            {
                CashPayment payment = new CashPayment();
                payment.ShowDialog(this);
                decimal amount = payment.Amount;
                if ((amount != 0M) && (amount > 0M))
                {
                    object[] values = new object[] { "Cash", amount, "", "" };
                    this.Payments_Gridview.Rows.Add(values);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            base.ActiveControl = this.Btn_CompleteTransaction;
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Quit ?", "MessageBox", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.PassedDt.Rows.Clear();
                base.Close();
            }
        }

        private void Btn_CompleteTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(this.Txt_AmountPaidTotal.Text) < GrossAmount)
                {
                    base.DialogResult = DialogResult.No;
                    MessageBox.Show("Insufficient Amount Paid !!", "Transaction Response", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    int count = this.Payments_Gridview.Rows.Count;
                    int num2 = 0;
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= count)
                        {
                            base.DialogResult = (num2 <= 0) ? DialogResult.OK : DialogResult.Cancel;
                            base.Close();
                            break;
                        }
                        try
                        {
                            object[] values = new object[] { this.Payments_Gridview.Rows[num3].Cells[0].Value.ToString(), this.Payments_Gridview.Rows[num3].Cells[1].Value.ToString(), this.Payments_Gridview.Rows[num3].Cells[2].Value.ToString(), this.Payments_Gridview.Rows[num3].Cells[3].Value.ToString(), this.Txt_AmountPaidTotal.Text, this.Txt_Balance.Text };
                            this.PassedDt.Rows.Add(values);
                        }
                        catch (Exception exception1)
                        {
                            num2 = 1;
                            MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        num3++;
                    }
                }
            }
            catch (Exception exception3)
            {
                MessageBox.Show(exception3.Message, "Accounting Response");
            }
        }

        private void Btn_Confirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.CheckTrials = 0;
                if (this.CheckMpesaMessage() == 0)
                {
                    this.Timer1.Start();
                }
            }
            catch
            {
            }
        }

        private void Btn_Mpesa_Click(object sender, EventArgs e)
        {
            try
            {
                MpesaPayment payment = new MpesaPayment();
                payment.ShowDialog(this);
                decimal amount = payment.Amount;
                string refference = payment.Refference;
                if ((amount != 0M) && (amount > 0M))
                {
                    object[] values = new object[] { "Mpesa", amount, refference, "" };
                    this.Payments_Gridview.Rows.Add(values);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            base.ActiveControl = this.Btn_CompleteTransaction;
        }

        private void Btn_SearchCustomer_Click(object sender, EventArgs e)
        {
            if (this.TextBox_CustomerPhone.Text.Trim() != "")
            {
                this.textBox2.Text = this.TextBox_CustomerPhone.Text;
                this.textBox3.Text = "Felix Kiprotich";
                this.textBox4.Text = "Male";
            }
            else
            {
                MessageBox.Show(this, "Please enter the Customers Phone Number!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
            }
        }

        private int CheckMpesaMessage()
        {
            int num5;
            try
            {
                string tCode = "";
                string tPhone = "";
                string tdate = "";
                string ttime = "";
                double tAmount = 0.0;
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand selectCommand = new MySqlCommand("select a.Transcode,a.Phone,a.Amount,a.Status,a.Date,a.Time,a.Name from `p.o.s`.mpesatrans a where a.Phone=@phone and a.Status=0;", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Transcode");
                dataTable.Columns.Add("Phone");
                dataTable.Columns.Add("Amount");
                dataTable.Columns.Add("Status");
                dataTable.Columns.Add("Date");
                dataTable.Columns.Add("Time");
                dataTable.Columns.Add("Name");
                adapter.Fill(dataTable);
                selectCommand.Dispose();
                connection.Close();
                if (dataTable.Rows.Count <= 0)
                {
                    num5 = 0;
                }
                else if (dataTable.Rows.Count == 1)
                {
                    double num2;
                    if (!double.TryParse(dataTable.Rows[0]["Amount"].ToString(), out num2))
                    {
                        MessageBox.Show("Unknown Value Paid", "MpesaMessage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        base.DialogResult = DialogResult.No;
                        num5 = 3;
                    }
                    else
                    {
                        tAmount = num2;
                        tCode = dataTable.Rows[0]["Transcode"].ToString();
                        tPhone = dataTable.Rows[0]["Phone"].ToString();
                        int num3 = 0;
                        int num4 = 0;
                        while (true)
                        {
                            if (num4 >= this.Payments_Gridview.Rows.Count)
                            {
                                if (num3 == 0)
                                {
                                    this.AddPayment(tAmount, tPhone, tCode);
                                }
                                num5 = 1;
                                break;
                            }
                            if (this.Payments_Gridview[2, num4].Value.ToString() == tCode)
                            {
                                num3++;
                            }
                            num4++;
                        }
                    }
                }
                else
                {
                    MpesaCheck check = new MpesaCheck("");
                    if (check.ShowDialog(this) != DialogResult.OK)
                    {
                        num5 = 4;
                    }
                    else
                    {
                        tAmount = check.TAmount;
                        tCode = check.TCode;
                        tPhone = check.TPhone;
                        tdate = check.Tdate;
                        ttime = check.Ttime;
                        int num6 = 0;
                        int num7 = 0;
                        while (true)
                        {
                            if (num7 >= this.Payments_Gridview.Rows.Count)
                            {
                                if (num6 == 0)
                                {
                                    this.AddPayment(tAmount, tPhone, tCode);
                                }
                                num5 = 5;
                                break;
                            }
                            if (this.Payments_Gridview[2, num7].Value.ToString() == tCode)
                            {
                                num6++;
                            }
                            num7++;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                num5 = -1;
            }
            return num5;
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            this.Functionkeys(e);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Payments_Gridview.Rows.Remove(this.Payments_Gridview.CurrentRow);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FindTotal()
        {
            try
            {
                int count = this.Payments_Gridview.Rows.Count;
                if (count <= 0)
                {
                    double num4 = 0.0;
                    this.Txt_AmountPaidTotal.Text = num4.ToString("N2");
                    this.Txt_Balance.Text = num4.ToString("N2");
                }
                else
                {
                    double num2 = 0.0;
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= count)
                        {
                            break;
                        }
                        try
                        {
                            num2 += Convert.ToDouble(this.Payments_Gridview.Rows[num3].Cells[1].Value);
                        }
                        catch (Exception exception1)
                        {
                            MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK);
                        }
                        this.Txt_AmountPaidTotal.Text = num2.ToString("N2");
                        num3++;
                    }
                }
            }
            catch (Exception exception3)
            {
                MessageBox.Show(exception3.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void Functionkeys(KeyEventArgs a)
        {
            Keys keyCode = a.KeyCode;
            if (keyCode == Keys.F2)
            {
                this.Btn_Close_Click(new object(), new EventArgs());
            }
            else
            {
                switch (keyCode)
                {
                    case Keys.F5:
                        this.Btn_Cash_Click(new object(), new EventArgs());
                        break;

                    case Keys.F6:
                    case Keys.F7:
                        break;

                    case Keys.F8:
                        this.Btn_Mpesa_Click(new object(), new EventArgs());
                        break;

                    case Keys.F9:
                        this.Btn_Card_Click(new object(), new EventArgs());
                        break;

                    default:
                        if (keyCode == Keys.F12)
                        {
                            this.Btn_CompleteTransaction_Click(new object(), new EventArgs());
                        }
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.Btn_CompleteTransaction = new Button();
            this.label12 = new Label();
            this.label13 = new Label();
            this.Txt_AmountPaidTotal = new TextBox();
            this.Txt_Balance = new TextBox();
            this.Payments_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.ContextMenuStrip_paymentsGridview = new ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new ToolStripMenuItem();
            this.Btn_Cash = new Button();
            this.Btn_Close = new Button();
            this.Timer1 = new Timer(this.components);
            this.Btn_Mpesa = new Button();
            this.Btn_Card = new Button();
            this.label1 = new Label();
            this.TextBox_CustomerPhone = new TextBox();
            this.label2 = new Label();
            this.textBox2 = new TextBox();
            this.label3 = new Label();
            this.Btn_SearchCustomer = new Button();
            this.label4 = new Label();
            this.textBox3 = new TextBox();
            this.label5 = new Label();
            this.textBox4 = new TextBox();
            ((ISupportInitialize) this.Payments_Gridview).BeginInit();
            this.ContextMenuStrip_paymentsGridview.SuspendLayout();
            base.SuspendLayout();
            this.Btn_CompleteTransaction.BackColor = Color.Green;
            this.Btn_CompleteTransaction.FlatAppearance.BorderSize = 0;
            this.Btn_CompleteTransaction.FlatStyle = FlatStyle.Flat;
            this.Btn_CompleteTransaction.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_CompleteTransaction.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_CompleteTransaction.Location = new Point(0xc4, 0x1ce);
            this.Btn_CompleteTransaction.Margin = new Padding(4, 5, 4, 5);
            this.Btn_CompleteTransaction.Name = "Btn_CompleteTransaction";
            this.Btn_CompleteTransaction.Size = new Size(0x106, 0x26);
            this.Btn_CompleteTransaction.TabIndex = 2;
            this.Btn_CompleteTransaction.Text = "Complete Transaction[F12]";
            this.Btn_CompleteTransaction.UseVisualStyleBackColor = false;
            this.Btn_CompleteTransaction.Click += new EventHandler(this.Btn_CompleteTransaction_Click);
            this.label12.AutoSize = true;
            this.label12.Font = new Font("Microsoft Sans Serif", 20f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label12.Location = new Point(6, 0x164);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0xb3, 0x1f);
            this.label12.TabIndex = 0x18;
            this.label12.Text = "Amount Paid";
            this.label13.AutoSize = true;
            this.label13.Font = new Font("Microsoft Sans Serif", 20f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label13.Location = new Point(6, 0x196);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x7f, 0x1f);
            this.label13.TabIndex = 0x19;
            this.label13.Text = "Balance ";
            this.Txt_AmountPaidTotal.Font = new Font("Microsoft Sans Serif", 25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Txt_AmountPaidTotal.Location = new Point(190, 0x164);
            this.Txt_AmountPaidTotal.Multiline = true;
            this.Txt_AmountPaidTotal.Name = "Txt_AmountPaidTotal";
            this.Txt_AmountPaidTotal.ReadOnly = true;
            this.Txt_AmountPaidTotal.Size = new Size(0x112, 0x2b);
            this.Txt_AmountPaidTotal.TabIndex = 0x1a;
            this.Txt_AmountPaidTotal.Text = "0.00";
            this.Txt_AmountPaidTotal.TextAlign = HorizontalAlignment.Right;
            this.Txt_AmountPaidTotal.WordWrap = false;
            this.Txt_AmountPaidTotal.TextChanged += new EventHandler(this.Txt_AmountPaidTotal_TextChanged);
            this.Txt_Balance.Font = new Font("Microsoft Sans Serif", 25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Txt_Balance.Location = new Point(190, 0x196);
            this.Txt_Balance.Name = "Txt_Balance";
            this.Txt_Balance.ReadOnly = true;
            this.Txt_Balance.Size = new Size(0x112, 0x2d);
            this.Txt_Balance.TabIndex = 0x1b;
            this.Txt_Balance.Text = "0.00";
            this.Txt_Balance.TextAlign = HorizontalAlignment.Right;
            this.Txt_Balance.WordWrap = false;
            this.Payments_Gridview.AllowUserToAddRows = false;
            this.Payments_Gridview.AllowUserToDeleteRows = false;
            this.Payments_Gridview.AllowUserToResizeColumns = false;
            this.Payments_Gridview.AllowUserToResizeRows = false;
            this.Payments_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Payments_Gridview.BackgroundColor = SystemColors.ButtonHighlight;
            this.Payments_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3, this.Column4 };
            this.Payments_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Payments_Gridview.ContextMenuStrip = this.ContextMenuStrip_paymentsGridview;
            this.Payments_Gridview.Location = new Point(4, 0xae);
            this.Payments_Gridview.Name = "Payments_Gridview";
            this.Payments_Gridview.ReadOnly = true;
            this.Payments_Gridview.RowHeadersVisible = false;
            this.Payments_Gridview.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0xff, 0x80, 0);
            this.Payments_Gridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Payments_Gridview.Size = new Size(460, 0xa7);
            this.Payments_Gridview.TabIndex = 4;
            this.Payments_Gridview.RowsAdded += new DataGridViewRowsAddedEventHandler(this.Payments_Gridview_RowsAdded);
            this.Payments_Gridview.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.Payments_Gridview_RowsRemoved);
            this.Payments_Gridview.UserAddedRow += new DataGridViewRowEventHandler(this.Payments_Gridview_UserAddedRow);
            this.Payments_Gridview.UserDeletedRow += new DataGridViewRowEventHandler(this.Payments_Gridview_UserDeletedRow);
            this.Payments_Gridview.KeyDown += new KeyEventHandler(this.Payments_Gridview_KeyDown);
            this.Column1.FillWeight = 60f;
            this.Column1.HeaderText = "PayMode";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.HeaderText = "Amount";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column3.HeaderText = "Refference";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column4.HeaderText = "SecRefference";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.deleteToolStripMenuItem };
            this.ContextMenuStrip_paymentsGridview.Items.AddRange(toolStripItems);
            this.ContextMenuStrip_paymentsGridview.Name = "ContextMenuStrip_paymentsGridview";
            this.ContextMenuStrip_paymentsGridview.RenderMode = ToolStripRenderMode.Professional;
            this.ContextMenuStrip_paymentsGridview.Size = new Size(0x6c, 0x1a);
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new Size(0x6b, 0x16);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new EventHandler(this.DeleteToolStripMenuItem_Click);
            this.Btn_Cash.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Cash.Location = new Point(0x13, 0x7f);
            this.Btn_Cash.Name = "Btn_Cash";
            this.Btn_Cash.Size = new Size(130, 0x22);
            this.Btn_Cash.TabIndex = 0;
            this.Btn_Cash.Text = "Cash [F5]";
            this.Btn_Cash.UseVisualStyleBackColor = true;
            this.Btn_Cash.Click += new EventHandler(this.Btn_Cash_Click);
            this.Btn_Close.BackColor = Color.Orange;
            this.Btn_Close.DialogResult = DialogResult.Cancel;
            this.Btn_Close.FlatAppearance.BorderSize = 0;
            this.Btn_Close.FlatStyle = FlatStyle.Flat;
            this.Btn_Close.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Close.Location = new Point(12, 0x1ce);
            this.Btn_Close.Margin = new Padding(4, 5, 4, 5);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0xac, 0x26);
            this.Btn_Close.TabIndex = 3;
            this.Btn_Close.Text = "Close [F2]";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            this.Timer1.Interval = 0x7d0;
            this.Timer1.Tick += new EventHandler(this.Timer1_Tick);
            this.Btn_Mpesa.BackColor = Color.OliveDrab;
            this.Btn_Mpesa.FlatStyle = FlatStyle.Flat;
            this.Btn_Mpesa.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Mpesa.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Mpesa.Location = new Point(0xa7, 0x7f);
            this.Btn_Mpesa.Name = "Btn_Mpesa";
            this.Btn_Mpesa.Size = new Size(130, 0x22);
            this.Btn_Mpesa.TabIndex = 1;
            this.Btn_Mpesa.Text = "Mpesa [F8]";
            this.Btn_Mpesa.UseVisualStyleBackColor = false;
            this.Btn_Mpesa.Click += new EventHandler(this.Btn_Mpesa_Click);
            this.Btn_Card.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Card.Location = new Point(0x13b, 0x7f);
            this.Btn_Card.Name = "Btn_Card";
            this.Btn_Card.Size = new Size(130, 0x22);
            this.Btn_Card.TabIndex = 0x1c;
            this.Btn_Card.Text = "Card [F9]";
            this.Btn_Card.UseVisualStyleBackColor = true;
            this.Btn_Card.Click += new EventHandler(this.Btn_Card_Click);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(5, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x66, 20);
            this.label1.TabIndex = 0x1d;
            this.label1.Text = "Phone No : ";
            this.TextBox_CustomerPhone.Font = new Font("Microsoft Sans Serif", 20f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.TextBox_CustomerPhone.Location = new Point(0x71, 10);
            this.TextBox_CustomerPhone.Name = "TextBox_CustomerPhone";
            this.TextBox_CustomerPhone.Size = new Size(0x12a, 0x26);
            this.TextBox_CustomerPhone.TabIndex = 30;
            this.TextBox_CustomerPhone.TextAlign = HorizontalAlignment.Center;
            this.TextBox_CustomerPhone.WordWrap = false;
            this.label2.BorderStyle = BorderStyle.FixedSingle;
            this.label2.Location = new Point(4, 0x76);
            this.label2.Name = "label2";
            this.label2.Size = new Size(460, 2);
            this.label2.TabIndex = 0x1f;
            this.textBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(14, 0x54);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x90, 0x1a);
            this.textBox2.TabIndex = 0x20;
            this.textBox2.WordWrap = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(14, 0x3d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4f, 20);
            this.label3.TabIndex = 0x21;
            this.label3.Text = "Phone No";
            this.Btn_SearchCustomer.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_SearchCustomer.Location = new Point(0x1a1, 12);
            this.Btn_SearchCustomer.Name = "Btn_SearchCustomer";
            this.Btn_SearchCustomer.Size = new Size(0x2f, 0x22);
            this.Btn_SearchCustomer.TabIndex = 0x22;
            this.Btn_SearchCustomer.Text = "Go";
            this.Btn_SearchCustomer.UseVisualStyleBackColor = true;
            this.Btn_SearchCustomer.Click += new EventHandler(this.Btn_SearchCustomer_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xb5, 0x3d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(80, 20);
            this.label4.TabIndex = 0x24;
            this.label4.Text = "Full Name";
            this.textBox3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0xb5, 0x54);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(160, 0x1a);
            this.textBox3.TabIndex = 0x23;
            this.textBox3.WordWrap = false;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x164, 0x3d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3f, 20);
            this.label5.TabIndex = 0x26;
            this.label5.Text = "Gender";
            this.textBox4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(0x164, 0x54);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0x59, 0x1a);
            this.textBox4.TabIndex = 0x25;
            this.textBox4.WordWrap = false;
            base.AcceptButton = this.Btn_CompleteTransaction;
            base.AutoScaleDimensions = new SizeF(9f, 20f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(0xff, 0xff, 0xc0);
            base.CancelButton = this.Btn_Close;
            base.ClientSize = new Size(0x1d5, 0x1ff);
            base.ControlBox = false;
            base.Controls.Add(this.label5);
            base.Controls.Add(this.textBox4);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.Btn_SearchCustomer);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.TextBox_CustomerPhone);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.Btn_Card);
            base.Controls.Add(this.Btn_Mpesa);
            base.Controls.Add(this.Btn_Close);
            base.Controls.Add(this.Btn_Cash);
            base.Controls.Add(this.Payments_Gridview);
            base.Controls.Add(this.Txt_Balance);
            base.Controls.Add(this.Txt_AmountPaidTotal);
            base.Controls.Add(this.label13);
            base.Controls.Add(this.label12);
            base.Controls.Add(this.Btn_CompleteTransaction);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.KeyPreview = true;
            base.Margin = new Padding(4, 5, 4, 5);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Transactions";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Complete Transaction";
            base.TopMost = true;
            base.Load += new EventHandler(this.Transactions_Load);
            base.Enter += new EventHandler(this.Transactions_Enter);
            base.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            ((ISupportInitialize) this.Payments_Gridview).EndInit();
            this.ContextMenuStrip_paymentsGridview.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void InputAmountPaid(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(((e.KeyChar.ToString() == ".") || char.IsControl(e.KeyChar)) || char.IsNumber(e.KeyChar));
        }

        private void Payments_Gridview_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Payments_Gridview.Rows.Count <= 0)
            {
                MessageBox.Show("You have no item to delete !!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.Payments_Gridview.Rows.Remove(this.Payments_Gridview.CurrentRow);
            }
        }

        private void Payments_Gridview_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.FindTotal();
        }

        private void Payments_Gridview_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.FindTotal();
        }

        private void Payments_Gridview_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.FindTotal();
        }

        private void Payments_Gridview_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.FindTotal();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.CheckTrials >= 3)
            {
                this.Timer1.Stop();
                MessageBox.Show("No Payment(s) have been Received!", "MPESA CONFIRMATION MESSAGE", MessageBoxButtons.OK);
            }
            else if (this.CheckMpesaMessage() == 1)
            {
                this.Timer1.Stop();
            }
            else
            {
                this.CheckTrials++;
            }
        }

        private void Transactions_Enter(object sender, EventArgs e)
        {
            while (base.Visible)
            {
            }
        }

        private void Transactions_Load(object sender, EventArgs e)
        {
        }

        private void Txt_AmountPaidTotal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal num3 = Convert.ToDecimal(this.Txt_AmountPaidTotal.Text) - GrossAmount;
                this.Txt_Balance.Text = num3.ToString("N2");
                if (num3 > 0M)
                {
                    base.AcceptButton = this.Btn_CompleteTransaction;
                }
            }
            catch
            {
            }
        }

        private void Txt_PhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar));
        }
    }
}

