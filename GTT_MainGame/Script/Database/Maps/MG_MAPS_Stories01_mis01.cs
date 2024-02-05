using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_MAPS_Stories01_mis01 : MonoBehaviour{
	public static MG_MAPS_Stories01_mis01 I;
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
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -16, -12, -8f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -14, -14, -7f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -17, -15, -8.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -11, -15, -5.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -14, -16, -7f, -7.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -8, -17, -4f, -8.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -6, -16, -3f, -7.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -12, -13, -6f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -8, -14, -4f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -6, -13, -3f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -10, -12, -5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -4, -14, -2f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 3, -8, 1.5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 7, -10, 3.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 6, -7, 3f, -3.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 10, -8, 5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 14, -7, 7f, -3.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, -9, 8f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 12, -9, 6f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 4, 1, 2f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 7, 3, 3.5f, 1.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 14, 1, 7f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, 3, 8f, 1.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 3, 12, 1.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 4, 15, 2f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 9, 14, 4.5f, 7.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, 12, 4f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 15, 13, 7.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, 14, 8f, 7.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -7, 2, -3.5f, 1.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -13, -1, -6.5f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -11, -2, -5.5f, -0.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 0, 3, 0f, 1.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -6, 9, -3f, 4.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -1, 12, -0.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -7, 13, -3.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -4, 15, -2f, 7.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -12, 11, -6f, 5.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -16, 7, -8f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -5, -6, -2.5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -13, -6, -6.5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 8, 7, 4f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 16, 7, 8f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", 4, 7, 2f, 3.65f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -17, 14, -8.5f, 7.15f);
		MG_ControlDoodad.I._createDoodad("treeSummer_001", -13, 16, -6.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -8, 1, -3.768002f, 0.6010001f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -6, 3, -3f, 1.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 5, 0, 2.360001f, 0.01799989f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 5, 3, 2.856998f, 1.36f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 14, 0, 7f, 0.1500001f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 13, 0, 6.725998f, 0.046f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 5, -8, 2.5f, -3.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 9, -10, 4.5f, -4.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 12, -6, 6f, -2.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -10, -13, -5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -15, -12, -7.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -16, -13, -8f, -6.35f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -13, -14, -6.5f, -6.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -9, -14, -4.5f, -6.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -10, -15, -5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -4, -12, -2f, -5.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -8, -15, -4f, -7.35f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -15, -15, -7.5f, -7.35f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -10, -6, -5f, -2.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -16, -6, -8f, -2.85f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -13, 5, -6.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -16, 10, -8f, 5.15f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -9, 13, -4.5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -1, 16, -0.5f, 8.15f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", -5, 12, -2.5f, 6.15f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 10, 13, 5f, 6.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_001", 15, 12, 8.200001f, 6.254f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 4, 13, 2f, 6.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 2, 10, 1f, 5.15f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 6, 1, 2.689999f, 0.9689999f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 3, 5, 1.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 15, 5, 7.5f, 2.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 10, 3, 5f, 1.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 8, 0, 4f, 0.1500001f);
		MG_ControlDoodad.I._createDoodad("grassSummer_002", 16, -1, 8f, -0.3499999f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 6, 1, 3f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", -14, 11, -7f, 5.65f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", -8, 16, -4f, 8.15f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 4, -9, 2f, -4.35f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 8, -6, 4f, -2.85f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_001", 16, -7, 8f, -3.35f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", 10, 7, 5f, 3.65f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", -3, 11, -1.5f, 5.65f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", -10, 10, -5f, 5.15f);
		MG_ControlDoodad.I._createDoodad("flowerSummer_002", 1, 1, 0.5f, 0.6500001f);
		MG_ControlDoodad.I._createDoodad("grassSummer_003", 5, 11, 2.5f, 5.65f);
		MG_ControlDoodad.I._createDoodad("grassSummer_003", 0, 14, 0f, 7.15f);
		MG_ControlDoodad.I._createDoodad("grassSummer_003", -14, -2, -7f, -0.85f);
		MG_ControlDoodad.I._createDoodad("dood_lampPost2", -4, -2, -2f, -0.65f);
		MG_ControlDoodad.I._createDoodad("dood_lampPost2", -9, -2, -4.5f, -0.65f);
		MG_ControlDoodad.I._createDoodad("dood_lampPost2", -15, -2, -7.5f, -0.65f);
		MG_ControlDoodad.I._createDoodad("dood_lampPost2", 0, -2, 0f, -0.65f);
		MG_ControlDoodad.I._createDoodad("dood_lampPost2", 6, -2, 3f, -0.65f);
		MG_ControlDoodad.I._createDoodad("dood_lampPost2", 12, -2, 6f, -0.65f);

		// Path blockers
		for(int x = -17; x <= 1; x++){
			for(int y = -10; y <= -8; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		}
		for(int x = -17; x <= 14; x++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", x, -11, 300, 300);
		}
		for(int x = -3; x <= 14; x++){
			for(int y = -15; y <= -12; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		}
		for(int y = -3; y <= 0; y++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", -17, y, 300, 300);
		}
		for(int x = -13; x <= -11; x++){
			for(int y = 0; y <= 3; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		}
		for(int y = 4; y <= 7; y++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", -11, y, 300, 300);
		}
		for(int x = -10; x <= -5; x++){
			for(int y = 5; y <= 8; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		}
		for(int y = 5; y <= 16; y++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", 1, y, 300, 300);
		}
		MG_ControlDoodad.I._createDoodad("pathBlocker", -11, 16, 300, 300);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -11, 15, 300, 300);
		for(int x = -15; x <= -12; x++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", x, 14, 300, 300);
		}
		MG_ControlDoodad.I._createDoodad("pathBlocker", -15, 13, 300, 300);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -17, 12, 300, 300);
		MG_ControlDoodad.I._createDoodad("pathBlocker", -16, 12, 300, 300);
		for(int x = 7; x <= 10; x++){
			for(int y = 8; y <= 11; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		}
		for(int x = 15; x <= 17; x++){
			for(int y = 8; y <= 11; y++){
				MG_ControlDoodad.I._createDoodad("pathBlocker", x, y, 300, 300);
			}
		}
		for(int y = 11; y <= 15; y++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", 7, y, 300, 300);
		}
		for(int x = 8; x <= 17; x++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", x, 15, 300, 300);
		}
		for(int y = 12; y <= 14; y++){
			MG_ControlDoodad.I._createDoodad("pathBlocker", 17, y, 300, 300);
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
		#endregion

		#region "Units"
		MG_ControlUnits.I._createUnit("CampBlue", -9, 0, 1);
		MG_ControlUnits.I._createUnit("CampBlue", -7, 0, 1);
		MG_ControlUnits.I._createUnit("BarracksBlue", -5, 2, 1);
		MG_ControlUnits.I._createUnit("SpellTowerBlue", -5, 4, 1);

		MG_ControlUnits.I._createUnit("May", -4, -4, 1);
		MG_Globals.I.players[1]._HERO_setSummonedState("May", true);
		MG_ControlUnits.I._createUnit("Officer", -6, -3, 1);
		MG_ControlUnits.I._createUnit("Rifleman", -6, -5, 1);
		
		MG_ControlUnits.I.createHeroSpirit(4, 12);
		MG_ControlUnits.I.createHeroSpirit(-2, 15);
		
		MG_ControlUnits.I._createUnit("Rifleman", 14, -4, 2);
		MG_ControlUnits.I._createUnit("Swordsman", 11, 14, 2);
		MG_ControlUnits.I._createUnit("Swordsman", 12, 14, 2);
		MG_ControlUnits.I._createUnit("Horseman", 3, 14, 2);
		MG_ControlUnits.I._createUnit("Horseman", 4, 13, 2);
		MG_ControlUnits.I._createUnit("Horseman", -14, 13, 2);
		MG_ControlUnits.I._createUnit("Rifleman", -14, 13, 2);
		MG_ControlUnits.I._createUnit("Rifleman", -14, 11, 2);
		MG_ControlUnits.I._createUnit("Swordsman", -2, 15, 2);
		MG_ControlUnits.I._createUnit("Officer", -1, 14, 2);
		MG_ControlUnits.I._createUnit("Apprentice", 14, -5, 2);
		MG_ControlUnits.I._createUnit("Medic", -8, 16, 2);
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
		MG_ControlCursor.I._moveCursor(0, 0, true);
		MG_ControlCamera.I._moveCamera(-1.855307f, -1.59312f);
		#endregion
	}
}
