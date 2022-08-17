//using System;
using UnityEngine;

namespace CharmCrab.Charms {
	class StalwartShell {
		public readonly float ProcRate = 0.25f;
		public int TakeDamage(ref int hazard, int dmg) {
			if (CharmData.Equipped(Charm.StalwartShell)) {
				if (Random.Range(0, 1) < ProcRate) {
					return 0;
				}
			}
			return dmg;
		}
	}
}
