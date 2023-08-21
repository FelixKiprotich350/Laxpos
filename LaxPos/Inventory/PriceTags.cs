using LaxPos.LaxPosFiles;
using LaxPos.Pos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaxPos.Inventory
{
    public partial class PriceTags : LaxcoForm
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();
        public AutoCompleteStringCollection Autocom = new AutoCompleteStringCollection();

        public PriceTags()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void PriceTags_Shown(object sender, EventArgs e)
        {
            try
            {
                LoadAutocompleteProducts();
                this.textBox4.AutoCompleteCustomSource = this.Autocom;
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            base.AcceptButton = this.Btn_AddItem;
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
                    while (reader.Read())
                    {
                        if ((reader.GetString("SellingUnit") == "") || (reader.GetDouble("SellingUnitPrice") <= 0.0))
                        {
                            MessageBox.Show("The Product Quantity In Stock Cannot Be Evaluated!!\n " + reader.GetString(5), "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.textBox4.Focus();
                        }
                        else
                        {
                            object[] values = new object[4];
                            values[0] = reader.GetString("ProductCode");
                            values[1] = reader.GetString("Description");
                            values[2] = reader.GetString("SellingUnit");
                            values[3] = reader.GetString("SellingUnitPrice");
                            this.Cart_Gridview.Rows.Add(values);
                            
                        }
                    }
                    this.textBox4.Focus();
                }
                reader.Close();

            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void PrintBill()
        {
            try
            { 
                
                
                foreach (DataGridViewRow R in Cart_Gridview.Rows)
                {
                    Rprint = R;
                    PrintDocument document = new PrintDocument();
                    document.PrintPage += new PrintPageEventHandler(this.ProvideContentforPrintBill);
                    document.PrintController = new StandardPrintController();
                    document.Print();
                    //printPreviewDialog1.Document = document;
                    //printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                    //printPreviewDialog1.ShowDialog();
                }
                MessageBox.Show("Completed");

            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "ERROR OCCURED", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        DataGridViewRow Rprint;
        public void ProvideContentforPrintBill(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics graphics = e.Graphics;
                int num = 10;
                StringFormat format1 = new StringFormat();
                format1.LineAlignment = StringAlignment.Center;
                format1.Alignment = StringAlignment.Center; 
                if (Rprint.Cells[1].Value.ToString().Length > 31)
                {
                    graphics.DrawString(Rprint.Cells[1].Value.ToString().Substring(0, 31), new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float)num,format1);
                    num += 25;
                    graphics.DrawString(Rprint.Cells[1].Value.ToString().Substring(31), new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float)num,format1);
                    num += 25;

                }
                else
                {
                    graphics.DrawString(Rprint.Cells[1].Value.ToString(), new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), 10f, (float)num);
                    num += 25;
                }

                graphics.DrawString("KSH "+ Rprint.Cells[3].Value.ToString()+".00" , new Font("Arial", 14,FontStyle.Bold), new SolidBrush(Color.Black), 70, (float)num);
                num += 20;
               
            }
            catch (Exception exception1)
            {
                //MessageBox.Show(exception1.Message, "ERROR MESSAGE!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void Btn_SelectAllItems_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Print_Click(object sender, EventArgs e)
        {
            PrintBill();
        }
    }
}
