using System;
using System.Linq;
using Cpp2IL.Core.Utils;
using HarmonyLib;
using Hazel;
using TheOtherRoles.Players;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;

namespace TheOtherRoles.Patches;

public class RolePatches {

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class PlayerControlUpdate {

        public static void Prefix(HudManager __instance) {
            if (__instance != null && __instance.KillButton != null) {
                if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) {
                    KillButton killButton = __instance.KillButton;

                   /* if (Swooper.swooper == CachedPlayer.LocalPlayer.PlayerControl) {
                            if (killButton == null) return;
                        if (!Swooper.isInvisible) {
                            if (killButton.gameObject != null) {killButton.gameObject.SetActive(false);}
                            else {
                                if (killButton.currentTarget != null) {
                                killButton.currentTarget = null;
                                } else {
                                    killButton.SetDisabled();
                                }
                            }
                        } else {
                            if (killButton.gameObject != null) {killButton.gameObject.SetActive(true);} else {killButton.SetEnabled();}
                        }

                    }*/
                }
            }
        }
    }
    [HarmonyPatch(typeof(AchievementManager), nameof(AchievementManager.OnTaskComplete))]
    public class onTaskCompleted {

        public static void Prefix(AchievementManager __instance) {
            PlayerControl me = CachedPlayer.LocalPlayer.PlayerControl;
            if (me == Tasker.tasker) {
                if (HudManager.Instance) {
                    System.Random random = new System.Random((int) DateTime.Now.Ticks);
                    int i = random.Next((int) Tasker.MinSecondsToRemove, (int) Tasker.MaxSecondsToRemove);
                    HudManager.Instance.KillButton.SetCoolDown(Tasker.liveCooldown - i, Tasker.KillCooldown);
                } 
            }
        }
    }

    [HarmonyPatch(typeof(ReportButton), nameof(ReportButton.DoClick))]
    public class onReport {
        public static void Prefix(ReportButton __instance) {
            DeadBody deadBody = null;
                foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(CachedPlayer.LocalPlayer.PlayerControl.GetTruePosition(), CachedPlayer.LocalPlayer.PlayerControl.MaxReportDistance, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody") {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported) {
                                Vector2 truePosition = CachedPlayer.LocalPlayer.PlayerControl.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= CachedPlayer.LocalPlayer.PlayerControl.MaxReportDistance && CachedPlayer.LocalPlayer.PlayerControl.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                    deadBody = component;
                                    break;
                                }
                            }
                        }
                    }
            if (deadBody != null && EvilTrapper.trappedBodys.Contains(deadBody)) {
                PlayerControl me = CachedPlayer.LocalPlayer.PlayerControl;
                if (EvilTrapper.player != null && EvilTrapper.player != me) {
                    Helpers.MurderPlayer(EvilTrapper.player, me, false);
                    __instance.SetCoolDown(0.69f, 1);

                    if (EvilTrapper.trappedBodys.Count < EvilTrapper.maxCountOfTrappedBodys) {


                        DeadBody myDeadBody = null;
                        foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(CachedPlayer.LocalPlayer.PlayerControl.GetTruePosition(), CachedPlayer.LocalPlayer.PlayerControl.MaxReportDistance, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody") {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported) {
                                Vector2 truePosition = CachedPlayer.LocalPlayer.PlayerControl.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= CachedPlayer.LocalPlayer.PlayerControl.MaxReportDistance && CachedPlayer.LocalPlayer.PlayerControl.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                    myDeadBody = component;
                                    break;
                                }
                            }
                        }
                    }
                        EvilTrapper.trappedBodys.Add(myDeadBody);
                    }

                }
            }
        }
    }

    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill {

        public static void Prefix(KillButton __instance) {
        PlayerControl me = CachedPlayer.LocalPlayer.PlayerControl;
        if (__instance != null && me != null) {
                if (me == Swooper.swooper) {
                    if (!Swooper.isInvisible) {
                        __instance.currentTarget = null;
                        __instance.SetDisabled();
                    } else {
                        __instance.SetEnabled();
                    }
                }

            if (__instance.currentTarget != null) {
                PlayerControl current = __instance.currentTarget;
                if (Sacraficer.sacraficer != null && Sacraficer.target != null && current == Sacraficer.target) {
                    PlayerControl target = Sacraficer.target;
                    PlayerControl sacraficer = Sacraficer.sacraficer;
                    __instance.SetDisabled();
                    target.ShowFailedMurder();
                    Helpers.showFlash(Color.red, 2f, "Sacraficer Saved You");
                    if (sacraficer != CachedPlayer.LocalPlayer.PlayerControl) {
                    sacraficer.MurderPlayer(me);
                    }
                    sacraficer.MurderPlayer(sacraficer, MurderResultFlags.Succeeded);
                    
                }
            }
        }
    }

        public static void PostFix(KillButton __instance) {
            PlayerControl me = CachedPlayer.LocalPlayer;
            if (__instance != null && me != null) {
                if (me == Haunter.haunter) {
                    if (Haunter.isHaunting) {
                        Haunter.haunter.killTimer = Haunter.killCD;
                    }
                }
            }
        }

    }

    /*[HarmonyPatch(typeof(Console), nameof(Console.CanUse))]    
    public class UseCanInterract {
    
        public static float PreFix(GameData.PlayerInfo pc, out bool canUse, out bool couldUse) {
            PlayerControl me = pc.Object;
        
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
        public void PostFix(PlayerControl __instance) {
            if (__instance == Betrayer.betrayer && __instance == CachedPlayer.LocalPlayer.PlayerControl && !Betrayer.hasBetrayedYet) {
                if (Betrayer.betrayer.AllTasksCompleted()) {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.SetRole, Hazel.SendOption.Reliable, -1);
                    writer.Write(8);
                    writer.Write(Betrayer.betrayer.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.setRole(56, Betrayer.betrayer.PlayerId);
                }
                Betrayer.hasBetrayedYet = true;
            }
        }
    }

}
