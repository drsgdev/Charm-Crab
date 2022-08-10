using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class NailMaster {
		public float Mult(DamageType n) {
			if (PlayerData.instance.GetBool("equippedCharm_26")) {
				switch (n) {
					case DamageType.Slash: return 1.2f;
					case DamageType.GreatSlash: return 3;
					case DamageType.Cyclone: return 1.2f;
					case DamageType.DashSlash: return 2;
					default: return 1;
				}				
			} else {
				switch (n) {
					case DamageType.Slash: return 1f;
					case DamageType.GreatSlash: return 2;
					case DamageType.Cyclone: return 0.8f;
					case DamageType.DashSlash: return 1.5f;
					default: return 1;
				}

			}
		}
	}
}
