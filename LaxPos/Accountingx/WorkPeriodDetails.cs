namespace LaxPos.Accounting
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WorkPeriodDetails : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox10;
        private TextBox textBox7;
        private Label label10;
        private Label label7;
        private TextBox textBox11;
        private Label label11;
        private TextBox textBox8;
        private TextBox textBox12;
        private Label label8;
        private Label label12;
        private TextBox textBox9;
        private Label label9;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox6;
        private Label label6;
        private GroupBox groupBox3;
        private TextBox textBox13;
        private TextBox textBox14;
        private Label label13;
        private Label label14;
        private TextBox textBox15;
        private Label label15;
        private TextBox textBox16;
        private TextBox textBox17;
        private Label label16;
        private Label label17;
        private TextBox textBox18;
        private Label label18;
        private TextBox textBox19;
        private Label label19;
        private TextBox textBox20;
        private Label label20;
        private TextBox textBox21;
        private Label label21;
        private Button Btn_Close;
        public TextBox textBox1;

        public WorkPeriodDetails()
        {
            this.InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
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

        private void GetFulldetails()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("select * from workperiods where workingdate=@workingdate", connection);
                command.Parameters.AddWithValue("@workingdate", this.textBox1.Text);
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The work period details does not exist!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox6.Text = reader.GetDateTime("workingdate").ToShortDateString();
                        this.textBox5.Text = reader.GetDateTime("workingdate").ToShortTimeString();
                        this.textBox9.Text = reader.GetString("openedby");
                        this.textBox8.Text = reader.GetString("openingcounter");
                        this.textBox7.Text = reader.GetString("openingbranch");
                        this.textBox12.Text = reader.GetString("openingcash");
                        this.textBox11.Text = reader.GetString("openingmpesa");
                        this.textBox10.Text = reader.GetString("openingcards");
                        this.textBox21.Text = reader.GetDateTime("closingtime").ToShortDateString();
                        this.textBox20.Text = reader.GetDateTime("closingtime").ToShortTimeString();
                        this.textBox18.Text = reader.GetString("closedby");
                        this.textBox17.Text = reader.GetString("closingcash");
                        this.textBox16.Text = reader.GetString("closingcounter");
                        this.textBox15.Text = reader.GetString("closingmpesa");
                        this.textBox14.Text = reader.GetString("closingbranch");
                        this.textBox13.Text = reader.GetString("closingcards");
                        this.SumOpeningBalances();
                        this.SumClosingBalances();
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.textBox10 = new TextBox();
            this.textBox7 = new TextBox();
            this.label10 = new Label();
            this.label7 = new Label();
            this.textBox11 = new TextBox();
            this.label11 = new Label();
            this.textBox8 = new TextBox();
            this.textBox12 = new TextBox();
            this.label8 = new Label();
            this.label12 = new Label();
            this.textBox9 = new TextBox();
            this.label9 = new Label();
            this.textBox4 = new TextBox();
            this.label4 = new Label();
            this.textBox5 = new TextBox();
            this.label5 = new Label();
            this.textBox6 = new TextBox();
            this.label6 = new Label();
            this.groupBox3 = new GroupBox();
            this.textBox13 = new TextBox();
            this.textBox14 = new TextBox();
            this.label13 = new Label();
            this.label14 = new Label();
            this.textBox15 = new TextBox();
            this.label15 = new Label();
            this.textBox16 = new TextBox();
            this.textBox17 = new TextBox();
            this.label16 = new Label();
            this.label17 = new Label();
            this.textBox18 = new TextBox();
            this.label18 = new Label();
            this.textBox19 = new TextBox();
            this.label19 = new Label();
            this.textBox20 = new TextBox();
            this.label20 = new Label();
            this.textBox21 = new TextBox();
            this.label21 = new Label();
            this.Btn_Close = new Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x202, 0x40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Period Summary";
            this.textBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0x155, 0x17);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x9c, 0x1a);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x106, 0x17);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x49, 0x12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Expenses";
            this.textBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(110, 0x17);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x80, 0x1a);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5c, 0x12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Work Period";
            this.groupBox2.Controls.Add(this.textBox10);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBox11);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Controls.Add(this.textBox12);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = DockStyle.Top;
            this.groupBox2.Location = new Point(0, 0x40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x202, 0xbf);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opening Info";
            this.textBox10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox10.Location = new Point(0x15d, 0x9e);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new Size(0x94, 0x1a);
            this.textBox10.TabIndex = 0x11;
            this.textBox10.TextAlign = HorizontalAlignment.Center;
            this.textBox7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox7.Location = new Point(0x15d, 0x65);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new Size(0x94, 0x1a);
            this.textBox7.TabIndex = 0x11;
            this.textBox7.TextAlign = HorizontalAlignment.Center;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x15d, 0x89);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x30, 0x12);
            this.label10.TabIndex = 0x10;
            this.label10.Text = "Cards";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x15d, 80);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x37, 0x12);
            this.label7.TabIndex = 0x10;
            this.label7.Text = "Branch";
            this.textBox11.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox11.Location = new Point(0xb6, 0x9e);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new Size(0x80, 0x1a);
            this.textBox11.TabIndex = 15;
            this.textBox11.TextAlign = HorizontalAlignment.Center;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0xb6, 0x89);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x35, 0x12);
            this.label11.TabIndex = 14;
            this.label11.Text = "Mpesa";
            this.textBox8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox8.Location = new Point(0xb6, 0x65);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new Size(0x80, 0x1a);
            this.textBox8.TabIndex = 15;
            this.textBox8.TextAlign = HorizontalAlignment.Center;
            this.textBox12.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox12.Location = new Point(15, 0x9e);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new Size(0x80, 0x1a);
            this.textBox12.TabIndex = 13;
            this.textBox12.TextAlign = HorizontalAlignment.Center;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xb6, 80);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x3d, 0x12);
            this.label8.TabIndex = 14;
            this.label8.Text = "Counter";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(15, 0x89);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x2b, 0x12);
            this.label12.TabIndex = 12;
            this.label12.Text = "Cash";
            this.textBox9.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox9.Location = new Point(15, 0x65);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new Size(0x80, 0x1a);
            this.textBox9.TabIndex = 13;
            this.textBox9.TextAlign = HorizontalAlignment.Center;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(15, 80);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x4d, 0x12);
            this.label9.TabIndex = 12;
            this.label9.Text = "OpenedBy";
            this.textBox4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(0x15d, 0x30);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0x94, 0x1a);
            this.textBox4.TabIndex = 11;
            this.textBox4.TextAlign = HorizontalAlignment.Center;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x15d, 0x1b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(120, 0x12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Opening Balance";
            this.textBox5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox5.Location = new Point(0xb6, 0x30);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0x80, 0x1a);
            this.textBox5.TabIndex = 9;
            this.textBox5.TextAlign = HorizontalAlignment.Center;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xb6, 0x1b);
            this.label5.Name = "label5";
            this.label5.Size = new Size(100, 0x12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Opening Time";
            this.textBox6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox6.Location = new Point(15, 0x30);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(0x80, 0x1a);
            this.textBox6.TabIndex = 7;
            this.textBox6.TextAlign = HorizontalAlignment.Center;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(15, 0x1b);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x62, 0x12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Opening Date";
            this.groupBox3.Controls.Add(this.textBox13);
            this.groupBox3.Controls.Add(this.textBox14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textBox15);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.textBox16);
            this.groupBox3.Controls.Add(this.textBox17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.textBox18);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.textBox19);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.textBox20);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.textBox21);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Dock = DockStyle.Top;
            this.groupBox3.Location = new Point(0, 0xff);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x202, 190);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Closing Info";
            this.textBox13.BackColor = SystemColors.Control;
            this.textBox13.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox13.Location = new Point(0x15d, 0x9e);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new Size(0x94, 0x1a);
            this.textBox13.TabIndex = 0x11;
            this.textBox13.TextAlign = HorizontalAlignment.Center;
            this.textBox14.BackColor = SystemColors.Control;
            this.textBox14.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox14.Location = new Point(0x15d, 0x65);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new Size(0x94, 0x1a);
            this.textBox14.TabIndex = 0x11;
            this.textBox14.TextAlign = HorizontalAlignment.Center;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x15d, 0x89);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x30, 0x12);
            this.label13.TabIndex = 0x10;
            this.label13.Text = "Cards";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x15d, 80);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x37, 0x12);
            this.label14.TabIndex = 0x10;
            this.label14.Text = "Branch";
            this.textBox15.BackColor = SystemColors.Control;
            this.textBox15.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox15.Location = new Point(0xb6, 0x9e);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new Size(0x80, 0x1a);
            this.textBox15.TabIndex = 15;
            this.textBox15.TextAlign = HorizontalAlignment.Center;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0xb6, 0x89);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x35, 0x12);
            this.label15.TabIndex = 14;
            this.label15.Text = "Mpesa";
            this.textBox16.BackColor = SystemColors.Control;
            this.textBox16.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox16.Location = new Point(0xb6, 0x65);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new Size(0x80, 0x1a);
            this.textBox16.TabIndex = 15;
            this.textBox16.TextAlign = HorizontalAlignment.Center;
            this.textBox17.BackColor = SystemColors.Control;
            this.textBox17.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox17.Location = new Point(15, 0x9e);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new Size(0x80, 0x1a);
            this.textBox17.TabIndex = 13;
            this.textBox17.TextAlign = HorizontalAlignment.Center;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0xb6, 80);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x3d, 0x12);
            this.label16.TabIndex = 14;
            this.label16.Text = "Counter";
            this.label17.AutoSize = true;
            this.label17.Location = new Point(15, 0x89);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x2b, 0x12);
            this.label17.TabIndex = 12;
            this.label17.Text = "Cash";
            this.textBox18.BackColor = SystemColors.Control;
            this.textBox18.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox18.Location = new Point(15, 0x65);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new Size(0x80, 0x1a);
            this.textBox18.TabIndex = 13;
            this.textBox18.TextAlign = HorizontalAlignment.Center;
            this.label18.AutoSize = true;
            this.label18.Location = new Point(15, 80);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x48, 0x12);
            this.label18.TabIndex = 12;
            this.label18.Text = "ClosedBy";
            this.textBox19.BackColor = SystemColors.Control;
            this.textBox19.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox19.Location = new Point(0x15d, 0x30);
            this.textBox19.Name = "textBox19";
            this.textBox19.ReadOnly = true;
            this.textBox19.Size = new Size(0x94, 0x1a);
            this.textBox19.TabIndex = 11;
            this.textBox19.TextAlign = HorizontalAlignment.Center;
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x15d, 0x1b);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x73, 0x12);
            this.label19.TabIndex = 10;
            this.label19.Text = "Closing Balance";
            this.textBox20.BackColor = SystemColors.Control;
            this.textBox20.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox20.Location = new Point(0xb6, 0x30);
            this.textBox20.Name = "textBox20";
            this.textBox20.ReadOnly = true;
            this.textBox20.Size = new Size(0x80, 0x1a);
            this.textBox20.TabIndex = 9;
            this.textBox20.TextAlign = HorizontalAlignment.Center;
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0xb6, 0x1b);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x5f, 0x12);
            this.label20.TabIndex = 8;
            this.label20.Text = "Closing Time";
            this.textBox21.BackColor = SystemColors.Control;
            this.textBox21.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox21.Location = new Point(15, 0x30);
            this.textBox21.Name = "textBox21";
            this.textBox21.ReadOnly = true;
            this.textBox21.Size = new Size(0x80, 0x1a);
            this.textBox21.TabIndex = 7;
            this.textBox21.TextAlign = HorizontalAlignment.Center;
            this.label21.AutoSize = true;
            this.label21.Location = new Point(15, 0x1b);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x5d, 0x12);
            this.label21.TabIndex = 6;
            this.label21.Text = "Closing Date";
            this.Btn_Close.Location = new Point(0x185, 0x1c3);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x6c, 0x1d);
            this.Btn_Close.TabIndex = 3;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            base.AcceptButton = this.Btn_Close;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x202, 0x1e6);
            base.Controls.Add(this.Btn_Close);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "WorkPeriodDetails";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Work Period Details";
            base.Load += new EventHandler(this.WorkPeriodDetails_Load);
            base.Shown += new EventHandler(this.WorkPeriodDetails_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

        private void SumClosingBalances()
        {
            try
            {
                double num = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                if (this.textBox17.Text != "")
                {
                    num = double.Parse(this.textBox17.Text);
                }
                if (this.textBox15.Text != "")
                {
                    num = double.Parse(this.textBox15.Text);
                }
                if (this.textBox13.Text != "")
                {
                    num3 = double.Parse(this.textBox13.Text);
                }
                this.textBox19.Text = ((num + num3) + num2).ToString();
            }
            catch (Exception exception)
            {
                this.textBox19.Text = "";
                MessageBox.Show(exception.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void SumOpeningBalances()
        {
            try
            {
                double num = 0.0;
                double num2 = 0.0;
                double num3 = 0.0;
                if (this.textBox12.Text != "")
                {
                    num = double.Parse(this.textBox12.Text);
                }
                if (this.textBox11.Text != "")
                {
                    num3 = double.Parse(this.textBox11.Text);
                }
                if (this.textBox10.Text != "")
                {
                    num2 = double.Parse(this.textBox10.Text);
                }
                this.textBox4.Text = ((num + num3) + num2).ToString();
            }
            catch (Exception exception)
            {
                this.textBox4.Text = "";
                MessageBox.Show(exception.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void WorkPeriodDetails_Load(object sender, EventArgs e)
        {
        }

        private void WorkPeriodDetails_Shown(object sender, EventArgs e)
        {
            this.GetFulldetails();
            this.Btn_Close.Focus();
        }
    }
}

