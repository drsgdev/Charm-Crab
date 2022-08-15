using System;
using System.Collections.Generic;
using System.Text;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace CharmCrab.Charms {
	class Wraith {
		public int Damage() {
			int basedmg = 0;
			if (PlayerData.instance.GetInt("screamLevel") == 1) {
				switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
					case 0: basedmg = 6; break;
					case 1: basedmg = 8; break;
					case 2: basedmg = 10; break;
					case 3: basedmg = 14; break;
					case 4: basedmg = 18; break;
					default: basedmg = 0; break;
				}
			} else if (PlayerData.instance.GetInt("screamLevel") == 2) {
				switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
					case 0: basedmg = 12; break;
					case 1: basedmg = 16; break;
					case 2: basedmg = 21; break;
					case 3: basedmg = 27; break;
					case 4: basedmg = 35; break;
					default: basedmg = 0; break;
				}
			}

			return basedmg;
		}
	}
}
