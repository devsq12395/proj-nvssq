using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_ItemUpgrade : MonoBehaviour {
	public static MG_UIControl_ItemUpgrade I;
	public void Awake(){ I = this; }

	// Canvas
	public GameObject c;
	public Image i_itemMain, i_item1, i_item2;
	public Text t_itemName1, t_itemName2, t_itemDesc1, t_itemDesc2, t_itemCost1, t_itemCost2;

	// Main variables
	public string itemMain, item1, item2;
	public int heroSlot, itemSlot;

	public void _start(){
		i_itemMain 				= GameObject.Find ("I_ItemUpg_ItemIMG_Main").GetComponent<Image> ();
		i_item1 				= GameObject.Find ("I_ItemUpg_ItemIMG_1").GetComponent<Image> ();
		i_item2 				= GameObject.Find ("I_ItemUpg_ItemIMG_2").GetComponent<Image> ();
		t_itemName1 			= GameObject.Find ("T_ItemUpg_ItemName_1").GetComponent<Text> ();
		t_itemName2 			= GameObject.Find ("T_ItemUpg_ItemName_2").GetComponent<Text> ();
		t_itemDesc1 			= GameObject.Find ("T_ItemUpg_ItemDesc_1").GetComponent<Text> ();
		t_itemDesc2			 	= GameObject.Find ("T_ItemUpg_ItemDesc_2").GetComponent<Text> ();
		t_itemCost1 			= GameObject.Find ("T_ItemUpg_ItemCost_1").GetComponent<Text> ();
		t_itemCost2 			= GameObject.Find ("T_ItemUpg_ItemCost_2").GetComponent<Text> ();

		c.SetActive (false);
	}

	#region "Show Window"
	public void _showWindow(string itemName, int newItemSlot){
		c.SetActive (true);

		itemMain = itemName;
		itemSlot = newItemSlot;
		heroSlot = MG_Globals.I.players[MG_Globals.I.selectedUnit.playerOwner]._getHeroSlot(MG_Globals.I.selectedUnit.name);
		MG_DB_Items.I._setValues (itemName);
		item1 = MG_DB_Items.I.upgInto1;
		item2 = MG_DB_Items.I.upgInto2;

		i_itemMain.sprite = MG_DB_Items.I._getSprite (itemName);

		if (item1 != "NONE" && item1 != "none") {
			i_item1.sprite = MG_DB_Items.I._getSprite (item1);
			t_itemName1.text = item1;
			MG_DB_Items.I._setValues (item1);
			t_itemDesc1.text = MG_DB_Items.I.desc;
			t_itemCost1.text = MG_DB_Items.I.cost.ToString();
		} else {
			i_item1.sprite = MG_DB_Items.I._getSprite ("none");
			t_itemName1.text = "";
			t_itemDesc1.text = "";
			t_itemCost1.text = "";
		}

		if (item2 != "NONE" && item2 != "none") {
			i_item2.sprite = MG_DB_Items.I._getSprite (item2);
			t_itemName2.text = item2;
			MG_DB_Items.I._setValues (item2);
			t_itemDesc2.text = MG_DB_Items.I.desc;
			t_itemCost2.text = MG_DB_Items.I.cost.ToString();
		} else {
			i_item2.sprite = MG_DB_Items.I._getSprite ("none");
			t_itemName2.text = "";
			t_itemDesc2.text = "";
			t_itemCost2.text = "";
		}
	}
	#endregion
	#region "Hide Window"
	public void _hideWindow(){
		c.SetActive (false);
	}
	#endregion

	#region "BUTTON - Upgrade 1"
	public void _upgrade_1(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		int gold = MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold;
		MG_DB_Items.I._setValues (item1);
		int itemCost = MG_DB_Items.I.cost;

		if (gold >= itemCost && item1 != "NONE") {
			gold -= itemCost;
			MG_UIControl_TopBar.I._goldGain (-itemCost);
			MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold = gold;

			MG_Globals.I.players [MG_Globals.I.curPlayerNum]._heroChangeItem (item1, heroSlot, itemSlot);
			c.SetActive (false);
			MG_UIControl_ItemCheck.I._hideWindow ();
			MG_ControlCursor.I._interact();
			MG_UIControl_TopBar.I._goldUI_update ();
		}
	}
	#endregion
	#region "BUTTON - Upgrade 2"
	public void _upgrade_2(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		int gold = MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold;
		MG_DB_Items.I._setValues (item2);
		int itemCost = MG_DB_Items.I.cost;

		if (gold >= itemCost && item2 != "NONE") {
			gold -= itemCost;
			MG_UIControl_TopBar.I._goldGain (-itemCost);
			MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold = gold;

			MG_Globals.I.players [MG_Globals.I.curPlayerNum]._heroChangeItem (item2, heroSlot, itemSlot);
			c.SetActive (false);
			MG_UIControl_ItemCheck.I._hideWindow ();
			MG_ControlCursor.I._interact();
			MG_UIControl_TopBar.I._goldUI_update ();
		}
	}
	#endregion
}
