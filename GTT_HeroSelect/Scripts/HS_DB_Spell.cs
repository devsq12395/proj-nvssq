using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_DB_Spell : MonoBehaviour {
	public static HS_DB_Spell I;
	public void Awake(){ I = this; }

	public Sprite port_none, port_test, port_heal, port_boost, port_massBlessing, port_vision, port_haste;

	#region "Spell Portrait"
	public Sprite _getPortrait(int heroNum){
		Sprite retVal = port_none;

		switch (heroNum) {
			case 0: 			retVal = port_none; break;
			case 1: 			retVal = port_heal; break;
			case 2: 			retVal = port_boost; break;
			case 3: 			retVal = port_massBlessing; break;
			case 4: 			retVal = port_vision; break;
			case 5: 			retVal = port_haste; break;
		}

		return retVal;
	}
	#endregion

	#region "In-code Name"
	public int _getUnitIndexNum(string actualName){
		int retVal = -1;
		switch (actualName) {
			case "Blank":						retVal = 0;break;
			case "Heal": 						retVal = 1;break;
			case "Boost": 						retVal = 2;break;
			case "MassBlessing": 				retVal = 3;break;
			case "Vision": 						retVal = 4;break;
			case "Haste": 						retVal = 5;break;
		}
		return retVal;
	}

	public string _getInCodeName(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = "NONE"; break;
			case 1: 			retVal = "Heal"; break;
			case 2: 			retVal = "Boost"; break;
			case 3: 			retVal = "MassBlessing"; break;
			case 4: 			retVal = "Vision"; break;
			case 5: 			retVal = "Haste"; break;
		}

		return retVal;
	}
	#endregion

	#region "Actual Name"
	public string _getNameActual(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = "Blank"; break;
			case 1: 			retVal = "Heal"; break;
			case 2: 			retVal = "Boost"; break;
			case 3: 			retVal = "Mass Blessing"; break;
			case 4: 			retVal = "Vision"; break;
			case 5: 			retVal = "Haste"; break;
		}

		return retVal;
	}
	#endregion

	#region "Description"
	public string _getDesc(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = " "; break;
			case 1: 			retVal = "Heals 200 HP to target units."; break;
			case 2: 			retVal = "Target units get +15 attack damage and +12% AP for 2 turns."; break;
			case 3: 			retVal = "Target units get +1 MS, +8% AP and +10 damage for 4 turns."; break;
			case 4: 			retVal = "Gains vision on target area for 2 turns."; break;
			case 5: 			retVal = "+4 MS to target units for a turn."; break;
		}

		return retVal;
	}
   	#endregion
}
