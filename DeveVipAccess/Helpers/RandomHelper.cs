using System;
using System.Linq;

namespace DeveVipAccess.Helpers
{
    public static class RandomHelper
    {
        public static string GenerateRandomStringOfLength(Random random, string characters, int length)
        {
            return new string(Enumerable.Repeat(characters, 12).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
