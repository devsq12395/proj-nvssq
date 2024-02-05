using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlSFX : MonoBehaviour {
	public static MG_ControlSFX I;
	public void Awake(){ I = this; }

	private int sfxCnt;
	public List<int> sfxToDestroy;

	public MG_ClassSFX _createSFX(string newSfxName, int newPosX, int newPosY, bool goThroughFog = false){
		sfxCnt++;
		MG_ClassSFX retVal = new MG_ClassSFX (newSfxName, sfxCnt, newPosX, newPosY, false, false, goThroughFog);
		MG_Globals.I.sfxTemp.Add(retVal);

		return retVal;
	}

	public MG_ClassSFX _createSFX_float(string newSfxName, float newPosX, float newPosY){
		sfxCnt++;
		MG_ClassSFX retVal;
		retVal = new MG_ClassSFX (newSfxName, sfxCnt, newPosX, newPosY);
		MG_Globals.I.sfxTemp.Add(retVal);

		return retVal;
	}

	#region "Timer sfx"
	public void _createTimer(int execNumber, float newDuration, int posX, int posY, string storage1="", string storage2=""){
		_createSFX("dummy", posX, posY);
		MG_Globals.I.sfxTemp[MG_Globals.I.sfxTemp.Count-1]._assignTimer(execNumber, newDuration);
		MG_Globals.I.sfxTemp[MG_Globals.I.sfxTemp.Count-1].timerStorage1 = storage1;
		MG_Globals.I.sfxTemp[MG_Globals.I.sfxTemp.Count-1].timerStorage2 = storage2;
	}

	public void _runTimer(MG_ClassSFX timerSfx){
		//Temporary variables
		int posX 					= timerSfx.posX;
		int posY					= timerSfx.posY;
		switch (timerSfx.timerExec) {
			case 1:
				// Sword Dance
				float sdpx = Random.Range (timerSfx.posX - 1.5f, timerSfx.posX + 1.5f), sdpy = Random.Range (timerSfx.posY - 1.5f, timerSfx.posY + 1.5f);
				_createSFX_float ((Random.Range(0, 1) <= 0.5) ? "bladeDance" : "bladeDance2", sdpx, sdpy);
			break;
		}
	}
	#endregion
	#region "Remove sfx"
	public void _addToDestroyList(MG_ClassSFX targSFX){
		sfxToDestroy.Add(targSFX.id);
	}

	public void _destroyListed(){
		if(sfxToDestroy.Count > 0){
			for(int listedUnits = 0; listedUnits < sfxToDestroy.Count; listedUnits++){
				_destroySfx(listedUnits);
			}
			sfxToDestroy.Clear();
		}
	}

	public void _destroySfx(int targetUnitIndex){
		int indexToRemove = -1;
		for(int desUnitLoop = MG_Globals.I.sfx.Count - 1; desUnitLoop >= 0; desUnitLoop--){
			if(sfxToDestroy[targetUnitIndex] == MG_Globals.I.sfx[desUnitLoop].id){
				indexToRemove = desUnitLoop;
				break;
			}
		}
		if(indexToRemove > -1){
			Destroy(MG_Globals.I.sfx[indexToRemove].sprite);
			MG_Globals.I.sfx.RemoveAt(indexToRemove);
		}
	}
	#endregion
}
