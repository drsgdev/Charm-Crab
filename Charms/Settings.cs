using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab {
    public class Settings {
        public Dictionary<NewCharms, Data> CharmData = new Dictionary<NewCharms, Data>() {
            { NewCharms.VoidTendrils, new Data() },
            { NewCharms.PureAura, new Data() },
            { NewCharms.SoulInfusedBlade, new Data() },
            { NewCharms.AfflictedDevourer, new Data() },
        };

        public class Data {
            public bool Obtained = true;
            public bool New = false;
            public bool Equipped = false;
	    }

        public bool Equipped(NewCharms n) {
            Data data;

            if (CharmData.TryGetValue(n, out data)) {
                return data.Equipped;
			} else {
                return false;
			}
		}
    }
}
