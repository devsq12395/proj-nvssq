using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_MAPS_FivePines : MonoBehaviour {
	public static MG_MAPS_FivePines I;
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

		//MG_ControlDoodad.I._createDoodad("treeWinter_002", 0, 0, 0f, 0.1500001f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", -1, 1, -0.5f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", 1, 1, 0.5f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", -1, -1, -0.5f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", 1, -1, 0.5f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", -7, 3, -3.5f, 1.65f);
		//MG_ControlDoodad.I._createDoodad("treeWinter_002", -6, 4, -3f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", -5, 5, -2.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", -7, -3, -3.5f, -1.35f);
		//MG_ControlDoodad.I._createDoodad("treeWinter_002", -6, -4, -3f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", -5, -5, -2.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", 7, -3, 3.5f, -1.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", 7, 3, 3.5f, 1.65f);
		//MG_ControlDoodad.I._createDoodad("treeWinter_002", 6, -4, 3f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", 5, -5, 2.5f, -2.35f);
		//MG_ControlDoodad.I._createDoodad("treeWinter_002", 6, 4, 3f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_002", 5, 5, 2.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -11, 4, -5.5f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -13, 6, -6.5f, 3.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 11, -4, 5.5f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 13, -6, 6.5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -11, -5, -5.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -13, -7, -6.5f, -3.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 11, 5, 5.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 13, 7, 6.5f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 15, 5, 7.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -15, -5, -7.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -17, -9, -8.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -11, -12, -5.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -14, -11, -7f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -12, -9, -6f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -12, -14, -6f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -16, -13, -8f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -14, -15, -7f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -16, -7, -8f, -3.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -15, -9, -7.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -17, -15, -8.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -16, -11, -8f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -13, -13, -6.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 11, 10, 5.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 14, 10, 7f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 14, 12, 7f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 15, 8, 7.5f, 4.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 17, 11, 8.5f, 5.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 16, 13, 8f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 12, 13, 6f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 14, 15, 7f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 16, 16, 8f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 11, 16, 5.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 17, 9, 8.5f, 4.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 8, 9, 4f, 4.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -8, -9, -4f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -5, -9, -2.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 5, 9, 2.5f, 4.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 5, 12, 2.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -5, -12, -2.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -5, -15, -2.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 5, 15, 2.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -3, -10, -1.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -3, -13, -1.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 3, 10, 1.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 3, 13, 1.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 3, -10, 1.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 5, -9, 2.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 3, -13, 1.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 5, -12, 2.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 3, -15, 1.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -3, 10, -1.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -3, 13, -1.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -3, 15, -1.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -5, 9, -2.5f, 4.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -5, 12, -2.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 7, -10, 3.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 7, -13, 3.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 7, -15, 3.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -7, 10, -3.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -7, 13, -3.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -7, 15, -3.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -12, 10, -6f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -11, 12, -5.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -11, 15, -5.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -13, 15, -6.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -16, 13, -8f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -14, 12, -7f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -15, 15, -7.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -17, 16, -8.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -17, 10, -8.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -15, 10, -7.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", -17, 14, -8.5f, 7.15f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 11, -10, 5.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 11, -13, 5.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 11, -15, 5.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 14, -10, 7f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 13, -9, 6.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 16, -10, 8f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 13, -13, 6.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 13, -11, 6.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 16, -13, 8f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 15, -15, 7.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 15, -12, 7.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 13, -15, 6.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 17, -15, 8.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeWinter_001", 17, -9, 8.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -13, 5, -6.380001f, 2.552f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -12, 4, -6.004002f, 2.197f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -12, 6, -6.117001f, 3.063f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -11, 6, -5.868f, 2.929f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -14, 6, -7f, 3.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -11, 3, -5.5f, 1.65f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -12, 5, -6f, 2.65f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -11, 6, -5.5f, 3.15f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -12, -6, -6f, -2.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -12, -5, -6f, -2.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -14, -6, -7f, -2.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -14, -7, -7f, -3.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -7, -5, -3.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -5, -4, -2.5f, -1.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -8, -2, -4f, -0.85f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", -4, -5, -2f, -2.35f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", 1, -2, 0.5f, -0.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", 0, -1, -0.09999847f, -0.3699999f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", 0, -2, 0f, -0.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -1, 0, -0.7400017f, 0.00999999f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 1, 0, 0.6599998f, -0.03999996f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 2, 1, 1.009998f, 0.45f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 0, 1, 0.1100006f, 0.4400001f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 2, -1, 1f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -2, -1, -1f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", 8, 2, 4f, 1.15f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", 6, 6, 3f, 3.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 7, 5, 3.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 4, 5, 2f, 2.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 8, 4, 4f, 2.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 8, -4, 4f, -1.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 8, -2, 4f, -0.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 7, -5, 3.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 5, -6, 2.5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 5, -4, 2.5f, -1.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -3, 11, -1.5f, 5.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -1, 10, -0.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -6, 15, -3f, 7.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -12, 13, -6f, 6.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -14, 11, -7f, 5.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 7, 14, 3.5f, 7.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 13, 14, 6.5f, 7.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 12, 15, 6f, 7.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 15, 11, 7.5f, 5.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 11, 7, 5.5f, 3.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 14, 6, 7f, 3.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 12, -6, 6f, -2.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 12, -11, 6f, -5.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 17, -11, 8.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 14, -14, 7f, -6.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 6, -13, 3f, -6.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 6, -15, 3f, -7.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", 4, -11, 2f, -5.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -3, -11, -1.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -2, -12, -1f, -5.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -7, -10, -3.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -13, -9, -6.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -15, -12, -7.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -13, -15, -6.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -11, -14, -5.5f, -6.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -13, -11, -6.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_003", -12, -11, -6f, -5.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -7, -12, -3.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -8, -14, -4f, -6.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -3, -15, -1.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 4, -14, 2f, -6.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 6, -11, 3f, -5.35f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 14, -12, 7f, -5.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 13, 12, 6.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 11, 14, 5.5f, 7.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 12, 11, 6f, 5.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 3, 15, 1.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", 4, 11, 2f, 5.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -13, 12, -6.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -14, 14, -7f, 7.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -12, 16, -6f, 8.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -12, 14, -6f, 7.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -16, 10, -8f, 5.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -13, 10, -6.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -16, 12, -8f, 6.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_002", -4, 12, -2f, 6.15f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -10, 13, -5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -5, 15, -2.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -1, 12, -0.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", 4, 14, 2f, 7.15f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", 6, 10, 3f, 5.15f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", 13, 5, 6.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", 11, -11, 5.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", 5, -13, 2.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", -2, -11, -1f, -5.35f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", -6, -13, -3f, -6.35f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", -11, -10, -5.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", -12, -15, -6f, -7.35f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_002", -16, -14, -8f, -6.85f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -7, 2, -3.5f, 1.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -7, 5, -3.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -4, 5, -2f, 2.65f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -5, 4, -2.5f, 2.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -6, 6, -3f, 3.15f);
		MG_ControlDoodad.I._createDoodad("grassWinter_001", -8, 1, -4f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("flowerWinter_001", -6, 2, -3f, 1.15f);
		#endregion

		#region "Units"
		MG_ControlUnits.I._createUnit("Commander", -11, 0, 1);
		MG_ControlUnits.I._createUnit("Farm", -10, 2, 1);
		MG_ControlUnits.I._createUnit("Barracks", -10, -2, 1);

		MG_ControlUnits.I._createUnit("Farm", 10, 2, 2);
		MG_ControlUnits.I._createUnit("Barracks", 10, -2, 2);
		MG_ControlUnits.I._createUnit("Ragnaros", 11, 0, 2);
		MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "29"});
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;

		MG_ControlUnits.I._createUnit("Giant", 10, 13, 2);
		#endregion

		#region "Campaign Events"
		MG_ControlEvents.I._addEvent ("TurnNumberReached", new string[]{"16", "28"}); // target turn num is 1st par, output is 2nd par
		MG_ControlEvents.I._addEvent ("TurnNumberReached", new string[]{"24", "28"}); // target turn num is 1st par, output is 2nd par
		MG_ControlEvents.I._addEvent ("TurnNumberReached", new string[]{"31", "28"}); // target turn num is 1st par, output is 2nd par
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
		#region "Cursor and camera"
		if(MG_Globals.I.curPlayerNum == 1){
			MG_ControlCursor.I._moveCursor(-5.5f, 0.17f, true);
			MG_ControlCamera.I._moveCamera(-4.699522f, 0.1363102f);
		}else{
			MG_ControlCursor.I._moveCursor(5.5f, 0.17f, true);
			MG_ControlCamera.I._moveCamera(4.699522f, 0.1363102f);
		}
		#endregion
	}
}
