using System.Linq;
using System;
using System.Collections.Generic;
using TheOtherRoles.Players;
using static TheOtherRoles.TheOtherRoles;
using UnityEngine;
using TheOtherRoles.Utilities;
using TheOtherRoles.CustomGameModes;
using System.Threading.Tasks;
using System.Net.Http;

namespace TheOtherRoles
{
    public class RoleInfo {
        public Color color;
        public string name;
        public string introDescription;
        public string shortDescription;
        public RoleId roleId;
        public bool isNeutral;
        public bool isModifier;

        public RoleInfo(string name, Color color, string IntroDescription, string TaskDescription, RoleId roleId, bool isNeutral = false, bool isModifier = false) {
            this.color = color;
            this.name = name;
            this.introDescription = IntroDescription;
            this.shortDescription = TaskDescription;
            this.roleId = roleId;
            this.isNeutral = isNeutral;
            this.isModifier = isModifier;
        }

        // TODO: (Done) Modifier: Recruiter is a modidfier that can only be applied to imposters and if only 1 imposter that can select a player to recruit to the imposter team!
        // TODO: (Done) Role: Haunter is a (Imposter) role that doesn't show up on admin table and has no kill cooldown for 6 seconds when using Haunt button!
        // TODO: (Done) Role: Sacraficer is a (Crewmate) role that can select someone to sacrafice for and when someone try to kill who i sacraficed for i die and they get a notification that they just got saved!
        
        // TODO: Role: Grenadeer is the TOUR grenadeer role
        // TODO: Role: Phantom is the tour phantom

        //ADDED:
        public static RoleInfo revealer = new RoleInfo("Revealer", Revealer.color, "Catch The Impostor roles by revealing their Role", "Reveal players roles", RoleId.Revealer);
        //public static RoleInfo tasker = new RoleInfo("Tasker", Tasker.color, "Remove Sec's from your CD by doing your Tasks", "Do your Tasks to kill", RoleId.Tasker);
        public static RoleInfo swooper = new RoleInfo("Swooper", Swooper.color, "Swoop and Sabotage", "Go invisible to kill", RoleId.Swooper);
        public static RoleInfo haunter = new RoleInfo("Haun'ter", Haunter.color, "Haun't Crewmates and <color=#FF1919FF>Impostors</color> to win", "Haun't.. oouuuh spooky", RoleId.Haunter, true);
        public static RoleInfo sniper = new RoleInfo("Sniper", Sniper.color, "Impostors Lunge when Killing, I don't", "Impostors Lunge when Killing, I don't!", RoleId.Sniper);
        public static RoleInfo teleporter = new RoleInfo("Teleporter", Teleporter.color, "Kill and Teleport", "Kill and Escape..", RoleId.Teleporter); //impasta
        public static RoleInfo evilTrapper = new RoleInfo("Evil-Trapper", EvilTrapper.color, "Select A Dead-Body to Trap", "Select a Deadbody to Trap", RoleId.EvilTrapper);

        public static RoleInfo befriender = new RoleInfo("Befriender", Befriender.color, "Befriend Everyone", "Befriend everyone to Win!", RoleId.Befriender, true);

        public static RoleInfo sacrificer = new RoleInfo("Sacrificer", Sacrificer.color, "Sacrifice Yourself to save a Friend..", "Sacrificer yourself..", RoleId.Sacraficer); // CREWROLE
        public static RoleInfo ghost = new RoleInfo("Ghost", Haunter.color, "Catch killers Red handed by Teleporting...", "Teleport to gain Information", RoleId.Ghost);
        public static RoleInfo betrayer = new RoleInfo("Betrayer", Betrayer.color, "Betray the crew", "Do your Tasks to Become imposter", RoleId.Betrayer);
        
        public static RoleInfo recruiter = new RoleInfo("Recruiter", Recruiter.color, "Recruit a Crew to make your Team stronger", "Recruit a CrewMember", RoleId.Recruiter, false, true);
        public static RoleInfo flash = new RoleInfo("Flash", Flash.color, "Travel at the Speed of light", "Travel at the speed of light", RoleId.Flash, false, true);
        public static RoleInfo giant = new RoleInfo("Giant", Giant.color, "Travel at the Speed and Size of a Mountain", "Travel at the Speed and Size of a Mountain", RoleId.Giant, false, true);
        public static RoleInfo onetimekiller = new RoleInfo("1TimeKiller", OneTimeKiller.color, "Kill the one You Suspect!", "You can kill but only Once!", RoleId.OneTimeKiller, false, true);


        public static RoleInfo jester = new RoleInfo("Jester", Jester.color, "Get voted out", "Get voted out", RoleId.Jester, true);
        public static RoleInfo mayor = new RoleInfo("Mayor", Mayor.color, "Your vote counts twice", "Your vote counts twice", RoleId.Mayor);
        public static RoleInfo portalmaker = new RoleInfo("Portalmaker", Portalmaker.color, "You can create portals", "You can create portals", RoleId.Portalmaker);
        public static RoleInfo engineer = new RoleInfo("Engineer",  Engineer.color, "Maintain important systems on the ship", "Repair the ship", RoleId.Engineer);
        public static RoleInfo sheriff = new RoleInfo("Sheriff", Sheriff.color, "Shoot the <color=#FF1919FF>Impostors</color>", "Shoot the Impostors", RoleId.Sheriff);
        public static RoleInfo deputy = new RoleInfo("Deputy", Sheriff.color, "Handcuff the <color=#FF1919FF>Impostors</color>", "Handcuff the Impostors", RoleId.Deputy);
        public static RoleInfo lighter = new RoleInfo("Lighter", Lighter.color, "Your light never goes out", "Your light never goes out", RoleId.Lighter);
        public static RoleInfo godfather = new RoleInfo("Godfather", Godfather.color, "Kill all Crewmates", "Kill all Crewmates", RoleId.Godfather);
        public static RoleInfo mafioso = new RoleInfo("Mafioso", Mafioso.color, "Work with the <color=#FF1919FF>Mafia</color> to kill the Crewmates", "Kill all Crewmates", RoleId.Mafioso);
        public static RoleInfo janitor = new RoleInfo("Janitor", Janitor.color, "Work with the <color=#FF1919FF>Mafia</color> by hiding dead bodies", "Hide dead bodies", RoleId.Janitor);
        public static RoleInfo morphling = new RoleInfo("Morphling", Morphling.color, "Change your look to not get caught", "Change your look", RoleId.Morphling);
        public static RoleInfo camouflager = new RoleInfo("Camouflager", Camouflager.color, "Camouflage and kill the Crewmates", "Hide among others", RoleId.Camouflager);
        public static RoleInfo vampire = new RoleInfo("Vampire", Vampire.color, "Kill the Crewmates with your bites", "Bite your enemies", RoleId.Vampire);
        public static RoleInfo eraser = new RoleInfo("Eraser", Eraser.color, "Kill the Crewmates and erase their roles", "Erase the roles of your enemies", RoleId.Eraser);
        public static RoleInfo trickster = new RoleInfo("Trickster", Trickster.color, "Use your jack-in-the-boxes to surprise others", "Surprise your enemies", RoleId.Trickster);
        public static RoleInfo cleaner = new RoleInfo("Cleaner", Cleaner.color, "Kill everyone and leave no traces", "Clean up dead bodies", RoleId.Cleaner);
        public static RoleInfo warlock = new RoleInfo("Warlock", Warlock.color, "Curse other players and kill everyone", "Curse and kill everyone", RoleId.Warlock);
        public static RoleInfo bountyHunter = new RoleInfo("Bounty Hunter", BountyHunter.color, "Hunt your bounty down", "Hunt your bounty down", RoleId.BountyHunter);
        public static RoleInfo detective = new RoleInfo("Detective", Detective.color, "Find the <color=#FF1919FF>Impostors</color> by examining footprints", "Examine footprints", RoleId.Detective);
        public static RoleInfo timeMaster = new RoleInfo("Time Master", TimeMaster.color, "Save yourself with your time shield", "Use your time shield", RoleId.TimeMaster);
        public static RoleInfo medic = new RoleInfo("Medic", Medic.color, "Protect someone with your shield", "Protect other players", RoleId.Medic);
        public static RoleInfo swapper = new RoleInfo("Swapper", Swapper.color, "Swap votes to exile the <color=#FF1919FF>Impostors</color>", "Swap votes", RoleId.Swapper);
        public static RoleInfo seer = new RoleInfo("Seer", Seer.color, "You will see players die", "You will see players die", RoleId.Seer);
        public static RoleInfo hacker = new RoleInfo("Hacker", Hacker.color, "Hack systems to find the <color=#FF1919FF>Impostors</color>", "Hack to find the Impostors", RoleId.Hacker);
        public static RoleInfo tracker = new RoleInfo("Tracker", Tracker.color, "Track the <color=#FF1919FF>Impostors</color> down", "Track the Impostors down", RoleId.Tracker);
        public static RoleInfo snitch = new RoleInfo("Snitch", Snitch.color, "Finish your tasks to find the <color=#FF1919FF>Impostors</color>", "Finish your tasks", RoleId.Snitch);
        public static RoleInfo jackal = new RoleInfo("Jackal", Jackal.color, "Kill all Crewmates and <color=#FF1919FF>Impostors</color> to win", "Kill everyone", RoleId.Jackal, true);
        public static RoleInfo sidekick = new RoleInfo("Sidekick", Sidekick.color, "Help your Jackal to kill everyone", "Help your Jackal to kill everyone", RoleId.Sidekick, true);
        public static RoleInfo spy = new RoleInfo("Spy", Spy.color, "Confuse the <color=#FF1919FF>Impostors</color>", "Confuse the Impostors", RoleId.Spy);
        public static RoleInfo securityGuard = new RoleInfo("Security Guard", SecurityGuard.color, "Seal vents and place cameras", "Seal vents and place cameras", RoleId.SecurityGuard);
        public static RoleInfo arsonist = new RoleInfo("Arsonist", Arsonist.color, "Let them burn", "Let them burn", RoleId.Arsonist, true);
        public static RoleInfo goodGuesser = new RoleInfo("Nice Guesser", Guesser.color, "Guess and shoot", "Guess and shoot", RoleId.NiceGuesser);
        public static RoleInfo evilGuesser = new RoleInfo("Evil Guesser", Palette.ImpostorRed, "Guess and shoot", "Guess and shoot", RoleId.EvilGuesser);
        public static RoleInfo vulture = new RoleInfo("Vulture", Vulture.color, "Eat corpses to win", "Eat dead bodies", RoleId.Vulture, true);
        public static RoleInfo medium = new RoleInfo("Medium", Medium.color, "Question the souls of the dead to gain information", "Question the souls", RoleId.Medium);
        public static RoleInfo trapper = new RoleInfo("Trapper", Trapper.color, "Place traps to find the Impostors", "Place traps", RoleId.Trapper);
        public static RoleInfo lawyer = new RoleInfo("Lawyer", Lawyer.color, "Defend your client", "Defend your client", RoleId.Lawyer, true);
        public static RoleInfo prosecutor = new RoleInfo("Prosecutor", Lawyer.color, "Vote out your target", "Vote out your target", RoleId.Prosecutor, true);
        public static RoleInfo pursuer = new RoleInfo("Pursuer", Pursuer.color, "Blank the Impostors", "Blank the Impostors", RoleId.Pursuer);
        public static RoleInfo impostor = new RoleInfo("Impostor", Palette.ImpostorRed, Helpers.cs(Palette.ImpostorRed, "Sabotage and kill everyone"), "Sabotage and kill everyone", RoleId.Impostor);
        public static RoleInfo crewmate = new RoleInfo("Crewmate", Color.white, "Find the Impostors", "Find the Impostors", RoleId.Crewmate);
        public static RoleInfo witch = new RoleInfo("Witch", Witch.color, "Cast a spell upon your foes", "Cast a spell upon your foes", RoleId.Witch);
        public static RoleInfo ninja = new RoleInfo("Ninja", Ninja.color, "Surprise and assassinate your foes", "Surprise and assassinate your foes", RoleId.Ninja);
        public static RoleInfo thief = new RoleInfo("Thief", Thief.color, "Steal a killers role by killing them", "Steal a killers role", RoleId.Thief, true);
        public static RoleInfo bomber = new RoleInfo("Bomber", Bomber.color, "Bomb all Crewmates", "Bomb all Crewmates", RoleId.Bomber);

        public static RoleInfo hunter = new RoleInfo("Hunter", Palette.ImpostorRed, Helpers.cs(Palette.ImpostorRed, "Seek and kill everyone"), "Seek and kill everyone", RoleId.Impostor);
        public static RoleInfo hunted = new RoleInfo("Hunted", Color.white, "Hide", "Hide", RoleId.Crewmate);

        public static RoleInfo prop = new RoleInfo("Prop", Color.white, "Disguise As An Object and Survive", "Disguise As An Object", RoleId.Crewmate);



        // Modifier
        public static RoleInfo bloody = new RoleInfo("Bloody", Color.yellow, "Your killer leaves a bloody trail", "Your killer leaves a bloody trail", RoleId.Bloody, false, true);
        public static RoleInfo antiTeleport = new RoleInfo("Anti tp", Color.yellow, "You will not get teleported", "You will not get teleported", RoleId.AntiTeleport, false, true);
        public static RoleInfo tiebreaker = new RoleInfo("Tiebreaker", Color.yellow, "Your vote breaks the tie", "Break the tie", RoleId.Tiebreaker, false, true);
        public static RoleInfo bait = new RoleInfo("Bait", Color.yellow, "Bait your enemies", "Bait your enemies", RoleId.Bait, false, true);
        public static RoleInfo sunglasses = new RoleInfo("Sunglasses", Color.yellow, "You got the sunglasses", "Your vision is reduced", RoleId.Sunglasses, false, true);
        public static RoleInfo lover = new RoleInfo("Lover", Lovers.color, $"You are in love", $"You are in love", RoleId.Lover, false, true);
        public static RoleInfo mini = new RoleInfo("Mini", Color.yellow, "No one will harm you until you grow up", "No one will harm you", RoleId.Mini, false, true);
        public static RoleInfo vip = new RoleInfo("VIP", Color.yellow, "You are the VIP", "Everyone is notified when you die", RoleId.Vip, false, true);
        public static RoleInfo invert = new RoleInfo("Invert", Color.yellow, "Your movement is inverted", "Your movement is inverted", RoleId.Invert, false, true);
        public static RoleInfo chameleon = new RoleInfo("Chameleon", Color.yellow, "You're hard to see when not moving", "You're hard to see when not moving", RoleId.Chameleon, false, true);
        public static RoleInfo shifter = new RoleInfo("Shifter", Color.yellow, "Shift your role", "Shift your role", RoleId.Shifter, false, true);


        public static List<RoleInfo> allRoleInfos = new List<RoleInfo>() {
            impostor,
            haunter,
            revealer,
            //tasker,
            swooper,
            sniper,
            teleporter,
            befriender, 
            evilTrapper,
            godfather,
            mafioso,
            janitor,
            morphling,
            camouflager,
            vampire,
            eraser,
            trickster,
            cleaner,
            warlock,
            bountyHunter,
            witch,
            ninja,
            bomber,
            goodGuesser,
            evilGuesser,
            lover,
            jester,
            arsonist,
            jackal,
            sidekick,
            vulture,
            pursuer,
            lawyer,
            thief,
            prosecutor,
            crewmate,
            ghost,
            sacrificer,
            betrayer,
            mayor,
            portalmaker,
            engineer,
            sheriff,
            deputy,
            lighter,
            detective,
            timeMaster,
            medic,
            swapper,
            seer,
            hacker,
            tracker,
            snitch,
            spy,
            securityGuard,
            recruiter, 
            flash, 
            giant, 
            onetimekiller,
            bait,
            medium,
            trapper,
            bloody,
            antiTeleport,
            tiebreaker,
            sunglasses,
            mini,
            vip,
            invert,
            chameleon,
            shifter
        };

        public static List<RoleInfo> getRoleInfoForPlayer(PlayerControl p, bool showModifier = true) {
            List<RoleInfo> infos = new List<RoleInfo>();
            if (p == null) return infos;

            // Modifier
            if (showModifier) {
                // after dead modifier
                if (!CustomOptionHolder.modifiersAreHidden.getBool() || PlayerControl.LocalPlayer.Data.IsDead || AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Ended)
                {
                    if (Bait.bait.Any(x => x.PlayerId == p.PlayerId)) infos.Add(bait);
                    if (Bloody.bloody.Any(x => x.PlayerId == p.PlayerId)) infos.Add(bloody);
                    if (Vip.vip.Any(x => x.PlayerId == p.PlayerId)) infos.Add(vip);
                }
                if (p == Lovers.lover1 || p == Lovers.lover2) infos.Add(lover);
                if (p == Tiebreaker.tiebreaker) infos.Add(tiebreaker);
                if (AntiTeleport.antiTeleport.Any(x => x.PlayerId == p.PlayerId)) infos.Add(antiTeleport);
                if (Sunglasses.sunglasses.Any(x => x.PlayerId == p.PlayerId)) infos.Add(sunglasses);
                if (p == Mini.mini) infos.Add(mini);
                if (Invert.invert.Any(x => x.PlayerId == p.PlayerId)) infos.Add(invert);
                if (Chameleon.chameleon.Any(x => x.PlayerId == p.PlayerId)) infos.Add(chameleon);
                if (p == Shifter.shifter) infos.Add(shifter);


                if (p == Recruiter.recruiter) infos.Add(recruiter);
                if (p == Flash.flash.Any(x => x.PlayerId == p.PlayerId)) infos.Add(flash);
                if (p == Giant.giant.Any(x => x.PlayerId == p.PlayerId)) infos.Add(giant);
                if (p == OneTimeKiller.player) infos.Add(onetimekiller); 

            }

            int count = infos.Count;  // Save count after modifiers are added so that the role count can be checked

            // Special roles
            if (p == Revealer.player) infos.Add(revealer);
            if (p == Swooper.swooper) infos.Add(swooper);
            //if (p == Tasker.tasker) infos.Add(tasker);
            if (p == Haunter.haunter) infos.Add(haunter);
            if (p == Sniper.sniper) infos.Add(sniper);
            if (p == Teleporter.teleporter) infos.Add(teleporter);
            if (p == EvilTrapper.player) infos.Add(evilTrapper);

            if (p == Befriender.befriender) infos.Add(befriender);

            if (p == Betrayer.betrayer) infos.Add(betrayer); 
            if (p == Ghost.ghost) infos.Add(ghost); 
            if (p == Sacrificer.sacrificer) infos.Add(sacrificer);

            if (p == Jester.jester) infos.Add(jester);
            if (p == Mayor.mayor) infos.Add(mayor);
            if (p == Portalmaker.portalmaker) infos.Add(portalmaker);
            if (p == Engineer.engineer) infos.Add(engineer);
            if (p == Sheriff.sheriff || p == Sheriff.formerSheriff) infos.Add(sheriff);
            if (p == Deputy.deputy) infos.Add(deputy);
            if (p == Lighter.lighter) infos.Add(lighter);
            if (p == Godfather.godfather) infos.Add(godfather);
            if (p == Mafioso.mafioso) infos.Add(mafioso);
            if (p == Janitor.janitor) infos.Add(janitor);
            if (p == Morphling.morphling) infos.Add(morphling);
            if (p == Camouflager.camouflager) infos.Add(camouflager);
            if (p == Vampire.vampire) infos.Add(vampire);
            if (p == Eraser.eraser) infos.Add(eraser);
            if (p == Trickster.trickster) infos.Add(trickster);
            if (p == Cleaner.cleaner) infos.Add(cleaner);
            if (p == Warlock.warlock) infos.Add(warlock);
            if (p == Witch.witch) infos.Add(witch);
            if (p == Ninja.ninja) infos.Add(ninja);
            if (p == Bomber.bomber) infos.Add(bomber);
            if (p == Detective.detective) infos.Add(detective);
            if (p == TimeMaster.timeMaster) infos.Add(timeMaster);
            if (p == Medic.medic) infos.Add(medic);
            if (p == Swapper.swapper) infos.Add(swapper);
            if (p == Seer.seer) infos.Add(seer);
            if (p == Hacker.hacker) infos.Add(hacker);
            if (p == Tracker.tracker) infos.Add(tracker);
            if (p == Snitch.snitch) infos.Add(snitch);
            if (p == Jackal.jackal || (Jackal.formerJackals != null && Jackal.formerJackals.Any(x => x.PlayerId == p.PlayerId))) infos.Add(jackal);
            if (p == Sidekick.sidekick) infos.Add(sidekick);
            if (p == Spy.spy) infos.Add(spy);
            if (p == SecurityGuard.securityGuard) infos.Add(securityGuard);
            if (p == Arsonist.arsonist) infos.Add(arsonist);
            if (p == Guesser.niceGuesser) infos.Add(goodGuesser);
            if (p == Guesser.evilGuesser) infos.Add(evilGuesser);
            if (p == BountyHunter.bountyHunter) infos.Add(bountyHunter);
            if (p == Vulture.vulture) infos.Add(vulture);
            if (p == Medium.medium) infos.Add(medium);
            if (p == Lawyer.lawyer && !Lawyer.isProsecutor) infos.Add(lawyer);
            if (p == Lawyer.lawyer && Lawyer.isProsecutor) infos.Add(prosecutor);
            if (p == Trapper.trapper) infos.Add(trapper);
            if (p == Pursuer.pursuer) infos.Add(pursuer);
            if (p == Thief.thief) infos.Add(thief);

            // Default roles (just impostor, just crewmate, or hunter / hunted for hide n seek, prop hunt prop ...
            if (infos.Count == count) {
                if (p.Data.Role.IsImpostor)
                    infos.Add(TORMapOptions.gameMode == CustomGamemodes.HideNSeek || TORMapOptions.gameMode == CustomGamemodes.PropHunt ? RoleInfo.hunter : RoleInfo.impostor);
                else
                    infos.Add(TORMapOptions.gameMode == CustomGamemodes.HideNSeek ? RoleInfo.hunted : TORMapOptions.gameMode == CustomGamemodes.PropHunt ? RoleInfo.prop : RoleInfo.crewmate);
            }

            return infos;
        }

        public static String GetRolesString(PlayerControl p, bool useColors, bool showModifier = true, bool suppressGhostInfo = false) {
            string roleName;
            roleName = String.Join(" ", getRoleInfoForPlayer(p, showModifier).Select(x => useColors ? Helpers.cs(x.color, x.name) : x.name).ToArray());
            if (Lawyer.target != null && p.PlayerId == Lawyer.target.PlayerId && CachedPlayer.LocalPlayer.PlayerControl != Lawyer.target) 
                roleName += (useColors ? Helpers.cs(Pursuer.color, " §") : " §");
            if (HandleGuesser.isGuesserGm && HandleGuesser.isGuesser(p.PlayerId)) roleName += " (Guesser)";

            if (!suppressGhostInfo && p != null) {
                if (p == Shifter.shifter && (CachedPlayer.LocalPlayer.PlayerControl == Shifter.shifter || Helpers.shouldShowGhostInfo()) && Shifter.futureShift != null)
                    roleName += Helpers.cs(Color.yellow, " ← " + Shifter.futureShift.Data.PlayerName);
                if (p == Vulture.vulture && (CachedPlayer.LocalPlayer.PlayerControl == Vulture.vulture || Helpers.shouldShowGhostInfo()))
                    roleName = roleName + Helpers.cs(Vulture.color, $" ({Vulture.vultureNumberToWin - Vulture.eatenBodies} left)");
                if (Helpers.shouldShowGhostInfo()) {
                    if (Eraser.futureErased.Contains(p))
                        roleName = Helpers.cs(Color.gray, "(erased) ") + roleName;
                    if (Vampire.vampire != null && !Vampire.vampire.Data.IsDead && Vampire.bitten == p && !p.Data.IsDead)
                        roleName = Helpers.cs(Vampire.color, $"(bitten {(int)HudManagerStartPatch.vampireKillButton.Timer + 1}) ") + roleName;
                    if (Deputy.handcuffedPlayers.Contains(p.PlayerId))
                        roleName = Helpers.cs(Color.gray, "(cuffed) ") + roleName;
                    if (Deputy.handcuffedKnows.ContainsKey(p.PlayerId))  // Active cuff
                        roleName = Helpers.cs(Deputy.color, "(cuffed) ") + roleName;
                    if (p == Warlock.curseVictim)
                        roleName = Helpers.cs(Warlock.color, "(cursed) ") + roleName;
                    if (p == Ninja.ninjaMarked)
                        roleName = Helpers.cs(Ninja.color, "(marked) ") + roleName;
                    if (Pursuer.blankedList.Contains(p) && !p.Data.IsDead)
                        roleName = Helpers.cs(Pursuer.color, "(blanked) ") + roleName;
                    if (Witch.futureSpelled.Contains(p) && !MeetingHud.Instance) // This is already displayed in meetings!
                        roleName = Helpers.cs(Witch.color, "☆ ") + roleName;
                    if (BountyHunter.bounty == p)
                        roleName = Helpers.cs(BountyHunter.color, "(bounty) ") + roleName;
                    if (Arsonist.dousedPlayers.Contains(p))
                        roleName = Helpers.cs(Arsonist.color, "♨ ") + roleName;
                    if (Befriender.befriendedPlayers.Contains(p))
                        roleName = Helpers.cs(Befriender.color, "🌫 ") + roleName;
                    if (p == Befriender.befriender) {
                        roleName = roleName + Helpers.cs(Befriender.color, $" ({CachedPlayer.AllPlayers.Count(x => { return x.PlayerControl != Befriender.befriender && !x.Data.IsDead && !x.Data.Disconnected && !Befriender.befriendedPlayers.Any(y => y.PlayerId == x.PlayerId); })} left)");
                    }
                    if (p == Arsonist.arsonist)
                        roleName = roleName + Helpers.cs(Arsonist.color, $" ({CachedPlayer.AllPlayers.Count(x => { return x.PlayerControl != Arsonist.arsonist && !x.Data.IsDead && !x.Data.Disconnected && !Arsonist.dousedPlayers.Any(y => y.PlayerId == x.PlayerId); })} left)");
                    if (p == Jackal.fakeSidekick)
                        roleName = Helpers.cs(Sidekick.color, $" (fake SK)") + roleName;

                    // Death Reason on Ghosts
                    if (p.Data.IsDead) {
                        string deathReasonString = "";
                        var deadPlayer = GameHistory.deadPlayers.FirstOrDefault(x => x.player.PlayerId == p.PlayerId);

                        Color killerColor = new();
                        if (deadPlayer != null && deadPlayer.killerIfExisting != null) {
                            killerColor = RoleInfo.getRoleInfoForPlayer(deadPlayer.killerIfExisting, false).FirstOrDefault().color;
                        }

                        if (deadPlayer != null) {
                            switch (deadPlayer.deathReason) {
                                case DeadPlayer.CustomDeathReason.Disconnect:
                                    deathReasonString = " - disconnected";
                                    break;
                                case DeadPlayer.CustomDeathReason.Exile:
                                    deathReasonString = " - voted out";
                                    break;
                                case DeadPlayer.CustomDeathReason.Kill:
                                    deathReasonString = $" - killed by {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)}";
                                    break;
                                case DeadPlayer.CustomDeathReason.Guess:
                                    if (deadPlayer.killerIfExisting.Data.PlayerName == p.Data.PlayerName)
                                        deathReasonString = $" - failed guess";
                                    else
                                        deathReasonString = $" - guessed by {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)}";
                                    break;
                                case DeadPlayer.CustomDeathReason.Shift:
                                    deathReasonString = $" - {Helpers.cs(Color.yellow, "shifted")} {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)}";
                                    break;
                                case DeadPlayer.CustomDeathReason.WitchExile:
                                    deathReasonString = $" - {Helpers.cs(Witch.color, "witched")} by {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)}";
                                    break;
                                case DeadPlayer.CustomDeathReason.LoverSuicide:
                                    deathReasonString = $" - {Helpers.cs(Lovers.color, "lover died")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.LawyerSuicide:
                                    deathReasonString = $" - {Helpers.cs(Lawyer.color, "bad Lawyer")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.Bomb:
                                    deathReasonString = $" - bombed by {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)}";
                                    break;
                                case DeadPlayer.CustomDeathReason.Arson:
                                    deathReasonString = $" - burnt by {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)}";
                                    break;
                            }
                            roleName = roleName + deathReasonString;
                        }
                    }
                }
            }
            return roleName;
        }


        static string ReadmePage = "";
        public static async Task loadReadme() {
            if (ReadmePage == "") {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://raw.githubusercontent.com/TheOtherRolesAU/TheOtherRoles/main/README.md");
                response.EnsureSuccessStatusCode();
                string httpres = await response.Content.ReadAsStringAsync();
                ReadmePage = httpres;
            }
        }
        public static string GetRoleDescription(RoleInfo roleInfo) {
            while (ReadmePage == "") {
            }
                
            int index = ReadmePage.IndexOf($"## {roleInfo.name}");
            int endindex = ReadmePage.Substring(index).IndexOf("### Game Options");
            return ReadmePage.Substring(index, endindex);

        }
    }
        public static class RoleInfoExtensions {
            

            public static RoleInfo Role(this PlayerControl p) { 
                if (p == Revealer.player) return RoleInfo.revealer;
                if (p == Swooper.swooper) return RoleInfo.swooper;
                if (p == Haunter.haunter) return RoleInfo.haunter;
                if (p == Sniper.sniper) return RoleInfo.sniper;
                if (p == Teleporter.teleporter) return RoleInfo.teleporter;
                if (p == EvilTrapper.player) return RoleInfo.evilTrapper;

                if (p == Befriender.befriender) return RoleInfo.befriender;

                if (p == Betrayer.betrayer) return RoleInfo.betrayer;
                if (p == Ghost.ghost) return RoleInfo.ghost;
                if (p == Sacrificer.sacrificer) return RoleInfo.sacrificer;

                if (p == Jester.jester) return RoleInfo.jester;
                if (p == Mayor.mayor) return RoleInfo.mayor;
                if (p == Portalmaker.portalmaker) return RoleInfo.portalmaker;
                if (p == Engineer.engineer) return RoleInfo.engineer;
                if (p == Sheriff.sheriff || p == Sheriff.formerSheriff) return RoleInfo.sheriff;
                if (p == Deputy.deputy) return RoleInfo.deputy;
                if (p == Lighter.lighter) return RoleInfo.lighter;
                if (p == Godfather.godfather) return RoleInfo.godfather;
                if (p == Mafioso.mafioso) return RoleInfo.mafioso;
                if (p == Janitor.janitor) return RoleInfo.janitor;
                if (p == Morphling.morphling) return RoleInfo.morphling;
                if (p == Camouflager.camouflager) return RoleInfo.camouflager;
                if (p == Vampire.vampire) return RoleInfo.vampire;
                if (p == Eraser.eraser) return RoleInfo.eraser;
                if (p == Trickster.trickster) return RoleInfo.trickster;
                if (p == Cleaner.cleaner) return RoleInfo.cleaner;
                if (p == Warlock.warlock) return RoleInfo.warlock;
                if (p == Witch.witch) return RoleInfo.witch;
                if (p == Ninja.ninja) return RoleInfo.ninja;
                if (p == Bomber.bomber) return RoleInfo.bomber;
                if (p == Detective.detective) return RoleInfo.detective;
                if (p == TimeMaster.timeMaster) return RoleInfo.timeMaster;
                if (p == Medic.medic) return RoleInfo.medic;
                if (p == Swapper.swapper) return RoleInfo.swapper;
                if (p == Seer.seer) return RoleInfo.seer;
                if (p == Hacker.hacker) return RoleInfo.hacker;
                if (p == Tracker.tracker) return RoleInfo.tracker;
                if (p == Snitch.snitch) return RoleInfo.snitch;
                if (p == Jackal.jackal || (Jackal.formerJackals != null && Jackal.formerJackals.Any(x => x.PlayerId == p.PlayerId))) return RoleInfo.jackal;
                if (p == Sidekick.sidekick) return RoleInfo.sidekick;
                if (p == Spy.spy) return RoleInfo.spy;
                if (p == SecurityGuard.securityGuard) return RoleInfo.securityGuard;
                if (p == Arsonist.arsonist) return RoleInfo.arsonist;
                if (p == Guesser.niceGuesser) return RoleInfo.goodGuesser;
                if (p == Guesser.evilGuesser) return RoleInfo.evilGuesser;
                if (p == BountyHunter.bountyHunter) return RoleInfo.bountyHunter;
                if (p == Vulture.vulture) return RoleInfo.vulture;
                if (p == Medium.medium) return RoleInfo.medium;
                if (p == Lawyer.lawyer && !Lawyer.isProsecutor) return RoleInfo.lawyer;
                if (p == Lawyer.lawyer && Lawyer.isProsecutor) return RoleInfo.prosecutor;
                if (p == Trapper.trapper) return RoleInfo.trapper;
                if (p == Pursuer.pursuer) return RoleInfo.pursuer;
                if (p == Thief.thief) return RoleInfo.thief;

            return null;
        }
            public static void clearCustomRole(this PlayerControl p, RoleInfo role = null) {
                if (role == null) {
                    if (p == Revealer.player) Revealer.clearAndReload();
                    if (p == Swooper.swooper) Swooper.clearAndReload();
                    if (p == Haunter.haunter) Haunter.clearAndReload();
                    if (p == Sniper.sniper) Sniper.clearAndReload();
                    if (p == Teleporter.teleporter) Teleporter.clearAndReload();
                    if (p == EvilTrapper.player) EvilTrapper.clearAndReload();
                    if (p == Befriender.befriender) Befriender.clearAndReload();
                    if (p == Betrayer.betrayer) Betrayer.clearAndReload();
                    if (p == Ghost.ghost) Ghost.clearAndReload();
                    if (p == Sacrificer.sacrificer) Sacrificer.clearAndReload();
                    if (p == Jester.jester) Jester.clearAndReload();
                    if (p == Mayor.mayor) Mayor.clearAndReload();
                    if (p == Portalmaker.portalmaker) Portalmaker.clearAndReload();
                    if (p == Engineer.engineer) Engineer.clearAndReload();
                    if (p == Sheriff.sheriff || p == Sheriff.formerSheriff) Sheriff.clearAndReload();
                    if (p == Deputy.deputy) Deputy.clearAndReload();
                    if (p == Lighter.lighter) Lighter.clearAndReload();
                    if (p == Godfather.godfather) Godfather.clearAndReload();
                    if (p == Mafioso.mafioso) Mafioso.clearAndReload();
                    if (p == Janitor.janitor) Janitor.clearAndReload();
                    if (p == Morphling.morphling) Morphling.clearAndReload();
                    if (p == Camouflager.camouflager) Camouflager.clearAndReload();
                    if (p == Vampire.vampire) Vampire.clearAndReload();
                    if (p == Eraser.eraser) Eraser.clearAndReload();
                    if (p == Trickster.trickster) Trickster.clearAndReload();
                    if (p == Cleaner.cleaner) Cleaner.clearAndReload();
                    if (p == Warlock.warlock) Warlock.clearAndReload();
                    if (p == Witch.witch) Witch.clearAndReload();
                    if (p == Ninja.ninja) Ninja.clearAndReload();
                    if (p == Bomber.bomber) Bomber.clearAndReload();
                    if (p == Detective.detective) Detective.clearAndReload();
                    if (p == TimeMaster.timeMaster) TimeMaster.clearAndReload();
                    if (p == Medic.medic) Medic.clearAndReload();
                    if (p == Swapper.swapper) Swapper.clearAndReload();
                    if (p == Seer.seer) Seer.clearAndReload();
                    if (p == Hacker.hacker) Hacker.clearAndReload();
                    if (p == Tracker.tracker) Tracker.clearAndReload();
                    if (p == Snitch.snitch) Snitch.clearAndReload();
                    if (p == Jackal.jackal || (Jackal.formerJackals != null && Jackal.formerJackals.Any(x => x.PlayerId == p.PlayerId))) Jackal.clearAndReload();
                    if (p == Sidekick.sidekick) Sidekick.clearAndReload();
                    if (p == Spy.spy) Spy.clearAndReload();
                    if (p == SecurityGuard.securityGuard) SecurityGuard.clearAndReload();
                    if (p == Arsonist.arsonist) Arsonist.clearAndReload();
                    if (p == Guesser.niceGuesser) Guesser.clearAndReload();
                    if (p == Guesser.evilGuesser) Guesser.clearAndReload();
                    if (p == BountyHunter.bountyHunter) BountyHunter.clearAndReload();
                    if (p == Vulture.vulture) Vulture.clearAndReload();
                    if (p == Medium.medium) Medium.clearAndReload();
                    if (p == Lawyer.lawyer && !Lawyer.isProsecutor) Lawyer.clearAndReload();
                    if (p == Lawyer.lawyer && Lawyer.isProsecutor) Lawyer.clearAndReload();
                    if (p == Trapper.trapper) Trapper.clearAndReload();
                    if (p == Pursuer.pursuer) Pursuer.clearAndReload();
                    if (p == Thief.thief) Thief.clearAndReload();
                } else {
                    //do somethin (TODO)!
                }
            }
        }

}
