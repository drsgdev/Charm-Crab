using System.Collections.Generic;
using UnityEngine;
using Vasi;
using HutongGames.PlayMaker.Actions;

namespace CharmCrab.Charms {
	class BaldurShell {
		public static GameObject blocker;
		public static PlayMakerFSM fsm;

		public static readonly HashSet<string> ActiveStates = new HashSet<string>() {
			"Hits Left?",
			"Shell Up",
			"Focusing",
		};

		public static void OpenBlocker() {
			if (!CharmData.Equipped(Charm.BaldurShell)) { return; }
			if (fsm != null) {
				fsm.SendEvent("FOCUS START");
			}
			
		}

		public static bool Active() {
			return CharmData.Equipped(Charm.BaldurShell) && ActiveStates.Contains(fsm.ActiveStateName);
		}


		public static void CloseBlocker() {
			if (!CharmData.Equipped(Charm.BaldurShell)) { return; }
			if (fsm != null) {
				fsm.SendEvent("FOCUS END");
			}
			
		}

		public BaldurShell() {
			this.Init();
		}

		private void Init() {
			if (blocker == null) {
				var effects = HeroController.instance.gameObject.transform.Find("Charm Effects");
				blocker = effects.gameObject.transform.Find("Blocker Shield").gameObject;
				fsm = FSMUtility.LocateFSM(blocker, "Control");
				var hitState = FsmUtil.GetState(fsm, "Blocker Hit");

				//FsmUtil.ChangeTransition(hitState, "2", "HUD 1");
				//FsmUtil.ChangeTransition(hitState, "3", "HUD 1");
				//FsmUtil.RemoveAction<IntCompare>(hitState);
				//FsmUtil.AddMethod(hitState, () => { if (PlayerData.instance.blockerHits == 1) { PlayerData.instance.blockerHits += 1; } } );

				
			}
		}

		public void Update() {
			
		}
	}
}
