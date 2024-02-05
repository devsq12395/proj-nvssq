using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_ChangeWeapon : MonoBehaviour {
	public static MG_UIControl_ChangeWeapon I;
	public void Awake(){ I = this; }
	
	/*Upublic GameObject go;
	
	public List<Text> wpList_name, wpList_ammo;
	public List<Image> wpList_img;
	public List<GameObject> wpList_go;
	
	public int NUMBER_OF_WEAPONS, selUnitID;
	public bool isShow;
	
	public void start (){
		NUMBER_OF_WEAPONS = 6;
		go.SetActive (true);
		
		wpList_name 		= new List<Text>();
		wpList_ammo 		= new List<Text>();
		wpList_img 			= new List<Image>();
		wpList_go 			= new List<GameObject>();
		
		for (int i = 1; i <= NUMBER_OF_WEAPONS; i++) {
			wpList_go.Add (GameObject.Find ("BTN_CW_" + i.ToString())); 
			wpList_name.Add (GameObject.Find ("BTN_CW_" + i.ToString() + "_T").GetComponent<Text> ());
			wpList_ammo.Add (GameObject.Find ("BTN_CW_" + i.ToString() + "_T2").GetComponent<Text> ());
			wpList_img.Add (GameObject.Find ("BTN_CW_" + i.ToString() + "_I").GetComponent<Image> ());
		}
		
		go.SetActive (false);
	}
	
	public void show (MG_ClassUnit unit){
		int weaponCnt = 0;
		for (int i = 0; i < wpList_go.Count; i++) {
			if (i < unit.weapon.Count){
				wpList_go [i].SetActive (true);
				
				wpList_name [i].text = unit.weapon [i];
				wpList_ammo [i].text = 
					"Ammo: " + unit.weapon_ammo [i].ToString () + 
					"| DMG: " + unit.weapon_damage [i].ToString () + 
					"| Type: " + unit.weapon_class [i];
				wpList_img [i].sprite = MG_DB_Weapons.I.get_image (unit.weapon [i]);
			}else {
				wpList_go [i].SetActive (false);
			}
		}
		
		selUnitID = unit.unitID;
		
		isShow = true;
		go.SetActive (true);
	}
	
	public void hide (){
		go.SetActive (false);
		
		isShow = false;
		MG_Globals.I.mode = "in map";
		MG_Globals.I.curCommand = "in map";
		MG_ControlCursor.I._interact ();
	}
	
	public void set_weapon_1 () {set_weapon (0);}
	public void set_weapon_2 () {set_weapon (1);}
	public void set_weapon_3 () {set_weapon (2);}
	public void set_weapon_4 () {set_weapon (3);}
	public void set_weapon_5 () {set_weapon (4);}
	public void set_weapon_6 () {set_weapon (5);}
	
	public void set_weapon(int index) {
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (selUnitID);
		
		MG_ControlUnits.I.change_weapon (unit, index);
		
		MG_ControlCursor.I._interact();
		hide ();
		
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			PhotonRoom.room.gameAction_changeWeapon(unit.unitID, index);
		}
	}
	
	public void photon_set_weapon (MG_ClassUnit unit, int newIndex) {
		MG_ControlUnits.I.change_weapon (unit, newIndex);
	}U*/
}