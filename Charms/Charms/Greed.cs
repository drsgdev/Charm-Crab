using UnityEngine;

namespace CharmCrab.Charms {
	class Greed {
		private const float COOLDOWN = 1.5f;
		private const float ACTIVE = 0.5F;
		private float coolDown = 0;
		private float activeTime = 0;


		public void Update() {
			this.coolDown = Mathf.Max(this.coolDown - Time.deltaTime, 0);
			this.activeTime = Mathf.Max(this.activeTime - Time.deltaTime, 0);
		}


		public int DamageBonus() {
			if (GameManager.instance.playerData.GetBool("equippedCharm_24") && !GameManager.instance.playerData.GetBool("brokenCharm_24")) {
				if (this.activeTime > 0) {
					int baseDMG = CharmEffects.BaseNailDamage();
					int bonus = Mathf.Min(baseDMG, PlayerData.instance.GetInt("geo"));
					return bonus;
				} else if (this.coolDown <= 0) {
					this.coolDown = COOLDOWN;
					this.activeTime = ACTIVE;

					int baseDMG = CharmEffects.BaseNailDamage();
					int bonus = Mathf.Min(baseDMG, PlayerData.instance.GetInt("geo"));

					if (bonus > 0) {
						HeroController.instance.TakeGeo(bonus);
					}

					return bonus;
				}
			}

			return 0;
		}
	}
}
