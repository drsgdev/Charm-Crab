using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace CharmCrab.Charms {
	class Weaverlings : MonoBehaviour {
		private List<GameObject> cds = new List<GameObject>();
		//private Dictionary<GameObject, float> cds = new Dictionary<GameObject, float>();
		public void OnTriggerEnter2D(Collider2D col) { 
			if (!this.cds.Contains(col.gameObject)) {
				this.cds.Add(col.gameObject);
				var h = col.gameObject.GetComponent<HealthManager>();
				if (h && !h.IsInvincible) {
					var d = col.gameObject.GetComponent<Debuffs>();
					if (d == null) {
						d = col.gameObject.AddComponent<Debuffs>();
					}
					d.StackBleed();
				}
			}			
		}

		public void OnTriggerExit2D(Collider2D col) {
			this.cds.Remove(col.gameObject);
		}
	}
}
