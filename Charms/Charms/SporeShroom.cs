using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HutongGames.PlayMaker.Actions;
using Vasi;

namespace CharmCrab.Charms {
	class SporeShroom {
		public readonly int CLOUD_DMG = 5;
		public static GameObject cloudPrefab;
		public static GameObject dungPrefab;

		private GameObject spores;
		private GameObject dung;


		public SporeShroom() {
			cloudPrefab = FsmUtil.GetAction<SpawnObjectFromGlobalPool>(
				HeroController.instance.spellControl,
				"Spore Cloud",
				3)
			.gameObject.Value;

			dungPrefab = FsmUtil.GetAction<SpawnObjectFromGlobalPool>(
				HeroController.instance.spellControl,
				"Dung Cloud",
				0)
			.gameObject.Value;

			//Modding.Logger.Log("Extra Damage Event = " + cloudPrefab.GetComponent<DamageEffectTicker>().damageEvent);
			GameObject.Destroy(cloudPrefab.GetComponent<DamageEffectTicker>());
			GameObject.Destroy(dungPrefab.GetComponent<DamageEffectTicker>());


			cloudPrefab.AddComponent<CloudTicker>();
			dungPrefab.AddComponent<CloudTicker>();

			FsmUtil.InsertMethod(FsmUtil.GetState(HeroController.instance.spellControl, "Fireball Antic"), 0, this.SpawnCloud);
			FsmUtil.InsertMethod(FsmUtil.GetState(HeroController.instance.spellControl, "Quake Antic"), 0, this.SpawnCloud);
			FsmUtil.InsertMethod(FsmUtil.GetState(HeroController.instance.spellControl, "Level Check 3"), 0, this.SpawnCloud);
		}

		public void SpawnCloud() {
			if (PlayerData.instance.GetBool("equippedCharm_17")) {
				if (PlayerData.instance.GetBool("equippedCharm_10")) {
					if (this.dung == null) {
						this.dung = GameObject.Instantiate(dungPrefab);
						this.dung.transform.position = HeroController.instance.gameObject.transform.position;
						this.dung.GetComponent<CloudTicker>().OnDamage = (a) => {
							var flash = a.GetComponent<SpriteFlash>();
							if (flash != null) {
								flash.flashDungQuick();
							}
							var d = Functions.AddIfNeeded<Debuffs>(a);
							d.Infest();
							d.Infest();
						};
						this.dung.GetComponent<CloudTicker>().DamageAmt = 2;
						GameObject.Destroy(this.spores);
					}
				} else {
					if (this.spores == null) {
						this.spores = GameObject.Instantiate(cloudPrefab);
						this.spores.transform.position = HeroController.instance.gameObject.transform.position;
						this.spores.GetComponent<CloudTicker>().DamageAmt = 1;
						this.spores.GetComponent<CloudTicker>().OnDamage = (a) => {
							var flash = a.GetComponent<SpriteFlash>();
							if (flash != null) {
								flash.flashSporeQuick();
							}
							var d = Functions.AddIfNeeded<Debuffs>(a);
							d.Infest();
						};
						GameObject.Destroy(this.dung);
					}
				}

			} else {
				GameObject.Destroy(this.dung);
				GameObject.Destroy(this.spores);
			}
		}

		public void Update() {			

		}

	}
}
