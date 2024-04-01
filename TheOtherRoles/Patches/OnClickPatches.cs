using HarmonyLib;
using Hazel;
using Rewired;
using TheOtherRoles.Objects;
using TheOtherRoles.Players;
using static TheOtherRoles.TheOtherRoles;

namespace TheOtherRoles.Patches;

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.OnClick))]
public static class clickPatch {

    public static void Prefix(PlayerControl __instance) {
        if (Sheriff.canClickOnPlayer) {
            PlayerControl local = CachedPlayer.LocalPlayer.PlayerControl;
            if (Sheriff.sheriff != null && local == Sheriff.sheriff && __instance == Sheriff.currentTarget) {
                if (HudManagerStartPatch.sheriffKillButton.Timer < 0.1f) {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Sheriff.sheriff, Sheriff.currentTarget);
                        if (murderAttemptResult == MurderAttemptResult.SuppressKill) return;

                        if (murderAttemptResult == MurderAttemptResult.PerformKill) {
                            byte targetId = 0;
                            if ((Sheriff.currentTarget.Data.Role.IsImpostor && (Sheriff.currentTarget != Mini.mini || Mini.isGrownUp())) ||
                                (Sheriff.spyCanDieToSheriff && Spy.spy == Sheriff.currentTarget) ||
                                (Sheriff.canKillNeutrals && Helpers.isNeutral(Sheriff.currentTarget)) ||
                                (Jackal.jackal == Sheriff.currentTarget || Sidekick.sidekick == Sheriff.currentTarget)) {
                                targetId = Sheriff.currentTarget.PlayerId;
                            }
                            else {
                                targetId = CachedPlayer.LocalPlayer.PlayerId;
                            }

                            MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                            killWriter.Write(Sheriff.sheriff.Data.PlayerId);
                            killWriter.Write(targetId);
                            killWriter.Write(byte.MaxValue);
                            AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                            RPCProcedure.uncheckedMurderPlayer(Sheriff.sheriff.Data.PlayerId, targetId, byte.MaxValue);
                        }
                }
            }
        }
    }

}