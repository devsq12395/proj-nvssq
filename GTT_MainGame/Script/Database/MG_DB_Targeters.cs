using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Targeters : MonoBehaviour {
	public static MG_DB_Targeters I;
	public void Awake(){ I = this; }

	public GameObject dummy, friendly, hostile;

	public GameObject _getSprite(int targeterType){
		GameObject returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		Destroy(returnValue);
		switch (targeterType) {
			case 1: returnValue = GameObject.Instantiate(friendly, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case 2: returnValue = GameObject.Instantiate(hostile, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
		}

		return returnValue;
	}
}
