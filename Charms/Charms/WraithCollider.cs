using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class WraithCollider: SpellCollider {
		public override DamageType DType {
			get {
				return DamageType.Dive;
			}
		}
	}
}
