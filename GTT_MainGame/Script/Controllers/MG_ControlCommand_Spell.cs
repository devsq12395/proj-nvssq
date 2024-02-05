using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlCommand_Spell : MonoBehaviour {
	public static MG_ControlCommand_Spell I;
	public void Awake(){ I = this; }

	public void castSpell(string spellName, int posX, int posY, int castingPlayer){
		MG_ClassUnit commander = MG_Globals.I.units [0];
		foreach (MG_ClassUnit u in MG_Globals.I.units) {
			if (u.isAHero && u.playerOwner == castingPlayer) {
				commander = u;
				break;
			}
		}

		switch (spellName) {
			#region "Heal"
			case "Heal":
				foreach (MG_ClassUnit u in MG_Globals.I.units) {
					if (!MG_ControlPlayer.I._getIsEnemy (castingPlayer, u.playerOwner)) {
						if (MG_CALC_Distance.I._distBetweenPoints (posX, posY, u.posX, u.posY) <= 2) {
							MG_CALC_Healing.I._HP_Heal (u, u, 200);
						}
					}
				}
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);
			break;
			#endregion
			#region "Area buffs"
			case "Brotherhood":
				foreach (MG_ClassUnit u in MG_Globals.I.units) {
					if (!MG_ControlPlayer.I._getIsEnemy (castingPlayer, u.playerOwner)) {
						if (MG_CALC_Distance.I._distBetweenPoints (posX, posY, u.posX, u.posY) <= 2) {
							MG_ControlBuffs.I._addBuff (u, spellName);
						}
					}
				}
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);
			break;
			#endregion
			#region "Single target buffs"
			case "Veteran":
				if(MG_GetUnit.I._pointHasUnit (posX, posY)){
					MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
					if(!MG_ControlPlayer.I._getIsEnemy (castingPlayer, u.playerOwner)){
						MG_ControlBuffs.I._addBuff (u, spellName);
					}
				}
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);
			break;
			#endregion
			#region "Reinforcements"
			case "Reinforcements":
				MG_ControlUnits.I._createUnit("Rifleman", posX + ((castingPlayer == 1) ? 1 : -1), posY, castingPlayer);
				MG_ControlUnits.I._createUnit("Rifleman", posX, posY + 1, castingPlayer);
				MG_ControlUnits.I._createUnit("Rifleman", posX, posY - 1, castingPlayer);
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);

				// Barracks
				foreach(MG_ClassUnit uBar in MG_Globals.I.units){
					if(uBar.name == "Barracks" && uBar.playerOwner == castingPlayer){
						MG_ControlUnits.I._createUnit("Rifleman", uBar.posX, uBar.posY, castingPlayer);
						MG_ControlSFX.I._createSFX("summonFx", uBar.posX, uBar.posY);
					}
				}
			break;
			#endregion
			#region "ImperialMarch"
			case "ImperialMarch":
				MG_ControlUnits.I._createUnit("ImperialInfantry", posX + ((castingPlayer == 1) ? 1 : -1), posY, castingPlayer);
				MG_ControlUnits.I._createUnit("ImperialInfantry", posX, posY + 1, castingPlayer);
				MG_ControlUnits.I._createUnit("ImperialInfantry", posX, posY - 1, castingPlayer);
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);

				// Barracks
				foreach(MG_ClassUnit uBar in MG_Globals.I.units){
					if(uBar.name == "Barracks" && uBar.playerOwner == castingPlayer){
						MG_ControlUnits.I._createUnit("ImperialInfantry", uBar.posX, uBar.posY, castingPlayer);
						MG_ControlSFX.I._createSFX("summonFx", uBar.posX, uBar.posY);
					}
				}
			break;
			#endregion
			#region "Holy Order"
			case "HolyOrder":
				MG_ControlUnits.I._createUnit("SacredKnight", posX + ((castingPlayer == 1) ? 1 : -1), posY, castingPlayer);
				MG_ControlUnits.I._createUnit("SacredKnight", posX, posY + 1, castingPlayer);
				MG_ControlUnits.I._createUnit("SacredKnight", posX, posY - 1, castingPlayer);
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);

				// Barracks
				foreach(MG_ClassUnit uBar in MG_Globals.I.units){
					if(uBar.name == "Barracks" && uBar.playerOwner == castingPlayer){
						MG_ControlUnits.I._createUnit("SacredKnight", uBar.posX, uBar.posY, castingPlayer);
						MG_ControlSFX.I._createSFX("summonFx", uBar.posX, uBar.posY);
					}
				}
			break;
			#endregion
			#region "Cavalry Charge"
			case "CavalryCharge":
				MG_ControlUnits.I._createUnit("Horseman", posX + ((castingPlayer == 1) ? 1 : -1), posY, castingPlayer);
				MG_ControlUnits.I._createUnit("Horseman", posX, posY + 1, castingPlayer);
				MG_ControlUnits.I._createUnit("Horseman", posX, posY - 1, castingPlayer);
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);

				// Barracks
				foreach(MG_ClassUnit uBar in MG_Globals.I.units){
					if(uBar.name == "Barracks" && uBar.playerOwner == castingPlayer){
						MG_ControlUnits.I._createUnit("Horseman", uBar.posX, uBar.posY, castingPlayer);
						MG_ControlSFX.I._createSFX("summonFx", uBar.posX, uBar.posY);
					}
				}
			break;
			#endregion
			#region "Fireball"
			case "Fireball":
				MG_ControlMissile.I._createMissile("fireballSpell", commander.posX, commander.posY, posX, posY, commander.unitID);
				MG_ControlSounds.I._playSound(31, commander.posX, commander.posY, true);
			break;
			#endregion
			#region "UNUSED SPELLS
			case "Boost":
				foreach (MG_ClassUnit u in MG_Globals.I.units) {
					if (!MG_ControlPlayer.I._getIsEnemy (castingPlayer, u.playerOwner)) {
						if (MG_CALC_Distance.I._distBetweenPoints (posX, posY, u.posX, u.posY) <= 2) {
							MG_ControlBuffs.I._addBuff (u, "Boost");
						}
					}
				}
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);
			break;
			case "MassBlessing":
				foreach (MG_ClassUnit u in MG_Globals.I.units) {
					if (!MG_ControlPlayer.I._getIsEnemy (castingPlayer, u.playerOwner)) {
						if (MG_CALC_Distance.I._distBetweenPoints (posX, posY, u.posX, u.posY) <= 2) {
							MG_ControlBuffs.I._addBuff (u, "Blessing");
						}
					}
				}
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);
			break;
			case "Vision":
				MG_ControlUnits.I._createUnit("WeissDummy", posX, posY, castingPlayer);

				MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], ((castingPlayer == 1) ? "VisionBlue" : "VisionRed"));
				MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
				MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 2;

				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);
			break;
			case "Haste":
				foreach (MG_ClassUnit u in MG_Globals.I.units) {
					if (!MG_ControlPlayer.I._getIsEnemy (castingPlayer, u.playerOwner)) {
						if (MG_CALC_Distance.I._distBetweenPoints (posX, posY, u.posX, u.posY) <= 2) {
							MG_ControlBuffs.I._addBuff (u, "Haste");
						}
					}
				}
				MG_ControlCommand.I._specialEffect_outward (posX, posY, "cartoonHoly01", 3, 0.2, 0.2);
			break;
			#endregion
		}
	}
}
