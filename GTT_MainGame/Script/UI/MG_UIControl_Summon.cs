using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Summon : MonoBehaviour {
	public static MG_UIControl_Summon I;
	public void Awake(){ I = this; }

	public GameObject go;
	public bool isShown = false;

	public List<Button> btnList;
	public List<string> heroList;
	public List<Text> txtList;

	public int MAX_BUTTONS; // Count starts at 1

	public void _start () {
		// Constants Setup
		MAX_BUTTONS = 18; // Count starts at 1

		// UI Setup
		go.SetActive (true);

		btnList 			= new List<Button> ();
		heroList 			= new List<string> ();
		txtList 			= new List<Text> ();

		for (int i = 1; i <= MAX_BUTTONS; i++) {
			if (i < 10) {
				btnList.Add (GameObject.Find ("BTN_SummonHero_0" + i.ToString ()).GetComponent<Button> ());
				txtList.Add (GameObject.Find ("T_SummonHero_0" + i.ToString ()).GetComponent<Text> ());
			} else {
				btnList.Add (GameObject.Find ("BTN_SummonHero_" + i.ToString ()).GetComponent<Button> ());
				txtList.Add (GameObject.Find ("T_SummonHero_" + i.ToString ()).GetComponent<Text> ());
			}

			heroList.Add ("NONE");
		}

		go.SetActive (false);
	}

	#region "Show and Hide"
	public void _show(){
		isShown = true;

		go.SetActive (true);
		_fillUpHeroList ();
	}

	public void _hide(){
		if(go.activeSelf) MG_ControlSounds.I._playSound(2, 0, 0, false);

		MG_Globals.I.mode = "in map";
		MG_Globals.I.curCommand = "in map";
		MG_ControlCommand.I._changeCommandsAndUI();

		_hide_UIOnly ();
	}

	public void _hide_UIOnly(){
		isShown = false;

		go.SetActive (false);
	}
	#endregion

	#region "Fill Up Hero List"
	public void _fillUpHeroList(){
		MG_ClassPlayer player = MG_Globals.I.players [MG_Globals.I.curPlayerNum];
		string curHero = "";
		int curIndex = 0;

		// Fill up hero list, and set-up buttons with actual heroes
		for (int i = 0; i < player.hero_name.Count; i++) {
			curHero = player.hero_name [i];

			if (curHero != "NONE" && !player.hero_isSummoned [i]) {
				if (player.hero_respawnTime [i] > 0) {
					btnList [curIndex].image.enabled = true;
					heroList [curIndex] = curHero;
					btnList [curIndex].image.sprite = MG_DB_Units.I._getPortrait ("Dead");
					txtList [curIndex].text = "Reviving...";
					btnList [curIndex].interactable = false;
				} else {
					btnList [curIndex].image.enabled = true;
					heroList [curIndex] = curHero;
					btnList [curIndex].image.sprite = MG_DB_Units.I._getPortrait (curHero);
					MG_DB_UnitValues.I._setData (curHero);
					txtList [curIndex].text = MG_DB_UnitValues.I.uiName_short;
					btnList [curIndex].interactable = true;
				}

				curIndex++;
			}
		}

		// The rest of the buttons with no heroes are to be disabled
		for (int i = curIndex; i < MAX_BUTTONS; i++) {
			btnList [i].image.enabled = false;
			txtList [i].text = "";
		}
	}
	#endregion

	#region "Summon Hero Buttons"
	public void _btn_summonHero(int heroNum){
		if (heroList [heroNum] != "NONE") {
			_hide_UIOnly ();

			MG_ControlCommand.I._issueCommand (-1, "summon hero");
			MG_ControlCommand.I.speStr_1 = heroList [heroNum];
		}
	}
	#endregion
}
