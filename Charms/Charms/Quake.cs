using System;
using System.Collections.Generic;
using System.Text;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CharmCrab.Charms {
	class Quake {
		public int Damage() {
			int basedmg = 0;
			switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
				case 0: basedmg = 12; break;
				case 1: basedmg = 16; break;
				case 2: basedmg = 21; break;
				case 3: basedmg = 27; break;
				case 4: basedmg = 35; break;
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
