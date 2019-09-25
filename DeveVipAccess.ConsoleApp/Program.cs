using System;
using System.Threading.Tasks;

namespace DeveVipAccess.ConsoleApp
{
    public static class Program
    {
        public static async Task MainAsync(string[] args)
        {
            var test = await VipAccess.ProvisionTokenNow();
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
