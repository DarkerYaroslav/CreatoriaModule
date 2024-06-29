using CreatoriaModule.Config;
using CreatoriaModule.Patches;
using HarmonyLib;
using SDG.Framework.Modules;
using SDG.Unturned;
using Steamworks;
using Logger = Rocket.Core.Logging.Logger;

namespace CreatoriaModule
{
    public class CModule : IModuleNexus
    {
        private static Harmony _harmony;
        public void initialize()
        {
            _harmony = new Harmony("creatoria.module");
            PatchAll();

            Logger.Log(" ############################################# ");
            Logger.Log(" ########## CreatoriaModule loaded! ########## ");
            Logger.Log(" ############################################# ");
        }
        
        public void shutdown()
        {
            _harmony.UnpatchAll("creatoria.module");
        }

        private static void PatchAll()
        {
            var cfg = JsonReader.GetCfg();
            if (cfg.GrenadePatch.Value)
            {
                var original = typeof(Grenade).GetMethod("Explode");
                var postfix = typeof(GrenadePatch).GetMethod("Explode");
                _harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            }
            if (cfg.NicknamePatch.Value)
            {
                var original = typeof(Provider).GetMethod("onCheckValidWithExplanation");
                var postfix = typeof(NicknamePatch).GetMethod("onCheckValidWithExplanation");
                _harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            }
            if (cfg.VoicePatch.Value)
            {
                var original = typeof(PlayerVoice).GetMethod("handleRelayVoiceCulling_Proximity");
                var prefix = typeof(VoicePatch).GetMethod("handler");
                _harmony.Patch(original, new HarmonyMethod(prefix));
            }
            if (cfg.MarkerPatch.Value)
            {
                var original = typeof(PlayerQuests).GetMethod("replicateSetMarker");
                var prefix = typeof(MarkerPatch).GetMethod("replicateSetMarker");
                _harmony.Patch(original, new HarmonyMethod(prefix));
            }
            if (cfg.GoldPatch.Value)
            {
                var original = typeof(SteamGameServer).GetMethod("UserHasLicenseForApp");
                var prefix = typeof(GoldPatch).GetMethod("UserHasLicenseHandle");
                _harmony.Patch(original, new HarmonyMethod(prefix));
            }
        }
    }
}
