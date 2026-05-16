[![Unit Test](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/dotnet.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/dotnet.yml)
[![Linux x64 Release](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-x64-release.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-x64-release.yml)
[![Linux Arm 64 Release](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-arm64-release.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-arm64-release.yml)
[![Windows Release](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/windows-release.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/windows-release.yml)

# TwitchAnnouncer

A .NET console application for sending announcements to Twitch.

The application will loop indefinitely until cancelled. After waiting for a defined number of milliseconds, if the defined broadcaster is online, the next message in the defined messages list is sent as an announcement (looping back to the start of the list if required).

# Usage
```
TwitchAnnouncer path/to/config.json
```

This JSON file should contain the following properties:
- `parameter` (string) - The Bearer token with `moderator:manage:announcements` scope.*
- `client_id` (string) - The client ID.*
- `broadcaster_id` (string) - The broadcaster ID.
- `moderator_id` (string) - The moderator ID.
- `milliseconds_delay` (integer) - The number of milliseconds to wait between announcement attempts.
- `messages` (string[]) - The list of messages.

\* See [Getting OAuth Access Tokens](https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/).

# Dependencies
- [Twitch](https://github.com/HollieShadbolt/Twitch)
