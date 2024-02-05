using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Tooltip : MonoBehaviour {
	public static MG_UIControl_Tooltip I;
	public void Awake(){ I = this; }

	public GameObject go, 
		go_i_mp, go_i_cd, go_i_unitPort,
		go_t_mp, go_t_cd;
	public Text t_name, t_mp, t_desc;

	public bool isShow;

	public string uinf, uinf_name;

	public void start() {
		go.SetActive (true);

		go.SetActive (false);
		isShow = false;
	}

	public void update(){
		if (!isShow) return;
		if (MG_Globals.I.selectedUnit == null) {
			hide ();
			return; 
		}

		if (!MG_GetUnit.I._checkIfUnitWithIDExists (MG_Globals.I.selectedUnit.unitID)) {
			hide ();
		}
	}
	
	public void show_unit_info (MG_ClassUnit unit){
		uinf_name = unit.regName;
		
		uinf = "Leader: " + unit.regCom + "\n\n";
		uinf += "Traits:\n";
		
		for (int i = 0; i < unit.traits.Count; i++){
			uinf += unit.traits[i] + "\n";
		}
		
		show (5);
	}
	
	public void set_tooltip_type (int tooltipType) {
		switch (tooltipType) {
			case 1:
				go_i_mp.SetActive (true); go_i_cd.SetActive (true);
				go_t_mp.SetActive (true); go_t_cd.SetActive (true);
				go_i_unitPort.SetActive (false);                                                                     
			break;
			case 2:
				go_i_mp.SetActive (false); go_i_cd.SetActive (false);
				go_t_mp.SetActive (false); go_t_cd.SetActive (false);
				go_i_unitPort.SetActive (true);
			break;
			case 3:
				go_i_mp.SetActive (false); go_i_cd.SetActive (false);
				go_t_mp.SetActive (false); go_t_cd.SetActive (false);
				go_i_unitPort.SetActive (false);
			break;
		}
	}

	public void show(int tooltipNum){
		if (MG_Globals.I.selectedUnit == null) {
			return;
		}
		
		if (MG_GetUnit.I._checkIfUnitWithIDExists (MG_Globals.I.selectedUnit.unitID)) {
			MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (MG_Globals.I.selectedUnit.unitID);
			MG_DB_UnitValues.I._setData (unit.name);
			MG_DB_UnitValues.I._setSkillData (unit.name);

			switch (tooltipNum) {
				case 0:
					if(MG_DB_UnitValues.I.skill1 != "NONE" || MG_DB_UnitValues.I.skill1 != ""){
						go.SetActive (true); isShow = true;
						set_tooltip_type (1);

						t_name.text = MG_DB_UnitValues.I.skill1_title;
						t_mp.text = MG_DB_UnitValues.I.skill1_mc;
						t_desc.text = MG_DB_UnitValues.I.skill1_desc;
					}else{
						go.SetActive (false); isShow = false;
					}
				break;
				case 1:
					if(MG_DB_UnitValues.I.skill2 != "NONE" || MG_DB_UnitValues.I.skill2 != ""){
						go.SetActive (true); isShow = true;
						set_tooltip_type (1);

						t_name.text = MG_DB_UnitValues.I.skill2_title;
						t_mp.text = MG_DB_UnitValues.I.skill2_mc;
						t_desc.text = MG_DB_UnitValues.I.skill2_desc;
					}else{
						go.SetActive (false); isShow = false;
					}
				break;
				case 2:
					if(MG_DB_UnitValues.I.skill3 != "NONE" || MG_DB_UnitValues.I.skill3 != ""){
						go.SetActive (true); isShow = true;
						set_tooltip_type (1);

						t_name.text = MG_DB_UnitValues.I.skill3_title;
						t_mp.text = MG_DB_UnitValues.I.skill3_mc;
						t_desc.text = MG_DB_UnitValues.I.skill3_desc;
					}else{
						go.SetActive (false); isShow = false;
					}
				break;
				case 3:
					go.SetActive (true); isShow = true;
					set_tooltip_type (3);
				
					t_name.text = "Cancel";
					t_mp.text = "0";
					t_desc.text = "";
				break;
				
				case 5:
					// Unit Info
					go.SetActive (true); isShow = true;
					set_tooltip_type (2);
					
					t_name.text = uinf_name;
					t_desc.text = uinf;
				break;
				
				case 10:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Health";
					t_mp.text = "";
					t_desc.text = "This squad's remaining HP.";
				break;
				case 11:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Morale";
					t_mp.text = "";
					t_desc.text = "This squad's current morale.\n\nMorale will affect the performance of this squad.\n\nMorale Tiers:\n10-8: Excellent\n5-7: Normal\n3-5: Shaken\n0-2: Terrified";
				break;
				case 12:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Traits";
					t_mp.text = "";
					t_desc.text = "Trait bonuses of this squad:";
				break;
				case 13:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Accuracy";
					t_mp.text = "";
					t_desc.text = "Gives more damage to your ranged attacks or skills.";
				break;
				case 14:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Defending";
					t_mp.text = "";
					t_desc.text = "Blocks a percentage of enemy damage.";
				break;
				case 15:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Endurance";
					t_mp.text = "";
					t_desc.text = "Affects the total hit points of this squad.";
				break;
				case 16:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Speed";
					t_mp.text = "";
					t_desc.text = "Affects the movement speed of this squad.";
				break;
				case 17:
					go.SetActive (true); isShow = true;
				
					t_name.text = "Zeal";
					t_mp.text = "";
					t_desc.text = "Affects the morale of this squad.";
				break;
			}
		}
	}

	public void hide(){
		go.SetActive (false);
		isShow = false;
	}
}
