using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_CALC_Healing : MonoBehaviour {
	public static MG_CALC_Healing I;
	public void Awake(){ I = this; }

	public void _HP_Regen2(MG_ClassUnit target){
		int toHeal = 0;

		/*Hero regen*/ if (target.isAHero)	toHeal += 5 + 2 * target.hero_str;
		/*Point aura*/ if(MG_ControlBuffs.I._unitHasBuff_returnStack(target, "PointAura") >= 1)  toHeal += 30;
		/*EVE - Witchcraft*/ if (target.name == "Eve")  toHeal += 20;

		target.HP += toHeal;
		if (target.HP > target.HPMax) target.HP = target.HPMax;
		target._updateHealthBar ();

		// Post-heal Additional code
		#region "MASAMUNE - Azure Dragon 2"
		if(target.name == "Masamune"){
			if(target.HP > (int)((double)target.HPMax * 0.5)){
				MG_ControlBuffs.I._removeBuff(target.unitID, "AzureDragon2");
				target._resetSprite();
			}
		}
		#endregion
	}

	public void _MP_Regen2(MG_ClassUnit target){
		int toHeal = 0;

		/*Hero regen*/ if (target.isAHero)	toHeal += target.hero_int / 2;
		/*Drakgul*/ if (target.name == "Drakgul")   toHeal += 20;
		/*Point aura*/ if(MG_ControlBuffs.I._unitHasBuff_returnStack(target, "PointAura") >= 1)  toHeal += 15;

		target.MP += toHeal;
		if (target.MP > target.MPMax) target.MP = target.MPMax;
		target._updateHealthBar ();
	}

	public void _HP_Heal(MG_ClassUnit healer, MG_ClassUnit target, int amount, string healType = "normal"){
		// Conditions
		// Buildings should not heal, unless there's an exception
		if(target.isABuilding){
			return;
		}

		int toHeal = amount;

		// Pre-heal Additional code
		#region "Abel - Test of Faith"
		if(MG_ControlBuffs.I._unitHasBuff_returnStack(target, "TestOfFaith_ally") >= 1){
			toHeal += amount/2;
		}
		#endregion

		target.HP += toHeal;
		if (target.HP > target.HPMax) target.HP = target.HPMax;
		target._updateHealthBar ();

		// Post-heal Additional code
		#region "MASAMUNE - Azure Dragon 2"
		if(target.name == "Masamune"){
			if(target.HP > (int)((double)target.HPMax * 0.5)){
				MG_ControlBuffs.I._removeBuff(target.unitID, "AzureDragon2");
				target._resetSprite();
			}
		}
		#endregion
	}

	public void _MP_Heal(MG_ClassUnit healer, MG_ClassUnit target, int amount, string healType = "normal"){
		int toHeal = amount;

		target.MP += toHeal;
		if (target.MP > target.MPMax) target.MP = target.MPMax;
		target._updateHealthBar ();
	}
}
