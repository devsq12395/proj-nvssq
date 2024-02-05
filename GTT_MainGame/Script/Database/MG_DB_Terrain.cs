using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Terrain : MonoBehaviour {
	public static MG_DB_Terrain I;
	public void Awake(){ I = this; }

	public GameObject dummy, grass01, grass02;
	public GameObject cliff01, cliff02, cliff03, cliff04, cliff05, cliff06, cliff07, cliff08, cliff09, cliff10, cliff11, cliff12, cliff13, cliff14;

	public GameObject _getSprite(string spriteNum){
		GameObject returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		Destroy(returnValue);
		switch (spriteNum) {
			case "grass01": returnValue = GameObject.Instantiate (grass01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "grass02": returnValue = GameObject.Instantiate (grass02, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;

			case "cliff01": returnValue = GameObject.Instantiate (cliff01, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff02": returnValue = GameObject.Instantiate (cliff02, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff03": returnValue = GameObject.Instantiate (cliff03, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff04": returnValue = GameObject.Instantiate (cliff04, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff05": returnValue = GameObject.Instantiate (cliff05, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff06": returnValue = GameObject.Instantiate (cliff06, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff07": returnValue = GameObject.Instantiate (cliff07, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff08": returnValue = GameObject.Instantiate (cliff08, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff09": returnValue = GameObject.Instantiate (cliff09, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff10": returnValue = GameObject.Instantiate (cliff10, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff11": returnValue = GameObject.Instantiate (cliff11, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff12": returnValue = GameObject.Instantiate (cliff12, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff13": returnValue = GameObject.Instantiate (cliff13, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "cliff14": returnValue = GameObject.Instantiate (cliff14, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}
		return returnValue;
	}
}
