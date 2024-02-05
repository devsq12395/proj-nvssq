using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_GoldenChest : MonoBehaviour {
	public static MM_GoldenChest I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Text t_keys;

	public void start (){
		go.SetActive (true);

		go.SetActive (false);
	}

	public void show(){
		go.SetActive (true);
		int keys = ZPlayerPrefs.GetInt ("keys");
		t_keys.text = keys.ToString();
	}

	public void openChest (){
		int keys = ZPlayerPrefs.GetInt ("keys");
		string title = "Chest opened!", content = "You opened a Golden Chest and found ";
		if (keys >= 5) {
			MM_Sounds.I._playSound(4, 0, 0, false);
			keys -= 5;
			ZPlayerPrefs.SetInt ("keys", keys);

			int randomReward = Random.Range (0, 100), bonus = 0, orig = 0, imgIcon = 0;
			if (randomReward <= 20) {
				// Crystals - few
				bonus = Random.Range (10, 30);
				orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
				ZPlayerPrefs.SetInt ("crystals", orig);
				content += "a handful of crystals! (+ " + bonus.ToString() +")";
				imgIcon = 0;
			}
			else if (randomReward <= 40) {
				// Crystals - medium
				bonus = Random.Range (40, 60);
				orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
				ZPlayerPrefs.SetInt ("crystals", orig);
				content += "a pack of crystals! (+ " + bonus.ToString() +")";
				imgIcon = 0;
			}
			else if (randomReward <= 50) {
				// Crystals - lots
				bonus = Random.Range (100, 120);
				orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
				ZPlayerPrefs.SetInt ("crystals", orig);
				content += "a huge amount of crystals! (+ " + bonus.ToString() +")";
				imgIcon = 0;
			}
			else if (randomReward <= 60) {
				// XP - few
				bonus = Random.Range (10, 30);
				orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
				ZPlayerPrefs.SetInt ("crystals", orig);
				content += "a handful of crystals! (+ " + bonus.ToString() +")";
				imgIcon = 0;
			}
			else if (randomReward <= 70) {
				// XP - lots
				bonus = Random.Range (40, 60);
				orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
				ZPlayerPrefs.SetInt ("crystals", orig);
				content += "a pack of crystals! (+ " + bonus.ToString() +")";
				imgIcon = 0;
			}
			else {
				// Nothing
				bonus = Random.Range (100, 120);
				orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
				ZPlayerPrefs.SetInt ("crystals", orig);
				content += "nothing! I guess that's better luck next time.";
				imgIcon = 0;
			}

			MM_Popups.I.show_im (title, content, imgIcon);
			MainMenu.I.updateKeys ();
			MainMenu.I.updateCrystals ();
			t_keys.text = keys.ToString();
		}
	}
}
