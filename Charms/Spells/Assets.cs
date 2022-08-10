using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using System.Reflection;

namespace CharmCrab.Spells
{
    class Assets {

		public static GameObject TendrilsPrefab;
		public static GameObject HorrorPrefab;
		public static GameObject RadiantOrbPrefab;
		public static GameObject SpellMenuPrefab;
		public static GameObject EnemyDetect;

		private static AssetBundle ab;

		public static void Init()
		{
			string bundleN = "assets";
			Assembly asm = Assembly.GetExecutingAssembly();

			foreach (string res in asm.GetManifestResourceNames())
			{
				using (Stream s = asm.GetManifestResourceStream(res))
				{
					if (s == null) continue;
					byte[] buffer = new byte[s.Length];
					s.Read(buffer, 0, buffer.Length);
					s.Dispose();
					string bundleName = Path.GetExtension(res).Substring(1);
					if (bundleName != bundleN) continue;
					ab = AssetBundle.LoadFromMemory(buffer); // Store this somewhere you can access again.
				}
			}

			SpellMenuPrefab = ab.LoadAsset<GameObject>("Spell Menu");
			GetSpells(ab);
		}

		private static void GetSpells(AssetBundle ab)
		{
			var v1 = GameManager.instance.gameSettings.masterVolume / 10.0f;
			var v2 = GameManager.instance.gameSettings.soundVolume / 10.0f;
			var v3 = v1 * v2;

			/// Void Tendrils Loading
			TendrilsPrefab = ab.LoadAsset<GameObject>("Tendril Spell");
			TendrilsPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			TendrilsPrefab.transform.GetChild(1).GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			TendrilsPrefab.transform.GetChild(0).gameObject.AddComponent<Spells.TendrilSpellController>();
			TendrilsPrefab.transform.GetChild(1).gameObject.AddComponent<Spells.TendrilSpellController>();
			foreach (var a in TendrilsPrefab.transform.GetChild(0).gameObject.GetComponents<AudioSource>())
			{
				a.volume = v3;
			}

			/// Horror Loading
			HorrorPrefab = ab.LoadAsset<GameObject>("VoidHorror Spell");
			HorrorPrefab.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			HorrorPrefab.AddComponent<Spells.HorrorSpellController>();
			HorrorPrefab.GetComponent<AudioSource>().volume = v3;

			/// Radiant Orb Loading
			RadiantOrbPrefab = ab.LoadAsset<GameObject>("Radiant Orb Spell");
			RadiantOrbPrefab.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			RadiantOrbPrefab.AddComponent<Spells.RadiantOrbController>();
			foreach (var a in RadiantOrbPrefab.GetComponents<AudioSource>())
			{
				a.volume = v3;
			}

			EnemyDetect = ab.LoadAsset<GameObject>("Detect Enemy");
		}


		public void Log(string msg)
		{
			Modding.Logger.Log(msg);
		}
	}
}
