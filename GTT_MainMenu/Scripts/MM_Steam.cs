using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
//using Steamworks;

public class MM_Steam : MonoBehaviour {
	public static MM_Steam I;
	public void Awake(){ I = this; }

	//protected Callback<UserAchievementStored_t> m_UserAchievementStored;
	public bool steamEnabled;

	public void unlockAchievement(string achieveName){
		if (!steamEnabled) 	return;

//		Debug.Log ("testing achievement = " + achieveName + ", " + SteamManager.Initialized.ToString());
//		if (SteamManager.Initialized) {
//			bool check;
//			Debug.Log ("Achievement: " + SteamUserStats.GetAchievement(achieveName, out check));
//			if (!check) {
//				Debug.Log ("unlocking achievement");
//				m_UserAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);
//				SteamUserStats.RequestCurrentStats ();
//				SteamUserStats.SetAchievement (achieveName);
//				SteamUserStats.StoreStats();
//			}
//		}
	}

	//void OnAchievementStored (UserAchievementStored_t pCallback) {

	//}
}
