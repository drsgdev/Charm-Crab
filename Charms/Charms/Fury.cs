using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class Fury {
		public readonly float MaxMult = 1.5f;
		public float Mult {
			get {
				if (PlayerData.instance.GetBool("equippedCharm_6")) {
					int health = PlayerData.instance.health;
					int max = PlayerData.instance.maxHealth;
					float prop = 1 - (float)health / ((float)max);

					return 1 + prop * MaxMult;
				}
				return 1;
			}
		}
	}
}
