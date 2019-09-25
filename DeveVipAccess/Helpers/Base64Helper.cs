using System;
using System.Text;

namespace DeveVipAccess.Helpers
{
    public static class Base64Helper
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.ASCII.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.ASCII.GetString(base64EncodedBytes);
        }

        public static string Base64EncodeBytes(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static byte[] Base64DecodeBytes(string base64EncodedData)
        {
            return Convert.FromBase64String(base64EncodedData);
        }
    }
}
