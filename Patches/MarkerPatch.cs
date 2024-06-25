using HarmonyLib;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CreatoriaModule.Patches
{
    public static class MarkerPatch
    {
        public delegate void OnMarkerHandler(bool newIsMarkerPlaced, Vector3 newMarkerPosition, string newMarkerTextOverride);
        public static event OnMarkerHandler OnMarker;

        [HarmonyPatch(typeof(PlayerQuests), nameof(PlayerQuests.replicateSetMarker))]
        [HarmonyPostfix]
        public static void replicateSetMarker(bool newIsMarkerPlaced, Vector3 newMarkerPosition, string newMarkerTextOverride = "")
        {
            OnMarker?.Invoke(
                newIsMarkerPlaced,
                newMarkerPosition,
                newMarkerTextOverride);
        }
    }
}