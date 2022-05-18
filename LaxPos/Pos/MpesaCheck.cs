namespace LaxPos.Pos
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MpesaCheck : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public string TCode = "";
        public string TPhone = "";
        public string Tdate = "";
        public string Ttime = "";
        public string Tname = "";
        public double TAmount = 0.0;
        private IContainer components = null;
        private Button Btn_Check;
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private Button Btn_Close;

        public MpesaCheck(string IdPhone)
        {
            this.InitializeComponent();
            this.textBox1.Text = IdPhone;
        }

        private void Btn_Check_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.textBox2.Text == "") || (this.textBox2.Text.Length != 10))
                {
                    MessageBox.Show("Enter Valid The MPESA TRANSACTION CODE", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("select a.Transcode,a.Phone,a.Amount,a.Status,a.Date,a.Time,a.Name from `p.o.s`.mpesatrans a where a.Phone=@phone and a.Transcode=@code and a.Status=0;", connection);
                    command.Parameters.AddWithValue("@phone", this.textBox1.Text);
                    command.Parameters.AddWithValue("@code", this.textBox2.Text);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("The Transaction Code does not exist!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        base.DialogResult = DialogResult.No;
                    }
                    else
                    {
                        while (true)
                        {
                            if (reader.Read())
                            {
                                double num;
                                if (double.TryParse(reader["Amount"].ToString(), out num))
                                {
                                    this.TAmount = num;
                                    this.TCode = reader["Transcode"].ToString();
                                    this.TPhone = reader["Phone"].ToString();
                                    this.Tname = reader["Name"].ToString();
                                    base.DialogResult = DialogResult.OK;
                                    continue;
                                }
                                MessageBox.Show("Unknown Value Paid", "MpesaMessage", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                base.DialogResult = DialogResult.No;
                            }
                            break;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Cancel;
            base.Close();
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
            this.Btn_Check = new Button();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.Btn_Close = new Button();
            base.SuspendLayout();
            this.Btn_Check.Location = new Point(0xb9, 0x6d);
            this.Btn_Check.Name = "Btn_Check";
            this.Btn_Check.Size = new Size(100, 0x22);
            this.Btn_Check.TabIndex = 0;
            this.Btn_Check.Text = "CheckOut";
            this.Btn_Check.UseVisualStyleBackColor = true;
            this.Btn_Check.Click += new EventHandler(this.Btn_Check_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x56, 0x16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Phone No :";
            this.textBox1.Location = new Point(0x71, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0xc5, 0x1d);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.textBox2.Location = new Point(0x71, 0x35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0xc5, 0x1d);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x38);
            this.label2.Name = "label2";
            this.label2.Size = new Size(100, 0x16);
            this.label2.TabIndex = 3;
            this.label2.Text = "MPESACode";
            this.Btn_Close.DialogResult = DialogResult.Cancel;
            this.Btn_Close.Location = new Point(0x34, 0x6d);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(100, 0x22);
            this.Btn_Close.TabIndex = 5;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            base.AcceptButton = this.Btn_Check;
            base.AutoScaleDimensions = new SizeF(9f, 22f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            base.CancelButton = this.Btn_Close;
            base.ClientSize = new Size(0x142, 0x9b);
            base.Controls.Add(this.Btn_Close);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.Btn_Check);
            this.Font = new Font("Palatino Linotype", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Margin = new Padding(4, 6, 4, 6);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MpesaCheck";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "MpesaCheck";
            base.TopMost = true;
            base.Load += new EventHandler(this.MpesaCheck_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void MpesaCheck_Load(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                base.Close();
            }
            this.textBox2.Focus();
        }
    }
}

