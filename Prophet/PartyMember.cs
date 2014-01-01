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

            if(Character.GroupMemberExistsInParty(PartySettings.Instance.PartyLeaderName)) { return; }

            Prophet.CustomNormalLog("Party leader {0} isn't in group, leaving group.", PartySettings.Instance.PartyLeaderName);
            Lua.DoString("LeaveParty()");
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
