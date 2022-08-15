using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Modding;
using HutongGames;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;
using Vasi;

namespace CharmCrab.Charms {
	class GlowingWomb{
		public readonly float HATCH_TIME_NULL = 600;
		public readonly float HATCH_TIME_MAX = 60;
		public readonly float HATCH_TIME_MIN = 1;

		public readonly int SPAWN_MAX = 15;
		public readonly int SPAWN_MIN = 5;
		public readonly int SPAWN_NULL = 1;

		private GameObject CharmEffectsVanilla;
		private PlayMakerFSM fsm;

		public GlowingWomb() {
			if (CharmEffectsVanilla == null) {
				CharmEffectsVanilla = HeroController.instance.gameObject.transform.Find("Charm Effects").gameObject;
			}
			this.fsm = FSMUtility.LocateFSM(CharmEffectsVanilla, "Hatchling Spawn");
			this.fsm.FsmVariables.FindFsmInt("Soul Cost").RawValue = 15;
			this.fsm.FsmVariables.FindFsmInt("Hatchling Max").RawValue = 15;
			this.fsm.FsmVariables.FindFsmFloat("Hatch Time").Value = 1f;
		}

		public void Update() {

		}
	}
}
