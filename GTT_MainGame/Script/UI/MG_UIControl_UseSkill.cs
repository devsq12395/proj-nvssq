using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_UseSkill : MonoBehaviour {
	public static MG_UIControl_UseSkill I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Image i;
	public Text t;

	public bool isShown = false;
	public float dur = 0, durMax = 0;

	public void _start(){
		durMax = 2;

		go.SetActive (false);
	}

	public void _update(float deltaTime){
		if (isShown) {
			dur -= deltaTime;
			if (dur <= 0) {
				isShown = false;
				go.SetActive (false);
			}
		}
	}

	public void _show(MG_ClassUnit unit, string skillName){
		if (MG_ControlFogOfWar.I._pointIsRevealed (unit.posX, unit.posY)) {
			go.SetActive (true);
			isShown = true;
			dur = durMax;

			i.sprite = MG_DB_Units.I._getPortrait (unit.name);
			t.text = skillName;
		}
	}
}
