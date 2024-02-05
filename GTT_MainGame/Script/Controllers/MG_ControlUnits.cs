using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MG_ControlUnits : MonoBehaviour {
	public static MG_ControlUnits I;
	public void Awake(){ I = this; }

	public int unitCnt;
	public List<int> unitsToDestroy;
	
	public List<int> stats;
	public string squad_name, squad_comm;

	public void _start(){
		//Variables
		shakeDur				= new List<float>();
		shakeOwnerID			= new List<int>();
		shakeID					= new List<int>();
		shakePlayer				= new List<int>();
		shakeToRemove			= new List<int>();
		unitHitList				= new List<int>(); // Used by tween
		shakeNum				= 1;
	
		stats = new List<int>();
	}
	
	public void _createUnit_Infantry (string infantryType, int newPosX, int newPosY, int newPlayerOwner) {
		int randNum, antiLoop = 0;
		
		// Creates the unit
		_createUnit (infantryType, newPosX, newPosY, newPlayerOwner);
	}
	
	public void _createUnit_Building (string bldgType, int newPosX, int newPosY, int newPlayerOwner) {
		int randNum, antiLoop = 0;
		
		// Generate Stats
		// Count for these starts at 1:
		int NUMBER_OF_STATS = 5;
		stats.Clear ();
		for (int i = 1; i <= NUMBER_OF_STATS; i++) stats.Add(1);
		
		// Creates the unit
		_createUnit (bldgType, newPosX, newPosY, newPlayerOwner);
	}

	public void _createUnit(string newUnitName, int newPosX, int newPosY, int newPlayerOwner){
		#region "Point is empty check"
		if (MG_GetUnit.I._pointHasUnit (newPosX, newPosY)) {
			Vector2 newPoint = MG_GetUnit.I._getNearbyEmptyTile (newPosX, newPosY);
			newPosX = (int)newPoint.x;
			newPosY = (int)newPoint.y;
		}
		#endregion

		#region "Skin check"
		int skinNum = 0;
		for(int i = 0; i < MG_Globals.I.players [newPlayerOwner].skinOwnerName.Count; i++){
			if(MG_Globals.I.players [newPlayerOwner].skinOwnerName[i] == newUnitName){
				skinNum = MG_Globals.I.players [newPlayerOwner].skins[i];
				break;
			}
		}
		#endregion

		unitCnt++;
		MG_ClassUnit _new = new MG_ClassUnit(newUnitName, unitCnt, newPosX, newPosY, newPlayerOwner, skinNum);
		
		MG_Globals.I.unitsTemp.Add(_new);

		MG_ControlFogOfWar.I._calculateReveal();

		// MG_ControlSFX_TextEffect.I.create_unit_num (_new);

		#region "EXTRAS - Starting Buffs"
		if(newUnitName == "PointBlue" || newUnitName == "PointRed") {
			MG_ControlBuffs.I._addBuff (MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], newUnitName);
		}
		#endregion
		#region "EXTRAS - Check aura for new unit"
		MG_ControlAura.I._moveAura(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1]);
		#endregion
	}
	
	#region "Create Hero Spirit"
	public void createHeroSpirit(int newPosX, int newPosY){
		int randHero = UnityEngine.Random.Range (0, 16);
		string newHero = "";
		switch (randHero) {
			case 0: newHero = "Robin"; break;
			case 1: newHero = "Ajax"; break;
			case 2: newHero = "Colt"; break;
			case 3: newHero = "Victoria"; break;
			case 4: newHero = "Amy"; break;
			case 5: newHero = "Alicia"; break;
			case 6: newHero = "ElderTreant"; break;
			case 7: newHero = "Ragnaros"; break;
			case 8: newHero = "Ifreet"; break;
			case 9: newHero = "SpectralWitch"; break;
			case 10: newHero = "Drakgul"; break;
			case 11: newHero = "Abel"; break;
			case 12: newHero = "Siegfried"; break;
			case 13: newHero = "Yukino"; break;
			case 14: newHero = "Cody"; break;
			case 15: newHero = "Hilde"; break;
			case 16: newHero = "Elise"; break;
		}

		_createUnit (newHero, newPosX, newPosY, 2);
		Color testCol = new Color(0.2f, 0.2f, 0.2f, 0.6f);
		MG_GetUnit.I.getLastCreatedUnit ().sprite.GetComponent<Renderer> ().material.color = testCol;
		MG_GetUnit.I.getLastCreatedUnit ().uiName = "Hero Spirit - " + MG_GetUnit.I.getLastCreatedUnit ().uiName;
	}
	#endregion
	
	public void calculate_cover_all_units (){
		foreach (MG_ClassUnit _u in MG_Globals.I.units) {
			calculate_cover (_u);
		}
	}
	
	public void calculate_cover (MG_ClassUnit _u){
		MG_ClassDoodad _dood = MG_GetDoodad.I.get_doodad (_u.posX, _u.posY);
		
		if (_dood != null) {
			_u.def_cover = _dood.armorBon;
		} else {
			_u.def_cover = 1f;
		}
	}

	#region "SPRITE MANIPULATION - Destroy target GameObject"
	//Destroys a target GameObject
	public void _destroyUIObject(GameObject spriteTarget){
		Destroy(spriteTarget);
	}
	#endregion
	#region "SPRITE MANIPULATION - Health Bar"
	//Create Health Bar
	public GameObject hpBarBase, hpBarBaseHero, hpBarRed, hpBarBlue;
	public GameObject _createHealthBar_Base(int newPosX, int newPosY, int barType){
		if(barType == 2)		// Hero health bar
			return GameObject.Instantiate(hpBarBaseHero, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
		else 					// Default health bar
			return GameObject.Instantiate(hpBarBase, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
	}
	public GameObject _createHealthBar(int newPosX, int newPosY, int playerOwner){
		if(playerOwner == 1){
			return GameObject.Instantiate(hpBarBlue, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-10), Quaternion.identity) as GameObject;
		}else{
			return GameObject.Instantiate(hpBarRed, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-10), Quaternion.identity) as GameObject;
		}
	}
	#endregion
	#region "SPRITE MANIPULATION - Class Icon"
	//Create Health Bar
	public GameObject ci_King, ci_Rook, ci_Knight, ci_Bishop, ci_Pawn, ci_hero;
	public GameObject _createClassIcon(int newPosX, int newPosY, string classIconNum){
		if (classIconNum == "king") {
			return GameObject.Instantiate(ci_King, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
		}else if (classIconNum == "rook") {
			return GameObject.Instantiate(ci_Rook, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
		}else if (classIconNum == "knight") {
			return GameObject.Instantiate(ci_Knight, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
		}else if (classIconNum == "bishop") {
			return GameObject.Instantiate(ci_Bishop, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
		}else if (classIconNum == "hero") {
			return GameObject.Instantiate(ci_hero, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
		}else{
			return GameObject.Instantiate(ci_Pawn, new Vector3((float)newPosX/2, ((float)newPosY+0.7f)/2, newPosY-1), Quaternion.identity) as GameObject;
		}
	}
	#endregion
	#region "SPRITE MANIPULATION - Shake Unit"
	private List<float> shakeDur;
	private List<int> shakeOwnerID;
	private List<int> shakePlayer, shakeID, shakeToRemove;
	private int shakeLoop, shakeNum;

	public void _addToShake(int unitIDToAdd, int playerToAdd){
		shakeDur.Add(0.3f);
		shakeOwnerID.Add(unitIDToAdd);
		shakePlayer.Add(playerToAdd);
		shakeID.Add(shakeNum);
	}

	public void _shakeSprites(float deltaTime){
		shakeLoop++;
		bool shakeIsFound = false;
		MG_ClassUnit tempUnit;
		if(shakeDur.Count > 0){
			for(int sLoop = 0; sLoop < shakeDur.Count; sLoop++){
				shakeDur[sLoop] -= deltaTime;
				if (!MG_GetUnit.I._checkIfUnitWithIDExists (shakeOwnerID[sLoop])) {
					_addToRemoveShake(shakeID[sLoop]);
					continue;
				}

				if(shakeDur[sLoop] < 0){
					tempUnit = MG_GetUnit.I._getUnitWithID(shakeOwnerID[sLoop]);
					if(tempUnit.sprite != null){
						tempUnit._moveUnit (tempUnit.posX, tempUnit.posY, true, true, true, false);
					}
					_addToRemoveShake(shakeID[sLoop]);
				}else{
					tempUnit = MG_GetUnit.I._getUnitWithID(shakeOwnerID[sLoop]);
					if(tempUnit.sprite != null){
						if(shakeLoop <= 3){
							tempUnit._relativeMove(new Vector3(0.025f, 0, 0));
						}else{
							tempUnit._relativeMove(new Vector3(-0.025f, 0, 0));
						}
						if(shakeLoop > 6)
							shakeLoop = 0;
					}else{
						_addToRemoveShake(shakeID[sLoop]);
					}
				}
			}
			_removeShakes();
		}
	}

	private void _addToRemoveShake(int idToRemove){
		if(!shakeToRemove.Contains(idToRemove)){
			shakeToRemove.Add(idToRemove);
		}
	}

	private void _removeShakes(){
		int numToRemove = -1;
		if(shakeToRemove.Count > 0){
			for(int sLoop = shakeToRemove.Count - 1; sLoop >= 0; sLoop--){
				for(int sLoop2 = shakeID.Count - 1; sLoop2 >= 0; sLoop2--){
					if(shakeToRemove[sLoop] == shakeID[sLoop2]){
						numToRemove = sLoop2;
						break;
					}
				}
			}
			shakeToRemove.Clear();
		}
		if(numToRemove > -1){
			shakeDur.RemoveAt(numToRemove);
			shakeOwnerID.RemoveAt(numToRemove);
			shakePlayer.RemoveAt(numToRemove);
			shakeID.RemoveAt(numToRemove);
		}
	}
	#endregion

	#region "UNIT TWEEN - Tween mode and init"
	public List<int> unitHitList;
	int cusVal1 = 0, cusVal2 = 0, cusVal3 = 0;

	public int unitTweenMode = 0;
	// Insert tween mode here
	//	1.	SIEGFRIED - Heavy Charge
	#endregion
	#region "UNIT TWEEN - Update Code"
	public void _unitTween_updateCode(MG_ClassUnit unit){
		switch (unitTweenMode) {
			case 1:
				// SIEGFRIED - Heavy Charge
				unit.cusVal_1--;
				if(unit.cusVal_1 <= 0){
					MG_ControlSFX.I._createSFX_float("moveSmoke", unit.sprite.transform.position.x * 2, unit.sprite.transform.position.y * 2);
					unit.cusVal_1 = 3;
				}
			break;
		}
	}
	#endregion
	#region "UNIT TWEEN - Change Position"
	public void _unitTween_changePositionCode(MG_ClassUnit unit){
		switch (unitTweenMode) {
			case 1:
				// SIEGFRIED - Heavy Charge
				foreach(MG_ClassUnit u in MG_Globals.I.units){
					if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner) && u.isAlive){
						if(unitHitList == null) unitHitList = new List<int>();

						if(MG_CALC_Distance.I._distBetweenPoints(unit.posX, unit.posY, u.posX, u.posY) <= 1 && !unitHitList.Contains(u.unitID)){
							MG_CALC_Damage.I._damageUnit(unit, u, 60, "magic");
							MG_ControlSFX.I._createSFX ("hit01", u.posX, u.posY);
							MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY);
							unitHitList.Add(u.unitID);
						}
					}
				}
			break;
		}
	}
	#endregion
	#region "UNIT TWEEN - Finish Tween"
	public void _unitTween_finishTween(MG_ClassUnit unit){
		switch (unitTweenMode) {
			case 1:
				// SIEGFRIED - Heavy Charge
				// Cause Warhorn
				if(MG_ControlBuffs.I._unitHasBuff_returnStack(unit, "MythrilLance") >= 1){
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner) && u.isAlive){
							if(MG_CALC_Distance.I._distBetweenPoints(unit.posX, unit.posY, u.posX, u.posY) <= 3){
								MG_ControlBuffs.I._addBuff (u, "Warhorn");
							}
						}
					}
				}
				MG_ControlSFX.I._createSFX("warhornEffect", unit.posX, unit.posY);
			break;
		}

		// Defaults:
		unitTweenMode = 0;
		unitHitList.Clear ();
		MG_Globals.I.pause_uiMove = false;
	}
	#endregion

	#region "UNIT LIMIT - Get unit type count"
	public int getUnitTypeCount(string unitName, int player) {
		int count = 0;
		foreach (MG_ClassUnit u in MG_Globals.I.units) {
			if (u.name == unitName && u.playerOwner == player) {
				count++;
			}
		}

		return count;
	}
	#endregion

	#region "Remove unit"
	public void _addToDestroyList(MG_ClassUnit targetUnit){
		if (unitsToDestroy.Contains (targetUnit.unitID))
			return;

		unitsToDestroy.Add(targetUnit.unitID);
		targetUnit.isAlive = false;
		MG_ControlFogOfWar.I._calculateReveal();

		MG_ControlBuffs.I._unitDies (targetUnit.unitID);
		MG_ControlAura.I._checkAuraHasOwner ();
		MG_ControlAura.I._reincludeBuffs (true);
	}

	public void _destroyListed(){
		if(unitsToDestroy.Count > 0){
			for(int listedUnits = 0; listedUnits < unitsToDestroy.Count; listedUnits++){
				_destroyUnit(listedUnits);
			}
			unitsToDestroy.Clear();
		}
	}

	public void _destroyUnit(int targetUnitIndex){
		int indexToRemove = -1;
		for(int desUnitLoop = MG_Globals.I.units.Count - 1; desUnitLoop >= 0; desUnitLoop--){
			if(unitsToDestroy[targetUnitIndex] == MG_Globals.I.units[desUnitLoop].unitID){
				indexToRemove = desUnitLoop;
				break;
			}
		}
		if(indexToRemove > -1){
			Destroy(MG_Globals.I.units[indexToRemove].sprite);
			Destroy(MG_Globals.I.units[indexToRemove].barHP);
			Destroy(MG_Globals.I.units[indexToRemove].barHP_base);
			Destroy(MG_Globals.I.units[indexToRemove].classIcon);
			MG_Globals.I.units.RemoveAt(indexToRemove);
		}
	}
	#endregion

	// Still being used so it's ok to put code here
	#region "End Turn effect for all units"
	public void _endTurnEffect_allUnits() {
		foreach (MG_ClassUnit u in MG_Globals.I.units) {
			// Unit-specific code
			switch (u.name) {
				#region "Point - Check capture status"
				case "PointRed":case "PointBlue":
					// Check capture status
					int CAPTURE_RANGE = 2;
					bool enemyIsClose = false, allyIsClose = false;
					int capturingPlayer = 0;
					if(!u.isAlive){
						continue;
					}
					foreach (MG_ClassUnit u2 in MG_Globals.I.units) {
						if(u == u2) continue;

						if(MG_CALC_Distance.I._distUnits(u, u2) <= CAPTURE_RANGE){
							if(MG_ControlPlayer.I._getIsEnemy(u.playerOwner, u2.playerOwner)){
								if(u2.isDummy) continue;

								enemyIsClose = true;
								capturingPlayer = u2.playerOwner;
							}else{
								allyIsClose = true;
							}
						}
					}
					if(enemyIsClose && !allyIsClose){
						_addToDestroyList(u);
						MG_ControlUnits.I._createUnit((capturingPlayer == 1) ? "PointBlue" : "PointRed", u.posX, u.posY, capturingPlayer);

						if(capturingPlayer == MG_Globals.I.curPlayerNum){
							MG_ControlSounds.I._playSound(10, 0, 0, false);
						}else{
							MG_ControlSounds.I._playSound(11, 0, 0, false);
						}

						// Check if victory
						bool isVictory = true;
						foreach(MG_ClassUnit u2 in MG_Globals.I.units){
							if(!u2.isAlive) continue;

							if((capturingPlayer == 1 && u2.name == "PointRed") || (capturingPlayer == 2 && u2.name == "PointBlue")){
								isVictory = false;
								break;
							}
						}
						if(isVictory){
							if(capturingPlayer == 1){
								MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[1], 1);
							}else{
								MG_UIControl_Popup.I.callVictory(MG_Globals.I.players[2], 1);
							}
						}
					}
				break;
				#endregion
				#region "ALICIA - Parasol Musket"
				case "Alicia":
					MG_ControlBuffs.I._addBuff(u, "Parasol");
					MG_ControlBuffs.I._addStack(u, "Parasol", 4);
				break;
				#endregion
				#region "SHIELD TOWER"
				case "ShieldTower":
					foreach(MG_ClassUnit AHA_u2 in MG_Globals.I.units){
						if(MG_CALC_Distance.I._distUnits(AHA_u2, u) <= 4 && AHA_u2.playerOwner == u.playerOwner){
							MG_ControlBuffs.I._addBuff(AHA_u2, (u.playerOwner == 1) ? "BarrierBlue": "BarrierRed");
							MG_ControlSFX.I._createSFX("cartoonHoly01", AHA_u2.posX, AHA_u2.posY);
						}
					}
				break;
				#endregion
			}

			// Buffs
			#region "ABEL - Healing Aura"
			if(MG_ControlBuffs.I._unitHasBuff_returnStack(u, "HealingAura") >= 1){
				foreach(MG_ClassUnit AHA_u in MG_Globals.I.units){
					if(AHA_u.name == "Abel" && AHA_u.playerOwner == u.playerOwner){
						MG_CALC_Healing.I._HP_Heal (AHA_u, u, 15);
						MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX, u.posY);
						break;
					}
				}
			}
			#endregion
			#region "ELDER TREANT - Fairies"
			if(MG_ControlBuffs.I._unitHasBuff_returnStack(u, "Fairies") >= 1){
				foreach(MG_ClassUnit AHA_u in MG_Globals.I.units){
					if(AHA_u.name == "ElderTreant" && AHA_u.playerOwner == u.playerOwner){
						MG_CALC_Healing.I._HP_Heal (AHA_u, u, 10);
						break;
					}
				}
			}
			#endregion
			#region "RAGNAROS - Rage"
			if(MG_ControlBuffs.I._unitHasBuff_returnStack(u, "Rage") >= 1 && MG_Globals.I.curPlayerOnTurn != u.playerOwner){
				if(u.action_atk){
					MG_ControlBuffs.I._removeBuff (u.unitID, "Rage");
				}
			}
			#endregion

			// Items
			#region "Items"
			if(u.isAHero){
				string item1, item2, item3;
				item1 = MG_Globals.I.players [u.playerOwner]._getHeroItemFromItemSlot (u.name, 0);
				item2 = MG_Globals.I.players [u.playerOwner]._getHeroItemFromItemSlot (u.name, 1);
				item3 = MG_Globals.I.players [u.playerOwner]._getHeroItemFromItemSlot (u.name, 2);
				#region "Gloves of Haste"
				if(item1 == "Gloves of Haste" || item2 == "Gloves of Haste" || item3 == "Gloves of Haste"){
					MG_ControlBuffs.I._addBuff(u, "GlovesOfHaste");
				}
				#endregion
				#region "Arcanium Ring"
				if(item1 == "Arcanium Ring Lvl 2" || item2 == "Arcanium Ring Lvl 2" || item3 == "Arcanium Ring Lvl 2"){
					MG_CALC_Healing.I._MP_Heal(u, u, 50);
				}else if(item1 == "Arcanium Ring Lvl 1" || item2 == "Arcanium Ring Lvl 1" || item3 == "Arcanium Ring Lvl 1"){
					MG_CALC_Healing.I._MP_Heal(u, u, 20);
				}
				#endregion
			}
			#endregion
		}
	}
	#endregion
}
