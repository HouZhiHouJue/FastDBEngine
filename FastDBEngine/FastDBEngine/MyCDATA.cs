namespace FastDBEngine
{
    using System;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    public class MyCDATA : IXmlSerializable
    {
        private string sqlData;

        public MyCDATA()
        {
        }

        public MyCDATA(string value)
        {
            this.sqlData = value;
        }

        public static implicit operator string(MyCDATA text)
        {
            return text.ToString();
        }

        public static implicit operator MyCDATA(string text)
        {
            return new MyCDATA(text);
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            this.sqlData = reader.ReadElementContentAsString();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteCData(this.sqlData);
        }

        public override string ToString()
        {
            return this.sqlData;
        }

        public string Value
        {
            get
            {
                return this.sqlData;
            }
        }
    }
}

