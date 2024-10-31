using DSharpPlus;
using Microsoft.Extensions.Hosting;
using ShadowRoller.Domain.Contexts.Dice;
using ShadowRoller.Domain.Contexts.ShadowRun;
using ShadowRoller.Domain.Contexts;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ShadowRoller.DiscordBot;
internal class DiscordBot : IHostedService
{
    private readonly ILogger<DiscordBot> _logger;
    private readonly DiscordClient _discordClient;
    private readonly IRollContextParser<DiceRollContext, DiceModifierSumRollResult> _diceRollContextParser;
    private readonly IRollContextParser<ShadowRunRollContext, ShadowRunRollResult> _shadowRunRollContext;
    private readonly string _prefix;
    private readonly string _delimiter;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public DiscordBot(
        ILogger<DiscordBot> logger,
        IOptions<DiscordBotOptions> options,
        IRollContextParser<DiceRollContext, DiceModifierSumRollResult> diceRollContextParser,
        IRollContextParser<ShadowRunRollContext, ShadowRunRollResult> shadowRunRollContext,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        _logger = logger;

        OptionsValidation(options.Value);

        _prefix = options.Value.Prefix;
        _delimiter = options.Value.Delimiter;
        _discordClient = new DiscordClient(
            new DiscordConfiguration
            {
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Information,
                Token = options.Value.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.GuildMessages | DiscordIntents.MessageContents
            }
        );

        _diceRollContextParser = diceRollContextParser;
        _shadowRunRollContext = shadowRunRollContext;

        _hostApplicationLifetime = hostApplicationLifetime;
    }

    private void OptionsValidation(DiscordBotOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Token))
        {
            var ex = new ArgumentException("Token is missing. Please provide a token.");
            _logger.LogError(ex, ex.Message);
            throw ex;
        }

        if (string.IsNullOrWhiteSpace(options.Prefix))
        {
            var ex = new ArgumentException("Prefix is missing. Please provide a prefix.");
            _logger.LogError(ex, ex.Message);
            throw ex;
        }

        if (string.IsNullOrEmpty(options.Delimiter))
        {
            var ex = new ArgumentException("Delimiter is missing. Please provide a delimiter.");
            _logger.LogError(ex, ex.Message);
            throw ex;
        }

    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello, World! Starting this bot!");
        _discordClient.MessageCreated += OnMessageCreated;
        await _discordClient.ConnectAsync();
    }

    private async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
    {
        if (!e.Message.Content.StartsWith(_prefix, StringComparison.InvariantCultureIgnoreCase))
            return;

        var messageParts = e.Message.Content.Split(_delimiter).ToArray();
        var command = messageParts.First().Replace(_prefix, "").ToLower();
        var args = messageParts.Skip(1).ToArray();

        switch (command)
        {
            case "exit":
                await sender.SendMessageAsync(e.Message.Channel, "Ok, I'm leaving");
                _hostApplicationLifetime.StopApplication();
                break;
            case "sr5":
                var srContext = _shadowRunRollContext.ParseToRollContext(args);
                var srResult = srContext.Resolve();
                await PrintResult(sender, e, srResult);
                break;
            case "roll":
                var diceContext = _diceRollContextParser.ParseToRollContext(args);
                var diceResult = diceContext.Resolve();
                await PrintResult(sender, e, diceResult);
                break;
        }
    }

    private static async Task PrintResult(DiscordClient sender, MessageCreateEventArgs e, IRollResult rollResult)
    {
        var result = rollResult.ToString(e.Message.Author.Username);
        await sender.SendMessageAsync(e.Message.Channel, result);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Discord bot.");
        await _discordClient.DisconnectAsync();
    }
}
