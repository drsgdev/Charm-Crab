using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab.Charms {
	class QuakeCollider: SpellCollider {
		public override DamageType DType {
			get {
				return DamageType.Shriek;
			}
		}
	}
}
