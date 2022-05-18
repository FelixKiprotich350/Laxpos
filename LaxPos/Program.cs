namespace LaxPos
{
    using LaxPos.LaxPosFiles;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        public static bool Shuttingdown = true;
        public static CompanyDetails Company_Details = new CompanyDetails();
        public static LaxposReportingDataset Report_Dataset = new LaxposReportingDataset();
        public static DataSet CustomerCart = new DataSet();
        public static List<string> ProductsItemsList = new List<string>();
        public static readonly DatabaseConfiguration DbConn = new DatabaseConfiguration();
        public static readonly CompanyProfile Client = new CompanyProfile();
        public static List<FunctionalityRight> Fun_Rights = new List<FunctionalityRight>();
        public static DateTime CurrentWorkPeriodDate = DateTime.Now;
        public static Dashboard CurrDashboardForm = null;
        public static Login CurrLoginForm = null;
        public static UserLoggedIn CurrLoggedInUser = null;
        public static string LogInCounter = Environment.MachineName;
        public static string LogInBranch = "Branch1";
        public static string CompanyName = Client.ClientTitle;
        public static string CompanyAddress = Client.ClientAddress;
        public static string CompanyTelephone = Client.ClientTel;
        public static string CompanyEmail = Client.ClientEmail;
        public static string CompanyPin = Client.ClientPin;
        public static string CompanyFooter1 = Client.ClientText1;
        public static string CompanyFooter2 = Client.ClientText2;
        public static string Dbconnstring = "";
        public static AutoCompleteStringCollection MasterProductList = new AutoCompleteStringCollection();
        public static DateTime LastActionTime = DateTime.Parse("0001-01-01");
        public static int AutologoutTime_seconds = 600;
        public static string Lastuser = "";

        public static DateTime CurrentDateTime() => 
            DateTime.Now;

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                MessageBox.Show(((Exception) e.ExceptionObject).Message, "Application Error : 100 ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch
            {
                Application.Exit();
            }
        }

        private static void Form1_UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            try
            {
                MessageBox.Show("An application error occurred. Please contact the adminstrator with the following information:\n\n" + t.Exception.Message + "\n\nStack Trace:\n" + t.Exception.StackTrace, "Application Error :101 ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Error", "Message Box", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string thisprocessname = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcesses().Count<Process>(p => (p.ProcessName == thisprocessname)) > 1)
            {
                MessageBox.Show("STOP!.Application is already running!!", "MESSAGE BOX", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Application.Exit();
            }
            else
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += new ThreadExceptionEventHandler(Program.Form1_UIThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
                CurrLoginForm = null;
                CurrDashboardForm = null;
                CurrLoggedInUser = null;
                Dbconnstring = DbConn.DBConnecString();
                Company_Details.GetCompanyDetails();
                PopulateRights();
                Application.Run(new Login());
            }
        }

        private static void PopulateRights()
        {
            Fun_Rights.Clear();
            FunctionalityRight item = new FunctionalityRight();
            item.RightID = "A100";
            item.RightLevel = 1;
            item.RightShortName = "Manage Users";
            item.RightFullName = "Manage Users";
            item.Department = UserRolesCategories.Administration;
            Fun_Rights.Add(item);
            FunctionalityRight right2 = new FunctionalityRight();
            right2.RightID = "A101";
            right2.RightLevel = 1;
            right2.RightShortName = "User Rights";
            right2.RightFullName = "Manage Users Rights";
            right2.Department = UserRolesCategories.Administration;
            Fun_Rights.Add(right2);
            FunctionalityRight right3 = new FunctionalityRight();
            right3.RightID = "A102";
            right3.RightLevel = 1;
            right3.RightShortName = "View Users";
            right3.RightFullName = "View users details";
            right3.Department = UserRolesCategories.Administration;
            Fun_Rights.Add(right3);
            FunctionalityRight right4 = new FunctionalityRight();
            right4.RightID = "A103";
            right4.RightLevel = 1;
            right4.RightShortName = "Edit Users";
            right4.RightFullName = "Edit users Details";
            right4.Department = UserRolesCategories.Administration;
            Fun_Rights.Add(right4);
            FunctionalityRight right5 = new FunctionalityRight();
            right5.RightID = "B100";
            right5.RightLevel = 1;
            right5.RightShortName = "WP Accounts";
            right5.RightFullName = "View Work Period Accounts";
            right5.Department = UserRolesCategories.Accounting;
            Fun_Rights.Add(right5);
            FunctionalityRight right6 = new FunctionalityRight();
            right6.RightID = "B101";
            right6.RightLevel = 1;
            right6.RightShortName = "CashOut";
            right6.RightFullName = "View Cashout";
            right6.Department = UserRolesCategories.Accounting;
            Fun_Rights.Add(right6);
            FunctionalityRight right7 = new FunctionalityRight();
            right7.RightID = "B102";
            right7.RightLevel = 1;
            right7.RightShortName = "View Sales";
            right7.RightFullName = "View Sales";
            right7.Department = UserRolesCategories.Accounting;
            Fun_Rights.Add(right7);
            FunctionalityRight right8 = new FunctionalityRight();
            right8.RightID = "B103";
            right8.RightLevel = 1;
            right8.RightShortName = "Manage WorkPeriod";
            right8.RightFullName = "Manage Work Period";
            right8.Department = UserRolesCategories.Accounting;
            Fun_Rights.Add(right8);
            FunctionalityRight right9 = new FunctionalityRight();
            right9.RightID = "C100";
            right9.RightLevel = 1;
            right9.RightShortName = "Master Entries";
            right9.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right9);
            FunctionalityRight right10 = new FunctionalityRight();
            right10.RightID = "C101";
            right10.RightLevel = 1;
            right10.RightShortName = "Stockin";
            right10.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right10);
            FunctionalityRight right11 = new FunctionalityRight();
            right11.RightID = "C102";
            right11.RightLevel = 1;
            right11.RightShortName = "Products";
            right11.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right11);
            FunctionalityRight right12 = new FunctionalityRight();
            right12.RightID = "C103";
            right12.RightLevel = 1;
            right12.RightShortName = "LPO";
            right12.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right12);
            FunctionalityRight right13 = new FunctionalityRight();
            right13.RightID = "C104";
            right13.RightLevel = 1;
            right13.RightShortName = "StockTaking";
            right13.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right13);
            FunctionalityRight right14 = new FunctionalityRight();
            right14.RightID = "C106";
            right14.RightLevel = 1;
            right14.RightShortName = "Suppliers";
            right14.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right14);
            FunctionalityRight right15 = new FunctionalityRight();
            right15.RightID = "C107";
            right15.RightLevel = 1;
            right15.RightShortName = "Reports";
            right15.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right15);
            FunctionalityRight right16 = new FunctionalityRight();
            right16.RightID = "C108";
            right16.RightLevel = 1;
            right16.RightShortName = "Inventory Settings";
            right16.Department = UserRolesCategories.Inventory;
            Fun_Rights.Add(right16);
            FunctionalityRight right17 = new FunctionalityRight();
            right17.RightID = "E100";
            right17.RightLevel = 1;
            right17.RightShortName = "Make Sales";
            right17.RightFullName = "Make Sales";
            right17.Department = UserRolesCategories.Cashier;
            Fun_Rights.Add(right17);
            FunctionalityRight right18 = new FunctionalityRight();
            right18.RightID = "E101";
            right18.RightLevel = 1;
            right18.RightShortName = "Add Expense";
            right18.RightFullName = "Create Expenses";
            right18.Department = UserRolesCategories.Cashier;
            Fun_Rights.Add(right18);
            FunctionalityRight right19 = new FunctionalityRight();
            right19.RightID = "E102";
            right19.RightLevel = 1;
            right19.RightShortName = "Reprint Receipt";
            right19.RightFullName = "Reprint Receipt(self)";
            right19.Department = UserRolesCategories.Cashier;
            Fun_Rights.Add(right19);
            FunctionalityRight right20 = new FunctionalityRight();
            right20.RightID = "E103";
            right20.RightLevel = 1;
            right20.RightShortName = "Checkout Invoice";
            right20.RightFullName = "Checkout Invoice Bill";
            right20.Department = UserRolesCategories.Cashier;
            Fun_Rights.Add(right20);
            FunctionalityRight right21 = new FunctionalityRight();
            right21.RightID = "E104";
            right21.RightLevel = 1;
            right21.RightShortName = "Daily report";
            right21.RightFullName = "View daily report(self)";
            right21.Department = UserRolesCategories.Cashier;
            Fun_Rights.Add(right21);
            FunctionalityRight right22 = new FunctionalityRight();
            right22.RightID = "F100";
            right22.RightLevel = 1;
            right22.RightShortName = "Add Customer";
            right22.RightFullName = "Add new customers";
            right22.Department = UserRolesCategories.CustomerSupport;
            Fun_Rights.Add(right22);
            FunctionalityRight right23 = new FunctionalityRight();
            right23.RightID = "F101";
            right23.RightLevel = 1;
            right23.RightShortName = "Edit Customer";
            right23.RightFullName = "Edit Customers Details";
            right23.Department = UserRolesCategories.CustomerSupport;
            Fun_Rights.Add(right23);
            FunctionalityRight right24 = new FunctionalityRight();
            right24.RightID = "F102";
            right24.RightLevel = 1;
            right24.RightShortName = "View Customers";
            right24.RightFullName = "View Customers Details";
            right24.Department = UserRolesCategories.CustomerSupport;
            Fun_Rights.Add(right24);
            FunctionalityRight right25 = new FunctionalityRight();
            right25.RightID = "F103";
            right25.RightLevel = 1;
            right25.RightShortName = "Loyalty Points";
            right25.RightFullName = "View Loyalty Points For customers";
            right25.Department = UserRolesCategories.CustomerSupport;
            Fun_Rights.Add(right25);
            FunctionalityRight right26 = new FunctionalityRight();
            right26.RightID = "F104";
            right26.RightLevel = 1;
            right26.RightShortName = "Loyalty Settings";
            right26.RightFullName = "Loyalty Settings";
            right26.Department = UserRolesCategories.CustomerSupport;
            Fun_Rights.Add(right26);
        }

        public enum UserRolesCategories
        {
            Administration,
            Accounting,
            Inventory,
            Supervising,
            Cashier,
            CustomerSupport
        }
    }
}

