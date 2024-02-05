using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Curtain : MonoBehaviour {
	public static MG_UIControl_Curtain I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Text t;

	public bool isGameLoaded = false;

	public void _start(){
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			isGameLoaded = true;

			PhotonRoom.room.gameAction_sendLoadedData ();
		} else {
			go.SetActive (false);
			MG_Globals.I.pause_gamePause = false;
		}
	}

	public void _startGame(){
		go.SetActive (false);
		MG_Globals.I.pause_gamePause = false;
	}
}
