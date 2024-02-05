using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Buffs : MonoBehaviour {
	public static MG_DB_Buffs I;
	public void Awake(){ I = this; }

	#region "Sprite"
	public Sprite dummy, testBuff1, testBuff2, testBuff3, pointRed, pointBlue, armorBreak, azureDragon, battleHunger, holdTheLine, arcShot, stun, closeCombat, shell, blessing, disarmShot, mirror, vigor, brinnande, blink
	, testOfFaith_ally, testOfFaith_enemy, healingAura, mythrilLance, warhorn, allOutAssault, holdTheLinePlus, parasol, bleed, windSlash, criticalStrike, entangle, rootArmor, fairies, fury, rage, glovesOfHaste
	, scream, antiMagic, armorbreaker1, armorbreaker2, armorbreaker3, punisherWand1, punisherWand2, punisherWand3, orbOfSlow1, orbofSlow2, frostWave, eskrima, phoenixWings, armorbreaker, archwizardAura, blinkStrike
	, barrier, rampageWalter, honorCode, boost, officersAura, warlordAura, haste, brotherhood, veteran, tradingPost, acidBreath, holyCrusade
	, unleash, heroDur, doubleCard, amplify, quickDraw, lightningBolt
	
	, pin;

	public Sprite _getSprite(string newName){
		Sprite retVal = dummy;

		switch (newName) {
			case "testBuff1": 							retVal = testBuff1; break;
			case "testBuff2": 							retVal = testBuff2; break;
			case "testBuff3": 							retVal = testBuff3; break;
			case "aiControl": 							retVal = testBuff1; break;
			case "PointRed": 							retVal = pointRed; break;
			case "PointBlue": 							retVal = pointBlue; break;
			case "ArmorBreak": 							retVal = armorBreak; break;
			case "AzureDragon": 						retVal = azureDragon; break;
			case "BattleHunger": 
			case "BattleHunger_Indic":					retVal = battleHunger; break;
			case "ArcShot":		 						retVal = arcShot; break;
			case "Stun":		 						retVal = stun; break;
			case "CloseCombat":		 					retVal = closeCombat; break;
			case "Shell":			 					retVal = shell; break;
			case "Blessing":		 					retVal = blessing; break;
			case "DisarmShot":		 					retVal = disarmShot; break;
			case "Mirror":		 						retVal = mirror; break;
			case "Vigor":		 						retVal = vigor; break;
			case "Brinnande":		 					retVal = brinnande; break;
			case "Blink":		 						retVal = blink; break;
			case "TestOfFaith_ally":		 			retVal = testOfFaith_ally; break;
			case "TestOfFaith_enemy":		 			retVal = testOfFaith_enemy; break;
			case "MythrilLance":		 				retVal = mythrilLance; break;
			case "Warhorn":		 						retVal = warhorn; break;
			case "AllOutAssault":		 				retVal = allOutAssault; break;
			case "HoldTheLinePlus":		 				retVal = holdTheLinePlus; break;
			case "Parasol":		 						retVal = parasol; break;
			case "Bleed":		 						retVal = bleed; break;
			case "WindSlash":		 					retVal = windSlash; break;
			case "CriticalStrike":		 				retVal = armorBreak; break;
			case "Entangle":			 				retVal = entangle; break;
			case "RootArmor":			 				retVal = rootArmor; break;
			case "Fairies":				 				retVal = fairies; break;
			case "Fury":				 				retVal = fury; break;
			case "Rage":				 				retVal = rage; break;
			case "GlovesOfHaste":				 		retVal = glovesOfHaste; break;
			case "Scream":						 		retVal = scream; break;
			case "Silence":							 	retVal = antiMagic; break;
			case "Armorbreaker1":						retVal = armorbreaker1; break;
			case "Armorbreaker2":						retVal = armorbreaker2; break;
			case "Armorbreaker3":						retVal = armorbreaker3; break;
			case "PunisherWand1":						retVal = punisherWand1; break;
			case "PunisherWand2":						retVal = punisherWand2; break;
			case "PunisherWand3":						retVal = punisherWand3; break;
			case "OrbofSlow1":							retVal = orbOfSlow1; break;
			case "OrbofSlow2":							retVal = orbofSlow2; break;
			case "FrostWave":							retVal = frostWave; break;
			case "Eskrima":								retVal = eskrima; break;
			case "PhoenixWings":						retVal = phoenixWings; break;
			case "Armorbreaker":						retVal = armorbreaker; break;
			case "ArchwizardsAura":						retVal = archwizardAura; break;
			case "BlinkStrike":							retVal = blinkStrike; break;
			case "BarrierRed":	
			case "BarrierBlue":							retVal = barrier; break;
			case "RampageWalter":						retVal = rampageWalter; break;
			case "HonorCode":							retVal = honorCode; break;
			case "Boost":								retVal = boost; break;
			case "OfficersAura":						retVal = officersAura; break;
			case "WarlordAura":							retVal = warlordAura; break;
			case "Haste":								retVal = haste; break;
			case "Brotherhood":							retVal = brotherhood; break;
			case "Veteran":								retVal = veteran; break;
			case "AcidBreath":							retVal = acidBreath; break;
			case "HolyCrusade":							retVal = holyCrusade; break;
			case "Unleash":								retVal = unleash; break;
			case "HeroDur":								retVal = heroDur; break;
			case "DoubleCard":							retVal = doubleCard; break;
			case "Amplify":								retVal = amplify; break;
			case "QuickDrawAtk":						retVal = quickDraw; break;
			case "LightningBolt":						retVal = lightningBolt; break;
			
			case "Pin":									retVal = pin; break;

			case "HoldTheLine":
			case "HoldTheLine Temp":					retVal = holdTheLine; break;
			case "HealingAura":
			case "HealingAura Temp":					retVal = healingAura; break;
			case "PointAura":
			case "PointAura Temp":						retVal = pointBlue; break;
			case "ParasolAura":
			case "ParasolAura Temp":					retVal = parasol; break;
			case "FairiesAura":
			case "FairiesAura Temp":					retVal = fairies; break;
			case "TradingPostAura":
			case "TradingPostAura Temp":				retVal = tradingPost; break;

			default: retVal = dummy; break;
		}

		return retVal;
	}
	#endregion
	#region "Buff data"
	public bool stackable = false, endTurnDurEffect = false;
	public int stackMax = 0, stackStart = 1;
	public int duration = 0;

	public void _setData(string newName){
		// Defaults:
		stackable = false;
		stackStart = 1;
		stackMax = 1;
		endTurnDurEffect = false;

		switch (newName) {
			case "testBuff1":
				stackable = true;
				stackMax = 10;
				duration = 6;
			break;
			case "aiControl":
				stackable = true;
				stackMax = 10;
				duration = 1;
			break;
			case "PointRed":case "PointBlue": duration = 9999; break;
			case "ArmorBreak":duration = 4; break;
			case "AzureDragon":duration = 2;break;
			case "BattleHunger":case "BattleHunger_Indic":duration = 9999;break;
			case "ArcShot":duration = 1;break;
			case "Stun":duration = 2;break;
			case "CloseCombat":duration = 2;break;
			case "Shell":duration = 2;break;
			case "Blessing":duration = 4;break;
			case "Weiss":case "VisionBlue":case "VisionRed":duration = 9999;break;
			case "DisarmShot":duration = 2;break;
			case "Blink":duration = 2;break;
			case "TestOfFaith_ally":duration = 8;break;
			case "TestOfFaith_enemy":duration = 4;break;
			case "MythrilLance":duration = 1;break;
			case "Warhorn":duration = 2;break;
			case "AllOutAssault":duration = 2;break;
			case "HoldTheLinePlus":duration = 2;break;
			case "Bleed":duration = 2;break;
			case "WindSlash":duration = 2;break;
			case "AzureDragon2":duration = 9999;break;
			case "Entangle":duration = 4;break;
			case "RootArmor":duration = 4;break;
			case "Fury":duration = 2;break;
			case "GlovesOfHaste":duration = 4;break;
			case "Mirror":duration = 6;break;
			case "Fairies":duration = 4;break;
			case "Silence":duration = 2;break;
			case "FrostWave":duration = 2;break;
			case "Armorbreaker1":case "Armorbreaker2":case "Armorbreaker3":duration = 2;break;
			case "PunisherWand1":case "PunisherWand2":case "PunisherWand3":duration = 2;break;
			case "OrbofSlow1":case "OrbofSlow2":duration = 2;break;
			case "Armorbreaker":duration = 2;break;
			case "BlinkStrike":duration = 4;break;
			case "RampageWalter":duration = 1;break;
			case "Boost":duration = 2;break;
			case "Haste":duration = 1;break;
			case "QuickDrawAtk":duration = 1;break;
			case "Brotherhood":duration = 1;break;
			case "Veteran":duration = 9999;break;
			case "AcidBreath":duration = 2;break;
			case "HolyCrusade":duration = 2;break;
			case "DoubleCard":duration = 2;break;
			case "Amplify":duration = 6;break;
			case "LightningBolt":duration = 4;break;
			case "Pin":duration = 2;break;
			case "HeroDur":
				endTurnDurEffect = true;
				duration = 6;
			break;
			case "Unleash":
				endTurnDurEffect = true;
				duration = 1;
			break;
			case "Eskrima":
				stackable = true;
				stackMax = 9999;
				duration = 1;
			break;
			case "PhoenixWings":
				stackable = true;
				stackMax = 15;
				duration = 9999;
			break;
			case "Brinnande":
				stackable = true;
				stackMax = 3;
				duration = 9999;
			break;
			case "Rage":
				stackable = true;
				stackMax = 5;
				duration = 9999;
			break;
			case "Parasol":
				stackable = true;
				stackStart = 3;
				stackMax = 3;
				duration = 9999;
			break;
			case "CriticalStrike":
				stackable = true;
				stackStart = 1;
				stackMax = 3;
				duration = 9999;
			break;
			case "Scream":
				stackable = true;
				stackMax = 3;
				duration = 2;
			break;
			case "BarrierBlue":case "BarrierRed":
				stackable = true;
				stackStart = 1;
				stackMax = 3;
				duration = 2;
			break;

			case "HoldTheLine":case "HoldTheLine Temp":
			case "HealingAura":case "HealingAura Temp":
			case "ParasolAura":case "ParasolAura Temp":
			case "PointAura":case "PointAura Temp":
			case "FairiesAura":case "FairiesAura Temp":
			case "ArchwizardsAura":case "ArchwizardsAura Temp":
			case "HonorCode":case "HonorCode Temp":
			case "OfficersAura":case "OfficersAura Temp":
			case "WarlordAura":case "WarlordAura Temp":
			case "TradingPostAura":case "TradingPostAura Temp":
				duration = 9999;
			break;

			case "TimedLife":
				stackable = false;
				endTurnDurEffect = true;
				stackMax = 1;
				duration = 1000;
			break;
		}
	}
   	#endregion
}
