using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI__General : MonoBehaviour{
    public static AI__General I;
	public void Awake(){ I = this; }
	
	public bool DEBUG_MODE;
	
	public bool priorityHero = true;
	public int buildOrder = -1, BUILD_ORDER_COUNT_MAX, buildOrder_mission = -1;
	
	public void _start(){
		BUILD_ORDER_COUNT_MAX = 1;
		
		DEBUG_MODE = true;
		
		switch(MG_Globals.I.curMap){
			case "Frozen Sanctuary": 		buildOrder_mission = 1; break;
			case "Stories02_mis01": 		buildOrder_mission = 2; break;
			case "Stories01_mis03": 		buildOrder_mission = 3; break;
		}
		buildOrder = Random.Range (1, BUILD_ORDER_COUNT_MAX + 1);
	}
	
	public void _move(MG_ClassUnit current, Vector2 target){
		// Check disable
		if(/*MG_ControlBuffs.I._unitIsStunned(current) || */MG_ControlBuffs.I._unitCannotMove(current) || current.isABuilding){
			MG_ControlAI.I.actionCD = 0.6f;
			return;
		}
		
		//Creates the targeters
		MG_ControlTargeters.I._createTargField(false, current.posX, current.posY, current.MS, true, true);

		//Pick the nearest targeter from the target unit
		bool canMove = false, facingRight = false;
		int distance = 0, targetDist = 0;
		MG_ClassTargeter targetO = MG_Globals.I.targetersTemp[0];
		foreach(MG_ClassTargeter oLoop in MG_Globals.I.targetersTemp){
			distance = MG_CALC_Distance.I._distBetweenPoints(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y), oLoop.posX, oLoop.posY);
			if(
				// Basic check
				(distance < targetDist || targetDist == 0)
				// Avoid overextending for heroes check
				&& (!current.isAHero || current.hero_lvl > 3 || oLoop.posX > 5)
			){
				targetDist = distance;
				targetO = oLoop; 
				canMove = true;
			}
		}

		#region "PRE-MOVE EXTRA CODES"
		#region "SIEGFRIED - Mythril Lance"
		if(current.name == "Siegfried"){
			if(MG_CALC_Distance.I._distBetweenPoints(current.posX, current.posY, targetO.posX, targetO.posY) >= 4){
				MG_ControlBuffs.I._addBuff (current, "MythrilLance");
			}
		}
		#endregion
		#region "MASAMUNE - Bleed"
		if(MG_ControlBuffs.I._unitHasBuff_returnStack(current, "Bleed") >= 1){
			if(MG_CALC_Distance.I._distBetweenPoints(current.posX, current.posY, targetO.posX, targetO.posY) >= 4){
				MG_CALC_Damage.I._HP_Loss(current, 50);
			}else{
				MG_CALC_Damage.I._HP_Loss(current, 25);
			}
		}
		#endregion
		#endregion

		//Moves the piece
		MG_ControlSounds.I._playSound(6, targetO.posX, targetO.posY, true);
		if(current.posX < target.x) facingRight = true;
		if(canMove)	current._moveUnit(targetO.posX, targetO.posY);
		if (facingRight) 		current._changeFacing ("right");
		else  					current._changeFacing ("left");
		MG_ControlSFX.I._createSFX("moveSmoke", targetO.posX, targetO.posY);

		//Removes the targeters, and ends the action
		MG_ControlTargeters.I.forceRemoveTempTargeters();
		MG_ControlAI.I.actionCD = 0.6f;

		#region "POST-MOVE EXTRA CODES"
		#region "SIEGFRIED - Warhorn"
		if(current.name == "Siegfried"){
			MG_ControlSFX.I._createSFX("warhornEffect", current.posX, current.posY);
			foreach(MG_ClassUnit u in MG_Globals.I.units){
				if(!MG_ControlPlayer.I._getIsEnemy(current.playerOwner, u.playerOwner) && u.isAlive){
					if(MG_CALC_Distance.I._distBetweenPoints(current.posX, current.posY, u.posX, u.posY) <= 3){
						MG_ControlBuffs.I._addBuff (u, "Warhorn");
					}
				}
			}
		}
		#endregion
		#endregion
		
		// AI Waypoints
		if(current.waypoints.Count > 0){
			if(MG_CALC_Distance.I._distBetweenPoints (Mathf.RoundToInt(current.waypoints[0].x), Mathf.RoundToInt(current.waypoints[0].y), current.posX, current.posY) <= 2){
				current.waypoints.RemoveAt(0);
			}
		}
	}
	
	public void attack(MG_ClassUnit current){
		// Check if unit can attack
		// Check disable
		if(MG_ControlBuffs.I._unitIsStunned(current) || MG_ControlBuffs.I._unitCannotAttack(current)){
			MG_ControlAI.I.actionCD = 0.6f;
			return;
		}
		if(!current.action_atk){
			MG_ControlBuffs.I._addBuff (current, "aiControl");
			return;
		}

		//Creates the targeters
		if(current.atkRange <= 1)
			MG_ControlTargeters.I._createTarg_Square(current.posX, current.posY, 1, true);
		else
			MG_ControlTargeters.I._createTargField(true, current.posX, current.posY, current.atkRange);

		//Pick the weakest enemy within a targeter
		int tarHP = 0;
		bool hasTarget = false, facingRight = false;
		MG_ClassUnit atkTarget = MG_Globals.I.units[0];
		foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
			if((uLoop.HP < tarHP || tarHP == 0) && MG_ControlPlayer.I._getIsEnemy(current.playerOwner, uLoop.playerOwner) 
				&& MG_GetTargeter.I._pointHasTargeterOfType(uLoop.posX, uLoop.posY, 2) && !uLoop.isInvulnerable){
				hasTarget = true;
				tarHP = uLoop.HP;
				atkTarget = uLoop;
			}
		}

		//Uses the ability on a target enemy
		if(hasTarget){
			current.action_atk = false;
			if (current.posX < atkTarget.posX)   facingRight = true;

			MG_ControlCommand.I._EXTENSION_attack_order(current, atkTarget.posX, atkTarget.posY);
			if (facingRight) 		current._changeFacing ("right");
			else  					current._changeFacing ("left");
		}

		//Removes the targeters, and ends the action
		MG_ControlTargeters.I.forceRemoveTempTargeters();
		MG_ControlAI.I.actionCD = 0.6f;
	}
	
	public void hire_buildOrder(MG_ClassUnit current, Vector2 target, int cardUsedNum){
		string cardToUse = AI__General_BuildOrder.I.get_BuildOrder_Card (buildOrder, MG_Globals.I.turnNum, cardUsedNum, buildOrder_mission);
		
		if(cardToUse == "free"){
			_hire (current, target);
		}else if(cardToUse == "NONE"){
			
		}else{
			useCard (current, target, cardToUse);
		}
	}
	
	public void _hire(MG_ClassUnit current, Vector2 target){
		// Pick whatever card the AI can afford from hand
		// AI will try to save 3 gold if possible
		// Prioritizing hero cards every 2nd turn
		int cardToUse = -1, cardCost = 0;
		int usableGold = (
			(MG_Globals.I.players [current.playerOwner].gold >= 3) ? 
			MG_Globals.I.players [current.playerOwner].gold : 
			MG_Globals.I.players [current.playerOwner].gold - 3
		);
		// Check list w/ priority
		bool getPrioCard = false;
		for (int i = 0; i <= 6; i++) {
			if(MG_DB_Cards.I.getCost (MG_ControlAI.I.deck [i]) <= usableGold && 
				((priorityHero && MG_DB_Cards.I.getType (MG_ControlAI.I.deck [i]) == "Hero") || 
				(!priorityHero && MG_DB_Cards.I.getType (MG_ControlAI.I.deck [i]) != "Hero")) ) {
				cardToUse = i;
				cardCost = MG_DB_Cards.I.getCost (MG_ControlAI.I.deck [i]);
				
				if (DEBUG_MODE) Debug.Log ("Priority mode = " + priorityHero);
				if (DEBUG_MODE) Debug.Log ("Got priority card: " + MG_ControlAI.I.deck [i]);
				
				priorityHero = !priorityHero;
				getPrioCard = true;
				break;
			}
		}
		if (DEBUG_MODE && !getPrioCard) Debug.Log ("No priority card got, moving to non-priority cards...");
		// Check list w/o priority if didn't get a card
		if(!getPrioCard){
			for (int i = 0; i <= 6; i++) {
				if(MG_DB_Cards.I.getCost (MG_ControlAI.I.deck [i]) <= usableGold) {
					cardToUse = i;
					cardCost = MG_DB_Cards.I.getCost (MG_ControlAI.I.deck [i]);
					break;
				}
			}
		}
		
		if (cardToUse == -1) {
			MG_ControlAI.I.actionCD = 0.6f;
			return;
		}

		useCard (current, target, MG_ControlAI.I.deck [cardToUse]);
		MG_Globals.I.players [2].gold -= cardCost;
		MG_ControlAI.I.throwCardToBottom (cardToUse);

		//Removes the targeters, and ends the action
		MG_ControlTargeters.I.forceRemoveTempTargeters();
		MG_ControlAI.I.actionCD = 0.6f;
	}
	
	public void useCard(MG_ClassUnit current, Vector2 target, string cardName){
		// Creates the targeters 
		MG_ControlTargeters.I._createTargField(false, current.posX, current.posY, 4, true, true);
		
		//Pick the nearest targeter from the target unit
		bool canMove = false, facingRight = false;
		int distance = 0, targetDist = 0;
		MG_ClassTargeter targetO = MG_Globals.I.targetersTemp[0];
		foreach(MG_ClassTargeter oLoop in MG_Globals.I.targetersTemp){
			distance = MG_CALC_Distance.I._distBetweenPoints(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y), oLoop.posX, oLoop.posY);
			if(
				(
					// Close to enemies when non-buildings
					(MG_DB_Cards.I.getType (cardName) != "Building") ? (distance < targetDist) :
					// Far away from enemies when a buildings
					(distance > targetDist)
				)
				// If no target yet
				|| targetDist == 0
			){
				targetDist = distance;
				targetO = oLoop; 
				canMove = true;
			}
		}
		
		// Use card here
		// Effect cards should be added at the switch case
		string hName = cardName;
		if (DEBUG_MODE) Debug.Log ("Will use card: " + hName);

		switch(hName){
			#region "Spells"
			case "Reinforcements":case "CavalryCharge":
				MG_ControlCommand_Spell.I.castSpell (hName, targetO.posX, targetO.posY, current.playerOwner);
			break;
			#endregion
			#region "Units and buildings"
			default:
				// Hero
				if (MG_DB_Cards.I.getType (hName) == "Hero") {
					if(current.name == "Commander"){
						////////////////////////// SWAP UNIT START //////////////////////////
						int newPosX = current.posX, newPosY = current.posY,
						newLvl = current.hero_lvl, newXp = current.hero_xp, newXpReq = current.hero_xpReq, playNum = current.playerOwner;
						double hpPerc = (double)current.HP / (double)current.HPMax;
						double mpPerc = (double)current.MP / (double)current.MPMax;
						bool act_move = current.action_move, act_atk = current.action_atk, act_skill = current.action_skill;

						MG_ControlUnits.I._addToDestroyList (current);
						MG_ControlUnits.I._createUnit (hName, newPosX, newPosY, playNum);

						MG_ClassUnit newHero = MG_Globals.I.unitsTemp [MG_Globals.I.unitsTemp.Count-1];
						newHero.hero_lvl = newLvl;
						newHero.hero_xp = newXp;
						newHero.hero_xpReq = newXpReq;
						newHero.setHeroStats ();
						MG_ControlBuffs.I._addBuff (newHero, "HeroDur");

						newHero.HP = (int)((double)newHero.HPMax*hpPerc);
						newHero.MP = (int)((double)newHero.MPMax*mpPerc);

						newHero.action_move = act_move;
						newHero.action_atk = act_atk;
						newHero.action_skill = act_skill;
						////////////////////////// SWAP UNIT END //////////////////////////
						MG_ControlTargeters.I.forceRemoveTempTargeters();
						MG_ControlAI.I.actionCD = 0.6f;

						return;
					}
				}
				else{
					// Unit
					MG_ControlAI.I.AI_createUnit(hName, targetO.posX, targetO.posY, current.playerOwner);

					// Barracks
					if (MG_DB_Cards.I.getType (hName) == "Unit Lv.1") {
						foreach (MG_ClassUnit uBar in MG_Globals.I.units) {
							if (uBar.name == "Barracks" && uBar.playerOwner == current.playerOwner) {
								MG_ControlUnits.I._createUnit (hName, uBar.posX, uBar.posY, current.playerOwner);
								MG_ControlSFX.I._createSFX ("summonFx", uBar.posX, uBar.posY);
							}
						}
					}
				}
			break;
			#endregion
		}
		
		// Finishing - Clear Targeters
		MG_ControlTargeters.I.forceRemoveTempTargeters ();
	}
}
