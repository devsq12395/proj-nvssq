using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Attachments : MonoBehaviour {
	public static MG_DB_Attachments I;
	public void Awake(){ I = this; }

	#region "Data"
	public bool hasAttachment(string buffName){
		bool retVal = false;
		switch (buffName) {
			case "testBuff1":case "PointRed":case "PointBlue":case "ArmorBreak":case "Stun":case "Shell":case "Blessing":case "Weiss":case "DisarmShot":case "Mirror":case "Vigor":
			case "Warhorn":case "AllOutAssault":case "HoldTheLinePlus":case "Bleed":case "Entangle":case "RootArmor":case "Fairies":case "TestOfFaith_ally":case "TestOfFaith_enemy":
			case "Silence":case "BarrierBlue":case "BarrierRed":case "Boost":case "VisionBlue":case "VisionRed":case "Haste":
			case "Brotherhood":case "Veteran":case "HolyCrusade":case "Amplify":case "LightningBolt":
				retVal = true;
			break;
		}
		return retVal;
	}
	#endregion

	#region "Sprite offset"
	public float offsetX, offsetY;

	public void _setAttachmentData(string buffName){
		switch (buffName) {
			case "testBuff1":
				offsetX = -0.15f;
				offsetY = 0.25f;
			break;
			case "PointBlue":case "PointRed":
				offsetX = 0;
				offsetY = -0.3f;
			break;
			case "ArmorBreak":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			case "Shell":
				offsetX = 0f;
				offsetY = 0.15f;
			break;
			case "Blessing":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			case "Haste":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			case "Weiss":case "VisionBlue":case "VisionRed":
				offsetX = 0f;
				offsetY = 0f;
			break;
			case "Mirror":
				offsetX = 0f;
				offsetY = 0.1f;
			break;
			case "Vigor":
				offsetX = -0.25f;
				offsetY = 0.25f;
			break;
			case "Brinnande":
				offsetX = 0.25f;
				offsetY = 0.25f;
			break;
			case "Warhorn":
				offsetX = 0.2f;
				offsetY = 0.2f;
			break;
			case "AllOutAssault":case "HoldTheLinePlus":case "Brotherhood":case "HolyCrusade":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			case "Veteran":
				offsetX = -0.2f;
				offsetY = 0.2f;
			break;
			case "Bleed":
				offsetX = -0.2f;
				offsetY = 0.2f;
			break;
			case "Entangle":
				offsetX = 0f;
				offsetY = -0.2f;
			break;
			case "RootArmor":
				offsetX = 0f;
				offsetY = 0f;
			break;
			case "Fairies":
				offsetX = 0f;
				offsetY = 0f;
			break;
			case "TestOfFaith_ally":
				offsetX = 0f;
				offsetY = 0.3f;
			break;
			case "TestOfFaith_enemy":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			case "Silence":
				offsetX = 0.1f;
				offsetY = 0.25f;
			break;
			case "Boost":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			case "Amplify":
				offsetX = 0f;
				offsetY = 0f;
			break;
			case "LightningBolt":
				offsetX = 0f;
				offsetY = 0.25f;
			break;

			case "Stun":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			case "Disarm":case "DisarmShot":
				offsetX = 0f;
				offsetY = 0.25f;
			break;
			default:
				offsetX = 0;
				offsetY = 0;
			break;
		}
	}
	#endregion

	#region "Sprite"
	public GameObject dummy, bonusMovement, pointBlue, pointRed, armorBreak, stun, shell, blessing, weiss, disarm, mirror, vigor, warhorn, allOutAssault, holdTheLinePlus, bleed, entangle, rootArmor, fairies,
		testOfFaithAlly, testOfFaithEnemy, silence, barrierBlue, barrierRed, boost, visionBlue, visionRed, haste, brotherhood, veteran, amplify, lightningBolt;

	public GameObject _getSprite(string newName){
		GameObject returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		Destroy(returnValue);
		switch (newName) {
			case "testBuff1": returnValue = GameObject.Instantiate (bonusMovement, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "PointBlue": returnValue = GameObject.Instantiate (pointBlue, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "PointRed": returnValue = GameObject.Instantiate (pointRed, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "ArmorBreak": returnValue = GameObject.Instantiate (armorBreak, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Shell": returnValue = GameObject.Instantiate (shell, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Blessing": returnValue = GameObject.Instantiate (blessing, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Weiss": returnValue = GameObject.Instantiate (weiss, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Mirror": returnValue = GameObject.Instantiate (mirror, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Vigor":case "Brinnande": returnValue = GameObject.Instantiate (vigor, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Warhorn": returnValue = GameObject.Instantiate (warhorn, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "AllOutAssault": returnValue = GameObject.Instantiate (allOutAssault, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "HoldTheLinePlus": returnValue = GameObject.Instantiate (holdTheLinePlus, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Bleed": returnValue = GameObject.Instantiate (bleed, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Entangle": returnValue = GameObject.Instantiate (entangle, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "RootArmor": returnValue = GameObject.Instantiate (rootArmor, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Fairies": returnValue = GameObject.Instantiate (fairies, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "TestOfFaith_ally": returnValue = GameObject.Instantiate (testOfFaithAlly, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "TestOfFaith_enemy": returnValue = GameObject.Instantiate (testOfFaithEnemy, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Silence": returnValue = GameObject.Instantiate (silence, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "BarrierBlue": returnValue = GameObject.Instantiate (barrierBlue, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "BarrierRed": returnValue = GameObject.Instantiate (barrierRed, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Boost": returnValue = GameObject.Instantiate (boost, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "VisionBlue": returnValue = GameObject.Instantiate (visionBlue, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "VisionRed": returnValue = GameObject.Instantiate (visionRed, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Haste": returnValue = GameObject.Instantiate (haste, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "HolyCrusade":case "Brotherhood": returnValue = GameObject.Instantiate (brotherhood, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Veteran": returnValue = GameObject.Instantiate (veteran, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "Amplify": returnValue = GameObject.Instantiate (amplify, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
			case "LightningBolt": returnValue = GameObject.Instantiate (lightningBolt, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;

			case "DisarmShot": returnValue = GameObject.Instantiate (disarm, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;

			case "Stun": returnValue = GameObject.Instantiate (stun, new Vector3 (0, 0, 0), Quaternion.Euler (new Vector3 (0, 0, 0))) as GameObject; break;
		}

		return returnValue;
	}
	#endregion
}
