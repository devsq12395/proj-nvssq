using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MG_UIControl_TurnChange : MonoBehaviour {
	public static MG_UIControl_TurnChange I;
	public void Awake(){ I = this; }

	public Image i;
	public Text t;
	public Sprite i_red, i_blue;
	private RectTransform rectTransform;

	public bool isShown = false;

	public void _start(){
		i 				= GameObject.Find ("I_BG_TurnChange").GetComponent<Image> ();
		rectTransform 	= GameObject.Find("I_BG_TurnChange").GetComponent<RectTransform>();
		t 				= GameObject.Find ("T_TurnChange").GetComponent<Text> ();
	}

	public void _show(string newText){
		t.text = newText;

		float firstAngle = 180;
		float targetAngle = 0;
		float duration = 0.75f;
		MG_Globals.I.pause_uiMove = true;
		isShown = true;

		if (newText == "Your Turn") {
			i.sprite = i_blue;
			t.color = new Color(0f, 0f, 0f);
		} else {
			i.sprite = i_red;
			t.color = new Color(1f, 1f, 1f);
		}

		gameObject.Tween("uiMove_turnChange", firstAngle, targetAngle, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rectTransform.eulerAngles = (new Vector3(0, 0, -t.CurrentValue));
		}, (t) =>{
			// completion
			rectTransform.eulerAngles  = (new Vector3(0, 0, -t.CurrentValue));
			MG_ControlEvents.I._addEvent ("Wait", new string[]{"0.5", "5"});		// Custom event timer, when done, call hide();
		});
	}

	public void _hide(){
		float firstAngle = 0;
		float targetAngle = 180;
		float duration = 0.75f;
		MG_Globals.I.pause_uiMove = true;
		isShown = true;

		gameObject.Tween("uiMove_turnChange", firstAngle, targetAngle, duration, TweenScaleFunctions.CubicEaseIn, (t) =>{
			// progress
			rectTransform.eulerAngles  = (new Vector3(0, 0, t.CurrentValue));
		}, (t) =>{
			// completion
			rectTransform.eulerAngles = (new Vector3(0, 0, t.CurrentValue));
			MG_Globals.I.pause_uiMove = false;
		});
	}
}
