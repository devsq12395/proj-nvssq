using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MG_ControlNetwork : MonoBehaviour {
	public static MG_ControlNetwork I;
	public void Awake(){ I = this; }

	public int playerId;

	NetworkClient myClient;

	public void _start(){
		playerId = PlayerPrefs.GetInt("multiplayer_playerId");

		if (playerId == 1) {
			// Server set-up
			NetworkServer.Listen (4444);
		} else {
			// Client set-up
			myClient = new NetworkClient();
			myClient.RegisterHandler (MsgType.Connect, OnConnected);
			myClient.Connect ("127.0.0.1", 4444);
		}
	}

	public void OnConnected(NetworkMessage netMsg){
		Debug.Log ("Connected to server");
	}

}
