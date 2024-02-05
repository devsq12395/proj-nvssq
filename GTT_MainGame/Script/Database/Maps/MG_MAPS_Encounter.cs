using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_MAPS_Encounter : MonoBehaviour {
	public static MG_MAPS_Encounter I;
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

		MG_DB_Maps.I._createGridMap("Encounter");
		#region "Generated Map Data"
		GameObject.Find("TableTop").transform.position = new Vector3(-0.297f, 8.71f, -300f);
		GameObject.Find("TableTop").transform.localScale = new Vector3(0.9646901f, 0.09056929f, -200f);
		GameObject.Find("TableTop2").transform.position = new Vector3(-0.387f, -8.3f, -300f);
		GameObject.Find("TableTop2").transform.localScale = new Vector3(0.9745809f, 0.09056929f, -200f);
		GameObject.Find("TableTop3").transform.position = new Vector3(9.16f, 0.24f, -300f);
		GameObject.Find("TableTop3").transform.localScale = new Vector3(0.9370484f, 0.09056929f, -200f);
		GameObject.Find("TableTop4").transform.position = new Vector3(-9.19f, 0.229f, -300f);
		GameObject.Find("TableTop4").transform.localScale = new Vector3(0.9442195f, 0.09056929f, -200f);
		
		MG_ControlDoodad.I._createDoodad("forest", -11, 6, -5.5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -11, 5, -5.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", -12, 4, -6f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -11, 4, -5.5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -10, 4, -5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -12, 3, -6f, 1.5f);
MG_ControlDoodad.I._createDoodad("forest", -11, 3, -5.5f, 1.5f);
MG_ControlDoodad.I._createDoodad("forest", -10, 3, -5f, 1.5f);
MG_ControlDoodad.I._createDoodad("forest", -10, 5, -5f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", -10, 6, -5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -9, 6, -4.5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -9, 5, -4.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", -9, 4, -4.5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -11, 2, -5.5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -10, 2, -5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -9, 2, -4.5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -8, 2, -4f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -7, 2, -3.5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -8, 1, -4f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", -9, 1, -4.5f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", -10, 1, -5f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", -11, 1, -5.5f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", -11, 0, -5.5f, 0f);
MG_ControlDoodad.I._createDoodad("forest", -10, 0, -5f, 0f);
MG_ControlDoodad.I._createDoodad("forest", -9, 0, -4.5f, 0f);
MG_ControlDoodad.I._createDoodad("forest", -8, 0, -4f, 0f);
MG_ControlDoodad.I._createDoodad("forest", -8, 3, -4f, 1.5f);
MG_ControlDoodad.I._createDoodad("forest", -7, 3, -3.5f, 1.5f);
MG_ControlDoodad.I._createDoodad("forest", -6, 3, -3f, 1.5f);
MG_ControlDoodad.I._createDoodad("forest", -6, 4, -3f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -6, 5, -3f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", -6, 6, -3f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -5, 6, -2.5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -5, 5, -2.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", -5, 4, -2.5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -4, 5, -2f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", -4, 6, -2f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -4, 7, -2f, 3.5f);
MG_ControlDoodad.I._createDoodad("forest", -5, 7, -2.5f, 3.5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 7, -1.5f, 3.5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 8, -1.5f, 4f);
MG_ControlDoodad.I._createDoodad("forest", -3, 9, -1.5f, 4.5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 10, -1.5f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 11, -1.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 12, -1.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -3, 13, -1.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("forest", -2, 13, -1f, 6.5f);
MG_ControlDoodad.I._createDoodad("forest", -4, 12, -2f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -5, 12, -2.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -5, 11, -2.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -4, 11, -2f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -4, 10, -2f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -5, 10, -2.5f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -6, 10, -3f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -8, 10, -4f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -7, 10, -3.5f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -6, 9, -3f, 4.5f);
MG_ControlDoodad.I._createDoodad("forest", -7, 9, -3.5f, 4.5f);
MG_ControlDoodad.I._createDoodad("forest", -8, 9, -4f, 4.5f);
MG_ControlDoodad.I._createDoodad("forest", -9, 9, -4.5f, 4.5f);
MG_ControlDoodad.I._createDoodad("forest", -9, 8, -4.5f, 4f);
MG_ControlDoodad.I._createDoodad("forest", -8, 8, -4f, 4f);
MG_ControlDoodad.I._createDoodad("forest", -9, 7, -4.5f, 3.5f);
MG_ControlDoodad.I._createDoodad("forest", -8, 7, -4f, 3.5f);
MG_ControlDoodad.I._createDoodad("forest", -8, 6, -4f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -9, 6, -4.5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -10, 6, -5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -11, 6, -5.5f, 3f);
MG_ControlDoodad.I._createDoodad("hills", -9, 3, -4.5f, 1.5f);
MG_ControlDoodad.I._createDoodad("hills", -8, 4, -4f, 2f);
MG_ControlDoodad.I._createDoodad("hills", -7, 4, -3.5f, 2f);
MG_ControlDoodad.I._createDoodad("hills", -7, 5, -3.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("hills", -8, 5, -4f, 2.5f);
MG_ControlDoodad.I._createDoodad("hills", -8, 6, -4f, 3f);
MG_ControlDoodad.I._createDoodad("hills", -7, 6, -3.5f, 3f);
MG_ControlDoodad.I._createDoodad("hills", -7, 7, -3.5f, 3.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, 8, -3.5f, 4f);
MG_ControlDoodad.I._createDoodad("hills", -6, 8, -3f, 4f);
MG_ControlDoodad.I._createDoodad("hills", -6, 7, -3f, 3.5f);
MG_ControlDoodad.I._createDoodad("hills", -5, 8, -2.5f, 4f);
MG_ControlDoodad.I._createDoodad("hills", -5, 9, -2.5f, 4.5f);
MG_ControlDoodad.I._createDoodad("hills", -4, 9, -2f, 4.5f);
MG_ControlDoodad.I._createDoodad("forest", -7, 2, -3.5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -7, 1, -3.5f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", -6, 1, -3f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", -7, 0, -3.5f, 0f);
MG_ControlDoodad.I._createDoodad("forest", -6, 0, -3f, 0f);
MG_ControlDoodad.I._createDoodad("forest", -6, -1, -3f, -0.5f);
MG_ControlDoodad.I._createDoodad("forest", -7, -1, -3.5f, -0.5f);
MG_ControlDoodad.I._createDoodad("forest", -7, -2, -3.5f, -1f);
MG_ControlDoodad.I._createDoodad("forest", -7, -4, -3.5f, -2f);
MG_ControlDoodad.I._createDoodad("forest", -6, -4, -3f, -2f);
MG_ControlDoodad.I._createDoodad("forest", -5, -4, -2.5f, -2f);
MG_ControlDoodad.I._createDoodad("forest", -5, -5, -2.5f, -2.5f);
MG_ControlDoodad.I._createDoodad("forest", -5, -6, -2.5f, -3f);
MG_ControlDoodad.I._createDoodad("forest", -7, 4, -3.5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -6, 4, -3f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -5, 4, -2.5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", -5, 5, -2.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", -5, 6, -2.5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -3, 2, -1.5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -2, 2, -1f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -1, 2, -0.5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", -3, 1, -1.5f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 0, -1.5f, 0f);
MG_ControlDoodad.I._createDoodad("forest", 0, 0, 0f, 0f);
MG_ControlDoodad.I._createDoodad("forest", 1, -2, 0.5f, -1f);
MG_ControlDoodad.I._createDoodad("forest", 2, -2, 1f, -1f);
MG_ControlDoodad.I._createDoodad("forest", 3, -2, 1.5f, -1f);
MG_ControlDoodad.I._createDoodad("forest", 3, -1, 1.5f, -0.5f);
MG_ControlDoodad.I._createDoodad("forest", 3, 0, 1.5f, 0f);
MG_ControlDoodad.I._createDoodad("forest", 5, -6, 2.5f, -3f);
MG_ControlDoodad.I._createDoodad("forest", 5, -5, 2.5f, -2.5f);
MG_ControlDoodad.I._createDoodad("forest", 5, -4, 2.5f, -2f);
MG_ControlDoodad.I._createDoodad("forest", 6, -4, 3f, -2f);
MG_ControlDoodad.I._createDoodad("forest", 7, -4, 3.5f, -2f);
MG_ControlDoodad.I._createDoodad("forest", 7, -2, 3.5f, -1f);
MG_ControlDoodad.I._createDoodad("forest", 7, -1, 3.5f, -0.5f);
MG_ControlDoodad.I._createDoodad("forest", 6, -1, 3f, -0.5f);
MG_ControlDoodad.I._createDoodad("forest", 6, 0, 3f, 0f);
MG_ControlDoodad.I._createDoodad("forest", 7, 0, 3.5f, 0f);
MG_ControlDoodad.I._createDoodad("forest", 7, 1, 3.5f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", 6, 1, 3f, 0.5f);
MG_ControlDoodad.I._createDoodad("forest", 7, 2, 3.5f, 1f);
MG_ControlDoodad.I._createDoodad("forest", 7, 4, 3.5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", 6, 4, 3f, 2f);
MG_ControlDoodad.I._createDoodad("forest", 5, 4, 2.5f, 2f);
MG_ControlDoodad.I._createDoodad("forest", 5, 5, 2.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("forest", 5, 6, 2.5f, 3f);
MG_ControlDoodad.I._createDoodad("forest", -4, -10, -2f, -5f);
MG_ControlDoodad.I._createDoodad("forest", -3, -10, -1.5f, -5f);
MG_ControlDoodad.I._createDoodad("forest", -2, -10, -1f, -5f);
MG_ControlDoodad.I._createDoodad("forest", -1, -10, -0.5f, -5f);
MG_ControlDoodad.I._createDoodad("forest", 0, -10, 0f, -5f);
MG_ControlDoodad.I._createDoodad("forest", 1, -10, 0.5f, -5f);
MG_ControlDoodad.I._createDoodad("forest", 2, -10, 1f, -5f);
MG_ControlDoodad.I._createDoodad("forest", 3, -10, 1.5f, -5f);
MG_ControlDoodad.I._createDoodad("forest", 4, -10, 2f, -5f);
MG_ControlDoodad.I._createDoodad("forest", -7, -11, -3.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", -6, -11, -3f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", -5, -11, -2.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", -4, -11, -2f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", -3, -11, -1.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", -2, -11, -1f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", -1, -11, -0.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 0, -11, 0f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 1, -11, 0.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 2, -11, 1f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 3, -11, 1.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 4, -11, 2f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 5, -11, 2.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 6, -11, 3f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", 7, -11, 3.5f, -5.5f);
MG_ControlDoodad.I._createDoodad("forest", -7, -12, -3.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", -6, -12, -3f, -6f);
MG_ControlDoodad.I._createDoodad("forest", -5, -12, -2.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", -4, -12, -2f, -6f);
MG_ControlDoodad.I._createDoodad("forest", -3, -12, -1.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", -2, -12, -1f, -6f);
MG_ControlDoodad.I._createDoodad("forest", -1, -12, -0.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 0, -12, 0f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 1, -12, 0.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 2, -12, 1f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 3, -12, 1.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 4, -12, 2f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 5, -12, 2.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 6, -12, 3f, -6f);
MG_ControlDoodad.I._createDoodad("forest", 7, -12, 3.5f, -6f);
MG_ControlDoodad.I._createDoodad("forest", -4, 10, -2f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 10, -1.5f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -2, 10, -1f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -1, 10, -0.5f, 5f);
MG_ControlDoodad.I._createDoodad("forest", 0, 10, 0f, 5f);
MG_ControlDoodad.I._createDoodad("forest", 1, 10, 0.5f, 5f);
MG_ControlDoodad.I._createDoodad("forest", 2, 10, 1f, 5f);
MG_ControlDoodad.I._createDoodad("forest", 3, 10, 1.5f, 5f);
MG_ControlDoodad.I._createDoodad("forest", 4, 10, 2f, 5f);
MG_ControlDoodad.I._createDoodad("forest", -7, 11, -3.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -6, 11, -3f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -5, 11, -2.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -4, 11, -2f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -3, 11, -1.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -2, 11, -1f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", -1, 11, -0.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 0, 11, 0f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 1, 11, 0.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 2, 11, 1f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 3, 11, 1.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 4, 11, 2f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 5, 11, 2.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 6, 11, 3f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 7, 11, 3.5f, 5.5f);
MG_ControlDoodad.I._createDoodad("forest", 7, 12, 3.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", 6, 12, 3f, 6f);
MG_ControlDoodad.I._createDoodad("forest", 5, 12, 2.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", 4, 12, 2f, 6f);
MG_ControlDoodad.I._createDoodad("forest", 3, 12, 1.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", 2, 12, 1f, 6f);
MG_ControlDoodad.I._createDoodad("forest", 1, 12, 0.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", 0, 12, 0f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -1, 12, -0.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -2, 12, -1f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -3, 12, -1.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -4, 12, -2f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -5, 12, -2.5f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -6, 12, -3f, 6f);
MG_ControlDoodad.I._createDoodad("forest", -7, 12, -3.5f, 6f);
MG_ControlDoodad.I._createDoodad("hills", -9, 15, -4.5f, 7.5f);
MG_ControlDoodad.I._createDoodad("hills", -8, 15, -4f, 7.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, 15, -3.5f, 7.5f);
MG_ControlDoodad.I._createDoodad("hills", -9, 14, -4.5f, 7f);
MG_ControlDoodad.I._createDoodad("hills", -8, 14, -4f, 7f);
MG_ControlDoodad.I._createDoodad("hills", -7, 14, -3.5f, 7f);
MG_ControlDoodad.I._createDoodad("hills", -7, 13, -3.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", -6, 13, -3f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", -5, 13, -2.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", -4, 13, -2f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", -3, 13, -1.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", -2, 13, -1f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", -1, 13, -0.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 0, 13, 0f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 1, 13, 0.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 2, 13, 1f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 3, 13, 1.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 4, 13, 2f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 5, 13, 2.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 6, 13, 3f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, 13, 3.5f, 6.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, 14, 3.5f, 7f);
MG_ControlDoodad.I._createDoodad("hills", 8, 14, 4f, 7f);
MG_ControlDoodad.I._createDoodad("hills", 9, 14, 4.5f, 7f);
MG_ControlDoodad.I._createDoodad("hills", 9, 15, 4.5f, 7.5f);
MG_ControlDoodad.I._createDoodad("hills", 8, 15, 4f, 7.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, 15, 3.5f, 7.5f);
MG_ControlDoodad.I._createDoodad("hills", -9, -15, -4.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", -8, -15, -4f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, -15, -3.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, -14, -3.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", -8, -14, -4f, -7f);
MG_ControlDoodad.I._createDoodad("hills", -9, -14, -4.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", 7, -14, 3.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", 8, -14, 4f, -7f);
MG_ControlDoodad.I._createDoodad("hills", 9, -14, 4.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", 9, -15, 4.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", 8, -15, 4f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, -15, 3.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, -13, -3.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -6, -13, -3f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -5, -13, -2.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -4, -13, -2f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -3, -13, -1.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -2, -13, -1f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -1, -13, -0.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 0, -13, 0f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 1, -13, 0.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 2, -13, 1f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 3, -13, 1.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 4, -13, 2f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 5, -13, 2.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 6, -13, 3f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, -13, 3.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -6, -6, -3f, -3f);
MG_ControlDoodad.I._createDoodad("hills", -6, -5, -3f, -2.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, -5, -3.5f, -2.5f);
MG_ControlDoodad.I._createDoodad("hills", -9, -3, -4.5f, -1.5f);
MG_ControlDoodad.I._createDoodad("hills", -10, -3, -5f, -1.5f);
MG_ControlDoodad.I._createDoodad("hills", -10, -2, -5f, -1f);
MG_ControlDoodad.I._createDoodad("hills", -10, 2, -5f, 1f);
MG_ControlDoodad.I._createDoodad("hills", -10, 3, -5f, 1.5f);
MG_ControlDoodad.I._createDoodad("hills", -9, 3, -4.5f, 1.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, 5, -3.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("hills", -6, 5, -3f, 2.5f);
MG_ControlDoodad.I._createDoodad("hills", -6, 6, -3f, 3f);
MG_ControlDoodad.I._createDoodad("hills", 6, 6, 3f, 3f);
MG_ControlDoodad.I._createDoodad("hills", 6, 5, 3f, 2.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, 5, 3.5f, 2.5f);
MG_ControlDoodad.I._createDoodad("hills", 9, 3, 4.5f, 1.5f);
MG_ControlDoodad.I._createDoodad("hills", 10, 3, 5f, 1.5f);
MG_ControlDoodad.I._createDoodad("hills", 10, 2, 5f, 1f);
MG_ControlDoodad.I._createDoodad("hills", 10, -2, 5f, -1f);
MG_ControlDoodad.I._createDoodad("hills", 10, -3, 5f, -1.5f);
MG_ControlDoodad.I._createDoodad("hills", 9, -3, 4.5f, -1.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, -5, 3.5f, -2.5f);
MG_ControlDoodad.I._createDoodad("hills", 6, -5, 3f, -2.5f);
MG_ControlDoodad.I._createDoodad("hills", 6, -6, 3f, -3f);
MG_ControlDoodad.I._createDoodad("hills", -9, -14, -4.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", -8, -14, -4f, -7f);
MG_ControlDoodad.I._createDoodad("hills", -7, -14, -3.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", -7, -15, -3.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", -8, -15, -4f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", -9, -15, -4.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, -14, 3.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", 8, -14, 4f, -7f);
MG_ControlDoodad.I._createDoodad("hills", 9, -14, 4.5f, -7f);
MG_ControlDoodad.I._createDoodad("hills", 9, -15, 4.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", 8, -15, 4f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, -15, 3.5f, -7.5f);
MG_ControlDoodad.I._createDoodad("hills", 7, -13, 3.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 6, -13, 3f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 5, -13, 2.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 4, -13, 2f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 3, -13, 1.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 2, -13, 1f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 1, -13, 0.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", 0, -13, 0f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -1, -13, -0.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -2, -13, -1f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -3, -13, -1.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -4, -13, -2f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -5, -13, -2.5f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -6, -13, -3f, -6.5f);
MG_ControlDoodad.I._createDoodad("hills", -7, -13, -3.5f, -6.5f);


		for(int x = -11; x <= 11; x++){
			for(int y = 14; y <= 16; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
			for(int y = -16; y <= -13; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		} 
		#endregion

		#region "Units" 
		// P1
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", -9, -2, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", -9, -1, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", -9, 1, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", -9, 2, 1);
		
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -11, -2, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -11, -1, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -11, 1, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -11, 2, 1);
		
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -13, -2, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -13, -1, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -13, 1, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -13, 2, 1);
		
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -15, -2, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", -15, 2, 1);
		
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", -12, 4, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", -13, 4, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", -14, 4, 1);
		
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", -12, -4, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", -13, -4, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", -14, -4, 1);
		
		MG_ControlUnits.I._createUnit_Infantry ("Artillery", -16, -1, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Artillery", -16, 0, 1);
		MG_ControlUnits.I._createUnit_Infantry ("Artillery", -16, 1, 1);
		
		// P2
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", 9, -2, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", 9, -1, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", 9, 1, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Skirmisher", 9, 2, 2);
		
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 11, -2, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 11, -1, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 11, 1, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 11, 2, 2);
		
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 13, -2, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 13, -1, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 13, 1, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 13, 2, 2);
		
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 15, -2, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Infantry", 15, 2, 2);
		
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", 12, 4, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", 13, 4, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", 14, 4, 2);
		
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", 12, -4, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", 13, -4, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Cavalry", 14, -4, 2);
		
		MG_ControlUnits.I._createUnit_Infantry ("Artillery", 16, -1, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Artillery", 16, 0, 2);
		MG_ControlUnits.I._createUnit_Infantry ("Artillery", 16, 1, 2);
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
