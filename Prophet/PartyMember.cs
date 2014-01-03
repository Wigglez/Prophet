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

        public static void LeaveParty() {
            if(PartySettings.Instance.PartyClassification != "Party Member") { return; }

            if(Character.GroupMemberExistsInParty(PartyLeader.Name)) { return; }

            Prophet.CustomNormalLog("Party leader {0} isn't in group, leaving group.", PartyLeader.Name);
            Lua.DoString("LeaveParty()");
        }

        public static void LeaderNameNormalize() {
            if(string.IsNullOrEmpty(PartySettings.Instance.PartyLeaderName)) {
                return;
            }

            if(PartySettings.Instance.PartyLeaderName.Contains('-')) {
                PartyLeader.Name = PartySettings.Instance.PartyLeaderName;
            } else {
                PartyLeader.Name = PartySettings.Instance.PartyLeaderName + '-' + Character.Me.RealmName;
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
