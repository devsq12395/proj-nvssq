using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Lobby_Player : NetworkLobbyPlayer {
	public GameObject parentPref;
	public Button btn_join, btn_test;
	public Text txt_name, txt_btn, txt_btnTest, txt_ready;

	[SyncVar(hook = "localPlayerNumber_change")]
	public int localPlayerNumber = 0;
	[SyncVar(hook = "playerConnectType_change")]
	public string playerConnectType;
	[SyncVar(hook = "gameObjectName_change")]
	public string gameObjectName;
	[SyncVar]
	public int testNum = 0;

	#region "Setup"
	#region "Setup starting data for local objects"
	// First to be called
	public override void OnClientEnterLobby(){
		base.OnClientEnterLobby ();
		parentPref = GameObject.FindGameObjectWithTag ("parentPref");
		gameObject.transform.SetParent (parentPref.transform);
		txt_ready.text = "";

		// Setup the data of other player's prefab
		playerConnectType_change (playerConnectType);
		gameObjectName_change (gameObjectName);
		localPlayerNumber_change (localPlayerNumber);
	}

	// Next to be called
	public override void OnStartLocalPlayer(){
		string newplayerConnectType, newgameObjectName;
		int newLocalPlayerNumber;

		// If local player, get the data to be setup
		if (isLocalPlayer) {
			if (isServer) {
				newplayerConnectType = "Host";
				newgameObjectName = "LobbyObject_Host";
				newLocalPlayerNumber = 1;
				btn_join.enabled = true;
			} else {
				newplayerConnectType = "Client";
				newgameObjectName = "LobbyObject_Client";
				newLocalPlayerNumber = 2;
				btn_join.enabled = false;
			}

			// Send these data to other players
			Cmd_Change_playerConnectType (newplayerConnectType);
			Cmd_Change_gameObjectName (newgameObjectName);
			Cmd_Change_localPlayerNumber (newLocalPlayerNumber);
		}
	}
	#endregion

	#region "UI SyncVar functions - Sync starting data to other players"
	public void playerConnectType_change(string newValue){
		playerConnectType = newValue;
		txt_name.text = newValue;
	}

	public void gameObjectName_change(string newValue){
		gameObjectName = newValue;
		gameObject.name = newValue;
	}

	public void localPlayerNumber_change(int newValue){
		localPlayerNumber = newValue;
	}

	[Command]
	public void Cmd_Change_playerConnectType(string newValue){
		playerConnectType = newValue;
	}

	[Command]
	public void Cmd_Change_gameObjectName(string newValue){
		gameObjectName = newValue;
	}

	[Command]
	public void Cmd_Change_localPlayerNumber(int newValue){
		localPlayerNumber = newValue;
	}
	#endregion
	#endregion

	#region "BTN - Join"
	public void _btn_Join(){
		SendReadyToBeginMessage ();

		btn_join.enabled = false;
		txt_ready.text = "Ready!";
	}
	#endregion
	#region "BTN - Text"
	public void _btn_Text(){
		if (!isLocalPlayer)
			return;

		testNum++;
		_btn_Text_Change (localPlayerNumber, testNum);
		if (isServer) {
			Rpc_BtnText (localPlayerNumber, testNum);
		} else {
			Cmd_BtnText (localPlayerNumber, testNum);
		}
	}

	public void _btn_Text_Change(int targetPlayerNumber, int newValue){
		var localPlayers = FindObjectsOfType<Lobby_Player> ();

		foreach (Lobby_Player loopPlayer in localPlayers) {
			if (loopPlayer.localPlayerNumber == targetPlayerNumber) {
				loopPlayer.txt_btnTest.text = newValue.ToString ();
			}
		}
	}

	/*
    	ClientRpc - Host to all clients, including host so always include if(!isHost)
    		- Host can also call these functions
    	Command - Client to host command, host will run the function using the client sender's player object, in this case, the client's Lobby_Player
    */
	[ClientRpc]
	public void Rpc_BtnText(int targetPlayerNumber, int newValue){
		if (isServer)  return;
		
		_btn_Text_Change (targetPlayerNumber, newValue);
	}
	[Command]
	public void Cmd_BtnText(int targetPlayerNumber, int newValue){
		_btn_Text_Change (targetPlayerNumber, newValue);
	}
	#endregion
}
