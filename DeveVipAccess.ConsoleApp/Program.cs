using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeveVipAccess.ConsoleApp
{
    public static class Program
    {
        public static async Task MainAsync(string[] args)
        {
            string secret = "";
            if (args.Any())
            {
                secret = string.Join("", args).Replace(" ", "");
                var token = VipAccess.CreateCurrentTotpKey(secret);
                Console.WriteLine($"Writing token '${token}' to token.txt");
                File.WriteAllText("token.txt", token);
            }
            else
            {
                while (string.IsNullOrWhiteSpace(secret))
                {
                    Console.WriteLine("Please choose what you want to do:");
                    Console.WriteLine("1: Generate new secret + show 2fa token");
                    Console.WriteLine("2: Enter existing secret to show 2fa token");

                    Console.Write("> ");
                    var inp = Console.ReadLine();

                    switch (inp)
                    {
                        case "1":
                            Console.WriteLine("Generating secret...");
                            var s = await VipAccess.ProvisionTokenNow();
                            secret = s.Secret;
                            Console.WriteLine($"Secret:{Environment.NewLine}{ApplyThisInFrontOfLines(s.ToString(), "\t")}");
                            break;
                        case "2":
                            Console.Write("Enter your secret> ");
                            secret = Console.ReadLine().Trim();
                            break;
                        default:
                            Console.WriteLine("Error: Option not found, please choose one of the options above");
                            break;
                    }
                }

                string currentToken = "";
                Console.WriteLine();
                Console.WriteLine(@"\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_");
                Console.WriteLine(@"\_\_\_ Auto refreshing token, press ESC to exit the application \_\_\_");
                Console.WriteLine(@"\_\_\_          Token will refresh every ~30 seconds            \_\_\_");
                Console.WriteLine(@"\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_");
                Console.WriteLine();
                while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Escape)
                {
                    var newToken = VipAccess.CreateCurrentTotpKey(secret);
                    if (currentToken != newToken)
                    {
                        currentToken = newToken;
                        Console.WriteLine($"\tCurrent token: {currentToken}");
                    }
                    await Task.Delay(100);
                }
            }
        }

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        public static string ApplyThisInFrontOfLines(string input, string prepender)
        {
            var lines = GetLines(input);
            var linesWithPrepender = lines.Select(t => $"{prepender}{t}");
            var joined = string.Join(Environment.NewLine, linesWithPrepender);
            return joined;
        }

        public static string[] GetLines(string input)
        {
            return input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
