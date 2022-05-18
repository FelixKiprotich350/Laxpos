namespace LaxPos.Administration
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ManageUserRights : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private TextBox textBox10;
        private Button Btn_ManageRightsSearchUser;
        private Label label16;
        private TextBox textBox11;
        private Label label13;
        private TextBox textBox9;
        private Label label12;
        private TextBox textBox6;
        private Label label8;
        private TextBox textBox4;
        private Label label5;
        private SplitContainer splitContainer1;
        private ListView ListView_UserRights;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Button Btn_Save;

        public ManageUserRights()
        {
            this.InitializeComponent();
        }

        private void Btn_ManageRightsSearchUser_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from posusers where UserId=@userid";
                command.Parameters.AddWithValue("@userid", this.textBox10.Text.Trim());
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    this.textBox4.Text = "";
                    this.textBox6.Text = "";
                    this.textBox9.Text = "";
                    this.textBox11.Text = "";
                    MessageBox.Show("The user does not exist", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        this.textBox4.Text = reader.GetString("UserId");
                        this.textBox6.Text = reader.GetString("FirstName") + " " + reader.GetString("LastName");
                        this.textBox9.Text = reader.GetString("Department");
                        this.textBox11.Text = reader.GetString("WorkingStatus");
                        string str = reader.GetString("Userrights");
                        char[] separator = new char[] { ',' };
                        //Func<string, bool> predicate = <>c.<>9__4_0;
                        //if (<>c.<>9__4_0 == null)
                        //{
                        //    Func<string, bool> local1 = <>c.<>9__4_0;
                        //    predicate = <>c.<>9__4_0 = n => !string.IsNullOrEmpty(n);
                        //}
                        foreach (string str2 in str.Split(separator).ToArray<string>())
                        {
                            try
                            {
                                if (this.ListView_UserRights.Items.ContainsKey(str2))
                                {
                                    this.ListView_UserRights.Items[str2].Checked = true;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textBox9.Text == "Super Admin")
                {
                    MessageBox.Show("You cannot change the rights of super admin");
                }
                else if (this.textBox4.Text.Trim() == "")
                {
                    MessageBox.Show("Select a user to update the rights!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    string str = "";
                    foreach (ListViewItem item in this.ListView_UserRights.CheckedItems)
                    {
                        str = str + item.Text + ",";
                    }
                    MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "update posusers set Userrights=@rights where UserId=@userid";
                    command.Parameters.AddWithValue("@userid", this.textBox4.Text.Trim());
                    command.Parameters.AddWithValue("@rights", str);
                    if (command.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Rights Successfully Updated.", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("The system cannot update the rights.", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
            this.textBox10 = new TextBox();
            this.Btn_ManageRightsSearchUser = new Button();
            this.label16 = new Label();
            this.textBox11 = new TextBox();
            this.label13 = new Label();
            this.textBox9 = new TextBox();
            this.label12 = new Label();
            this.textBox6 = new TextBox();
            this.label8 = new Label();
            this.textBox4 = new TextBox();
            this.label5 = new Label();
            this.splitContainer1 = new SplitContainer();
            this.Btn_Save = new Button();
            this.ListView_UserRights = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.textBox10.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox10.Location = new Point(12, 0x20);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Size(0xae, 0x1a);
            this.textBox10.TabIndex = 0x17;
            this.textBox10.TextAlign = HorizontalAlignment.Center;
            this.Btn_ManageRightsSearchUser.BackColor = Color.FromArgb(0, 0xc0, 0);
            this.Btn_ManageRightsSearchUser.FlatStyle = FlatStyle.Flat;
            this.Btn_ManageRightsSearchUser.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_ManageRightsSearchUser.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_ManageRightsSearchUser.Location = new Point(0x24, 0x40);
            this.Btn_ManageRightsSearchUser.Name = "Btn_ManageRightsSearchUser";
            this.Btn_ManageRightsSearchUser.Size = new Size(0x70, 0x24);
            this.Btn_ManageRightsSearchUser.TabIndex = 20;
            this.Btn_ManageRightsSearchUser.Text = "Search User";
            this.Btn_ManageRightsSearchUser.UseVisualStyleBackColor = false;
            this.Btn_ManageRightsSearchUser.Click += new EventHandler(this.Btn_ManageRightsSearchUser_Click);
            this.label16.AutoSize = true;
            this.label16.Location = new Point(12, 9);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x2b, 20);
            this.label16.TabIndex = 13;
            this.label16.Text = "User";
            this.textBox11.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox11.Location = new Point(12, 0x13b);
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new Size(0xae, 0x1a);
            this.textBox11.TabIndex = 0x27;
            this.textBox11.TextAlign = HorizontalAlignment.Center;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(12, 290);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x70, 20);
            this.label13.TabIndex = 0x26;
            this.label13.Text = "Access Status";
            this.textBox9.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox9.Location = new Point(12, 0xfb);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new Size(0xae, 0x1a);
            this.textBox9.TabIndex = 0x25;
            this.textBox9.TextAlign = HorizontalAlignment.Center;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x11, 0xe2);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x2a, 20);
            this.label12.TabIndex = 0x24;
            this.label12.Text = "Department";
            this.textBox6.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox6.Location = new Point(12, 0xc5);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(0xae, 0x1a);
            this.textBox6.TabIndex = 0x22;
            this.textBox6.TextAlign = HorizontalAlignment.Center;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(12, 0xac);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x56, 20);
            this.label8.TabIndex = 0x20;
            this.label8.Text = "First Name";
            this.textBox4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(12, 0x88);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(0xae, 0x1a);
            this.textBox4.TabIndex = 0x1f;
            this.textBox4.TextAlign = HorizontalAlignment.Center;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 0x6f);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x40, 20);
            this.label5.TabIndex = 0x1d;
            this.label5.Text = "User ID";
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.Btn_Save);
            this.splitContainer1.Panel1.Controls.Add(this.textBox11);
            this.splitContainer1.Panel1.Controls.Add(this.label13);
            this.splitContainer1.Panel1.Controls.Add(this.textBox10);
            this.splitContainer1.Panel1.Controls.Add(this.Btn_ManageRightsSearchUser);
            this.splitContainer1.Panel1.Controls.Add(this.textBox9);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.textBox4);
            this.splitContainer1.Panel1.Controls.Add(this.textBox6);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.ListView_UserRights);
            this.splitContainer1.Size = new Size(0x3f5, 600);
            this.splitContainer1.SplitterDistance = 0xc6;
            this.splitContainer1.TabIndex = 1;
            this.Btn_Save.BackColor = Color.FromArgb(0, 0xc0, 0);
            this.Btn_Save.FlatStyle = FlatStyle.Flat;
            this.Btn_Save.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Save.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Save.Location = new Point(0x15, 0x16d);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new Size(0x9a, 0x24);
            this.Btn_Save.TabIndex = 40;
            this.Btn_Save.Text = "Update Rights";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Click += new EventHandler(this.Btn_Save_Click);
            this.ListView_UserRights.CheckBoxes = true;
            ColumnHeader[] values = new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 };
            this.ListView_UserRights.Columns.AddRange(values);
            this.ListView_UserRights.Dock = DockStyle.Fill;
            this.ListView_UserRights.FullRowSelect = true;
            this.ListView_UserRights.GridLines = true;
            this.ListView_UserRights.HeaderStyle = ColumnHeaderStyle.None;
            this.ListView_UserRights.HideSelection = false;
            this.ListView_UserRights.Location = new Point(0, 0);
            this.ListView_UserRights.Name = "ListView_UserRights";
            this.ListView_UserRights.Size = new Size(0x32b, 600);
            this.ListView_UserRights.TabIndex = 6;
            this.ListView_UserRights.UseCompatibleStateImageBehavior = false;
            this.ListView_UserRights.View = View.Details;
            this.columnHeader1.Text = "RCode";
            this.columnHeader1.Width = 0x5f;
            this.columnHeader2.Text = "ShortName";
            this.columnHeader2.Width = 210;
            this.columnHeader3.Text = "Full Name";
            this.columnHeader3.Width = 360;
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = SystemColors.ButtonHighlight;
            base.ClientSize = new Size(0x3f5, 600);
            base.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "ManageUserRights";
            this.Text = "ManageUserRights";
            base.Load += new EventHandler(this.ManageUserRights_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadRoleRights()
        {
            try
            {
                this.ListView_UserRights.Items.Clear();
                this.ListView_UserRights.Groups.Clear();
                foreach (string str in Enum.GetNames(typeof(Program.UserRolesCategories)).ToList<string>())
                {
                    ListViewGroup group = new ListViewGroup();
                    group.Header = str;
                    group.Name = str;
                    this.ListView_UserRights.Groups.Add(group);
                }
                foreach (FunctionalityRight right in Program.Fun_Rights.ToList<FunctionalityRight>())
                {
                    ListViewItem item1 = new ListViewItem();
                    item1.Name = right.RightID;
                    item1.Text = right.RightID;
                    item1.Group = this.ListView_UserRights.Groups[right.Department.ToString()];
                    ListViewItem item = item1;
                    item.SubItems.Add(right.RightShortName);
                    item.SubItems.Add(right.RightFullName);
                    this.ListView_UserRights.Items.Add(item);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void ManageUserRights_Load(object sender, EventArgs e)
        {
            this.LoadRoleRights();
        }

        //[Serializable, CompilerGenerated]
        //private sealed class <>c
        //{
        //    public static readonly ManageUserRights.<>c <>9 = new ManageUserRights.<>c();
        //    public static Func<string, bool> <>9__4_0;

        //    internal bool <Btn_ManageRightsSearchUser_Click>b__4_0(string n) => 
        //        !string.IsNullOrEmpty(n);
        //}
    }
}

