using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Modding;
using HutongGames;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;
using CharmCrab.Utils;

namespace CharmCrab.Charms {
	class GlowingWomb : Spell {
		private static GameObject CharmEffectsVanilla;
		private GameObject babyPrefab;

		public GlowingWomb() {
			if (CharmEffectsVanilla == null) {
				CharmEffectsVanilla = HeroController.instance.gameObject.transform.Find("Charm Effects").gameObject;
			}
			this.babyPrefab = Functions.GetAction<SpawnObjectFromGlobalPool>(
				FSMUtility.LocateFSM(CharmEffectsVanilla, "Hatchling Spawn"), 
				"Hatch", 
				2)
			.gameObject.Value;
		}

		private int SpawnCount() {
			if (PlayerData.instance.GetBool("equippedCharm_19")) {
				return 15;
			}
			if (PlayerData.instance.GetBool("equippedCharm_33")) {
				return 1;
			}		

			return 4;
		}

		public GameObject Spawn() {
			for (int i = 0; i < SpawnCount(); ++i) {
				var baby = GameObject.Instantiate(this.babyPrefab);
				baby.transform.position = HeroController.instance.gameObject.transform.position;
			}
			
			return babyPrefab;
		}
	}
}
