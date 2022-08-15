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

	class CharmCrab : Mod, ILocalSettings<Settings> {

		public static Dictionary<int, NewCharmData> NewCharms = new Dictionary<int, NewCharmData>() {
			{0, new NewCharmData() {
					Name = "Void Horror",
					Cost = 1,
					Desc = "Changes your Shriek Spell to Summon Void Tendrils.",
					SpriteName = "Void Horror Icon",
					EnumValue = global::CharmCrab.NewCharms.VoidTendrils,
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

			ModHooks.HeroUpdateHook += AddBehaviour;
			ModHooks.HitInstanceHook += (HutongGames.PlayMaker.Fsm owner, HitInstance hitInst) => { return charmEffects.DamageRecalc(owner, hitInst); };
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
			//ModHooks.RecordKillForJournalHook

			UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
		}

		public void RecordKillForJournalHandler(EnemyDeathEffects enemyDeathEffects, string playerDataName, string killedBoolPlayerDataLookupKey, string killCountIntPlayerDataLookupKey, string newDataBoolPlayerDataLookupKey) {
			
		}


		public int OnSoulGain(int num) {
			// This just undoes what the built-in Soul gain calculations do based off of charms. This is so the soul
			// gain from Soul Catcher/Eater isn't ridiculous with their new effects. This information comes directly from
			// the default code in the game.
			if (PlayerData.instance.GetInt("MPCharge") < PlayerData.instance.GetInt("maxMP")) {
				if (PlayerData.instance.GetBool("equippedCharm_20")) {
					num -= 3;
				}
				if (PlayerData.instance.GetBool("equippedCharm_21")) {
					num -= 8;
				}
			} else {
				if (PlayerData.instance.GetBool("equippedCharm_20")) {
					num -= 2;
				}
				if (PlayerData.instance.GetBool("equippedCharm_21")) {
					num -= 6;
				}
			}
			return num/2;
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

		public void SceneLoaded(Scene scene, LoadSceneMode mode) {
			if (scene.name == "Menu_Title") {
				ModHooks.ObjectPoolSpawnHook -= charmEffects.UpdateSpells;
				ModHooks.HeroUpdateHook += AddBehaviour;
				charmEffects = null;
			} else {
				// Useful on some odd scene transitions it resets some FSM.
				Spells.SpellUpdater.UpdateSpellCosts();
			}
			
		}

		public int TakeDamage(ref int hazard, int dmg) {
			return charmEffects.TakeDamage(ref hazard, dmg);

		}

		private void AddBehaviour() {
			charmEffects = Functions.AddIfNeeded<Charms.CharmEffects>(HeroController.instance.gameObject);

			ModHooks.SlashHitHook   += charmEffects.SlashHitHandler;			
			ModHooks.TakeDamageHook += TakeDamage;
			ModHooks.ObjectPoolSpawnHook += charmEffects.UpdateSpells;
			ModHooks.ColliderCreateHook += charmEffects.OnColliderCreate;
			ModHooks.TakeDamageHook += charmEffects.DamageTaken;
			Spells.SpellUpdater.Init();
			Spells.SpellUpdater.UpdateSpellCosts();

			ModHooks.HeroUpdateHook -= AddBehaviour;

			
		}


		private bool OnGetPlayerBoolHook(string target, bool orig) {
			if (target.StartsWith("gotCharm_")) {
				int charmNum = int.Parse(target.Split('_')[1]);
				if (NewCharms.ContainsKey(charmNum)) {
					var en = NewCharms[charmNum].EnumValue;
					return NewCharmData.Obtained(en) || Settings.CharmObtained[en].Obtained;
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
			
			if (target == "killedAbyssTendril" && orig) {
				Settings.CharmObtained[global::CharmCrab.NewCharms.VoidTendrils].Obtained = true;
			}

			if (target == "bigCatShadeConvo" && orig) {
				Settings.CharmObtained[global::CharmCrab.NewCharms.ShadeAura].Obtained = true;
			}

			if (target == "givenEmilitiaFlower" && orig) {
				Settings.CharmObtained[global::CharmCrab.NewCharms.SoulInfusedBlade].Obtained = true;
			}

			if (target == "midwifeWeaverlingConvo" && orig) {
				Settings.CharmObtained[global::CharmCrab.NewCharms.AfflictedDevourer].Obtained = true;
			}

			if (target == "atBench" && !orig) {
				Spells.SpellUpdater.UpdateSpellCosts();
			}

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
	}	
}
