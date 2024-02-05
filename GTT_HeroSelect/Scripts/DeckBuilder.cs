using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class DeckBuilder : MonoBehaviour {
	public static DeckBuilder I;
	public void Awake(){ I = this; }
	
	public bool DEBUG_MODE;

	public Image i_port, i_cursor, i_loading;
	public Text t_name, t_name2, t_type, t_type2, t_gold, t_gold2, t_desc, t_loading, t_multStatus, t_page;

	public List<Image> btn_cards;
	public List<int> deck, deckShown;
	public List<string> lockedCards;

	public int selIndex, page, pageMax, cardsPerPage, cardsPerDeck;

	public float pickTimer = 180;

	void Start(){
		ZPlayerPrefs.Initialize("sqPrefEncrypt29845", "09164667352sss");
		
		DEBUG_MODE = false;

		///////////////////////////////////////////////////////////////
		#region "SET LOCKED CARDS HERE"
		// Set locked cards
		lockedCards = new List<string>();
		setLockedCard ("Spy");
		setLockedCard ("Viking");
		setLockedCard ("VikingRaider");
		setLockedCard ("Spectre");
		setLockedCard ("FireSpawn");
		setLockedCard ("FireElemental");
		#endregion
		///////////////////////////////////////////////////////////////

		DB_ChangeCard.I.start ();
		HS_Popup.I._start_load ();

		page = 0;
		pageMax = 1;
		cardsPerPage = 12;
		cardsPerDeck = 24;

		for (int i = 1; i <= cardsPerPage; i++) {
			string targ = "BTN_Unit" + ((i >= 10) ? "" : "0") + i.ToString ();
			btn_cards.Add (GameObject.Find (targ).GetComponent<Button> ().image);
		}

		updateDeck (true);

		i_loading.enabled = false;
		t_loading.enabled = false;

		if (PlayerPrefs.GetInt ("isMultiplayer") != 1) {
			t_multStatus.text = "";
		}
	}

	void Update(){
		
	}

	public void BTNBack() {
		
	}

	#region "Update Deck"
	public void updateDeck(bool firstLoad = false) {
		if(DEBUG_MODE && firstLoad) Debug.Log("Loading card data...");
		
		for (int i = 1; i <= cardsPerDeck; i++) {
			deck.Add (DB_Database.I.getIndex(ZPlayerPrefs.GetString("cardName_" + (i).ToString())));
			changeCard (i - 1, (i - 1) % 12, deck [i - 1], false);
			
			if(DEBUG_MODE && firstLoad) Debug.Log("Loaded card, index = " + i.ToString() + ", Name = " + DB_Database.I.getInCodeName (deck[i-1]));
		}
		
		if(DEBUG_MODE && firstLoad) Debug.Log("Loaded card data!");
		updateMainCard ();
	}
	#endregion
	#region "Save Card Data"
	public void saveCardData() {
		if(DEBUG_MODE) Debug.Log("Saving card data...");
		
		for (int i = 1; i <= cardsPerDeck; i++) {
			ZPlayerPrefs.SetString ("cardName_" + i.ToString (), DB_Database.I.getInCodeName (deck[i-1]));
			
			if(DEBUG_MODE) Debug.Log("Saved card, index = " + i.ToString() + ", Name = " + DB_Database.I.getInCodeName (deck[i-1]));
		}
		
		if(DEBUG_MODE) Debug.Log("Saved card data!");
	}
	#endregion

	#region "Change Selected Card"
	public void changeSel(int newSel){
		HH_Sounds.I._playSound(3, 0, 0, false);

		if (selIndex == newSel) {
			startChangeCard ();
		} else {
			selIndex = newSel;
			
			setCurPosition (newSel + 1);
		}

		updateMainCard ();
	}
	#endregion
	#region "Set Cursor Position"
	public void setCurPosition(int newIndex){
		int selectPos = newIndex;
		float posX = ((selectPos - 1) % 4) + 1;
		float posY = Mathf.Floor ((selectPos - 1) / 4);
		float actPosX = -479.04f + 80.5f * posX;
		float actPosY = 125.52f - 84.82f * posY;
		i_cursor.transform.localPosition = new Vector2 (actPosX, actPosY);
	}
	#endregion
	#region "Set Locked Card"
	public void setLockedCard(string cardName){
		if (ZPlayerPrefs.GetInt ("cardUnlocked_" + cardName) == 0) {
			lockedCards.Add (cardName);
		}
	}
	#endregion

	#region "Update Main Card"
	public void updateMainCard(int newCardIndex = -1){
		if(newCardIndex == -1) newCardIndex = selIndex + (cardsPerPage * page);
		
		i_port.sprite = DB_Database.I._getPortrait (deck [newCardIndex]);
		t_name.text = t_name2.text = DB_Database.I.getName (deck [newCardIndex]);
		t_type.text = t_type2.text = DB_Database.I.getType (deck [newCardIndex]);
		t_gold.text = t_gold2.text = DB_Database.I.getCost (deck [newCardIndex]).ToString ();
		t_desc.text = DB_Database.I.getDesc (deck [newCardIndex]);
	}
	#endregion
	#region "Start Change Card"
	public void startChangeCard() {
		DB_ChangeCard.I.show (selIndex);
	}
	#endregion
	#region "Change Card"
	public void changeCard(int cardIndex, int indexOnPage, int newCard, bool changeSelec = true) {
		if(indexOnPage + (cardsPerPage * page) == cardIndex){
			btn_cards [indexOnPage].sprite = DB_Database.I._getPortrait (newCard);
		}
		
		deck [cardIndex] = newCard;
		if(changeSelec) changeSel (cardIndex);
		
		updateMainCard (cardIndex);
	}
	#endregion

	#region "DECK - Switch page"
	public void deck_nextPage() { deck_changePage (1); }
	public void deck_prevPage() { deck_changePage (-1); }

	public void deck_changePage(int inc) {
		page += inc;
		if (page < 0)  					page = pageMax;
		if (page > pageMax) 			page = 0;

		t_page.text = (page + 1).ToString () + "/" + (pageMax + 1).ToString ();

		updateDeck ();
		HH_Sounds.I._playSound(3, 0, 0, false);
	}
	#endregion

	// CONTAINS
	// getNumberOfCardsInDeck
	// loadGameScene
	#region "Misc Functions"
	public int getNumberOfCardsInDeck (string cardName){
		int ret = 0;
		for (int i = 0; i < cardsPerDeck; i++) {
			if (DB_Database.I.getInCodeName (deck [i]) == cardName) {
				ret++;
			}
		}

		return ret;
	}
	#endregion
	#region "Load Game Scene"
	public void loadGameScene(){
		i_loading.enabled = true;
		t_loading.enabled = true;

		SceneManager.LoadScene ("MainGame");
	}
	#endregion
}
