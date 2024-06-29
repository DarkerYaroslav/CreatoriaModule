using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;

namespace CreatoriaModule.Extensions
{
    public static class EffectExtension
    {
        public static Dictionary<CSteamID, TextLibrary> EffectDictionary = new();

        public static void SendUIEffectText(this UnturnedPlayer player, short key, string childNameOrPath, string text) =>
            EffectManager.sendUIEffectText(key, player.TransportConnection(), true, childNameOrPath, text);
        public static void SendUIEffect(this UnturnedPlayer player, ushort id, short key) =>
            EffectManager.sendUIEffect(id, key, player.TransportConnection(), true);
        public static void SendUIEffectVisibility(this UnturnedPlayer player, short key, string childNameOrPath,
            bool visible) => EffectManager.sendUIEffectVisibility(key, player.TransportConnection(), true, childNameOrPath, visible);
        public static void SendUIClear(this UnturnedPlayer player, ushort id) =>
            EffectManager.askEffectClearByID(id, player.TransportConnection());

        public static string GetTextFromInputUI(this Player player, string inputField)
        {
            if (!EffectDictionary.TryGetValue(player.channel.owner.playerID.steamID, out var value))
            {
                return null;
            }
            
            return value.InputDictionary[inputField];
        }

        public static string GetTextFromUI(this Player player, string textField)
        {
            if (!EffectDictionary.TryGetValue(player.channel.owner.playerID.steamID, out var value))
            {
                return null;
            }

            return value.TextDictionary[textField];
        }
    }
    public record TextLibrary
    {
        public Dictionary<string, string> TextDictionary = new();
        public Dictionary<string, string> InputDictionary = new();
    }
}
