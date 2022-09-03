using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Charms {
	class SpellCollider: MonoBehaviour {
		private static HashSet<GameObject> afflicted = new HashSet<GameObject>();

		public static void Apply(GameObject obj) {
			if (afflicted.Contains(obj)) {
				obj.GetComponent<Debuffs>().SoulEat();
			}
		}

		public static void AddEnemy(GameObject obj) {
			afflicted.Add(obj);
		}

		public  virtual DamageType DType {
			get;
		}

		public static void Remove(GameObject obj) {
			afflicted.Remove(obj);
		}

		public void OnTriggerEnter2D(Collider2D col) {
			var cc = CharmData.Equipped(Charm.SoulCatcher);
			var ce = CharmData.Equipped(Charm.SoulEater);
			if (cc || ce) {
				var h = col.gameObject.GetComponent<HealthManager>();
				if (h) {
					var d = col.gameObject.GetComponent<Debuffs>();
					if (d == null) {
						d = col.gameObject.AddComponent<Debuffs>();
					}
					if (ce) {
						d.SoulEat();
						afflicted.Add(col.gameObject);
					}
					if (cc) {
						d.SoulCatch(CharmEffects.instance.ComputeDamage(this.DType));
					}
				}
			}
		}

		public void OnCollisionEnter2D(Collision2D col) {
			var cc = CharmData.Equipped(Charm.SoulCatcher);
			var ce = CharmData.Equipped(Charm.SoulEater);
			if (cc || ce) {
				var h = col.gameObject.GetComponent<HealthManager>();
				if (h) {
					var d = col.gameObject.GetComponent<Debuffs>();
					if (d == null) {
						d = col.gameObject.AddComponent<Debuffs>();
					}
					if (ce) {
						d.SoulEat();
						afflicted.Add(col.gameObject);
					}
					if (cc) {
						d.SoulCatch(CharmEffects.instance.ComputeDamage(this.DType));
					}
				}
			}
		}

		public void OnCollisionExit2D(Collision2D col) {
			
		}

		public void OnTriggerExit2D(Collider2D col) {

		}
	}
}
