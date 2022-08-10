using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CharmCrab.Utils {
	class Cooldown {
		private Action a;
		private float cooldown;
		private float last;
		private Stopwatch s;
		public Cooldown(float t, Action a) {
			this.cooldown = t;
			this.a = a;
			this.s = new Stopwatch();
			this.s.Start();
		}

		public void Act() {
			float now = this.s.ElapsedMilliseconds / 1000;
			Modding.Logger.Log("now - this.last: " + (now - this.last));
			if (now - this.last > this.cooldown) {
				this.a();
				this.last = now;
			}
		}
	}
}
