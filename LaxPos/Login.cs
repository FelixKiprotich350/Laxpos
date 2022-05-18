namespace LaxPos
{
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class Login : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public int LoginStatus = 0;
        private IContainer components = null;
        private Label label1;
        private Button Btn_Exit;
        private Button Btn_Login;
        private TextBox TextBox1;
        private TextBox TextBox2;
        private Label label2;
        private Label label3;

        public Login()
        {
            this.InitializeComponent();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            if ((this.TextBox1.Text == "") || (this.TextBox2.Text == ""))
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
                    command.Parameters.AddWithValue("@Pass", this.TextBox2.Text);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show(this, "Incorrect Password", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        UserLoggedIn @in = new UserLoggedIn();
                        while (true)
                        {
                            if (!reader.Read())
                            {
                                Program.CurrLoggedInUser = @in;
                                Program.LastActionTime = DateTime.Now;
                                this.TextBox1.Text = "";
                                this.TextBox2.Text = "";
                                Program.CurrLoginForm = this;
                                Program.CurrDashboardForm = new Dashboard();
                                Program.CurrDashboardForm.Show();
                                base.Hide();
                                break;
                            }
                            Program.Lastuser = "";
                            char[] separator = new char[] { ',' };
                            string[] source = reader["Userrights"].ToString().Split(separator);
                            //Func<string, bool> predicate = <>c.<>9__5_0;
                            //if (<>c.<>9__5_0 == null)
                            //{
                            //    Func<string, bool> local1 = <>c.<>9__5_0;
                            //    predicate = <>c.<>9__5_0 = n => n != "";
                            //}
                            @in.UserRights = source.ToList<string>();
                            @in.UserID = reader["UserId"].ToString();
                            @in.UserFirstname = reader["FirstName"].ToString();
                            @in.UserLastname = reader["Lastname"].ToString();
                            @in.UserRole = reader["Department"].ToString();
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this, exception.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Btn_Login = new System.Windows.Forms.Button();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.BackColor = System.Drawing.Color.OrangeRed;
            this.Btn_Exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Exit.FlatAppearance.BorderColor = System.Drawing.Color.OrangeRed;
            this.Btn_Exit.FlatAppearance.BorderSize = 0;
            this.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Exit.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Exit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Exit.Location = new System.Drawing.Point(92, 122);
            this.Btn_Exit.Margin = new System.Windows.Forms.Padding(1);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(100, 30);
            this.Btn_Exit.TabIndex = 6;
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.UseVisualStyleBackColor = false;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // Btn_Login
            // 
            this.Btn_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Btn_Login.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Btn_Login.FlatAppearance.BorderSize = 0;
            this.Btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Login.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Login.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Login.Location = new System.Drawing.Point(210, 122);
            this.Btn_Login.Margin = new System.Windows.Forms.Padding(1);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(100, 30);
            this.Btn_Login.TabIndex = 7;
            this.Btn_Login.Text = "Login";
            this.Btn_Login.UseVisualStyleBackColor = false;
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox1.Location = new System.Drawing.Point(92, 42);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(220, 29);
            this.TextBox1.TabIndex = 8;
            this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // TextBox2
            // 
            this.TextBox2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox2.Location = new System.Drawing.Point(92, 84);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(220, 29);
            this.TextBox2.TabIndex = 11;
            this.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBox2.UseSystemPasswordChar = true;
            this.TextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox2_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 10;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(315, 27);
            this.label3.TabIndex = 12;
            this.label3.Text = "Login";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label3_MouseDown);
            // 
            // Login
            // 
            this.AcceptButton = this.Btn_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Btn_Exit;
            this.ClientSize = new System.Drawing.Size(315, 158);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Btn_Login);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Palatino Linotype", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Shown += new System.EventHandler(this.Login_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Label3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(base.Handle, 0x112, 0xf012, 0);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.TextBox1.Focus();
            base.ActiveControl = this.TextBox1;
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            this.TextBox1.Focus();
            base.ActiveControl = this.TextBox1;
        }

        [DllImport("user32.DLL")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL")]
        private static extern void SendMessage(IntPtr one, int two, int three, int four);
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                this.TextBox2.Focus();
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
        //    public static readonly Login.<>c <>9 = new Login.<>c();
        //    public static Func<string, bool> <>9__5_0;

        //    internal bool <Btn_Login_Click>b__5_0(string n) => 
        //        n != "";
        //}
    }
}

