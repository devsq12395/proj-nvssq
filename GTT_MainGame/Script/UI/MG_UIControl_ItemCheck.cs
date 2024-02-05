using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_ItemCheck : MonoBehaviour {
	public static MG_UIControl_ItemCheck I;
	public void Awake(){ I = this; }

	// UI elements
	public GameObject c;
	public Image i_item;
	public Text t_itemName, t_itemDesc, t_itemCost;
	public Button btn_buy, btn_upg, btn_sell;

	// Item variables
	public string itemName;
	public int itemCost, heroSlot, itemSlot;
	public bool isShop;

	public void _start(){
		i_item 					= GameObject.Find ("I_ItemCheck_ItemIMG").GetComponent<Image> ();
		btn_buy 				= GameObject.Find ("BTN_ItemCheck_Buy").GetComponent<Button> ();
		btn_upg 				= GameObject.Find ("BTN_ItemCheck_Upgrade").GetComponent<Button> ();
		btn_sell 				= GameObject.Find ("BTN_ItemCheck_Sell").GetComponent<Button> ();
		t_itemName 				= GameObject.Find ("T_ItemCheck_Name").GetComponent<Text> ();
		t_itemDesc 				= GameObject.Find ("T_ItemCheck_Desc").GetComponent<Text> ();
		t_itemCost 				= GameObject.Find ("T_ItemCheck_Cost").GetComponent<Text> ();

		c.SetActive (false);
	}

	#region "Show Window"
	public void _showWindow(string newItemName, bool newIsShop = false, int newItemSlot = 0, bool isEnemyCheck = false){
		c.SetActive (true);

		itemName = newItemName;
		itemSlot = newItemSlot;

		i_item.sprite = MG_DB_Items.I._getSprite (itemName);
		t_itemName.text = itemName;
		MG_DB_Items.I._setValues (itemName);
		t_itemDesc.text = MG_DB_Items.I.desc;

		isShop = newIsShop;
		if (isShop) {
			itemCost = MG_DB_Items.I.cost;
			t_itemCost.text = "";
			t_itemCost.text = itemCost.ToString();

			btn_buy.gameObject.SetActive (true);
			btn_upg.gameObject.SetActive (false);
			btn_sell.gameObject.SetActive (false);
		} else if (isEnemyCheck) {
			itemCost = MG_DB_Items.I.cost;
			t_itemCost.text = "";
			t_itemCost.text = itemCost.ToString();

			btn_buy.gameObject.SetActive (false);
			btn_upg.gameObject.SetActive (false);
			btn_sell.gameObject.SetActive (false);
		} else {
			t_itemCost.text = "";
			itemCost = MG_DB_Items.I.cost / 2;
			t_itemCost.text = "Sell for " + itemCost.ToString();

			// Already owned item check
			btn_buy.gameObject.SetActive (false);
			btn_upg.gameObject.SetActive (true);
			btn_sell.gameObject.SetActive (true);
		}
	}
	#endregion
	#region "Show Window - NEW"
	public void _showWindow_New(string newItemName, bool newIsShop = false, int newHeroSlot = 0, int newItemSlot = 0, bool isEnemyCheck = false){
		c.SetActive (true);
		MG_Globals.I.pause_itemBuying = true;

		heroSlot = newHeroSlot;
		itemName = newItemName;
		itemSlot = newItemSlot;

		i_item.sprite = MG_DB_Items.I._getSprite (itemName);
		t_itemName.text = itemName;
		MG_DB_Items.I._setValues (itemName);
		t_itemDesc.text = MG_DB_Items.I.desc;

		isShop = newIsShop;
		if (isShop) {
			itemCost = MG_DB_Items.I.cost;
			t_itemCost.text = "";
			t_itemCost.text = itemCost.ToString();

			btn_buy.gameObject.SetActive (true);
			btn_upg.gameObject.SetActive (false);
			btn_sell.gameObject.SetActive (false);
		} else if (isEnemyCheck) {
			itemCost = MG_DB_Items.I.cost;
			t_itemCost.text = "";
			t_itemCost.text = itemCost.ToString();

			btn_buy.gameObject.SetActive (false);
			btn_upg.gameObject.SetActive (false);
			btn_sell.gameObject.SetActive (false);
		} else {
			t_itemCost.text = "";
			itemCost = MG_DB_Items.I.cost / 2;
			t_itemCost.text = "Sell for " + itemCost.ToString();

			// Already owned item check
			btn_buy.gameObject.SetActive (false);
			btn_upg.gameObject.SetActive (true);
			btn_sell.gameObject.SetActive (true);
		}
	}
	#endregion

	#region "Hide Window"
	public void _hideWindow(){
		c.SetActive (false);
		MG_Globals.I.pause_itemBuying = false;
	}
	#endregion

	#region "BUTTON - Buy"
	public void _buyButton(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		int gold = MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold;

		if (gold >= itemCost) {
			gold -= itemCost;
			MG_UIControl_TopBar.I._goldGain (-itemCost);
			MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold = gold;
			MG_UIControl_TopBar.I._goldUI_update ();
			MG_Globals.I.players [MG_Globals.I.curPlayerNum]._heroChangeItem (itemName, heroSlot, itemSlot);

			MG_ControlCursor.I._interact();
			MG_UIControl_Shop.I._hideWindow ();
			_hideWindow ();
		}
	}
	#endregion
	#region "BUTTON - Sell"
	public void _sellButton(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold += itemCost;
		MG_UIControl_TopBar.I._goldUI_update ();

		// OLD
		/*MG_Globals.I.players [MG_Globals.I.curPlayerNum]._changeItem ("NONE", itemSlot);

		c.enabled = false;
		MG_UIControl_Shop.I._hideWindow ();
		MG_UIControl_Items.I._updateItemsUI ();
		MG_UIControl_TopBar.I._goldUI_update ();*/

		// NEW
		MG_Globals.I.players [MG_Globals.I.curPlayerNum]._heroChangeItem ("none", heroSlot, itemSlot);
		MG_UIControl_Shop.I._hideWindow ();
		MG_ControlCursor.I._interact();
		_hideWindow ();
	}
	#endregion
	#region "BUTTON - Upgrade"
	public void _upgradeButton(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		MG_UIControl_ItemUpgrade.I._showWindow (itemName, itemSlot);
	}
	#endregion
}
