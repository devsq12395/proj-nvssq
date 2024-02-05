using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassDoodad {
	public GameObject sprite;
	public int posX, posY, id;
	public string type;
	public bool isBlocker, isSightBlocker;
	
	public float armorBon;

	public MG_ClassDoodad(string newDoodType, int newDoodId, int newPosX, int newPosY, float actualPosX, float actualPosY, bool isActualPos = false){
		sprite = MG_DB_Doodads.I._getSprite (newDoodType);
		MG_DB_Doodads.I._setValues (newDoodType);

		posX = newPosX;
		posY = newPosY;
		type = newDoodType;
		id = newDoodId;
		isBlocker = MG_DB_Doodads.I.isBlocker;
		isSightBlocker = MG_DB_Doodads.I.isSightBlocker;
		
		armorBon = MG_DB_Doodads.I.armorBon;

		sprite.name = newDoodType;
		if (isActualPos) {
			sprite.transform.position = new Vector3 (
				actualPosX,
				actualPosY,
				posY - 0.9f
			);
		} else {
			sprite.transform.position = new Vector3 (
				(float)actualPosX / 2,
				(float)actualPosY / 2 + MG_DB_Doodads.I.yOffset,
				(float)posY - 0.9f
			);
		}
		sprite.transform.SetParent(GameObject.Find ("_DOODADS").transform);

		// Sight blocker
		if(isSightBlocker){
			MG_Globals.I.sightBlocker.Add(new MG_ClassSightBlock(posX, posY, "Doodad", id));
		}
	}
}
