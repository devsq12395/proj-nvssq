using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlAura : MonoBehaviour {
	public static MG_ControlAura I;
	public void Awake() { I = this; }

	public List<MG_ClassAura> listAura;
	public List<int> aurasToDestroy;
	public int numAura;

	public bool runTemp;

	public void _start() {
		listAura = new List<MG_ClassAura> ();
		aurasToDestroy = new List<int> ();
	}

	#region "Create Aura"
	public void _createAura(MG_ClassUnit newAuraOwner, string newAuraType, string newBuffType, int newSquareRange){
		listAura.Add(new MG_ClassAura(MG_DB_Aura.I._getSprite(newAuraType, newAuraOwner.playerOwner), newAuraType, newAuraOwner, numAura, newBuffType, newSquareRange));
		numAura++;
		MG_ControlEvents.I._addEvent("Wait", new string[]{"0.1", "6"});
	}
	#endregion

	// Returns the aura range if unit has aura, 0 if unit has no aura
	#region "Check unit has aura"
	public int _checkUnitHasAura(int targetUnitID){
		int output = 0;
		foreach(MG_ClassAura aL in listAura){
			if(aL.ownerID == targetUnitID){
				output = aL.squareRange;
				break;
			}
		}
		return output;
	}
	#endregion

	#region "Check aura's owner is alive"
	public void _checkAuraHasOwner(){
		bool hasOwner = false;
		foreach (MG_ClassAura aL in listAura) {
			foreach (MG_ClassUnit uL in MG_Globals.I.units) {
				if (aL.ownerID == uL.unitID && uL.isAlive) {
					hasOwner = true;
					break;
				}
			}
			if (!hasOwner) {
				_addToDestroyList (aL.auraID);
				MG_ControlBuffs.I._removeBuffOfType (aL.buffType);
				MG_ControlBuffs.I._removeBuffOfType (aL.buffType + " Temp");
			}
			hasOwner = false;
		}
	}
	#endregion

	// Called whenever a unit moves
	// Checks the list if an aura is registered to that unit
	// And moves that aura along with the unit
	#region "Move Aura"
	public void _moveAura(MG_ClassUnit movingUnit){
		int movPosX = movingUnit.posX, movPosY = movingUnit.posY;
		foreach(MG_ClassAura aL in listAura){
			if(aL.ownerID == movingUnit.unitID){
				aL.posX = movingUnit.posX;
				aL.posY = movingUnit.posY;
				aL.sprite.transform.position = new Vector3(movingUnit.sprite.transform.position.x, movingUnit.sprite.transform.position.y, 2000);
			}
		}
		_reincludeBuffs(true);
	}
	#endregion

	/// This function removes all aura buffs, picks each auras and reattach all their buffs
	#region "Reinclude Buffs"
	public void _reincludeBuffs(bool isTemp) {
		bool hasOwner;
		MG_ClassUnit auraOwner;
		List<MG_ClassUnit> combinedUnitList = new List<MG_ClassUnit> ();
		combinedUnitList.AddRange (MG_Globals.I.units);
		combinedUnitList.AddRange (MG_Globals.I.unitsTemp);

		foreach(MG_ClassAura aL in listAura) {
			//Remove the buffs
			//Remove the actual buff if temp
			if(isTemp) {
				MG_ControlBuffs.I._removeBuffOfType(aL.buffType);
			}else{
				MG_ControlBuffs.I._removeBuffOfType(aL.buffType + " Temp");
			}

			//Detects if this aura still has its owner
			hasOwner = MG_GetUnit.I._checkIfUnitWithIDExists(aL.ownerID);
			if (hasOwner) {
				auraOwner = MG_GetUnit.I._getUnitWithID(aL.ownerID);

				if (MG_DB_Aura.I._getCond_TargetIsEnemy (aL.type)) {
					//This aura will affect enemies and will now start to detect each enemy
					foreach (MG_ClassUnit uL in combinedUnitList) {
						if (uL.playerOwner != aL.playerOwner) {
							if (MG_CALC_Distance.I._distUnits (auraOwner, uL) <= aL.squareRange && addAuraConditions(uL, aL.buffType))
							if (isTemp)
								MG_ControlBuffs.I._addBuff (uL, aL.buffType + " Temp");
							else
								MG_ControlBuffs.I._addBuff (uL, aL.buffType);
						}
					}
				} else {
					//This aura will affect allies and will now start to detect each ally
					foreach (MG_ClassUnit uL in combinedUnitList) {

						if (uL.playerOwner == aL.playerOwner) {
							if (MG_CALC_Distance.I._distUnits (auraOwner, uL) <= aL.squareRange && addAuraConditions(uL, aL.buffType)) {
								if (isTemp)
									MG_ControlBuffs.I._addBuff (uL, aL.buffType + " Temp");
								else
									MG_ControlBuffs.I._addBuff (uL, aL.buffType);
							}
						}
					}
				}
			}
			// Remove the aura if the owner does not exist
			else {
				_addToDestroyList (aL.auraID);
			}
		}
		for (int i = combinedUnitList.Count - 1; i >= 0; i--) combinedUnitList.RemoveAt (i);

		//Tells the MainGameScript to run the non-temp version of this function on next frame
		if(isTemp)
			runTemp = true;
		else 
			runTemp = false;
	}
	#endregion
	#region "Add Aura conditions"
	/// <summary>
	/// Returns TRUE if aura can be added.
	/// </summary>
	public bool addAuraConditions(MG_ClassUnit target, string auraName){
		if ((auraName == "WarlordAura" || auraName == "WarlordAura Temp") && target.isAHero) return false;
		if ((auraName == "HonorCode" || auraName == "HonorCode Temp") && target.isAHero) return false;
		if ((auraName == "OfficersAura" || auraName == "OfficersAura Temp") && target.isAHero) return false;
		if ((auraName == "TradingPostAura" || auraName == "TradingPostAura Temp") && (target.name != "Farm" && target.name != "Plantation")) return false;

		return true;
	}
	#endregion

	#region "Removal Codes"
	public void _addToDestroyList(int targetID){
		for (int i = 0; i < listAura.Count; i++) {
			if (listAura [i].auraID == targetID) {
				listAura[i].isAlive = false;
				_reincludeBuffs(true);
				aurasToDestroy.Add(listAura [i].auraID);
				break;
			}
		}
	}

	public void _destroyListed(){
		for(int dLoop = 0; dLoop < aurasToDestroy.Count; dLoop++){
			_removeAura(dLoop);
		}
		aurasToDestroy.Clear();
	}

	private void _removeAura(int targetIndex){
		int indexToRemove = -1;
		for(int dbLoop = listAura.Count-1; dbLoop >= 0;dbLoop--){
			if(aurasToDestroy[targetIndex] == listAura[dbLoop].auraID){
				indexToRemove = dbLoop;
				break;
			}
		}
		if(indexToRemove > -1){
			Destroy(listAura[indexToRemove].sprite);
			listAura.RemoveAt(indexToRemove);
		}
	}
	#endregion
}
