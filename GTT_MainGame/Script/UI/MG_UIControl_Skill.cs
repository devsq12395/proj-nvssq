using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MG_UIControl_Skill : MonoBehaviour {
	public static MG_UIControl_Skill I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Canvas canvas;
	private RectTransform rectTransform;
	public GameObject btn1, btn2, btn3;

	public bool isShown = false;

	public Text t_sName1, t_sName2, t_sName3, t_sDesc1, t_sDesc2, t_sDesc3, t_mc1, t_mc2, t_mc3, t_cd1, t_cd2, t_cd3;
	public Image i_skill1, i_skill2, i_skill3;

	public void _start(){
		// Define UI
		go.SetActive (true);
		rectTransform 					= canvas.GetComponent<RectTransform>();

		t_sName1 						= GameObject.Find("T_Skill1_Name").GetComponent<Text>();
		t_sName2 						= GameObject.Find("T_Skill2_Name").GetComponent<Text>();
		t_sName3 						= GameObject.Find("T_Skill3_Name").GetComponent<Text>();
		t_sDesc1 						= GameObject.Find("T_Skill1_Desc").GetComponent<Text>();
		t_sDesc2 						= GameObject.Find("T_Skill2_Desc").GetComponent<Text>();
		t_sDesc3 						= GameObject.Find("T_Skill3_Desc").GetComponent<Text>();
		t_mc1 							= GameObject.Find("T_Skill1_MC").GetComponent<Text>();
		t_mc2 							= GameObject.Find("T_Skill2_MC").GetComponent<Text>();
		t_mc3 							= GameObject.Find("T_Skill3_MC").GetComponent<Text>();
		t_cd1 							= GameObject.Find("T_Skill1_CD").GetComponent<Text>();
		t_cd2 							= GameObject.Find("T_Skill2_CD").GetComponent<Text>();
		t_cd3 							= GameObject.Find("T_Skill3_CD").GetComponent<Text>();
		i_skill1 						= GameObject.Find("I_Skill1_Icon").GetComponent<Image>();
		i_skill2 						= GameObject.Find("I_Skill2_Icon").GetComponent<Image>();
		i_skill3 						= GameObject.Find("I_Skill3_Icon").GetComponent<Image>();
		btn1 							= GameObject.Find("BTN_Skill1_Use");
		btn2 							= GameObject.Find("BTN_Skill2_Use");
		btn3 							= GameObject.Find("BTN_Skill3_Use");
		go.SetActive (false);
	}

	#region "Show and Hide"
	public void _show(){
		isShown = true;

		go.SetActive (true);
	}

	public void _hide(){
		isShown = false;

		go.SetActive (false);
	}
	#endregion
	#region "Update UI"
	public void _updateUI(){
		MG_DB_UnitValues.I._setData (MG_Globals.I.selectedUnit.name);
		MG_DB_UnitValues.I._setSkillData (MG_Globals.I.selectedUnit.name);

		btn1.SetActive (true);btn2.SetActive (true);btn3.SetActive (true);

		if (MG_DB_UnitValues.I.skill1 == "NONE") {
			t_sName1.text = "";
			t_sDesc1.text = "";
			t_mc1.text = "";
			t_cd1.text = "";
			i_skill1.sprite = MG_DB_UnitValues.I.icn_none;
			_checkPassiveButton (btn1, "NONE");
		} else {
			t_sName1.text = MG_DB_UnitValues.I.skill1_title;
			t_sDesc1.text = MG_DB_UnitValues.I.skill1_desc;
			i_skill1.sprite = MG_DB_UnitValues.I.icn_skill1;
			t_mc1.text = MG_DB_UnitValues.I.skill1_mc;
			t_cd1.text = MG_DB_UnitValues.I.skill1_cd;
			_checkPassiveButton (btn1, MG_DB_UnitValues.I.skill1);
		}

		if (MG_DB_UnitValues.I.skill2 == "NONE") {
			t_sName2.text = "";
			t_sDesc2.text = "";
			t_mc2.text = "";
			t_cd2.text = "";
			_checkPassiveButton (btn2, "NONE");
			i_skill2.sprite = MG_DB_UnitValues.I.icn_none;
		} else {
			t_sName2.text = MG_DB_UnitValues.I.skill2_title;
			t_sDesc2.text = MG_DB_UnitValues.I.skill2_desc;
			i_skill2.sprite = MG_DB_UnitValues.I.icn_skill2;
			t_mc2.text = MG_DB_UnitValues.I.skill2_mc;
			t_cd2.text = MG_DB_UnitValues.I.skill2_cd;
			_checkPassiveButton (btn2, MG_DB_UnitValues.I.skill2);
		}

		if (MG_DB_UnitValues.I.skill3 == "NONE") {
			t_sName3.text = "";
			t_sDesc3.text = "";
			t_mc3.text = "";
			t_cd3.text = "";
			_checkPassiveButton (btn3, "NONE");
			i_skill3.sprite = MG_DB_UnitValues.I.icn_none;
		} else {
			_checkPassiveButton (btn3, MG_DB_UnitValues.I.skill3);
			t_sName3.text = MG_DB_UnitValues.I.skill3_title;
			t_sDesc3.text = MG_DB_UnitValues.I.skill3_desc;
			i_skill3.sprite = MG_DB_UnitValues.I.icn_skill3;
			t_mc3.text = MG_DB_UnitValues.I.skill3_mc;
			t_cd3.text = MG_DB_UnitValues.I.skill3_cd;
		}
	}

	public void _checkPassiveButton(GameObject btn, string skillName){
		switch (skillName) {
			case "NONE":case "ParasolMusket":case "Fairies2":case "Rage":case "Brinnande":
			case "Mirror2":case "CriticalStrike":case "Warhorn":case "MythrilLance":
			case "HealingAura":case "Point":case "Point2":case "Camp":case "ArmorbreakerCody":
			case "ArchwizardsAura":case "Eskrima":case "SkullBash":case "HonorCode":case "InnerDemons":case "Warlord":case "Witchcraft":case "OfficersAura":
				btn.SetActive (false);
			break;
			default:
				btn.SetActive (true);
			break;
		}
	}
	#endregion
}
