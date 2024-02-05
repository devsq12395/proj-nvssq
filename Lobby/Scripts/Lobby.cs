using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class Lobby : NetworkLobbyManager {

	public GameObject playersLobby;

	private void Start(){
		playersLobby.SetActive (false);
	}

	public override void OnStartHost(){
		base.OnStartHost ();
		print ("Game created...");
		playersLobby.SetActive (true);
	}
}
