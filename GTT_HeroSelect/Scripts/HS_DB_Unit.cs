using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_DB_Unit : MonoBehaviour{
	public static HS_DB_Unit I;
	public void Awake(){ I = this; }

	public Sprite port_none, port_test, port_swordsman, port_rifleman, port_apprentice, port_horseman, port_officer, 
		port_medic, port_goblin, port_goblinArcher, port_hellhound, port_grenadier, port_orcGrunt, port_orcRifleman, port_impeInfantry, port_impeCavalry, port_impeSniper;

	#region "Unit Portrait"
	public Sprite _getPortrait(int heroNum){
		Sprite retVal = port_none;

		switch (heroNum) {
			case 0: 			retVal = port_none; break;
			case 1: 			retVal = port_swordsman; break;
			case 2: 			retVal = port_rifleman; break;
			case 3: 			retVal = port_apprentice; break;
			case 4: 			retVal = port_horseman; break;
			case 5: 			retVal = port_officer; break;
			case 6: 			retVal = port_medic; break;
			case 7: 			retVal = port_goblin; break;
			case 8: 			retVal = port_goblinArcher; break;
			case 9: 			retVal = port_hellhound; break;
			case 10: 			retVal = port_grenadier; break;
			case 11: 			retVal = port_orcGrunt; break;
			case 12: 			retVal = port_orcRifleman; break;
			case 13: 			retVal = port_impeInfantry; break;
			case 14: 			retVal = port_impeCavalry; break;
			case 15: 			retVal = port_impeSniper; break;
		}

		return retVal;
	}
	#endregion

	#region "In-code Name"
	public int _getUnitIndexNum(string actualName){
		int retVal = -1;
		switch (actualName) {
			case "Blank":						retVal = 0;break;
			case "Swordsman": 					retVal = 1;break;
			case "Rifleman": 					retVal = 2;break;
			case "Apprentice": 					retVal = 3;break;
			case "Horseman": 					retVal = 4;break;
			case "Officer": 					retVal = 5;break;
			case "Medic":	 					retVal = 6;break;
			case "Goblin":	 					retVal = 7;break;
			case "GoblinArcher": 				retVal = 8;break;
			case "Hellhound": 					retVal = 9;break;
			case "Grenadier": 					retVal = 10;break;
			case "OrcGrunt": 					retVal = 11;break;
			case "OrcRifleman": 				retVal = 12;break;
			case "ImperialInfantry": 			retVal = 13;break;
			case "ImperialCavalry": 			retVal = 14;break;
			case "ImperialSniper": 				retVal = 15;break;
		}
		return retVal;
	}

	public string _getInCodeName(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = "NONE"; break;
			case 1: 			retVal = "Swordsman"; break;
			case 2: 			retVal = "Rifleman"; break;
			case 3: 			retVal = "Apprentice"; break;
			case 4: 			retVal = "Horseman"; break;
			case 5: 			retVal = "Officer"; break;
			case 6: 			retVal = "Medic"; break;
			case 7: 			retVal = "Goblin"; break;
			case 8: 			retVal = "GoblinArcher"; break;
			case 9: 			retVal = "Hellhound"; break;
			case 10: 			retVal = "Grenadier"; break;
			case 11: 			retVal = "OrcGrunt"; break;
			case 12: 			retVal = "OrcRifleman"; break;
			case 13: 			retVal = "ImperialInfantry"; break;
			case 14: 			retVal = "ImperialCavalry"; break;
			case 15: 			retVal = "ImperialSniper"; break;
		}

		return retVal;
	}
	#endregion

	#region "Actual Name"
	public string _getNameActual(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = "Blank"; break;
			case 1: 			retVal = "Swordsman"; break;
			case 2: 			retVal = "Rifleman"; break;
			case 3: 			retVal = "Apprentice"; break;
			case 4: 			retVal = "Horseman"; break;
			case 5: 			retVal = "Officer"; break;
			case 6: 			retVal = "Medic"; break;
			case 7: 			retVal = "Goblin"; break;
			case 8: 			retVal = "Goblin Archer"; break;
			case 9: 			retVal = "Hellhound"; break;
			case 10: 			retVal = "Grenadier"; break;
			case 11: 			retVal = "OrcGrunt"; break;
			case 12: 			retVal = "OrcRifleman"; break;
			case 13: 			retVal = "Imperial Infantry"; break;
			case 14: 			retVal = "Imperial Cavalry"; break;
			case 15: 			retVal = "Imperial Sniper"; break;
		}

		return retVal;
	}
	#endregion

	#region "Description"
	public string _getDesc(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = " "; break;
			case 1: 			retVal = "Basic melee unit with decent health and damage."; break;
			case 2: 			retVal = "Basic ranged unit with decent health and damage."; break;
			case 3: 			retVal = "Can cast Barrier which prevents allies from taking damage."; break;
			case 4: 			retVal = "Fast-moving melee unit, low health but has high damage."; break;
			case 5: 			retVal = "Expensive unit but deals high damage and inspires non-hero units."; break;
			case 6: 			retVal = "Ranged unit with different healing capabilities."; break;
			case 7: 			retVal = "Cheap unit with lower health and damage. Faster than most infantry units."; break;
			case 8: 			retVal = "Cheap ranged unit with lower health and damage. Faster than most infantry units and can outrange some guns."; break;
			case 9: 			retVal = "Deals high damage but has low health and is expensive."; break;
			case 10: 			retVal = "Strong but expensive unit. Can throw grenades."; break;
			case 11: 			retVal = "Basic orcish melee unit. Stronger than most basic human units."; break;
			case 12: 			retVal = "Basic orcish ranged unit. Has higher health compared to other gun units."; break;
			case 13: 			retVal = "Basic imperial unit. Stronger but more expensive than most infantry."; break;
			case 14: 			retVal = "Fast-moving melee unit. Stronger but more expensive than most cavalry."; break;
			case 15: 			retVal = "Has huge attack damage and can shoot from afar."; break;
		}

		return retVal;
	}
	#endregion

	#region "Limit"
	public int _getLim(int heroNum){
		int retVal = 0;

		switch (heroNum) {
			case 0: 			retVal = 0; break;
			case 1: 			retVal = 3; break;
			case 2: 			retVal = 3; break;
			case 3: 			retVal = 2; break;
			case 4: 			retVal = 2; break;
			case 5: 			retVal = 2; break;
			case 6: 			retVal = 2; break;
			case 7: 			retVal = 3; break;
			case 8: 			retVal = 3; break;
			case 9: 			retVal = 2; break;
			case 10: 			retVal = 2; break;
			case 11: 			retVal = 2; break;
			case 12: 			retVal = 2; break;
			case 13: 			retVal = 3; break;
			case 14: 			retVal = 3; break;
			case 15: 			retVal = 2; break;
		}

		return retVal;
	}
	#endregion

	#region "Cost"
	public int _getCost(int heroNum){
		int retVal = 0;

		switch (heroNum) {
			case 0: 			retVal = 0; break;
			case 1: 			retVal = 175; break;
			case 2: 			retVal = 225; break;
			case 3: 			retVal = 300; break;
			case 4: 			retVal = 300; break;
			case 5: 			retVal = 400; break;
			case 6: 			retVal = 400; break;
			case 7: 			retVal = 150; break;
			case 8: 			retVal = 200; break;
			case 9: 			retVal = 400; break;
			case 10: 			retVal = 450; break;
			case 11: 			retVal = 210; break;
			case 12: 			retVal = 260; break;
			case 13: 			retVal = 250; break;
			case 14: 			retVal = 340; break;
			case 15: 			retVal = 400; break;
		}

		return retVal;
	}
	#endregion
}
