using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_SpellTower : MonoBehaviour {
	public static MG_UIControl_SpellTower I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Image i_cursor;
	public Text t_gold;
	public List<Button> btnList;
	public List<Text> btnTxt;

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
			btnList.Add (GameObject.Find ("BTN_SpellTower_0" + i.ToString ()).GetComponent<Button> ());
			btnTxt.Add (GameObject.Find ("T_SpellTower_Name_0" + i.ToString ()).GetComponent<Text> ());

			unitList.Add ("NONE");
		}

		go.SetActive (false);
	}

	public void show() {
		go.SetActive (true);
		_fillUpList ();
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

	#region "Fill Up List"
	public void _fillUpList(){
		MG_ClassPlayer player = MG_Globals.I.players [MG_Globals.I.curPlayerNum];
		string curUnit = "";
		int curIndex = 0;

		// Fill up spell list with "NONE"
		for (int i = 0; i < unitList.Count && i < MAX_BUTTONS; i++) {
			unitList[i] = "NONE";
		}

		// For each of player's spells, it is added into the spell list
		for (int i = 0; i < MAX_BUTTONS; i++) {
			if (i >= player.spell_name.Count) {
				btnList [curIndex].image.enabled = false;
				btnList [curIndex].interactable = false;
				btnTxt [curIndex].text = "";
				continue;
			}

			curUnit = player.spell_name [i];

			if (curUnit != "NONE" && curUnit != "") {
				btnList [curIndex].image.enabled = true;
				unitList [curIndex] = curUnit;
				btnList [curIndex].image.sprite = MG_DB_Spells.I.getPortrait (curUnit);
				btnTxt [curIndex].text = MG_DB_Spells.I.getInGameName (curUnit);
				btnList [curIndex].interactable = true;

				curIndex++;
			} else {
				btnList [curIndex].image.enabled = false;
				btnList [curIndex].interactable = false;
				btnTxt [curIndex].text = "";
			}
		}

		// Rewrite player's spells
		for (int i = 0; i < unitList.Count; i++) {
			player.spell_name[i] = unitList[i];
		}

		// The rest of the buttons with no heroes are to be disabled
		for (int i = curIndex; i < MAX_BUTTONS; i++) {
			btnList [i].image.enabled = false;
			btnTxt [i].text = "";
		}
	}
	#endregion

	#region "Cast Spell Buttons"
	public void _btn_castSpell(int unitNum){
		if (unitList [unitNum] != "NONE") {
			go.SetActive (false);
			MG_Globals.I.pause_windowOpen = false;

			MG_ControlCommand.I._issueCommand (-1, "cast spell");
			MG_ControlCommand.I.speStr_1 = unitList [unitNum];
			selUnitIndex = unitNum;
		}
	}
	#endregion
}
