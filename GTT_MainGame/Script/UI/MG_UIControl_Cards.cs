using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Cards : MonoBehaviour {
	public static MG_UIControl_Cards I;
	public void Awake(){ I = this; }

	public GameObject c;
	public Image i_cursor, i_port;
	public Text t_name, t_name2, t_type, t_type2, t_gold, t_gold2, t_desc, t_announce, t_gold3;

	public List<Button> btn_cards;
	public List<string> card_deck; // First 4 entries of this list will be the cards in hand

	public List<Vector2> curPos;

	public bool isShow;
	public int selCard;

	public float announceTime;

	public int CARD_PER_HAND, CARD_ON_DECK;

	public void start() {
		ZPlayerPrefs.Initialize("sqPrefEncrypt29845", "09164667352sss");
		c.SetActive (true);

		CARD_PER_HAND = 6;
		CARD_ON_DECK = 24;

		for (int i = 1; i <= CARD_PER_HAND; i++) {
			btn_cards.Add (GameObject.Find("BTN_Card_0" + i.ToString()).GetComponent<Button>());
		}
		curPos.Add (new Vector2(-260.1f, -217.10001f));
		curPos.Add (new Vector2(-156.1f, -217.10001f));
		curPos.Add (new Vector2(-52.6f, -217.10001f));
		curPos.Add (new Vector2(51.1f, -217.10001f));
		curPos.Add (new Vector2(150f, -217.10001f));
		curPos.Add (new Vector2(246.7f, -217.10001f));

		// Gather deck info
		for (int i = 1; i <= CARD_ON_DECK; i++) {
			card_deck.Add (ZPlayerPrefs.GetString ("cardName_" + i.ToString()));
			Debug.Log (card_deck [i-1]);
		}

		shuffleDeck ();
		c.SetActive (false);
	}

	public void update(float deltaTime){
		if (announceTime > 0) {
			announceTime -= deltaTime;
			if (announceTime <= 0) {
				announceTime = 0;
				t_announce.text = "";
			}
		}
	}

	#region "Card Effects"
	public void useCard() {
		name = card_deck [selCard];
		string type = MG_DB_Cards.I.getType (name);
		
		#region "Change Hero"
		if(type == "Hero"){
			if(MG_DB_Cards.I.getCost (name) <= MG_Globals.I.players[MG_Globals.I.curPlayerNum].gold){
				foreach(MG_ClassUnit u in MG_Globals.I.units) {
					if(u.isAHero && u.playerOwner == MG_Globals.I.curPlayerNum){
						/////// In-Game Multiplayer RPC ///////
						if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
							PhotonRoom.room.gameAction_swapHero(MG_Globals.I.selectedUnit.unitID);
						}

						MG_ControlSFX.I._createSFX("summonFx", u.posX, u.posY);
						MG_ControlSounds.I._playSound(27, u.posX, u.posY, true);

						////////////////////////// SWAP UNIT START //////////////////////////
						int newPosX = u.posX, newPosY = u.posY,
						newLvl = u.hero_lvl, newXp = u.hero_xp, newXpReq = u.hero_xpReq, playNum = u.playerOwner;
						double hpPerc = u.HP / u.HPMax;
						double mpPerc = u.MP / u.MPMax;
						int backupHP = u.HP, backupMP = u.MP;
						bool act_move = u.action_move, act_atk = u.action_atk, act_skill = u.action_skill;

						MG_ControlUnits.I._addToDestroyList (u);
						MG_ControlUnits.I._createUnit (name, newPosX, newPosY, playNum);

						MG_ClassUnit newHero = MG_Globals.I.unitsTemp [MG_Globals.I.unitsTemp.Count-1];
						newHero.hero_lvl = newLvl;
						newHero.hero_xp = newXp;
						newHero.hero_xpReq = newXpReq;
						newHero.setHeroStats ();
						MG_ControlBuffs.I._addBuff (newHero, "HeroDur");
						MG_Globals.I.selectedUnit = newHero;
						MG_Globals.I.curCommand = "in map";
						MG_ControlCommand.I._changeCommandsAndUI();
						MG_ControlCursor.I._interact ();

						newHero.HP = (int)((double)newHero.HPMax*hpPerc);
						if(newHero.HP <= 1) newHero.HP = backupHP;
						newHero.MP = (int)((double)newHero.MPMax*mpPerc);
						if(newHero.MP <= 1) newHero.MP = backupMP;

						MG_UIControl_TopBar.I._goldGain (-6);
						MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold -= 6;
						MG_UIControl_TopBar.I._goldUI_update ();

						newHero.action_card = false;
						newHero.action_move = act_move;
						newHero.action_atk = act_atk;
						newHero.action_skill = act_skill;
						////////////////////////// SWAP UNIT END //////////////////////////

						MG_ControlCommand.I.resetCommands();
						hide (false);

						return;
					}
				}
			}else{
				cardAnnounce ("Not enough gold.");
				MG_ControlSounds.I._playSound(49, 0, 0, false);
				return;
			}
		}
		#endregion
		#region "Effect"
		else if(type == "Effect"){
			if(MG_DB_Cards.I.getCost (name) <= MG_Globals.I.players[MG_Globals.I.curPlayerNum].gold){
				switch(name){
					#region "Plantation"
					case "Plantation":
						MG_ControlCommand.I.speStr_1 = "Farm";
						MG_ControlCommand.I.speStr_2 = "Plantation";
						MG_ControlCommand.I.speStr_3 = MG_DB_Cards.I.getCost (name).ToString();
						MG_ControlCommand.I._issueCommand (-1, "Upgrade");
						hide (false);
					break;
					#endregion
					#region "Cannon Tower"
					case "CannonTower":
						MG_ControlCommand.I.speStr_1 = "Tower";
						MG_ControlCommand.I.speStr_2 = "CannonTower";
						MG_ControlCommand.I.speStr_3 = MG_DB_Cards.I.getCost (name).ToString();
						MG_ControlCommand.I._issueCommand (-1, "Upgrade");
						hide (false);
					break;
					#endregion
					#region "War Bonds"
					case "WarBonds":
						MG_UIControl_TopBar.I._goldGain (7);
						MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold += 7;
						MG_UIControl_TopBar.I._goldUI_update ();
						MG_ControlSounds.I._playSound(7, 0, 0, false);

						throwSelCard_toBottomDeck();
						MG_Globals.I.selectedUnit.action_card = false;
						MG_ControlCommand.I.resetCommands();
						hide (false);
					break;
					#endregion
					#region "Brotherhood"
					case "Brotherhood":
						gameAction_brotherhood(MG_Globals.I.selectedUnit);

						throwSelCard_toBottomDeck();
						MG_Globals.I.selectedUnit.action_card = false;
						MG_ControlCommand.I.resetCommands();
						hide (false);

						/////// In-Game Multiplayer RPC ///////
						if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
							PhotonRoom.room.gameAction_brotherhood(MG_Globals.I.selectedUnit.unitID);
						}
					break;
					#endregion
					#region "Conscription"
					case "Conscription":
						gameAction_conscription(MG_Globals.I.selectedUnit);

						throwSelCard_toBottomDeck();
						MG_Globals.I.selectedUnit.action_card = false;
						MG_ControlCommand.I.resetCommands();
						hide (false);

						/////// In-Game Multiplayer RPC ///////
						if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
							PhotonRoom.room.gameAction_conscription(MG_Globals.I.selectedUnit.unitID);
						}
					break;
					#endregion
					#region "Crusader"
					case "Crusader":
						MG_ControlCommand.I.speStr_1 = "SacredKnight";
						MG_ControlCommand.I.speStr_2 = "Crusader";
						MG_ControlCommand.I.speStr_3 = "3";
						MG_ControlCommand.I.speStr_4 = MG_DB_Cards.I.getCost (name).ToString();
						MG_ControlCommand.I._issueCommand (-1, "Upgrade2");
						hide (false);
					break;
					#endregion
					#region "BarracksLvl2"
					case "BarracksLvl2":
						MG_ControlCommand.I.speStr_1 = "Barracks";
						MG_ControlCommand.I.speStr_2 = "BarracksLvl2";
						MG_ControlCommand.I.speStr_3 = MG_DB_Cards.I.getCost (name).ToString();
						MG_ControlCommand.I._issueCommand (-1, "Upgrade");
						hide (false);
					break;
					#endregion
					#region "Holy Crusade"
					case "HolyCrusade":
						gameAction_holyCrusade(MG_Globals.I.selectedUnit);

						throwSelCard_toBottomDeck();
						MG_Globals.I.selectedUnit.action_card = false;
						MG_ControlCommand.I.resetCommands();
						hide (false);

						/////// In-Game Multiplayer RPC ///////
						if (PlayerPrefs.GetInt ("isMultiplayer") == 1) {
							PhotonRoom.room.gameAction_holyCrusade(MG_Globals.I.selectedUnit.unitID);
						}
					break;
					#endregion
					#region "Fire Elemental"
					case "FireElemental":
						MG_ControlCommand.I.speStr_1 = "FireSpawn";
						MG_ControlCommand.I.speStr_2 = "FireElemental";
						MG_ControlCommand.I.speStr_3 = MG_DB_Cards.I.getCost (name).ToString();
						MG_ControlCommand.I._issueCommand (-1, "Upgrade");
						hide (false);
					break;
					#endregion

					#region "Global Spell-type Effects"
					case "Veteran":case "Fireball":
						MG_ControlCommand.I.globalCast = true;
						MG_ControlCommand.I.speStr_1 = name;
						MG_ControlCommand.I._issueCommand (-1, "cast spell");
						hide (false);
					break;
					#endregion
					#region "Non-Global Spell-type Effects"
					case "Reinforcements":case "HolyOrder":case "CavalryCharge":case "ImperialMarch":
						MG_ControlTargeters.I._createTargField(false, MG_Globals.I.selectedUnit.posX, MG_Globals.I.selectedUnit.posY, 4);
						MG_ControlCommand.I.speStr_1 = name;
						MG_ControlCommand.I._issueCommand (-1, "cast spell");
						hide (false);
					break;
					#endregion
				}
			}else{
				cardAnnounce ("Not enough gold.");
				MG_ControlSounds.I._playSound(49, 0, 0, false);
				return;
			}
		}
		#endregion
	}
	#endregion

	#region "CARDS - Multiplayer RPC"
	#region "Brotherhood"
	public void gameAction_brotherhood(MG_ClassUnit unit){
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(u.playerOwner == unit.playerOwner && (u.name == "Rifleman" || u.name == "Swordsman")){
				MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX, u.posY);
				MG_ControlBuffs.I._addBuff (u, "Brotherhood");
			}
		}
	}
	#endregion
	#region "Conscription"
	public void gameAction_conscription(MG_ClassUnit unit){
		int offset = (unit.playerOwner == 1) ? 1 : -1;
		MG_ControlSFX.I._createSFX("cartoonHoly01", unit.posX, unit.posY);

		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(u.playerOwner == unit.playerOwner && u.name == "Officer"){
				MG_ControlUnits.I._createUnit("Rifleman", u.posX + offset, u.posY + 1, unit.playerOwner);
				MG_ControlUnits.I._createUnit("Rifleman", u.posX + offset, u.posY - 1, unit.playerOwner);

				MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX + offset, u.posY + 1);
				MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX + offset, u.posY - 1);
			}
		}
	}
	#endregion
	#region "Holy Crusade"
	public void gameAction_holyCrusade(MG_ClassUnit unit){
		foreach(MG_ClassUnit u in MG_Globals.I.units){
			if(u.playerOwner == unit.playerOwner && (u.name == "Crusader")){
				MG_ControlSFX.I._createSFX("cartoonHoly01", u.posX, u.posY);
				MG_ControlBuffs.I._addBuff (u, "HolyCrusade");
			}
		}
	}
	#endregion
	#region "Hero Gain XP"
	public void gameAction_heroGainXP(MG_ClassUnit u){
		u.gainLevel(25);
		MG_ControlSFX.I._createSFX("summonFx", u.posX, u.posY);
		MG_ControlSounds.I._playSound(27, u.posX, u.posY, true);
	}
	#endregion
	#region "Swap Hero"
	public void gameAction_swapHero(MG_ClassUnit unit){
		MG_ControlSFX.I._createSFX("summonFx", unit.posX, unit.posY);
		MG_ControlSounds.I._playSound(27, unit.posX, unit.posY, true);

		////////////////////////// SWAP UNIT START //////////////////////////
		int newPosX = unit.posX, newPosY = unit.posY,
		newLvl = unit.hero_lvl, newXp = unit.hero_xp, newXpReq = unit.hero_xpReq, playNum = unit.playerOwner;
		double hpPerc = unit.HP / unit.HPMax;
		double mpPerc = unit.MP / unit.MPMax;
		int backupHP = unit.HP, backupMP = unit.MP;
		bool act_move = unit.action_move, act_atk = unit.action_atk, act_skill = unit.action_skill;

		MG_ControlUnits.I._addToDestroyList (unit);
		MG_ControlUnits.I._createUnit (name, newPosX, newPosY, playNum);

		MG_ClassUnit newHero = MG_Globals.I.unitsTemp [MG_Globals.I.unitsTemp.Count-1];
		newHero.hero_lvl = newLvl;
		newHero.hero_xp = newXp;
		newHero.hero_xpReq = newXpReq;
		newHero.setHeroStats ();
		MG_ControlBuffs.I._addBuff (newHero, "HeroDur");

		newHero.HP = (int)((double)newHero.HPMax*hpPerc);
		if(newHero.HP <= 1) newHero.HP = backupHP;
		newHero.MP = (int)((double)newHero.MPMax*mpPerc);
		if(newHero.MP <= 1) newHero.MP = backupMP;

		newHero.action_card = false;
		newHero.action_move = act_move;
		newHero.action_atk = act_atk;
		newHero.action_skill = act_skill;
		////////////////////////// SWAP UNIT END //////////////////////////
	}
	#endregion
	#endregion

	public void cardAnnounce(string newText){
		announceTime = 2;
		t_announce.text = newText;
	}

	public void show(){
		c.SetActive (true);
		selectCard (selCard);
		isShow = true;

		t_gold3.text = MG_Globals.I.players [MG_Globals.I.curPlayerNum].gold.ToString();
	}

	public void hide(bool isCancel = true){
		c.SetActive (false);
		announceTime = 0;
		isShow = false;

		if (isCancel) {
			MG_ControlTargeters.I._clearTargeters ();
			MG_ControlCursor.I._interact ();
			MG_Globals.I.mode = "in map";
			MG_Globals.I.curCommand = "in map";
			MG_ControlCursor.I._changeCursorSprite ("Normal");
			MG_ControlCommand.I._changeCommandsAndUI ();
		}
	}

	public void selectCard(int newSel) {
		selCard = newSel;

		i_port.sprite = MG_DB_Cards.I._getPortrait (card_deck[selCard]);
		i_cursor.transform.localPosition = curPos [newSel];

		// t_name, t_name2, t_type, t_type2, t_gold, t_gold2, t_desc
		t_name.text = t_name2.text = MG_DB_Cards.I.getName (card_deck[selCard]);
		t_type.text = t_type2.text = MG_DB_Cards.I.getType (card_deck[selCard]);
		t_gold.text = t_gold2.text = MG_DB_Cards.I.getCost (card_deck[selCard]).ToString();
		t_desc.text = MG_DB_Cards.I.getDesc (card_deck[selCard]);

		MG_ControlSounds.I._playSound(2, 0, 0, false);
	}

	public void throwSelCard_toBottomDeck(){
		// Bring the used card to bottom of the deck
		string temp = card_deck [selCard], temp2 = "";
		for (int i = card_deck.Count - 1; i >= selCard; i--) {
			temp2 = card_deck [i];
			card_deck [i] = temp;
			temp = temp2;
		}

		// Change button images
		for (int i = 0; i < btn_cards.Count; i++) {
			btn_cards [i].image.sprite = MG_DB_Cards.I._getPortrait (card_deck[i]);
		}
	}

	public void shuffleDeck() {
		selCard = 0;

		// Shuffle the deck
		for (int i = 0; i < card_deck.Count; i++) {
			string temp = card_deck[i];
			int randomIndex = Random.Range(i, card_deck.Count);
			card_deck[i] = card_deck[randomIndex];
			card_deck[randomIndex] = temp;
		}

		// Change button images
		for (int i = 0; i < btn_cards.Count; i++) {
			btn_cards [i].image.sprite = MG_DB_Cards.I._getPortrait (card_deck[i]);
		}
	}
}
