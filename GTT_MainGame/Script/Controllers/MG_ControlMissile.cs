using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlMissile : MonoBehaviour {
	public static MG_ControlMissile I;
	public void Awake(){ I = this; }

	private int misCnt;
	public List<int> missilesToDestroy;

	public void _createMissile(string newMissileName, int newPosX, int newPosY, int targetPosX, int targetPosY, int unitOwnerId){
		misCnt++;
		MG_Globals.I.missilesTemp.Add(new MG_ClassMissile(newMissileName, unitOwnerId, newPosX, newPosY, targetPosX, targetPosY, misCnt));
	}

	#region "On Missile Path"
	////////////////////////////////// 
	/// This function will not be called on the missile's first frame
	////////////////////////////////// 
	public void _update_missilePath(MG_ClassMissile missile){
		if (!MG_GetUnit.I._checkIfUnitWithIDExists (missile.ownerId)) {
			return;
		}
		MG_ClassUnit owner = MG_GetUnit.I._getUnitWithID (missile.ownerId);

		switch (missile.type) {
			#region "Spear Toss"
			case "speartoss":
				missile.cusVal1--;
				if(missile.cusVal1 <= 0){
					//MG_ControlSFX.I._createSFX_float("RD_MHwater", missile.sprite.transform.position.x, missile.sprite.transform.position.y);
					missile.cusVal1 = 3;
				}
				foreach(MG_ClassUnit u in MG_Globals.I.units){
					if(MG_ControlPlayer.I._getIsEnemy(owner.playerOwner, u.playerOwner) && u.isAlive){
						if(missile.unitHitList == null) missile.unitHitList = new List<int>();

						if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 1 && !missile.unitHitList.Contains(u.unitID)){
							MG_CALC_Damage.I._damageUnit(owner, u, 20, "magic");
							MG_ControlSFX.I._createSFX("cartoonHit03", u.posX, u.posY);
							missile.unitHitList.Add(u.unitID);
						}
					}
				}
			break;
			#endregion
			#region "Wind Slash"
			case "WindSlash":
				missile.cusVal1--;
				if(missile.cusVal1 <= 0){
					MG_ControlSFX.I._createSFX_float("moveSmoke", missile.sprite.transform.position.x * 2, missile.sprite.transform.position.y * 2);
					missile.cusVal1 = 3;
				}
				foreach(MG_ClassUnit u in MG_Globals.I.units){
					if(MG_ControlPlayer.I._getIsEnemy(owner.playerOwner, u.playerOwner) && u.isAlive){
						if(missile.unitHitList == null) missile.unitHitList = new List<int>();

						if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 1 && !missile.unitHitList.Contains(u.unitID)){
							MG_CALC_Damage.I._damageUnit(owner, u, 70, "magic");
							MG_ControlSFX.I._createSFX("cartoonHit03", u.posX, u.posY);
							MG_ControlBuffs.I._addBuff (u, "Bleed");
							MG_ControlBuffs.I._addBuff (u, "WindSlash");
							missile.unitHitList.Add(u.unitID);
						}
					}
				}
			break;
			#endregion
			#region "Axe Throw/Axe Throw (Boss)"
			case "AxeThrow":case "AxeThrow(Boss)":
				missile.cusVal1--;
				if(missile.cusVal1 <= 0){
					//MG_ControlSFX.I._createSFX_float("RD_MHwater", missile.sprite.transform.position.x, missile.sprite.transform.position.y);
					missile.cusVal1 = 3;
				}
				foreach(MG_ClassUnit u in MG_Globals.I.units){
					if(MG_ControlPlayer.I._getIsEnemy(owner.playerOwner, u.playerOwner) && u.isAlive){
						if(missile.unitHitList == null) missile.unitHitList = new List<int>();

						if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 1 && !missile.unitHitList.Contains(u.unitID)){
							MG_CALC_Damage.I._damageUnit(owner, u, (u.isAHero) ? 50 : 80, "magic");
							MG_ControlSFX.I._createSFX("cartoonHit03", u.posX, u.posY);
							missile.unitHitList.Add(u.unitID);
						}
					}
				}
			break;
			#endregion
			#region "AcidBreath"
			case "AcidBreath":
				foreach(MG_ClassUnit u in MG_Globals.I.units){
					if(MG_ControlPlayer.I._getIsEnemy(owner.playerOwner, u.playerOwner) && u.isAlive){
						if(missile.unitHitList == null) missile.unitHitList = new List<int>();

						if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 1 && !missile.unitHitList.Contains(u.unitID)){
							MG_CALC_Damage.I._damageUnit(owner, u, 20, "magic");
							MG_ControlSFX.I._createSFX("cartoonHit03", u.posX, u.posY);
							MG_ControlBuffs.I._addBuff (u, "AcidBreath");
							missile.unitHitList.Add(u.unitID);
						}
					}
				}
			break;
			#endregion
		}
	}
	#endregion

	#region "On Missile Hit"
	public void _update_missileHit(MG_ClassMissile missile){
		#region "Defaults"
		// Check for enemy on target area, to be stored on "targUnit", "hasTargUnit" is true if unit exists
		bool hasTargUnit = false;
		MG_ClassUnit targUnit = MG_Globals.I.units[0];
		if (MG_GetUnit.I._pointHasUnit (missile.targetX, missile.targetY)) {
			targUnit = MG_GetUnit.I._pickUnit (missile.targetX, missile.targetY);
			hasTargUnit = true;
		}

		// Check if missile owner is still alive, "hasOwner" is false if not, this will NOT return the function if false
		bool hasOwner = false;
		MG_ClassUnit missileOwner = MG_Globals.I.units[0];
		if(MG_GetUnit.I._checkIfUnitWithIDExists(missile.ownerId)){
			hasOwner = true;
			missileOwner = MG_GetUnit.I._getUnitWithID(missile.ownerId);
		}
		#endregion

		switch (missile.type) {
			#region "testMissile"
			case "testMissile":
				MG_ControlSFX.I._createSFX ("testSFX", missile.targetX, missile.targetY);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_ControlBuffs.I._addBuff (targUnit, "testBuff1");
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, 25);
					}
				}
			break;
			#endregion
			#region "hookshot"
			case "hookshot":
				if (hasOwner) {
					missileOwner._moveUnit(missile.targetX, missile.targetY);
					MG_ControlSFX.I._createSFX("moveSmoke", missile.targetX, missile.targetY);
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_ControlSounds.I._playSound(15, missile.posX, missile.posY, true);
				}
			break;
			#endregion
			#region "hookdance"
			case "hookdance":
				bool HD_bonDam = false;

				if (hasOwner) {
					if(MG_CALC_Distance.I._distBetweenPoints(missile.targetX, missile.targetY, missileOwner.posX, missileOwner.posY) >= 4){
						HD_bonDam = true;
					}

					missileOwner._moveUnit(missile.targetX, missile.targetY);
					MG_ControlSFX.I._createSFX("moveSmoke", missile.targetX, missile.targetY);

					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(missileOwner.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(missileOwner, u) <= 2){
								MG_CALC_Damage.I._damageUnit(missileOwner, u, (HD_bonDam) ? 25 : 20, "magic");
								MG_ControlSFX.I._createSFX ("hit01", u.posX, u.posY);
								MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY); 
							}
						}
					}
					for(int i = 1; i <= 7; i++){
						MG_ControlSFX.I._createTimer(1, 0.15f * i, missileOwner.posX, missileOwner.posY);
						MG_ControlSFX.I._createTimer(1, 0.15f * i, missileOwner.posX, missileOwner.posY);
						MG_ControlSFX.I._createTimer(1, 0.15f * i, missileOwner.posX, missileOwner.posY);
						MG_ControlSFX.I._createTimer(1, 0.15f * i, missileOwner.posX, missileOwner.posY);
					}
					MG_ControlSounds.I._playSound(15, missile.posX, missile.posY, true);
					MG_ControlSounds.I._playSound(30, missile.posX, missile.posY, true);

					MG_ControlCursor.I._interact (); // Change selected unit and update UI
				}
			break;
				#endregion
			#region "multishot"
			case "multishotMain":
				if (hasOwner) {
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(missileOwner.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 2){
								MG_CALC_Damage.I._damageUnit(missileOwner, u, 75, "magic");
								MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY);
							}
						}
					}
					MG_ControlSounds.I._playSound(34, missile.posX, missile.posY, true);

					MG_ControlCursor.I._interact (); // Change selected unit and update UI
				}
			break;
			#endregion
			#region "grapeshot"
			case "grapeshotMain":
				if (hasOwner) {
					MG_ControlSFX.I._createSFX ("cartoonExplodeFire01", missile.posX, missile.posY);
					MG_ControlSFX.I._createSFX ("slam01", missile.posX, missile.posY);
					MG_ControlSFX.I._createSFX ("slam02", missile.posX, missile.posY);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(missileOwner.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 1){
								MG_CALC_Damage.I._damageUnit(missileOwner, u, 25, "magic");
								MG_ControlSFX.I._createSFX ("cartoonExplodeFire01", u.posX, u.posY);
								MG_ControlSFX.I._createSFX ("slam01", u.posX, u.posY);
								MG_ControlSFX.I._createSFX ("slam02", u.posX, u.posY);
							}
						}
					}
					MG_ControlSounds.I._playSound(51, missile.posX, missile.posY, true);

					MG_ControlCursor.I._interact (); // Change selected unit and update UI
				}
			break;
			#endregion
			#region "fireballSpell"
			case "fireballSpell":
				if (hasOwner) {
					MG_ControlSFX.I._createSFX ("cartoonExplodeFire01", missile.posX, missile.posY);
					MG_ControlSFX.I._createSFX ("slam01", missile.posX, missile.posY);
					MG_ControlSFX.I._createSFX ("slam02", missile.posX, missile.posY);
					MG_ControlCommand.I._specialEffect_outward(missile.posX, missile.posY, "cartoonHit04", 2, 0.2, 0.2);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(missileOwner.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 2){
								MG_CALC_Damage.I._damageUnit(missileOwner, u, 35, "magic");
							}
						}
					}
					MG_ControlSounds.I._playSound(51, missile.posX, missile.posY, true);

					MG_ControlCursor.I._interact (); // Change selected unit and update UI
				}
			break;
			#endregion
			#region "Quick Draw"
			case "QuickDrawMain":case "QuickDraw":
				MG_ControlSFX.I._createSFX ("cartoonHit03", missile.targetX, missile.targetY);

				if(missile.type == "QuickDrawMain") {
					MG_ControlSFX.I._createSFX ("slam01", missile.targetX, missile.targetY);
					MG_ControlSounds.I._playSound(36, missile.posX, missile.posY, true);
					if (hasTargUnit) {
						if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
							MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, 40, "magic");
						}
					}
				}
			break;
			#endregion
			#region "Mythril Revolver"
			case "MythrilRevolver":
				MG_ControlSFX.I._createSFX ("cartoonHit03", missile.targetX, missile.targetY);

				MG_ControlSounds.I._playSound(35, missile.posX, missile.posY, true);
				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, 25, "magic");
					}
				}
			break;
			#endregion
			#region "Dynamite"
			case "Dynamite":
				MG_ControlCommand.I._specialEffect_outward(missile.posX, missile.posY, "cartoonHit04", 2, 0.2, 0.2);
				MG_ControlSFX.I._createSFX ("explodeDust01", missile.posX, missile.posY);
				MG_ControlSounds.I._playSound(18, missile.posX, missile.posY, true);

				if (hasOwner) {
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(missileOwner.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 2){
								MG_CALC_Damage.I._damageUnit(missileOwner, u, 20, "magic");
							}
						}
					}

					MG_ControlCursor.I._interact (); // Change selected unit and update UI
				}
			break;
			#endregion
			#region "Disarm Shot"
			case "DisarmShot":
				MG_ControlSFX.I._createSFX ("cartoonHit03", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(35, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, 15, "magic");
						MG_ControlBuffs.I._addBuff(targUnit, "DisarmShot");
					}
				}
			break;
			#endregion
			#region "Lightning Bolt"
			case "LightningBolt":
				MG_ControlSFX.I._createSFX ("cartoonHit03", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(36, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, 15, "magic");
						MG_ControlBuffs.I._addBuff(targUnit, "LightningBolt");
					}
				}
			break;
			#endregion
			#region "Arcane Bolt"
			case "ArcaneBolt":
				MG_ControlSFX.I._createSFX ("cartoonDeathBall", missile.targetX, missile.targetY);
				MG_ControlSFX.I._createSFX ("explodeDust01", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(36, missile.posX, missile.posY, true);
				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, 85, "magic");
					}
				}
			break;
			#endregion
			#region "Burst Fire"
			case "BurstFireMain":
				MG_ControlSFX.I._createSFX ("cartoonHit03", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(35, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, (MG_ControlBuffs.I._unitHasBuff_returnStack (missileOwner, "RampageWalter") >= 1) ? 60 : 30, "magic");
					}
				}
			break;
			#endregion
			#region "Death Arrow"
			case "DeathArrow":
				MG_ControlSFX.I._createSFX ("deathArrowHit", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(45, missile.posX, missile.posY, true);
				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, 110, "magic");
					}
				}
			break;
			#endregion
			#region "Drain Life"
			case "DrainLife":
				MG_ControlSFX.I._createSFX ("drainLifeHit", missile.targetX, missile.targetY);
				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Healing.I._HP_Heal (missileOwner, missileOwner, MG_CALC_Damage.I.lastDamageDealt);
					}
				}
			break;
			#endregion
			#region "Grenade"
			case "Grenade":
				MG_ControlSFX.I._createSFX ("cartoonHit04", missile.posX, missile.posY-1);
				MG_ControlSFX.I._createSFX ("cartoonHit04", missile.posX-1, missile.posY);
				MG_ControlSFX.I._createSFX ("cartoonHit04", missile.posX, missile.posY);
				MG_ControlSFX.I._createSFX ("cartoonHit04", missile.posX+1, missile.posY);
				MG_ControlSFX.I._createSFX ("cartoonHit04", missile.posX, missile.posY+1);
				MG_ControlSounds.I._playSound(18, missile.posX, missile.posY, true);

				if (hasOwner) {
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(missileOwner.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distBetweenPoints(missile.posX, missile.posY, u.posX, u.posY) <= 1){
								MG_CALC_Damage.I._damageUnit(missileOwner, u, 45, "magic");
							}
						}
					}

					MG_ControlCursor.I._interact (); // Change selected unit and update UI
				}
			break;
			#endregion

			#region "NORMAL - Amy"
			case "Amy":
				MG_ControlSFX.I._createSFX ("cartoonHit01", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(34, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, missileOwner.atkDamage);
					}
				}
			break;
			#endregion
			#region "NORMAL - ArcGun01"
			case "ArcGun01":
				MG_ControlSFX.I._createSFX ("cartoonHit01", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(35, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I.damage_unit_atk (missileOwner, targUnit, "arcgun");
					}
				}
			break;
			#endregion
			#region "NORMAL - Victoria"
			case "clericMissile":
				MG_ControlSFX.I._createSFX ("cartoonHit01", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(36, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, missileOwner.atkDamage);
					}
				}
			break;
			#endregion
			#region "NORMAL - Colt, Walter"
			case "coltMissile":case "WalterMain":
				MG_ControlSFX.I._createSFX ("cartoonHit01", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(35, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, missileOwner.atkDamage);
					}
				}
			break;
			#endregion
			#region "NORMAL - Spectral Witch and Spectre"
			case "spectreMissile":
				MG_ControlSFX.I._createSFX ("cartoonDeathBall", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(36, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, missileOwner.atkDamage);
					}
				}
			break;
			#endregion
			#region "NORMAL - Tower"
			case "Tower":
				MG_ControlSFX.I._createSFX ("cartoonHit01", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(34, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, missileOwner.atkDamage);
					}
				}
			break;
			#endregion
			#region "NORMAL - Cannon"
			case "Cannon":
				MG_ControlSFX.I._createSFX ("cartoonExplodeFire01", missile.targetX, missile.targetY);
				MG_ControlSFX.I._createSFX ("slam01", missile.targetX, missile.targetY);
				MG_ControlSFX.I._createSFX ("slam02", missile.targetX, missile.targetY);
				MG_ControlSounds.I._playSound(51, missile.posX, missile.posY, true);

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (missileOwner.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (missileOwner, targUnit, missileOwner.atkDamage);
					}
				}
			break;
			#endregion
		}

		#region "Default missile effect"
		switch(missile.type){
			case "multishotMain":case "multishot":
				MG_ControlSFX.I._createSFX ("cartoonHit03", missile.posX, missile.posY);
			break;
		}
		#endregion
		#region "End Defaults"
		_addToDestroyList(missile);
		#endregion
	}
	#endregion

	#region "On Instant Move"
	public void _onInstantMove(MG_ClassMissile missile){
		if (!MG_GetUnit.I._checkIfUnitWithIDExists (missile.ownerId)) {
			return;
		}
		MG_ClassUnit owner = MG_GetUnit.I._getUnitWithID (missile.ownerId);
		float speed = 0.25f;

		switch (missile.type) {
			#region "Spectre 1"
			case "Spectre1":
				speed = 0.25f;
				while(missile.targetDistance_half > 0){
					missile.targetDistance_half -= speed;
					float nextPosX = missile.sprite.transform.position.x + speed * Mathf.Cos((missile.angle * Mathf.PI) / 180);
					float nextPosY = missile.sprite.transform.position.y + speed * Mathf.Sin((missile.angle * Mathf.PI) / 180);
					missile.sprite.transform.position = new Vector2(nextPosX, nextPosY);

					missile.posX = Mathf.RoundToInt(Mathf.Floor(nextPosX*2));
					missile.posY = Mathf.RoundToInt(Mathf.Floor(nextPosY*2));
					MG_ControlFogOfWar.I._recalculateRevealForMissile(missile);

					missile.tail.Add(MG_DB_Missiles.I._getSprite("Spectre1Tail"));
					missile.tail[missile.tail.Count-1].transform.position = new Vector3(
						missile.sprite.transform.position.x,
						missile.sprite.transform.position.y,
						(missile.isRevealed) ? missile.posY - 2 : 2000
					);
					missile.tail[missile.tail.Count-1].transform.rotation = missile.sprite.transform.rotation;
				}

				missile.tail.Clear();
			break;
			#endregion
			#region "Spiral"
			case "Spiral":
				speed = 0.25f;
				while(missile.targetDistance_half > 0){
					missile.targetDistance_half -= speed;
					float nextPosX = missile.sprite.transform.position.x + speed * Mathf.Cos((missile.angle * Mathf.PI) / 180);
					float nextPosY = missile.sprite.transform.position.y + speed * Mathf.Sin((missile.angle * Mathf.PI) / 180);
					missile.sprite.transform.position = new Vector2(nextPosX, nextPosY);

					missile.posX = Mathf.RoundToInt(Mathf.Floor(nextPosX*2));
					missile.posY = Mathf.RoundToInt(Mathf.Floor(nextPosY*2));
					MG_ControlFogOfWar.I._recalculateRevealForMissile(missile);

					missile.tail.Add(MG_DB_Missiles.I._getSprite("SpiralTail"));
					missile.tail[missile.tail.Count-1].transform.position = new Vector3(
						missile.sprite.transform.position.x,
						missile.sprite.transform.position.y,
						(missile.isRevealed) ? missile.posY - 2 : 2000
					);
					missile.tail[missile.tail.Count-1].transform.rotation = missile.sprite.transform.rotation;
				}

				missile.tail.Clear();
			break;
			#endregion
		}

		#region "Default missile effect"
		switch(missile.type){
			case "multishotMain":case "multishot":
				MG_ControlSFX.I._createSFX ("hit05", missile.posX, missile.posY);
			break;
		}
		#endregion
		#region "End Defaults"
		_addToDestroyList(missile);
		#endregion
	}
	#endregion

	#region "Remove missile"
	public void _addToDestroyList(MG_ClassMissile targetMissile){
		missilesToDestroy.Add(targetMissile.id);
		targetMissile.isAlive = false;
	}

	public void _destroyListed(){
		if(missilesToDestroy.Count > 0){
			for(int listedMissile = 0; listedMissile < missilesToDestroy.Count; listedMissile++){
				_destroyMissile(listedMissile);
			}
			missilesToDestroy.Clear();
		}
	}

	public void _destroyMissile(int targetUnitIndex){
		int indexToRemove = -1;
		for(int desUnitLoop = MG_Globals.I.missiles.Count - 1; desUnitLoop >= 0; desUnitLoop--){
			if(missilesToDestroy[targetUnitIndex] == MG_Globals.I.missiles[desUnitLoop].id){
				indexToRemove = desUnitLoop;
				break;
			}
		}
		if(indexToRemove > -1){
			Destroy(MG_Globals.I.missiles[indexToRemove].sprite);
			MG_Globals.I.missiles.RemoveAt(indexToRemove);
		}
	}
	#endregion
}
