namespace FastDBEngine
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DbColumnAttribute : Attribute
    {
        internal bool HasPrefix;
        public string Alias { get; set; }

        public bool IgnoreLoad { get; set; }

        public string SubItemPrefix { get; set; }

    }
}

