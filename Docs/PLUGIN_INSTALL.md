# Install native Discord Game SDK binaries

Download the official Discord Game SDK from the Discord Developer website.
Place the native files into `Assets/Plugins/<platform>/`:

- Windows (x86_64): Assets/Plugins/x86_64/discord_game_sdk.dll
- macOS (x86_64/arm64): Assets/Plugins/osx/libdiscord_game_sdk.dylib
- Linux (x86_64): Assets/Plugins/x86_64/libdiscord_game_sdk.so

Make sure the files are excluded from git (`.gitignore` already covers them).
