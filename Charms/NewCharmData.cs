using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharmCrab {
	class NewCharmData {
		public string Name;
		public int Cost;
		public string Desc;
		public string SpriteName;
		public Sprite Sprite;
		public NewCharms EnumValue;

		public static bool Obtained(NewCharms c) {
			switch (c) {
				case NewCharms.VoidTendrils: return PlayerData.instance.GetBool("killedAbyssTendril");
				case NewCharms.PureAura: return PlayerData.instance.GetBool("bigCatShadeConvo");
				case NewCharms.SoulInfusedBlade: return PlayerData.instance.GetBool("givenEmilitiaFlower");
				case NewCharms.AfflictedDevourer: return PlayerData.instance.GetBool("midwifeWeaverlingConvo");
				default: return false;
			}
		}
	}
}
