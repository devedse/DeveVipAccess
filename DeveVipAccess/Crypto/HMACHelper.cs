using System.Security.Cryptography;
using System.Text;

namespace DeveVipAccess.Crypto
{
    public static class HMACHelper
    {
        public static byte[] HashHMACSHA256(byte[] key, string message)
        {
            return HashHMACSHA256(key, Encoding.UTF8.GetBytes(message));
        }

        public static byte[] HashHMACSHA256(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }
    }
}
