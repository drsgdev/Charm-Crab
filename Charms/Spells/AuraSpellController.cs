using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Spells {
	class AuraSpellController: MonoBehaviour {
		public static AuraSpellController instance;

		public static float RecentSoulUse = 0;
		public readonly float MAX_LIFE = 60;
		// The objects we need to regularly apply damage to.
		private readonly HashSet<HealthManager> attacking = new HashSet<HealthManager>();
		private Functions.HitDetectManager hit;
		// Animator to keep track of.
		private Animator anim;

		private float duration;
		private float lifetime;

		public void Start() {
			instance = this;
			this.duration = 0;
			this.lifetime = RecentSoulUse / 99 * MAX_LIFE;

			if (CharmData.Equipped(Charm.SpellTwister)) {
				this.lifetime *= 2;
			}

			this.anim = this.GetComponent<Animator>();
			this.hit = new Functions.HitDetectManager(1f);
			//this.transform.parent = HeroController.instance.gameObject.transform;
		}
		public void Update() {
			this.transform.position = HeroController.instance.gameObject.transform.position - 0.5f * Vector3.up;
			this.duration += Time.deltaTime;
			//var v3 = v1 * v2;
			if (this.IsActive()) {
				if (!GameManager.instance.isPaused) {
					this.ApplyDamage();
					this.hit.Update();
				}				
			} else {
				//Destroy(this.transform.parent.gameObject);
				Destroy(this.gameObject);
			}
		}


		public bool IsActive() {
			return this.duration < this.lifetime;
		}

		private void ApplyDamage() {
			foreach (var hm in this.attacking) {
				if (!this.hit.Hit(hm.gameObject)) {
					var hit = this.GenHit();
					hm.Hit(hit);
					FSMUtility.SendEventToGameObject(hm.gameObject, "TAKE DAMAGE", false);

					var d = hm.gameObject.GetComponent<Charms.Debuffs>();

					if (d) {
						d.SoulCatch(hit.DamageDealt);
					}

					this.hit.Register(hm.gameObject);
				}

			}
		}

		public void OnTriggerEnter2D(Collider2D col) {
			var go = col.gameObject;
			var hm = go.GetComponent<HealthManager>();

			if (hm) {
				this.attacking.Add(hm);
			}
		}

		public void OnTriggerExit2D(Collider2D col) {
			var go = col.gameObject;
			var hm = go.GetComponent<HealthManager>();

			if (hm) {
				this.attacking.Remove(hm);
			}
		}



		private HitInstance GenHit() {
			int basedmg;
			switch (PlayerData.instance.nailSmithUpgrades) {
				case 0: basedmg = 5; break;
				case 1: basedmg = 10; break;
				case 2: basedmg = 25; break;
				case 4: basedmg = 20; break;
				default: basedmg = 0; break;
			}

			if (CharmData.Equipped(Charm.ShamanStone)) {
				basedmg *= 2;
			}

			var hit = new HitInstance() {
				DamageDealt = basedmg,
				AttackType = AttackTypes.Spell,
				Direction = 90.0f,
				CircleDirection = false,
				Source = this.gameObject,
				Multiplier = 1.0f,
			};
			return hit;
		}
	}
}

