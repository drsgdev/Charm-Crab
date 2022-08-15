using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Charms {
	class Weaverlings : MonoBehaviour {
		public void OnTriggerEnter2D(Collider2D col) {
			var h = col.gameObject.GetComponent<HealthManager>();
			if (h) {
				var d = col.gameObject.GetComponent<Debuffs>();
				if (d == null) {
					d = col.gameObject.AddComponent<Debuffs>();
				}
				d.StackBleed();
			}
		}
	}
}
