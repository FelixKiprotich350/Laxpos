namespace LaxPos.Pos
{
    using LaxPos;
    using LaxPos.LaxPosFiles;
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class Sales : Form
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        private readonly CompanyProfile Client = new CompanyProfile();
        private readonly Accounts Acc = new Accounts();
        public AutoCompleteStringCollection Autocom = new AutoCompleteStringCollection();
        private readonly List<string> listOnit = new List<string>();
        private static string Title = "";
        private static string Box = "";
        private static string Email = "";
        private static string Tel = "";
        private static string Pin = "";
        private static string Text1 = "";
        private static string Text2 = "";
        private static string Text3 = "";
        private static string Text4 = "";
        private static decimal TaxPercentage = 0M;
        public int InvoicePeriod = 0;
        public int QuotationPeriod = 0;
        public static int CartTableId = 0x3e8;
        public string TransactionCode = "";
        public int Center_X = 150;
        public decimal TaxAmt = 0M;
        public decimal DiscountAmount = 0M;
        public int PointsAwarded = 0;
        public decimal AmountPaid = 0M;
        public decimal Balance = 0M;
        public decimal GrossTotal = 0M;
        public decimal Total_Profit = 0M;
        public string CustomerID = "";
        private IContainer components = null;
        private ContextMenuStrip MainCart_MenuStrip;
        private ToolStripMenuItem clearCartToolStripMenuItem;
        private ToolStripMenuItem holdCartToolStripMenuItem;
        private ToolStripMenuItem restoreCartToolStripMenuItem;
        private Button Btn_AddItem;
        private Label Product_IdLabel;
        private Panel Panel_ProductSearch;
        private Button Btn_CartRetrieval;
        private Panel panel2;
        private Label label11;
        private Button Btn_Payments;
        private Button Btn_HoldCart;
        private Label label1;
        private TextBox Txt_Tax;
        private Label label2;
        private TextBox textBox1;
        private Panel Panel_salesOperations;
        public DataGridView Cart_Gridview;
        private TextBox textBox2;
        private Button Btn_Invoice;
        private Button Btn_Quotation;
        private Panel panel1;
        private Label label3;
        private TextBox textBox3;
        private Button Btn_Close;
        private Panel panel3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn TotalProfit;
        private DataGridViewTextBoxColumn DiscAmount;
        private DataGridViewButtonColumn Column6;
        private TextBox textBox4;

        public Sales()
        {
            this.InitializeComponent();
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.LoadReceiptSettings();
        }

        private void BindComboBox()
        {
            this.listOnit.AddRange(Program.ProductsItemsList.ToArray());
        }

        private void Btn_AddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CheckIfNotEmpty())
                {
                    if (this.Cart_Gridview.Rows.Count > 0)
                    {
                        this.CheckCart();
                        this.textBox4.Text = "";
                    }
                    else
                    {
                        this.FindProducts();
                        this.textBox4.Text = "";
                    }
                }
            }
            catch (Exception exception)
            {
                this.textBox4.Text = "";
                MessageBox.Show(exception.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_CartRetrieval_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Restore the Suspended Cart ?", "Confirmation Box", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                CartRetrieve retrieve = new CartRetrieve(Program.CustomerCart);
                retrieve.ShowDialog(this);
                if (retrieve.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        if (Program.CustomerCart.Tables["999"].Rows.Count <= 0)
                        {
                            MessageBox.Show("Failed to Restore the cart!!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (this.Cart_Gridview.Rows.Count <= 0)
                        {
                            int num4 = 0;
                            while (true)
                            {
                                if (num4 >= Program.CustomerCart.Tables["999"].Rows.Count)
                                {
                                    break;
                                }
                                object[] values = new object[11];
                                values[0] = Program.CustomerCart.Tables["999"].Rows[num4][0];
                                values[1] = Program.CustomerCart.Tables["999"].Rows[num4][1];
                                values[2] = Program.CustomerCart.Tables["999"].Rows[num4][2];
                                values[3] = Program.CustomerCart.Tables["999"].Rows[num4][3];
                                values[4] = Program.CustomerCart.Tables["999"].Rows[num4][4];
                                values[5] = Program.CustomerCart.Tables["999"].Rows[num4][5];
                                values[6] = Program.CustomerCart.Tables["999"].Rows[num4][6];
                                values[7] = Program.CustomerCart.Tables["999"].Rows[num4][7];
                                values[8] = "";
                                values[9] = Program.CustomerCart.Tables["999"].Rows[num4][8];
                                values[10] = Program.CustomerCart.Tables["999"].Rows[num4][9];
                                this.Cart_Gridview.Rows.Add(values);
                                this.Total();
                                num4++;
                            }
                        }
                        else
                        {
                            int num = 0;
                            while (true)
                            {
                                if (num >= Program.CustomerCart.Tables["999"].Rows.Count)
                                {
                                    break;
                                }
                                string str = Program.CustomerCart.Tables["999"].Rows[num][0].ToString();
                                int count = this.Cart_Gridview.Rows.Count;
                                int num3 = 0;
                                while (true)
                                {
                                    if (num3 < count)
                                    {
                                        if (this.Cart_Gridview.Rows[num3].Cells[0].Value.ToString() == str)
                                        {
                                            this.Cart_Gridview.Rows[num3].Cells[2].Value = int.Parse(this.Cart_Gridview.Rows[num3].Cells[2].Value.ToString()) + 1;
                                            this.Cart_Gridview.Update();
                                            this.Total();
                                        }
                                        else
                                        {
                                            if ((this.Cart_Gridview.Rows[num3].Cells[0].Value.ToString() == str) || (num3 != (count - 1)))
                                            {
                                                num3++;
                                                continue;
                                            }
                                            object[] values = new object[11];
                                            values[0] = Program.CustomerCart.Tables["999"].Rows[num][0];
                                            values[1] = Program.CustomerCart.Tables["999"].Rows[num][1];
                                            values[2] = Program.CustomerCart.Tables["999"].Rows[num][2];
                                            values[3] = Program.CustomerCart.Tables["999"].Rows[num][3];
                                            values[4] = Program.CustomerCart.Tables["999"].Rows[num][4];
                                            values[5] = Program.CustomerCart.Tables["999"].Rows[num][5];
                                            values[6] = Program.CustomerCart.Tables["999"].Rows[num][6];
                                            values[7] = Program.CustomerCart.Tables["999"].Rows[num][7];
                                            values[8] = "";
                                            values[9] = Program.CustomerCart.Tables["999"].Rows[num][8];
                                            values[10] = Program.CustomerCart.Tables["999"].Rows[num][9];
                                            this.Cart_Gridview.Rows.Add(values);
                                            this.Total();
                                        }
                                    }
                                    num++;
                                    break;
                                }
                            }
                        }
                        Program.CustomerCart.Tables.Remove("999");
                    }
                    catch (Exception exception1)
                    {
                        MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void Btn_CheckouPayment_Click(object sender, EventArgs e)
        {
            try
            {
                this.GenerateId();
                this.Total_Profit = 0M;
                this.CustomerID = "";
                if (this.Cart_Gridview.Rows.Count <= 0)
                {
                    MessageBox.Show("You have No items to pay for in the Cart!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    CustomerCart.Table_PaymentsDataTable dt = new CustomerCart.Table_PaymentsDataTable();
                    decimal totalAmount = Convert.ToDecimal(this.textBox2.Text);
                    this.Total();
                    Transactions transactions = new Transactions(totalAmount, dt);
                    if (transactions.ShowDialog(this) == DialogResult.OK)
                    {
                        this.GrossTotal = totalAmount;
                        this.AmountPaid = Convert.ToDecimal(transactions.Txt_AmountPaidTotal.Text.ToString());
                        this.Balance = Convert.ToDecimal(transactions.Txt_Balance.Text.ToString());
                        this.TaxAmt = Convert.ToDecimal((decimal) ((this.Client.ClientTaxRate * this.GrossTotal) / 100M));
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("No payments Found!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            this.InsertSale(dt);
                            this.PrintReceipt();
                            this.GenerateId();
                            if (MessageBox.Show("Completed", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                            {
                                this.ResetForm();
                            }
                            this.GrossTotal = 0M;
                            this.AmountPaid = 0M;
                            this.Balance = 0M;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Btn_HoldCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Cart_Gridview.Rows.Count <= 0)
                {
                    MessageBox.Show("No items to suspend !!", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    CartTableId++;
                    string name = CartTableId.ToString();
                    Program.CustomerCart.Tables.Add(name);
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[0].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[1].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[2].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[3].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[4].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[5].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[6].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[7].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[9].HeaderText.ToString());
                    Program.CustomerCart.Tables[name].Columns.Add(this.Cart_Gridview.Columns[10].HeaderText.ToString());
                    foreach (DataGridViewRow row in (IEnumerable) this.Cart_Gridview.Rows)
                    {
                        object[] values = new object[10];
                        values[0] = row.Cells[0].Value;
                        values[1] = row.Cells[1].Value;
                        values[2] = row.Cells[2].Value;
                        values[3] = row.Cells[3].Value;
                        values[4] = row.Cells[4].Value;
                        values[5] = row.Cells[5].Value;
                        values[6] = row.Cells[6].Value;
                        values[7] = row.Cells[7].Value;
                        values[8] = row.Cells[9].Value;
                        values[9] = row.Cells[10].Value;
                        Program.CustomerCart.Tables[name].Rows.Add(values);
                    }
                    if (new CartSuspend(name, Program.CustomerCart).ShowDialog(this) != DialogResult.OK)
                    {
                        Program.CustomerCart.Tables.Remove(name);
                    }
                    else
                    {
                        this.Cart_Gridview.Rows.Clear();
                        this.ResetForm();
                        MessageBox.Show("You have successfully susspended the cart", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_Invoice_Click(object sender, EventArgs e)
        {
            InvoiceBilling billing = new InvoiceBilling(this.TransactionCode, this.textBox2.Text, this.Txt_Tax.Text);
            if (billing.ShowDialog(this) == DialogResult.OK)
            {
                this.InsertInvoice(Convert.ToDecimal(this.textBox2.Text), billing.textBox17.Text);
                this.AmountPaid = Convert.ToDecimal(this.textBox2.Text);
                this.InsertSale(new DataTable());
                if (MessageBox.Show("Completed", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                {
                    this.ResetForm();
                }
                this.GrossTotal = 0M;
                this.AmountPaid = 0M;
                this.Balance = 0M;
            }
        }

        private void Btn_Quotation_Click(object sender, EventArgs e)
        {
            Quotation quotation = new Quotation();
            if (quotation.ShowDialog(this) == DialogResult.OK)
            {
                this.InsertQuotation(Convert.ToDecimal(this.textBox2.Text), quotation.textBox1.Text);
            }
        }

        private void CaptureNumeric(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar));
        }

        private void Cart_Gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.Cart_Gridview.Rows.Count > 0) && ((this.Cart_Gridview.CurrentCell.Value.ToString() == "Del") && (MessageBox.Show("You are about to Delete an item from the cart...\nDo you want to continue ?", "CONFIRMATION...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)))
            {
                this.Cart_Gridview.Rows.RemoveAt(e.RowIndex);
                this.Total();
            }
        }

        private void Cart_Gridview_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.Cart_Gridview_CellValueChanged(new object(), new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
        }

        private void Cart_Gridview_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        private void Cart_Gridview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((this.Cart_Gridview.Rows.Count > 0) && (e.ColumnIndex == 3))
                {
                    double num3;
                    if (this.Cart_Gridview.CurrentCell.Value.ToString() == "")
                    {
                        this.Cart_Gridview.CurrentCell.Value = Convert.ToDouble(1);
                    }
                    int rowIndex = this.Cart_Gridview.CurrentCell.RowIndex;
                    if (double.TryParse(this.Cart_Gridview.CurrentCell.Value.ToString(), out num3))
                    {
                        this.Cart_Gridview.UpdateCellValue(3, this.Cart_Gridview.CurrentCell.RowIndex);
                        double num4 = num3 * Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[4].Value);
                        this.Cart_Gridview.Rows[rowIndex].Cells[5].Value = num4;
                    }
                    else
                    {
                        this.Cart_Gridview.CurrentCell.Value = 1;
                        double num5 = 1.0 * Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[5].Value);
                        this.Cart_Gridview.Rows[rowIndex].Cells[5].Value = num5;
                    }
                }
                this.Total();
            }
            catch
            {
                MessageBox.Show("Unknown Value", "ERROR MESSAGE");
            }
        }

        private void Cart_Gridview_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(this.Column_KeyPress);
            if (this.Cart_Gridview.CurrentCell.ColumnIndex == 2)
            {
                TextBox control = e.Control as TextBox;
                if (control != null)
                {
                    control.KeyPress += new KeyPressEventHandler(this.Column_KeyPress);
                }
            }
        }

        private void Cart_Gridview_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void Cart_Gridview_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                int count = this.Cart_Gridview.Rows.Count;
                if (count > 0)
                {
                    int num2 = count - 1;
                    this.Cart_Gridview.CurrentCell = this.Cart_Gridview.Rows[num2].Cells[3];
                    this.Cart_Gridview.BeginEdit(true);
                    int rowIndex = this.Cart_Gridview.CurrentCell.RowIndex;
                    string str = this.Cart_Gridview.CurrentCell.Value.ToString();
                    if (string.IsNullOrEmpty(str.ToString()))
                    {
                        this.Cart_Gridview.CurrentCell.Value = 1;
                        this.Cart_Gridview.CurrentRow.Cells[5].Value = (1.0 * Convert.ToDouble(this.Cart_Gridview.CurrentRow.Cells[4].Value)).ToString();
                    }
                    else
                    {
                        double num4 = Convert.ToDouble(str);
                        if (num4 > 0.0)
                        {
                            this.Cart_Gridview.CurrentRow.Cells[5].Value = (num4 * Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[4].Value)).ToString();
                        }
                        else
                        {
                            this.Cart_Gridview.CurrentCell.Value = 1;
                            this.Cart_Gridview.CurrentRow.Cells[5].Value = (1.0 * Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[4].Value)).ToString();
                        }
                    }
                }
                this.Cart_Gridview.UpdateCellValue(2, this.Cart_Gridview.CurrentCell.RowIndex);
                this.Cart_Gridview.Update();
                this.Total();
                this.textBox4.Focus();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Cart_Gridview_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.Total();
        }

        private void Cart_Gridview_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            this.textBox4.Focus();
        }

        private void Cart_Gridview_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.Total();
            this.textBox4.Focus();
        }

        private void Cart_Gridview_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.Total();
            this.textBox4.Focus();
        }

        private void Cart_Gridview_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            this.Total();
            this.textBox4.Focus();
        }

        public void CheckCart()
        {
            int num = 0;
            int count = this.Cart_Gridview.Rows.Count;
            int num3 = 0;
            while (true)
            {
                if (num3 < count)
                {
                    if ((this.Cart_Gridview.Rows[num3].Cells[0].Value.ToString() != this.textBox4.Text.Trim()) && (this.Cart_Gridview.Rows[num3].Cells[1].Value.ToString() != this.textBox4.Text.ToString()))
                    {
                        num3++;
                        continue;
                    }
                    this.Cart_Gridview.Rows[num3].Cells[3].Value = int.Parse(this.Cart_Gridview.Rows[num3].Cells[3].Value.ToString()) + 1;
                    this.Cart_Gridview.Update();
                    num += num + 1;
                    this.Total();
                }
                if (num == 0)
                {
                    this.FindProducts();
                }
                return;
            }
        }

        public bool CheckIfNotEmpty() => 
            this.textBox4.Text != "";

        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ComboBox_ItemsSearchBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void ComboBox_ItemsSearchBox_TextChanged(object sender, EventArgs e)
        {
            base.AcceptButton = this.Btn_AddItem;
        }

        private void ComboBox_ItemsSearchBox_TextUpdate(object sender, EventArgs e)
        {
        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            this.Functionkeys(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void FindProducts()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT a.ProductCode,a.Description,b.SellingUnit,b.SellingUnitPrice,b.Disc,b.GSST,b.VAT,b.TAX3,a.StockBalance,Pprice FROM inventorymaster a,productprice b WHERE (a.ProductCode=@product and b.ProductCode=a.ProductCode) or (a.Description=@product and b.ProductCode=a.ProductCode);", connection);
                command.Parameters.AddWithValue("@product", this.textBox4.Text.Trim());
                MySqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("The Item Does Not Exist", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            connection.Close();
                            break;
                        }
                        int num = 0;
                        int count = this.Cart_Gridview.Rows.Count;
                        int num3 = 0;
                        while (true)
                        {
                            if (num3 < count)
                            {
                                if ((this.Cart_Gridview.Rows[num3].Cells[0].Value.ToString() != reader["ProductCode"].ToString()) && (this.Cart_Gridview.Rows[num3].Cells[1].Value.ToString() != reader["Description"].ToString()))
                                {
                                    num3++;
                                    continue;
                                }
                                this.Cart_Gridview.Rows[num3].Cells[3].Value = int.Parse(this.Cart_Gridview.Rows[num3].Cells[3].Value.ToString()) + 1;
                                num++;
                                this.Total();
                            }
                            if (num == 0)
                            {
                                if ((reader.GetString("SellingUnit") == "") || (reader.GetDouble("SellingUnitPrice") <= 0.0))
                                {
                                    MessageBox.Show("The Product Quantity In Stock Cannot Be Evaluated!!\n " + reader.GetString(5), "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    this.textBox4.Focus();
                                }
                                else
                                {
                                    int num4 = reader.GetInt32("SellingUnitPrice") - reader.GetInt32("Pprice");
                                    double num5 = reader.GetDouble("SellingUnitPrice") * (reader.GetDouble("Disc") / 100.0);
                                    object[] values = new object[14];
                                    values[0] = reader.GetString("ProductCode");
                                    values[1] = reader.GetString("Description");
                                    values[2] = reader.GetString("SellingUnit");
                                    values[3] = 1;
                                    values[4] = reader.GetString("SellingUnitPrice");
                                    values[5] = reader.GetString("SellingUnitPrice");
                                    values[6] = reader.GetString("Disc");
                                    values[7] = reader.GetString("GSST");
                                    values[8] = reader.GetString("VAT");
                                    values[9] = reader.GetString("TAX3");
                                    values[10] = num4;
                                    values[11] = 1;
                                    values[12] = num5;
                                    values[13] = "";
                                    this.Cart_Gridview.Rows.Add(values);
                                    this.textBox4.Focus();
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void Functionkeys(KeyEventArgs a)
        {
            switch (a.KeyCode)
            {
                case Keys.F1:
                    this.Btn_Close_Click(new object(), new EventArgs());
                    break;

                case Keys.F3:
                    this.OpenSearchForm();
                    break;

                case Keys.F4:
                    this.Btn_HoldCart_Click(new object(), new EventArgs());
                    break;

                case Keys.F6:
                    this.Btn_CartRetrieval_Click(new object(), new EventArgs());
                    break;

                case Keys.F8:
                    this.Btn_Quotation_Click(new object(), new EventArgs());
                    break;

                case Keys.F10:
                    this.Btn_Invoice_Click(new object(), new EventArgs());
                    break;

                case Keys.F12:
                    this.Btn_CheckouPayment_Click(new object(), new EventArgs());
                    break;

                default:
                    break;
            }
        }

        public void GenerateId()
        {
            try
            {
                this.TransactionCode = "";
                string str = Program.CurrentDateTime().ToString("ddHHmmssffff");
                this.TransactionCode = str;
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR OCCURED", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public static string GetTimestamp(DateTime value) => 
            value.ToString("HHmmssffff");

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainCart_MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearCartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.holdCartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreCartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_AddItem = new System.Windows.Forms.Button();
            this.Product_IdLabel = new System.Windows.Forms.Label();
            this.Panel_ProductSearch = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.Btn_CartRetrieval = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_Tax = new System.Windows.Forms.TextBox();
            this.Btn_Payments = new System.Windows.Forms.Button();
            this.Btn_HoldCart = new System.Windows.Forms.Button();
            this.Panel_salesOperations = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Btn_Invoice = new System.Windows.Forms.Button();
            this.Btn_Quotation = new System.Windows.Forms.Button();
            this.Cart_Gridview = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalProfit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MainCart_MenuStrip.SuspendLayout();
            this.Panel_ProductSearch.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Panel_salesOperations.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Cart_Gridview)).BeginInit();
            this.SuspendLayout();
            // 
            // MainCart_MenuStrip
            // 
            this.MainCart_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearCartToolStripMenuItem,
            this.holdCartToolStripMenuItem,
            this.restoreCartToolStripMenuItem});
            this.MainCart_MenuStrip.Name = "MainCart_MenuStrip";
            this.MainCart_MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainCart_MenuStrip.Size = new System.Drawing.Size(203, 70);
            this.MainCart_MenuStrip.Text = "Cart Management";
            this.MainCart_MenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainCart_MenuStrip_ItemClicked);
            // 
            // clearCartToolStripMenuItem
            // 
            this.clearCartToolStripMenuItem.Name = "clearCartToolStripMenuItem";
            this.clearCartToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
            this.clearCartToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.clearCartToolStripMenuItem.Text = "Clear Cart";
            // 
            // holdCartToolStripMenuItem
            // 
            this.holdCartToolStripMenuItem.Name = "holdCartToolStripMenuItem";
            this.holdCartToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.H)));
            this.holdCartToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.holdCartToolStripMenuItem.Text = "Hold Cart";
            // 
            // restoreCartToolStripMenuItem
            // 
            this.restoreCartToolStripMenuItem.Name = "restoreCartToolStripMenuItem";
            this.restoreCartToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.R)));
            this.restoreCartToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.restoreCartToolStripMenuItem.Text = "Restore Cart";
            // 
            // Btn_AddItem
            // 
            this.Btn_AddItem.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_AddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_AddItem.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_AddItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_AddItem.Location = new System.Drawing.Point(474, 7);
            this.Btn_AddItem.Name = "Btn_AddItem";
            this.Btn_AddItem.Size = new System.Drawing.Size(144, 30);
            this.Btn_AddItem.TabIndex = 13;
            this.Btn_AddItem.Text = "Add Item";
            this.Btn_AddItem.UseVisualStyleBackColor = false;
            this.Btn_AddItem.Click += new System.EventHandler(this.Btn_AddItem_Click);
            // 
            // Product_IdLabel
            // 
            this.Product_IdLabel.AutoSize = true;
            this.Product_IdLabel.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Product_IdLabel.Location = new System.Drawing.Point(9, 11);
            this.Product_IdLabel.Name = "Product_IdLabel";
            this.Product_IdLabel.Size = new System.Drawing.Size(88, 22);
            this.Product_IdLabel.TabIndex = 11;
            this.Product_IdLabel.Text = "Item Code";
            // 
            // Panel_ProductSearch
            // 
            this.Panel_ProductSearch.Controls.Add(this.textBox4);
            this.Panel_ProductSearch.Controls.Add(this.Btn_AddItem);
            this.Panel_ProductSearch.Controls.Add(this.Product_IdLabel);
            this.Panel_ProductSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_ProductSearch.Location = new System.Drawing.Point(0, 0);
            this.Panel_ProductSearch.Name = "Panel_ProductSearch";
            this.Panel_ProductSearch.Size = new System.Drawing.Size(1298, 43);
            this.Panel_ProductSearch.TabIndex = 18;
            // 
            // textBox4
            // 
            this.textBox4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(103, 9);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(365, 26);
            this.textBox4.TabIndex = 15;
            this.textBox4.TextChanged += new System.EventHandler(this.TextBox4_TextChanged);
            // 
            // Btn_CartRetrieval
            // 
            this.Btn_CartRetrieval.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_CartRetrieval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_CartRetrieval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_CartRetrieval.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_CartRetrieval.Location = new System.Drawing.Point(367, 5);
            this.Btn_CartRetrieval.Margin = new System.Windows.Forms.Padding(10);
            this.Btn_CartRetrieval.Name = "Btn_CartRetrieval";
            this.Btn_CartRetrieval.Size = new System.Drawing.Size(160, 40);
            this.Btn_CartRetrieval.TabIndex = 50;
            this.Btn_CartRetrieval.Text = "Retrieve Bill  [F6]";
            this.Btn_CartRetrieval.UseVisualStyleBackColor = false;
            this.Btn_CartRetrieval.Click += new System.EventHandler(this.Btn_CartRetrieval_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.panel2.Size = new System.Drawing.Size(1298, 60);
            this.panel2.TabIndex = 17;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Navy;
            this.textBox2.Location = new System.Drawing.Point(684, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(607, 59);
            this.textBox2.TabIndex = 57;
            this.textBox2.Text = "0";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Palatino Linotype", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(0)))), ((int)(((byte)(18)))));
            this.label11.Location = new System.Drawing.Point(558, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 58);
            this.label11.TabIndex = 39;
            this.label11.Text = "Net Amount";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Txt_Tax);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 58);
            this.panel1.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(364, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 20);
            this.label3.TabIndex = 59;
            this.label3.Text = "Discount";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Snow;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(450, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 30);
            this.textBox3.TabIndex = 60;
            this.textBox3.Text = "0";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(0)))), ((int)(((byte)(18)))));
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 57;
            this.label2.Text = "Gross";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Snow;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(61, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(136, 30);
            this.textBox1.TabIndex = 58;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.Texbox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(0)))), ((int)(((byte)(18)))));
            this.label1.Location = new System.Drawing.Point(203, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 55;
            this.label1.Text = "Tax";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Txt_Tax
            // 
            this.Txt_Tax.BackColor = System.Drawing.Color.Snow;
            this.Txt_Tax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Tax.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Tax.Location = new System.Drawing.Point(246, 14);
            this.Txt_Tax.Name = "Txt_Tax";
            this.Txt_Tax.ReadOnly = true;
            this.Txt_Tax.Size = new System.Drawing.Size(112, 30);
            this.Txt_Tax.TabIndex = 56;
            this.Txt_Tax.Text = "0";
            this.Txt_Tax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Btn_Payments
            // 
            this.Btn_Payments.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_Payments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Payments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Payments.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Payments.Location = new System.Drawing.Point(910, 5);
            this.Btn_Payments.Margin = new System.Windows.Forms.Padding(8);
            this.Btn_Payments.Name = "Btn_Payments";
            this.Btn_Payments.Size = new System.Drawing.Size(160, 40);
            this.Btn_Payments.TabIndex = 49;
            this.Btn_Payments.Text = "CheckOut [F12]";
            this.Btn_Payments.UseVisualStyleBackColor = false;
            this.Btn_Payments.Click += new System.EventHandler(this.Btn_CheckouPayment_Click);
            // 
            // Btn_HoldCart
            // 
            this.Btn_HoldCart.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_HoldCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_HoldCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_HoldCart.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_HoldCart.Location = new System.Drawing.Point(186, 5);
            this.Btn_HoldCart.Margin = new System.Windows.Forms.Padding(10);
            this.Btn_HoldCart.Name = "Btn_HoldCart";
            this.Btn_HoldCart.Size = new System.Drawing.Size(160, 40);
            this.Btn_HoldCart.TabIndex = 51;
            this.Btn_HoldCart.Text = "Hold bill  [F4]";
            this.Btn_HoldCart.UseVisualStyleBackColor = false;
            this.Btn_HoldCart.Click += new System.EventHandler(this.Btn_HoldCart_Click);
            // 
            // Panel_salesOperations
            // 
            this.Panel_salesOperations.BackColor = System.Drawing.Color.GhostWhite;
            this.Panel_salesOperations.Controls.Add(this.panel3);
            this.Panel_salesOperations.Controls.Add(this.panel2);
            this.Panel_salesOperations.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_salesOperations.Location = new System.Drawing.Point(0, 536);
            this.Panel_salesOperations.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.Panel_salesOperations.Name = "Panel_salesOperations";
            this.Panel_salesOperations.Size = new System.Drawing.Size(1298, 112);
            this.Panel_salesOperations.TabIndex = 19;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Btn_Close);
            this.panel3.Controls.Add(this.Btn_HoldCart);
            this.panel3.Controls.Add(this.Btn_Invoice);
            this.panel3.Controls.Add(this.Btn_Payments);
            this.panel3.Controls.Add(this.Btn_Quotation);
            this.panel3.Controls.Add(this.Btn_CartRetrieval);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(213, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1085, 52);
            this.panel3.TabIndex = 62;
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Close.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Close.Location = new System.Drawing.Point(5, 5);
            this.Btn_Close.Margin = new System.Windows.Forms.Padding(10);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(160, 40);
            this.Btn_Close.TabIndex = 61;
            this.Btn_Close.Text = "Close [F1]";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Btn_Invoice
            // 
            this.Btn_Invoice.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_Invoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Invoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Invoice.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Invoice.Location = new System.Drawing.Point(729, 5);
            this.Btn_Invoice.Margin = new System.Windows.Forms.Padding(10);
            this.Btn_Invoice.Name = "Btn_Invoice";
            this.Btn_Invoice.Size = new System.Drawing.Size(160, 40);
            this.Btn_Invoice.TabIndex = 59;
            this.Btn_Invoice.Text = "Invoice Bill [F10]";
            this.Btn_Invoice.UseVisualStyleBackColor = false;
            this.Btn_Invoice.Click += new System.EventHandler(this.Btn_Invoice_Click);
            // 
            // Btn_Quotation
            // 
            this.Btn_Quotation.BackColor = System.Drawing.Color.Chocolate;
            this.Btn_Quotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Quotation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Quotation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Quotation.Location = new System.Drawing.Point(548, 5);
            this.Btn_Quotation.Margin = new System.Windows.Forms.Padding(10);
            this.Btn_Quotation.Name = "Btn_Quotation";
            this.Btn_Quotation.Size = new System.Drawing.Size(160, 40);
            this.Btn_Quotation.TabIndex = 60;
            this.Btn_Quotation.Text = "Quotation [F8]";
            this.Btn_Quotation.UseVisualStyleBackColor = false;
            this.Btn_Quotation.Click += new System.EventHandler(this.Btn_Quotation_Click);
            // 
            // Cart_Gridview
            // 
            this.Cart_Gridview.AllowUserToAddRows = false;
            this.Cart_Gridview.AllowUserToResizeColumns = false;
            this.Cart_Gridview.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cart_Gridview.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Cart_Gridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Cart_Gridview.BackgroundColor = System.Drawing.Color.White;
            this.Cart_Gridview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSalmon;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Cart_Gridview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Cart_Gridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Cart_Gridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column9,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column8,
            this.Column7,
            this.Column13,
            this.Column14,
            this.Column10,
            this.TotalProfit,
            this.DiscAmount,
            this.Column6});
            this.Cart_Gridview.ContextMenuStrip = this.MainCart_MenuStrip;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Cart_Gridview.DefaultCellStyle = dataGridViewCellStyle16;
            this.Cart_Gridview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cart_Gridview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.Cart_Gridview.EnableHeadersVisualStyles = false;
            this.Cart_Gridview.Location = new System.Drawing.Point(0, 43);
            this.Cart_Gridview.Name = "Cart_Gridview";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.NullValue = "X";
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Cart_Gridview.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.Cart_Gridview.RowHeadersVisible = false;
            this.Cart_Gridview.RowHeadersWidth = 20;
            this.Cart_Gridview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Palatino Linotype", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.NullValue = null;
            this.Cart_Gridview.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.Cart_Gridview.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cart_Gridview.RowTemplate.Height = 26;
            this.Cart_Gridview.Size = new System.Drawing.Size(1298, 493);
            this.Cart_Gridview.TabIndex = 20;
            this.Cart_Gridview.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Cart_Gridview_CellContentClick);
            this.Cart_Gridview.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.Cart_Gridview_CellValidated);
            this.Cart_Gridview.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Cart_Gridview_CellValidating);
            this.Cart_Gridview.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Cart_Gridview_CellValueChanged);
            this.Cart_Gridview.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Cart_Gridview_EditingControlShowing);
            this.Cart_Gridview.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Cart_Gridview_RowsAdded);
            this.Cart_Gridview.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.Cart_Gridview_RowsRemoved);
            this.Cart_Gridview.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.Cart_Gridview_RowValidated);
            this.Cart_Gridview.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.Cart_Gridview_UserAddedRow);
            this.Cart_Gridview.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.Cart_Gridview_UserDeletedRow);
            this.Cart_Gridview.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.Cart_Gridview_UserDeletingRow);
            this.Cart_Gridview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Cart_Gridview_MouseClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.FillWeight = 30F;
            this.Column1.HeaderText = "Code";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.FillWeight = 62.55304F;
            this.Column2.HeaderText = "Description";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.FillWeight = 20F;
            this.Column9.HeaderText = "Unit";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column3.FillWeight = 20F;
            this.Column3.HeaderText = "Quantity";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            dataGridViewCellStyle6.Format = "N2";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column4.FillWeight = 20F;
            this.Column4.HeaderText = "UnitPrice";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            dataGridViewCellStyle7.Format = "N2";
            this.Column5.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column5.FillWeight = 25F;
            this.Column5.HeaderText = "Total";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column8
            // 
            dataGridViewCellStyle8.Format = "N2";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column8.FillWeight = 15F;
            this.Column8.HeaderText = "Disc%";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column7
            // 
            dataGridViewCellStyle9.Format = "N2";
            this.Column7.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column7.FillWeight = 15F;
            this.Column7.HeaderText = "Gsst%";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column13
            // 
            dataGridViewCellStyle10.Format = "N2";
            this.Column13.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column13.FillWeight = 20F;
            this.Column13.HeaderText = "Tax1%";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            // 
            // Column14
            // 
            dataGridViewCellStyle11.Format = "N2";
            this.Column14.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column14.FillWeight = 20F;
            this.Column14.HeaderText = "Tax2%";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Visible = false;
            // 
            // Column10
            // 
            dataGridViewCellStyle12.Format = "N2";
            this.Column10.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column10.FillWeight = 20F;
            this.Column10.HeaderText = "Profit";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            // 
            // TotalProfit
            // 
            dataGridViewCellStyle13.Format = "N2";
            this.TotalProfit.DefaultCellStyle = dataGridViewCellStyle13;
            this.TotalProfit.FillWeight = 20F;
            this.TotalProfit.HeaderText = "Tprofit";
            this.TotalProfit.Name = "TotalProfit";
            this.TotalProfit.ReadOnly = true;
            this.TotalProfit.Visible = false;
            // 
            // DiscAmount
            // 
            dataGridViewCellStyle14.Format = "N2";
            this.DiscAmount.DefaultCellStyle = dataGridViewCellStyle14;
            this.DiscAmount.FillWeight = 20F;
            this.DiscAmount.HeaderText = "DiscAmt";
            this.DiscAmount.Name = "DiscAmount";
            this.DiscAmount.ReadOnly = true;
            this.DiscAmount.Visible = false;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle15.NullValue = "Del";
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.DarkRed;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle15;
            this.Column6.FillWeight = 15F;
            this.Column6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column6.Text = "Del";
            this.Column6.ToolTipText = "Remove Item From Cart";
            this.Column6.UseColumnTextForButtonValue = true;
            this.Column6.Width = 40;
            // 
            // Sales
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1298, 648);
            this.ControlBox = false;
            this.Controls.Add(this.Cart_Gridview);
            this.Controls.Add(this.Panel_ProductSearch);
            this.Controls.Add(this.Panel_salesOperations);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Sales";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Sales_FormClosing);
            this.Load += new System.EventHandler(this.Sales_Load);
            this.Shown += new System.EventHandler(this.Sales_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            this.MainCart_MenuStrip.ResumeLayout(false);
            this.Panel_ProductSearch.ResumeLayout(false);
            this.Panel_ProductSearch.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Panel_salesOperations.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Cart_Gridview)).EndInit();
            this.ResumeLayout(false);

        }

        public void InsertInvoice(decimal Total, string Customerid)
        {
            MySqlTransaction transaction = null;
            try
            {
                try
                {
                    if ((Program.CurrLoggedInUser.UserID == null) || (Program.CurrLoggedInUser.UserFirstname == null))
                    {
                        MessageBox.Show("UserId is null! Cannot complete transaction!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                        connection.Open();
                        if (this.Cart_Gridview.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Records To Save !!!", "ERROR MESSAGE");
                        }
                        else
                        {
                            int num = 0;
                            transaction = connection.BeginTransaction();
                            MySqlCommand command = new MySqlCommand();
                            int num3 = 0;
                            while (true)
                            {
                                if (num3 >= this.Cart_Gridview.Rows.Count)
                                {
                                    command = new MySqlCommand("INSERT INTO invoicemaster (InvoiceNo,CustRef,ItemsNo,Total,status,Period,InvoiceDate,Vfrom,Vto,Counter,ReceivedBy) VALUES(@InvoiceNo,@CustRef,@ItemsNo,@Total,@status,@Period,@InvoiceDate,@Vfrom,@Vto,@WorkStationID,@ReceivedBy);", connection, transaction);
                                    command.Parameters.AddWithValue("@InvoiceNo", this.TransactionCode);
                                    command.Parameters.AddWithValue("@CustRef", Customerid);
                                    command.Parameters.AddWithValue("@ItemsNo", this.Cart_Gridview.Rows.Count);
                                    command.Parameters.AddWithValue("@Total", Total);
                                    command.Parameters.AddWithValue("@status", 0);
                                    command.Parameters.AddWithValue("@Period", this.InvoicePeriod);
                                    command.Parameters.AddWithValue("@InvoiceDate", Program.CurrentDateTime());
                                    command.Parameters.AddWithValue("@Vfrom", Program.CurrentDateTime());
                                    command.Parameters.AddWithValue("@Vto", Program.CurrentDateTime().AddDays((double) this.InvoicePeriod));
                                    command.Parameters.AddWithValue("@WorkStationID", Program.LogInCounter);
                                    command.Parameters.AddWithValue("@ReceivedBy", Program.CurrLoggedInUser.UserID);
                                    int num2 = command.ExecuteNonQuery();
                                    command.Parameters.Clear();
                                    command.Dispose();
                                    transaction.Commit();
                                    if (num == this.Cart_Gridview.Rows.Count)
                                    {
                                        this.GenerateId();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Warning !!", "MessageBox", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        this.GenerateId();
                                    }
                                    break;
                                }
                                command = new MySqlCommand("INSERT INTO invoiceitems (InvoiceNo,ProductCode,Description,Quantity,Unit,UnitPrice,Total,Profit,Date) VALUES(@InvoiceNo,@ProductCode,@Description,@Quantity,@Unit,@UnitPrice,@Total,@Profit,@Date)", connection, transaction);
                                command.Parameters.AddWithValue("@InvoiceNo", this.TransactionCode);
                                command.Parameters.AddWithValue("@ProductCode", this.Cart_Gridview.Rows[num3].Cells[0].Value);
                                command.Parameters.AddWithValue("@Description", this.Cart_Gridview.Rows[num3].Cells[1].Value);
                                command.Parameters.AddWithValue("@Quantity", this.Cart_Gridview.Rows[num3].Cells[3].Value);
                                command.Parameters.AddWithValue("@Unit", this.Cart_Gridview.Rows[num3].Cells[2].Value);
                                command.Parameters.AddWithValue("@UnitPrice", this.Cart_Gridview.Rows[num3].Cells[4].Value);
                                command.Parameters.AddWithValue("@Total", this.Cart_Gridview.Rows[num3].Cells[5].Value);
                                command.Parameters.AddWithValue("@Profit", this.Cart_Gridview.Rows[num3].Cells[9].Value);
                                command.Parameters.AddWithValue("@Date", Program.CurrentDateTime());
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                                command.Dispose();
                                num++;
                                num3++;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    MessageBox.Show("The following error occured:\n" + exception.Message, "ERROR OCCURED", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.GenerateId();
                }
            }
            finally
            {
            }
        }

        public void InsertQuotation(decimal Total, string Customerid)
        {
            MySqlTransaction transaction = null;
            try
            {
                try
                {
                    if ((Program.CurrLoggedInUser.UserID == null) || (Program.CurrLoggedInUser.UserFirstname == null))
                    {
                        MessageBox.Show("UserId is null! Cannot complete transaction!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                        connection.Open();
                        if (this.Cart_Gridview.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Records To Save !!!", "ERROR MESSAGE");
                        }
                        else
                        {
                            int num = 0;
                            transaction = connection.BeginTransaction();
                            MySqlCommand command = new MySqlCommand();
                            int num3 = 0;
                            while (true)
                            {
                                if (num3 >= this.Cart_Gridview.Rows.Count)
                                {
                                    command = new MySqlCommand("INSERT INTO quotationmaster (QuotationNo,ItemsNo,Gross,CustomerRef,DateQuoted,WorkStationID,ReceivedBy) VALUES(@QuotationNo,@ItemsNo,@Gross,@CustomerRef,@DateQuoted,@WorkStationID,@ReceivedBy);", connection, transaction);
                                    command.Parameters.AddWithValue("@QuotationNo", this.TransactionCode);
                                    command.Parameters.AddWithValue("@ItemsNo", this.Cart_Gridview.Rows.Count);
                                    command.Parameters.AddWithValue("@Gross", Total);
                                    command.Parameters.AddWithValue("@CustomerRef", Customerid);
                                    command.Parameters.AddWithValue("@WorkStationID", Program.LogInCounter);
                                    command.Parameters.AddWithValue("@ReceivedBy", Program.CurrLoggedInUser.UserID);
                                    command.Parameters.AddWithValue("@DateQuoted", Program.CurrentDateTime());
                                    int num2 = command.ExecuteNonQuery();
                                    command.Parameters.Clear();
                                    command.Dispose();
                                    transaction.Commit();
                                    if (num != this.Cart_Gridview.Rows.Count)
                                    {
                                        this.GenerateId();
                                    }
                                    else
                                    {
                                        this.Cart_Gridview.Rows.Clear();
                                        MessageBox.Show("SUCCESS !!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        this.GenerateId();
                                    }
                                    break;
                                }
                                command = new MySqlCommand("INSERT INTO quotationitems (QuotationNo,ProductCode,Description,Quantity,Unit,UnitPrice,Total,Profit,DateQuoted) VALUES(@QuotationNo,@ProductCode,@Description,@Quantity,@Unit,@UnitPrice,@Total,@Profit,@DateQuoted)", connection, transaction);
                                command.Parameters.AddWithValue("@QuotationNo", this.Cart_Gridview.Rows[num3].Cells[0].Value);
                                command.Parameters.AddWithValue("@ProductCode", this.Cart_Gridview.Rows[num3].Cells[0].Value);
                                command.Parameters.AddWithValue("@Description", this.Cart_Gridview.Rows[num3].Cells[1].Value);
                                command.Parameters.AddWithValue("@Quantity", this.Cart_Gridview.Rows[num3].Cells[3].Value);
                                command.Parameters.AddWithValue("@Unit", this.Cart_Gridview.Rows[num3].Cells[2].Value);
                                command.Parameters.AddWithValue("@UnitPrice", this.Cart_Gridview.Rows[num3].Cells[4].Value);
                                command.Parameters.AddWithValue("@Total", this.Cart_Gridview.Rows[num3].Cells[5].Value);
                                command.Parameters.AddWithValue("@Profit", this.Cart_Gridview.Rows[num3].Cells[9].Value);
                                command.Parameters.AddWithValue("@DateQuoted", Program.CurrentDateTime());
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                                command.Dispose();
                                num++;
                                num3++;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    MessageBox.Show("The following error occured:\n\n" + exception.Message, "ERROR OCCURED", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.GenerateId();
                }
            }
            finally
            {
            }
        }

        public void InsertSale(DataTable Dt)
        {
            MySqlTransaction tr = null;
            try
            {
                if ((Program.CurrLoggedInUser.UserID == null) || (Program.CurrLoggedInUser.UserFirstname == null))
                {
                    MessageBox.Show("UserId is null! Cannot complete transaction!", "WARNING MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MySqlConnection con = new MySqlConnection(this.Db.DBConnecString());
                    con.Open();
                    if (this.Cart_Gridview.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Records To Save !!!", "ERROR MESSAGE");
                    }
                    else if (this.AmountPaid < this.GrossTotal)
                    {
                        MessageBox.Show("The Amount Paid Is Less !!", "ERROR MESSAGE");
                    }
                    else
                    {
                        int num = 0;
                        tr = con.BeginTransaction();
                        int count = Dt.Rows.Count;
                        int num3 = 0;
                        if (!this.Acc.InsertBillmaster(this.TransactionCode, this.GrossTotal, this.AmountPaid, this.Balance, this.Total_Profit, this.DiscountAmount, TaxPercentage, this.TaxAmt, this.PointsAwarded, Program.CurrentDateTime(), this.CustomerID, Program.CurrLoggedInUser.UserID, Program.LogInCounter, Program.LogInBranch, con, tr))
                        {
                            tr.Rollback();
                            MessageBox.Show("Bill saving failed...!!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            int num4 = 0;
                            while (true)
                            {
                                if (num4 >= count)
                                {
                                    if (num3 != 0)
                                    {
                                        MessageBox.Show("Cannot Save the accounting Of the Bill!", "ERROR MESSAGE");
                                        tr.Rollback();
                                    }
                                    else
                                    {
                                        MySqlCommand command = new MySqlCommand();
                                        int num5 = 0;
                                        while (true)
                                        {
                                            if (num5 >= this.Cart_Gridview.Rows.Count)
                                            {
                                                if (num == this.Cart_Gridview.Rows.Count)
                                                {
                                                    tr.Commit();
                                                }
                                                else
                                                {
                                                    tr.Rollback();
                                                    MessageBox.Show("Failed to save the items of the bil...!!", "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                                }
                                                break;
                                            }
                                            command = new MySqlCommand("INSERT INTO itemsales (ProductCode,Description,Quantity,Unit,UnitPrice,Gross,CustomerRef,TransDate,WorkStationID,TransNo,ReceivedBy,Branchcode,Profit,Discount) VALUES(@ProductCode,@Description,@Quantity,@Unit,@UnitPrice,@Gross,@CustomerRef,@TransDate,@WorkStationID,@TransNo,@ReceivedBy,@Branchcode,@profit,@discount)", con, tr);
                                            command.Parameters.AddWithValue("@ProductCode", this.Cart_Gridview.Rows[num5].Cells[0].Value);
                                            command.Parameters.AddWithValue("@Description", this.Cart_Gridview.Rows[num5].Cells[1].Value);
                                            command.Parameters.AddWithValue("@Quantity", this.Cart_Gridview.Rows[num5].Cells[3].Value);
                                            command.Parameters.AddWithValue("@Unit", this.Cart_Gridview.Rows[num5].Cells[2].Value);
                                            command.Parameters.AddWithValue("@UnitPrice", this.Cart_Gridview.Rows[num5].Cells[4].Value);
                                            command.Parameters.AddWithValue("@Gross", this.Cart_Gridview.Rows[num5].Cells[5].Value);
                                            command.Parameters.AddWithValue("@CustomerRef", this.CustomerID);
                                            command.Parameters.AddWithValue("@TransDate", Program.CurrentDateTime());
                                            command.Parameters.AddWithValue("@WorkStationID", Program.LogInCounter);
                                            command.Parameters.AddWithValue("@TransNo", this.TransactionCode);
                                            command.Parameters.AddWithValue("@ReceivedBy", Program.CurrLoggedInUser.UserID);
                                            command.Parameters.AddWithValue("@Branchcode", (int) 100);
                                            command.Parameters.AddWithValue("@profit", this.Cart_Gridview.Rows[num5].Cells[11].Value);
                                            command.Parameters.AddWithValue("@discount", this.Cart_Gridview.Rows[num5].Cells[12].Value);
                                            int num6 = command.ExecuteNonQuery();
                                            command.Parameters.Clear();
                                            command.Dispose();
                                            command = new MySqlCommand("update inventorymaster set StockBalance=if ( StockBalance > @quantity, StockBalance-@quantity , StockBalance ) where ProductCode=@ProductCode;", con, tr);
                                            command.Parameters.AddWithValue("@ProductCode", this.Cart_Gridview.Rows[num5].Cells[0].Value.ToString());
                                            command.Parameters.AddWithValue("@quantity", this.Cart_Gridview.Rows[num5].Cells[3].Value);
                                            int num7 = command.ExecuteNonQuery();
                                            command.Parameters.Clear();
                                            command.Dispose();
                                            if ((num6 > 0) && (num7 > 0))
                                            {
                                                num++;
                                            }
                                            num5++;
                                        }
                                    }
                                    break;
                                }
                                try
                                {
                                    if (!this.Acc.InsertAccounts(this.TransactionCode, "Receivable", Dt.Rows[num4][0].ToString(), Convert.ToDecimal(Dt.Rows[num4][1].ToString()), Dt.Rows[num4][2].ToString(), Program.CurrLoggedInUser.UserID, Program.LogInCounter, con, tr))
                                    {
                                        num3 = 1;
                                    }
                                }
                                catch (Exception exception1)
                                {
                                    MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    num3 = 1;
                                }
                                num4++;
                            }
                        }
                    }
                }
            }
            catch (Exception exception2)
            {
                tr.Rollback();
                MessageBox.Show("The following error occured:\n\n" + exception2.Message, "ERROR OCCURED", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void LoadAutocompleteProducts()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT a.ProductCode,a.Description FROM inventorymaster a;";
                command.Parameters.AddWithValue("@status", "On");
                MySqlDataReader reader = command.ExecuteReader();
                Program.ProductsItemsList.Clear();
                if (!reader.HasRows)
                {
                    MessageBox.Show("No Products Have Been Found !!", "Loading items...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            this.Autocom.AddRange(Program.ProductsItemsList.ToArray());
                            break;
                        }
                        Program.ProductsItemsList.Add(reader[0].ToString());
                        Program.ProductsItemsList.Add(reader[1].ToString());
                    }
                }
                command.Dispose();
                reader.Dispose();
                connection.Close();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE");
            }
        }

        public void LoadReceiptSettings()
        {
            Title = this.Client.ClientTitle;
            Box = this.Client.ClientAddress;
            Email = this.Client.ClientEmail;
            Tel = this.Client.ClientTel;
            TaxPercentage = this.Client.ClientTaxRate;
            Pin = this.Client.ClientPin;
            Text1 = this.Client.ClientText1;
            Text2 = this.Client.ClientText2;
            Text3 = this.Client.ClientText3;
            Text4 = this.Client.ClientText4;
        }

        private void MainCart_MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.MainCart_MenuStrip.Close();
                if (e.ClickedItem.Name == "clearCartToolStripMenuItem")
                {
                    if (MessageBox.Show("Are you sure yo want to clear all items on cart?", "MESSAGE BOX", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        this.Cart_Gridview.Rows.Clear();
                    }
                }
                else if (e.ClickedItem.Name == "holdCartToolStripMenuItem")
                {
                    if (MessageBox.Show("Are you sure yo want to hold items on cart?", "MESSAGE BOX", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        this.Btn_HoldCart_Click(new object(), new EventArgs());
                    }
                }
                else if (e.ClickedItem.Name == "restoreCartToolStripMenuItem")
                {
                    this.Btn_CartRetrieval_Click(new object(), new EventArgs());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("The following error occured:\n" + exception.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void OpenSearchForm()
        {
            new SearchProduct { textBox1 = { AutoCompleteSource = this.textBox4.AutoCompleteSource } }.ShowDialog(this);
        }

        public void PrintReceipt()
        {
            try
            {
                PrintDocument document = new PrintDocument();
                document.PrintPage += new PrintPageEventHandler(this.ProvideContent);
                document.PrintController = new StandardPrintController();
                document.Print();
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Failed to print Receipt", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ProvideContent(object sender, PrintPageEventArgs e)
        {
            ReceiptData data = new ReceiptData();
            int num2 = 0;
            while (true)
            {
                if (num2 >= this.Cart_Gridview.Rows.Count)
                {
                    Graphics graphics = e.Graphics;
                    int num = 10;
                    StringFormat format1 = new StringFormat();
                    format1.LineAlignment = StringAlignment.Center;
                    format1.Alignment = StringAlignment.Center;
                    StringFormat format = format1;
                    graphics.DrawString(Title.ToUpper(), new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 20;
                    graphics.DrawString(Box, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 20;
                    graphics.DrawString(Tel, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 20;
                    graphics.DrawString(Email, new Font("Arial", 10f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 20;
                    graphics.DrawString(Pin.ToUpper(), new Font("Arial", 12f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 20;
                    graphics.DrawString("Sales Receipt", new Font("Palatino Linotype", 15f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    graphics.DrawString("____________", new Font("Palatino Linotype", 15f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                    num += 15;
                    graphics.DrawString("BillNo:" + this.TransactionCode, new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
                    num += 20;
                    graphics.DrawString("Date:" + Program.CurrentDateTime().ToShortDateString(), new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
                    graphics.DrawString("Counter : " + Program.LogInCounter, new Font("Arial", 10f), new SolidBrush(Color.Black), 120f, (float) num);
                    num += 20;
                    graphics.DrawString("Time : " + Program.CurrentDateTime().ToShortTimeString(), new Font("Arial", 10f, FontStyle.Regular), new SolidBrush(Color.Black), 10f, (float) num);
                    graphics.DrawString("Served By: " + Program.CurrLoggedInUser.UserFirstname, new Font("Arial", 10f), new SolidBrush(Color.Black), 120f, (float) num);
                    num += 10;
                    graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                    num += 10;
                    graphics.DrawString("Item                              Qty Price    Total", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float) num);
                    graphics.DrawString("______________________________________", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                    num += 20;
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= data.DataTable1.Rows.Count)
                        {
                            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float) num);
                            num += 15;
                            graphics.DrawString("TOTAL :", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
                            graphics.DrawString(this.GrossTotal.ToString("N2"), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 150f, (float) num);
                            num += 20;
                            graphics.DrawString("Amount Paid :", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
                            graphics.DrawString(this.AmountPaid.ToString("N2"), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 150f, (float) num);
                            num += 20;
                            graphics.DrawString("Balance", new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), 50f, (float) num);
                            graphics.DrawString(this.Balance.ToString("N2"), new Font("Arial", 12f, FontStyle.Bold), new SolidBrush(Color.Black), 150f, (float) num);
                            num += 10;
                            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                            num += 15;
                            graphics.DrawString("Tax%        TaxAmt", new Font("Arial", 10f, FontStyle.Underline), new SolidBrush(Color.Black), 70f, (float) num);
                            num += 15;
                            graphics.DrawString(TaxPercentage.ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 80f, (float) num);
                            graphics.DrawString(this.TaxAmt.ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 135f, (float) num);
                            num += 10;
                            graphics.DrawString("----------------------------------------------------------------", new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                            num += 20;
                            graphics.DrawString(Text1, new Font("Arial", 10f, FontStyle.Bold), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                            num += 20;
                            graphics.DrawString(Text2, new Font("Arial", 10f, FontStyle.Italic), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                            num += 15;
                            graphics.DrawString(Text3, new Font("Arial", 8f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                            num += 15;
                            graphics.DrawString(Text4, new Font("Arial", 8f), new SolidBrush(Color.Black), (float) this.Center_X, (float) num, format);
                            return;
                        }
                        if (data.DataTable1.Rows[num3][0].ToString().Length <= 0x1f)
                        {
                            graphics.DrawString(data.DataTable1.Rows[num3][0].ToString(), new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                        }
                        else
                        {
                            Array array = data.DataTable1.Rows[num3][0].ToString().ToCharArray(0, 30);
                            string s = "";
                            int index = 0;
                            while (true)
                            {
                                string text1;
                                if (index >= array.Length)
                                {
                                    graphics.DrawString(s, new Font("Arial", 10f), new SolidBrush(Color.Black), 10f, (float) num);
                                    break;
                                }
                                object obj1 = array.GetValue(index);
                                if (obj1 != null)
                                {
                                    text1 = obj1.ToString();
                                }
                                else
                                {
                                    object local1 = obj1;
                                    text1 = null;
                                }
                                s = s + text1;
                                index++;
                            }
                        }
                        num += 15;
                        string[] textArray1 = new string[] { "                                                     ", data.DataTable1.Rows[num3][1].ToString().Trim(), " *  ", data.DataTable1.Rows[num3][2].ToString(), "   ", decimal.Parse(data.DataTable1.Rows[num3][3].ToString()).ToString("N2") };
                        graphics.DrawString(string.Concat(textArray1), new Font("Arial", 8f), new SolidBrush(Color.Black), 0f, (float) num);
                        num += 15;
                        num3++;
                    }
                }
                object[] values = new object[] { this.Cart_Gridview.Rows[num2].Cells[1].Value.ToString(), this.Cart_Gridview.Rows[num2].Cells[3].Value.ToString(), this.Cart_Gridview.Rows[num2].Cells[4].Value.ToString(), this.Cart_Gridview.Rows[num2].Cells[5].Value.ToString() };
                data.DataTable1.Rows.Add(values);
                num2++;
            }
        }

        public void ResetForm()
        {
            this.Cart_Gridview.Rows.Clear();
            this.textBox4.Text = "";
            this.textBox1.Text = "0.0";
            this.Txt_Tax.Text = "0.0";
        }

        private void Sales_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((this.Cart_Gridview.Rows.Count > 0) && (MessageBox.Show("You have unsaved items in the cart!.Are You Sure You Want to Close?", "WARNING MESSAGE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
            {
                e.Cancel = true;
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            this.GenerateId();
            base.ActiveControl = this.textBox4;
        }

        private void Sales_Shown(object sender, EventArgs e)
        {
            this.LoadAutocompleteProducts();
            this.BindComboBox();
            this.textBox4.AutoCompleteCustomSource = this.Autocom;
        }

        private void Texbox1_TextChanged(object sender, EventArgs e)
        {
            double num;
            if (double.TryParse(this.textBox1.Text, out num))
            {
                this.Txt_Tax.Text = Convert.ToDouble((double) ((14.0 * num) / 100.0)).ToString("N2");
                this.textBox2.Text = this.textBox1.Text;
            }
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            base.AcceptButton = this.Btn_AddItem;
        }

        public void Total()
        {
            try
            {
                if (this.Cart_Gridview.Rows.Count > 0)
                {
                    int rowIndex = 0;
                    while (true)
                    {
                        if (rowIndex >= this.Cart_Gridview.Rows.Count)
                        {
                            break;
                        }
                        double num2 = Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[3].Value);
                        if (string.IsNullOrEmpty(num2.ToString()))
                        {
                            this.Cart_Gridview.Rows[rowIndex].Cells[3].Value = 1;
                        }
                        else if (num2 <= 0.0)
                        {
                            this.Cart_Gridview.Rows[rowIndex].Cells[3].Value = 1;
                        }
                        double num3 = num2 * Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[4].Value);
                        this.Cart_Gridview.Rows[rowIndex].Cells[5].Value = num3;
                        this.Cart_Gridview.UpdateCellValue(5, rowIndex);
                        double num4 = num2 * Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[10].Value);
                        this.Cart_Gridview.Rows[rowIndex].Cells[11].Value = num4;
                        this.Cart_Gridview.UpdateCellValue(11, rowIndex);
                        double num5 = Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[5].Value) * (Convert.ToDouble(this.Cart_Gridview.Rows[rowIndex].Cells[6].Value) / 100.0);
                        this.Cart_Gridview.Rows[rowIndex].Cells[12].Value = num5;
                        this.Cart_Gridview.UpdateCellValue(12, rowIndex);
                        this.Total_Profit += Convert.ToDecimal(this.Cart_Gridview.Rows[rowIndex].Cells[11].Value.ToString());
                        rowIndex++;
                    }
                }
                if (this.Cart_Gridview.Rows.Count <= 0)
                {
                    this.textBox1.Text = "0.00";
                    this.textBox2.Text = "0.00";
                    this.textBox3.Text = "0.00";
                    this.Txt_Tax.Text = "0.00";
                }
                else
                {
                    double num6 = double.Parse(0.ToString());
                    double num7 = double.Parse(0.ToString());
                    int num9 = 0;
                    while (true)
                    {
                        if (num9 >= this.Cart_Gridview.Rows.Count)
                        {
                            this.textBox1.Text = num7.ToString("N2");
                            this.textBox3.Text = num6.ToString("N2");
                            this.textBox2.Text = (num7 - num6).ToString("N2");
                            break;
                        }
                        num7 += Convert.ToDouble(this.Cart_Gridview.Rows[num9].Cells[5].Value.ToString());
                        num6 += Convert.ToDouble(this.Cart_Gridview.Rows[num9].Cells["DiscAmount"].Value.ToString());
                        num9++;
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}

