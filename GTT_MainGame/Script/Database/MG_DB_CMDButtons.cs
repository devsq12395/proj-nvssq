using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_CMDButtons : MonoBehaviour {
	public static MG_DB_CMDButtons I;
	public void Awake(){ I = this; }

	public Sprite dummy, move, attack, summon, skill, confirm, cancel, endTurn, cards, regButton;

	public Sprite _getSprite(string newSpriteName){
		Sprite retVal = dummy;
		switch (newSpriteName) {
			// Basics
			case "move": 					retVal = move; break;
			case "attack": 					retVal = attack; break;
			case "summon":case "hire": 		retVal = summon; break;
			case "skill":case "cast":  		retVal = skill; break;
			case "cards": 					retVal = cards; break;
			case "confirm": 				retVal = confirm; break;
			case "cancel": 					retVal = cancel; break;
			case "end turn": 				retVal = endTurn; break;

			case "regButton": 				retVal = regButton; break;
		}

		return retVal;
	}
}
