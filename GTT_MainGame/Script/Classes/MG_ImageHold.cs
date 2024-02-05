using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MG_ImageHold : Image, IPointerEnterHandler, IPointerExitHandler  {

	public bool isPointerOn;

	public void OnPointerEnter(PointerEventData eventData){
		isPointerOn = true;
	}

	public void OnPointerExit(PointerEventData eventData){
		isPointerOn = false;
	}
}
