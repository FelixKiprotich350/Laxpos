namespace LaxPos.LaxPosFiles
{
    using LaxPos;
    using MySql.Data.MySqlClient;
    using System;
    using System.Windows.Forms;

    internal class Accounts
    {
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();

        public bool InsertAccounts(string TransNo, string AccType, string PaymentMethod, decimal Amount, string Refference, string UserId, string WorkStation, MySqlConnection Con, MySqlTransaction Tr)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                MySqlCommand command = new MySqlCommand("INSERT INTO accounts (TransactionNo,AccType,Amount,PaymentMethod,Date,Refference,UserId,Station) VALUES(@TransactionNo,@AccType,@Amount,@PaymentMethod,@Date,@Refference,@UserId,@Station);", Con, Tr);
                command.Parameters.AddWithValue("@TransactionNo", TransNo);
                command.Parameters.AddWithValue("@AccType", AccType);
                command.Parameters.AddWithValue("@Amount", Amount);
                command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);
                command.Parameters.AddWithValue("@Refference", Refference);
                command.Parameters.AddWithValue("@Date", Program.CurrentDateTime());
                command.Parameters.AddWithValue("@UserId", UserId);
                command.Parameters.AddWithValue("@Station", WorkStation);
                return (command.ExecuteNonQuery() > 0);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        public bool InsertBillmaster(string Billno, decimal Gross, decimal Totalpaid, decimal Balance, decimal Profit, decimal Discount, decimal Tax, decimal TaxAmount, int Points, DateTime Date, string Custref, string Cashier, string Counter, string Branch, MySqlConnection Con, MySqlTransaction Tr)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("INSERT INTO billmaster (Billno, Gross, Totalpaid, Balance, Profit, Discount, Tax, TaxAmount, Points, BillDate, Custref, Cashier, Counter, Branch) VALUES(@Billno, @Gross, @Totalpaid, @Balance, @Profit, @Discount, @Tax, @TaxAmount, @Points, @BillDate, @Custref, @Cashier,@Counter, @Branch);", Con, Tr);
                command.Parameters.AddWithValue("@Billno", Billno);
                command.Parameters.AddWithValue("@Gross", Gross);
                command.Parameters.AddWithValue("@Totalpaid", Totalpaid);
                command.Parameters.AddWithValue("@Balance", Balance);
                command.Parameters.AddWithValue("@Profit", Profit);
                command.Parameters.AddWithValue("@Discount", Discount);
                command.Parameters.AddWithValue("@Tax", Tax);
                command.Parameters.AddWithValue("@TaxAmount", TaxAmount);
                command.Parameters.AddWithValue("@Points", Points);
                command.Parameters.AddWithValue("@BillDate", Date);
                command.Parameters.AddWithValue("@Custref", Custref);
                command.Parameters.AddWithValue("@Cashier", Cashier);
                command.Parameters.AddWithValue("@Counter", Counter);
                command.Parameters.AddWithValue("@Branch", Branch);
                return (command.ExecuteNonQuery() > 0);
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
    }
}

