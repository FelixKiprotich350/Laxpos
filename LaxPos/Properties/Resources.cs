namespace LaxPos.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new System.Resources.ResourceManager("LaxPos.Properties.Resources", typeof(Resources).Assembly);
                }
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => 
                resourceCulture;
            set => 
                resourceCulture = value;
        }

        internal static Bitmap button_power_yellow =>
            (Bitmap) ResourceManager.GetObject("button-power_yellow", resourceCulture);

        internal static Bitmap Expand =>
            (Bitmap) ResourceManager.GetObject("Expand", resourceCulture);

        internal static Bitmap icon_collapse =>
            (Bitmap) ResourceManager.GetObject("icon_collapse", resourceCulture);

        internal static Bitmap icons_hamburger_menu =>
            (Bitmap) ResourceManager.GetObject("icons-hamburger-menu", resourceCulture);

        internal static Bitmap logo_laxco =>
            (Bitmap) ResourceManager.GetObject("logo laxco", resourceCulture);

        internal static Bitmap settings =>
            (Bitmap) ResourceManager.GetObject("settings", resourceCulture);
    }
}

