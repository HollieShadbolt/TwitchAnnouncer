[![Unit Test](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/dotnet.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/dotnet.yml)

[![Linux Release](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-release.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/linux-release.yml)

[![Windows Release](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/windows-release.yml/badge.svg)](https://github.com/HollieShadbolt/TwitchAnnouncer/actions/workflows/windows-release.yml)

A .NET console application for making Twitch announcements.

# Usage
```
TwitchAnnouncer path/to/config.json
```

This file should contain the following JSON properties:
- `parameter` (string) - The credentials containing the authentication information of the user agent.
- `client_id` (string) - The client ID.
- `broadcaster_id` (string) - The broadcaster ID.
- `moderator_id` (string) - The ID of a user who has permission to moderate the broadcaster’s chat room.
- `milliseconds_delay` (integer) - The number of milliseconds to wait before completing the returned task.
- `messages` (string[]) - The messages.

# Dependencies
- [Twitch](https://github.com/HollieShadbolt/Twitch)
