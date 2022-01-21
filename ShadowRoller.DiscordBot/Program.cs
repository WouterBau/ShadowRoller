// See https://aka.ms/new-console-template for more information
using DSharpPlus;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World! Starting this bot!");

var config = new ConfigurationBuilder()
    .AddUserSecrets(typeof(Program).Assembly, true)
    .Build();

var cancellationTokenSource = new CancellationTokenSource();

var discordClient = new DiscordClient(
    new DiscordConfiguration
    {
        AutoReconnect = true,
        MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
        Token = config["discord:token"],
        TokenType = TokenType.Bot
    }
);

var discordBot = new DiscordBot(discordClient, cancellationTokenSource);

await discordClient.ConnectAsync();

while(!cancellationTokenSource.IsCancellationRequested)
{

}