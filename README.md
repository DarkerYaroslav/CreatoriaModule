
# CreatoriaModule

The **CreatoriaModule** is a module, expanding functionality for your plugins.

We recommend using this module with [RocketModFix][rocketmodfix].


# Why a module and not a library?

**The module is better suited for harmony patches and events than the library**

## Compatibility

If you notice any errors, create [issue][issues].

## Our plan and what we're done

- [x] Compatibility with [RocketModFix][rocketmodfix]
- [ ] NuGet package for developers
- [ ] More patches and custom events!

## Installation

Install to U3DS/Modules/ .dll file from [releases][releases] and start server.

Enabled or disabled patches in config in the same place

## Discord

Feel free to join our [Discord Server][discordserver_url].

## Features
- [GoldPatch.cs][GoldPatch] - every player has a gold account (expand premium function, e.g. multi/more character on server)
- [GrenadePatch.cs][GrenadePatch] - event when granade explode
- [MarkerPatch.cs][MarkerPatch] - event when player set marker
- [NicknamePatch.cs][NicknamePatch] - незнаю нахуя он нужен
- [VoicePatch.cs][VoicePatch] - !need to finished!

- [Extensions][Extensions] - extension😂 methods

[discordserver_url]: https://discord.gg/RejmMseuXd 
[rocketmodfix]: https://github.com/RocketModFix/RocketModFix
[issues]: https://github.com/DarkerYaroslav/CreatoriaModule/issues
[releases]: https://github.com/DarkerYaroslav/CreatoriaModule/releases
[GoldPatch]: https://github.com/DarkerYaroslav/CreatoriaModule/blob/main/Patches/GoldPatch.cs
[GrenadePatch]: https://github.com/DarkerYaroslav/CreatoriaModule/blob/main/Patches/GrenadePatch.cs
[MarkerPatch]: https://github.com/DarkerYaroslav/CreatoriaModule/blob/main/Patches/MarkerPatch.cs
[NicknamePatch]: https://github.com/DarkerYaroslav/CreatoriaModule/blob/main/Patches/NicknamePatch.cs
[VoicePatch]: https://github.com/DarkerYaroslav/CreatoriaModule/blob/main/Patches/VoicePatch.cs
[Extensions]: https://github.com/DarkerYaroslav/CreatoriaModule/tree/main/Extensions
