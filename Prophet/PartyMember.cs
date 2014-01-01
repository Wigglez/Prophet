using System.Diagnostics;
using System.Linq;
using Styx.WoWInternals;

namespace Prophet {
    public class PartyMember {

        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        public static string[] Name = new string[4];

        public static Stopwatch PartyMemberTimer = new Stopwatch();

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static bool Exists() {
            return Name.Any(member => member != null);
        }

        public static void AcceptInvite() {
            Prophet.CustomNormalLog("Accepted invite.");
            Lua.DoString("AcceptGroup()");

            if(!PartyMemberTimer.IsRunning) {
                PartyMemberTimer.Start();
            }    
        }

        public static void DeclineInvite() {
            Prophet.CustomNormalLog("Declined invite.");
            Lua.DoString("DeclineGroup()");

            HandleStaticPopup();
        }

        public static void HandleStaticPopup() {
            // If there is a popup visible, get rid of that shit
            var partyInvitePopup = Lua.GetReturnVal<bool>("return StaticPopup_Visible('PARTY_INVITE')", 0);
            var partyInviteCrossrealmPopup = Lua.GetReturnVal<bool>("return StaticPopup_Visible('PARTY_INVITE_XREALM')", 0);

            if(!partyInvitePopup && !partyInviteCrossrealmPopup) {
                return;
            }

            Lua.DoString("StaticPopup_Hide('PARTY_INVITE')");
            Lua.DoString("StaticPopup_Hide('PARTY_INVITE_XREALM')");
        }

        public static void LeaveParty() {
            if(PartySettings.Instance.PartyClassification != "Party Member") { return; }

            if(PartyLeader.GroupMemberExistsInParty(PartySettings.Instance.PartyLeaderName)) { return; }

            Prophet.CustomNormalLog("Party leader {0} isn't in group, leaving group.", PartySettings.Instance.PartyLeaderName);
            Lua.DoString("LeaveParty()");
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
