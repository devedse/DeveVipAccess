using DeveVipAccess.Crypto;
using DeveVipAccess.Helpers;
using DeveVipAccess.Symantec.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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


            return serialized;
        }

        public static async Task<HttpResponseMessage> GetProvisioningResponse(HttpClient httpClient, string request)
        {
            var retval = await httpClient.PostAsync(PROVISIONING_URL, new StringContent(request));
            return retval;
        }
    }
}
