using DSharpPlus;
using DSharpPlus.EventArgs;

public class DiscordBot
{

    private readonly CancellationTokenSource _cancellationTokenSource;

    public DiscordBot(DiscordClient discordClient, CancellationTokenSource cancellationTokenSource)
    {
        _cancellationTokenSource = cancellationTokenSource;
        discordClient.MessageCreated += OnMessageCreated;
    }

    private async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
    {
        throw new NotImplementedException();
    }
}