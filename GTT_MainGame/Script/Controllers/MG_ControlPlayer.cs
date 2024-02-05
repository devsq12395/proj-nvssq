using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MG_ControlPlayer : MonoBehaviour {
	public static MG_ControlPlayer I;
	public void Awake(){ I = this; }

	public int maxPlayers, turnNumber;

	public void _start(){
		// Defaults
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			MG_Globals.I.curPlayerNum = PlayerPrefs.GetInt ("playerNum");
		} else {
			MG_Globals.I.curPlayerNum = 1;

			PlayerPrefs.SetString ("playerName_p1", PlayerPrefs.GetString ("playerName"));
			PlayerPrefs.SetString ("playerName_p2", "Computer");
		}
		maxPlayers = 2;

		//////////////////////////////// Load players //////////////////////////////////
		// Player 0 - Unused
		MG_Globals.I.players.Add(new MG_ClassPlayer(0, new int[]{0}));
		// Player 1 - Main player
		MG_Globals.I.players.Add(new MG_ClassPlayer(1, new int[]{0}, 
			(PlayerPrefs.GetInt ("isMultiplayer") == 2) ? PlayerPrefs.GetInt ("camp_curGold") : 600
			, PlayerPrefs.GetString ("playerName_p1")));
		// Player 2 - Main player / CPU
		MG_Globals.I.players.Add(new MG_ClassPlayer(2, new int[]{0}, 13, PlayerPrefs.GetString ("playerName_p2")));
		// Player 3
		MG_Globals.I.players.Add(new MG_ClassPlayer(3, new int[]{0}));
		// Player 4
		MG_Globals.I.players.Add(new MG_ClassPlayer(4, new int[]{0}));
		// Player 5
		MG_Globals.I.players.Add(new MG_ClassPlayer(5, new int[]{0}));
		/////////////////////////// Finished Loading Players ///////////////////////////
	}

	#region "End Turn"
	public void _endTurn(){
		MG_ControlSounds.I._playSound(12, 0, 0, false);

		// Turn number
		turnNumber++;
		MG_Globals.I.curPlayerOnTurn++;
		MG_Globals.I.turnNum++;
		if (MG_Globals.I.curPlayerOnTurn > maxPlayers) 		MG_Globals.I.curPlayerOnTurn = 1;
		MG_Globals.I.killStreak = 0;

		// Event system - check for "Turn Number Reached" events
		MG_ControlEvents.I.turnNumberReached (turnNumber);

		// CAMPAIGN - Reduce Score
		if(PlayerPrefs.GetInt ("isMultiplayer") == 2 && MG_Globals.I.curPlayerOnTurn == 2){
			MG_Globals.I.CAMP_score -= 10;
			Debug.Log("Current score is " + MG_Globals.I.CAMP_score.ToString());
		}

		// Timer
		MG_Globals.I.turnTimer = MG_Globals.I.turnTimerMax;

		// Units
		/*All Unit's end turn effect*/			MG_ControlUnits.I._endTurnEffect_allUnits ();
		for (int i = 0; i < MG_Globals.I.units.Count; i++) {
			// used for regen, etc...
			MG_Globals.I.units [i]._endTurn ();
		}

		// Aura
		//MG_ControlAura.I._reincludeBuffs(true);

		// Player
		for (int i = 1; i < MG_Globals.I.players.Count; i++) {
			MG_Globals.I.players [i]._heroEndTurn ();
		}

		// SFX
		for(int i = 0; i < MG_Globals.I.sfx.Count; i++){
			MG_Globals.I.sfx [i]._endTurn ();
		}
		for(int i = 0; i < MG_Globals.I.sfxTemp.Count; i++){
			MG_Globals.I.sfx [i]._endTurn ();
		}

		// Turn Change UI - Transferred at MG_UIControl_TopBar.I._updateWhosTurn();

		// Hide all windows
		MG_Globals.I.pause_windowOpen = false;
		MG_UIControl_Shop.I._hideWindow ();
		MG_UIControl_ItemUpgrade.I._hideWindow ();
		MG_UIControl_ItemCheck.I._hideWindow ();
		MG_UIControl_Skill.I._hide ();

		// Command
		MG_ControlTargeters.I._clearTargeters ();
		MG_Globals.I.mode = "in map";
		MG_Globals.I.curCommand = "in map";

		// AI Start
		MG_ControlAI.I._startTurn ();

		// Extras
		MG_ControlCursor.I._interact ();
		MG_UIControl_TopBar.I._updateWhosTurn ();
		/*Reset kill feed*/						MG_UIControl_KillFeed.I._resetKillFeed ();
	}
	#endregion

	#region "SKIN - Receive skin"
	public void shareSkin (){
		MG_Globals.I.players [MG_Globals.I.curPlayerNum].shareSkin ();
	}

	public void receiveSkin (int playerNum, string ownerName, int skinNum){
		MG_Globals.I.players [playerNum].setSkin (ownerName, skinNum);
		Debug.Log (playerNum.ToString () + ", " + ownerName + ", " + skinNum.ToString ());

		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(u.name == ownerName && u.playerOwner == playerNum){
				u.updateSkin (skinNum);
			}
		}

		foreach(MG_ClassUnit u in MG_Globals.I.unitsTemp){
			if(u.name == ownerName && u.playerOwner == playerNum){
				u.updateSkin (skinNum);
			}
		}
	}
	#endregion

	//Includes
	//	- _getIsEnemy()						- Return true if the two players are enemies
	//  - _getLocalPlayer()					- Return the local player as MG_ClassPlayer
	#region "Tools"
	public bool _getIsEnemy(int attackingPlayer, int defendingPlayer){
		if (attackingPlayer > 5 || defendingPlayer > 5)
			return false;
		if (attackingPlayer < 0 || defendingPlayer < 0)
			return false;

		MG_ClassPlayer p1 = MG_Globals.I.players[0], p2 = MG_Globals.I.players[0];
		for (int i = 0; i < 5; i++) {
			if (MG_Globals.I.players [i].playerNum == attackingPlayer) {
				p1 = MG_Globals.I.players [i];
				continue;
			}

			if (MG_Globals.I.players [i].playerNum == defendingPlayer) {
				p2 = MG_Globals.I.players [i];
				continue;
			}
		}

		if (p1.allies.Contains (p2.playerNum)) {
			return false;
		} else {
			return true;
		}
	}

	public MG_ClassPlayer _getLocalPlayer(){
		return MG_Globals.I.players[MG_Globals.I.curPlayerNum];
	}
	#endregion
}
