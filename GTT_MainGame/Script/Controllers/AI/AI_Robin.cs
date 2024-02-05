﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Robin : MonoBehaviour {
	public static AI_Robin I;
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
			case 2: 				useAbility (current); break;
			case 3:					AI__General.I.attack(current); break;
			case 4:					AI__General.I._hire(current, target); break;
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

	public void useAbility(MG_ClassUnit current){
		// Check disable
		if(MG_ControlBuffs.I._unitIsStunned(current) || MG_ControlBuffs.I._unitCannotSkill(current)){
			MG_ControlAI.I.actionCD = 0.6f;
			return;
		}
		
		assesSituation (current, 6);

		//Uses the ability on a target enemy
		if(hasTarget && current.MP >= 75){
			if (nearestEnemyRange <= 2) {
				#region "Blade Dance"
				MG_ControlCommand.I.gameAction_robin_sworddance(current);
				#endregion
			} else {
				#region "Hook Dance"
				if(current.MP >= 75){
					// Get target unit
					MG_ClassUnit target = getSingleAttackTarget (current, 4);

					// Creates the targeters
					MG_ControlTargeters.I._createTargField(false, current.posX, current.posY, 4, false, true);

					// Pick the nearest targeter from the target unit
					bool canMove = false, facingRight = false;
					int distance = 0, targetDist = 0;
					MG_ClassTargeter targetO = MG_Globals.I.targetersTemp[0];
					foreach(MG_ClassTargeter oLoop in MG_Globals.I.targetersTemp){
						distance = MG_CALC_Distance.I._distBetweenPoints(Mathf.RoundToInt(target.posX), Mathf.RoundToInt(target.posY), oLoop.posX, oLoop.posY);
						if(distance < targetDist || targetDist == 0){
							targetDist = distance;
							targetO = oLoop; 
							canMove = true;
						}
					}

					// Cast
					if(canMove){
						MG_ControlCommand.I.gameAction_robin_hookdance(current, targetO.posX, targetO.posY);
					}

					// Removes the targeters
					MG_ControlTargeters.I.forceRemoveTempTargeters();
				}
				#endregion
			}
		}

		//Ends the action
		MG_ControlAI.I.actionCD = 0.6f;
	}

	#region "Special Functions"
	// Get target variables
	int tarHP = 0;
	bool hasTarget = false;

	// Asses variables (used for assessing situation NOT for getting target)
	int nearestEnemyRange = 9999;
	public void assesSituation(MG_ClassUnit current, int range){
		//Creates the targeters
		if(range <= 1)
			MG_ControlTargeters.I._createTarg_Square(current.posX, current.posY, 1, true);
		else
			MG_ControlTargeters.I._createTargField(true, current.posX, current.posY, range);

		//Pick the weakest enemy within a targeter
		tarHP = 0;
		hasTarget = false;
		MG_ClassUnit atkTarget = MG_Globals.I.units[0];
		int dist = 0;
		foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
			// Asses enemy range
			dist = MG_CALC_Distance.I._distBetweenPoints (current.posX, current.posY, uLoop.posX, uLoop.posY);
			if (dist < nearestEnemyRange && dist > 0 && dist <= range) {
				if (uLoop.playerOwner == 1) {
					nearestEnemyRange = dist;
				}
			}

			// Get target
			if((uLoop.HP < tarHP || tarHP == 0) && uLoop.playerOwner == 1 && MG_GetTargeter.I._pointHasTargeterOfType(uLoop.posX, uLoop.posY, 2) && !uLoop.isInvulnerable){
				hasTarget = true;
				tarHP = uLoop.HP;
				atkTarget = uLoop;
			}
		}

		MG_ControlTargeters.I.forceRemoveTempTargeters();
	}

	public MG_ClassUnit getSingleAttackTarget(MG_ClassUnit current, int range){
		//Creates the targeters
		if(range <= 1)
			MG_ControlTargeters.I._createTarg_Square(current.posX, current.posY, 1, true);
		else
			MG_ControlTargeters.I._createTargField(true, current.posX, current.posY, current.atkRange);

		//Pick the weakest enemy within a targeter
		int tarHP = 0;
		bool hasTarget = false;
		MG_ClassUnit atkTarget = MG_Globals.I.units[0];
		foreach(MG_ClassUnit uLoop in MG_Globals.I.units){
			if((uLoop.HP < tarHP || tarHP == 0) && uLoop.playerOwner == 1 && MG_GetTargeter.I._pointHasTargeterOfType(uLoop.posX, uLoop.posY, 2) && !uLoop.isInvulnerable){
				hasTarget = true;
				tarHP = uLoop.HP;
				atkTarget = uLoop;
			}
		}

		MG_ControlTargeters.I.forceRemoveTempTargeters();
		return atkTarget;
	}
	#endregion
}
