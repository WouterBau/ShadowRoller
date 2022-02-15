using System.Text;
using DSharpPlus;
using DSharpPlus.EventArgs;
using ShadowRoller.Domain;

public class DiscordBot
{

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
            case "eval":
                await Evaluate(sender, e, args);
                break;
            case "roll":
                await Roll(sender, e, args);
                break;
            case "test":
                await Test(sender, e, args);
                break;
        }
    }

    private async Task Roll(DiscordClient sender, MessageCreateEventArgs e, string[] args)
    {
        var parsedValues = ParseValues(args);
        if (!parsedValues.Values.Any())
        {
            await sender.SendMessageAsync(e.Message.Channel, "Invalid input");
            return;
        }
        var rollResult = ShadowRunRoller.RollAmountDice(parsedValues.Values.First(), parsedValues.HitLimit);
        await Evaluate(sender, e, rollResult);
    }

    private async Task Test(DiscordClient sender, MessageCreateEventArgs e, string[] args)
    {
        var parsedValues = ParseValues(args);
        var rollResult = ShadowRunRoller.RollByAttributes(parsedValues.Values, parsedValues.HitLimit);
        await Evaluate(sender, e, rollResult);
    }

    private async Task Evaluate(DiscordClient sender, MessageCreateEventArgs e, string[] args)
    {
        var parsedValues = ParseValues(args);
        var rollResult = new ShadowRunRollResult
        {
            DiceResults = parsedValues.Values,
            HitLimit = parsedValues.HitLimit
        };
        await Evaluate(sender, e, rollResult);
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

    private (int[] Values, int? HitLimit) ParseValues(string[] args)
    {
        var values = new List<int>();
        int? hitLimit = null;
        foreach (var arg in args.Select(x => x.Trim()))
        {
            if (arg.StartsWith('[') && arg.EndsWith(']'))
            {
                hitLimit = GetHitLimit(arg);
                if (hitLimit.HasValue)
                    break;
            }
            else
            {
                var value = GetValue(arg);
                if (value.HasValue)
                    values.Add(value.Value);
            }
        }
        return (values.ToArray(), hitLimit);
    }

    private int? GetHitLimit(string arg)
    {
        arg = arg.Replace("[", "").Replace("]", "");
        return GetValue(arg);
    }

    private int? GetValue(string arg)
    {
        if (int.TryParse(arg, out var value))
            return value;
        return null;
    }

}