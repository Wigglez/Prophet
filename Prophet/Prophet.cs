using System;
using System.Windows.Media;
using Bots.DungeonBuddy.Helpers;
using Styx.Common;
using Styx.Plugins;
using Styx.WoWInternals;

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
            get { return "AknA & Wigglez"; }
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
                Lua.Events.AttachEvent("PARTY_INVITE_REQUEST", Character.HandlePartyInviteRequest);
                base.OnEnable();
            } catch(Exception e) {
                CustomDiagnosticLog("Could not initialize. Message = " + e.Message + " Stacktrace = " + e.StackTrace);
            } finally {
                CustomNormalLog("Initialization complete.");
            }
        }

        public override void OnDisable() {
            try {
                Lua.Events.DetachEvent("PARTY_INVITE_REQUEST", Character.HandlePartyInviteRequest);
                base.OnDisable();
            } catch(Exception e) {
                CustomDiagnosticLog("Could not dispose. Message = " + e.Message + " Stacktrace = " + e.StackTrace);
            } finally {
                CustomNormalLog("Shutdown complete.");
            }
        }

        public override void Pulse() {
            if(!PartyLeader.CanInvite()) {
                return;
            }

            // The default settings
            if(PartySettings.Instance.PartyClassification == "None") {
                return;
            }
            
            // If the user is the party leader
            if(PartySettings.Instance.PartyClassification == "Party Leader") {
                PartyMember.DetermineNameAndRealm();
                
                // Constantly update the amount of people we should have in group (in case it changes)
                PartyLeader.RequiredPartyCount = PartyLeader.GetRequiredPartyCount();

                if(!PartyMember.Exists()) {
                    CustomNormalLog("You have to provide at least one party member name.");
                    return;
                }

                PartyLeader.SendOutInvites();

                if(Character.Me.GroupInfo.IsInParty) {
                    if(!PartyLeader.HandlePartyFunctionsTimer.IsRunning) {
                        Character.HandleLootMethod();
                        Character.HandleLootThreshold();
                        Character.HandleDungeonDifficulty();
                        Character.HandlePassOnLoot();
                        Character.HandleSetRole();
                        PartyLeader.HandlePartyFunctionsTimer.Start();
                    } else {
                        if(PartyLeader.HandlePartyFunctionsTimer.ElapsedMilliseconds > 1000) {
                            PartyLeader.HandlePartyFunctionsTimer.Reset();
                        }
                    }
                }
            }

            // If the user is the party member
            if(PartySettings.Instance.PartyClassification != "Party Member") {
                return;
            }

            if(!PartyLeader.Exists()) {
                CustomNormalLog("You have to provide the party leader name.");
                return;
            }

            PartyLeader.DetermineNameAndRealm();

            if(!Character.Me.GroupInfo.IsInParty) {
                return;
            }

            if(!PartyMember.AcceptInviteTimer.IsRunning) {
                PartyMember.AcceptInviteTimer.Start();
            } else {
                if(PartyMember.AcceptInviteTimer.ElapsedMilliseconds < 500) {
                    return;
                }

                PartyMember.AcceptInviteTimer.Reset();
            }


            if(Character.Me.IsLeader()) {
                if(Character.GroupMemberExistsInParty(PartyLeader.Name)) {
                    Lua.DoString(string.Format("PromoteToLeader('{0}');", PartyLeader.Name));
                }
            }

            Character.HandlePassOnLoot();
            Character.HandleSetRole();
            Character.HandleStaticPopup();
            // If party leader is gone, leave
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

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
