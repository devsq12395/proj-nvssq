using UnityEngine;
using System.Collections;

public class MG_ClassTerrain{

	public GameObject sprite, fogOfWar;
	public int posX, posY;
	public string type, spriteName, objID;
	public bool isAlive;

	public MG_ClassTerrain(GameObject newSprite, string newObjId, int newPosX, int newPosY, string newTerrType, string newSpriteName){
		sprite 							= newSprite;
		sprite.transform.position 		= new Vector3((float)newPosX / 2, (float)newPosY / 2, 199);
		posX 							= newPosX;
		posY							= newPosY;
		sprite.name						= newObjId;
		objID							= newObjId;
		type							= newTerrType;
		isAlive							= true;
		spriteName 						= newSpriteName;

		sprite.name = sprite.name.Replace("(Clone)", "");
		sprite.transform.SetParent(GameObject.Find ("_TERRAIN").transform);

		// Fog of war set for this terrain
		fogOfWar = MG_ControlTerrain.I.createFogOfWarGameObject((float)newPosX / 2, (float)newPosY / 2);
		fogOfWar.transform.position = new Vector3 (sprite.transform.position.x, sprite.transform.position.y, sprite.transform.position.y - 4);
	}

	#region "Sprite Changes"
	public void _changeTerrain(string newSpriteName){
		Vector3 origPos = new Vector3 (0, 0, 0);
		if (sprite) {
			MG_ControlUnits.I._destroyUIObject (sprite);
			origPos = sprite.transform.position;
		} else {
			return;
		}

		sprite = MG_DB_Terrain.I._getSprite (newSpriteName);
		sprite.transform.position = origPos;
		type = newSpriteName;

		sprite.name = sprite.name.Replace("(Clone)", "");
		sprite.transform.SetParent(GameObject.Find ("_TERRAIN").transform);
	}
	#endregion

	#region "FOG OF WAR - Reveal/Unreveal (Terrain Only)"
	public void _reveal(){
		fogOfWar.transform.position = new Vector3 (fogOfWar.transform.position.x, fogOfWar.transform.position.y, 2000);
	}

	public void _unreveal(){
		fogOfWar.transform.position = new Vector3 (fogOfWar.transform.position.x, fogOfWar.transform.position.y, fogOfWar.transform.position.y - 20);
	}
	#endregion
}
