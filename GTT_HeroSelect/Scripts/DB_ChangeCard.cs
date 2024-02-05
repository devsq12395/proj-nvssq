using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB_ChangeCard : MonoBehaviour {
	public static DB_ChangeCard I;
	public void Awake(){ I = this; }

	public GameObject c;
	public Image i_port, i_cursor;
	public Text t_name, t_desc, t_page;

	public List<Image> c_image;
	public List<string> cardList;

	public int selIndex, cardIndex, page;

	public int CARDS_PER_PAGE, PAGE_MAX;

	public void start(){
		c.SetActive (true);
		CARDS_PER_PAGE = 40; // Count starts at 0
		PAGE_MAX = 1; // Count starts at 0

		// Get Cards GameObject and store the images in the list
		c_image = new List<Image> ();
		GameObject c_parent = GameObject.Find("I_BG_UnitPool_1");
		Transform c_child;
		int c_childCount = c_parent.transform.childCount - 1;
		for (int lp = 0; lp <= c_childCount; lp++) {
			c_child = c_parent.transform.GetChild(lp);
			c_image.Add (c_child.gameObject.GetComponent<Image> ());
		}

		cardList = new List<string>();
		setPage (0);

		c.SetActive (false);
	}
	
	public void setPage_dec(){setPage(-1);}
	public void setPage_inc(){setPage(1);}
	
	public void setPage(int inc) {
		page += inc;
		if(page > PAGE_MAX) page = 0;
		if(page < 0) page = PAGE_MAX;
		
		cardList.Clear();
		
		// Check if cards are unlocked
		GameObject c_parent = GameObject.Find("I_BG_UnitPool_1");
		int c_childCount = c_parent.transform.childCount - 1;
		string cardName = "";
		for (int lp = 0; lp <= c_childCount; lp++) {
			if(lp <= DB_Database.I.CARD_MAX){
				cardName = DB_Database.I.getInCodeName (lp + CARDS_PER_PAGE * page);
			}else{
				break;
			}
			
			if(cardName != "" && cardName != "NONE"){
				c_image[lp].enabled = true;
				c_image[lp].sprite = DB_Database.I._getPortrait (lp + CARDS_PER_PAGE * page);
				cardList.Add (cardName);
			}else{
				c_image[lp].enabled = false;
			}

			if (DeckBuilder.I.lockedCards.Contains (cardName)) {
				c_image[lp].color = new Color32 (100, 100, 100, 225);
			} else {
				c_image[lp].color = new Color32 (255, 255, 225, 225);
			}
		}
		
		t_page.text = (page+1).ToString() + "/" + (PAGE_MAX+1).ToString();
		
		HH_Sounds.I._playSound(3, 0, 0, false);
		selIndex = -1;
		changeSel (0);
	}

	public void show(int newCard) {
		c.SetActive (true);
		cardIndex = newCard;
	}

	public void hide(){
		c.SetActive (false);
	}

	public void changeSel(int newSel){
		HH_Sounds.I._playSound(3, 0, 0, false);

		if (selIndex == newSel) {
			choose ();
		} else {
			selIndex = newSel;

			int selectPos = newSel + 1;
			float posX = ((selectPos - 1) % 8) + 1;
			float posY = Mathf.Floor ((selectPos - 1) / 8);
			float actPosX = -480.7f + 74.6f * posX;
			float actPosY = 210.3f - 80 * posY;
			i_cursor.transform.localPosition = new Vector2 (actPosX, actPosY);

			i_port.sprite = DB_Database.I._getPortrait (selIndex + CARDS_PER_PAGE * page);
			t_name.text = DB_Database.I.getName (selIndex + CARDS_PER_PAGE * page);
			t_desc.text = "Type: " + DB_Database.I.getType (selIndex + CARDS_PER_PAGE * page) +
				"\nCost: " + DB_Database.I.getCost (selIndex + CARDS_PER_PAGE * page).ToString () +
				"\n\n" + DB_Database.I.getDesc (selIndex + CARDS_PER_PAGE * page);
		}
	}

	public void choose() {
		if (DeckBuilder.I.getNumberOfCardsInDeck (DB_Database.I.getInCodeName (selIndex + CARDS_PER_PAGE * page)) >= 3) {
			HS_Popup.I._show ("Limit for this card is reached. Card limit is 3 cards on a deck.");
		} else if (DeckBuilder.I.lockedCards.Contains (DB_Database.I.getInCodeName (selIndex + CARDS_PER_PAGE * page))){
			HS_Popup.I._show ("Card is locked.");
		}else {
			DeckBuilder.I.changeCard (cardIndex + DeckBuilder.I.cardsPerPage * DeckBuilder.I.page, cardIndex, selIndex + CARDS_PER_PAGE * page, true);
			
			Debug.Log(cardIndex);
			DeckBuilder.I.setCurPosition ((cardIndex + 1) % (DeckBuilder.I.cardsPerPage + 1));
			
			c.SetActive (false);
		}
	}
}
