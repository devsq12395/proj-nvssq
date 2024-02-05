using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class MG_ControlTargeters : MonoBehaviour {
	public static MG_ControlTargeters I;
	public void Awake(){ I = this; }

	private uint targCnt;
	public List<uint> targToDestroy;
	public float targeterScale;

	public void _createTarg(int newPosX, int newPosY, int targeterType, int rangeNum = 0, bool bypassCondition = false){
		if (
			/*Bypass condition*/ (!bypassCondition) &&
			/*Map border condition*/ (newPosX > MG_Globals.I.mapLimitX + 2 || newPosX < -MG_Globals.I.mapLimitX - 2 || newPosY > MG_Globals.I.mapLimitY + 2 || newPosY < -MG_Globals.I.mapLimitY - 2)
		) {
			return;
		}

		targCnt++;
		MG_Globals.I.targetersTemp.Add(new MG_ClassTargeter(MG_DB_Targeters.I._getSprite (targeterType), targCnt, newPosX, newPosY, targeterType, rangeNum));
	}

	#region "UPDATE"
	public void _update_targeterScale(){
		foreach (MG_ClassTargeter targ in MG_Globals.I.targeters) {
			if (targ.sprite.transform.localScale.x != targeterScale) {
				targ.sprite.transform.localScale = new Vector3 (targeterScale, targeterScale, targ.sprite.transform.localScale.z);
			}
		}
	}
	#endregion

	#region "CHECK - Target field is empty"
	// Return false if target is not empty AND if it's the border of the map
	public bool _checkIfTargFieldIsEmpty(int posX, int posY){
		if (posX > MG_Globals.I.mapLimitX+2 || posX < -MG_Globals.I.mapLimitX-2 ||
		   	posY > MG_Globals.I.mapLimitY+2 || posY < -MG_Globals.I.mapLimitY-2)
			return false;
		if (MG_GetUnit.I._pointHasUnit (posX, posY)) {
			if(MG_GetUnit.I._pickUnit(posX, posY).isRevealed) return false;
		}
		if (MG_GetDoodad.I._pointHasColliderDoodad (posX, posY)) {
			return false;
		}

		return true;
	}
	#endregion

	#region "Create Targeter - FIELD"
	public void _createTargField(bool isHostile, int origX, int origY, int Range, bool isMove = false, bool hasCollision = false, bool canTargetSelf = false){
		int tType = 1; if(isHostile) tType = 2;
		int curVal = 1, refX = origX, refY = origY;
		MG_ClassTargeter tmpObj;
		if (Range <= 0) Range = 2;

		//Creates the first targeters
		if (!MG_GetTargeter.I._pointHasTargeterOfType (refX, refY + 1, tType) && (_checkIfTargFieldIsEmpty(refX, refY+1) || !hasCollision)) { _createTarg (refX, refY + 1, tType, 1); }
		if (!MG_GetTargeter.I._pointHasTargeterOfType (refX, refY - 1, tType) && (_checkIfTargFieldIsEmpty(refX, refY-1) || !hasCollision)) { _createTarg (refX, refY - 1, tType, 1); }
		if (!MG_GetTargeter.I._pointHasTargeterOfType (refX + 1, refY, tType) && (_checkIfTargFieldIsEmpty(refX+1, refY) || !hasCollision)) { _createTarg (refX + 1, refY, tType, 1); }
		if (!MG_GetTargeter.I._pointHasTargeterOfType (refX - 1, refY, tType) && (_checkIfTargFieldIsEmpty(refX-1, refY) || !hasCollision)) { _createTarg (refX - 1, refY, tType, 1); }

		//While current value < range, keep creating targeters
		while(curVal < Range){
			//Checks every entity in the object temp list
			for(int loopObj = 0; loopObj < MG_Globals.I.targetersTemp.Count; loopObj++){
				//Sets a temporary variable for easier access
				tmpObj = MG_Globals.I.targetersTemp[loopObj];
				//Checks the current selected object's custom value if it should spawn objects
				if(tmpObj.rangeNum == curVal){
					//Sets a new reference point
					refX = tmpObj.posX;
					refY = tmpObj.posY;
					//Spawns the objects, second/third argument prevents spawning the targeter in original point
					if (isMove) {
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX, refY + 1, tType) && _checkIfTargFieldIsEmpty(refX, refY + 1) && !(refX == origX && refY + 1 == origY)){
							_createTarg(refX, refY + 1, tType, curVal + 1);}
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX, refY - 1, tType) && _checkIfTargFieldIsEmpty(refX, refY - 1) && !(refX == origX && refY - 1 == origY)){
							_createTarg(refX, refY - 1, tType, curVal + 1);}
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX + 1, refY, tType) && _checkIfTargFieldIsEmpty(refX + 1, refY) && !(refX + 1 == origX && refY == origY)){
							_createTarg(refX + 1, refY, tType, curVal + 1);}
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX - 1, refY, tType) && _checkIfTargFieldIsEmpty(refX - 1, refY) && !(refX - 1 == origX && refY == origY)){
							_createTarg(refX - 1, refY, tType, curVal + 1);}
					} else {
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX, refY + 1, tType) && !(refX == origX && refY + 1 == origY)){
							_createTarg(refX, refY + 1, tType, curVal + 1);}
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX, refY - 1, tType) && !(refX == origX && refY - 1 == origY)){
							_createTarg(refX, refY - 1, tType, curVal + 1);}
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX + 1, refY, tType) && !(refX + 1 == origX && refY == origY)){
							_createTarg(refX + 1, refY, tType, curVal + 1);}
						if(!MG_GetTargeter.I._pointHasTargeterOfType(refX - 1, refY, tType) && !(refX - 1 == origX && refY == origY)){
							_createTarg(refX - 1, refY, tType, curVal + 1);}
					}
				}
			}
			//Increase the curVal
			curVal++;
		}

		//Collision
		if(hasCollision)
			foreach(MG_ClassTargeter oL in MG_Globals.I.targetersTemp){
				if(oL.type == tType){
					if(!_checkIfTargFieldIsEmpty(oL.posX, oL.posY)){
						oL.isAlive = false;
						targToDestroy.Add(oL.id);
					}
				}
			}

		//Spawns a targeter on original point if canTargetSelf is true
		if(canTargetSelf)		_createTarg(origX, origY, tType, 0);

		// Spawn animation start
		_tweenAnimationStart();
	}
	#endregion
	#region "Create Targeter - SQUARE"
	/// <summary>
	/// 1 is the smallest square
	/// </summary>
	public void _createTarg_Square(int origX, int origY, int size, bool isHostile, bool bypassCondition = false){
		int tType = 1; if(isHostile) tType = 2;

		for (int x = -size; x <= size; x++) {
			for (int y = -size; y <= size; y++) {
				if (x == 0 && y == 0) 		continue;

				_createTarg (origX + x, origY + y, tType, 0, bypassCondition);
			}
		}

		// Spawn animation start
		_tweenAnimationStart();
	}
	#endregion

	#region "Create Targeter - TWEEN ANIMATION START"
	public void _tweenAnimationStart(){
		// Spawn Tween
		float firstScale = 0.01f;
		float targetScale = 0.25f;
		float duration = 0.15f;
		gameObject.Tween("targeterTween_" + targCnt.ToString(), firstScale, targetScale, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			targeterScale = t.CurrentValue;
		}, (t) =>{
			// completion
			targeterScale = t.CurrentValue;
		});
	}
	#endregion

	#region "Force remove temp targeters"
	public void forceRemoveTempTargeters(){
		foreach (MG_ClassTargeter targ in MG_Globals.I.targetersTemp) {
			_addToDestroyList (targ);
		}
	}
	#endregion
	#region "Remove unit"
	/// <summary>
	/// Does not clear targetersTemp, to clear targetersTemp use forceRemoveTempTargeters()
	/// </summary>
	public void _clearTargeters(){
		foreach (MG_ClassTargeter targ in MG_Globals.I.targeters) {
			_addToDestroyList (targ);
		}
		targCnt = 0;
	}

	public void _addToDestroyList(MG_ClassTargeter targeter){
		targToDestroy.Add(targeter.id);
		targeter.isAlive = false;
	}

	public void _destroyListed(){
		if(targToDestroy.Count > 0){
			for(int listedUnits = 0; listedUnits < targToDestroy.Count; listedUnits++){
				_destroyTarg(listedUnits);
			}
			targToDestroy.Clear();
		}
	}

	public void _destroyTarg(int targetUnitIndex){
		int indexToRemove = -1;
		for(int desUnitLoop = MG_Globals.I.targeters.Count - 1; desUnitLoop >= 0; desUnitLoop--){
			if(targToDestroy[targetUnitIndex] == MG_Globals.I.targeters[desUnitLoop].id){
				indexToRemove = desUnitLoop;
				break;
			}
		}
		if(indexToRemove > -1){
			Destroy(MG_Globals.I.targeters[indexToRemove].sprite);
			MG_Globals.I.targeters.RemoveAt(indexToRemove);
		}
	}
	#endregion
}
