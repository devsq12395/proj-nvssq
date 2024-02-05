using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Announcer : MonoBehaviour {
	public static MG_UIControl_Announcer I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Text t;
	public float dur = 0;
	public bool isShown = false, isPermanent;

	public void _start(){
		go.SetActive (true);

		t = GameObject.Find ("TM_Announcer").GetComponent<Text> ();

		go.SetActive (false);
	}

	public void _update(float deltaTime){
		if (!isShown)		return;

		if (!isPermanent) {
			dur -= deltaTime;
			if (dur <= 0) {
				isShown = false;
				go.SetActive (false);
			}
		}
	}

	public void _announce(string text, float duration = 2.5f, bool permanent = false){
		go.SetActive (true);
		t.text = text;

		isPermanent = permanent;
		isShown = true;
		dur = duration;
	}

	public void _clearAnnounce(){
		isShown = false;
		go.SetActive (false);
		isPermanent = false;
	}
}
