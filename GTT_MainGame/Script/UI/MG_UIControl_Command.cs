using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MG_UIControl_Command : MonoBehaviour {
	public static MG_UIControl_Command I;
	public void Awake(){ I = this; }

	// UI elements
	public GameObject go_commandWindow, go_unitData, go_tipPopup01;
	public Canvas c_commandWindow, c_unitData;
	public RectTransform rt_commandWindow, rt_unitData;
	public MG_ImageHold img_cmdBtnBG1, img_cmdBtnBG3;
	public List<MG_ButtonHold> cmdBtn, skillBtn;
	public List<Text> TXT_cmdBtn_Name;
	public bool shown_tipPopup01 = false;

	private float Y_START, Y_STOP;

	public void _start(){
		// Define UI elements
		for (int i = 1; i <= 4; i++) {		// Command buttons
			cmdBtn.Add (GameObject.Find ("BTN_Comm" + i.ToString ()).GetComponent<MG_ButtonHold> ());
			TXT_cmdBtn_Name.Add (GameObject.Find ("BTN_Comm" + i.ToString () + "_T").GetComponent<Text> ());
		}

		go_commandWindow 				= GameObject.Find ("C_CommandWindow");
		go_unitData 					= GameObject.Find ("C_UnitData");
		c_commandWindow 				= GameObject.Find ("C_CommandWindow").GetComponent<Canvas> ();
		c_unitData 						= GameObject.Find ("C_UnitData").GetComponent<Canvas> ();
		rt_commandWindow 				= GameObject.Find ("C_CommandWindow").GetComponent<RectTransform> ();
		rt_unitData 					= GameObject.Find ("C_UnitData").GetComponent<RectTransform> ();
		img_cmdBtnBG1 					= GameObject.Find ("I_BG_ComWind").GetComponent<MG_ImageHold> ();
		//i_unitData1 					= GameObject.Find ("I_BG_UnitData").GetComponent<MG_ImageHold> ();
		img_cmdBtnBG3 					= GameObject.Find ("I_BG_UnitData2").GetComponent<MG_ImageHold> ();

		go_tipPopup01.SetActive (false);

		// Define variables
		Y_START = 0;
		Y_STOP = -300;
	}

	#region "Tween"
	public void _tweenIn(){
		float firstPos = rt_commandWindow.localPosition.y;
		float targetPos = Y_START;
		float duration = 1f;

		gameObject.Tween("uiMove_comWindow", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_commandWindow.localPosition = new Vector3(rt_commandWindow.localPosition.x, t.CurrentValue, rt_commandWindow.localPosition.z);
			rt_unitData.localPosition = new Vector3(rt_unitData.localPosition.x, t.CurrentValue, rt_unitData.localPosition.z);
		}, (t) =>{
			// completion
			rt_commandWindow.localPosition = new Vector3(rt_commandWindow.localPosition.x, t.CurrentValue, rt_commandWindow.localPosition.z);
			rt_unitData.localPosition = new Vector3(rt_unitData.localPosition.x, t.CurrentValue, rt_unitData.localPosition.z);
		});
	}

	public void _tweenOut(){
		float firstPos = rt_commandWindow.localPosition.y;
		float targetPos = Y_STOP;
		float duration = 1.5f;

		gameObject.Tween("uiMove_comWindow", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_commandWindow.localPosition = new Vector3(rt_commandWindow.localPosition.x, t.CurrentValue, rt_commandWindow.localPosition.z);
			rt_unitData.localPosition = new Vector3(rt_unitData.localPosition.x, t.CurrentValue, rt_unitData.localPosition.z);
		}, (t) =>{
			// completion
			rt_commandWindow.localPosition = new Vector3(rt_commandWindow.localPosition.x, t.CurrentValue, rt_commandWindow.localPosition.z);
			rt_unitData.localPosition = new Vector3(rt_unitData.localPosition.x, t.CurrentValue, rt_unitData.localPosition.z);
		});
	}
	#endregion

	#region "Command Button Functions"
	public void _pressFctn_1(){MG_ControlCommand.I._issueCommand(1);}
	public void _pressFctn_2(){MG_ControlCommand.I._issueCommand(2);}
	public void _pressFctn_3(){MG_ControlCommand.I._issueCommand(3);}
	public void _pressFctn_4(){
		MG_ControlCommand.I._issueCommand(4);
		
		MG_UIControl_Barracks.I.hide();
		MG_UIControl_SpellTower.I.hide();
		MG_UIControl_Summon.I._hide();
		MG_UIControl_Skill.I._hide();
		
		MG_UIControl_Action.I.hide ();
	}

	public void showTipPopup1() {
		if (!shown_tipPopup01) {
			shown_tipPopup01 = true;
			go_tipPopup01.SetActive (true);
		}
	}
	#endregion

	//Includes
	//	- _update_CMDBTNSprites()					- change command button sprites based from MG_ControlCommand's btn_order strings
	#region "Tools"
	public void _update_CMDBTNSprites(){
		if (!go_commandWindow.activeSelf || !go_unitData.activeSelf)
			return;

		// Command buttons 1-4
		cmdBtn [0].image.sprite = MG_DB_CMDButtons.I._getSprite (MG_ControlCommand.I.btn1_order);
		cmdBtn [1].image.sprite = MG_DB_CMDButtons.I._getSprite (MG_ControlCommand.I.btn2_order);
		cmdBtn [2].image.sprite = MG_DB_CMDButtons.I._getSprite (MG_ControlCommand.I.btn3_order);
		cmdBtn [3].image.sprite = MG_DB_CMDButtons.I._getSprite (MG_ControlCommand.I.btn4_order);

		TXT_cmdBtn_Name [0].text = (MG_ControlCommand.I.btn1_order == "none" || MG_ControlCommand.I.btn1_order.Contains("(hidden)")) ? " " : MG_Tools.I.FirstLetterToUpper(MG_ControlCommand.I.btn1_order);
		TXT_cmdBtn_Name [1].text = (MG_ControlCommand.I.btn2_order == "none" || MG_ControlCommand.I.btn2_order.Contains("(hidden)")) ? " " : MG_Tools.I.FirstLetterToUpper(MG_ControlCommand.I.btn2_order);
		TXT_cmdBtn_Name [2].text = (MG_ControlCommand.I.btn3_order == "none" || MG_ControlCommand.I.btn3_order.Contains("(hidden)")) ? " " : MG_Tools.I.FirstLetterToUpper(MG_ControlCommand.I.btn3_order);
		TXT_cmdBtn_Name [3].text = "End Turn";

		if (MG_ControlCommand.I.btn1_order == "none" || MG_ControlCommand.I.btn1_order.Contains("(hidden)")) 		GameObject.Find ("I_Key1").GetComponent<Image> ().enabled = false;
		else 												GameObject.Find ("I_Key1").GetComponent<Image> ().enabled = true;
		if (MG_ControlCommand.I.btn2_order == "none" || MG_ControlCommand.I.btn2_order.Contains("(hidden)")) 		GameObject.Find ("I_Key2").GetComponent<Image> ().enabled = false;
		else 												GameObject.Find ("I_Key2").GetComponent<Image> ().enabled = true;
		if (MG_ControlCommand.I.btn3_order == "none" || MG_ControlCommand.I.btn3_order.Contains("(hidden)")) 		GameObject.Find ("I_Key3").GetComponent<Image> ().enabled = false;
		else 												GameObject.Find ("I_Key3").GetComponent<Image> ().enabled = true;
		GameObject.Find ("I_Key4").GetComponent<Image> ().enabled = true;

		if (MG_Globals.I.mode == "in map" && MG_Globals.I.selectedUnit_exist) {
			if (MG_Globals.I.selectedUnit.action_move) 		cmdBtn [0].image.color = new Color32 (255, 255, 225, 225);
			else 											cmdBtn [0].image.color = new Color32 (100, 100, 100, 225);
			if (MG_Globals.I.selectedUnit.action_atk) 		cmdBtn [1].image.color = new Color32 (255, 255, 225, 225);
			else 											cmdBtn [1].image.color = new Color32 (100, 100, 100, 225);
			if (MG_Globals.I.selectedUnit.action_skill) 	cmdBtn [2].image.color = new Color32 (255, 255, 225, 225);
			else 											cmdBtn [2].image.color = new Color32 (100, 100, 100, 225);
			cmdBtn [3].image.color = new Color32 (255, 255, 225, 225);
		} else {
			for(int i = 0; i <= 2; i++) 	cmdBtn [i].image.color = new Color32 (255, 255, 225, 225);
		}
	}
	#endregion
}
