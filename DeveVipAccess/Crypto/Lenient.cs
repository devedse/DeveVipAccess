namespace DeveVipAccess.Crypto
{
    public static class Lenient
    {
        public static byte[] Lenient_b32decode(string data)
        {
            var data2 = data.ToUpper(); //Ensure correct case

            var paddingThing = (8 - (data2.Length)) % 8;
            var extraPadding = "".PadRight(paddingThing, '=');
            var data3 = data2 + extraPadding;


            var base32bytes = Base32.ToBytes(data3);
            return base32bytes;
        }
    }
}
