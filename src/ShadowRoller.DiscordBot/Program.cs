// See https://aka.ms/new-console-template for more information
using DSharpPlus;
using Microsoft.Extensions.Configuration;
using ShadowRoller.DiscordBot;

Console.WriteLine("Hello, World! Starting this bot!");

var config = new ConfigurationBuilder()
    .AddUserSecrets(typeof(Program).Assembly, true)
    .AddEnvironmentVariables()
    .Build();

var token = config["token"];
if (string.IsNullOrWhiteSpace(token))
{
    Console.WriteLine("Token is missing. Please provide a token.");
    return;
}

var cancellationTokenSource = new CancellationTokenSource();

var discordClient = new DiscordClient(
    new DiscordConfiguration
    {
        AutoReconnect = true,
        MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
        Token = token,
        TokenType = TokenType.Bot
    }
);

var _ = new DiscordBot(discordClient, cancellationTokenSource);

await discordClient.ConnectAsync();

while (!cancellationTokenSource.IsCancellationRequested)
{

}