using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_Campaign : MonoBehaviour {
	public static DB_Campaign I;
	public void Awake(){ I = this; }

	public string name, desc, heroDialog;
	public Sprite sprite;
	public Sprite s_test;

	public void _start(){
		
	}

	public void _setValues(int campaignNum){
		switch (campaignNum) {
			case 1:
				name = "Test Campaign";
				desc = "This is the description of the test campaign";
				heroDialog = "This is the dialog";
				sprite = s_test;
			break;
		}
	}
}
