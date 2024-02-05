using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class HeroSelect : MonoBehaviour {
	public static HeroSelect I;
	public void Awake(){ I = this; }

	public Image i_loading;
	public Text t_loading, t_multStatus;

	// For multiplayer
	public PhotonRoom roomControl;
	public bool finishPicking = false;

	public float pickTimer = 180;

	public bool goTo_Game = false;

	#region "Start"
	void Start () {
		#region "Multiplayer setup"
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			roomControl = PhotonRoom.room;
			roomControl.p1IsReady = false;
			roomControl.p2IsReady = false;

			roomControl.setEnemyPhotonPlayer ();

			if (PlayerPrefs.GetInt ("enemyDisconnect") == 1) {
				HS_Popup.I._show ("Enemy disconnected.", false, true);
			}
		} 
		#endregion

		// Generate gameId
		generateGameId ();
	}
	#endregion

	#region "Update"
	void Update(){
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			pickTimer -= Time.deltaTime;
			if (pickTimer < 0)  pickTimer = 0;

			TimeSpan timeSpan = TimeSpan.FromSeconds ((double)Mathf.Floor (pickTimer));
			string sTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
			if (finishPicking) {
				t_multStatus.text = "Waiting for other players (" + sTime + ")...";
			} else {
				t_multStatus.text = "(" + sTime + ")";
			}
			if (pickTimer <= 0 && !finishPicking) {
				BTN_Next ();
			}
		}

		if (goTo_Game) {
			goTo_Game = false;
			loadGameScene ();
		}
	}
	#endregion

	#region "BTN Back"
	public void BTN_Back(){
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			HS_Popup.I._show ("Are you sure you want to leave the game?", true);
		} else {
			SceneManager.LoadScene ("MainMenu");
		}
	}
	#endregion
	#region "BTN Next"
	public void BTN_Next(){
		DeckBuilder.I.saveCardData ();

		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			mult_setHeroes ();
			setReady (PlayerPrefs.GetInt ("playerNum"));
			finishPicking = true;
		} else {
			generateAICard ();

			loadGameScene ();
		}
	}
	#endregion

	#region "Generate AI Cards"
	public void generateAICard() {
		PlayerPrefs.SetString ("cardNameAI_1", "Reinforcements");
		PlayerPrefs.SetString ("cardNameAI_2", "Reinforcements");
		PlayerPrefs.SetString ("cardNameAI_3", "Cannon");
		PlayerPrefs.SetString ("cardNameAI_4", "Cannon");
		PlayerPrefs.SetString ("cardNameAI_5", "ImperialInfantry");
		PlayerPrefs.SetString ("cardNameAI_6", "ImperialInfantry");
		PlayerPrefs.SetString ("cardNameAI_7", "LumberMill");
		PlayerPrefs.SetString ("cardNameAI_8", "Farm");
		PlayerPrefs.SetString ("cardNameAI_9", "LumberMill");
		PlayerPrefs.SetString ("cardNameAI_10", "LumberMill");
		PlayerPrefs.SetString ("cardNameAI_11", "Barracks");
		PlayerPrefs.SetString ("cardNameAI_12", "Barracks");

		PlayerPrefs.SetString ("cardNameAI_13", "Robin");
		PlayerPrefs.SetString ("cardNameAI_14", "Robin");
		PlayerPrefs.SetString ("cardNameAI_15", "Robin");
		PlayerPrefs.SetString ("cardNameAI_16", "Robin");
		PlayerPrefs.SetString ("cardNameAI_17", "Reinforcements");
		PlayerPrefs.SetString ("cardNameAI_18", "Reinforcements");
		PlayerPrefs.SetString ("cardNameAI_19", "LumberMill");
		PlayerPrefs.SetString ("cardNameAI_20", "Farm");
		PlayerPrefs.SetString ("cardNameAI_21", "CavalryCharge");
		PlayerPrefs.SetString ("cardNameAI_22", "CavalryCharge");
		PlayerPrefs.SetString ("cardNameAI_23", "ImperialInfantry");
		PlayerPrefs.SetString ("cardNameAI_24", "ImperialInfantry");
	}
	#endregion
	#region "Load Game Scene"
	public void loadGameScene(){
		i_loading.enabled = true;
		t_loading.enabled = true;

		SceneManager.LoadScene ("MainGame");
	}
	#endregion

	#region "Set GameId"
	public void generateGameId (){
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1 && PlayerPrefs.GetInt ("playerNum") != 1) {
			return;
		}

		string glyphs= "abcdefghijklmnopqrstuvwxyz0123456789";
		string newId = "";

		int charAmount = 6;
		for(int i=0; i<charAmount; i++){
			newId += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
		}

		PlayerPrefs.SetString ("gameId", newId);
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1 && PlayerPrefs.GetInt ("playerNum") != 1) {
			roomControl.gameAction_shareGameId (newId);
		}
	}

	public void receiveGameId (string newId){
		PlayerPrefs.SetString ("gameId", newId);
	}
           	#endregion

	#region "MULTIPLAYER - Set Team"
	public void mult_setHeroes(){
		string strPlayer = PlayerPrefs.GetInt ("playerNum").ToString ();
	}
	#endregion
	#region "MULTIPLAYER - Set Ready"
	public void setReady(int playerNum){
		roomControl.photon_setReady (playerNum);
	}
	#endregion
}
