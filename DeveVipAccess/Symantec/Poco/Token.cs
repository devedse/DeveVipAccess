namespace DeveVipAccess.Symantec.Poco
{
    public class Token
    {
        public long TimeSkew { get; set; }
        public byte[] Salt { get; set; }
        public int IterationCount { get; set; }
        public byte[] Iv { get; set; }
        public string Id { get; set; }
        public byte[] Cipher { get; set; }
        public byte[] Digest { get; set; }
        public string Expiry { get; set; }
        public int Period { get; set; }
        public int Counter { get; set; }
        public string Algorithm { get; set; }
        public int Digits { get; set; }
    }
}
