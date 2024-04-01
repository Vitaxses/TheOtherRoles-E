using System;
using System.Linq;
using System.Threading.Tasks;
using AmongUs.GameOptions;
using Cpp2IL.Core.Utils;
using HarmonyLib;
using Hazel;
using TheOtherRoles.Players;
using UnityEngine;
using UnityEngine.ProBuilder;
using static TheOtherRoles.TheOtherRoles;

namespace TheOtherRoles.Patches;

public class RolePatches {

    /*[HarmonyPatch(typeof(Console), nameof(Console.CanUse))]    
    public class UseCanInterract {
    
        public static float PreFix(GameData.PlayerInfo pc, out bool canUse, out bool couldUse) {
            PlayerControl pc = pc.Object;
            PlayerControl me = CachedPlayer.LocalPlayer.PlayerControl;
        
            if (me == pc) {

                float num = float.MaxValue;
            
                couldUse = me.CanMove;
                canUse = couldUse;
                if (me == Tasker.tasker) {
                    canUse = true;
                }
                if (canUse)
                {
                    num = Vector2.Distance(me.GetTruePosition(), me.transform.position);
                    canUse &= num <= 1f;
                }
            }
		return num;
        }
    }*/

    /*

    [HarmonyPatch(typeof(Console), nameof(Console.CanUse))]
    public static class ConsoleCanUsePatch {
        public static bool Prefix(ref float __result, Console __instance, [HarmonyArgument(0)] GameData.PlayerInfo pc, [HarmonyArgument(1)] out bool canUse, [HarmonyArgument(2)] out bool couldUse) {
            canUse = couldUse = false;
            if (__instance.AllowImpostor) return true;
            Debug.Log("Task-Id: " + __instance.ConsoleId);
            if (CachedPlayer.LocalPlayer.PlayerControl.Data.Role.IsImpostor) {
                if(Tasker.tasker != null && Tasker.tasker == CachedPlayer.LocalPlayer.PlayerControl) {
                    canUse = true;
                    couldUse = true;
                    __instance.AllowImpostor = true;
                    return true;
                } else {
                    __instance.AllowImpostor = false;
                    return !CachedPlayer.LocalPlayer.PlayerControl.hasFakeTasks();
                }
            }
            Debug.Log(__instance.AllowImpostor);
            return !CachedPlayer.LocalPlayer.PlayerControl.Data.Role.IsImpostor || !Helpers.isNeutral(CachedPlayer.LocalPlayer.PlayerControl);
        }
    }*/


    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public class update {

        public static void Prefix(PlayerControl __instance) {
            if (__instance == Revealer.player && __instance == CachedPlayer.LocalPlayer.PlayerControl) {
                foreach (PlayerControl targets in Revealer.allTargets) {
                        RoleInfo role = RoleInfo.getRoleInfoForPlayer(targets, false).FirstOrDefault();
                        if (!role.isNeutral && !targets.Data.Role.IsImpostor) {
                            // is crewmate
                            targets.cosmetics.nameText.color = new Color32(0, 255, 69, byte.MaxValue);
                        }
                        // show neutrals!
                        if (Revealer.showNeutral) {
                            if (role.isNeutral && Helpers.isKiller(targets) && !targets.Data.Role.IsSimpleRole && !targets.Data.Role.IsImpostor) {
                                // is neutral killer
                                targets.cosmetics.nameText.color = Jester.color;
                            } else if (role.isNeutral && !Helpers.isKiller(targets) && !targets.Data.Role.IsSimpleRole && !targets.Data.Role.IsImpostor) {
                                targets.cosmetics.nameText.color = new Color32(148, 148, 148, byte.MaxValue);
                            }
                            if (!role.isNeutral && targets.Data.Role.IsImpostor) {
                                // is imp
                                targets.cosmetics.nameText.color = Palette.ImpostorRed;
                            }
                        }
                    }
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
    public class completetaskpatch {

        public static void Postfix(PlayerControl __instance) {
            if (__instance != Betrayer.betrayer) return;
            

            var taskInfo = __instance.Data.Tasks.ToArray();
            var tasks = taskInfo.Count(x => !x.Complete);

            if (tasks == 0 && !Betrayer.hasBetrayedYet) {
                if (!Betrayer.betrayer.Data.Role.IsImpostor) {
                    Betrayer.betrayer.clearAllTasks();
                    
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.setImpostor, SendOption.Reliable, -1);
                    writer.Write(Betrayer.betrayer.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    RPCProcedure.setImpostor(Betrayer.betrayer.PlayerId);
                } else {
                    return;
                }
            }
        }

    }

}
