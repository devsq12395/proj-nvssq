using UnityEngine;
using System.Collections;
using DigitalRuby.Tween;

public class MG_ControlCursor : MonoBehaviour {
	public static MG_ControlCursor I;
	public void Awake(){ I = this; }

	public GameObject cursorS;
	public int posX = 0, posY = 0;
	public bool canMoveCursor = true;

	public void _start(){
		//Variables
		cursorS								= GameObject.Find("_CURSOR");
	}
	
	public void update (){
		
	}

	#region "Move Cursor"
	public void _moveCursor(float newPosX, float newPosY, bool byPassConditions = false){
		if (!canMoveCursor && !byPassConditions) 			return;
		if (!_pressConditions() && !byPassConditions)		return;

		double newPosX_R = System.Math.Round((double)newPosX, 1) ;
		double newPosY_R = System.Math.Round((double)newPosY, 1) ;
		float tmpX = Mathf.Ceil((float)((newPosX_R*10)-2.5f)/5f) * 5f;
		float tmpY = Mathf.Ceil((float)((newPosY_R*10)-2.5f)/5f) * 5f;

		int tempPosX = posX;
		int tempPosY = posY;

		if (!byPassConditions) {
			if ((int)(tmpX / 5) != tempPosX || (int)(tmpY / 5) != tempPosY) {
				MG_ControlSounds.I._playSound(3, 0, 0, false);
			}
		}

		posX = (int)(tmpX/5);
		posY = (int)(tmpY/5);
		//Debug.Log (posX + ", " + posY);

		if(posX > MG_Globals.I.mapLimitX || posX < -MG_Globals.I.mapLimitX || posY > MG_Globals.I.mapLimitY || posY < -MG_Globals.I.mapLimitY){	
			posX = tempPosX; posY = tempPosY;
		}

		//cursorS.transform.position = new Vector3((float)posX/2, (float)posY/2, ((float)posY/2)-1);
		Vector3 firstPos = cursorS.transform.position;
		// Old - Y-position dependent
		//Vector3 targetPos = new Vector3((float)posX/2, (float)posY/2, ((float)posY/2)-1);
		// New - Constant, cursor always in front
		Vector3 targetPos = new Vector3((float)posX/2, (float)posY/2, -200);
		float duration = 0.05f;
		gameObject.Tween("uiMove_skillWindow", firstPos, targetPos, duration, TweenScaleFunctions.Linear, (t) =>{
			// progress
			cursorS.transform.position = t.CurrentValue;
		}, (t) =>{
			// completion
			cursorS.transform.position = targetPos;
		});

		// Change selected's facing
		if (MG_Globals.I.curCommand == "aim friendly" || MG_Globals.I.curCommand == "aim hostile") {
			MG_ClassUnit selected = MG_Globals.I.selectedUnit;

			if(posX > selected.posX){
				selected._changeFacing("right");
			}else if(posX < selected.posX){
				selected._changeFacing("left");
			}
		}

		_interact (true);

		#region "Hide Action Bar"
		if (MG_Globals.I.curCommand == "skill") {
			MG_UIControl_Action.I.hide ();

			MG_ControlTargeters.I._clearTargeters();
			MG_ControlCursor.I._interact();
			MG_Globals.I.mode = "in map";
			MG_Globals.I.curCommand = "in map";
			MG_ControlCursor.I._changeCursorSprite("Normal");
		}
		#endregion
		#region "DEV DEBUGGING - Cursor move"
		if(MG_Globals.I.DEBUG_DEV_MODE){
			if(!MG_UIControl_DebugUI.I.mode_cursorMoveUpdateReport)			return;

			MG_UIControl_DebugUI.I._addConsoleTextLine("CURSOR HAS MOVED");
			MG_UIControl_DebugUI.I._addConsoleTextLine("At (X = " + posX + ", Y = " + posY + ")");
			MG_UIControl_DebugUI.I._addConsoleTextLine("cur command: " + MG_Globals.I.curCommand);
			MG_UIControl_DebugUI.I._addConsoleTextLine("cur mode: " + MG_Globals.I.mode);
			try{
				if(MG_Globals.I.selectedUnit != null){
					MG_UIControl_DebugUI.I._addConsoleTextLine("sel unit name: " + MG_Globals.I.selectedUnit);
					MG_UIControl_DebugUI.I._addConsoleTextLine("sel unit pos: (X = " + MG_Globals.I.selectedUnit.posX + ", Y = " + MG_Globals.I.selectedUnit.posY + ")");
				}else{
					MG_UIControl_DebugUI.I._addConsoleTextLine("selected unit is null");
				}
			}catch{
				MG_UIControl_DebugUI.I._addConsoleTextLine("error when looking for selected unit");
			}
		}
		#endregion
	}
	#endregion
	#region "Show/Hide Cursor"
	public void _showCursor(){
		cursorS.transform.position = new Vector3 (cursorS.transform.position.x, cursorS.transform.position.y, -200);
	}

	public void _hideCursor(){
		cursorS.transform.position = new Vector3 (cursorS.transform.position.x, cursorS.transform.position.y, 2000);
	}
	#endregion

	//Returns false when player cannot move the cursor
	private bool _pressConditions(){
		bool output = true;

		#region "Conditions"
		// condition - PAUSE (a window is open)
		if(MG_Globals.I.pause_windowOpen || MG_UIControl_UnitShop.I.isShow)	
			output = false;

		// condition - PAUSE (a ui is moving)
		if(MG_Globals.I.pause_uiMove || MG_Globals.I.pause_gamePause || MG_Globals.I.pause_gameOver)		 		output = false;

		// condition - PAUSE (on item screen)
		if(MG_Globals.I.pause_itemBuying)			output = false;

		// Not current player's turn/
		if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum)  output = false;

		// Command states where cursor should be unmovable
		if(MG_Globals.I.curCommand == "skill" || MG_Globals.I.curCommand == "summon") 
			output = false;
		#endregion

		return output;
	}

	#region "Change Cursor Sprite"
	//Script file can be found at _CURSOR
	public GameObject cursorNormalBlue, cursorNormalRed, cursorArea1, cursorArea2, cursorArea3, cursorArea4, cursorArea5;
	public void _changeCursorSprite(string newCursor){
		Vector3 tempPos = cursorS.transform.position;
		Destroy(cursorS);
		switch(newCursor){
			case "Normal":
				if (MG_Globals.I.curPlayerNum != 1) {
					cursorS = GameObject.Instantiate (cursorNormalRed, tempPos, Quaternion.identity) as GameObject;
				}else{
					cursorS = GameObject.Instantiate(cursorNormalBlue, tempPos, Quaternion.identity) as GameObject;
				}
			break;
			case "Area1":
				cursorS = GameObject.Instantiate(cursorArea1, tempPos, Quaternion.identity) as GameObject;
			break;
			case "Area2":
				cursorS = GameObject.Instantiate(cursorArea2, tempPos, Quaternion.identity) as GameObject;
			break;
			case "Area3":
				cursorS = GameObject.Instantiate(cursorArea3, tempPos, Quaternion.identity) as GameObject;
			break;
			case "Area4":
				cursorS = GameObject.Instantiate(cursorArea4, tempPos, Quaternion.identity) as GameObject;
			break;
			case "Area5":
				cursorS = GameObject.Instantiate(cursorArea5, tempPos, Quaternion.identity) as GameObject;
			break;
		}
	}
	#endregion

	/// <summary>
	/// Once a point in the map has been pressed, this function will decide on 
	///	what actions the game will make depending on the current playerMode.
	/// This method is called from multiple sources
	/// </summary>
	#region "_interact()"
	public void _interact(bool cursorMove = false){
		// Hide all aura sprites
		foreach(MG_ClassAura aL in MG_ControlAura.I.listAura){
			aL.sprite.transform.position = new Vector3(aL.sprite.transform.position.x, aL.sprite.transform.position.y, 2000);
		}

		if (MG_GetUnit.I._pointHasUnit (posX, posY) && MG_ControlFogOfWar.I._pointIsRevealed(posX, posY)) {
			// Point has unit
			MG_ClassUnit unit = MG_GetUnit.I._pickUnit(posX, posY);

			switch (MG_Globals.I.mode) {
				case "in map":
					if (unit.playerOwner == MG_Globals.I.curPlayerOnTurn) {
						// Change selected unit
						MG_Globals.I.selectedUnit = unit;
						MG_Globals.I.selectedUnit_exist = true;
						MG_ControlCommand.I._changeCommandsAndUI ();

						// Show aura if unit has aura
						if(cursorMove && MG_Globals.I.curPlayerNum == MG_Globals.I.curPlayerOnTurn){
							foreach(MG_ClassAura aL in MG_ControlAura.I.listAura){
								if (aL.ownerID == unit.unitID) {
									aL.sprite.transform.position = new Vector3(aL.sprite.transform.position.x, aL.sprite.transform.position.y, unit.sprite.transform.position.z);
								}
							}
						}
					} else {
						MG_Globals.I.selectedUnit = null;
						MG_Globals.I.selectedUnit_exist = false;
						MG_ControlCommand.I._changeCommandsAndUI ();

						// Show aura if unit has aura
						if (cursorMove && MG_Globals.I.curPlayerNum == MG_Globals.I.curPlayerOnTurn) {
							foreach (MG_ClassAura aL in MG_ControlAura.I.listAura) {
								if (aL.ownerID == unit.unitID) {
									aL.sprite.transform.position = new Vector3 (aL.sprite.transform.position.x, aL.sprite.transform.position.y, unit.sprite.transform.position.z);
								}
							}
						}
					}
				break;
			}
		}else{
			// Point has no unit
			switch (MG_Globals.I.mode) {
				case "in map":
					// Change selected unit
					MG_Globals.I.selectedUnit = null;
					MG_Globals.I.selectedUnit_exist = false;
					MG_ControlCommand.I._changeCommandsAndUI ();
				break;
			}
		}

		MG_UIControl_UnitData.I._setUIData (posX, posY);
		MG_UIControl_Command.I._update_CMDBTNSprites();
	}
	#endregion
}
