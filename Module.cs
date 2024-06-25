using HarmonyLib;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Framework.Modules;
using SDG.Unturned;
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
            harmony = new Harmony("creatoria.module");
            PatchAll();
        }
        public void shutdown() => harmony.UnpatchAll("creatoria.module");
        private void PatchAll()
        {
            if (true)
            {
                var original = typeof(Grenade).GetMethod("Explode");
                var postfix = typeof(GrenadePatch).GetMethod("Explode");
                harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            }
            if (true)
            {
                var original = typeof(Provider).GetMethod("onCheckValidWithExplanation");
                var postfix = typeof(NicknamePatch).GetMethod("onCheckValidWithExplanation");
                harmony.Patch(original, postfix: new HarmonyMethod(postfix));
            }
            if (true)
            {
                var original = typeof(PlayerVoice).GetMethod("handleRelayVoiceCulling_Proximity");
                var prefix = typeof(VoicePatch).GetMethod("handler");
                harmony.Patch(original, new HarmonyMethod(prefix));
            }
            if (true)
            {
                var original = typeof(PlayerQuests).GetMethod("replicateSetMarker");
                var prefix = typeof(MarkerPatch).GetMethod("replicateSetMarker");
                harmony.Patch(original, new HarmonyMethod(prefix));
            }
        }
    }
}
