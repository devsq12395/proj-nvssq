using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_ControlCommand : MonoBehaviour {
	public static MG_ControlCommand I;
	public void Awake(){ I = this; }

	/*
	 * 	LIST OF MODES: (This list can be found on MG_Globals and MG_ControlCommand
	 * 	
	 * 	in map					- selecting a unit to give order to
	 * 	aim friendly			- currently opn targeting mode with friendly targeters
	 */

	public string btn1_order, btn2_order, btn3_order, btn4_order, btn5_order;

	// Some special variables, can be used by skills
	public int speInt_1, speInt_2, speInt_3, speInt_4;
	public string speStr_1, speStr_2, speStr_3, speStr_4;
	public float speFloat_1, speFloat_2, speFloat_3, speFloat_4;
	public bool globalCast = false;

	public bool CHEAT_INFINTE_MOVE = false;

	public void _start(){
		
	}

	// This will change commands stored in btn1_order strings
	// based from the current mode (MG_Globals.I.mode)
	#region "Change Commands and UI"
	public void _changeCommandsAndUI(){
		if (MG_Globals.I.selectedUnit_exist) {
			MG_ClassUnit selUnit = MG_Globals.I.selectedUnit;

			switch (MG_Globals.I.curCommand) {
				case "in map":
					btn1_order = selUnit.btn1_orderDef;
					btn2_order = selUnit.btn2_orderDef;
					btn3_order = selUnit.btn3_orderDef;
					btn4_order = selUnit.btn4_orderDef;
					btn5_order = selUnit.btn5_orderDef;
				break;
			}
		} else {
			btn1_order = "none";
			btn2_order = "none";
			btn3_order = "none";
			btn4_order = "end turn";
		}

		// Update UI
		MG_UIControl_Command.I._update_CMDBTNSprites();
	}
	#endregion

	// Command database also goes here
	// When an order is issued, this method is called
	// Special commands use -1 as command number
	#region "Issue command"
	public void _issueCommand(int commandNum, string specialComm = ""){
		MG_ClassUnit selectedUnit = MG_Globals.I.selectedUnit;

		#region "Conditions"
		// Bypass all conditions if player is not in turn
		// Will mostly be used on multiplayer
		if(MG_Globals.I.curPlayerOnTurn == MG_Globals.I.curPlayerNum){
			// condition - PAUSE (something is moving)
			if(MG_Globals.I.pause_uiMove || MG_Globals.I.pause_objMove_dur.Count > 0)	return;

			// condition - PAUSE (a window is open)
			if(MG_Globals.I.pause_windowOpen || MG_Globals.I.pause_gamePause || MG_Globals.I.pause_gameOver || MG_UIControl_Popup.I.inPopup || MG_Globals.I.pause_itemBuying)		return;
		}
		#endregion
		#region "Get Command Name"
		string command = "move"; // default value
		if (commandNum > 0) {
			if(MG_UIControl_Action.I.isShow) { 
				// Command is action from action bar
				command = MG_UIControl_Action.I.actions [commandNum-1];
				if(command == "NONE" || command == "" || command == "Passive") return;

				MG_UIControl_Action.I.hide (true);
				MG_UIControl_Action.I.isAction = true;
				MG_UIControl_Action.I.secAct = commandNum-1;
			}else{
				// Classic regular commands
				switch (commandNum) {
					case 1:command = btn1_order;break;
					case 2:command = btn2_order;break;
					case 3:command = btn3_order;break;
					case 4:command = btn4_order;break;
					case 5:command = btn5_order;break;
				}
			}
		} else {
			command = specialComm;
		}
		#endregion
		#region "Sound"
		MG_ControlSounds.I._playSound(2, 0, 0, false);
		#endregion

		switch (command) {
			#region "Friendly targeters"

				#region "Move"
				case "move":
					// Conditions
					if(!selectedUnit.action_move){
						MG_UIControl_Announcer.I._announce ("This unit has already moved"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					//if(MG_ControlBuffs.I._unitIsStunned(selectedUnit)){
						//MG_UIControl_Announcer.I._announce ("This unit is dizzy"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						//return;
					//}
					if(MG_ControlBuffs.I._unitCannotMove(selectedUnit)){
						MG_UIControl_Announcer.I._announce ("This unit cannot move"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					int MS = selectedUnit.MS + MG_ControlBuffs.I.CALC_bonusMS(selectedUnit);
					if(MS > MG_Globals.I.MAX_MS)	MS = MG_Globals.I.MAX_MS;
					if(!selectedUnit.isFlying){
						MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, MS, true, true);
					}else{
						MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, MS);
					}
					MG_UIControl_Announcer.I._announce ("Select move target", 0, true);

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
              	#endregion
				#region "Summon Hero"
				case "summon hero":
					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 3, true, true);
					MG_UIControl_Announcer.I._announce ("Select summon area", 0, true);

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Hire Unit"
				case "hire unit":
					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 3, true, true);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Cast Spell"
				case "cast spell":
					// Effect
					MG_ControlTargeters.I._createTarg_Square(500, 500, 2, true, true);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);
					MG_ControlCursor.I._changeCursorSprite("Area2");

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Hookshot"
			case "Hookshot":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 5){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim friendly";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Hook Dance"
			case "HookDance":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 10){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim friendly";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Scout"
				case "Scout":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 20){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 11);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Recruit"
				case "Recruit":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 50){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 3);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim friendly";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "EyeOfTheMagi"
				case "EyeOfTheMagi":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 3);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Dispel"
				case "Dispel":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Concentrate"
				case "Concentrate":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Heal / Potion"
				case "Heal":case "Potion":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 10){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, (command == "Heal") ? 5 : 3, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Shell"
				case "Shell":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area2");
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Blessing"
				case "Blessing":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 60){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Summon Spectre"
				case "SummonSpectre":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Blink"
				case "Blink":case "BlinkSpectre":case "Gallop":case "Skirmish":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 0){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 2);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Spiral"
				case "Spiral":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 25){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 3);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);
					MG_ControlCursor.I._changeCursorSprite("Area2");

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Vigor"
				case "Vigor":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 4, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Test of Faith"
				case "TestOfFaith":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Root Armor"
				case "RootArmor":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Fairies"
				case "Fairies":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 4, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Phoenix Wings"
				case "PhoenixWings":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 25){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 5);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Sacred Knights"
				case "SacredKnights":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Healing Spray"
				case "HealingSpray":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 5){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area2");
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 3, false, false, true);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "testSkill1"
				case "testSkill1":
					// Conditions
					if(!selectedUnit.action_skill){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(false, selectedUnit.posX, selectedUnit.posY, 6, false, false, true);

					// Finishing
					MG_UIControl_Skill.I._hide(); // for skill
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim friendly";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion

			#endregion
			#region "Hostile targeters"

				#region "Attack"
				case "attack":
					// Conditions
					if(!selectedUnit.action_atk){
						MG_UIControl_Announcer.I._announce ("This unit has already attacked"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_ControlBuffs.I._unitIsStunned(selectedUnit)){
						MG_UIControl_Announcer.I._announce ("This unit is dizzy"); MG_ControlSounds.I._playSound(49, 0, 0, false); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_ControlBuffs.I._unitCannotAttack(selectedUnit)){
						MG_UIControl_Announcer.I._announce ("This unit cannot attack"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_UIControl_Announcer.I._announce ("Select attack target", 0, true);
					if(selectedUnit.atkRange > 1)
						MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, selectedUnit.atkRange + MG_ControlBuffs.I.CALC_bonusAtkRange(selectedUnit));
					else if(selectedUnit.atkRange == 1)
						MG_ControlTargeters.I._createTarg_Square(selectedUnit.posX, selectedUnit.posY, 1, true);

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Armor Break"
			case "ArmorBreak":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 50){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_UIControl_Announcer.I._announce ("Select target", 0, true);
				MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 2);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Spear Toss"
			case "SpearToss":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 5){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Multi Shot"
				case "MultiShot":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area2");
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 7);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Close Combat"
			case "CloseCombat":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 75){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlTargeters.I._createTarg_Square(selectedUnit.posX, selectedUnit.posY, 1, true);
				MG_UIControl_Announcer.I._announce ("Select target", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Bayonet"
			case "Bayonet":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlTargeters.I._createTarg_Square(selectedUnit.posX, selectedUnit.posY, 1, true);
				MG_UIControl_Announcer.I._announce ("Select target", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Quick Draw"
				case "QuickDraw":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 10){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Dynamite"
			case "Dynamite":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 50){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlCursor.I._changeCursorSprite("Area2");
				MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Disarming Shot"
			case "DisarmShot":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 7){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlCursor.I._changeCursorSprite("Area1");
				MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Lightning Bolt"
			case "LightningBolt":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 5){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlCursor.I._changeCursorSprite("Area1");
				MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
			break;
				#endregion
				#region "Burning Grip"
				case "BurningGrip":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 75){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Smite"
				case "Smite":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 5);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Heavy Charge"
				case "HeavyCharge":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 5);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Wind Slash"
				case "WindSlash":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Entangle"
				case "Entangle":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 5);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Frost Nova"
				case "FrostNova":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area2");
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Silence"
				case "Silence":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area1");
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Arcane Bolt"
				case "ArcaneBolt":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 75){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Blink Strike"
				case "BlinkStrike":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Photon Bomb"
				case "PhotonBomb":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(selectedUnit.MP < 50){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlCursor.I._changeCursorSprite("Area2");
				MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 7);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Multi Strike"
				case "MultiStrike":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 2);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Axe Throw"
				case "AxeThrow":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Axe Throw (Boss)"
				case "AxeThrow(Boss)":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 100){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Mighty Slam/Mighty Slam (Boss)"
				case "MightySlam":case "MightySlam(Boss)":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < ((command == "MightySlam") ? 50 : 100)){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);
					MG_ControlCursor.I._changeCursorSprite("Area2");

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Burst Fire"
				case "BurstFire":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 40){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area1");
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Mythril Revolver"
				case "MythrilRevolver":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 10){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Death Arrow"
				case "DeathArrow":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 25){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Drain Life"
				case "DrainLife":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Grenade"
				case "Grenade":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 10){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area1");
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 6);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Artillery"
				case "Artillery":
				// Conditions
				if(!selectedUnit.action_skill){
					MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}

				// Effect
				MG_ControlCursor.I._changeCursorSprite("Area2");
				//MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 7);
				MG_UIControl_Announcer.I._announce ("Select target area", 0, true);
				MG_ControlTargeters.I._createTarg_Square(500, 500, 2, true, true);
				globalCast = true;

				// Finishing
				MG_UIControl_Skill.I._hide();
				MG_Globals.I.mode = command;
				MG_Globals.I.curCommand = "aim hostile";
				btn1_order = "confirm";
				btn2_order = "cancel";
				btn3_order = "none";
				btn4_order = "none";btn5_order = "cancel(hidden)";
				MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Grapeshot"
				case "Grapeshot":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 5){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlCursor.I._changeCursorSprite("Area1");
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 7);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Acid Breath"
				case "AcidBreath":
					// Conditions
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(selectedUnit.MP < 10){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}

					// Effect
					MG_ControlTargeters.I._createTargField(true, selectedUnit.posX, selectedUnit.posY, 4);
					MG_UIControl_Announcer.I._announce ("Select target area", 0, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "testSkill1.1"
				case "testSkill1.1":
					// This is a SPECIAL COMMAND so no condition checks

					// Effect
					MG_ControlTargeters.I._createTargField(true, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, 9, false, true);
					MG_ControlTargeters.I._clearTargeters();
					speInt_1 = MG_ControlCursor.I.posX;
					speInt_2 = MG_ControlCursor.I.posY;

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "aim hostile";
					btn1_order = "confirm";
					btn2_order = "cancel";
					btn3_order = "none";
					btn4_order = "none";btn5_order = "cancel(hidden)";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion

			#endregion
			#region "Instants"

				#region "End Turn"
				case "end turn":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					gameAction_endTurn();
					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_endTurn ();
					}
				break;
				#endregion
				#region "Skill (OLD and UNUSED)"
				case "skill(old)":
					// Conditions
					if(MG_Globals.I.pause_uiMove){
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_ControlBuffs.I._unitIsStunned(selectedUnit)){
						MG_UIControl_Announcer.I._announce ("This unit is dizzy"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("This unit already used a skill"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					
					btn1_order = selectedUnit.skill1;
					btn2_order = selectedUnit.skill2;
					btn3_order = selectedUnit.skill3;
					MG_UIControl_Skill.I._updateUI();
					MG_UIControl_Skill.I._show();

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "skill";
					btn5_order = "cancel(hidden)";
					//MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Skill"
				case "skill":
					// Conditions
					if(MG_Globals.I.pause_uiMove){
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_ControlBuffs.I._unitIsStunned(selectedUnit)){
						MG_UIControl_Announcer.I._announce ("This unit is dizzy"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Already used a skill!"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					
					btn1_order = selectedUnit.skill1;
					btn2_order = selectedUnit.skill2;
					btn3_order = selectedUnit.skill3;
					btn5_order = "cancel(hidden)";
					MG_UIControl_Action.I.show(selectedUnit);

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "skill";
					//MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Summon"
				case "summon":
					// Conditions
					if(MG_Globals.I.pause_uiMove){
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(!selectedUnit.action_atk){
						MG_UIControl_Announcer.I._announce ("Already summoned a hero this turn.");  MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					MG_UIControl_Summon.I._show();

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "summon";
					btn1_order = "";
					btn2_order = "";
					btn3_order = "";
					btn4_order = "";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Hire"
				case "hire":
					// Conditions
					if(MG_Globals.I.pause_uiMove){
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					MG_UIControl_Barracks.I.show();

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "hire";
					btn1_order = "";
					btn2_order = "";
					btn3_order = "";
					btn4_order = "";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Cast"
				case "cast":
					// Conditions
					if(MG_Globals.I.pause_uiMove){
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					MG_UIControl_SpellTower.I.show();

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "cast";
					btn1_order = "";
					btn2_order = "";
					btn3_order = "";
					btn4_order = "";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Cards"
				case "cards":
					// Conditions
					if(MG_Globals.I.pause_uiMove){
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(!selectedUnit.action_card){
						MG_UIControl_Announcer.I._announce ("Already used a card!"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					MG_UIControl_Cards.I.show();

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "summon";
					btn1_order = "";
					btn2_order = "";
					btn3_order = "";
					btn4_order = "";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Recruit (Unit Shop)"
				case "recruit":
					// Conditions
					if(MG_Globals.I.pause_uiMove){
						return;
					}
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					MG_UIControl_UnitShop.I.show();

					// Finishing
					MG_Globals.I.mode = command;
					MG_Globals.I.curCommand = "summon";
					btn1_order = "";
					btn2_order = "";
					btn3_order = "";
					btn4_order = "";
					MG_UIControl_Command.I._update_CMDBTNSprites();
				break;
				#endregion
				#region "Blade Dance"
				case "BladeDance":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 7){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 7; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 2){
								MG_CALC_Damage.I._damageUnit(selectedUnit, u, 20, "magic");
								MG_ControlSFX.I._createSFX ("hit01", u.posX, u.posY);
								MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY); 
							}
						}
					}
					for(int i = 1; i <= 7; i++){
						MG_ControlSFX.I._createTimer(1, 0.15f * i, selectedUnit.posX, selectedUnit.posY);
						MG_ControlSFX.I._createTimer(1, 0.15f * i, selectedUnit.posX, selectedUnit.posY);
						MG_ControlSFX.I._createTimer(1, 0.15f * i, selectedUnit.posX, selectedUnit.posY);
						MG_ControlSFX.I._createTimer(1, 0.15f * i, selectedUnit.posX, selectedUnit.posY);
					}
					MG_UIControl_UseSkill.I._show(selectedUnit, "Blade Dance!");
					MG_ControlSounds.I._playSound(30, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_robin_sworddance(selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Azure Dragon"
			case "AzureDragon":
				// Conditions
				if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
					return;
				}
				if(selectedUnit.MP < 75){
					MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
					return;
				}
				if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

				// Effect
				selectedUnit.MP -= 75; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
				selectedUnit._transformSprite("MasamuneAzure", 2);
				MG_ControlBuffs.I._addBuff(selectedUnit, "AzureDragon");
				MG_ControlSounds.I._playSound(27, selectedUnit.posX, selectedUnit.posY, true);

				// Finishing
				MG_UIControl_Skill.I._hide();
				selectedUnit.action_skill = false;
				MG_Globals.I.mode = "in map";
				MG_Globals.I.curCommand = "in map";
				MG_ControlCursor.I._interact (); // Change selected unit and update UI
				MG_UIControl_Announcer.I._clearAnnounce();

				/////// In-Game Multiplayer RPC ///////
				if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
					
				}
			break;
				#endregion
				#region "Mirror"
				case "Mirror":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 75){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 75; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlBuffs.I._addBuff(selectedUnit, "Mirror");
					MG_ControlSFX.I._createSFX ("cartoonPowerup01", selectedUnit.posX, selectedUnit.posY); 
					foreach(MG_ClassUnit un in MG_Globals.I.units){
						if(un.playerOwner == selectedUnit.playerOwner && un.name == "Spectre"){
							MG_ControlBuffs.I._addBuff(un, "Mirror");
							MG_ControlSFX.I._createSFX ("cartoonPowerup01", un.posX, un.posY); 
						}
					}

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_specWitch_mirror (selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Arc Shot"
				case "ArcShot":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 10){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 10; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlBuffs.I._addBuff(selectedUnit, "ArcShot");
					MG_ControlSFX.I._createSFX("cartoonPowerup01", selectedUnit.posX, selectedUnit.posY);
					MG_UIControl_UseSkill.I._show(selectedUnit, "Arc Shot!");
					MG_ControlSounds.I._playSound(27, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_amy_arcShot (selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Fury"
				case "Fury":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					MG_ControlBuffs.I._addBuff(selectedUnit, "Fury");
					MG_ControlSFX.I._createSFX("cartoonExplodeFire02", selectedUnit.posX, selectedUnit.posY);
					MG_ControlSFX.I._createSFX("explodeDust01", selectedUnit.posX, selectedUnit.posY);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
								MG_CALC_Damage.I._damageUnit(selectedUnit, u, 30, "magic");
								MG_ControlSFX.I._createSFX ("cartoonHit04", u.posX, u.posY);
							}
						}
					}
					_specialEffect_outward(selectedUnit.posX, selectedUnit.posY, "cartoonExplodeFire01", 3, 0.2, 0.2);
					_specialEffect_outward(selectedUnit.posX, selectedUnit.posY, "cartoonHit04", 3, 0.2, 0.2);
					MG_UIControl_UseSkill.I._show(selectedUnit, "Fury!");
					MG_ControlSounds.I._playSound(17, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_ragnaros_fury (selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Upgrade2"
				case "Upgrade2":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					int upgCost2 = 0;
					int.TryParse (MG_ControlCommand.I.speStr_4, out upgCost2);
					MG_UIControl_TopBar.I._goldGain (-upgCost2);
					MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold -= upgCost2;
					MG_UIControl_TopBar.I._goldUI_update ();
					
					List<MG_ClassUnit> unitUpgList = new List<MG_ClassUnit>();
					List<Vector2> unitUpgPoint = new List<Vector2>();
					int unitsToUpgNum = 1;
					int.TryParse(speStr_3, out unitsToUpgNum);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(u.name == speStr_1 && u.playerOwner == selectedUnit.playerOwner){
							unitUpgList.Add(u);
							unitUpgPoint.Add(new Vector2(u.posX, u.posY));
							if(unitUpgList.Count >= unitsToUpgNum){
								break;
							}
						}
					}
					for(int i = unitUpgList.Count-1; i >= 0; i--){
						MG_ControlUnits.I._addToDestroyList (unitUpgList[i]);
						MG_ControlUnits.I._createUnit(speStr_2, (int)unitUpgPoint[i].x, (int)unitUpgPoint[i].y, selectedUnit.playerOwner);
						MG_ControlSFX.I._createSFX("cartoonHoly02", (int)unitUpgPoint[i].x, (int)unitUpgPoint[i].y);
					}
					MG_UIControl_UseSkill.I._show(selectedUnit, "Upgrade!");
					MG_UIControl_Cards.I.throwSelCard_toBottomDeck ();

					// Finishing
					if(!selectedUnit.action_card)		selectedUnit.action_card = true;
					else 								MG_ControlBuffs.I._addBuff(selectedUnit, "DoubleCard");
					
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_upgrade2 (selectedUnit.unitID, speStr_1, speStr_2, unitsToUpgNum);
					}
				break;
				#endregion
				#region "All-out Assault & Hold the Line"
				case "All-outAssault":case "HoldTheLine":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 5){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlSFX.I._createSFX("warhornEffect", selectedUnit.posX, selectedUnit.posY);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(!MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
								if(command == "All-outAssault"){
									MG_ControlBuffs.I._addBuff(u, "AllOutAssault");
								}else{
									MG_ControlBuffs.I._addBuff(u, "HoldTheLinePlus");
								}
							}
						}
					}
					if(command == "All-outAssault"){
						MG_UIControl_UseSkill.I._show(selectedUnit, "All-out Assault!");
					}else{
						MG_UIControl_UseSkill.I._show(selectedUnit, "Hold the Line!");
					}
					MG_ControlSounds.I._playSound(32, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						if (command == "All-outAssault") {
							PhotonRoom.room.gameAction_ajax_allOutAssault(selectedUnit.unitID);
						}else{
							PhotonRoom.room.gameAction_ajax_holdTheLine(selectedUnit.unitID);
						}
					}
				break;
				#endregion
				#region "Amplify"
				case "Amplify":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 5){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlSFX.I._createSFX("warhornEffect", selectedUnit.posX, selectedUnit.posY);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(!MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
								MG_ControlBuffs.I._addBuff(u, "Amplify");
							}
						}
					}
					MG_UIControl_UseSkill.I._show(selectedUnit, "Amplify!");
					MG_ControlSounds.I._playSound(32, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_elizabeth_amplify(selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Explosion"
				case "Explosion":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
								MG_CALC_Damage.I._damageUnit(selectedUnit, u, 30, "magic");
								MG_ControlSFX.I._createSFX ("cartoonHit04", u.posX, u.posY);
							}
						}
					}
					_specialEffect_outward(selectedUnit.posX, selectedUnit.posY, "cartoonExplodeFire01", 3, 0.2, 0.2);
					_specialEffect_outward(selectedUnit.posX, selectedUnit.posY, "cartoonHit04", 3, 0.2, 0.2);
					MG_ControlSFX.I._createSFX ("explodeDust01", selectedUnit.posX, selectedUnit.posY);
					MG_UIControl_UseSkill.I._show(selectedUnit, "Explosion!");
					MG_ControlSounds.I._playSound(17, selectedUnit.posX, selectedUnit.posY, true);
					
					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_ifreet_explosion (selectedUnit.unitID);	
					}
				break;
				#endregion
				#region "War Stomp"
				case "WarStomp":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
								MG_CALC_Damage.I._damageUnit(selectedUnit, u, 25, "magic");
								MG_ControlSFX.I._createSFX ("cartoonHit04", u.posX, u.posY);
							}
						}
					}
					_specialEffect_outward(selectedUnit.posX, selectedUnit.posY, "slam01", 3, 0.2, 0.2);
					_specialEffect_outward(selectedUnit.posX, selectedUnit.posY, "slam02", 3, 0.2, 0.2);
					MG_ControlSFX.I._createSFX ("explodeDust01", selectedUnit.posX, selectedUnit.posY);
					MG_UIControl_UseSkill.I._show(selectedUnit, "War Stomp!");
					MG_ControlSounds.I._playSound(43, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_warStomp (selectedUnit.unitID);	
					}
				break;
				#endregion
				#region "Eye Explode"
				case "Explode":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 2){
								MG_CALC_Damage.I._HP_Loss(u, 50);
							}
						}
					}
					_specialEffect_outward(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, "cartoonHit05", 2, 0.2, 0.2);
					MG_UIControl_UseSkill.I._show(selectedUnit, "Explode!");
					selectedUnit.isAlive = false;
					MG_ControlUnits.I._addToDestroyList(selectedUnit);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_eye_explode (selectedUnit.unitID);	
					}
				break;
				#endregion
				#region "Azure Dragon 2"
				case "AzureDragon2":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					if(selectedUnit.HP > (int)((double)selectedUnit.HPMax * 0.48)){
						selectedUnit.HP = (int)((double)selectedUnit.HPMax * 0.48);
						selectedUnit._updateHealthBar();
						MG_ControlBuffs.I._addBuff(selectedUnit, "AzureDragon2");
						selectedUnit._transformSprite("MasamuneAzure", 9999);
					}
					MG_ControlSFX.I._createSFX("cartoonPowerup01", selectedUnit.posX, selectedUnit.posY);
					MG_UIControl_UseSkill.I._show(selectedUnit, "Azure Dragon!");

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_masamune_azureDragon (selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Frost Wave"
				case "FrostWave":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 50){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlSFX.I._createSFX("cartoonSnow01", selectedUnit.posX, selectedUnit.posY);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
								MG_CALC_Damage.I._damageUnit(selectedUnit, u, 75, "magic");
								MG_ControlBuffs.I._addBuff(u, "FrostWave");
								MG_ControlSFX.I._createSFX ("cartoonExplodeIce01", u.posX, u.posY);
							}
						}
					}
					_specialEffect_outward(selectedUnit.posX, selectedUnit.posY, "cartoonExplodeIce01", 3, 0.2, 0.2);
					MG_UIControl_UseSkill.I._show(selectedUnit, "Frost Wave!");
					MG_ControlSounds.I._playSound(38, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_yukino_frostWave (selectedUnit.unitID);	
					}
				break;
				#endregion
				#region "Demonic Slash"
				case "DemonicSlash":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 10){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 10; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
							MG_CALC_Damage.I._damageUnit(selectedUnit, u, 30, "magic");
								MG_ControlSFX.I._createSFX ("hit01", u.posX, u.posY);
								MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY); 
							}
						}
					}
					MG_ControlMissile.I._createMissile("WindSlashBlue", selectedUnit.posX, selectedUnit.posY, selectedUnit.posX, selectedUnit.posY+3, selectedUnit.unitID);
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.02", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX+1).ToString(),  (selectedUnit.posY+2).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.04", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX+2).ToString(),  (selectedUnit.posY+1).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.06", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX+3).ToString(),  (selectedUnit.posY).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.08", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX+2).ToString(),  (selectedUnit.posY-1).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.10", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX+1).ToString(),  (selectedUnit.posY-2).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.12", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX).ToString(),  (selectedUnit.posY-3).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.14", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX-2).ToString(),  (selectedUnit.posY-1).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.16", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX-1).ToString(),  (selectedUnit.posY-2).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.18", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX-3).ToString(),  (selectedUnit.posY).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.20", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX-1).ToString(),  (selectedUnit.posY+2).ToString() });
					MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.22", "16", selectedUnit.unitID.ToString(), (selectedUnit.posX-2).ToString(),  (selectedUnit.posY+1).ToString() });
					MG_UIControl_UseSkill.I._show(selectedUnit, "Demonic Slash!");
					MG_ControlSounds.I._playSound(30, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_walter_demonicSlash(selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Unleash Hero"
				case "unleash":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(!selectedUnit.action_skill){
						MG_UIControl_Announcer.I._announce ("Cannot Unleash after using a card!"); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					MG_UIControl_UseSkill.I._show(selectedUnit, "Unleash!");
					MG_ControlBuffs.I._addBuff(selectedUnit, "Unleash");
					selectedUnit.btn3_orderDef = "skill";
					selectedUnit.btn5_orderDef = "none";
					selectedUnit.MS = 4;
					selectedUnit.sightRadius = 6;
					MG_ControlSounds.I._playSound(27, selectedUnit.posX, selectedUnit.posY, true);
					MG_ControlSFX.I._createSFX("cartoonHit04", selectedUnit.posX, selectedUnit.posY);
					MG_ControlSFX.I._createSFX("explodeDust01", selectedUnit.posX, selectedUnit.posY);
					MG_ControlFogOfWar.I._calculateReveal();

					// Finishing
					MG_UIControl_Skill.I._hide();
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_unleashHero(selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Barrier"
				case "Barrier":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 5){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlSFX.I._createSFX("warhornEffectBlue", selectedUnit.posX, selectedUnit.posY);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(!MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
							if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 3){
								MG_ControlBuffs.I._addBuff(u, (u.playerOwner == 1) ? "BarrierBlue" : "BarrierRed");
								MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX, u.posY);
							}
						}
					}
					MG_UIControl_UseSkill.I._show(selectedUnit, "Barrier!");
					MG_ControlSounds.I._playSound(47, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_barrier(selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Global Barrier"
				case "GlobalBarrier":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 0){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 0; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlSFX.I._createSFX("warhornEffectBlue", selectedUnit.posX, selectedUnit.posY);
					foreach(MG_ClassUnit u in MG_Globals.I.units){
						if(!MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
								MG_ControlBuffs.I._addBuff(u, (u.playerOwner == 1) ? "BarrierBlue" : "BarrierRed");
								MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX, u.posY);
						}
					}
					MG_UIControl_UseSkill.I._show(selectedUnit, "Global Barrier!");
					MG_ControlSounds.I._playSound(47, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_globalBarrier(selectedUnit.unitID);
					}
				break;
				#endregion
				#region "Double Card"
				case "DoubleCard":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(selectedUnit.MP < 0){
						MG_UIControl_Announcer.I._announce ("Not enough mana"); MG_Globals.I.curCommand = "in map"; _changeCommandsAndUI();  MG_ControlCursor.I._interact(); MG_ControlSounds.I._playSound(49, 0, 0, false);
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					selectedUnit.MP -= 0; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
					MG_ControlSFX.I._createSFX("warhornEffectBlue", selectedUnit.posX, selectedUnit.posY);
					if(!selectedUnit.action_card)		selectedUnit.action_card = true;
					else 								MG_ControlBuffs.I._addBuff(selectedUnit, "DoubleCard");
					
					MG_UIControl_UseSkill.I._show(selectedUnit, "Double Card!");
					MG_ControlSounds.I._playSound(47, selectedUnit.posX, selectedUnit.posY, true);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_doubleCard(selectedUnit.unitID);
					}
				break;
				#endregion
				
				// Fix Bayonet
				case "FixBayonet":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					gameAction_fixBayonet (selectedUnit);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_fixBayonet(selectedUnit.unitID);
					}
				break;
				
				// Remove Bayonet
				case "RemoveBayonet":
					// Conditions
					if(MG_Globals.I.missiles.Count > 0 || MG_Globals.I.missilesTemp.Count > 0){
						return;
					}
					if(MG_Globals.I.curPlayerOnTurn != MG_Globals.I.curPlayerNum) 		return;

					// Effect
					gameAction_removeBayonet (selectedUnit);

					// Finishing
					MG_UIControl_Skill.I._hide();
					selectedUnit.action_skill = false;
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._interact (); // Change selected unit and update UI
					MG_UIControl_Announcer.I._clearAnnounce();

					/////// In-Game Multiplayer RPC ///////
					if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
						PhotonRoom.room.gameAction_removeBayonet(selectedUnit.unitID);
					}
				break;
			#endregion

			#region "Confirms"
			case "confirm":
				if(MG_Globals.I.curCommand == "aim friendly"){
					#region "Friendly targeters"
					#region "Generic intro code"
					MG_ClassTargeter targeter;
					if(MG_GetTargeter.I._pointHasTargeterOfType(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, 1) || globalCast){
						targeter = MG_GetTargeter.I._pickTargeterOfType(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, 1);
					}else{
						return;
					}
					#endregion

					switch(MG_Globals.I.mode){
						// ACTUAL ORDERS
						#region "Move"
						case "move":
							// Pre-move Additional codes
							#region "CAVALRY CHARGE"
							if(selectedUnit.name == "Horseman" || selectedUnit.name == "VikingRaider" || selectedUnit.name == "ImperialCavalry"){
								if(MG_CALC_Distance.I._distBetweenPoints(selectedUnit.posX, selectedUnit.posY, targeter.posX, targeter.posY) >= 4){
									MG_ControlBuffs.I._addBuff (selectedUnit, "MythrilLance");
								}
							}
							#endregion
							#region "MASAMUNE - Bleed"
							if(MG_ControlBuffs.I._unitHasBuff_returnStack(selectedUnit, "Bleed") >= 1){
								if(MG_CALC_Distance.I._distBetweenPoints(selectedUnit.posX, selectedUnit.posY, targeter.posX, targeter.posY) >= 4){
									MG_CALC_Damage.I._HP_Loss(selectedUnit, 50);
								}else{
									MG_CALC_Damage.I._HP_Loss(selectedUnit, 25);
								}
							}
							#endregion
							MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);

							// Effect
							selectedUnit._moveUnit(targeter.posX, targeter.posY);
							MG_ControlSFX.I._createSFX("moveSmoke", targeter.posX, targeter.posY);
							#region "Move sounds"
							switch(selectedUnit.name){
								case "ArcanyteTruck":
									MG_ControlSounds.I._playSound(42, targeter.posX, targeter.posY, true);
								break;
								default: MG_ControlSounds.I._playSound(6, targeter.posX, targeter.posY, true); break;
							}
							#endregion

							// Post-move Additional codes
							#region "SIEGFRIED - Warhorn"
							if(selectedUnit.name == "Siegfried"){
								MG_ControlSFX.I._createSFX("warhornEffect", selectedUnit.posX, selectedUnit.posY);
								foreach(MG_ClassUnit u in MG_Globals.I.units){
									if(!MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner) && u.isAlive){
										if(MG_CALC_Distance.I._distBetweenPoints(selectedUnit.posX, selectedUnit.posY, u.posX, u.posY) <= 3){
											MG_ControlBuffs.I._addBuff (u, "Warhorn");
										}
									}
								}
								MG_ControlSounds.I._playSound(32, selectedUnit.posX, selectedUnit.posY, true);
							}
							#endregion
							#region "WALTER - Rampage"
							if(selectedUnit.name == "Walter" || selectedUnit.name == "DemonPrince"){
								MG_ControlBuffs.I._addBuff (selectedUnit, "RampageWalter");
								selectedUnit.action_atk = true;
								selectedUnit.action_skill = true;
							}
							#endregion

							// Finishing
							if(!CHEAT_INFINTE_MOVE) selectedUnit.action_move = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_move (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Summon Hero"
						case "summon hero":
							// Effect
							MG_ControlUnits.I._createUnit(speStr_1, targeter.posX, targeter.posY, selectedUnit.playerOwner);
							MG_Globals.I.players[selectedUnit.playerOwner]._HERO_setSummonedState(speStr_1, true);
							MG_ControlSFX.I._createSFX("summonFx", targeter.posX, targeter.posY);
							MG_ControlSounds.I._playSound(27, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_move = false;
							selectedUnit.action_atk = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_summon (selectedUnit.unitID, speStr_1, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Hire Unit"
						case "hire unit":
							// Effect
							MG_ControlUnits.I._createUnit(speStr_1, targeter.posX, targeter.posY, selectedUnit.playerOwner);
							int itemCost = MG_DB_Cards.I.getCost (speStr_1);
							MG_UIControl_TopBar.I._goldGain (-itemCost);
							MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold -= itemCost;
							MG_UIControl_TopBar.I._goldUI_update ();
							MG_ControlSFX.I._createSFX("summonFx", targeter.posX, targeter.posY);
							MG_ControlSounds.I._playSound(7, targeter.posX, targeter.posY, true);
							MG_UIControl_Cards.I.throwSelCard_toBottomDeck ();

							// Barracks
							if(MG_DB_Cards.I.getType(speStr_1) == "Unit Lv.1") {
								foreach(MG_ClassUnit uBar in MG_Globals.I.units){
									if(uBar.name == "Barracks" && uBar.playerOwner == selectedUnit.playerOwner){
										MG_ControlUnits.I._createUnit(speStr_1, uBar.posX, uBar.posY, selectedUnit.playerOwner);
										MG_ControlSFX.I._createSFX("summonFx", uBar.posX, uBar.posY);
									}
								}
							}else if(MG_DB_Cards.I.getType(speStr_1) == "Unit Lv.2") {
								foreach(MG_ClassUnit uBar in MG_Globals.I.units){
									if(uBar.name == "BarracksLvl2" && uBar.playerOwner == selectedUnit.playerOwner){
										MG_ControlUnits.I._createUnit(speStr_1, uBar.posX, uBar.posY, selectedUnit.playerOwner);
										MG_ControlSFX.I._createSFX("summonFx", uBar.posX, uBar.posY);
									}
								}
							}

							// Finishing
							if(MG_ControlBuffs.I._unitHasBuff_returnStack(selectedUnit, "DoubleCard") >= 1){
								MG_ControlBuffs.I._removeBuff (selectedUnit.unitID, "DoubleCard");
							}else{
								selectedUnit.action_card = false;
							}
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_hireUnit (selectedUnit.unitID, speStr_1, targeter.posX, targeter.posY, MG_UIControl_Barracks.I.selUnitIndex);
							}
						break;
						#endregion
						#region "Cast Spell"
						case "cast spell":
							// Effect
							MG_ControlCommand_Spell.I.castSpell (speStr_1, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, MG_Globals.I.curPlayerNum);
							MG_Globals.I.players[MG_Globals.I.curPlayerNum].spell_name[MG_UIControl_SpellTower.I.selUnitIndex] = "NONE";
							MG_UIControl_UseSpell.I._show(speStr_1);
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_ControlSounds.I._playSound(24, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);
							int itemCost_spell = MG_DB_Cards.I.getCost (speStr_1);
							MG_UIControl_TopBar.I._goldGain (-itemCost_spell);
							MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold -= itemCost_spell;
							MG_UIControl_TopBar.I._goldUI_update ();
							MG_UIControl_Cards.I.throwSelCard_toBottomDeck ();

							// Finishing
							if(MG_ControlBuffs.I._unitHasBuff_returnStack(selectedUnit, "DoubleCard") >= 1){
								MG_ControlBuffs.I._removeBuff (selectedUnit.unitID, "DoubleCard");
							}else{
								selectedUnit.action_card = false;
							}
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_spell (selectedUnit.unitID, speStr_1, MG_UIControl_SpellTower.I.selUnitIndex, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Spiral"
						case "Spiral":
							// Effect
							selectedUnit.MP -= 25; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("Spiral", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							selectedUnit._moveUnit(targeter.posX, targeter.posY);
							foreach(MG_ClassUnit u in MG_Globals.I.units){
								if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
									if(MG_CALC_Distance.I._distUnits(selectedUnit, u) <= 2){
										MG_CALC_Damage.I._damageUnit(selectedUnit, u, 30, "magic");
										MG_ControlSFX.I._createSFX ("cartoonHit04", u.posX, u.posY);
									}
								}
							}
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_ControlSFX.I._createSFX ("explodeDust01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							_specialEffect_outward(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, "cartoonExplodeFire01", 2, 0.2, 0.2);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Spiral!");
							MG_ControlSounds.I._playSound(17, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_ifreet_spiral (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
							#endregion
						#region "Hookshot"
						case "Hookshot":
							// Effect
							selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("hookshot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Hookshot!");
							MG_ControlSounds.I._playSound(16, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_robin_hookshot (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Hook Dance"
						case "HookDance":
							// Effect
							selectedUnit.MP -= 10; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("hookdance", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Hook Dance!");
							MG_ControlSounds.I._playSound(16, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_robin_hookdance (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Scout"
						case "Scout":
							// Max summoned unit check
							_maxSummonedUnitCheck(selectedUnit, 1);

							// Effect
							selectedUnit.MP -= 20; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlUnits.I._createUnit("WeissDummy", targeter.posX, targeter.posY, selectedUnit.playerOwner);
							MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = selectedUnit.name;
							MG_Globals.I.players[selectedUnit.playerOwner]._HERO_addSummonedUnit(selectedUnit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

							MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "Weiss");
							MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
							MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;

							MG_UIControl_UseSkill.I._show(selectedUnit, "Scout!");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_amy_scout (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Recruit"
						case "Recruit":
							// Max summoned unit check
							_maxSummonedUnitCheck(selectedUnit, 3);

							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlUnits.I._createUnit("Recruit", targeter.posX, targeter.posY, selectedUnit.playerOwner);
							MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = selectedUnit.name;
							MG_Globals.I.players[selectedUnit.playerOwner]._HERO_addSummonedUnit(selectedUnit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

							MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = selectedUnit.name;
							MG_ControlSFX.I._createSFX("moveSmoke", targeter.posX, targeter.posY);

							MG_UIControl_UseSkill.I._show(selectedUnit, "Recruit!");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_alicia_summonRecruit (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "EyeOfTheMagi"
						case "EyeOfTheMagi":
							// Max summoned unit check:
							_maxSummonedUnitCheck(selectedUnit, 2);

							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlUnits.I._createUnit("EyeOfTheMagi", targeter.posX, targeter.posY, selectedUnit.playerOwner);
							MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = selectedUnit.name;
							MG_Globals.I.players[selectedUnit.playerOwner]._HERO_addSummonedUnit(selectedUnit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

							MG_ControlSFX.I._createSFX("drakgul02", targeter.posX, targeter.posY);
							MG_ControlSFX.I._createSFX("cartoonSpark01", targeter.posX, targeter.posY);

							MG_UIControl_UseSkill.I._show(selectedUnit, "Eye of the Magi!");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_drakgul_eye (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Dispel"
						case "Dispel":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("drakgul01", targeter.posX, targeter.posY);
							MG_ControlSFX.I._createSFX("cartoonSpark01", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_ControlBuffs.I._dispel_buffs (u);
									MG_ControlBuffs.I._addBuff (u, "Silence");
								}else{
									MG_ControlBuffs.I._dispel_debuffs (u);
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Anti-magic!");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_drakgul_dispel (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Concentrate"
						case "Concentrate":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("drakgul02", targeter.posX, targeter.posY);
							MG_ControlSFX.I._createSFX("cartoonSpark01", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_CALC_Healing.I._HP_Heal(selectedUnit, u, 60);
									MG_CALC_Healing.I._MP_Heal(selectedUnit, u, 40);
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Concentrate!");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_drakgul_concentration (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Heal"
						case "Heal":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonHoly01", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_CALC_Healing.I._HP_Heal(selectedUnit, u, 100);
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Heal!");
							MG_ControlSounds.I._playSound(24, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_victoria_heal (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Potion"
						case "Potion":
							// Effect
							selectedUnit.MP -= 10; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonHoly01", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_CALC_Healing.I._HP_Heal(selectedUnit, u, 40);
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Potion!");
							MG_ControlSounds.I._playSound(24, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_potion (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Shell"
						case "Shell":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonHoly02", targeter.posX, targeter.posY);
							foreach(MG_ClassUnit u in MG_Globals.I.units){
								if(!MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
									if(MG_CALC_Distance.I._distBetweenPoints(targeter.posX, targeter.posY, u.posX, u.posY) <= 2){
										MG_ControlSFX.I._createSFX("cartoonHoly02", u.posX, u.posY);
										MG_ControlBuffs.I._addBuff (u, "Shell");
									}
								}
							}
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Shell!");
							MG_ControlSounds.I._playSound(25, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_victoria_shell (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Blessing"
						case "Blessing":
							// Effect
							selectedUnit.MP -= 5;
							MG_ControlSFX.I._createSFX("cartoonHoly01", targeter.posX, targeter.posY);
							MG_ControlSFX.I._createSFX("cartoonHoly02", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_ControlBuffs.I._addBuff (u, "Blessing");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Blessing!");
							MG_ControlSounds.I._playSound(26, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_victoria_blessing (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Summon Spectre"
						case "SummonSpectre":
							// Max summoned unit check
							_maxSummonedUnitCheck(selectedUnit, 3);

							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlUnits.I._createUnit("Spectre", targeter.posX, targeter.posY, selectedUnit.playerOwner);
							MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = selectedUnit.name;
							MG_Globals.I.players[selectedUnit.playerOwner]._HERO_addSummonedUnit(selectedUnit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);
							MG_ControlMissile.I._createMissile("Spectre1", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_ControlSFX.I._createSFX("cartoonDeathBall", targeter.posX, targeter.posY);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Summon Spectre!");
							MG_ControlSounds.I._playSound(28, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_specWitch_summonSpectre (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Blink"
						case "Blink":case "BlinkSpectre":
							// Effect
							MG_ControlSFX.I._createSFX("moveSmoke", targeter.posX, targeter.posY);
							selectedUnit._moveUnit(targeter.posX, targeter.posY);
							MG_ControlSFX.I._createSFX("moveSmoke", targeter.posX, targeter.posY);
							MG_ControlSounds.I._playSound(28, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_specWitch_blink (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Vigor"
						case "Vigor":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("hit02", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_ControlBuffs.I._addBuff (u, "Vigor");
								}
							}

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {

							}
						break;
						#endregion
						#region "Test of Faith"
						case "TestOfFaith":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonHoly02", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_ControlBuffs.I._addBuff (u, "TestOfFaith_ally");
								}else{
									MG_ControlBuffs.I._addBuff (u, "TestOfFaith_enemy");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Test of Faith!");
							MG_ControlSounds.I._playSound(25, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_abel_testOfFaith (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Root Armor"
						case "RootArmor":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonNature01", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_ControlBuffs.I._addBuff (u, "RootArmor");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Root Armor!");
							MG_ControlSounds.I._playSound(26, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_elderTreant_rootArmor (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Fairies"
						case "Fairies":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonNature01", targeter.posX, targeter.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(!MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_ControlBuffs.I._addBuff (u, "Fairies");
								}
							}

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_elderTreant_fairies (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Phoenix Wings"
						case "PhoenixWings":
							// Effect
							selectedUnit.MP -= 25; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);
							selectedUnit._moveUnit(targeter.posX, targeter.posY);
							MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Phoenix Wings!");
							MG_ControlSounds.I._playSound(6, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_elise_phoenixWings (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "SacredKnights"
						case "SacredKnights":
							// Max summoned unit check
							_maxSummonedUnitCheck(selectedUnit, 3);

							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlUnits.I._createUnit("SacredKnight", targeter.posX, targeter.posY, selectedUnit.playerOwner);
							MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = selectedUnit.name;
							MG_Globals.I.players[selectedUnit.playerOwner]._HERO_addSummonedUnit(selectedUnit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

							MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = selectedUnit.name;
							MG_ControlSFX.I._createSFX("moveSmoke", targeter.posX, targeter.posY);

							MG_UIControl_UseSkill.I._show(selectedUnit, "Sacred Knight!");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_may_sacredKnights (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Healing Spray"
						case "HealingSpray":
							// Effect
							selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonHoly02", targeter.posX, targeter.posY);
							foreach(MG_ClassUnit u in MG_Globals.I.units){
								if(!MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
									if(MG_CALC_Distance.I._distBetweenPoints(targeter.posX, targeter.posY, u.posX, u.posY) <= 2){
										MG_ControlSFX.I._createSFX("cartoonHoly02", u.posX, u.posY);
										MG_CALC_Healing.I._HP_Heal(selectedUnit, u, 20);
									}
								}
							}
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Healing Spray!");
							MG_ControlSounds.I._playSound(25, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_healSpray (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Upgrade"
						case "Upgrade":
							// Effect
							bool hasTar = false;
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								if(MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY).name == MG_ControlCommand.I.speStr_1){
									hasTar = true;
								}
							}
							if(hasTar){
								MG_ControlSFX.I._createSFX("cartoonHoly01", targeter.posX, targeter.posY);
								MG_ControlSFX.I._createSFX("cartoonHoly02", targeter.posX, targeter.posY);
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);

								MG_ControlUnits.I._addToDestroyList (u);
								MG_ControlUnits.I._createUnit(speStr_2, targeter.posX, targeter.posY, selectedUnit.playerOwner);

								int upgCost = 0;
								int.TryParse (MG_ControlCommand.I.speStr_3, out upgCost);
								MG_UIControl_TopBar.I._goldGain (-upgCost);
								MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold -= upgCost;
								MG_UIControl_TopBar.I._goldUI_update ();

								MG_UIControl_UseSkill.I._show(selectedUnit, "Upgrade!");
								MG_ControlSounds.I._playSound(26, targeter.posX, targeter.posY, true);

								MG_UIControl_Cards.I.throwSelCard_toBottomDeck ();

								// Finishing
								if(!selectedUnit.action_card)		selectedUnit.action_card = true;
								else 								MG_ControlBuffs.I._addBuff(selectedUnit, "DoubleCard");
								
								MG_ControlTargeters.I._clearTargeters();
								MG_Globals.I.mode = "in map";
								MG_Globals.I.curCommand = "in map";
								MG_ControlCursor.I._interact (); // Change selected unit and update UI
								MG_UIControl_Announcer.I._clearAnnounce();

								/////// In-Game Multiplayer RPC ///////
								if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
									PhotonRoom.room.gameAction_upgrade (selectedUnit.unitID, targeter.posX, targeter.posY, speStr_2);
								}
							}else{
								MG_UIControl_Announcer.I._announce ("Invalid target."); MG_ControlSounds.I._playSound(49, 0, 0, false);
								return;
							}
						break;
						#endregion
						#region "testSkill1"
						case "testSkill1":
							// Effect
							// Change order to testSkill1.1, the next phase of this skill
							if(MG_GetUnit.I._pointHasUnit(targeter.posX, targeter.posY)){
								_issueCommand(-1, "testSkill1.1");
							}
						break;
						#endregion
					}
					#endregion
				}else if(MG_Globals.I.curCommand == "aim hostile"){
					#region "Hostile targeters"
					#region "Generic code intro"
					MG_ClassTargeter targeter;
					if(MG_GetTargeter.I._pointHasTargeterOfType(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, 2) || globalCast){
						targeter = MG_GetTargeter.I._pickTargeterOfType(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, 2);
					}else{
						return;
					}
					#endregion

					switch(MG_Globals.I.mode){
						// ACTUAL ORDERS
						#region "Attack"
						case "attack":
							// Effect
							_EXTENSION_attack_order(selectedUnit, targeter.posX, targeter.posY);

							// Finishing
							if (MG_ControlBuffs.I._unitHasBuff_returnStack (selectedUnit, "GlovesOfHaste") >= 1) {
								MG_ControlBuffs.I._removeBuff (selectedUnit.unitID, "GlovesOfHaste");
							} else {
								if(!CHEAT_INFINTE_MOVE) selectedUnit.action_atk = false;
							}
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_attack (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Armor Break"
						case "ArmorBreak":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_CALC_Damage.I._damageUnit (selectedUnit, u, 50, "magic");
									MG_ControlSFX.I._createSFX ("testSFX", u.posX, u.posY);
									MG_ControlBuffs.I._addBuff (u, "ArmorBreak");
								}
							}

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								
							}
						break;
						#endregion
						#region "Spear Toss"
						case "SpearToss":
							// Effect
							selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("speartoss", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Spear Toss!");
							MG_ControlSounds.I._playSound(31, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_ajax_spearToss (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
							#endregion
						#region "Multi Shot"
						case "MultiShot":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("multishotMain", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("multishot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX-1, MG_ControlCursor.I.posY-1, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("multishot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX+1, MG_ControlCursor.I.posY-1, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("multishot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX-1, MG_ControlCursor.I.posY+1, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("multishot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX+1, MG_ControlCursor.I.posY+1, selectedUnit.unitID);
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Multi Shot!");
							MG_ControlSounds.I._playSound(14, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_amy_multishot (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
							#endregion
						#region "Close Combat"
						case "CloseCombat":
							// Effect
							selectedUnit.MP -= 75; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX ("cartoonHit01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if(MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)){
									MG_CALC_Damage.I._damageUnit (selectedUnit, u, 40, "magic");
									MG_ControlBuffs.I._addBuff (u, "CloseCombat");
									MG_ControlBuffs.I._addBuff (u, "Stun");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Close Combat!");
							MG_ControlSounds.I._playSound(22, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_alicia_closeCombat (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Bayonet"
						case "Bayonet":
							// Effect
							MG_ControlSFX.I._createSFX ("hit07", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if (MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)) {
									MG_CALC_Damage.I._damageUnit (selectedUnit, u, (MG_ControlBuffs.I._unitHasBuff_returnStack (u, "CloseCombat") >= 1) ? 35 : 15, "magic" );
								}
							}

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								
							}
						break;
						#endregion
						#region "Quick Draw"
						case "QuickDraw":
							// Effect
							selectedUnit.MP -= 10; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("QuickDrawMain", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.1", "8", selectedUnit.unitID.ToString(), MG_ControlCursor.I.posX.ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.2", "8", selectedUnit.unitID.ToString(), MG_ControlCursor.I.posX.ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.3", "8", selectedUnit.unitID.ToString(), MG_ControlCursor.I.posX.ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.4", "8", selectedUnit.unitID.ToString(), MG_ControlCursor.I.posX.ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.5", "8", selectedUnit.unitID.ToString(), MG_ControlCursor.I.posX.ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_UIControl_UseSkill.I._show(selectedUnit, "Quick Draw!");
							MG_ControlSounds.I._playSound(20, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_colt_quickDraw (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Dynamite"
						case "Dynamite":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("Dynamite", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Dynamite!");
							MG_ControlSounds.I._playSound(31, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_colt_dynamite (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Disarming Shot"
						case "DisarmShot":
							// Effect
							selectedUnit.MP -= 7; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("DisarmShot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY + 1, selectedUnit.unitID);
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.1", "7", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX-1).ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.2", "7", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.3", "7", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX+1).ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.4", "7", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY-1).ToString() });
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Disarming Shot!");
							MG_ControlSounds.I._playSound(20, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_colt_disarmingShot (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
							#endregion
							#region "Lightning Bolt"
						case "LightningBolt":
							// Effect
							selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("LightningBolt", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY + 1, selectedUnit.unitID);
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.05", "32", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX-1).ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.1", "32", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.15", "32", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX+1).ToString(),  MG_ControlCursor.I.posY.ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.2", "32", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY-1).ToString() });
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Lightning Bolt!");
							MG_ControlSounds.I._playSound(13, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_elizabeth_lightningBolt (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
							#endregion
						#region "Burning Grip"
						case "BurningGrip":
							// Effect
							selectedUnit.MP -= 75; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX ("cartoonHit04", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							MG_ControlSFX.I._createSFX ("slam01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							for(int i = 1; i <= 3; i++){
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (MG_ControlCursor.I.posX + i).ToString(),  (MG_ControlCursor.I.posY + i).ToString() });
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (MG_ControlCursor.I.posX - i).ToString(),  (MG_ControlCursor.I.posY + i).ToString() });
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (MG_ControlCursor.I.posX + i).ToString(),  (MG_ControlCursor.I.posY - i).ToString() });
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (MG_ControlCursor.I.posX - i).ToString(),  (MG_ControlCursor.I.posY - i).ToString() });

								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (MG_ControlCursor.I.posX + i).ToString(),  (MG_ControlCursor.I.posY + i).ToString() });
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (MG_ControlCursor.I.posX - i).ToString(),  (MG_ControlCursor.I.posY + i).ToString() });
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (MG_ControlCursor.I.posX + i).ToString(),  (MG_ControlCursor.I.posY - i).ToString() });
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (MG_ControlCursor.I.posX - i).ToString(),  (MG_ControlCursor.I.posY - i).ToString() });
							}
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if (MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)) {
									if(((float)u.HP / (float)u.HPMax) <= 0.25f)
										MG_CALC_Damage.I._damageUnit (selectedUnit, u, 9999, "pure");
									else 
										MG_CALC_Damage.I._damageUnit (selectedUnit, u, 30, "magic");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Burning Grip!");
							MG_ControlSounds.I._playSound(17, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);
							
							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_ifreet_burningGrip (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Smite"
						case "Smite":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX ("cartoonExplodeFire01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							MG_ControlSFX.I._createSFX("cartoonHoly02", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							MG_ControlSFX.I._createSFX("explodeDust01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if (MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)) {
									MG_CALC_Damage.I._damageUnit (selectedUnit, u, 75, "magic");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Smite!");
							MG_ControlSounds.I._playSound(17, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_abel_smite (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Heavy Charge"
						case "HeavyCharge":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlUnits.I.unitTweenMode = 1;
							selectedUnit._tweenMove(new Vector2((float)targeter.posX/2, (float)targeter.posY/2), 3f, false);
							MG_Globals.I.pause_uiMove = true;
							if(MG_CALC_Distance.I._distBetweenPoints(selectedUnit.posX, selectedUnit.posY, targeter.posX, targeter.posY) >= 4){
								MG_ControlBuffs.I._addBuff (selectedUnit, "MythrilLance");
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Heavy Charge!");
							MG_ControlSounds.I._playSound(21, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_siegfried_heavyCharge (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Wind Slash"
						case "WindSlash":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("WindSlash", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Wind Slash!");
							MG_ControlSounds.I._playSound(33, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_masamune_windSlash (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Entangle"
						case "Entangle":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if (MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)) {
									MG_CALC_Damage.I._damageUnit (selectedUnit, u, 50, "magic");
									MG_ControlSFX.I._createSFX("cartoonNature01", targeter.posX, targeter.posY);
									MG_ControlBuffs.I._addBuff (u, "Entangle");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Entangle!");
							MG_ControlSounds.I._playSound(26, targeter.posX, targeter.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_elderTreant_entangle (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Frost Nova"
						case "FrostNova":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonSnow01", targeter.posX, targeter.posY);
							foreach(MG_ClassUnit u in MG_Globals.I.units){
								if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
									if(MG_CALC_Distance.I._distBetweenPoints(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, u.posX, u.posY) <= 2){
										MG_CALC_Damage.I._damageUnit(selectedUnit, u, 70, "magic");
										MG_ControlSFX.I._createSFX ("cartoonExplodeIce01", u.posX, u.posY);
									}
								}
							}
							MG_ControlCursor.I._changeCursorSprite("Normal");
							_specialEffect_outward(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, "cartoonExplodeIce01", 2, 0.2, 0.2);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Frost Nova!");
							MG_ControlSounds.I._playSound(38, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_yukino_frostNova (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Silence"
						case "Silence":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("cartoonSnow01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							foreach(MG_ClassUnit u in MG_Globals.I.units){
								if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
									if(MG_CALC_Distance.I._distBetweenPoints(MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, u.posX, u.posY) <= 1){
										MG_ControlBuffs.I._addBuff (u, "Silence");
									}
								}
							}
							MG_ControlSFX.I._createSFX ("cartoonPowerup01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
							MG_ControlSFX.I._createSFX ("cartoonPowerup01", MG_ControlCursor.I.posX-1, MG_ControlCursor.I.posY);
							MG_ControlSFX.I._createSFX ("cartoonPowerup01", MG_ControlCursor.I.posX+1, MG_ControlCursor.I.posY);
							MG_ControlSFX.I._createSFX ("cartoonPowerup01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY-1);
							MG_ControlSFX.I._createSFX ("cartoonPowerup01", MG_ControlCursor.I.posX, MG_ControlCursor.I.posY+1);
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Silence!");
							//MG_ControlSounds.I._playSound(17, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_yukino_silence (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Arcane Bolt"
						case "ArcaneBolt":
							// Effect
							selectedUnit.MP -= 75; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("ArcaneBolt", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Arcane Bolt!");
							MG_ControlSounds.I._playSound(13, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_hilde_arcaneBolt (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Blink Strike"
						case "BlinkStrike":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if (MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)) {
									MG_CALC_Damage.I._damageUnit (selectedUnit, u, 75, "magic");
									MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);
									MG_ControlSFX.I._createSFX ("hit01", targeter.posX, targeter.posY); 
									MG_ControlSFX.I._createSFX ("cartoonHit01", targeter.posX, targeter.posY); 
									MG_ControlSounds.I._playSound(22, targeter.posX, targeter.posY, true);
									MG_ControlBuffs.I._addBuff (u, "BlinkStrike");

									if(selectedUnit.facing == "right"){
										selectedUnit._moveUnit(targeter.posX - 1, targeter.posY);
										MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX - 1, selectedUnit.posY);
									}else{
										selectedUnit._moveUnit(targeter.posX + 1, targeter.posY);
										MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX + 1, selectedUnit.posY);
									}
								}
							}else{
								selectedUnit._moveUnit(targeter.posX, targeter.posY);
								MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Blink Strike!");
							MG_ControlSounds.I._playSound(6, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_cody_blinkStrike (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Photon Bomb"
						case "PhotonBomb":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							foreach(MG_ClassUnit u in MG_Globals.I.units){
								if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
									if(MG_CALC_Distance.I._distBetweenPoints(u.posX, u.posY, targeter.posX, targeter.posY) <= 2){
										MG_CALC_Damage.I._damageUnit(selectedUnit, u, 75, "magic");
									}
								}
							}
							_specialEffect_outward(targeter.posX, targeter.posY, "photonBomb", 2, 0.2, 0.2);
							_specialEffect_outward(targeter.posX, targeter.posY, "explodeDust01", 2, 0.2, 0.2);
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Photon Bomb!");
							MG_ControlSounds.I._playSound(44, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_hilde_photonBomb (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Multi Strike"
						case "MultiStrike":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							bool ms_isEven = false;
							for(int i = 0; i < 4; i++){
								ms_isEven = !ms_isEven;
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.15 * i).ToString(), "9", (ms_isEven) ? "hit01" : "hit01_Reverse", (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY).ToString() });
								MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.15 * i).ToString(), "10", (ms_isEven) ? "22" : "23", (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY).ToString() });
							}
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								MG_ClassUnit u = MG_GetUnit.I._pickUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY);
								if (MG_ControlPlayer.I._getIsEnemy (selectedUnit.playerOwner, u.playerOwner)) {
									MG_CALC_Damage.I._damageUnit (selectedUnit, u, 75, "magic");
									MG_ControlSFX.I._createSFX ("cartoonHit01", targeter.posX, targeter.posY); 
									MG_ControlBuffs.I._addBuff(selectedUnit, "PhoenixWings");
									MG_ControlBuffs.I._addBuff(selectedUnit, "PhoenixWings");
								}
							}
							MG_UIControl_UseSkill.I._show(selectedUnit, "Multi-Strike!");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_elise_multiStrike (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Axe Throw/Axe Throw (Boss)"
						case "AxeThrow":case "AxeThrow(Boss)":
							// Effect
							selectedUnit.MP -= (MG_Globals.I.mode == "AxeThrow") ? 50 : 100; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile(MG_Globals.I.mode, selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Axe Throw!");
							MG_ControlSounds.I._playSound(31, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_aleric_axeThrow (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Mighty Slam/Mighty Slam(Boss)"
						case "MightySlam":case "MightySlam(Boss)":
							// Effect
							selectedUnit.MP -= (MG_Globals.I.mode == "MightySlam") ? 50 : 100; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);

							// Blink part
							if(MG_GetUnit.I._pointHasUnit (MG_ControlCursor.I.posX, MG_ControlCursor.I.posY)){
								if(selectedUnit.facing == "right"){
									selectedUnit._moveUnit(targeter.posX - 1, targeter.posY);
								}else{
									selectedUnit._moveUnit(targeter.posX + 1, targeter.posY);
								}
							}else{
								selectedUnit._moveUnit(targeter.posX, targeter.posY);
							}
							MG_ControlSFX.I._createSFX("moveSmoke", selectedUnit.posX, selectedUnit.posY);
							MG_ControlSFX.I._createSFX("slam01", selectedUnit.posX, selectedUnit.posY);
							MG_ControlSFX.I._createSFX("slam02", selectedUnit.posX, selectedUnit.posY);

							// Damage part
							foreach(MG_ClassUnit u in MG_Globals.I.units){
								if(MG_ControlPlayer.I._getIsEnemy(selectedUnit.playerOwner, u.playerOwner)){
									if(MG_CALC_Distance.I._distBetweenPoints(u.posX, u.posY, targeter.posX, targeter.posY) <= 2){
										MG_CALC_Damage.I._damageUnit(selectedUnit, u, (u.isAHero) ? 80 : 50, "magic");
										MG_ControlSFX.I._createSFX("cartoonHit01", u.posX, u.posY);
									}
								}
							}

							// Misc
							MG_UIControl_UseSkill.I._show(selectedUnit, "Mighty Slam!");
							MG_ControlSounds.I._playSound(43, selectedUnit.posX, selectedUnit.posY, true);
							MG_ControlCursor.I._changeCursorSprite("Normal");

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_aleric_mightySlam (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Burst Fire"
						case "BurstFire":
							// Effect
							selectedUnit.MP -= 50; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("BurstFireMain", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY + 1, selectedUnit.unitID);
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.03", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY + 1).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.06", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY + 1).ToString() });

							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.09", "14", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX-1).ToString(),  (MG_ControlCursor.I.posY).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.12", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX-1).ToString(),  (MG_ControlCursor.I.posY).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.15", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX-1).ToString(),  (MG_ControlCursor.I.posY).ToString() });

							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.18", "14", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.21", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.24", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY).ToString() });

							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.27", "14", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX+1).ToString(),  (MG_ControlCursor.I.posY).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.3", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX+1).ToString(),  (MG_ControlCursor.I.posY).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.33", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX+1).ToString(),  (MG_ControlCursor.I.posY).ToString() });

							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.36", "14", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY-1).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.39", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY-1).ToString() });
							MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.42", "15", selectedUnit.unitID.ToString(), (MG_ControlCursor.I.posX).ToString(),  (MG_ControlCursor.I.posY-1).ToString() });

							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Burst Fire!");
							MG_ControlSounds.I._playSound(40, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_walter_burstFire (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Mythril Revolver"
						case "MythrilRevolver":
							// Effect
							selectedUnit.MP -= 10; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("MythrilRevolver", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Mythril Revolver!");
							MG_ControlSounds.I._playSound(19, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_mythrilRevolver (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Death Arrow"
						case "DeathArrow":
							// Effect
							gameAction_eve_deathArrow (selectedUnit, targeter.posX, targeter.posY);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_eve_deathArrow (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Drain Life"
						case "DrainLife":
							// Effect
							gameAction_eve_drainLife (selectedUnit, targeter.posX, targeter.posY);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_eve_drainLife (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Grenade"
						case "Grenade":
							// Effect
							gameAction_grenade (selectedUnit, targeter.posX, targeter.posY);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();
							MG_ControlCursor.I._changeCursorSprite("Normal");

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_grenade (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Artillery"
						case "Artillery":
							// Effect
							int[] artPosX = {MG_ControlCursor.I.posX + Random.Range(-2, 2), MG_ControlCursor.I.posX + Random.Range(-2, 2), MG_ControlCursor.I.posX + Random.Range(-2, 2)};
							int[] artPosY = {MG_ControlCursor.I.posY + Random.Range(-2, 2), MG_ControlCursor.I.posY + Random.Range(-2, 2), MG_ControlCursor.I.posY + Random.Range(-2, 2)};
							gameAction_artillery (selectedUnit, artPosX[0], artPosY[0], artPosX[1], artPosY[1], artPosX[2], artPosY[2]);
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Artillery!");
							globalCast = false;
							MG_ControlSounds.I._playSound(18, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, false);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_artillery (selectedUnit.unitID, artPosX[0], artPosY[0], artPosX[1], artPosY[1], artPosX[2], artPosY[2]);
							}
						break;
						#endregion
						#region "Grapeshot"
						case "Grapeshot":
							// Effect
							selectedUnit.MP -= 5; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("grapeshotMain", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("grapeshot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX-1, MG_ControlCursor.I.posY-1, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("grapeshot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX+1, MG_ControlCursor.I.posY-1, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("grapeshot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX-1, MG_ControlCursor.I.posY+1, selectedUnit.unitID);
							MG_ControlMissile.I._createMissile("grapeshot", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX+1, MG_ControlCursor.I.posY+1, selectedUnit.unitID);
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_UIControl_UseSkill.I._show(selectedUnit, "Grapeshot!");
							MG_ControlSounds.I._playSound(50, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_grapeshot (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "Acid Breath"
						case "AcidBreath":
							// Effect
							selectedUnit.MP -= 10; if(selectedUnit.MP < 0) selectedUnit.MP = 0;
							MG_ControlMissile.I._createMissile("AcidBreath", selectedUnit.posX, selectedUnit.posY, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, selectedUnit.unitID);
							MG_UIControl_UseSkill.I._show(selectedUnit, "Acid Breath!");
							MG_ControlSounds.I._playSound(33, selectedUnit.posX, selectedUnit.posY, true);

							// Finishing
							selectedUnit.action_skill = false;
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._interact (); // Change selected unit and update UI
							MG_UIControl_Announcer.I._clearAnnounce();

							/////// In-Game Multiplayer RPC ///////
							if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
								PhotonRoom.room.gameAction_acidBreath (selectedUnit.unitID, targeter.posX, targeter.posY);
							}
						break;
						#endregion
						#region "testSkill1.1"
							case "testSkill1.1":
								// Effect
								MG_ClassUnit targUnit = MG_GetUnit.I._pickUnit(speInt_1, speInt_2);
								targUnit._tweenMove(new Vector2((float)targeter.posX/2, (float)targeter.posY/2), 3f, true);

								// Finishing
								selectedUnit.action_skill = false;
								MG_ControlTargeters.I._clearTargeters();
								MG_Globals.I.mode = "in map";
								MG_Globals.I.curCommand = "in map";
								MG_ControlCursor.I._interact (); // Change selected unit and update UI
							break;
						#endregion
					}
					#endregion
				}
			break;
			#endregion
			#region "Cancels"
			case "cancel":case "cancel(hidden)":
				if(MG_Globals.I.curCommand == "aim friendly"){
					#region "Friendly targeters"
					switch(MG_Globals.I.mode){
						#region "General Cancel"
						default:
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_ControlCursor.I._interact();
							_changeCommandsAndUI();
							MG_UIControl_Announcer.I._clearAnnounce();
						break;
						#endregion
					}
					#endregion
				}else if(MG_Globals.I.curCommand == "aim hostile"){
					#region "Hostile targeters"
					switch(MG_Globals.I.mode){
						#region "General Cancel"
						default:
							MG_ControlTargeters.I._clearTargeters();
							MG_Globals.I.mode = "in map";
							MG_Globals.I.curCommand = "in map";
							MG_ControlCursor.I._changeCursorSprite("Normal");
							MG_ControlCursor.I._interact();
							_changeCommandsAndUI();
							MG_UIControl_Announcer.I._clearAnnounce();
						break;
						#endregion
					}
					#endregion
				}else if(MG_Globals.I.curCommand == "skill"){
					#region "Cancel Skill Window"
					MG_UIControl_Skill.I._hide();
					MG_ControlTargeters.I._clearTargeters();
					MG_ControlCursor.I._interact();
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._changeCursorSprite("Normal");
					_changeCommandsAndUI();
					#endregion
				}
				globalCast = false;
			break;
			#endregion
		}
	}

	#region "EXTENSION - Attack Order"
	public void _EXTENSION_attack_order(MG_ClassUnit attackingUnit, int targPointX, int targPointY, bool _isDefnd = false){
		#region "Defaults"
		bool hasTargUnit = false;
		MG_ClassUnit targUnit = MG_Globals.I.units[0];
		if (MG_GetUnit.I._pointHasUnit (targPointX, targPointY)) {
			targUnit = MG_GetUnit.I._pickUnit (targPointX, targPointY);
			hasTargUnit = true;
		}
		#endregion

		switch (attackingUnit.atkType) {
			#region "Melee Units"
			case "Melee":
				// Special FX
				switch (attackingUnit.atkType) {
					default: 
						MG_ControlSFX.I._createSFX ("hit01", targPointX, targPointY); 
						MG_ControlSFX.I._createSFX ("cartoonHit01", targPointX, targPointY); 
						if(Random.Range(0, 1) <= 0.65f){
							MG_ControlSounds.I._playSound(22, targPointX, targPointY, true);
						}else{
							MG_ControlSounds.I._playSound(23, targPointX, targPointY, true);
						}
					break;
				}

				if (hasTargUnit) {
					if (MG_ControlPlayer.I._getIsEnemy (attackingUnit.playerOwner, targUnit.playerOwner)) {
						MG_CALC_Damage.I._damageUnit (attackingUnit, targUnit, attackingUnit.atkDamage);
					}
				}
			break;
			#endregion
			
			// Ranged
			case "Ranged":
				MG_ControlMissile.I._createMissile("ArcGun01", attackingUnit.posX, attackingUnit.posY, targPointX, targPointY, attackingUnit.unitID);
				MG_ControlSounds.I._playSound(41, attackingUnit.posX, attackingUnit.posY, true);
				MG_ControlSFX.I._createSFX("moveSmoke", attackingUnit.posX, attackingUnit.posY);
				
				
			break;
		}
		
		// Check enemy retaliation
		if (!_isDefnd) {
			MG_ClassUnit _targ = MG_GetUnit.I._pickUnit (targPointX, targPointY);
			
			if(_targ.atkRange <= 1)
				MG_ControlTargeters.I._createTarg_Square(_targ.posX, _targ.posY, 1, true);
			else
				MG_ControlTargeters.I._createTargField(true, _targ.posX, _targ.posY, _targ.atkRange);
			
			MG_ControlCommand.I._EXTENSION_attack_order(_targ, attackingUnit.posX, attackingUnit.posY, true);
			MG_ControlTargeters.I.forceRemoveTempTargeters();
		}

		// Post-damage Additionals
		// MG_UIControl_UseSkill.I._show(attackingUnit, "Attack!");
		if (!hasTargUnit) return;
	}
	#endregion
	#region "EXTENSION - Reset Commands"
	public void resetCommands(){
		MG_ControlTargeters.I._clearTargeters();
		MG_ControlCursor.I._interact();
		MG_Globals.I.mode = "in map";
		MG_Globals.I.curCommand = "in map";
		MG_ControlCursor.I._changeCursorSprite("Normal");
		_changeCommandsAndUI();
	}
	#endregion
	#endregion

	#region "Multiplayer RPC Game Actions"
	#region "Move Order"
	public void gameAction_move(MG_ClassUnit unit, int posX, int posY){
		// Pre-move Additional codes
		#region "CAVALRY CHARGE"
		if(unit.name == "Horseman" || unit.name == "VikingRaider" || unit.name == "ImperialCavalry"){
			if(MG_CALC_Distance.I._distBetweenPoints(unit.posX, unit.posY, posX, posY) >= 4){
				MG_ControlBuffs.I._addBuff (unit, "MythrilLance");
			}
		}
		#endregion
		#region "MASAMUNE - Bleed"
		if(MG_ControlBuffs.I._unitHasBuff_returnStack(unit, "Bleed") >= 1){
			if(MG_CALC_Distance.I._distBetweenPoints(unit.posX, unit.posY, posX, posY) >= 4){
				MG_CALC_Damage.I._HP_Loss(unit, 50);
			}else{
				MG_CALC_Damage.I._HP_Loss(unit, 25);
			}
		}
		#endregion

		MG_ControlSFX.I._createSFX("moveSmoke", unit.posX, unit.posY);
		unit._moveUnit(posX, posY);
		MG_ControlSFX.I._createSFX("moveSmoke", posX, posY);
		unit.action_move = false;
		#region "Move sounds"
		switch(unit.name){
			case "ArcanyteTruck":
				MG_ControlSounds.I._playSound(42, posX, posY, true);
			break;
			default: MG_ControlSounds.I._playSound(6, posX, posY, true); break;
		}
		#endregion

		// Post-move Additional codes
		#region "SIEGFRIED - Warhorn"
		if(unit.name == "Siegfried"){
			MG_ControlSFX.I._createSFX("warhornEffect", unit.posX, unit.posY);
			foreach(MG_ClassUnit u in MG_Globals.I.units){
				if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner) && u.isAlive){
					if(MG_CALC_Distance.I._distBetweenPoints(unit.posX, unit.posY, u.posX, u.posY) <= 3){
						MG_ControlBuffs.I._addBuff (u, "Warhorn");
					}
				}
			}
			MG_ControlSounds.I._playSound(32, posX, posY, true);
		}
		#endregion
		#region "WALTER - Rampage"
		if(unit.name == "Walter" || unit.name == "DemonPrince"){
			MG_ControlBuffs.I._addBuff (unit, "RampageWalter");
			unit.action_atk = true;
			unit.action_skill = true;
		}
		#endregion
		#region "COLT - Quick Draw ATK"
		if(unit.name == "Colt"){
			if(MG_ControlBuffs.I._unitHasBuff_returnStack(unit, "QuickDrawAtk") <= 0){
				MG_ControlBuffs.I._addBuff (unit, "QuickDrawAtk");
				unit.action_atk = true;
			}else{
				unit.action_atk = false;
			}
		}
		#endregion
	}
	#endregion
	#region "Attack Order"
	public void gameAction_attack(MG_ClassUnit unit, int posX, int posY){
		_EXTENSION_attack_order(unit, posX, posY);

		if (MG_ControlBuffs.I._unitHasBuff_returnStack (unit, "GlovesOfHaste") >= 1) {
			MG_ControlBuffs.I._removeBuff (unit.unitID, "GlovesOfHaste");
		} else {
			if(unit.name != "Colt") unit.action_atk = false;
		}
	}
	#endregion
	#region "Summon Order"
	public void gameAction_summon(MG_ClassUnit unit, string summonedUnit, int posX, int posY){
		MG_ControlUnits.I._createUnit(summonedUnit, posX, posY, unit.playerOwner);
		MG_Globals.I.players[unit.playerOwner]._HERO_setSummonedState(summonedUnit, true);
		MG_ControlSFX.I._createSFX("summonFx", posX, posY);
		MG_ControlSounds.I._playSound(27, posX, posY, true);
	}
	#endregion
	#region "Hire Unit"
	public void gameAction_hireUnit(MG_ClassUnit unit, string summonedUnit, int posX, int posY, int hireIndex){
		MG_ControlUnits.I._createUnit(summonedUnit, posX, posY, unit.playerOwner);
		MG_ControlSFX.I._createSFX("summonFx", posX, posY);
		MG_ControlSounds.I._playSound(7, posX, posY, true);

		// Barracks
		if (MG_DB_Cards.I.getType (speStr_1) == "Unit Lv.1") {
			foreach (MG_ClassUnit uBar in MG_Globals.I.units) {
				if (uBar.name == "Barracks" && uBar.playerOwner == unit.playerOwner) {
					MG_ControlUnits.I._createUnit (summonedUnit, uBar.posX, uBar.posY, unit.playerOwner);
					MG_ControlSFX.I._createSFX ("summonFx", uBar.posX, uBar.posY);
				}
			}
		}else if (MG_DB_Cards.I.getType (speStr_1) == "Unit Lv.2") {
			foreach (MG_ClassUnit uBar in MG_Globals.I.units) {
				if (uBar.name == "BarracksLvl2" && uBar.playerOwner == unit.playerOwner) {
					MG_ControlUnits.I._createUnit (summonedUnit, uBar.posX, uBar.posY, unit.playerOwner);
					MG_ControlSFX.I._createSFX ("summonFx", uBar.posX, uBar.posY);
				}
			}
		}
	}
	#endregion
	#region "Spell Tower"
	public void gameAction_spell(MG_ClassUnit unit, string spellCasted, int spellIndex, int posX, int posY){
		// Effect
		MG_ControlCommand_Spell.I.castSpell (spellCasted, posX, posY, unit.playerOwner);
		MG_Globals.I.players[unit.playerOwner].spell_name[spellIndex] = "NONE";
		MG_UIControl_UseSpell.I._show(spellCasted);
		MG_ControlSounds.I._playSound(24, posX, posY, true);
	}
	#endregion
	#region "End Turn"
	public void gameAction_endTurn(){
		MG_ControlPlayer.I._endTurn();
	}
	#endregion

	#region "ROBIN - Hookshot"
	public void gameAction_robin_hookshot(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("hookshot", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Hookshot!");
		MG_ControlSounds.I._playSound(16, posX, posY, true);
	}
	#endregion
	#region "ROBIN - Sword Dance"
	public void gameAction_robin_sworddance(MG_ClassUnit unit){
		unit.MP -= 7; if(unit.MP < 0) unit.MP = 0;
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 2){
					MG_CALC_Damage.I._damageUnit(unit, u, 20, "magic");
					MG_ControlSFX.I._createSFX ("hit01", u.posX, u.posY);
					MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY); 
				}
			}
		}
		for(int i = 1; i <= 7; i++){
			MG_ControlSFX.I._createTimer(1, 0.15f * i, unit.posX, unit.posY);
			MG_ControlSFX.I._createTimer(1, 0.15f * i, unit.posX, unit.posY);
			MG_ControlSFX.I._createTimer(1, 0.15f * i, unit.posX, unit.posY);
			MG_ControlSFX.I._createTimer(1, 0.15f * i, unit.posX, unit.posY);
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Blade Dance!");
		MG_ControlSounds.I._playSound(30, unit.posX, unit.posY, true);
	}
	#endregion
	#region "ROBIN - Hook Dance"
	public void gameAction_robin_hookdance(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("hookdance", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Hook Dance!");
		MG_ControlSounds.I._playSound(16, posX, posY, true);
	}
	#endregion

	#region "AJAX - Spear Toss"
	public void gameAction_ajax_spearToss(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("speartoss", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Spear Toss!");
		MG_ControlSounds.I._playSound(31, unit.posX, unit.posY, true);
	}
	#endregion
	#region "AJAX - Hold the Line"
	public void gameAction_ajax_holdTheLine(MG_ClassUnit unit){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("warhornEffect", unit.posX, unit.posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_ControlBuffs.I._addBuff(u, "HoldTheLinePlus");
				}
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Hold the Line!");
		MG_ControlSounds.I._playSound(32, unit.posX, unit.posY, true);
	}
	#endregion
	#region "AJAX - All out assault"
	public void gameAction_ajax_allOutAssault(MG_ClassUnit unit){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("warhornEffect", unit.posX, unit.posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_ControlBuffs.I._addBuff(u, "AllOutAssault");
				}
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "All-out Assault!");
		MG_ControlSounds.I._playSound(32, unit.posX, unit.posY, true);
	}
	#endregion

	#region "COLT - Quick Draw"
	public void gameAction_colt_quickDraw(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("QuickDrawMain", unit.posX, unit.posY, posX, posY, unit.unitID);
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.1", "8", unit.unitID.ToString(), posX.ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.2", "8", unit.unitID.ToString(), posX.ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.3", "8", unit.unitID.ToString(), posX.ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.4", "8", unit.unitID.ToString(), posX.ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.5", "8", unit.unitID.ToString(), posX.ToString(), posY.ToString() });
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Quick Draw!");
		MG_ControlSounds.I._playSound(20, unit.posX, unit.posY, true);
	}
	#endregion
	#region "COLT - Dynamite"
	public void gameAction_colt_dynamite(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("Dynamite", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Dynamite!");
		MG_ControlSounds.I._playSound(32, unit.posX, unit.posY, true);
	}
	#endregion
	#region "COLT - Disarming Shot"
	public void gameAction_colt_disarmingShot(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 7; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("DisarmShot", unit.posX, unit.posY, posX, posY + 1, unit.unitID);
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.1", "7", unit.unitID.ToString(), (posX-1).ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.2", "7", unit.unitID.ToString(), (posX).ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.3", "7", unit.unitID.ToString(), (posX+1).ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.4", "7", unit.unitID.ToString(), (posX).ToString(),  (posY-1).ToString() });
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Disarming Shot!");
		MG_ControlSounds.I._playSound(20, unit.posX, unit.posY, true);
	}
	#endregion

	#region "VICTORIA - Heal"
	public void gameAction_victoria_heal(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonHoly01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(!MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_CALC_Healing.I._HP_Heal(unit, u, 100);
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Heal!");
		unit.action_skill = false;
		MG_ControlSounds.I._playSound(24, posX, posY, true);
	}
	#endregion
	#region "VICTORIA - Shell"
	public void gameAction_victoria_shell(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonHoly02", posX, posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distBetweenPoints(posX, posY, u.posX, u.posY) <= 2){
					MG_ControlSFX.I._createSFX("cartoonHoly02", u.posX, u.posY);
					MG_ControlBuffs.I._addBuff (u, "Shell");
				}
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Shell!");
		unit.action_skill = false;
		MG_ControlSounds.I._playSound(25, posX, posY, true);
	}
	#endregion
	#region "VICTORIA - Blessing"
	public void gameAction_victoria_blessing(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 5;
		MG_ControlSFX.I._createSFX("cartoonHoly01", posX, posY);
		MG_ControlSFX.I._createSFX("cartoonHoly02", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(!MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_ControlBuffs.I._addBuff (u, "Blessing");
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Blessing!");
		unit.action_skill = false;
		MG_ControlSounds.I._playSound(25, posX, posY, true);
	}
	#endregion

	#region "AMY - Scout"
	public void gameAction_amy_scout(MG_ClassUnit unit, int posX, int posY){
		// Max summoned unit check
		_maxSummonedUnitCheck(unit, 1);

		// Effect
		unit.MP -= 20; if(unit.MP < 0) unit.MP = 0;
		MG_ControlUnits.I._createUnit("WeissDummy", posX, posY, unit.playerOwner);
		MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = unit.name;
		MG_Globals.I.players[unit.playerOwner]._HERO_addSummonedUnit(unit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

		MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "Weiss");
		MG_ControlBuffs.I._addBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife");
		MG_ControlBuffs.I._getBuff(MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1], "TimedLife").turnDuration = 4;

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Scout!");
	}
	#endregion
	#region "AMY - Multi Shot"
	public void gameAction_amy_multishot(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("multishotMain", unit.posX, unit.posY, posX, posY, unit.unitID);
		MG_ControlMissile.I._createMissile("multishot", unit.posX, unit.posY, posX-1, posY-1, unit.unitID);
		MG_ControlMissile.I._createMissile("multishot", unit.posX, unit.posY, posX+1, posY-1, unit.unitID);
		MG_ControlMissile.I._createMissile("multishot", unit.posX, unit.posY, posX-1, posY+1, unit.unitID);
		MG_ControlMissile.I._createMissile("multishot", unit.posX, unit.posY, posX+1, posY+1, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Multi Shot!");
		MG_ControlSounds.I._playSound(14, unit.posX, unit.posY, true);
	}
	#endregion
	#region "AMY - Arc Shot"
	public void gameAction_amy_arcShot(MG_ClassUnit unit){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;
		MG_ControlBuffs.I._addBuff(unit, "ArcShot");
		MG_ControlSFX.I._createSFX("cartoonPowerup01", unit.posX, unit.posY);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Arc Shot!");
		MG_ControlSounds.I._playSound(27, unit.posX, unit.posY, true);
	}
	#endregion

	#region "ALICIA - Recruit"
	public void gameAction_alicia_summonRecruit(MG_ClassUnit unit, int posX, int posY){
		// Max summoned unit check
		_maxSummonedUnitCheck(unit, 3);

		// Effect
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlUnits.I._createUnit("Recruit", posX, posY, unit.playerOwner);
		MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = unit.name;
		MG_Globals.I.players[unit.playerOwner]._HERO_addSummonedUnit(unit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

		MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = unit.name;
		MG_ControlSFX.I._createSFX("moveSmoke", posX, posY);

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Recruit!");
	}
	#endregion
	#region "ALICIA - Close Combat"
	public void gameAction_alicia_closeCombat(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 75; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX ("cartoonHit01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_CALC_Damage.I._damageUnit (unit, u, 40, "magic");
				MG_ControlBuffs.I._addBuff (u, "CloseCombat");
				MG_ControlBuffs.I._addBuff (u, "Stun");
			}
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Close Combat!");
		MG_ControlSounds.I._playSound(22, posX, posY, true);
	}
	#endregion

	#region "ELDER TREANT - Entangle"
	public void gameAction_elderTreant_entangle(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonNature01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if (MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)) {
				MG_CALC_Damage.I._damageUnit (unit, u, 50, "magic");
				MG_ControlBuffs.I._addBuff (u, "Entangle");
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Entangle!");
		unit.action_skill = false;
		MG_ControlSounds.I._playSound(26, posX, posY, true);
	}
	#endregion
	#region "ELDER TREANT - Root Armor"
	public void gameAction_elderTreant_rootArmor(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonNature01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(!MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_ControlBuffs.I._addBuff (u, "RootArmor");
			}
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Root Armor!");
		MG_ControlSounds.I._playSound(26, posX, posY, true);
	}
	#endregion
	#region "ELDER TREANT - Fairies"
	public void gameAction_elderTreant_fairies(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonNature01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(!MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_ControlBuffs.I._addBuff (u, "Fairies");
			}
		}
		unit.action_skill = false;
	}
	#endregion

	#region "RAGNAROS - Fury"
	public void gameAction_ragnaros_fury(MG_ClassUnit unit){
		MG_ControlBuffs.I._addBuff(unit, "Fury");
		MG_ControlSFX.I._createSFX("cartoonExplodeFire02", unit.posX, unit.posY);
		MG_ControlSFX.I._createSFX("explodeDust01", unit.posX, unit.posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_CALC_Damage.I._damageUnit(unit, u, 30, "magic");
					MG_ControlSFX.I._createSFX ("cartoonHit04", u.posX, u.posY);
				}
			}
		}
		_specialEffect_outward(unit.posX, unit.posY, "cartoonExplodeFire01", 3, 0.2, 0.2);
		_specialEffect_outward(unit.posX, unit.posY, "cartoonHit04", 3, 0.2, 0.2);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Fury!");
		MG_ControlSounds.I._playSound(17, unit.posX, unit.posY, true);
	}
	#endregion

	#region "IFREET - Spiral"
	public void gameAction_ifreet_spiral(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 25; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("Spiral", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit._moveUnit(posX, posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 2){
					MG_CALC_Damage.I._damageUnit(unit, u, 30, "magic");
					MG_ControlSFX.I._createSFX ("cartoonHit04", u.posX, u.posY);
				}
			}
		}
		MG_ControlSFX.I._createSFX ("explodeDust01", posX, posY);
		_specialEffect_outward(posX, posY, "cartoonExplodeFire01", 2, 0.2, 0.2);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Spiral!");
		MG_ControlSounds.I._playSound(17, posX, posY, true);
	}
	#endregion
	#region "IFREET - Explosion"
	public void gameAction_ifreet_explosion(MG_ClassUnit unit){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_CALC_Damage.I._damageUnit(unit, u, 30, "magic");
					MG_ControlSFX.I._createSFX ("cartoonHit04", u.posX, u.posY);
				}
			}
		}
		_specialEffect_outward(unit.posX, unit.posY, "cartoonExplodeFire01", 3, 0.2, 0.2);
		_specialEffect_outward(unit.posX, unit.posY, "cartoonHit04", 3, 0.2, 0.2);
		MG_ControlSFX.I._createSFX ("explodeDust01", unit.posX, unit.posY);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Explosion!");
		MG_ControlSounds.I._playSound(17, unit.posX, unit.posY, true);
	}
	#endregion
	#region "IFREET - Burning Grip"
	public void gameAction_ifreet_burningGrip(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 75; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX ("cartoonHit04", posX, posY);
		MG_ControlSFX.I._createSFX ("slam01", posX, posY);
		for(int i = 1; i <= 3; i++){
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (posX + i).ToString(),  (posY + i).ToString() });
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (posX - i).ToString(),  (posY + i).ToString() });
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (posX + i).ToString(),  (posY - i).ToString() });
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "cartoonExplodeFire01", (posX - i).ToString(),  (posY - i).ToString() });

			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (posX + i).ToString(),  (posY + i).ToString() });
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (posX - i).ToString(),  (posY + i).ToString() });
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (posX + i).ToString(),  (posY - i).ToString() });
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.2 * i).ToString(), "9", "slam01", (posX - i).ToString(),  (posY - i).ToString() });
		}
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if (MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)) {
				if(((float)u.HP / (float)u.HPMax) <= 0.25f)
					MG_CALC_Damage.I._damageUnit (unit, u, 9999, "pure");
				else 
					MG_CALC_Damage.I._damageUnit (unit, u, 30, "magic");
			}
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Burning Grip!");
		MG_ControlSounds.I._playSound(17, posX, posY, true);
	}
	#endregion

	#region "SPEC WITCH - Summon Spectre"
	public void gameAction_specWitch_summonSpectre(MG_ClassUnit unit, int posX, int posY){
		// Max summoned unit check
		_maxSummonedUnitCheck(unit, 3);

		// Effect
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlUnits.I._createUnit("Spectre", posX, posY, unit.playerOwner);
		MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = unit.name;
		MG_Globals.I.players[unit.playerOwner]._HERO_addSummonedUnit(unit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);
		MG_ControlMissile.I._createMissile("Spectre1", unit.posX, unit.posY, posX, posY, unit.unitID);
		MG_ControlSFX.I._createSFX("cartoonDeathBall", posX, posY);

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Summon Spectre!");
		MG_ControlSounds.I._playSound(28, posX, posY, true);
	}
	#endregion
	#region "SPEC WITCH - Blink"
	public void gameAction_specWitch_blink(MG_ClassUnit unit, int posX, int posY){
		MG_ControlSFX.I._createSFX("moveSmoke", unit.posX, unit.posY);
		unit._moveUnit(posX, posY);
		MG_ControlSFX.I._createSFX("moveSmoke", posX, posY);

		unit.action_skill = false;
		MG_ControlSounds.I._playSound(6, posX, posY, true);
	}
	#endregion
	#region "SPEC WITCH - Mirror"
	public void gameAction_specWitch_mirror(MG_ClassUnit unit){
		unit.MP -= 75; if(unit.MP < 0) unit.MP = 0;
		MG_ControlBuffs.I._addBuff(unit, "Mirror");
		MG_ControlSFX.I._createSFX ("cartoonPowerup01", unit.posX, unit.posY); 
		foreach(MG_ClassUnit un in MG_Globals.I.units){
			if(un.playerOwner == unit.playerOwner && un.name == "Spectre"){
				MG_ControlBuffs.I._addBuff(un, "Mirror");
				MG_ControlSFX.I._createSFX ("cartoonPowerup01", un.posX, un.posY); 
			}
		}
		unit.action_skill = false;
	}
	#endregion

	#region "DRAKGUL - Eye of the Magi"
	public void gameAction_drakgul_eye(MG_ClassUnit unit, int posX, int posY){
		// Max summoned unit check:
		_maxSummonedUnitCheck(unit, 2);

		// Effect
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlUnits.I._createUnit("EyeOfTheMagi", posX, posY, unit.playerOwner);
		MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = unit.name;
		MG_Globals.I.players[unit.playerOwner]._HERO_addSummonedUnit(unit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

		MG_ControlSFX.I._createSFX("drakgul02", posX, posY);
		MG_ControlSFX.I._createSFX("cartoonSpark01", posX, posY);

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Eye of the Magi!");
	}
	#endregion
	#region "DRAKGUL - Dispel"
	public void gameAction_drakgul_dispel(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("drakgul01", posX, posY);
		MG_ControlSFX.I._createSFX("cartoonSpark01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_ControlBuffs.I._addBuff (u, "Silence");
				MG_ControlBuffs.I._dispel_buffs (u);
			}else{
				MG_ControlBuffs.I._dispel_debuffs (u);
			}
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Anti-magic!");
	}
	#endregion
	#region "DRAKGUL - Concentration"
	public void gameAction_drakgul_concentration(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("drakgul02", posX, posY);
		MG_ControlSFX.I._createSFX("cartoonSpark01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(!MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_CALC_Healing.I._HP_Heal(unit, u, 60);
				MG_CALC_Healing.I._MP_Heal(unit, u, 40);
			}
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Concentrate!");
	}
	#endregion

	#region "MASAMUNE - Wind Slash"
	public void gameAction_masamune_windSlash(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("WindSlash", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Wind Slash!");
		MG_ControlSounds.I._playSound(33, unit.posX, unit.posY, true);
	}
	#endregion
	#region "MASAMUNE - Azure Dragon 2"
	public void gameAction_masamune_azureDragon(MG_ClassUnit unit){
		if(unit.HP > (int)((double)unit.HPMax * 0.48)){
			unit.HP = (int)((double)unit.HPMax * 0.48);
			unit._updateHealthBar();
			MG_ControlBuffs.I._addBuff(unit, "AzureDragon2");
			unit._transformSprite("MasamuneAzure", 9999);
		}
		MG_ControlSFX.I._createSFX("cartoonPowerup01", unit.posX, unit.posY);
		MG_UIControl_UseSkill.I._show(unit, "Azure Dragon!");
		unit.action_skill = false;
		MG_ControlSounds.I._playSound(27, unit.posX, unit.posY, true);
	}
	#endregion

	#region "SIEGFRIED - Heavy Charge"
	public void gameAction_siegfried_heavyCharge(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlUnits.I.unitTweenMode = 1;
		unit._tweenMove(new Vector2((float)posX/2, (float)posY/2), 3f, false);
		MG_Globals.I.pause_uiMove = true;
		if(MG_CALC_Distance.I._distBetweenPoints(unit.posX, unit.posY, posX, posY) >= 4){
			MG_ControlBuffs.I._addBuff (unit, "MythrilLance");
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Heavy Charge!");
		MG_ControlSounds.I._playSound(21, unit.posX, unit.posY, true);
	}
	#endregion

	#region "ABEL - Smite"
	public void gameAction_abel_smite(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX ("cartoonExplodeFire01", posX, posY);
		MG_ControlSFX.I._createSFX("cartoonHoly02", posX, posY);
		MG_ControlSFX.I._createSFX("explodeDust01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if (MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)) {
				MG_CALC_Damage.I._damageUnit (unit, u, 75, "magic");
			}
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Smite!");
		MG_ControlSounds.I._playSound(17, posX, posY, true);
	}
	#endregion
	#region "ABEL - Test of Faith"
	public void gameAction_abel_testOfFaith(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonHoly02", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(!MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_ControlBuffs.I._addBuff (u, "TestOfFaith_ally");
			}else{
				MG_ControlBuffs.I._addBuff (u, "TestOfFaith_enemy");
			}
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Test of Faith!");
		MG_ControlSounds.I._playSound(25, posX, posY, true);
	}
	#endregion

	#region "YUKINO - Frost Nova"
	public void gameAction_yukino_frostNova(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonSnow01", posX, posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distBetweenPoints(posX, posY, u.posX, u.posY) <= 2){
					MG_CALC_Damage.I._damageUnit(unit, u, 70, "magic");
					MG_ControlSFX.I._createSFX ("cartoonExplodeIce01", u.posX, u.posY);
				}
			}
		}
		_specialEffect_outward(posX, posY, "cartoonExplodeIce01", 2, 0.2, 0.2);
		MG_UIControl_UseSkill.I._show(unit, "Frost Nova!");
		MG_ControlSounds.I._playSound(38, posX, posY, true);
		unit.action_skill = false;
	}
	#endregion
	#region "YUKINO - Frost Wave"
	public void gameAction_yukino_frostWave(MG_ClassUnit unit){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonSnow01", unit.posX, unit.posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_CALC_Damage.I._damageUnit(unit, u, 75, "magic");
					MG_ControlBuffs.I._addBuff(u, "FrostWave");
					MG_ControlSFX.I._createSFX ("cartoonExplodeIce01", u.posX, u.posY);
				}
			}
		}
		_specialEffect_outward(unit.posX, unit.posY, "cartoonExplodeIce01", 3, 0.2, 0.2);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Frost Wave!");
		MG_ControlSounds.I._playSound(38, unit.posX, unit.posY, true);
	}
	#endregion
	#region "YUKINO - Silence"
	public void gameAction_yukino_silence(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distBetweenPoints(posX, posY, u.posX, u.posY) <= 1){
					MG_ControlBuffs.I._addBuff (u, "Silence");
				}
			}
		}
		MG_ControlSFX.I._createSFX("cartoonSnow01", posX, posY);
		MG_ControlSFX.I._createSFX ("cartoonPowerup01", posX, posY);
		MG_ControlSFX.I._createSFX ("cartoonPowerup01", posX-1, posY);
		MG_ControlSFX.I._createSFX ("cartoonPowerup01", posX+1, posY);
		MG_ControlSFX.I._createSFX ("cartoonPowerup01", posX, posY-1);
		MG_ControlSFX.I._createSFX ("cartoonPowerup01", posX, posY+1);
		MG_UIControl_UseSkill.I._show(unit, "Silence!");
		//MG_ControlSounds.I._playSound(17, MG_ControlCursor.I.posX, MG_ControlCursor.I.posY, true);
		unit.action_skill = false;
	}
	#endregion

	#region "CODY - Blink Strike"
	public void gameAction_cody_blinkStrike(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("moveSmoke", unit.posX, unit.posY);
		if (MG_GetUnit.I._pointHasUnit (posX, posY)) {
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if (MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)) {
				MG_CALC_Damage.I._damageUnit (unit, u, 75, "magic");
				MG_ControlSFX.I._createSFX ("moveSmoke", unit.posX, unit.posY);
				MG_ControlSFX.I._createSFX ("hit01", posX, posY); 
				MG_ControlSFX.I._createSFX ("cartoonHit01", posX, posY); 
				MG_ControlSounds.I._playSound (22, posX, posY, true);
				MG_ControlBuffs.I._addBuff (u, "BlinkStrike");

				if (unit.facing == "right") {
					unit._moveUnit (posX - 1, posY);
					MG_ControlSFX.I._createSFX ("moveSmoke", posX - 1, posY);
				} else {
					unit._moveUnit (posX + 1, posY);
					MG_ControlSFX.I._createSFX ("moveSmoke", posX + 1, posY);
				}
			}
		} else {
			unit._moveUnit(posX, posY);
			MG_ControlSFX.I._createSFX("moveSmoke", posX, posY);
		}
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Blink Strike!");
	}
	#endregion

	#region "HILDE - Photon Bomb"
	public void gameAction_hilde_photonBomb(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distBetweenPoints(u.posX, u.posY, posX, posY) <= 2){
					MG_CALC_Damage.I._damageUnit(unit, u, 75, "magic");
				}
			}
		}
		_specialEffect_outward(posX, posY, "photonBomb", 2, 0.2, 0.2);
		MG_UIControl_UseSkill.I._show(unit, "Photon Bomb!");
		MG_ControlSounds.I._playSound(44, posX, posY, true);
	}
	#endregion
	#region "HILDE - Arcane Bolt"
	public void gameAction_hilde_arcaneBolt(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 75; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("ArcaneBolt", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Arcane Bolt!");
	}
	#endregion

	#region "ELISE - Multi-Strike"
	public void gameAction_elise_multiStrike(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		bool ms_isEven = false;
		for(int i = 0; i < 4; i++){
			ms_isEven = !ms_isEven;
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.15 * i).ToString(), "9", (ms_isEven) ? "hit01" : "hit01_Reverse", (posX).ToString(),  (posY).ToString() });
			MG_ControlEvents.I._addEvent("Wait", new string[]{ (0.15 * i).ToString(), "10", (ms_isEven) ? "22" : "23", (posX).ToString(),  (posY).ToString() });
		}
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if (MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)) {
				MG_CALC_Damage.I._damageUnit (unit, u, 75, "magic");
				MG_ControlSFX.I._createSFX ("cartoonHit01", posX, posY); 
				MG_ControlBuffs.I._addBuff(unit, "PhoenixWings");
				MG_ControlBuffs.I._addBuff(unit, "PhoenixWings");
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Multi-Strike!");
	}
	#endregion
	#region "ELISE - Phoenix Wings"
	public void gameAction_elise_phoenixWings(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 25; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("moveSmoke", unit.posX, unit.posY);
		unit._moveUnit(posX, posY);
		MG_ControlSFX.I._createSFX("moveSmoke", unit.posX, unit.posY);
		MG_ControlSounds.I._playSound(6, posX, posY, true);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Phoenix Wings!");
	}
	#endregion

	#region "MAY - Sacred Knights"
	public void gameAction_may_sacredKnights(MG_ClassUnit unit, int posX, int posY){
		// Max summoned unit check
		_maxSummonedUnitCheck(unit, 3);

		// Effect
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlUnits.I._createUnit("SacredKnight", posX, posY, unit.playerOwner);
		MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = unit.name;
		MG_Globals.I.players[unit.playerOwner]._HERO_addSummonedUnit(unit.name, MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].unitID);

		MG_Globals.I.unitsTemp[MG_Globals.I.unitsTemp.Count-1].summonedHeroName = unit.name;
		MG_ControlSFX.I._createSFX("moveSmoke", posX, posY);

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Sacred Knights!");
	}
	#endregion

	#region "WALTER - Burst Fire"
	public void gameAction_walter_burstFire(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 40; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("BurstFireMain", unit.posX, unit.posY, posX, posY + 1, unit.unitID);
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.03", "15", unit.unitID.ToString(), (posX).ToString(),  (posY + 1).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.06", "15", unit.unitID.ToString(), (posX).ToString(),  (posY + 1).ToString() });

		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.09", "14", unit.unitID.ToString(), (posX-1).ToString(),  (posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.12", "15", unit.unitID.ToString(), (posX-1).ToString(),  (posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.15", "15", unit.unitID.ToString(), (posX-1).ToString(),  (posY).ToString() });

		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.18", "14", unit.unitID.ToString(), (posX).ToString(),  (posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.21", "15", unit.unitID.ToString(), (posX).ToString(),  (posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.24", "15", unit.unitID.ToString(), (posX).ToString(),  (posY).ToString() });

		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.27", "14", unit.unitID.ToString(), (posX+1).ToString(),  (posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.3", "15", unit.unitID.ToString(), (posX+1).ToString(),  (posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.33", "15", unit.unitID.ToString(), (posX+1).ToString(),  (posY).ToString() });

		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.36", "14", unit.unitID.ToString(), (posX).ToString(),  (posY-1).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.39", "15", unit.unitID.ToString(), (posX).ToString(),  (posY-1).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.42", "15", unit.unitID.ToString(), (posX).ToString(),  (posY-1).ToString() });
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Burst Fire!");
		MG_ControlSounds.I._playSound(40, unit.posX, unit.posY, true);
	}
	#endregion
	#region "WALTER - Demonic Slash"
	public void gameAction_walter_demonicSlash(MG_ClassUnit unit){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 2){
					MG_CALC_Damage.I._damageUnit(unit, u, 30, "magic");
					MG_ControlSFX.I._createSFX ("hit01", u.posX, u.posY);
					MG_ControlSFX.I._createSFX ("cartoonHit01", u.posX, u.posY); 
				}
			}
		}
		MG_ControlMissile.I._createMissile("WindSlashBlue", unit.posX, unit.posY, unit.posX, unit.posY+3, unit.unitID);
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.02", "16", unit.unitID.ToString(), (unit.posX+1).ToString(),  (unit.posY+2).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.04", "16", unit.unitID.ToString(), (unit.posX+2).ToString(),  (unit.posY+1).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.06", "16", unit.unitID.ToString(), (unit.posX+3).ToString(),  (unit.posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.08", "16", unit.unitID.ToString(), (unit.posX+2).ToString(),  (unit.posY-1).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.10", "16", unit.unitID.ToString(), (unit.posX+1).ToString(),  (unit.posY-2).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.12", "16", unit.unitID.ToString(), (unit.posX).ToString(),  (unit.posY-3).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.14", "16", unit.unitID.ToString(), (unit.posX-2).ToString(),  (unit.posY-1).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.16", "16", unit.unitID.ToString(), (unit.posX-1).ToString(),  (unit.posY-2).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.18", "16", unit.unitID.ToString(), (unit.posX-3).ToString(),  (unit.posY).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.20", "16", unit.unitID.ToString(), (unit.posX-1).ToString(),  (unit.posY+2).ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.22", "16", unit.unitID.ToString(), (unit.posX-2).ToString(),  (unit.posY+1).ToString() });

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Demonic Slash!");
		MG_ControlSounds.I._playSound(30, unit.posX, unit.posY, true);
	}
	#endregion

	#region "ALERIC - Axe Throw"
	public void gameAction_aleric_axeThrow(MG_ClassUnit unit, int posX, int posY, string missileType, int manaCost){
		unit.MP -= manaCost; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile(missileType, unit.posX, unit.posY, posX, posY, unit.unitID);
		MG_UIControl_UseSkill.I._show(unit, "Axe Throw!");
		MG_ControlSounds.I._playSound(31, unit.posX, unit.posY, true);
	}
	#endregion
	#region "ALERIC - Mighty Slam"
	public void gameAction_aleric_mightySlam(MG_ClassUnit unit, int posX, int posY, bool isBoss, int manaCost){
		unit.MP -= manaCost; if(unit.MP < 0) unit.MP = 0;

		MG_ControlSFX.I._createSFX("moveSmoke", unit.posX, unit.posY);

		// Blink part
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			if(unit.facing == "right"){
				unit._moveUnit(posX - 1, posY);
			}else{
				unit._moveUnit(posX + 1, posY);
			}
		}else{
			unit._moveUnit(posX, posY);
		}
		MG_ControlSFX.I._createSFX("moveSmoke", unit.posX, unit.posY);
		MG_ControlSFX.I._createSFX("slam01", unit.posX, unit.posY);
		MG_ControlSFX.I._createSFX("slam02", unit.posX, unit.posY);

		// Damage part
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distBetweenPoints(u.posX, u.posY, posX, posY) <= 2){
					MG_CALC_Damage.I._damageUnit(unit, u, (u.isAHero) ? 80 : 50, "magic");
					MG_ControlSFX.I._createSFX("cartoonHit01", u.posX, u.posY);
				}
			}
		}

		MG_UIControl_UseSkill.I._show(unit, "Mighty Slam!");
		MG_ControlSounds.I._playSound(43, posX, posY, true);
	}
	#endregion

	#region "EVE - Death Arrow"
	public void gameAction_eve_deathArrow(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 25; if(unit.MP < 0) unit.MP = 0;
		MG_CALC_Damage.I._HP_Loss(unit, 50);
		MG_ControlMissile.I._createMissile("DeathArrow", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Death Arrow!");
		MG_ControlSounds.I._playSound(14, unit.posX, unit.posY, true);
	}
	#endregion
	#region "EVE - Drain Life"
	public void gameAction_eve_drainLife(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 25; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("drainLifeHit", posX, posY);
		MG_ControlSounds.I._playSound(46, unit.posX, unit.posY, true);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if (MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)) {
				MG_CALC_Damage.I._damageUnit (unit, u, 50, "magic");
				MG_ControlMissile.I._createMissile("DrainLife", posX, posY, unit.posX, unit.posY, unit.unitID);
			}
		}

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Drain Life!");
	}
	#endregion
	
	#region "ELIZABETH - Amplify"
	public void gameAction_elizabeth_amplify(MG_ClassUnit unit){
		unit.MP -= 50; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("warhornEffect", unit.posX, unit.posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_ControlBuffs.I._addBuff(u, "Amplify");
				}
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Amplify!");
		MG_ControlSounds.I._playSound(32, unit.posX, unit.posY, true);
	}
	#endregion
	#region "ELIZABETH - Lightning Bolt"
	public void gameAction_elizabeth_lightningBolt(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("LightningBolt", unit.posX, unit.posY, posX, posY + 1, unit.unitID);
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.05", "32", unit.unitID.ToString(), (posX-1).ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.1", "32", unit.unitID.ToString(), (posX).ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.15", "32", unit.unitID.ToString(), (posX+1).ToString(),  posY.ToString() });
		MG_ControlEvents.I._addEvent("Wait", new string[]{ "0.2", "32", unit.unitID.ToString(), (posX).ToString(),  (posY-1).ToString() });
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Lightning Bolt!");
		MG_ControlSounds.I._playSound(13, unit.posX, unit.posY, true);
	}
	#endregion

	#region "EYE OF THE MAGI - Explode"
	public void gameAction_eye_explode(MG_ClassUnit unit){
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 2){
					MG_CALC_Damage.I._HP_Loss(u, 50);
				}
			}
		}
		_specialEffect_outward(unit.posX, unit.posY, "cartoonHit05", 2, 0.2, 0.2);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Explode!");
		unit.isAlive = false;
		MG_ControlUnits.I._addToDestroyList(unit);
	}
	#endregion

	#region "SHARED - Barrier"
	public void gameAction_barrier(MG_ClassUnit unit){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("warhornEffectBlue", unit.posX, unit.posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_ControlBuffs.I._addBuff(u, (u.playerOwner == 1) ? "BarrierBlue" : "BarrierRed");
					MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX, u.posY);
				}
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Barrier!");
		MG_ControlSounds.I._playSound(47, unit.posX, unit.posY, true);
	}
	#endregion
	#region "SHARED - Potion"
	public void gameAction_potion(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonHoly01", posX, posY);
		if(MG_GetUnit.I._pointHasUnit (posX, posY)){
			MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);
			if(!MG_ControlPlayer.I._getIsEnemy (unit.playerOwner, u.playerOwner)){
				MG_CALC_Healing.I._HP_Heal(unit, u, 40);
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Potion!");
		unit.action_skill = false;
		MG_ControlSounds.I._playSound(24, posX, posY, true);
	}
	#endregion
	#region "SHARED - Heal Spray"
	public void gameAction_healSpray(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("cartoonHoly02", posX, posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distBetweenPoints(posX, posY, u.posX, u.posY) <= 2){
					MG_ControlSFX.I._createSFX("cartoonHoly02", u.posX, u.posY);
					MG_CALC_Healing.I._HP_Heal(unit, u, 20);
				}
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Healing Spray!");
		unit.action_skill = false;
		MG_ControlSounds.I._playSound(25, posX, posY, true);
	}
	#endregion
	#region "SHARED - Mythril Revolver"
	public void gameAction_mythrilRevolver(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("MythrilRevolver", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Mythril Revolver!");
		MG_ControlSounds.I._playSound(19, unit.posX, unit.posY, true);
	}
	#endregion
	#region "SHARED - Grenade"
	public void gameAction_grenade(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("Grenade", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Grenade!");
		MG_ControlSounds.I._playSound(31, unit.posX, unit.posY, true);
	}
	#endregion
	#region "SHARED - Upgrade"
	public void gameAction_upgrade(MG_ClassUnit unit, int posX, int posY, string upgTo){
		MG_ControlSFX.I._createSFX("cartoonHoly01", posX, posY);
		MG_ControlSFX.I._createSFX("cartoonHoly02", posX, posY);
		MG_ClassUnit u = MG_GetUnit.I._pickUnit (posX, posY);

		MG_ControlUnits.I._addToDestroyList (u);
		MG_ControlUnits.I._createUnit(upgTo, posX, posY, unit.playerOwner);

		MG_UIControl_UseSkill.I._show(unit, "Upgrade!");
		MG_ControlSounds.I._playSound(26, posX, posY, true);
	}
	#endregion
	#region "SHARED - Upgrade"
	public void gameAction_upgrade2(MG_ClassUnit unit, string upgradeFrom, string upgradeTo, int upgradeCount){
		List<MG_ClassUnit> unitUpgList = new List<MG_ClassUnit>();
		List<Vector2> unitUpgPoint = new List<Vector2>();
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(u.name == upgradeFrom && u.playerOwner == unit.playerOwner){
				unitUpgList.Add(u);
				unitUpgPoint.Add(new Vector2(u.posX, u.posY));
				if(unitUpgList.Count >= upgradeCount){
					break;
				}
			}
		}
		for(int i = unitUpgList.Count-1; i >= 0; i--){
			MG_ControlUnits.I._addToDestroyList (unitUpgList[i]);
			MG_ControlUnits.I._createUnit(upgradeTo, (int)unitUpgPoint[i].x, (int)unitUpgPoint[i].y, unit.playerOwner);
			MG_ControlSFX.I._createSFX("cartoonHoly02", (int)unitUpgPoint[i].x, (int)unitUpgPoint[i].y);
		}
		MG_UIControl_UseSkill.I._show(unit, "Upgrade!");
	}
	#endregion
	#region "SHARED - Artillery"
	public void gameAction_artillery(MG_ClassUnit unit, int posX, int posY, int posX2, int posY2, int posX3, int posY3){
		int[] artPosX = {posX, posX2, posX3};
		int[] artPosY = {posY, posY2, posY3};
		for(int i = 0; i < artPosX.Length; i++){
			MG_ControlSFX.I._createSFX ("cartoonExplodeFire01", artPosX[i], artPosY[i], true);
			MG_ControlSFX.I._createSFX ("slam01", artPosX[i], artPosY[i], true);
			MG_ControlSFX.I._createSFX ("slam02", artPosX[i], artPosY[i], true);
			if(MG_GetUnit.I._pointHasUnit (artPosX[i], artPosY[i])){
				MG_ClassUnit u = MG_GetUnit.I._pickUnit (artPosX[i], artPosY[i]);
				if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
					MG_CALC_Damage.I._damageUnit(unit, u, ((u.isABuilding) ? 70 : 40), "magic");
				}
			}
		}
		MG_ControlSounds.I._playSound(18, posX, posY, false);

		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Artillery!");
	}
	#endregion
	#region "SHARED - Grapeshot"
	public void gameAction_grapeshot(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 5; if(unit.MP < 0) unit.MP = 0;
		MG_ControlMissile.I._createMissile("grapeshotMain", unit.posX, unit.posY, posX, posY, unit.unitID);
		MG_ControlMissile.I._createMissile("grapeshot", unit.posX, unit.posY, posX-1, posY-1, unit.unitID);
		MG_ControlMissile.I._createMissile("grapeshot", unit.posX, unit.posY, posX+1, posY-1, unit.unitID);
		MG_ControlMissile.I._createMissile("grapeshot", unit.posX, unit.posY, posX-1, posY+1, unit.unitID);
		MG_ControlMissile.I._createMissile("grapeshot", unit.posX, unit.posY, posX+1, posY+1, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Grapeshot!");
		MG_ControlSounds.I._playSound(50, unit.posX, unit.posY, true);
	}
	#endregion
	#region "SHARED - Acid Breath"
	public void gameAction_acidBreath(MG_ClassUnit unit, int posX, int posY){
		unit.MP -= 10; if(unit.MP < 0) unit.MP = 0;

		MG_ControlMissile.I._createMissile("AcidBreath", unit.posX, unit.posY, posX, posY, unit.unitID);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "Acid Breath!");
		MG_ControlSounds.I._playSound(33, unit.posX, unit.posY, true);
	}
	#endregion
	#region "SHARED - War Stomp"
	public void gameAction_warStomp(MG_ClassUnit unit){
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				if(MG_CALC_Distance.I._distUnits(unit, u) <= 3){
					MG_CALC_Damage.I._damageUnit(unit, u, 25, "magic");
				}
			}
		}
		_specialEffect_outward(unit.posX, unit.posY, "slam01", 3, 0.2, 0.2);
		_specialEffect_outward(unit.posX, unit.posY, "slam02", 3, 0.2, 0.2);
		MG_ControlSFX.I._createSFX ("explodeDust01", unit.posX, unit.posY);
		unit.action_skill = false;
		MG_UIControl_UseSkill.I._show(unit, "War Stomp!");
		MG_ControlSounds.I._playSound(43, unit.posX, unit.posY, true);
	}
	#endregion
	#region "SHARED - Unleash Hero"
	public void gameAction_unleashHero(MG_ClassUnit unit){
		MG_UIControl_UseSkill.I._show(unit, "Unleash!");
		MG_ControlBuffs.I._addBuff(unit, "Unleash");
		unit.btn3_orderDef = "skill";
		unit.btn5_orderDef = "none";
		unit.MS = 4;
		unit.sightRadius = 6;
		MG_ControlSounds.I._playSound(27, unit.posX, unit.posY, true);
		MG_ControlSFX.I._createSFX("cartoonHit04", unit.posX, unit.posY);
		MG_ControlSFX.I._createSFX("explodeDust01", unit.posX, unit.posY);
	}
	#endregion
	#region "SHARED - Global Barrier"
	public void gameAction_globalBarrier(MG_ClassUnit unit){
		unit.MP -= 0; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("warhornEffectBlue", unit.posX, unit.posY);
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(!MG_ControlPlayer.I._getIsEnemy(unit.playerOwner, u.playerOwner)){
				MG_ControlBuffs.I._addBuff(u, (u.playerOwner == 1) ? "BarrierBlue" : "BarrierRed");
				MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX, u.posY);
			}
		}
		MG_UIControl_UseSkill.I._show(unit, "Global Barrier!");
		MG_ControlSounds.I._playSound(47, unit.posX, unit.posY, true);
	}
	#endregion
	#region "SHARED - Double Card"
	public void gameAction_doubleCard(MG_ClassUnit unit){
		unit.MP -= 0; if(unit.MP < 0) unit.MP = 0;
		MG_ControlSFX.I._createSFX("warhornEffectBlue", unit.posX, unit.posY);
		
		MG_UIControl_UseSkill.I._show(unit, "Double Card!");
		MG_ControlSounds.I._playSound(47, unit.posX, unit.posY, true);
	}
	#endregion
	#endregion
	
	// Fix Bayonet
	public void gameAction_fixBayonet (MG_ClassUnit _u) {
		MG_ControlSFX.I._createSFX("warhornEffectBlue", _u.posX, _u.posY);
		MG_ControlSounds.I._playSound(47, _u.posX, _u.posY, true);
					
		_u.transform_unit (_u.name + "Bayonet");
	}
	
	// Remove Bayonet
	public void gameAction_removeBayonet (MG_ClassUnit _u) {
		MG_ControlSFX.I._createSFX("warhornEffectBlue", _u.posX, _u.posY);
		MG_ControlSounds.I._playSound(47, _u.posX, _u.posY, true);
					
		string _str = _u.name.Replace ("Bayonet", "");
		_u.transform_unit (_str);
	}

	//Includes:
	//	- special effect - outward
	//	_maxSummonedUnitCheck
	#region "MISC - Special Effect - Outward"
	public void _specialEffect_outward(int posX, int posY, string sfx, int mRange, double time, double delayPerWave){
		// Special visual effect
		for(int lrange = 0; lrange <= mRange; lrange++){
			bool isBreak = false;
			int lx = -lrange, ly = 0, lStatus = 0;
			while(!isBreak){
				MG_ControlEvents.I._addEvent("Wait", new string[]{ time.ToString(), "9", sfx, (posX + lx).ToString(),  (posY + ly).ToString() });

				if(lStatus == 0){
					lx++; ly++;
					if (lx >= 0) {
						lx = 0; ly = lrange;
						lStatus++;
					}
				}else if(lStatus == 1){
					lx++; ly--;
					if (lx >= lrange) {
						lx = lrange; ly = 0;
						lStatus++;
					}
				}else if(lStatus == 2){
					lx--; ly--;
					if (lx <= 0) {
						lx = 0; ly = -lrange;
						lStatus++;
					}
				}else if(lStatus == 3){
					lx--; ly++;
					if(lx <= -lrange){
						isBreak = true;
						time += delayPerWave;
					}
				}
			}
		}
	}
	#endregion
	#region "MISC - Max Summoned unit check"
	public void _maxSummonedUnitCheck(MG_ClassUnit owner, int maxSummonedNum){
		// Max summoned unit check
		if(MG_Globals.I.players[owner.playerOwner]._HERO_getSummonedUnitCount(owner.name) >= maxSummonedNum){
			if(MG_GetUnit.I._checkIfUnitWithIDExists(MG_Globals.I.players[owner.playerOwner]._HERO_getFirstSummonedUnitID(owner.name))){
				MG_ClassUnit sumUn = MG_GetUnit.I._getUnitWithID(MG_Globals.I.players[owner.playerOwner]._HERO_getFirstSummonedUnitID(owner.name));

				sumUn.isAlive = false;
				MG_ControlUnits.I._addToDestroyList(sumUn);
				MG_Globals.I.players[owner.playerOwner]._HERO_removeSummonedUnit(owner.name, sumUn.unitID);
			}
		}
	}
	#endregion
}
