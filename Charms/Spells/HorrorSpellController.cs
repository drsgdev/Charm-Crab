using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace CharmCrab.Spells {
	

	class HorrorSpellController: MonoBehaviour {
		private static HitInstance Hit = new HitInstance() {
			DamageDealt=10,
		};
		// The objects we need to regularly apply damage to.
		private readonly HashSet<HealthManager> attacking = new HashSet<HealthManager>();
		// Animator to keep track of.
		private Animator anim;
		public void Start() {
			this.anim = this.GetComponent<Animator>();
		}
		public void Update() {
			if (this.IsActive()) {
				var v1 = GameManager.instance.gameSettings.masterVolume / 10.0f;
				var v2 = GameManager.instance.gameSettings.soundVolume / 10.0f;
				var v3 = v1 * v2;
				if (GameManager.instance.isPaused) {
					foreach (var a in this.GetComponents<AudioSource>()) {
						a.Pause();
					}
				} else {
					foreach (var a in this.GetComponents<AudioSource>()) {
						a.UnPause();
						a.volume = v3;
					}
					this.ApplyDamage();
				}
			} else { 
				Destroy(this.gameObject);
			}
		}


		private bool IsActive() {
			return !(this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !anim.IsInTransition(0));
		}

		private void ApplyDamage() {
			foreach (var hm in this.attacking) {
				var hit = this.GenHit();
				hm.Hit(hit);
				FSMUtility.SendEventToGameObject(hm.gameObject, "TAKE DAMAGE", false);
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
			float dir = HeroController.instance.cState.facingRight ? 0f : 180f;

			

			var hit = new HitInstance() {
				DamageDealt = 4,
				AttackType = AttackTypes.Spell,
				Direction = -dir,
				CircleDirection = false,
				Source = this.gameObject,
				Multiplier = 1.0f,
			};
			return hit;
		}
	}
}
