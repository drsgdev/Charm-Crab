using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Modding;

namespace CharmCrab.Charms {
	class CharmEffects: MonoBehaviour {
		public static CharmEffects instance;

		private HeavyBleed bleed;
		private MarkOfPride pride;
		private Fury fury;
		private NailMaster nmg;
		private DashMaster dashmaster;
		private SteadyBody steady;
		private JoniBlessing joni;
		private Fireball fireball;
		private Wraith wraith;
		private Quake quake;
		private BaldurShell baldurshell;
		private Kingsoul kingsoul;
		private SporeShroom sporeshroom;
		private SharpShadow sharpshadow;
		private GrimmChild grimmchild;

		public void Start() {
			instance = this;

			this.bleed = new HeavyBleed();
			this.pride = new MarkOfPride();
			this.fury = new Fury();
			this.nmg = new NailMaster();
			this.dashmaster = new DashMaster();
			this.steady = new SteadyBody();
			this.joni = new JoniBlessing();
			this.fireball = new Fireball();
			this.baldurshell = new BaldurShell();
			this.kingsoul = new Kingsoul();
			this.sporeshroom = new SporeShroom();
			this.sharpshadow = new SharpShadow();
			this.quake = new Quake();
			this.wraith = new Wraith();
			this.grimmchild = new GrimmChild();
		}
		public void Update() {
			this.bleed.Update();
			this.pride.Update();
			this.dashmaster.Update();
			this.baldurshell.Update();
			this.kingsoul.Update();
			this.sporeshroom.Update();
			this.sharpshadow.Update();
			this.grimmchild.Update();
		}

		public int ComputeDamage(DamageType n) {
			int baseDMG = 0;

			if (PlayerData.instance.nailSmithUpgrades == 0) {
				baseDMG = 15;
			} else if (PlayerData.instance.nailSmithUpgrades == 1) {
				baseDMG = 25;
			} else if (PlayerData.instance.nailSmithUpgrades == 2) {
				baseDMG = 32;
			} else if (PlayerData.instance.nailSmithUpgrades == 3) {
				baseDMG = 38;
			} else if (PlayerData.instance.nailSmithUpgrades == 4) {
				baseDMG = 45;
			}

			baseDMG += this.pride.DmgBonus;
			baseDMG = (int) (baseDMG * this.fury.Mult);

			switch (n) {
				case DamageType.Slash: return (int) (this.nmg.Mult(n) * baseDMG);
				case DamageType.GreatSlash: return (int)(this.nmg.Mult(n) * baseDMG);
				case DamageType.Cyclone: return (int)(this.nmg.Mult(n) * baseDMG);
				case DamageType.DashSlash: return (int)(this.nmg.Mult(n) * baseDMG);
				case DamageType.FireBall: return this.fireball.Damage();
				case DamageType.Dive: return this.quake.Damage();
				case DamageType.Shriek: return this.wraith.Damage();
				case DamageType.SharpShadow: return this.sharpshadow.Damage();
				default: return baseDMG;
			}

		}

		public void SlashHitHandler(Collider2D col, GameObject slash) {
			this.bleed.SlashHitHandler(col, slash);
			this.joni.SlashHitHandler(col, slash);
		}

		public int TakeDamage(ref int hazard, int dmg) {
			this.pride.TakeDamage(dmg);
			return this.steady.TakeDamage(ref hazard, dmg);
		}
	}
}
