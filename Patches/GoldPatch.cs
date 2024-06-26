using HarmonyLib;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace CreatoriaModule.Patches
{
    public static class GoldPatch
    {
        public delegate void CheckLicenseHandler(CSteamID steamID, ref bool __result);
        public static event CheckLicenseHandler CheckLicense;
        [HarmonyPatch(typeof(SteamGameServer), methodName: "UserHasLicenseForApp")]
        [HarmonyPrefix]
        public static bool UserHasLicenseHandle(CSteamID steamID, AppId_t appID, ref bool __result)
        {
            __result = true;
            CheckLicense?.Invoke(steamID, ref __result);
            return false;
        }
    }
}
