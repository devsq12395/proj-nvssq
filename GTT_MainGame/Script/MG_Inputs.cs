using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Inputs : MonoBehaviour {
	public static MG_Inputs I;
	public void Awake(){ I = this; }

	public bool LEFT_CLICK_DRAG_ON;

	public void _start(){
		/////////////// SETTINGS ///////////////
		LEFT_CLICK_DRAG_ON = false;
		/////////////// SETTINGS ///////////////

		distX = 0; distY = 0;
		testerX = " "; testerY = " ";
		#if UNITY_ANDROID && !UNITY_EDITOR
			slctMaxFrames = 3;
			camSpeed = 0.4f;
		#else
			slctMaxFrames = 2;
			camSpeed = 0.25f;
		#endif
	}

	public float zoomVal = 0;
	public void _update(){
		_update_InGame ();
	}

	#region "In Game"
	private void _update_InGame(){
		#if UNITY_ANDROID && !UNITY_EDITOR
		// Handle native touch events
		if (Input.touchCount == 1) {
		//Navigation
		foreach (Touch touch in Input.touches) {
		HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
		}
		}else if (Input.touchCount == 2){
		//Zooming, positive should be zoom out
		Touch touchZero = Input.GetTouch(0);
		Touch touchOne = Input.GetTouch(1);

		Vector2 touchZeroDelta = touchZero.position - touchZero.deltaPosition;
		Vector2 touchOneDelta = touchOne.position - touchOne.deltaPosition;

		float prevTouchDeltaMag = (touchZeroDelta - touchOneDelta).magnitude;
		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

		float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

		// Disabled android zooming for now
		//zoomVal = deltaMagDiff;
		}
		#else
		// Keyboard
		if(!MG_Globals.I.editorMode){
			// IN DIALOG
			if(MG_UIControl_Dialog.I.isOnDialog){
				// OK Key, progresses dialog
				if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("OK Key") || Input.GetButtonDown("Button1")){ 
					MG_UIControl_Dialog.I.isPressConfirm = true;
				}

				// Hotkey for dialog options
				// Does not feel like a good idea so disabled for now
				if(MG_UIControl_Dialog.I.isOnOptions){
//					if(Input.GetButtonDown("Button1")){ MG_UIControl_Dialog.I._pressDialog_1(); }
//					if(Input.GetButtonDown("Button2")){ MG_UIControl_Dialog.I._pressDialog_2(); }
//					if(Input.GetButtonDown("Button3")){ MG_UIControl_Dialog.I._pressDialog_3(); }
//					if(Input.GetButtonDown("Button4")){ MG_UIControl_Dialog.I._pressDialog_4(); }
				}

				return;
			}
			// IN GAME
			else{
				if(Input.GetButtonDown("Button1")){ 
					if(MG_UIControl_Cards.I.isShow) MG_UIControl_Cards.I.selectCard (0);
					else MG_ControlCommand.I._issueCommand(1); 
				}
				if(Input.GetButtonDown("Button2")){ 
					if(MG_UIControl_Cards.I.isShow) MG_UIControl_Cards.I.selectCard (1);
					else MG_ControlCommand.I._issueCommand(2); 
				}
				if(Input.GetButtonDown("Button3")){ 
					if(MG_UIControl_Cards.I.isShow) MG_UIControl_Cards.I.selectCard (2);
					else MG_ControlCommand.I._issueCommand(3); 
				}
				if(Input.GetButtonDown("Button4")){ 
					if(MG_UIControl_Cards.I.isShow) {
						MG_UIControl_Cards.I.selectCard (3);
					} else{
						MG_ControlCommand.I._issueCommand(4); 
						MG_UIControl_Barracks.I.hide();
						MG_UIControl_SpellTower.I.hide();
						MG_UIControl_Summon.I._hide();
						MG_UIControl_Skill.I._hide();
						
						MG_UIControl_Action.I.hide ();
					}
				}
				if(Input.GetButtonDown("Button6")){ 
					if(MG_UIControl_Cards.I.isShow) MG_UIControl_Cards.I.selectCard (5);
				}

				if(Input.GetButtonDown("Enter")){ 
					if(MG_UIControl_Cards.I.isShow) {
						MG_UIControl_Cards.I.useCard();
					}
				}
				if(Input.GetButtonDown("Escape")){ 
					MG_UIControl_Barracks.I.hide();
					MG_UIControl_SpellTower.I.hide();
					MG_UIControl_Summon.I._hide();
					MG_UIControl_Skill.I._hide();
					MG_UIControl_Cards.I.hide(true);

					MG_ControlTargeters.I._clearTargeters();
					MG_ControlCursor.I._interact();
					MG_Globals.I.mode = "in map";
					MG_Globals.I.curCommand = "in map";
					MG_ControlCursor.I._changeCursorSprite("Normal");
					MG_ControlCommand.I._changeCommandsAndUI();
					MG_UIControl_Announcer.I._clearAnnounce();
				}
			}

			if(Input.GetButtonDown("Objectives")){ MG_UIControl_TopBar.I._show_objectives(); }
			if(Input.GetButtonDown("Items")){ MG_UIControl_Chat.I._show(); }
			if(Input.GetButtonDown("Menu")){ MG_UIControl_TopBar.I._show_menu(); }
		}else{
			// MAP EDITOR
			for(int i = 1; i <= 4; i++)
				MG_ControlEditor.I.markIsPressed("Button" + i.ToString(), Input.GetButtonDown("Button" + i.ToString()));
		}
		if(!MG_Globals.I.pause_windowOpen){
			if(Input.GetButton("CameraUp")){ 		MG_ControlCamera.I._moveCamera(0, 0.16f); }
			if(Input.GetButton("CameraDown")){ 		MG_ControlCamera.I._moveCamera(0, -0.16f); }
			if(Input.GetButton("CameraLeft")){ 		MG_ControlCamera.I._moveCamera(-0.16f, 0); }
			if(Input.GetButton("CameraRight")){ 	MG_ControlCamera.I._moveCamera(0.16f, 0); }
		}

		// Simulate touch events from mouse events
		//Left Click
		if (Input.GetMouseButtonDown(0) ) {
			HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
		}
		if (Input.GetMouseButton(0) ) {
			HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
		}
		if (Input.GetMouseButtonUp(0) ) {
			HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
		}
		//Right Click
		if (Input.GetMouseButtonDown(1) ) {
			RightClick(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
		}
		if (Input.GetMouseButton(1) ) {
			RightClick(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
		}
		if (Input.GetMouseButtonUp(1) ) {
			RightClick(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
		}
		//Mouse wheel zoom
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
			zoomVal = 1;
		else if (Input.GetAxis("Mouse ScrollWheel") > 0)
			zoomVal = -1;
		else
			zoomVal = 0;
		#endif

		// Moves the stick of the camera
		if(moveX != 0)	MG_ControlCamera.I._moveCamera(moveX, 0);
		if(moveY != 0)	MG_ControlCamera.I._moveCamera(0, moveY);
		MG_ControlCamera.I._moveCamera(moveX, moveY);
		MG_ControlCamera.I._zoom();
	}

	//Handle Touch:
	private Vector3 startPos;
	public string testerX, testerY;
	public float distX, distY;
	private int slctFrameCount, slctMaxFrames;//Change this at _start()
	//Stick Movement:
	private float moveX, moveY;
	public float camSpeed;//Change this at _setup()
	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
		switch (touchPhase) {
			case TouchPhase.Began:
				startPos = touchPosition;
				slctFrameCount = 0;

				if (!LEFT_CLICK_DRAG_ON) {
					_select (touchFingerId, touchPosition, touchPhase);
				}
			break;
			case TouchPhase.Stationary:
				if(slctFrameCount < slctMaxFrames){
					if (LEFT_CLICK_DRAG_ON) {
						slctFrameCount++;
						if (slctFrameCount >= slctMaxFrames) 	_select (touchFingerId, touchPosition, touchPhase);
					}
				}
				//For android, prevents camera for moving too much when swiping is stationary
				moveY = 0;
				moveX = 0;
			break;
			case TouchPhase.Moved:
				if(slctFrameCount < slctMaxFrames){
					slctFrameCount++;
					#if !UNITY_ANDROID || UNITY_EDITOR
					if(slctFrameCount >= slctMaxFrames && startPos == touchPosition)	_select(touchFingerId, touchPosition, touchPhase);
					#endif
				}else{
					#if UNITY_ANDROID && !UNITY_EDITOR
						_swipe(touchFingerId, touchPosition, touchPhase);
					#else
						if(LEFT_CLICK_DRAG_ON){
							_swipe(touchFingerId, touchPosition, touchPhase);
						}
					#endif
				}
			break;
			case TouchPhase.Ended:
				testerX = "ended"; moveX = 0;
				testerY = "ended"; moveY = 0;
			break;
		}
	}

	private void RightClick(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
		switch (touchPhase) {
			case TouchPhase.Began:
				startPos = touchPosition;
			break;
			case TouchPhase.Moved:
				_swipe(touchFingerId, touchPosition, touchPhase);
			break;
			case TouchPhase.Ended:
				testerX = "ended"; moveX = 0;
				testerY = "ended"; moveY = 0;
			break;
		}
	}

	//This function only focuses on moving the cursor and updating the selected unit UI
	private void _select(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase){
		//If a buttons is being pressed, no unit will be selected and the cursor should not move
		//Pressable buttons in game goes here
		#region "Pressed Button Check"
		bool pressed = false;
		// Entire command window
		if(/*Input.mousePosition.x >= Screen.width * 0.25 && Input.mousePosition.x <= Screen.width * 0.75 &&*/ Input.mousePosition.y <= Screen.height * 0.16){
			pressed = true;
		}
		// Top bar
		if(Input.mousePosition.y >= Screen.height * 0.96){
			pressed = true;
		}
		// Unit Stats window
		if(MG_UIControl_UnitStats.I.isShown){
			if(Input.mousePosition.x >= Screen.width * 0.24 && Input.mousePosition.x <= Screen.width * 0.27 && 
				Input.mousePosition.y >= Screen.height * 0.13 && Input.mousePosition.y >= Screen.height * 0.17){
				pressed = true;
			}
		}
		/*// Command background images
		if(MG_UIControl_Command.I.img_cmdBtnBG1.isPointerOn || MG_UIControl_Command.I.img_cmdBtnBG2.isPointerOn ||
			MG_UIControl_Command.I.img_cmdBtnBG3.isPointerOn){
			pressed = true;
		}
		// Command buttons
		if(!pressed){
			for (int i = 0; i < MG_UIControl_Command.I.cmdBtn.Count; i++) {
				//if (MG_UIControl_Command.I.cmdBtn [i].isPressed) {
				if(MG_UIControl_Command.I.cmdBtn [i].GetComponent<RectTransform>().rect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y), true) ||
					MG_UIControl_Command.I.cmdBtn [i].isPointerOn) {
					pressed = true;
					break;
				}
			}
		}
		if(!pressed){
			for (int i = 0; i < MG_UIControl_Command.I.skillBtn.Count; i++) {
				if (MG_UIControl_Command.I.skillBtn [i].GetComponent<RectTransform>().rect.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y), true) ||
					MG_UIControl_Command.I.skillBtn [i].isPointerOn) {
					pressed = true;
					break;
				}
			}
		}*/

		if(pressed) 		return;
		#endregion
		
		//Select
		float selPosX, selPosY;
		selPosX = touchPosition.x;
		selPosY = touchPosition.y;
		MG_ControlCursor.I._moveCursor(selPosX, selPosY);
		//PlayerModes.I._interact();//_cursorMove_Multiplaye also has this function.
	}

	#region "_swipe function - OLD, BLOCKY CONTROLS"
//	private void _swipe(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase){
//		//Move
//		//FOR Y
//		distY = touchPosition.y - startPos.y;
//		if(distY > camSpeed || distY < -camSpeed){
//			float swipeValue = Mathf.Sign(touchPosition.y - startPos.y);
//			if (swipeValue > 0){//up swipe
//				testerY = "up"; 
//				moveY = -camSpeed;
//			}
//			else if (swipeValue < 0){//down swipe
//				testerY = "down"; 
//				moveY = camSpeed;
//			}
//		}else{
//			testerY = "ended"; moveY = 0;
//		}
//		//FOR X
//		distX = touchPosition.x - startPos.x;
//		if(distX > camSpeed || distX < -camSpeed){
//			float swipeValue = Mathf.Sign(touchPosition.x - startPos.x);
//			if (swipeValue > 0){//right swipe
//				testerX = "right"; 
//				moveX = -camSpeed;
//			}
//			else if (swipeValue < 0){//left swipe
//				testerX = "left"; 
//				moveX = camSpeed;
//			}
//		}else{
//			testerX = "ended"; moveX = 0;
//		}
//		#if UNITY_ANDROID && !UNITY_EDITOR
//		//startPos = touchPosition;
//		#endif
//	}
	#endregion
	#region "_swipe function - NEW, SMOOTHER CONTROLS"
	private void _swipe(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase){
		//Move
		//FOR Y
		distY = touchPosition.y - startPos.y;
		if(distY > camSpeed || distY < -camSpeed){
			float swipeValue = Mathf.Sign(touchPosition.y - startPos.y);
			distY /= 20;
			distY = Mathf.Abs (distY);
			if (swipeValue > 0){//up swipe
				testerY = "up"; 
				moveY = -distY;
			}
			else if (swipeValue < 0){//down swipe
				testerY = "down"; 
				moveY = distY;
			}
		}else{
			testerY = "ended"; moveY = 0;
		}
		//FOR X
		distX = touchPosition.x - startPos.x;
		if(distX > camSpeed || distX < -camSpeed){
			float swipeValue = Mathf.Sign(touchPosition.x - startPos.x);
			distX /= 20;
			distX = Mathf.Abs (distX);
			if (swipeValue > 0){//right swipe
				testerX = "right"; 
				moveX = -distX;
			}
			else if (swipeValue < 0){//left swipe
				testerX = "left"; 
				moveX = distX;
			}
		}else{
			testerX = "ended"; moveX = 0;
		}
		#if UNITY_ANDROID && !UNITY_EDITOR
		//startPos = touchPosition;
		#endif
	}
	#endregion
	#endregion
}
