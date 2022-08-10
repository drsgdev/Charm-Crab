using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Charms {
	class SharpShadow {
		public readonly float CooldownMax = 1;
		public readonly int MPCharge = 2;
		private float cooldown = -1;

		public void Update() {
			if (cooldown > 0) {
				this.cooldown -= Time.deltaTime;
			}
		}

		public int Damage() {
			if (this.cooldown <= 0) {
				HeroController.instance.AddMPCharge(MPCharge);
			}

			int basedmg = 0;
			switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
				case 0: basedmg = 5; break;
				case 1: basedmg = 7; break;
				case 2: basedmg = 10; break;
				case 3: basedmg = 12; break;
				case 4: basedmg = 15; break;
				default: basedmg = 0; break;
			}
			return basedmg;
		}
	}
}
