using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab.Charms {
	class BaldurShell {
		private GameObject blocker;
		private PlayMakerFSM fsm;


		public BaldurShell() {
			
		}

		public void Update() {
			if (this.blocker == null) {
				var effects = HeroController.instance.gameObject.transform.Find("Charm Effects");
				this.blocker = effects.gameObject.transform.Find("Blocker Shield").gameObject;
				this.fsm = FSMUtility.LocateFSM(this.blocker, "Control");
				fsm.FsmVariables.IntVariables[1].RawValue = 0;
				//fsm.FsmVariables.IntVariables[0].RawValue = 1;
			}
		}
	}
}
