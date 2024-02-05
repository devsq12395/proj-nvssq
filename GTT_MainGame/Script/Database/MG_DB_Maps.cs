using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Maps : MonoBehaviour {
	public static MG_DB_Maps I;
	public void Awake(){ I = this; }

	#region "Map Script"
	public void _createMap(string mapName){
		switch (mapName) {
			/*Test Map*/					case "testMap001":MG_MAPS_TestMap001.I._generateMap (); break;
			/*Encounter*/					case "Encounter":MG_MAPS_Encounter.I._generateMap (); break;
			/*Frozen Sanctuary*/			case "Frozen Sanctuary":MG_MAPS_FivePines.I._generateMap (); break;
			/*Stories 1 Mission 1*/			case "Stories01_mis01":MG_MAPS_Stories01_mis01.I._generateMap (); break;
			/*Stories 1 Mission 2*/			case "Stories01_mis02":MG_MAPS_Stories01_mis02.I._generateMap (); break;
			/*Stories 1 Mission 3*/			case "Stories01_mis03":MG_MAPS_Stories01_mis03.I._generateMap (); break;
			/*Stories 2 Mission 1*/			case "Stories02_mis01":MG_MAPS_Stories02_mis01.I._generateMap (); break;
			/*Stories 2 Mission 2*/			case "Stories02_mis02":MG_MAPS_Stories02_mis02.I._generateMap (); break;
			/*Stories 2 Mission 3*/			case "Stories02_mis03":MG_MAPS_Stories02_mis03.I._generateMap (); break;
		}
	}
	#endregion

	#region "Cursor and camera data"
	public void _cursorData(string mapName){
		switch (mapName) {
			/*Encounter*/					case "Encounter":MG_MAPS_Encounter.I._moveCursor (); break;
			/*Frozen Sanctuary*/			case "Frozen Sanctuary":MG_MAPS_FivePines.I._moveCursor (); break;
			/*Stories 1 Mission 1*/			case "Stories01_mis01":MG_MAPS_Stories01_mis01.I._moveCursor (); break;
			/*Stories 1 Mission 2*/			case "Stories01_mis02":MG_MAPS_Stories01_mis02.I._moveCursor (); break;
			/*Stories 1 Mission 3*/			case "Stories01_mis03":MG_MAPS_Stories01_mis03.I._moveCursor (); break;
			/*Stories 2 Mission 1*/			case "Stories02_mis01":MG_MAPS_Stories02_mis01.I._moveCursor (); break;
			/*Stories 2 Mission 2*/			case "Stories02_mis02":MG_MAPS_Stories02_mis02.I._moveCursor (); break;
			/*Stories 2 Mission 3*/			case "Stories02_mis03":MG_MAPS_Stories02_mis03.I._moveCursor (); break;
		}
	}
	#endregion

	#region "Terrain GridMap GameObject"
	public GameObject gridMap_testMap01, gridMap_encounter, gridMap_fivePines, gridMap_story01map01, gridMap_story01map02, gridMap_story01map03,
		gridMap_story02map01, gridMap_story02map02, gridMap_story02map03;

	public void _createGridMap(string mapName){
		switch (mapName) {
			/*Test Map*/					case "testMap001": GameObject.Instantiate (gridMap_testMap01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Encounter*/					case "Encounter": GameObject.Instantiate (gridMap_encounter, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Frozen Sanctuary*/			case "Frozen Sanctuary": GameObject.Instantiate (gridMap_fivePines, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Stories 1 Mission 1*/			case "Stories01_mis01": GameObject.Instantiate (gridMap_story01map01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Stories 1 Mission 2*/			case "Stories01_mis02": GameObject.Instantiate (gridMap_story01map02, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Stories 1 Mission 3*/			case "Stories01_mis03": GameObject.Instantiate (gridMap_story01map03, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Stories 2 Mission 1*/			case "Stories02_mis01": GameObject.Instantiate (gridMap_story02map01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Stories 2 Mission 2*/			case "Stories02_mis02": GameObject.Instantiate (gridMap_story02map02, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
			/*Stories 2 Mission 3*/			case "Stories02_mis03": GameObject.Instantiate (gridMap_story02map03, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))); break;
		}
	}
	#endregion
}
