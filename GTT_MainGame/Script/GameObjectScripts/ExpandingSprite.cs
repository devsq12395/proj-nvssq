using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingSprite : MonoBehaviour
{
	public float origScale;
	public float scalePerFrame = 0.05f;

	void Start(){
		origScale = transform.localScale.x;
	}

	void Update() {
		origScale += scalePerFrame;
		transform.localScale = new Vector3(origScale, origScale, 1);
	}
}
