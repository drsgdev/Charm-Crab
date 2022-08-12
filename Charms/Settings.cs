using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab {
    public class Settings {
        public Dictionary<CharmsNew, CharmObtainData> CharmObtained = new Dictionary<CharmsNew, CharmObtainData>() {
            { CharmsNew.VoidTendrils, new CharmObtainData() },
        };

        public class CharmObtainData {
            public bool Obtained = true;
            public bool New = false;
            public bool Equipped = false;
	    }
    }
}
