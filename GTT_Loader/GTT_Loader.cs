using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GTT_Loader : MonoBehaviour{
	public static GTT_Loader I;
	private void Awake() {I = this;}

	public float loadTime = 2;

    void Start() {
		PlayerPrefs.SetInt ("lobby_backButton", 0);
    }

    void Update() {
		loadTime -= Time.deltaTime;
		if (loadTime <= 0) {
			SceneManager.LoadScene ("MainMenu");
		}
    }
}
