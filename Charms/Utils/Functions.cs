using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HutongGames.PlayMaker;
using UnityEngine;

namespace CharmCrab.Utils {
	class Functions {
		public static T GetAction<T>(PlayMakerFSM fsm, string stateName, int index) where T: FsmStateAction {
			foreach (var t in fsm.FsmStates) {
				if (t.Name != stateName) continue;
				FsmStateAction[] actions = t.Actions;

				Array.Resize(ref actions, actions.Length + 1);

				return actions[index] as T;
			}

			return null;
		}

		public static FsmState GetState(PlayMakerFSM fsm, string stateName) {
			return fsm.FsmStates.Where(t => t.Name == stateName)
				.Select(t => new { t, actions = t.Actions })
				.Select(t1 => t1.t)
				.FirstOrDefault();
		}

		public static void RemoveTransitions(PlayMakerFSM fsm, IEnumerable<string> states,
			IEnumerable<string> transitions) {
			IEnumerable<string> enumerable = states as string[] ?? states.ToArray();

			foreach (FsmState t in fsm.FsmStates) {
				if (!enumerable.Contains(t.Name)) continue;

				
				t.Transitions = t.Transitions.Where(trans => !transitions.Contains(trans.ToState)).ToArray();
				
			}
		}

		public static void RemoveTransition(PlayMakerFSM fsm, string state, string transition) {
			foreach (FsmState t in fsm.FsmStates) {
				if (state != t.Name) continue;

				t.Transitions = t.Transitions.Where(trans => transition != trans.ToState).ToArray();
			}
		}

		public static GameObject ObjectFromPath(GameObject obj, string path) {
			var a = path.Split('/');

			GameObject Find(GameObject root, int index) {
				if (root == null) {
					return null;
				} else if (index >= a.Length) {
					return root;
				} else {

					var child = root.transform.Find(a[index]);

					if (child != null) {
						return Find(child.gameObject, index + 1);
					} else {
						return null;
					}
				}
            }

			return Find(obj, 0);
        }

		public static bool HasCharmVanilla(CharmsVanilla c) {
			switch (c) {
				case CharmsVanilla.GatheringSwarm:      return PlayerData.instance.GetBool("gotCharm_01");
				case CharmsVanilla.WaywardCompass:      return PlayerData.instance.GetBool("gotCharm_02");
				case CharmsVanilla.GrubSong:            return PlayerData.instance.GetBool("gotCharm_03");
				case CharmsVanilla.StalwartShell:       return PlayerData.instance.GetBool("gotCharm_04");
				case CharmsVanilla.BaldurShell:         return PlayerData.instance.GetBool("gotCharm_05");
				case CharmsVanilla.Fury:                return PlayerData.instance.GetBool("gotCharm_06");
				case CharmsVanilla.QuickFocus:          return PlayerData.instance.GetBool("gotCharm_06");
				case CharmsVanilla.LifebloodHeart:      return PlayerData.instance.GetBool("gotCharm_07");
				case CharmsVanilla.LifebloodCore:       return PlayerData.instance.GetBool("gotCharm_08");
				case CharmsVanilla.DefenderCrest:       return PlayerData.instance.GetBool("gotCharm_09");
				case CharmsVanilla.Flukenest:           return PlayerData.instance.GetBool("gotCharm_10");
				case CharmsVanilla.Thorns:              return PlayerData.instance.GetBool("gotCharm_11");
				case CharmsVanilla.MarkOfPride:         return PlayerData.instance.GetBool("gotCharm_12");
				case CharmsVanilla.SteadyBody:          return PlayerData.instance.GetBool("gotCharm_13");
				case CharmsVanilla.HeavyBlow:           return PlayerData.instance.GetBool("gotCharm_14");
				case CharmsVanilla.SharpShadow:         return PlayerData.instance.GetBool("gotCharm_15");
				case CharmsVanilla.SporeShroom:         return PlayerData.instance.GetBool("gotCharm_16");
				case CharmsVanilla.LongNail:            return PlayerData.instance.GetBool("gotCharm_17");
				case CharmsVanilla.ShamanStone:         return PlayerData.instance.GetBool("gotCharm_18");
				case CharmsVanilla.SoulCatcher:         return PlayerData.instance.GetBool("gotCharm_19");
				case CharmsVanilla.SoulEater:           return PlayerData.instance.GetBool("gotCharm_20");
				case CharmsVanilla.GlowingWomb:         return PlayerData.instance.GetBool("gotCharm_21");
				case CharmsVanilla.FragileHeart:        return PlayerData.instance.GetBool("gotCharm_23");
				case CharmsVanilla.BrokenHeart:         return PlayerData.instance.GetBool("gotCharm_23_BRK");
				case CharmsVanilla.UnbreakableHeart:    return PlayerData.instance.GetBool("gotCharm_23_G");
				case CharmsVanilla.FragileGreed:        return PlayerData.instance.GetBool("gotCharm_24");
				case CharmsVanilla.BrokenGreed:         return PlayerData.instance.GetBool("gotCharm_24_BRK");
				case CharmsVanilla.UnbreakableGreed:    return PlayerData.instance.GetBool("gotCharm_24_G");
				case CharmsVanilla.FragileStrength:     return PlayerData.instance.GetBool("gotCharm_25");
				case CharmsVanilla.BrokenStrength:      return PlayerData.instance.GetBool("gotCharm_25_BRK");
				case CharmsVanilla.UnbreakableStrength: return PlayerData.instance.GetBool("gotCharm_25_G");
				case CharmsVanilla.NailMaster:          return PlayerData.instance.GetBool("gotCharm_26");
				case CharmsVanilla.JoniBlessing:        return PlayerData.instance.GetBool("gotCharm_27");
				case CharmsVanilla.ShapeOfUnn:          return PlayerData.instance.GetBool("gotCharm_28");
				case CharmsVanilla.HiveBlood:           return PlayerData.instance.GetBool("gotCharm_29");
				case CharmsVanilla.DreamWielder:        return PlayerData.instance.GetBool("gotCharm_30");
				case CharmsVanilla.DashMaster:          return PlayerData.instance.GetBool("gotCharm_31");
				case CharmsVanilla.QuickSlash:          return PlayerData.instance.GetBool("gotCharm_32");
				case CharmsVanilla.SpellTwister:        return PlayerData.instance.GetBool("gotCharm_33");
				case CharmsVanilla.DeepFocus:           return PlayerData.instance.GetBool("gotCharm_034");
				case CharmsVanilla.GrubElegy:           return PlayerData.instance.GetBool("gotCharm_35");
				case CharmsVanilla.WhiteFragment:       return PlayerData.instance.GetBool("gotCharm_36_A");
				case CharmsVanilla.KingSoul:            return PlayerData.instance.GetBool("gotCharm_36_B");
				case CharmsVanilla.VoidHeart:           return PlayerData.instance.GetBool("gotCharm_36_C");
				case CharmsVanilla.Sprintmaster:        return PlayerData.instance.GetBool("gotCharm_37");
				case CharmsVanilla.Dreamshield:         return PlayerData.instance.GetBool("gotCharm_38");
				case CharmsVanilla.Weaversong:          return PlayerData.instance.GetBool("gotCharm_39");
				case CharmsVanilla.Grimmchild:          return PlayerData.instance.GetBool("gotCharm_40");
				case CharmsVanilla.CarefreeMelody:      return PlayerData.instance.GetBool("gotCharm_40_N");
				default: return false;
			}
		}
	}
}
