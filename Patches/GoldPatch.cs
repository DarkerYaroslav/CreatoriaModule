using HarmonyLib;
using Steamworks;

namespace CreatoriaModule.Patches
{
    public static class GoldPatch
    {
        public delegate void CheckLicenseHandler(CSteamID steamID, ref EUserHasLicenseForAppResult __result);

        public static event CheckLicenseHandler CheckLicense;

        [HarmonyPatch(typeof(SteamGameServer), methodName: "UserHasLicenseForApp")]
        [HarmonyPrefix]
        public static bool UserHasLicenseHandle(CSteamID steamID, AppId_t appID, ref EUserHasLicenseForAppResult __result)
        {
            __result = EUserHasLicenseForAppResult.k_EUserHasLicenseResultHasLicense;
            CheckLicense?.Invoke(steamID, ref __result);
            return false;
        }
    }
}