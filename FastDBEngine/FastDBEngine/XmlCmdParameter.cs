namespace FastDBEngine
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Xml.Serialization;

    public class XmlCmdParameter
    {
        [XmlAttribute, DefaultValue(1)]
        public ParameterDirection Direction = ParameterDirection.Input;
        [XmlAttribute]
        public string Name;
        [DefaultValue(0), XmlAttribute]
        public int Size;
        [XmlAttribute]
        public DbType Type;
    }
}

