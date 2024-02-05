using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Apprentice : MonoBehaviour {
	public static AI_Apprentice I;
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
		}
	}

	#region "Move"
	public void _move(MG_ClassUnit current, Vector2 target){
		// Check disable
		if(/*MG_ControlBuffs.I._unitIsStunned(current) || */MG_ControlBuffs.I._unitCannotMove(current)){
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
			if(distance < targetDist || targetDist == 0){
				targetDist = distance;
				targetO = oLoop; 
				canMove = true;
			}
		}

		//Moves the piece
		MG_ControlSounds.I._playSound(6, targetO.posX, targetO.posY, true);
		if(current.posX < target.x) facingRight = true;
		if (canMove)
			MG_ControlCommand.I.gameAction_move (current, targetO.posX, targetO.posY);
		if (facingRight) 		current._changeFacing ("right");
		else  					current._changeFacing ("left");
		MG_ControlSFX.I._createSFX("moveSmoke", targetO.posX, targetO.posY);

		//Removes the targeters, and ends the action
		MG_ControlTargeters.I.forceRemoveTempTargeters();
		MG_ControlAI.I.actionCD = 0.6f;

		// AI Waypoints
		if(current.waypoints.Count > 0){
			if(MG_CALC_Distance.I._distBetweenPoints (Mathf.RoundToInt(current.waypoints[0].x), Mathf.RoundToInt(current.waypoints[0].y), current.posX, current.posY) <= 2){
				current.waypoints.RemoveAt(0);
			}
		}
	}
	#endregion

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

	#region "Attack command"
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
	#endregion

	#region "Use Ability"
	public void useAbility(MG_ClassUnit current){
		// Check disable
		if(MG_ControlBuffs.I._unitIsStunned(current) || MG_ControlBuffs.I._unitCannotSkill(current)){
			MG_ControlAI.I.actionCD = 0.6f;
			return;
		}
		assesSituation (current, 6);

		//Uses the ability on a target enemy
		if(hasTarget && current.MP >= 50){
			Debug.Log (enemyCount);
			if (enemyCount >= 2) {
				#region "Barrier"
				MG_ControlCommand.I.gameAction_barrier(current);
				#endregion
			}
		}

		//Ends the action
		MG_ControlAI.I.actionCD = 0.6f;
	}
	#endregion

	#region "Special Functions"
	// Get target variables
	int tarHP = 0;
	bool hasTarget = false;

	// Asses variables (used for assessing situation NOT for getting target)
	int nearestEnemyRange = 9999,
	enemyCount = 0, allyCount = 0;
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
		foreach (MG_ClassUnit uLoop in MG_Globals.I.units) {
			// Asses enemy range
			dist = MG_CALC_Distance.I._distBetweenPoints (current.posX, current.posY, uLoop.posX, uLoop.posY);
			if (dist < nearestEnemyRange && dist > 0 && dist <= range) {
				if (uLoop.playerOwner == 1) {
					nearestEnemyRange = dist;
					enemyCount++;
				} else {
					allyCount++;
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

	public MG_ClassUnit getHealTarget(MG_ClassUnit current, int range){
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
			if((uLoop.HP < tarHP || tarHP == 0) && uLoop.playerOwner == 2 && MG_GetTargeter.I._pointHasTargeterOfType(uLoop.posX, uLoop.posY, 2) && !uLoop.isInvulnerable){
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
