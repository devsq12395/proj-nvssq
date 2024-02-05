using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Missiles : MonoBehaviour {
	public static MG_DB_Missiles I;
	public void Awake(){ I = this; }

	public GameObject dummy;
	public GameObject testMissile;
	public GameObject hookshot, hookshotTail, speartoss, longbow, arcGun01, arcGun02, revolver, dynamite, spectre1, spectre2, spiral, windSlash, windSlashBlue, clericMissile, coltMissile, spectreMissile, arcaneBolt, 
		axeThrow, deathArrow, drainLife, grenade, cannon, fireball, acidBreath, lightningBolt;

	public GameObject _getSprite(string newSpriteName){
		GameObject returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		Destroy(returnValue);
		switch (newSpriteName) {
			case "testMissile": returnValue = GameObject.Instantiate(testMissile, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hookshot":case "hookdance": returnValue = GameObject.Instantiate(hookshot, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "hookshotTail": returnValue = GameObject.Instantiate(hookshotTail, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "speartoss": returnValue = GameObject.Instantiate(speartoss, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "multishotMain":case "multishot":case "Amy":case "Tower": returnValue = GameObject.Instantiate(longbow, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "ArcGun01":returnValue = GameObject.Instantiate(arcGun01, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "ArcGun02":case "WalterMain":case "Walter":case "BurstFire":case "BurstFireMain":returnValue = GameObject.Instantiate(arcGun02, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "MythrilRevolver":case "QuickDrawMain":case "QuickDraw":case "DisarmShot":returnValue = GameObject.Instantiate(revolver, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Dynamite":returnValue = GameObject.Instantiate(dynamite, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Spectre1Tail":returnValue = GameObject.Instantiate(spectre1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Spectre2Tail":returnValue = GameObject.Instantiate(spectre2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "SpiralTail":returnValue = GameObject.Instantiate(spiral, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "WindSlash":returnValue = GameObject.Instantiate(windSlash, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "WindSlashBlue":returnValue = GameObject.Instantiate(windSlashBlue, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "clericMissile":returnValue = GameObject.Instantiate(clericMissile, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "coltMissile":returnValue = GameObject.Instantiate(coltMissile, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "spectreMissile":returnValue = GameObject.Instantiate(spectreMissile, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "ArcaneBolt":returnValue = GameObject.Instantiate(arcaneBolt, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "AxeThrow":case "AxeThrow(Boss)":returnValue = GameObject.Instantiate(axeThrow, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "DeathArrow":returnValue = GameObject.Instantiate(deathArrow, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "DrainLife":returnValue = GameObject.Instantiate(drainLife, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Grenade":returnValue = GameObject.Instantiate(grenade, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Cannon":case "grapeshotMain":case "grapeshot":returnValue = GameObject.Instantiate(cannon, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "fireballSpell":returnValue = GameObject.Instantiate(fireball, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "AcidBreath":returnValue = GameObject.Instantiate(acidBreath, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "LightningBolt":returnValue = GameObject.Instantiate(lightningBolt, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			default: returnValue = GameObject.Instantiate (dummy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return returnValue;
	}

	///////////////////////////// STATS /////////////////////////////
	public float speed;
	public bool hasTail, instantMove, deathOnAnimEnd;
	public string tailType;

	public void _setValues(string missileName){
		#region "Defaults"
		hasTail = false;
		instantMove = false;
		deathOnAnimEnd = false;
		tailType = "";
		speed = 5f;
		#endregion

		switch (missileName) {
			// No need to put anything here if it's a generic 5f speed missile
			case "hookshot":case "hookdance":
				speed = 5f;
				hasTail = true;
				tailType = "hookshotTail";
			break;
			case "fireballSpell":
				speed = 8f;
			break;
			case "multishotMain":case "multishot":case "grapeshotMain":case "grapeshot":
				speed = 5.3f;
			break;
			case "WindSlashBlue":
				speed = 2f;
			break;
			case "ArcGun01":case "QuickDraw":case "QuickDrawMain":case "DisarmShot":case "MythrilRevolver":case "LightningBolt":
				speed = 6f;
			break;
			case "Spectre1":case "Spectre2":case "Spiral":
				instantMove = true;
			break;
			case "Spectre1Tail":case "Spectre2Tail":case "SpiralTail":
				deathOnAnimEnd = true;
			break;
		}
	}
	///////////////////////////// STATS ///////////////////////////// 
}
