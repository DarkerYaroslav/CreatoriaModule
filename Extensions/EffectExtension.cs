using CreatoriaModule.Patches;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreatoriaModule.Patches.EffectPatch;

namespace CreatoriaModule.Extensions
{
    public static class EffectExtension
    {
        public static Dictionary<CSteamID, List<Effect>> PlayerEffects = new Dictionary<CSteamID, List<Effect>>();
        public static void SendUIText(this UnturnedPlayer player, short key, string childNameOrPath, string text) =>
            EffectManager.sendUIEffectText(key, player.TransportConnection(), true, childNameOrPath, text);
        public static void SendUIEffect(this UnturnedPlayer player, ushort id, short key) =>
            EffectManager.sendUIEffect(id, key, player.TransportConnection(), true);
        public static void SendUIEffectVisibility(this UnturnedPlayer player, short key, string childNameOrPath,
            bool visible) => EffectManager.sendUIEffectVisibility(key, player.TransportConnection(), true, childNameOrPath, visible);
        public static void SendUIClear(this UnturnedPlayer player, ushort id) =>
            EffectManager.askEffectClearByID(id, player.TransportConnection());
        /// <summary>
        /// List of existing effects
        /// </summary>
        /// <param name="player"></param>
        /// <param name="effects"></param>
        /// <returns></returns>
        public static bool tryGetListEffects(this UnturnedPlayer player, out List<Effect> effects)
        {
            bool suc = PlayerEffects.TryGetValue(player.CSteamID, out List<Effect> value);
            effects = value;
            return suc;
        }
    }
}
