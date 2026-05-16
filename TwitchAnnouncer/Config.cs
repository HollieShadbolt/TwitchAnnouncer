using JsonPropertyNameAttribute = System.Text.Json.Serialization.JsonPropertyNameAttribute;

namespace TwitchAnnouncer;

public class Config : Twitch.Config
{
    /// <summary>
    /// Get the number of milliseconds to wait before completing the returned task.
    /// </summary>
    /// <returns>The number of milliseconds to wait before completing the returned task.</returns>
    [JsonPropertyName("milliseconds_delay")]
    public required int MillisecondsDelay { get; init; }

    /// <summary>
    /// Get the messages to send.
    /// </summary>
    /// <returns>The messages.</returns>
    [JsonPropertyName("messages")]
    public required IEnumerable<string> Messages { get; init; }

    /// <summary>
    /// Get the ID of a user who has permission to moderate the broadcaster’s chat room.
    /// </summary>
    /// <returns>The ID of a user who has permission to moderate the broadcaster’s chat room.</returns>
    [JsonPropertyName("moderator_id")]
    public required string ModeratorId { get; init; }
}