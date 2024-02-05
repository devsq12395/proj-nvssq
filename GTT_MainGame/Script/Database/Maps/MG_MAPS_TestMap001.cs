using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_MAPS_TestMap001 : MonoBehaviour {
	public static MG_MAPS_TestMap001 I;
	public void Awake(){ I = this; }

	public void _generateMap(){
		MG_Globals.I.mapLimitX = 12;
		MG_Globals.I.mapLimitY = 12;

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

		#region "Generated Map Data"
		MG_DB_Maps.I._createGridMap("testMap001");
		GameObject.Find("TableTop").transform.position = new Vector3(0.036f, 5.689f, -300f);
		GameObject.Find("TableTop").transform.localScale = new Vector3(0.6593917f, 0.09056929f, 1f);
		GameObject.Find("TableTop2").transform.position = new Vector3(0.036f, -5.7f, -300f);
		GameObject.Find("TableTop2").transform.localScale = new Vector3(0.6593917f, 0.09056929f, 1f);
		GameObject.Find("TableTop3").transform.position = new Vector3(5.83f, 0.01f, -300f);
		GameObject.Find("TableTop3").transform.localScale = new Vector3(0.6432012f, 0.09056929f, 1f);
		GameObject.Find("TableTop4").transform.position = new Vector3(-5.75f, -0.06f, -300f);
		GameObject.Find("TableTop4").transform.localScale = new Vector3(0.6432012f, 0.09056929f, 1f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -3, -3, -1.5f, -1.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 3, 3, 1.5f, 1.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -6, 4, -3f, 2.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 2, 7, 1f, 3.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 7, 6, 3.5f, 3.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, 1, 4f, 0.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -4, 8, -2f, 4.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, 7, -0.5f, 3.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 6, 10, 3f, 5.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -9, -2, -4.5f, -0.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -9, -5, -4.5f, -2.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -6, -2, -3f, -0.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -8, -9, -4f, -4.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -3, -11, -1.5f, -5.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 1, -10, 0.5f, -4.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 6, -7, 3f, -3.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 7, -10, 3.5f, -4.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 10, -8, 5f, -3.75f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 9, -3, 4.5f, -1.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 11, -1, 5.5f, -0.25f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -7, -6, -3.5f, -2.75f);
            		#endregion

		#region "Units"
		MG_ControlUnits.I._createUnit("PointBlue", -4, 0, 1);
		MG_ControlUnits.I._createUnit("PointRed", 4, 0, 2);
		MG_ControlUnits.I._createUnit("CampBlue", -8, 0, 1);
		MG_ControlUnits.I._createUnit("CampRed", 8, 0, 2);

		MG_ControlUnits.I._createUnit("Robin", -9, 1, 1);
		MG_ControlUnits.I._createUnit("Robin", -9, 2, 1);
		MG_ControlUnits.I._createUnit("Robin", -9, 3, 1);
		MG_ControlUnits.I._createUnit("Robin", -9, 4, 1);
		#endregion
	}
}
