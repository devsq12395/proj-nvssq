using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class MG_ControlAds : MonoBehaviour {
	public static MG_ControlAds I;
	public void Awake(){ I = this; }

	public InterstitialAd interstitial;

	public void _start() {
		string appId = "ca-app-pub-8572288216412906~1859175371";
		// Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);
		this.RequestAd();
	}

	private void RequestAd() {
		string adUnitId = "ca-app-pub-8572288216412906/1037048290";

		this.interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);
	}

	public void showAd(){
		if (this.interstitial.IsLoaded()) {
			this.interstitial.Show();
		}
	}

	float sec = 0;
	// Update is called once per frame
	void Update()
	{
//		sec += Time.deltaTime;Debug.Log (sec);
//		if (sec >= 0.5f) {
//			SceneManager.LoadScene ("MainMenu");
//		}
	}
}
