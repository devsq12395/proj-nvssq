using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_MAPS_Stories02_mis03 : MonoBehaviour {
	public static MG_MAPS_Stories02_mis03 I;
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
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -13, -13, -6.5f, -6.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -10, -11, -5f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", -11, -14, -5.5f, -6.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 3, -12, 1.5f, -5.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 6, -11, 3f, -5.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_001", 8, -9, 4f, -4.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_002", 14, -10, 7f, -4.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_002", 16, -6, 8f, -2.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_002", -4, -8, -2f, -3.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_002", -14, -4, -7f, -1.85f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_002", -10, -3, -5f, -1.35f);
		MG_ControlDoodad.I._createDoodad("treeSwamp_002", -17, -7, -8.5f, -3.35f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -16, -5, -8f, -2.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -11, -3, -5.5f, -1.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -13, -2, -6.5f, -1f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -13, -5, -6.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -4, -6, -2f, -3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -1, -8, -0.5f, -4f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -12, -12, -6f, -6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -9, -12, -4.5f, -6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -14, -15, -7f, -7.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", 4, -10, 2f, -5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", 5, -12, 2.5f, -6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", 9, -9, 4.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", 12, -4, 6f, -2f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", 15, -6, 7.5f, -3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", 17, -3, 8.5f, -1.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", 15, -9, 7.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -16, 7, -8f, 3.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -11, 9, -5.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -14, 11, -7f, 5.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -9, 13, -4.5f, 6.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -13, 14, -6.5f, 7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -17, 12, -8.5f, 6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_002", -9, 6, -4.5f, 3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -5, 14, -2.5f, 7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -15, 14, -7.5f, 7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", -12, 6, -6f, 3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 6, 14, 3f, 7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 12, 11, 6f, 5.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 15, 14, 7.5f, 7f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 9, 9, 4.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_001", 15, 6, 7.5f, 3f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 16, 9, 8f, 4.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 10, 7, 5f, 3.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 9, 13, 4.5f, 6.5f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 15, 12, 7.5f, 6f);
		MG_ControlDoodad.I._createDoodad("grassSwamp_003", 13, 9, 6.5f, 4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -15, -6, -7.5f, -3f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -12, -3, -6f, -1.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -12, -11, -6f, -5.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -4, -5, -2f, -2.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -2, -7, -1f, -3.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 0, -9, 0f, -4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 4, -11, 2f, -5.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 8, -11, 4f, -5.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 13, -4, 6.5f, -2f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 14, -6, 7f, -3f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 16, -3, 8f, -1.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 16, -8, 8f, -4f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -14, -11, -7f, -5.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -11, -5, -5.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", -5, -9, -2.5f, -4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 0, -5, 0f, -2.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 9, -13, 4.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 2, -10, 1f, -5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 16, -12, 8f, -6f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp4", 12, -7, 6f, -3.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", 13, -5, 6.5f, -2.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -2, -9, -1f, -4.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -9, -14, -4.5f, -7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -14, -6, -7f, -3f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp3", -11, -2, -5.5f, -1f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", -15, -8, -7.5f, -4f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", -7, -13, -3.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", 4, -6, 2f, -3f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", -7, -2, -3.5f, -1f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", 11, -2, 5.5f, -1f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", 11, -14, 5.5f, -7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", -1, -14, -0.5f, -7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", -9, -7, -4.5f, -3.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", 5, -2, 2.5f, -1f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", 15, -13, 7.5f, -6.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", -15, 8, -7.5f, 4f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", -10, 12, -5f, 6f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", -17, 14, -8.5f, 7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", 4, 14, 2f, 7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", 11, 14, 5.5f, 7f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp2", 9, 10, 4.5f, 5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", 11, 6, 5.5f, 3f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", 16, 11, 8f, 5.5f);
		MG_ControlDoodad.I._createDoodad("dood_swampProp1", -9, 8, -4.5f, 4f);

		for(int x = -17; x <= 17; x++){
			for(int y = 15; y <= 16; y++){
				MG_ControlDoodad.I._createDoodad("pathSightBlocker", x, y, 300, 300);
			}
		}
		for(int x = -7; x <= -5; x++){
			for(int y = 5; y <= 12; y++){
				MG_ControlDoodad.I._createDoodad("pathSightBlocker", x, y, 300, 300);
			}
		}
		for(int x = 5; x <= 7; x++){
			for(int y = 5; y <= 12; y++){
				MG_ControlDoodad.I._createDoodad("pathSightBlocker", x, y, 300, 300);
			}
		}
		for(int x = -17; x <= -5; x++){
			for(int y = -1; y <= 4; y++){
				MG_ControlDoodad.I._createDoodad("pathSightBlocker", x, y, 300, 300);
			}
		}
		for(int x = 5; x <= 17; x++){
			for(int y = -1; y <= 4; y++){
				MG_ControlDoodad.I._createDoodad("pathSightBlocker", x, y, 300, 300);
			}
		}
		#endregion

		MG_DB_Maps.I._createGridMap(MG_Globals.I.curMap);

		#region "Units"
		MG_ControlUnits.I._createUnit("CampBlue", -2, -6, 1);
		MG_ControlUnits.I._createUnit("CampBlue", 2, -6, 1);
		MG_ControlUnits.I._createUnit("BarracksBlue", -2, -8, 1);
		MG_ControlUnits.I._createUnit("SpellTowerBlue", 2, -8, 1);
		
		MG_ControlUnits.I._createUnit("Walter", -2, -4, 1);
		MG_GetUnit.I.us_storeUnit (MG_GetUnit.I.getLastCreatedUnit(), "Walter Normal");
		MG_ControlUnits.I._createUnit("Eve", 2, -4, 1); MG_Globals.I.players[1]._HERO_setSummonedState("Eve", true);
		
		MG_ControlUnits.I._createUnit("ImperialInfantry", -2, 7, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I.createHeroSpirit(0, 7); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialInfantry", 2, 7, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		
		MG_ControlUnits.I._createUnit("ImperialInfantry", -2, 13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialCavalry", 0, 13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialInfantry", 2, 13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		
		MG_ControlUnits.I._createUnit("ImperialSniper", -11, 13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialCavalry", -11, 10, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialSniper", -13, 13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		
		MG_ControlUnits.I._createUnit("ImperialSniper", 11, 13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialCavalry", 11, 10, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialSniper", 13, 13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		// Cody at -2, 13
		
		// Drakgul at -16, -6
		MG_ControlUnits.I._createUnit("OrcGrunt", -12, -5, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("OrcGrunt", -13, -8, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("OrcRifleman", -12, -10, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		
		MG_ControlUnits.I._createUnit("OrcGrunt", -8, -13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I.createHeroSpirit(-6, -13); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("OrcRifleman", -4, -13, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		
		// Abel at 15, -7
		// Faylinn at 15, -5
		MG_ControlUnits.I._createUnit("ImperialInfantry", 12, -5, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I.createHeroSpirit(12, -7); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialInfantry", 12, -9, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		
		MG_ControlUnits.I._createUnit("ImperialSniper", 16, -11, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I.createHeroSpirit(16, -13); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
		MG_ControlUnits.I._createUnit("ImperialSniper", 16, -15, 2); MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(0, -6));
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
		MG_ControlCamera.I._moveCamera(0.07195407f, -1.945557f);
		#endregion
	}
}
