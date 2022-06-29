using System;
using System.Threading.Tasks;
using DSharpPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace bub_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var token = "";

            using(StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                var data = JsonConvert.DeserializeObject<appsettings>(json);
                token = data?.token ?? "";
            }

            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong!");         

            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}