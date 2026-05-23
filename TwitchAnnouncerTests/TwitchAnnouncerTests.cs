using Moq;

namespace TwitchAnnouncerTests;

[TestFixture]
public static class Tests
{
    [Test]
    public static void RunAsync_Test()
    {
        // Arrange
        var mockTwitch = new Mock<Twitch.Interfaces.ITwitch>();

        var cancellationTokenSource = new CancellationTokenSource();

        var getStreamsAsyncCount = 0;

        mockTwitch.Setup(twitch => twitch.GetStreamAsync(cancellationTokenSource.Token))
            .ReturnsAsync(() => getStreamsAsyncCount++ < 3 ? null : new Twitch.Responses.Stream());

        string[] messages =
        [
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString()
        ];

        var sendAnnouncementAsyncCount = 0;

        var moderatorId = Guid.NewGuid().ToString();

        mockTwitch
            .Setup(twitch => twitch.SendAnnouncementAsync(
                It.Is<Twitch.Params.SendAnnouncementAsyncParams>(sendAnnouncementAsyncParams =>
                    sendAnnouncementAsyncParams.ModeratorId == moderatorId &&
                    messages.AsEnumerable().Contains(sendAnnouncementAsyncParams.Message)),
                cancellationTokenSource.Token))
            .Callback(() =>
            {
                if (sendAnnouncementAsyncCount++ >= 4)
                {
                    throw new TaskCanceledException();
                }
            });

        var config = new TwitchAnnouncer.Config
        {
            Parameter = Guid.NewGuid().ToString(),
            ClientId = Guid.NewGuid().ToString(),
            MillisecondsDelay = 1_2000_000,
            Messages = messages,
            BroadcasterId = Guid.NewGuid().ToString(),
            ModeratorId = moderatorId
        };

        var mockDelayHandler = new Mock<HttpRequestMessageHandler.Interfaces.IDelayHandler>();

        var twitchAnnouncer = new TwitchAnnouncer.TwitchAnnouncer(mockTwitch.Object, config, mockDelayHandler.Object);

        // Act
        Assert.ThrowsAsync<TaskCanceledException>(() => twitchAnnouncer.RunAsync(cancellationTokenSource.Token));

        // Assert
        mockTwitch.Verify(twitch => twitch.GetStreamAsync(cancellationTokenSource.Token), Times.Exactly(8));

        mockTwitch.Verify(
            twitch => twitch.SendAnnouncementAsync(
                It.Is<Twitch.Params.SendAnnouncementAsyncParams>(sendAnnouncementAsyncParams =>
                    sendAnnouncementAsyncParams.ModeratorId == moderatorId &&
                    sendAnnouncementAsyncParams.Message == messages.ElementAt(0)), cancellationTokenSource.Token),
            Times.Exactly(2));

        mockTwitch.Verify(
            twitch => twitch.SendAnnouncementAsync(
                It.Is<Twitch.Params.SendAnnouncementAsyncParams>(sendAnnouncementAsyncParams =>
                    sendAnnouncementAsyncParams.ModeratorId == moderatorId &&
                    sendAnnouncementAsyncParams.Message == messages.ElementAt(1)), cancellationTokenSource.Token),
            Times.Exactly(2));

        mockTwitch.Verify(
            twitch => twitch.SendAnnouncementAsync(
                It.Is<Twitch.Params.SendAnnouncementAsyncParams>(sendAnnouncementAsyncParams =>
                    sendAnnouncementAsyncParams.ModeratorId == moderatorId &&
                    sendAnnouncementAsyncParams.Message == messages.ElementAt(2)), cancellationTokenSource.Token),
            Times.Exactly(1));

        mockTwitch.VerifyNoOtherCalls();

        mockDelayHandler.Verify(delayHandler => delayHandler.Delay(1_2000_000, cancellationTokenSource.Token),
            Times.Exactly(8));

        mockDelayHandler.VerifyNoOtherCalls();
    }
}
