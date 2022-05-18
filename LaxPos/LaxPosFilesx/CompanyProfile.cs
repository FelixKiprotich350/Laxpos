namespace LaxPos.LaxPosFiles
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    public sealed class CompanyProfile : ApplicationSettingsBase
    {
        private static CompanyProfile defaultInstance = ((CompanyProfile) Synchronized(new CompanyProfile()));

        public static CompanyProfile Default =>
            defaultInstance;

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientText3
        {
            get => 
                (string) this["ClientText3"];
            set => 
                this["ClientText3"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientTel
        {
            get => 
                (string) this["ClientTel"];
            set => 
                this["ClientTel"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("Carrace@gmail.com")]
        public string ClientEmail
        {
            get => 
                (string) this["ClientEmail"];
            set => 
                this["ClientEmail"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientText2
        {
            get => 
                (string) this["ClientText2"];
            set => 
                this["ClientText2"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientPin
        {
            get => 
                (string) this["ClientPin"];
            set => 
                this["ClientPin"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("CARRACE DRINKS LTD")]
        public string ClientTitle
        {
            get => 
                (string) this["ClientTitle"];
            set => 
                this["ClientTitle"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientText4
        {
            get => 
                (string) this["ClientText4"];
            set => 
                this["ClientText4"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("P.O. BOX 55555")]
        public string ClientAddress
        {
            get => 
                (string) this["ClientAddress"];
            set => 
                this["ClientAddress"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("16")]
        public decimal ClientTaxRate
        {
            get => 
                (decimal) this["ClientTaxRate"];
            set => 
                this["ClientTaxRate"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientText1
        {
            get => 
                (string) this["ClientText1"];
            set => 
                this["ClientText1"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientVATRegNo
        {
            get => 
                (string) this["ClientVATRegNo"];
            set => 
                this["ClientVATRegNo"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
        public decimal ClientPointsToCashRate
        {
            get => 
                (decimal) this["ClientPointsToCashRate"];
            set => 
                this["ClientPointsToCashRate"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
        public decimal ClientCashToPointsRate
        {
            get => 
                (decimal) this["ClientCashToPointsRate"];
            set => 
                this["ClientCashToPointsRate"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientBackupOnline
        {
            get => 
                (string) this["ClientBackupOnline"];
            set => 
                this["ClientBackupOnline"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("")]
        public string ClientBackupOffline
        {
            get => 
                (string) this["ClientBackupOffline"];
            set => 
                this["ClientBackupOffline"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
        public int ClientQuotationPeriod
        {
            get => 
                (int) this["ClientQuotationPeriod"];
            set => 
                this["ClientQuotationPeriod"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("0")]
        public int ClientInvoicePeriod
        {
            get => 
                (int) this["ClientInvoicePeriod"];
            set => 
                this["ClientInvoicePeriod"] = value;
        }
    }
}

