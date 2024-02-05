using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Barracks : MonoBehaviour {
	public static MG_UIControl_Barracks I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Image i_cursor;
	public Text t_gold;
	public List<Button> btnList;
	public List<Text> btnTxt, btnTxtLim, btnTxtCost;

	public List<string> unitList;

	public int selUnitIndex;

	public int MAX_BUTTONS; // Count starts at 1

	public void start() {
		// Lists
		unitList = new List<string> ();

		// Constants Setup
		MAX_BUTTONS = 6; // Count starts at 1

		go.SetActive (true);

		for (int i = 1; i <= MAX_BUTTONS; i++) {
			btnList.Add (GameObject.Find ("BTN_HireUnit_0" + i.ToString ()).GetComponent<Button> ());
			btnTxt.Add (GameObject.Find ("T_HireUnit_Name_0" + i.ToString ()).GetComponent<Text> ());
			btnTxtLim.Add (GameObject.Find ("T_HireUnit_Limit_0" + i.ToString ()).GetComponent<Text> ());
			btnTxtCost.Add (GameObject.Find ("T_HireUnit_Cost_0" + i.ToString ()).GetComponent<Text> ());

			unitList.Add ("NONE");
		}

		go.SetActive (false);
	}

	public void show() {
		go.SetActive (true);
		t_gold.text = MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold.ToString();
		_fillUpUnitList ();
		MG_Globals.I.pause_windowOpen = true;
	}

	public void hide() {
		if(go.activeSelf) MG_ControlSounds.I._playSound(2, 0, 0, false);

		go.SetActive (false);
		MG_Globals.I.pause_windowOpen = false;

		MG_Globals.I.mode = "in map";
		MG_Globals.I.curCommand = "in map";
		MG_ControlCommand.I._changeCommandsAndUI();
	}

	#region "Fill Up Unit List"
	public void _fillUpUnitList(){
		MG_ClassPlayer player = MG_Globals.I.players [MG_Globals.I.curPlayerNum];
		string curUnit = "";
		int curIndex = 0;

		// Fill up unit list, and set-up buttons with actual heroes
		for (int i = 0; i < player.unit_name.Count && i < MAX_BUTTONS; i++) {
			curUnit = player.unit_name [i];

			if (curUnit != "NONE") {
				btnList [curIndex].image.enabled = true;
				unitList [curIndex] = curUnit;
				btnList [curIndex].image.sprite = MG_DB_Units.I._getPortrait (curUnit);
				MG_DB_UnitValues.I._setData (curUnit);
				btnTxt [curIndex].text = MG_DB_UnitValues.I.uiName_short;
				btnTxtLim [curIndex].text = "Limit: " + player.unit_lim [i].ToString() + "/" + player.unit_limMax [i].ToString();
				btnTxtCost [curIndex].text = "Cost: " + player.unit_cost [i] + " gold";
				btnList [curIndex].interactable = true;

				curIndex++;
			}
		}

		// The rest of the buttons with no heroes are to be disabled
		for (int i = curIndex; i < MAX_BUTTONS; i++) {
			btnList [i].image.enabled = false;
			btnTxt [i].text = "";
			btnTxtLim [i].text = "";
			btnTxtCost [i].text = "";
		}
	}
	#endregion

	#region "Hire Unit Buttons"
	public void _btn_summonUnit(int unitNum){
		if (unitList [unitNum] != "NONE") {
			if(MG_Globals.I.players [MG_Globals.I.curPlayerNum].unit_lim [unitNum] >= MG_Globals.I.players [MG_Globals.I.curPlayerNum].unit_limMax [unitNum]){
				MG_UIControl_Announcer.I._announce ("Hire limit reached.");
				MG_ControlSounds.I._playSound(49, 0, 0, false);
				return;
			}
			if (MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold < MG_Globals.I.players [MG_Globals.I.curPlayerNum].unit_cost [unitNum]) {
				MG_UIControl_Announcer.I._announce ("Not enough gold.");
				MG_ControlSounds.I._playSound(49, 0, 0, false);
				return;
			}

			go.SetActive (false);
			MG_Globals.I.pause_windowOpen = false;

			MG_ControlCommand.I._issueCommand (-1, "hire unit");
			MG_ControlCommand.I.speStr_1 = unitList [unitNum];
			selUnitIndex = unitNum;
		}
	}
	#endregion
}
