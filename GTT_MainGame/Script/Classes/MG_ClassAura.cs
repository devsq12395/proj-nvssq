using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassAura {
	public GameObject sprite;
	public string type;
	public int playerOwner, auraID, posX, posY, squareRange, ownerID;
	public string buffType;
	public bool isAlive;

	public MG_ClassAura(GameObject newSprite, string newType, MG_ClassUnit newUnitOwner, int newAuraID, string newBuffType, int newSquareRange){
		sprite = newSprite;
		sprite.transform.position = newUnitOwner.sprite.transform.position;
		type = newType;
		ownerID = newUnitOwner.unitID;
		playerOwner = newUnitOwner.playerOwner;
		auraID = newAuraID;
		posX = newUnitOwner.posX;
		posY = newUnitOwner.posY;
		buffType = newBuffType;
		squareRange = newSquareRange;
		isAlive = true;
	}
}
