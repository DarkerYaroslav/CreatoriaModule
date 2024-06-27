using HarmonyLib;
using Steamworks;

namespace CreatoriaModule.Patches
{
    public static class GoldPatch
    {
        public delegate void CheckLicenseHandler(CSteamID steamID, ref EUserHasLicenseForAppResult __result);

        public static event CheckLicenseHandler CheckLicense;

        /// <summary>
        /// Every player has a gold account.
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="steamID"></param>
        /// <param name="appID"></param>
        /// <returns></returns>
        [HarmonyPatch(typeof(SteamGameServer), nameof(SteamGameServer.UserHasLicenseForApp))]
        [HarmonyPrefix]
        public static bool UserHasLicenseHandle(ref EUserHasLicenseForAppResult __result, CSteamID steamID, AppId_t appID)
        {
            __result = EUserHasLicenseForAppResult.k_EUserHasLicenseResultHasLicense;
            CheckLicense?.Invoke(steamID, ref __result);
            return false;
        }
    }
}