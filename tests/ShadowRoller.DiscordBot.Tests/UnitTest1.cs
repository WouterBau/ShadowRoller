using DSharpPlus;
using DSharpPlus.EventArgs;
using Emzi0767.Utilities;
using Moq;

namespace ShadowRoller.DiscordBot.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        /*var exceptionHandler = new AsyncEventExceptionHandler<DiscordClient, MessageCreateEventArgs>((_, _, _, _, _) => { });
        var messageCreatedEvent = new AsyncEvent<DiscordClient, MessageCreateEventArgs>("MESSAGE_CREATED", TimeSpan.FromSeconds(1.0), exceptionHandler);
        var mockMessageCreatedEventHandler = new Mock<AsyncEventHandler<DiscordClient, MessageCreateEventArgs>>();*/
        var mockDiscordClient = new Mock<DiscordClient>();
        mockDiscordClient.SetupAdd(_ => _.MessageCreated += (sender, args) => { return Task.CompletedTask; });
        var mockCancellationTokenSource = new Mock<CancellationTokenSource>();

        _ = new DiscordBot(mockDiscordClient.Object, mockCancellationTokenSource.Object);

        mockDiscordClient.VerifyAdd(_ => _.MessageCreated += It.IsAny<AsyncEventHandler<DiscordClient, MessageCreateEventArgs>>(), Times.Exactly(1));
    }
}