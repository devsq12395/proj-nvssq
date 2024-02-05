using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayerPrefs : MonoBehaviour {
	public static SetupPlayerPrefs I;
	public void Awake(){ I = this; }

	public void _start(){
		ZPlayerPrefs.Initialize("sqPrefEncrypt29845", "09164667352sss");
		#region "Test Player Prefs"
		PlayerPrefs.SetInt("multiplayer_playerId", 1);
		PlayerPrefs.SetInt("playerNum", 1);
		#endregion

		PlayerPrefs.SetString("game_mapName", "Encounter");
		//PlayerPrefs.SetString ("playerName", "");
		// First run
		if (PlayerPrefs.GetString ("playerName") == null || PlayerPrefs.GetString ("playerName") == "") {
			PlayerPrefs.SetInt("lvl", 1);
			PlayerPrefs.SetInt("exp", 0);

			PlayerPrefs.SetInt("opt_sound", 1);
			PlayerPrefs.SetInt("opt_music", 1);
		}

		#region "0.5"
		// 0.5 First run
		//PlayerPrefs.SetString("0.5", "");
		if(PlayerPrefs.GetString("0.5") == null || PlayerPrefs.GetString ("0.5") == ""){
			PlayerPrefs.SetString ("0.5", "1");
			ZPlayerPrefs.SetInt ("crystals", 0);
			ZPlayerPrefs.SetInt ("keys", 0);
			ZPlayerPrefs.SetString("lastPlayDate", System.DateTime.Now.Date.AddDays(-1).ToString());
			ZPlayerPrefs.SetInt("DailyBon_Day", 1);

			// Skins - Buildings
			string defSkinStat_bldg = "1";
			for (int i = 1; i <= 30; i++) {
				defSkinStat_bldg += "0";
			}
			ZPlayerPrefs.SetString ("skinsUnlocked_Camp", defSkinStat_bldg);
			ZPlayerPrefs.SetInt ("skinEquipped_Camp", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_CampBlue", defSkinStat_bldg);
			ZPlayerPrefs.SetInt ("skinEquipped_CampBlue", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_CampRed", defSkinStat_bldg);
			ZPlayerPrefs.SetInt ("skinEquipped_CampRed", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Point", defSkinStat_bldg);
			ZPlayerPrefs.SetInt ("skinEquipped_Point", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_PointBlue", defSkinStat_bldg);
			ZPlayerPrefs.SetInt ("skinEquipped_PointBlue", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_PointRed", defSkinStat_bldg);
			ZPlayerPrefs.SetInt ("skinEquipped_PointRed", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Tower", defSkinStat_bldg);
			ZPlayerPrefs.SetInt ("skinEquipped_Tower", 0);

			// Skins - Hero
			string defSkinStat_hero = "1";
			for (int i = 1; i <= 19; i++) {
				defSkinStat_hero += "0";
			}
			ZPlayerPrefs.SetString ("skinsUnlocked_Robin", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Robin", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Ajax", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Ajax", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Colt", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Colt", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Victoria", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Victoria", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Amy", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Amy", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Alicia", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Alicia", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_ElderTreant", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_ElderTreant", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Ragnaros", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Ragnaros", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Ifreet", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Ifreet", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_SpectralWitch", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_SpectralWitch", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Drakgul", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Drakgul", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Masamune", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Masamune", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Siegfried", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Siegfried", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Abel", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Abel", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Yukino", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Yukino", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Cody", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Cody", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Hilde", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Hilde", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Elise", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Elise", 0);
		}
            		#endregion
		#region "0.7"
		//PlayerPrefs.SetString("0.7", "");
		if (PlayerPrefs.GetString ("0.7") == null || PlayerPrefs.GetString ("0.7") == "") {
			PlayerPrefs.SetString ("0.7", "1");

			PlayerPrefs.SetInt ("camp_highScore_01", 0);
			PlayerPrefs.SetInt ("camp_highScore_02", 0);
			PlayerPrefs.SetInt ("camp_heroKills", 100);
			PlayerPrefs.SetInt ("camp_unitKills", 100);
			PlayerPrefs.SetInt ("camp_heroDeaths", 100);

			for (int i = 1; i <= 10; i++) {
				PlayerPrefs.SetInt ("preset_slot" + i.ToString () + "_occupied", 0);
			}

			ZPlayerPrefs.SetInt ("unitUnlocked_Swordsman", 1);
			ZPlayerPrefs.SetInt ("unitUnlocked_Rifleman", 1);
			ZPlayerPrefs.SetInt ("unitUnlocked_Apprentice", 1);
			ZPlayerPrefs.SetInt ("unitUnlocked_Horseman", 1);
			ZPlayerPrefs.SetInt ("unitUnlocked_Officer", 1);
			ZPlayerPrefs.SetInt ("unitUnlocked_Medic", 1);
			ZPlayerPrefs.SetInt ("unitUnlocked_Goblin", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_GoblinArcher", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_Hellhound", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_Grenadier", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_OrcGrunt", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_OrcRifleman", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_ImperialInfantry", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_ImperialCavalry", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_ImperialSniper", 0);
			ZPlayerPrefs.SetInt ("unitUnlocked_testLocked", 0);

			ZPlayerPrefs.SetInt ("spellUnlocked_Heal", 1);
			ZPlayerPrefs.SetInt ("spellUnlocked_Boost", 1);
			ZPlayerPrefs.SetInt ("spellUnlocked_MassBlessing", 1);
			ZPlayerPrefs.SetInt ("spellUnlocked_Vision", 1);
			ZPlayerPrefs.SetInt ("spellUnlocked_Haste", 1);

			string defSkinStat_hero = "1";
			for (int i = 1; i <= 19; i++) {
				defSkinStat_hero += "0";
			}
			ZPlayerPrefs.SetString ("skinsUnlocked_May", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_May", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Walter", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Walter", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Aleric", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Aleric", 0);
			ZPlayerPrefs.SetString ("skinsUnlocked_Eve", defSkinStat_hero);
			ZPlayerPrefs.SetInt ("skinEquipped_Eve", 0);
		}
	#endregion
		#region "1.1"
		//PlayerPrefs.SetString("1.1", "");
		if(PlayerPrefs.GetString("1.1") == null || PlayerPrefs.GetString ("1.1") == ""){
			PlayerPrefs.SetString ("1.1", "1");
			ZPlayerPrefs.SetString ("cardName_1", "Swordsman");
			ZPlayerPrefs.SetString ("cardName_2", "Swordsman");
			ZPlayerPrefs.SetString ("cardName_3", "Rifleman");
			ZPlayerPrefs.SetString ("cardName_4", "Rifleman");
			ZPlayerPrefs.SetString ("cardName_5", "Apprentice");
			ZPlayerPrefs.SetString ("cardName_6", "Apprentice");
			ZPlayerPrefs.SetString ("cardName_7", "Officer");
			ZPlayerPrefs.SetString ("cardName_8", "Farm");
			ZPlayerPrefs.SetString ("cardName_9", "Farm");
			ZPlayerPrefs.SetString ("cardName_10", "Plantation");
			ZPlayerPrefs.SetString ("cardName_11", "Barracks");
			ZPlayerPrefs.SetString ("cardName_12", "Barracks");
		}
		#endregion
		#region "1.2"
		//PlayerPrefs.SetString("1.2", "");
		if(PlayerPrefs.GetString("1.2") == null || PlayerPrefs.GetString ("1.2") == ""){
			PlayerPrefs.SetString ("1.2", "1");

			PlayerPrefs.SetInt ("camp_highScore_01", 0);
			PlayerPrefs.SetInt ("camp_highScore_02", 0);
			PlayerPrefs.SetInt ("camp_highScore_03", 0);

			ZPlayerPrefs.SetString ("cardName_11", "Barracks");
			ZPlayerPrefs.SetString ("cardName_12", "Barracks");

			ZPlayerPrefs.SetInt ("cardUnlocked_Spy", 0);
			ZPlayerPrefs.SetInt ("cardUnlocked_Viking", 0);
			ZPlayerPrefs.SetInt ("cardUnlocked_VikingRaider", 0);
			ZPlayerPrefs.SetInt ("cardUnlocked_Spectre", 0);
			ZPlayerPrefs.SetInt ("cardUnlocked_FireSpawn", 1);
			ZPlayerPrefs.SetInt ("cardUnlocked_FireElemental", 1);
		}
		#endregion
		#region "1.3"
		//PlayerPrefs.SetString("1.3", "");
		if (PlayerPrefs.GetString("1.3") == null || PlayerPrefs.GetString ("1.3") == "") {
			PlayerPrefs.SetString ("1.3", "1");

			ZPlayerPrefs.SetString ("cardName_13", "Robin");
			ZPlayerPrefs.SetString ("cardName_14", "Ajax");
			ZPlayerPrefs.SetString ("cardName_15", "Colt");
			ZPlayerPrefs.SetString ("cardName_16", "Elizabeth");
			ZPlayerPrefs.SetString ("cardName_17", "Reinforcements");
			ZPlayerPrefs.SetString ("cardName_18", "Reinforcements");
			ZPlayerPrefs.SetString ("cardName_19", "Horseman");
			ZPlayerPrefs.SetString ("cardName_20", "Horseman");
			ZPlayerPrefs.SetString ("cardName_21", "Cannon");
			ZPlayerPrefs.SetString ("cardName_22", "Cannon");
			ZPlayerPrefs.SetString ("cardName_23", "LumberMill");
			ZPlayerPrefs.SetString ("cardName_24", "LumberMill");
		}
		#endregion
		
		#region "1.4"
		//PlayerPrefs.SetString("1.4", "");
		if (PlayerPrefs.GetString("1.4") == null || PlayerPrefs.GetString ("1.4") == "") {
			PlayerPrefs.SetString ("1.4", "1");

			PlayerPrefs.SetInt ("preset_slot1_occupied", 1);
			PlayerPrefs.SetString ("preset_slot1_name", "Begginer's Deck");
			PlayerPrefs.SetString ("preset_slot1_card1", "Robin");
			PlayerPrefs.SetString ("preset_slot1_card2", "Ajax");
			PlayerPrefs.SetString ("preset_slot1_card3", "Reinforcements");
			PlayerPrefs.SetString ("preset_slot1_card4", "Reinforcements");
			PlayerPrefs.SetString ("preset_slot1_card5", "Reinforcements");
			PlayerPrefs.SetString ("preset_slot1_card6", "Farm");
			PlayerPrefs.SetString ("preset_slot1_card7", "Farm");
			PlayerPrefs.SetString ("preset_slot1_card8", "Farm");
			PlayerPrefs.SetString ("preset_slot1_card9", "LumberMill");
			PlayerPrefs.SetString ("preset_slot1_card10", "LumberMill");
			PlayerPrefs.SetString ("preset_slot1_card11", "LumberMill");
			PlayerPrefs.SetString ("preset_slot1_card12", "Barracks");
			
			PlayerPrefs.SetString ("preset_slot1_card13", "Barracks");
			PlayerPrefs.SetString ("preset_slot1_card14", "Barracks");
			PlayerPrefs.SetString ("preset_slot1_card15", "HydraSpawn");
			PlayerPrefs.SetString ("preset_slot1_card16", "HydraSpawn");
			PlayerPrefs.SetString ("preset_slot1_card17", "Cannon");
			PlayerPrefs.SetString ("preset_slot1_card18", "Cannon");
			PlayerPrefs.SetString ("preset_slot1_card19", "Apprentice");
			PlayerPrefs.SetString ("preset_slot1_card20", "Apprentice");
			PlayerPrefs.SetString ("preset_slot1_card21", "ShieldTower");
			PlayerPrefs.SetString ("preset_slot1_card22", "ShieldTower");
			PlayerPrefs.SetString ("preset_slot1_card23", "Rifleman");
			PlayerPrefs.SetString ("preset_slot1_card24", "Rifleman");

			PlayerPrefs.SetInt ("preset_slot2_occupied", 1);
			PlayerPrefs.SetString ("preset_slot2_name", "Mass Infantry");
			PlayerPrefs.SetString ("preset_slot2_card1", "Ajax");
			PlayerPrefs.SetString ("preset_slot2_card2", "Elizabeth");
			PlayerPrefs.SetString ("preset_slot2_card3", "Reinforcements");
			PlayerPrefs.SetString ("preset_slot2_card4", "Reinforcements");
			PlayerPrefs.SetString ("preset_slot2_card5", "ImperialMarch");
			PlayerPrefs.SetString ("preset_slot2_card6", "ImperialMarch");
			PlayerPrefs.SetString ("preset_slot2_card7", "LumberMill");
			PlayerPrefs.SetString ("preset_slot2_card8", "LumberMill");
			PlayerPrefs.SetString ("preset_slot2_card9", "LumberMill");
			PlayerPrefs.SetString ("preset_slot2_card10", "Barracks");
			PlayerPrefs.SetString ("preset_slot2_card11", "Barracks");
			PlayerPrefs.SetString ("preset_slot2_card12", "Barracks");
			
			PlayerPrefs.SetString ("preset_slot2_card13", "Farm");
			PlayerPrefs.SetString ("preset_slot2_card14", "Farm");
			PlayerPrefs.SetString ("preset_slot2_card15", "Farm");
			PlayerPrefs.SetString ("preset_slot2_card16", "Officer");
			PlayerPrefs.SetString ("preset_slot2_card17", "Officer");
			PlayerPrefs.SetString ("preset_slot2_card18", "Apprentice");
			PlayerPrefs.SetString ("preset_slot2_card19", "Apprentice");
			PlayerPrefs.SetString ("preset_slot2_card20", "Apprentice");
			PlayerPrefs.SetString ("preset_slot2_card21", "Rifleman");
			PlayerPrefs.SetString ("preset_slot2_card22", "Rifleman");
			PlayerPrefs.SetString ("preset_slot2_card23", "ShieldTower");
			PlayerPrefs.SetString ("preset_slot2_card24", "ShieldTower");

			PlayerPrefs.SetInt ("preset_slot3_occupied", 1);
			PlayerPrefs.SetString ("preset_slot3_name", "Monster Deck");
			PlayerPrefs.SetString ("preset_slot3_card1", "Colt");
			PlayerPrefs.SetString ("preset_slot3_card2", "ImperialInfantry");
			PlayerPrefs.SetString ("preset_slot3_card3", "ImperialInfantry");
			PlayerPrefs.SetString ("preset_slot3_card4", "LumberMill");
			PlayerPrefs.SetString ("preset_slot3_card5", "LumberMill");
			PlayerPrefs.SetString ("preset_slot3_card6", "LumberMill");
			PlayerPrefs.SetString ("preset_slot3_card7", "Farm");
			PlayerPrefs.SetString ("preset_slot3_card8", "Farm");
			PlayerPrefs.SetString ("preset_slot3_card9", "Farm");
			PlayerPrefs.SetString ("preset_slot3_card10", "Barracks");
			PlayerPrefs.SetString ("preset_slot3_card11", "Barracks");
			PlayerPrefs.SetString ("preset_slot3_card12", "ShieldTower");
			
			PlayerPrefs.SetString ("preset_slot3_card13", "ShieldTower");
			PlayerPrefs.SetString ("preset_slot3_card14", "HydraSpawn");
			PlayerPrefs.SetString ("preset_slot3_card15", "HydraSpawn");
			PlayerPrefs.SetString ("preset_slot3_card16", "GiantTurtle");
			PlayerPrefs.SetString ("preset_slot3_card17", "GiantTurtle");
			PlayerPrefs.SetString ("preset_slot3_card18", "WarTurtle");
			PlayerPrefs.SetString ("preset_slot3_card19", "WarTurtle");
			PlayerPrefs.SetString ("preset_slot3_card20", "Giant");
			PlayerPrefs.SetString ("preset_slot3_card21", "Cannon");
			PlayerPrefs.SetString ("preset_slot3_card22", "Cannon");
			PlayerPrefs.SetString ("preset_slot3_card23", "WarBonds");
			PlayerPrefs.SetString ("preset_slot3_card24", "WarBonds");
		}
		#endregion
	}
}
