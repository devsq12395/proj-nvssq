using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_MAPS_Stories01_mis03 : MonoBehaviour{
	public static MG_MAPS_Stories01_mis03 I;
	public void Awake(){ I = this; }

	public void _generateMap(){
		MG_Globals.I.mapLimitX = 17;
		MG_Globals.I.mapLimitY = 17;

		#region "Terrain and fog of war"
		string terType = "grass01";
		for (int x = -MG_Globals.I.mapLimitX; x <= MG_Globals.I.mapLimitX; x++) {
			for (int y = -MG_Globals.I.mapLimitY; y <= MG_Globals.I.mapLimitY; y++) {
				MG_ControlTerrain.I._createTerrain ("grass01", x, y, "Grass");
				if (terType == "grass01")
					terType = "grass02";
				else
					terType = "grass01";
			}
		}
		#endregion

		MG_DB_Maps.I._createGridMap(MG_Globals.I.curMap);
		#region "Generated Map Data"
		GameObject.Find("TableTop").transform.position = new Vector3(-0.297f, 8.71f, -300f);
		GameObject.Find("TableTop").transform.localScale = new Vector3(0.9646901f, 0.09056929f, -200f);
		GameObject.Find("TableTop2").transform.position = new Vector3(-0.387f, -8.3f, -300f);
		GameObject.Find("TableTop2").transform.localScale = new Vector3(0.9745809f, 0.09056929f, -200f);
		GameObject.Find("TableTop3").transform.position = new Vector3(9.16f, 0.24f, -300f);
		GameObject.Find("TableTop3").transform.localScale = new Vector3(0.9370484f, 0.09056929f, -200f);
		GameObject.Find("TableTop4").transform.position = new Vector3(-9.19f, 0.229f, -300f);
		GameObject.Find("TableTop4").transform.localScale = new Vector3(0.9442195f, 0.09056929f, -200f);
		

		MG_ControlDoodad.I._createDoodad("treeSummer_001", -16, -14, -8f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -13, -16, -6.5f, -7.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -11, -15, -5.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -4, -14, -2f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -4, -5, -2f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 7, -6, 3.5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 11, -9, 5.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 13, -8, 6.5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, -8, 4f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, -11, 8f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 17, -9, 8.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, -6, 8f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 17, -3, 8.5f, -1.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, -1, 8f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, -4, 8f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 17, 2, 8.5f, 1.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, 6, 8f, 3.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 17, 4, 8.5f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, 10, 8f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 17, 12, 8.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 14, 13, 7f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 10, 12, 5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 7, 13, 3.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, 8, 4f, 4.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 6, 7, 3f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 12, 8, 6f, 4.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 13, 7, 6.5f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 5, 9, 2.5f, 4.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 1, 11, 0.5f, 5.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -4, 11, -2f, 5.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -9, 9, -4.5f, 4.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -16, 7, -8f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -5, 7, -2.5f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, 8, -0.5f, 4.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 2, 6, 1f, 3.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 12, 4, 6f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, 2, 4f, 1.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 11, 0, 5.5f, 0.1500001f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -4, -10, -2f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, -6, -0.5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 1, -8, 0.5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, -11, -0.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 1, -14, 0.5f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 6, -16, 3f, -7.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 11, -15, 5.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 14, -16, 7f, -7.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 2, -16, 1f, -7.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 2, -5, 1f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 2, -11, 1f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -16, -3, -8f, -1.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -13, -4, -6.5f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -9, -3, -4.5f, -1.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -11, -2, -5.5f, -0.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -14, -2, -7f, -0.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -16, 4, -8f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -17, 1, -8.5f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -16, -1, -8f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -3, 5, -1.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 2, 8, 1f, 4.15f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -13, -13, -6.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -9, -10, -4.5f, -5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -4, -6, -2f, -3f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -1, -8, -0.5f, -4f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 1, -10, 0.5f, -5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -1, -13, -0.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 5, -15, 2.5f, -7.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 9, -15, 4.5f, -7.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 9, -9, 4.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 12, -6, 6f, -3f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 16, -8, 8f, -4f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 17, 1, 8.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 16, 3, 8f, 1.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 17, 9, 8.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 15, 12, 7.5f, 6f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 12, 13, 6f, 6.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 9, 13, 4.5f, 6.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 10, 8, 5f, 4f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 7, 7, 3.5f, 3.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 12, 7, 6f, 3.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 16, 13, 8f, 6.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 12, 2, 6f, 1f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 9, 0, 4.5f, 0f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", 12, -2, 6f, -1f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", 3, 4, 1.5f, 2f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", -3, 8, -1.5f, 4f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", 0, 5, 0f, 2.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", -16, 2, -8f, 1f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", -11, -4, -5.5f, -2f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", -15, -3, -7.5f, -1.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", -9, 7, -4.5f, 3.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", -16, 9, -8f, 4.5f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", -1, 11, -0.5f, 5.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -14, 0, -7f, 0f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -10, 4, -5f, 2f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -5, 5, -2.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 0, 7, 0f, 3.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 9, 4, 4.5f, 2f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 5, 4, 2.5f, 2f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 8, -1, 4f, -0.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 2, -7, 1f, -3.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 0, -5, 0f, -2.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 2, -13, 1f, -6.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -7, -14, -3.5f, -7f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -10, -15, -5f, -7.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -16, -10, -8f, -5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -7, -3, -3.5f, -1.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", -7, -7, -3.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 9, -13, 4.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 15, -13, 7.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -9, -1, -4.5f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -9, 1, -4.5f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, 4, -0.5f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, -2, 4f, -0.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, 0, 4f, 0.1500001f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, -9, -0.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, -14, -0.5f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, -5, -0.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("dood_crimsonGate4", 0, 7, 0.3039f, 7.561f);
		MG_ControlDoodad.I._createDoodad("dood_crimsonGate3", -6, -4, -6.1464f, 4.7989f);
		MG_ControlDoodad.I._createDoodad("dood_crimsonGate2", -7, 5, -7.53f, 5.06f);
		MG_ControlDoodad.I._createDoodad("dood_crimsonGate1", -6, 7, -6.26f, 7.595f);
		
		// Path blockers
		for(int x = -17; x <= 17; x++){
			for(int y = 14; y <= 16; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		}
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -17, 5, -8.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -16, 5, -8f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -15, 5, -7.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -14, 5, -7f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -13, 5, -6.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -12, 5, -6f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -11, 5, -5.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -10, 5, -5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -9, 5, -4.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -9, 5, -4.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, 5, -4f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 5, -3.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 6, -3.5f, 3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 7, -3.5f, 3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 8, -3.5f, 4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 9, -3.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -6, 9, -3f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -5, 9, -2.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -4, 9, -2f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -3, 9, -1.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, 9, -1f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -1, 9, -0.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 0, 9, 0f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 1, 9, 0.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 2, 9, 1f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 9, 1.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 8, 1.5f, 4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 7, 1.5f, 3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 6, 1.5f, 3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 7, 1.5f, 3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 5, 1.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 4, 5, 2f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 5, 5, 2.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 6, 5, 3f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, 5, 3.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 8, 5, 4f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 9, 5, 4.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 10, 5, 5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 11, 5, 5.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 12, 5, 6f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, 5, 6.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, 4, 6.5f, 2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, 3, 6.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, 3, 6.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, 2, 6.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, 1, 6.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, 0, 6.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, -1, 6.5f, -0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, -2, 6.5f, -1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, -3, 6.5f, -1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 12, -3, 6f, -1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 8, -3, 4f, -1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, -3, 3.5f, -1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, -2, 3.5f, -1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, -1, 3.5f, -0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, 0, 3.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, 1, 3.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, 2, 3.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, 3, 3.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 6, 3, 3f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 5, 3, 2.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 4, 3, 2f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 3, 1.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 2, 3, 1f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 1, 3, 0.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 0, 3, 0f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -1, 3, -0.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, 3, -1f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -3, 3, -1.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -4, 3, -2f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -5, 3, -2.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -6, 3, -3f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 3, -3.5f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, 3, -4f, 1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, 2, -4f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, 1, -4f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, 0, -4f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -1, -4f, -0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, 0, -4f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -1, -4f, -0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -2, -4f, -1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -3, -4f, -1.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -4, -4f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -5, -4f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -9, -5, -4.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -10, -5, -5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -11, -5, -5.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -12, -5, -6f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -13, -5, -6.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -14, -5, -7f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -15, -5, -7.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -16, -5, -8f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -17, -5, -8.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -15, -1f, -7.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -14, -1f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -13, -1f, -6.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -12, -1f, -6f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -11, -1f, -5.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -10, -1f, -5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -9, -1f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -8, -1f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -7, -1f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -6, -1f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -5, -1f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, -4, -1f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -1, -4, -0.5f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 0, -4, 0f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 1, -4, 0.5f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 2, -4, 1f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -4, 1.5f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -5, 1.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -6, 1.5f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -5, 1.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -7, 1.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -8, 1.5f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -9, 1.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -10, 1.5f, -5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -11, 1.5f, -5.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -12, 1.5f, -6f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -12, 1.5f, -6f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -13, 1.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, -14, 1.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 4, -14, 2f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 5, -14, 2.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 6, -14, 3f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, -14, 3.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 8, -14, 4f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 9, -14, 4.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 10, -14, 5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 11, -14, 5.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 12, -14, 6f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, -14, 6.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 14, -14, 7f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 15, -14, 7.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 16, -14, 8f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 17, -14, 8.5f, -7f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -17, -6, -8.5f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -16, -6, -8f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -15, -6, -7.5f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -14, -6, -7f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -13, -6, -6.5f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -12, -6, -6f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -11, -6, -5.5f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -10, -6, -5f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -9, -6, -4.5f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -6, -4f, -3f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -7, -4f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -9, -7, -4.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -10, -7, -5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -11, -7, -5.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -12, -7, -6f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -13, -7, -6.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -14, -7, -7f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -15, -7, -7.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -16, -7, -8f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -17, -7, -8.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -17, -8, -8.5f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -16, -8, -8f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -15, -8, -7.5f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -14, -8, -7f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -13, -8, -6.5f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -12, -8, -6f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -11, -8, -5.5f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -10, -8, -5f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -9, -8, -4.5f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -8, -4f, -4f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -8, -9, -4f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -9, -9, -4.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -10, -9, -5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -11, -9, -5.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -12, -9, -6f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -13, -9, -6.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -14, -9, -7f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -15, -9, -7.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -16, -9, -8f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -17, -9, -8.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 2, -3.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -6, 2, -3f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -5, 2, -2.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -4, 2, -2f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -3, 2, -1.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, 2, -1f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -1, 2, -0.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 0, 2, 0f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 1, 2, 0.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 2, 2, 1f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 2, 1.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 4, 2, 2f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 5, 2, 2.5f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 6, 2, 3f, 1f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 6, 1, 3f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 5, 1, 2.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 4, 1, 2f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 1, 1.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 2, 1, 1f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 1, 1, 0.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 0, 1, 0f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -1, 1, -0.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, 1, -1f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -3, 1, -1.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -4, 1, -2f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -5, 1, -2.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -6, 1, -3f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 1, -3.5f, 0.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -7, 0, -3.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -6, 0, -3f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -6, 0, -3f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -5, 0, -2.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -4, 0, -2f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -3, 0, -1.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -2, 0, -1f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", -1, 0, -0.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 0, 0, 0f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 1, 0, 0.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 2, 0, 1f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 3, 0, 1.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 4, 0, 2f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 5, 0, 2.5f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 6, 0, 3f, 0f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, -4, 3.5f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 8, -4, 4f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 8, -5, 4f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 7, -5, 3.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 12, -4, 6f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, -4, 6.5f, -2f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 13, -5, 6.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("pathSightBlocker", 12, -5, 6f, -2.5f);
		#endregion

		#region "Units"
		MG_ControlUnits.I._createUnit("CampBlue", -11, -10, 1);
		MG_ControlUnits.I._createUnit("CampBlue", -9, -10, 1);
		MG_ControlUnits.I._createUnit("BarracksBlue", -11, -13, 1);
		MG_ControlUnits.I._createUnit("SpellTowerBlue", -14, -13, 1);

		MG_ControlUnits.I._createUnit("May", -8, -11, 1);
		MG_Globals.I.players[1]._HERO_setSummonedState("May", true);
		MG_ControlUnits.I._createUnit("Officer", -10, -11, 1);
		MG_ControlUnits.I._createUnit("Apprentice", -10, -12, 1);
		MG_ControlUnits.I._createUnit("ArcanyteTruck", -13, -11, 1);
		MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "21"});
		MG_ControlUnits.I._createUnit("ArcanyteTruck", -16, -11, 1);
		MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "21"});
		#endregion
		
		#region "First enemies"
		// SPAWN ENEMIES
		MG_ControlUnits.I._createUnit("GoblinArcher", -14, 4, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_ControlUnits.I._createUnit("GoblinArcher", -11, 4, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_ControlUnits.I._createUnit("Goblin", -13, 3, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_ControlUnits.I._createUnit("Goblin", -14, 4, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		
		MG_ControlUnits.I._createUnit("GoblinArcher", -1, 8, 2); // no waypoint needed
		MG_ControlUnits.I._createUnit("GoblinArcher", 2, -6, 2); // no waypoint needed
		MG_ControlUnits.I._createUnit("GoblinArcher", -9, 2, 2); // no waypoint needed
		MG_ControlUnits.I._createUnit("GoblinArcher", -4, 4, 2); // no waypoint needed
		MG_ControlUnits.I._createUnit("GoblinArcher", 3, 4, 2); // no waypoint needed
		
		MG_ControlUnits.I._createUnit("GoblinArcher", -6, -4, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", 5, -2, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", 5, -5, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", -3, -1, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", 14, 12, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", 12, 10, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", 9, 12, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", 6, 12, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("GoblinArcher", 5, 8, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		
		MG_ControlUnits.I.createHeroSpirit(0, -2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I.createHeroSpirit(11, -7); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I.createHeroSpirit(-2, 11); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I.createHeroSpirit(11, 10); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I.createHeroSpirit(15, -1); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I.createHeroSpirit(10, 0); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I.createHeroSpirit(9, 10); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;

		MG_ControlUnits.I._createUnit("Hellhound", 10, -9, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("Hellhound", 11, -10, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		MG_ControlUnits.I._createUnit("Hellhound", 10, -12, 2); // station this
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;
		
		MG_ControlUnits.I._createUnit("Treasure", -2, 8, 2);
		MG_ControlUnits.I._createUnit("Treasure", -16, 3, 2);
		MG_ControlUnits.I._createUnit("Treasure", 1, -15, 2);
		MG_ControlUnits.I._createUnit("Treasure", -1, -7, 2);
		MG_ControlUnits.I._createUnit("Treasure", 12, -15, 2);
		MG_ControlUnits.I._createUnit("Treasure", 5, -4, 2);
		MG_ControlUnits.I._createUnit("Treasure", 15, -3, 2);
		MG_ControlUnits.I._createUnit("Treasure", 10, 2, 2);
		#endregion
		
		#region "Campaign Events"
		MG_ControlEvents.I._addEvent ("TurnNumberReached", new string[]{"10", "23"}); // target turn num is 1st par, output is 2nd par
		MG_ControlEvents.I._addEvent ("TurnNumberReached", new string[]{"26", "23"}); // target turn num is 1st par, output is 2nd par
		MG_ControlEvents.I._addEvent ("TurnNumberReached", new string[]{"52", "23"}); // target turn num is 1st par, output is 2nd par
		#endregion
	}

	public void summonHero(int player, int index, int posX, int posY){
		string hName = MG_Globals.I.players[player].hero_name[index];
		if(hName != "NONE"){
			MG_Globals.I.players[player].hero_isSummoned[index] = true;
			MG_ControlUnits.I._createUnit(hName, posX, posY, player);
		}
	}

	public void _moveCursor(){
		MG_ControlCursor.I._moveCursor(-4f, 5.5f, true);
		MG_ControlCamera.I._moveCamera(-2.547f, -5.451f);
	}
}
