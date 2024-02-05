using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MG_ControlTerrain : MonoBehaviour {
	public static MG_ControlTerrain I;
	public void Awake(){ I = this; }

	public GameObject fogOfWar;

	private int terCount;

	public void _createTerrain(string spriteName, int newPosX, int newPosY, string newTerrainType){
		MG_ClassTerrain newTerrain = new MG_ClassTerrain(MG_DB_Terrain.I._getSprite(spriteName), "Terrain" + terCount, newPosX, newPosY, newTerrainType, spriteName);
		MG_Globals.I.terrains.Add(newTerrain);
		//Path Blocker
		switch(newTerrainType){
			case "River":case "Cliff":
//				IntObjects.I._createObj("pathBlocker", newPosX, newPosY); 
			break;
		}
		#region "Creation specials"
//		switch(spriteName){
//			/*Tree*/case 9:case 20:case 44:
////			IntObjects.I._createObj("treeBig", newPosX, newPosY); 
////			IntObjects.I._createObj("pathBlocker", newPosX-1, newPosY); 
////			IntObjects.I._createObj("pathBlocker", newPosX+1, newPosY); 
//			break; 
//		}
		#endregion
		terCount++;
	}

	public void _createTerrain_Line(int rowNumber, int[] tileType){
		int currentX = (((tileType.Length-1)/2)*-1);
		string terrainType = "grass01";
		for(int curTile = 0; curTile < tileType.Length; curTile++){
			switch(tileType[curTile]){
				case 1: 			terrainType = "grass01"; break;
				case 2: 			terrainType = "grass02"; break;
			}
			_createTerrain(terrainType, currentX, rowNumber, terrainType);
			currentX++;
		}
	}

	public GameObject createFogOfWarGameObject(float posX, float posY){
		return GameObject.Instantiate(fogOfWar, new Vector3(posX, posY, -190), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
	}
}
