using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Globals : MonoBehaviour {
	public static MG_Globals I;
	public void Awake(){ I = this; }

	// CONSTANTS declared at _delcareConstants()
	public int MAX_MS;

	public bool DEBUG_DEV_MODE;
	public GameObject EDITOR_PARENT;

	public List<MG_ClassUnit> units, unitsTemp;
	public List<MG_ClassTerrain> terrains;
	public List<MG_ClassTargeter> targeters, targetersTemp;
	public List<MG_ClassDoodad> doodads;
	public List<MG_ClassMissile> missiles, missilesTemp;
	public List<MG_ClassSFX> sfx, sfxTemp;
	public List<MG_ClassSightBlock> sightBlocker;

	// Options
	public bool opt_sound, opt_music;

	// Game
	public string curMap;

	// Player and turns
	public int curPlayerNum; // Assigns player number for multiplayer, defined on MG_ControlPlayer
	public int curPlayerOnTurn = 1;
	public int turnNum = 1;
	public List<MG_ClassPlayer> players;
	public float turnTimer, turnTimerMax;

	// Streak
	public bool firstBlood = false;
	public int killStreak = 0;

	public int mapLimitX, mapLimitY;

	// Pause modes
	public bool pause_uiMove = false, pause_windowOpen = false, pause_itemBuying = false, pause_gamePause = false, pause_gameOver = false;
	public List<float> pause_objMove_dur;			// Add a duration float if something is moving
													// This is a countdown that is subtracted at MainGame.cs
													// And an element is removed once it reaches 0

	// Select unit system
	public string mode = "in map", curCommand = "in map";
	/*
	 * 	LIST OF MODES: (This list can be found on MG_Globals and MG_ControlCommand
	 * 	
	 * 	in map					- selecting a unit to give order to
	 * 	aim friendly			- currently opn targeting mode with friendly targeters
	 */
	public MG_ClassUnit selectedUnit;
	public bool selectedUnit_exist;

	// Campaign
	public int CAMP_score, camp_curGold;

	// Misc
	public bool editorMode = false;

	// For multiplayer
	public PhotonRoom roomControl;
	public bool enemyDisconnect;

	public void _start(){
		_delcareConstants ();

		// Globals
		curMap = PlayerPrefs.GetString("game_mapName");

		// Lists
		units 						= new List<MG_ClassUnit> ();
		unitsTemp 					= new List<MG_ClassUnit> ();
		terrains 					= new List<MG_ClassTerrain> ();
		targeters 					= new List<MG_ClassTargeter> ();
		targetersTemp 				= new List<MG_ClassTargeter> ();
		doodads 					= new List<MG_ClassDoodad> ();
		players 					= new List<MG_ClassPlayer> ();
		missiles 					= new List<MG_ClassMissile> ();
		missilesTemp 				= new List<MG_ClassMissile> ();
		sfx 						= new List<MG_ClassSFX> ();
		sfxTemp 					= new List<MG_ClassSFX> ();
		sightBlocker 				= new List<MG_ClassSightBlock> ();

		pause_objMove_dur 			= new List<float> ();

		// Options
		opt_sound = (PlayerPrefs.GetInt("opt_sound") == 1) ? true : false;
		opt_music = (PlayerPrefs.GetInt("opt_music") == 1) ? true : false;

		// For multiplayer
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			roomControl = PhotonRoom.room;
		}

		// For campaign
		if (PlayerPrefs.GetInt ("isMultiplayer") == 2) {
			CAMP_score = PlayerPrefs.GetInt ("camp_score_" + PlayerPrefs.GetInt ("camp_Number").ToString()) + 1000;
		}
	}

	public void _delcareConstants(){
		MAX_MS 						= 10;
	}
}
