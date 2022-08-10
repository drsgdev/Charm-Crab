using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class SteadyBody {
		public int TakeDamage(ref int hazard, int dmg) {
			if (PlayerData.instance.GetBool("equippedCharm_14")) {
				return Math.Max(1, dmg - 1);
			} else {
				return dmg;
			}
		}
	}
}
