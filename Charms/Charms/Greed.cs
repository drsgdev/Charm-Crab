using UnityEngine;

namespace CharmCrab.Charms {
	class Greed {
		private const float COOLDOWN = 1.5f;
		private const float ACTIVE = 0.5F;
		private float coolDown = 0;
		private float activeTime = 0;

		private float shadeDisable = 0;

		public void Update() {
			this.coolDown = Mathf.Max(this.coolDown - Time.deltaTime, 0);
			this.activeTime = Mathf.Max(this.activeTime - Time.deltaTime, 0);

			if (PlayerData.instance.soulLimited) {
				this.shadeDisable = 10;
			} else {
				this.shadeDisable = Mathf.Max(this.shadeDisable - Time.deltaTime, 0);
			}
			
		}

		public int GeoBonus(int value) {
			if (GameManager.instance.playerData.GetBool("equippedCharm_24") && !GameManager.instance.playerData.GetBool("brokenCharm_24")) {
				if (this.shadeDisable > 0) {
					return value;
				} else {
					return value * 3;
				}
			} else {
				return value;
			}
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
