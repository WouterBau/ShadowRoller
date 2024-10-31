// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShadowRoller.DiscordBot;
using ShadowRoller.Domain.Contexts.Dice;
using ShadowRoller.Domain.Contexts.ShadowRun;
using ShadowRoller.Domain.Contexts;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddUserSecrets<Program>();
            config.AddEnvironmentVariables();
        })
        .ConfigureLogging((hostContext, logging) =>
        {
            logging.AddConsole();
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.Configure<DiscordBotOptions>(hostContext.Configuration.GetSection(DiscordBotOptions.SECTIONNAME));
            services.AddScoped<IRollContextParser<DiceRollContext, DiceModifierSumRollResult>, DiceRollContextParser>();
            services.AddScoped<IRollContextParser<ShadowRunRollContext, ShadowRunRollResult>, ShadowRunContextParser>();
            services.AddHostedService<DiscordBot>();
        });

Console.WriteLine("Hello, World! Starting the host!");

var host = CreateHostBuilder(args).Build();
await host.RunAsync();