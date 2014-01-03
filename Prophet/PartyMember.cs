using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
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
        public static string[] Realm = new string[4];
        public static string[] NameAndRealm = new string[4];

        public static Stopwatch AcceptInviteTimer = new Stopwatch();

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

        public static void DetermineNameAndRealm() {
            for(var i = 0; i < PartySettings.Instance.PartyMemberName.Length; i++) {
                if(string.IsNullOrEmpty(PartySettings.Instance.PartyMemberName[i])) {
                    continue;
                }

                if(!PartySettings.Instance.PartyMemberName[i].Contains('-')) {
                    Name[i] = PartySettings.Instance.PartyMemberName[i];
                    Realm[i] = Character.Me.RealmName;
                } else {
                    var indexOfDash = PartySettings.Instance.PartyMemberName[i].IndexOf('-');
                    Name[i] = PartySettings.Instance.PartyMemberName[i].Substring(0, indexOfDash);
                    Realm[i] = PartySettings.Instance.PartyMemberName[i].Substring(indexOfDash + 1);
                }

                NameAndRealm[i] = Name[i] + "-" + Realm[i];
            }
        }

        public static bool Exists() {
            return Name.Any(member => member != null);
        }

        public static void LeaveParty() {
            if(PartySettings.Instance.PartyClassification != PartySettings.StringPartyMember) { return; }

            if(Character.GroupMemberExistsInParty(PartyLeader.NameAndRealm)) { return; }

            if(Character.GroupMemberExistsInParty(PartyLeader.Name)) { return; }

            Prophet.CustomNormalLog("Our designated party leader isn't in the party, leaving party.");
            Lua.DoString("LeaveParty()");
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
