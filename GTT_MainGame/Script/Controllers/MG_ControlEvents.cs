using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlEvents : MonoBehaviour {
	public static MG_ControlEvents I;
	public void Awake(){ I = this; }

	public struct cusEvent{
		public string type;
		public string[] data;
		public int id;
	};

	private int evtID, pref;
	public List<cusEvent> events;
	public List<int> toRemove;

	public int storage1, storage2, storage3;								// Used by certain events
	public int storageCamp1, storageCamp2, storageCamp3;					// Only to be used for campaign purposes (to count something, etc..)

	#region "Add event and update loop"
	public void _start(){
		//Variables
		events 					= new List<cusEvent>();
		toRemove 				= new List<int>();
	}

	public void _addEvent(string newEvt_Type, string[] newEvt_Data){
		cusEvent tempEvt = new cusEvent();
		tempEvt.type = newEvt_Type;
		tempEvt.data = new string[7];
		int length = newEvt_Data.Length;

		for(int i = 0; i < newEvt_Data.Length; i++) 		{tempEvt.data[i] = newEvt_Data[i];}
		for(int i = length; i < 7; i++) 					tempEvt.data[i] = "NONE";

		tempEvt.id = evtID;
		evtID++;

		events.Add(tempEvt);
	}

	public void _update(float deltaTime){
		if (events.Count >= 1) {
			for (int iL = 0; iL < events.Count; iL++) {
				switch (events [iL].type) {
					case "Wait":
						_cusEvent_Wait (iL, deltaTime);
					break;
				}
			}
		}
	}
	#endregion

	// LOOP(update per frame) and FIRE(event is triggered) goes here
	#region "All event LOOP and/or FIRE methods"
	#region "Wait"
	public void _cusEvent_Wait(int evt_Index, float deltaTime){
		/*Type = "Wait", ARG1 = wait time, ARG2 = output number*/
		int evt_Output;
		float evt_waitTime;
		float.TryParse(events[evt_Index].data[0], out evt_waitTime);
		int.TryParse(events[evt_Index].data[1], out evt_Output);
		evt_waitTime -= deltaTime;
		if(evt_waitTime <= 0){
			_addToDestroyList(events[evt_Index]);
			_customTrigger_Fire(evt_Output, evt_Index);
		}else{
			events[evt_Index].data[0] = evt_waitTime.ToString();
		}
	}
	#endregion
	#region "Unit Dies (called from MG_CALC_Damage)"
	public void _cusEvent_UnitDies(int targetUnitID){
		/*Type = "UnitDies", ARG1 = unit ID, ARG2 = output number*/
		int targIndex, evtOutput;
		for (int iL = 0; iL < events.Count; iL++) {
			switch (events [iL].type) {
				case "UnitDies":
					int.TryParse (events [iL].data [0], out targIndex);
					int.TryParse (events [iL].data [1], out evtOutput);
					if (targetUnitID == targIndex) {
						_addToDestroyList(events[iL]);
						_customTrigger_Fire(evtOutput, iL);
						// break;
					}
				break;
			}
		}
	}
	#endregion
	#region "Dialog Ends"
	public void _cusEvent_DialogEnd(int dialogNum){
		/*Type = "DialogEnd", ARG1 = last dialog num, ARG2 = output number*/
		int targIndex, evtOutput;
		for (int iL = 0; iL < events.Count; iL++) {
			switch (events [iL].type) {
				case "DialogEnd":
					int.TryParse (events [iL].data [0], out targIndex);
					int.TryParse (events [iL].data [1], out evtOutput);
					if (dialogNum == targIndex) {
						_addToDestroyList(events[iL]);
						_customTrigger_Fire(evtOutput, iL);
						break;
					}
				break;
			}
		}
	}
	#endregion
	#region "Unit enters region"
	public void _cusEvent_UnitEntersRegion(MG_ClassUnit unit){
		/*Type = "DialogEnd", ARG1 = boxPosX1, ARG2 = boxPosX2, ARG3 = boxPosY1, ARG4 = boxPosY2, ARG5 = output number*/
		/*Called from MG_ClassUnit class, each time a unit moves/is moved*/
		/*Note that you have to manually add the event to destroy list when done*/
		/*Auto-remove is disabled for this event*/
		int targIndex, boxPosX1, boxPosX2, boxPosY1, boxPosY2, evtOutput;
		for (int iL = 0; iL < events.Count; iL++) {
			switch (events [iL].type) {
				case "UnitEnterRegion":
					int.TryParse (events [iL].data [0], out boxPosX1);
					int.TryParse (events [iL].data [1], out boxPosX2);
					int.TryParse (events [iL].data [2], out boxPosY1);
					int.TryParse (events [iL].data [3], out boxPosY2);
					int.TryParse (events [iL].data [4], out evtOutput);

					if (unit.posX >= boxPosX1 && unit.posX <= boxPosX2 && unit.posY <= boxPosY1 && unit.posY >= boxPosY2 && unit.isAlive) {
						// _addToDestroyList (events [iL]);
						storage1 = unit.unitID;
						_customTrigger_Fire (evtOutput, iL);
					}
				break;
			}
		}
	}
	#endregion
	#region "Turn number reached"
	public void turnNumberReached(int turnNum){
		/*Type = "TurnNumberReached", ARG1 = targetNum, ARG2 = output number*/
		/*Called from MG_ControlPlayer's _endTurn function*/
		int turnReq, evtOutput;
		for (int iL = 0; iL < events.Count; iL++) {
			switch (events [iL].type) {
				case "TurnNumberReached":
					int.TryParse (events [iL].data [0], out turnReq);
					int.TryParse (events [iL].data [1], out evtOutput);

					if (turnReq == turnNum) {
						_addToDestroyList (events [iL]);
						_customTrigger_Fire (evtOutput, iL);
					}
				break;
			}
		}
	}
	#endregion
	#endregion

	#region "Custom trigger fire"
	private void _customTrigger_Fire(int trigNumber, int evtIndex){
		switch(trigNumber){
			case 1: /*Debug.Log ("Test");*/ break;
			/*Dialog options window*/ 						case 4: _trigger_4(trigNumber, evtIndex); break;
			/*Turn Change UI - Hide*/ 						case 5: _trigger_5(trigNumber, evtIndex); break;
			/*Aura - Reinclude buffs*/ 						case 6: _trigger_6(); break;
			/*Disarm Shot - Extra shots*/ 					case 7: _trigger_7(trigNumber, evtIndex); break;
			/*Quick Draw - Extra shots*/ 					case 8: _trigger_8(trigNumber, evtIndex); break;
			/*Delayed SFX*/ 								case 9: _trigger_9(trigNumber, evtIndex); break;
			/*Delayed Sound*/ 								case 10: _trigger_10(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 1 - Aleric Dies*/ 			case 11: _trigger_11(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 1 - Dialog ends*/ 			case 12: _trigger_12(trigNumber, evtIndex); break;
			/*STORY GENERAL - Victory*/ 					case 13: _trigger_13(trigNumber, evtIndex); break;
			/*Burst Fire - Extra shots*/ 					case 14: _trigger_14(trigNumber, evtIndex); break;
			/*Burst Fire - Extra shots*/ 					case 15: _trigger_15(trigNumber, evtIndex); break;
			/*Demonic Slash - Extra slashes*/ 				case 16: _trigger_16(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 2 - Ifreet Dies*/ 			case 17: _trigger_17(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 2 - Rag. Dies, Ifreet moves*/ case 18: _trigger_18(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 2 - Dialog ends*/ 			case 19: _trigger_19(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 3 - Trucks enter gate*/ 		case 20: _trigger_20(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 3 - A Truck dies*/ 			case 21: _trigger_21(trigNumber, evtIndex); break;
			/*STORY GENERAL - Defeat*/ 						case 22: _trigger_22(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 3 - Enemy Reinforcements*/ 	case 23: _trigger_23(trigNumber, evtIndex); break;
			/*STORY 1 MISSION 3 - Checkpoint*/ 				case 24: _trigger_24(trigNumber, evtIndex); break;
			/*STORY 2 MISSION 1 - Drakgul Dies*/ 			case 25: _trigger_25(trigNumber, evtIndex); break;
			/*STORY 2 MISSION 2 - An enemy hero Dies*/ 		case 26: _trigger_26(trigNumber, evtIndex); break;
			/*STORY 2 MISSION 3 - An enemy hero Dies*/ 		case 27: _trigger_27(trigNumber, evtIndex); break;

			/*MISSION RAGNAROS - Enemy Reinforcements*/ 	case 28: _trigger_28(trigNumber, evtIndex); break;
			/*MISSION RAGNAROS - Ragnaros Dies*/ 			case 29: _trigger_29(trigNumber, evtIndex); break;

			/*MISSION SPEC WITCH - SpecWitch Dies*/ 		case 30: _trigger_30(trigNumber, evtIndex); break;
			/*MISSION IFREET - Ifreet Dies*/ 				case 31: _trigger_31(trigNumber, evtIndex); break;
			
			/*Lightning Bolt - Extra shots*/ 				case 32: _trigger_32(trigNumber, evtIndex); break;
		}
	}
	#endregion

	#region "Triggers"
	#region "#4 - Dialog options window"
	// Dialog options window
	private void _trigger_4(int trigNumber, int evtIndex){
		string[] options = new string[4];
		for(int i = 2; i <= 5; i++){
			options[i-2] = events[evtIndex].data[i];
		}

		MG_UIControl_Dialog.I._createDialogOptions(options);
	}
	#endregion
	#region "#5 - Turn Change UI - Hide"
	// Turn Change UI - Hide
	private void _trigger_5(int trigNumber, int evtIndex){
		MG_UIControl_TurnChange.I._hide ();
	}
	#endregion
	#region "#6 - Aura - Reinclude buffs"
	// Turn Change UI - Hide
	private void _trigger_6() {
		MG_ControlAura.I._reincludeBuffs(true);
	}
	#endregion
	#region "#7 - Disarm shot - extra shots"
	private void _trigger_7(int trigNumber, int evtIndex) {
		int uId = -1;
		int.TryParse(events[evtIndex].data[2], out uId);
		if (MG_GetUnit.I._checkIfUnitWithIDExists (uId)) {
			MG_ClassUnit caster = MG_GetUnit.I._getUnitWithID (uId);

			int posX = 0, posY = 0;
			int.TryParse(events[evtIndex].data[3], out posX);
			int.TryParse(events[evtIndex].data[4], out posY);
			MG_ControlMissile.I._createMissile("DisarmShot", caster.posX, caster.posY, posX, posY, caster.unitID);
		}
	}
	#endregion
	#region "#8 - Quick Draw - extra shots"
	private void _trigger_8(int trigNumber, int evtIndex) {
		int uId = -1;
		int.TryParse(events[evtIndex].data[2], out uId);
		if (MG_GetUnit.I._checkIfUnitWithIDExists (uId)) {
			MG_ClassUnit caster = MG_GetUnit.I._getUnitWithID (uId);

			int posX = 0, posY = 0;
			int.TryParse(events[evtIndex].data[3], out posX);
			int.TryParse(events[evtIndex].data[4], out posY);
			MG_ControlMissile.I._createMissile("QuickDraw", caster.posX, caster.posY, posX, posY, caster.unitID);
		}
	}
	#endregion
	#region "#9 - Delayed SFX"
	private void _trigger_9(int trigNumber, int evtIndex) {
		int uId = -1;

		int posX = 0, posY = 0;
		int.TryParse(events[evtIndex].data[3], out posX);
		int.TryParse(events[evtIndex].data[4], out posY);
		MG_ControlSFX.I._createSFX(events[evtIndex].data[2], posX, posY);
	}
	#endregion
	#region "#10 - Delayed Soound"
	private void _trigger_10(int trigNumber, int evtIndex) {
		int uId = -1;

		int posX = 0, posY = 0, soundId = 0;
		int.TryParse (events [evtIndex].data [2], out soundId);
		int.TryParse(events[evtIndex].data[3], out posX);
		int.TryParse(events[evtIndex].data[4], out posY);
		MG_ControlSounds.I._playSound(soundId, posX, posY);
	}
	#endregion
	#region "#11 - STORY 1 MISSION 1 - Aleric Dies"
	private void _trigger_11(int trigNumber, int evtIndex) {
		MG_CALC_Damage.I.bypassKillCode = true;
		MG_UIControl_Dialog.I._initDialog (11);
	}
	#endregion
	#region "#12 - STORY 1 MISSION 1 - Test dialog end"
	private void _trigger_12(int trigNumber, int evtIndex) {
		
	}
	#endregion
	#region "#13 - STORY GENERAL - Victory"
	private void _trigger_13(int trigNumber, int evtIndex) {
		MG_UIControl_Popup.I.callVictory (MG_Globals.I.players[1], 5);
	}
	#endregion
	#region "#14 - Burst Fire (Main) - extra shots"
	private void _trigger_14(int trigNumber, int evtIndex) {
		int uId = -1;
		int.TryParse(events[evtIndex].data[2], out uId);
		if (MG_GetUnit.I._checkIfUnitWithIDExists (uId)) {
			MG_ClassUnit caster = MG_GetUnit.I._getUnitWithID (uId);

			int posX = 0, posY = 0;
			int.TryParse(events[evtIndex].data[3], out posX);
			int.TryParse(events[evtIndex].data[4], out posY);
			MG_ControlMissile.I._createMissile("BurstFireMain", caster.posX, caster.posY, posX, posY, caster.unitID);
		}
	}
	#endregion
	#region "#15 - Burst Fire (Main) - extra shots"
	private void _trigger_15(int trigNumber, int evtIndex) {
		int uId = -1;
		int.TryParse(events[evtIndex].data[2], out uId);
		if (MG_GetUnit.I._checkIfUnitWithIDExists (uId)) {
			MG_ClassUnit caster = MG_GetUnit.I._getUnitWithID (uId);

			int posX = 0, posY = 0;
			int.TryParse(events[evtIndex].data[3], out posX);
			int.TryParse(events[evtIndex].data[4], out posY);
			MG_ControlMissile.I._createMissile("BurstFire", caster.posX, caster.posY, posX, posY, caster.unitID);
		}
	}
	#endregion
	#region "#16 - Demonic Slash - Extra slashes"
	private void _trigger_16(int trigNumber, int evtIndex) {
		int uId = -1;
		int.TryParse(events[evtIndex].data[2], out uId);
		if (MG_GetUnit.I._checkIfUnitWithIDExists (uId)) {
			MG_ClassUnit caster = MG_GetUnit.I._getUnitWithID (uId);

			int posX = 0, posY = 0;
			int.TryParse(events[evtIndex].data[3], out posX);
			int.TryParse(events[evtIndex].data[4], out posY);
			MG_ControlMissile.I._createMissile("WindSlashBlue", caster.posX, caster.posY, posX, posY, caster.unitID);
		}
	}
	#endregion
	#region "#17 - STORY 1 MISSION 2 - Ifreet Dies"
	private void _trigger_17(int trigNumber, int evtIndex) {
		MG_CALC_Damage.I.bypassKillCode = true;
		MG_UIControl_Dialog.I._initDialog (11);
	}
	#endregion
	#region "#18 - STORY 1 MISSION 2 - Ragnaros Dies, Ifreet begins to move"
	private void _trigger_18(int trigNumber, int evtIndex) {
		AI__BOSS_Ifreet.I.canMove = true;
	}
	#endregion
	#region "#19 - STORY 1 MISSION 2 - Final dialog ends, Mission ends"
	private void _trigger_19(int trigNumber, int evtIndex) {
		MG_UIControl_Popup.I.callVictory (MG_Globals.I.players[1], 5);
	}
	#endregion
	#region "#20 - STORY 1 MISSION 3 - Trucks enter gate"
	private void _trigger_20(int trigNumber, int evtIndex) {
		if (!MG_GetUnit.I._checkIfUnitWithIDExists (storage1)) return;

		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (storage1);
		if (unit.name == "ArcanyteTruck") {
			MG_ControlUnits.I._addToDestroyList(unit);

			// Check if there's any trucks left
			bool hasTrucks = false;
			for(int i = 0; i < MG_Globals.I.units.Count; i++){
				if (MG_Globals.I.units [i].name == "ArcanyteTruck" && MG_Globals.I.units [i] != unit) {
					hasTrucks = true;
					break;
				}
			}

			// Show dialog depending if player still has trucks
			if (hasTrucks) {
				MG_UIControl_Dialog.I._initDialog (8);
			} else {
				MG_UIControl_Dialog.I._initDialog (9);
			}

			// Re-add the event (since it's destroyed once triggered)
			_addEvent ("UnitEnterRegion", new string[]{"-17", "-8", "13", "6", "20"});
		}

		storage1 = 0;
	}
	#endregion
	#region "#21 - STORY 1 MISSION 3 - A Truck dies"
	private void _trigger_21(int trigNumber, int evtIndex) {
		MG_CALC_Damage.I.bypassKillCode = true;
		MG_UIControl_Dialog.I._initDialog (12);
	}
	#endregion
	#region "#22 - STORY GENERAL - Defeat"
	private void _trigger_22(int trigNumber, int evtIndex) {
		MG_UIControl_Popup.I.callVictory (MG_Globals.I.players[1], 6);
	}
	#endregion
	#region "#23 - STORY 1 MISSION 3 - Enemy Reinforcements"
	private void _trigger_23(int trigNumber, int evtIndex) {
		MG_ControlUnits.I._createUnit("GoblinArcher", -14, 4, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_ControlUnits.I._createUnit("Goblin", -13, 3, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));

		MG_ControlUnits.I._createUnit("Goblin", -17, 13, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_ControlUnits.I._createUnit("Goblin", -17, 11, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));

		MG_ControlUnits.I._createUnit("Goblin", -17, -10, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
		MG_ControlUnits.I._createUnit("Goblin", -17, -14, 2);
		MG_ControlAI.I.createAutoWaypoint (MG_GetUnit.I.getLastCreatedUnit(), new Vector2(-11, -10));
	}
	#endregion
	#region "#24 - STORY 1 MISSION 3 - Checkpoint"
	private void _trigger_24(int trigNumber, int evtIndex) {
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (storage1);
		if (unit.name == "ArcanyteTruck") {
			_addToDestroyList (events [evtIndex]);
			MG_UIControl_Dialog.I._initDialog (14);
		}
	}
	#endregion
	#region "#25 - STORY 2 MISSION 1 - Drakgul Dies"
	private void _trigger_25(int trigNumber, int evtIndex) {
		MG_CALC_Damage.I.bypassKillCode = true;
		MG_UIControl_Dialog.I._initDialog (14);
	}
	#endregion
	#region "#26 - STORY 2 MISSION 2 - An enemy hero dies"
	private void _trigger_26(int trigNumber, int evtIndex) {
		storageCamp1--;

		if (storageCamp1 <= 0) {
			MG_CALC_Damage.I.bypassKillCode = true;
			MG_UIControl_Dialog.I._initDialog (14);
		}
	}
	#endregion
	#region "#27 - STORY 2 MISSION 3 - An enemy hero dies"
	private void _trigger_27(int trigNumber, int evtIndex) {
		storageCamp1--;

		if (storageCamp1 <= 0) {
			MG_CALC_Damage.I.bypassKillCode = true;
			MG_UIControl_Dialog.I._initDialog (16);
		}
	}
	#endregion

	#region "#28 - MISSION RAGNAROS - Enemy Reinforcements"
	private void _trigger_28(int trigNumber, int evtIndex) {
		MG_ControlUnits.I._createUnit("Rifleman", 0, 10, 2);
		MG_ControlUnits.I._createUnit("Officer", 2, 10, 2);

		MG_ControlUnits.I._createUnit("Rifleman", 0, -10, 2);
		MG_ControlUnits.I._createUnit("Officer", 2, -10, 2);
	}
	#endregion
	#region "#29 - MISSION RAGNAROS - Ragnaros Dies"
	private void _trigger_29(int trigNumber, int evtIndex) {
		ZPlayerPrefs.SetInt ("cardUnlocked_Viking", 1);
		ZPlayerPrefs.SetInt ("cardUnlocked_VikingRaider", 1);
	}
	#endregion
	#region "#30 - MISSION SPECTRAL WITCH - Spec Witch Dies"
	private void _trigger_30(int trigNumber, int evtIndex) {
		ZPlayerPrefs.SetInt ("cardUnlocked_Spectre", 1);
	}
	#endregion
	#region "#31 - MISSION IFREET - Ifreet Dies"
	private void _trigger_31(int trigNumber, int evtIndex) {
		ZPlayerPrefs.SetInt ("cardUnlocked_FireSpawn", 1);
		ZPlayerPrefs.SetInt ("cardUnlocked_FireElemental", 1);
	}
	#endregion
	#region "#32 - Lightning Bolt - extra shots"
	private void _trigger_32(int trigNumber, int evtIndex) {
		int uId = -1;
		int.TryParse(events[evtIndex].data[2], out uId);
		if (MG_GetUnit.I._checkIfUnitWithIDExists (uId)) {
			MG_ClassUnit caster = MG_GetUnit.I._getUnitWithID (uId);

			int posX = 0, posY = 0;
			int.TryParse(events[evtIndex].data[3], out posX);
			int.TryParse(events[evtIndex].data[4], out posY);
			MG_ControlMissile.I._createMissile("LightningBolt", caster.posX, caster.posY, posX, posY, caster.unitID);
		}
	}
	#endregion
	#endregion

	#region "Destroy codes"
	public void _addToDestroyList(cusEvent targetEvt){
		toRemove.Add(targetEvt.id);
	}

	public void _destroyListed(){
		if(toRemove.Count > 0){
			for(int listedEvt = 0; listedEvt < toRemove.Count; listedEvt++){
				_destroyEvt(listedEvt);
			}
			toRemove.Clear();
		}
	}

	public void _destroyEvt(int targetEvt){
		int indexToRemove = -1;
		for(int desUnitLoop = events.Count - 1; desUnitLoop >= 0; desUnitLoop--){
			if(toRemove[targetEvt] == events[desUnitLoop].id){
				indexToRemove = desUnitLoop;
				break;
			}
		}

		if(indexToRemove > -1){
			events.RemoveAt(indexToRemove);
		}
	}
	#endregion
}
