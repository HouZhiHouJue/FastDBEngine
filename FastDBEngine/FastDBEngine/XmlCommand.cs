namespace FastDBEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Xml.Serialization;

    public class XmlCommand
    {
        [XmlAttribute("Name")]
        public string CommandName;
        [XmlElement]
        public MyCDATA CommandText;
        [DefaultValue(1), XmlAttribute]
        public System.Data.CommandType CommandType = System.Data.CommandType.Text;
        [XmlAttribute]
        public string Database;
        [XmlArrayItem("Parameter")]
        public List<XmlCmdParameter> Parameters = new List<XmlCmdParameter>();
        [XmlAttribute, DefaultValue(30)]
        public int Timeout = 30;
    }
}

