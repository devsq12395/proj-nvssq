using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI__General_BuildOrder : MonoBehaviour{
    public static AI__General_BuildOrder I;
	public void Awake(){ I = this; }
	
	public void generateCardDeck(int presetDeck = -1){
		int NUMBER_OF_PRESET_DECKS = 2, randomDeck = 0;
		if(presetDeck == -1){
			randomDeck = Random.Range(1, NUMBER_OF_PRESET_DECKS + 1);
		
			switch(randomDeck){
				case 1:
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "ImperialInfantry" );
					MG_ControlAI.I.deck.Add ( "ImperialInfantry" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Hedgehog" );
					MG_ControlAI.I.deck.Add ( "Hedgehog" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "HydraSpawn" );
					MG_ControlAI.I.deck.Add ( "HydraSpawn" );
					MG_ControlAI.I.deck.Add ( "Apprentice" );
					MG_ControlAI.I.deck.Add ( "Apprentice" );
				break;
				default:
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "HydraSpawn" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "HydraSpawn" );
					MG_ControlAI.I.deck.Add ( "HydraSpawn" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "Apprentice" );
					MG_ControlAI.I.deck.Add ( "Apprentice" );
				break;
			}
		}
		else{
			switch(presetDeck){
				#region "Ragnaros Deck"
				case 1:
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "Viking" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Giant" );
					MG_ControlAI.I.deck.Add ( "Giant" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Swordsman" );
					MG_ControlAI.I.deck.Add ( "Swordsman" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
				break;
				#endregion
				#region "Spectral Witch Deck"
				case 2:
					MG_ControlAI.I.deck.Add ( "Hellhound" );
					MG_ControlAI.I.deck.Add ( "Hellhound" );
					MG_ControlAI.I.deck.Add ( "Hellhound" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "HydraSpawn" );
					MG_ControlAI.I.deck.Add ( "HydraSpawn" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "Spectre" );
					MG_ControlAI.I.deck.Add ( "Spectre" );
					MG_ControlAI.I.deck.Add ( "Spectre" );
					MG_ControlAI.I.deck.Add ( "Spectre" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
				break;
				#endregion
				#region "Ifreet Deck"
				case 3:
					MG_ControlAI.I.deck.Add ( "ImperialInfantry" );
					MG_ControlAI.I.deck.Add ( "ImperialInfantry" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "ImperialInfantry" );
					MG_ControlAI.I.deck.Add ( "ImperialInfantry" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "LumberMill" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Farm" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					MG_ControlAI.I.deck.Add ( "Barracks" );
					
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Reinforcements" );
					MG_ControlAI.I.deck.Add ( "Giant" );
					MG_ControlAI.I.deck.Add ( "Giant" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "GiantTurtle" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Rifleman" );
					MG_ControlAI.I.deck.Add ( "Swordsman" );
					MG_ControlAI.I.deck.Add ( "Swordsman" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
					MG_ControlAI.I.deck.Add ( "Cannon" );
				break;
				#endregion
			}
		}
	}
	
	
	public string get_BuildOrder_Card(int buildOrderNum, int turnNum, int cardUsedNum, int missionOnly = -1){
		string retVal = "free";
		
		if(missionOnly == -1){
			switch(buildOrderNum){
				#region "#1 - CLASSIC"
				case 1:
					switch(turnNum){
						case 2: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 4: 							retVal = (cardUsedNum == 1) ? "Farm" : "Farm"; break;
						case 6: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 8: 							retVal = (cardUsedNum == 1) ? "Apprentice" : "Apprentice"; break;
						case 10: 							retVal = (cardUsedNum == 1) ? "Barracks" : "Reinforcements"; break;
						case 12: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "Reinforcements"; break;
						case 14: 							retVal = (cardUsedNum == 1) ? "Cannon" : "Cannon"; break;
						case 16: 							retVal = (cardUsedNum == 1) ? "CavalryCharge" : "CavalryCharge"; break;
						
						case 18: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "HydraSpawn"; break;
						case 20: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "WarTurtle"; break;
						case 22: 							retVal = (cardUsedNum == 1) ? "Apprentice" : "Apprentice"; break;
						case 24: 							retVal = (cardUsedNum == 1) ? "WarTurtle" : "CavalryCharge"; break;
						case 26: 							retVal = (cardUsedNum == 1) ? "Rifleman" : "Rifleman"; break;
						case 28: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "NONE"; break;
						case 30: 							retVal = (cardUsedNum == 1) ? "Cannon" : "NONE"; break;
					}
				break;
				#endregion
			}
		}else{
			switch(missionOnly){
				#region "#1 - MISSION - Ragnaros"
				case 1:
					switch(turnNum){
						case 2: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 4: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 6: 							retVal = (cardUsedNum == 1) ? "Barracks" : "Farm"; break;
						case 8: 							retVal = (cardUsedNum == 1) ? "HydraSpawn" : "NONE"; break;
						case 10: 							retVal = (cardUsedNum == 1) ? "WarTurtle" : "NONE"; break;
						case 12: 							retVal = (cardUsedNum == 1) ? "Viking" : "Viking"; break;
						case 14: 							retVal = (cardUsedNum == 1) ? "Viking" : "Viking"; break;
						case 16: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "NONE"; break;
						
						case 18: 							retVal = (cardUsedNum == 1) ? "Viking" : "NONE"; break;
						case 20: 							retVal = (cardUsedNum == 1) ? "Viking" : "Officer"; break;
						case 22: 							retVal = (cardUsedNum == 1) ? "Viking" : "Apprentice"; break;
						case 24: 							retVal = (cardUsedNum == 1) ? "Apprentice" : "Apprentice"; break;
						case 26: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "Viking"; break;
						case 28: 							retVal = (cardUsedNum == 1) ? "Giant" : "Officer"; break;
						case 30: 							retVal = (cardUsedNum == 1) ? "Apprentice" : "Apprentice"; break;
					}
				break;
				#endregion
				#region "#2 - MISSION - Spectral Witch"
				case 2:
					switch(turnNum){
						case 2: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 4: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 6: 							retVal = (cardUsedNum == 1) ? "Barracks" : "Farm"; break;
						case 8: 							retVal = (cardUsedNum == 1) ? "HydraSpawn" : "NONE"; break;
						case 10: 							retVal = (cardUsedNum == 1) ? "HydraSpawn" : "NONE"; break;
						case 12: 							retVal = (cardUsedNum == 1) ? "Spectre" : "Spectre"; break;
						case 14: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "Reinforcements"; break;
						case 16: 							retVal = (cardUsedNum == 1) ? "Spectre" : "Spectre"; break;
						
						case 18: 							retVal = (cardUsedNum == 1) ? "Apprentice" : "Apprentice"; break;
						case 20: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "Reinforcements"; break;
						case 22: 							retVal = (cardUsedNum == 1) ? "Giant" : "Giant"; break;
						case 24: 							retVal = (cardUsedNum == 1) ? "HydraSpawn" : "WarTurtle"; break;
						case 26: 							retVal = (cardUsedNum == 1) ? "ImperialMarch" : "Apprentice"; break;
						case 28: 							retVal = (cardUsedNum == 1) ? "ImperialMarch" : "Apprentice"; break;
						case 30: 							retVal = (cardUsedNum == 1) ? "ImperialMarch" : "Apprentice"; break;
					}
				break;
				#endregion
				#region "#3 - MISSION - Ifreet"
				case 3:
					switch(turnNum){
						case 2: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 4: 							retVal = (cardUsedNum == 1) ? "LumberMill" : "LumberMill"; break;
						case 6: 							retVal = (cardUsedNum == 1) ? "Barracks" : "Farm"; break;
						case 8: 							retVal = (cardUsedNum == 1) ? "HydraSpawn" : "WarTurtle"; break;
						case 10: 							retVal = (cardUsedNum == 1) ? "Hellhound" : "Hellhound"; break;
						case 12: 							retVal = (cardUsedNum == 1) ? "Hellhound" : "HydraSpawn"; break;
						case 14: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "Reinforcements"; break;
						case 16: 							retVal = (cardUsedNum == 1) ? "Hellhound" : "Hellhound"; break;
						
						case 18: 							retVal = (cardUsedNum == 1) ? "Apprentice" : "Apprentice"; break;
						case 20: 							retVal = (cardUsedNum == 1) ? "Reinforcements" : "Reinforcements"; break;
						case 22: 							retVal = (cardUsedNum == 1) ? "Giant" : "Giant"; break;
						case 24: 							retVal = (cardUsedNum == 1) ? "HydraSpawn" : "WarTurtle"; break;
						case 26: 							retVal = (cardUsedNum == 1) ? "Hellhound" : "Apprentice"; break;
						case 28: 							retVal = (cardUsedNum == 1) ? "Hellhound" : "Apprentice"; break;
						case 30: 							retVal = (cardUsedNum == 1) ? "Hellhound" : "Apprentice"; break;
					}
				break;
				#endregion
			}
		}
		
		return retVal;
	}
}
