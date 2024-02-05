using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Barracks : MonoBehaviour {
	public static AI_Barracks I;
	public void Awake(){ I = this; }

	public bool DEBUG_MODE;

	public void _runAI(MG_ClassUnit current, Vector2 target, int moveToDo){
		MG_Globals.I.selectedUnit = current;

		bool willMove = true;
		DEBUG_MODE = false;

		//Next move to do
		if(DEBUG_MODE) Debug.Log(current.unitID + current.name + " issuing order " + moveToDo);
		switch(moveToDo){
			case 1:					_hire(current, target); break;
		}
	}

	public void _hire(MG_ClassUnit current, Vector2 target){
		//Creates the targeters
		MG_ControlTargeters.I._createTargField(false, current.posX, current.posY, 4, true, true);

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

		// Gold check
		bool canHireUnit = false;
		if(MG_Globals.I.players[current.playerOwner].gold >= 400){
			canHireUnit = true;
		}

		// Hire units here
		if (canHireUnit) {
			string hName = "";
			for (int i = 0; i < 6; i++) {
				if (MG_Globals.I.players [current.playerOwner].unit_lim[i] >= MG_Globals.I.players [current.playerOwner].unit_limMax[i] ||
					MG_Globals.I.players [current.playerOwner].gold < MG_Globals.I.players [current.playerOwner].unit_cost[i]) {
					continue;
				}

				hName = MG_Globals.I.players [current.playerOwner].unit_name [i];

				MG_ControlUnits.I._createUnit (hName, targetO.posX, targetO.posY, current.playerOwner);
				MG_GetUnit.I.getLastCreatedUnit().isHired = true;
				MG_ControlSFX.I._createSFX ("summonFx", targetO.posX, targetO.posY);
				MG_Globals.I.players [2].gold -= MG_Globals.I.players [2].unit_cost[i];
				MG_Globals.I.players [2].unit_lim [i]++;

				// Add waypoints here
				#region "Waypoints - ENCOUNTER, FROZEN SANCTUARY"
				MG_ClassUnit lHero = MG_GetUnit.I.getLastCreatedUnit ();
				int nextPoint = Random.Range (0, 3);
				if (nextPoint <= 0) {
					lHero.waypoints.Add (new Vector2 (0, 7));
				} else if (nextPoint <= 1) {
					lHero.waypoints.Add (new Vector2 (0, -7));
				}
				lHero.waypoints.Add (new Vector2 (0, 0));
              	#endregion
			}
		}

		//Removes the targeters, and ends the action
		MG_ControlTargeters.I.forceRemoveTempTargeters();
		MG_ControlAI.I.actionCD = 0.6f;
	}
}
