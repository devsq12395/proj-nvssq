using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlItems : MonoBehaviour {
	public static MG_ControlItems I;
	public void Awake(){ I = this; }

	private int MAX_ITEMS_IN_INVENTORY;

	public void _start(){
		MAX_ITEMS_IN_INVENTORY = 6; // Starts from 1
	}

	// Item effect - when attacking
	// Can return a value that will be added to the damage of the attack
	#region "ITEM EFFECTS - Damage Dealing - Attack (Normal)"
	public int _itemEffects_DamageDealing_attack(MG_ClassUnit damager, int damageDealt){
		int damager_playerNum = damager.playerOwner;
		int output = 0;

		for (int itemIndex = 0; itemIndex < MAX_ITEMS_IN_INVENTORY; itemIndex++) {
			switch (MG_Globals.I.players [damager_playerNum].items [itemIndex]) {
				#region "testItem_01"
				case "testItem_lvl1":
					
				break;
				#endregion
			}
		}

		return output;
	}
	#endregion
}
