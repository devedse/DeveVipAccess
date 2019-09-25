using OtpNet;
using System.Text;

namespace DeveVipAccess.Helpers
{
    public static class Base32Helper
    {
        public static string Base32Encode(string plainText)
        {
            var plainTextBytes = Encoding.ASCII.GetBytes(plainText);
            return Base32Encoding.ToString(plainTextBytes);
        }

        public static string Base32Decode(string base32EncodedData)
        {
            var base32EncodedBytes = Base32Encoding.ToBytes(base32EncodedData);
            return Encoding.ASCII.GetString(base32EncodedBytes);
        }

        public static string Base32EncodeBytes(byte[] bytes)
        {
            return Base32Encoding.ToString(bytes);
        }

        public static byte[] Base32DecodeBytes(string base32EncodedData)
        {
            return Base32Encoding.ToBytes(base32EncodedData);
        }
    }
}
