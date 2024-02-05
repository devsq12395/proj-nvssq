using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlBuffs : MonoBehaviour {
	public static MG_ControlBuffs I;
	public void Awake(){ I = this; }

	public List<MG_ClassBuff> buffToDestroy;

	public void _start(){
		buffToDestroy = new List<MG_ClassBuff> ();
	}

	#region "Add Buff"
	public void _addBuff(MG_ClassUnit targUnit, string buffType){
		// Conditions
		#region "Buildings/Bosses cannot be stunned"
		if(targUnit.isABuilding || (targUnit.name.Contains("(Boss)") && targUnit.HPMax >= 1000)){
			if(buffType == "Stun"){
				
				return;
			}
		}
		#endregion

		if (_unitHasBuff_returnStack (targUnit, buffType) < 1) {
			targUnit.buffNum++;

			MG_ClassBuff newBuff = new MG_ClassBuff (buffType, targUnit.unitID, targUnit.buffNum);
			targUnit.buffs.Add (newBuff);
		} else {
			MG_ClassBuff targBuff = _getBuff (targUnit, buffType);

			MG_DB_Buffs.I._setData (buffType);
			if (targBuff != null) {
				if (targBuff.stackable) {
					targBuff.stack++;
					if (targBuff.stack > targBuff.stackMax) 			targBuff.stack = targBuff.stackMax;
					targBuff.turnDuration = MG_DB_Buffs.I.duration;
				}
			}
		}
	}
	#endregion

	#region "Get Buff"
	public MG_ClassBuff _getBuff(MG_ClassUnit targUnit, string buffName){
		if (targUnit.buffs.Count <= 0) {
			return null;
		}

		MG_ClassBuff retval = targUnit.buffs [0];
		bool isFound = false;
		foreach (MG_ClassBuff bL in targUnit.buffs) {
			if (bL.type == buffName) {
				retval = bL;
				isFound = true;
				break;
			}
		}
		if (!isFound)
			return null;
		
		return retval;
	}
	#endregion

	#region "DISPEL - Buffs"
	public void _dispel_buffs(MG_ClassUnit targUnit){
		if (_unitHasBuff_returnStack (targUnit, "testBuff1") >= 1)
			_removeBuff (targUnit.unitID, "testBuff1");
		if (_unitHasBuff_returnStack (targUnit, "AllOutAssault") >= 1)
			_removeBuff (targUnit.unitID, "AllOutAssault");
		if (_unitHasBuff_returnStack (targUnit, "HoldTheLinePlus") >= 1)
			_removeBuff (targUnit.unitID, "HoldTheLinePlus");
		if (_unitHasBuff_returnStack (targUnit, "Vigor") >= 1)
			_removeBuff (targUnit.unitID, "Vigor");
		if (_unitHasBuff_returnStack (targUnit, "Mirror") >= 1)
			_removeBuff (targUnit.unitID, "Mirror");
		if (_unitHasBuff_returnStack (targUnit, "Shell") >= 1)
			_removeBuff (targUnit.unitID, "Shell");
		if (_unitHasBuff_returnStack (targUnit, "Blessing") >= 1)
			_removeBuff (targUnit.unitID, "Blessing");
		if (_unitHasBuff_returnStack (targUnit, "ArcShot") >= 1)
			_removeBuff (targUnit.unitID, "ArcShot");
		if (_unitHasBuff_returnStack (targUnit, "RootArmor") >= 1)
			_removeBuff (targUnit.unitID, "RootArmor");
		if (_unitHasBuff_returnStack (targUnit, "Fairies") >= 1)
			_removeBuff (targUnit.unitID, "Fairies");
		if (_unitHasBuff_returnStack (targUnit, "Fury") >= 1)
			_removeBuff (targUnit.unitID, "Fury");
		if (_unitHasBuff_returnStack (targUnit, "Warhorn") >= 1)
			_removeBuff (targUnit.unitID, "Warhorn");
		if (_unitHasBuff_returnStack (targUnit, "TestOfFaith_ally") >= 1)
			_removeBuff (targUnit.unitID, "TestOfFaith_ally");
			if (_unitHasBuff_returnStack (targUnit, "Amplify") >= 1)
			_removeBuff (targUnit.unitID, "Amplify");
	}
	#endregion
	#region "DISPEL - Debuffs"
	public void _dispel_debuffs(MG_ClassUnit targUnit){
		if (_unitHasBuff_returnStack (targUnit, "ArmorBreak") >= 1)
			_removeBuff (targUnit.unitID, "ArmorBreak");
		if (_unitHasBuff_returnStack (targUnit, "Stun") >= 1)
			_removeBuff (targUnit.unitID, "Stun");
		if (_unitHasBuff_returnStack (targUnit, "CloseCombat") >= 1)
			_removeBuff (targUnit.unitID, "CloseCombat");
		if (_unitHasBuff_returnStack (targUnit, "Disarm") >= 1)
			_removeBuff (targUnit.unitID, "Disarm");
		if (_unitHasBuff_returnStack (targUnit, "DisarmShot") >= 1)
			_removeBuff (targUnit.unitID, "DisarmShot");
		if (_unitHasBuff_returnStack (targUnit, "Entangle") >= 1)
			_removeBuff (targUnit.unitID, "Entangle");
		if (_unitHasBuff_returnStack (targUnit, "Bleed") >= 1)
			_removeBuff (targUnit.unitID, "Bleed");
		if (_unitHasBuff_returnStack (targUnit, "TestOfFaith_enemy") >= 1)
			_removeBuff (targUnit.unitID, "TestOfFaith_enemy");
		if (_unitHasBuff_returnStack (targUnit, "Armorbreaker1") >= 1)
			_removeBuff (targUnit.unitID, "Armorbreaker1");
		if (_unitHasBuff_returnStack (targUnit, "Armorbreaker2") >= 1)
			_removeBuff (targUnit.unitID, "Armorbreaker2");
		if (_unitHasBuff_returnStack (targUnit, "Armorbreaker3") >= 1)
			_removeBuff (targUnit.unitID, "Armorbreaker3");
		if (_unitHasBuff_returnStack (targUnit, "PunisherWand1") >= 1)
			_removeBuff (targUnit.unitID, "PunisherWand1");
		if (_unitHasBuff_returnStack (targUnit, "PunisherWand2") >= 1)
			_removeBuff (targUnit.unitID, "PunisherWand2");
		if (_unitHasBuff_returnStack (targUnit, "PunisherWand3") >= 1)
			_removeBuff (targUnit.unitID, "PunisherWand3");
		if (_unitHasBuff_returnStack (targUnit, "OrbofSlow1") >= 1)
			_removeBuff (targUnit.unitID, "OrbofSlow1");
		if (_unitHasBuff_returnStack (targUnit, "OrbofSlow2") >= 1)
			_removeBuff (targUnit.unitID, "OrbofSlow2");
		if (_unitHasBuff_returnStack (targUnit, "FrostWave") >= 1)
			_removeBuff (targUnit.unitID, "FrostWave");
		if (_unitHasBuff_returnStack (targUnit, "Silence") >= 1)
			_removeBuff (targUnit.unitID, "Silence");
		if (_unitHasBuff_returnStack (targUnit, "Armorbreaker") >= 1)
			_removeBuff (targUnit.unitID, "Armorbreaker");
	}
	#endregion

	#region "Remove Stack"
	public void _removeStack(MG_ClassUnit targunit, string buffType, int stackToRemove){
		foreach (MG_ClassBuff bL in targunit.buffs) {
			if (bL.type == buffType) {
				bL.stack -= stackToRemove;
				if (bL.stack <= 0) {
					_removeBuff (targunit.unitID, buffType);
				}
				break;
			}
		}
	}
	#endregion
	#region "Add Stack"
	public void _addStack(MG_ClassUnit targunit, string buffType, int stackToAdd){
		foreach (MG_ClassBuff bL in targunit.buffs) {
			if (bL.type == buffType) {
				bL.stack += stackToAdd;
				if (bL.stack >= bL.stackMax) {
					bL.stack = bL.stackMax;
				}
				break;
			}
		}
	}
	#endregion

	#region "CALCULATE - Bonus damage"
	public int CALC_bonusDamage(MG_ClassUnit targUnit){
		int retVal = 0;

		// Increase
		if(_unitHasBuff_returnStack(targUnit, "AzureDragon") >= 1)
			retVal += 10;
		if(_unitHasBuff_returnStack(targUnit, "ArcShot") >= 1)
			retVal += 15;
		if(_unitHasBuff_returnStack(targUnit, "Fury") >= 1)
			retVal += 15;
		if(_unitHasBuff_returnStack(targUnit, "Rage") >= 1)
			retVal += 5 * _unitHasBuff_returnStack(targUnit, "Rage");
		if(_unitHasBuff_returnStack(targUnit, "BattleHunger") >= 1)
			retVal += 5 * _unitHasBuff_returnStack(targUnit, "BattleHunger");
		if(_unitHasBuff_returnStack(targUnit, "Blessing") >= 1)
			retVal += 10;
		if(_unitHasBuff_returnStack(targUnit, "Vigor") >= 1)
			retVal += 10;
		if(_unitHasBuff_returnStack(targUnit, "Blink") >= 1)
			retVal += 10;
		if(_unitHasBuff_returnStack(targUnit, "Warhorn") >= 1)
			retVal += 5;
		if(_unitHasBuff_returnStack(targUnit, "AllOutAssault") >= 1)
			retVal += 5;
		if(_unitHasBuff_returnStack(targUnit, "AzureDragon2") >= 1)
			retVal += 40;
		if(_unitHasBuff_returnStack(targUnit, "ParasolAura") >= 1)
			retVal += 7;
		if(_unitHasBuff_returnStack(targUnit, "PhoenixWings") >= 1)
			retVal += 2 * _unitHasBuff_returnStack(targUnit, "PhoenixWings");
		if(_unitHasBuff_returnStack(targUnit, "HonorCode") >= 1)
			retVal += 5;
		if(_unitHasBuff_returnStack(targUnit, "Boost") >= 1)
			retVal += 15;
		if(_unitHasBuff_returnStack(targUnit, "OfficersAura") >= 1)
			retVal += 7;
		if(_unitHasBuff_returnStack(targUnit, "Brotherhood") >= 1)
			retVal += 3;
		if(_unitHasBuff_returnStack(targUnit, "Veteran") >= 1)
			retVal += 4;
		if(_unitHasBuff_returnStack(targUnit, "HolyCrusade") >= 1)
			retVal += 5;
		if(_unitHasBuff_returnStack(targUnit, "Amplify") >= 1)
			retVal += 5;

		// Decrease
		if(_unitHasBuff_returnStack(targUnit, "testBuff1") >= 1)
			retVal -= 20 * _unitHasBuff_returnStack(targUnit, "testBuff1");

		return retVal;
	}
	#endregion
	#region "CALCULATE - Bonus ability power"
	public double CALC_bonusAbilityPower(MG_ClassUnit targUnit){
		double retVal = 0;

		// Increase
		if(_unitHasBuff_returnStack(targUnit, "Blessing") >= 1)
			retVal += 0.08;
		if(_unitHasBuff_returnStack(targUnit, "Vigor") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "Warhorn") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "AllOutAssault") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "TestOfFaith_ally") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "PhoenixWings") >= 1)
			retVal += 0.01 * _unitHasBuff_returnStack(targUnit, "PhoenixWings");
		if(_unitHasBuff_returnStack(targUnit, "ArchwizardsAura") >= 1)
			retVal += 0.07;
		if(_unitHasBuff_returnStack(targUnit, "Boost") >= 1)
			retVal += 0.12;

		// Decrease
		if(_unitHasBuff_returnStack(targUnit, "testBuff1") >= 1)
			retVal -= 0.2;

		return retVal;
	}
	#endregion
	#region "CALCULATE - Bonus armor"
	public double CALC_bonusArmor(MG_ClassUnit targUnit){
		double retVal = 0;

		// Increase
		if(_unitHasBuff_returnStack(targUnit, "HoldTheLine") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "HoldTheLinePlus") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "Parasol") >= 1)
			retVal += 0.04 * _unitHasBuff_returnStack(targUnit, "Parasol");
		if(_unitHasBuff_returnStack(targUnit, "AzureDragon2") >= 1)
			retVal += 0.1;
		if(_unitHasBuff_returnStack(targUnit, "RootArmor") >= 1)
			retVal += 0.08;
		if (_unitHasBuff_returnStack (targUnit, "Eskrima") >= 1)
			retVal += 0.03 * ((_unitHasBuff_returnStack (targUnit, "Eskrima") <= 5) ? _unitHasBuff_returnStack (targUnit, "Eskrima") : 5);
		if(_unitHasBuff_returnStack(targUnit, "BlinkStrike") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "HonorCode") >= 1)
			retVal += 0.1;
		if(_unitHasBuff_returnStack(targUnit, "Veteran") >= 1)
			retVal += 0.07;

		// Decrease
		if(_unitHasBuff_returnStack(targUnit, "ArmorBreak") >= 1)
			retVal -= 0.06;
		if(_unitHasBuff_returnStack(targUnit, "AzureDragon") >= 1)
			retVal -= 0.05;
		if(_unitHasBuff_returnStack(targUnit, "WindSlash") >= 1)
			retVal -= 0.1;
		if(_unitHasBuff_returnStack(targUnit, "Scream") >= 1)
			retVal -= 0.05 * _unitHasBuff_returnStack(targUnit, "Scream");
		if(_unitHasBuff_returnStack(targUnit, "Armorbreaker1") >= 1)
			retVal -= 0.1;
		if(_unitHasBuff_returnStack(targUnit, "Armorbreaker2") >= 1)
			retVal -= 0.14;
		if(_unitHasBuff_returnStack(targUnit, "Armorbreaker3") >= 1)
			retVal -= 0.18;
		if(_unitHasBuff_returnStack(targUnit, "Armorbreaker") >= 1)
			retVal -= 0.07;
		if(_unitHasBuff_returnStack(targUnit, "WarlordAura") >= 1)
			retVal -= 0.07;
		if(_unitHasBuff_returnStack(targUnit, "LightningBolt") >= 1)
			retVal -= 0.05;

		return retVal;
	}
	#endregion
	#region "CALCULATE - Bonus resistance"
	public double CALC_bonusResistance(MG_ClassUnit targUnit){
		double retVal = 0;

		// Increase
		if(_unitHasBuff_returnStack(targUnit, "Shell") >= 1)
			retVal += 0.1;
		if(_unitHasBuff_returnStack(targUnit, "HoldTheLine") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "HoldTheLinePlus") >= 1)
			retVal += 0.05;
		if(_unitHasBuff_returnStack(targUnit, "Parasol") >= 1)
			retVal += 0.04 * _unitHasBuff_returnStack(targUnit, "Parasol");
		if(_unitHasBuff_returnStack(targUnit, "AzureDragon2") >= 1)
			retVal += 0.1;
		if(_unitHasBuff_returnStack(targUnit, "RootArmor") >= 1)
			retVal += 0.08;
		if(_unitHasBuff_returnStack(targUnit, "Eskrima") >= 1)
			retVal += 0.03 * ((_unitHasBuff_returnStack(targUnit, "Eskrima") <= 5) ? _unitHasBuff_returnStack(targUnit, "Eskrima") : 5);

		// Decrease
		if(_unitHasBuff_returnStack(targUnit, "TestOfFaith_enemy") >= 1)
			retVal -= 0.07;
		if(_unitHasBuff_returnStack(targUnit, "Scream") >= 1)
			retVal -= 0.05 * _unitHasBuff_returnStack(targUnit, "Scream");
		if(_unitHasBuff_returnStack(targUnit, "PunisherWand1") >= 1)
			retVal -= 0.06;
		if(_unitHasBuff_returnStack(targUnit, "PunisherWand2") >= 1)
			retVal -= 0.12;
		if(_unitHasBuff_returnStack(targUnit, "PunisherWand3") >= 1)
			retVal -= 0.14;
		if(_unitHasBuff_returnStack(targUnit, "Armorbreaker") >= 1)
			retVal -= 0.07;
		if(_unitHasBuff_returnStack(targUnit, "WarlordAura") >= 1)
			retVal -= 0.07;
		if(_unitHasBuff_returnStack(targUnit, "LightningBolt") >= 1)
			retVal -= 0.05;

		return retVal;
	}
	#endregion
	#region "CALCULATE - Bonus MS"
	public int CALC_bonusMS(MG_ClassUnit targUnit){
		int retVal = 0;

		// Increase
		if(_unitHasBuff_returnStack(targUnit, "Blessing") >= 1)
			retVal += 1;
		if(_unitHasBuff_returnStack(targUnit, "Haste") >= 1)
			retVal += 4;

		// Decrease
		if(_unitHasBuff_returnStack(targUnit, "Pin") >= 1)
			retVal -= 2;
		if(_unitHasBuff_returnStack(targUnit, "DisarmShot") >= 1)
			retVal -= 2;
		if(_unitHasBuff_returnStack(targUnit, "Bleed") >= 1)
			retVal -= 1;
		if(_unitHasBuff_returnStack(targUnit, "OrbofSlow1") >= 1)
			retVal -= 1;
		if(_unitHasBuff_returnStack(targUnit, "OrbofSlow2") >= 1)
			retVal -= 2;
		if(_unitHasBuff_returnStack(targUnit, "FrostWave") >= 1)
			retVal -= 2;
		if(_unitHasBuff_returnStack(targUnit, "Stun") >= 1)
			retVal -= 1;
		if(_unitHasBuff_returnStack(targUnit, "AcidBreath") >= 1)
			retVal -= 1;

		return retVal;
	}
	#endregion
	#region "CALCULATE - Bonus Attack Range"
	public int CALC_bonusAtkRange(MG_ClassUnit targUnit){
		int retVal = 0;

		// Increase
		if(_unitHasBuff_returnStack(targUnit, "ArcShot") >= 1)
			retVal += 4;

		// Decrease
		//if(_unitHasBuff_returnStack(targUnit, "testBuff1") >= 1)
		//	retVal -= 0.2;

		return retVal;
	}
	#endregion

	#region "CHECK - Unit has buff - RETURN Stack number"
	public int _unitHasBuff_returnStack(MG_ClassUnit targunit, string buffType){
		int buffStack = -1;

		foreach (MG_ClassBuff bL in targunit.buffs) {
			if (bL.type == buffType) {
				buffStack = bL.stack;
				break;
			}
		}

		return buffStack;
	}
	#endregion

	#region "CHECK - Unit has stun debuff"
	public bool _unitIsStunned(MG_ClassUnit targUnit){
		bool retVal = false;

		if (_unitHasBuff_returnStack (targUnit, "Stun") >= 1) return true;

		return retVal;
	}
	#endregion
	#region "CHECK - Unit cannot attack"
	public bool _unitCannotAttack(MG_ClassUnit targUnit){
		bool retVal = false;

		if (_unitHasBuff_returnStack (targUnit, "DisarmShot") >= 1) return true;

		return retVal;
	}
	#endregion
	#region "CHECK - Unit cannot move"
	public bool _unitCannotMove(MG_ClassUnit targUnit){
		bool retVal = false;

		if (_unitHasBuff_returnStack (targUnit, "Entangle") >= 1) return true;

		return retVal;
	}
	#endregion
	#region "CHECK - Unit cannot skill"
	public bool _unitCannotSkill(MG_ClassUnit targUnit){
		bool retVal = false;

		if (_unitHasBuff_returnStack (targUnit, "Silence") >= 1) return true;
		if (_unitHasBuff_returnStack (targUnit, "AntiMagic") >= 1) return true;

		return retVal;
	}
	#endregion
	#region "CHECK - Unit has disable debuff (LEGACY CODE, CHECKED BY AI, STUN IN GENERAL IS CHECKED HERE)"
	// If returned true, that AI unit will not move
	public bool _unitIsDisabled(MG_ClassUnit targUnit){
		bool retVal = false;

		if (_unitHasBuff_returnStack (targUnit, "Stun") >= 1) return true;

		return retVal;
	}
	#endregion

	#region "END DURATION BUFF EFFECTS"
	public void _endDurationBuffEffect(MG_ClassUnit owner, string buffName){
		Debug.Log (buffName);
		switch (buffName) {
			case "TimedLife":
				owner.isAlive = false;
				MG_ControlUnits.I._addToDestroyList(owner);
			break;
			case "Unleash":
				owner.btn3_orderDef = "cards";
				owner.btn5_orderDef = "unleash";
				owner.MS = 1;
				owner.sightRadius = 3;
			break;
			case "HeroDur":
				MG_ControlSFX.I._createSFX("summonFx", owner.posX, owner.posY);
				MG_ControlSounds.I._playSound(27, owner.posX, owner.posY, true);

				////////////////////////// SWAP UNIT START //////////////////////////
				int newPosX = owner.posX, newPosY = owner.posY,
				newLvl = owner.hero_lvl, newXp = owner.hero_xp, newXpReq = owner.hero_xpReq, playNum = owner.playerOwner;
				double hpPerc = (double)owner.HP / (double)owner.HPMax;
				double mpPerc = (double)owner.MP / (double)owner.MPMax;
				int backupHP = owner.HP, backupMP = owner.MP;
				bool act_move = owner.action_move, act_atk = owner.action_atk, act_skill = owner.action_skill;

				MG_ControlUnits.I._addToDestroyList (owner);
				MG_ControlUnits.I._createUnit ("Commander", newPosX, newPosY, playNum);

				MG_ClassUnit newHero = MG_Globals.I.unitsTemp [MG_Globals.I.unitsTemp.Count-1];
				newHero.hero_lvl = newLvl;
				newHero.hero_xp = newXp;
				newHero.hero_xpReq = newXpReq;
				newHero.setHeroStats ();
				MG_Globals.I.selectedUnit = newHero;
				MG_Globals.I.curCommand = "in map";
				MG_ControlCursor.I._interact ();
				newHero._updateHealthBar ();

				newHero.HP = (int)((double)newHero.HPMax*hpPerc);
				if(newHero.HP <= 1) newHero.HP = backupHP;
				newHero.MP = (int)((double)newHero.MPMax*mpPerc);
				if(newHero.MP <= 1) newHero.MP = backupMP;
				////////////////////////// SWAP UNIT END //////////////////////////
			break;
		}
	}
	#endregion

	#region "Remove Buff"
	/// <summary>
	/// Called when a unit dies to clear out all buffs he possesses.
	/// </summary>
	public void _unitDies(int targetUnitID){
		MG_ClassUnit targ = MG_GetUnit.I._getUnitWithID (targetUnitID);

		foreach(MG_ClassBuff bLoop in targ.buffs){
			_addToDestroyList(bLoop);
		}
	}

	/// <summary>
	/// Removes the buff from the target unit.
	/// </summary>
	public void _removeBuff(int targetUnitID, string buffType){
		MG_ClassUnit targ = MG_GetUnit.I._getUnitWithID (targetUnitID);

		foreach(MG_ClassBuff bLoop in targ.buffs){
			if(bLoop.type == buffType){
				_addToDestroyList(bLoop);
				break;
			}
		}
	}

	public void _addToDestroyList(MG_ClassBuff buff){
		buffToDestroy.Add(buff);
	}
	public void _destroyListed(){
		if(buffToDestroy.Count > 0){
			for(int listedBuff = 0; listedBuff < buffToDestroy.Count; listedBuff++){
				_destroyBuff(listedBuff);
			}
			buffToDestroy.Clear();
		}
	}

	public void _destroyBuff(int targetBuffIndex){
		int indexToRemove = -1;
		if (!MG_GetUnit.I._checkIfUnitWithIDExists (buffToDestroy [targetBuffIndex].ownerID)) {
			return;
		}

		MG_ClassUnit owner = MG_GetUnit.I._getUnitWithID (buffToDestroy [targetBuffIndex].ownerID);

		for(int desUnitLoop = owner.buffs.Count - 1; desUnitLoop >= 0; desUnitLoop--){
			if(buffToDestroy[targetBuffIndex].buffID == owner.buffs[desUnitLoop].buffID){
				indexToRemove = desUnitLoop;
				break;
			}
		}
		if(indexToRemove > -1){
			owner.destroyBuff (indexToRemove);
			owner.buffs.RemoveAt(indexToRemove);
		}
	}

	/// <summary>
	/// Removes all existing buffs of the same buff type.
	/// </summary>
	public void _removeBuffOfType(string buffType){
		List<MG_ClassUnit> combinedUnitList = new List<MG_ClassUnit> ();
		combinedUnitList.AddRange (MG_Globals.I.units);
		combinedUnitList.AddRange (MG_Globals.I.unitsTemp);

		foreach(MG_ClassUnit uLoop in combinedUnitList){
			foreach (MG_ClassBuff bLoop in uLoop.buffs) {
				if(bLoop.type == buffType){
					_addToDestroyList(bLoop);
				}
			}
		}

		for (int i = combinedUnitList.Count - 1; i >= 0; i--) combinedUnitList.RemoveAt (i);
	}
	#endregion
}
