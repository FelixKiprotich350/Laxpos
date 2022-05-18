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

    public class ViewUsers : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private IContainer components = null;
        private GroupBox groupBox2;
        private DataGridView Gridview_UsersList;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private Button Btn_SearchAll;
        private Button Btn_SearchRole;
        private Label label2;
        private TextBox textBox1;
        private Label label1;
        private ComboBox comboBox1;
        private Button Btn_SearchUserId;

        public ViewUsers()
        {
            this.InitializeComponent();
            this.comboBox1.Items.Clear();
            List<string> list = Enum.GetNames(typeof(Program.UserRolesCategories)).ToList<string>();
            this.comboBox1.Items.AddRange(list.ToArray());
        }

        private void Btn_SearchAll_Click(object sender, EventArgs e)
        {
            this.LoadUsersList("");
        }

        private void Btn_SearchRole_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text.Trim() != "")
            {
                this.LoadUsersList(" AND Department=@Department");
            }
            else
            {
                MessageBox.Show("Select the Department to search for!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Btn_SearchUserId_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != "")
            {
                this.LoadUsersList(" AND UserId=@userid");
            }
            else
            {
                MessageBox.Show("Enter the UserID to search for a specific user!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void Gridview_UsersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    string str = this.Gridview_UsersList.Rows[e.RowIndex].Cells[6].Value.ToString();
                    string str2 = this.Gridview_UsersList.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string str3 = this.Gridview_UsersList.Rows[e.RowIndex].Cells[1].Value.ToString() + " " + this.Gridview_UsersList.Rows[e.RowIndex].Cells[2].Value.ToString();
                    ViewPassword_Enabler enabler1 = new ViewPassword_Enabler();
                    enabler1.Text = str;
                    ViewPassword_Enabler enabler = enabler1;
                    enabler.textBox1.Text = str2;
                    enabler.textBox2.Text = str3;
                    enabler.ShowDialog(this);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.groupBox2 = new GroupBox();
            this.Btn_SearchRole = new Button();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.Btn_SearchUserId = new Button();
            this.Btn_SearchAll = new Button();
            this.Gridview_UsersList = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.Gridview_UsersList).BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.Btn_SearchRole);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.Btn_SearchUserId);
            this.groupBox2.Controls.Add(this.Btn_SearchAll);
            this.groupBox2.Dock = DockStyle.Left;
            this.groupBox2.Location = new Point(0, 0);
            this.groupBox2.Margin = new Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(4, 5, 4, 5);
            this.groupBox2.Size = new Size(0xac, 0x1ef);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.Btn_SearchRole.Location = new Point(0x10, 0x97);
            this.Btn_SearchRole.Margin = new Padding(4, 5, 4, 5);
            this.Btn_SearchRole.Name = "Btn_SearchRole";
            this.Btn_SearchRole.Size = new Size(140, 0x23);
            this.Btn_SearchRole.TabIndex = 6;
            this.Btn_SearchRole.Text = "View Per Role";
            this.Btn_SearchRole.UseVisualStyleBackColor = true;
            this.Btn_SearchRole.Click += new EventHandler(this.Btn_SearchRole_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0xe5);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x40, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "User ID";
            this.textBox1.Location = new Point(0x10, 0xfc);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x88, 0x1a);
            this.textBox1.TabIndex = 4;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x5c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x5b, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Role";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(0x10, 0x73);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x88, 0x1c);
            this.comboBox1.TabIndex = 2;
            this.Btn_SearchUserId.Location = new Point(0x10, 0x11e);
            this.Btn_SearchUserId.Margin = new Padding(4, 5, 4, 5);
            this.Btn_SearchUserId.Name = "Btn_SearchUserId";
            this.Btn_SearchUserId.Size = new Size(140, 0x23);
            this.Btn_SearchUserId.TabIndex = 1;
            this.Btn_SearchUserId.Text = "Search User";
            this.Btn_SearchUserId.UseVisualStyleBackColor = true;
            this.Btn_SearchUserId.Click += new EventHandler(this.Btn_SearchUserId_Click);
            this.Btn_SearchAll.Location = new Point(0x10, 0x1f);
            this.Btn_SearchAll.Margin = new Padding(4, 5, 4, 5);
            this.Btn_SearchAll.Name = "Btn_SearchAll";
            this.Btn_SearchAll.Size = new Size(140, 0x23);
            this.Btn_SearchAll.TabIndex = 0;
            this.Btn_SearchAll.Text = "View All Users";
            this.Btn_SearchAll.UseVisualStyleBackColor = true;
            this.Btn_SearchAll.Click += new EventHandler(this.Btn_SearchAll_Click);
            this.Gridview_UsersList.AllowUserToAddRows = false;
            this.Gridview_UsersList.AllowUserToDeleteRows = false;
            this.Gridview_UsersList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Gridview_UsersList.BackgroundColor = SystemColors.ButtonHighlight;
            style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style.BackColor = Color.Indigo;
            style.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style.ForeColor = SystemColors.ButtonHighlight;
            style.SelectionBackColor = SystemColors.Highlight;
            style.SelectionForeColor = SystemColors.HighlightText;
            style.WrapMode = DataGridViewTriState.True;
            this.Gridview_UsersList.ColumnHeadersDefaultCellStyle = style;
            this.Gridview_UsersList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column4, this.Column5, this.Column6, this.Column7, this.Column9, this.Column10 };
            this.Gridview_UsersList.Columns.AddRange(dataGridViewColumns);
            this.Gridview_UsersList.Dock = DockStyle.Fill;
            this.Gridview_UsersList.EnableHeadersVisualStyles = false;
            this.Gridview_UsersList.Location = new Point(0xac, 0);
            this.Gridview_UsersList.Margin = new Padding(4, 5, 4, 5);
            this.Gridview_UsersList.Name = "Gridview_UsersList";
            this.Gridview_UsersList.ReadOnly = true;
            this.Gridview_UsersList.RowHeadersVisible = false;
            this.Gridview_UsersList.Size = new Size(0x404, 0x1ef);
            this.Gridview_UsersList.TabIndex = 2;
            this.Gridview_UsersList.CellDoubleClick += new DataGridViewCellEventHandler(this.Gridview_UsersList_CellDoubleClick);
            this.Column1.HeaderText = "UserId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column2.HeaderText = "FirstName";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column4.HeaderText = "LastName";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column5.HeaderText = "Email";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column6.HeaderText = "Gender";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column7.HeaderText = "Phone";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column9.HeaderText = "Department";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column10.HeaderText = "WorkingStatus";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            base.AutoScaleMode = AutoScaleMode.None;
            base.ClientSize = new Size(0x4b0, 0x1ef);
            base.Controls.Add(this.Gridview_UsersList);
            base.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Margin = new Padding(4, 5, 4, 5);
            base.Name = "ViewUsers";
            this.Text = "ViewUsers";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((ISupportInitialize) this.Gridview_UsersList).EndInit();
            base.ResumeLayout(false);
        }

        private void LoadUsersList(string SearchQuery)
        {
            try
            {
                this.Gridview_UsersList.Rows.Clear();
                string cmdText = "select * from posusers where UserId is not null" + SearchQuery;
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand(cmdText, connection);
                command.Parameters.AddWithValue("@userid", this.textBox1.Text.Trim());
                command.Parameters.AddWithValue("@Department", this.comboBox1.Text.Trim());
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No users have been Found!", "users list", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        object[] values = new object[] { reader["UserId"].ToString(), reader["FirstName"].ToString(), reader["LastName"].ToString(), reader["Email"].ToString(), reader["Gender"].ToString(), reader["Phone"].ToString(), reader["Department"].ToString(), reader["WorkingStatus"].ToString() };
                        this.Gridview_UsersList.Rows.Add(values);
                    }
                }
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}

