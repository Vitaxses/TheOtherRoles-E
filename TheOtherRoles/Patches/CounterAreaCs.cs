using System.Collections.Generic;
using System.Linq;
using static TheOtherRoles.TheOtherRoles;
using HarmonyLib;
using UnityEngine;
using TheOtherRoles.Players;

namespace TheOtherRoles.Patches.Added;

    [HarmonyPatch(typeof(RoomTracker), nameof(RoomTracker.FixedUpdate))]
    class CounterAreaUpdateCountPatch {
        
        static void Postfix(RoomTracker __instance) {
            if (Haunter.haunter != null && CachedPlayer.LocalPlayer.PlayerControl == Tasker.tasker)
            __instance.LastRoom = null;
            __instance.playerCollider = null;
        }
        
    }