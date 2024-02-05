using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DB : MonoBehaviour {
	public static C_DB I;
	public void Awake(){ I = this; }

	public int NUMBER_OF_CAMPAIGNS;

	public Sprite dummy, test1, test2;
	public Sprite port_ragnaros, port_specWitch, port_ifreet;

	void Start() {
		NUMBER_OF_CAMPAIGNS = 1; // Count starts at 0
	}

	public string getName(int campNum){
		string retVal = "";
		switch (campNum) {
			case 0: retVal = "1. Ragnaros"; break;
			case 1: retVal = "2. Spectral Witch"; break;
			case 2: retVal = "3. Ifreet"; break;
		}
		return retVal;
	}

	public Sprite getSprite(int campNum){
		Sprite retVal = dummy;
		switch (campNum) {
			case 0: retVal = port_ragnaros; break;
			case 1: retVal = port_specWitch; break;
			case 2: retVal = port_ifreet; break;
		}
		return retVal;
	}

	public Sprite getPortrait(int campNum){
		Sprite retVal = dummy;
		switch (campNum) {
			case 0: retVal = port_ragnaros; break;
			case 1: retVal = port_specWitch; break;
			case 2: retVal = port_ifreet; break;
		}
		return retVal;
	}

	public string getDesc(int campNum){
		string retVal = "";
		switch (campNum) {
			case 0: retVal = "Card Rewards:\nViking\nViking Raider"; break;
			case 1: retVal = "Card Rewards:\nSpectre"; break;
			case 2: retVal = "Card Rewards:\nFire Spawn\nFire Elemental"; break;
		}
		return retVal;
	}

	public string getMap(int campNum){
		string retVal = "";
		switch (campNum) {
			case 0: retVal = "Frozen Sanctuary"; break;
			case 1: retVal = "Stories02_mis01"; break;
			case 2: retVal = "Stories01_mis02"; break;
		}
		return retVal;
	}
	
	public int getMaxMapNum(int campNum){
		int retVal = 1;
		switch (campNum) {
			case 0: retVal = 1; break;
		}
		return retVal;
	}
}
