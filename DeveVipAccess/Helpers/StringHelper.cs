using System.Text;

namespace DeveVipAccess.Helpers
{
    public static class StringHelper
    {
        public static string ToUtf8(string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
