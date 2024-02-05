using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Chat : MonoBehaviour {
	public static MG_UIControl_Chat I;
	public void Awake(){ I = this; }

	public GameObject c, i_chatNotif;

	public Text t_chat, t_mute;
	public InputField inField;
	public Button btn_send, btn_mute;
	public List<string> chatStr;

	public bool inChat = false, isMute = false, isEnemyMute = false;

	public void _start(){
		c = GameObject.Find ("C_Chat");
		t_chat = GameObject.Find ("T_Chat").GetComponent<Text> ();
		inField = GameObject.Find ("IF_Chat").GetComponent<InputField> ();
		btn_send = GameObject.Find ("BTN_ChatSend").GetComponent<Button> ();
		btn_mute = GameObject.Find ("BTN_ChatMute").GetComponent<Button> ();
		t_mute = GameObject.Find ("BTN_T_ChatMute").GetComponent<Text> ();
		chatStr = new List<string> ();

		c.SetActive (false);
		i_chatNotif.SetActive (false);
	}

	public void _show(){
		if (PlayerPrefs.GetInt ("isMultiplayer") != 1)  return;
		if(MG_Globals.I.pause_uiMove || MG_Globals.I.pause_windowOpen || MG_Globals.I.pause_gamePause || MG_Globals.I.pause_gameOver || MG_UIControl_Popup.I.inPopup)		return;

		MG_ControlSounds.I._playSound(2, 0, 0, false);

		c.SetActive (true);
		i_chatNotif.SetActive (false);
		inChat = true;
	}

	public void _hide(){
		c.SetActive (false);
		inChat = false;
	}

	public void _send(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		string toSend = PlayerPrefs.GetString ("playerName") + ": " + inField.text;
		if(toSend.Length < 1)	return;
		//Cut the string to acceptable length
		if(toSend.Length > 47)  toSend = toSend.Substring(0, 45);

		inField.text = "";

		PhotonRoom.room.gameAction_chat (toSend);

		_append(toSend);
	}

	public void _append(string newInput){
		if(chatStr.Count > 10)
			chatStr.RemoveAt(0);
		chatStr.Add(newInput);

		t_chat.text = "";
		foreach(string sL in chatStr){
			t_chat.text += sL + "\n";
		}
	}

	#region "Mute system"
	public void _pressMute(){
		if (PlayerPrefs.GetInt ("isMultiplayer") != 1)  return;

		if (!isMute) {
			_mute ();
		} else {
			_unmute ();
		}
	}

	public void _mute(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		isMute = true;

		btn_send.enabled = false;

		if (MG_Globals.I.curPlayerNum == 1) {
			_append (PlayerPrefs.GetString ("playerName_p1") + " muted the chat.");
		} else {
			_append (PlayerPrefs.GetString ("playerName_p2") + " muted the chat.");
		}

		t_mute.text = "Unmute";
		PhotonRoom.room.gameAction_chatMute ();
	}

	public void _unmute(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		isMute = false;

		btn_send.enabled = true;

		if (MG_Globals.I.curPlayerNum == 1) {
			_append (PlayerPrefs.GetString ("playerName_p1") + " unmuted the chat.");
		} else {
			_append (PlayerPrefs.GetString ("playerName_p2") + " unmuted the chat.");
		}

		t_mute.text = "Mute";
		PhotonRoom.room.gameAction_chatUnmute ();
	}

	public void _enemyMute(){
		isEnemyMute = true;

		if (MG_Globals.I.curPlayerNum == 1) {
			_append (PlayerPrefs.GetString ("playerName_p2") + " muted the chat.");
		} else {
			_append (PlayerPrefs.GetString ("playerName_p1") + " muted the chat.");
		}

		t_mute.text = "Unmute";
		btn_send.enabled = false;
		btn_mute.enabled = false;
	}

	public void _enemyUnmute(){
		isEnemyMute = false;

		if (MG_Globals.I.curPlayerNum == 1) {
			_append (PlayerPrefs.GetString ("playerName_p2") + " unmuted the chat.");
		} else {
			_append (PlayerPrefs.GetString ("playerName_p1") + " unmuted the chat.");
		}

		t_mute.text = "Mute";
		btn_send.enabled = true;
		btn_mute.enabled = true;
	}
	#endregion
}
