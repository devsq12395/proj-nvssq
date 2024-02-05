using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Default : MonoBehaviour {
	public static AI_Default I;
	public void Awake(){ I = this; }

	public bool DEBUG_MODE;

	public void _runAI(MG_ClassUnit current, Vector2 target, int moveToDo){
		MG_Globals.I.selectedUnit = current;

		bool willMove = true;
		DEBUG_MODE = false;

		//Next move to do
		if(DEBUG_MODE) Debug.Log(current.unitID + " issuing order " + moveToDo);
		switch(moveToDo){
			case 1:					AI__General.I._move(current, target); break;
			case 2:					attack(current); break;
		}
	}

	public void _move(MG_ClassUnit current, Vector2 target){
		// Check disable
		if(MG_ControlBuffs.I._unitIsStunned(current) || MG_ControlBuffs.I._unitCannotMove(current) || current.isABuilding){
			MG_ControlAI.I.actionCD = 0.3f;
			return;
		}
		
		//Creates the targeters
		MG_ControlTargeters.I._createTargField(false, current.posX, current.posY, current.MS, true, true);
		if (MG_Globals.I.targetersTemp.Count <= 0) {
			MG_ControlTargeters.I.forceRemoveTempTargeters();
			MG_ControlAI.I.actionCD = 0.3f;
		}

		//Pick the nearest targeter from the target unit
		bool canMove = false, facingRight = false;
		int distance = 0, targetDist = 0;
		MG_ClassTargeter targetO = MG_Globals.I.targetersTemp [0];
		if (targetO != null) {
			foreach (MG_ClassTargeter oLoop in MG_Globals.I.targetersTemp) {
				distance = MG_CALC_Distance.I._distBetweenPoints (Mathf.RoundToInt (target.x), Mathf.RoundToInt (target.y), oLoop.posX, oLoop.posY);
				if (distance < targetDist || targetDist == 0) {
					targetDist = distance;
					targetO = oLoop; 
					canMove = true;
				}
			}
		} else {
			MG_ControlAI.I.actionCD = 0.1f;
			return;
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

		MG_ControlSounds.I._playSound(6, targetO.posX, targetO.posY, true);
		//Moves the piece
		if(current.posX < target.x) facingRight = true;
		if(canMove)	current._moveUnit(targetO.posX, targetO.posY);
		if (facingRight) 		current._changeFacing ("right");
		else  					current._changeFacing ("left");
		MG_ControlSFX.I._createSFX("moveSmoke", targetO.posX, targetO.posY);

		//Removes the targeters, and ends the action
		MG_ControlTargeters.I.forceRemoveTempTargeters();
		MG_ControlAI.I.actionCD = 0.3f;

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

	// Returns FALSE if there is a nearby enemy to attack
	// Unused for now
	public bool _checkAttack(MG_ClassUnit current){
		bool output = true;
		foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
			if(MG_CALC_Distance.I._distUnits(current, uLoop) <= current.atkRange && MG_ControlPlayer.I._getIsEnemy(current.playerOwner, uLoop.playerOwner)){
				output = false;
				break;
			}
		}
		return output;
	}

	public void attack(MG_ClassUnit current){
		// Check if unit can attack
		// Check disable
		if(MG_ControlBuffs.I._unitIsStunned(current) || MG_ControlBuffs.I._unitCannotAttack(current)){
			MG_ControlAI.I.actionCD = 0.3f;
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
		MG_ControlAI.I.actionCD = 0.3f;
	}

//	public void useAbility(MG_ClassUnit current){
//		//Creates the targeters
//		if(current.atkRange <= 1)
//			MG_ControlTargeters.I._createTarg_Square(current.posX, current.posY, 1, true);
//		else
//			MG_ControlTargeters.I._createTargField(true, current.posX, current.posY, current.atkRange);
//
//		//Pick the weakest enemy within a targeter
//		int tarHP = 0;
//		bool hasTarget = false;
//		MG_ClassUnit atkTarget = MG_Globals.I.units[0];
//		foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
//			if((uLoop.HP < tarHP || tarHP == 0) && uLoop.playerOwner == 1 && MG_GetTargeter.I._pointHasTargeterOfType(uLoop.posX, uLoop.posY, 2) && !uLoop.isInvulnerable){
//				hasTarget = true;
//				tarHP = uLoop.HP;
//				atkTarget = uLoop;
//			}
//		}
//
//		//Uses the ability on a target enemy
//		if(hasTarget){
//			switch(ability){
//				case "Battle":
//					//Globals.I.movesLeft++;  AISystem.I.movesLeft++;
//					UIControl_Battle.I._showPredictions(atkTarget, current);
//					UIControl_Battle.I._yes_Actual(); Debug.Log(current.unitName);
//					/////////////////////////////////Special attack effects/////////////////////////////////////////
//					if(current.unitName == "Balrog"){
//						ActionControl.I._endTurn();
//						AISystem.I.movesLeft = 0;
//						int balAtk = Random.Range(0, 100);
//						//Balrog - Slam
//						if(balAtk <= 45) _balrogSlam(atkTarget, current);
//					}
//					/////////////////////////////////End Special attack effects/////////////////////////////////////
//				break;
//			}
//		}
//
//		//Removes the targeters, and ends the action
//		IntObjects.I._ai_forceRemoveTargeters();
//		AISystem.I.actionCD = 1f;
//	}
//
//	#region "Special Functions"
//	private void _balrogSlam(MG_ClassUnit atkTarget, MG_ClassUnit current){
//		Sounds_Game.I._playSound(37);
//		IntSFX.I._createSfx("fireExplosion1", atkTarget.posX, atkTarget.posY);
//		IntSFX.I._createSfx("fireExplosion1", atkTarget.posX+3, atkTarget.posY+3);
//		IntSFX.I._createSfx("fireExplosion1", atkTarget.posX-3, atkTarget.posY+3);
//		IntSFX.I._createSfx("fireExplosion1", atkTarget.posX+3, atkTarget.posY-3);
//		IntSFX.I._createSfx("fireExplosion1", atkTarget.posX-3, atkTarget.posY-3);
//		foreach(MG_ClassUnit uL in Globals.I.listUnits){
//			if(uL.playerOwner == 1 && DistanceGame.I._distWithinSquare_FromPoint(atkTarget.posX, atkTarget.posY, uL, 5)){
//				Damage.I._damage_P2toP1(current.unitID, uL.unitID, current.damage);
//			}
//		}
//		bool hasDood = true;
//		int randX = 0, randY = 0, antiLoop = 40;
//		int spawnPosX = current.posX, spawnPosY = current.posY;
//		//Crack Line
//		for(int iL = 1; iL <= 10; iL++){
//			while(hasDood){
//				randX = Random.Range(-4, 4);
//				randY = Random.Range(-4, 4);
//				hasDood = PickObject.I._pointHasDoodadOfType("Crack Line", spawnPosX + randX, spawnPosY + randY);
//				antiLoop--; if(antiLoop <= 0)	break;
//			}
//			IntObjects.I._createBattleSFX("Crack Line", spawnPosX + randX, spawnPosY + randY, 6);
//			antiLoop = 40;
//			hasDood = true;
//		}
//		//Fire Line
//		for(int iL = 1; iL <= 5; iL++){
//			while(hasDood){
//				randX = Random.Range(-4, 4);
//				randY = Random.Range(-4, 4);
//				hasDood = PickObject.I._pointHasDoodadOfType("Fire Line", spawnPosX + randX, spawnPosY + randY);
//				antiLoop--; if(antiLoop <= 0)	break;
//			}
//			IntObjects.I._createBattleSFX("Fire Line", spawnPosX + randX, spawnPosY + randY, 6);
//			antiLoop = 40;
//			hasDood = true;
//		}
//	}
//	#endregion
}
