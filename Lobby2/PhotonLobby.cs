using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Analytics;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
	public static PhotonLobby lobby;
	private void Awake() {lobby = this;}

	public GameObject battleButton, cancelButton, backButton;
	public int roomNumber;
	RoomInfo[] rooms;

	public bool isLooking = false;
	public float timeWait;

	void Start() {
		// BETA VERSION MULTIPLAYER SAMPLE GAME SETTINGS:
		PlayerPrefs.SetInt ("isMultiplayer", 1);
		PlayerPrefs.SetString("game_mapName", "Encounter");

		if (PlayerPrefs.GetInt ("lobby_backButton") == 1) {
			PlayerPrefs.SetInt ("lobby_backButton", 0);
			battleButton.SetActive (true);
			backButton.SetActive (true);
			LobbyUI.I.changeStatusText ("Connected to server");
		} else {
			PhotonNetwork.ConnectUsingSettings ();
		}

		LobbyUI.I._start ();
		LobbyUI.I.changeStatusText ("Connecting to server...");
    }

	public override void OnConnectedToMaster(){
		Debug.Log ("Connected to Photon Master Server");
		LobbyUI.I.changeStatusText ("Connected to server");

		PhotonNetwork.AutomaticallySyncScene = true;
		battleButton.SetActive (true);
		backButton.SetActive (true);
	}

	public void OnBattleButtonClicked(){
		L_Sounds.I._playSound(2, 0, 0, false);
		Debug.Log ("Battle Button clicked");
		LobbyUI.I.lookingForMatch = true;

		battleButton.SetActive (false);
		cancelButton.SetActive (true);
		backButton.SetActive (false);
		PhotonNetwork.JoinRandomRoom ();
	}

	public void battleClicked(){
		if (!isLooking) {
			isLooking = true;
			Analytics.CustomEvent("LobbyClicked", new Dictionary<string, object>
			{
				{ "name", PlayerPrefs.GetString ("playerName")}
			});
		}
	}

	public override void OnJoinRandomFailed(short returnCode, string message){
		Debug.Log ("Failed to join random room...");
		Debug.Log (message);
		CreateRoom ();
	}

	void CreateRoom(){
		Debug.Log ("Creating a new room...");

		int randomRoomName = Random.Range (0, 10000);
		RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSetting.multiplayerSetting.maxPlayers };

		PhotonNetwork.CreateRoom ("Room" + randomRoomName, roomOps);
	}

	public override void OnJoinedRoom(){
		Debug.Log ("Joined a room...");
	}

	public override void OnCreateRoomFailed(short returnCode, string message){
		Debug.Log ("Failed to create new room");
		CreateRoom ();
	}

	#region "Disconnection callbacks"
	public override void OnDisconnected (DisconnectCause cause){
		if (cause == DisconnectCause.MaxCcuReached) {
			Analytics.CustomEvent("MaxCCUReached", new Dictionary<string, object>
			{
				{ PlayerPrefs.GetString ("playerName"), PlayerPrefs.GetString ("playerName")}
			});

			Debug.Log ("Disconnected from Photon");
			LobbyPopup.I.show_im ("Max players reached!", 
				"Server is currently full. Try coming back later.\n\nIf demand is high and i can afford it, i'll upgrade the server. For now the game is using the free server version. Please support the game so i can afford a server upgrade. Thank you for understanding.", 
				1);
		}
	}
	#endregion

	public void OnCancelButtonClicked(){
		Debug.Log ("Cancel Button clicked");

		cancelButton.SetActive (false);
		battleButton.SetActive (true);
		backButton.SetActive (true);

		PhotonNetwork.LeaveRoom ();
	}

	public void cancelClicked(){
		if (isLooking) {
			Analytics.CustomEvent("LobbyCancelled", new Dictionary<string, object>
				{
					{ PlayerPrefs.GetString ("playerName"), timeWait}
				});

			timeWait = 0;
			isLooking = false;
		}
	}

    void Update() {
		
    }
}
