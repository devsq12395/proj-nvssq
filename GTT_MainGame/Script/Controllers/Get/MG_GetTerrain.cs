using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_GetTerrain : MonoBehaviour {
	public static MG_GetTerrain I;
	public void Awake(){ I = this; }

	#region "Point Checker"
	public MG_ClassTerrain _pickTerrain(int pickPosX, int pickPosY){
		MG_ClassTerrain ret = MG_Globals.I.terrains [0];

		foreach (MG_ClassTerrain ter in MG_Globals.I.terrains) {
			if (ter.posX == pickPosX && ter.posY == pickPosY) {
				ret = ter;
				break;
			}
		}

		return ret;
	}
	#endregion
}
