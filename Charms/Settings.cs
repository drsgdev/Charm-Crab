using System;
using System.Collections.Generic;
using System.Text;

namespace CharmCrab {
    public class Settings {
        private bool hasRadOrb = false;
        private bool hasVoidTends = false;

        public bool HasRadOrb {
            get {
                return this.hasRadOrb;
            }
            set {
                this.hasRadOrb = value;
			}
        }

        public bool HasVoidTends {
            get {
                return this.hasVoidTends;
            }
            set {
                this.hasVoidTends = value;
            }
        }
    }
}
