using DeveVipAccess.Crypto;
using DeveVipAccess.Helpers;
using DeveVipAccess.Symantec.Poco;
using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
