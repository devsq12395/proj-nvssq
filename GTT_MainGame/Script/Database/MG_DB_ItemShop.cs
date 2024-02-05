using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_ItemShop : MonoBehaviour {
	public static MG_DB_ItemShop I;
	public void Awake(){ I = this; }

	public Sprite dummy, shop_blacksmith, shop_accessory;
	public List<string> items;

	public void _start(){
		items = new List<string> ();
	}

	#region "Get Sprite"
	public Sprite _getSprite(string itemName){
		Sprite retVal = dummy;
		switch (itemName) {
			case "shop_Blacksmith": retVal = shop_blacksmith; break;
			case "shop_Basic Shop": retVal = shop_blacksmith; break;
			case "shop_Accessory Shop": retVal = shop_accessory; break;
		}
		return retVal;
	}
	#endregion

	public List<string> _getItems(string shopName){
		items.Clear ();
		switch (shopName) {
			case "Basic Shop":
				items.Add ("Sword Lvl 1");
				items.Add ("Shield Lvl 1");
				items.Add ("Wand Lvl 1");
				items.Add ("Hood Lvl 1");
				items.Add ("Armorbreaker Mace Lvl 1");
				items.Add ("Centaur Axe Lvl 1");
				items.Add ("Wizard Sceptre Lvl 1");
				items.Add ("Punisher's Wand Lvl 1");
			break;
			case "Accessory Shop":
				items.Add ("Gloves of Haste");
				items.Add ("Arcanium Ring Lvl 1");
				items.Add ("Orb of Slow Lvl 1");
			break;
		}

		return items;
	}
}
