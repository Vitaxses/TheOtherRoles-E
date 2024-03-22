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

        /*public static void Prefix(AchievementManager __instance) {
            PlayerControl me = CachedPlayer.LocalPlayer.PlayerControl;
            if (me == Tasker.tasker) {
                if (HudManager.Instance) {
                    System.Random random = new System.Random((int) DateTime.Now.Ticks);
                    int i = random.Next((int) Tasker.MinSecondsToRemove, (int) Tasker.MaxSecondsToRemove);
                    HudManager.Instance.KillButton.SetCoolDown(Tasker.liveCooldown - i, Tasker.KillCooldown);
                } 
            }
        }*/
    }

    [HarmonyPatch(typeof(ReportButton), nameof(ReportButton.DoClick))]
    public class onReport {
        public static void Prefix(ReportButton __instance) {
            DeadBody deadBody = null;
                foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(CachedPlayer.LocalPlayer.PlayerControl.GetTruePosition(), CachedPlayer.LocalPlayer.PlayerControl.MaxReportDistance, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody") {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component != null) {
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
                if (Sacrificer.sacrificer != null && Sacrificer.target != null && current == Sacrificer.target) {
                    PlayerControl sacrificer = Sacrificer.sacrificer;
                    __instance.SetDisabled();
                    if (sacrificer != CachedPlayer.LocalPlayer.PlayerControl) {
                        Helpers.MurderPlayer(sacrificer, me);
                    }
                    Helpers.checkMurderAttemptAndKill(sacrificer, me, false, false, true, true);
                    Helpers.checkMurderAttemptAndKill(sacrificer, sacrificer, false, false, true, true);
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

                    MessageWriter boolWriter = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.setBetrayerHasBetrayed, SendOption.Reliable, -1);
                    boolWriter.Write(byte.MaxValue);
                    AmongUsClient.Instance.FinishRpcImmediately(boolWriter);
                    RPCProcedure.setBetrayerHasBetrayed(byte.MaxValue);

                    
                    RoleManager.Instance.SetRole(Betrayer.betrayer, RoleTypes.Impostor);

                    MessageWriter writer1 = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.SetRole, SendOption.Reliable, -1);
                    writer1.Write((byte)RoleId.Impostor);
                    writer1.Write(Betrayer.betrayer.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer1);
                    RPCProcedure.setRole((byte)RoleId.Impostor, Betrayer.betrayer.PlayerId);

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.SetRoleTeam, SendOption.Reliable, -1);
                    writer.Write((byte)RoleTypes.Impostor);
                    writer.Write(Betrayer.betrayer.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.setRoleTeam((byte)RoleTypes.Impostor, Betrayer.betrayer.PlayerId);


                    if ( __instance == CachedPlayer.LocalPlayer.PlayerControl && __instance == Betrayer.betrayer ) {
                        Betrayer.betrayer.ClearTasks();
                        HudManager.Instance.ImpostorVentButton.Show();
                        HudManager.Instance.ImpostorVentButton.SetEnabled();

                        HudManager.Instance.KillButton.Show();
                        HudManager.Instance.KillButton.SetEnabled();

                        HudManager.Instance.SabotageButton.Show();
                        HudManager.Instance.SabotageButton.SetEnabled();
                    }


                } else {
                    return;
                }
            }
        }

    }

}
