namespace LaxPos.Accounting
{
    using LaxPos;
    using Microsoft.Reporting.WinForms;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class ReportingForm : Form
    {
        private readonly DataSet CurrDataset;
        private string ReportTitle = "";
        private double Reporttotal = 0.0;
        private double Reportcards = 0.0;
        private double Reportcash = 0.0;
        private double Reportmpesa = 0.0;
        private IContainer components = null;
        private Panel panel1;
        private Panel panel2;
        private Button Btn_Close;
        private Button Btn_Refresh;
        public ReportViewer reportViewer1;

        public ReportingForm(DataSet Reporting_Dataset, string R_Title, double total, double cash, double cards, double mpesa)
        {
            this.InitializeComponent();
            this.CurrDataset = Reporting_Dataset;
            this.ReportTitle = R_Title;
            this.Reporttotal = total;
            this.Reportcash = cash;
            this.Reportcards = cards;
            this.Reportmpesa = mpesa;
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Btn_LoadReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.reportViewer1.Reset();
                this.reportViewer1.ProcessingMode = ProcessingMode.Local;
                this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
                DataSet set = new LaxposReportingDataset();
                ReportParameter parameter = new ReportParameter("ReportTitle", this.ReportTitle);
                ReportParameter parameter2 = new ReportParameter("Total", this.Reporttotal.ToString());
                ReportParameter parameter3 = new ReportParameter("Cards", this.Reportcards.ToString());
                ReportParameter parameter4 = new ReportParameter("Cash", this.Reportcash.ToString());
                ReportParameter parameter5 = new ReportParameter("Mpesa", this.Reportmpesa.ToString());
                ReportDataSource item = new ReportDataSource("DataSet1", this.CurrDataset.Tables[0]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                ReportParameter[] parameters = new ReportParameter[] { parameter, parameter2 };
                this.reportViewer1.LocalReport.SetParameters(parameters);
                this.reportViewer1.LocalReport.DataSources.Add(item);
                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }
            catch (Exception exception)
            {
                string text1;
                string[] textArray1 = new string[5];
                textArray1[0] = exception.Message;
                textArray1[1] = "\n==>";
                textArray1[2] = exception.StackTrace;
                textArray1[3] = "\n==>";
                Exception innerException = exception.InnerException;
                string[] textArray2 = textArray1;
                if (innerException != null)
                {
                    text1 = innerException.ToString();
                }
                else
                {
                    Exception local1 = innerException;
                    text1 = null;
                }
                textArray1[4] = text1;
                MessageBox.Show(string.Concat(textArray1));
            }
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            this.Btn_LoadReport_Click(new object(), new EventArgs());
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
            this.panel1 = new Panel();
            this.reportViewer1 = new ReportViewer();
            this.panel2 = new Panel();
            this.Btn_Refresh = new Button();
            this.Btn_Close = new Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(610, 0x234);
            this.panel1.TabIndex = 0;
            this.reportViewer1.Dock = DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            this.reportViewer1.Location = new Point(0, 0);
            this.reportViewer1.Name = "ReportViewer";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new Size(610, 0x234);
            this.reportViewer1.TabIndex = 0;
            this.panel2.Controls.Add(this.Btn_Refresh);
            this.panel2.Controls.Add(this.Btn_Close);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x234);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(610, 0x2f);
            this.panel2.TabIndex = 1;
            this.Btn_Refresh.Location = new Point(0xc0, 12);
            this.Btn_Refresh.Name = "Btn_Refresh";
            this.Btn_Refresh.Size = new Size(0x65, 0x17);
            this.Btn_Refresh.TabIndex = 2;
            this.Btn_Refresh.Text = "Refresh";
            this.Btn_Refresh.UseVisualStyleBackColor = true;
            this.Btn_Refresh.Click += new EventHandler(this.Btn_Refresh_Click);
            this.Btn_Close.Location = new Point(0x167, 12);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new Size(0x4d, 0x17);
            this.Btn_Close.TabIndex = 0;
            this.Btn_Close.Text = "Close";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new EventHandler(this.Btn_Close_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(610, 0x263);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MinimizeBox = false;
            base.Name = "ReportingForm";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Report Form";
            base.Load += new EventHandler(this.ReportingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void ReportingForm_Load(object sender, EventArgs e)
        {
            this.Btn_LoadReport_Click(new object(), new EventArgs());
        }
    }
}

