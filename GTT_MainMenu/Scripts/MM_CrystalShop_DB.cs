using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_CrystalShop_DB : MonoBehaviour {
	public static MM_CrystalShop_DB I;
	public void Awake(){ I = this; }

	#region "Hero Portrait"
	public Sprite port_point, port_camp, port_tower, port_none, port_robin, port_ajax, port_colt, port_victoria, port_amy, port_alicia, port_elderTreant, port_ragnaros, port_ifreet, port_specWitch, port_drakgul, port_masamune, port_siegfried, 
	port_abel, port_yukino, port_cody, port_hilde, port_elise, port_may, port_walter, port_aleric, port_eve;

	public Sprite heroList_portrait(int heroNum){
		Sprite retVal = port_none;

		switch (heroNum) {
			case 0: 			retVal = port_point; break;
			case 1: 			retVal = port_camp; break;
			case 2: 			retVal = port_tower; break;

			case 3: 			retVal = port_robin; break;
			case 4: 			retVal = port_ajax; break;
			case 5: 			retVal = port_colt; break;
			case 6: 			retVal = port_victoria; break;
			case 7: 			retVal = port_amy; break;
			case 8: 			retVal = port_alicia; break;
			case 9: 			retVal = port_elderTreant; break;
			case 10: 			retVal = port_ragnaros; break;
			case 11: 			retVal = port_ifreet; break;
			case 12: 			retVal = port_specWitch; break;
			case 13: 			retVal = port_drakgul; break;
			case 14: 			retVal = port_masamune; break;
			case 15: 			retVal = port_siegfried; break;
			case 16: 			retVal = port_abel; break;
			case 17: 			retVal = port_yukino; break;
			case 18: 			retVal = port_cody; break;
			case 19: 			retVal = port_hilde; break;
			case 20: 			retVal = port_elise; break;

			case 21: 			retVal = port_may; break;
			case 22: 			retVal = port_walter; break;
			case 23: 			retVal = port_aleric; break;
			case 24: 			retVal = port_eve; break;
		}

		return retVal;
	}
	#endregion

	#region "In-code Name"
	public string heroList_inCodeName(int heroNum){
		string retVal = "NONE";

		switch (heroNum) {
			case 0: 			retVal = "Point"; break;
			case 1: 			retVal = "Camp"; break;
			case 2: 			retVal = "Tower"; break;

			case 3: 			retVal = "Robin"; break;
			case 4: 			retVal = "Ajax"; break;
			case 5: 			retVal = "Colt"; break;
			case 6: 			retVal = "Victoria"; break;
			case 7: 			retVal = "Amy"; break;
			case 8: 			retVal = "Alicia"; break;
			case 9: 			retVal = "ElderTreant"; break;
			case 10: 			retVal = "Ragnaros"; break;
			case 11: 			retVal = "Ifreet"; break;
			case 12: 			retVal = "SpectralWitch"; break;
			case 13: 			retVal = "Drakgul"; break;
			case 14: 			retVal = "Masamune"; break;
			case 15: 			retVal = "Siegfried"; break;
			case 16: 			retVal = "Abel"; break;
			case 17: 			retVal = "Yukino"; break;
			case 18: 			retVal = "Cody"; break;
			case 19: 			retVal = "Hilde"; break;
			case 20: 			retVal = "Elise"; break;

			case 21: 			retVal = "May"; break;
			case 22: 			retVal = "Walter"; break;
			case 23: 			retVal = "Aleric"; break;
			case 24: 			retVal = "Eve"; break;
		}

		return retVal;
	}
	#endregion

	#region "Get Skin Count"
	public int skinList_skinCount(string heroName){
		// Used to count skin pages
		int ret = 0;
		switch (heroName) {
			case "Point": ret = 4; break;
			case "Camp": ret = 4; break;
			case "Tower": ret = 1; break;

			case "Robin": ret = 2; break;
			case "Ajax": ret = 2; break;
			case "Colt": ret = 1; break;
			case "Victoria": ret = 2; break;
			case "Amy": ret = 2; break;
			case "Alicia": ret = 2; break;
			case "ElderTreant": ret = 1; break;
			case "Ragnaros": ret = 1; break;
			case "Ifreet": ret = 1; break;
			case "SpectralWitch": ret = 1; break;
			case "Drakgul": ret = 1; break;
			case "Masamune": ret = 1; break;
			case "Siegfried": ret = 1; break;
			case "Abel": ret = 1; break;
			case "Yukino": ret = 1; break;
			case "Cody": ret = 1; break;
			case "Hilde": ret = 1; break;
			case "Elise": ret = 1; break;

			case "May": ret = 1; break;
			case "Walter": ret = 1; break;
			case "Aleric": ret = 1; break;
			case "Eve": ret = 1; break;
		}

		return ret;
	}
	#endregion

	#region "Get Skin Name"
	public string skinList_skinName(string heroName, int skinNum){
		string ret = "NONE";
		switch (heroName) {
			#region "Point"
			case "Point":
				switch(skinNum){
					case 0: ret = "Default"; break;
					case 1: ret = "Banner of Chaos"; break;
					case 2: ret = "Kaiser's Banner"; break;
					case 3: ret = "UNIP Banner"; break;
				}
			break;
			#endregion
			#region "Camp"
			case "Camp":
				switch(skinNum){
					case 0: ret = "Default"; break;
					case 1: ret = "Brick House"; break;
					case 2: ret = "Goat Tower"; break;
					case 3: ret = "Sacrificial Pit"; break;
				}
			break;
			#endregion
			#region "Tower"
			case "Tower":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion

			#region "Robin"
			case "Robin":
				switch(skinNum){
					case 0: ret = "Default"; break;
					case 1: ret = "Strider"; break;
				}
			break;
			#endregion
			#region "Ajax"
			case "Ajax":
				switch(skinNum){
					case 0: ret = "Default"; break;
					case 1: ret = "Spartan"; break;
				}
			break;
			#endregion
			#region "Colt"
			case "Colt":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Victoria"
			case "Victoria":
				switch(skinNum){
					case 0: ret = "Default"; break;
					case 1: ret = "Halloween"; break;
				}
			break;
			#endregion
			#region "Amy"
			case "Amy":
				switch(skinNum){
					case 0: ret = "Default"; break;
					case 1: ret = "Long-skirt Maid"; break;
				}
			break;
			#endregion
			#region "Alicia"
			case "Alicia":
				switch(skinNum){
					case 0: ret = "Default"; break;
					case 1: ret = "Queen Bee"; break;
				}
			break;
			#endregion
			#region "ElderTreant"
			case "ElderTreant":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Ragnaros"
			case "Ragnaros":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Ifreet"
			case "Ifreet":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "SpectralWitch"
			case "SpectralWitch":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Drakgul"
			case "Drakgul":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Masamune"
			case "Masamune":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Siegfried"
			case "Siegfried":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Abel"
			case "Abel":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Yukino"
			case "Yukino":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Cody"
			case "Cody":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Hilde"
			case "Hilde":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Elise"
			case "Elise":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion

			#region "May"
			case "May":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Walter"
			case "Walter":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Aleric"
			case "Aleric":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
			#region "Eve"
			case "Eve":
				switch(skinNum){
					case 0: ret = "Default"; break;
				}
			break;
			#endregion
		}

		return ret;
	}
	#endregion

	#region "Get Skin Price"
	public int skinList_skinPrice(string heroName, int skinNum){
		int ret = 0;
		switch (heroName) {
			#region "Point"
			case "Point":
				switch(skinNum){
					case 0: ret = 0; break;
					case 1: ret = 200; break;
					case 2: ret = 200; break;
					case 3: ret = 200; break;
				}
			break;
			#endregion
			#region "Camp"
			case "Camp":
				switch(skinNum){
					case 0: ret = 0; break;
					case 1: ret = 200; break;
					case 2: ret = 200; break;
					case 3: ret = 200; break;
				}
			break;
			#endregion
			#region "Tower"
			case "Tower":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion

			#region "Robin"
			case "Robin":
				switch(skinNum){
					case 0: ret = 0; break;
					case 1: ret = 300; break;
					case 2: ret = 0; break;
				}
			break;
			#endregion
			#region "Ajax"
			case "Ajax":
				switch(skinNum){
					case 0: ret = 0; break;
					case 1: ret = 300; break;
				}
			break;
			#endregion
			#region "Colt"
			case "Colt":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Victoria"
			case "Victoria":
				switch(skinNum){
					case 0: ret = 0; break;
					case 1: ret = 400; break;
				}
			break;
			#endregion
			#region "Amy"
			case "Amy":
				switch(skinNum){
					case 0: ret = 0; break;
					case 1: ret = 400; break;
				}
			break;
			#endregion
			#region "Alicia"
			case "Alicia":
				switch(skinNum){
					case 0: ret = 0; break;
					case 1: ret = 400; break;
				}
			break;
			#endregion
			#region "ElderTreant"
			case "ElderTreant":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Ragnaros"
			case "Ragnaros":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Ifreet"
			case "Ifreet":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "SpectralWitch"
			case "SpectralWitch":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Drakgul"
			case "Drakgul":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Masamune"
			case "Masamune":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Siegfried"
			case "Siegfried":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Abel"
			case "Abel":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Yukino"
			case "Yukino":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Cody"
			case "Cody":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Hilde"
			case "Hilde":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Elise"
			case "Elise":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion

			#region "May"
			case "May":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Walter"
			case "Walter":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Aleric"
			case "Aleric":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
			#region "Eve"
			case "Eve":
				switch(skinNum){
					case 0: ret = 0; break;
				}
			break;
			#endregion
		}

		return ret;
	}
	#endregion

	#region "Get Skin Sprite"
	public Sprite dummy, skin_robin001, skin_robin002, skin_ajax001, skin_ajax002, skin_colt001, skin_victoria001, skin_victoria002, skin_amy001, skin_amy002, skin_alicia001, skin_alicia002, skin_elderTreant001, skin_ragnaros001, skin_ifreet001, skin_specWitch001, skin_drakgul001,
		skin_masamune001, skin_siegfried001, skin_abel001, skin_yukino001, skin_cody001, skin_hilde001, skin_elise001, skin_may001, skin_walter001, skin_aleric001, skin_eve001;
	public Sprite skin_point001, skin_point002, skin_point003, skin_point004, skin_camp001, skin_camp002, skin_camp003, skin_camp004, skin_tower001;
	public Sprite skinList_skinSprite(string heroName, int skinNum){
		Sprite ret = dummy;
		switch (heroName) {
			#region "Point"
			case "Point":
				switch(skinNum){
					case 0: ret = skin_point001; break;
					case 1: ret = skin_point002; break;
					case 2: ret = skin_point003; break;
					case 3: ret = skin_point004; break;
				}
			break;
			#endregion
			#region "Camp"
			case "Camp":
				switch(skinNum){
					case 0: ret = skin_camp001; break;
					case 1: ret = skin_camp002; break;
					case 2: ret = skin_camp003; break;
					case 3: ret = skin_camp004; break;
				}
			break;
			#endregion
			#region "Tower"
			case "Tower":
				switch(skinNum){
					case 0: ret = skin_tower001; break;
				}
			break;
			#endregion

			#region "Robin"
			case "Robin":
				switch(skinNum){
					case 0: ret = skin_robin001; break;
					case 1: ret = skin_robin002; break;
				}
			break;
			#endregion
			#region "Ajax"
			case "Ajax":
				switch(skinNum){
					case 0: ret = skin_ajax001; break;
					case 1: ret = skin_ajax002; break;
				}
			break;
			#endregion
			#region "Colt"
			case "Colt":
				switch(skinNum){
					case 0: ret = skin_colt001; break;
				}
			break;
			#endregion
			#region "Victoria"
			case "Victoria":
				switch(skinNum){
					case 0: ret = skin_victoria001; break;
					case 1: ret = skin_victoria002; break;
				}
			break;
			#endregion
			#region "Amy"
			case "Amy":
				switch(skinNum){
					case 0: ret = skin_amy001; break;
					case 1: ret = skin_amy002; break;
				}
			break;
			#endregion
			#region "Alicia"
			case "Alicia":
				switch(skinNum){
					case 0: ret = skin_alicia001; break;
					case 1: ret = skin_alicia002; break;
				}
			break;
			#endregion
			#region "ElderTreant"
			case "ElderTreant":
				switch(skinNum){
					case 0: ret = skin_elderTreant001; break;
				}
			break;
			#endregion
			#region "Ragnaros"
			case "Ragnaros":
				switch(skinNum){
					case 0: ret = skin_ragnaros001; break;
				}
			break;
			#endregion
			#region "Ifreet"
			case "Ifreet":
				switch(skinNum){
					case 0: ret = skin_ifreet001; break;
				}
			break;
			#endregion
			#region "SpectralWitch"
			case "SpectralWitch":
				switch(skinNum){
					case 0: ret = skin_specWitch001; break;
				}
			break;
			#endregion
			#region "Drakgul"
			case "Drakgul":
				switch(skinNum){
					case 0: ret = skin_drakgul001; break;
				}
			break;
			#endregion
			#region "Masamune"
			case "Masamune":
				switch(skinNum){
					case 0: ret = skin_masamune001; break;
				}
			break;
			#endregion
			#region "Siegfried"
			case "Siegfried":
				switch(skinNum){
					case 0: ret = skin_siegfried001; break;
				}
			break;
			#endregion
			#region "Abel"
			case "Abel":
				switch(skinNum){
					case 0: ret = skin_abel001; break;
				}
			break;
			#endregion
			#region "Yukino"
			case "Yukino":
				switch(skinNum){
					case 0: ret = skin_yukino001; break;
				}
			break;
			#endregion
			#region "Cody"
			case "Cody":
				switch(skinNum){
					case 0: ret = skin_cody001; break;
				}
			break;
			#endregion
			#region "Hilde"
			case "Hilde":
				switch(skinNum){
					case 0: ret = skin_hilde001; break;
				}
			break;
			#endregion
			#region "Elise"
			case "Elise":
				switch(skinNum){
					case 0: ret = skin_elise001; break;
				}
			break;
			#endregion

			#region "May"
			case "May":
				switch(skinNum){
					case 0: ret = skin_may001; break;
				}
			break;
			#endregion
			#region "Walter"
			case "Walter":
				switch(skinNum){
					case 0: ret = skin_walter001; break;
				}
			break;
			#endregion
			#region "Aleric"
			case "Aleric":
				switch(skinNum){
					case 0: ret = skin_aleric001; break;
				}
			break;
			#endregion
			#region "Eve"
			case "Eve":
				switch(skinNum){
					case 0: ret = skin_eve001; break;
				}
			break;
			#endregion
		}

		return ret;
	}
	#endregion

	#region "Get Skin Dimensions"
	public Vector2 skinList_skinDimensions(string heroName, int skinNum){
		Vector2 ret = new Vector2(0, 0);
		switch (heroName) {
			#region "Point"
			case "Point":
				switch(skinNum){
					case 0: ret = new Vector2(60.774f, 71.085f); break;
					case 1: ret = new Vector2(60.774f, 71.085f); break;
					case 2: ret = new Vector2(60.774f, 71.085f); break;
					case 3: ret = new Vector2(60.774f, 71.085f); break;
				}
			break;
			#endregion
			#region "Camp"
			case "Camp":
				switch(skinNum){
					case 0: ret = new Vector2(54.55f, 54.55f); break;
					case 1: ret = new Vector2(54.55f, 54.55f); break;
					case 2: ret = new Vector2(54.55f, 54.55f); break;
					case 3: ret = new Vector2(54.55f, 54.55f); break;
				}
			break;
			#endregion
			#region "Tower"
			case "Tower":
				switch(skinNum){
					case 0: ret = new Vector2(29.61f, 71.381f); break;
				}
			break;
			#endregion

			#region "Robin"
			case "Robin":
				switch(skinNum){
					case 0: ret = new Vector2(54.55f, 54.55f); break;
					case 1: ret = new Vector2(54.55f, 54.55f); break;
					case 2: ret = new Vector2(54.55f, 54.55f); break;
				}
			break;
			#endregion
			#region "Ajax"
			case "Ajax":
				switch(skinNum){
					case 0: ret = new Vector2(85.32f, 85.32f); break;
					case 1: ret = new Vector2(85.32f, 85.32f); break;
				}
			break;
			#endregion
			#region "Colt"
			case "Colt":
				switch(skinNum){
					case 0: ret = new Vector2(52.858f, 66.98f); break;
				}
			break;
 			#endregion
			#region "Victoria"
			case "Victoria":
				switch(skinNum){
					case 0: ret = new Vector2(54.27f, 70.43f); break;
					case 1: ret = new Vector2(49.44f, 70.43f); break;
				}
			break;
			#endregion
			#region "Amy"
			case "Amy":
				switch(skinNum){
					case 0: ret = new Vector2(60.64f, 75.39f); break;
					case 1: ret = new Vector2(60.64f, 75.39f); break;
				}
			break;
			#endregion
			#region "Alicia"
			case "Alicia":
				switch(skinNum){
					case 0: ret = new Vector2(64.243f, 80.17f); break;
					case 1: ret = new Vector2(64.243f, 80.17f); break;
				}
			break;
			#endregion
			#region "ElderTreant"
			case "ElderTreant":
				switch(skinNum){
					case 0: ret = new Vector2(75.82f, 75.817f); break;
				}
			break;
			#endregion
			#region "Ragnaros"
			case "Ragnaros":
				switch(skinNum){
					case 0: ret = new Vector2(75.51f, 75.51f); break;
				}
			break;
			#endregion
			#region "Ifreet"
			case "Ifreet":
				switch(skinNum){
					case 0: ret = new Vector2(54.55f, 54.55f); break;
				}
			break;
			#endregion
			#region "SpectralWitch"
			case "SpectralWitch":
				switch(skinNum){
					case 0: ret = new Vector2(62.71f, 73.75f); break;
				}
			break;
			#endregion
			#region "Drakgul"
			case "Drakgul":
				switch(skinNum){
					case 0: ret = new Vector2(82.56f, 82.55f); break;
				}
			break;
			#endregion
			#region "Masamune"
			case "Masamune":
				switch(skinNum){
					case 0: ret = new Vector2(60.49f, 94.3f); break;
				}
			break;
			#endregion
			#region "Siegfried"
			case "Siegfried":
				switch(skinNum){
					case 0: ret = new Vector2(68.016f, 68.02f); break;
				}
			break;
			#endregion
			#region "Abel"
			case "Abel":
				switch(skinNum){
					case 0: ret = new Vector2(70.07f, 70.07f); break;
				}
			break;
			#endregion
			#region "Yukino"
			case "Yukino":
				switch(skinNum){
					case 0: ret = new Vector2(63.85f, 63.85f); break;
				}
			break;
			#endregion
			#region "Cody"
			case "Cody":
				switch(skinNum){
					case 0: ret = new Vector2(84.06f, 84.06f); break;
				}
			break;
			#endregion
			#region "Hilde"
			case "Hilde":
				switch(skinNum){
					case 0: ret = new Vector2(81.59f, 81.59f); break;
				}
			break;
			#endregion
			#region "Elise"
			case "Elise":
				switch(skinNum){
					case 0: ret = new Vector2(75.37f, 75.37f); break;
				}
			break;
			#endregion

			#region "May"
			case "May":
				switch(skinNum){
					case 0: ret = new Vector2(58.56f, 58.56f); break;
				}
			break;
			#endregion
			#region "Walter"
			case "Walter":
				switch(skinNum){
					case 0: ret = new Vector2(58.56f, 58.56f); break;
				}
			break;
			#endregion
			#region "Aleric"
			case "Aleric":
				switch(skinNum){
					case 0: ret = new Vector2(60.11f, 60.11f); break;
				}
			break;
			#endregion
			#region "Eve"
			case "Eve":
				switch(skinNum){
					case 0: ret = new Vector2(52.33f, 52.33f); break;
				}
			break;
			#endregion
		}

		return ret;
	}
	#endregion

	#region "Get Skin Position"
	public Vector2 skinList_skinPosition(string heroName, int skinNum){
		Vector2 ret = new Vector2(0, 0);
		switch (heroName) {
			#region "Point"
			case "Point":
				switch(skinNum){
					case 0: ret = new Vector2(-108.79f, 5.9599f); break;
					case 1: ret = new Vector2(-108.79f, 5.9599f); break;
					case 2: ret = new Vector2(-108.79f, 5.9599f); break;
					case 3: ret = new Vector2(-108.79f, 5.9599f); break;
				}
			break;
			#endregion
			#region "Camp"
			case "Camp":
				switch(skinNum){
					case 0: ret = new Vector2(-106.5f, 1.1f); break;
					case 1: ret = new Vector2(-106.5f, 1.1f); break;
					case 2: ret = new Vector2(-106.5f, 1.1f); break;
					case 3: ret = new Vector2(-106.5f, 1.1f); break;
				}
			break;
			#endregion
			#region "Tower"
			case "Tower":
				switch(skinNum){
					case 0: ret = new Vector2(-111.74f, -5.5694f); break;
				}
			break;
			#endregion

			#region "Robin"
			case "Robin":
				switch(skinNum){
					case 0: ret = new Vector2(-106.5f, -2.21f); break;
					case 1: ret = new Vector2(-106.5f, -2.21f); break;
					case 2: ret = new Vector2(-106.5f, -2.21f); break;
				}
			break;
			#endregion
			#region "Ajax"
			case "Ajax":
				switch(skinNum){
					case 0: ret = new Vector2(-106.5f, 1.1f); break;
					case 1: ret = new Vector2(-106.5f, 1.1f); break;
				}
			break;
			#endregion
			#region "Colt"
			case "Colt":
				switch(skinNum){
					case 0: ret = new Vector2(-111.4f, -5.11f); break;
				}
			break;
			#endregion
			#region "Victoria"
			case "Victoria":
				switch(skinNum){
					case 0: ret = new Vector2(-112.62f, -6.84f); break;
					case 1: ret = new Vector2(-115.04f, -6.84f); break;
				}
			break;
			#endregion
			#region "Amy"
			case "Amy":
				switch(skinNum){
					case 0: ret = new Vector2(-112.99f, -6.742497f); break;
					case 1: ret = new Vector2(-112.99f, -6.742497f); break;
				}
			break;
			#endregion
			#region "Alicia"
			case "Alicia":
				switch(skinNum){
					case 0: ret = new Vector2(-112.12f, -10.08f); break;
					case 1: ret = new Vector2(-112.12f, -10.08f); break;
				}
			break;
			#endregion
			#region "ElderTreant"
			case "ElderTreant":
				switch(skinNum){
					case 0: ret = new Vector2(-115.31f, -7.7066f); break;
				}
			break;
			#endregion
			#region "Ragnaros"
			case "Ragnaros":
				switch(skinNum){
					case 0: ret = new Vector2(-112.42f, -4.82f); break;
				}
			break;
				#endregion
			#region "Ifreet"
			case "Ifreet":
				switch(skinNum){
					case 0: ret = new Vector2(-106.5f, 1.1f); break;
				}
			break;
			#endregion
			#region "SpectralWitch"
			case "SpectralWitch":
				switch(skinNum){
					case 0: ret = new Vector2(-109.02f, -2.49f); break;
				}
			break;
			#endregion
			#region "Drakgul"
			case "Drakgul":
				switch(skinNum){
					case 0: ret = new Vector2(-109.6f, -8.1f); break;
				}
			break;
			#endregion
			#region "Masamune"
			case "Masamune":
				switch(skinNum){
					case 0: ret = new Vector2(-110.25f, -10.7f); break;
				}
			break;
			#endregion
			#region "Siegfried"
			case "Siegfried":
				switch(skinNum){
					case 0: ret = new Vector2(-111.62f, -4.01f); break;
				}
			break;
			#endregion
			#region "Abel"
			case "Abel":
				switch(skinNum){
					case 0: ret = new Vector2(-105.58f, -1.18f); break;
				}
			break;
			#endregion
			#region "Yukino"
			case "Yukino":
				switch(skinNum){
					case 0: ret = new Vector2(-111.15f, -3.55f); break;
				}
			break;
			#endregion
			#region "Cody"
			case "Cody":
				switch(skinNum){
					case 0: ret = new Vector2(-104.36f, 3.24f); break;
				}
			break;
			#endregion
			#region "Hilde"
			case "Hilde":
				switch(skinNum){
					case 0: ret = new Vector2(-105.91f, 0.29f); break;
				}
			break;
			#endregion
			#region "Elise"
			case "Elise":
				switch(skinNum){
					case 0: ret = new Vector2(-110.49f, -2.89f); break;
				}
			break;
			#endregion

			#region "May"
			case "May":
				switch(skinNum){
					case 0: ret = new Vector2(-106.3f, 1.573f); break;
				}
			break;
			#endregion
			#region "Walter"
			case "Walter":
				switch(skinNum){
					case 0: ret = new Vector2(-109.28f, -3.814f); break;
				}
			break;
			#endregion
			#region "Aleric"
			case "Aleric":
				switch(skinNum){
					case 0: ret = new Vector2(-110.06f, -0.89499f); break;
				}
			break;
			#endregion
			#region "Eve"
			case "Eve":
				switch(skinNum){
					case 0: ret = new Vector2(-111.2f, 0.049983f); break;
				}
			break;
			#endregion
		}

		return ret;
	}
	#endregion

	#region "UNIT - In-code Name"
	public string unitList_inCodeName(int heroNum){
		string retVal = "NONE";

		switch (heroNum) {
			case 0: 			retVal = "Swordsman"; break;
			case 1: 			retVal = "Rifleman"; break;
			case 2: 			retVal = "Apprentice"; break;
			case 3: 			retVal = "Horseman"; break;
			case 4: 			retVal = "Officer"; break;
			case 5: 			retVal = "Medic"; break;
			case 6: 			retVal = "Goblin"; break;
			case 7: 			retVal = "GoblinArcher"; break;
			case 8: 			retVal = "Hellhound"; break;
			case 9: 			retVal = "Grenadier"; break;
			case 10: 			retVal = "OrcGrunt"; break;
			case 11: 			retVal = "OrcRifleman"; break;
			case 12: 			retVal = "ImperialInfantry"; break;
			case 13: 			retVal = "ImperialCavalry"; break;
			case 14: 			retVal = "ImperialSniper"; break;
		}

		return retVal;
	}
	#endregion

	#region "UNIT - Name"
	public string unitList_Name(int heroNum){
		string retVal = "NONE";

		switch (heroNum) {
			case 0: 			retVal = "Swordsman"; break;
			case 1: 			retVal = "Rifleman"; break;
			case 2: 			retVal = "Apprentice"; break;
			case 3: 			retVal = "Horseman"; break;
			case 4: 			retVal = "Officer"; break;
			case 5: 			retVal = "Medic"; break;
			case 6: 			retVal = "Goblin"; break;
			case 7: 			retVal = "Goblin Archer"; break;
			case 8: 			retVal = "Hellhound"; break;
			case 9: 			retVal = "Grenadier"; break;
			case 10: 			retVal = "Orc Grunt"; break;
			case 11: 			retVal = "Orc Rifleman"; break;
			case 12: 			retVal = "Imperial Infantry"; break;
			case 13: 			retVal = "Imperial Cavalry"; break;
			case 14: 			retVal = "Imperial Sniper"; break;
		}

		return retVal;
	}
	#endregion

	#region "UNIT - Desc"
	public string unitList_Desc(int heroNum){
		string retVal = "NONE";

		switch (heroNum) {
			case 0: 			retVal = "Basic melee unit with decent health and damage."; break;
			case 1: 			retVal = "Basic ranged unit with decent health and damage."; break;
			case 2: 			retVal = "Can cast Barrier which prevents allies from taking damage."; break;
			case 3: 			retVal = "Fast-moving melee unit, low health but has high damage."; break;
			case 4: 			retVal = "Expensive unit but deals high damage and inspires non-hero units."; break;
			case 5: 			retVal = "Ranged unit with different healing capabilities."; break;
			case 6: 			retVal = "Cheap unit with lower health and damage. Faster than most infantry units."; break;
			case 7: 			retVal = "Cheap ranged unit with lower health and damage. Faster than most infantry units and can outrange some guns."; break;
			case 8: 			retVal = "Deals high damage but has low health and is expensive."; break;
			case 9: 			retVal = "Strong but expensive unit. Can throw grenades."; break;
			case 10: 			retVal = "Basic orcish melee unit. Stronger than most basic human units."; break;
			case 11: 			retVal = "Basic orcish ranged unit. Has higher health compared to other gun units."; break;
			case 12: 			retVal = "Basic imperial unit. Stronger but more expensive than most infantry."; break;
			case 13: 			retVal = "Fast-moving melee unit. Stronger but more expensive than most cavalry."; break;
			case 14: 			retVal = "Has huge attack damage and can shoot from afar."; break;
		}

		return retVal;
	}
	#endregion

	#region "UNIT - Cost"
	public int unitList_Cost(int heroNum){
		int retVal = 0;

		switch (heroNum) {
			case 0: 			retVal = 0; break;
			case 1: 			retVal = 0; break;
			case 2: 			retVal = 0; break;
			case 3: 			retVal = 0; break;
			case 4: 			retVal = 0; break;
			case 5: 			retVal = 0; break;
			case 6: 			retVal = 100; break;
			case 7: 			retVal = 150; break;
			case 8: 			retVal = 250; break;
			case 9: 			retVal = 1000; break;
			case 10: 			retVal = 600; break;
			case 11: 			retVal = 600; break;
			case 12: 			retVal = 800; break;
			case 13: 			retVal = 800; break;
			case 14: 			retVal = 1000; break;
		}

		return retVal;
	}
	#endregion

	#region "Unit Portrait"
	public Sprite port_swordsman, port_rifleman, port_apprentice, port_horseman, port_officer, port_medic, port_goblin, port_goblinArcher, port_hellhound, 
		port_grenadier, port_orcGrunt, port_orcRifleman, port_impeInfantry, port_impeCavalry, port_impeSniper;

	public Sprite unitList_portrait(int heroNum){
		Sprite retVal = port_none;

		switch (heroNum) {
			case 0: 			retVal = port_swordsman; break;
			case 1: 			retVal = port_rifleman; break;
			case 2: 			retVal = port_apprentice; break;
			case 3: 			retVal = port_horseman; break;
			case 4: 			retVal = port_officer; break;
			case 5: 			retVal = port_medic; break;
			case 6: 			retVal = port_goblin; break;
			case 7: 			retVal = port_goblinArcher; break;
			case 8: 			retVal = port_hellhound; break;
			case 9: 			retVal = port_grenadier; break;
			case 10: 			retVal = port_orcGrunt; break;
			case 11: 			retVal = port_orcRifleman; break;
			case 12: 			retVal = port_impeInfantry; break;
			case 13: 			retVal = port_impeCavalry; break;
			case 14: 			retVal = port_impeSniper; break;
		}

		return retVal;
	}
	#endregion

	#region "SPELL - In-code Name"
	public string spellList_inCodeName(int heroNum){
		string retVal = "NONE";

		switch (heroNum) {
			case 0: 			retVal = "Heal"; break;
			case 1: 			retVal = "Boost"; break;
			case 2: 			retVal = "MassBlessing"; break;
			case 3: 			retVal = "Vision"; break;
			case 4: 			retVal = "Haste"; break;
		}

		return retVal;
	}
	#endregion

	#region "SPELL - Name"
	public string spellList_Name(int heroNum){
		string retVal = "NONE";

		switch (heroNum) {
			case 0: 			retVal = "Heal"; break;
			case 1: 			retVal = "Boost"; break;
			case 2: 			retVal = "Mass Blessing"; break;
			case 3: 			retVal = "Vision"; break;
			case 4: 			retVal = "Haste"; break;
		}

		return retVal;
	}
	#endregion

	#region "SPELL - Desc"
	public string spellList_Desc(int heroNum){
		string retVal = "NONE";

		switch (heroNum) {
			case 0: 			retVal = "Heals 200 HP to target units."; break;
			case 1: 			retVal = "Target units get +15 attack damage and +12% AP for 2 turns."; break;
			case 2: 			retVal = "Target units get +1 MS, +8% AP and +10 damage for 4 turns."; break;
			case 3: 			retVal = "Gains vision on target area for 2 turns."; break;
			case 4: 			retVal = "+4 MS to target units for a turn."; break;
		}

		return retVal;
	}
	#endregion

	#region "SPELL - Cost"
	public int spellList_Cost(int heroNum){
		int retVal = 0;

		switch (heroNum) {
			case 0: 			retVal = 0; break;
			case 1: 			retVal = 0; break;
			case 2: 			retVal = 0; break;
			case 3: 			retVal = 0; break;
			case 4: 			retVal = 0; break;
		}

		return retVal;
	}
	#endregion

	#region "Spell Portrait"
	public Sprite port_heal, port_boost, port_massBlessing, port_vision, port_haste;

	public Sprite spellList_portrait(int heroNum){
		Sprite retVal = port_none;

		switch (heroNum) {
			case 0: 			retVal = port_heal; break;
			case 1: 			retVal = port_boost; break;
			case 2: 			retVal = port_massBlessing; break;
			case 3: 			retVal = port_vision; break;
			case 4: 			retVal = port_haste; break;
		}

		return retVal;
	}
	#endregion
}
