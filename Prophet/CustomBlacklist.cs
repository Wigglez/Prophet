using System;
using System.Collections.Generic;
using System.Linq;
using Styx.Common.Helpers;

namespace Prophet {
    public class CustomBlacklist {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

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
            var maxSweepTime = TimeSpan.FromSeconds(30);

            _sweepTimer = new WaitTimer(maxSweepTime) {WaitTime = maxSweepTime};
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