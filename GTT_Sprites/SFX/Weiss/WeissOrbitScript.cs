using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeissOrbitScript : MonoBehaviour
{
	private float RotateSpeed = 0.75f;
	private float Radius = 0.45f;

	private Vector2 _centre;
	private float _angle;

	private void Start()
	{
		_centre = transform.parent.position;
	}

	public void Update() {
		_angle += RotateSpeed * Time.deltaTime;

		var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
		transform.position = _centre + offset;
		transform.position = new Vector3 (transform.position.x, transform.position.y, -200);

		Vector3 relativePos = transform.parent.position - transform.position;

		// the second argument, upwards, defaults to Vector3.up
		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);
		transform.rotation = rotation;
	}
}
