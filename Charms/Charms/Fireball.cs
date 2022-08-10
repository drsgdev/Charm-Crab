using System;
using System.Collections.Generic;
using System.Text;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CharmCrab.Charms {
	class Fireball {
		private GameObject prefab;

		public Fireball() {
			this.prefab = Utils.Functions.GetAction<SpawnObjectFromGlobalPool>(HeroController.instance.spellControl, "Fireball 1", 3).gameObject.Value;
		}

		public int Damage() {
			int basedmg = 0;
			switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
				case 0: basedmg = 30; break;
				case 1: basedmg = 40; break;
				case 2: basedmg = 59; break;
				case 3: basedmg = 67; break;
				case 4: basedmg = 75; break;
				default: basedmg = 0; break;
			}
			
			// Shaman Stone
			if (PlayerData.instance.GetBool("equippedCharm_19")) {
				basedmg *= 4;
			}

			if (PlayerData.instance.GetBool("equippedCharm_33")) {
				basedmg /= 2;
			}

			return basedmg;
		}
	}
}
