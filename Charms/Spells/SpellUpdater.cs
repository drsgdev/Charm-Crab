using System;
using Modding;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;
using CharmCrab.Utils;
using CharmCrab;
using UnityEngine;
using Vasi;

namespace CharmCrab.Spells {
	class SpellUpdater {
		private static FireballSwitch Fball;
		private static ShriekSwitch Shriek;

		public static void Init() {
			Fball = new FireballSwitch();
			Shriek = new ShriekSwitch();
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

			public ShriekSwitch() {
				var switchstate = FsmUtil.GetState(HeroController.instance.spellControl, "Level Check 3");

				FsmUtil.RemoveAction(switchstate, 0);

				FsmUtil.AddMethod(switchstate, this.Branch);

			}
			public void Branch() {

				if (PlayerData.instance.GetInt("screamLevel") == 1) {
					HeroController.instance.spellControl.SendEvent("LEVEL 1");
				} else if (PlayerData.instance.GetInt("screamLevel") == 2) {
					HeroController.instance.spellControl.SendEvent("LEVEL 2");
				}
			}
		}

	}
}
