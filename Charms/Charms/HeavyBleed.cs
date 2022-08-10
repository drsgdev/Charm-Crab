using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Modding;

namespace CharmCrab.Charms {
	class HeavyBleed {
		public readonly float DURATION = 5;

		private Dictionary<HealthManager, float> Duration = new Dictionary<HealthManager, float>();
		private Dictionary<HealthManager, uint> Stacks = new Dictionary<HealthManager, uint>();

		public void Start() { }

		public void Update() {
			List<HealthManager> rem = new List<HealthManager>();

			foreach (var k in Duration) {
				var t = k.Value + Time.deltaTime;
				if (t < DURATION) {
					// Check to see if a full second has passed.
					if ((int)k.Value < (int)t) {
						var hit = this.GenHit(Stacks[k.Key]);
						k.Key.Hit(hit);
					}
					Duration[k.Key] = t;
				} else {
					rem.Add(k.Key);
				}
			}

			foreach (var h in rem) {
				Duration.Remove(h);
				Stacks.Remove(h);
			}
		}

		public void Clear() {
			this.Duration.Clear();
			this.Stacks.Clear();
		}

		public void SlashHitHandler(Collider2D col, GameObject slash) {

			if (PlayerData.instance.GetBool("equippedCharm_15")) {
				var h = col.gameObject.GetComponent<HealthManager>();
				if (h != null) {
					if (Stacks.ContainsKey(h)) {
						Stacks[h] += 1;
					} else {
						Stacks[h] = 1;
					}
					Duration[h] = 0;
				}
			}
		}

		private HitInstance GenHit(uint stacks) {
			int dmg = ((int)stacks) * (1 + PlayerData.instance.nailSmithUpgrades);
			var hit = new HitInstance()
			{
				DamageDealt = dmg,
				AttackType = AttackTypes.Spell,
				Direction = 0,
				CircleDirection = false,
				Source = HeroController.instance.gameObject,
				Multiplier = 1.0f,
				IgnoreInvulnerable = true,
			};

			return hit;
		}
	}
}
