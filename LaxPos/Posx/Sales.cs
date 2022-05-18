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
            this.components = new Container();
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            DataGridViewCellStyle style4 = new DataGridViewCellStyle();
            DataGridViewCellStyle style5 = new DataGridViewCellStyle();
            DataGridViewCellStyle style6 = new DataGridViewCellStyle();
            DataGridViewCellStyle style7 = new DataGridViewCellStyle();
            DataGridViewCellStyle style8 = new DataGridViewCellStyle();
            DataGridViewCellStyle style9 = new DataGridViewCellStyle();
            DataGridViewCellStyle style10 = new DataGridViewCellStyle();
            DataGridViewCellStyle style11 = new DataGridViewCellStyle();
            DataGridViewCellStyle style12 = new DataGridViewCellStyle();
            DataGridViewCellStyle style13 = new DataGridViewCellStyle();
            DataGridViewCellStyle style14 = new DataGridViewCellStyle();
            DataGridViewCellStyle style15 = new DataGridViewCellStyle();
            DataGridViewCellStyle style16 = new DataGridViewCellStyle();
            DataGridViewCellStyle style17 = new DataGridViewCellStyle();
            DataGridViewCellStyle style18 = new DataGridViewCellStyle();
            this.MainCart_MenuStrip = new ContextMenuStrip(this.components);
            this.clearCartToolStripMenuItem = new ToolStripMenuItem();
            this.holdCartToolStripMenuItem = new ToolStripMenuItem();
            this.restoreCartToolStripMenuItem = new ToolStripMenuItem();
            this.Btn_AddItem = new Button();
            this.Product_IdLabel = new Label();
            this.Panel_ProductSearch = new Panel();
            this.textBox4 = new TextBox();
            this.Btn_CartRetrieval = new Button();
            this.panel2 = new Panel();
            this.textBox2 = new TextBox();
            this.label11 = new Label();
            this.panel1 = new Panel();
            this.label3 = new Label();
            this.textBox3 = new TextBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.label1 = new Label();
            this.Txt_Tax = new TextBox();
            this.Btn_Payments = new Button();
            this.Btn_HoldCart = new Button();
            this.Panel_salesOperations = new Panel();
            this.panel3 = new Panel();
            this.Btn_Close = new Button();
            this.Btn_Invoice = new Button();
            this.Btn_Quotation = new Button();
            this.Cart_Gridview = new DataGridView();
            this.Column1 = new DataGridViewTextBoxColumn();
            this.Column2 = new DataGridViewTextBoxColumn();
            this.Column9 = new DataGridViewTextBoxColumn();
            this.Column3 = new DataGridViewTextBoxColumn();
            this.Column4 = new DataGridViewTextBoxColumn();
            this.Column5 = new DataGridViewTextBoxColumn();
            this.Column8 = new DataGridViewTextBoxColumn();
            this.Column7 = new DataGridViewTextBoxColumn();
            this.Column13 = new DataGridViewTextBoxColumn();
            this.Column14 = new DataGridViewTextBoxColumn();
            this.Column10 = new DataGridViewTextBoxColumn();
            this.TotalProfit = new DataGridViewTextBoxColumn();
            this.DiscAmount = new DataGridViewTextBoxColumn();
            this.Column6 = new DataGridViewButtonColumn();
            this.MainCart_MenuStrip.SuspendLayout();
            this.Panel_ProductSearch.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Panel_salesOperations.SuspendLayout();
            this.panel3.SuspendLayout();
            ((ISupportInitialize) this.Cart_Gridview).BeginInit();
            base.SuspendLayout();
            ToolStripItem[] toolStripItems = new ToolStripItem[] { this.clearCartToolStripMenuItem, this.holdCartToolStripMenuItem, this.restoreCartToolStripMenuItem };
            this.MainCart_MenuStrip.Items.AddRange(toolStripItems);
            this.MainCart_MenuStrip.Name = "MainCart_MenuStrip";
            this.MainCart_MenuStrip.RenderMode = ToolStripRenderMode.System;
            this.MainCart_MenuStrip.Size = new Size(0xcb, 70);
            this.MainCart_MenuStrip.Text = "Cart Management";
            this.MainCart_MenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.MainCart_MenuStrip_ItemClicked);
            this.clearCartToolStripMenuItem.Name = "clearCartToolStripMenuItem";
            this.clearCartToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Control | Keys.C;
            this.clearCartToolStripMenuItem.Size = new Size(0xca, 0x16);
            this.clearCartToolStripMenuItem.Text = "Clear Cart";
            this.holdCartToolStripMenuItem.Name = "holdCartToolStripMenuItem";
            this.holdCartToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Control | Keys.H;
            this.holdCartToolStripMenuItem.Size = new Size(0xca, 0x16);
            this.holdCartToolStripMenuItem.Text = "Hold Cart";
            this.restoreCartToolStripMenuItem.Name = "restoreCartToolStripMenuItem";
            this.restoreCartToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Control | Keys.R;
            this.restoreCartToolStripMenuItem.Size = new Size(0xca, 0x16);
            this.restoreCartToolStripMenuItem.Text = "Restore Cart";
            this.Btn_AddItem.BackColor = Color.Chocolate;
            this.Btn_AddItem.FlatStyle = FlatStyle.Flat;
            this.Btn_AddItem.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_AddItem.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_AddItem.Location = new Point(0x1da, 7);
            this.Btn_AddItem.Name = "Btn_AddItem";
            this.Btn_AddItem.Size = new Size(0x90, 30);
            this.Btn_AddItem.TabIndex = 13;
            this.Btn_AddItem.Text = "Add Item";
            this.Btn_AddItem.UseVisualStyleBackColor = false;
            this.Btn_AddItem.Click += new EventHandler(this.Btn_AddItem_Click);
            this.Product_IdLabel.AutoSize = true;
            this.Product_IdLabel.Font = new Font("Palatino Linotype", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Product_IdLabel.Location = new Point(9, 11);
            this.Product_IdLabel.Name = "Product_IdLabel";
            this.Product_IdLabel.Size = new Size(0x58, 0x16);
            this.Product_IdLabel.TabIndex = 11;
            this.Product_IdLabel.Text = "Item Code";
            this.Panel_ProductSearch.Controls.Add(this.textBox4);
            this.Panel_ProductSearch.Controls.Add(this.Btn_AddItem);
            this.Panel_ProductSearch.Controls.Add(this.Product_IdLabel);
            this.Panel_ProductSearch.Dock = DockStyle.Top;
            this.Panel_ProductSearch.Location = new Point(0, 0);
            this.Panel_ProductSearch.Name = "Panel_ProductSearch";
            this.Panel_ProductSearch.Size = new Size(0x512, 0x2b);
            this.Panel_ProductSearch.TabIndex = 0x12;
            this.textBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.textBox4.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox4.Location = new Point(0x67, 9);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0x16d, 0x1a);
            this.textBox4.TabIndex = 15;
            this.textBox4.TextChanged += new EventHandler(this.TextBox4_TextChanged);
            this.Btn_CartRetrieval.BackColor = Color.Chocolate;
            this.Btn_CartRetrieval.FlatStyle = FlatStyle.Flat;
            this.Btn_CartRetrieval.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_CartRetrieval.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_CartRetrieval.Location = new Point(0x16f, 5);
            this.Btn_CartRetrieval.Margin = new Padding(10);
            this.Btn_CartRetrieval.Name = "Btn_CartRetrieval";
            this.Btn_CartRetrieval.Size = new Size(160, 40);
            this.Btn_CartRetrieval.TabIndex = 50;
            this.Btn_CartRetrieval.Text = "Retrieve Bill  [F6]";
            this.Btn_CartRetrieval.UseVisualStyleBackColor = false;
            this.Btn_CartRetrieval.Click += new EventHandler(this.Btn_CartRetrieval_Click);
            this.panel2.BorderStyle = BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Margin = new Padding(3, 3, 0, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new Padding(0, 0, 5, 0);
            this.panel2.Size = new Size(0x512, 60);
            this.panel2.TabIndex = 0x11;
            this.textBox2.BackColor = Color.White;
            this.textBox2.BorderStyle = BorderStyle.FixedSingle;
            this.textBox2.Dock = DockStyle.Fill;
            this.textBox2.Font = new Font("Microsoft Sans Serif", 34f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox2.ForeColor = Color.Navy;
            this.textBox2.Location = new Point(0x2ac, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(0x25f, 0x3b);
            this.textBox2.TabIndex = 0x39;
            this.textBox2.Text = "0";
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            this.label11.Dock = DockStyle.Left;
            this.label11.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label11.ForeColor = Color.FromArgb(0x5e, 0, 0x12);
            this.label11.Location = new Point(0x22e, 0);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x7e, 0x3a);
            this.label11.TabIndex = 0x27;
            this.label11.Text = "Net Amount";
            this.label11.TextAlign = ContentAlignment.MiddleCenter;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Txt_Tax);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x22e, 0x3a);
            this.panel1.TabIndex = 0x3b;
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label3.ForeColor = Color.Black;
            this.label3.Location = new Point(0x16c, 13);
            this.label3.Name = "label3";
            this.label3.Size = new Size(80, 20);
            this.label3.TabIndex = 0x3b;
            this.label3.Text = "Discount";
            this.label3.TextAlign = ContentAlignment.TopCenter;
            this.textBox3.BackColor = Color.Snow;
            this.textBox3.BorderStyle = BorderStyle.FixedSingle;
            this.textBox3.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox3.Location = new Point(450, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new Size(100, 30);
            this.textBox3.TabIndex = 60;
            this.textBox3.Text = "0";
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            this.label2.AutoSize = true;
            this.label2.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label2.ForeColor = Color.FromArgb(0x5e, 0, 0x12);
            this.label2.Location = new Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x39, 20);
            this.label2.TabIndex = 0x39;
            this.label2.Text = "Gross";
            this.textBox1.BackColor = Color.Snow;
            this.textBox1.BorderStyle = BorderStyle.FixedSingle;
            this.textBox1.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.textBox1.Location = new Point(0x3d, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new Size(0x88, 30);
            this.textBox1.TabIndex = 0x3a;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            this.textBox1.TextChanged += new EventHandler(this.Texbox1_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.label1.ForeColor = Color.FromArgb(0x5e, 0, 0x12);
            this.label1.Location = new Point(0xcb, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x25, 20);
            this.label1.TabIndex = 0x37;
            this.label1.Text = "Tax";
            this.label1.TextAlign = ContentAlignment.TopCenter;
            this.Txt_Tax.BackColor = Color.Snow;
            this.Txt_Tax.BorderStyle = BorderStyle.FixedSingle;
            this.Txt_Tax.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Txt_Tax.Location = new Point(0xf6, 14);
            this.Txt_Tax.Name = "Txt_Tax";
            this.Txt_Tax.ReadOnly = true;
            this.Txt_Tax.Size = new Size(0x70, 30);
            this.Txt_Tax.TabIndex = 0x38;
            this.Txt_Tax.Text = "0";
            this.Txt_Tax.TextAlign = HorizontalAlignment.Center;
            this.Btn_Payments.BackColor = Color.Chocolate;
            this.Btn_Payments.FlatStyle = FlatStyle.Flat;
            this.Btn_Payments.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Payments.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Payments.Location = new Point(910, 5);
            this.Btn_Payments.Margin = new Padding(8);
            this.Btn_Payments.Name = "Btn_Payments";
            this.Btn_Payments.Size = new Size(160, 40);
            this.Btn_Payments.TabIndex = 0x31;
            this.Btn_Payments.Text = "CheckOut [F12]";
            this.Btn_Payments.UseVisualStyleBackColor = false;
            this.Btn_Payments.Click += new EventHandler(this.Btn_CheckouPayment_Click);
            this.Btn_HoldCart.BackColor = Color.Chocolate;
            this.Btn_HoldCart.FlatStyle = FlatStyle.Flat;
            this.Btn_HoldCart.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_HoldCart.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_HoldCart.Location = new Point(0xba, 5);
            this.Btn_HoldCart.Margin = new Padding(10);
            this.Btn_HoldCart.Name = "Btn_HoldCart";
            this.Btn_HoldCart.Size = new Size(160, 40);
            this.Btn_HoldCart.TabIndex = 0x33;
            this.Btn_HoldCart.Text = "Hold bill  [F4]";
            this.Btn_HoldCart.UseVisualStyleBackColor = false;
            this.Btn_HoldCart.Click += new EventHandler(this.Btn_HoldCart_Click);
            this.Panel_salesOperations.BackColor = Color.GhostWhite;
            this.Panel_salesOperations.Controls.Add(this.panel3);
            this.Panel_salesOperations.Controls.Add(this.panel2);
            this.Panel_salesOperations.Dock = DockStyle.Bottom;
            this.Panel_salesOperations.Location = new Point(0, 0x218);
            this.Panel_salesOperations.Margin = new Padding(3, 3, 0, 3);
            this.Panel_salesOperations.Name = "Panel_salesOperations";
            this.Panel_salesOperations.Size = new Size(0x512, 0x70);
            this.Panel_salesOperations.TabIndex = 0x13;
            this.panel3.Controls.Add(this.Btn_Close);
            this.panel3.Controls.Add(this.Btn_HoldCart);
            this.panel3.Controls.Add(this.Btn_Invoice);
            this.panel3.Controls.Add(this.Btn_Payments);
            this.panel3.Controls.Add(this.Btn_Quotation);
            this.panel3.Controls.Add(this.Btn_CartRetrieval);
            this.panel3.Dock = DockStyle.Right;
            this.panel3.Location = new Point(0xd5, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x43d, 0x34);
            this.panel3.TabIndex = 0x3e;
            this.Btn_Close.BackColor = Color.Chocolate;
            this.Btn_Close.FlatStyle = FlatStyle.Flat;
            this.Btn_Close.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Close.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Close.Location = new Point(5, 5);
            this.Btn_Close.Margin = new Padding(10);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(160, 40);
            this.Btn_Close.TabIndex = 0x3d;
            this.Btn_Close.Text = "Close [F1]";
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            this.Btn_Invoice.BackColor = Color.Chocolate;
            this.Btn_Invoice.FlatStyle = FlatStyle.Flat;
            this.Btn_Invoice.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Invoice.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Invoice.Location = new Point(0x2d9, 5);
            this.Btn_Invoice.Margin = new Padding(10);
            this.Btn_Invoice.Name = "Btn_Invoice";
            this.Btn_Invoice.Size = new Size(160, 40);
            this.Btn_Invoice.TabIndex = 0x3b;
            this.Btn_Invoice.Text = "Invoice Bill [F10]";
            this.Btn_Invoice.UseVisualStyleBackColor = false;
            this.Btn_Invoice.Click += new EventHandler(this.Btn_Invoice_Click);
            this.Btn_Quotation.BackColor = Color.Chocolate;
            this.Btn_Quotation.FlatStyle = FlatStyle.Flat;
            this.Btn_Quotation.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Btn_Quotation.ForeColor = SystemColors.ButtonHighlight;
            this.Btn_Quotation.Location = new Point(0x224, 5);
            this.Btn_Quotation.Margin = new Padding(10);
            this.Btn_Quotation.Name = "Btn_Quotation";
            this.Btn_Quotation.Size = new Size(160, 40);
            this.Btn_Quotation.TabIndex = 60;
            this.Btn_Quotation.Text = "Quotation [F8]";
            this.Btn_Quotation.UseVisualStyleBackColor = false;
            this.Btn_Quotation.Click += new EventHandler(this.Btn_Quotation_Click);
            this.Cart_Gridview.AllowUserToAddRows = false;
            this.Cart_Gridview.AllowUserToResizeColumns = false;
            this.Cart_Gridview.AllowUserToResizeRows = false;
            style.Font = new Font("Palatino Linotype", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Cart_Gridview.AlternatingRowsDefaultCellStyle = style;
            this.Cart_Gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Cart_Gridview.BackgroundColor = Color.White;
            this.Cart_Gridview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            style2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style2.BackColor = Color.FromArgb(0xe0, 0xe0, 0xe0);
            style2.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style2.ForeColor = Color.FromArgb(0, 0, 0x40);
            style2.SelectionBackColor = Color.LightSalmon;
            style2.SelectionForeColor = SystemColors.HighlightText;
            style2.WrapMode = DataGridViewTriState.True;
            this.Cart_Gridview.ColumnHeadersDefaultCellStyle = style2;
            this.Cart_Gridview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewColumn[] dataGridViewColumns = new DataGridViewColumn[14];
            dataGridViewColumns[0] = this.Column1;
            dataGridViewColumns[1] = this.Column2;
            dataGridViewColumns[2] = this.Column9;
            dataGridViewColumns[3] = this.Column3;
            dataGridViewColumns[4] = this.Column4;
            dataGridViewColumns[5] = this.Column5;
            dataGridViewColumns[6] = this.Column8;
            dataGridViewColumns[7] = this.Column7;
            dataGridViewColumns[8] = this.Column13;
            dataGridViewColumns[9] = this.Column14;
            dataGridViewColumns[10] = this.Column10;
            dataGridViewColumns[11] = this.TotalProfit;
            dataGridViewColumns[12] = this.DiscAmount;
            dataGridViewColumns[13] = this.Column6;
            this.Cart_Gridview.Columns.AddRange(dataGridViewColumns);
            this.Cart_Gridview.ContextMenuStrip = this.MainCart_MenuStrip;
            style3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style3.BackColor = SystemColors.Window;
            style3.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style3.ForeColor = SystemColors.ControlText;
            style3.SelectionBackColor = SystemColors.ActiveCaption;
            style3.SelectionForeColor = SystemColors.HighlightText;
            style3.WrapMode = DataGridViewTriState.False;
            this.Cart_Gridview.DefaultCellStyle = style3;
            this.Cart_Gridview.Dock = DockStyle.Fill;
            this.Cart_Gridview.EditMode = DataGridViewEditMode.EditOnF2;
            this.Cart_Gridview.EnableHeadersVisualStyles = false;
            this.Cart_Gridview.Location = new Point(0, 0x2b);
            this.Cart_Gridview.Name = "Cart_Gridview";
            style4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            style4.BackColor = Color.Sienna;
            style4.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            style4.ForeColor = SystemColors.WindowText;
            style4.NullValue = "X";
            style4.SelectionBackColor = SystemColors.Highlight;
            style4.SelectionForeColor = SystemColors.HighlightText;
            style4.WrapMode = DataGridViewTriState.True;
            this.Cart_Gridview.RowHeadersDefaultCellStyle = style4;
            this.Cart_Gridview.RowHeadersVisible = false;
            this.Cart_Gridview.RowHeadersWidth = 20;
            this.Cart_Gridview.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            style5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style5.Font = new Font("Palatino Linotype", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style5.NullValue = null;
            this.Cart_Gridview.RowsDefaultCellStyle = style5;
            this.Cart_Gridview.RowTemplate.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Cart_Gridview.RowTemplate.Height = 0x1a;
            this.Cart_Gridview.Size = new Size(0x512, 0x1ed);
            this.Cart_Gridview.TabIndex = 20;
            this.Cart_Gridview.CellContentClick += new DataGridViewCellEventHandler(this.Cart_Gridview_CellContentClick);
            this.Cart_Gridview.CellValidated += new DataGridViewCellEventHandler(this.Cart_Gridview_CellValidated);
            this.Cart_Gridview.CellValidating += new DataGridViewCellValidatingEventHandler(this.Cart_Gridview_CellValidating);
            this.Cart_Gridview.CellValueChanged += new DataGridViewCellEventHandler(this.Cart_Gridview_CellValueChanged);
            this.Cart_Gridview.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.Cart_Gridview_EditingControlShowing);
            this.Cart_Gridview.RowsAdded += new DataGridViewRowsAddedEventHandler(this.Cart_Gridview_RowsAdded);
            this.Cart_Gridview.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.Cart_Gridview_RowsRemoved);
            this.Cart_Gridview.RowValidated += new DataGridViewCellEventHandler(this.Cart_Gridview_RowValidated);
            this.Cart_Gridview.UserAddedRow += new DataGridViewRowEventHandler(this.Cart_Gridview_UserAddedRow);
            this.Cart_Gridview.UserDeletedRow += new DataGridViewRowEventHandler(this.Cart_Gridview_UserDeletedRow);
            this.Cart_Gridview.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.Cart_Gridview_UserDeletingRow);
            this.Cart_Gridview.MouseClick += new MouseEventHandler(this.Cart_Gridview_MouseClick);
            style6.BackColor = Color.White;
            style6.SelectionBackColor = Color.White;
            style6.SelectionForeColor = Color.Black;
            this.Column1.DefaultCellStyle = style6;
            this.Column1.FillWeight = 30f;
            this.Column1.HeaderText = "Code";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            style7.SelectionBackColor = Color.White;
            style7.SelectionForeColor = Color.Black;
            this.Column2.DefaultCellStyle = style7;
            this.Column2.FillWeight = 62.55304f;
            this.Column2.HeaderText = "Description";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column9.FillWeight = 20f;
            this.Column9.HeaderText = "Unit";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            style8.Format = "N2";
            style8.NullValue = null;
            this.Column3.DefaultCellStyle = style8;
            this.Column3.FillWeight = 20f;
            this.Column3.HeaderText = "Quantity";
            this.Column3.Name = "Column3";
            style9.Format = "N2";
            this.Column4.DefaultCellStyle = style9;
            this.Column4.FillWeight = 20f;
            this.Column4.HeaderText = "UnitPrice";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            style10.Format = "N2";
            this.Column5.DefaultCellStyle = style10;
            this.Column5.FillWeight = 25f;
            this.Column5.HeaderText = "Total";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            style11.Format = "N2";
            this.Column8.DefaultCellStyle = style11;
            this.Column8.FillWeight = 15f;
            this.Column8.HeaderText = "Disc%";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            style12.Format = "N2";
            this.Column7.DefaultCellStyle = style12;
            this.Column7.FillWeight = 15f;
            this.Column7.HeaderText = "Gsst%";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            style13.Format = "N2";
            this.Column13.DefaultCellStyle = style13;
            this.Column13.FillWeight = 20f;
            this.Column13.HeaderText = "Tax1%";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            style14.Format = "N2";
            this.Column14.DefaultCellStyle = style14;
            this.Column14.FillWeight = 20f;
            this.Column14.HeaderText = "Tax2%";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Visible = false;
            style15.Format = "N2";
            this.Column10.DefaultCellStyle = style15;
            this.Column10.FillWeight = 20f;
            this.Column10.HeaderText = "Profit";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Visible = false;
            style16.Format = "N2";
            this.TotalProfit.DefaultCellStyle = style16;
            this.TotalProfit.FillWeight = 20f;
            this.TotalProfit.HeaderText = "Tprofit";
            this.TotalProfit.Name = "TotalProfit";
            this.TotalProfit.ReadOnly = true;
            this.TotalProfit.Visible = false;
            style17.Format = "N2";
            this.DiscAmount.DefaultCellStyle = style17;
            this.DiscAmount.FillWeight = 20f;
            this.DiscAmount.HeaderText = "DiscAmt";
            this.DiscAmount.Name = "DiscAmount";
            this.DiscAmount.ReadOnly = true;
            this.DiscAmount.Visible = false;
            this.Column6.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            style18.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style18.BackColor = Color.FromArgb(0xc0, 0, 0);
            style18.Font = new Font("Times New Roman", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            style18.ForeColor = SystemColors.ButtonHighlight;
            style18.NullValue = "Del";
            style18.SelectionBackColor = Color.FromArgb(0xc0, 0, 0);
            style18.SelectionForeColor = Color.DarkRed;
            this.Column6.DefaultCellStyle = style18;
            this.Column6.FillWeight = 15f;
            this.Column6.FlatStyle = FlatStyle.Popup;
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = DataGridViewTriState.False;
            this.Column6.SortMode = DataGridViewColumnSortMode.Automatic;
            this.Column6.Text = "Del";
            this.Column6.ToolTipText = "Remove Item From Cart";
            this.Column6.UseColumnTextForButtonValue = true;
            this.Column6.Width = 40;
            base.AutoScaleMode = AutoScaleMode.None;
            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.ClientSize = new Size(0x512, 0x288);
            base.ControlBox = false;
            base.Controls.Add(this.Cart_Gridview);
            base.Controls.Add(this.Panel_ProductSearch);
            base.Controls.Add(this.Panel_salesOperations);
            this.DoubleBuffered = true;
            this.Font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Point, 0);
            base.FormBorderStyle = FormBorderStyle.None;
            base.KeyPreview = true;
            base.Name = "Sales";
            base.SizeGripStyle = SizeGripStyle.Hide;
            base.StartPosition = FormStartPosition.CenterParent;
            base.FormClosing += new FormClosingEventHandler(this.Sales_FormClosing);
            base.Load += new EventHandler(this.Sales_Load);
            base.Shown += new EventHandler(this.Sales_Shown);
            base.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            this.MainCart_MenuStrip.ResumeLayout(false);
            this.Panel_ProductSearch.ResumeLayout(false);
            this.Panel_ProductSearch.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Panel_salesOperations.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((ISupportInitialize) this.Cart_Gridview).EndInit();
            base.ResumeLayout(false);
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

