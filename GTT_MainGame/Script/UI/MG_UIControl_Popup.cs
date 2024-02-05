using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using DigitalRuby.Tween;

public class MG_UIControl_Popup : MonoBehaviour {
	public static MG_UIControl_Popup I;
	public void Awake(){ I = this; }

	public GameObject c, c_rewards;
	public RectTransform rt_window;
	public Text t_title, t_text, t_rewardCrystals, t_rewardKeys, t_rewardXP, t_rewardTip;
	public Image i_popup;
	public GameObject btn_yes, btn_no, btn_ok;

	public bool inPopup = false;
	public int popupType = 0;
	public int rewardCrystals, rewardKeys, rewardXP, vicNum;

	public Sprite spr_blueFlag, spr_redFlag, spr_victory, spr_defeat;

	public void _start(){
		c.SetActive (true);
		t_title 				= GameObject.Find ("T_PopupTitle").GetComponent<Text> ();
		t_text 					= GameObject.Find ("T_PopupText").GetComponent<Text> ();
		i_popup 				= GameObject.Find ("I_Popup").GetComponent<Image> ();
		c_rewards 				= GameObject.Find ("C_Rewards");
		btn_yes 				= GameObject.Find ("BTN_PopupYes");
		btn_no 					= GameObject.Find ("BTN_PopupNo");
		btn_ok 					= GameObject.Find ("BTN_PopupOk");

		c_rewards.SetActive (false);
		c.SetActive (false);
	}

	#region "Victory"
	public void callVictory(MG_ClassPlayer playerWon, int winType){
		if (MG_Globals.I.pause_gameOver)  return;

		// Image
		if (playerWon.playerNum == MG_Globals.I.curPlayerNum) {
			i_popup.sprite = spr_victory;
		} else {
			i_popup.sprite = spr_defeat;
		}

		// Tween window
		float firstPos = -1000;
		float targetPos = 7.1f;
		float duration = 1f;

		gameObject.Tween("uiM_victoryPopup", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_window.localPosition = new Vector3(rt_window.localPosition.x, t.CurrentValue, rt_window.localPosition.z);
		}, (t) =>{
			// completion
			rt_window.localPosition = new Vector3(rt_window.localPosition.x, t.CurrentValue, rt_window.localPosition.z);
		});

		// Content text - phrase 1
		t_text.text = "";
		string vicType, scoreStr = "campaign";
		vicNum = winType;
		switch (winType) {
			case 1:
				// Capture all points
				t_title.text = playerWon.name + " wins!";
				t_text.text = playerWon.name + " has captured all points!\n\n";
				if (PlayerPrefs.GetInt ("isMultiplayer") == 1 && playerWon.playerNum == MG_Globals.I.curPlayerNum) {
					rewardCrystals += 8;
					// Steam Achievement - Private Property
					if(MM_Steam.I != null) MM_Steam.I.unlockAchievement ("WIN_CONDITION_3_1_7");
				}

				// Content text - phase 2 (Score)
				scoreStr = "SCORE:\n" + MG_Globals.I.players [1].name + ": " + MG_Globals.I.players [1].heroKills + "\n" +
					MG_Globals.I.players [2].name + ": " + MG_Globals.I.players [2].heroKills;
				t_text.text += scoreStr;
			break;
			case 2:
				// Destroy a camp
				t_title.text = playerWon.name + " wins!";
				t_text.text = playerWon.name + " has slain the enemy commander!\n\n";
				if (PlayerPrefs.GetInt ("isMultiplayer") == 1 && playerWon.playerNum == MG_Globals.I.curPlayerNum) {
					rewardCrystals += 11;
					// Steam Achievement - Bulldozer
						if(MM_Steam.I != null) MM_Steam.I.unlockAchievement ("WIN_CONDITION_1_1_5");
				}

				// Content text - phase 2 (Score)
				scoreStr = "SCORE:\n" + MG_Globals.I.players [1].name + ": " + MG_Globals.I.players [1].heroKills + "\n" +
					MG_Globals.I.players [2].name + ": " + MG_Globals.I.players [2].heroKills;
				t_text.text += scoreStr;
			break;
			case 3:
				// Disconnect
				t_title.text = playerWon.name + " wins!";
				t_text.text = "Enemy has disconnected!\n\n";
				if (PlayerPrefs.GetInt ("isMultiplayer") == 1 && playerWon.playerNum == MG_Globals.I.curPlayerNum)  rewardCrystals += 5;

				// Content text - phase 2 (Score)
				scoreStr = "SCORE:\n" + MG_Globals.I.players [1].name + ": " + MG_Globals.I.players [1].heroKills + "\n" +
					MG_Globals.I.players [2].name + ": " + MG_Globals.I.players [2].heroKills;
				t_text.text += scoreStr;
			break;
			case 4:
				// 10 hero kills
				t_title.text = playerWon.name + " wins!";
				t_text.text = playerWon.name + " killed the enemy commander!\n\n";
				if (PlayerPrefs.GetInt ("isMultiplayer") == 1 && playerWon.playerNum == MG_Globals.I.curPlayerNum) {
					rewardCrystals += 14;
					// Steam Achievement - An Epic Struggle
					if(MM_Steam.I != null) MM_Steam.I.unlockAchievement ("WIN_CONDITION_2_1_6");
				}

				// Content text - phase 2 (Score) 
				scoreStr = "SCORE:\n" + MG_Globals.I.players [1].name + ": " + MG_Globals.I.players [1].heroKills + "\n" +
					MG_Globals.I.players [2].name + ": " + MG_Globals.I.players [2].heroKills;
				t_text.text += scoreStr;
			break;
			case 5:
				// Campaign Victory
				t_title.text = "Mission Complete!";
				MG_Globals.I.camp_curGold += MG_Globals.I.CAMP_score;
				if (MG_Globals.I.camp_curGold < 1000)
					MG_Globals.I.camp_curGold = 1000;
				t_text.text = "Congratulations! You have completed this mission!\n\nYour Score: " + MG_Globals.I.CAMP_score.ToString () + "\n\nYou have been rewarded with new Cards!";
				rewardCrystals += 5;
				PlayerPrefs.SetInt ("camp_curGold", MG_Globals.I.camp_curGold);

				int hK = PlayerPrefs.GetInt ("camp_heroKills"),
				uK = PlayerPrefs.GetInt ("camp_unitKills"),
				hD = PlayerPrefs.GetInt ("camp_heroDeaths");
				PlayerPrefs.SetInt ("camp_heroKills", hK + MG_Globals.I.players [1].heroKills);
				PlayerPrefs.SetInt ("camp_unitKills", uK + MG_Globals.I.players [1].unitKills);
				PlayerPrefs.SetInt ("camp_heroDeaths", hD + MG_Globals.I.players [2].heroKills);
			break;
			case 6:
				// Campaign Defeat
				t_title.text = "Mission Failed!";
				t_text.text = "You have been defeated! Better luck next time.";
			break;
		}

		vicType = t_text.text;

		// Gain XP
		int xpGain = PlayerPrefs.GetInt ("expGained");

		if (playerWon.playerNum == MG_Globals.I.curPlayerNum) {
			// Win
			if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
				xpGain += 35;
			} else {
				xpGain += 10;
			}
			MG_ControlSounds.I._playSound(8, 0, 0, false);

			// Steam Achievement - First Victory
			if(MM_Steam.I != null) MM_Steam.I.unlockAchievement ("FIRST_VICTORY");
		} else {
			// Lose
			if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
				xpGain += 15;
			} else {
				xpGain += 5;
			}
			MG_ControlSounds.I._playSound(9, 0, 0, false);
		}
		PlayerPrefs.SetInt ("expGained", xpGain);
		rewardXP = xpGain;

		// Mark game as over
		MG_Globals.I.pause_gameOver = true;
		btn_ok.SetActive (true);
		btn_yes.SetActive (false);
		btn_no.SetActive (false);
		_show ();

		// Generate crystals and keys reward
		int tempLvl = PlayerPrefs.GetInt ("lvl");
		if (playerWon.playerNum == MG_Globals.I.curPlayerNum) {
			if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
				// Multiplayer win
				rewardCrystals += Random.Range (10, 16) + tempLvl * 3;
				rewardKeys = 2;
			} else {
				// Singleplayer win
				rewardCrystals += Random.Range (5, 10);
				rewardKeys = 0;
			}
		} else {
			if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
				// Multiplayer lose
				rewardCrystals += Random.Range (5, 10) + tempLvl * 1;
				rewardKeys = 1;
			} else {
				// Singleplayer lose
				rewardCrystals += Random.Range (1, 5);
				rewardKeys = 0;
			}
		}

		// Show rewards
		c_rewards.SetActive (true);
		t_rewardCrystals.text = "+" + rewardCrystals.ToString ();
		t_rewardKeys.text = "+" + rewardKeys.ToString ();
		t_rewardXP.text = "+" + rewardXP.ToString ();
		if(PlayerPrefs.GetInt ("isMultiplayer") == 1){
			t_rewardTip.text = "";
		}

		// Give rewards
		int tempCrystals = ZPlayerPrefs.GetInt ("crystals") + rewardCrystals;
		int tempKeys = ZPlayerPrefs.GetInt ("keys") + rewardKeys;
		ZPlayerPrefs.SetInt ("crystals", tempCrystals);
		ZPlayerPrefs.SetInt ("keys", tempKeys);

		// Analytics
		string gameId = PlayerPrefs.GetString ("gameId");
		Analytics.CustomEvent("Victory", new Dictionary<string, object>
		{
			{ gameId, PlayerPrefs.GetInt ("isMultiplayer").ToString() + "," + vicType + "," + scoreStr},
		});
	}
	#endregion

	public void _show(int msgNum = 0){
		c.SetActive (true);
		inPopup = true;
		popupType = msgNum;

		MG_ControlAds.I.showAd ();

		switch (msgNum) {
			case 1:
				// Enemy disconnected (UNUSED)
				t_title.text = "";
				t_text.text = "Enemy player has disconnected.";
				i_popup.sprite = spr_blueFlag;
				btn_ok.SetActive (true);
				btn_yes.SetActive (false);
				btn_no.SetActive (false);
			break;
			case 2:
				// Quit Mission
				t_title.text = "";
				t_text.text = "Are you sure you want to quit the game?";
				i_popup.sprite = spr_blueFlag;
				btn_ok.SetActive (false);
				btn_yes.SetActive (true);
				btn_no.SetActive (true);
			break;
		}
	}

	#region "YES Button"
	public void BTN_Yes(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		switch (popupType) {
			case 2:
				// Quit Mission
				if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
					PhotonRoom.room.disconnect ();
					SceneManager.LoadScene ("Lobby2");
				} else {
					SceneManager.LoadScene ("MainMenu");
				}
			break;
		}
	}
	#endregion
	#region "NO Button"
	public void BTN_No(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		switch (popupType) {
			default:
				c.SetActive (false);
				inPopup = false;
			break;
		}
	}
	#endregion
	#region "OK Button"
	public void _hide(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		c.SetActive (false);
		inPopup = false;

		// Back to menu/lobby
		if (MG_Globals.I.pause_gameOver) {
			if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
				PhotonRoom.room.disconnect ();
				SceneManager.LoadScene ("Lobby2");
			} else {
				switch (vicNum) {
					case 5:
						int curMap = PlayerPrefs.GetInt ("camp_mapNum");
						PlayerPrefs.SetInt ("camp_mapNum", curMap + 1);
						
						if(curMap >= PlayerPrefs.GetInt ("camp_mapNum_Max")){
							PlayerPrefs.SetInt ("camp_score_" + PlayerPrefs.GetInt ("camp_Number").ToString(), MG_Globals.I.CAMP_score);
							int hS = PlayerPrefs.GetInt ("camp_highScore_" + PlayerPrefs.GetInt ("camp_Number").ToString ());
							if (hS <= MG_Globals.I.CAMP_score) {
								PlayerPrefs.SetInt ("camp_highScore_" + PlayerPrefs.GetInt ("camp_Number").ToString(), MG_Globals.I.CAMP_score);
							}
							SceneManager.LoadScene ("Campaigns");
						}else{
							string 	storyNum = ((PlayerPrefs.GetInt ("camp_Number") < 10) ? "0" : "") + PlayerPrefs.GetInt ("camp_Number").ToString(), 
							mapNum = ((PlayerPrefs.GetInt ("camp_mapNum") < 10) ? "0" : "") + PlayerPrefs.GetInt ("camp_mapNum").ToString();
							PlayerPrefs.SetString("game_mapName", "Stories"+storyNum+"_mis"+mapNum);
							PlayerPrefs.SetInt ("camp_score_" + PlayerPrefs.GetInt ("camp_Number").ToString(), MG_Globals.I.CAMP_score);

							SceneManager.LoadScene ("MainGame");
						}
					break;
					case 6:
						PlayerPrefs.SetInt ("camp_curGold", 1000);
						PlayerPrefs.SetInt ("camp_score_" + PlayerPrefs.GetInt ("camp_Number").ToString(), MG_Globals.I.CAMP_score);
						SceneManager.LoadScene ("Campaigns");
					break;
					default:
						SceneManager.LoadScene ("MainMenu");
					break;
				}
			}
		}
	}
	#endregion
}
