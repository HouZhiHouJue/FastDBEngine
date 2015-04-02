namespace FastDBEngineGenerator
{
    using System;

    public class Field
    {
        public bool Computed;
        public string DataType;
        public string DefaultValue;
        public string Formular;
        public bool Identity;
        public int IncrementValue;
        public bool IsPersisted;
        public int Length;
        private string name;

        public string Name
        {
            get { return name.Substring(0, 1) + name.Substring(1, name.Length - 1).ToLower(); }
            set { name = value; }
        }
        public bool Nullable;
        public int Precision;
        public int scale;
        public int SeedValue;

        public string GetCsDataType()
        {
            return (TypeConverter.GetShortTypeName(this.DataType,this.scale) + (this.Nullable ? (TypeConverter.IsKnowedType(this.DataType) ? "?" : "") : ""));
        }
    }
}

