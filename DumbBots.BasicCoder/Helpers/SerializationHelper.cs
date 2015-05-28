using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DumbBots.BasicCoder
{
    public static class SerializationHelper
    {
        public static string ToXml(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartDocument();

                    XmlSerializer ser = new XmlSerializer(obj.GetType(), "");
                    ser.Serialize(writer, obj);

                    writer.WriteEndDocument();
                    writer.Flush();

                    stream.Position = 0;

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static object FromXml(string xml, Type type)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(xml);
                    writer.Flush();

                    stream.Position = 0;

                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        XmlSerializer ser = new XmlSerializer(type);

                        try
                        {
                            return ser.Deserialize(reader);
                        }
                        catch (Exception e)
                        {
                            var typeName = type.ToString();
                            if (typeName == string.Empty)
                                throw new Exception("Object could not be deserialized from supplied XML.", e);
                            else
                                throw new Exception(string.Format("Object type \"{0}\" could not be deserialized from XML.", typeName), e);
                        }
                    }
                }
            }
        }
    }
}