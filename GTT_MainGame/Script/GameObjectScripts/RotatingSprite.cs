using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSprite : MonoBehaviour
{
	public float speed = 100;

	public void Update() {
		transform.Rotate (0,0,speed*Time.deltaTime);
	}
}
