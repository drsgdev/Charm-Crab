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
using Vasi;

namespace CharmCrab {

	class CharmCrab : Mod, ILocalSettings<Settings> {
		
		public const int BossDamage = 2;
		public const int SuperBossDamage = 3;
		public const int SuperHealthScaleFactor = 5;
		public const int HealthScaleFactor = 5;
		public const int BossHealthThreshold = 65;		
		public const int SuperBossHealthThreshold = 500;

		public static Dictionary<int, NewCharmData> NewCharms = new Dictionary<int, NewCharmData>() {
			{0, new NewCharmData() {
					Name = "Void Tendrils",
					Cost = 1,
					Desc = "Changes your Shriek Spell to Summon Void Tendrils.",
					SpriteName = "Tendril Charm Icon",
					EnumValue = global::CharmCrab.NewCharms.VoidTendrils,
				}
			},

			{1, new NewCharmData() {
					Name = "Soul Infused Blade",
					Cost = 1,
					Desc = "Infuses Nail Arts with Soul",
					SpriteName = "Soul Infused Icon",
					EnumValue = global::CharmCrab.NewCharms.SoulInfusedBlade,
				}
			},

			{2, new NewCharmData() {
					Name = "Afflicted Devourer",
					Cost = 1,
					Desc = "Causes afflictions to harm your foe.",
					SpriteName = "Devourer Icon",
					EnumValue = global::CharmCrab.NewCharms.AfflictedDevourer,
				}
			},

			{3, new NewCharmData() {
					Name = "Aura of Purity",
					Cost = 1,
					Desc = "Converts your Shriek spell into a variably-lasting aura spell.",
					SpriteName = "Pure Aura Icon",
					EnumValue = global::CharmCrab.NewCharms.PureAura,
				}
			},
		};

		public static Assets Assets;
		//public static Charms.CharmEffects charmEffects;

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

			ModHooks.HeroUpdateHook += AddBehaviour;
			ModHooks.OnEnableEnemyHook += OnEnemyEnable;
			ModHooks.GetPlayerBoolHook += OnGetPlayerBoolHook;
			ModHooks.SetPlayerBoolHook += OnSetPlayerBoolHook;
			ModHooks.GetPlayerIntHook += OnGetPlayerIntHook;
			ModHooks.SetPlayerIntHook += OnSetPlayerIntHook;
			ModHooks.LanguageGetHook += OnLanguageGetHook;
			ModHooks.GetPlayerBoolHook += OnGetPlayerBoolHook;
			ModHooks.SetPlayerBoolHook += OnSetPlayerBoolHook;
			ModHooks.GetPlayerIntHook += OnGetPlayerIntHook;
			ModHooks.SetPlayerIntHook += OnSetPlayerIntHook;
			ModHooks.SoulGainHook += OnSoulGain;

			UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
		}

		public int OnSoulGain(int num) {
			return Charms.CharmEffects.instance.OnSoulGain(num);
		}

		public void OnLoadLocal(Settings s) {
			Settings = s;
        }

		public Settings OnSaveLocal() {
			return Settings;
        }

		private bool OnEnemyEnable(GameObject obj, bool isdead) {
			if (obj.GetComponent<EnemyStats>() == null) {
				//Modding.Logger.Log("Statifying: " + obj.name);
				var stats = obj.AddComponent<EnemyStats>();
				var hm = obj.GetComponent<HealthManager>();
				stats.origHp = hm.hp;

				if (obj.name == "Hollow Shade(Clone)") {
					// Special case to prevent the hollow shade from having thousands of health inadvertently and doing super boss damage.
					hm.hp = PlayerData.instance.maxHealth / 2 * Charms.CharmEffects.instance.ComputeDamage(DamageType.Slash);
					stats.origHp = hm.hp;
				} else {
					if (hm.hp >= SuperBossHealthThreshold) {
						hm.hp *= SuperHealthScaleFactor;
					} else if (hm.hp >= BossHealthThreshold) {
						hm.hp *= HealthScaleFactor;
					} else {
						hm.hp *= HealthScaleFactor;
					}
				}
			}

			return isdead;
        }

		public void SceneLoaded(Scene scene, LoadSceneMode mode) {
			if (scene.name == "Menu_Title") {
				if (Charms.CharmEffects.instance != null) {
					ModHooks.SlashHitHook -= Charms.CharmEffects.instance.SlashHitHandler;
					ModHooks.TakeDamageHook -= Charms.CharmEffects.instance.TakeDamage;
					ModHooks.ObjectPoolSpawnHook -= Charms.CharmEffects.instance.UpdateSpells;
					ModHooks.ColliderCreateHook -= Charms.CharmEffects.instance.OnColliderCreate;
					ModHooks.HitInstanceHook -= Charms.CharmEffects.instance.DamageRecalc;
					ModHooks.ObjectPoolSpawnHook -= Charms.CharmEffects.instance.UpdateSpells;
					ModHooks.SetPlayerIntHook -= Charms.CharmEffects.instance.OnPlayerSetInt;
				}
				ModHooks.HeroUpdateHook += AddBehaviour;
				//charmEffects = null;
			} else {
				// Useful on some odd scene transitions it resets some FSM.
				Spells.SpellUpdater.UpdateSpellCosts();
			}
			
		}

		private void AddBehaviour() {
			var charmEffects = HeroController.instance.gameObject.GetComponent<Charms.CharmEffects>();
			if (charmEffects != null) {
				return;
			} else {
				charmEffects = HeroController.instance.gameObject.AddComponent<Charms.CharmEffects>();
			}
			//var charmEffects = Functions.AddIfNeeded<Charms.CharmEffects>(HeroController.instance.gameObject);

			ModHooks.SlashHitHook   += charmEffects.SlashHitHandler;	
			ModHooks.TakeDamageHook += charmEffects.TakeDamage;
			ModHooks.ObjectPoolSpawnHook += charmEffects.UpdateSpells;
			ModHooks.ColliderCreateHook += charmEffects.OnColliderCreate;
			ModHooks.HitInstanceHook += charmEffects.DamageRecalc;
			ModHooks.SetPlayerIntHook += charmEffects.OnPlayerSetInt;
			Spells.SpellUpdater.Init();
			Spells.SpellUpdater.UpdateSpellCosts();

			ModHooks.HeroUpdateHook -= AddBehaviour;

			//Modding.Logger.Log("Blocker: " + HeroController.instance.gameObject.transform.Find("Blocker"));
		}


		private bool OnGetPlayerBoolHook(string target, bool orig) {
			if (target.StartsWith("gotCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					return NewCharmData.Obtained(en) || Settings.CharmData[en].Obtained;
				}
			}
			if (target.StartsWith("newCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					return Settings.CharmData[en].New;
				}
			}
			if (target.StartsWith("equippedCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					return Settings.CharmData[en].Equipped;
				}
			}

			return orig;
        }

		private bool OnSetPlayerBoolHook(string target, bool orig) {
			
			if (target == "killedAbyssTendril" && orig) {
				Settings.CharmData[global::CharmCrab.NewCharms.VoidTendrils].Obtained = true;
			}

			if (target == "bigCatShadeConvo" && orig) {
				Settings.CharmData[global::CharmCrab.NewCharms.PureAura].Obtained = true;
			}

			if (target == "givenEmilitiaFlower" && orig) {
				Settings.CharmData[global::CharmCrab.NewCharms.SoulInfusedBlade].Obtained = true;
			}

			if (target == "midwifeWeaverlingConvo" && orig) {
				Settings.CharmData[global::CharmCrab.NewCharms.AfflictedDevourer].Obtained = true;
			}

			if (target == "atBench" && !orig) {
				Spells.SpellUpdater.UpdateSpellCosts();
			}

			if (target.StartsWith("gotCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					Settings.CharmData[en].Obtained = orig;
				}
			}
			if (target.StartsWith("newCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					Settings.CharmData[en].New = orig;
				}
			}
			if (target.StartsWith("equippedCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					Settings.CharmData[en].Equipped = orig;					
				}
				Spells.SpellUpdater.UpdateSpellCosts();
			}
			return orig;
		}

		private int OnGetPlayerIntHook(string target, int orig) {
			if (target.StartsWith("charmCost_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					return NewCharms[charmNum].Cost;
				} else {
					if (charmNum == 36 && PlayerData.instance.royalCharmState == 4) {
						return 0;
					}
					return CharmData.Cost(CharmData.Index(charmNum));
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

		private class EnemyStats: MonoBehaviour {
			public int origHp = 1;


			public void OnEnable() {
				if (this.name.Contains("Fluke Fly Spawner")) {
					var fsm = FSMUtility.LocateFSM(this.gameObject, "Fluke Fly");

					if (fsm != null) {
						// Special case for recycled fluke spawners in Fluke Marm's arena.
						FsmUtil.GetAction<SetHP>(FsmUtil.GetState(fsm, "Reset")).hp = 5 * 13;
					}
				}
			}

			public void Update() {
				if (this.origHp >= SuperBossHealthThreshold) {
					foreach (var d in this.GetComponentsInChildren<DamageHero>()) {
						d.damageDealt = SuperBossDamage;
					}
				} else if (this.origHp >= BossHealthThreshold) {
					foreach (var d in this.GetComponentsInChildren<DamageHero>()) {
						d.damageDealt = BossDamage;
					}
				}
			}
		}
	}	
}
