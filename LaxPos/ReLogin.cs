namespace LaxPos
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ReLogin : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public int LoginStatus = 0;
        private IContainer components = null;
        private Label label1;
        private Button Btn_Exit;
        private Button Btn_Login;
        private TextBox TextBox1;
        private TextBox textBox2;
        private Label label2;
        private Label label3;

        public ReLogin()
        {
            this.InitializeComponent();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to Quit?", "Message Box", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            if ((this.TextBox1.Text == "") || (this.textBox2.Text == ""))
            {
                MessageBox.Show("Enter Password and Username", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                base.UseWaitCursor = true;
                try
                {
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("select * from posusers where UserId=@id and Password=@Pass", connection);
                    command.Parameters.AddWithValue("@id", this.TextBox1.Text);
                    command.Parameters.AddWithValue("@Pass", this.textBox2.Text);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Incorrect Password", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        UserLoggedIn @in = new UserLoggedIn();
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                Program.CurrLoggedInUser = @in;
                                if (Program.Lastuser != Program.CurrLoggedInUser.UserID)
                                {
                                    Program.CurrDashboardForm = new Dashboard();
                                    Program.CurrDashboardForm.Show();
                                }
                                else
                                {
                                    Program.CurrDashboardForm.Show();
                                    foreach (Form form in Program.CurrDashboardForm.OwnedForms)
                                    {
                                        form.Show();
                                    }
                                }
                                base.Close();
                                break;
                            }
                            char[] separator = new char[] { ',' };
                            string[] source = reader["Userrights"].ToString().Split(separator);
                            //Func<string, bool> predicate = <>c.<>9__3_0;
                            //if (<>c.<>9__3_0 == null)
                            //{
                            //    Func<string, bool> local1 = <>c.<>9__3_0;
                            //    predicate = <>c.<>9__3_0 = n => n != "";
                            //}
                            @in.UserRights = source.ToList<string>();
                            @in.UserID = reader["UserId"].ToString();
                            @in.UserFirstname = reader["FirstName"].ToString();
                            @in.UserLastname = reader["Lastname"].ToString();
                            @in.UserRole = reader["Department"].ToString();
                        }
                    }
                }
                catch (Exception exception1)
                {
                    MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                base.UseWaitCursor = false;
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
            this.label1 = new Label();
            this.Btn_Exit = new Button();
            this.Btn_Login = new Button();
            this.TextBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.label2 = new Label();
            this.label3 = new Label();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 0x26);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x52, 0x13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            this.Btn_Exit.BackColor = Color.OrangeRed;
            this.Btn_Exit.DialogResult = DialogResult.Cancel;
            this.Btn_Exit.FlatAppearance.BorderColor = Color.Red;
            this.Btn_Exit.FlatStyle = FlatStyle.Flat;
            this.Btn_Exit.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Exit.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Exit.Location = new Point(0x62, 0x76);
            this.Btn_Exit.Margin = new Padding(1);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new Size(100, 30);
            this.Btn_Exit.TabIndex = 6;
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.UseVisualStyleBackColor = false;
            this.Btn_Exit.Click += new EventHandler(this.Btn_Exit_Click);
            this.Btn_Login.BackColor = Color.FromArgb(0, 0xc0, 0);
            this.Btn_Login.FlatAppearance.BorderColor = Color.FromArgb(0, 0xc0, 0);
            this.Btn_Login.FlatAppearance.BorderSize = 0;
            this.Btn_Login.FlatStyle = FlatStyle.Flat;
            this.Btn_Login.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Login.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Login.Location = new Point(0xd8, 0x76);
            this.Btn_Login.Margin = new Padding(1);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new Size(100, 30);
            this.Btn_Login.TabIndex = 7;
            this.Btn_Login.Text = "Login";
            this.Btn_Login.UseVisualStyleBackColor = false;
            this.Btn_Login.Click += new EventHandler(this.Btn_Login_Click);
            this.TextBox1.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.TextBox1.Location = new Point(0x62, 0x26);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new Size(220, 0x1d);
            this.TextBox1.TabIndex = 8;
            this.TextBox1.KeyDown += new KeyEventHandler(this.TextBox1_KeyDown);
            this.textBox2.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox2.Location = new Point(0x62, 80);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(220, 0x1d);
            this.textBox2.TabIndex = 11;
            this.textBox2.UseSystemPasswordChar = true;
            this.textBox2.KeyDown += new KeyEventHandler(this.TextBox2_KeyDown);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(10, 80);
            this.label2.Margin = new Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(70, 0x13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Password";
            this.label3.BackColor = Color.Navy;
            this.label3.BorderStyle = BorderStyle.FixedSingle;
            this.label3.Dock = DockStyle.Top;
            this.label3.Font = new Font("Palatino Linotype", 11f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = SystemColors.ButtonHighlight;
            this.label3.Location = new Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x145, 0x1a);
            this.label3.TabIndex = 12;
            this.label3.Text = "Login";
            this.label3.TextAlign = ContentAlignment.MiddleCenter;
            base.AcceptButton = this.Btn_Login;
            base.AutoScaleDimensions = new SizeF(8f, 18f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.Btn_Exit;
            base.ClientSize = new Size(0x145, 0x9e);
            base.ControlBox = false;
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.TextBox1);
            base.Controls.Add(this.Btn_Login);
            base.Controls.Add(this.Btn_Exit);
            base.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new Font("Palatino Linotype", 10f, FontStyle.Bold, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Margin = new Padding(4);
            base.MaximizeBox = false;
            this.MaximumSize = new Size(350, 200);
            base.MinimizeBox = false;
            this.MinimumSize = new Size(0x145, 50);
            base.Name = "ReLogin";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Login Form";
            base.Load += new EventHandler(this.Login_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.TextBox1.Focus();
            base.ActiveControl = this.TextBox1;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                this.textBox2.Focus();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                this.TextBox1.Focus();
            }
        }

        //[Serializable, CompilerGenerated]
        //private sealed class <>c
        //{
        //    public static readonly ReLogin.<>c <>9 = new ReLogin.<>c();
        //    public static Func<string, bool> <>9__3_0;

        //    internal bool <Btn_Login_Click>b__3_0(string n) => 
        //        n != "";
        //}
    }
}

