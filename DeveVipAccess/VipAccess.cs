﻿using DeveVipAccess.Crypto;
using OtpNet;
using System;

namespace DeveVipAccess
{
    public static class VipAccess
    {
        /// <summary>
        /// Generate a TotpKey for a specific secret
        /// </summary>
        /// <param name="secret">The secret</param>
        /// <param name="utcTime">The DateTime (defaults to DateTime.UtcNow)</param>
        /// <returns></returns>
        public static string CreateCurrentTotpKey(string secret, DateTime? utcTime = null)
        {
            var preKey = Lenient.Lenient_b32decode(secret);
            //Console.WriteLine(BitConverter.ToString(preKey));
            var totp = new Totp(preKey);

            var code = totp.ComputeTotp(utcTime ?? DateTime.UtcNow);
            return code;
        }
    }
}