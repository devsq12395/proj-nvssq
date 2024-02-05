using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyControl : MonoBehaviour {

	public InputField matchName;
	public Lobby lobbyManager;
	public GameObject joinRoomObj;
	JoinRoom joinRoom;

	#region "Buttons"
	public void _hostButton(){
		//lobbyManager.StopMatchMaker ();
		lobbyManager.StartMatchMaker ();
		lobbyManager.matchMaker.CreateMatch (
			matchName.text, 
			(uint)lobbyManager.maxPlayers,
			true,
			"","","",0,0,
			lobbyManager.OnMatchCreate
		);
	}

	public void _joinAGameButton(){
		lobbyManager.StartMatchMaker ();
		joinRoomObj.SetActive (true);
		joinRoom = joinRoomObj.GetComponent<JoinRoom> ();
		joinRoom.RefreshList ();
	}
	#endregion
}
