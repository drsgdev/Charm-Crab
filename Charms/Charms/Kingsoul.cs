using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using CharmCrab.Utils;

namespace CharmCrab.Charms {
	class Kingsoul {
		public readonly float TickTime = 4;
		private PlayMakerFSM fsm;
		private float time = 0;
		public void Update() {
			this.time += Time.deltaTime;
			if (this.fsm == null) {
				var effects = HeroController.instance.gameObject.transform.Find("Charm Effects").gameObject;
				this.fsm = FSMUtility.LocateFSM(effects, "White Charm");
				Functions.RemoveTransition(this.fsm, "Check", "Wait");
			} else {
				if (PlayerData.instance.GetBool("equippedCharm_36")) {
					if (this.time >= TickTime) {
						this.time = 0;
						if (PlayerData.instance.gotShadeCharm) {
							HeroController.instance.AddMPCharge(4);
						} else {
							HeroController.instance.AddMPCharge(2);
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
