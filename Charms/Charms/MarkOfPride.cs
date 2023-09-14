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

		public int Stacks {
			get { return stacks; }
		}

		public void Start() { }

		public void Update() {
			timeSinceLast += Time.deltaTime;
			if (timeSinceLast > ResetTime) {
				stacks = 0;
			}
		}

		private void ApplyStack() {
			stacks =+ 1;
			timeSinceLast = 0;
		}

		public int DamageBonus(int i) {
				if (PlayerData.instance.GetBool("equippedCharm_13")) {
					int damageBonus = i + DamageScale * stacks * (2 + PlayerData.instance.GetInt("nailSmithUpgrades"));
					ApplyStack();

					return damageBonus;
				}
				return i;
		}

		public void TakeDamage(int i) {
			if (i > 0) {
				stacks = 0;
				timeSinceLast = 0;
			}
		}
	}
}
