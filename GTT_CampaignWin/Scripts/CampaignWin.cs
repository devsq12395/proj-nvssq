using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DigitalRuby.Tween;

public class CampaignWin : MonoBehaviour {
	public Image i, i_port;
	public Text t_name, t_desc, t_desc2;

	public GameObject go_winPopup, go_backBTN;
	public RectTransform rt_winWindow;
	public float txtData_score, txtData_heroKills, txtData_unitKills, txtData_heroDeaths;
	public int tweenNum = 0, campNum, campScore, heroKills, unitKills, heroDeaths;

    void Start() {
		campNum = PlayerPrefs.GetInt ("camp_Number") - 1;
		campScore = PlayerPrefs.GetInt ("camp_score_" + (campNum+1).ToString());
		heroKills = PlayerPrefs.GetInt ("camp_heroKills");
		unitKills = PlayerPrefs.GetInt ("camp_unitKills");
		heroDeaths = PlayerPrefs.GetInt ("camp_heroDeaths");

		i.sprite = CW_DB.I.getSprite(campNum);
		i_port.sprite = CW_DB.I.getPortrait(campNum);
		t_name.text = CW_DB.I.getName(campNum);
		t_desc.text = CW_DB.I.getDesc(campNum);
		t_desc2.text = "";

		go_winPopup.SetActive (false);
		go_backBTN.SetActive (false);
    }

    void Update() {
		// Complete window, text data
		t_desc2.text = "Missions: 3\nScore: " + txtData_score.ToString() + 
			"\n\nTotal Hero Kills: " + txtData_heroKills.ToString() +
			"\nTotal Unit Kills: " + txtData_unitKills.ToString() +
			"\nTotal Hero Deaths: " + txtData_heroDeaths.ToString();
    }

	public void winPopup(){
		go_winPopup.SetActive (true);
		_playSound (4);

		// Tween window
		float firstPos = -1000;
		float targetPos = 7.1f;
		float duration = 0.5f;

		gameObject.Tween("uiM_winWindow", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_winWindow.localPosition = new Vector3(rt_winWindow.localPosition.x, t.CurrentValue, rt_winWindow.localPosition.z);
		}, (t) =>{
			// completion
			rt_winWindow.localPosition = new Vector3(rt_winWindow.localPosition.x, t.CurrentValue, rt_winWindow.localPosition.z);
			tweenData ();
		});
	}

	public void tweenData(){
		float firstPos = 0, targetPos = 0;
		switch (tweenNum) {
			case 0: targetPos = (float)campScore; break;
			case 1: targetPos = (float)heroKills; break;
			case 2: targetPos = (float)unitKills; break;
			case 3: targetPos = (float)heroDeaths; break;
		}

		float duration = 0.25f;

		if (targetPos > 0) {
			gameObject.Tween ("uiM_winWindow", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) => {
				// progress
				switch (tweenNum) {
					case 0: txtData_score = (int)t.CurrentValue; break;
					case 1: txtData_heroKills = (int)t.CurrentValue; break;
					case 2: txtData_unitKills = (int)t.CurrentValue; break;
					case 3: txtData_heroDeaths = (int)t.CurrentValue; break;
				}
			}, (t) => {
				// completion
				switch (tweenNum) {
					case 0: txtData_score = (int)t.CurrentValue; break;
					case 1: txtData_heroKills = (int)t.CurrentValue; break;
					case 2: txtData_unitKills = (int)t.CurrentValue; break;
					case 3: txtData_heroDeaths = (int)t.CurrentValue; break;
				}

				tweenNum++;
				if (tweenNum <= 3) 	tweenData ();
				else 				go_backBTN.SetActive (true);
			});
		} else {
			tweenNum++;
			if (tweenNum <= 3) 	tweenData ();
			else 				go_backBTN.SetActive (true);
		}
	}

	public void BTNBack(){
		SceneManager.LoadScene ("Campaigns");
	}

	#region "SOUND"
	//MG_ControlSounds.I._playSound(8, 0, 0, false);
	public AudioSource soundSource;
	public AudioClip snd_win;
	public void _playSound(int audioNumber){
		if(PlayerPrefs.GetInt("opt_sound") != 1)	return;

		Vector3 tempLoc = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
		switch(audioNumber){
			/*Chat Notif*/					//case 1: soundSource.PlayOneShot(snd_chatNotif, 1); break;
			/*Click Button*/				//case 2: soundSource.PlayOneShot(snd_clickBtn, 1); break;
			/*Click Cursor Move*/			//case 3: soundSource.PlayOneShot(snd_clickCursor, 1); break;
			/*Win*/							case 4: soundSource.PlayOneShot(snd_win, 1); break;
		}
	}
	#endregion
}
