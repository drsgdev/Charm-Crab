using System;
using System.Collections.Generic;
using System.Text;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Vasi;

namespace CharmCrab.Charms {
	class Fireball {
		public int Damage() {
			int basedmg = 0;
			if (PlayerData.instance.GetInt("fireballLevel") == 1) {
				switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
					case 0: basedmg = 15; break;
					case 1: basedmg = 22; break;
					case 2: basedmg = 29; break;
					case 3: basedmg = 34; break;
					case 4: basedmg = 38; break;
					default: basedmg = 0; break;
				}
			} else if (PlayerData.instance.GetInt("fireballLevel") == 2) {
				switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
					case 0: basedmg = 30; break;
					case 1: basedmg = 45; break;
					case 2: basedmg = 59; break;
					case 3: basedmg = 67; break;
					case 4: basedmg = 75; break;
					default: basedmg = 0; break;
				}
			}
	
			return basedmg;
		}
	}
}
