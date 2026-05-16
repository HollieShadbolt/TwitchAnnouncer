[![Unit Test](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/dotnet.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/dotnet.yml)
[![Linux Release](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-release.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-release.yml)
[![Windows Release](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/windows-release.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/windows-release.yml)

# TwitchAnnouncer

A .NET console application for sending announcements to Twitch.

The application will loop indefinitely until cancelled. After waiting for a defined number of milliseconds, a check is performed on the target broadcaster. If they are online, the next message in the defined messages list is sent as an announcement (looping back to the start of the list if required). The application will then loop back to waiting.

# Usage
```
TwitchAnnouncer path/to/config.json
```

This JSON file should contain the following:
- `parameter` (string) - The Bearer token.*
- `client_id` (string) - The client ID.*
- `broadcaster_id` (string) - The broadcaster ID.
- `moderator_id` (string) - The moderator ID.
- `milliseconds_delay` (integer) - The number of milliseconds to wait between announcement attempts.
- `messages` (string[]) - The list of messages.

\* See [Getting OAuth Access Tokens](https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/).

# Dependencies
- [Twitch](https://github.com/HollieShadbolt/Twitch)
