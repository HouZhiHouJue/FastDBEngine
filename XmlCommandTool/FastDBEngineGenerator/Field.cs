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
        public int int_0;
        public bool IsPersisted;
        public int Length;
        public string Name;
        public bool Nullable;
        public int Precision;
        public int SeedValue;

        public string GetCsDataType()
        {
            return (TypeConverter.GetShortTypeName(this.DataType) + (this.Nullable ? (TypeConverter.IsKnowedType(this.DataType) ? "?" : "") : ""));
        }
    }
}

