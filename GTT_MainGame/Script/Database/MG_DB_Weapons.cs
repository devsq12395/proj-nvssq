using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_DB_Weapons : MonoBehaviour {
	public static MG_DB_Weapons I;
	public void Awake(){ I = this; }
	
	public Sprite img_dummy, img_test;
	
	public Sprite get_image (string weapName) {
		Sprite retVal = img_dummy;
		switch (weapName) {
			case "test": retVal = img_test; break;
		}
		
		return retVal;
	}
}