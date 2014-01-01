using System;
using System.Windows.Media;
using Bots.DungeonBuddy.Helpers;
using Styx;
using Styx.Common;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace Prophet {
    public class Prophet : HBPlugin {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override string Name {
            get { return "Prophet"; }
        }

        public override string Author {
            get { return "Akna & Wigglez"; }
        }

        public override Version Version {
            get { return new Version(1, 0); }
        }

        public override bool WantButton {
            get { return true; }
        }

        public override string ButtonText {
            get { return "User Interface"; }
        }

        public override void OnButtonPress() {
            var gui = new ProphetGUI();
            gui.ShowDialog();
        }

        public override void OnEnable() {
            try {
                CustomNormalLog("test enable");
                Lua.Events.AttachEvent("PARTY_INVITE_REQUEST", HandlePartyInviteRequest);
                base.OnEnable();
            } catch(Exception e) {
                CustomDiagnosticLog("Could not initialize. Message = " + e.Message + " Stacktrace = " + e.StackTrace);
            } finally {
                CustomNormalLog("Initialization complete.");
            }
        }

        public override void OnDisable() {
            try {
                CustomNormalLog("test disable");
                Lua.Events.DetachEvent("PARTY_INVITE_REQUEST", HandlePartyInviteRequest);
                base.OnDisable();
            } catch(Exception e) {
                CustomDiagnosticLog("Could not dispose. Message = " + e.Message + " Stacktrace = " + e.StackTrace);
            } finally {
                CustomNormalLog("Shutdown complete.");
            }
        }
        
        public override void Pulse() {
            if(!PartyLeader.CanInvite()) { return; }

            // The default settings
            if(PartySettings.Instance.PartyClassification == "None") { return; }

            // If the user is the party leader
            if(PartySettings.Instance.PartyClassification == "Party Leader") {
                // Constantly update the amount of people we should have in group (in case it changes)
                PartyLeader.RequiredPartyCount = PartyLeader.GetRequiredPartyCount();

                if(!PartyMember.Exists()) {
                    CustomNormalLog("You have to provide at least one party member name.");
                    return;
                }

                if(Character.Me.GroupInfo.IsInParty) {

                    if(!PartyLeader.PartyLeaderTimer.IsRunning) {
                        Character.HandleLootMethod();
                        Character.HandleLootThreshold();
                        Character.HandleDungeonDifficulty();
                        Character.HandlePassOnLoot();
                        Character.HandleSetRole();
                        PartyLeader.PartyLeaderTimer.Start();
                    } else {
                        if(PartyLeader.PartyLeaderTimer.ElapsedMilliseconds > 1000) {
                            PartyLeader.PartyLeaderTimer.Reset();
                        }
                    }
                }

                PartyLeader.SendOutInvites();
            }

            // If the user is the party member
            if(PartySettings.Instance.PartyClassification != "Party Member") {
                return;
            }

            if(!PartyLeader.Exists()) {
                CustomNormalLog("You have to provide the party leader name.");
            }

            if(Character.Me.GroupInfo.IsInParty) {
                if(Character.Me.IsLeader()) {
                    if(Character.GroupMemberExistsInParty(PartySettings.Instance.PartyLeaderName)) {
                        Lua.DoString(string.Format("PromoteToLeader('{0}');", PartySettings.Instance.PartyLeaderName));
                    }
                }
            }

            if(!PartyMember.PartyMemberTimer.IsRunning) {
                return;
            }

            if(PartyMember.PartyMemberTimer.ElapsedMilliseconds < 1000) {
                return;
            }

            if(!Character.Me.GroupInfo.IsInParty) {
                PartyMember.PartyMemberTimer.Reset();
            }

            Character.HandleStaticPopup();
            PartyMember.LeaveParty();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public static void CustomNormalLog(string message, params object[] args) {
            Logging.Write(Colors.DeepSkyBlue, "[Prophet]: " + message, args);
        }

        public static void CustomDiagnosticLog(string message, params object[] args) {
            Logging.WriteDiagnostic(Colors.DeepSkyBlue, "[Prophet]: " + message, args);
        }

        public static void HandlePartyInviteRequest(object sender, LuaEventArgs args) {
            if(PartySettings.Instance.PartyClassification == "Party Leader") {
                Character.DeclineInvite();
                return;
            }

            var partyInviteSender = args.Args[0].ToString();

            CustomNormalLog("We got an invite from " + partyInviteSender + ".");

            if(partyInviteSender == PartySettings.Instance.PartyLeaderName) {
                Character.AcceptInvite();

                if(!PartyMember.PartyMemberTimer.IsRunning) {
                    PartyMember.PartyMemberTimer.Start();
                }
            } else {
                Character.DeclineInvite();
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

    }
}
