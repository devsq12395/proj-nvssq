using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlFogOfWar : MonoBehaviour {
	public static MG_ControlFogOfWar I;
	public void Awake(){ I = this; }

	public List<Vector2> revealedAreas, revealedAreas_temp, areasToDestroy;
	public bool revealAll = false;

	#region "Calculator functions"
	public void _addRevealedTempArea(float posX, float posY){
		Vector2 areaToAdd = new Vector2 (posX, posY);

		if (!revealedAreas_temp.Contains (areaToAdd)) {
			revealedAreas_temp.Add (areaToAdd);
		}
	}

	public void _addAreasFromTemp(){
		revealedAreas.AddRange (revealedAreas_temp);
	}

	public bool checkForSightBlocker(int posX, int posY){
		bool retVal = false;

		foreach (MG_ClassSightBlock sbL in MG_Globals.I.sightBlocker) {
			if (sbL.posX == posX && sbL.posY == posY) {
				retVal = true; 
				break;
			}
		}
		return retVal;
	}

	#region "Temp area removal"
	public void removeFromTemp(int posX, int posY, string orient){
		switch (orient) {
			case "n":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x == posX && revealedAreas_temp [vL].y >= posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
			case "s":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x == posX && revealedAreas_temp [vL].y <= posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
			case "e":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x >= posX && revealedAreas_temp [vL].y == posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
			case "w":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x <= posX && revealedAreas_temp [vL].y == posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
			case "nw":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x <= posX && revealedAreas_temp [vL].y >= posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
			case "ne":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x >= posX && revealedAreas_temp [vL].y >= posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
			case "sw":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x <= posX && revealedAreas_temp [vL].y <= posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
			case "se":
				for (int vL = 0; vL < revealedAreas_temp.Count; vL++) {
					if (revealedAreas_temp [vL].x >= posX && revealedAreas_temp [vL].y <= posY) {
						areasToDestroy.Add (revealedAreas_temp [vL]);
					}
				}
			break;
		}

		_destroyListed ();
	}

	public void _destroyListed(){
		if(areasToDestroy.Count > 0){
			foreach (Vector2 remAreas in areasToDestroy) {
				for(int i = 0; i < revealedAreas_temp.Count; i++){
					if(remAreas == revealedAreas_temp[i]){
						revealedAreas_temp.RemoveAt (i);
						break;
					}
				}
			}

			areasToDestroy.Clear ();
		}
	}
	#endregion
	#endregion

	public void _calculateReveal(){
		revealedAreas.Clear ();

		///////////////////// CALCULATE POINTS THAT ARE REVEALED /////////////////////
		//removeFromTemp(posX:, posY, orient);
		// Get revealed areas based from local player's owned units and enemy buildings
		foreach (MG_ClassUnit unit in MG_Globals.I.units) {
			if (unit.name == "testUnit001") continue;
			#region "Per unit calculation - 1 range sight for enemy buildings (Disabled for Emperors)"
			if(unit.isAHero){
				revealedAreas_temp.Clear();
				_addRevealedTempArea (unit.posX, unit.posY);
				_addAreasFromTemp ();
				revealedAreas_temp.Clear();
			}
			#endregion
			#region "Per unit calculation"
			if (unit.playerOwner == MG_Globals.I.curPlayerNum && unit.isAlive) {
				revealedAreas_temp.Clear ();

				int range = unit.sightRadius;
				int distX = 0, distY = 0, dist = 0;

				// add temp targeters
				for (int x = -range; x <= range; x++) {
				for (int y = -range; y <= range; y++) {
					dist = Mathf.Abs(x) +  Mathf.Abs(y);
					if (dist <= range) {
						_addRevealedTempArea (x + unit.posX, y + unit.posY);
					}
				}
				}

				// check the rest of the remaining sight temp for sight blockers
				int curValX = 0, curValY = 0, refX = unit.posX, refY = unit.posY, curRange = 1;

				if(!unit.isFlying){
					while(curRange < range){
						// starting at west -> north
						curValX = -curRange;
						curValY = 0;
						while (curValX < 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValY == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "w");
								} else {
									removeFromTemp (refX + curValX, refY + curValY, "nw");
								}
							}

							curValX++;
							curValY++;
						}

						// starting at north -> east
						curValX = 0;
						curValY = curRange;
						while (curValY > 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValX == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "n");
								} else {
									removeFromTemp (refX + curValX, refY + curValY, "ne");
								}
							}

							curValX++;
							curValY--;
						}

						// starting at east -> south
						curValX = curRange;
						curValY = 0;
						while (curValX > 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValY == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "e");
								}else {
									removeFromTemp (refX + curValX, refY + curValY, "se");
								}
							}

							curValX--;
							curValY--;
						}

						// starting at south -> east
						curValX = 0;
						curValY = -curRange;
						while (curValY < 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValX == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "s");
								}else {
									removeFromTemp (refX + curValX, refY + curValY, "sw");
								}
							}

							curValX--;
							curValY++;
						}

						curRange++;
					}
				}

				_addAreasFromTemp ();
				revealedAreas_temp.Clear();
			}
			#endregion
		}
		foreach (MG_ClassUnit unit in MG_Globals.I.unitsTemp) {
			if (unit.name == "testUnit001") continue;
			#region "Per unit calculation - 1 range sight for enemy buildings (Disabled for Emperors)"
			if(unit.isAHero){
				revealedAreas_temp.Clear();
				_addRevealedTempArea (unit.posX, unit.posY);
				_addAreasFromTemp ();
				revealedAreas_temp.Clear();
			}
			#endregion
			#region "Per unit calculation"
			if (unit.playerOwner == MG_Globals.I.curPlayerNum && unit.isAlive) {
				revealedAreas_temp.Clear ();

				int range = unit.sightRadius;
				int distX = 0, distY = 0, dist = 0;

				// add temp targeters
				for (int x = -range; x <= range; x++) {
					for (int y = -range; y <= range; y++) {
						dist = Mathf.Abs(x) +  Mathf.Abs(y);
						if (dist <= range) {
							_addRevealedTempArea (x + unit.posX, y + unit.posY);
						}
					}
				}

				// check the rest of the remaining sight temp for sight blockers
				int curValX = 0, curValY = 0, refX = unit.posX, refY = unit.posY, curRange = 1;

				if(!unit.isFlying){
					while(curRange < range){
						// starting at west -> north
						curValX = -curRange;
						curValY = 0;
						while (curValX < 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValY == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "w");
								} else {
									removeFromTemp (refX + curValX, refY + curValY, "nw");
								}
							}

							curValX++;
							curValY++;
						}

						// starting at north -> east
						curValX = 0;
						curValY = curRange;
						while (curValY > 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValX == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "n");
								} else {
									removeFromTemp (refX + curValX, refY + curValY, "ne");
								}
							}

							curValX++;
							curValY--;
						}

						// starting at east -> south
						curValX = curRange;
						curValY = 0;
						while (curValX > 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValY == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "e");
								}else {
									removeFromTemp (refX + curValX, refY + curValY, "se");
								}
							}

							curValX--;
							curValY--;
						}

						// starting at south -> east
						curValX = 0;
						curValY = -curRange;
						while (curValY < 0) {
							if (checkForSightBlocker (refX + curValX, refY + curValY)) {
								if (curValX == 0) {
									removeFromTemp (refX + curValX, refY + curValY, "s");
								}else {
									removeFromTemp (refX + curValX, refY + curValY, "sw");
								}
							}

							curValX--;
							curValY++;
						}

						curRange++;
					}
				}

				_addAreasFromTemp ();
				revealedAreas_temp.Clear();
			}
			#endregion
		}

		///////////////////// REVEAL / UNREVEAL OBJECTS /////////////////////
		// Re-calculate terrain if revealed or not
		// If terrain's position is in "revealedAreas", then reveal
		// Same goes with units
		Vector2 objPos = new Vector2(0, 0);
		foreach (MG_ClassTerrain ter in MG_Globals.I.terrains) {
			objPos.x = ter.posX;
			objPos.y = ter.posY;

			if (revealedAreas.Contains (objPos) || revealAll) {
				ter._reveal ();
			} else {
				ter._unreveal ();
			}
		}
		foreach (MG_ClassUnit unit in MG_Globals.I.unitsTemp) {
			if (unit.name == "testUnit001") continue;
			objPos.x = unit.posX;
			objPos.y = unit.posY;

			if (revealedAreas.Contains (objPos) || revealAll) {
				if(unit.moveByRevealCalc) unit._reveal ();
				else unit.isRevealed = true;
			} else {
				if(unit.moveByRevealCalc) unit._unreveal ();
				else unit.isRevealed = false;
			}
		}
		foreach (MG_ClassUnit unit in MG_Globals.I.units) {
			if (unit.name == "testUnit001") continue;
			objPos.x = unit.posX;
			objPos.y = unit.posY;

			if (revealedAreas.Contains (objPos) || revealAll) {
				if(unit.moveByRevealCalc) unit._reveal ();
				else unit.isRevealed = true;
			} else {
				if(unit.moveByRevealCalc) unit._unreveal ();
				else unit.isRevealed = false;
			}
		}
	}

	public void _recalculateRevealForUnit(MG_ClassUnit unit){
		Vector2 objPos = new Vector2(0, 0);
		objPos.x = unit.posX;
		objPos.y = unit.posY;

		if (revealedAreas.Contains (objPos)) {
			unit.isRevealed = true;
		} else {
			unit.isRevealed = false;
		}
	}

	public void _recalculateRevealForMissile(MG_ClassMissile missile){
		Vector2 objPos = new Vector2(0, 0);
		objPos.x = missile.posX;
		objPos.y = missile.posY;

		if (revealedAreas.Contains (objPos)) {
			missile.isRevealed = true;
		} else {
			missile.isRevealed = false;
		}
	}

	public bool _pointIsRevealed(int posX, int posY){
		Vector2 objPos = new Vector2(posX, posY);

		if (revealedAreas.Contains (objPos)) {
			return true;
		} else {
			return false;
		}
	}
}
