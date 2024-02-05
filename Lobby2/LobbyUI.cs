using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
	public static LobbyUI I;
	private void Awake() {I = this;}

	public GameObject c_main, c_gameFound;
	public Text t_status, t_redName, t_blueName, t_status2;
	public InputField if_name;
	public bool lookingForMatch = false;
	public int dots = 1;
	public float changeDur = 0;

	// Player data
	public Text t_name, t_lvl, t_exp;
	public string name;
	public int lvl, exp;
	public Image i_rank, i_rankP1, i_rankP2;
	public Slider pb_exp;

	public Sprite[] rankSprites;

	public void _start(){
		L_Sounds.I._start ();
		LobbyPopup.I._start ();

		// Gain xp
		_calculateLevel ();

		// Player data
		name = PlayerPrefs.GetString ("playerName");
		lvl = PlayerPrefs.GetInt ("lvl");
		exp = PlayerPrefs.GetInt ("exp");
		PlayerPrefs.SetInt ("enemyDisconnect", 0);

		// UI
		t_name.text = name;
		t_lvl.text = "Level " + lvl.ToString () + ": " + getRankName(lvl);
		int expReq = lvl * 100;
		t_exp.text = exp.ToString() + "/" + expReq.ToString();
		i_rank.sprite = rankSprites [lvl - 1];
		pb_exp.value = (float)exp / (float)expReq;
	}

	void Update(){
		if (lookingForMatch) {
			string txt = "Looking for a match";
			for (int i = 1; i <= dots; i++) {
				txt += ".";
			}
			changeDur -= Time.deltaTime;
			if (changeDur <= 0) {
				dots++;
				if (dots > 3) dots = 1;
				changeDur = 1;
			}

			t_status.text = txt;
			t_status2.text = txt;

			if (PhotonLobby.lobby != null) {
				PhotonLobby.lobby.timeWait += Time.deltaTime;
			}
		}
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
			LobbyPopup.I.show_im ("You ranked up!", "Congratulations! You ranked up to " + getRankName(lvl) + "!\n\nThe higher you rank up, the higher the rewards you take from battles. Good luck on your future adventures in Terra!", 1);
		}
	}

	public void changeStatusText(string text){
		t_status.text = text;
		t_status2.text = text;
		lookingForMatch = false;
	}

	public void _gameFound(){
		c_main.SetActive (false);
		c_gameFound.SetActive (true);

		L_Sounds.I._playSound(4, 0, 0, false);
	}

	public void _back(){
//		PhotonRoom.room.disconnect ();
//		Destroy(GameObject.Find("MultiplayerSettings"));
//		Destroy(GameObject.Find("RoomController"));
		L_Sounds.I._playSound(2, 0, 0, false);
		PlayerPrefs.SetInt ("lobby_backButton", 1);
		SceneManager.LoadScene ("MainMenu");
	}

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
}
