using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_MAPS_Stories02_mis01 : MonoBehaviour {
	public static MG_MAPS_Stories02_mis01 I;
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
		
		GameObject.Find("TableTop").transform.position = new Vector3(-0.297f, 8.71f, -300f);
		GameObject.Find("TableTop").transform.localScale = new Vector3(0.9646901f, 0.09056929f, -200f);
		GameObject.Find("TableTop2").transform.position = new Vector3(-0.387f, -8.3f, -300f);
		GameObject.Find("TableTop2").transform.localScale = new Vector3(0.9745809f, 0.09056929f, -200f);
		GameObject.Find("TableTop3").transform.position = new Vector3(9.16f, 0.24f, -300f);
		GameObject.Find("TableTop3").transform.localScale = new Vector3(0.9370484f, 0.09056929f, -200f);
		GameObject.Find("TableTop4").transform.position = new Vector3(-9.19f, 0.229f, -300f);
		GameObject.Find("TableTop4").transform.localScale = new Vector3(0.9442195f, 0.09056929f, -200f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -8, -13, -4f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -13, -11, -6.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -12, -13, -6f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 2, -14, 1f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 0, -14, 0f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -2, -13, -1f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -3, -15, -1.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 9, -9, 4.5f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 11, -8, 5.5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 13, -6, 6.5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 15, -5, 7.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 15, -7, 7.5f, -3.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 13, -10, 6.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 13, -8, 6.5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 11, -10, 5.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 10, -6, 5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 9, -4, 4.5f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 8, -13, 4f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 9, -11, 4.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 5, -15, 2.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 6, -13, 3f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 3, -12, 1.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 1, -11, 0.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 5, -11, 2.5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 13, -12, 6.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 15, -10, 7.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 17, -8, 8.5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 16, -14, 8f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 17, -3, 8.5f, -1.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 16, -1, 8f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 3, 5, 1.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 5, 4, 2.5f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 6, 8, 3f, 4.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 4, 7, 2f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 7, 10, 3.5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 9, 12, 4.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 7, 12, 3.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 13, 11, 6.5f, 5.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 15, 13, 7.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 12, 13, 6f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 14, 15, 7f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 11, 16, 5.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 16, 15, 8f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 13, 16, 6.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 1, 16, 0.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -1, 15, -0.5f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -3, 14, -1.5f, 7.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -1, 13, -0.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -14, 15, -7f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -16, 15, -8f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -11, 16, -5.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -17, 13, -8.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -17, 16, -8.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -15, 16, -7.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -13, 16, -6.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -17, 7, -8.5f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -15, 5, -7.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -16, 3, -8f, 1.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -14, 3, -7f, 1.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -17, 5, -8.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -7, 4, -3.5f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -5, 6, -2.5f, 3.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -4, 8, -2f, 4.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -13, 4, -6.5f, 2.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -17, 0, -8.5f, 0.1500001f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -15, 1, -7.5f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -12, 6, -6f, 3.15f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -13, -3, -6.5f, -1.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -15, -5, -7.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -17, -4, -8.5f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -15, -2, -7.5f, -0.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -17, -7, -8.5f, -3.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -13, -5, -6.5f, -2.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -13, 0, -6.5f, 0.1500001f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -11, -15, -5.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -12, 2, -6f, 1f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -17, -2, -8.5f, -1f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -12, -2, -6f, -1f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -14, -7, -7f, -3.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -11, -4, -5.5f, -2f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -16, 9, -8f, 4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -15, 12, -7.5f, 6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -17, 10, -8.5f, 5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -10, 7, -5f, 3.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -12, 14, -6f, 7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 0, 14, 0f, 7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -3, 16, -1.5f, 8f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 8, 9, 4f, 4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 9, 11, 4.5f, 5.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 5, 9, 2.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 2, 4, 1f, 2f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 5, 6, 2.5f, 3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -3, 6, -1.5f, 3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -6, 4, -3f, 2f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 13, 13, 6.5f, 6.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 15, 11, 7.5f, 5.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 12, 15, 6f, 7.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 10, -8, 5f, -4f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 12, -9, 6f, -4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 10, -10, 5f, -5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 16, -6, 8f, -3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -1, -15, -0.5f, -7.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -10, -14, -5f, -7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -13, -13, -6.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -8, 3, -4f, 1.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -11, 0, -5.5f, 0f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -8, 8, -4f, 4f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", -2, 12, -1f, 6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", -4, 13, -2f, 6.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 3, 13, 1.5f, 6.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 7, 15, 3.5f, 7.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 7, 5, 3.5f, 2.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 1, 8, 0.5f, 4f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 15, 0, 7.5f, 0f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 16, -3, 8f, -1.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", -2, -11, -1f, -5.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 0, -12, 0f, -6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 4, -10, 2f, -5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 4, -13, 2f, -6.5f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -17, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -16, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -15, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -15, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -16, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -17, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -17, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -16, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -15, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -16, -13, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -17, -13, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -17, -14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -16, -14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -16, -15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -17, -15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, -14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, -14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, -14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, -14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, -15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, -15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, -15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, -15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -9, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -9, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -9, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -9, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -8, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -7, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -7, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -8, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -9, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -5, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -9, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -8, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -7, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -4, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -3, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -3, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -3, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -2, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -2, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -2, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -1, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -1, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -1, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 0, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 0, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 0, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 1, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 1, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 2, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 2, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 3, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 3, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 4, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 4, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, -4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, -3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, -2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, -1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, -1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, -1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, -1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 0, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 0, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 0, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 0, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 0, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 1, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 2, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 3, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 15, 4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 16, 4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 17, 4, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 17, 5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 16, 5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 15, 5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 5, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 11, 6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 12, 6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 13, 6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 14, 6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 15, 6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 16, 6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", 17, 6, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 16, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, 16, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, 16, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, 15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, 15, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, 14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, 14, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 13, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, 13, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, 13, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -14, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -13, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -12, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -11, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -10, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -9, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, 12, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -9, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -10, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -11, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -12, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -13, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -14, 11, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -14, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -13, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -12, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -11, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -10, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -9, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -7, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -6, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -8, 10, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -12, 9, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -13, 9, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -14, 9, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -14, 8, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -13, 8, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -12, 8, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -12, 7, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -13, 7, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -14, 7, 300f, 300f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", -9, -9, -4.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", -7, -8, -3.5f, -4f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", -3, -10, -1.5f, -5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", -5, -13, -2.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", -15, -13, -7.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", 15, 2, 7.5f, 1f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", 12, 7, 6f, 3.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", 10, -2, 5f, -1f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", 14, -8, 7f, -4f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", 12, -9, 6f, -4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", 11, -12, 5.5f, -6f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -13, -14, -6.5f, -7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -16, -1, -8f, -0.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -17, 4, -8.5f, 2f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -15, 15, -7.5f, 7.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -2, 15, -1f, 7.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -17, 2, -8.5f, 1f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 16, -9, 8f, -4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 14, -9, 7f, -4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 13, 14, 6.5f, 7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 14, 12, 7f, 6f);
		#endregion

		MG_DB_Maps.I._createGridMap(MG_Globals.I.curMap);

		#region "Units"
		MG_ControlUnits.I._createUnit("Commander", -11, 0, 1);
		MG_ControlUnits.I._createUnit("Farm", -10, 2, 1);
		MG_ControlUnits.I._createUnit("Barracks", -10, -2, 1);

		MG_ControlUnits.I._createUnit("Farm", 10, 2, 2);
		MG_ControlUnits.I._createUnit("Barracks", 10, -2, 2);

		MG_ControlUnits.I._createUnit("SpectralWitch", 10, 0, 2);
		MG_ControlEvents.I._addEvent("UnitDies", new string[]{(MG_ControlUnits.I.unitCnt).ToString(), "30"});
		MG_GetUnit.I.getLastCreatedUnit().isStationed = true;

		MG_ControlUnits.I._createUnit("HydraSpawn", 10, 10, 2);
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
