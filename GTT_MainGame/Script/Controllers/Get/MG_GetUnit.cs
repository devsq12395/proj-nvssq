using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_GetUnit : MonoBehaviour {
	public static MG_GetUnit I;
	public void Awake(){ I = this; }

	public void start(){
		us_name = new List<string> ();
		us_id = new List<int> ();
	}

	// This stores a unit in a list
	// So you can get its ID later
	// This will NOT check if the unit is alive or not
	#region "Unit storer"
	public List<string> us_name;
	public List<int> us_id;

	public void us_storeUnit(MG_ClassUnit unit, string codeName){
		us_id.Add (unit.unitID);
		us_name.Add (codeName);
	}

	public MG_ClassUnit us_getUnit(string unitStoreName){
		MG_ClassUnit retVal = null;
		for (int i = 0; i < us_name.Count; i++) {
			if (unitStoreName == us_name [i]) {
				retVal = _getUnitWithID (us_id [i]);
			}
		}
		return retVal;
	}
	#endregion

	#region "Point checker"
	public bool _pointHasUnit(int pickPosX, int pickPosY){
		bool hasUnit = false;

		foreach (MG_ClassUnit unit in MG_Globals.I.units) {
			if (unit.posX == pickPosX && unit.posY == pickPosY && unit.isAlive) {
				hasUnit = true;
				break;
			}
		}
		if (!hasUnit) {
			foreach (MG_ClassUnit unit in MG_Globals.I.unitsTemp) {
				if (unit.posX == pickPosX && unit.posY == pickPosY && unit.isAlive) {
					hasUnit = true;
					break;
				}
			}
		}

		return hasUnit;
	}

	public MG_ClassUnit _pickUnit(int pickPosX, int pickPosY){
		MG_ClassUnit ret = MG_Globals.I.units [0];
		bool hasUnit = false;

		foreach (MG_ClassUnit unit in MG_Globals.I.units) {
			if (unit.posX == pickPosX && unit.posY == pickPosY && unit.isAlive) {
				hasUnit = true;
				ret = unit;
				break;
			}
		}
		if (!hasUnit) {
			foreach (MG_ClassUnit unit in MG_Globals.I.unitsTemp) {
				if (unit.posX == pickPosX && unit.posY == pickPosY && unit.isAlive) {
					hasUnit = true;
					ret = unit;
					break;
				}
			}
		}

		return ret;
	}

	public int _pointHasUnit_GetID(int pickPosX, int pickPosY){
		int uId = -1;

		foreach (MG_ClassUnit unit in MG_Globals.I.units) {
			if (unit.posX == pickPosX && unit.posY == pickPosY && unit.isAlive) {
				uId = unit.unitID;
				break;
			}
		}
		if (uId < 0) {
			foreach (MG_ClassUnit unit in MG_Globals.I.unitsTemp) {
				if (unit.posX == pickPosX && unit.posY == pickPosY && unit.isAlive) {
					uId = unit.unitID;
					break;
				}
			}
		}

		return uId;
	}
   	#endregion
	#region "Unit ID checker"
	public bool _checkIfUnitWithIDExists(int unitID){
		bool hasUnit = false;

		foreach (MG_ClassUnit unit in MG_Globals.I.units) {
			if (unit.unitID == unitID) {
				hasUnit = true;
				break;
			}
		}
		if (!hasUnit) {
			foreach (MG_ClassUnit unit in MG_Globals.I.unitsTemp) {
				if (unit.unitID == unitID) {
					hasUnit = true;
					break;
				}
			}
		}

		return hasUnit;
	}

	public MG_ClassUnit _getUnitWithID(int unitID){
		MG_ClassUnit ret = MG_Globals.I.units [0];
		bool hasUnit = false;

		foreach (MG_ClassUnit unit in MG_Globals.I.units) {
			if (unit.unitID == unitID) {
				hasUnit = true;
				ret = unit;
				break;
			}
		}
		if (!hasUnit) {
			foreach (MG_ClassUnit unit in MG_Globals.I.unitsTemp) {
				if (unit.unitID == unitID) {
					hasUnit = true;
					ret = unit;
					break;
				}
			}
		}

		return ret;
	}
	#endregion

	#region "Get last created unit"
	/// <summary>
	/// USE WITH CAUTION
	/// </summary>
	public MG_ClassUnit getLastCreatedUnit(){
		Debug.Log(MG_Globals.I.unitsTemp [MG_Globals.I.unitsTemp.Count-1].name);
		return MG_Globals.I.unitsTemp [MG_Globals.I.unitsTemp.Count-1];
	}
	#endregion

	#region "Nearby Empty Tile Checker"
	/// <summary>
	/// Returns true if target point is empty
	/// </summary>
	public bool _pointIsEmpty(int posX, int posY){
		bool isEmpty = true;
		bool hasUnit = _pointHasUnit (posX, posY);
		bool hasDood = MG_GetDoodad.I._pointHasColliderDoodad (posX, posY);

		if (hasUnit || hasDood) isEmpty = false;

		return isEmpty;
	}

	/// <summary>
	/// Checks for Units and Doodads
	/// </summary>
	public Vector2 _getNearbyEmptyTile(int posX, int posY){
		int curRange = 0;
		bool hasEmptyTile = false;
		Vector2 emptyTile = new Vector2 (0, 0);
		if (_pointIsEmpty (posX, posY)) {
			emptyTile.x = posX;
			emptyTile.y = posY;
			return emptyTile;
		}

		while (!hasEmptyTile) {
			for (int x = -curRange; x <= curRange; x++) {
				for (int y = -curRange; y <= curRange; y++) {
					if (_pointIsEmpty (posX + x, posY + y)) {
						hasEmptyTile = true;
						emptyTile.x = posX + x;
						emptyTile.y = posY + y;
						break;
					}
				}
			}

			if (!hasEmptyTile)
				curRange++;
		}

		return emptyTile;
	}
	#endregion
}
