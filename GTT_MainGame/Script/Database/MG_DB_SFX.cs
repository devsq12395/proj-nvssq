using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_SFX : MonoBehaviour {
	public static MG_DB_SFX I;
	public void Awake(){ I = this; }

	// New
	public GameObject cartoonHit01, cartoonHit02, cartoonHit03, cartoonHit04, cartoonHit05, cartoonHoly01, cartoonHoly02, cartoonNature01, cartoonSnow01, cartoonSpark01, cartoonExplodeFire01, cartoonExplodeFire02, cartoonPowerup01, cartoonDeathBall;
	public GameObject warhornEffect, warhornEffectBlue, scream;
	public GameObject koEffect, summonFx;
	public GameObject cartoonExplodeIce01, hit01_Reverse, photonBomb, deathArrowHit, drainLifeHit;

	// HEROES
	public GameObject dummy;
	public GameObject testSFX, heroSpot;

	// Colored Text Effect
	public GameObject bPlus, b1, b2, b3, b4, b5, b6, b7, b8, b9, b0;
	public GameObject rPlus, r1, r2, r3, r4, r5, r6, r7, r8, r9, r0;
	public GameObject o1, o2, o3, o4, o5, o6, o7, o8, o9, o0;
	public GameObject support, miss, resist;

	// Effects
	public GameObject explodeDust01;
	 
	// Other
	public GameObject moveSmoke, bladeDance1, bladeDance2;
	public GameObject weiss;
	public GameObject hit01, hit02, hit03, hit04, hit05, hit06, hit07, hit08, hit09, hit10, hit11, hit12, hit13, hit14;
	public GameObject hitBullet;
	public GameObject particle01;
	public GameObject drakgul01, drakgul02, parasolB, parasolR;
	public GameObject holy01, holy02, holy03, death01, death02, fire01, slam01, slam02;
	public GameObject RD_bloomGreen, RD_bloomLight, RD_bloomMagic;
	public GameObject RD_expBlue, RD_expBlueSmoke, RD_expFire, RD_expFireSmoke, RD_expYellow, RD_expYellowSmoke, RD_expWaterBlue, RD_flameBlue, RD_flameBlueSmoke, RD_flameBlueWater, RD_flameFire, 
		RD_flameSmoke, RD_flameYellow, RD_MHblackSmoke, RD_MHblue, RD_MHfire, RD_MHfireSmoke, RD_MHfireYellow, RD_MHmagic, RD_MHsmoke, RD_MHwater, RD_ringBlue, RD_ringFire, RD_ringFireSmoke, RD_ringMagic,
		RD_ringSmoke, RD_ringWater, RD_ringYellow;

	public GameObject _getSprite(string newSpriteName){
		GameObject returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		Destroy(returnValue);
		switch (newSpriteName) {
			case "testSFX": returnValue = GameObject.Instantiate(testSFX, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "heroSpot": returnValue = GameObject.Instantiate(heroSpot, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
				
			// New
			case "cartoonHit01":			returnValue = GameObject.Instantiate(cartoonHit01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonHit02":			returnValue = GameObject.Instantiate(cartoonHit02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonHit03":			returnValue = GameObject.Instantiate(cartoonHit03, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonHit04":			returnValue = GameObject.Instantiate(cartoonHit04, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonHit05":			returnValue = GameObject.Instantiate(cartoonHit05, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonHoly01":			returnValue = GameObject.Instantiate(cartoonHoly01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonHoly02":			returnValue = GameObject.Instantiate(cartoonHoly02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonNature01":			returnValue = GameObject.Instantiate(cartoonNature01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonSnow01":			returnValue = GameObject.Instantiate(cartoonSnow01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonSpark01":			returnValue = GameObject.Instantiate(cartoonSpark01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonExplodeFire01":	returnValue = GameObject.Instantiate(cartoonExplodeFire01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonExplodeFire02":	returnValue = GameObject.Instantiate(cartoonExplodeFire02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonPowerup01":		returnValue = GameObject.Instantiate(cartoonPowerup01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonDeathBall":		returnValue = GameObject.Instantiate(cartoonDeathBall, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "warhornEffect":			returnValue = GameObject.Instantiate(warhornEffect, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "warhornEffectBlue":		returnValue = GameObject.Instantiate(warhornEffectBlue, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "koEffect":				returnValue = GameObject.Instantiate(koEffect, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "scream":					returnValue = GameObject.Instantiate(scream, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "cartoonExplodeIce01":		returnValue = GameObject.Instantiate(cartoonExplodeIce01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "summonFx":				returnValue = GameObject.Instantiate(summonFx, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "slam01":					returnValue = GameObject.Instantiate(slam01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "slam02":					returnValue = GameObject.Instantiate(slam02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "deathArrowHit":			returnValue = GameObject.Instantiate(deathArrowHit, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "drainLifeHit":			returnValue = GameObject.Instantiate(drainLifeHit, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			// Rubberduck
			case "RD_expBlue":				returnValue = GameObject.Instantiate(RD_expBlue, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_expBlueSmoke":			returnValue = GameObject.Instantiate(RD_expBlueSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_expFire":				returnValue = GameObject.Instantiate(RD_expFire, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_expFireSmoke":			returnValue = GameObject.Instantiate(RD_expFireSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_expYellow":			returnValue = GameObject.Instantiate(RD_expYellow, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_expYellowSmoke":		returnValue = GameObject.Instantiate(RD_expYellowSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_expWaterBlue":			returnValue = GameObject.Instantiate(RD_expWaterBlue, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_flameBlue":			returnValue = GameObject.Instantiate(RD_flameBlue, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_flameBlueSmoke":		returnValue = GameObject.Instantiate(RD_flameBlueSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_flameBlueWater":		returnValue = GameObject.Instantiate(RD_flameBlueWater, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_flameFire":			returnValue = GameObject.Instantiate(RD_flameFire, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_flameSmoke":			returnValue = GameObject.Instantiate(RD_flameSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_flameYellow":			returnValue = GameObject.Instantiate(RD_flameYellow, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHblackSmoke":			returnValue = GameObject.Instantiate(RD_MHblackSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHblue":				returnValue = GameObject.Instantiate(RD_MHblue, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHfire":				returnValue = GameObject.Instantiate(RD_MHfire, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHfireSmoke":			returnValue = GameObject.Instantiate(RD_MHfireSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHfireYellow":			returnValue = GameObject.Instantiate(RD_MHfireYellow, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHmagic":				returnValue = GameObject.Instantiate(RD_MHmagic, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHsmoke":				returnValue = GameObject.Instantiate(RD_MHsmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_MHwater":				returnValue = GameObject.Instantiate(RD_MHwater, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_ringFire":				returnValue = GameObject.Instantiate(RD_ringFire, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_ringFireSmoke":		returnValue = GameObject.Instantiate(RD_ringFireSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_ringMagic":			returnValue = GameObject.Instantiate(RD_ringMagic, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_ringSmoke":			returnValue = GameObject.Instantiate(RD_ringSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_ringWater":			returnValue = GameObject.Instantiate(RD_ringWater, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "RD_ringYellow":			returnValue = GameObject.Instantiate(RD_ringYellow, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			// Colored Text Effect
			case "b+":						returnValue = GameObject.Instantiate(bPlus, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b1":						returnValue = GameObject.Instantiate(b1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b2":						returnValue = GameObject.Instantiate(b2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b3":						returnValue = GameObject.Instantiate(b3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b4":						returnValue = GameObject.Instantiate(b4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b5":						returnValue = GameObject.Instantiate(b5, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b6":						returnValue = GameObject.Instantiate(b6, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b7":						returnValue = GameObject.Instantiate(b7, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b8":						returnValue = GameObject.Instantiate(b8, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b9":						returnValue = GameObject.Instantiate(b9, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "b0":						returnValue = GameObject.Instantiate(b0, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r+":						returnValue = GameObject.Instantiate(rPlus, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r1":						returnValue = GameObject.Instantiate(r1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r2":						returnValue = GameObject.Instantiate(r2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r3":						returnValue = GameObject.Instantiate(r3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r4":						returnValue = GameObject.Instantiate(r4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r5":						returnValue = GameObject.Instantiate(r5, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r6":						returnValue = GameObject.Instantiate(r6, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r7":						returnValue = GameObject.Instantiate(r7, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r8":						returnValue = GameObject.Instantiate(r8, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r9":						returnValue = GameObject.Instantiate(r9, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "r0":						returnValue = GameObject.Instantiate(r0, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o1":						returnValue = GameObject.Instantiate(o1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o2":						returnValue = GameObject.Instantiate(o2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o3":						returnValue = GameObject.Instantiate(o3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o4":						returnValue = GameObject.Instantiate(o4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o5":						returnValue = GameObject.Instantiate(o5, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o6":						returnValue = GameObject.Instantiate(o6, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o7":						returnValue = GameObject.Instantiate(o7, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o8":						returnValue = GameObject.Instantiate(o8, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o9":						returnValue = GameObject.Instantiate(o9, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "o0":						returnValue = GameObject.Instantiate(o0, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Support":					returnValue = GameObject.Instantiate(support, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Miss":					returnValue = GameObject.Instantiate(miss, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Resist":					returnValue = GameObject.Instantiate(resist, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			
			// Manually made fx
			case "moveSmoke":				returnValue = GameObject.Instantiate(moveSmoke, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "bladeDance":				returnValue = GameObject.Instantiate(bladeDance1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "bladeDance2":				returnValue = GameObject.Instantiate(bladeDance2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "weiss":					returnValue = GameObject.Instantiate(weiss, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "drakgul01":				returnValue = GameObject.Instantiate(drakgul01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "drakgul02":				returnValue = GameObject.Instantiate(drakgul02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hitBullet":				returnValue = GameObject.Instantiate(hitBullet, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "parasolB":				returnValue = GameObject.Instantiate(parasolB, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "parasolR":				returnValue = GameObject.Instantiate(parasolR, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit01":					returnValue = GameObject.Instantiate(hit01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit01_Reverse":			returnValue = GameObject.Instantiate(hit01_Reverse, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "photonBomb":				returnValue = GameObject.Instantiate(photonBomb, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "explodeDust01":			returnValue = GameObject.Instantiate(explodeDust01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
				
			// RPG Maker assets - Should not be used
			case "hit02":					returnValue = GameObject.Instantiate(hit02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit03":					returnValue = GameObject.Instantiate(hit03, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit04":					returnValue = GameObject.Instantiate(hit04, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit05":					returnValue = GameObject.Instantiate(hit05, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit06":					returnValue = GameObject.Instantiate(hit06, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit07":					returnValue = GameObject.Instantiate(hit07, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit08":					returnValue = GameObject.Instantiate(hit08, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit09":					returnValue = GameObject.Instantiate(hit09, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit10":					returnValue = GameObject.Instantiate(hit10, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit11":					returnValue = GameObject.Instantiate(hit11, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit12":					returnValue = GameObject.Instantiate(hit12, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit13":					returnValue = GameObject.Instantiate(hit13, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hit14":					returnValue = GameObject.Instantiate(hit14, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			// RPG Maker assets - Should not be used
			case "particle01":				returnValue = GameObject.Instantiate(particle01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "holy01":					returnValue = GameObject.Instantiate(holy01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "holy02":					returnValue = GameObject.Instantiate(holy02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "holy03":					returnValue = GameObject.Instantiate(holy03, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "death01":					returnValue = GameObject.Instantiate(death01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "death02":					returnValue = GameObject.Instantiate(death02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "fire01":					returnValue = GameObject.Instantiate(fire01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			default: returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
		}

		return returnValue;
	}

	public float duration, customZPosOffset, customXOffset, customYOffset;
	public int turnDuration;
	public bool KILL_ON_ANIM_END, KILL_ON_DUR_END, KILL_ON_TURN_DUR_END, CUSTOM_Z_POS_OFFSET;
	public void _setValues(string newName){
		// Defaults
		customXOffset = 0;
		customYOffset = 0;
		KILL_ON_ANIM_END = true;
		KILL_ON_DUR_END = false;
		KILL_ON_TURN_DUR_END = false;
		CUSTOM_Z_POS_OFFSET = false;

		switch (newName) {
			#region "dummy"
			case "dummy": 
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = true;
			break;
				#endregion
			#region "heroSpot"
			case "heroSpot": 
				//duration = 1f;
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = false;
				KILL_ON_TURN_DUR_END = false;
				CUSTOM_Z_POS_OFFSET = true; customZPosOffset = 50;
			break;
			#endregion
			#region "Parasol"
			case "parasolB": case "parasolR": 
				duration = 0.75f;
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = true;
				KILL_ON_TURN_DUR_END = false;
			break;
			#endregion
			#region "holy01"
			case "holy01":
				customYOffset = 0.5f;
			break;
			#endregion
			#region "Text Effects"
			case "b+":case "b1":case "b2":case "b3":case "b4":case "b5":case "b6":case "b7":case "b8":case "b9":case "b0":
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = true; duration = 999999999f;
				KILL_ON_TURN_DUR_END = false;
				CUSTOM_Z_POS_OFFSET = true; customZPosOffset = -200;
			break;
			case "r+":case "r1":case "r2":case "r3":case "r4":case "r5":case "r6":case "r7":case "r8":case "r9":case "r0":
			case "o1":case "o2":case "o3":case "o4":case "o5":case "o6":case "o7":case "o8":case "o9":case "o0":
			case "Support":case "Miss":case "Resist":
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = true; duration = 1.5f;
				KILL_ON_TURN_DUR_END = false;
				CUSTOM_Z_POS_OFFSET = true; customZPosOffset = -200;
			break;
 			#endregion
			#region "Weiss"
			case "weiss": 
				//duration = 1f;
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = false;
				KILL_ON_TURN_DUR_END = true;
				turnDuration = 4;
				CUSTOM_Z_POS_OFFSET = true; customZPosOffset = -50; 
			break;
			#endregion
			#region "slam02":
			case "slam02": 
				//duration = 1f;
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = true; duration = 1f;
				KILL_ON_TURN_DUR_END = false;
				CUSTOM_Z_POS_OFFSET = true; customZPosOffset = 50;
			break;
			#endregion
			
			#region "Cartoon Spinning Effects"
			case "cartoonHoly02":case "cartoonNature01":case "cartoonDeathBall":case "warhornEffect":case "warhornEffectBlue":case "koEffect":case "scream":case "cartoonSnow01":
			case "slam01":case "deathArrowHit":case "drainLifeHit":case "explodeDust01":
				KILL_ON_ANIM_END = false;
				KILL_ON_DUR_END = true; duration = 1f;
				KILL_ON_TURN_DUR_END = false;
				CUSTOM_Z_POS_OFFSET = false;
			break;
			#endregion
		}
	}
}
