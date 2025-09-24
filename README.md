# Discord Game SDK Rich Presence — Unity Advanced Example

This repository is an **expanded** Unity sample demonstrating a practical, production-minded approach
to integrating Discord's Game SDK for Rich Presence and Social features in Unity.

## What’s included (high-level)
- Robust `DiscordManagerAdvanced` that handles initialization, activity updates (rich presence),
  join/spectate handling, reconnection/backoff, and graceful shutdown.
- `LobbyController` — examples for creating, joining, updating lobbies using the Social SDK (lobby lifecycle).
- `SocialController` — relationships (friends) and invites helpers.
- `VoiceController` — bootstrapped voice state example (connect/disconnect hooks).
- `DiscordEditorSettings` — Unity Editor window to store App ID in EditorPrefs and generate a small `Resources` JSON for runtime.
- `ExampleUI` — runtime UI to mutate presence and simulate invites/joins.
- `Docs/` with step-by-step setup, security guidance, and packaging notes.
- CI workflow placeholder for Unity projects and PR/Issue templates.
- `.gitignore`, `SECURITY.md`, `CONTRIBUTING.md`, `CODE_OF_CONDUCT.md`, `LICENSE`.

## New features vs base
- Automatic callback pumping and safe threading model.
- Activity queue to avoid rate-limits and TransactionAborted issues.
- Lobby creation + automatic activity updates with lobby join secret.
- Handlers for `OnActivityJoin`, `OnActivitySpectate`, and `OnActivityInvite` to show how to route join requests to in-game logic.
- Simple Editor integration to reduce friction when updating the App ID across team members.
- Recommended packaging & release checklist (Docs).

## Quick start
1. Clone this repo and open in Unity (2020.3 LTS+ recommended).
2. Create a Discord application at the Developer Portal and enable Rich Presence/Activities.
3. Download the native Discord Game SDK for the platforms you target. **Do not commit binaries.**
4. Place native libs in `Assets/Plugins/<platform>/` as explained in `Docs/PLUGIN_INSTALL.md`.
5. Open `Window -> Discord SDK Settings` in Unity and paste your App ID, then press Save.
6. Open the `Sample` scene (create it per `Docs/SCENE_SETUP.md`) and attach `DiscordManagerAdvanced` to a GameObject.
7. Play and test rich presence, invites, and lobby actions.

## References
- Discord Game SDK docs (Activities / ActivityManager): see Discord Developer docs.
- Social SDK (lobbies & relationships): see Discord Social SDK docs.

## Notes
- This repo is a sample; adapt memory/serialization and networking to your game's systems.
- Do not ship the native SDK in public repos or include tokens/secrets.

