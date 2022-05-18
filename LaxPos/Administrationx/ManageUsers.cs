namespace LaxPos.Administration
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class ManageUsers : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox groupBox1;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Button Btn_Reset;
        private Button Btn_AddUser;
        private Label label1;
        private Label label4;
        private TextBox textBox5;
        private Label label9;
        private Label label6;
        private TextBox textBox7;
        private TextBox textBox8;
        private Label label7;
        private ComboBox comboBox1;
        private ComboBox comboBox3;
        private Label label11;
        private ComboBox comboBox2;
        private Label label10;

        public ManageUsers()
        {
            this.InitializeComponent();
            this.comboBox1.Items.Clear();
            List<string> list = Enum.GetNames(typeof(Program.UserRolesCategories)).ToList<string>();
            this.comboBox1.Items.AddRange(list.ToArray());
        }

        private void AddUser()
        {
            try
            {
                int num = 0;
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlDataReader reader = new MySqlCommand("select max(userid) as Userid from posusers;", connection).ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Failed to Get the ID!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            reader.Close();
                            if (num > 0)
                            {
                                int num2 = num + 1;
                                MySqlCommand command = new MySqlCommand("INSERT INTO posusers (FirstName, LastName, Gender, Phone, Email, Password, Role, WorkingStatus, DateRegistered) VALUES(@FirstName, @LastName, @Gender, @Phone, @Email, @Password, @Role, @WorkingStatus, @DateRegistered);", connection);
                                command.Parameters.AddWithValue("@UserId", num2);
                                command.Parameters.AddWithValue("@FirstName", this.textBox2.Text.Trim());
                                command.Parameters.AddWithValue("@LastName", this.textBox3.Text.Trim());
                                command.Parameters.AddWithValue("@Gender", this.comboBox2.Text);
                                command.Parameters.AddWithValue("@Phone", this.textBox5.Text.Trim());
                                command.Parameters.AddWithValue("@Email", this.textBox8.Text.Trim());
                                command.Parameters.AddWithValue("@Password", this.textBox7.Text.Trim());
                                command.Parameters.AddWithValue("@Role", this.comboBox1.Text);
                                command.Parameters.AddWithValue("@WorkingStatus", this.comboBox3.Text);
                                command.Parameters.AddWithValue("@DateRegistered", Program.CurrentDateTime());
                                if (command.ExecuteNonQuery() > 0)
                                {
                                    MessageBox.Show("You have successfully registred the user", "Message Box", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    MessageBox.Show("Failed to register the user!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            connection.Close();
                            break;
                        }
                        num = reader.GetInt32("Userid");
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK);
            }
        }

        private void Btn_AddUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Add this User?", "Message Box", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.AddUser();
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
            this.groupBox1 = new GroupBox();
            this.comboBox3 = new ComboBox();
            this.label11 = new Label();
            this.comboBox2 = new ComboBox();
            this.label10 = new Label();
            this.comboBox1 = new ComboBox();
            this.label9 = new Label();
            this.label6 = new Label();
            this.textBox7 = new TextBox();
            this.textBox8 = new TextBox();
            this.label7 = new Label();
            this.textBox5 = new TextBox();
            this.textBox3 = new TextBox();
            this.label3 = new Label();
            this.textBox2 = new TextBox();
            this.Btn_Reset = new Button();
            this.Btn_AddUser = new Button();
            this.label1 = new Label();
            this.label4 = new Label();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.textBox8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.Btn_Reset);
            this.groupBox1.Controls.Add(this.Btn_AddUser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new Font("Palatino Linotype", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.groupBox1.Location = new Point(0xbd, 0x23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x2f2, 0x11d);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New User Details";
            this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            object[] items = new object[] { "On", "Off" };
            this.comboBox3.Items.AddRange(items);
            this.comboBox3.Location = new Point(0x25d, 0x6b);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0x7b, 0x1d);
            this.comboBox3.TabIndex = 0x2a;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x25a, 0x53);
            this.label11.Name = "label11";
            this.label11.Size = new Size(120, 0x15);
            this.label11.TabIndex = 0x29;
            this.label11.Text = "Working Status";
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            object[] objArray2 = new object[] { "Male", "Female" };
            this.comboBox2.Items.AddRange(objArray2);
            this.comboBox2.Location = new Point(0x25b, 0x2e);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x7d, 0x1d);
            this.comboBox2.TabIndex = 40;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x25f, 0x15);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x3f, 0x15);
            this.label10.TabIndex = 0x27;
            this.label10.Text = "Gender";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x142, 0xab);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0xfe, 0x1d);
            this.comboBox1.TabIndex = 0x26;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x142, 150);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x5d, 0x15);
            this.label9.TabIndex = 0x23;
            this.label9.Text = "Department";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x11, 150);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x4f, 0x15);
            this.label6.TabIndex = 0x20;
            this.label6.Text = "Password";
            this.textBox7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox7.Location = new Point(0x11, 0xae);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Size(0xfe, 0x1a);
            this.textBox7.TabIndex = 0x21;
            this.textBox7.TextAlign = HorizontalAlignment.Center;
            this.textBox8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox8.Location = new Point(0x142, 0x6b);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Size(0xfe, 0x1a);
            this.textBox8.TabIndex = 0x1f;
            this.textBox8.TextAlign = HorizontalAlignment.Center;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x142, 0x53);
            this.label7.Name = "label7";
            this.label7.Size = new Size(50, 0x15);
            this.label7.TabIndex = 30;
            this.label7.Text = "Email";
            this.textBox5.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox5.Location = new Point(0x142, 0x2e);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(0xfe, 0x1a);
            this.textBox5.TabIndex = 0x1c;
            this.textBox5.TextAlign = HorizontalAlignment.Center;
            this.textBox3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(0x11, 0x6b);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0xfe, 0x1a);
            this.textBox3.TabIndex = 0x19;
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x11, 0x53);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x54, 0x15);
            this.label3.TabIndex = 0x18;
            this.label3.Text = "Last Name";
            this.textBox2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0x11, 0x2e);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0xfe, 0x1a);
            this.textBox2.TabIndex = 0x17;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.Btn_Reset.BackColor = Color.OrangeRed;
            this.Btn_Reset.FlatStyle = FlatStyle.Flat;
            this.Btn_Reset.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Reset.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Reset.Location = new Point(0xec, 0xe0);
            this.Btn_Reset.Name = "Btn_Reset";
            this.Btn_Reset.Size = new Size(0x6f, 0x24);
            this.Btn_Reset.TabIndex = 0x16;
            this.Btn_Reset.Text = "Reset";
            this.Btn_Reset.UseVisualStyleBackColor = false;
            this.Btn_AddUser.BackColor = Color.FromArgb(0, 0xc0, 0);
            this.Btn_AddUser.FlatStyle = FlatStyle.Flat;
            this.Btn_AddUser.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_AddUser.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_AddUser.Location = new Point(0x17d, 0xe0);
            this.Btn_AddUser.Name = "Btn_AddUser";
            this.Btn_AddUser.Size = new Size(0x70, 0x24);
            this.Btn_AddUser.TabIndex = 20;
            this.Btn_AddUser.Text = "Save";
            this.Btn_AddUser.UseVisualStyleBackColor = false;
            this.Btn_AddUser.Click += new EventHandler(this.Btn_AddUser_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x142, 0x15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Phone";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x11, 0x15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x58, 0x15);
            this.label4.TabIndex = 13;
            this.label4.Text = "First Name";
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x4c4, 640);
            base.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ManageUsers";
            this.Text = "Users List";
            base.Load += new EventHandler(this.ManageUsers_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
        }
    }
}

