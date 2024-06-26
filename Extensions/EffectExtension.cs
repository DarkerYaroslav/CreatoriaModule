﻿using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatoriaModule.Extensions
{
    public static class EffectExtension
    {
        public static void SendUIText(this UnturnedPlayer player, short key, string childNameOrPath, string text) =>
            EffectManager.sendUIEffectText(key, player.TransportConnection(), true, childNameOrPath, text);
        public static void SendUIEffect(this UnturnedPlayer player, ushort id, short key) =>
            EffectManager.sendUIEffect(id, key, player.TransportConnection(), true);
        public static void SendUIEffectVisibility(this UnturnedPlayer player, short key, string childNameOrPath,
            bool visible) => EffectManager.sendUIEffectVisibility(key, player.TransportConnection(), true, childNameOrPath, visible);
        public static void SendUIClear(this UnturnedPlayer player, ushort id) =>
            EffectManager.askEffectClearByID(id, player.TransportConnection());
    }
}
