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
using UnityEngine.SceneManagement;
using HutongGames;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using SFCore;

namespace CharmCrab {

	class CharmCrab: Mod, ILocalSettings<Settings> {

		public static Dictionary<int, CharmData> NewCharms = new Dictionary<int, CharmData>() {
			{0, new CharmData() { 
					Name = "Void Horror", 
					Cost = 1, 
					Desc = "Changes your Shriek Spell to Summon Void Tendrils.",
					SpriteName = "Void Horror Icon",
					EnumValue = CharmsNew.VoidTendrils,
				}
			},
		};

		public static Assets Assets;
		public static Charms.CharmEffects charmEffects;

		public static Settings Settings = new Settings();

		public CharmCrab() : base("Charm Crab") { }

		public override string GetVersion() {
			return "0.1";
		}

		public override void Initialize() {
			Assets = new Assets();


			base.Initialize();
			this.InitHooks();
			this.SetCharmIndices();
		}

		private void SetCharmIndices() {
			Sprite[] sprites = new Sprite[NewCharms.Count];

			for (int i = 0; i < NewCharms.Count; ++i) {
				sprites[i] = NewCharms[i].Sprite;
			}

			var indices = CharmHelper.AddSprites(sprites);

			for (int i = 0; i < NewCharms.Count; ++i) {
				var v = NewCharms[i];
				NewCharms.Remove(i);
				NewCharms.Add(indices[i], v);
			}
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
			charmEffects = AddIfNeeded<Charms.CharmEffects>();

			ModHooks.SlashHitHook   += charmEffects.SlashHitHandler;			
			ModHooks.TakeDamageHook += charmEffects.TakeDamage;
			Spells.SpellUpdater.Init();

			ModHooks.HeroUpdateHook -= AddBehaviour;

			
		}

		public HitInstance HitInstanceCreated(HutongGames.PlayMaker.Fsm owner, HitInstance hitInst) {
			if (owner.GameObject.name == "Slash" || owner.GameObject.name == "UpSlash" || owner.GameObject.name == "DownSlash" || owner.GameObject.name == "AltSlash") {
				hitInst.DamageDealt = charmEffects.ComputeDamage(DamageType.Slash);
			} else if (owner.GameObject.name == "Great Slash") {
				hitInst.DamageDealt = charmEffects.ComputeDamage(DamageType.GreatSlash);
			} else if (owner.GameObject.name == "Hit L" || owner.GameObject.name == "Hit R") {
				hitInst.DamageDealt = charmEffects.ComputeDamage(DamageType.Cyclone);
			} else if (owner.GameObject.name == "Fireball(Clone)") {
				hitInst.DamageDealt = charmEffects.ComputeDamage(DamageType.FireBall);
			} else if (owner.GameObject.name == "Q Fall Damage") {
				hitInst.DamageDealt = charmEffects.ComputeDamage(DamageType.Dive);
			} else if (owner.GameObject.name == "Hit L" || owner.GameObject.name == "Hit R" || owner.GameObject.name == "Hit U" || owner.GameObject.name == "Hit D") {
				hitInst.DamageDealt = charmEffects.ComputeDamage(DamageType.Shriek);
			} else if (owner.GameObject.name == "Sharp Shadow") {
				hitInst.DamageDealt = charmEffects.ComputeDamage(DamageType.SharpShadow);
			} else {
				//Log("Hit source name: " + owner.GameObject.name);
			}

			return hitInst;
		}

		private bool OnGetPlayerBoolHook(string target, bool orig) {

			if (target.StartsWith("gotCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					return Settings.CharmObtained[en].Obtained;
				}
			}
			if (target.StartsWith("newCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					return Settings.CharmObtained[en].New;
				}
			}
			if (target.StartsWith("equippedCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					return Settings.CharmObtained[en].Equipped;
				}
			}

			return orig;
        }

		private bool OnSetPlayerBoolHook(string target, bool orig) {
			if (target.StartsWith("gotCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					Settings.CharmObtained[en].Obtained = orig;
				}
			}
			if (target.StartsWith("newCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					Settings.CharmObtained[en].New = orig;
				}
			}
			if (target.StartsWith("equippedCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					Settings.CharmObtained[en].Equipped = orig;
				}
			}

			return orig;
		}

		private int OnGetPlayerIntHook(string target, int orig) {
			if (target.StartsWith("charmCost_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					return NewCharms[charmNum].Cost;
				} else {
					return CharmCosts.COSTS[charmNum];
				}
			}

			return orig;
		}

		private int OnSetPlayerIntHook(string target, int val) {
			return val;
		}

		private string OnLanguageGetHook(string key, string sheetTitle, string orig) {
			if (key.StartsWith("CHARM_NAME_")) {
				int charmNum = int.Parse(key.Split('_')[2]);
				if (NewCharms.ContainsKey(charmNum)) {
					return NewCharms[charmNum].Name;					
				}
			}
			if (key.StartsWith("CHARM_DESC_")) {
				int charmNum = int.Parse(key.Split('_')[2]);
				if (NewCharms.ContainsKey(charmNum)) {
					return NewCharms[charmNum].Desc;
				}
			}

			return orig;
		}
	}	
}
