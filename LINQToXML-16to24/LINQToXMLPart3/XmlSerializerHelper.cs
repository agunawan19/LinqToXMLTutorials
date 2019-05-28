using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace LINQToXMLPart3
{
    public static class XmlSerializerHelper
    {
        public static string Serialize<T>(this T value)
        {
            string returnValue = string.Empty;

            if (value != null)
            {
                var serializer = new XmlSerializer(typeof(T));

                using (var sw = new StringWriter())
                {
                    serializer.Serialize(sw, value);
                    returnValue = sw.ToString();
                }
            }

            return returnValue;
        }

        public static T Deserialize<T>(this T value, string xml)
        {
            T returnValue = default;

            if (!string.IsNullOrWhiteSpace(xml))
            {
                var serializer = new XmlSerializer(typeof(T));

                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(xml)))
                {
                    ms.Position = 0;
                    returnValue = (T)serializer.Deserialize(ms);
                }
            }

            return returnValue;
        }
    }
}
