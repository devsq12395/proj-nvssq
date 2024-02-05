using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnAnimEnd : MonoBehaviour
{
	public Animator animation;

	void Start(){
		if(GetComponent<Animator> () != null)
			animation = GetComponent<Animator> ();
    }

	void Update(){
		if (this.animation.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !this.animation.IsInTransition(0)) {
			MG_ControlUnits.I._destroyUIObject(transform.gameObject);
		}
    }
}
