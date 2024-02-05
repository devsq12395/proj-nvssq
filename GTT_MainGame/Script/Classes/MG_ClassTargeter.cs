using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassTargeter {

	public GameObject sprite;
	public uint id;
	public int posX, posY;
	public int type;

	public int rangeNum;
	public bool isAlive = true;

	public MG_ClassTargeter(GameObject newSprite, uint newObjId, int newPosX, int newPosY, int newType, int newRangeNum){
		id = newObjId;
		sprite = newSprite;
		sprite.transform.position 		= new Vector3((float)newPosX / 2, (float)newPosY / 2, 
			((MG_Globals.I.curPlayerNum == MG_Globals.I.curPlayerOnTurn) ? ((float)newPosY / 2) + 5 : 2000)
		);
		sprite.transform.localScale		= new Vector3(0.01f, 0.01f, sprite.transform.localScale.z);
		posX 							= newPosX;
		posY							= newPosY;
		type 							= newType;
		rangeNum 						= newRangeNum;

		sprite.transform.SetParent(GameObject.Find ("_TARGETER").transform);
	}
}
