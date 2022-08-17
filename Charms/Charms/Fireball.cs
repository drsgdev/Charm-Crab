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
					case 0: basedmg = 30; break;
					case 1: basedmg = 40; break;
					case 2: basedmg = 50; break;
					case 3: basedmg = 60; break;
					case 4: basedmg = 70; break;
					default: basedmg = 0; break;
				}
			} else if (PlayerData.instance.GetInt("fireballLevel") == 2) {
				switch (PlayerData.instance.GetInt("nailSmithUpgrades")) {
					case 0: basedmg = 50; break;
					case 1: basedmg = 65; break;
					case 2: basedmg = 80; break;
					case 3: basedmg = 90; break;
					case 4: basedmg = 100; break;
					default: basedmg = 0; break;
				}
			}
	
			return basedmg;
		}
	}
}
