using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HS_Database : MonoBehaviour {
	public static HS_Database I;
	public void Awake(){ I = this; }

	public Sprite port_none, port_robin, port_ajax, port_colt, port_victoria, port_amy, port_alicia, port_elderTreant, port_ragnaros, port_ifreet, port_specWitch, port_drakgul, port_masamune, port_siegfried, 
	port_abel, port_yukino, port_cody, port_hilde, port_elise, port_may, port_walter, port_aleric, port_eve;

	#region "Hero Portrait"
	public Sprite _getPortrait(int heroNum){
		Sprite retVal = port_none;

		switch (heroNum) {
			case 0: 			retVal = port_none; break;
			case 1: 			retVal = port_robin; break;
			case 2: 			retVal = port_ajax; break;
			case 3: 			retVal = port_colt; break;
			case 4: 			retVal = port_victoria; break;
			case 5: 			retVal = port_amy; break;
			case 6: 			retVal = port_alicia; break;
			case 7: 			retVal = port_elderTreant; break;
			case 8: 			retVal = port_ragnaros; break;
			case 9: 			retVal = port_ifreet; break;
			case 10: 			retVal = port_specWitch; break;
			case 11: 			retVal = port_drakgul; break;
			case 12: 			retVal = port_masamune; break;
			case 13: 			retVal = port_siegfried; break;
			case 14: 			retVal = port_abel; break;
			case 15: 			retVal = port_yukino; break;
			case 16: 			retVal = port_cody; break;
			case 17: 			retVal = port_hilde; break;
			case 18: 			retVal = port_elise; break;
			case 19: 			retVal = port_may; break;
			case 20: 			retVal = port_walter; break;
			case 21: 			retVal = port_aleric; break;
			case 22: 			retVal = port_eve; break;
		}

		return retVal;
	}
	#endregion

	#region "In-code Name"
	public int _getHeroIndexNum(string actualName){
		int retVal = -1;
		switch (actualName) {
			case "NONE":						retVal = 0;break;
			case "Robin": 						retVal = 1;break;
			case "Ajax": 						retVal = 2;break;
			case "Colt": 						retVal = 3;break;
			case "Victoria": 					retVal = 4;break;
			case "Amy": 						retVal = 5;break;
			case "Alicia": 						retVal = 6;break;
			case "ElderTreant": 				retVal = 7;break;
			case "Ragnaros": 					retVal = 8;break;
			case "Ifreet": 						retVal = 9;break;
			case "SpectralWitch": 				retVal = 10;break;
			case "Drakgul": 					retVal = 11;break;
			case "Masamune": 					retVal = 12;break;
			case "Siegfried": 					retVal = 13;break;
			case "Abel": 						retVal = 14;break;
			case "Yukino": 						retVal = 15;break;
			case "Cody": 						retVal = 16;break;
			case "Hilde": 						retVal = 17;break;
			case "Elise": 						retVal = 18;break;
			case "May": 						retVal = 19;break;
			case "Walter": 						retVal = 20;break;
			case "Aleric": 						retVal = 21;break;
			case "Eve": 						retVal = 22;break;
		}
		return retVal;
	}

	public string _getInCodeName(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = "NONE"; break;
			case 1: 			retVal = "Robin"; break;
			case 2: 			retVal = "Ajax"; break;
			case 3: 			retVal = "Colt"; break;
			case 4: 			retVal = "Victoria"; break;
			case 5: 			retVal = "Amy"; break;
			case 6: 			retVal = "Alicia"; break;
			case 7: 			retVal = "ElderTreant"; break;
			case 8: 			retVal = "Ragnaros"; break;
			case 9: 			retVal = "Ifreet"; break;
			case 10: 			retVal = "SpectralWitch"; break;
			case 11: 			retVal = "Drakgul"; break;
			case 12: 			retVal = "Masamune"; break;
			case 13: 			retVal = "Siegfried"; break;
			case 14: 			retVal = "Abel"; break;
			case 15: 			retVal = "Yukino"; break;
			case 16: 			retVal = "Cody"; break;
			case 17: 			retVal = "Hilde"; break;
			case 18: 			retVal = "Elise"; break;
			case 19: 			retVal = "May"; break;
			case 20: 			retVal = "Walter"; break;
			case 21: 			retVal = "Aleric"; break;
			case 22: 			retVal = "Eve"; break;
		}

		return retVal;
	}
	#endregion

	#region "Actual Name"
	public string _getNameActual(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = "Blank"; break;
			case 1: 			retVal = "Robin"; break;
			case 2: 			retVal = "Ajax"; break;
			case 3: 			retVal = "Colt"; break;
			case 4: 			retVal = "Faylinn"; break;
			case 5: 			retVal = "Amy"; break;
			case 6: 			retVal = "Alicia"; break;
			case 7: 			retVal = "Elder Treant"; break;
			case 8: 			retVal = "Ragnaros"; break;
			case 9: 			retVal = "Ifreet"; break;
			case 10: 			retVal = "Spectral Witch"; break;
			case 11: 			retVal = "Drakgul"; break;
			case 12: 			retVal = "Masamune"; break;
			case 13: 			retVal = "Siegfried"; break;
			case 14: 			retVal = "Abel"; break;
			case 15: 			retVal = "Yukino"; break;
			case 16: 			retVal = "Cody"; break;
			case 17: 			retVal = "Hilde"; break;
			case 18: 			retVal = "Elise"; break;
			case 19: 			retVal = "May"; break;
			case 20: 			retVal = "Walter"; break;
			case 21: 			retVal = "Aleric"; break;
			case 22: 			retVal = "Eve"; break;
		}

		return retVal;
	}
	#endregion

	#region "Description"
	public string _getDesc(int heroNum){
		string retVal = "";

		switch (heroNum) {
			case 0: 			retVal = " "; break;
			case 1: 			retVal = "Agile hero. Can quickly move around the field and deal area damage."; break;
			case 2: 			retVal = "Durable warrior. Can give buffs to nearby allies."; break;
			case 3: 			retVal = "Skilled gunslinger. Can disarm opponents, and deal huge damage."; break;
			case 4: 			retVal = "Heals and gives buffs to allies."; break;
			case 5: 			retVal = "A longbow master. Can scout and shoot enemies from afar."; break;
			case 6: 			retVal = "Durable hero. Can summon recruits and stun opponents."; break;
			case 7: 			retVal = "Roots enemy heroes while giving bonus armor and healing to allies."; break;
			case 8: 			retVal = "Has a huge health and deals more damage the more he fights."; break;
			case 9: 			retVal = "Durable hero. Deals area damage and executes weakened opponents."; break;
			case 10: 			retVal = "Can summon spectres that can move around the field with massive attack damage."; break;
			case 11: 			retVal = "Skilled shaman. Can scout around the map, remove enemy buffs and supply allies with mana."; break;
			case 12: 			retVal = "Deals huge damage especially when low on health."; break;
			case 13: 			retVal = "Quickly moves around the battlefield damaging and stunning enemies in his way."; break;
			case 14: 			retVal = "Has healing aura and can amplify allies' ability power."; break;
			case 15: 			retVal = "Deals area damage from afar, slows enemies up-close and silence enemy casters."; break;
			case 16: 			retVal = "Warrior hero. Crushes the armor of his target with every strike."; break;
			case 17: 			retVal = "Has high damage and long range spells and increases nearby allies' ability power."; break;
			case 18: 			retVal = "Cunning hero. Gains power when dealing or taking damage."; break;
			case 19: 			retVal = "A leader that can summon her Sacred Knights and inspires non-heroes to deal more damage."; break;
			case 20: 			retVal = "Warrior hero. Can cast spells twice per turn."; break;
			case 21: 			retVal = "Excellent at dealing with mass numbers of non-hero units."; break;
			case 22: 			retVal = "Has high life regen and uses her life to deal huge damage."; break;
		}

		return retVal;
	}
	#endregion
}
