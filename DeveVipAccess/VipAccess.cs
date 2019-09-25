using DeveVipAccess.Crypto;
using DeveVipAccess.Helpers;
using DeveVipAccess.Symantec;
using DeveVipAccess.Symantec.Poco;
using OtpNet;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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

        public static async Task<TokenInfo> ProvisionTokenNow()
        {
            var request = ProvisionToken.GenerateRequest();

            using (var httpClient = new HttpClient())
            {
                var response = await ProvisionToken.GetProvisioningResponse(httpClient, request);

                var otpToken = ProvisionToken.GetTokenFromResponse(response);
                var otpSecret = ProvisionToken.DecryptKey(otpToken.Iv, otpToken.Cipher);
                var otpSecretb32 = Base32Helper.Base32EncodeBytes(otpSecret).ToUpperInvariant();

                if (await ProvisionToken.CheckToken(otpToken, otpSecret, httpClient))
                {
                    return new TokenInfo(1, otpSecretb32, otpToken);
                }

                throw new InvalidOperationException("Created token is not valid");
            }

            throw new InvalidOperationException("Could not create a token");
        }
    }
}
