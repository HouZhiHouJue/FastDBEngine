namespace FastDBEngineGenerator.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), CompilerGenerated, DebuggerNonUserCode]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static Icon _003b
        {
            get
            {
                return (Icon) ResourceManager.GetObject("_003b", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static Bitmap database
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("database", resourceCulture);
            }
        }

        internal static Bitmap folderclosed2
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("folderclosed2", resourceCulture);
            }
        }

        internal static Bitmap folderopened2
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("folderopened2", resourceCulture);
            }
        }

        internal static Bitmap Help
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("Help", resourceCulture);
            }
        }

        internal static Bitmap notInclude
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("notInclude", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("FastDBEngineGenerator.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        internal static Bitmap SqlTemplate
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("SqlTemplate", resourceCulture);
            }
        }

        internal static Bitmap table
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("table", resourceCulture);
            }
        }

        internal static Bitmap view
        {
            get
            {
                return (Bitmap) ResourceManager.GetObject("view", resourceCulture);
            }
        }
    }
}

