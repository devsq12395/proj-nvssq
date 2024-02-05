using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_UnitShop : MonoBehaviour {
	public static MG_UIControl_UnitShop I;
	public void Awake(){ I = this; }
	
	public GameObject go, go_menu, go_shop;
	public Text t_gold, t_recruits;
	
	public List<Image> unit_img;
	public List<Text> unit_txt, unit_txt2;
	public List<GameObject> unit_go;
	public List<string> catalog;
	public List<int> catalog_cost, catalog_cost_R;
	
	public int NUMBER_OF_UNITS;
	public bool isShow;
	
	public void start (){
		NUMBER_OF_UNITS = 6;
		
		go.SetActive (true); go_menu.SetActive (true); go_shop.SetActive (true);
		
		unit_img 		= new List<Image>();
		unit_txt 		= new List<Text>();
		unit_txt2 		= new List<Text>();
		unit_go 		= new List<GameObject>();
		catalog			= new List<string>();
		catalog_cost	= new List<int>();
		catalog_cost_R  = new List<int>();
		
		for (int i = 1; i <= NUMBER_OF_UNITS; i++) {
			unit_go.Add (GameObject.Find ("BTN_US_Cat_" + i.ToString()));
			unit_img.Add (GameObject.Find ("BTN_US_Cat_" + i.ToString() + "_I").GetComponent<Image> ());
			unit_txt.Add (GameObject.Find ("BTN_US_Cat_" + i.ToString() + "_T").GetComponent<Text> ());
			unit_txt2.Add (GameObject.Find ("BTN_US_Cat_" + i.ToString() + "_T2").GetComponent<Text> ());
		}
		
		go.SetActive (false); go_menu.SetActive (false); go_shop.SetActive (false);
	} 
	
	public void show (){
		go_menu.SetActive (true);
		go_shop.SetActive (false);
		
		t_gold.text = MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold.ToString();
		t_recruits.text = MG_Globals.I.players [MG_Globals.I.curPlayerNum].recruits.ToString();
		
		isShow = true;
		go.SetActive (true);
	}
	
	public void show_catalog (int catalogNum) {
		go_menu.SetActive (false);
		go_shop.SetActive (true);
		
		catalog.Clear ();
		catalog_cost.Clear ();
		catalog_cost_R.Clear ();
		
		switch (catalogNum) {
			case 0:
				// Infantry
				MG_DB_UnitShop.I.set_catalog (1);
			break;
		}
		
		catalog.AddRange (MG_DB_UnitShop.I.catalog);
		catalog_cost.AddRange (MG_DB_UnitShop.I.catalog_cost);
		catalog_cost_R.AddRange (MG_DB_UnitShop.I.catalog_cost2);
		
		for (int i = 0; i < NUMBER_OF_UNITS; i++) {
			if (i < catalog.Count) {
				unit_go [i].SetActive (true);
				unit_img [i].sprite = MG_DB_UnitShop.I.get_port (catalog [i]);
				unit_txt [i].text = catalog [i];
				unit_txt2 [i].text = "$" + catalog_cost [i].ToString() + " | Recruits: " + catalog_cost_R [i].ToString();
			}else {
				unit_go [i].SetActive (false);
			} 
		}
	}
	
	public void buy_unit (int unitIndex){
		if(catalog_cost [unitIndex] <= MG_Globals.I.players[MG_Globals.I.curPlayerNum].gold &&
			catalog_cost_R [unitIndex] <= MG_Globals.I.players[MG_Globals.I.curPlayerNum].recruits){
			MG_ControlCommand.I.speStr_1 = catalog [unitIndex];
			MG_ControlCommand.I._issueCommand (-1, "hire unit");
			
			MG_Globals.I.players[MG_Globals.I.curPlayerNum].gold -= catalog_cost [unitIndex];
			MG_Globals.I.players[MG_Globals.I.curPlayerNum].recruits -= catalog_cost_R [unitIndex];
			
			hide (false);
		} else {
			// cardAnnounce ("Not enough resources.");
			MG_ControlSounds.I._playSound(49, 0, 0, false);
		}
	}
	
	public void hide (bool isCancel = true){
		isShow = false;
		go.SetActive (false);

		if (isCancel) {
			MG_ControlTargeters.I._clearTargeters ();
			MG_ControlCursor.I._interact ();
			MG_Globals.I.mode = "in map";
			MG_Globals.I.curCommand = "in map";
			MG_ControlCursor.I._changeCursorSprite ("Normal");
			MG_ControlCommand.I._changeCommandsAndUI ();
		}
	}
	
	
}