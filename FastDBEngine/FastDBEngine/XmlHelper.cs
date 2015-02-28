namespace FastDBEngine
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class XmlHelper
    {
        private static void XmlSerializeObj(Stream stream, object obj, Encoding encoding)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("o");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlWriterSettings settings = new XmlWriterSettings {
                Indent = true,
                NewLineChars = "\r\n",
                Encoding = encoding,
                IndentChars = "    "
            };
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj);
                writer.Close();
            }
        }

        public static T XmlDeserialize<T>(string string_0, Encoding encoding)
        {
            T local;
            if (string.IsNullOrEmpty(string_0))
            {
                throw new ArgumentNullException("s");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(encoding.GetBytes(string_0)))
            {
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    local = (T) serializer.Deserialize(reader);
                }
            }
            return local;
        }

        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            return XmlDeserialize<T>(File.ReadAllText(path, encoding), encoding);
        }

        public static string XmlSerialize(object object_0, Encoding encoding)
        {
            string str;
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeObj(stream, object_0, encoding);
                stream.Position = 0L;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        public static string XmlSerializerObject(object object_0)
        {
            string str;
            if (object_0 == null)
            {
                throw new ArgumentNullException("o");
            }
            Encoding encoding = Encoding.UTF8;
            XmlSerializer serializer = new XmlSerializer(object_0.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                XmlWriterSettings settings = new XmlWriterSettings {
                    Indent = true,
                    NewLineChars = "\r\n",
                    Encoding = encoding,
                    OmitXmlDeclaration = true,
                    IndentChars = "    "
                };
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add("", "");
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(writer, object_0, namespaces);
                    writer.Close();
                }
                stream.Position = 0L;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        public static void XmlSerializeToFile(object object_0, string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializeObj(stream, object_0, encoding);
            }
        }
    }
}

