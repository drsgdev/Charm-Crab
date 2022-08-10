using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Charms {

	class MarkOfPride {
		private readonly float ResetTime = 5;
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

		public int DmgBonus {
			get {
				if (PlayerData.instance.GetBool("equippedCharm_13")) {
					return this.stacks;
					/*
					int dmg = 1 + PlayerData.instance.nailSmithUpgrades;
					int mult = this.stacks / 5;

					return dmg * mult;
					*/
				}
				return 0;
			}
		}

		public void TakeDamage(int i) {
			if (i > 0) {
				this.stacks = 0;
				this.timeSinceLast = 0;
			}
		}

		public void ApplyStack() {
			stacks =+ 1;
			timeSinceLast = 0;
		}


	}
}
