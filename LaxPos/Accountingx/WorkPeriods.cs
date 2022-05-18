namespace LaxPos.Accounting
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class WorkPeriods : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private TabControl tabControl1;
        private TabPage TabPage1;
        private TabPage tabPage2;
        private TextBox textBox20;
        private Label label21;
        private Button Btn_CloseWorkPeriod;
        private TextBox textBox7;
        private Label label7;
        private TextBox textBox13;
        private Label label13;
        private TextBox textBox14;
        private Label label14;
        private GroupBox groupBox3;
        private CheckBox CheckBox_UsePreviousBalance;
        private Button Btn_CreateWorkPeriod;
        private TextBox textBox8;
        private Label label8;
        private TextBox textBox9;
        private Label label9;
        private TextBox textBox10;
        private Label label10;
        private TextBox textBox11;
        private Label label11;
        private TextBox textBox12;
        private Label label12;
        private GroupBox groupBox1;
        private TextBox textBox6;
        private Label label6;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox4;
        private Label label4;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox1;
        private Label label1;
        private GroupBox groupBox4;
        private TextBox textBox23;
        private Label label23;
        private GroupBox groupBox5;
        private TextBox textBox24;
        private Label label24;
        private TextBox textBox22;
        private Label label22;
        private TextBox textBox21;
        private Label label17;
        private Label label16;
        private TextBox textBox16;
        private TextBox textBox17;
        private Label label15;
        private Label label18;
        private TextBox textBox15;
        private TextBox textBox18;
        private Label label20;
        private Label label19;
        private TextBox textBox19;
        private Button Btn_UpdateWorkPeriod;

        public WorkPeriods()
        {
            this.InitializeComponent();
        }

        private void Btn_Autocomplete_Click(object sender, EventArgs e)
        {
            this.Btn_CloseWorkPeriod_Click(this.Btn_UpdateWorkPeriod, new EventArgs());
        }

        private void Btn_CloseWorkPeriod_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox16.Text == "")
                {
                    MessageBox.Show("The current work period is unknown!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    int num2;
                    if (!int.TryParse(this.textBox7.Text, out num2))
                    {
                        MessageBox.Show("The Cards' Amount cannot be validated!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.textBox7.Focus();
                    }
                    else if (!int.TryParse(this.textBox13.Text, out num2))
                    {
                        MessageBox.Show("The Mpesa Amount cannot be validated!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.textBox13.Focus();
                    }
                    else if (!int.TryParse(this.textBox14.Text, out num2))
                    {
                        MessageBox.Show("The Cash Amount cannot be validated!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.textBox14.Focus();
                    }
                    else
                    {
                        MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                        connection.Open();
                        MySqlCommand command = new MySqlCommand("update workperiods set workingstatus=@workingstatus, closingcash=@closingcash, closingmpesa=@closingmpesa, closingcards=@closingcards, closingtime=@closingtime, closedby=@closedby, closingcounter=@closingcounter, closingbranch=@closingbranch where workingdate=@currworkingdate and workingstatus=@currworkingstatus", connection);
                        command.Parameters.AddWithValue("@workingstatus", "closed");
                        command.Parameters.AddWithValue("@closingcash", int.Parse(this.textBox14.Text));
                        command.Parameters.AddWithValue("@closingmpesa", int.Parse(this.textBox13.Text));
                        command.Parameters.AddWithValue("@closingcards", int.Parse(this.textBox7.Text));
                        command.Parameters.AddWithValue("@closingtime", Program.CurrentDateTime());
                        command.Parameters.AddWithValue("@closedby", Program.CurrLoggedInUser.UserID);
                        command.Parameters.AddWithValue("@closingcounter", Program.LogInCounter);
                        command.Parameters.AddWithValue("@closingbranch", Program.LogInBranch);
                        command.Parameters.AddWithValue("@currworkingdate", this.textBox16.Text);
                        command.Parameters.AddWithValue("@currworkingstatus", "open");
                        if (command.ExecuteNonQuery() <= 0)
                        {
                            MessageBox.Show("The unclosed period does not exist!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("The period has been successfully closed!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.CheckOpenworkperiod();
                            this.ResetClosingperiod();
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_CreateWorkPeriod_Click(object sender, EventArgs e)
        {
            if (this.textBox12.Text.Trim() == "")
            {
                MessageBox.Show("Work Period Cannot be empty!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.textBox12.Focus();
            }
            else
            {
                int num;
                if (!int.TryParse(this.textBox8.Text, out num))
                {
                    MessageBox.Show("The Cards' Amount cannot be validated!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.textBox8.Focus();
                }
                else if (!int.TryParse(this.textBox9.Text, out num))
                {
                    MessageBox.Show("The Mpesa Amount cannot be validated!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.textBox9.Focus();
                }
                else if (!int.TryParse(this.textBox10.Text, out num))
                {
                    MessageBox.Show("The Cash Amount cannot be validated!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.textBox10.Focus();
                }
                else
                {
                    try
                    {
                        bool flag5 = false;
                        string str = Program.CurrentDateTime().ToString("yyyy-MM-dd");
                        string str2 = "";
                        MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                        connection.Open();
                        MySqlTransaction transaction = connection.BeginTransaction();
                        try
                        {
                            MySqlCommand command = new MySqlCommand("select count(a.workingdate) as totalperiods,max(a.workingdate) as workingdate from workperiods a;", connection);
                            MySqlDataReader reader = command.ExecuteReader();
                            if (!reader.HasRows)
                            {
                                flag5 = true;
                            }
                            else
                            {
                                int num2 = 0;
                                while (true)
                                {
                                    if (!reader.Read())
                                    {
                                        break;
                                    }
                                    num2 = reader.GetInt32("totalperiods");
                                    if (num2 > 0)
                                    {
                                        str2 = reader.GetDateTime("workingdate").ToString("yyyy-MM-dd");
                                        continue;
                                    }
                                    MessageBox.Show("You are creating a work period for the FIRST time....\n\nRemember to END your work period!!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    flag5 = true;
                                    this.ResetCreateworkperiod();
                                }
                            }
                            reader.Close();
                            command.Parameters.Clear();
                            command.Dispose();
                            if ((str2 != "") | flag5)
                            {
                                MySqlCommand command2 = new MySqlCommand("insert into workperiods(workingdate, prevperioddate, openingcash, openingmpesa, openingcards, openingtime, openedby, openingcounter, openingbranch, workingstatus, closingcash, closingmpesa, closingcards, closingtime, closedby, closingcounter, closingbranch) values(@workingdate, @prevperioddate, @openingcash, @openingmpesa, @openingcards, @openingtime, @openedby, @openingcounter, @openingbranch, @workingstatus, @closingcash, @closingmpesa, @closingcards, @closingtime, @closedby, @closingcounter, @closingbranch)", connection, transaction);
                                command2.Parameters.AddWithValue("@workingdate", str);
                                if (flag5)
                                {
                                    command2.Parameters.AddWithValue("@prevperioddate", "0000-00-00");
                                }
                                else
                                {
                                    command2.Parameters.AddWithValue("@prevperioddate", str2);
                                }
                                command2.Parameters.AddWithValue("@openingcash", int.Parse(this.textBox10.Text));
                                command2.Parameters.AddWithValue("@openingmpesa", int.Parse(this.textBox9.Text));
                                command2.Parameters.AddWithValue("@openingcards", int.Parse(this.textBox8.Text));
                                command2.Parameters.AddWithValue("@openingtime", Program.CurrentDateTime());
                                command2.Parameters.AddWithValue("@openedby", Program.CurrLoggedInUser.UserID);
                                command2.Parameters.AddWithValue("@openingcounter", Program.LogInCounter);
                                command2.Parameters.AddWithValue("@openingbranch", Program.LogInBranch);
                                command2.Parameters.AddWithValue("@workingstatus", "open");
                                command2.Parameters.AddWithValue("@closingcash", -1);
                                command2.Parameters.AddWithValue("@closingmpesa", -1);
                                command2.Parameters.AddWithValue("@closingcards", -1);
                                command2.Parameters.AddWithValue("@closingtime", "0001-01-01 00:00:00");
                                command2.Parameters.AddWithValue("@closedby", "");
                                command2.Parameters.AddWithValue("@closingcounter", "");
                                command2.Parameters.AddWithValue("@closingbranch", "");
                                if (command2.ExecuteNonQuery() != 1)
                                {
                                    MessageBox.Show("Failed to create the work period!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    transaction.Rollback();
                                }
                                else
                                {
                                    transaction.Commit();
                                    MessageBox.Show("Work period created successfully.\nRemember to END your work period!!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    this.CheckOpenworkperiod();
                                }
                            }
                        }
                        catch (Exception exception1)
                        {
                            MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            transaction.Rollback();
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    catch (Exception exception3)
                    {
                        MessageBox.Show(exception3.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void CheckBox_UsePreviousBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CheckBox_UsePreviousBalance.Checked)
            {
                this.textBox11.ReadOnly = true;
                this.textBox10.ReadOnly = true;
                this.textBox9.ReadOnly = true;
                this.textBox8.ReadOnly = true;
                this.textBox10.Text = this.textBox3.Text;
                this.textBox9.Text = this.textBox4.Text;
                this.textBox8.Text = this.textBox5.Text;
            }
            else
            {
                this.textBox11.ReadOnly = false;
                this.textBox10.ReadOnly = false;
                this.textBox9.ReadOnly = false;
                this.textBox8.ReadOnly = false;
                this.textBox11.Text = "0";
                this.textBox10.Text = "0";
                this.textBox9.Text = "0";
                this.textBox8.Text = "0";
            }
        }

        private void CheckOpenworkperiod()
        {
            try
            {
                this.Btn_CloseWorkPeriod.Enabled = base.Enabled;
                this.Btn_UpdateWorkPeriod.Enabled = base.Enabled;
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("select * from workperiods where workingstatus=@workingstatus;", connection);
                command.Parameters.AddWithValue("@workingstatus", "open");
                if (command.ExecuteReader().HasRows)
                {
                    this.groupBox3.Enabled = false;
                    this.groupBox4.Enabled = true;
                }
                else
                {
                    this.groupBox3.Enabled = true;
                    this.groupBox4.Enabled = true;
                    this.Btn_CloseWorkPeriod.Enabled = false;
                    this.Btn_UpdateWorkPeriod.Enabled = true;
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void ClosingBalanceAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox) sender).Text == "")
                {
                    ((TextBox) sender).Text = "0";
                    ((TextBox) sender).SelectionStart = 1;
                }
                decimal num2 = decimal.Parse(this.textBox13.Text);
                decimal num3 = decimal.Parse(this.textBox7.Text);
                this.textBox20.Text = ((num3 + decimal.Parse(this.textBox14.Text)) + num2).ToString();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void CurrentOpeningBalanceAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox) sender).Text == "")
                {
                    ((TextBox) sender).Text = "0";
                    ((TextBox) sender).SelectionStart = 1;
                }
                decimal num2 = decimal.Parse(this.textBox18.Text);
                decimal num3 = decimal.Parse(this.textBox17.Text);
                this.textBox15.Text = ((num3 + decimal.Parse(this.textBox19.Text)) + num2).ToString();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

        private void FindCurrentPeriodopeningDetails()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM workperiods where workingstatus=@workingstatus", connection);
                command.Parameters.AddWithValue("@workingstatus", "open");
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The current working period does not exist!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.groupBox4.Enabled = false;
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox19.Text = reader.GetString("openingcash");
                        this.textBox18.Text = reader.GetString("openingmpesa");
                        this.textBox17.Text = reader.GetString("openingcards");
                        this.textBox16.Text = reader.GetDateTime("workingdate").ToString("yyyy-MM-dd");
                        this.textBox21.Text = reader.GetString("openingtime");
                        this.textBox22.Text = reader.GetString("openedby");
                        this.textBox24.Text = reader.GetString("workingstatus");
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.textBox16.Text = "";
            }
        }

        private void FindpreviousPeriodDetails()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM workperiods where workingdate=(SELECT max(workingdate) FROM workperiods)", connection);
                command.Parameters.AddWithValue("@currworkingdate", Program.CurrentDateTime().ToString("yyyy-MM-dd"));
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The previous working period does not exist!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.textBox1.Text = "";
                    this.textBox3.Text = "0";
                    this.textBox4.Text = "0";
                    this.textBox5.Text = "0";
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox1.Text = reader.GetDateTime("workingdate").ToString("yyyy-MM-dd");
                        this.textBox3.Text = reader.GetString("closingcash");
                        this.textBox4.Text = reader.GetString("closingmpesa");
                        this.textBox5.Text = reader.GetString("closingcards");
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new TabControl();
            this.TabPage1 = new TabPage();
            this.groupBox3 = new GroupBox();
            this.CheckBox_UsePreviousBalance = new CheckBox();
            this.Btn_CreateWorkPeriod = new Button();
            this.textBox8 = new TextBox();
            this.label8 = new Label();
            this.textBox9 = new TextBox();
            this.label9 = new Label();
            this.textBox10 = new TextBox();
            this.label10 = new Label();
            this.textBox11 = new TextBox();
            this.label11 = new Label();
            this.textBox12 = new TextBox();
            this.label12 = new Label();
            this.groupBox1 = new GroupBox();
            this.textBox6 = new TextBox();
            this.label6 = new Label();
            this.textBox5 = new TextBox();
            this.label5 = new Label();
            this.textBox4 = new TextBox();
            this.label4 = new Label();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.tabPage2 = new TabPage();
            this.groupBox4 = new GroupBox();
            this.Btn_UpdateWorkPeriod = new Button();
            this.textBox23 = new TextBox();
            this.label23 = new Label();
            this.label14 = new Label();
            this.textBox20 = new TextBox();
            this.textBox14 = new TextBox();
            this.label21 = new Label();
            this.label13 = new Label();
            this.textBox13 = new TextBox();
            this.label7 = new Label();
            this.textBox7 = new TextBox();
            this.Btn_CloseWorkPeriod = new Button();
            this.groupBox5 = new GroupBox();
            this.textBox24 = new TextBox();
            this.label24 = new Label();
            this.textBox22 = new TextBox();
            this.label22 = new Label();
            this.textBox21 = new TextBox();
            this.label17 = new Label();
            this.label16 = new Label();
            this.textBox16 = new TextBox();
            this.textBox17 = new TextBox();
            this.label15 = new Label();
            this.label18 = new Label();
            this.textBox15 = new TextBox();
            this.textBox18 = new TextBox();
            this.label20 = new Label();
            this.label19 = new Label();
            this.textBox19 = new TextBox();
            this.tabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.TabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x3e1, 450);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.TabControl1_SelectedIndexChanged);
            this.tabControl1.Selected += new TabControlEventHandler(this.TabControl1_Selected);
            this.TabPage1.Controls.Add(this.groupBox3);
            this.TabPage1.Controls.Add(this.groupBox1);
            this.TabPage1.Location = new Point(4, 0x1d);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new Padding(3);
            this.TabPage1.Size = new Size(0x3d9, 0x1a1);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Create Work Periods";
            this.TabPage1.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.CheckBox_UsePreviousBalance);
            this.groupBox3.Controls.Add(this.Btn_CreateWorkPeriod);
            this.groupBox3.Controls.Add(this.textBox8);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox9);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBox11);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.textBox12);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Location = new Point(150, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(800, 0xd8);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Work Period Openings";
            this.CheckBox_UsePreviousBalance.AutoSize = true;
            this.CheckBox_UsePreviousBalance.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.CheckBox_UsePreviousBalance.Location = new Point(0x169, 0x1d);
            this.CheckBox_UsePreviousBalance.Name = "CheckBox_UsePreviousBalance";
            this.CheckBox_UsePreviousBalance.Size = new Size(0xad, 0x16);
            this.CheckBox_UsePreviousBalance.TabIndex = 11;
            this.CheckBox_UsePreviousBalance.Text = "Use Previous Balance";
            this.CheckBox_UsePreviousBalance.UseVisualStyleBackColor = true;
            this.CheckBox_UsePreviousBalance.CheckedChanged += new EventHandler(this.CheckBox_UsePreviousBalance_CheckedChanged);
            this.Btn_CreateWorkPeriod.Location = new Point(0x14d, 0x97);
            this.Btn_CreateWorkPeriod.Name = "Btn_CreateWorkPeriod";
            this.Btn_CreateWorkPeriod.Size = new Size(0xaf, 0x1c);
            this.Btn_CreateWorkPeriod.TabIndex = 0;
            this.Btn_CreateWorkPeriod.Text = "Create Work Period";
            this.Btn_CreateWorkPeriod.UseVisualStyleBackColor = true;
            this.Btn_CreateWorkPeriod.Click += new EventHandler(this.Btn_CreateWorkPeriod_Click);
            this.textBox8.BackColor = SystemColors.Info;
            this.textBox8.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox8.Location = new Point(0x19d, 0x5b);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Size(0xaf, 30);
            this.textBox8.TabIndex = 10;
            this.textBox8.Text = "0";
            this.textBox8.TextAlign = HorizontalAlignment.Center;
            this.textBox8.TextChanged += new EventHandler(this.OpeningBalanceAmount_TextChanged);
            this.label8.AutoSize = true;
            this.label8.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label8.Location = new Point(0x1be, 70);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x30, 0x12);
            this.label8.TabIndex = 9;
            this.label8.Text = "Cards";
            this.textBox9.BackColor = SystemColors.Info;
            this.textBox9.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox9.Location = new Point(0xe4, 0x5b);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Size(0xb2, 30);
            this.textBox9.TabIndex = 8;
            this.textBox9.Text = "0";
            this.textBox9.TextAlign = HorizontalAlignment.Center;
            this.textBox9.TextChanged += new EventHandler(this.OpeningBalanceAmount_TextChanged);
            this.label9.AutoSize = true;
            this.label9.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label9.Location = new Point(0xe4, 70);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x35, 0x12);
            this.label9.TabIndex = 7;
            this.label9.Text = "Mpesa";
            this.textBox10.BackColor = SystemColors.Info;
            this.textBox10.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox10.Location = new Point(0x1d, 0x5b);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Size(0xbf, 30);
            this.textBox10.TabIndex = 6;
            this.textBox10.Text = "0";
            this.textBox10.TextAlign = HorizontalAlignment.Center;
            this.textBox10.TextChanged += new EventHandler(this.OpeningBalanceAmount_TextChanged);
            this.label10.AutoSize = true;
            this.label10.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label10.Location = new Point(0x1d, 70);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x2b, 0x12);
            this.label10.TabIndex = 5;
            this.label10.Text = "Cash";
            this.textBox11.BackColor = SystemColors.Control;
            this.textBox11.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox11.Location = new Point(0x1d, 0x99);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new Size(0xbf, 30);
            this.textBox11.TabIndex = 4;
            this.textBox11.Text = "0";
            this.textBox11.TextAlign = HorizontalAlignment.Center;
            this.label11.AutoSize = true;
            this.label11.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label11.Location = new Point(0x1d, 130);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x9d, 0x12);
            this.label11.TabIndex = 3;
            this.label11.Text = "Total Opening Balance";
            this.textBox12.Location = new Point(0x89, 0x1d);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new Size(0xc4, 0x1a);
            this.textBox12.TabIndex = 2;
            this.textBox12.TextAlign = HorizontalAlignment.Center;
            this.label12.AutoSize = true;
            this.label12.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label12.Location = new Point(0x1d, 0x21);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x5c, 0x12);
            this.label12.TabIndex = 1;
            this.label12.Text = "Work Period";
            this.groupBox1.BackColor = Color.Ivory;
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(150, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(800, 150);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Previous Work Period";
            this.textBox6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox6.Location = new Point(380, 0x2d);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(0x9f, 0x1a);
            this.textBox6.TabIndex = 12;
            this.textBox6.Text = "0";
            this.textBox6.TextAlign = HorizontalAlignment.Center;
            this.label6.AutoSize = true;
            this.label6.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label6.Location = new Point(380, 0x16);
            this.label6.Name = "label6";
            this.label6.Size = new Size(110, 0x12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Total Expenses";
            this.textBox5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox5.Location = new Point(380, 0x66);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(0x9f, 0x1a);
            this.textBox5.TabIndex = 10;
            this.textBox5.Text = "0";
            this.textBox5.TextAlign = HorizontalAlignment.Center;
            this.textBox5.TextChanged += new EventHandler(this.PreviousBalances_TextChanged);
            this.label5.AutoSize = true;
            this.label5.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label5.Location = new Point(380, 0x51);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x30, 0x12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cards";
            this.textBox4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(210, 0x66);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0x8e, 0x1a);
            this.textBox4.TabIndex = 8;
            this.textBox4.Text = "0";
            this.textBox4.TextAlign = HorizontalAlignment.Center;
            this.textBox4.TextChanged += new EventHandler(this.PreviousBalances_TextChanged);
            this.label4.AutoSize = true;
            this.label4.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label4.Location = new Point(210, 0x51);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x35, 0x12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Mpesa";
            this.textBox3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0x30, 0x66);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(0x8e, 0x1a);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "0";
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            this.textBox3.TextChanged += new EventHandler(this.PreviousBalances_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label3.Location = new Point(0x30, 0x51);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2b, 0x12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cash";
            this.textBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(210, 0x2d);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x8e, 0x1a);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "0";
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label2.Location = new Point(210, 0x16);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x73, 0x12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Closing Balance";
            this.textBox1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0x30, 0x2d);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x8e, 0x1a);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label1.Location = new Point(0x30, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5c, 0x12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Work Period";
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new Point(4, 0x1d);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x3d9, 0x1a1);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Close WorkPeriods";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.groupBox4.Controls.Add(this.Btn_UpdateWorkPeriod);
            this.groupBox4.Controls.Add(this.textBox23);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.textBox20);
            this.groupBox4.Controls.Add(this.textBox14);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.textBox13);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.textBox7);
            this.groupBox4.Controls.Add(this.Btn_CloseWorkPeriod);
            this.groupBox4.Location = new Point(150, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(800, 0x9c);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Closing Details";
            this.Btn_UpdateWorkPeriod.Location = new Point(510, 0x2e);
            this.Btn_UpdateWorkPeriod.Name = "Btn_UpdateWorkPeriod";
            this.Btn_UpdateWorkPeriod.Size = new Size(0xbc, 0x1c);
            this.Btn_UpdateWorkPeriod.TabIndex = 0x16;
            this.Btn_UpdateWorkPeriod.Text = "Update WorkPeriod";
            this.Btn_UpdateWorkPeriod.UseVisualStyleBackColor = true;
            this.Btn_UpdateWorkPeriod.Click += new EventHandler(this.Btn_Autocomplete_Click);
            this.textBox23.BackColor = SystemColors.Control;
            this.textBox23.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox23.Location = new Point(0x108, 0x67);
            this.textBox23.Name = "textBox23";
            this.textBox23.ReadOnly = true;
            this.textBox23.Size = new Size(200, 30);
            this.textBox23.TabIndex = 0x15;
            this.textBox23.TextAlign = HorizontalAlignment.Center;
            this.label23.AutoSize = true;
            this.label23.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label23.Location = new Point(0x106, 0x52);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x4c, 0x12);
            this.label23.TabIndex = 20;
            this.label23.Text = "Closed By";
            this.label14.AutoSize = true;
            this.label14.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label14.Location = new Point(0x4d, 0x19);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x2b, 0x12);
            this.label14.TabIndex = 5;
            this.label14.Text = "Cash";
            this.textBox20.BackColor = SystemColors.Control;
            this.textBox20.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox20.Location = new Point(0x1d, 0x69);
            this.textBox20.Name = "textBox20";
            this.textBox20.ReadOnly = true;
            this.textBox20.Size = new Size(200, 30);
            this.textBox20.TabIndex = 0x13;
            this.textBox20.Text = "0";
            this.textBox20.TextAlign = HorizontalAlignment.Center;
            this.textBox14.BackColor = SystemColors.Info;
            this.textBox14.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox14.Location = new Point(0x1b, 0x2e);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Size(0x8e, 30);
            this.textBox14.TabIndex = 6;
            this.textBox14.Text = "0";
            this.textBox14.TextAlign = HorizontalAlignment.Center;
            this.textBox14.TextChanged += new EventHandler(this.ClosingBalanceAmount_TextChanged);
            this.label21.AutoSize = true;
            this.label21.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label21.Location = new Point(0x1b, 0x54);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x90, 0x12);
            this.label21.TabIndex = 0x12;
            this.label21.Text = "Total Closing Blance";
            this.label13.AutoSize = true;
            this.label13.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label13.Location = new Point(0xe8, 0x19);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x35, 0x12);
            this.label13.TabIndex = 7;
            this.label13.Text = "Mpesa";
            this.textBox13.BackColor = SystemColors.Info;
            this.textBox13.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox13.Location = new Point(0xbd, 0x2e);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Size(0x8e, 30);
            this.textBox13.TabIndex = 8;
            this.textBox13.Text = "0";
            this.textBox13.TextAlign = HorizontalAlignment.Center;
            this.textBox13.TextChanged += new EventHandler(this.ClosingBalanceAmount_TextChanged);
            this.label7.AutoSize = true;
            this.label7.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label7.Location = new Point(0x180, 0x19);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x30, 0x12);
            this.label7.TabIndex = 9;
            this.label7.Text = "Cards";
            this.textBox7.BackColor = SystemColors.Info;
            this.textBox7.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox7.Location = new Point(0x15f, 0x2e);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Size(0x8e, 30);
            this.textBox7.TabIndex = 10;
            this.textBox7.Text = "0";
            this.textBox7.TextAlign = HorizontalAlignment.Center;
            this.textBox7.TextChanged += new EventHandler(this.ClosingBalanceAmount_TextChanged);
            this.Btn_CloseWorkPeriod.Location = new Point(510, 0x69);
            this.Btn_CloseWorkPeriod.Name = "Btn_CloseWorkPeriod";
            this.Btn_CloseWorkPeriod.Size = new Size(0xbc, 0x1c);
            this.Btn_CloseWorkPeriod.TabIndex = 0;
            this.Btn_CloseWorkPeriod.Text = "Close Work Period";
            this.Btn_CloseWorkPeriod.UseVisualStyleBackColor = true;
            this.Btn_CloseWorkPeriod.Click += new EventHandler(this.Btn_CloseWorkPeriod_Click);
            this.groupBox5.Controls.Add(this.textBox24);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.textBox22);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.textBox21);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.textBox16);
            this.groupBox5.Controls.Add(this.textBox17);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.textBox15);
            this.groupBox5.Controls.Add(this.textBox18);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.textBox19);
            this.groupBox5.Location = new Point(150, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(800, 150);
            this.groupBox5.TabIndex = 0x16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Opening Details";
            this.textBox24.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox24.Location = new Point(560, 0x2b);
            this.textBox24.Name = "textBox24";
            this.textBox24.ReadOnly = true;
            this.textBox24.Size = new Size(0x7f, 0x1a);
            this.textBox24.TabIndex = 0x17;
            this.textBox24.TextAlign = HorizontalAlignment.Center;
            this.label24.AutoSize = true;
            this.label24.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label24.Location = new Point(0x22d, 0x16);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x61, 0x12);
            this.label24.TabIndex = 0x16;
            this.label24.Tag = "";
            this.label24.Text = "Period Status";
            this.textBox22.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox22.Location = new Point(0x198, 0x2b);
            this.textBox22.Name = "textBox22";
            this.textBox22.ReadOnly = true;
            this.textBox22.Size = new Size(0x7f, 0x1a);
            this.textBox22.TabIndex = 0x15;
            this.textBox22.TextAlign = HorizontalAlignment.Center;
            this.label22.AutoSize = true;
            this.label22.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label22.Location = new Point(0x195, 0x16);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x51, 0x12);
            this.label22.TabIndex = 20;
            this.label22.Tag = "";
            this.label22.Text = "Opened By";
            this.textBox21.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox21.Location = new Point(0xd3, 0x2b);
            this.textBox21.Name = "textBox21";
            this.textBox21.ReadOnly = true;
            this.textBox21.Size = new Size(0xb3, 0x1a);
            this.textBox21.TabIndex = 0x13;
            this.textBox21.TextAlign = HorizontalAlignment.Center;
            this.label17.AutoSize = true;
            this.label17.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label17.Location = new Point(0xd0, 0x16);
            this.label17.Name = "label17";
            this.label17.Size = new Size(100, 0x12);
            this.label17.TabIndex = 0x12;
            this.label17.Tag = "";
            this.label17.Text = "Opening Time";
            this.label16.AutoSize = true;
            this.label16.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label16.Location = new Point(0x2c, 0x16);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x5c, 0x12);
            this.label16.TabIndex = 1;
            this.label16.Text = "Work Period";
            this.textBox16.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox16.Location = new Point(0x1b, 0x2b);
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new Size(0xa6, 0x1a);
            this.textBox16.TabIndex = 2;
            this.textBox16.TextAlign = HorizontalAlignment.Center;
            this.textBox17.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox17.Location = new Point(0x148, 0x6c);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new Size(0x7f, 0x1a);
            this.textBox17.TabIndex = 0x11;
            this.textBox17.Text = "0";
            this.textBox17.TextAlign = HorizontalAlignment.Center;
            this.textBox17.TextChanged += new EventHandler(this.CurrentOpeningBalanceAmount_TextChanged);
            this.label15.AutoSize = true;
            this.label15.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label15.Location = new Point(0x1df, 0x55);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x9d, 0x12);
            this.label15.TabIndex = 3;
            this.label15.Text = "Total Opening Balance";
            this.label18.AutoSize = true;
            this.label18.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label18.Location = new Point(0x169, 0x55);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x30, 0x12);
            this.label18.TabIndex = 0x10;
            this.label18.Text = "Cards";
            this.textBox15.BackColor = SystemColors.Control;
            this.textBox15.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox15.Location = new Point(0x1df, 0x6c);
            this.textBox15.Name = "textBox15";
            this.textBox15.ReadOnly = true;
            this.textBox15.Size = new Size(0xdb, 0x1a);
            this.textBox15.TabIndex = 4;
            this.textBox15.Text = "0";
            this.textBox15.TextAlign = HorizontalAlignment.Center;
            this.textBox18.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox18.Location = new Point(0xae, 0x6c);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new Size(0x8e, 0x1a);
            this.textBox18.TabIndex = 15;
            this.textBox18.Text = "0";
            this.textBox18.TextAlign = HorizontalAlignment.Center;
            this.textBox18.TextChanged += new EventHandler(this.CurrentOpeningBalanceAmount_TextChanged);
            this.label20.AutoSize = true;
            this.label20.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label20.Location = new Point(0x48, 0x55);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x2b, 0x12);
            this.label20.TabIndex = 12;
            this.label20.Text = "Cash";
            this.label19.AutoSize = true;
            this.label19.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label19.Location = new Point(0xd9, 0x55);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x35, 0x12);
            this.label19.TabIndex = 14;
            this.label19.Text = "Mpesa";
            this.textBox19.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox19.Location = new Point(0x16, 0x6c);
            this.textBox19.Name = "textBox19";
            this.textBox19.ReadOnly = true;
            this.textBox19.Size = new Size(0x8e, 0x1a);
            this.textBox19.TabIndex = 13;
            this.textBox19.Text = "0";
            this.textBox19.TextAlign = HorizontalAlignment.Center;
            this.textBox19.TextChanged += new EventHandler(this.CurrentOpeningBalanceAmount_TextChanged);
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x3e1, 450);
            base.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "WorkPeriods";
            this.Text = "WorkPeriods";
            base.Load += new EventHandler(this.WorkPeriods_Load);
            this.tabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            base.ResumeLayout(false);
        }

        private void OpeningBalanceAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox) sender).Text == "")
                {
                    ((TextBox) sender).Text = "0";
                    ((TextBox) sender).SelectionStart = 1;
                }
                decimal num2 = decimal.Parse(this.textBox9.Text);
                decimal num3 = decimal.Parse(this.textBox8.Text);
                this.textBox11.Text = ((num3 + decimal.Parse(this.textBox10.Text)) + num2).ToString();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void PreviousBalances_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox) sender).Text == "")
                {
                    ((TextBox) sender).Text = "0";
                    ((TextBox) sender).SelectionStart = 1;
                }
                decimal num2 = decimal.Parse(this.textBox4.Text);
                decimal num3 = decimal.Parse(this.textBox5.Text);
                this.textBox2.Text = ((num3 + decimal.Parse(this.textBox3.Text)) + num2).ToString();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void ResetClosingperiod()
        {
            this.textBox7.Text = "0";
            this.textBox13.Text = "0";
            this.textBox14.Text = "0";
            this.textBox15.Text = "0";
            this.textBox16.Text = "";
            this.textBox17.Text = "0";
            this.textBox18.Text = "0";
            this.textBox19.Text = "0";
            this.textBox20.Text = "0";
            this.textBox21.Text = "";
            this.textBox22.Text = "";
            this.textBox23.Text = "";
            this.textBox24.Text = "";
        }

        private void ResetCreateworkperiod()
        {
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (ReferenceEquals(this.tabControl1.SelectedTab, this.TabPage1))
            {
                this.CheckOpenworkperiod();
                if (this.groupBox3.Enabled)
                {
                    this.FindpreviousPeriodDetails();
                }
            }
            if (ReferenceEquals(this.tabControl1.SelectedTab, this.tabPage2))
            {
                this.CheckOpenworkperiod();
                if (this.groupBox4.Enabled)
                {
                    this.FindCurrentPeriodopeningDetails();
                }
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void WorkPeriods_Load(object sender, EventArgs e)
        {
            this.textBox12.Text = Program.CurrentDateTime().ToString("yyyy-MM-dd");
            this.textBox23.Text = Program.CurrLoggedInUser.UserID;
            this.tabControl1.SelectedTab = this.tabControl1.TabPages[0];
            this.CheckOpenworkperiod();
        }
    }
}

