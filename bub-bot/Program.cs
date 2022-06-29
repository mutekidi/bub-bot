using System;
using System.Threading.Tasks;
using DSharpPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace bub_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateDefaultBuilder().Build();
            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            var botInstance = provider.GetService<BotService>();
            botInstance.MainAsync().GetAwaiter().GetResult();
            host.Run();
        }

        static IHostBuilder CreateDefaultBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<BotService>();
                });
        }
    }
}