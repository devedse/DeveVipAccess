using System;

namespace DeveVipAccess.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var test = VipAccess.ProvisionTokenNow();
        }
    }
}
