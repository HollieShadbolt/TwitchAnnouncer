namespace TwitchAnnouncer;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var path = args.Single();

        var text = await File.ReadAllTextAsync(path);

        var config = System.Text.Json.JsonSerializer.Deserialize<Config>(text) ?? throw new InvalidOperationException();

        var httpRequestMessageHandler = new HttpRequestMessageHandler.HttpRequestMessageHandler();

        var delayHandler = new HttpRequestMessageHandler.DelayHandler();

        var httpRequestMessageFactoryHandler =
            new HttpRequestMessageHandler.HttpRequestMessageFactoryHandler(httpRequestMessageHandler, delayHandler);

        var twitch = new Twitch.Twitch(httpRequestMessageFactoryHandler, config);

        var twitchAnnouncer = new TwitchAnnouncer(twitch, config, delayHandler);

        await twitchAnnouncer.RunAsync(CancellationToken.None);
    }
}