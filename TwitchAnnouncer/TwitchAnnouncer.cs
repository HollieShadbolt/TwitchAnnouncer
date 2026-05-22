using IDelayHandler = HttpRequestMessageHandler.Interfaces.IDelayHandler;
using ITwitch = Twitch.Interfaces.ITwitch;

namespace TwitchAnnouncer;

/// <summary>
/// A Twitch bot instance for scheduled announcements.
/// </summary>
/// <param name="twitch">The <see cref="ITwitch"/>.</param>
/// <param name="config">The <see cref="Config"/>.</param>
/// <param name="delayHandler">The <see cref="IDelayHandler"/>.</param>
public sealed class TwitchAnnouncer(ITwitch twitch, Config config, IDelayHandler delayHandler)
{
    private int _index;

    /// <summary>
    /// Run.
    /// </summary>
    /// <param name="cancellationToken"> The cancellation token to cancel operation.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="TaskCanceledException">The cancellation token was cancelled.</exception>
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await delayHandler.Delay(config.MillisecondsDelay, cancellationToken);

            await StepAsync(cancellationToken);
        }

        throw new TaskCanceledException();
    }

    private async Task StepAsync(CancellationToken cancellationToken)
    {
        var stream = await twitch.GetStreamAsync(cancellationToken);

        if (stream is null)
        {
            return;
        }

        var index = _index++ % config.Messages.Count();

        var message = config.Messages.ElementAt(index);

        var sendAnnouncementAsyncParams = new Twitch.Params.SendAnnouncementAsyncParams
        {
            ModeratorId = config.ModeratorId,
            Message = message
        };

        await twitch.SendAnnouncementAsync(sendAnnouncementAsyncParams, cancellationToken);
    }
}
