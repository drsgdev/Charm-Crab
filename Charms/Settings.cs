using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab {
    public class Settings {
        public Dictionary<NewCharms, CharmObtainData> CharmObtained = new Dictionary<NewCharms, CharmObtainData>() {
            { NewCharms.VoidTendrils, new CharmObtainData() },
            { NewCharms.ShadeAura, new CharmObtainData() },
            { NewCharms.SoulInfusedBlade, new CharmObtainData() },
        };

        public class CharmObtainData {
            public bool Obtained = false;
            public bool New = false;
            public bool Equipped = false;
	    }
    }
}
