using System.Collections;
using System;
using Modding;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;
using CharmCrab.Utils;
using CharmCrab;
using UnityEngine;

namespace SpellSystem.Behaviours {
	public class SpellOverride : MonoBehaviour {
		private readonly uint LoopThreshold = 20;
		

		private static SpellOverride Instance;
		private static GameObject FireballPrefab;
		private static PlayMakerFSM ThornFSM;
		private static PlayMakerFSM defaultControl;
		private static PlayMakerFSM nailArtsControl;
		private static GameObject CharmEffectsVanilla;
		

		
		private SpellState CurState = new Waiting();
		

		public static int ComputeSpellCost(SpellsNew c) {
			if (PlayerData.instance.GetBool("equippedCharm_19")) {
				switch (c) {
					case SpellsNew.Fireball: return 99;
					case SpellsNew.Shriek: return 99;
					case SpellsNew.Dive: return 99;
					case SpellsNew.VoidHorror: return 99;
					case SpellsNew.VoidTendrils: return 99;
					case SpellsNew.GreatSlash: return 33;
					case SpellsNew.DashSlash: return 33;
					case SpellsNew.CycloneSlash: return 33;
					default: return 0;
				}
			} else if (PlayerData.instance.GetBool("equippedCharm_33")) {
				switch (c) {
					case SpellsNew.Fireball: return 0;
					case SpellsNew.Shriek: return 0;
					case SpellsNew.Dive: return 0;
					case SpellsNew.VoidHorror: return 0;
					case SpellsNew.VoidTendrils: return 0;
					case SpellsNew.GreatSlash: return 33;
					case SpellsNew.DashSlash: return 33;
					case SpellsNew.CycloneSlash: return 33;
					default: return 0;
				}
			} else {
				switch (c) {
					case SpellsNew.Fireball: return 33;
					case SpellsNew.Shriek: return 33;
					case SpellsNew.Dive: return 33;
					case SpellsNew.VoidHorror: return 33;
					case SpellsNew.VoidTendrils: return 33;
					case SpellsNew.GreatSlash: return 33;
					case SpellsNew.DashSlash: return 33;
					case SpellsNew.CycloneSlash: return 33;
					default: return 0;
				}
			}
		}



		public static int ThornCost() {
			if (PlayerData.instance.GetBool("equippedCharm_19")) {
				return 66;
			} else if (PlayerData.instance.equippedCharm_21) {
				return 0;
			} else {
				return 33;
			}
		}

		public static int WombCost() {
			if (PlayerData.instance.GetBool("equippedCharm_19")) { 
				return 100; 
			} else if (PlayerData.instance.equippedCharm_21) {
				return 0;
			} else {
				return 33;
			}			
		}

		public static bool HasMP(int mp) {
			if (PlayerData.instance.equippedCharm_21) {
				return true;
			} else {
				return PlayerData.instance.MPCharge >= mp;
			}
		}

		// Use this for initialization
		void Start() {
			Instance = this;
			//FireballPrefab = HeroController.instance.spellControl.GetAction<SpawnObjectFromGlobalPool>("Fireball 1", 3).gameObject.Value;
			FireballPrefab = Functions.GetAction<SpawnObjectFromGlobalPool>(HeroController.instance.spellControl, "Fireball 1", 3).gameObject.Value;
			//HeroController.instance.spellControl.Fsm.
			//Modding.Logger.Log(obj);
			defaultControl = FSMUtility.LocateFSM(HeroController.instance.gameObject, "Spell Control");
			nailArtsControl = FSMUtility.LocateFSM(HeroController.instance.gameObject, "Nail Arts");
			CharmEffectsVanilla = HeroController.instance.gameObject.transform.Find("Charm Effects").gameObject;


			//spellFSM.Fsm.Clear(spellFSM);
			// Remove this transition to take control of spell controls, but keep enough to keep transitions working.
			//defaultControl.RemoveTransition("Reset Damage Mode", "FINISHED");

			//Functions.RemoveTransition(defaultControl, "Reset Damage Mode", "FINISHED");

			//Functions.RemoveTransition(defaultControl, "Inactive", "QUICK CAST");
			//Functions.RemoveTransition(defaultControl, "Button Down", "BUTTON UP ");

			Functions.RemoveTransition(defaultControl, "Inactive", "Can Cast? QC");
			Functions.RemoveTransition(defaultControl, "Button Down", "Can Cast?");

			// Remove this to use standard spell controls, but not transition to rest of FSM.
			//defaultControl.RemoveTransition("Spell End", "FINISHED");
			//Modding.Logger.Log(defaultControl.Fsm.GetState("Reset Damage Mode").Transitions);
			//defaultControl.Fsm.GetState("Reset Damage Mode").Transitions = new HutongGames.PlayMaker.FsmTransition[] { };
			//defaultControl.RemoveTransition("Inactive", "QUICK CAST");
			//defaultControl.Fsm.GetState("Inactive").Transitions = new HutongGames.PlayMaker.FsmTransition[] { };
			nailArtsControl.Fsm.GetState("Inactive").Transitions = new HutongGames.PlayMaker.FsmTransition[] { };

			//cdashFSM.Fsm.Clear(cdashFSM);
			//dnailFSM.Fsm.Clear(dnailFSM);

			// Get Thorn FSM for control
			var charms = HeroController.instance.gameObject.transform.Find("Charm Effects");
			ThornFSM = FSMUtility.LocateFSM(charms.gameObject, "Thorn Counter");			
		}

		

		// Update is called once per frame
		void Update() {
			this.CurState = this.CurState.Run();

			uint loops = 0;
			while (this.CurState.SameFrame && loops < LoopThreshold) {
				this.CurState = this.CurState.Run();
				loops += 1;
			}

			this.DebugTools();
		}

		private void DebugTools() {
			if (Input.GetKeyDown(KeyCode.O)) {
				HeroController.instance.AddMPCharge(100);
			}
		}

		private abstract class SpellState {
			
			public abstract SpellState Run();

			public bool Spell2 {
				get => (InputHandler.Instance.inputActions.quickCast.IsPressed) && InputHandler.Instance.inputActions.up.IsPressed;
			}

			public bool Spell3 {
				get => (InputHandler.Instance.inputActions.quickCast.IsPressed) && InputHandler.Instance.inputActions.down.IsPressed;
			}

			public bool Spell1 {
				get => InputHandler.Instance.inputActions.quickCast.IsPressed;
			}

			public bool Focus {
				get => InputHandler.Instance.inputActions.focus.IsPressed;
			}

			public virtual bool SameFrame {
				get {
					return false;
				}
			}
		}

		private class Waiting: SpellState {
			private readonly float MaxCooldown = 15;
			private static float Cooldown = -1;
			public override SpellState Run() {
				if (HeroController.instance.controlReqlinquished) {
					return this;
				}

				if (GameManager.instance.isPaused) {
					return this;
				}

				if (this.Focus) {
					//return new FSMUse("Focus Start");
				} else {
					if (PlayerData.instance.GetBool("equppedCharm_19")) {
						if (Cooldown < 0) {
							Cooldown = MaxCooldown;
							if (this.Spell2) {
								return this.CastSpell(SpellSystemMod.Settings.spellUp);
							} else if (this.Spell3) {
								return this.CastSpell(SpellSystemMod.Settings.spellDown);
							} else if (this.Spell1) {
								return this.CastSpell(SpellSystemMod.Settings.spellForward);
							}
						} else {
							Cooldown -= Time.deltaTime;
							return this;
						}
					} else {
						Cooldown = -1;
						if (this.Spell2) {
							return this.CastSpell(SpellSystemMod.Settings.spellUp);
						} else if (this.Spell3) {
							return this.CastSpell(SpellSystemMod.Settings.spellDown);
						} else if (this.Spell1) {
							return this.CastSpell(SpellSystemMod.Settings.spellForward);
						}
					}
				}

				return this;
			}

			private SpellState CastSpell(SpellsNew? s) {
				if (s == null) {
					return this;
				} else {
					switch (s) {
						case SpellsNew.Fireball: if (HasMP(ComputeSpellCost(SpellsNew.Fireball))) {
								defaultControl.FsmVariables.IntVariables[4].RawValue = ComputeSpellCost(SpellsNew.Fireball);
								return new FSMUse("Has Fireball?");
							} else {
								return this;
                            }
						case SpellsNew.Dive:
							if (SpellSystemMod.Settings.EquippedSpell(SpellsNew.Dive)) {
								if (HasMP(ComputeSpellCost(SpellsNew.Dive))) {
									defaultControl.FsmVariables.IntVariables[4].RawValue = ComputeSpellCost(SpellsNew.Dive);
									return new FSMUse("On Ground?");
								}
								return this;
							} else {
								return this;
                            }
						case SpellsNew.Shriek: if (HasMP(ComputeSpellCost(SpellsNew.Shriek))) {
								defaultControl.FsmVariables.IntVariables[4].RawValue = ComputeSpellCost(SpellsNew.Shriek);
								return new FSMUse("Level Check 3");
							} else {
								return this;
                            }
						case SpellsNew.CycloneSlash: if (HasMP(ComputeSpellCost(SpellsNew.CycloneSlash))) {
								HeroController.instance.TakeMP(ComputeSpellCost(SpellsNew.CycloneSlash));
								return new CycloneSlash();
							} else {
								return this;
                            }
						case SpellsNew.Womb: if (HasMP(WombCost())) {
								return new UpwardStart(new Spells.GlowingWomb());
							} else {
								return this;
                            }
						case SpellsNew.RadiantOrb: if (HasMP(ComputeSpellCost(SpellsNew.RadiantOrb))) {
								return new ForwardStart(new Spells.RadiantOrb());
							} else {
								return this;
							}
						case SpellsNew.Thorns: if (HasMP(ThornCost())) {
								return new ThornCast();
							} else {
								return this;
                            }
						case SpellsNew.GreatSlash:
							if (HasMP(ComputeSpellCost(SpellsNew.GreatSlash))) {
								HeroController.instance.TakeMP(ComputeSpellCost(SpellsNew.GreatSlash));
								return new GreatSlash();
							} else {
								return this;
                            }
						case SpellsNew.VoidTendrils: if (HasMP(ComputeSpellCost(SpellsNew.VoidTendrils))) {
								return new UpwardStart(new Spells.VoidTendrils());
							} else {
								return this;
                            }
						default: return this;
                    }
				}

			}
		}
	

		private class FSMUse: SpellState {
			private String stateName;
			public PlayMakerFSM control;
			private bool run = false;

			public FSMUse(string name) {
				this.stateName = name;
				this.control = defaultControl;
			}

			public FSMUse(PlayMakerFSM control, string state) {
				this.stateName = state;
				this.control = control;
			}

			public override SpellState Run() {
				if (this.run) {
					if (control.ActiveStateName == "Inactive" || control.ActiveStateName.Contains("Reset Damage")) {
						control.SetState("FSM Cancel");
						return new Waiting();
					} else {
						return this;
					}
				} else {
					this.control.SetState(this.stateName);
					this.run = true;
					return this;
				}
				

			}
		}

		private class GreatSlash : SpellState {
			private bool run = false;

			public override SpellState Run() {
				if (this.run) {
					if (nailArtsControl.ActiveStateName == "Inactive") {
						nailArtsControl.SetState("FSM Cancel");
						return new Waiting();
					} else {
						return this;
					}
				} else {
					HeroController.instance.RelinquishControlNotVelocity();
					HeroController.instance.StopAnimationControl();
					nailArtsControl.SetState("Flash 2");
					this.run = true;
					return this;
				}
			}
		}

		private class CycloneSlash : SpellState {
			private bool run = false;

			public override SpellState Run() {
				if (this.run) {
					if (nailArtsControl.ActiveStateName == "Inactive") {
						nailArtsControl.SetState("FSM Cancel");
						return new Waiting();
					} else {
						return this;
					}
				} else {
					HeroController.instance.RelinquishControlNotVelocity();
					HeroController.instance.StopAnimationControl();
					nailArtsControl.SetState("Flash");
					this.run = true;
					return this;
				}
			}
		}

		private class ThornCast : SpellState {
			private static float COOLDOWN = 0.45f;
			private float CoolDown = 0f;
			public override SpellState Run() {
				if (this.CoolDown == 0f) {
					HeroController.instance.SetDamageModeFSM(1);
					this.CoolDown += Time.deltaTime;
					ThornFSM.SetState("Counter Start");
					HeroController.instance.TakeMP(ThornCost());
					return this;
				} else {
					if (this.CoolDown < COOLDOWN) {
						this.CoolDown += Time.deltaTime;
						return this;
					} else {
						//HeroController.instance.GetComponent<HealthManager>().IsInvincible = false;
						HeroController.instance.SetDamageModeFSM(0);
						return new Waiting();
					}
				}

			}
		}

		private class ForwardCast: SpellState {
			private Spells.Spell _Prefab;
			public ForwardCast(Spells.Spell prefab) {
				this._Prefab = prefab;
			}

			public override SpellState Run() {
				HeroController.instance.GetComponent<tk2dSpriteAnimator>().Play("Fireball1 Cast");
				PlayMakerFSM.BroadcastEvent("STOP HERO EXIT");
				// TODO Take MP away.
				var fb = this._Prefab.Spawn();
				//HeroController.instance.TakeMP(this._Prefab.MP);
				// TODO Playvibration? I think this is controller related.
				return new ForwardRecoil();
			}
		}

		private class ForwardRecoil: SpellState {
			private bool _Ran = false;
			private float _Recoil = 1.0f;
			public ForwardRecoil() {
				HeroController.instance.GetComponent<Rigidbody2D>().gravityScale = 0;
				this._Recoil *= HeroController.instance.gameObject.transform.localScale.x;
				this._Recoil *= 2;
			}

			public override SpellState Run() {
				// TODO SetVelocity2D
				HeroController.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(this._Recoil, 0f);
				if (this._Ran) {
					if (HeroController.instance.GetComponent<tk2dSpriteAnimator>().IsPlaying("Fireball1 Cast")) {
						return this;
					} else {
						HeroController.instance.GetComponent<Rigidbody2D>().velocity.Set(0, 0);
						HeroController.instance.RegainControl();
						HeroController.instance.StartAnimationControl();
						HeroController.instance.AffectedByGravity(true);
						HeroController.instance.GetComponent<Rigidbody2D>().gravityScale = 0.79f;
						return new Waiting();
					}
				} else {
					PlayMakerFSM.BroadcastEvent("HERO CAST SPELL");
					// TODO figure out ITweenMoveBy
					this._Ran = true;
					return this;
				}
			}
		}

		private class ForwardStart: SpellState {
			private bool _SameFrame = true;
			private bool _Started = false;
			private Spells.Spell _Prefab;
			public ForwardStart(Spells.Spell prefab) {
				this._Prefab = prefab;
			}

			public override SpellState Run() {
				if (this._Started) {
					var playing = HeroController.instance.GetComponent<tk2dSpriteAnimator>().IsPlaying("Fireball Antic");
					if (playing) {
						return this;
					} else {
						return new ForwardCast(this._Prefab);
					}
				} else {
					var invert = HeroController.instance.wallSlidingL || HeroController.instance.wallSlidingR;

					HeroController.instance.GetComponent<tk2dSpriteAnimator>().Play("Fireball Antic");
					HeroController.instance.RelinquishControl();
					HeroController.instance.StopAnimationControl();
					HeroController.instance.AffectedByGravity(false);

					this._Started = true;
					return this;
				}
			}
			public override bool SameFrame {
				get {
					if (this._SameFrame) {
						this._SameFrame = false;
						return true;
					} else {
						return false;
					}

				}
			}
		}

		private class UpwardCast : SpellState {
			private Spells.Spell _Prefab;
			private float _Wait = 0f;
			public UpwardCast(Spells.Spell prefab) {
				this._Prefab = prefab;
			}

			public override SpellState Run() {
				//var playing = HeroController.instance.GetComponent<tk2dSpriteAnimator>().IsPlaying("Scream");
				if (this._Wait == 0f) {
					HeroController.instance.GetComponent<tk2dSpriteAnimator>().Play("Scream");
					PlayMakerFSM.BroadcastEvent("HERO CAST SPELL");
					var fb = this._Prefab.Spawn();
					//HeroController.instance.TakeMP(this._Prefab.MP);
					this._Wait += Time.deltaTime;
					return this;
				} else {
					if (this._Wait <= 0.45f) {
						this._Wait += Time.deltaTime;
						return this;
					} else {
						return new UpwardEnd();
					}
				}
			}
		}

		private class UpwardEnd : SpellState {
			private bool _Ran = false;

			public override SpellState Run() {
				// TODO SetVelocity2D
				//HeroController.instance.GetComponent<Rigidbody2D>().velocity = new Vector2(this._Recoil, 0f);
				if (this._Ran) {
					if (HeroController.instance.GetComponent<tk2dSpriteAnimator>().IsPlaying("Scream End")) {
						return this;
					} else {
						//HeroController.instance.GetComponent<Rigidbody2D>().velocity.Set(0, 0);
						HeroController.instance.RegainControl();
						HeroController.instance.StartAnimationControl();
						HeroController.instance.AffectedByGravity(true);
						HeroController.instance.GetComponent<Rigidbody2D>().gravityScale = 0.79f;
						return new Waiting();
					}
				} else {
					HeroController.instance.GetComponent<tk2dSpriteAnimator>().Play("Scream End");
					this._Ran = true;
					return this;
				}
			}
		}

		private class UpwardStart : SpellState {
			private bool _SameFrame = true;
			private bool _Started = false;
			private Spells.Spell _Prefab;
			public UpwardStart(Spells.Spell prefab) {
				this._Prefab = prefab;
			}

			public override SpellState Run() {
				if (this._Started) {
					var playing = HeroController.instance.GetComponent<tk2dSpriteAnimator>().IsPlaying("Scream Start");
					if (playing) {
						return this;
					} else {
						return new UpwardCast(this._Prefab);
					}
				} else {
					var invert = HeroController.instance.wallSlidingL || HeroController.instance.wallSlidingR;

					HeroController.instance.GetComponent<tk2dSpriteAnimator>().Play("Scream Start");
					HeroController.instance.RelinquishControl();
					HeroController.instance.StopAnimationControl();
					HeroController.instance.AffectedByGravity(false);
					/// TODO Look into Playing Audio like in FSM.

					this._Started = true;
					return this;
				}
			}
			public override bool SameFrame {
				get {
					if (this._SameFrame) {
						this._SameFrame = false;
						return true;
					} else {
						return false;
					}

				}
			}
		}
	}
}