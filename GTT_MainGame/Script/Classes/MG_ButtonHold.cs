using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MG_ButtonHold : Button  {

	public bool isPressed, isPointerOn;

	public bool hasTooltip = false;
	public int tooltipTigger = 0;

	public void Update(){
		isPressed = IsPressed();
	}
}
