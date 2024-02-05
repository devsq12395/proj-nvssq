using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ClassSightBlock : MonoBehaviour {
	public int posX, posY, id;
	public string ownerType; // unit, doodad

	public MG_ClassSightBlock(int newPosX, int newPosY, string newOwnerType, int newId){
		reposition (newPosX, newPosY);

		ownerType = newOwnerType;
		id = newId;
	}

	public void reposition(int newPosX, int newPosY){
		posX = newPosX;
		posY = newPosY;
	}
}
