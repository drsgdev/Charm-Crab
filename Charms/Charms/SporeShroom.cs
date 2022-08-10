using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using CharmCrab.Utils;
using HutongGames.PlayMaker.Actions;

namespace CharmCrab.Charms {
	class SporeShroom {
		public static GameObject cloudPrefab;
		public static GameObject dungPrefab;

		private GameObject spores;
		private GameObject dung;
		
		public SporeShroom() {
			cloudPrefab = Functions.GetAction<SpawnObjectFromGlobalPool>(
				HeroController.instance.spellControl,
				"Spore Cloud",
				3)
			.gameObject.Value;

			dungPrefab = Functions.GetAction<SpawnObjectFromGlobalPool>(
				HeroController.instance.spellControl,
				"Dung Cloud",
				0)
			.gameObject.Value;
		}

		public void Update() {
			if (PlayerData.instance.GetBool("equippedCharm_17")) {
				if (PlayerData.instance.GetBool("equippedCharm_10")) {
					if (this.dung == null) {
						this.dung = GameObject.Instantiate(dungPrefab);
						this.dung.transform.position = HeroController.instance.gameObject.transform.position;
						GameObject.Destroy(this.spores);
					}
				} else {
					if (this.spores == null) {
						this.spores = GameObject.Instantiate(cloudPrefab);
						this.spores.transform.position = HeroController.instance.gameObject.transform.position;
						GameObject.Destroy(this.dung);
					}
				}

			} else {
				GameObject.Destroy(this.dung);
				GameObject.Destroy(this.spores);
			}

		}

	}
}
