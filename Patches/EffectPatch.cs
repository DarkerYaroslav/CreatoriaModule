using CreatoriaModule.Extensions;
using HarmonyLib;
using SDG.NetTransport;
using SDG.Unturned;

namespace CreatoriaModule.Patches
{
    public static class EffectPatch
    {
        [HarmonyPatch(typeof(EffectManager), nameof(EffectManager.sendUIEffect))]
        [HarmonyPostfix]
        public static void sendUIEffectHandle(ushort id, short key, ITransportConnection transportConnection, bool reliable)
        {
            var steamPlayer = transportConnection.SteamPlayer();

            if (!EffectExtension.EffectDictionary.TryGetValue(steamPlayer.playerID.steamID, out var valueLibrary))
            {
                EffectExtension.EffectDictionary.Add(steamPlayer.playerID.steamID, new TextLibrary
                {
                    InputDictionary = new(),
                    TextDictionary = new()
                });
            }
        }

        [HarmonyPatch(typeof(EffectManager), nameof(EffectManager.ReceiveEffectTextCommitted))]
        [HarmonyPostfix]
        public static void ReceiveEffectTextCommitted(in ServerInvocationContext context, string inputFieldName, string text)
        {
            if (!EffectExtension.EffectDictionary.TryGetValue(context.GetCallingPlayer().playerID.steamID, out var valueLibrary))
            {
                return;
            }

            valueLibrary.InputDictionary[inputFieldName] = text;
        }

        [HarmonyPatch(typeof(EffectManager), nameof(EffectManager.sendUIEffectText))]
        [HarmonyPostfix]
        public static void sendUIEffectText(short key, ITransportConnection transportConnection, bool reliable,
            string childNameOrPath, string text)
        {
            var steamPlayer = transportConnection.SteamPlayer();

            if (!EffectExtension.EffectDictionary.TryGetValue(steamPlayer.playerID.steamID, out var valueLibrary))
            {
                return;
            }

            valueLibrary.TextDictionary[childNameOrPath] = text;
        }
    }
}