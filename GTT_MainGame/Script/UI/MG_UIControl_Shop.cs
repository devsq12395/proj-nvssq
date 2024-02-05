using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Shop : MonoBehaviour {
	public static MG_UIControl_Shop I;
	public void Awake(){ I = this; }

	// UI elements
	public GameObject c;
	public Image i_shopPort;
	public Text t_shopName;
	public List<Image> i_itemPort;
	public List<Text> t_item, t_cost;
	public List<Canvas> c_itemBox;

	// Shop variables
	public List<string> shopItems, shopTypes;
	public string shopType;
	public int playerNum, heroSlotNum, itemSlotNum, shopTypeNum;

	// Constants
	public int MAX_ITEMS_IN_SHOP;

	#region "Start"
	public void _start(){
		// Constants
		MAX_ITEMS_IN_SHOP = 10;

		// UI Elements
		c.SetActive (true);
		i_shopPort = GameObject.Find("I_ItemShopPortrait").GetComponent<Image>();
		t_shopName = GameObject.Find("T_ItemShop_ShopName").GetComponent<Text>();
		for (int i = 1; i <= MAX_ITEMS_IN_SHOP; i++) {
			if (i < 10) {
				i_itemPort.Add (GameObject.Find ("I_ItBox_ItemImg_Shop_0"+i.ToString()).GetComponent<Image> ());
				t_item.Add (GameObject.Find ("TXT_ItBox_name_Shop_0"+i.ToString()).GetComponent<Text> ());
				t_cost.Add (GameObject.Find ("TXT_ItBox_price_Shop_0"+i.ToString()).GetComponent<Text> ());
				c_itemBox.Add (GameObject.Find ("C_ItemBox_Shop_0"+i.ToString()).GetComponent<Canvas> ());
			} else {
				i_itemPort.Add (GameObject.Find ("I_ItBox_ItemImg_Shop_"+i.ToString()).GetComponent<Image> ());
				t_item.Add (GameObject.Find ("TXT_ItBox_name_Shop_"+i.ToString()).GetComponent<Text> ());
				t_cost.Add (GameObject.Find ("TXT_ItBox_price_Shop_"+i.ToString()).GetComponent<Text> ());
				c_itemBox.Add (GameObject.Find ("C_ItemBox_Shop_"+i.ToString()).GetComponent<Canvas> ());
			}
		}

		// Disable UI Elements on start
		for(int i = 0; i < MAX_ITEMS_IN_SHOP; i++){
			c_itemBox [i].enabled = false;
		}
		c.SetActive (false);

		// Variables
		string[] shops = {
			"Basic Shop", "Accessory Shop"
		};
		shopTypes = new List<string> (shops);
		shopItems = new List<string> ();
	}
	#endregion

	#region "Show window"
	public void _showWindow(int newPlayerNum, int newItemSlotNum){
		c.SetActive (true);

		MG_Globals.I.pause_windowOpen = true;

		playerNum = newPlayerNum;
		itemSlotNum = newItemSlotNum;

		shopTypeNum = 0;
		_changeShop ();
	}
	#endregion
	#region "Show window - NEW"
	public void _showWindow_New(int newPlayerNum, int newHeroSlot, int newItemSlot){
		c.SetActive (true);
		MG_Globals.I.pause_itemBuying = true;

		playerNum = newPlayerNum;
		heroSlotNum = newHeroSlot;
		itemSlotNum = newItemSlot;

		shopTypeNum = 0;
		_changeShop ();
	}
	#endregion

	#region "Hide window"
	public void _hideWindow(){
		c.SetActive (false);
		MG_Globals.I.pause_itemBuying = false;

		shopTypeNum = 0;
		shopItems.Clear ();

		for (int i = 0; i < MAX_ITEMS_IN_SHOP; i++) {
			c_itemBox [i].enabled = false;
		}
	}
	#endregion

	#region "BUTTON - Check"
	public void _buttonInteract_01(){_buttonInteract (0);}
	public void _buttonInteract_02(){_buttonInteract (1);}
	public void _buttonInteract_03(){_buttonInteract (2);}
	public void _buttonInteract_04(){_buttonInteract (3);}
	public void _buttonInteract_05(){_buttonInteract (4);}
	public void _buttonInteract_06(){_buttonInteract (5);}
	public void _buttonInteract_07(){_buttonInteract (6);}
	public void _buttonInteract_08(){_buttonInteract (7);}
	public void _buttonInteract_09(){_buttonInteract (8);}
	public void _buttonInteract_10(){_buttonInteract (9);}

	public void _buttonInteract(int buttonNum){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		string checkedItem = shopItems [buttonNum];

		MG_UIControl_ItemCheck.I._showWindow_New (checkedItem, true, heroSlotNum, itemSlotNum, false);
	}
	#endregion
	#region "BUTTON - Buy"
	public void _buttonInteractBuy_01(){_buttonInteractBuy (0);}
	public void _buttonInteractBuy_02(){_buttonInteractBuy (1);}
	public void _buttonInteractBuy_03(){_buttonInteractBuy (2);}
	public void _buttonInteractBuy_04(){_buttonInteractBuy (3);}
	public void _buttonInteractBuy_05(){_buttonInteractBuy (4);}
	public void _buttonInteractBuy_06(){_buttonInteractBuy (5);}
	public void _buttonInteractBuy_07(){_buttonInteractBuy (6);}
	public void _buttonInteractBuy_08(){_buttonInteractBuy (7);}
	public void _buttonInteractBuy_09(){_buttonInteractBuy (8);}
	public void _buttonInteractBuy_10(){_buttonInteractBuy (9);}

	public void _buttonInteractBuy(int buttonNum){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		string checkedItem = shopItems [buttonNum];

		int gold = MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold;
		MG_DB_Items.I._setValues (shopItems [buttonNum]);
		int itemCost = MG_DB_Items.I.cost;

		if (gold >= itemCost) {
			gold -= itemCost;
			MG_UIControl_TopBar.I._goldGain (-itemCost);
			MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold = gold;
			MG_UIControl_TopBar.I._goldUI_update ();
			MG_Globals.I.players [MG_Globals.I.curPlayerNum]._heroChangeItem (checkedItem, heroSlotNum, itemSlotNum);

			MG_ControlCursor.I._interact();
			_hideWindow ();
		}
	}
	#endregion

	#region "Change Shop"
	public void _changeShop_next(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		shopTypeNum++;
		if (shopTypeNum >= shopTypes.Count) 			shopTypeNum = 0;
		_changeShop ();
	}

	public void _changeShop_prev(){
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		shopTypeNum--;
		if (shopTypeNum < 0) 							shopTypeNum = shopTypes.Count - 1;
		_changeShop ();
	}

	// Change shopTypeNum first before calling this to actually change shop
	public void _changeShop(){
		shopType = shopTypes [shopTypeNum];
		t_shopName.text = shopType;
		i_shopPort.sprite = MG_DB_ItemShop.I._getSprite ("shop_" + shopType);
		shopItems.Clear ();
		shopItems.AddRange(MG_DB_ItemShop.I._getItems(shopType));

		for (int i = 0; i < MAX_ITEMS_IN_SHOP; i++) {
			if (i >= shopItems.Count) {
				c_itemBox [i].enabled = false;
			} else {
				if (shopItems [i] == "NONE") {
					c_itemBox [i].enabled = false;
				} else {
					MG_DB_Items.I._setValues (shopItems [i]);

					c_itemBox [i].enabled = true;
					i_itemPort [i].sprite = MG_DB_Items.I._getSprite (shopItems [i]);
					t_item [i].text = shopItems [i];
					t_cost [i].text = MG_DB_Items.I.cost.ToString();
				}
			}
		}
	}
	#endregion
}
