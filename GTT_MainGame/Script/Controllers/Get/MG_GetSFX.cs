using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_GetSFX : MonoBehaviour {
	public static MG_GetSFX I;
	public void Awake(){ I = this; }

	#region "Get SFX from GameObject"
	public MG_ClassSFX _getSFXfromGameObject(GameObject go){
		if (MG_Globals.I.sfx.Count <= 0) {
			Debug.Log ("No SFX currently in game");
			return null;
		}

		MG_ClassSFX retVal = MG_Globals.I.sfx [0];
		bool isFound = false;
		foreach (MG_ClassSFX sfx in MG_Globals.I.sfx) {
			if (sfx.sprite == go) {
				retVal = sfx;
				isFound = true;
				break;
			}
		}
		if (!isFound) {
			Debug.Log ("SFX owner of GameObject not found. Will return null");
			return null;
		}

		return retVal;
	}
	#endregion
}
