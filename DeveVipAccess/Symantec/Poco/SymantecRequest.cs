using DeveVipAccess.Crypto;
using DeveVipAccess.Helpers;
using OtpNet;
using System;
using System.Linq;

namespace DeveVipAccess.Symantec.Poco
{
    public class SymantecRequest
    {
        private const string HMAC_KEY_STRING = "\xdd\x0b\xa6\x92\xc3\x8a\xa3\xa9\x93\xa3\xaa\x26\x96\x8c\xd9\xc2\xaa\x2a\xa2\xcb\x23\xb7\xc2\xd2\xaa\xaf\x8f\x8f\xc9\xa0\xa9\xa1";
        private static readonly byte[] HMAC_KEY = HMAC_KEY_STRING.Select(x => Convert.ToByte(x)).ToArray();

        public long timestamp { get; set; }
        public string token_model { get; set; } = "VSST";
        public string otp_algorithm { get; set; } = "HMAC-SHA1-TRUNC-6DIGITS";
        public string shared_secret_delivery_method { get; set; } = "HTTPS";
        public string manufacturer { get; set; } = "Apple Inc.";
        public string serial { get; set; }
        public string model { get; set; }
        public string app_handle { get; set; } = "iMac010200";
        public string client_id_type { get; set; } = "BOARDID";
        public string client_id { get; set; }
        public string dist_channel { get; set; } = "Symantec";
        public string platform { get; set; } = "iMac";
        public string os { get; set; }
        public string data { get; set; }

        public SymantecRequest(Random random, DateTime utcTime)
        {
            timestamp = UnixTimestampHelper.ConvertToUnixTimeStamp(utcTime);
            var default_model = $"MacBookPro{random.Next(1, 12)},{random.Next(1, 4)}";

            model = default_model;
            serial = RandomHelper.GenerateRandomStringOfLength(random, CharacterHelper.AllDigits + CharacterHelper.AllUpperCase, 12);
            client_id = $"Mac-{RandomHelper.GenerateRandomStringOfLength(random, CharacterHelper.HexCharacters, 16)}";
            os = default_model;

            var data_before_hmac = $"{timestamp}{timestamp}{client_id_type}{client_id}{dist_channel}";
            var data_before_hmac_utf8 = StringHelper.ToUtf8(data_before_hmac);

            var digesthmac = HMACHelper.HashHMACSHA256(HMAC_KEY, data_before_hmac_utf8);
            var base64hmac = Convert.ToBase64String(digesthmac);

            data = base64hmac;
        }
    }
}
