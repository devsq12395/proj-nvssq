using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MG_UIControl_TopBar : MonoBehaviour {
	public static MG_UIControl_TopBar I;
	public void Awake(){ I = this; }

	public Canvas c_topBar;
	public GameObject go_topBar, go_menu, go_obj;
	public GameObject windows_mainCanvas;

	#region "GENERAL - Start"
	public void _start(){
		go_topBar = GameObject.Find ("C_TopBar");
		c_topBar = GameObject.Find ("C_TopBar").GetComponent<Canvas> ();

		windows_mainCanvas.SetActive(true);

		if (PlayerPrefs.GetInt ("isMultiplayer") != 1) {
			GameObject.Find ("BTN_Chat").SetActive (false);
		}

		_startObj ();
		_startItem ();
		_startMenu ();
		_startOptions ();
		_start_TimerUI ();
		_goldUI_start ();
	}
	#endregion
	#region "GENERAL - Close window"
	public void _closeWindow(){
		// Item box canvases
		/*for(int i = 1; i <= 6; i++){
			GameObject.Find ("C_ItemBox_P1_0" + i.ToString ()).GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("C_ItemBox_P2_0" + i.ToString ()).GetComponent<Canvas> ().enabled = false;
		}*/

		MG_Globals.I.pause_windowOpen = false;
		MG_ControlSounds.I._playSound(2, 0, 0, false);

		go_obj.SetActive (false);
		go_menu.SetActive (false);
		c_item.SetActive (false);
		c_options.SetActive (false);

		_goldUI_update ();
	}
	#endregion

	#region "For Objectives"
	public Canvas c_obj;

	public void _startObj(){
		go_obj.SetActive (false);
	}

	public void _show_objectives(){
		// Conditions
		if(/*MG_Globals.I.pause_uiMove || */MG_Globals.I.pause_windowOpen || MG_Globals.I.pause_gamePause || MG_Globals.I.pause_gameOver || MG_UIControl_Popup.I.inPopup)		return;
		//if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum && PlayerPrefs.GetInt ("isMultiplayer") != 1) 		return;

		go_obj.SetActive (true);
		MG_ControlSounds.I._playSound(2, 0, 0, false);
	}
	#endregion

	#region "For Items"
	public GameObject c_item;

	public void _startItem(){
		c_item.SetActive (true);
		// Item box canvases
		for(int i = 1; i <= 6; i++){
			GameObject.Find ("C_ItemBox_P1_0" + i.ToString ()).GetComponent<Canvas> ().enabled = false;
			GameObject.Find ("C_ItemBox_P2_0" + i.ToString ()).GetComponent<Canvas> ().enabled = false;
		}
		c_item.SetActive (false);
	}

	public void _show_item(){
		// Conditions
		if(MG_Globals.I.pause_uiMove || MG_Globals.I.pause_windowOpen || MG_Globals.I.pause_gamePause || MG_Globals.I.pause_gameOver)		return;
		if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

		c_item.SetActive (true);
		// Item box canvases
		for(int i = 1; i <= 6; i++){
			GameObject.Find ("C_ItemBox_P1_0" + i.ToString ()).GetComponent<Canvas> ().enabled = true;
			GameObject.Find ("C_ItemBox_P2_0" + i.ToString ()).GetComponent<Canvas> ().enabled = true;
		}
		MG_UIControl_Items.I._updateItemsUI ();
		_goldUI_update ();

		MG_Globals.I.pause_windowOpen = true;
	}
	#endregion

	#region "For Menu"
	public Canvas c_menu;

	public void _startMenu(){
		go_menu.SetActive (false);
	}

	public void _show_menu(){
		// Conditions
		if(/*MG_Globals.I.pause_uiMove || */MG_Globals.I.pause_windowOpen || MG_Globals.I.pause_gamePause || MG_Globals.I.pause_gameOver || MG_UIControl_Popup.I.inPopup)		return;
		//if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum && PlayerPrefs.GetInt ("isMultiplayer") != 1) 		return;

		go_menu.SetActive (true);
		MG_ControlSounds.I._playSound(2, 0, 0, false);

		MG_Globals.I.pause_windowOpen = true;
	}

	public void _quit_mission(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		MG_UIControl_Popup.I._show (2);
	}
	#endregion

	#region "For Options"
	public GameObject c_options;
	public Toggle tog_sound, tog_music;
	public bool disableSoundToggle;

	public void _startOptions(){
		c_options.SetActive (false);

		disableSoundToggle = true;
		tog_sound.isOn = MG_Globals.I.opt_sound;
		tog_music.isOn = MG_Globals.I.opt_music;
		disableSoundToggle = false;
	}

	public void _show_options(){
		c_options.SetActive (true);
		MG_ControlSounds.I._playSound(2, 0, 0, false);

		MG_Globals.I.pause_windowOpen = true;
	}

	public void _toggle_sound(){
		if (disableSoundToggle) return;

		MG_Globals.I.opt_sound = !MG_Globals.I.opt_sound;
		PlayerPrefs.SetInt ("opt_sound", (MG_Globals.I.opt_sound) ? 1 : 0);
		MG_ControlSounds.I._playSound(2, 0, 0, false);
	}
		
	public void _toggle_music(){
		if (disableSoundToggle) return;

		MG_Globals.I.opt_music = !MG_Globals.I.opt_music;
		PlayerPrefs.SetInt ("opt_music", (MG_Globals.I.opt_music) ? 1 : 0);
		if(MG_Globals.I.opt_music) MG_ControlSounds.I._playMusic ();
		else MG_ControlSounds.I._stopMusic ();
	}

	public void _hide_options(){
		c_options.SetActive (false);
		MG_ControlSounds.I._playSound(2, 0, 0, false);
	}
	#endregion

	#region "For Timer UI"
	public Text t_whosTurn, t_timer;
	public Image i_timer;
	public Sprite clockTurn_red, clockTurn_blue;

	public void _start_TimerUI(){
		t_whosTurn 			= GameObject.Find ("T_WhosTurn").GetComponent<Text> ();
		t_timer 			= GameObject.Find ("T_TimeTurn").GetComponent<Text> ();
		i_timer 			= GameObject.Find ("I_Clock").GetComponent<Image> ();
	}

	public void _updateWhosTurn(){
		if (MG_Globals.I.curPlayerOnTurn == MG_Globals.I.curPlayerNum) {
			t_whosTurn.text = "Your Turn";
			t_whosTurn.color = new Color(0f, 0f, 0f);
			i_timer.sprite = clockTurn_blue;

			MG_UIControl_TurnChange.I._show ("Your Turn");
			MG_ControlCursor.I._showCursor ();
			MG_UIControl_Command.I._tweenIn ();
		}else if(MG_ControlPlayer.I._getIsEnemy(MG_Globals.I.curPlayerNum, MG_Globals.I.curPlayerOnTurn)){
			t_whosTurn.text = "Enemy's Turn";
			t_whosTurn.color = new Color(1f, 1f, 1f);
			i_timer.sprite = clockTurn_red;

			MG_UIControl_TurnChange.I._show ("Enemy Turn");
			MG_ControlCursor.I._hideCursor ();
			MG_UIControl_Command.I._tweenOut ();
		}else{
			t_whosTurn.text = "Ally's Turn";
			t_whosTurn.color = new Color(0f, 0f, 0f);
			i_timer.sprite = clockTurn_blue;

			MG_UIControl_TurnChange.I._show ("Ally's Turn");
			MG_ControlCursor.I._hideCursor ();
			MG_UIControl_Command.I._tweenOut ();
		}
	}
	#endregion

	#region "For Gold UI"
	public Text t_gold, t_goldGain, t_recruits, t_recruitsGain;
	public RectTransform rt_goldGain, rt_recruitsGain;
	public GameObject c_gold;

	private float Y_START_GOLD, Y_END_GOLD, Y_START_RECRUITS, Y_END_RECRUITS;

	public void _goldUI_start(){
		t_gold = GameObject.Find ("T_Gold").GetComponent<Text> ();
		t_goldGain = GameObject.Find ("T_GoldGain").GetComponent<Text> ();
		t_goldGain.enabled = false;
		c_gold = GameObject.Find ("I_GoldBG");
		rt_goldGain = GameObject.Find ("T_GoldGain").GetComponent<RectTransform> ();
		rt_recruitsGain = GameObject.Find ("T_RecruitsGain").GetComponent<RectTransform> ();
		t_recruits = GameObject.Find ("T_Recruits").GetComponent<Text> ();
		t_recruitsGain = GameObject.Find ("T_RecruitsGain").GetComponent<Text> ();
		t_recruitsGain.enabled = false;
		
		Y_START_GOLD = rt_goldGain.localPosition.y;
		Y_END_GOLD = rt_goldGain.localPosition.y - 25;
		Y_START_RECRUITS = rt_recruitsGain.localPosition.y;
		Y_END_RECRUITS = rt_recruitsGain.localPosition.y - 25;

		_goldUI_update ();
	}

	public void _goldUI_update(){
		if (GameObject.Find ("C_TopBar") == null) return;

		t_gold.text = MG_ControlPlayer.I._getLocalPlayer ().gold.ToString();
		t_recruits.text = MG_ControlPlayer.I._getLocalPlayer ().recruits.ToString();
	}

	public void _goldGain(int goldGain){
		if (goldGain == 0) 	return;

		t_goldGain.enabled = true;
		rt_goldGain.localPosition = new Vector3(rt_goldGain.localPosition.x, Y_START_GOLD, rt_goldGain.localPosition.z);
		t_goldGain.text = ((goldGain < 0) ? "" : "+") + goldGain.ToString ();
		_tween_goldGain ();
		
		_goldUI_update ();
	}

	public void _tween_goldGain(){
		float firstPos = Y_START_GOLD;
		float targetPos = Y_END_GOLD;
		float duration = 1.5f;

		gameObject.Tween("uiMove_gold", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_goldGain.localPosition = new Vector3(rt_goldGain.localPosition.x, t.CurrentValue, rt_goldGain.localPosition.z);
		}, (t) =>{
			// completion
			rt_goldGain.localPosition = new Vector3(rt_goldGain.localPosition.x, t.CurrentValue, rt_goldGain.localPosition.z);
			t_goldGain.enabled = false;
		});
	}
	
	public void _recruitsGain(int goldGain){
		if (goldGain == 0) 	return;

		t_recruitsGain.enabled = true;
		rt_goldGain.localPosition = new Vector3(rt_recruitsGain.localPosition.x, Y_START_RECRUITS, rt_recruitsGain.localPosition.z);
		t_recruitsGain.text = ((goldGain < 0) ? "" : "+") + goldGain.ToString ();
		_tween_recruitsGain ();
		
		_goldUI_update ();
	}

	public void _tween_recruitsGain(){
		float firstPos = Y_START_RECRUITS;
		float targetPos = Y_END_RECRUITS;
		float duration = 1.5f;

		gameObject.Tween("uiMove_rec", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_recruitsGain.localPosition = new Vector3(rt_recruitsGain.localPosition.x, t.CurrentValue, rt_recruitsGain.localPosition.z);
		}, (t) =>{
			// completion
			rt_recruitsGain.localPosition = new Vector3(rt_recruitsGain.localPosition.x, t.CurrentValue, rt_recruitsGain.localPosition.z);
			t_recruitsGain.enabled = false;
		});
	}
	#endregion
}
