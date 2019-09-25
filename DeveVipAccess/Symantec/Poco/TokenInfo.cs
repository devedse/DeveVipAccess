using System;

namespace DeveVipAccess.Symantec.Poco
{
    public class TokenInfo
    {
        public int Version { get; set; }
        public string Secret { get; set; }
        public string Id => OriginalToken.Id;
        public string Expiry => OriginalToken.Expiry;

        public Token OriginalToken { get; set; }

        public TokenInfo(int version, string secret, Token originalToken)
        {
            Version = version;
            Secret = secret;
            OriginalToken = originalToken;
        }

        public override string ToString()
        {
            return $"version {Version}{Environment.NewLine}secret {Secret}{Environment.NewLine}id {Id}{Environment.NewLine}expiry {Expiry}";
        }
    }
}
