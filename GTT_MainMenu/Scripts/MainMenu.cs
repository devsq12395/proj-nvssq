using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MainMenu : MonoBehaviour {
	public static MainMenu I;
	public void Awake(){ I = this; }
	
	public RectTransform rt_title, rt_mainMenu;
	public GameObject i_curtain;

	public bool goTo_multiplayer, goTo_singlePlayer, goTo_campaigns;

	// Player data
	public Text t_name, t_lvl, t_exp, t_crystals, t_keys;
	public string name;
	public int lvl, exp;
	public Image i_rank;
	public Slider pb_exp;

	public Toggle tog_sound, tog_music;

	public Sprite[] rankSprites;

	void Start () {
		Application.targetFrameRate = 60;
		
		SetupPlayerPrefs.I._start ();

		MM_Popups.I._start ();
		MM_Sounds.I._start ();
		MM_CrystalShop.I.start ();
		MM_Ads.I._start ();

		rt_title.localPosition = new Vector3 (rt_title.localPosition.x, 500, rt_title.localPosition.y);
		rt_mainMenu.localPosition = new Vector3 (rt_mainMenu.localPosition.x, -690, rt_mainMenu.localPosition.y);
		
		if (PlayerPrefs.GetString ("playerName") == "") {
			MM_Popups.I.go_ifName.SetActive (true);
		} else {
			_calculateLevel ();
			_setupMainMenu ();
			_startTween_title ();

			//dailyReward ();
		}

		disableSoundToggle = true;
		tog_sound.isOn = (PlayerPrefs.GetInt ("opt_sound") == 1) ? true : false;
		tog_music.isOn = (PlayerPrefs.GetInt ("opt_music") == 1) ? true : false;
		disableSoundToggle = false;

		updateCrystals ();
		updateKeys ();

		MM_Ads.I.showAd ();
	}

	void Update () {
		if (goTo_multiplayer) {
			PlayerPrefs.SetInt("isMultiplayer", 1);
			SceneManager.LoadScene ("Lobby2");

			goTo_multiplayer = false;
		}

		if (goTo_singlePlayer) {
			PlayerPrefs.SetInt("isMultiplayer", 0);
			SceneManager.LoadScene ("HeroSelect");

			goTo_singlePlayer = false;
		}

		if (goTo_campaigns) {
			PlayerPrefs.SetInt("isMultiplayer", 0);
			SceneManager.LoadScene ("Campaigns");

			goTo_campaigns = false;
		}
	}

	#region "Setup Main Menu"
	public void _setupMainMenu(){
		// Player data
		name = PlayerPrefs.GetString ("playerName");
		lvl = PlayerPrefs.GetInt ("lvl");
		exp = PlayerPrefs.GetInt ("exp");

		// UI
		t_name.text = name;
		t_lvl.text = "Level " + lvl.ToString () + ": " + getRankName(lvl);
		int expReq = lvl * 100;
		t_exp.text = exp.ToString() + "/" + expReq.ToString();
		i_rank.sprite = rankSprites [lvl - 1];
		pb_exp.value = (float)exp / (float)expReq;
	}

	public void _calculateLevel(){
		int expGained = PlayerPrefs.GetInt ("expGained");
		lvl = PlayerPrefs.GetInt ("lvl");
		exp = PlayerPrefs.GetInt ("exp");
		int lCount = 0;
		bool isLvlUp = false;

		if (lvl < 17) {
			while (expGained > 0) {
				int expReq = lvl * 100;

				if ((exp + expGained) >= expReq) {
					lvl++;
					isLvlUp = true;
					int expLeft = (exp + expGained) - expReq;
					expGained = expLeft;
					exp = 0;

					if (lvl >= 17) {
						expGained = 0;
						break;
					}
				} else {
					exp += expGained;
					expGained = 0;
				}

				lCount++;
				if (lCount >= 30) {
					break;
				}
			}
		}

		PlayerPrefs.SetInt ("expGained", 0);
		PlayerPrefs.SetInt ("lvl", lvl);
		PlayerPrefs.SetInt ("exp", exp);

		if (isLvlUp) {
			MM_Popups.I.show_im ("You ranked up!", "Congratulations! You ranked up to " + getRankName(lvl) + "!\n\nThe higher you rank up, the higher the rewards you take from battles. Good luck on your future adventures in Terra!", 1);
		
			// Steam Achievement - Second lieutenant
			if(lvl >= 4){
				MM_Steam.I.unlockAchievement ("RANK_UP_4_1_1");
			}

			// Steam Achievement - 1-Star General
			if(lvl >= 13){
				MM_Steam.I.unlockAchievement ("RANK_UP_17_1_2");
			}
		}
	}
	#endregion
	#region "Daily Reward"
	public void dailyReward (){
		//Decrease daily reward to 1 if the player skips a day
		if(System.DateTime.Parse(ZPlayerPrefs.GetString("lastPlayDate")) < System.DateTime.Now.Date){
			if(System.DateTime.Parse(ZPlayerPrefs.GetString("lastPlayDate")) < System.DateTime.Now.Date.AddDays(-2)){
				ZPlayerPrefs.SetInt("DailyBon_Day", 1);
			}
		}

		//Daily reward
		if(System.DateTime.Parse(ZPlayerPrefs.GetString("lastPlayDate")) < System.DateTime.Now.Date){
			// Content here
			int dailyBon_day = ZPlayerPrefs.GetInt("DailyBon_Day");
			int bonus = 0, orig = 0, imgIcon = 0;
			string drContent = "";
			switch (dailyBon_day % 10) {
				case 1:
					// Day 1 - crystals
					bonus = Random.Range (10, 30);
					orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
					ZPlayerPrefs.SetInt ("crystals", orig);
					drContent = "+" + bonus.ToString () + " crystals!";
					imgIcon = 0;
				break;
				case 2:
					// Day 2 - keys
					bonus = Random.Range (1, 3);
					orig = ZPlayerPrefs.GetInt ("keys") + bonus;
					ZPlayerPrefs.SetInt ("keys", orig);
					drContent = "+" + bonus.ToString () + " keys!";
					imgIcon = 1;
				break;
				case 3:case 4:
					// Day 3 & 4 - crystals
					bonus = Random.Range (20, 50);
					orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
					ZPlayerPrefs.SetInt ("crystals", orig);
					drContent = "+" + bonus.ToString () + " crystals!";
					imgIcon = 0;
				break;
				case 5:
					// Day 5 - keys
					bonus = Random.Range (2, 4);
					orig = ZPlayerPrefs.GetInt ("keys") + bonus;
					ZPlayerPrefs.SetInt ("keys", orig);
					drContent = "+" + bonus.ToString () + " keys!";
					imgIcon = 1;
				break;
				case 6:case 7:
					// Day 6 & 7 - crystals
					bonus = Random.Range (40, 60);
					orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
					ZPlayerPrefs.SetInt ("crystals", orig);
					drContent = "+" + bonus.ToString () + " crystals!";
					imgIcon = 0;
				break;
				case 8:
					// Day 8 - keys
					bonus = Random.Range (3, 5);
					orig = ZPlayerPrefs.GetInt ("keys") + bonus;
					ZPlayerPrefs.SetInt ("keys", orig);
					drContent = "+" + bonus.ToString () + " keys!";
					imgIcon = 1;
				break;
				case 9:
					// Day 9 - crystals
					bonus = Random.Range (60, 80);
					orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
					ZPlayerPrefs.SetInt ("crystals", orig);
					drContent = "+" + bonus.ToString () + " crystals!";
					imgIcon = 0;
				break;
				default:
					// Day 0/default - crystals
					bonus = Random.Range (100, 150);
					orig = ZPlayerPrefs.GetInt ("crystals") + bonus;
					ZPlayerPrefs.SetInt ("crystals", orig);
					drContent = "+" + bonus.ToString () + " crystals!";
				break;
			}

			MM_Popups.I.show_dr("Day " + dailyBon_day.ToString(), drContent, imgIcon);

			// Set new daily reward data
			dailyBon_day++;
			ZPlayerPrefs.SetInt("DailyBon_Day", dailyBon_day);
			ZPlayerPrefs.SetString("lastPlayDate", System.DateTime.Now.Date.ToString());
			updateCrystals ();
			MM_CrystalShop.I.updateCrystals ();
			updateKeys ();
		}
	}
	#endregion

	#region "Update Crystals/Keys"
	public void updateCrystals(){
		int crystals = ZPlayerPrefs.GetInt ("crystals");
		if (crystals != null) {
			//t_crystals.text = crystals.ToString ();
		}
	}

	public void updateKeys(){
		int keys = ZPlayerPrefs.GetInt ("keys");
		if (keys != null) {
			//t_keys.text = keys.ToString ();
		}
	}
	#endregion

	#region "Rank Names"
	public string getRankName(int rankNum){
		string retVal = "";
		switch (rankNum) {
			case 1: 			retVal = "Cadet"; break;
			case 2: 			retVal = "Private"; break;
			case 3: 			retVal = "Sergeant"; break;
			case 4: 			retVal = "Second Lieutenant"; break;
			case 5: 			retVal = "Lieutenant"; break;
			case 6: 			retVal = "Captain"; break;
			case 7: 			retVal = "Major"; break;
			case 8: 			retVal = "Lieutenant Colenel"; break;
			case 9: 			retVal = "Colenel"; break;
			case 10: 			retVal = "Brigadier"; break;
			case 11: 			retVal = "Major General"; break;
			case 12: 			retVal = "Lieutenant General"; break;
			case 13: 			retVal = "1-Star General"; break;
			case 14: 			retVal = "2-Star General"; break;
			case 15: 			retVal = "3-Star General"; break;
			case 16: 			retVal = "4-Star General"; break;
			case 17: 			retVal = "5-Star General"; break;
		}

		return retVal;
	}
	#endregion
	#region "Tweens"
	public void _startTween_title(){
		float firstPos = 500;
		float targetPos = 187.6f;
		float duration = 1f;

		gameObject.Tween("uiM_mainMenu_title", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_title.localPosition = new Vector3(rt_title.localPosition.x, t.CurrentValue, rt_title.localPosition.z);
		}, (t) =>{
			// completion
			rt_title.localPosition = new Vector3(rt_title.localPosition.x, t.CurrentValue, rt_title.localPosition.z);
			_startTween_mainMenu ();
		});
	}
	public void _startTween_mainMenu(){
		float firstPos = -690f;
		float targetPos = -27.899f;
		float duration = 1f;

		gameObject.Tween("uiM_mainMenu_mainMenu", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_mainMenu.localPosition = new Vector3(rt_mainMenu.localPosition.x, t.CurrentValue, rt_mainMenu.localPosition.z);
		}, (t) =>{
			// completion
			rt_mainMenu.localPosition = new Vector3(rt_mainMenu.localPosition.x, t.CurrentValue, rt_mainMenu.localPosition.z);
		});
	}
	#endregion
	#region "Main Menu Buttons"
	public void singlePlayer(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		i_curtain.SetActive (true);
		goTo_singlePlayer = true;
	}

	public void campaigns(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		i_curtain.SetActive (true);
		goTo_campaigns = true;
	}

	public void multiPlayer(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		i_curtain.SetActive (true);
		goTo_multiplayer = true;
	}
	#endregion
	#region "Options"
	public bool disableSoundToggle;
	public void _toggle_sound(){
		if (disableSoundToggle) return;

		int opt_sound = PlayerPrefs.GetInt ("opt_sound");
		PlayerPrefs.SetInt ("opt_sound", (opt_sound == 0) ? 1 : 0);
		MM_Sounds.I._playSound(2, 0, 0, false);
	}

	public void _toggle_music(){
		if (disableSoundToggle) return;

		int opt_music = PlayerPrefs.GetInt ("opt_music");
		Debug.Log (opt_music);
		PlayerPrefs.SetInt ("opt_music", (opt_music == 0) ? 1 : 0);
		MM_Sounds.I._playSound(2, 0, 0, false);
		if(opt_music == 0) MM_Sounds.I._playMusic ();
		else MM_Sounds.I._stopMusic ();
	}
	#endregion
}
