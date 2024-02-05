using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_Database : MonoBehaviour {
	public static DB_Database I;
	public void Awake(){ I = this; }

	public int CARD_MAX = 40;
		// Count starts at 0
		// Set in Unity, at _DECK object

	public Sprite port_dummy, port_swordsman, port_rifleman, port_apprentice, port_officer, port_horseman, port_farm, port_plantation, port_howitzer, port_cannon,
		port_tower, port_cannonTower, port_hellhound, port_medic, port_warBonds, port_brotherhood, port_veteran, port_sacredKnight, port_reinfor, port_holyOrder, port_cavCharge,
		post_tradingPost, port_lumberMill, port_conscription, port_fireball, port_grenadier, port_impInfantry, port_impSniper, port_impMarch, port_giant, port_knight, port_hydraSpawn,
		port_giantTurtle, port_warTurtle, port_hedgehog, port_holyCrusade, port_spy,
	port_viking, port_vikingRaider, port_spectre, port_fireSpawn, port_fireElem, port_barracks, port_robin, port_ajax, port_colt, port_elizabeth, port_shieldTower, port_barracksLvl2;

	#region "Portrait Sprites"
	public Sprite _getPortrait(int newNum){
		Sprite retVal = port_dummy;

		switch (newNum) {
			case 0: 									retVal = port_swordsman; break;
			case 1: 									retVal = port_rifleman; break;
			case 2: 									retVal = port_apprentice; break;
			case 3: 									retVal = port_officer; break;
			case 4: 									retVal = port_horseman; break;
			case 5: 									retVal = port_farm; break;
			case 6: 									retVal = port_plantation; break;
			case 7: 									retVal = port_howitzer; break;
			case 8: 									retVal = port_cannon; break;

			case 9: 									retVal = port_tower; break;
			case 10: 									retVal = port_cannonTower; break;
			case 11: 									retVal = port_hellhound; break;
			case 12: 									retVal = port_medic; break;
			case 13: 									retVal = port_warBonds; break;
			case 14: 									retVal = port_brotherhood; break;
			case 15: 									retVal = port_veteran; break;
			case 16: 									retVal = port_sacredKnight; break;
			case 17: 									retVal = port_reinfor; break;
			case 18: 									retVal = port_holyOrder; break;
			case 19: 									retVal = port_cavCharge; break;

			case 20: 									retVal = post_tradingPost; break;
			case 21: 									retVal = port_lumberMill; break;
			case 22: 									retVal = port_conscription; break;
			case 23: 									retVal = port_fireball; break;
			case 24: 									retVal = port_grenadier; break;
			case 25: 									retVal = port_impInfantry; break;
			case 26: 									retVal = port_impSniper; break;
			case 27: 									retVal = port_impMarch; break;
			case 28: 									retVal = port_giant; break;
			case 29: 									retVal = port_knight; break;
			case 30: 									retVal = port_hydraSpawn; break;
			case 31: 									retVal = port_giantTurtle; break;
			case 32: 									retVal = port_warTurtle; break;
			case 33: 									retVal = port_hedgehog; break;
			case 34: 									retVal = port_holyCrusade; break;
			case 35: 									retVal = port_spy; break;

			case 36: 									retVal = port_viking; break;
			case 37: 							retVal = port_vikingRaider; break;
			case 38: 								retVal = port_spectre; break;
			case 39: 								retVal = port_fireSpawn; break;
			case 40: 							retVal = port_fireElem; break;

			case 41: 							retVal = port_barracks; break;
			
			case 42: 							retVal = port_robin; break;
			case 43: 							retVal = port_ajax; break;
			case 44: 							retVal = port_colt; break;
			case 45: 							retVal = port_elizabeth; break;
			
			case 46: 							retVal = port_shieldTower; break;
			
			case 47: 							retVal = port_barracksLvl2; break;
		}

		return retVal;
	}
	#endregion

	#region "Index"
	public int getIndex(string newName){
		int retVal = 0;

		switch (newName) {
			case "Swordsman": 						retVal = 0; break;
			case "Rifleman": 						retVal = 1; break;
			case "Apprentice": 						retVal = 2; break;
			case "Officer": 						retVal = 3; break;
			case "Horseman": 						retVal = 4; break;
			case "Farm": 							retVal = 5; break;
			case "Plantation": 						retVal = 6; break;
			case "Howitzer": 						retVal = 7; break;
			case "Cannon": 							retVal = 8; break;

			case "Tower": 							retVal = 9; break;
			case "CannonTower": 					retVal = 10; break;
			case "Hellhound": 						retVal = 11; break;
			case "Medic": 							retVal = 12; break;
			case "WarBonds": 						retVal = 13; break;
			case "Brotherhood": 					retVal = 14; break;
			case "Veteran": 						retVal = 15; break;
			case "SacredKnight": 					retVal = 16; break;
			case "Reinforcements": 					retVal = 17; break;
			case "HolyOrder": 						retVal = 18; break;
			case "CavalryCharge": 					retVal = 19; break;

			case "TradingPost": 					retVal = 20; break;
			case "LumberMill": 						retVal = 21; break;
			case "Conscription": 					retVal = 22; break;
			case "Fireball": 						retVal = 23; break;
			case "Grenadier": 						retVal = 24; break;
			case "ImperialInfantry": 				retVal = 25; break;
			case "ImperialSniper": 					retVal = 26; break;
			case "ImperialMarch": 					retVal = 27; break;
			case "Giant": 							retVal = 28; break;
			case "Crusader": 						retVal = 29; break;
			case "HydraSpawn": 						retVal = 30; break;
			case "GiantTurtle": 					retVal = 31; break;
			case "WarTurtle": 						retVal = 32; break;
			case "Hedgehog": 						retVal = 33; break;
			case "HolyCrusade": 					retVal = 34; break;
			case "Spy": 							retVal = 35; break;

			case "Viking": 									retVal = 36; break;
			case "VikingRaider": 							retVal = 37; break;
			case "Spectre": 								retVal = 38; break;
			case "FireSpawn": 								retVal = 39; break;
			case "FireElemental": 							retVal = 40; break;

			case "Barracks": 							retVal = 41; break;
			
			case "Robin": 							retVal = 42; break;
			case "Ajax": 							retVal = 43; break;
			case "Colt": 							retVal = 44; break;
			case "Elizabeth": 							retVal = 45; break;
			
			case "ShieldTower": 							retVal = 46; break;
			
			case "BarracksLvl2": 							retVal = 47; break;
		}

		return retVal;
	}
	#endregion

	#region "In-Code Name"
	public string getInCodeName(int newName){
		string retVal = "";

		switch (newName) {
			case 0: 						retVal = "Swordsman"; break;
			case 1: 						retVal = "Rifleman"; break;
			case 2: 						retVal = "Apprentice"; break;
			case 3: 						retVal = "Officer"; break;
			case 4: 						retVal = "Horseman"; break;
			case 5: 						retVal = "Farm"; break;
			case 6: 						retVal = "Plantation"; break;
			case 7: 						retVal = "Howitzer"; break;
			case 8: 						retVal = "Cannon"; break;

			case 9: 						retVal = "Tower"; break;
			case 10: 						retVal = "CannonTower"; break;
			case 11: 						retVal = "Hellhound"; break;
			case 12: 						retVal = "Medic"; break;
			case 13: 						retVal = "WarBonds"; break;
			case 14: 						retVal = "Brotherhood"; break;
			case 15: 						retVal = "Veteran"; break;
			case 16: 						retVal = "SacredKnight"; break;
			case 17: 						retVal = "Reinforcements"; break;
			case 18: 						retVal = "HolyOrder"; break;
			case 19: 						retVal = "CavalryCharge"; break;

			case 20: 						retVal = "TradingPost"; break;
			case 21: 						retVal = "LumberMill"; break;
			case 22: 						retVal = "Conscription"; break;
			case 23: 						retVal = "Fireball"; break;
			case 24: 						retVal = "Grenadier"; break;
			case 25: 						retVal = "ImperialInfantry"; break;
			case 26: 						retVal = "ImperialSniper"; break;
			case 27: 						retVal = "ImperialMarch"; break;
			case 28: 						retVal = "Giant"; break;
			case 29: 						retVal = "Crusader"; break;
			case 30: 						retVal = "HydraSpawn"; break;
			case 31: 						retVal = "GiantTurtle"; break;
			case 32: 						retVal = "WarTurtle"; break;
			case 33: 						retVal = "Hedgehog"; break;
			case 34: 						retVal = "HolyCrusade"; break;
			case 35: 						retVal = "Spy"; break;

			case 36: 								retVal = "Viking"; break;
			case 37: 								retVal = "VikingRaider"; break;
			case 38: 								retVal = "Spectre"; break;
			case 39: 								retVal = "FireSpawn"; break;
			case 40: 								retVal = "FireElemental"; break;

			case 41: 								retVal = "Barracks"; break;
			
			case 42: 								retVal = "Robin"; break;
			case 43: 								retVal = "Ajax"; break;
			case 44: 								retVal = "Colt"; break;
			case 45: 								retVal = "Elizabeth"; break;
			
			case 46: 								retVal = "ShieldTower"; break;
			
			case 47: 								retVal = "BarracksLvl2"; break;
		}

		return retVal;
	}
	#endregion

	#region "Name"
	public string getName(int newName){
		string retVal = "";

		switch (newName) {
			case 0: 						retVal = "Swordsman"; break;
			case 1: 						retVal = "Rifleman"; break;
			case 2: 						retVal = "Apprentice"; break;
			case 3: 						retVal = "Officer"; break;
			case 4: 						retVal = "Horseman"; break;
			case 5: 						retVal = "Farm"; break;
			case 6: 						retVal = "Plantation"; break;
			case 7: 						retVal = "Howitzer"; break;
			case 8: 						retVal = "Cannon"; break;

			case 9: 						retVal = "Tower"; break;
			case 10: 						retVal = "Cannon Tower"; break;
			case 11: 						retVal = "Hellhound"; break;
			case 12: 						retVal = "Medic"; break;
			case 13: 						retVal = "War Bonds"; break;
			case 14: 						retVal = "Brotherhood"; break;
			case 15: 						retVal = "Veteran"; break;
			case 16: 						retVal = "Sacred Knight"; break;
			case 17: 						retVal = "Reinforcements"; break;
			case 18: 						retVal = "Holy Order"; break;
			case 19: 						retVal = "Cavalry Charge"; break;

			case 20: 						retVal = "Trading Post"; break;
			case 21: 						retVal = "Lumber Mill"; break;
			case 22: 						retVal = "Conscription"; break;
			case 23: 						retVal = "Fireball"; break;
			case 24: 						retVal = "Grenadier"; break;
			case 25: 						retVal = "Imperial Infantry"; break;
			case 26: 						retVal = "Imperial Sniper"; break;
			case 27: 						retVal = "Imperial March"; break;
			case 28: 						retVal = "Giant"; break;
			case 29: 						retVal = "Crusader"; break;
			case 30: 						retVal = "Hydra Spawn"; break;
			case 31: 						retVal = "Giant Turtle"; break;
			case 32: 						retVal = "War Turtle"; break;
			case 33: 						retVal = "Hedgehog"; break;
			case 34: 						retVal = "Holy Crusade"; break;
			case 35: 						retVal = "Spy"; break;

			case 36: 								retVal = "Viking"; break;
			case 37: 								retVal = "Viking Raider"; break;
			case 38: 								retVal = "Spectre"; break;
			case 39: 								retVal = "Fire Spawn"; break;
			case 40: 								retVal = "Fire Elemental"; break;

			case 41: 								retVal = "Barracks"; break;
			
			case 42: 								retVal = "Robin"; break;
			case 43: 								retVal = "Ajax"; break;
			case 44: 								retVal = "Colt"; break;
			case 45: 								retVal = "Elizabeth"; break;
			
			case 46: 								retVal = "Shield Tower"; break;
			
			case 47: 								retVal = "Barracks Lvl. 2"; break;
		}

		return retVal;
	}
	#endregion

	#region "Type"
	public string getType(int newName){
		string retVal = "";

		switch (newName) {
			case 0: 						retVal = "Unit Lv.1"; break;
			case 1: 						retVal = "Unit Lv.1"; break;
			case 2: 						retVal = "Unit Lv.2"; break;
			case 3: 						retVal = "Unit Lv.2"; break;
			case 4: 						retVal = "Unit Lv.1"; break;
			case 5: 						retVal = "Building"; break;
			case 6: 						retVal = "Effect"; break;
			case 7: 						retVal = "Unit Lv.2"; break;
			case 8: 						retVal = "Unit Lv.2"; break;

			case 9: 						retVal = "Building"; break;
			case 10: 						retVal = "Effect"; break;
			case 11: 						retVal = "Unit Lv.1"; break;
			case 12: 						retVal = "Unit Lv.2"; break;
			case 13: 						retVal = "Effect"; break;
			case 14: 						retVal = "Effect"; break;
			case 15: 						retVal = "Effect"; break;
			case 16: 						retVal = "Unit Lv.1"; break;
			case 17: 						retVal = "Effect"; break;
			case 18: 						retVal = "Effect"; break;
			case 19: 						retVal = "Effect"; break;

			case 20: 						retVal = "Building"; break;
			case 21: 						retVal = "Building"; break;
			case 22: 						retVal = "Effect"; break;
			case 23: 						retVal = "Fireball"; break;
			case 24: 						retVal = "Unit Lv.2"; break;
			case 25: 						retVal = "Unit Lv.1"; break;
			case 26: 						retVal = "Unit Lv.1"; break;
			case 27: 						retVal = "Effect"; break;
			case 28: 						retVal = "Unit Lv.3"; break;
			case 29: 						retVal = "Effect"; break;
			case 30: 						retVal = "Unit Lv.3"; break;
			case 31: 						retVal = "Unit Lv.3"; break;
			case 32: 						retVal = "Effect"; break;
			case 33: 						retVal = "Unit Lv.3"; break;
			case 34: 						retVal = "Effect"; break;
			case 35: 						retVal = "Unit Lv.2"; break;

			case 36: 								retVal = "Unit Lv.1"; break;
			case 37: 								retVal = "Unit Lv.1"; break;
			case 38: 								retVal = "Unit Lv.1"; break;
			case 39: 								retVal = "Unit Lv.3"; break;
			case 40: 								retVal = "Effect"; break;

			case 41: 								retVal = "Building"; break;
			
			case 42: 								retVal = "Hero"; break;
			case 43: 								retVal = "Hero"; break;
			case 44: 								retVal = "Hero"; break;
			case 45: 								retVal = "Hero"; break;
			
			case 46: 								retVal = "Building"; break;
			
			case 47: 						retVal = "Effect"; break;
		}

		return retVal;
	}
	#endregion

	#region "Description"
	public string getDesc(int newName){
		string retVal = "";

		switch (newName) {
			case 0: 						retVal = "Cheap & basic melee unit."; break;
			case 1: 						retVal = "Cheap & basic ranged unit."; break;
			case 2: 						retVal = "Support unit. Can cast Barrier."; break;
			case 3: 						retVal = "Can increase the attack damage of nearby allies."; break;
			case 4: 						retVal = "Fast-moving unit. Deals more damage after moving."; break;
			case 5: 						retVal = "Gives +5 gold on your turn. Unit Limit: 5"; break;
			case 6: 						retVal = "Upgrades a target Farm into Plantation, giving +9 gold per turn. Unit Limit: 5"; break;
			case 7: 						retVal = "Heavy-hitting unit. Can cast Artillery, hitting enemies at any range. Effective against buildings. Unit Limit: 5"; break;
			case 8: 						retVal = "Heavy-hitting unit. Can cast Grape Shot, effective against clump of enemies."; break;

			case 9: 						retVal = "Basic defensive buildings. Unit Limit: 5"; break;
			case 10: 						retVal = "Upgrades a target Tower into Cannon Tower, dealing area damage on attacks. Unit Limit: 5"; break;
			case 11: 						retVal = "Fast-moving unit that deals high cleaving damage."; break;
			case 12: 						retVal = "Can heal friendly units."; break;
			case 13: 						retVal = "Gives +7 gold after use."; break;
			case 14: 						retVal = "All Swordsman and Rifleman gains +3 attack for 1 turn."; break;
			case 15: 						retVal = "Target Swordsman or Rifleman gains +4 attack and +10% armor permanently. Does not stack."; break;
			case 16: 						retVal = "Strong melee unit."; break;
			case 17: 						retVal = "Summons 3 Rifleman in an area."; break;
			case 18: 						retVal = "Summons 3 Sacred Knights in an area."; break;
			case 19: 						retVal = "Summons 3 Horseman in an area."; break;

			case 20: 						retVal = "Gives +1 income to nearby Farms and Plantations."; break;
			case 21: 						retVal = "Gives +7 gold per turn. Unit Limit: 5"; break;
			case 22: 						retVal = "Spawns 2 Rifleman for every Officer you have."; break;
			case 23: 						retVal = "Deals 35 damage to target area."; break;
			case 24: 						retVal = "Strong infantry that can throw grenades."; break;
			case 25: 						retVal = "Basic imperial infantry unit."; break;
			case 26: 						retVal = "Infantry that can shoot at a long range."; break;
			case 27: 						retVal = "Summons 3 Imperial Infantry in an area."; break;
			case 28: 						retVal = "Strong gigantic unit. Effective against buildings."; break;
			case 29: 						retVal = "Upgrades 3 random Sacred Knights into Crusaders."; break;
			case 30: 						retVal = "Gigantic beast. Can slow enemies."; break;
			case 31: 						retVal = "Gigantic beast. Effective against mass infantry."; break;
			case 32: 						retVal = "Upgrades a target Giant Turtle into War Turtle."; break;
			case 33: 						retVal = "Fast and light tank. Effective at hit-and-run tactics."; break;
			case 34: 						retVal = "All Crusader gains +5 attack for 1 turn."; break;
			case 35: 						retVal = "Effective at sneaking behind enemies and destroying their buildings."; break;

			case 36: 							retVal = "Fast-moving unit. Gains gold when attacking buildings."; break;
			case 37: 					retVal = "Fast-moving cavalry. Gains gold when attacking buildings."; break;
			case 38: 						retVal = "Ranged unit that can teleport around the map."; break;
			case 39: 						retVal = "Expensive and has low health but deals a lot of damage."; break;
			case 40: 					retVal = "Upgrades a target Fire Spawn into Fire Elemental. Fire Elemental deals heavy splash damage."; break;

			case 41: 					retVal = "Each time you use a Unit Lv. 1 Card, this building will spawn an identical unit. Unit Limit: 2"; break;
			
			case 42: 					retVal = "Change your Commander into this hero for 6 turns."; break;
			case 43: 					retVal = "Change your Commander into this hero for 6 turns."; break;
			case 44: 					retVal = "Change your Commander into this hero for 6 turns."; break;
			case 45: 					retVal = "Change your Commander into this hero for 6 turns."; break;
			
			case 46: 					retVal = "Applies Barrier to nearby friendly units for 2 turns. Can stack up to 3 times with other Barriers. Unit Limit: 4"; break;
			
			case 47: 					retVal = "Upgrades a target Barracks into Barracks Lvl. 2. Each time you use a Unit Lv. 2 Card, this building will spawn an identical unit. Unit Limit: 2"; break;
		}

		return retVal;
	}
	#endregion

	#region "Cost"
	public int getCost(int newName){
		int retVal = 0;

		switch (newName) {
			case 0: 						retVal = 1; break;
			case 1: 						retVal = 2; break;
			case 2: 						retVal = 5; break;
			case 3: 						retVal = 7; break;
			case 4: 						retVal = 6; break;
			case 5: 						retVal = 7; break;
			case 6: 						retVal = 14; break;
			case 7: 						retVal = 10; break;
			case 8: 						retVal = 10; break;

			case 9: 						retVal = 5; break;
			case 10: 						retVal = 13; break;
			case 11: 						retVal = 7; break;
			case 12: 						retVal = 5; break;
			case 13: 						retVal = 0; break;
			case 14: 						retVal = 10; break;
			case 15: 						retVal = 7; break;
			case 16: 						retVal = 5; break;
			case 17: 						retVal = 9; break;
			case 18: 						retVal = 15; break;
			case 19: 						retVal = 21; break;

			case 20: 						retVal = 15; break;
			case 21: 						retVal = 8; break;
			case 22: 						retVal = 13; break;
			case 23: 						retVal = 6; break;
			case 24: 						retVal = 5; break;
			case 25: 						retVal = 4; break;
			case 26: 						retVal = 6; break;
			case 27: 						retVal = 14; break;
			case 28: 						retVal = 15; break;
			case 29: 						retVal = 10; break;
			case 30: 						retVal = 25; break;
			case 31: 						retVal = 20; break;
			case 32: 						retVal = 20; break;
			case 33: 						retVal = 18; break;
			case 34: 						retVal = 15; break;
			case 35: 						retVal = 15; break;

			case 36: 								retVal = 7; break;
			case 37: 								retVal = 8; break;
			case 38: 								retVal = 6; break;
			case 39: 								retVal = 25; break;
			case 40: 								retVal = 35; break;

			case 41: 								retVal = 15; break;
			
			case 42: 								retVal = 6; break;
			case 43: 								retVal = 6; break;
			case 44: 								retVal = 6; break;
			case 45: 								retVal = 6; break;
			
			case 46: 								retVal = 10; break;
			
			case 47: 								retVal = 15; break;
		}

		return retVal;
	}
	#endregion
}
