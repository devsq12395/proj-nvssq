using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MainGame : MonoBehaviour {
	public static MainGame I;
	public void Awake(){ I = this; }

	void Start () {
		// Optimization
		Physics.autoSimulation = false;
		Physics2D.autoSimulation = false;

		MG_Globals.I._start ();
		MG_ControlPlayer.I._start ();
		MG_ControlCursor.I._start ();
		MG_ControlCamera.I._start ();
		MG_ControlSounds.I._start ();
		MG_Inputs.I._start ();
		MG_ControlBuffs.I._start ();
		MG_ControlNetwork.I._start ();
		MG_ControlEvents.I._start ();
		MG_ControlItems.I._start ();
		MG_ControlUnits.I._start ();
		MG_ControlAura.I._start ();
		MG_GetUnit.I.start ();
		MG_CALC_Random.I.start ();
		MG_ControlSFX_TextEffect.I.init ();

		MG_DB_ItemShop.I._start ();
		MG_DB_UnitShop.I.start ();
		MG_DB_Units_MiniWW2.I.start ();

		MG_UIControl_UnitData.I._start ();
		MG_UIControl_UnitStats.I._start ();
		MG_UIControl_Command.I._start ();
		//MG_UIControl_DebugUI.I._start ();
		MG_UIControl_Buffs.I._start ();
		MG_UIControl_Skill.I._start ();
		MG_UIControl_TopBar.I._start ();
		MG_UIControl_Dialog.I._start ();
		MG_UIControl_Items.I._start ();
		MG_UIControl_Shop.I._start ();
		MG_UIControl_ItemCheck.I._start ();
		MG_UIControl_ItemUpgrade.I._start ();
		MG_UIControl_Announcer.I._start ();
		MG_UIControl_KillFeed.I._start ();
		MG_UIControl_TurnChange.I._start ();
		MG_UIControl_Summon.I._start ();
		MG_UIControl_Chat.I._start ();
		MG_UIControl_Popup.I._start ();
		MG_UIControl_UseSkill.I._start ();
		MG_UIControl_UseSpell.I._start ();
		MG_UIControl_Streak.I._start ();
		MG_UIControl_Barracks.I.start ();
		MG_UIControl_SpellTower.I.start ();
		MG_UIControl_Cards.I.start ();
		MG_UIControl_Action.I.start ();
		MG_UIControl_Tooltip.I.start ();
		MG_UIControl_UnitShop.I.start ();

		MG_ControlAds.I._start ();

		// Generate AI deck
		if (PlayerPrefs.GetInt ("isMultiplayer") != 1) {
			// 30 gold starting cheat for AI
			MG_Globals.I.players[2].gold = 30;
		}

		// Create first unit
		MG_ControlUnits.I._createUnit_Infantry ("testUnit001", -500, -500, 1);
		MG_Globals.I.units.Add (MG_Globals.I.unitsTemp[0]);
		MG_Globals.I.unitsTemp.RemoveAt (0);

		// Create Map
		MG_DB_Maps.I._createMap(MG_Globals.I.curMap);

		// Set current game data
		MG_Globals.I.turnTimerMax = 180;
		MG_Globals.I.turnTimer = MG_Globals.I.turnTimerMax;

		// Turn AI on/off
		MG_ControlAI.I._setup ();

		// Pause while preloading
		MG_Globals.I.pause_gamePause = true;

		// Test
		MG_ControlEvents.I._addEvent ("Wait", new string[]{"1", "1"});

		/*Calculate fog of war*/			MG_ControlFogOfWar.I._calculateReveal();

		// Update whos turn ui if not in dialog
		if(!MG_UIControl_Dialog.I.isOnDialog) MG_UIControl_TopBar.I._updateWhosTurn ();
	}

	public void Ready(){
		// Start game signal
		MG_UIControl_Curtain.I._start ();

		// Move cursor
		MG_DB_Maps.I._cursorData(MG_Globals.I.curMap);
		if (MG_Globals.I.curPlayerNum != 1) {
			MG_ControlCursor.I._hideCursor ();
		}

		// Share skin
		MG_ControlPlayer.I.shareSkin ();

		// Check for starting dialog
		MG_UIControl_Dialog.I.checkForStartDialog();

		// Test Dialog
		// MG_UIControl_Dialog.I._initDialog (1);
	}

	private bool READY = true;
	void Update () {
		// Ready function
		if (READY) {
			Ready ();
			READY = false;
		}

		// Major pause
		if(MG_Globals.I.pause_gamePause || MG_Globals.I.pause_gameOver){
			return;
		}
		/*Inputs*/							MG_Inputs.I._update ();
		/*AI*/								MG_ControlAI.I._update (Time.deltaTime);
		/*Temp to main list*/				_tempToMainList ();
		/*Destroy listed objects*/			_destroyListed ();
		/*Sounds*/							MG_ControlSounds.I._update (Time.deltaTime);
		/*Control Cursor*/					MG_ControlCursor.I.update ();

		// UI UPDATES
		/*Dialog update*/					MG_UIControl_Dialog.I._drawDialogFrame ();
		/*Dialog input*/					MG_UIControl_Dialog.I._pressConfirm ();
		/*Announcer*/						MG_UIControl_Announcer.I._update (Time.deltaTime);
		/*Use skill*/						MG_UIControl_UseSkill.I._update (Time.deltaTime);
		/*Use spell*/						MG_UIControl_UseSpell.I._update (Time.deltaTime);
		/*Streak*/							MG_UIControl_Streak.I._update (Time.deltaTime);
		/*Cards UI*/						MG_UIControl_Cards.I.update (Time.deltaTime);
		/*Action Bar*/						MG_UIControl_Action.I.update ();
		/*Tooltip*/							MG_UIControl_Tooltip.I.update ();
		
		// MAIN GAME UPDATES
		/*Update objects*/					_updateObjects();
		/*Aura*/							if(MG_ControlAura.I.runTemp)	MG_ControlAura.I._reincludeBuffs(false);
		/*Shake unit sprites*/				MG_ControlUnits.I._shakeSprites (Time.deltaTime);
		/*Update pause duration*/			_updatePauseDuration (Time.deltaTime);
		/*Targeter scake*/					MG_ControlTargeters.I._update_targeterScale ();
		/*Music*/							MG_ControlSounds.I._musicPerFrame(Time.deltaTime);
		
		/*Custom Events*/
		if (Time.timeScale > 0) 			MG_ControlEvents.I._update (Time.deltaTime);
		else 								MG_ControlEvents.I._update (0.02f);

		/*Turn timer*/						_turnTimer (Time.deltaTime);
	}

	#region "Turn Timer"
	public bool pauseTimer = false;
	public void _turnTimer(float deltaTime){
		if (pauseTimer) return;

		// Conditions for timer
		if(MG_Globals.I.pause_uiMove || MG_Globals.I.pause_windowOpen)		return;
		if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
			return;
		}
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1 && MG_Globals.I.curPlayerNum != MG_Globals.I.curPlayerOnTurn) {
			MG_UIControl_TopBar.I.t_timer.text = "";
			return;
		}

		MG_Globals.I.turnTimer -= deltaTime;

		if (MG_Globals.I.turnTimer <= 0) {
			if (MG_Globals.I.curPlayerNum == MG_Globals.I.curPlayerOnTurn) {
				MG_Globals.I.turnTimer = 0;

				MG_ControlPlayer.I._endTurn ();
			} else {
				MG_Globals.I.turnTimer = 0;
				MG_UIControl_TopBar.I.t_timer.text = "0";
			}
		}

		TimeSpan timeSpan = TimeSpan.FromSeconds ((double)Mathf.Floor (MG_Globals.I.turnTimer));
		MG_UIControl_TopBar.I.t_timer.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
	}
	#endregion

	#region "Update Objects"
	public void _updateObjects(){
		// Units
		for(int i = 0; i < MG_Globals.I.units.Count; i++){
			MG_Globals.I.units [i]._update (Time.deltaTime);
		}

		// Sfx
		for(int i = 0; i < MG_Globals.I.sfx.Count; i++){
			MG_Globals.I.sfx [i]._update (Time.deltaTime);
		}

		// Missile
		for(int i = 0; i < MG_Globals.I.missiles.Count; i++){
			MG_Globals.I.missiles [i]._update ();
		}
	}
	#endregion
	#region "Update Pause Duration"
	public void _updatePauseDuration(float deltaTime){
		if (MG_Globals.I.pause_objMove_dur.Count <= 0)
			return;

		List<int> i = new List<int> ();

		for(int x = MG_Globals.I.pause_objMove_dur.Count-1; x >= 0; x--){
			MG_Globals.I.pause_objMove_dur[x] -= deltaTime;

			if(MG_Globals.I.pause_objMove_dur[x] <= 0){
				i.Add (x);
			}
		}
		foreach (int q in i) {
			MG_Globals.I.pause_objMove_dur.RemoveAt(i[q]);
		}
	}
	#endregion

	#region "Temp to main list"
	public void _tempToMainList(){
		// Units
		if(MG_Globals.I.unitsTemp.Count > 0){
			MG_Globals.I.units.AddRange (MG_Globals.I.unitsTemp);
			MG_Globals.I.unitsTemp.Clear ();
		}

		// Targeters
		if(MG_Globals.I.targetersTemp.Count > 0){
			MG_Globals.I.targeters.AddRange (MG_Globals.I.targetersTemp);
			MG_Globals.I.targetersTemp.Clear ();
		}

		// Missiles
		if(MG_Globals.I.missilesTemp.Count > 0){
			MG_Globals.I.missiles.AddRange (MG_Globals.I.missilesTemp);
			MG_Globals.I.missilesTemp.Clear ();
		}

		// SFx
		if(MG_Globals.I.sfxTemp.Count > 0){
			MG_Globals.I.sfx.AddRange (MG_Globals.I.sfxTemp);
			MG_Globals.I.sfxTemp.Clear ();
		}
	}
	#endregion
	#region "Destroy listed objects"
	public void _destroyListed(){
		/*Units*/					MG_ControlUnits.I._destroyListed ();
		/*Targeters*/				MG_ControlTargeters.I._destroyListed ();
		/*SFX*/ 					MG_ControlSFX.I._destroyListed ();
		/*Missile*/					MG_ControlMissile.I._destroyListed ();
		/*Events*/					MG_ControlEvents.I._destroyListed ();
		/*Buffs (per unit*/			MG_ControlBuffs.I._destroyListed ();
		/*Aura*/					MG_ControlAura.I._destroyListed ();
	}
	#endregion
}
