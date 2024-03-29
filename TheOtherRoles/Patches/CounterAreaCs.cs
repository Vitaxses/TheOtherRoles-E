using System.Collections.Generic;
using System.Linq;
using static TheOtherRoles.TheOtherRoles;
using HarmonyLib;
using UnityEngine;
using TheOtherRoles.Players;

namespace TheOtherRoles.Patches.Added;

    [HarmonyPatch(typeof(RoomTracker), nameof(RoomTracker.FixedUpdate))]
    class CounterAreaUpdateCountPatch {

        static void Prefix(RoomTracker __instance) {
            if ((Haunter.haunter != null && CachedPlayer.LocalPlayer.PlayerControl == Haunter.haunter) || (Swooper.swooper != null && CachedPlayer.LocalPlayer.PlayerControl == Swooper.swooper && Swooper.isInvisible)) {
                __instance.LastRoom = null;
                __instance.playerCollider = null;
            }
        }
    }