namespace LaxPos.LaxPosFiles
{
    using System;
    using System.CodeDom.Compiler;
    using System.Configuration;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed class ClientDatabase : ApplicationSettingsBase
    {
        private static ClientDatabase defaultInstance = ((ClientDatabase) Synchronized(new ClientDatabase()));

        public static ClientDatabase Default =>
            defaultInstance;

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("localhost")]
        public string Server
        {
            get => 
                (string) this["Server"];
            set => 
                this["Server"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("toor")]
        public string Pass
        {
            get => 
                (string) this["Pass"];
            set => 
                this["Pass"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("root")]
        public string User
        {
            get => 
                (string) this["User"];
            set => 
                this["User"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("p.o.s")]
        public string Db
        {
            get => 
                (string) this["Db"];
            set => 
                this["Db"] = value;
        }

        [UserScopedSetting, DebuggerNonUserCode, DefaultSettingValue("3306")]
        public string Port
        {
            get => 
                (string) this["Port"];
            set => 
                this["Port"] = value;
        }
    }
}

