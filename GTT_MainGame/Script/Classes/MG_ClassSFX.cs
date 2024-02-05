using UnityEngine;
using System.Collections;

public class MG_ClassSFX {

	public GameObject sprite;
	public Animator animation;
	public int id, turnDuration;
	public float duration, customZPosOffset;
	public string type;
	public int posX, posY;

	// STATS
	public bool KILL_ON_ANIM_END, KILL_ON_DUR_END, KILL_ON_TURN_DUR_END, CUSTOM_Z_POS_OFFSET;
	public float customXOffset, customYOffset;

	public MG_ClassSFX(string newSfxType, int newObjId, float newPosX, float newPosY, bool floatCreate = false, bool adjustGamePos = false, bool throughFog = false){
		sprite = MG_DB_SFX.I._getSprite(newSfxType);
		if(sprite.GetComponent<Animator> () != null)
			animation = sprite.GetComponent<Animator> ();
		
		id = newObjId;
		type = newSfxType;
		sprite.name = newSfxType + id.ToString();
		//duration = DB_SFX.I._getDuration(newSfxType);
		if (adjustGamePos) {
			posX = (int)newPosX / 2;
			posY = (int)newPosY / 2;
		} else {
			posX = (int)newPosX;
			posY = (int)newPosY;
		}

		#region "Values from SFX Database"
		MG_DB_SFX.I._setValues(newSfxType);
		KILL_ON_ANIM_END 				= MG_DB_SFX.I.KILL_ON_ANIM_END;
		KILL_ON_DUR_END 				= MG_DB_SFX.I.KILL_ON_DUR_END;
		if(KILL_ON_DUR_END){
			duration = MG_DB_SFX.I.duration;
		}
		KILL_ON_TURN_DUR_END 			= MG_DB_SFX.I.KILL_ON_TURN_DUR_END;
		if (KILL_ON_TURN_DUR_END){
			turnDuration = MG_DB_SFX.I.turnDuration;
		}
		CUSTOM_Z_POS_OFFSET				= MG_DB_SFX.I.CUSTOM_Z_POS_OFFSET;
		if (CUSTOM_Z_POS_OFFSET) {
			customZPosOffset = MG_DB_SFX.I.customZPosOffset;
		}

		customXOffset = MG_DB_SFX.I.customXOffset;
		customYOffset = MG_DB_SFX.I.customYOffset;
		#endregion

		if(MG_ControlFogOfWar.I._pointIsRevealed(posX, posY) || throughFog){
			if(CUSTOM_Z_POS_OFFSET)
				sprite.transform.position = new Vector3(((float)newPosX/2) + customXOffset, ((float)newPosY/2) + customYOffset, (((float)newPosY - 3)) + customZPosOffset);
			else 
				sprite.transform.position = new Vector3(((float)newPosX/2) + customXOffset, ((float)newPosY/2) + customYOffset, ((float)newPosY - 3));
		}else{
			sprite.transform.position = new Vector3(2000, 2000, 2000);
		}
	}

	#region "Update"
	public void _update(float deltaTime){
		// Kill on animation end
		if (KILL_ON_ANIM_END) {
			if (this.animation.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !this.animation.IsInTransition(0)) {
				MG_ControlSFX.I._addToDestroyList (this);
				sprite.transform.position = new Vector3(1000, 1000, -1000);
			}
		}

		// Kill on duration end
		if(KILL_ON_DUR_END){
			duration -= deltaTime;
			if (duration <= 0) {
				if(isTimer){
					MG_ControlSFX.I._runTimer(this);
				}

				MG_ControlSFX.I._addToDestroyList (this);
				if(sprite!=null)sprite.transform.position = new Vector3(1000, 1000, -1000);
			}
		}
	}
	#endregion
	#region "End Turn"
	public void _endTurn(){
		// Kill on turn duration end
		if(KILL_ON_TURN_DUR_END){
			turnDuration--;
			if (turnDuration <= 0) {
				MG_ControlSFX.I._addToDestroyList (this);
				sprite.transform.position = new Vector3(1000, 1000, -1000);
			}
		}
	}
	#endregion

	// Movement
	public bool coloredText;
	public void _moveSprite(float xIncrement, float yIncrement){
		sprite.transform.position = sprite.transform.position + new Vector3(xIncrement, yIncrement, 0);
	}

	// Timer
	public string timerStorage1, timerStorage2, timerStorage3;
	public int timerExec;
	public bool isTimer;
	public void _assignTimer(int executeNumber, float newDuration){
		timerExec = executeNumber;
		isTimer = true;
		duration = newDuration;
	}
}
