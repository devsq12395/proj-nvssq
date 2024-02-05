using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CW_DB : MonoBehaviour {
	public static CW_DB I;
	public void Awake(){ I = this; }

	public int NUMBER_OF_CAMPAIGNS;

	public Sprite dummy, test1, test2;
	public Sprite port_test1, port_test2;

	void Start() {
		NUMBER_OF_CAMPAIGNS = 1; // Count starts at 0
	}

	public string getName(int campNum){
		string retVal = "";
		switch (campNum) {
			case 0: retVal = "I. The Crossing"; break;
			case 1: retVal = "II. Empire of the Damned"; break;
		}
		return retVal;
	}

	public Sprite getSprite(int campNum){
		Sprite retVal = dummy;
		switch (campNum) {
			case 0: retVal = test1; break;
			case 1: retVal = test2; break;
		}
		return retVal;
	}

	public Sprite getPortrait(int campNum){
		Sprite retVal = dummy;
		switch (campNum) {
			case 0: retVal = port_test1; break;
			case 1: retVal = port_test2; break;
		}
		return retVal;
	}

	public string getDesc(int campNum){
		string retVal = "";
		switch (campNum) {
			case 0: retVal = @"""And so we started our crossing to the World of Terra. " +
					"Who knows what could be waiting on the other side of this gate.\n\n" +
					"There is not much time. Walter and his forces are getting more powerful the more we delay.\n\n" +
					@"We have already beaten him once. In Terra we shall bring this all to an end.""" +
			""; break;
			case 1: retVal = @"""At last with our capture of Fort Mons this region of Palude is ours. " +
					"I expect the Ragnians to arrive soon, and the Imperials may try to regian control of this Fort.\n\n" +
					"No matter. They will all be the first to witness my new power and what my growing new empire is capable of.\n\n" +
					@"Vengeance will soon be upon us, my brothers. Once this is done all shall serve the name of the Dark Lord.""" +
			""; break;
		}
		return retVal;
	}

	public int getMaxMapNum(int campNum){
		int retVal = 3;
		switch (campNum) {
			case 0: retVal = 3; break;
		}
		return retVal;
	}
}
