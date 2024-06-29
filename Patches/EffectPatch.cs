using CreatoriaModule.Extensions;
using HarmonyLib;
using MonoMod.Utils;
using SDG.NetTransport;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using static CreatoriaModule.Patches.EffectPatch;

namespace CreatoriaModule.Patches
{
    /// <summary>
    /// In developing, not recomend to use
    /// </summary>
    public static class EffectPatch
    {
        public class Effect
        {
            public ushort Id;
            public short Key;
            public bool IsUI;
            public Dictionary<string,string> texts;
            public Dictionary<string, string> inputs;
            public Effect() { texts = new Dictionary<string, string>(); inputs = new Dictionary<string, string>(); }
        }
        public delegate void sendUIEffectHandler(Player player, ushort id, short key);
        public static event sendUIEffectHandler onSendUIEffect;
        [HarmonyPatch(typeof(EffectManager), methodName: "sendUIEffect")]
        [HarmonyPostfix]
        public static void sendUIEffectHandle(ushort id, short key, ITransportConnection transportConnection, bool reliable)
        {
            var player = Provider.clients.Find(p => p.transportConnection == transportConnection);
            bool isExists = EffectExtension.PlayerEffects.TryGetValue(player.playerID.steamID, out List<Effect> effects);
            if (!isExists)
            {
                EffectExtension.PlayerEffects.Add(player.playerID.steamID, new List<Effect>() { new Effect() { Id = id, Key = key, IsUI = true } });
            }
            else
            {
                var neweffects = effects;
                effects.Add(new Effect() { Id = id, Key = key, IsUI = true });
                EffectExtension.PlayerEffects.Remove(player.playerID.steamID);
                EffectExtension.PlayerEffects.Add(player.playerID.steamID, neweffects);
            }
            onSendUIEffect?.Invoke(player.player, id, key);
        }
    }
}