using CreatoriaModule.Config;
using HarmonyLib;
using Rocket.Core.Logging;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Framework.Modules;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CreatoriaModule.Patches
{
    public class Module : IModuleNexus
    {
        public static Harmony harmony;
        public void initialize()
        {
            try
            {
                var field = typeof(Customization).GetField("FREE_CHARACTERS", BindingFlags.Static | BindingFlags.Public);
                field.SetValue(null, (byte)5);
            }
            catch(Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            harmony = new Harmony("creatoria.module");
            PatchAll();
        }
        public void shutdown() => harmony.UnpatchAll("creatoria.module");
        private void PatchAll()
        {
            var cfg = jsonreader.GetCfg();
            if (cfg.GrenadePatch.Value)
            {
                var original = typeof(Grenade).GetMethod("Explode");
                var postfix = typeof(GrenadePatch).GetMethod("Explode");
                harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            }
            if (cfg.NicknamePatch.Value)
            {
                var original = typeof(Provider).GetMethod("onCheckValidWithExplanation");
                var postfix = typeof(NicknamePatch).GetMethod("onCheckValidWithExplanation");
                harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            }
            if (cfg.VoicePatch.Value)
            {
                var original = typeof(PlayerVoice).GetMethod("handleRelayVoiceCulling_Proximity");
                var prefix = typeof(VoicePatch).GetMethod("handler");
                harmony.Patch(original, new HarmonyMethod(prefix));
            }
            if (cfg.MarkerPatch.Value)
            {
                var original = typeof(PlayerQuests).GetMethod("replicateSetMarker");
                var prefix = typeof(MarkerPatch).GetMethod("replicateSetMarker");
                harmony.Patch(original, new HarmonyMethod(prefix));
            }
            if (cfg.GoldPatch.Value)
            {
                var original = typeof(SteamGameServer).GetMethod("UserHasLicenseForApp");
                var prefix = typeof(GoldPatch).GetMethod("UserHasLicenseHandle");
                harmony.Patch(original, new HarmonyMethod(prefix));
            }
        }
    }
}
