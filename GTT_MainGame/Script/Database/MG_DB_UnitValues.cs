using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_UnitValues : MonoBehaviour {
	public static MG_DB_UnitValues I;
	public void Awake(){ I = this; }

	// Basic stats
	public int HP, MP, MS, atkRange, sightRadius, atkDamage, goldGain, unitLvl, recGain;
	public double armor, resistance, abilityPower;
	public bool isAHero, isABuilding, canAttack, isInvulnerable, isFlying, isDummy, isSummoned;
	public string classType;
	public int hero_str, hero_int, hero_agi, hero_strGain, hero_intGain, hero_agiGain;
	public bool isSquad;
	
	public int atk_dam;
	public float atk_hitChance, atk_fatalChanceMin, atk_fatalChanceMax;
	public float def_armor, def_cover;

	// Skills
	public string skill1, skill2, skill3, skill4, skill1_title, skill2_title, skill3_title, skill4_title, skill1_desc, skill2_desc, skill3_desc, skill4_desc, skill1_mc, skill2_mc, skill3_mc, skill4_mc, skill1_cd, skill2_cd, skill3_cd, skill4_cd;
	public Sprite icn_skill1, icn_skill2, icn_skill3, icn_skill4;

	// For UI
	public string uiName, uiName_short;

	// Misc
	public float xOffset, yOffset;

	#region "Skill icons"
	public Sprite icn_none, icn_testSkill01, icn_testSkill02, icn_testSkill03;
	/*Robin*/public Sprite icn_hookshot, icn_bladeDance, icn_hookDance;
	/*Ajax*/public Sprite icn_spearToss, icn_holdTheLine, icn_allOutAssault;
	/*Masamune*/public Sprite icn_armorBreak, icn_azureDragon, icn_battleHunger, icn_windSlash;
	/*Amy*/public Sprite icn_scout, icn_multiShot, icn_arcShot;
	/*Alicia*/public Sprite icn_recruit, icn_closeCombat, icn_parasolMusket, icn_bayonet;
	/*Drakgul*/public Sprite icn_eyeOfTheMagi, icn_dispel, icn_concentrate;
	/*Victoria*/public Sprite icn_heal, icn_shell, icn_blessing;
	/*Colt*/public Sprite icn_quickDraw, icn_dynamite, icn_disarmShot;
	/*Spectral Witch*/public Sprite icn_summonSpec, icn_blink, icn_mirror, icn_scream;
	/*Ifreet*/public Sprite icn_burningLeap, icn_volcStomp, icn_burningGrip;
	/*Ragnaros*/public Sprite icn_fury, icn_ragnaros2, icn_brinnande;
	/*Abel*/public Sprite icn_smite, icn_testOfFaith, icn_healingAura;
	/*Siegfried*/public Sprite icn_heavyCharge, icn_warhorn, icn_mythrilLance;
	/*Elder Treant*/public Sprite icn_entangle, icn_rootArmor, icn_fairies;
	/*Yukino*/public Sprite icn_frostNova, icn_frostWave, icn_silence;
	/*Cody*/public Sprite icn_smash, icn_skullCrush, icn_armorbreaker;
	/*Hilde*/public Sprite icn_arcaneBolt, icn_photonBomb, icn_archwizardAura;
	/*Elise*/public Sprite icn_multiStrike, icn_phoenixWings, icn_eskrima;
	/*May*/public Sprite icn_sacredKnights, icn_barrier, icn_honorCode;
	/*Walter*/public Sprite icn_rapidFire, icn_demonicSlash, icn_rampage;
	/*Aleric*/public Sprite icn_axeThrow, icn_mightySlam, icn_intimidate;
	/*Eve*/public Sprite icn_deathArrow, icn_drainLife, icn_witchcraft;
	/*Commander*/public Sprite icn_healHeroes, icn_doubleCard;
	/*Elizabeth*/public Sprite icn_amplify, icn_lightningBolt;

	/*Officer*/public Sprite icn_mythrilRevolver, icn_officersAura;
	/*Medic*/public Sprite icn_potion, icn_healingSpray;
	/*Grenadier*/public Sprite icn_grenade;
	/*Howitzer*/public Sprite icn_artillery;
	/*Cannon*/public Sprite icn_grapeshot, icn_splash;
	/*Giant*/public Sprite icn_demolisher;
	/*Trading Post*/public Sprite icn_tradingPost;
	/*Hydra Spawn*/public Sprite icn_acidBreath, icn_devour;
	/*Giant Turtle*/public Sprite icn_warStomp;
	/*Viking*/public Sprite icn_pillage;
	#endregion

	// Attack data
	public string atkType;

	public Sprite getActionPort (string actionName){
		switch(actionName){
			#region "Robin"
			case "Hookshot": return icn_hookshot; break;
			#endregion
		}

		return icn_none;
	}

	public void _setData(string unitName){
		#region "Defaults"
		HP = 1; MP = 0;
		isInvulnerable = false;
		isFlying = false;
		isDummy = false;
		isSummoned = false;
		isABuilding = false; isSquad = false;
		xOffset = 0; yOffset = 0;
		abilityPower = 1;
		goldGain = 0;
		recGain = 0;
		skill1 = "NONE"; skill2 = "NONE"; skill3 = "NONE"; skill4 = "NONE";
		MS = 6;sightRadius = 6;atkRange = 4;atkDamage = 25;armor = 0.1f;
		hero_str = 0; hero_int = 0; hero_agi = 0; 
		hero_strGain = 0; hero_intGain = 0; hero_agiGain = 0;
		unitLvl = 1;
		
		atk_dam = 1;
		atk_hitChance = 0.25f;
		atk_fatalChanceMin = 0.5f;
		atk_fatalChanceMax = 0.8f;
		def_armor = 1;
		def_cover = 1;
		#endregion

		switch(unitName){
			
			// Camp
			case "Camp":
				// Stats
				HP = 300; MP = 0;
				isAHero = false; classType = ""; isABuilding = true; isInvulnerable = true;
				MS = 4; sightRadius = 4;
				canAttack = true; atkType = "NONE"; atkRange = 6; atkDamage = 70;
				armor = 0f; resistance = 0.1f; abilityPower = 1.5;

				// Skill name (desc and icons at _setSkillData())
				skill1 = "NONE";skill2 = "NONE";skill3 = "NONE";
				
				atk_dam = 0;
				atk_hitChance = 0.2f;
				atk_fatalChanceMin = 0.5f;
				atk_fatalChanceMax = 0.8f;
				def_armor = 1;
				def_cover = 1;

				// UI
				uiName = "Camp";uiName_short = "Camp";
			break;
			
			// Farm
			case "Farm":
				// Stats
				HP = 300; MP = 0;
				isAHero = false; classType = ""; isABuilding = true; isInvulnerable = true;
				MS = 4; sightRadius = 4;
				canAttack = true; atkType = "NONE"; atkRange = 6; atkDamage = 70;
				armor = 0f; resistance = 0.1f; abilityPower = 1.5;

				// Skill name (desc and icons at _setSkillData())
				skill1 = "NONE";skill2 = "NONE";skill3 = "NONE";
				
				atk_dam = 0;
				atk_hitChance = 0.2f;
				atk_fatalChanceMin = 0.5f;
				atk_fatalChanceMax = 0.8f;
				def_armor = 1;
				def_cover = 1;

				// UI
				uiName = "Farm";uiName_short = "Farm";
			break;
			
			// Union/ Infantry
			case "Infantry":
				// Stats
				HP = 300; MP = 0;
				isAHero = false; classType = ""; isABuilding = false; isInvulnerable = false;
				MS = 4; sightRadius = 7;
				canAttack = true; atkType = "Ranged"; atkRange = 6; atkDamage = 70;
				armor = 0.1f; resistance = 0.1f; abilityPower = 1.5;

				// Skill name (desc and icons at _setSkillData())
				skill1 = "FixBayonet";skill2 = "Pin";skill3 = "NONE";
				
				atk_dam = 1;
				atk_hitChance = 0.2f;
				atk_fatalChanceMin = 0.5f;
				atk_fatalChanceMax = 0.8f;
				def_armor = 1;
				def_cover = 1;

				// UI
				uiName = "Union Infantry";uiName_short = "Union Infantry";
			break;
			
			// Union/ Infantry (Bayonet)
			case "InfantryBayonet":
				// Stats
				HP = 300; MP = 0;
				isAHero = false; classType = ""; isABuilding = false; isInvulnerable = false;
				MS = 4; sightRadius = 7;
				canAttack = true; atkType = "Melee"; atkRange = 1; atkDamage = 70;
				armor = 0.1f; resistance = 0.1f; abilityPower = 1.5;

				// Skill name (desc and icons at _setSkillData())
				skill1 = "RemoveBayonet";skill2 = "Pin";skill3 = "NONE";
				
				atk_dam = 1;
				atk_hitChance = 0.35f;
				atk_fatalChanceMin = 0.65f;
				atk_fatalChanceMax = 0.85f;
				def_armor = 1;
				def_cover = 1;

				// UI
				uiName = "Union Infantry";uiName_short = "Union Infantry";
			break;
			
			// Union/ Cavalry
			case "Cavalry":
				// Stats
				HP = 150; MP = 0;
				isAHero = false; classType = ""; isABuilding = false; isInvulnerable = false;
				MS = 6; sightRadius = 8;
				canAttack = true; atkType = "Ranged"; atkRange = 5; atkDamage = 70;
				armor = 0.1f; resistance = 0.1f; abilityPower = 1.5;

				// Skill name (desc and icons at _setSkillData())
				skill1 = "Gallop";skill2 = "PreemptiveStrike";skill3 = "NONE";
				
				atk_dam = 1;
				atk_hitChance = 0.22f;
				atk_fatalChanceMin = 0.6f;
				atk_fatalChanceMax = 0.85f;
				def_armor = 1;
				def_cover = 1;

				// UI
				uiName = "Union Cavalry";uiName_short = "Union Cavalry";
			break;
			
			// Union/ Skirmisher
			case "Skirmisher":
				// Stats
				HP = 150; MP = 0;
				isAHero = false; classType = ""; isABuilding = false; isInvulnerable = false;
				MS = 4; sightRadius = 8;
				canAttack = true; atkType = "Ranged"; atkRange = 8; atkDamage = 70;
				armor = 0.1f; resistance = 0.1f; abilityPower = 1.5;

				// Skill name (desc and icons at _setSkillData())
				skill1 = "Skirmish";skill2 = "PreemptiveStrike";skill3 = "NONE";
				
				atk_dam = 1;
				atk_hitChance = 0.3f;
				atk_fatalChanceMin = 0.65f;
				atk_fatalChanceMax = 0.85f;
				def_armor = 1;
				def_cover = 1;

				// UI
				uiName = "Union Skirmisher";uiName_short = "Union Skirmisher";
			break;
			
			// Union/ Artillery
			case "Artillery":
				// Stats
				HP = 50; MP = 0;
				isAHero = false; classType = ""; isABuilding = false; isInvulnerable = false;
				MS = 4; sightRadius = 8;
				canAttack = true; atkType = "NONE"; atkRange = 8; atkDamage = 70;
				armor = 0.1f; resistance = 0.1f; abilityPower = 1.5;

				// Skill name (desc and icons at _setSkillData())
				skill1 = "RoundShot";skill2 = "ShrapnelShot";skill3 = "GrapeShot";
				
				atk_dam = 1;
				atk_hitChance = 0.3f;
				atk_fatalChanceMin = 0.65f;
				atk_fatalChanceMax = 0.85f;
				def_armor = 1;
				def_cover = 1;

				// UI
				uiName = "Union Artillery";uiName_short = "Union Artillery";
			break;
		}
	}

	public void _setSkillData(string unitName){
		#region "Default"
		// Skill desc and icons
		skill1_title = "";
		skill2_title = "";
		skill3_title = "";
		skill4_title = "";
		skill1_desc = "";
		skill2_desc = "";
		skill3_desc = "";
		skill4_desc = "";
		skill1_mc = "0";
		skill2_mc = "0";
		skill3_mc = "0";
		skill4_mc = "0";
		skill1_cd = "No CD";
		skill2_cd = "No CD";
		skill3_cd = "No CD";
		skill4_cd = "No CD";
		icn_skill1 = icn_none;
		icn_skill2 = icn_none;
		icn_skill3 = icn_none;
		icn_skill4 = icn_none;
		#endregion 

		switch(unitName){
			#region "testUnit001"
			case "testUnit001":
				// Skill desc and icons
				skill1_title = "test";
				skill2_title = "test";
				skill3_title = "test";
				skill1_desc = "testDesc1";
				skill2_desc = "testDesc2";
				skill3_desc = "testDesc3";
				icn_skill1 = icn_testSkill01;
				icn_skill2 = icn_testSkill02;
				icn_skill3 = icn_testSkill03;
			break;
			#endregion
			
		}
	}
}
