using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using UnityEngine.SceneManagement;

public class MG_ClassMissile {

	public GameObject sprite;
	public int targetX, targetY, id, ownerId, posX, posY;
	public string type;
	public float speed, angle, targetDistance_half;

	public bool isAlive = true, firstFrame = true, instantMove = false, deathOnAnimEnd = false;

	// Fog control
	public bool seen_through_fog = false;
	public bool isRevealed = false;

	// Misc
	public List<int> unitHitList;
	public int cusVal1, cusVal2, cusVal3, cusVal4;

	public MG_ClassMissile(string newType, int newOwnerId, int newPosX, int newPosY, int targetPosX, int targetPosY, int missileId){
		type = newType;
		targetX = targetPosX;
		targetY = targetPosY;
		targetDistance_half = Vector2.Distance (new Vector2 (newPosX, newPosY), new Vector2 (targetX, targetY)) / 2;
		id = missileId;
		ownerId = newOwnerId;
		tail = new List<GameObject>();

		sprite = MG_DB_Missiles.I._getSprite (newType);
		sprite.transform.position = (Vector3)sprite.transform.position + (new Vector3 ((float)newPosX/2, (float)newPosY/2, newPosY-2));

		sprite.transform.SetParent(GameObject.Find ("_MISSILES").transform);

		// Values
		MG_DB_Missiles.I._setValues(newType);
		speed 									= MG_DB_Missiles.I.speed;
		hasTail 								= MG_DB_Missiles.I.hasTail;
		tailType 								= MG_DB_Missiles.I.tailType;
		instantMove 							= MG_DB_Missiles.I.instantMove;
		deathOnAnimEnd							= MG_DB_Missiles.I.deathOnAnimEnd;

		// Rotation
		angle = (Mathf.Atan2(newPosY-targetPosY, newPosX-targetPosX) * (180/Mathf.PI)+180);
		sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+180));

		if (instantMove) {
			// Trigger instant move
			MG_ControlMissile.I._onInstantMove(this);
		} else {
			// Start missile movement tween
			_startTweenMove (new Vector3 ((float)targetPosX/2, (float)targetPosY/2, targetPosY-2));
		}

		// Misc
		unitHitList = new List<int>();
		unitHitList.Add (newOwnerId); // added to prevent errors
	}

	#region "Start Tween Move"
	public void _startTweenMove(Vector3 endTarget){
		Vector3 currentPos = sprite.transform.position;
		Vector3 endPos = endTarget;
		Vector2 sPosV2 = currentPos, ePosV2 = endPos;
		float dist = Vector2.Distance (sPosV2, ePosV2);
		float duration = dist / speed;

		MG_ControlCamera.I.transform.gameObject.Tween("missileMove#" + id, currentPos, endPos, duration, TweenScaleFunctions.Linear, (t) =>
		{
			// re-calculate position
			posX = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.x*2));
			posY = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.y*2));

			// calculate fog
			MG_ControlFogOfWar.I._recalculateRevealForMissile(this);
			if(hasTail) _createTail();
			// move sprite
			if(isRevealed){
				sprite.gameObject.transform.position = t.CurrentValue;
			}else{
				sprite.gameObject.transform.position = new Vector3(t.CurrentValue.x, t.CurrentValue.y, 2000);
			}
			if(firstFrame){
				firstFrame = false;
			}else{
				MG_ControlMissile.I._update_missilePath(this);
			}
		}, (t) =>
		{
			// re-calculate position
			posX = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.x*2));
			posY = Mathf.RoundToInt(Mathf.Floor((float)t.CurrentValue.y*2));
			
			// completion
			if(isRevealed){
				sprite.gameObject.transform.position = t.CurrentValue;
			}else{
				sprite.gameObject.transform.position = new Vector3(t.CurrentValue.x, t.CurrentValue.y, 2000);
			}

			_destroyTails();
			MG_ControlMissile.I._update_missileHit(this);
		});
	}
	#endregion
	#region "Tail System"
	public bool hasTail = false;
	public string tailType;
	public List<GameObject> tail;

	public void _createTail(){
		if(!isAlive)		return;
		tail.Add(MG_DB_Missiles.I._getSprite(tailType));
		tail[tail.Count-1].transform.position = sprite.transform.position;
		tail[tail.Count-1].transform.rotation = sprite.transform.rotation;
	}

	public void _destroyTails(){
		foreach(GameObject tLoop in tail){
			MG_ControlUnits.I._destroyUIObject(tLoop);
		}
		tail.Clear();
	}
	#endregion

	#region "Update()"
	public void _update(){
		
	}
	#endregion
}
