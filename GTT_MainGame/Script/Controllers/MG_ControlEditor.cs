using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlEditor : MonoBehaviour {
	public static MG_ControlEditor I;
	public void Awake(){ I = this; }

	public bool isPressed_button1 = false, isPressed_button2 = false, isPressed_button3 = false, isPressed_button4 = false;

	public void markIsPressed(string button, bool isPressed){
		switch(button){
			case "Button1": isPressed_button1 = isPressed; break;
			case "Button2": isPressed_button2 = isPressed; break;
			case "Button3": isPressed_button3 = isPressed; break;
			case "Button4": isPressed_button4 = isPressed; break;
		}
	}
}
