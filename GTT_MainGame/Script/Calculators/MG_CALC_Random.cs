using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_CALC_Random : MonoBehaviour {
	public static MG_CALC_Random I;
	public void Awake(){ I = this; }
	
	public List<int> randomKeys;
	
	public void start (){
		randomKeys = new List<int>();
	}

	public int get_random(){
		int rand = UnityEngine.Random.Range (0, 100);
		
		
		return rand;
	}
}
