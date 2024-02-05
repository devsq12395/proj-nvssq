using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MG_UIControl_UnitStats : MonoBehaviour {
	public static MG_UIControl_UnitStats I;
	public void Awake(){ I = this; }

	public Canvas c;
	private RectTransform rectTransform;
	public Text t_dam, t_abiPow, t_arm, t_res, t_ms;

	public bool isShown = false;
	private float X_START, X_STOP;

	public void _start(){
		c 								= GameObject.Find ("C_UnitStats").GetComponent<Canvas> ();
		rectTransform 					= GameObject.Find ("C_UnitStats").GetComponent<RectTransform>();

		t_dam 							= GameObject.Find ("TM_US_Damage").GetComponent<Text> ();
		t_abiPow 						= GameObject.Find ("TM_US_AbilityPower").GetComponent<Text> ();
		t_arm 							= GameObject.Find ("TM_US_Armor").GetComponent<Text> ();
		t_res 							= GameObject.Find ("TM_US_MagicResistance").GetComponent<Text> ();
		t_ms 							= GameObject.Find ("TM_US_Movespeed").GetComponent<Text> ();

		// Define variables
		X_START = -600;
		X_STOP = 0;
	}

	#region "Update Stat Window"
	public void updateStatWindow(MG_ClassUnit unit){
		if (unit.isAHero && MG_Globals.I.players [unit.playerOwner]._getHeroSlot(unit.name) != -1) {
			int bonusDamage = MG_ControlBuffs.I.CALC_bonusDamage (unit) + MG_Globals.I.players [unit.playerOwner]._getItemBonusStat_inputName_damage (unit.name);
			t_dam.text = unit.atkDamage.ToString();
			if (bonusDamage > 0) 	t_dam.text += "<color=#FBFF00FF>+" + bonusDamage.ToString ()  + "</color>";
			if (bonusDamage < 0) 	t_dam.text += "<color=#FF0000FF>" + bonusDamage.ToString ()  + "</color>";

			double bonusAbiPower = MG_ControlBuffs.I.CALC_bonusAbilityPower (unit) + MG_Globals.I.players [unit.playerOwner]._getItemBonusStat_inputName_abiPow (unit.name);
			t_abiPow.text = ((int)(unit.abilityPower * 100)).ToString() + "%";
			if (bonusAbiPower > 0) 		t_abiPow.text += "<color=#FBFF00FF>+" + (Mathf.Floor((float)bonusAbiPower*100)).ToString () + "%</color>";
			if (bonusAbiPower < 0) 		t_abiPow.text += "<color=#FF0000FF>" + (Mathf.Floor((float)bonusAbiPower*100)).ToString () + "%</color>";

			double bonusArm = MG_ControlBuffs.I.CALC_bonusArmor (unit) + MG_Globals.I.players [unit.playerOwner]._getItemBonusStat_inputName_armor (unit.name);
			t_arm.text = ((int)(unit.armor_DEF * 100)).ToString() + "%";
			if (bonusArm > 0) 		t_arm.text += "<color=#FBFF00FF>+" + (Mathf.Floor((float)bonusArm*100)).ToString () + "%</color>";
			if (bonusArm < 0) 		t_arm.text += "<color=#FF0000FF>" + (Mathf.Floor((float)bonusArm*100)).ToString () + "%</color>";

			double bonusRes = MG_ControlBuffs.I.CALC_bonusResistance (unit) + MG_Globals.I.players [unit.playerOwner]._getItemBonusStat_inputName_resistance (unit.name);
			t_res.text = ((int)(unit.resistance_DEF * 100)).ToString() + "%";
			if (bonusRes > 0) 		t_res.text += "<color=#FBFF00FF>+" + (Mathf.Floor((float)bonusRes*100)).ToString () + "%</color>";
			if (bonusRes < 0) 		t_res.text += "<color=#FF0000FF>" + (Mathf.Floor((float)bonusRes*100)).ToString () + "%</color>";

			int bonusMS = MG_ControlBuffs.I.CALC_bonusMS (unit) + MG_Globals.I.players [unit.playerOwner]._getItemBonusStat_inputName_MS (unit.name);
			t_ms.text = unit.MS_DEF.ToString();
			if (bonusMS > 0) 	t_ms.text += "<color=#FBFF00FF>+" + bonusMS.ToString ()  + "</color>";
			if (bonusMS < 0) 	t_ms.text += "<color=#FF0000FF>" + bonusMS.ToString ()  + "</color>";
		}else{
			int bonusDamage = MG_ControlBuffs.I.CALC_bonusDamage (unit);
			t_dam.text = unit.atkDamage.ToString();
			if (bonusDamage > 0) 	t_dam.text += "<color=#FBFF00FF>+" + bonusDamage.ToString ()  + "</color>";
			if (bonusDamage < 0) 	t_dam.text += "<color=#FF0000FF>" + bonusDamage.ToString ()  + "</color>";

			double bonusAbiPower = MG_ControlBuffs.I.CALC_bonusAbilityPower (unit);
			t_abiPow.text = ((int)(unit.abilityPower * 100)).ToString() + "%";
			if (bonusAbiPower > 0) 		t_abiPow.text += "<color=#FBFF00FF>+" + (Mathf.Floor((float)bonusAbiPower*100)).ToString () + "%</color>";
			if (bonusAbiPower < 0) 		t_abiPow.text += "<color=#FF0000FF>" + (Mathf.Floor((float)bonusAbiPower*100)).ToString () + "%</color>";

			double bonusArm = MG_ControlBuffs.I.CALC_bonusArmor (unit);
			t_arm.text = ((int)(unit.armor_DEF * 100)).ToString() + "%";
			if (bonusArm > 0) 		t_arm.text += "<color=#FBFF00FF>+" + (Mathf.Floor((float)bonusArm*100)).ToString () + "%</color>";
			if (bonusArm < 0) 		t_arm.text += "<color=#FF0000FF>" + (Mathf.Floor((float)bonusArm*100)).ToString () + "%</color>";

			double bonusRes = MG_ControlBuffs.I.CALC_bonusResistance (unit);
			t_res.text = ((int)(unit.resistance_DEF * 100)).ToString() + "%";
			if (bonusRes > 0) 		t_res.text += "<color=#FBFF00FF>+" + (Mathf.Floor((float)bonusRes*100)).ToString () + "%</color>";
			if (bonusRes < 0) 		t_res.text += "<color=#FF0000FF>" + (Mathf.Floor((float)bonusRes*100)).ToString () + "%</color>";

			int bonusMS = MG_ControlBuffs.I.CALC_bonusMS (unit);
			t_ms.text = unit.MS_DEF.ToString();
			if (bonusMS > 0) 	t_ms.text += "<color=#FBFF00FF>+" + bonusMS.ToString ()  + "</color>";
			if (bonusMS < 0) 	t_ms.text += "<color=#FF0000FF>" + bonusMS.ToString ()  + "</color>";
		}
	}
	#endregion

	#region "Show and Hide"
	public void _show(){
		if (isShown) {
			_hide ();
			return;
		}

		if (rectTransform == null) {
			rectTransform 					= GameObject.Find ("C_UnitStats").GetComponent<RectTransform>();
		}

		MG_ControlSounds.I._playSound(2, 0, 0, false);
		Vector2 firstPos = rectTransform.localPosition;
		Vector2 targetPos = new Vector2 (X_STOP, firstPos.y);
		float duration = 0.2f;

		gameObject.Tween("uiMove_unitData", firstPos, targetPos, duration, TweenScaleFunctions.Linear, (t) =>{
			// progress
			rectTransform.localPosition = t.CurrentValue;
		}, (t) =>{
			// completion
			rectTransform.localPosition = t.CurrentValue;
			isShown = true;
		});
	}

	public void _hide(){
		Vector2 firstPos = rectTransform.localPosition;
		Vector2 targetPos = new Vector2 (X_START, firstPos.y);
		float duration = 0.2f;

		gameObject.Tween("uiMove_unitData", firstPos, targetPos, duration, TweenScaleFunctions.Linear, (t) =>{
			// progress
			rectTransform.localPosition = t.CurrentValue;
		}, (t) =>{
			// completion
			rectTransform.localPosition = t.CurrentValue;
			isShown = false;
		});
	}
	#endregion
}
