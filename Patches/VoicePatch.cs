using HarmonyLib;
using SDG.Unturned;

namespace CreatoriaModule.Patches
{
    public static class VoicePatch
    {
        public delegate bool Handle(PlayerVoice speaker, PlayerVoice listener);

        public static Handle onHandle;
        [HarmonyPatch(typeof(PlayerVoice), "handleRelayVoiceCulling_Proximity")]
        [HarmonyPrefix]
        private static bool handler(PlayerVoice speaker, PlayerVoice listener)
        {
            return onHandle(speaker, listener);
        }
    }
}
