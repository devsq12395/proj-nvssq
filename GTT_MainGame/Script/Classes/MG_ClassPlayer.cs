using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassPlayer {
	public int playerNum;
	public string name;
	public List<int> allies;

	// Heroes
	public List<string> hero_name, hero_item1, hero_item2, hero_item3;
	public List<int> hero_spawnPosX, hero_spawnPosY;
	public List<int> hero_respawnTime;
	public List<bool> hero_isSummoned;
	public int numberOfHeroes;

	// Items
	public List<string> items;
	public int gold, recruits;
	// Item bonuses
	public List<int> bon_damage, bon_MS;
	public List<double> bon_armor, bon_resistance, bon_abiPower;

	// Summoned units
	public List<List<int>> summonedUnits;

	// Skin list
	public List<int> skins;
	public List<string> skinOwnerName;

	// Stats
	public int heroKills = 0, unitKills = 0;

	// Units
	public List<string> unit_name;
	public List<int> unit_lim, unit_limMax, unit_cost;

	// Spells
	public List<string> spell_name;

	public MG_ClassPlayer(int newPlayerNum, int[] newAllies, int customGold = 1000, string newName = ""){
		playerNum = newPlayerNum;
		name = newName;

		allies = new List<int> ();
		allies.AddRange (newAllies);

		// Heroes
		hero_name 				= new List<string>();
		hero_item1 				= new List<string> ();
		hero_item2 				= new List<string> ();
		hero_item3 				= new List<string> ();
		hero_spawnPosX 			= new List<int> ();
		hero_spawnPosY 			= new List<int> ();
		hero_respawnTime 		= new List<int> ();
		hero_isSummoned 		= new List<bool> ();
		summonedUnits 			= new List<List<int>> ();
		unit_name				= new List<string>();
		unit_lim				= new List<int>();
		unit_limMax				= new List<int>();
		unit_cost				= new List<int>();
		spell_name				= new List<string>();

		// Set Hero List - and their item stats
		string nameTemp = "", numTemp = "", unitLimTemp = "", unitCostTemp = "";
		bon_damage = new List<int> ();
		bon_MS = new List<int> ();
		bon_armor = new List<double> ();
		bon_resistance = new List<double> ();
		bon_abiPower = new List<double> ();
		for(int i = 0; i < 8; i++){
			numTemp = (i < 10) ? ("0" + (1 + i).ToString ()) : (1 + i).ToString ();
			nameTemp = "heroName" + numTemp + "_p" + playerNum.ToString();
			_setHero (PlayerPrefs.GetString (nameTemp), 0, 0);

			bon_damage.Add (0);
			bon_MS.Add (0);
			bon_armor.Add (0);
			bon_resistance.Add (0);
			bon_abiPower.Add (0);
			summonedUnits.Add (new List<int> ());
		}

		// Set Units
		for(int i = 0; i < 10; i++){
			numTemp = (i < 10) ? ("0" + (1 + i).ToString ()) : (1 + i).ToString ();

			nameTemp = "unitName" + numTemp + "_p" + playerNum.ToString();
			unitLimTemp = "unitLim" + numTemp + "_p" + playerNum.ToString();
			unitCostTemp = "unitCost" + numTemp + "_p" + playerNum.ToString();
		}
		_setUnit ("Infantry", 7, 200);
		_setUnit ("Skirmisher", 4, 300);
		_setUnit ("Cavalry", 4, 300);
		_setUnit ("Artillery", 4, 500);
		_setUnit ("NONE", 7, 200);
		_setUnit ("NONE", 7, 200);

		// Set Spells
		for(int i = 0; i < 10; i++){
			numTemp = (i < 10) ? ("0" + (1 + i).ToString ()) : (1 + i).ToString ();
			nameTemp = "spellName" + numTemp + "_p" + playerNum.ToString();
			_setSpell (PlayerPrefs.GetString (nameTemp));
		}

		// Gold and items
		gold = customGold;
		recruits = 15;
		items = new List<string> ();
		for (int i = 1; i <= 6; i++) {
			items.Add ("NONE");
		}

		// Set skins
		skins = new List<int> ();
		skinOwnerName = new List<string> ();
		if(MG_Globals.I.curPlayerNum == playerNum){
			setAllSkins ();
		}
	}

	#region "UNIT - Set unit on available unit slot"
	public void _setUnit(string unitName, int unitLim, int unitCost){
		unit_name.Add (unitName);
		unit_lim.Add (0);
		unit_limMax.Add (unitLim);
		unit_cost.Add (unitCost);
	}
	#endregion

	#region "SPELL - Set spell on available spell slot"
	public void _setSpell(string spellName){
		spell_name.Add (spellName);
	}
	#endregion

	#region "SKIN - Set all skins"
	public void setAllSkins(){
		setSkin ("Point", ZPlayerPrefs.GetInt ("skinEquipped_Point"));
		setSkin ("PointBlue", ZPlayerPrefs.GetInt ("skinEquipped_Point"));
		setSkin ("PointRed", ZPlayerPrefs.GetInt ("skinEquipped_Point"));
		setSkin ("Camp", ZPlayerPrefs.GetInt ("skinEquipped_Camp"));
		setSkin ("CampBlue", ZPlayerPrefs.GetInt ("skinEquipped_Camp"));
		setSkin ("CampRed", ZPlayerPrefs.GetInt ("skinEquipped_Camp"));
		setSkin ("Tower", ZPlayerPrefs.GetInt ("skinEquipped_Tower"));

		int skinNum = 0;
		for (int i = 0; i < hero_name.Count; i++) {
			if (hero_name [i] != "NONE") {
				skinNum = ZPlayerPrefs.GetInt ("skinEquipped_" + hero_name [i]);
				if (skinNum != null) {
					setSkin (hero_name [i], skinNum);
				}
			}
		}
	}

	public void setSkin(string skinOwner, int newSkinNum){
		skins.Add (newSkinNum);
		skinOwnerName.Add (skinOwner);
	}

	public void shareSkin(){
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			for (int i = 0; i < skins.Count; i++) {
				if (skins [i] != 0) {
					PhotonRoom.room.gameAction_shareSkin (playerNum, skinOwnerName [i], skins [i]);
				}
			}
		}
	}
	#endregion

	#region "HERO - Set hero on available hero slot"
	public void _setHero(string heroName, int posX, int posY){
		hero_name.Add (heroName);
		hero_item1.Add ("none");
		hero_item2.Add ("none");
		hero_item3.Add ("none");
		hero_spawnPosX.Add (posX);
		hero_spawnPosY.Add (posY);
		hero_respawnTime.Add (0);
		hero_isSummoned.Add (false);

		numberOfHeroes++;

		MG_ControlSFX.I._createSFX ("heroSpot", posX, posY);
	}
	#endregion
	#region "HERO - Spawn hero on spawn point"
	public void _spawnHero_onSpawnPoint(int heroSlot){
		if (heroSlot >= numberOfHeroes) 	return;

		//MG_ControlUnits.I._createUnit(hero_name[heroSlot], hero_spawnPosX[heroSlot], hero_spawnPosY[heroSlot], playerNum);
	}
	#endregion
	#region "HERO - Set revive time"
	public void _setReviveTime(int heroSlot, int reviveTime){
		hero_respawnTime [heroSlot] = reviveTime;
	}
	#endregion
	#region "HERO - Get hero slot"
	public int _getHeroSlot(string heroName){
		int heroNum = -1;
		string output = "none";

		bool heroFound = false;
		for (int i = 0; i < hero_name.Count; i++) {
			if (heroName == hero_name [i]) {
				heroNum = i;
				break;
			}
		}

		return heroNum;
	}
	#endregion
	#region "HERO - Set summoned state"
	public void _HERO_setSummonedState(string heroName, bool summonState){
		int hIndex = _getHeroSlot (heroName);
		if (hIndex == -1)
			return;

		hero_isSummoned [hIndex] = summonState;
	}
	#endregion
	#region "HERO - Get summoned unit count"
	public int _HERO_getSummonedUnitCount(string heroName){
		int heroSlot = _getHeroSlot (heroName);
		int retVal = -1;

		if (heroSlot != -1) {
			retVal = summonedUnits [heroSlot].Count;
		}

		return retVal;
	}
	#endregion
	#region "HERO - Add summoned unit"
	public void _HERO_addSummonedUnit(string heroName, int summonedUnitID){
		int heroSlot = _getHeroSlot (heroName);

		if (heroSlot != -1) {
			summonedUnits [heroSlot].Add (summonedUnitID);
		}
	}
	#endregion
	#region "HERO - Remove summoned unit"
	public void _HERO_removeSummonedUnit(string heroName, int summonedUnitID){
		int heroSlot = _getHeroSlot (heroName);

		if (heroSlot != -1) {
			summonedUnits [heroSlot].Remove (summonedUnitID);
		}
	}
	#endregion
	#region "HERO - Get first summoned unit ID"
	public int _HERO_getFirstSummonedUnitID(string heroName){
		int heroSlot = _getHeroSlot (heroName);
		return summonedUnits [heroSlot] [0];
	}
	#endregion

	#region "HERO ITEM - Get hero item from item slot"
	/// <summary>
	/// Item slot is numbered 0, 1 & 2
	/// </summary>
	public string _getHeroItemFromItemSlot(string heroName, int itemSlot){
		int heroNum = 0;
		string output = "none";

		bool heroFound = false;
		for (int i = 0; i < hero_name.Count; i++) {
			if (heroName == hero_name [i]) {
				heroNum = i;
				heroFound = true;
				break;
			}
		}

		if (!heroFound) 		return "none";

		if (itemSlot == 0) {
			return hero_item1 [heroNum];
		}else if (itemSlot == 1) {
			return hero_item2 [heroNum];
		} else {
			return hero_item3 [heroNum];
		}
	}
	#endregion
	#region "HERO ITEM - Change item"
	public void _heroChangeItem(string itemName, int heroSlot, int itemSlot){
		MG_ControlSounds.I._playSound(7, 0, 0, false);

		if (itemSlot == 0) {
			_setItem_Hero (itemName, heroSlot, hero_item1 [heroSlot]);
			hero_item1 [heroSlot] = itemName;
		}else if (itemSlot == 1) {
			_setItem_Hero (itemName, heroSlot, hero_item2 [heroSlot]);
			hero_item2 [heroSlot] = itemName;
		}else{
			_setItem_Hero (itemName, heroSlot, hero_item3 [heroSlot]);
			hero_item3 [heroSlot] = itemName;
		}

		// AQUIRE ITEM SPECIAL CODES
		#region "Essentials"
		bool hasUnit = false;
		MG_ClassUnit iOwner = MG_Globals.I.units [0];
		foreach (MG_ClassUnit u in MG_Globals.I.units) {
			if (u.name == hero_name [heroSlot] && u.playerOwner == playerNum) {
				hasUnit = true;
				iOwner = u;
				break;
			}
		}
		#endregion
		#region "Gloves of Haste"
		switch(itemName){
			case "Gloves of Haste":
				MG_ControlBuffs.I._addBuff(iOwner, "GlovesOfHaste");
			break;
		}
		#endregion

		// Multiplayer RPC
		#region "Multiplayer RPC"
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			if(MG_Globals.I.curPlayerNum == MG_Globals.I.curPlayerOnTurn){
				PhotonRoom.room.gameAction_changeItem (itemName, heroSlot, itemSlot);
			}
		}
		#endregion
	}

	public void _setItem_Hero(string itemName, int heroSlot, string origItem){
		MG_DB_Items.I._setValues(origItem);
		bon_damage [heroSlot] -= MG_DB_Items.I.bon_damage;
		bon_MS [heroSlot] -= MG_DB_Items.I.bon_MS;
		bon_armor [heroSlot] -= MG_DB_Items.I.bon_armor;
		bon_resistance [heroSlot] -= MG_DB_Items.I.bon_resistance;
		bon_abiPower [heroSlot] -= MG_DB_Items.I.bon_abiPower;

		MG_DB_Items.I._setValues(itemName);
		bon_damage [heroSlot] += MG_DB_Items.I.bon_damage;
		bon_MS [heroSlot] += MG_DB_Items.I.bon_MS;
		bon_armor [heroSlot] += MG_DB_Items.I.bon_armor;
		bon_resistance [heroSlot] += MG_DB_Items.I.bon_resistance;
		bon_abiPower [heroSlot] += MG_DB_Items.I.bon_abiPower;
	}
	#endregion
	#region "HERO ITEM - Get item bonus stat from name"
	public int _getItemBonusStat_inputName_damage(string heroName){
		int hSlot = _getHeroSlot (heroName);
		return bon_damage [hSlot];
	}
	public int _getItemBonusStat_inputName_MS(string heroName){
		int hSlot = _getHeroSlot (heroName);
		return bon_MS [hSlot];
	}
	public double _getItemBonusStat_inputName_armor(string heroName){
		int hSlot = _getHeroSlot (heroName);
		return bon_armor [hSlot];
	}
	public double _getItemBonusStat_inputName_resistance(string heroName){
		int hSlot = _getHeroSlot (heroName);
		return bon_resistance [hSlot];
	}
	public double _getItemBonusStat_inputName_abiPow(string heroName){
		int hSlot = _getHeroSlot (heroName);
		return bon_abiPower [hSlot];
	}
	#endregion

	#region "HERO - End Turn"
	public void _heroEndTurn(){
		for (int i = 0; i < hero_respawnTime.Count; i++) {
			if (hero_respawnTime [i] > 0) {
				hero_respawnTime [i]--;
				if (hero_respawnTime [i] <= 0) {
					hero_respawnTime [i] = 0;
				}
			}
		}
	}
	#endregion

	#region "ITEM - Change item (NOW UNUSED)"
	public void _changeItem(string newItem, int itemSlot){
		// Remove old item's bonuses
//		MG_DB_Items.I._setValues(items [itemSlot]);
//		bon_damage -= MG_DB_Items.I.bon_damage;
//		bon_MS -= MG_DB_Items.I.bon_MS;
//		bon_armor -= MG_DB_Items.I.bon_armor;
//		bon_resistance -= MG_DB_Items.I.bon_resistance;
//		bon_abiPower -= MG_DB_Items.I.bon_abiPower;
//
//		items [itemSlot] = newItem;
//
//		// Add new item's bonuses
//		MG_DB_Items.I._setValues(newItem);
//		bon_damage += MG_DB_Items.I.bon_damage;
//		bon_MS += MG_DB_Items.I.bon_MS;
//		bon_armor += MG_DB_Items.I.bon_armor;
//		bon_resistance += MG_DB_Items.I.bon_resistance;
//		bon_abiPower += MG_DB_Items.I.bon_abiPower;
//
//		if (MG_Globals.I.selectedUnit_exist) {
//			MG_UIControl_UnitStats.I.updateStatWindow (MG_Globals.I.selectedUnit);
//		}
	}
	#endregion
}
