using System.Text;
using DSharpPlus;
using DSharpPlus.EventArgs;
using ShadowRoller.Domain.Contexts;
using ShadowRoller.Domain.Contexts.Dice;
using ShadowRoller.Domain.Contexts.ShadowRun;

public class DiscordBot
{
    private readonly IRollContextParser<DiceRollContext, DiceModifierSumRollResult> _diceRollContextParser = new DiceRollContextParser();
    private readonly IRollContextParser<ShadowRunRollContext, ShadowRunRollResult> _shadowRunRollContext = new ShadowRunContextParser();
    private const string PREFIX = "!sr-";
    private const string DELIMITER = " ";
    private readonly CancellationTokenSource _cancellationTokenSource;

    public DiscordBot(DiscordClient discordClient, CancellationTokenSource cancellationTokenSource)
    {
        _cancellationTokenSource = cancellationTokenSource;
        discordClient.MessageCreated += OnMessageCreated;
    }

    private async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
    {
        if (!e.Message.Content.StartsWith(PREFIX, StringComparison.InvariantCultureIgnoreCase))
            return;
        
        var messageParts = e.Message.Content.Split(DELIMITER).ToArray();
        var command = messageParts.First().Replace(PREFIX, "").ToLower();
        var args = messageParts.Skip(1).ToArray();

        switch (command)
        {
            case "exit":
                await sender.SendMessageAsync(e.Message.Channel, "Ok, I'm leaving");
                _cancellationTokenSource.Cancel();
                break;
            case "sr5":
                var srContext = _shadowRunRollContext.ParseToRollContext(args);
                var srResult = srContext.Resolve();
                await Evaluate(sender, e, srResult);
                break;
            case "roll":
                var diceContext = _diceRollContextParser.ParseToRollContext(args);
                var diceResult = diceContext.Resolve();
                await Evaluate(sender, e, diceResult);
                break;
        }
    }

    private async Task Evaluate(DiscordClient sender, MessageCreateEventArgs e, ShadowRunRollResult rollResult)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{e.Message.Author.Username} Rolled: {string.Join(" ", rollResult.DiceResults)} Limit: {rollResult.HitLimit}");
        sb.AppendLine($"Net amount hits: {rollResult.NetAmountHits} Amount misses: {rollResult.AmountMisses}");
        if (rollResult.HasGlitchedCritically)
            sb.AppendLine("CRITICAL GLITCH!!");
        else if (rollResult.HasGlitched)
            sb.AppendLine("REGULAR GLITCH!!");
        await sender.SendMessageAsync(e.Message.Channel, sb.ToString());
    }

    private async Task Evaluate(DiscordClient sender, MessageCreateEventArgs e, DiceModifierSumRollResult rollResult)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{e.Message.Author.Username} Rolled: {string.Join(" ", rollResult.DiceResults)} Modifiers: {string.Join(" ", rollResult.Modifiers)}");
        sb.AppendLine($"Result: {rollResult.Result}");
        await sender.SendMessageAsync(e.Message.Channel, sb.ToString());
    }

}