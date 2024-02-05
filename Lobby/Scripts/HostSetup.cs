using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class HostSetup : MonoBehaviour {
	MatchInfoSnapshot match;
	public Text hostName;
	Lobby lobby;
	public GameObject lobbyParent;

	// Use this for initialization
	void Start () {
		lobby = GameObject.FindGameObjectWithTag("LMANAGER").GetComponent<Lobby>();
		lobbyParent = GameObject.FindGameObjectWithTag("LPARENT");
	}

	public void _setup(MatchInfoSnapshot _match){
		match = _match;
		hostName.text = match.name;
	}

	public void _joinButton(){
		if (lobby == null) {
			lobby = GameObject.FindGameObjectWithTag("LMANAGER").GetComponent<Lobby>();
		}

		var go = lobbyParent.GetComponentsInChildren<Transform> (true);
		foreach (var item in go) {
			item.gameObject.SetActive (true);
		}
		lobby.matchMaker.JoinMatch (match.networkId, "", "", "", 0, 0, lobby.OnMatchJoined);
	}
}
