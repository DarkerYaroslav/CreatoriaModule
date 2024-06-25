using HarmonyLib;
using Rocket.API;
using Rocket.Core;
using SDG.Unturned;
using Steamworks;
using System.Linq;

namespace CreatoriaModule.Patches
{
    public static class NicknamePatch
    {
        public delegate void OnNicknameHandler(CSteamID id,ref string Nickname,ref bool WithSuffix,ref bool WithPrefix);
        public static event OnNicknameHandler OnNickname;
        [HarmonyPatch(typeof(Provider), nameof(Provider.onCheckValidWithExplanation))]
        [HarmonyPostfix]
        private static void onCheckValidWithExplanation(ValidateAuthTicketResponse_t r, ref bool isValid, ref string explanation)
        {
            var playerGroups = R.Permissions.GetGroups(new RocketPlayer(r.m_SteamID.ToString()), true);
            string prefix = playerGroups.FirstOrDefault(x => !string.IsNullOrEmpty(x.Prefix))?.Prefix ?? "";
            string suffix = playerGroups.FirstOrDefault(x => !string.IsNullOrEmpty(x.Suffix))?.Suffix ?? "";
            SteamPending steamPending = Provider.pending.FirstOrDefault(x => x.playerID.steamID == r.m_SteamID);
            string name = steamPending.playerID.characterName;
            bool needsuffix = true;
            bool needprefix = true;
            OnNickname?.Invoke(
                steamPending.playerID.steamID, 
                ref name, 
                ref needsuffix, 
                ref needprefix);
            if (prefix != "" || suffix != "")
            {
                if (steamPending != null)
                {
                    if (prefix != "" && needprefix)
                    {
                        steamPending.playerID.characterName = $"{prefix}{name}";
                    }
                    if (suffix != "" && needsuffix)
                    {
                        steamPending.playerID.characterName = $"{name}{suffix}";
                    }
                }
            }
            else
            {
                steamPending.playerID.characterName = name;
            }
            isValid = true;
            explanation = "Change name";
        }
    }
}
