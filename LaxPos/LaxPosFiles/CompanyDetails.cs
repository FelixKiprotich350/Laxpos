namespace LaxPos.LaxPosFiles
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CompanyDetails
    {
        private readonly CompanyProfile Profile = new CompanyProfile();
        private readonly DatabaseConfiguration Db = new DatabaseConfiguration();

        public void GetCompanyDetails()
        {
            try
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                MySqlConnection connection = new MySqlConnection(this.Db.DBConnecString());
                connection.Open();
                MySqlDataReader reader = new MySqlCommand("select * from companydetails", connection).ExecuteReader();
                if (reader.HasRows)
                {
                    while (true)
                    {
                        if (!reader.Read())
                        {
                            break;
                        }
                        string key = reader["VariableName"].ToString();
                        string str2 = reader["Value"].ToString();
                        dictionary.Add(key, str2);
                    }
                }
                else
                {
                    this.Profile.ClientTitle = "";
                    this.Profile.ClientAddress = "";
                    this.Profile.ClientEmail = "";
                    this.Profile.ClientTel = "";
                    this.Profile.ClientPin = "";
                    this.Profile.ClientVATRegNo = "";
                    this.Profile.ClientText1 = "";
                    this.Profile.ClientText2 = "";
                    this.Profile.ClientText3 = "";
                    this.Profile.ClientText4 = "";
                    this.Profile.ClientTaxRate = 0M;
                    this.Profile.ClientCashToPointsRate = 0M;
                    this.Profile.ClientPointsToCashRate = 0M;
                    this.Profile.ClientInvoicePeriod = 0;
                    this.Profile.ClientQuotationPeriod = 0;
                    this.Profile.ClientBackupOffline = "";
                    this.Profile.ClientBackupOnline = "";
                }
                connection.Close();
                if (dictionary.Count == 0x11)
                {
                    foreach (string str3 in dictionary.Keys.ToList<string>())
                    {
                        if (str3 == "Title")
                        {
                            this.Profile.ClientTitle = dictionary[str3].ToString();
                        }
                        if (str3 == "Address")
                        {
                            this.Profile.ClientAddress = dictionary[str3].ToString();
                        }
                        if (str3 == "Email")
                        {
                            this.Profile.ClientEmail = dictionary[str3].ToString();
                        }
                        if (str3 == "Telephone")
                        {
                            this.Profile.ClientTel = dictionary[str3].ToString();
                        }
                        if (str3 == "PinNo")
                        {
                            this.Profile.ClientPin = dictionary[str3].ToString();
                        }
                        if (str3 == "VatRegNo")
                        {
                            this.Profile.ClientVATRegNo = dictionary[str3].ToString();
                        }
                        if (str3 == "Text1")
                        {
                            this.Profile.ClientText1 = dictionary[str3].ToString();
                        }
                        if (str3 == "Text2")
                        {
                            this.Profile.ClientText2 = dictionary[str3].ToString();
                        }
                        if (str3 == "Text3")
                        {
                            this.Profile.ClientText3 = dictionary[str3].ToString();
                        }
                        if (str3 == "Text4")
                        {
                            this.Profile.ClientText4 = dictionary[str3].ToString();
                        }
                        if (str3 == "TaxRate")
                        {
                            this.Profile.ClientTaxRate = decimal.Parse(dictionary[str3].ToString());
                        }
                        if (str3 == "CashToPointsRate")
                        {
                            this.Profile.ClientCashToPointsRate = decimal.Parse(dictionary[str3].ToString());
                        }
                        if (str3 == "PointsToCashRate")
                        {
                            this.Profile.ClientPointsToCashRate = decimal.Parse(dictionary[str3].ToString());
                        }
                        if (str3 == "InvoicePeriod")
                        {
                            this.Profile.ClientInvoicePeriod = int.Parse(dictionary[str3].ToString());
                        }
                        if (str3 == "BackupOffline")
                        {
                            this.Profile.ClientBackupOffline = dictionary[str3].ToString();
                        }
                        if (str3 == "BackupOnline")
                        {
                            this.Profile.ClientBackupOnline = dictionary[str3].ToString();
                        }
                    }
                }
                this.Profile.Save();
                this.Profile.Upgrade();
                this.Profile.Reload();
            }
            catch
            {
            }
        }
    }
}

