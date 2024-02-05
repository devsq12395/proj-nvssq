using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MG_UIControl_Streak : MonoBehaviour {
	public static MG_UIControl_Streak I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Image i_streak;
	public Sprite spr_fb, spr_dk, spr_tk, spr_qk, spr_mk, spr_uk, spr_rp, spr_ace;

	public bool isShow = false;
	public float dur, durStart;

	public void _start(){
		go.SetActive (false);
		durStart = 2.5f;
	}

	public void _update(float deltaTime){
		if (isShow) {
			dur -= deltaTime;
			if (dur <= 0) {
				hide ();
			}
		}
	}

	public void show(int spriteNum){
		MG_ControlSounds.I._playSound (10, 0, 0, false);

		go.SetActive (true);
		isShow = true;
		dur = durStart;

		i_streak.color = new Color (1, 1, 1, 0);
		Color targetColor = new Color (1, 1, 1, 1);
		float duration = 0.5f;

		switch (spriteNum) {
			case 0:i_streak.sprite = spr_fb; break;
			case 1:i_streak.sprite = spr_dk; break;
			case 2:i_streak.sprite = spr_tk; break;
			case 3:i_streak.sprite = spr_qk; break;
			case 4:
				i_streak.sprite = spr_mk; 
				// Steam Achievement - Tactical Beatdown
				if(MM_Steam.I != null) MM_Steam.I.unlockAchievement ("KILLSTREAK_4_1_3");
			break;
			case 5:i_streak.sprite = spr_uk; break;
			case 6:i_streak.sprite = spr_rp; break;
			case 7:
				i_streak.sprite = spr_ace; 
				// Steam Achievement - Royal Flush
				if(MM_Steam.I != null) MM_Steam.I.unlockAchievement ("KILLSTREAK_8_1_4");
			break;
		}

		gameObject.Tween("uiMove_streak", i_streak.color, targetColor, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			i_streak.color = t.CurrentValue;
		}, (t) =>{
			// completion
			i_streak.color = t.CurrentValue;
		});
	}

	public void hide(){
		Color targetColor = new Color (1, 1, 1, 0);
		float duration = 0.5f;

		gameObject.Tween("uiMove_streak", i_streak.color, targetColor, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			i_streak.color = t.CurrentValue;
		}, (t) =>{
			// completion
			i_streak.color = t.CurrentValue;
			go.SetActive (false);
			isShow = false;
		});
	}
}
