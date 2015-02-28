namespace FastDBEngine
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false, Inherited=false)]
    public sealed class DbColumnAttribute : Attribute
    {
        internal bool HasPrefix;
        [CompilerGenerated]
        private bool ignoreLoad;
        [CompilerGenerated]
        private string alias;
        [CompilerGenerated]
        private string subItemPrefix;

        public string Alias
        {
            [CompilerGenerated]
            get
            {
                return this.alias;
            }
            [CompilerGenerated]
            set
            {
                this.alias = value;
            }
        }

        public bool IgnoreLoad
        {
            [CompilerGenerated]
            get
            {
                return this.ignoreLoad;
            }
            [CompilerGenerated]
            set
            {
                this.ignoreLoad = value;
            }
        }

        public string SubItemPrefix
        {
            [CompilerGenerated]
            get
            {
                return this.subItemPrefix;
            }
            [CompilerGenerated]
            set
            {
                this.subItemPrefix = value;
            }
        }
    }
}

