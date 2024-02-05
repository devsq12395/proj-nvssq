using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Cards : MonoBehaviour {
	public static MG_DB_Cards I;
	public void Awake(){ I = this; }

	public Sprite port_dummy, port_swordsman, port_rifleman, port_apprentice, port_officer, port_horseman, port_farm, port_plantation, port_howitzer, port_cannon,
		port_tower, port_cannonTower, port_hellhound, port_medic, port_warBonds, port_brotherhood, port_veteran, port_sacredKnight, port_reinfor, port_holyOrder, port_cavCharge,
		post_tradingPost, port_lumberMill, port_conscription, port_fireball, port_grenadier, port_impInfantry, port_impSniper, port_impMarch, port_giant, port_knight, port_hydraSpawn,
		port_giantTurtle, port_warTurtle, port_hedgehog, port_holyCrusade, port_spy,
		port_viking, port_vikingRaider, port_spectre, port_fireSpawn, port_fireElem, port_barracks, port_robin, port_ajax, port_colt, port_elizabeth, port_shieldTower,
		port_barracksLvl2;

	#region "Portrait Sprites"
	public Sprite _getPortrait(string newName){
		Sprite retVal = port_dummy;

		switch (newName) {
			case "Swordsman": 									retVal = port_swordsman; break;
			case "Rifleman":  									retVal = port_rifleman; break;
			case "Apprentice":  								retVal = port_apprentice; break;
			case "Officer": 									retVal = port_officer; break;
			case "Horseman": 									retVal = port_horseman; break;
			case "Farm": 										retVal = port_farm; break;
			case "Plantation":  								retVal = port_plantation; break;
			case "Howitzer":  									retVal = port_howitzer; break;
			case "Cannon":  									retVal = port_cannon; break;

			case "Tower": 										retVal = port_tower; break;
			case "CannonTower":									retVal = port_cannonTower; break;
			case "Hellhound": 									retVal = port_hellhound; break;
			case "Medic": 										retVal = port_medic; break;
			case "WarBonds": 									retVal = port_warBonds; break;
			case "Brotherhood":									retVal = port_brotherhood; break;
			case "Veteran": 									retVal = port_veteran; break;
			case "SacredKnight":								retVal = port_sacredKnight; break;
			case "Reinforcements": 								retVal = port_reinfor; break;
			case "HolyOrder": 									retVal = port_holyOrder; break;
			case "CavalryCharge":								retVal = port_cavCharge; break;

			case "TradingPost": 								retVal = post_tradingPost; break;
			case "LumberMill": 									retVal = port_lumberMill; break;
			case "Conscription": 								retVal = port_conscription; break;
			case "Fireball": 									retVal = port_fireball; break;
			case "Grenadier": 									retVal = port_grenadier; break;
			case "ImperialInfantry": 							retVal = port_impInfantry; break;
			case "ImperialSniper": 								retVal = port_impSniper; break;
			case "ImperialMarch": 								retVal = port_impMarch; break;
			case "Giant": 										retVal = port_giant; break;
			case "Crusader": 									retVal = port_knight; break;
			case "HydraSpawn": 									retVal = port_hydraSpawn; break;
			case "GiantTurtle": 								retVal = port_giantTurtle; break;
			case "WarTurtle": 									retVal = port_warTurtle; break;
			case "Hedgehog": 									retVal = port_hedgehog; break;
			case "HolyCrusade": 								retVal = port_holyCrusade; break;
			case "Spy": 										retVal = port_spy; break;

			case "Viking": 									retVal = port_viking; break;
			case "VikingRaider": 							retVal = port_vikingRaider; break;
			case "Spectre": 								retVal = port_spectre; break;
			case "FireSpawn": 								retVal = port_fireSpawn; break;
			case "FireElemental": 							retVal = port_fireElem; break;

			case "Barracks": 								retVal = port_barracks; break;
			
			case "Robin": 									retVal = port_robin; break;
			case "Ajax": 									retVal = port_ajax; break;
			case "Colt": 									retVal = port_colt; break;
			case "Elizabeth": 									retVal = port_elizabeth; break;
			
			case "ShieldTower": 									retVal = port_shieldTower; break;
			
			case "BarracksLvl2": 									retVal = port_barracksLvl2; break;
		}

		return retVal;
	}
	#endregion

	#region "Name"
	public string getName(string newName){
		string retVal = "";

		switch (newName) {
			case "Swordsman": 						retVal = "Swordsman"; break;
			case "Rifleman": 						retVal = "Rifleman"; break;
			case "Apprentice": 						retVal = "Apprentice"; break;
			case "Officer": 						retVal = "Officer"; break;
			case "Horseman": 						retVal = "Horseman"; break;
			case "Farm": 							retVal = "Farm"; break;
			case "Plantation": 						retVal = "Plantation"; break;
			case "Howitzer": 						retVal = "Howitzer"; break;
			case "Cannon": 							retVal = "Cannon"; break;

			case "Tower":							retVal = "Tower"; break;
			case "CannonTower": 					retVal = "Cannon Tower"; break;
			case "Hellhound": 						retVal = "Hellhound"; break;
			case "Medic": 							retVal = "Medic"; break;
			case "WarBonds": 						retVal = "War Bonds"; break;
			case "Brotherhood": 					retVal = "Brotherhood"; break;
			case "Veteran": 						retVal = "Veteran"; break;
			case "SacredKnight": 					retVal = "Sacred Knight"; break;
			case "Reinforcements": 					retVal = "Reinforcements"; break;
			case "HolyOrder": 						retVal = "Holy Order"; break;
			case "CavalryCharge":					retVal = "Cavalry Charge"; break;

			case "TradingPost": 								retVal = "Trading Post"; break;
			case "LumberMill": 								retVal = "Lumber Mill"; break;
			case "Conscription": 								retVal = "Conscription"; break;
			case "Fireball": 								retVal = "Fireball"; break;
			case "Grenadier": 								retVal = "Grenadier"; break;
			case "ImperialInfantry": 								retVal = "Imperial Infantry"; break;
			case "ImperialSniper": 								retVal = "Imperial Sniper"; break;
			case "ImperialMarch": 								retVal = "Imperial March"; break;
			case "Giant": 								retVal = "Giant"; break;
			case "Crusader": 								retVal = "Crusader"; break;
			case "HydraSpawn":								retVal = "Hydra Spawn"; break;
			case "GiantTurtle": 								retVal = "Giant Turtle"; break;
			case "WarTurtle": 								retVal = "War Turtle"; break;
			case "Hedgehog": 								retVal = "Hedgehog"; break;
			case "HolyCrusade": 								retVal = "Holy Crusade"; break;
			case "Spy": 								retVal = "Spy"; break;

			case "Viking": 								retVal = "Viking"; break;
			case "VikingRaider": 								retVal = "Viking Raider"; break;
			case "Spectre": 								retVal = "Spectre"; break;
			case "FireSpawn": 								retVal = "Fire Spawn"; break;
			case "FireElemental": 								retVal = "Fire Elemental"; break;

			case "Barracks": 						retVal = "Barracks"; break;
			
			case "Robin": 						retVal = "Robin"; break;
			case "Ajax": 						retVal = "Ajax"; break;
			case "Colt": 						retVal = "Colt"; break;
			case "Elizabeth": 						retVal = "Elizabeth"; break;
			
			case "ShieldTower": 						retVal = "Shield Tower"; break;
			
			case "BarracksLvl2": 						retVal = "Barracks Lvl. 2"; break;
		}

		return retVal;
	}
	#endregion

	#region "Type"
	public string getType(string newName){
		string retVal = "";

		switch (newName) {
			case "Swordsman": 						retVal = "Unit Lv.1"; break;
			case "Rifleman": 						retVal = "Unit Lv.1"; break;
			case "Apprentice": 						retVal = "Unit Lv.2"; break;
			case "Officer": 						retVal = "Unit Lv.2"; break;
			case "Horseman": 						retVal = "Unit Lv.1"; break;
			case "Farm": 							retVal = "Building"; break;
			case "Plantation": 						retVal = "Effect"; break;
			case "Howitzer": 						retVal = "Unit Lv.2"; break;
			case "Cannon": 							retVal = "Unit Lv.2"; break;

			case "Tower":							retVal = "Building"; break;
			case "CannonTower": 					retVal = "Effect"; break;
			case "Hellhound": 						retVal = "Unit Lv.1"; break;
			case "Medic": 							retVal = "Unit Lv.2"; break;
			case "WarBonds": 						retVal = "Effect"; break;
			case "Brotherhood": 					retVal = "Effect"; break;
			case "Veteran": 						retVal = "Effect"; break;
			case "SacredKnight": 					retVal = "Unit Lv.1"; break;
			case "Reinforcements": 					retVal = "Effect"; break;
			case "HolyOrder": 						retVal = "Effect"; break;
			case "CavalryCharge":					retVal = "Effect"; break;

			case "TradingPost": 						retVal = "Building"; break;
			case "LumberMill": 						retVal = "Building"; break;
			case "Conscription": 						retVal = "Effect"; break;
			case "Fireball": 						retVal = "Effect"; break;
			case "Grenadier": 						retVal = "Unit Lv.2"; break;
			case "ImperialInfantry": 						retVal = "Unit Lv.1"; break;
			case "ImperialSniper": 						retVal = "Unit Lv.1"; break;
			case "ImperialMarch": 						retVal = "Effect"; break;
			case "Giant": 						retVal = "Unit Lv.3"; break;
			case "Crusader": 						retVal = "Effect"; break;
			case "HydraSpawn":						retVal = "Unit Lv.3"; break;
			case "GiantTurtle": 						retVal = "Unit Lv.3"; break;
			case "WarTurtle": 						retVal = "Effect"; break;
			case "Hedgehog": 						retVal = "Unit Lv.3"; break;
			case "HolyCrusade": 						retVal = "Effect"; break;
			case "Spy": 						retVal = "Unit Lv.2"; break;

			case "Viking": 								retVal = "Unit Lv.1"; break;
			case "VikingRaider": 								retVal = "Unit Lv.1"; break;
			case "Spectre": 								retVal = "Unit Lv.1"; break;
			case "FireSpawn": 								retVal = "Unit Lv.3"; break;
			case "FireElemental": 								retVal = "Effect"; break;

			case "Barracks": 							retVal = "Building"; break;
			
			case "Robin": 							retVal = "Hero"; break;
			case "Ajax": 							retVal = "Hero"; break;
			case "Colt": 							retVal = "Hero"; break;
			case "Elizabeth": 							retVal = "Hero"; break;
			
			case "ShieldTower": 							retVal = "Building"; break;
			
			case "BarracksLvl2": 							retVal = "Effect"; break;
		}

		return retVal;
	}
	#endregion

	#region "Desc"
	public string getDesc(string newName){
		string retVal = "";

		switch (newName) {
			case "Swordsman": 						retVal = "Cheap & basic melee unit."; break;
			case "Rifleman": 						retVal = "Cheap & basic ranged unit."; break;
			case "Apprentice": 						retVal = "Support unit. Can cast Barrier."; break;
			case "Officer": 						retVal = "Can increase the attack damage of nearby allies."; break;
			case "Horseman": 						retVal = "Fast-moving unit. Deals more damage after moving."; break;
			case "Farm": 							retVal = "Gives +5 gold on your turn. Unit Limit: 5"; break;
			case "Plantation": 						retVal = "Upgrades a target farm into plantation, giving +9 gold per turn. Unit Limit: 5"; break;
			case "Howitzer": 						retVal = "Heavy-hitting unit. Can cast Artillery, hitting enemies at any range. Effective against buildings. Unit Limit: 5"; break;
			case "Cannon": 							retVal = "Heavy-hitting unit. Can cast Grape Shot, effective against clump of enemies."; break;
		
			case "Tower":							retVal = "Basic defensive buildings. Unit Limit: 5"; break;
			case "CannonTower": 					retVal = "Upgrades a target Tower into Cannon Tower, dealing area damage on attacks. Unit Limit: 5"; break;
			case "Hellhound": 						retVal = "Fast-moving unit that deals high cleaving damage."; break;
			case "Medic": 							retVal = "Can heal friendly units."; break;
			case "WarBonds": 						retVal = "Gives +7 gold after use."; break;
			case "Brotherhood": 					retVal = "All Swordsman and Rifleman gains +3 attack for 1 turn."; break;
			case "Veteran": 						retVal = "Target Swordsman or Rifleman gains +4 attack and +7% armor permanently. Does not stack."; break;
			case "SacredKnight": 					retVal = "Strong melee unit."; break;
			case "Reinforcements": 					retVal = "Summons 3 Rifleman in an area."; break;
			case "HolyOrder": 						retVal = "Summons 3 Sacred Knights in an area."; break;
			case "CavalryCharge":					retVal = "Summons 3 Horseman in an area."; break;

			case "TradingPost": 						retVal = "Gives +1 income to nearby Farms and Plantations."; break;
			case "LumberMill": 						retVal = "Gives +7 gold per turn. Unit Limit: 5"; break;
			case "Conscription": 						retVal = "Spawns 2 Rifleman for every Officer you have."; break;
			case "Fireball": 						retVal = "Deals 35 damage to target area."; break;
			case "Grenadier": 						retVal = "Strong infantry that can throw grenades."; break;
			case "ImperialInfantry": 						retVal = "Basic imperial infantry unit."; break;
			case "ImperialSniper": 						retVal = "Infantry that can shoot at a long range."; break;
			case "ImperialMarch": 						retVal = "Summons 3 Imperial Infantry in an area."; break;
			case "Giant": 						retVal = "Strong gigantic unit. Effective against buildings."; break;
			case "Crusader": 						retVal = "Upgrades 3 random Sacred Knights into Crusaders."; break;
			case "HydraSpawn":						retVal = "Gigantic beast. Can slow enemies."; break;
			case "GiantTurtle": 						retVal = "Gigantic beast. Effective against mass infantry."; break;
			case "WarTurtle": 						retVal = "Upgrades a target Giant Turtle into War Turtle."; break;
			case "Hedgehog": 						retVal = "Fast and light tank. Effective at hit-and-run tactics."; break;
			case "HolyCrusade": 						retVal = "All Crusader gains +5 attack for 1 turn."; break;
			case "Spy": 						retVal = "Effective at sneaking behind enemies and destroying their buildings."; break;

			case "Viking": 							retVal = "Fast-moving unit. Gains gold when attacking buildings."; break;
			case "VikingRaider": 					retVal = "Fast-moving cavalry. Gains gold when attacking buildings."; break;
			case "Spectre": 						retVal = "Ranged unit that can teleport around the map."; break;
			case "FireSpawn": 						retVal = "Expensive and has low health but deals a lot of damage."; break;
			case "FireElemental": 					retVal = "Upgrades a target Fire Spawn into Fire Elemental. Fire Elemental deals heavy splash damage."; break;

			case "Barracks": 					retVal = "Each time you use a Unit Lv. 1 Card, this building will spawn an identical unit. Unit Limit: 2"; break;
			
			case "Robin": 					retVal = "Change your Commander into this hero for 6 turns."; break;
			case "Ajax": 					retVal = "Change your Commander into this hero for 6 turns."; break;
			case "Colt": 					retVal = "Change your Commander into this hero for 6 turns."; break;
			case "Elizabeth": 					retVal = "Change your Commander into this hero for 6 turns."; break;
			
			case "ShieldTower": 					retVal = "Applies Barrier to nearby friendly units for 2 turns. Can stack up to 3 times with other Barriers. Unit Limit: 4"; break;
			
			case "BarracksLvl2": 					retVal = "Upgrades a target Barracks into Barracks Lvl. 2. Each time you use a Unit Lv. 2 Card, this building will spawn an identical unit. Unit Limit: 2"; break;
		}

		return retVal;
	}
	#endregion

	#region "Cost"
	public int getCost(string newName){
		int retVal = 0;

		switch (newName) {
			case "Swordsman": 						retVal = 1; break;
			case "Rifleman": 						retVal = 2; break;
			case "Apprentice": 						retVal = 5; break;
			case "Officer": 						retVal = 7; break;
			case "Horseman": 						retVal = 6; break;
			case "Farm": 							retVal = 7; break;
			case "Plantation": 						retVal = 14; break;
			case "Howitzer": 						retVal = 10; break;
			case "Cannon": 							retVal = 10; break;

			case "Tower":							retVal = 5; break;
			case "CannonTower": 					retVal = 13; break;
			case "Hellhound": 						retVal = 7; break;
			case "Medic": 							retVal = 5; break;
			case "WarBonds": 						retVal = 0; break;
			case "Brotherhood": 					retVal = 10; break;
			case "Veteran": 						retVal = 7; break;
			case "SacredKnight": 					retVal = 5; break;
			case "Reinforcements": 					retVal = 9; break;
			case "HolyOrder": 						retVal = 15; break;
			case "CavalryCharge":					retVal = 21; break;

			case "TradingPost": 						retVal = 15; break;
			case "LumberMill": 						retVal = 8; break;
			case "Conscription": 						retVal = 13; break;
			case "Fireball": 						retVal = 6; break;
			case "Grenadier": 						retVal = 5; break;
			case "ImperialInfantry": 						retVal = 4; break;
			case "ImperialSniper": 						retVal = 6; break;
			case "ImperialMarch": 						retVal = 14; break;
			case "Giant": 						retVal = 15; break;
			case "Crusader": 						retVal = 10; break;
			case "HydraSpawn":						retVal = 25; break;
			case "GiantTurtle": 						retVal = 20; break;
			case "WarTurtle": 						retVal = 20; break;
			case "Hedgehog": 						retVal = 18; break;
			case "HolyCrusade": 						retVal = 15; break;
			case "Spy": 						retVal = 15; break;

			case "Viking": 								retVal = 7; break;
			case "VikingRaider": 								retVal = 8; break;
			case "Spectre": 								retVal = 6; break;
			case "FireSpawn": 								retVal = 25; break;
			case "FireElemental": 								retVal = 35; break;

			case "Barracks": 						retVal = 15; break;
			
			case "Robin": 						retVal = 6; break;
			case "Ajax": 						retVal = 6; break;
			case "Colt": 						retVal = 6; break;
			case "Elizabeth": 						retVal = 6; break;
			
			case "ShieldTower": 						retVal = 10; break;
			
			case "BarracksLvl2": 						retVal = 15; break;
		}

		return retVal;
	}
	#endregion
}
