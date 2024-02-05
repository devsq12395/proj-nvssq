using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MG_CALC_Damage : MonoBehaviour {
	public static MG_CALC_Damage I;
	public void Awake(){ I = this; }

	public bool bypassKillCode = false;
	public int lastDamageDealt = 0;

	public bool CHEAT_HIGH_DAMAGE = false;
	
	public void damage_unit_atk (MG_ClassUnit attacker, MG_ClassUnit defender, string damageType = "attack"){
		int _damInt = 0;
		
		float _hitChance = attacker.atk_hitChance;
		float _dam = (float)attacker.atk_dam;
		float _fatalChance_min = attacker.atk_fatalChanceMin;
		float _fatalChance_max = attacker.atk_fatalChanceMax;
		float _armor = defender.def_armor;
		float _cover = defender.def_cover;
		
		#region "Special Codes - Pre-calculate"
		// Preemptive Strike
		if (attacker.name == "ArthurSkirmisher" || attacker.name == "FreeStatesSkirmisher" ||
			attacker.name == "ArthurCavalry" || attacker.name == "FreeStatesSkirmisher") {
				if (MG_Globals.I.curPlayerOnTurn == attacker.playerOwner) {
					_hitChance += 0.075f;
				}
			}
		// Preemptive Strike - Additional defense against return/counter
		if (defender.name == "ArthurSkirmisher" || defender.name == "FreeStatesSkirmisher" ||
			defender.name == "ArthurCavalry" || defender.name == "FreeStatesSkirmisher") {
				if (MG_Globals.I.curPlayerOnTurn == defender.playerOwner) {
					_hitChance -= 0.15f;
				}
			}
		#endregion
		
		_dam = (float)attacker.HP * _hitChance * _dam;
		_dam *= UnityEngine.Random.Range (_fatalChance_min, _fatalChance_max);
		_dam *= _armor;
		_dam *= _cover;
		
		int.TryParse (_dam.ToString ("0"), out _damInt);
		_damageUnit (attacker, defender, _damInt, damageType); 
	}

	/// <summary>
	/// This function automatically detects the 2 unit's owners and deals the damage.
	/// </summary>
	public void _damageUnit(MG_ClassUnit attacker, MG_ClassUnit defender, int damage, string damageType = "attack"){
		//Essential Variables
		bool hasHitter = false, hasHitted = false, canCalcDamage = false;
		int damageDealt = damage;
		double armorToBlock = 0;
		bool BLOCK_WITH_ARMOR = true;
		int bounty = 0;

		if (damageType == "attack" || damageType == "cleave") {
			// armor through normal attacks
			armorToBlock = defender.armor;
		} else if (damageType == "magic") {
			// armor and ability power through magic
			double bonDam = (double)damageDealt * attacker.abilityPower;
			damageDealt = (int)bonDam;
			armorToBlock = defender.resistance;
			BLOCK_WITH_ARMOR = false;
		} else if (damageType == "pure" || damageType == "return") {
			// ignore armor
			armorToBlock = 0;
		}

		#region "Essential Conditions"
		// Unit still exists check
		if (attacker == null)	return;
		if (defender == null)	return;

		// Invulnerable check
		if(defender.isInvulnerable) return;

		// Units are still alive check
		if (!attacker.isAlive || !defender.isAlive) 		return;
		#endregion

		/////////// ARMOR CALC GOES HERE ////////////////////////////////////////////////
		if (CHEAT_HIGH_DAMAGE) 	damageDealt = 9999;

		#region "PRE-DAMAGE EFFECTS"
		// Splash Attack
		switch(attacker.name){
			case "Cannon": case "CannonTower": case "Hellhound": case "FireElemental":
				int CA_damage = 0;
				CA_damage = (int)((double)damageDealt * 0.4);

				foreach(MG_ClassUnit u in MG_Globals.I.units){
					if(MG_ControlPlayer.I._getIsEnemy (attacker.playerOwner, u.playerOwner) && u != defender && damageType == "attack"){
						if(MG_CALC_Distance.I._distUnits(defender, u) <= 2){
							_damageUnit(attacker, u, CA_damage, "cleave");
							MG_ControlSFX.I._createSFX ("hit01", u.posX, u.posY);
							MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY);
						}
					}
				}
			break;
		}
		
		// Infantry - Pin
		if (attacker.name == "ArthurInfantry" || attacker.name == "FreeStatesInfantry") {
			MG_ControlBuffs.I._addBuff (defender, "Pin");
		}
		#endregion

		#region "Calculate Item and Buff Influence"
		// Bonus damage through items and buffs
		if(damageType == "attack"){
			int bonDam = MG_ControlBuffs.I.CALC_bonusDamage(attacker);
			damageDealt += bonDam;
		}else if(damageType == "magic"){
			double bonMagDam = MG_ControlBuffs.I.CALC_bonusAbilityPower(attacker);
			double bonDam = (double)damageDealt * bonMagDam;
			damageDealt += (int)bonDam;
		}
		damageDealt += MG_ControlBuffs.I.CALC_bonusDamage(attacker);

		// Bonus armor through items and buffs
		if (damageType != "pure" && damageType != "return") {
			// Buff influence
			if(BLOCK_WITH_ARMOR){
				armorToBlock += MG_ControlBuffs.I.CALC_bonusArmor(defender);
			}else{
				armorToBlock += MG_ControlBuffs.I.CALC_bonusResistance(defender);
			}
		}
		#endregion
		
		#region "ARMOR CALC"
		damageDealt -= (int)Mathf.Floor((float)damageDealt * (float)armorToBlock);
		#endregion

		#region "Deal the damage"
		if(damageDealt <= 0) damageDealt = 0;
		defender.HP -= damageDealt;
		lastDamageDealt = damageDealt;
		#endregion

		#region "POST-DAMAGE EFFECTS"

		#endregion

		#region "UI"
		// Update health bar
		defender._updateHealthBar();
		if(MG_GetUnit.I._pointHasUnit_GetID(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY) == defender.unitID){
			MG_UIControl_UnitData.I._setUIData(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
		}

		// Damage Indicator
		MG_ControlSFX_TextEffect.I._createComboIndic(damageDealt, defender.sprite.transform.position.x, defender.sprite.transform.position.y);

		/*Shake unit's sprite*/MG_ControlUnits.I._addToShake(defender.unitID, defender.playerOwner);
		#endregion

		#region "IF IT KILLS Codes"
		if(defender.HP <= 0){
			// Before actual killing
			#region "EVENT SYSTEM - Unit Dies"
			MG_ControlEvents.I._cusEvent_UnitDies (defender.unitID);
			#endregion
			#region "Check if kill code should be bypassed"
			if(bypassKillCode){
				_markAsDead(defender);
				bypassKillCode = false;
				defender.HP = 0;
				return;
			}
			#endregion

			#region "CAMP - Mark as game over"
			if(PlayerPrefs.GetInt ("isMultiplayer") == 2){
				if(defender.isAHero){
					MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[2], 6);
				}
			}else{
				if(defender.playerOwner == 1 && defender.isAHero){
					MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[2], 2);
				}else if(defender.playerOwner == 2 && defender.isAHero){
					MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[1], 2);
				}
			}
			#endregion
			
			#region "CAMPAIGN - Add/Remove score"
			if(defender.playerOwner == 2){
				if(defender.isAHero){
					MG_Globals.I.CAMP_score += 250;
				}else{
					MG_Globals.I.CAMP_score += 50;
				}
			}else{
				if(defender.isAHero){
					MG_Globals.I.CAMP_score -= 50;
					MG_Globals.I.camp_curGold -= 100;
				}
			}
			#endregion
				
			// HERO CODES
			if(defender.isAHero){
				// UI - Hero Kill Feed
				MG_UIControl_KillFeed.I._showKill(attacker.name, defender.name);

				#region "KO Effect and other UI"
				MG_ControlSFX.I._createSFX("koEffect", defender.posX, defender.posY);
				#endregion
				#region "Hero kill bounty & Killstreak"
				bounty += 6;
				if(!MG_Globals.I.firstBlood){
					bounty += 3;
					MG_Globals.I.firstBlood = true;
					MG_UIControl_Streak.I.show (0);
				}
				MG_Globals.I.killStreak++;
				if(MG_Globals.I.killStreak >= 2){
					bounty += 3;
					MG_UIControl_Streak.I.show (MG_Globals.I.killStreak - 1);
				}
  				#endregion

				#region "Sound"
				if(defender.playerOwner == MG_Globals.I.curPlayerNum){
					MG_ControlSounds.I._playSound(4, 0, 0, false);
				}else{
					MG_ControlSounds.I._playSound(5, 0, 0, false);
				}
				#endregion

				#region "HERO KILL - Mark as game over"
				if(defender.isAHero){
					if(defender.playerOwner == 1){
						if(PlayerPrefs.GetInt ("isMultiplayer") == 2){
							MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[2], 6);
						}else{
							MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[2], 4);
						}
					}else if(defender.playerOwner == 2){
						if(PlayerPrefs.GetInt ("isMultiplayer") == 2){
							MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[2], 5);
						}else{
							MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[2], 4);
						}
					}
				}
				#endregion
			}
			// NON-HERO CODES
			else{
				#region "Player stats update"
				if(!defender.isDummy) MG_Globals.I.players[attacker.playerOwner].unitKills++;
				#endregion
				#region "Effect and sounds"
				MG_ControlSFX.I._createSFX("moveSmoke", defender.posX, defender.posY);
				MG_ControlSounds.I._playSound(48, defender.posX, defender.posY, true);
				#endregion
				#region "Barracks - Unit limit"
				if(defender.isHired){
					MG_Globals.I.players[defender.playerOwner].unit_lim[defender.hire_indexNum]--;
				}
				#endregion
				#region "Treasure"
				if(defender.name == "Treasure"){
					MG_UIControl_TopBar.I._goldGain (10);
					MG_Globals.I.players[attacker.playerOwner].gold += 10;
					MG_UIControl_TopBar.I._goldUI_update ();

					MG_Globals.I.CAMP_score += 20;
					MG_ControlSounds.I._playSound(7, defender.posX, defender.posY, true);
				}
				#endregion
			}

			#region "Give Bounty"
			if(MG_Globals.I.curPlayerNum == attacker.playerOwner) {
				MG_UIControl_TopBar.I._goldGain (bounty);
				MG_Globals.I.players[attacker.playerOwner].gold += bounty;
				MG_UIControl_TopBar.I._goldUI_update ();
			}
			#endregion
			#region "Gain XP for heroes"
			int heroCount = 0;
			foreach (MG_ClassUnit u in MG_Globals.I.units){
				if (u.isAHero && u.playerOwner == attacker.playerOwner){
					heroCount++;
				}
			}
			
			if (heroCount > 0) {
				int defLvl = (defender.isAHero) ? (defender.hero_lvl + 1) : defender.unitLvl;
				int xpGain = (defLvl * 10) / heroCount;

				foreach (MG_ClassUnit u in MG_Globals.I.units){
					if (u.isAHero && u.playerOwner == attacker.playerOwner){
						u.gainLevel(xpGain);
					}
				}
			}
			#endregion

			// Do the actual killing here
			// Mark as dead and add to destroy list
			// XP gain is inside the function
			_markAsDead(defender);
		}
		
		/*if (defender.isAlive) {
			MG_ControlSFX_TextEffect.I.create_unit_num (attacker);
		}
		if (defender.isAlive) {
			MG_ControlSFX_TextEffect.I.create_unit_num (defender);
		}*/
		#endregion
	}

	/// <summary>
	/// Simple HP loss, this will not kill. (Used for bleed, etc..)
	/// </summary>
	public void _HP_Loss(MG_ClassUnit victim, int amt){
		#region "Deal the damage"
		victim.HP -= amt;
		if(victim.HP <= 0) victim.HP = 1;
		#endregion

		#region "UI"
		// Damage Indicator
		MG_ControlSFX_TextEffect.I._createComboIndic(amt, victim.sprite.transform.position.x, victim.sprite.transform.position.y);
		// Update health bar
		victim._updateHealthBar();
		if(MG_GetUnit.I._pointHasUnit_GetID(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY) == victim.unitID){
			MG_UIControl_UnitData.I._setUIData(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
		}
		#endregion
	}

	#region "Miscellaneous Functions"
	public void _markAsDead(MG_ClassUnit dyingUnit){
		dyingUnit.isAlive = false;
		MG_ControlUnits.I._addToDestroyList(dyingUnit);

		// Gain XP
		if(dyingUnit.playerOwner != MG_Globals.I.curPlayerNum){
			int xpGain = PlayerPrefs.GetInt ("expGained");
			xpGain += 1;
			PlayerPrefs.SetInt ("expGained", xpGain);
		}
	}
	#endregion
}
