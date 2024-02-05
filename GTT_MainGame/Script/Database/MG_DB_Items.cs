using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_DB_Items : MonoBehaviour {
	public static MG_DB_Items I;
	public void Awake(){ I = this; }

	public Sprite dummy, i_none, i_testItem_lvl1, i_testItem_lvl2, i_testItem_spec,
		i_sword01, i_sword02, i_sword03, i_sword04,
		i_shield01, i_shield02, i_shield03, i_shield04,
		i_hood01, i_hood02, i_hood03, i_hood04,
		i_wand01, i_wand02, i_wand03, i_wand04,
		i_glovesOfHaste, i_arcRing01, i_arcRing02,
		i_armorBreaker01, i_armorBreaker02, i_armorBreaker03,
		i_orbOfSlow01, i_orbOfSlow02,
		i_centaurAxe01, i_centaurAxe02, i_centaurAxe03,
		i_wizardSceptre01, i_wizardSceptre02, i_wizardSceptre03,
		i_punisherWand01, i_punisherWand02, i_punisherWand03;

	public string name, desc, upgInto1, upgInto2;
	public int cost;
	public int bon_damage, bon_MS;
	public double bon_armor, bon_resistance, bon_abiPower;

	#region "Get Sprite"
	public Sprite _getSprite(string itemName){
		Sprite retVal = dummy;
		switch (itemName) {
			case "testItem_lvl1": retVal = i_testItem_lvl1; break;
			case "testItem_lvl2": retVal = i_testItem_lvl2; break;
			case "testItem_special": retVal = i_testItem_spec; break;

			case "Sword Lvl 1": retVal = i_sword01; break;
			case "Sword Lvl 2": retVal = i_sword02; break;
			case "Sword Lvl 3": retVal = i_sword03; break;
			case "Sword Lvl 4": retVal = i_sword04; break;

			case "Shield Lvl 1": retVal = i_shield01; break;
			case "Shield Lvl 2": retVal = i_shield02; break;
			case "Shield Lvl 3": retVal = i_shield03; break;
			case "Shield Lvl 4": retVal = i_shield04; break;

			case "Hood Lvl 1": retVal = i_hood01; break;
			case "Hood Lvl 2": retVal = i_hood02; break;
			case "Hood Lvl 3": retVal = i_hood03; break;
			case "Hood Lvl 4": retVal = i_hood04; break;

			case "Wand Lvl 1": retVal = i_wand01; break;
			case "Wand Lvl 2": retVal = i_wand02; break;
			case "Wand Lvl 3": retVal = i_wand03; break;
			case "Wand Lvl 4": retVal = i_wand04; break;
			
			case "Gloves of Haste": retVal = i_glovesOfHaste; break;

			case "Arcanium Ring Lvl 1": retVal = i_arcRing01; break;
			case "Arcanium Ring Lvl 2": retVal = i_arcRing02; break;

			case "Armorbreaker Mace Lvl 1": retVal = i_armorBreaker01; break;
			case "Armorbreaker Mace Lvl 2": retVal = i_armorBreaker02; break;
			case "Armorbreaker Mace Lvl 3": retVal = i_armorBreaker03; break;

			case "Orb of Slow Lvl 1": retVal = i_orbOfSlow01; break;
			case "Orb of Slow Lvl 2": retVal = i_orbOfSlow02; break;

			case "Centaur Axe Lvl 1": retVal = i_centaurAxe01; break;
			case "Centaur Axe Lvl 2": retVal = i_centaurAxe02; break;
			case "Centaur Axe Lvl 3": retVal = i_centaurAxe03; break;

			case "Wizard Sceptre Lvl 1": retVal = i_wizardSceptre01; break;
			case "Wizard Sceptre Lvl 2": retVal = i_wizardSceptre02; break;
			case "Wizard Sceptre Lvl 3": retVal = i_wizardSceptre03; break;

			case "Punisher's Wand Lvl 1": retVal = i_punisherWand01; break;
			case "Punisher's Wand Lvl 2": retVal = i_punisherWand02; break;
			case "Punisher's Wand Lvl 3": retVal = i_punisherWand03; break;

			default: retVal = i_none; break;
		}
		return retVal;
	}
	#endregion

	#region "Set Values"
	public void _setValues(string itemName){
		#region "Default values"
		desc = "";
		cost = 0;

		bon_damage = 0;
		bon_MS = 0;
		bon_armor = 0;
		bon_resistance = 0;
		bon_abiPower = 0;

		upgInto1 = "";
		upgInto2 = "";
		#endregion

		switch (itemName) {
			#region "testItem_lvl1"
			case "testItem_lvl1":
				desc = "sadasdsad";
				cost = 100;

				bon_damage = 25;
				bon_MS = 0;
				bon_armor = 0;
				bon_resistance = 0;
				bon_abiPower = 0;

				upgInto1 = "testItem_lvl2";
				upgInto2 = "testItem_special";
			break;
			#endregion
			#region "testItem_lvl2"
			case "testItem_lvl2":
				desc = "sadasdsad";
				cost = 1;

				upgInto1 = "testItem_lvl2";
				upgInto2 = "testItem_special";
			break;
			#endregion
			#region "testItem_special"
			case "testItem_special":
				desc = "sadasdsad";
				cost = 1;

				upgInto1 = "testItem_lvl2";
				upgInto2 = "testItem_special";
			break;
			#endregion

			#region "Sword Lvl 1"
			case "Sword Lvl 1":
				desc = "+3 attack damage";
				cost = 100;

				bon_damage = 3;
				upgInto1 = "Sword Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Sword Lvl 2"
			case "Sword Lvl 2":
				desc = "+6 attack damage";
				cost = 200;

				bon_damage = 6;
				upgInto1 = "Sword Lvl 3";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Sword Lvl 3"
			case "Sword Lvl 3":
				desc = "+9 attack damage";
				cost = 400;

				bon_damage = 9;
				upgInto1 = "Sword Lvl 4";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Sword Lvl 4"
			case "Sword Lvl 4":
				desc = "+12 attack damage";
				cost = 800;

				bon_damage = 12;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Shield Lvl 1"
			case "Shield Lvl 1":
				desc = "+4% armor";
				cost = 100;

				bon_armor = 0.04;
				upgInto1 = "Shield Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Shield Lvl 2"
			case "Shield Lvl 2":
				desc = "+7% armor";
				cost = 200;

				bon_armor = 0.07;
				upgInto1 = "Shield Lvl 3";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Shield Lvl 3"
			case "Shield Lvl 3":
				desc = "+10% armor";
				cost = 400;

				bon_armor = 0.1;
				upgInto1 = "Shield Lvl 4";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Shield Lvl 4"
			case "Shield Lvl 4":
				desc = "+13% armor";
				cost = 800;

				bon_armor = 0.13;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Wand Lvl 1"
			case "Wand Lvl 1":
				desc = "+5% magic damage";
				cost = 100;

				bon_abiPower = 0.05;
				upgInto1 = "Wand Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Wand Lvl 2"
			case "Wand Lvl 2":
				desc = "+8% magic damage";
				cost = 200;

				bon_abiPower = 0.08;
				upgInto1 = "Wand Lvl 3";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Wand Lvl 3"
			case "Wand Lvl 3":
				desc = "+11% magic damage";
				cost = 400;

				bon_abiPower = 0.11;
				upgInto1 = "Wand Lvl 4";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Wand Lvl 4"
			case "Wand Lvl 4":
				desc = "+14% magic damage";
				cost = 800;

				bon_abiPower = 0.14;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Hood Lvl 1"
			case "Hood Lvl 1":
				desc = "+4% magic resistance";
				cost = 100;

				bon_resistance = 0.04;
				upgInto1 = "Hood Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Hood Lvl 2"
			case "Hood Lvl 2":
				desc = "+7% magic resistance";
				cost = 200;

				bon_resistance = 0.07;
				upgInto1 = "Hood Lvl 3";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Hood Lvl 3"
			case "Hood Lvl 3":
				desc = "+10% magic resistance";
				cost = 400;

				bon_resistance = 0.10;
				upgInto1 = "Hood Lvl 4";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Hood Lvl 4"
			case "Hood Lvl 4":
				desc = "+13% magic resistance";
				cost = 800;

				bon_resistance = 0.13;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Gloves of Haste"
			case "Gloves of Haste":
				desc = "Wearer can attack twice per turn. Does not stack.";
				cost = 700;

				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Arcanium Ring Lvl 1"
			case "Arcanium Ring Lvl 1":
				desc = "20 MP regen per turn. Does not stack with other Arcanium Rings.";
				cost = 500;

				upgInto1 = "Arcanium Ring Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Arcanium Ring Lvl 2"
			case "Arcanium Ring Lvl 2":
				desc = "50 MP regen per turn. Does not stack with other Arcanium Rings.";
				cost = 1000;

				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Armorbreaker Mace Lvl 1"
			case "Armorbreaker Mace Lvl 1":
				desc = "+9 damage and attacks do -10% armor to target for 2 turns. Armor break does not stack.";
				cost = 500;

				bon_damage = 9;
				upgInto1 = "Armorbreaker Mace Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Armorbreaker Mace Lvl 2"
			case "Armorbreaker Mace Lvl 2":
				desc = "+15 damage and attacks do -14% armor to target for 2 turns. Armor break does not stack.";
				cost = 600;

				bon_damage = 15;
				upgInto1 = "Armorbreaker Mace Lvl 3";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Armorbreaker Mace Lvl 3"
			case "Armorbreaker Mace Lvl 3":
				desc = "+21 damage and attacks do -18% armor to target for 2 turns. Armor break does not stack.";
				cost = 700;

				bon_damage = 21;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Orb of Slow Lvl 1"
			case "Orb of Slow Lvl 1":
				desc = "+9 damage and attacks do -1 MS to target for 2 turns. Slow does not stack.";
				cost = 400;

				bon_damage = 9;
				upgInto1 = "Orb of Slow Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Orb of Slow Lvl 2"
			case "Orb of Slow Lvl 2":
				desc = "+15 damage and attacks do -2 MS to target for 2 turns. Slow does not stack.";
				cost = 500;

				bon_damage = 15;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Centaur Axe Lvl 1"
			case "Centaur Axe Lvl 1":
				desc = "+9 damage and attacks will cleave to nearby enemies for 40% damage. Cleave does not stack.";
				cost = 400;

				bon_damage = 9;
				upgInto1 = "Centaur Axe Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Centaur Axe Lvl 2"
			case "Centaur Axe Lvl 2":
				desc = "+15 damage and attacks will cleave to nearby enemies for 60% damage. Cleave does not stack.";
				cost = 400;

				bon_damage = 15;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Wizard Sceptre Lvl 1"
			case "Wizard Sceptre Lvl 1":
				desc = "+10% ability power.";
				cost = 800;

				bon_abiPower = 0.1;
				upgInto1 = "Wizard Sceptre Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Wizard Sceptre Lvl 2"
			case "Wizard Sceptre Lvl 2":
				desc = "+15% ability power.";
				cost = 1000;

				bon_abiPower = 0.15;
				upgInto1 = "Wizard Sceptre Lvl 3";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Wizard Sceptre Lvl 3"
			case "Wizard Sceptre Lvl 3":
				desc = "+25% ability power.";
				cost = 1300;

				bon_abiPower = 0.25;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion

			#region "Punisher's Wand Lvl 1"
			case "Punisher's Wand Lvl 1":
				desc = "+6% ability power and attacks will do -6% magic resistance to target for 2 turns. Effect does not stack.";
				cost = 600;

				bon_abiPower = 0.06;
				upgInto1 = "Punisher's Wand Lvl 2";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Punisher's Wand Lvl 2"
			case "Punisher's Wand Lvl 2":
				desc = "+9% ability power and attacks will do -12% magic resistance to target for 2 turns. Effect does not stack.";
				cost = 700;

				bon_abiPower = 0.09;
				upgInto1 = "Punisher's Wand Lvl 3";
				upgInto2 = "NONE";
			break;
			#endregion
			#region "Punisher's Wand Lvl 3"
			case "Punisher's Wand Lvl 3":
				desc = "+12% ability power and attacks will do -14% magic resistance to target for 2 turns. Effect does not stack.";
				cost = 800;

				bon_abiPower = 0.12;
				upgInto1 = "NONE";
				upgInto2 = "NONE";
			break;
			#endregion
		}
	}
	#endregion
}
