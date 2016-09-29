using System;
using System.IO;
using System.Xml.Serialization;

namespace CRAP.Helper
{
    public class XmlHelper
    {
        public static object DeserializeToObject(Stream stream, Type type)
        {
            try
            {
                var ser = new XmlSerializer(type);


                return ser.Deserialize(stream);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string SerializeToXml(object obj, Type type)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(type);
            serializer.Serialize(stringwriter, obj);
            return stringwriter.ToString();
        }
    }
}
