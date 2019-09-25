using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace DeveVipAccess.Helpers
{
    public static class XmlHelper
    {
        public static string Serialize<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var memstream = new MemoryStream())
            {
                serializer.Serialize(memstream, obj);
                return Encoding.UTF8.GetString(memstream.GetBuffer());
            }
        }

        public static T Deserialize<T>(string input)
        {
            var serializer = new XmlSerializer(typeof(T));

            var byteData = Encoding.UTF8.GetBytes(input);
            using (var memstream = new MemoryStream(byteData))
            {
                var retval = serializer.Deserialize(memstream);
                return (T)retval;
            }
        }
    }
}
