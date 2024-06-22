using HarmonyLib;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class GrenadeManager
{
    public delegate void OnGrenadeExplodeHandler(CSteamID killer, Vector3 pos, ushort explosion, float range);
    public static event OnGrenadeExplodeHandler OnGrenadeExplode;

    [HarmonyPatch(typeof(Grenade), nameof(Grenade.Explode))]
    [HarmonyPostfix]
    public static void Explode(Grenade __instance)
    {
        OnGrenadeExplode?.Invoke(
            __instance.killer,
            __instance.transform.position,
            __instance.explosion,
            __instance.range);
    }
}
