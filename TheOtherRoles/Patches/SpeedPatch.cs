using HarmonyLib;
using AmongUs;
using TheOtherRoles.Players;
using static TheOtherRoles.TheOtherRoles;
using System.Linq;
using UnityEngine;

namespace TheOtherRoles.Patches.Added
{
    [HarmonyPatch]
    public static class GiantFlashModifier
    {
        [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
        [HarmonyPostfix]
        public static void PostfixPhysics(PlayerPhysics __instance)
        {
            if (__instance.AmOwner && GameData.Instance && __instance.myPlayer.CanMove)
            foreach (PlayerControl giants in Giant.giant) {
                if (Camouflager.camouflageTimer > 0f || Helpers.MushroomSabotageActive()) return;
                
                giants.transform.localScale = Giant.Scale;
            }

            if (__instance.AmOwner && GameData.Instance && __instance.myPlayer.Data != null && __instance.myPlayer.CanMove && !__instance.myPlayer.Data.IsDead)
            {
                    if (__instance.myPlayer == CachedPlayer.LocalPlayer.PlayerControl)
                        if (Flash.flash.Any(x => x.PlayerId == __instance.myPlayer.PlayerId) == CachedPlayer.LocalPlayer.PlayerControl) {
                            __instance.body.velocity *= Flash.speed;
                            __instance.Speed = Flash.speed;
                            //__instance.body.velocity *= __instance.TrueSpeed;
                        } else if (Giant.giant.Any(x => x.PlayerId == __instance.myPlayer.PlayerId) == CachedPlayer.LocalPlayer.PlayerControl) {
                            __instance.body.velocity *= Giant.speed;
                            __instance.myPlayer.transform.localScale = Giant.Scale;
                            //__instance.body.velocity *= __instance.TrueSpeed;
                        }
            } else if (__instance.AmOwner && GameData.Instance && __instance.myPlayer.Data != null && __instance.myPlayer.CanMove && __instance.myPlayer.Data.IsDead) {
                __instance.myPlayer.transform.localScale = Giant.Scale;
                //__instance.body.velocity *= __instance.TrueSpeed;
            }
        }
    }
}