using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;

public class EditorMainGame : EditorWindow {
	[MenuItem("Map Editor/Editor Windows/MainGame Editor")]
	public static void _showWindow(){
		EditorWindow.GetWindow (typeof(EditorMainGame));
	}

	string 							terClass, terType, doodClass, doodType, unitClass, unitType, mapName;

	// Index of pop-up list
	int ind_terName = 0, ind_doodName = 0, ind_unitName = 0, ind_mapNum = 0;

	// Editor
	bool editorMode;
	string brushMode;

	#region "Pop-up lists and index"
	// Brush Types
	string[] optionsBT = new string[] {
		"NONE", "Terrain", "Doodad", "Units"
	};

	#region "LIST - Terrain"
	// Terrain Tiles
	string[] optionsTer = new string[] {
		"Grass-Summer", "Cliff-Summer"
	};
	List<string> terrainNames = new List<string>();
	public List<string> _getTerrainNames(string curTerType){
		List<string> retVal = new List<string> ();

		switch (curTerType) {
			case "Grass-Summer": retVal.AddRange ( new string[]{"grass01", "grass02"}); break;
			case "Cliff-Summer": retVal.AddRange ( new string[]{"cliff01", "cliff02", "cliff03", "cliff04", "cliff05", "cliff06", "cliff07", "cliff08", "cliff09", "cliff10", "cliff11", "cliff12", "cliff13", "cliff14"}); break;
		}

		return retVal;
	}
	#endregion
	#region "LIST - Doodad"
	// Terrain Tiles
	string[] optionsDood = new string[] {
		"Trees", "Fauna", "Doodads", "Blockers"
	};
	List<string> doodNames = new List<string>();
	public List<string> _getDoodNames(string curDoodType){
		List<string> retVal = new List<string> ();

		switch (curDoodType) {
			case "Trees": retVal.AddRange ( new string[]{"treeSummer_001", "treeSummer_002", "treeWinter_001", "treeWinter_002", "treeSwamp_001", "treeSwamp_002"}); break;
			case "Fauna": retVal.AddRange ( new string[]{
				"grassSummer_001", "grassSummer_002", "grassSummer_003", "flowerSummer_001", "flowerSummer_002", 
				"grassWinter_001", "grassWinter_002", "grassWinter_003", "flowerWinter_001", "flowerWinter_002",
				"grassSwamp_001", "grassSwamp_002", "grassSwamp_003"
			}); break;
			case "Doodads": retVal.AddRange ( new string[]{
				"dood_carFront", "dood_lampPost", "dood_lampPost2",
				"dood_bed", "dood_box", "dood_box2", "dood_cabinet", "dood_chair", "dood_painting", "dood_pot", "dood_pot2",
				"dood_pottedPlant", "dood_table", "dood_window",
				"dood_swampProp1", "dood_swampProp2", "dood_swampProp3", "dood_swampProp4"
			}); break;
			case "Blockers": retVal.AddRange ( new string[]{
				"forest", "hills", "water", "building", "pathBlocker", "pathSightBlocker" 
			}); break;
		}

		return retVal;
	}
	#endregion
	#region "LIST - Units"
	// Terrain Tiles
	string[] optionsUnits = new string[] {
		"Heroes"
	};
	List<string> unitNames = new List<string>();
	public List<string> _getUnitNames(string curUnitType){
		List<string> retVal = new List<string> ();

		switch (curUnitType) {
			case "Heroes": retVal.AddRange ( new string[]{"testUnit001"}); break;
		}

		return retVal;
	}
	#endregion
	#region "LIST - Maps"
	// Terrain Tiles
	string[] optionsMaps = new string[] {
		"Stories01_mis01", "Stories01_mis02", "Stories01_mis03",
		"Stories02_mis01", "Stories02_mis02", "Stories02_mis03"
	};
	#endregion
	#endregion

	// Index of pop-up lists
	int ind_optBT = 0;
	int ind_terrainList = 0, ind_doodList = 0, ind_map = 0;
	string ind_unitOwner = "0";

	// Console
	string con_status = "";
	string ediConsole = "";

	#region "SWITCH - Turn on/off editor"
	private void _turnOnEditor(){
		try{
			Debug.Log("Editor Mode has started");
			MG_Globals.I.EDITOR_PARENT.SetActive(true);
			editorMode = true;
			MG_Globals.I.editorMode = true;

			_startHelper();
		}catch(Exception ex){
			Debug.Log ("Editor failed to start. Reason: " + ex.Message);
			_turnOffEditor (false);
		}
	}

	private void _turnOffEditor(bool crash = false){
		editorMode = false;
		MG_Globals.I.editorMode = false;
		MG_Globals.I.EDITOR_PARENT.SetActive (false);
	}
	#endregion

	#region "GUI Design"
	void OnGUI(){
		GUILayout.Label ("--------- Editor Mode: ---------", EditorStyles.boldLabel);
		if (GUILayout.Button ("Get Cursor Position")) {
			Debug.Log (MG_ControlCursor.I.posX.ToString () + ", " + MG_ControlCursor.I.posY.ToString ());
		}
		if (GUILayout.Button ("Pause Game Timer")) {
			MainGame.I.pauseTimer = true;
		}
		if (GUILayout.Button ("Start Editor Mode")) {
			if (!editorMode) {
				_turnOnEditor ();
			} else {
				_turnOffEditor ();
				Debug.Log ("Editor Mode is turned off.");
			}
		}

		#region "Editor Mode UI"
		if (editorMode) {
			ind_optBT = EditorGUILayout.Popup ("Brush Type", ind_optBT, optionsBT);
			brushMode = optionsBT [ind_optBT];
			_selectHelperSubCanvas (brushMode);		// Update editor helper sub-canvas

			#region "GUI - Terrain"
			if (brushMode == "Terrain") {
				GUILayout.Label ("Terrain Creation", EditorStyles.boldLabel);
				ind_terrainList = EditorGUILayout.Popup ("Terrain Type", ind_terrainList, optionsTer);
				terClass = optionsTer [ind_terrainList];

				terrainNames = _getTerrainNames (terClass);
				ind_terName = EditorGUILayout.Popup ("Terrain Name", ind_terName, terrainNames.ToArray ());
				if (ind_terName >= terrainNames.Count)
					ind_terName = 0;
				terType = terrainNames [ind_terName];

				if (GUILayout.Button ("Fill Map Terrain")) {
					foreach (MG_ClassTerrain tL in MG_Globals.I.terrains) {
						tL._changeTerrain (terType);
					}
				}
				GUILayout.Label ("-\tThis will turn all tiles into the selected terrain ");
				GUILayout.Label ("-\tPress 1 to place the terrain ");

				try {
					if (Application.isPlaying) {
						// Show selected terrain in helper
						GameObject dummy = MG_DB_Terrain.I._getSprite (terType);
						Sprite image = dummy.GetComponent<SpriteRenderer> ().sprite;
						iHlp_Terrain.sprite = image;
						DestroyImmediate (dummy);
					}
				} catch (Exception ex) {

				}
				#endregion
				#region "GUI - Doodad"
			} else if (brushMode == "Doodad") {
				GUILayout.Label ("Doodad Creation", EditorStyles.boldLabel);
				ind_doodList = EditorGUILayout.Popup ("Doodad Type", ind_doodList, optionsDood);
				doodClass = optionsDood [ind_doodList];

				doodNames = _getDoodNames (doodClass);
				ind_doodName = EditorGUILayout.Popup ("Doodad Name", ind_doodName, doodNames.ToArray ());
				if (ind_doodName >= doodNames.Count)
					ind_doodName = 0;
				doodType = doodNames [ind_doodName];

				GUILayout.Label ("-\tPress 1 to place the doodad ");

				try {
					if (Application.isPlaying) {
						// Show selected terrain in helper
						GameObject dummy = MG_DB_Doodads.I._getSprite (doodType);
						Sprite image = dummy.GetComponent<SpriteRenderer> ().sprite;
						iHlp_Doodad.sprite = image;
						DestroyImmediate (dummy);
					}
				} catch (Exception ex) {

				}
				#endregion
				#region "GUI - Units"
			} else if (brushMode == "Units") {
				GUILayout.Label ("Unit Creation", EditorStyles.boldLabel);
				ind_terrainList = EditorGUILayout.Popup ("Unit Type", ind_unitName, optionsUnits);
				unitClass = optionsUnits [ind_terrainList];

				unitNames = _getUnitNames (unitClass);
				ind_unitName = EditorGUILayout.Popup ("Unit Name", ind_unitName, unitNames.ToArray ());
				if (ind_unitName >= unitNames.Count)
					ind_unitName = 0;
				unitType = unitNames [ind_unitName];

				ind_unitOwner = EditorGUILayout.TextField ("Unit Owner", ind_unitOwner);

				GUILayout.Label ("-\tPress 1 to place the unit ");
			}
			#endregion
		}
		#endregion

		// Misc. Tools
		GUILayout.Label ("--------- Misc. Tools: ---------", EditorStyles.boldLabel);
		ediConsole = EditorGUILayout.TextField("Console Command: ", ediConsole);
		if (GUILayout.Button ("Trigger Console Command")) {
			#region "Console Commands"
			switch (con_status){
				#region "Blank Status"
				case "":
					#region "Force Multiplayer"
					if(ediConsole == "force_multiplayer"){
						PlayerPrefs.SetInt ("isMultiplayer", 1);
						Debug.Log("Multiplayer status forced!");
						return;
					}
					#endregion
					#region "Go To Scene"
					if(ediConsole.Contains("goToScene_")){
						string[] keyValue = ediConsole.Split(new string[]{"_"}, System.StringSplitOptions.None);
						string co_gts_scene = keyValue[1];
						int co_gts_multiForce = (keyValue[2] != null) ? 0 : 1;
						PlayerPrefs.SetInt ("isMultiplayer", co_gts_multiForce);

						Debug.Log("Going to target scene...");

						SceneManager.LoadScene (co_gts_scene);
						Debug.Log("Scene changed!");
						return;
					}
					#endregion
				break;
					#endregion
			}
			#endregion
		}
		ind_mapNum = EditorGUILayout.Popup ("Change Map", ind_mapNum, optionsMaps);
		mapName = optionsMaps [ind_mapNum];
		if (GUILayout.Button ("Change Map Now")) {
			Debug.Log ("Removing all Doodads...");
			GameObject CM_parentD = GameObject.Find("_DOODADS");
			Transform CM_childD;
			int CM_childCountD = CM_parentD.transform.childCount - 1;
			for (int lp = CM_childCountD; lp >= 0; lp--) {
				CM_childD = CM_parentD.transform.GetChild(lp);
				Destroy (CM_childD.gameObject);
			}

			Debug.Log ("Removing all Units...");
			GameObject CM_parentU = GameObject.Find("_UNITS");
			Transform CM_childU;
			int CM_childCountU = CM_parentU.transform.childCount - 1;
			for (int lp = CM_childCountU; lp >= 0; lp--) {
				CM_childU = CM_parentU.transform.GetChild(lp);
				Destroy (CM_childU.gameObject);
			}

			Debug.Log ("Changing map to " + mapName + "...");
			MG_Globals.I.curMap = mapName;
			MG_DB_Maps.I._createMap(MG_Globals.I.curMap);

			Debug.Log ("Map changed!");
		}

		// Cheats
		GUILayout.Label ("--------- Cheats: ---------", EditorStyles.boldLabel);
		if (GUILayout.Button ("Remove fog")) {
			MG_ControlFogOfWar.I.revealAll = true;
			Debug.Log ("Fog removed!");
		}
		if (GUILayout.Button ("High damage cheat")) {
			MG_CALC_Damage.I.CHEAT_HIGH_DAMAGE = true;
			Debug.Log ("Cheat enabled!");
		}
		if (GUILayout.Button ("Infinite move and attack")) {
			MG_ControlCommand.I.CHEAT_INFINTE_MOVE = true;
			Debug.Log ("Cheat enabled!");
		}
		if (GUILayout.Button ("Force Shuffle Deck")) {
			MG_UIControl_Cards.I.shuffleDeck ();
		}
		if (GUILayout.Button ("Refresh")) {
			foreach (MG_ClassUnit unit in MG_Globals.I.units) {
				if (unit.playerOwner == 1) {
					unit.action_move = true;
					unit.action_atk = true;
					unit.action_skill = true;
				}
			}
			Debug.Log ("Cheat enabled!");
		}
	}
	#endregion

	#region "Update();"
	void Update(){
		if (!editorMode) {
			return;
		}

		try{
			Vector3 gamePoint = MG_ControlCamera.I._getGamePoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
			int actPosX = (int)gamePoint.x, actPosY = (int)gamePoint.y;

			#region "INPUT - Command Button 1"
			if (MG_ControlEditor.I.isPressed_button1){
				#region "Terrain Brush"
				if(brushMode == "Terrain"){
					MG_ClassTerrain targetTile = MG_GetTerrain.I._pickTerrain(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
					targetTile._changeTerrain(terType);
				}
				#endregion
				#region "Doodad Brush"
				else if(brushMode == "Doodad"){
					MG_ControlDoodad.I._createDoodad(doodType, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
				}
				#endregion
				#region "Unit Brush"
				else if(brushMode == "Units"){
					MG_ControlUnits.I._createUnit(unitType, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, int.Parse(ind_unitOwner));
				}
				#endregion
			}
			#endregion
		}catch(Exception ex){
			Debug.Log ("Turning off editor mode. Reason: " + ex.Message);
			_turnOffEditor (true);
		}
	}
	#endregion

	#region "Helper UI Control"
	public Canvas helper, helperTerrain, helperDoodad;
	public Image iHlp_Terrain, iHlp_Doodad;

	public void _startHelper(){
		// For Helper
		//helper = GameObject.Find ("C_Helper").GetComponent<Canvas>();

		// For Terrain
		helperTerrain = GameObject.Find ("C_EditorTerrain").GetComponent<Canvas>();
		iHlp_Terrain = GameObject.Find ("I_EditorTerrain").GetComponent<Image> ();

		// For Doodad
		helperDoodad = GameObject.Find ("C_EditorDoodad").GetComponent<Canvas>();
		iHlp_Doodad = GameObject.Find ("I_EditorDoodad").GetComponent<Image> ();
	}

	public void _selectHelperSubCanvas(string canvasMode){
		//helper.enabled = false;
		helperTerrain.enabled = false;
		helperDoodad.enabled = false;

		switch (canvasMode) {
			//case "Helper": 								helper.enabled = true; break;
			case "Terrain":									helperTerrain.enabled = true; break;
			case "Doodad":									helperDoodad.enabled = true; break;
		}
	}
	#endregion
}
