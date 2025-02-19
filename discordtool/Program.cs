using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace discordtool
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Title = "EZ DISCORD TOOL";
                Console.Clear();
                Console.WriteLine("\r\n  ______ ______  _____ _____  _____  _____ ____  _____  _____    _______ ____   ____  _      \r\n |  ____|___  / |  __ \\_   _|/ ____|/ ____/ __ \\|  __ \\|  __ \\  |__   __/ __ \\ / __ \\| |     \r\n | |__     / /  | |  | || | | (___ | |   | |  | | |__) | |  | |    | | | |  | | |  | | |     \r\n |  __|   / /   | |  | || |  \\___ \\| |   | |  | |  _  /| |  | |    | | | |  | | |  | | |     \r\n | |____ / /__  | |__| || |_ ____) | |___| |__| | | \\ \\| |__| |    | | | |__| | |__| | |____ \r\n |______/_____| |_____/_____|_____/ \\_____\\____/|_|  \\_\\_____/     |_|  \\____/ \\____/|______|\r\n                                                                                             \r\n                                                                                             \r\n");
                Console.Write("Enter  Discord Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter  Discord Password: ");
                string pass = Console.ReadLine();
                await getToken(email, pass);

                if (!ReturnToMainScreen())
                {
                    break;
                }
            }
        }

        async static Task getToken(string email, string pass)
        {
            string url = "https://discord.com/api/v9/auth/login";
            var payload = new Dictionary<string, string>()
            {
                { "login", email }, { "password", pass }
            };
            string jsonPayload = JsonConvert.SerializeObject(payload);
            HttpClient client = new HttpClient();
            StringContent stringContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("\nLogin Successful!");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
                JObject json = JObject.Parse(responseContent);
                Console.WriteLine($"Token: {json["token"]}");
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
            }
        }

        static bool ReturnToMainScreen()
        {
            while (true)
            {
                Console.Write("\nReturn to main screen y/n: ");
                string answer = Console.ReadLine().Trim().ToLower();
                if (answer == "y")
                {
                    return true;
                }
                else if (answer == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                }
            }
        }
    }
}
