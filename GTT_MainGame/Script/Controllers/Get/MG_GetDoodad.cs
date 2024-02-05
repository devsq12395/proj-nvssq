using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_GetDoodad : MonoBehaviour {
	public static MG_GetDoodad I;
	public void Awake(){ I = this; }
	
	public MG_ClassDoodad get_doodad(int pickPosX, int pickPosY){
		MG_ClassDoodad ret = null;

		foreach (MG_ClassDoodad dood in MG_Globals.I.doodads) {
			if (dood.posX == pickPosX && dood.posY == pickPosY) {
				ret = dood;
			}
		}

		return ret;
	}

	#region "Point checker"
	/// <summary>
	/// Returns true if point has doodad
	/// </summary>
	public bool _pointHasColliderDoodad(int pickPosX, int pickPosY){
		bool hasUnit = false;

		foreach (MG_ClassDoodad dood in MG_Globals.I.doodads) {
			if (!dood.isBlocker)
				continue;

			if (dood.posX == pickPosX && dood.posY == pickPosY) {
				hasUnit = true;
				break;
			}
		}

		return hasUnit;
	}
	#endregion

	#region "Get via GameObject"
	public MG_ClassDoodad _getDoodadWithGameObject(GameObject go){
		MG_ClassDoodad ret = null;

		foreach (MG_ClassDoodad dood in MG_Globals.I.doodads) {
			if (dood.sprite == go) {
				ret = dood;
				break;
			}
		}

		return ret;
	}
	#endregion
}
