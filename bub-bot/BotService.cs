using DSharpPlus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bub_bot
{
    public class BotService : IBotService
    {
        private readonly IConfiguration configuration;

        public BotService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = configuration["Discord:BotToken"],
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
