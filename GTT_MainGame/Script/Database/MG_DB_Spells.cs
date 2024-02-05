using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Spells : MonoBehaviour {
	public static MG_DB_Spells I;
	public void Awake(){ I = this; }

	public Sprite port_dummy, port_heal, port_boost, port_massBlessing, port_vision, port_haste;

	public Sprite getPortrait(string spellName){
		Sprite retVal = port_dummy;
		switch (spellName) {
			case "Heal": retVal = port_heal; break;
			case "Boost": retVal = port_boost; break;
			case "MassBlessing": retVal = port_massBlessing; break;
			case "Vision": retVal = port_vision; break;
			case "Haste": retVal = port_haste; break;
		}
		return retVal;
	}

	public string getInGameName(string spellName){
		string retVal = "";
		switch (spellName) {
			case "Heal": retVal = "Heal"; break;
			case "Boost": retVal = "Boost"; break;
			case "MassBlessing": retVal = "Mass Blessing"; break;
			case "Vision": retVal = "Vision"; break;
			case "Haste": retVal = "Haste"; break;
		}
		return retVal;
	}
}
