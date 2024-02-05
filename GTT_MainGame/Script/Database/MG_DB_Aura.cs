using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Aura : MonoBehaviour {
	public static MG_DB_Aura I;
	public void Awake() { I = this; }

	public GameObject dummy, aura02blue, aura02red, aura03blue, aura03red;

	// Returns false if aura targets ALLIES
	// Returns true if aura targets ENEMIES
	public bool _getCond_TargetIsEnemy(string auraName){
		bool output = false;
		switch(auraName){
			case "WarlordAura":
				output = true;
			break;
		}
		return output;
	}

	public GameObject _getSprite(string newAuraName, int player){
		GameObject output = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		Destroy (output);

		if (player == 1) {
			switch (newAuraName) {
				case "PointAura":case "PointAura Temp": output = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
				default: output = GameObject.Instantiate(aura03blue, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			}
		} else {
			switch (newAuraName) {
				case "PointAura":case "PointAura Temp": output = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
				default: output = GameObject.Instantiate(aura03red, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			}
		}

		return output;
	}
}
