using System;
using System.Threading.Tasks;

namespace DeveVipAccess.ConsoleApp
{
    public static class Program
    {
        public static async Task MainAsync(string[] args)
        {
            var secret = await VipAccess.ProvisionTokenNow();
            Console.WriteLine($"Secret: {secret}");
            var token = VipAccess.CreateCurrentTotpKey(secret.Secret);
            Console.WriteLine($"Token: {token}");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            MainAsync(args).GetAwaiter().GetResult();

            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
        }
    }
}
