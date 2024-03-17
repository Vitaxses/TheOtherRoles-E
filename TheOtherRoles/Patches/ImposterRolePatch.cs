using HarmonyLib;
using AmongUs.Data;
using AmongUs.GameOptions;
using AmongUsServer;
using static TheOtherRoles.TheOtherRoles;
using Discord;

namespace TheOtherRoles.Patches.Added;

public class ImpRolePatch {

    /*[HarmonyPatch(typeof(ImpostorRole), nameof(ImpostorRole.CanUse))]
    public class CanUsePatch {

        public static bool Postfix(IUsable usable) {
            if (PlayerControl.LocalPlayer== Tasker.tasker) return !GameManager.Instance.LogicUsables.CanUse(usable, Tasker.tasker);
            
            return GameManager.Instance.LogicUsables.CanUse(usable, PlayerControl.LocalPlayer);
        }
    }*/

}