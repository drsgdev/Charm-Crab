using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Modding;

namespace CharmCrab.Charms {
	class HeavyBleed {

		public void Update() {

		}

		public void SlashHitHandler(Collider2D col, GameObject slash) {
			if (CharmData.Equipped(Charm.HeavyBlow)) {

				var h = col.gameObject.GetComponent<HealthManager>();
				if (h) {
					var d = Functions.AddIfNeeded<Debuffs>(col.gameObject);
					d.StackBleed();
				}
			}
		}
	}
}
