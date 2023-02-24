using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab {
	class Cooldown {
		private float cd;
		private float duration;

		public Cooldown(float duration) {
			this.duration = duration;
		}

		public void Update() {
			this.cd -= Time.deltaTime;
		}

		public void Reset() {
			this.cd = this.duration;
		}

		public bool Available {
			get { return this.cd <= 0; }
		}
	}
}
