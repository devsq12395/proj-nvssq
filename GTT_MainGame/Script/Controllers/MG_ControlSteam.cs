using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
//using Steamworks;

public class MG_ControlSteam : MonoBehaviour {
	public static MG_ControlSteam I;
	public void Awake(){ I = this; }

	public bool steamEnabled = true;

	public void unlockAchievement(string achieveName){
		if (!steamEnabled)
			return;

//		Debug.Log ("testing achievement = " + achieveName + ", " + SteamManager.Initialized.ToString());
//		if (SteamManager.Initialized) {
//			SteamUserStats.SetAchievement (achieveName);
//			SteamUserStats.StoreStats();
//			bool check;
//			Debug.Log ("Achievement: " + SteamUserStats.GetAchievement(achieveName, out check));
//		}
	}
}