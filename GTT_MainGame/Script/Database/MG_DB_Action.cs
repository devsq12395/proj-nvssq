using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Action : MonoBehaviour {
	public static MG_DB_Action I;
	public void Awake(){ I = this; }

	public Sprite i_none, i_test;
	public Sprite getPort (string actionName){
		Sprite retVal = i_none;
		switch (actionName){
			case "Hookshot": retVal = i_test; break;
		}

		return retVal;
	}

	public string uiName, uiDesc, uiMP, uiCD;
	public void _setActionData(string actionName){
		switch(actionName){
			#region "TestAction"
			case "Hookshot":
				uiName = "Hookshot";
				uiDesc = "Moves to target area with a hookshot.";
				uiMP = "50";
			break;
				#endregion
		}
	}
}
