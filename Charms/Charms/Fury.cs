using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class Fury {
		public readonly float MaxMult = 1.5f;
		public readonly float MultPerMask = 0.25f;
		public float Mult {
			get {
				if (PlayerData.instance.GetBool("equippedCharm_6")) {
					int health = PlayerData.instance.health;
					int max = PlayerData.instance.maxHealth;
					float prop = 1 - (float)health / ((float)max);

					int diff = PlayerData.instance.GetInt("maxHealth") - PlayerData.instance.GetInt("health");

					return 1 + MultPerMask *diff;
				}
				return 1;
			}
		}
	}
}
