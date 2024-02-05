using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_DB_Units_MiniWW2 : MonoBehaviour {
	public static MG_DB_Units_MiniWW2 I;
	public void Awake(){ I = this; }
	
	
	
	public void start (){
		
	}
	
	public void get_reg_name (string type) {
		 
	}
	
	public void set_traits (string regName){
		 
	}
	
	public string get_name (int nameType) {
		return "a";
	}
	
	public Sprite port_1, port_2, port_3, port_4;
	
	public int get_random_port () {
		// count starts at 0
		float MAX_NUM_OF_PORTRAITS = 3;
		
		return (int)(MG_CALC_Random.I.get_random() * MAX_NUM_OF_PORTRAITS);
	}
	
	public Sprite get_unit_portrait (string regName) {
		Sprite retVal = port_1;
		
		switch (regName) {
			case "test": retVal = port_1; break;
		}
		
		return retVal;
	}
}