using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using System.Reflection;

namespace CharmCrab
{
    class Assets {

		public GameObject TendrilsPrefab;
		public GameObject HorrorPrefab;
		public GameObject RadiantOrbPrefab;

		private AssetBundle ab;

		public Assets()	{
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
					this.ab = AssetBundle.LoadFromMemory(buffer); // Store this somewhere you can access again.
				}
			}

			GetCharms();
			GetSpells();
		}

		private void GetCharms() {
			foreach (var item in CharmCrab.NewCharms) {
				item.Value.Sprite = ab.LoadAsset<Sprite>(item.Value.SpriteName);
				Log(item.Value.Sprite.ToString());
			}
		}

		private void GetSpells() {
			var v1 = GameManager.instance.gameSettings.masterVolume / 10.0f;
			var v2 = GameManager.instance.gameSettings.soundVolume / 10.0f;
			var v3 = v1 * v2;

			/// Void Tendrils Loading
			TendrilsPrefab = this.ab.LoadAsset<GameObject>("Tendril Spell");
			TendrilsPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			TendrilsPrefab.transform.GetChild(1).GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			TendrilsPrefab.transform.GetChild(0).gameObject.AddComponent<Spells.TendrilSpellController>();
			TendrilsPrefab.transform.GetChild(1).gameObject.AddComponent<Spells.TendrilSpellController>();
			foreach (var a in TendrilsPrefab.transform.GetChild(0).gameObject.GetComponents<AudioSource>())
			{
				a.volume = v3;
			}

			/// Horror Loading
			//HorrorPrefab = ab.LoadAsset<GameObject>("VoidHorror Spell");
			//HorrorPrefab.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			//HorrorPrefab.AddComponent<Spells.HorrorSpellController>();
			//HorrorPrefab.GetComponent<AudioSource>().volume = v3;

			/// Radiant Orb Loading
			RadiantOrbPrefab = ab.LoadAsset<GameObject>("Radiant Orb Spell");
			RadiantOrbPrefab.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Default"));
			RadiantOrbPrefab.AddComponent<Spells.RadiantOrbController>();
			foreach (var a in RadiantOrbPrefab.GetComponents<AudioSource>()) {
				a.volume = v3;
			}
		}


		public void Log(string msg)
		{
			Modding.Logger.Log(msg);
		}
	}
}
