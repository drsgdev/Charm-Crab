using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Vasi;

namespace CharmCrab.Charms {
	class Kingsoul {
		public static readonly int SoulCharge = 1;
		public static readonly int VoidCharge = 2;
		public static readonly float TickTime = 4;
		private PlayMakerFSM fsm;
		private Cooldown cd = new Cooldown(TickTime);

		public void Update() {
			this.cd.Update();
			if (this.fsm == null) {
				var effects = HeroController.instance.gameObject.transform.Find("Charm Effects").gameObject;
				this.fsm = FSMUtility.LocateFSM(effects, "White Charm");
				FsmUtil.RemoveTransition(FsmUtil.GetState(this.fsm, "Check"), "Active");
			} else {
				if (PlayerData.instance.GetBool("equippedCharm_36")) {
					if (this.cd.Available) {
						this.cd.Reset();
						if (PlayerData.instance.gotShadeCharm) {
							HeroController.instance.AddMPCharge(VoidCharge);
						} else {
							HeroController.instance.AddMPCharge(SoulCharge);
						}
					} else {

					}
				} else {
					this.cd.Reset();
				}
			}			
		}
	}

	
}
