using System;
using System.Collections.Generic;
using System.Text;
using Vasi;

namespace CharmCrab.Charms {
	class InfusedBlade {
		public readonly float SoulMax = 99;
		public readonly float MultMax = 2;

		private bool active = false;
		private int mpTaken = 0;

		public InfusedBlade() {
			var fsm = FSMUtility.LocateFSM(HeroController.instance.gameObject, "Nail Arts");

			FsmUtil.InsertMethod(FsmUtil.GetState(fsm, "Flash"), 0, this.Activate);
			FsmUtil.InsertMethod(FsmUtil.GetState(fsm, "Flash 2"), 0, this.Activate);
			FsmUtil.InsertMethod(FsmUtil.GetState(fsm, "DSlash Start"), 0, this.Activate);

			FsmUtil.InsertMethod(FsmUtil.GetState(fsm, "Regain Control"), 0, this.Deactivate);
			FsmUtil.InsertMethod(FsmUtil.GetState(fsm, "Cancel All"), 0, this.Deactivate);
		}

		private void Activate() {
			if (CharmCrab.Settings.Equipped(NewCharms.SoulInfusedBlade)) {
				this.active = true;
				this.mpTaken = PlayerData.instance.GetInt("MPCharge");
				HeroController.instance.TakeMP((int)Math.Min(SoulMax, this.mpTaken));
			}
		}

		private void Deactivate() {
			this.active = false;
			this.mpTaken = 0;
		}

		public int AddDamage(int i) {
			if (CharmCrab.Settings.Equipped(NewCharms.SoulInfusedBlade) && this.active) {
				var ratio = mpTaken / SoulMax;
				var baseDmg = CharmEffects.BaseNailDamage();

				return baseDmg * (1 + (int)(MultMax * ratio));

			} else {
				return i;
			}
		}
	}
}
