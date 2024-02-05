using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlDoodad : MonoBehaviour {
	public static MG_ControlDoodad I;
	public void Awake(){ I = this; }

	private int doodCnt;
	public List<int> unitsToDestroy;

	public void _createDoodad(string newDoodName, int newPosX, int newPosY){
		doodCnt++;
		MG_Globals.I.doodads.Add (new MG_ClassDoodad (newDoodName, doodCnt, newPosX, newPosY, newPosX, newPosY));
	}

	public void _createDoodad(string newDoodName, int newPosX, int newPosY, float actPosX, float actPosY){
		doodCnt++;
		MG_Globals.I.doodads.Add (new MG_ClassDoodad (newDoodName, doodCnt, newPosX, newPosY, actPosX, actPosY, true));
	}
}
