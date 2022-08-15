using System;
using Modding;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;
using CharmCrab;
using UnityEngine;
using Vasi;

namespace CharmCrab.Spells {
	class SpellUpdater {
		private static FireballSwitch Fball;
		private static ShriekSwitch Shriek;

		public static void Init() {			

			if (Fball == null) {
				Fball = new FireballSwitch();
			}

			if (Shriek == null) {
				Shriek = new ShriekSwitch();
			}

			//var coststate = FsmUtil.GetState(HeroController.instance.spellControl, "Inactive");
			//FsmUtil.InsertMethod(coststate, 0, UpdateSpellCosts);
		}

		public static void UpdateSpellCosts() {
			if (HeroController.instance == null) { return; }

			var shaman = CharmData.Equipped(Charm.ShamanStone);
			var twister = CharmData.Equipped(Charm.SpellTwister);
			if (shaman) {
				if (twister) {
					// Both shaman and twister
					HeroController.instance.spellControl.FsmVariables.IntVariables[4].RawValue = 50;
				} else {
					// Only shaman
					HeroController.instance.spellControl.FsmVariables.IntVariables[4].RawValue = 99;
				}
			} else {
				if (CharmData.Equipped(Charm.SpellTwister)) {
					// Only twister
					HeroController.instance.spellControl.FsmVariables.IntVariables[4].RawValue = 0;
				} else {
					// Neither shaman nor twister.
					HeroController.instance.spellControl.FsmVariables.IntVariables[4].RawValue = 33;
				}
			}
		}

		private class FireballSwitch: FsmObject {
			
			public FireballSwitch() {
				var switchstate = FsmUtil.GetState(HeroController.instance.spellControl, "Level Check");

				FsmUtil.RemoveAction(switchstate, 0);

				FsmUtil.AddMethod(switchstate, this.Branch);

			}
			public void Branch() {
				
				if (PlayerData.instance.GetInt("fireballLevel") == 1) {					
					HeroController.instance.spellControl.SendEvent("LEVEL 1");
				} else if (PlayerData.instance.GetInt("fireballLevel") == 2) {
					HeroController.instance.spellControl.SendEvent("LEVEL 2");
				}
			}
		}

		private class ShriekSwitch : FsmObject {
			private int tendrilIndex;
			private FsmState switchstate;
			public ShriekSwitch() {
				switchstate = FsmUtil.GetState(HeroController.instance.spellControl, "Level Check 3");

				FsmUtil.RemoveAction(switchstate, 0);
				FsmUtil.AddMethod(switchstate, this.Branch);

				foreach (var item in CharmCrab.NewCharms) {
					if (item.Value.EnumValue == NewCharms.VoidTendrils) {
						tendrilIndex = item.Key;
						break;
					}
				}

				this.CreateTendrilPath();
			}

			private void CreateTendrilPath() {
				var s1 = FsmUtil.CopyState(HeroController.instance.spellControl, "Scream Antic2", "Tendril Antic");
				var s2 = FsmUtil.CopyState(HeroController.instance.spellControl, "Scream Burst 2", "Tendril Summon");
				FsmUtil.AddTransition(switchstate, "TENDRILS", "Tendril Antic");
				FsmUtil.ChangeTransition(s1, "FINISHED", "Tendril Summon");

				FsmUtil.RemoveAction<ActivateGameObject>(s2);
				FsmUtil.RemoveAction<ActivateGameObject>(s2);
				FsmUtil.RemoveAction<CreateObject>(s2);
				FsmUtil.InsertMethod(s2, 0, SpawnTendrils);
			}

			private void Testing() {
				Modding.Logger.Log("This state has been reached");
			}

			private void SpawnTendrils() {
				var obj = GameObject.Instantiate(CharmCrab.Assets.TendrilsPrefab);
				obj.transform.position = HeroController.instance.gameObject.transform.position + 2*Vector3.up;
			}

			private void Branch() {
				if (PlayerData.instance.GetBool("equippedCharm_" + this.tendrilIndex)) {
					if (PlayerData.instance.GetInt("screamLevel") > 0) {
						HeroController.instance.spellControl.SendEvent("TENDRILS");
					}
				} else {
					if (PlayerData.instance.GetInt("screamLevel") == 1) {
						HeroController.instance.spellControl.SendEvent("LEVEL 1");
					} else if (PlayerData.instance.GetInt("screamLevel") == 2) {
						HeroController.instance.spellControl.SendEvent("LEVEL 2");
					}
				}
				
			}
		}

	}
}
