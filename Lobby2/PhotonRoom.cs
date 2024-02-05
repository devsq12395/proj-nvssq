using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
	public static PhotonRoom room;
	private void Awake(){
		if (PhotonRoom.room == null) {
			PhotonRoom.room = this;
		} else {
			if (PhotonRoom.room != this) {
				Destroy (PhotonRoom.room.gameObject);
				PhotonRoom.room = this;
			}
		}

		DontDestroyOnLoad (this.gameObject);
	}

	public override void OnEnable(){
		base.OnEnable ();
		PhotonNetwork.AddCallbackTarget (this);
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	public override void OnDisable(){
		base.OnDisable ();
		PhotonNetwork.RemoveCallbackTarget (this);
		SceneManager.sceneLoaded += OnSceneFinishedLoading;
	}

	// Room Info
	public PhotonView PV;
	public bool isGameLoaded;
	public int currentScene;

	// Player Info
	Player[] photonPlayers;
	public int playersInRoom, myNumberInRoom;

	public int playerInGame;

	// Delayed Start
	private bool readyToCount, readyToStart;
	public float startingTime;
	private float lessThanMaxPlayers, atMaxPlayer, timeToStart;

	// Start is called before the first frame update
    void Start() {
		PV = GetComponent<PhotonView> ();
		readyToCount = false;
		readyToStart = false;
		lessThanMaxPlayers = startingTime;
		atMaxPlayer = 11;		// Start timer, also has on RestartTimer();
		timeToStart = startingTime;
    }

    // Update is called once per frame
    void Update() {
		if (MultiplayerSetting.multiplayerSetting.delayStart) {
			if (playersInRoom == 1) {
				RestartTimer ();
			}
			if (!isGameLoaded) {
				if (readyToStart) {
					atMaxPlayer -= Time.deltaTime;
					lessThanMaxPlayers = atMaxPlayer;
					timeToStart = atMaxPlayer;

					int countdown = (int)atMaxPlayer;
					string countdownStr = countdown.ToString();
					LobbyUI.I.changeStatusText ("Starting match in " + countdownStr + "...");
				}else if(readyToCount){
					lessThanMaxPlayers -= Time.deltaTime;
					timeToStart = lessThanMaxPlayers;

					int countdown = (int)atMaxPlayer;
					string countdownStr = countdown.ToString();
					LobbyUI.I.changeStatusText ("Starting match in " + countdownStr + "...");
				}

				if (timeToStart <= 0) {
					StartGame ();
				}
			}
		}
    }

	public override void OnJoinedRoom(){
		base.OnJoinedRoom ();
		Debug.Log ("Joined a room...");

		photonPlayers = PhotonNetwork.PlayerList;
		playersInRoom = photonPlayers.Length;
		myNumberInRoom = playersInRoom;
		PhotonNetwork.NickName = myNumberInRoom.ToString ();
		PlayerPrefs.SetInt ("playerNum", myNumberInRoom);
		if (MultiplayerSetting.multiplayerSetting.delayStart) {
			Debug.Log ("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSetting.multiplayerSetting.maxPlayers + ")");
			if (playersInRoom > 1) {
				readyToCount = true;

				_setPlayerData ();
			}
			if (playersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers) {
				readyToStart = true;
				if (!PhotonNetwork.IsMasterClient)
					return;
				PhotonNetwork.CurrentRoom.IsOpen = false;

				_setPlayerData ();
			}
		} 
		else {
			StartGame ();
		}
	}

	public override void OnPlayerEnteredRoom(Player newPlayer){
		base.OnPlayerEnteredRoom (newPlayer);
		Debug.Log ("A new player has joined");
		photonPlayers = PhotonNetwork.PlayerList;
		playersInRoom++;

		if (MultiplayerSetting.multiplayerSetting.delayStart) {
			Debug.Log ("Displayer players in room out of max players possible (" + playersInRoom + ":" + MultiplayerSetting.multiplayerSetting.maxPlayers + ")");
			if (playersInRoom > 1) {
				readyToCount = true;

				_setPlayerData ();
			}
			if (playersInRoom == MultiplayerSetting.multiplayerSetting.maxPlayers) {
				readyToStart = true;
				if (!PhotonNetwork.IsMasterClient)
					return;
				PhotonNetwork.CurrentRoom.IsOpen = false;

				_setPlayerData ();
			}
		}
	}

	void StartGame(){
		isGameLoaded = true;
		if (!PhotonNetwork.IsMasterClient)
			return;

		if (MultiplayerSetting.multiplayerSetting.delayStart) {
			PhotonNetwork.CurrentRoom.IsOpen = false;
		}
		PhotonNetwork.LoadLevel (MultiplayerSetting.multiplayerSetting.multiplayerScene);
	}

	void RestartTimer(){
		lessThanMaxPlayers = startingTime;
		timeToStart = startingTime;
		atMaxPlayer = 11;
		readyToCount = false;
		readyToStart = false;
	}

	void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode){
		currentScene = scene.buildIndex;
		if (currentScene == MultiplayerSetting.multiplayerSetting.multiplayerScene) {
			isGameLoaded = true;

			if (MultiplayerSetting.multiplayerSetting.delayStart) {
				PV.RPC ("RPC_LoadedGameScene", RpcTarget.MasterClient);
			} else {
				RPC_CreatePlayer ();
			}
		}
	}

	[PunRPC]
	private void RPC_LoadedGameScene(){
		playerInGame++;
		if (playerInGame == PhotonNetwork.PlayerList.Length) {
			PV.RPC ("RPC_CreatePlayer", RpcTarget.All);
		}
	}

	[PunRPC]
	private void RPC_CreatePlayer(){
		PhotonNetwork.Instantiate (Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
	}

	#region "Set Player Data"
	public void _setPlayerData(){
		string pName = PlayerPrefs.GetString ("playerName");
		int rankNum = PlayerPrefs.GetInt ("lvl");

		int enemyNum = 0;
		if (myNumberInRoom == 1) {
			LobbyUI.I.t_blueName.text = pName + "(You)";
			PlayerPrefs.SetString ("playerName_p1", pName);
			enemyNum = 1;
			LobbyUI.I.i_rankP1.sprite = LobbyUI.I.rankSprites [rankNum - 1];
		} else {
			LobbyUI.I.t_redName.text = pName + "(You)";
			PlayerPrefs.SetString ("playerName_p2", pName);
			enemyNum = 0;
			LobbyUI.I.i_rankP2.sprite = LobbyUI.I.rankSprites [rankNum - 1];
		}

		PV.RPC ("RPC_setPlayerData", photonPlayers[enemyNum], pName, rankNum);
		LobbyUI.I._gameFound ();
	}

	[PunRPC]
	private void RPC_setPlayerData(string pName, int rankNum){
		PlayerPrefs.SetString ("enemyName", pName);

		if (myNumberInRoom == 1) {
			LobbyUI.I.t_redName.text = pName;
			PlayerPrefs.SetString ("playerName_p2", pName);
			LobbyUI.I.i_rankP2.sprite = LobbyUI.I.rankSprites [rankNum - 1];
		} else {
			LobbyUI.I.t_blueName.text = pName;
			PlayerPrefs.SetString ("playerName_p1", pName);
			LobbyUI.I.i_rankP1.sprite = LobbyUI.I.rankSprites [rankNum - 1];
		}
	}
	#endregion
	#region "Get Enemy Photon player"
	public int enemyPlayerInt, localPlayerInt;
	public void setEnemyPhotonPlayer(){
		localPlayerInt = PlayerPrefs.GetInt ("playerNum") - 1;
		enemyPlayerInt = (localPlayerInt == 1) ? 0 : 1;
	}
	#endregion
	#region "Enemy Leaves Game"
	public override void OnPlayerLeftRoom(Player player) {  
		Debug.Log ("Enemy disconnected " + SceneManager.GetActiveScene ().name);

		switch (SceneManager.GetActiveScene ().name) {
			case "HeroSelect":
				HS_Popup.I._show ("Enemy disconnected.", false, true);
			break;
			case "Lobby2":
				PlayerPrefs.SetInt ("enemyDisconnect", 1);
			break;
			case "MainGame":
				if (!MG_Globals.I.pause_gameOver) {
					MG_UIControl_Popup.I.callVictory (MG_Globals.I.players [MG_Globals.I.curPlayerNum], 3);
				} else {
					MG_UIControl_Curtain.I._startGame ();
					MG_UIControl_Popup.I.callVictory (MG_Globals.I.players [MG_Globals.I.curPlayerNum], 3);
				}
			break;
		}
	}
	#endregion
	#region "Disconnect"
	public void disconnect(){
		StartCoroutine (attemptDisconnect ());
	}

	IEnumerator attemptDisconnect(){
		PhotonNetwork.LeaveRoom ();
		while (PhotonNetwork.InRoom)
			yield return null;
	}
	#endregion

	#region "RPC - Select Hero Screen"
	#region "Set Hero Data"
	public void photon_SetHeroData(string playerNum, string slotNum_str, string heroName){
		PV.RPC ("RPC_SetHeroData", RpcTarget.All, playerNum, slotNum_str, heroName);
	}

	[PunRPC]
	private void RPC_SetHeroData(string playerNum, string slotNum_str, string heroName){
		PlayerPrefs.SetString("heroName" + slotNum_str + "_p" + playerNum, heroName);
	}
	#endregion
	#region "Set Unit Data"
	public void photon_SetUnitData(string playerNum, string slotNum_str, string heroName){
		PV.RPC ("RPC_SetUnitData", RpcTarget.All, playerNum, slotNum_str, heroName);
	}

	[PunRPC]
	private void RPC_SetUnitData(string playerNum, string slotNum_str, string heroName){
		PlayerPrefs.SetString("unitName" + slotNum_str + "_p" + playerNum, heroName);
	}
	#endregion
	#region "Set Spell Data"
	public void photon_SetSpellData(string playerNum, string slotNum_str, string heroName){
		PV.RPC ("RPC_SetSpellData", RpcTarget.All, playerNum, slotNum_str, heroName);
	}

	[PunRPC]
	private void RPC_SetSpellData(string playerNum, string slotNum_str, string heroName){
		PlayerPrefs.SetString("spellName" + slotNum_str + "_p" + playerNum, heroName);
	}
	#endregion
	#region "Set Ready and Load Game Scene"
	public bool p1IsReady = false, p2IsReady = false;

	public void photon_setReady(int playerNum){
		PV.RPC ("RPC_SetReady", RpcTarget.All, playerNum);
	}

	[PunRPC]
	private void RPC_SetReady(int playerNum){
		if (playerNum == 1)
			p1IsReady = true;
		else
			p2IsReady = true;

		Debug.Log ("Ready!");
		Debug.Log (p1IsReady + ", " + p2IsReady);

		if (p1IsReady && p2IsReady) {
			/*if (PhotonNetwork.IsMasterClient) {
				RPC_LoadGameScene ();
			} else {
				PV.RPC ("RPC_LoadGameScene", RpcTarget.MasterClient);
			}*/
			HeroSelect.I.i_loading.enabled = true;
			HeroSelect.I.t_loading.enabled = true;
			HeroSelect.I.goTo_Game = true;
		}
	}

	[PunRPC]
	private void RPC_LoadGameScene(){
		PhotonNetwork.LoadLevel (2);
	}
	#endregion

	#endregion


	#region "Test function - Send RPC to the enemy only"
	public void testFcn(){
		if(localPlayerInt == 1)
			PV.RPC ("RPC_testFcn", photonPlayers[enemyPlayerInt]);
	}

	[PunRPC]
	private void RPC_testFcn(){
		Debug.Log ("sadasdsadsa");
	}
	#endregion


	#region "RPC - Game Screen"
	#region "START - Send loaded data"
	public void gameAction_sendLoadedData(){
		PV.RPC ("RPC_gameAction_sendLoadedData", photonPlayers[enemyPlayerInt]);
	}

	[PunRPC]
	private void RPC_gameAction_sendLoadedData(){
		if (MG_UIControl_Curtain.I.isGameLoaded) {
			gameAction_sendStartData ();
			MG_UIControl_Curtain.I._startGame ();
		}
	}
	#endregion

	#region "START - Send start data"
	public void gameAction_sendStartData(){
		PV.RPC ("RPC_gameAction_sendStartData", photonPlayers[enemyPlayerInt]);
	}

	[PunRPC]
	private void RPC_gameAction_sendStartData(){
		MG_UIControl_Curtain.I._startGame ();
	}
	#endregion

	#region "CHAT - Send chat"
	public void gameAction_chat(string chatStr){
		PV.RPC ("RPC_gameAction_chat", photonPlayers[enemyPlayerInt], chatStr);
	}

	[PunRPC]
	private void RPC_gameAction_chat(string chatStr){
		MG_UIControl_Chat.I._append(chatStr);

		if (!MG_UIControl_Chat.I.inChat) {
			MG_UIControl_Chat.I.i_chatNotif.SetActive (true);
			MG_ControlSounds.I._playSound(1, 0, 0, false);
		}
	}
	#endregion
	#region "CHAT - Mute"
	public void gameAction_chatMute(){
		PV.RPC ("RPC_gameAction_chatMute", photonPlayers[enemyPlayerInt]);
	}

	[PunRPC]
	private void RPC_gameAction_chatMute(){
		MG_UIControl_Chat.I._enemyMute ();
	}
	#endregion
	#region "CHAT - Unmute"
	public void gameAction_chatUnmute(){
		PV.RPC ("RPC_gameAction_chatUnmute", photonPlayers[enemyPlayerInt]);
	}

	[PunRPC]
	private void RPC_gameAction_chatUnmute(){
		MG_UIControl_Chat.I._enemyUnmute ();
	}
	#endregion

	#region "SKIN - Share skin"
	public void gameAction_shareSkin(int playerNum, string ownerName, int skinNum){
		PV.RPC ("RPC_gameAction_shareSkin", photonPlayers[enemyPlayerInt], playerNum, ownerName, skinNum);
	}

	[PunRPC]
	private void RPC_gameAction_shareSkin(int playerNum, string ownerName, int skinNum){
		MG_ControlPlayer.I.receiveSkin (playerNum, ownerName, skinNum);
	}
	#endregion
	#region "MAP - Share map"
	public void gameAction_shareMap(int mapIndex){
		PV.RPC ("RPC_gameAction_shareMap", photonPlayers[enemyPlayerInt], mapIndex);
	}

	[PunRPC]
	private void RPC_gameAction_shareMap(int mapIndex){
		if (HeroSelect.I == null) {
			return;
		}
		// HeroSelect.I.changeMap (mapIndex);
	}
	#endregion
	#region "GAME ID - Share game id"
	public void gameAction_shareGameId(string newId){
		PV.RPC ("RPC_gameAction_shareGameId", photonPlayers[enemyPlayerInt], newId);
	}

	[PunRPC]
	private void RPC_gameAction_shareGameId(string newId){
		if (HeroSelect.I == null) {
			return;
		}
		HeroSelect.I.receiveGameId (newId);
	}
	#endregion

	#region "Change Facing"
	public void gameAction_changeFacing(int unitID, string newFacing){
		PV.RPC ("RPC_gameAction_changeFacing", photonPlayers[enemyPlayerInt], unitID, newFacing);
	}

	[PunRPC]
	private void RPC_gameAction_changeFacing(int unitID, string newFacing){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		Debug.Log ("change facing for " + unitID);
		unit._changeFacing_actual (newFacing);
	}
	#endregion
	#region "Move order"
	public void gameAction_move(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_move", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_move(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		Debug.Log (unit.name + " moved at " + posX + ", " + posY);
		MG_ControlCommand.I.gameAction_move (unit, posX, posY);
	}
	#endregion
	#region "Summon order"
	public void gameAction_summon(int unitID, string unitName, int posX, int posY){
		PV.RPC ("RPC_gameAction_summon", photonPlayers[enemyPlayerInt], unitID, unitName, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_summon(int unitID, string unitName, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_summon (unit, unitName, posX, posY);
	}
	#endregion
	#region "Hire Unit"
	public void gameAction_hireUnit(int unitID, string unitName, int posX, int posY, int hireIndex){
		PV.RPC ("RPC_gameAction_hireUnit", photonPlayers[enemyPlayerInt], unitID, unitName, posX, posY, hireIndex);
	}

	[PunRPC]
	private void RPC_gameAction_hireUnit(int unitID, string unitName, int posX, int posY, int hireIndex){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_hireUnit (unit, unitName, posX, posY, hireIndex);
	}
	#endregion
	#region "Cast Spell"
	public void gameAction_spell(int unitID, string spellName, int spellIndex, int posX, int posY){
		PV.RPC ("RPC_gameAction_spell", photonPlayers[enemyPlayerInt], unitID, spellName, spellIndex, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_spell(int unitID, string spellName, int spellIndex, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_spell (unit, spellName, spellIndex, posX, posY);
	}
	#endregion
	#region "Attack order"
	public void gameAction_attack(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_attack", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_attack(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		Debug.Log (unit.name + " moved at " + posX + ", " + posY);
		MG_ControlCommand.I.gameAction_attack (unit, posX, posY);
	}
	#endregion
	#region "Change Item"
	public void gameAction_changeItem(string itemName, int heroSlot, int itemSlot){
		PV.RPC ("RPC_gameAction_changeItem", photonPlayers[enemyPlayerInt], itemName, heroSlot, itemSlot);
	}

	[PunRPC]
	private void RPC_gameAction_changeItem(string itemName, int heroSlot, int itemSlot){
		MG_Globals.I.players [enemyPlayerInt + 1]._heroChangeItem (itemName, heroSlot, itemSlot);
	}
	#endregion
	#region "End Turn"
	public void gameAction_endTurn(){
		PV.RPC ("RPC_gameAction_endTurn", photonPlayers[enemyPlayerInt]);
	}

	[PunRPC]
	private void RPC_gameAction_endTurn(){
		MG_ControlPlayer.I._endTurn();

		Debug.Log ("End Turn");
	}
	#endregion

	#region "ROBIN - Hookshot"
	public void gameAction_robin_hookshot(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_robin_hookshot", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_robin_hookshot(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		Debug.Log ("Robin Hookshot at " + posX + ", " + posY);
		MG_ControlCommand.I.gameAction_robin_hookshot (unit, posX, posY);
	}
	#endregion
	#region "ROBIN - Sword Dance"
	public void gameAction_robin_sworddance(int unitID){
		PV.RPC ("RPC_gameAction_robin_sworddance", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_robin_sworddance(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		Debug.Log ("Robin Sword Dance at " + unit.posX + ", " + unit.posY);
		MG_ControlCommand.I.gameAction_robin_sworddance (unit);
	}
	#endregion
	#region "ROBIN - Hook Dance"
	public void gameAction_robin_hookdance(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_robin_hookdance", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_robin_hookdance(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		Debug.Log ("Robin Hookdance at " + posX + ", " + posY);
		MG_ControlCommand.I.gameAction_robin_hookdance (unit, posX, posY);
	}
	#endregion

	#region "AJAX - Spear Toss"
	public void gameAction_ajax_spearToss(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_ajax_spearToss", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_ajax_spearToss(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		Debug.Log ("Ajax Spear Toss at " + posX + ", " + posY);
		MG_ControlCommand.I.gameAction_ajax_spearToss (unit, posX, posY);
	}
	#endregion
	#region "AJAX - Hold the Line"
	public void gameAction_ajax_holdTheLine(int unitID){
		PV.RPC ("RPC_gameAction_ajax_holdTheLine", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_ajax_holdTheLine(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_ajax_holdTheLine (unit);
	}
	#endregion
	#region "AJAX - All-out assault"
	public void gameAction_ajax_allOutAssault(int unitID){
		PV.RPC ("RPC_gameAction_ajax_allOutAssault", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_ajax_allOutAssault(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_ajax_allOutAssault (unit);
	}
	#endregion

	#region "COLT - Quick Draw"
	public void gameAction_colt_quickDraw(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_colt_quickDraw", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_colt_quickDraw(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_colt_quickDraw (unit, posX, posY);
	}
	#endregion
	#region "COLT - Dynamite"
	public void gameAction_colt_dynamite(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_colt_dynamite", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_colt_dynamite(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_colt_dynamite (unit, posX, posY);
	}
	#endregion
	#region "COLT - Disarming Shot"
	public void gameAction_colt_disarmingShot(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_colt_disarmingShot", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_colt_disarmingShot(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_colt_disarmingShot (unit, posX, posY);
	}
	#endregion

	#region "VICTORIA - Heal"
	public void gameAction_victoria_heal(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_victoria_heal", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_victoria_heal(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_victoria_heal (unit, posX, posY);
	}
	#endregion
	#region "VICTORIA - Shell"
	public void gameAction_victoria_shell(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_victoria_shell", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_victoria_shell(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_victoria_shell (unit, posX, posY);
	}
	#endregion
	#region "VICTORIA - Blessing"
	public void gameAction_victoria_blessing(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_victoria_blessing", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_victoria_blessing(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_victoria_blessing (unit, posX, posY);
	}
	#endregion

	#region "AMY - Scout"
	public void gameAction_amy_scout(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_amy_scout", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_amy_scout(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_amy_scout (unit, posX, posY);
	}
	#endregion
	#region "AMY - Multi Shot"
	public void gameAction_amy_multishot(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_amy_multishot", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_amy_multishot(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_amy_multishot (unit, posX, posY);
	}
	#endregion
	#region "AMY - Arc Shot"
	public void gameAction_amy_arcShot(int unitID){
		PV.RPC ("RPC_gameAction_amy_arcShot", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_amy_arcShot(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_amy_arcShot (unit);
	}
	#endregion

	#region "ALICIA - Recruit"
	public void gameAction_alicia_summonRecruit(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_alicia_summonRecruit", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_alicia_summonRecruit(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_alicia_summonRecruit (unit, posX, posY);
	}
	#endregion
	#region "ALICIA - Close Combat"
	public void gameAction_alicia_closeCombat(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_alicia_closeCombat", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_alicia_closeCombat(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_alicia_closeCombat (unit, posX, posY);
	}
	#endregion

	#region "ELDER TREANT - Entangle"
	public void gameAction_elderTreant_entangle(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_elderTreant_entangle", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_elderTreant_entangle(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_elderTreant_entangle (unit, posX, posY);
	}
	#endregion
	#region "ELDER TREANT - Root Armor"
	public void gameAction_elderTreant_rootArmor(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_elderTreant_rootArmor", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_elderTreant_rootArmor(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_elderTreant_rootArmor (unit, posX, posY);
	}
	#endregion
	#region "ELDER TREANT - Fairies"
	public void gameAction_elderTreant_fairies(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_elderTreant_fairies", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_elderTreant_fairies(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_elderTreant_fairies (unit, posX, posY);
	}
	#endregion

	#region "RAGNAROS - Fury"
	public void gameAction_ragnaros_fury(int unitID){
		PV.RPC ("RPC_gameAction_ragnaros_fury", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_ragnaros_fury(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_ragnaros_fury (unit);
	}
	#endregion

	#region "IFREET - Spiral"
	public void gameAction_ifreet_spiral(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_ifreet_spiral", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_ifreet_spiral(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_ifreet_spiral (unit, posX, posY);
	}
	#endregion
	#region "IFREET - Explosion"
	public void gameAction_ifreet_explosion(int unitID){
		PV.RPC ("RPC_gameAction_ifreet_explosion", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_ifreet_explosion(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_ifreet_explosion (unit);
	}
	#endregion
	#region "IFREET - Burning Grip"
	public void gameAction_ifreet_burningGrip(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_ifreet_burningGrip", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_ifreet_burningGrip(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_ifreet_burningGrip (unit, posX, posY);
	}
	#endregion

	#region "SPEC WITCH - Summon Spectre"
	public void gameAction_specWitch_summonSpectre(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_specWitch_summonSpectre", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_specWitch_summonSpectre(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_specWitch_summonSpectre (unit, posX, posY);
	}
	#endregion
	#region "SPEC WITCH - Blink"
	public void gameAction_specWitch_blink(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_specWitch_blink", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_specWitch_blink(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_specWitch_blink (unit, posX, posY);
	}
	#endregion
	#region "SPEC WITCH - Mirror"
	public void gameAction_specWitch_mirror(int unitID){
		PV.RPC ("RPC_gameAction_specWitch_mirror", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_specWitch_mirror(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_specWitch_mirror (unit);
	}
	#endregion

	#region "DRAKGUL - Eye of the Magi"
	public void gameAction_drakgul_eye(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_drakgul_eye", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_drakgul_eye(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_drakgul_eye (unit, posX, posY);
	}
	#endregion
	#region "DRAKGUL - Dispel"
	public void gameAction_drakgul_dispel(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_drakgul_dispel", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_drakgul_dispel(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_drakgul_dispel (unit, posX, posY);
	}
	#endregion
	#region "DRAKGUL - Concentration"
	public void gameAction_drakgul_concentration(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_drakgul_concentration", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_drakgul_concentration(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_drakgul_concentration (unit, posX, posY);
	}
	#endregion

	#region "MASAMUNE - Wind Slash"
	public void gameAction_masamune_windSlash(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_masamune_windSlash", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_masamune_windSlash(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_masamune_windSlash (unit, posX, posY);
	}
	#endregion
	#region "MASAMUNE - Azure Dragon"
	public void gameAction_masamune_azureDragon(int unitID){
		PV.RPC ("RPC_gameAction_masamune_azureDragon", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_masamune_azureDragon(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_masamune_azureDragon (unit);
	}
	#endregion

	#region "SIEGFRIED - Heavy Charge"
	public void gameAction_siegfried_heavyCharge(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_siegfried_heavyCharge", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_siegfried_heavyCharge(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_siegfried_heavyCharge (unit, posX, posY);
	}
	#endregion

	#region "ABEL - Smite"
	public void gameAction_abel_smite(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_abel_smite", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_abel_smite(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_abel_smite (unit, posX, posY);
	}
	#endregion
	#region "ABEL - Test of Faith"
	public void gameAction_abel_testOfFaith(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_abel_testOfFaith", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_abel_testOfFaith(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_abel_testOfFaith (unit, posX, posY);
	}
	#endregion

	#region "YUKINO - Frost Nova"
	public void gameAction_yukino_frostNova(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_yukino_frostNova", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_yukino_frostNova(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_yukino_frostNova (unit, posX, posY);
	}
	#endregion
	#region "YUKINO - Frost Wave"
	public void gameAction_yukino_frostWave(int unitID){
		PV.RPC ("RPC_gameAction_yukino_frostWave", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_yukino_frostWave(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_yukino_frostWave (unit);
	}
	#endregion
	#region "YUKINO - Silence"
	public void gameAction_yukino_silence(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_yukino_silence", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_yukino_silence(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_yukino_silence (unit, posX, posY);
	}
	#endregion

	#region "CODY - Blink Strike"
	public void gameAction_cody_blinkStrike(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_cody_blinkStrike", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_cody_blinkStrike(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_cody_blinkStrike (unit, posX, posY);
	}
	#endregion

	#region "HILDE - Arcane Bolt"
	public void gameAction_hilde_arcaneBolt(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_hilde_arcaneBolt", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_hilde_arcaneBolt(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_hilde_arcaneBolt (unit, posX, posY);
	}
	#endregion
	#region "HILDE - Photon Bomb"
	public void gameAction_hilde_photonBomb(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_hilde_photonBomb", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_hilde_photonBomb(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_hilde_photonBomb (unit, posX, posY);
	}
	#endregion

	#region "ELISE - Multi-Strike"
	public void gameAction_elise_multiStrike(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_elise_multiStrike", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_elise_multiStrike(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_elise_multiStrike (unit, posX, posY);
	}
	#endregion
	#region "ELISE - Phoenix Wings"
	public void gameAction_elise_phoenixWings(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_elise_phoenixWings", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_elise_phoenixWings(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_elise_phoenixWings (unit, posX, posY);
	}
	#endregion

	#region "MAY - Sacred Knights"
	public void gameAction_may_sacredKnights(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_may_sacredKnights", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_may_sacredKnights(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_may_sacredKnights (unit, posX, posY);
	}
	#endregion

	#region "WALTER - Burst Fire"
	public void gameAction_walter_burstFire(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_walter_burstFire", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_walter_burstFire(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_walter_burstFire (unit, posX, posY);
	}
	#endregion
	#region "WALTER - Demonic Slash"
	public void gameAction_walter_demonicSlash(int unitID){
		PV.RPC ("RPC_gameAction_walter_demonicSlash", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_walter_demonicSlash(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_walter_demonicSlash (unit);
	}
	#endregion

	#region "ALERIC - Axe Throw"
	public void gameAction_aleric_axeThrow(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_aleric_axeThrow", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_aleric_axeThrow(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_aleric_axeThrow (unit, posX, posY, "AxeThrow", 50);
	}
	#endregion
	#region "ALERIC - Mighty Slam"
	public void gameAction_aleric_mightySlam(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_aleric_mightySlam", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_aleric_mightySlam(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_aleric_mightySlam (unit, posX, posY, false, 50);
	}
	#endregion

	#region "EVE - Death Arrow"
	public void gameAction_eve_deathArrow(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_eve_deathArrow", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_eve_deathArrow(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_eve_deathArrow (unit, posX, posY);
	}
	#endregion
	#region "EVE - Drain Life"
	public void gameAction_eve_drainLife(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_eve_drainLife", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_eve_drainLife(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_eve_drainLife (unit, posX, posY);
	}
	#endregion
	
	#region "ELIZABETH - Amplify"
	public void gameAction_elizabeth_amplify(int unitID){
		PV.RPC ("RPC_gameAction_elizabeth_amplify", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_elizabeth_amplify(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_elizabeth_amplify (unit);
	}
	#endregion
	#region "ELIZABETH - Lightning Bolt"
	public void gameAction_elizabeth_lightningBolt(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_elizabeth_lightningBolt", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_elizabeth_lightningBolt(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_elizabeth_lightningBolt (unit, posX, posY);
	}
	#endregion

	#region "SHARED - Barrier"
	public void gameAction_barrier(int unitID){
		PV.RPC ("RPC_gameAction_barrier", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_barrier(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_barrier (unit);
	}
	#endregion
	#region "SHARED - Potion"
	public void gameAction_potion(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_potion", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_potion(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_potion (unit, posX, posY);
	}
	#endregion
	#region "SHARED - Heal Spray"
	public void gameAction_healSpray(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_healSpray", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_healSpray(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_healSpray (unit, posX, posY);
	}
	#endregion
	#region "SHARED - Mythril Revolver"
	public void gameAction_mythrilRevolver(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_mythrilRevolver", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_mythrilRevolver(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_mythrilRevolver (unit, posX, posY);
	}
	#endregion
	#region "SHARED - Grenade"
	public void gameAction_grenade(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_grenade", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_grenade(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_grenade (unit, posX, posY);
	}
	#endregion
	#region "SHARED - Upgrade"
	public void gameAction_upgrade(int unitID, int posX, int posY, string upgradeTo){
		PV.RPC ("RPC_gameAction_upgrade", photonPlayers[enemyPlayerInt], unitID, posX, posY, upgradeTo);
	}

	[PunRPC]
	private void RPC_gameAction_upgrade(int unitID, int posX, int posY, string upgradeTo){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_upgrade (unit, posX, posY, upgradeTo);
	}
	#endregion
	#region "SHARED - Upgrade2"
	public void gameAction_upgrade2(int unitID, string upgradeFrom, string upgradeTo, int upgradeCount){
		PV.RPC ("RPC_gameAction_upgrade2", photonPlayers[enemyPlayerInt], upgradeFrom, upgradeTo, upgradeCount);
	}

	[PunRPC]
	private void RPC_gameAction_upgrade2(int unitID, string upgradeFrom, string upgradeTo, int upgradeCount){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_upgrade2 (unit, upgradeFrom, upgradeTo, upgradeCount);
	}
	#endregion
	#region "SHARED - Artillery"
	public void gameAction_artillery(int unitID, int posX, int posY, int posX2, int posY2, int posX3, int posY3){
		PV.RPC ("RPC_gameAction_artillery", photonPlayers[enemyPlayerInt], unitID, posX, posY, posX2, posY2, posX3, posY3);
	}

	[PunRPC]
	private void RPC_gameAction_artillery(int unitID, int posX, int posY, int posX2, int posY2, int posX3, int posY3){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_artillery (unit, posX, posY, posX2, posY2, posX3, posY3);
	}
	#endregion
	#region "SHARED - Grapeshot"
	public void gameAction_grapeshot(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_grapeshot", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_grapeshot(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_grapeshot (unit, posX, posY);
	}
	#endregion
	#region "SHARED - Global Barrier"
	public void gameAction_globalBarrier(int unitID){
		PV.RPC ("RPC_gameAction_globalBarrier", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_globalBarrier(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_globalBarrier (unit);
	}
	#endregion

	#region "SHARED - Brotherhood"
	public void gameAction_brotherhood(int unitID){
		PV.RPC ("RPC_gameAction_brotherhood", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_brotherhood(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_UIControl_Cards.I.gameAction_brotherhood (unit);
	}
	#endregion
	#region "SHARED - Conscription"
	public void gameAction_conscription(int unitID){
		PV.RPC ("RPC_gameAction_conscription", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_conscription(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_UIControl_Cards.I.gameAction_conscription (unit);
	}
	#endregion
	#region "SHARED - Acid Breath"
	public void gameAction_acidBreath(int unitID, int posX, int posY){
		PV.RPC ("RPC_gameAction_acidBreath", photonPlayers[enemyPlayerInt], unitID, posX, posY);
	}

	[PunRPC]
	private void RPC_gameAction_acidBreath(int unitID, int posX, int posY){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_acidBreath (unit, posX, posY);
	}
	#endregion
	#region "SHARED - Holy Crusade"
	public void gameAction_holyCrusade(int unitID){
		PV.RPC ("RPC_gameAction_holyCrusade", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_holyCrusade(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_UIControl_Cards.I.gameAction_holyCrusade (unit);
	}
	#endregion
	#region "SHARED - War Stomp"
	public void gameAction_warStomp(int unitID){
		PV.RPC ("RPC_gameAction_warStomp", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_warStomp(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_warStomp (unit);
	}
	#endregion
	#region "SHARED - Unleash Hero"
	public void gameAction_unleashHero(int unitID){
		PV.RPC ("RPC_gameAction_unleashHero", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_unleashHero(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_unleashHero (unit);
	}
	#endregion
	#region "SHARED - Swap Hero"
	public void gameAction_swapHero(int unitID){
		PV.RPC ("RPC_gameAction_swapHero", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_swapHero(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_UIControl_Cards.I.gameAction_swapHero (unit);
	}
	#endregion
	#region "SHARED - Hero Gain XP"
	public void gameAction_heroGainXP(int unitID){
		PV.RPC ("RPC_gameAction_heroGainXP", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_heroGainXP(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_unleashHero (unit);
	}
	#endregion
	#region "SHARED - Double Card"
	public void gameAction_doubleCard(int unitID){
		PV.RPC ("RPC_gameAction_doubleCard", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_doubleCard(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_doubleCard (unit);
	}
	#endregion
	#region "SHARED - Change Weapon"
	public void gameAction_changeWeapon(int unitID, int newIndex){
		PV.RPC ("RPC_gameAction_changeWeapon", photonPlayers[enemyPlayerInt], unitID, newIndex);
	}

	[PunRPC]
	private void RPC_gameAction_changeWeapon(int unitID, int newIndex){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);
	}
	#endregion

	#region "EYE OF THE MAGI - Explode"
	public void gameAction_eye_explode(int unitID){
		PV.RPC ("RPC_gameAction_eye_explode", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_eye_explode(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_eye_explode (unit);
	}
	#endregion
	
	// Fix Bayonet
	public void gameAction_fixBayonet(int unitID){
		PV.RPC ("RPC_gameAction_fixBayonet", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_fixBayonet(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_fixBayonet (unit);
	}
	
	// Remove Bayonet
	public void gameAction_removeBayonet(int unitID){
		PV.RPC ("RPC_gameAction_removeBayonet", photonPlayers[enemyPlayerInt], unitID);
	}

	[PunRPC]
	private void RPC_gameAction_removeBayonet(int unitID){
		MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitID);

		MG_ControlCommand.I.gameAction_removeBayonet (unit);
	}
	
	#endregion
}
