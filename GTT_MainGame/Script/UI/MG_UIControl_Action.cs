using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Action : MonoBehaviour {
	public static MG_UIControl_Action I;
	public void Awake(){ I = this; }

	public GameObject go;
	public List<MG_ButtonHold> buttons;
	public List<string> actions;
	public List<float> actCd;

	public int unitID;
	public int secAct = -1;
	public bool isShow, isAction;

	public int MAX_BUTTONS;

	public void start(){
		MAX_BUTTONS = 4;
		secAct = -1;

		go.SetActive (true);
		for (int i = 1; i <= MAX_BUTTONS; i++) {
			MG_ButtonHold btn = GameObject.Find ("BTN_Action" + i.ToString ()).GetComponent<MG_ButtonHold> ();
			btn.hasTooltip = true;
			btn.tooltipTigger = i - 1;
			buttons.Add (btn);
			actions.Add ("NONE");
			actCd.Add (0);
		}

		go.SetActive (false);
		isShow = false;
	}

	public void show(MG_ClassUnit unit) {
		go.SetActive (true);
		isShow = true;

		MG_DB_UnitValues.I._setData (unit.name);
		MG_DB_UnitValues.I._setSkillData (unit.name);

		unitID = unit.unitID;
		setSkillBTN (0, MG_DB_UnitValues.I.skill1);
		setSkillBTN (1, MG_DB_UnitValues.I.skill2);
		setSkillBTN (2, MG_DB_UnitValues.I.skill3);
		setSkillBTN (3, "cancel");

		MG_UIControl_Command.I.cmdBtn [0].image.color = new Color32 (100, 100, 100, 225);
		MG_UIControl_Command.I.cmdBtn [1].image.color = new Color32 (100, 100, 100, 225);
		MG_UIControl_Command.I.cmdBtn [2].image.color = new Color32 (100, 100, 100, 225);
		MG_UIControl_Command.I.cmdBtn [3].image.color = new Color32 (100, 100, 100, 225);
	}

	public void setSkillBTN(int BTNIndex, string skillName) {
		actions [BTNIndex] = skillName;
		if (actions [BTNIndex] != "NONE" || actions [BTNIndex] != "") {
			buttons [BTNIndex].gameObject.GetComponent<Image> ().enabled = true;
			switch (BTNIndex) {
				case 0: buttons [0].image.sprite = MG_DB_UnitValues.I.icn_skill1; break;
				case 1: buttons [1].image.sprite = MG_DB_UnitValues.I.icn_skill2; break;
				case 2: buttons [2].image.sprite = MG_DB_UnitValues.I.icn_skill3; break;
				case 3: buttons [3].image.sprite = MG_DB_UnitValues.I.icn_skill4; break;
			}
			GameObject.Find ("I_ActKey" + (BTNIndex + 1).ToString ()).GetComponent<Image> ().enabled = true;
		} else {
			buttons [BTNIndex].gameObject.GetComponent<Image> ().enabled = false;
			GameObject.Find ("I_ActKey" + (BTNIndex+1).ToString()).GetComponent<Image> ().enabled = false;
		}
	}

	public void update (){
		if (!isShow) return;

		if(!MG_GetUnit.I._checkIfUnitWithIDExists (unitID)) {
			hide ();
		}
	}

	public void hide(bool isActionUsed = false) {
		go.SetActive (false);
		isShow = false;

		MG_UIControl_Command.I._update_CMDBTNSprites ();

		if (!isActionUsed) {
			MG_UIControl_Action.I.secAct = -1;
			isAction = false;
		}

		MG_UIControl_Tooltip.I.hide ();
	}
}
