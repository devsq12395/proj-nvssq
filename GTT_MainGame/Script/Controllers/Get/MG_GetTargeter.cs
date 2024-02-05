using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_GetTargeter : MonoBehaviour {
	public static MG_GetTargeter I;
	public void Awake(){ I = this; }

	/*
	 * 1 = friendly, 2 = hostile
	 */

	#region "Point checker"
	public bool _pointHasTargeter(int pickPosX, int pickPosY){
		bool hasTar = false;

		foreach (MG_ClassTargeter targeter in MG_Globals.I.targeters) {
			if (targeter.posX == pickPosX && targeter.posY == pickPosY) {
				hasTar = true;
				break;
			}
		}
		if (!hasTar) {
			foreach (MG_ClassTargeter targeter in MG_Globals.I.targetersTemp) {
				if (targeter.posX == pickPosX && targeter.posY == pickPosY) {
					hasTar = true;
					break;
				}
			}
		}

		return hasTar;
	}

	public bool _pointHasTargeterOfType(int pickPosX, int pickPosY, int targeterType){
		bool hasTar = false;

		foreach (MG_ClassTargeter targeter in MG_Globals.I.targeters) {
			if (targeter.posX == pickPosX && targeter.posY == pickPosY && targeter.type == targeterType) {
				hasTar = true;
				break;
			}
		}
		if (!hasTar) {
			foreach (MG_ClassTargeter targeter in MG_Globals.I.targetersTemp) {
				if (targeter.posX == pickPosX && targeter.posY == pickPosY && targeter.type == targeterType) {
					hasTar = true;
					break;
				}
			}
		}

		return hasTar;
	}

	public MG_ClassTargeter _pickTargeter(int pickPosX, int pickPosY){
		MG_ClassTargeter ret = MG_Globals.I.targeters [0];
		bool hasTar = false;

		foreach (MG_ClassTargeter targeter in MG_Globals.I.targeters) {
			if (targeter.posX == pickPosX && targeter.posY == pickPosY) {
				hasTar = true;
				ret = targeter;
				break;
			}
		}
		if (!hasTar) {
			foreach (MG_ClassTargeter targeter in MG_Globals.I.targetersTemp) {
				if (targeter.posX == pickPosX && targeter.posY == pickPosY) {
					hasTar = true;
					ret = targeter;
					break;
				}
			}
		}

		return ret;
	}

	public MG_ClassTargeter _pickTargeterOfType(int pickPosX, int pickPosY, int targeterType){
		MG_ClassTargeter ret = MG_Globals.I.targeters [0];
		bool hasTar = false;

		foreach (MG_ClassTargeter targeter in MG_Globals.I.targeters) {
			if (targeter.posX == pickPosX && targeter.posY == pickPosY && targeter.type == targeterType) {
				hasTar = true;
				ret = targeter;
				break;
			}
		}
		if (!hasTar) {
			foreach (MG_ClassTargeter targeter in MG_Globals.I.targetersTemp) {
				if (targeter.posX == pickPosX && targeter.posY == pickPosY && targeter.type == targeterType) {
					hasTar = true;
					ret = targeter;
					break;
				}
			}
		}

		return ret;
	}
	#endregion
}
