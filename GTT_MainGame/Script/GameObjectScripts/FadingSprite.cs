using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingSprite : MonoBehaviour
{
	public float newAlpha = 1f;
	public float alphaPerFrame = 0.05f;
	public SpriteRenderer renderer;

	void Start(){
		renderer = GetComponent <SpriteRenderer> ();
	}

	void Update() {
		newAlpha -= alphaPerFrame;
		renderer.color = new Color (1f, 1f, 1f, newAlpha);
	}
}
