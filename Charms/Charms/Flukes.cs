using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Charms {
	class Flukes {
		public readonly float HealDelay = 0.05f;
		public readonly uint HealThreshold = 50;
		private uint stacks = 0;
		private float duration = 0;

		public int Damage() {
			int basedmg = 0;
			if (PlayerData.instance.GetInt("fireballLevel") == 1) {
				switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
					case 0: basedmg = 1; break;
					case 1: basedmg = 1; break;
					case 2: basedmg = 2; break;
					case 3: basedmg = 2; break;
					case 4: basedmg = 3; break;
					default: basedmg = 0; break;
				}
			} else if (PlayerData.instance.GetInt("fireballLevel") == 2) {
				switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
					case 0: basedmg = 2; break;
					case 1: basedmg = 2; break;
					case 2: basedmg = 4; break;
					case 3: basedmg = 4; break;
					case 4: basedmg = 6; break;
					default: basedmg = 0; break;
				}
			}

			return basedmg;
		}

		public void Hit(GameObject ob) {
			this.Stack();
			//this.ApplyCharmEffects(ob);
		}

		public void Update() {
			this.duration -= Time.deltaTime;
		}

		public void Stack() {
			if (this.duration <= 0) {
				this.stacks += 1;
				this.duration = HealDelay;

				if (this.stacks > HealThreshold) {
					HeroController.instance.AddHealth(1);
					this.stacks = 0;
				}
			}
		}
	}
}
