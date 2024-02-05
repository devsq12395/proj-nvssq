using UnityEngine;
using System.Collections;
using DigitalRuby.Tween;

/*
	Methods inside:
	_moveCamera() 		= Moves the camera
	_changeTarget()		= Change the camera stick
*/
public class MG_ControlCamera : MonoBehaviour {
	public float dampTimeDEF;
	private float dampTime;
	private Vector3 velocity = Vector3.zero;
	public GameObject target, sky;
	public static MG_ControlCamera I;
	private float maxCameraDist = 10.57f;
	private float minCameraDist = 1.5f;
	private float cameraSizeP1 = 2.5f;
	private float cameraSizeP2 = 2.5f;
	private float cameraSize = 3.3f;
	private float zoomSpeed;

	public void Awake(){
		I = this;
		dampTimeDEF = 0;//POSSIBLE VALUES: 0.05f, 0.15f
		#if !UNITY_ANDROID || UNITY_EDITOR
		zoomSpeed = 0.4f;
		#else
		zoomSpeed = 0.1f;
		#endif
	}

	public void _start(){
		//tableP1 = tableP2 = sky.transform.localScale;
		#if !UNITY_ANROID
		float cameraSizeP1 = 2.5f;
		float cameraSizeP2 = 2.5f;
		float cameraSize = 2.5f;
		#else
		float cameraSizeP1 = 2.5f;
		float cameraSizeP2 = 2.5f;
		float cameraSize = 2.5f;
		#endif
		GetComponent<Camera>().orthographicSize = cameraSize;
	}

	public void _moveCamera(float newPosX, float newPosY, bool forceMove = false){
		//Camera bounds limit
		if((target.transform.position.x + newPosX >= MG_Globals.I.mapLimitX - 11 && newPosX > 0) || 
			(target.transform.position.x - newPosX <= (MG_Globals.I.mapLimitX * -1) + 11 && newPosX < 0))
			newPosX = 0;
		if((target.transform.position.y + newPosY >= MG_Globals.I.mapLimitY - 11 && newPosY > 0) || 
			(target.transform.position.y - newPosY <= (MG_Globals.I.mapLimitY * -1) + 11 && newPosY < 0))
			newPosY = 0;

		target.transform.position = (Vector2)target.transform.position + new Vector2(newPosX, newPosY);
		//sky.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, 200);
		if (target)
		{
			if (forceMove) {
				transform.position = new Vector3(newPosX, newPosY, transform.position.z);
			} else {
				Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.transform.position);
				Vector3 delta = target.transform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
				Vector3 destination = transform.position + delta;
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
				transform.position = destination;
			}
		}
	}

	public void _zoom(){
		if(MG_Inputs.I.zoomVal > 0 && cameraSize < maxCameraDist){
			cameraSize += zoomSpeed;
			if(cameraSize > maxCameraDist)	cameraSize = maxCameraDist;
			//else sky.transform.localScale = new Vector3(sky.transform.localScale.x + zoomSpeed * 3.7f, 
				//sky.transform.localScale.y + zoomSpeed * 3.7f, 0.0001f);

		}else if(MG_Inputs.I.zoomVal < 0  && cameraSize > minCameraDist){
			cameraSize -= zoomSpeed;
			if(cameraSize < minCameraDist)	cameraSize = minCameraDist;
			//else sky.transform.localScale = new Vector3(sky.transform.localScale.x - zoomSpeed * 3.7f, 
			//	sky.transform.localScale.y - zoomSpeed * 3.7f, 0.0001f);
		}
		GetComponent<Camera>().orthographicSize = cameraSize;
	}

	public void _adjustCam(bool isZoomIn){
		if(isZoomIn){
			cameraSizeP1 -= 1f;
			if(cameraSizeP1 < minCameraDist)	cameraSizeP1 = minCameraDist;
		}else{
			cameraSizeP1 += 1f;
			if(cameraSizeP1 > maxCameraDist)	cameraSizeP1 = maxCameraDist;
		}
	}

	public void _adjustOnEndTurn(){
		GetComponent<Camera>().orthographicSize = cameraSizeP1;
	}

	public Vector3 _getGamePoint(Vector3 screenPoint){
		Camera camera = GetComponent<Camera>();
		screenPoint.z = camera.nearClipPlane;
		Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return p;
	}
}