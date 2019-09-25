using System;
using System.Collections.Generic;
using System.Text;

namespace DeveVipAccess.Symantec.Poco
{
    public class Token
    {
        public long TimeSkew { get; set; }
        public string Salt { get; set; }
        public int IterationCount { get; set; }
        public string Iv { get; set; }
        public string Id { get; set; }
        public string Cipher { get; set; }
        public string Digest { get; set; }
        public string Expiry { get; set; }
        public int Period { get; set; }
        public int Counter { get; set; }
        public string Algorithm { get; set; }
        public int Digits { get; set; }
    }
}
