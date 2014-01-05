using System;
using System.Collections.Generic;
using System.Linq;
using Styx.Common.Helpers;
using Styx.CommonBot.Database;
using Styx.WoWInternals.WoWObjects;

namespace Chameleon.Helpers {
    public class CustomBlacklist {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static readonly Dictionary<ulong, DateTime> GuidBlacklist = new Dictionary<ulong, DateTime>();
        private static readonly Dictionary<int, DateTime> EntryBlacklist = new Dictionary<int, DateTime>();
        private static readonly Dictionary<string, DateTime> NameBlacklist = new Dictionary<string, DateTime>();

        private static WaitTimer _sweepTimer;

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

        public static void SweepTimer() {
            TimeSpan maxSweepTime = TimeSpan.FromSeconds(30);

            _sweepTimer = new WaitTimer(maxSweepTime) {WaitTime = maxSweepTime};
        }

        // GUID
        public static void AddGUID(ulong guid, TimeSpan timeSpan) {
            RemoveExpiredGUID();
            GuidBlacklist[guid] = DateTime.Now.Add(timeSpan);
        }

        public static void AddGUID(WoWObject wowObject, TimeSpan timeSpan) {
            if(wowObject != null) {
                AddGUID(wowObject.Guid, timeSpan);
            }
        }


        public static bool ContainsGUID(ulong guid) {
            DateTime expiry;
            if(GuidBlacklist.TryGetValue(guid, out expiry)) {
                return (expiry > DateTime.Now);
            }
            return false;
        }

        public static bool ContainsGUID(WoWObject wowObject) { return (wowObject != null) && ContainsGUID(wowObject.Guid); }

        public static void RemoveExpiredGUID() {
            if(_sweepTimer.IsFinished) {
                DateTime now = DateTime.Now;
                List<ulong> expiredEntries = (from key in GuidBlacklist.Keys where (GuidBlacklist[key] < now) select key).ToList();
                foreach(ulong entry in expiredEntries) {
                    GuidBlacklist.Remove(entry);
                }
                _sweepTimer.Reset();
            }
        }

        public static void RemoveAllGUID() {
            List<ulong> everything = (from key in GuidBlacklist.Keys select key).ToList();
            foreach(ulong entry in everything) {
                GuidBlacklist.Remove(entry);
            }
        }

        // Entry
        public static void AddEntry(int entry, TimeSpan timeSpan) {
            RemoveExpiredGUID();
            EntryBlacklist[entry] = DateTime.Now.Add(timeSpan);
        }

        public static void AddEntry(NpcResult npcResult, TimeSpan timeSpan) {
            if(npcResult != null) {
                AddEntry(npcResult.Entry, timeSpan);
            }
        }

        public static bool ContainsEntry(int entry) {
            DateTime expiry;
            if(EntryBlacklist.TryGetValue(entry, out expiry)) {
                return (expiry > DateTime.Now);
            }
            return false;
        }

        public static bool ContainsEntry(NpcResult npcResult) { return (npcResult != null) && ContainsEntry(npcResult.Entry); }

        public static void RemoveExpiredEntry() {
            if(!_sweepTimer.IsFinished) {
                return;
            }

            var now = DateTime.Now;
            var expiredEntries = (from key in EntryBlacklist.Keys where (EntryBlacklist[key] < now) select key).ToList();

            foreach(var entry in expiredEntries) {
                EntryBlacklist.Remove(entry);
            }

            _sweepTimer.Reset();
        }

        public static void RemoveAllEntries() {
            var everything = (from key in EntryBlacklist.Keys select key).ToList();

            foreach(var entry in everything) {
                EntryBlacklist.Remove(entry);
            }
        }

        // Name
        public static void AddName(string name, TimeSpan timeSpan) {
            RemoveExpiredName();
            NameBlacklist[name] = DateTime.Now.Add(timeSpan);
        }

        public static bool ContainsName(string name) {
            DateTime expiry;
            if(NameBlacklist.TryGetValue(name, out expiry)) {
                return (expiry > DateTime.Now);
            }
            return false;
        }

        public static void RemoveExpiredName() {
            if(!_sweepTimer.IsFinished) {
                return;
            }

            var now = DateTime.Now;
            var expiredNames = (from key in NameBlacklist.Keys where (NameBlacklist[key] < now) select key).ToList();

            foreach(var name in expiredNames) {
                NameBlacklist.Remove(name);
            }

            _sweepTimer.Reset();
        }

        public static void RemoveAllNames() {
            var everything = (from key in NameBlacklist.Keys select key).ToList();

            foreach(var name in everything) {
                NameBlacklist.Remove(name);
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}