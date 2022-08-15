using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Vasi;

namespace CharmCrab.Charms {
	class Kingsoul {
		public readonly int SoulCharge = 1;
		public readonly int VoidCharge = 2;
		public readonly float TickTime = 4;
		private PlayMakerFSM fsm;
		private float time = 0;
		public void Update() {
			this.time += Time.deltaTime;
			if (this.fsm == null) {
				var effects = HeroController.instance.gameObject.transform.Find("Charm Effects").gameObject;
				this.fsm = FSMUtility.LocateFSM(effects, "White Charm");
				FsmUtil.RemoveTransition(FsmUtil.GetState(this.fsm, "Check"), "Active");
			} else {
				if (PlayerData.instance.GetBool("equippedCharm_36")) {
					if (this.time >= TickTime) {
						this.time = 0;
						if (PlayerData.instance.gotShadeCharm) {
							HeroController.instance.AddMPCharge(VoidCharge);
						} else {
							HeroController.instance.AddMPCharge(SoulCharge);
						}
					} else {

					}
				} else {
					this.time = 0;
				}
			}

			
		}
	}

	
}
