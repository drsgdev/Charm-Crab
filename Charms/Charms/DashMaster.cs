using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class DashMaster {
		public void Update() {
			if (PlayerData.instance.GetBool("equippedCharm_31")) {
				HeroController.instance.SHADOW_DASH_COOLDOWN = 0.1f;
			} else {
				HeroController.instance.SHADOW_DASH_COOLDOWN = 1.5f;
			}
		}
	}
}
