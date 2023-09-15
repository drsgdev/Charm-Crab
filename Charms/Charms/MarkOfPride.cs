using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Charms {

	class MarkOfPride {
		private readonly int DamageScale = 3;
		private readonly float ResetTime = 30;
		private int stacks = 0;
		private float timeSinceLast = 0;
		public void Start() { }

		public void Update() {
			timeSinceLast += Time.deltaTime;
			if (this.timeSinceLast > ResetTime) {
				this.stacks = 0;
			}
		}

		public int Stacks {
			get { return this.stacks; }
		}

		public int DamageBonus(int i) {
				if (PlayerData.instance.GetBool("equippedCharm_13")) {
					return i + DamageScale * this.stacks * (2 + PlayerData.instance.GetInt("nailSmithUpgrades"));
				}
				return i;
		}

		public void TakeDamage(int i) {
			if (i > 0) {
				this.stacks = 0;
				this.timeSinceLast = 0;
			}
		}

		public void ApplyStack() {
			stacks += 1;
			timeSinceLast = 0;
		}


	}
}
