using System;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using UnityEngine;
using UnityEngine.Events;
using UnityEngineInternal;
//using UnityStandardAssets;
using UnityEngine.SceneManagement;
using HutongGames;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using SFCore;

namespace CharmCrab {

	class CharmCrab: Mod, ILocalSettings<Settings> {
		
		public Charms.CharmEffects charmEffects;
		public static Settings Settings = new Settings();

		public CharmCrab() : base("Charm Crab") { }

		public override string GetVersion() {
			return "0.1";
		}

		public override void Initialize() {
			base.Initialize();
			this.InitHooks();
		}

		private void InitHooks() {

			ModHooks.HeroUpdateHook     += AddBehaviour;
			ModHooks.HitInstanceHook    += HitInstanceCreated;
			ModHooks.TakeDamageHook     += DamageTaken;
			ModHooks.OnEnableEnemyHook  += OnEnemyEnable;
			ModHooks.GetPlayerBoolHook  += OnGetPlayerBoolHook;
			ModHooks.SetPlayerBoolHook  += OnSetPlayerBoolHook;
			ModHooks.GetPlayerIntHook   += OnGetPlayerIntHook;
			ModHooks.SetPlayerIntHook   += OnSetPlayerIntHook;
			ModHooks.LanguageGetHook    += OnLanguageGetHook;
			ModHooks.GetPlayerBoolHook  += OnGetPlayerBoolHook;
			ModHooks.SetPlayerBoolHook  += OnSetPlayerBoolHook;
			ModHooks.GetPlayerIntHook   += OnGetPlayerIntHook;
			ModHooks.SetPlayerIntHook   += OnSetPlayerIntHook;

			UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
		}

		public void OnLoadLocal(Settings s) {
			Settings = s;
        }

		public Settings OnSaveLocal() {
			return Settings;
        }

		private bool OnEnemyEnable(GameObject obj, bool isdead) {
			var hm = obj.GetComponent<HealthManager>();

			if (hm) {
				hm.hp *= 4;
			}			

			return isdead;
        }

		private int DamageTaken(ref int hazard, int dmg) {
			return dmg;
		}


		public void SceneLoaded(Scene scene, LoadSceneMode mode) {
			if (scene.name == "Menu_Title") {
				ModHooks.HeroUpdateHook += AddBehaviour;
			}						
		}

		private T AddIfNeeded<T>() where T: MonoBehaviour {
			var test = HeroController.instance.gameObject.GetComponent<T>();

			if (test == null) {
				return HeroController.instance.gameObject.AddComponent<T>();
			} else {
				return test;
			}
		}

		private void AddBehaviour() {
			this.charmEffects = AddIfNeeded<Charms.CharmEffects>();

			ModHooks.SlashHitHook   += this.charmEffects.SlashHitHandler;			
			ModHooks.TakeDamageHook += this.charmEffects.TakeDamage;

			ModHooks.HeroUpdateHook -= AddBehaviour;
		}

		public HitInstance HitInstanceCreated(HutongGames.PlayMaker.Fsm owner, HitInstance hitInst) {
			if (owner.GameObject.name == "Slash" || owner.GameObject.name == "UpSlash" || owner.GameObject.name == "DownSlash" || owner.GameObject.name == "AltSlash") {
				hitInst.DamageDealt = this.charmEffects.ComputeDamage(DamageType.Slash);
			} else if (owner.GameObject.name == "Great Slash") {
				hitInst.DamageDealt = this.charmEffects.ComputeDamage(DamageType.GreatSlash);
			} else if (owner.GameObject.name == "Hit L" || owner.GameObject.name == "Hit R") {
				hitInst.DamageDealt = this.charmEffects.ComputeDamage(DamageType.Cyclone);
			} else if (owner.GameObject.name == "Fireball(Clone)") {
				hitInst.DamageDealt = this.charmEffects.ComputeDamage(DamageType.FireBall);
			} else if (owner.GameObject.name == "Q Fall Damage") {
				hitInst.DamageDealt = this.charmEffects.ComputeDamage(DamageType.Dive);
			} else if (owner.GameObject.name == "Hit L" || owner.GameObject.name == "Hit R" || owner.GameObject.name == "Hit U" || owner.GameObject.name == "Hit D") {
				hitInst.DamageDealt = this.charmEffects.ComputeDamage(DamageType.Shriek);
			} else if (owner.GameObject.name == "Sharp Shadow") {
				hitInst.DamageDealt = this.charmEffects.ComputeDamage(DamageType.SharpShadow);
			} else {
				//Log("Hit source name: " + owner.GameObject.name);
			}

			return hitInst;
		}

		private bool OnGetPlayerBoolHook(string target, bool orig) {

			if (target.StartsWith("gotCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
			}
			if (target.StartsWith("newCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);				
			}
			if (target.StartsWith("equippedCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
			}

			return orig;
        }

		private bool OnSetPlayerBoolHook(string target, bool orig) {
			if (target.StartsWith("gotCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
			}
			if (target.StartsWith("newCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
			}
			if (target.StartsWith("equippedCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
			}

			return orig;
		}

		private int OnGetPlayerIntHook(string target, int orig) {
			if (target.StartsWith("charmCost_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				return CharmCosts.COSTS[charmNum];
			}


			return orig;
		}

		private int OnSetPlayerIntHook(string target, int val) {
			return val;
		}

		private string OnLanguageGetHook(string key, string sheetTitle, string orig) {
			if (key.StartsWith("CHARM_NAME_")) {
				int charmNum = int.Parse(key.Split('_')[2]);
			}
			if (key.StartsWith("CHARM_DESC_")) {
				int charmNum = int.Parse(key.Split('_')[2]);
			}

			return orig;
		}
	}	
}
