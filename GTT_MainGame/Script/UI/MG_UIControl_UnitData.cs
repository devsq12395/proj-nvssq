using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_UnitData : MonoBehaviour {
	public static MG_UIControl_UnitData I;
	public void Awake(){ I = this; }

	// UI Elements
	public Image i_unitData2, i_iconHP, i_iconMP, i_itemData, i_stat1, i_stat2, i_stat3, i_stat4, i_stat5;
	public Text t_name, t_hp, t_mp, t_item1, t_item2, t_item3, t_itemTitle, t_leader, t_traits;
	public List<Image> i_buffImg;

	public Sprite s_unitData1, s_unitData1_gray, s_unitData2, s_unitData2_gray, s_itemData, s_itemData_gray;
	public Button btn_stats, btn_item1, btn_item2, btn_item3;
	public int uiData_selectedPosX, uiData_selectedPosY;

	public void _start(){
		// Define UI elements
		//i_unitData1 = GameObject.Find("I_BG_UnitData").GetComponent<Image>();
		i_unitData2 = GameObject.Find("I_BG_UnitData2").GetComponent<Image>();
		i_itemData = GameObject.Find("I_BG_UnitData_Items").GetComponent<Image>();
		i_iconHP = GameObject.Find("I_UD_IconHP").GetComponent<Image>();
		i_iconMP = GameObject.Find("I_UD_IconMP").GetComponent<Image>();
		t_name = GameObject.Find("T_UData_UnitName").GetComponent<Text>();
		t_hp = GameObject.Find("T_UData_UnitHP").GetComponent<Text>();
		t_mp = GameObject.Find("T_UData_UnitMP").GetComponent<Text>();
		btn_stats = GameObject.Find("BTN_Stats").GetComponent<Button>();
		btn_item1 = GameObject.Find("BTN_HeroItem1").GetComponent<Button>();
		btn_item2 = GameObject.Find("BTN_HeroItem2").GetComponent<Button>();
		btn_item3 = GameObject.Find("BTN_HeroItem3").GetComponent<Button>();
		t_item1 = GameObject.Find("BTN_HeroItem1_T").GetComponent<Text>();
		t_item2 = GameObject.Find("BTN_HeroItem2_T").GetComponent<Text>();
		t_item3 = GameObject.Find("BTN_HeroItem3_T").GetComponent<Text>();
		t_itemTitle = GameObject.Find("T_UData_HeroItems").GetComponent<Text>();

		for (int i = 0; i < 10; i++) {
			i_buffImg.Add (GameObject.Find (((i < 9) ? "I_UData_Buff0" : "I_UData_Buff") + (i+1).ToString ()).GetComponent<Image> ());
		}
	}

	/// <summary>
	/// Called from:
	/// 1.	MG_ControlCursor.I._interact();
	/// 2.	MG_CALC_Damage.I._damageUnit();
	/// 3.	When buying hero items: 
	/// 		- MG_UIControl_Shop.I._buttonInteractBuy();
	/// 		- 
	/// </summary>
	#region "setUIData"
	public void _setUIData(int curPosX, int curPosY){
		bool hasUnit = MG_GetUnit.I._pointHasUnit(curPosX, curPosY);
		uiData_selectedPosX = curPosX;
		uiData_selectedPosY = curPosY;

		if (hasUnit && MG_ControlFogOfWar.I._pointIsRevealed(curPosX, curPosY)) {
			//MG_UIControl_Command.I.c_unitData.enabled = true;
			MG_ClassUnit targUnit = MG_GetUnit.I._pickUnit(curPosX, curPosY);

			if (targUnit.isDummy) {
				_clearUIData ();
			} else {
				t_name.text = targUnit.uiName;
				if (!targUnit.isInvulnerable) {
					i_iconHP.enabled = true;
					i_iconMP.enabled = true;
					t_hp.text = targUnit.HP + "/" + targUnit.HPMax;
					t_mp.text = targUnit.MP + "/" + targUnit.MPMax;
					
					t_leader.text = targUnit.regCom;
					/*string t_tR = "Traits:\n";
					foreach(string tRL in targUnit.traits) {
						t_tR += tRL + "\n";
					} 
					t_traits.text = t_tR;*/
				} else {
					_clearUIData ();
				}

				//i_unitData1.sprite = s_unitData1;
				i_unitData2.sprite = s_unitData2;
				btn_stats.gameObject.SetActive (true);

				MG_UIControl_Buffs.I._updateBuffs (targUnit.unitID);

				MG_UIControl_UnitStats.I.updateStatWindow(targUnit);
				
				// MG_UIControl_Tooltip.I.show_unit_info (targUnit);
			}
		} else {
			_clearUIData ();
		}
	}

	public void _clearUIData(){
		//MG_UIControl_Command.I.c_unitData.enabled = false;
		t_name.text = "";
		t_hp.text = "";
		t_mp.text = "";

		i_iconHP.enabled = false;
		i_iconMP.enabled = false;
		//i_unitData1.sprite = s_unitData1_gray;
		i_unitData2.sprite = s_unitData2_gray;
		btn_stats.gameObject.SetActive (false);

		// Hero Items
		t_itemTitle.text = "";
		i_itemData.sprite = s_itemData_gray;
		btn_item1.gameObject.SetActive (false);
		btn_item2.gameObject.SetActive (false);
		btn_item3.gameObject.SetActive (false);
		
		clear_squad_ui_data ();

		MG_UIControl_Buffs.I._updateBuffs ();

		MG_UIControl_UnitStats.I._hide ();
		
		MG_UIControl_Tooltip.I.hide ();
	}
	
	public void clear_squad_ui_data (){
		t_leader.text = "";
		t_traits.text = "";
	}
	#endregion

	#region "HERO ITEMS - BTN Hero Item"
	public void _btn_heroItem_1(){ _buttonInteract (0); }
	public void _btn_heroItem_2(){ _buttonInteract (1); }
	public void _btn_heroItem_3(){ _buttonInteract (2); }
	#endregion
	#region "HERO ITEMS - Button Interact"
	public void _buttonInteract(int buttonNum){
		// Get selected unit
		if(!MG_GetUnit.I._pointHasUnit(uiData_selectedPosX, uiData_selectedPosY))		return;
		MG_ClassUnit targUnit = MG_GetUnit.I._pickUnit(uiData_selectedPosX, uiData_selectedPosY);

		bool isBuy = false;

		int bi_playerNum = targUnit.playerOwner;
		int bi_playerHeroSlot = MG_Globals.I.players [bi_playerNum]._getHeroSlot (targUnit.name);
		string itemName = MG_Globals.I.players [bi_playerNum]._getHeroItemFromItemSlot(targUnit.name, buttonNum);

		if (targUnit.playerOwner == MG_Globals.I.curPlayerNum) {
			if (itemName == "none") {
				isBuy = true;
			}
		}

		if (isBuy) {
			MG_UIControl_Shop.I._showWindow_New (bi_playerNum, bi_playerHeroSlot, buttonNum);
		} else {
			MG_UIControl_ItemCheck.I._showWindow_New (itemName, false, bi_playerHeroSlot, buttonNum);
		}
	}
	#endregion
}
