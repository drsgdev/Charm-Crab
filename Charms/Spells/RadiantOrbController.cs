using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace CharmCrab.Spells {
	

	class RadiantOrbController: MonoBehaviour {
		// The objects we need to regularly apply damage to.
		private readonly Dictionary<HealthManager, float> attacked = new Dictionary<HealthManager, float>();
		private readonly float LifeTime = 15;
		// Cooldown of amount of times it can do damage when multi-hitting.
		private readonly float Cooldown = 1;
		//private readonly float rotation = 0.4f;
		//private readonly float speed = 1f;

		// Animator to keep track of.
		private Animator anim;
		private float life;
		private float radius;


		public void Start() {
			this.anim = this.GetComponent<Animator>();
			this.life = 0;
			this.radius = this.GetComponent<CircleCollider2D>().radius;
			//var a = Instantiate(Assets.EnemyDetect);
			//a.transform.position = this.transform.position;
			//this.detect = a.AddComponent<DetectEnemy>();
		}

		public void Update() {
			var v1 = GameManager.instance.gameSettings.masterVolume / 10.0f;
			var v2 = GameManager.instance.gameSettings.soundVolume / 10.0f;
			var v3 = v1 * v2;

			this.life += Time.deltaTime;
			if (this.IsActive()) {
				//this.detect.transform.position = this.transform.position;
				if (GameManager.instance.isPaused) {
					this.life += Time.deltaTime;
					this.HandleMovement();
					foreach (var a in this.GetComponents<AudioSource>()) {
						a.Pause();
					}
				} else {
					foreach (var a in this.GetComponents<AudioSource>()) {
						a.UnPause();
						a.volume = v3;
					}
				}
				foreach (var a in this.attacked) {
					if (a.Value >= Cooldown) {
						this.ApplyDamage(a.Key);
						this.attacked[a.Key] = 0;
                    } else {
						this.attacked[a.Key] += Time.deltaTime;
                    }
                }
			} else { 
				//Destroy(this.transform.parent.gameObject);
				Destroy(this);
			}
		}

		private void HandleMovement() {
			/*
			var obj = this.detect.FindClosest(this.transform.position);

			if (obj != null) {
				var newpos = Time.deltaTime * speed * (obj.transform.position - this.transform.position) + this.transform.position;
				this.transform.position = newpos;
			}
			*/
        }

		private bool IsActive() {
			return this.life < LifeTime;
		}

		private void ApplyDamage(HealthManager hm) {
			hm.Hit(this.GenHit());
		}

		public void OnTriggerEnter2D(Collider2D col) {
			var go = col.gameObject;
			var hm = go.GetComponent<HealthManager>();

			if (hm && !this.attacked.Keys.Contains(hm)) {
				this.attacked.Add(hm, Cooldown + 1);
			}			
		}

		public void OnTriggerExit2D(Collider2D col) {
			var go = col.gameObject;
			var hm = go.GetComponent<HealthManager>();

			if (hm && this.attacked.Keys.Contains(hm)) {
				this.attacked.Remove(hm);
			}
		}

		

		private HitInstance GenHit() {
			int basedmg;
			switch (PlayerData.instance.nailSmithUpgrades) {
				case 0: basedmg = 1; break;
				case 1: basedmg = 2; break;
				case 2: basedmg = 3; break;
				case 4: basedmg = 7; break;
				default: basedmg = 0; break;
			}
						
			if (PlayerData.instance.GetBool("equippedCharm_33")) {
				basedmg = Mathf.Max(1, basedmg / 2);

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
