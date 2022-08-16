using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class InfusedBlade {
		public readonly float SoulMax = 50;
		public readonly float MultMax = 1;

		public int AddDamage(int i) {
			if (CharmCrab.Settings.Equipped(NewCharms.SoulInfusedBlade)) {
				var mpcur = PlayerData.instance.GetInt("MPCharge");
				var ratio = mpcur / SoulMax;
				var baseDmg = CharmEffects.instance.BaseNailDamage();

				HeroController.instance.TakeMP((int) Math.Min(SoulMax, mpcur));

				return baseDmg * (int)(MultMax * ratio);

			} else {
				return i;
			}
		}
	}
}
