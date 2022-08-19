using System;

namespace CharmCrab.Charms {
	class StalwartShell {
		public int TakeDamage(ref int hazard, int dmg) {
			if (PlayerData.instance.GetBool("equippedCharm_4")) {
				return Math.Max(1, dmg - 1);
			} else {
				return dmg;
			}
		}
	}		
}
