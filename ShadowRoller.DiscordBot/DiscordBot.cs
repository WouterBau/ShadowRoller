using DSharpPlus;
using DSharpPlus.EventArgs;

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
        if(!e.Message.Content.StartsWith(PREFIX, StringComparison.InvariantCultureIgnoreCase))
            return;
        
        var messageParts = e.Message.Content.Split(DELIMITER).ToArray();
        var command = messageParts.First().Replace(PREFIX, "").ToLower();

        switch(command)
        {
            case "exit":
                await sender.SendMessageAsync(e.Message.Channel, "Ok, I'm leaving");
                _cancellationTokenSource.Cancel();
                break;
            case "eval":
                break;
            case "roll":
                break;
        }

    }
}