using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_DebugUI : MonoBehaviour {
	public static MG_UIControl_DebugUI I;
	public void Awake(){ I = this; }

	/*
	 * 	Every code throughout the project that has debugging codes can
	 *  be found by looking for "DEV DEBUGGING" comments/regions
	 * 
	 */

	// CONSTANTS - ALL SET AT _start FUNCTION
	public bool DEBUG_DEV_MODE;
	public int DEBUG_TEXT_MAX_LINES;

	public Canvas c_debug;
	public Text t_debugText;
	public List<string> t_debugLines;
	public InputField if_debugConsole;
	public Button btn_debugConsole;

	// MODES
	public bool mode_cursorMoveUpdateReport;

	public void _start(){
		////////// SET HERE //////////
		/*DEBUG_DEV_MODE = false;
		MG_Globals.I.DEBUG_DEV_MODE = DEBUG_DEV_MODE;
		DEBUG_TEXT_MAX_LINES = 9;

		mode_cursorMoveUpdateReport = false;
		////////// SET HERE //////////

		if (!DEBUG_DEV_MODE) {
			c_debug.enabled = false;
			MG_Globals.I.DEBUG_DEV_MODE = false;
			Destroy (this.gameObject);
			return;
		}

		for(int i = 0; i < DEBUG_TEXT_MAX_LINES; i++){
			t_debugLines.Add ("");
		}

		_updateConsoleText ();*/
	}

	#region "CONSOLE TEXT CONTROL"
	public void _addConsoleTextLine(string newLine){
		for(int i = 1; i < DEBUG_TEXT_MAX_LINES; i++){
			t_debugLines [i - 1] = t_debugLines [i];
		}
		t_debugLines [DEBUG_TEXT_MAX_LINES-1] = newLine;

		_updateConsoleText ();
	}

	public void _updateConsoleText(){
		t_debugText.text = "";

		for(int i = 0; i < DEBUG_TEXT_MAX_LINES; i++){
			t_debugText.text += t_debugLines [i] + "\r\n";
		}
	}
	#endregion

	#region "CONSOLE COMMANDS"
	public void _executeConsoleCommand(){
		string fullCommand = if_debugConsole.text;
		string command = fullCommand.IndexOf (" ") > -1
							? fullCommand.Substring (0, fullCommand.IndexOf (" "))
							: fullCommand;
		string subCommand = fullCommand.IndexOf (" ") > -1
							? fullCommand.Substring (fullCommand.IndexOf (" ")+1,fullCommand.Length - command.Length - 1) : "";

		switch (command) {
			#region "cursorMoveUpdateReport"
			case "-cursorMoveUpdateReport":
				if(subCommand == "false" || subCommand == "0"){
					mode_cursorMoveUpdateReport = false;
				}else if(subCommand == "true" || subCommand == "1"){
					mode_cursorMoveUpdateReport = true;
				}
			break;
			#endregion // Updates where the cursor moves and what's the new selected unit and the order is
			#region "clear"
			case "-clear":
				for(int i = 1; i < DEBUG_TEXT_MAX_LINES; i++){
					_addConsoleTextLine("");
				}
				_addConsoleTextLine("console is cleared");
			break;
			#endregion // Clears the console
			#region "removeConsole"
			case "-removeConsole":
				c_debug.enabled = false;
				MG_Globals.I.DEBUG_DEV_MODE = false;
				Destroy (this.gameObject);
			break;
			#endregion // Removes the console
		}
	}
	#endregion
}
