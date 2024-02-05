using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;

public class MG_ClassUnit
{
	public int posX, posY, playerOwner;
	public string name, spriteName, origSpriteName, classType, facing = "right";
	public bool isAlive, isSupport;
	public int unitID;
	public int killSound;

	// Sprites
	public GameObject sprite;
	public GameObject barHP_base, barHP, classIcon;
	public List<MG_ClassSFX> hpTexts;
	public float xOffset, yOffset;
	public bool showHealthBar = true;

	// Stats:
	public int HP, MP, MS = 10, HPMax, MPMax, MS_DEF, atkDamage, atkDamage_DEF, atkRange, atkRange_DEF, sightRadius, sightRadius_DEF, hire_indexNum = 0;
	public double armor, armor_DEF, resistance, resistance_DEF;
	public double abilityPower = 1, abilityPower_DEF = 1;
	public bool isAHero, isABuilding, isInvulnerable, canAttack, isFlying, isDummy, isSummoned, isHired;
	public string skill1, skill2, skill3;
	public bool isRevealed = false;
	public int goldGain = 0;
	public int unitLvl = 1;
	
	// Mini WW2 new variables
	public List<string> traits;
	public string regName, regCom;
	public int atk_dam;
	public float atk_hitChance, atk_fatalChanceMin, atk_fatalChanceMax;
	public float def_armor, def_cover;

	// Hero Stats:
	public int hero_lvl, hero_xp, hero_xpReq;
	public int hero_str, hero_int, hero_agi, hero_strBase, hero_intBase, hero_agiBase, hero_strGain, hero_intGain, hero_agiGain;

	// Buffs and attachments:
	public List<MG_ClassBuff> buffs;
	public int buffNum;

	// Animation
	public string animation = "idle";

	// Transform system:
	public bool trans_isTransformed = false;
	public int trans_transformDur = 0;

	// Default orders
	public string btn1_orderDef = "move", btn2_orderDef = "attack", btn3_orderDef = "skill", btn4_orderDef = "end turn", btn5_orderDef = "end turn";

	// Attack data 
	public string atkType, atkType_Def, atkClass;

	// Actions per turn
	public bool action_move = true, action_atk = true, action_skill = true, action_card = true;

	// Skill cooldown
	public int cd_skill1 = 0, cd_skill2 = 0, cd_skill3 = 0;

	// For UI
	public string uiName, uiName_short;

	// For AI
	public List<Vector2> waypoints;
	public bool isStationed = false;

	// Misc (Used by special codes
	public bool moveByRevealCalc = true;
	public bool TWEEN_MOVE_canCollide = false;
	public string summonedHeroName;
	public int cusVal_1 = 0, cusVal_2 = 0, cusVal_3 = 0, cusVal_4 = 0;

	public MG_ClassUnit (string newUnitName, int newUnitID, int newPosX, int newPosY, int newPlayerOwner, int skinNum)
	{
		if (newPlayerOwner == 1)		facing = "right";
		else 							facing = "left";

		spriteName = newUnitName;
		updateSkin (skinNum);
		playerOwner = newPlayerOwner;
		posX = newPosX;
		posY = newPosY;
		name = newUnitName;
		unitID = newUnitID;
		string unitNameID = unitID.ToString ();
		sprite.name = newUnitName + unitNameID;
		
		hpTexts = new List<MG_ClassSFX> ();

		// Unit values taken from MG_DB_UnitValues
		MG_DB_UnitValues.I._setData(name);
		HPMax 						= MG_DB_UnitValues.I.HP; HP = HPMax;
		MPMax 						= MG_DB_UnitValues.I.MP; MP = MPMax;
		set_unit ();
		
		// MiniWW2 - Set Squad Info
		

		// Health bars and class icon
		int 				healthBarType = 1;
		if (isAHero) 		healthBarType = 2;
		barHP_base = MG_ControlUnits.I._createHealthBar_Base(posX, posY, healthBarType);
		barHP = MG_ControlUnits.I._createHealthBar(posX, posY, playerOwner);

		if (isAHero)		classType = "hero";
		classIcon = MG_ControlUnits.I._createClassIcon(posX, posY, classType);

		if (isABuilding)
			facing = "NONE";

		// Setup hero stats
		if (isAHero){
			hero_lvl = 1;
			hero_xp = 0;
			hero_xpReq = 25;

			hero_str 				= MG_DB_UnitValues.I.hero_str;
			hero_int 				= MG_DB_UnitValues.I.hero_int;
			hero_agi 				= MG_DB_UnitValues.I.hero_agi;

			hero_strBase = hero_str;
			hero_intBase = hero_int;
			hero_agiBase = hero_agi;

			hero_strGain 			= MG_DB_UnitValues.I.hero_strGain;
			hero_intGain 			= MG_DB_UnitValues.I.hero_intGain;
			hero_agiGain 			= MG_DB_UnitValues.I.hero_agiGain;

			setHeroStats ();
		}

		// Buffs and attachments
		buffs = new List<MG_ClassBuff>();

		isAlive = true;
		sprite.transform.SetParent(GameObject.Find ("_UNITS").transform);

		// Fog of war reveal
		if(MG_Globals.I.curPlayerNum == playerOwner){
			isRevealed = true;
		}

		MG_ControlFogOfWar.I._recalculateRevealForUnit(this);
		_switchSprite ();
		_updateHealthBar ();

		// Misc variables
		waypoints = new List<Vector2>();

		#region "SPECIAL INIT CODES"
		#region "Buildings General"
		if(isABuilding){
			btn1_orderDef = "none";
			btn2_orderDef = "none";
		}
		#endregion
		#region "Towers"
		if(name == "Tower" || name == "CannonTower"){
			btn2_orderDef = "attack";
		}
		#endregion
		#region "HQ"
		if(name == "HQ"){
			btn1_orderDef = "recruit";
			btn2_orderDef = "none";
		}
		#endregion
		#region "Point"
		if(name == "PointBlue" || name == "PointRed"){
			btn1_orderDef = "none";
			btn2_orderDef = "none";
		}
		#endregion
		#region "Camp"
		if(name == "CampBlue" || name == "CampRed"){
			btn1_orderDef = "summon";
			btn2_orderDef = "none";
		}
		#endregion
		#region "Barracks"
		if(name == "BarracksBlue" || name == "BarracksRed"){
			btn1_orderDef = "hire";
			btn2_orderDef = "none";
			btn3_orderDef = "none";
		}
		#endregion
		#region "Spell Tower"
		if(name == "SpellTowerBlue" || name == "SpellTowerRed"){
			btn1_orderDef = "cast";
			btn2_orderDef = "none";
			btn3_orderDef = "none";
		}
		#endregion
		#region "First unit"
		if(name == "testUnit001"){
			btn1_orderDef = "none";
			btn2_orderDef = "none";
			btn3_orderDef = "none";
		}
		#endregion
		#region "HERO - Can use cards"
		if(isAHero){
			btn4_orderDef = "cards";
		}
		#endregion

		#region "Invulnerable - Hide health bar"
		if(isInvulnerable){
			showHealthBar = false;
		}
		#endregion
		#region "Dummy - No commands"
		if(isDummy){
			btn1_orderDef = "none";
			btn2_orderDef = "none";
			btn3_orderDef = "none";
		}
		#endregion
		#region "Eye of the Magi - remove attack"
		if(name == "EyeOfTheMagi"){
			btn2_orderDef = "none";
		}
		#endregion

		#region "SPECTRE - Mirror"
		if(name == "Spectre"){
			MG_ControlBuffs.I._addBuff(this, "Mirror");
		}
		#endregion
		#endregion
	}
	
	public void set_unit (){
		MG_DB_UnitValues.I._setData(name);
		
		MS 							= MG_DB_UnitValues.I.MS; MS_DEF = MS;
		armor_DEF 					= MG_DB_UnitValues.I.armor; armor = armor_DEF;
		atkDamage_DEF 				= MG_DB_UnitValues.I.atkDamage; atkDamage = atkDamage_DEF;
		resistance_DEF 				= MG_DB_UnitValues.I.resistance; resistance = resistance_DEF;
		atkRange_DEF 				= MG_DB_UnitValues.I.atkRange; atkRange = atkRange_DEF;
		sightRadius_DEF 			= MG_DB_UnitValues.I.sightRadius; sightRadius = sightRadius_DEF; 
		isAHero 					= MG_DB_UnitValues.I.isAHero;
		isABuilding 				= MG_DB_UnitValues.I.isABuilding;
		isInvulnerable				= MG_DB_UnitValues.I.isInvulnerable;
		canAttack 					= MG_DB_UnitValues.I.canAttack;
		atkType 					= MG_DB_UnitValues.I.atkType;
		skill1 						= MG_DB_UnitValues.I.skill1;
		skill2 						= MG_DB_UnitValues.I.skill2;
		skill3 						= MG_DB_UnitValues.I.skill3;
		classType 					= MG_DB_UnitValues.I.classType;
			// Class Types: infantry, artillery, vehicle, tank, plane
		uiName 						= MG_DB_UnitValues.I.uiName;
		uiName_short 				= MG_DB_UnitValues.I.uiName_short;
		xOffset 					= MG_DB_UnitValues.I.xOffset;
		yOffset 					= MG_DB_UnitValues.I.yOffset;
		isFlying 					= MG_DB_UnitValues.I.isFlying;
		isDummy 					= MG_DB_UnitValues.I.isDummy;
		isSummoned 					= MG_DB_UnitValues.I.isSummoned;
		abilityPower				= MG_DB_UnitValues.I.abilityPower; abilityPower_DEF = abilityPower;
		goldGain 					= MG_DB_UnitValues.I.goldGain;
		unitLvl						= MG_DB_UnitValues.I.unitLvl;
		
		atk_dam						= MG_DB_UnitValues.I.atk_dam;
		atk_hitChance				= MG_DB_UnitValues.I.atk_hitChance;
		atk_fatalChanceMin			= MG_DB_UnitValues.I.atk_fatalChanceMin;
		atk_fatalChanceMax			= MG_DB_UnitValues.I.atk_fatalChanceMax;
		def_armor					= MG_DB_UnitValues.I.def_armor;
		def_cover					= MG_DB_UnitValues.I.def_cover;
	}

	#region "Ready 1"
	public bool isReady = false;
	public void _ready(){
		if (isReady) 		return;

		isReady = true;

		#region "SPECIAL READY CODES"
		#region "MASAMUNE - Battle Hunger (Unused)"
		if(name == "Masamune"){
			//_MASAMUNE_calcBattleHunger();
		}
		#endregion
		#region "AJAX - Hold the Line aura"
		if(name == "Ajax"){
			MG_ControlAura.I._createAura(this, "HoldTheLine", "HoldTheLine", 3);
		}
		#endregion
		#region "ABEL - Healing aura"
		if(name == "Abel"){
			MG_ControlAura.I._createAura(this, "HealingAura", "HealingAura", 3);
		}
		#endregion
		#region "ALICIA - Parasol and Parasol Aura"
		if(name == "Alicia"){
			MG_ControlBuffs.I._addBuff(this, "Parasol");
			MG_ControlBuffs.I._addStack(this, "Parasol", 4);
			MG_ControlAura.I._createAura(this, "ParasolAura", "ParasolAura", 3);
		}
		#endregion
		#region "ET - Fairies Aura"
		if(name == "ElderTreant"){
			MG_ControlAura.I._createAura(this, "FairiesAura", "FairiesAura", 3);
		}
		#endregion
		#region "POINT - Healing aura"
		if(name == "PointBlue" || name == "PointRed"){
			MG_ControlAura.I._createAura(this, "PointAura", "PointAura", 2);
		}
		#endregion
		#region "HILDE - Archwizard's Aura"
		if(name == "Hilde"){
			MG_ControlAura.I._createAura(this, "ArchwizardsAura", "ArchwizardsAura", 3);
		}
		#endregion
		#region "MAY - Honor Code"
		if(name == "May"){
			MG_ControlAura.I._createAura(this, "HonorCode", "HonorCode", 3);
		}
		#endregion
		#region "OFFICER - Officer's Aura"
		if(name == "Officer"){
			MG_ControlAura.I._createAura(this, "OfficersAura", "OfficersAura", 3);
		}
		#endregion
		#region "ALERIC - Warlord Aura"
		if(name == "Aleric"){
			MG_ControlAura.I._createAura(this, "WarlordAura", "WarlordAura", 3);
		}
		#endregion
		#region "TRADING POST - Aura"
		if(name == "TradingPost"){
			MG_ControlAura.I._createAura(this, "TradingPostAura", "TradingPostAura", 3);
		}
		#endregion
		#endregion
	}
	#endregion

	#region "Update"
	public void _update(float deltaTime){
		_ready ();
	}
	#endregion
	#region "End Turn"
	public void _endTurn(){
		// Refresh actions
		action_move = true;
		action_atk = true;
		action_skill = true;
		action_card = true;

		// Regenerate
		MG_CALC_Healing.I._HP_Regen2(this);	//Default HP regen is 0
		MG_CALC_Healing.I._MP_Regen2(this);	//Default MP regen is 0
		if (cd_skill1 > 0) cd_skill1--;
		if (cd_skill2 > 0) cd_skill2--;
		if (cd_skill3 > 0) cd_skill3--;

		// Buff duration
		foreach(MG_ClassBuff bL in buffs){
			bL.turnDuration--;
			if (bL.turnDuration <= 0) {
				MG_ControlBuffs.I._addToDestroyList (bL);

				if (bL.endTurnDurEffect) {
					MG_ControlBuffs.I._endDurationBuffEffect (this, bL.type);
				}
			}

			#region "ELISE - Phoenix Wings"
			if (name == "Elise"){
				if(bL.type == "PhoenixWings" && bL.stack >= 1){
					bL.stack--;
					if(bL.stack <= 0){
						MG_ControlBuffs.I._addToDestroyList (bL);
					}
				}
			}
			#endregion
		}

		// Transform sprite
		_transformDur();
	}
	#endregion

	// Will not completely destroy the buff
	// To fully destroy a buff, call MG_ControlBuffs.I._addToDestroyList()
	#region "BUFF - Destroy Buff"
	public void destroyBuff(int index){
		this.buffs [index].destroy ();
	}
	#endregion

	#region "HERO - Gain Level"
	public void gainLevel(int newXP) {
		hero_xp += newXP;
		bool lvlUp = false;

		while (hero_xp >= hero_xpReq) {
			hero_xp = hero_xp - hero_xpReq;
			hero_xpReq *= 2;
			hero_lvl++;

			lvlUp = true;
		}

		if (lvlUp) {
			// Lvl up effects here
			setHeroStats ();
			MG_ControlSFX.I._createSFX("summonFx", posX, posY);
			MG_ControlSounds.I._playSound(27, posX, posY, true);
		}
	}
	#endregion
	#region "HERO - Set Hero Stats"
	public void setHeroStats(){
		hero_str = hero_strBase + hero_strGain * hero_lvl;
		hero_int = hero_intBase + hero_intGain * hero_lvl;
		hero_agi = hero_agiBase + hero_agiGain * hero_lvl;

		HPMax = 200 + hero_str * 25;
		HP += 50;
		if (HP > HPMax) 	HP = HPMax;

		MPMax = 50 + hero_int * 5;
		MP += 50;
		if (MP > MPMax) 	MP = MPMax;

		abilityPower = 1 + 0.1 * hero_int;
		atkDamage = 12 + 5 * hero_agi;
	}
	#endregion

	#region "Move Unit/Sprite functions"

	#region "_moveUnit"
	/// <summary>
	/// Use this to register actual unit's movement changing in the map since this should also trigger other events when a unit moves
	/// </summary>
	public void _moveUnit (int movToX, int movToY, bool ignoreCollision = false, bool updateSpritePositionAndHPBar = true, bool ignorePointIsEmpty = false, bool updateFogOfWar = true)
	{
		if (isAlive) {
			if ((!MG_GetUnit.I._pointIsEmpty (movToX, movToY) || ignorePointIsEmpty) && MG_GetUnit.I._pointHasUnit (movToX, movToY) && !ignoreCollision && !isDummy) {
				Vector2 newPoint = MG_GetUnit.I._getNearbyEmptyTile (movToX, movToY);
				posX = (int)newPoint.x;
				posY = (int)newPoint.y;
			} else {
				posX = movToX;
				posY = movToY;
			}
			if(updateFogOfWar) MG_ControlFogOfWar.I._calculateReveal();
			if (updateSpritePositionAndHPBar) {
				_updateSpritePosition ();
				_updateHealthBar ();
			}

			///////////////////////// OTHER /////////////////////////
			MG_ControlAura.I._moveAura(this);
			MG_ControlEvents.I._cusEvent_UnitEntersRegion (this);
			//MG_ControlSFX_TextEffect.I.create_unit_num (this);	
			MG_ControlUnits.I.calculate_cover (this);
//			RuneSystem.I._getRune(unitID, posX, posY);
		}
	}
	#endregion
	#region "_moveSprite"
	/// <summary>
	/// Moves the sprite by (Input Vector3 / 2)
	/// </summary>
	public void _moveSprite (Vector3 movePos)
	{
		if (isAlive) {
			_updateSpritePosition (true, movePos.x, movePos.y);
			_updateHealthBar ();
			//Attachments.I._moveAttachments(this);
		}
	}

	/// <summary>
	/// Moves the sprite by (Current sprite's in-game position + Input Vector3)
	/// </summary>
	public void _relativeMove(Vector3 movePos){
		_updateSpritePosition (true, sprite.transform.position.x + movePos.x, sprite.transform.position.y + movePos.y);
		_updateHealthBar ();
//		Attachments.I._moveAttachments(this);
	}
	#endregion
	#region "Tween Move"
	/// <summary>
	/// Can be used for knockbacks or something
	/// </summary>
	public void _tweenMove(Vector2 endTarget, float speed, bool canCollide = false){
		Vector2 currentPos = (Vector2)sprite.transform.position;
		Vector2 endPos = endTarget;
		Vector2 sPosV2 = currentPos, ePosV2 = endPos;
		float dist = Vector2.Distance (sPosV2, ePosV2);
		float duration = dist / speed;
		int finalPosX = Mathf.RoundToInt(Mathf.Floor((float)endTarget.x*2));
		int finalPosY = Mathf.RoundToInt(Mathf.Floor((float)endTarget.y*2));
		int lastPosX = posX, lastPosY = posY;
		int nextPosX = 0, nextPosY = 0;
		bool collided = false, shakeLeft = false;
		Vector2 lastTValue = new Vector2 (0, 0);

		MG_Globals.I.pause_objMove_dur.Add (duration);
		moveByRevealCalc = false;

		TWEEN_MOVE_canCollide = canCollide;

		//Vector3 camPosOrig = MG_ControlCamera.I.transform.position;
		//MG_ControlCamera.I._moveCamera (posX / 2, posY / 2);
		MG_ControlCamera.I.transform.gameObject.Tween("unitMove#" + unitID, currentPos, endPos, duration, TweenScaleFunctions.Linear, (t) =>
		{
			if(collided){
				t.Stop(TweenStopBehavior.DoNotModify);

				// re-calculate position
				lastPosX = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.x*2));
				lastPosY = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.y*2));

				// Extra code
				MG_ControlUnits.I._unitTween_finishTween(this);

				// completion
				_moveUnit(lastPosX, lastPosY);
				MG_ControlCursor.I._interact();
				moveByRevealCalc = true;
			}else{
				// progress
				if(isRevealed){
					sprite.gameObject.transform.position = t.CurrentValue;
				}else{
					sprite.gameObject.transform.position = new Vector3(t.CurrentValue.x, t.CurrentValue.y, 2000);
				}
				_updateHealthBar();

				// Extra code
				MG_ControlUnits.I._unitTween_updateCode(this);

				// re-calculate position
				nextPosX = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.x*2));
				nextPosY = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.y*2));
				if(nextPosX != lastPosX || nextPosY != lastPosY){
					if(TWEEN_MOVE_canCollide){
						if(!MG_GetUnit.I._pointIsEmpty(nextPosX, nextPosY)){
							nextPosX = lastPosX;
							nextPosY = lastPosY;
							finalPosX = nextPosX;
							finalPosY = nextPosY;
							sprite.gameObject.transform.position = lastTValue;
							collided = true;
						}
					}

					// Extra code - changed position
					MG_ControlUnits.I._unitTween_changePositionCode(this);
				}

				// Actual positional movement on update
				// Will not trigger on end position, end tween should handle that
				if(nextPosX != finalPosX || nextPosY != finalPosY){
					_moveUnit(nextPosX, nextPosY, true, false);
				}

				// re-calculate reveal for this unit
				//if(nextPosX != lastPosX && nextPosY != lastPosY){Debug.Log("asdasd");
				//MG_ControlFogOfWar.I._calculateReveal();
				//}else{Debug.Log("zxczxczxc");
					MG_ControlFogOfWar.I._recalculateRevealForUnit(this);
				//}

				lastPosX = posX;
				lastPosY = posY;
				lastTValue = t.CurrentValue;
			}
		}, (t) =>
		{
			// re-calculate position  - commented out, since _moveUnit() should handle this
			// posX = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.x*2));
			// posY = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.y*2));

			// Finish tween
			MG_ControlUnits.I._unitTween_finishTween(this);

			// completion
			_moveUnit(finalPosX, finalPosY);
			MG_ControlCursor.I._interact();
			moveByRevealCalc = true;
		});

		//MG_ControlCamera.I._moveCamera (camPosOrig.x, camPosOrig.y);
	}
	#endregion

	#endregion

	#region "SPRITE MANIPULATION - Main Sprite"
	// Actual function to be used by all to move the sprite
	// Will not affect the healthbars or attachments
	public void _updateSpritePosition(bool floatMovement = false, float newPosX = 0, float newPosY = 0){
		sprite.transform.position = new Vector3 (0, 0, 0);
		float flyingInc = (isFlying) ? -10 : 0;
		if (floatMovement) {
			sprite.transform.position = new Vector3 (newPosX + xOffset, newPosY + yOffset, (!isRevealed) ? 2000 : newPosY - 2 + flyingInc);
		} else {
			sprite.transform.position = (Vector3)sprite.transform.position + (new Vector3 ((float)posX/2 + xOffset, (float)posY/2 + yOffset, (!isRevealed) ? 2000 : posY-1 + flyingInc));
		}

		// Color modification (used by Hero Sprits)
		if(uiName != null)
		if(uiName.Contains("Hero Spirit")){
			Color testCol = new Color(0.2f, 0.2f, 0.2f, 0.6f);
			sprite.GetComponent<Renderer> ().material.color = testCol;
		}
	}

	public void _switchSprite () {
		// Attachments - prepare
		for (int i = sprite.transform.childCount - 1; i >= 0; i--) {
			sprite.transform.GetChild(i).gameObject.transform.parent = null;
		}

		MG_ControlUnits.I._destroyUIObject(sprite);
		if (isABuilding) {
			sprite = MG_DB_Units.I._getSprite (spriteName + "_p" + playerOwner + "_" + animation);
		} else {
			sprite = MG_DB_Units.I._getSprite (spriteName + "_p" + playerOwner + "_" + animation + "_" + facing);
		}

		_updateSpritePosition ();
		string unitNameID = unitID.ToString ();
		sprite.name = name + unitNameID;

		sprite.transform.SetParent(GameObject.Find ("_UNITS").transform);

		// Attachments - move
		if(buffs != null){
			foreach(MG_ClassBuff buff in buffs){
				buff._updateAttachPosition (this);
			}
		}
	}

	public void _resetSprite () {
		trans_isTransformed = false;
		spriteName = origSpriteName;
		_switchSprite ();
	}

	public void _transformSprite (string newSpriteName, int transTurnDuration) {
		spriteName = newSpriteName;
		trans_transformDur = transTurnDuration;
		trans_isTransformed = true;
		_switchSprite ();
	}
	
	public void transform_unit (string _newUnitName) {
		spriteName = _newUnitName;
		set_unit ();
		_switchSprite ();
	}

	public void _changeFacing (string newFacing) {
		_changeFacing_actual (newFacing);

		/////// In-Game Multiplayer RPC ///////
		if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
			PhotonRoom.room.gameAction_changeFacing (unitID, newFacing);
		}
	}

	public void _changeFacing_actual(string newFacing){
		facing = newFacing;
		if (isABuilding) {
			facing = "NONE";
			_switchSprite ();
		} else {
			_switchSprite ();
		}
	}

	public void _transformDur () {
		//Transform system - reduce duration:
		if (trans_isTransformed) {
			trans_transformDur--;
			if (trans_transformDur <= 0) {
				_resetSprite ();
			}
		}
	}
	#endregion
	#region "SPRITE MANIPULATION - Health Bar"
	public void _updateHealthBar(){
		float adjustPercentage;

		if (showHealthBar) {
			float hpBarPerc = ((float)HP / (float)HPMax);
			adjustPercentage = ((float)HP/(float)HPMax) * 0.1f;
			
			if (adjustPercentage < 0) adjustPercentage = 0.001f;
			if(classIcon != null) classIcon.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y + 0.45f, (!isRevealed) ? 2000 : sprite.transform.position.y-23);
			if (isAHero) {
				barHP.transform.localScale = new Vector3 (adjustPercentage, 0.1f, 1);
				barHP.transform.position = new Vector3 (sprite.transform.position.x - (1 - hpBarPerc) * 0.23f, sprite.transform.position.y + 0.45f, (!isRevealed) ? 2000 : sprite.transform.position.y - 22);
				barHP_base.transform.position = new Vector3 (sprite.transform.position.x, sprite.transform.position.y + 0.45f, (!isRevealed) ? 2000 : sprite.transform.position.y - 21);
			} else {
				barHP.transform.localScale = new Vector3 (adjustPercentage, 0.0543f, 1);
				barHP.transform.position = new Vector3 (sprite.transform.position.x - (1 - hpBarPerc) * 0.23f, sprite.transform.position.y + 0.35f, (!isRevealed) ? 2000 : sprite.transform.position.y - 22);
				barHP_base.transform.localScale = new Vector3 (0.1f, 0.0537f, 1);
				barHP_base.transform.position = new Vector3 (sprite.transform.position.x, sprite.transform.position.y + 0.35f, (!isRevealed) ? 2000 : sprite.transform.position.y - 21);
			}
		} else {
			if(classIcon != null) classIcon.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y + 0.45f, 2000);
			barHP.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y + 0.35f, 2000);
			barHP_base.transform.position = new Vector3(sprite.transform.position.x, sprite.transform.position.y + 0.35f, 2000);
		}
	}
	#endregion
	#region "SPRITE MANIPULATOR - Update Skin"
	public void updateSkin (int skinNum){
		if (skinNum != 0) {
			spriteName += skinNum.ToString ();
		}
		if (sprite != null) {
			MG_ControlUnits.I._destroyUIObject(sprite);
		}

		sprite = MG_DB_Units.I._getSprite (spriteName + "_p" + playerOwner + "_" + animation + ( (facing == "NONE") ? "" : ("_" + facing) ));
		origSpriteName = spriteName;
		_resetSprite ();
	}
	#endregion

	#region "FOG OF WAR - Reveal/Unreveal (Terrain Only)"
	public void _reveal(){
		isRevealed = true;
		_updateSpritePosition ();
		_updateHealthBar ();
	}

	public void _unreveal(){
		isRevealed = false;
		_updateSpritePosition ();
		_updateHealthBar ();
	}
	#endregion

	// MISC UNIT CODES

}
