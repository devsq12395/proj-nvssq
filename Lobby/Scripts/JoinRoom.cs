using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinRoom : MonoBehaviour {

	Lobby lobbyManager;
	public GameObject prefabForHost, parentForHost;

	// Use this for initialization
	void Start () {
		lobbyManager = GameObject.FindGameObjectWithTag ("LMANAGER").GetComponent<Lobby>();
	}

	public void RefreshList(){
		if (lobbyManager == null) {
			lobbyManager = GameObject.FindGameObjectWithTag ("LMANAGER").GetComponent<Lobby>();
		}
		if (lobbyManager.matchMaker == null) {
			lobbyManager.StartMatchMaker ();
		}
		lobbyManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, onMatchList);
	}

	private void onMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
		//throw new NotImplementedException();
		if(!success){
			print ("Please refresh");
		}

		foreach (MatchInfoSnapshot match in matchList) {
			GameObject ListGo = Instantiate (prefabForHost);
			ListGo.transform.SetParent (parentForHost.transform);
			HostSetup hostSetup = ListGo.GetComponent<HostSetup> ();
			hostSetup._setup (match);
		}
	}
}
