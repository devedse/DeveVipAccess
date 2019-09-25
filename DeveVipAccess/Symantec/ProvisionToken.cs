using DeveVipAccess.Helpers;
using DeveVipAccess.Symantec.Poco;
using OtpNet;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeveVipAccess.Symantec
{
    public static class ProvisionToken
    {
        public const string PROVISIONING_URL = "https://services.vip.symantec.com/prov";

        private const string TOKEN_ENCRYPTION_KEY_STRING = "\x01\xad\x9b\xc6\x82\xa3\xaa\x93\xa9\xa3\x23\x9a\x86\xd6\xcc\xd9";
        private static readonly byte[] TOKEN_ENCRYPTION_KEY = TOKEN_ENCRYPTION_KEY_STRING.Select(x => Convert.ToByte(x)).ToArray();

        public static string GenerateRequest()
        {
            var r = new Random();

            var requestData = new SymantecRequest(r, DateTime.UtcNow);

            var srss = new GetSharedSecretRequest()
            {
                Id = requestData.timestamp.ToString(),
                TokenModel = requestData.token_model,
                OtpAlgorithm = new OtpAlgorithm()
                {
                    Type = requestData.otp_algorithm
                },
                SharedSecretDeliveryMethod = requestData.shared_secret_delivery_method,
                DeviceId = new DeviceId()
                {
                    Manufacturer = requestData.manufacturer,
                    SerialNo = requestData.serial,
                    Model = requestData.model
                },
                Extension = new Extension()
                {
                    AppHandle = requestData.app_handle,
                    ClientIDType = requestData.client_id_type,
                    ClientID = requestData.client_id,
                    DistChannel = requestData.dist_channel,
                    ClientInfo = new ClientInfo()
                    {
                        Os = requestData.os,
                        Platform = requestData.platform
                    },
                    ClientTimestamp = requestData.timestamp.ToString(),
                    Data = requestData.data
                }
            };


            var serialized = XmlHelper.Serialize(srss);

            //var blahhh = serialized.Replace("  ", "\t");
            var fixedSerialized = serialized.Replace("vip:", "");

            return fixedSerialized;
        }

        public static async Task<GetSharedSecretResponse> GetProvisioningResponse(HttpClient httpClient, string request)
        {
            var content = new StringContent(request, Encoding.UTF8, "application/xml");

            var req = new HttpRequestMessage(HttpMethod.Post, PROVISIONING_URL);
            req.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("*/*"));
            req.Headers.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            req.Headers.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("deflate"));

            req.Content = content;

            var result = await httpClient.SendAsync(req);

            var resultTxt = await result.Content.ReadAsStringAsync();

            var retval = XmlHelper.Deserialize<GetSharedSecretResponse>(resultTxt);
            return retval;
        }

        public static Token GetTokenFromResponse(GetSharedSecretResponse response)
        {
            var unixTime = UnixTimestampHelper.ConvertToUnixTimeStamp(DateTime.UtcNow);

            var token = new Token()
            {
                TimeSkew = unixTime - long.Parse(response.UTCTimestamp),
                Salt = Base64Helper.Base64DecodeBytes(response.SecretContainer.EncryptionMethod.PBESalt),
                IterationCount = int.Parse(response.SecretContainer.EncryptionMethod.PBEIterationCount),
                Iv = Base64Helper.Base64DecodeBytes(response.SecretContainer.EncryptionMethod.IV),
                Id = response.SecretContainer.Device.Secret.Id,
                Cipher = Base64Helper.Base64DecodeBytes(response.SecretContainer.Device.Secret.Data.Cipher),
                Digest = Base64Helper.Base64DecodeBytes(response.SecretContainer.Device.Secret.Data.Digest.Text),
                Expiry = response.SecretContainer.Device.Secret.Expiry,
                Period = int.Parse(response.SecretContainer.Device.Secret.Usage.TimeStep),
                Counter = int.Parse(response.SecretContainer.Device.Secret.Usage.Counter ?? "0")
            };

            var alg = response.SecretContainer.Device.Secret.Usage.AI.Type;
            var algSplitted = alg.Split('-');

            if (algSplitted.Length == 4 && algSplitted[0] == "HMAC" && algSplitted[2] == "TRUNC" && algSplitted[3].EndsWith("DIGITS"))
            {
                token.Algorithm = algSplitted[1].ToLowerInvariant();
                token.Digits = int.Parse(algSplitted[3].Substring(0, algSplitted[3].Length - 6));
            }
            else
            {
                throw new InvalidOperationException($"Unknown algorithm: {alg}");
            }

            return token;
        }

        public static byte[] DecryptKey(byte[] iv, byte[] cipher)
        {
            var aes = Aes.Create();
            var decryptor = aes.CreateDecryptor(TOKEN_ENCRYPTION_KEY, iv);

            var decrypted = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

            return decrypted;
        }
    }
}
