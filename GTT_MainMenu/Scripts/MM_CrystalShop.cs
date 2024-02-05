using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_CrystalShop : MonoBehaviour {
	public static MM_CrystalShop I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Text t_selWindow;

	public int curWindow = 0, WINDOW_MAX;

	public void start (){
		WINDOW_MAX = 2; // count starts at 0

		go.SetActive (true);
		go_skins.SetActive (true);
		go_units.SetActive (true);
		go_spells.SetActive (true);
		start_Skins ();
		start_Units ();
		start_spells ();
		start_buyCrystals ();
		changeWindow (0);

		go.SetActive (false);

		updateCrystals ();
	}

	public void changeWindow_prev(){changeWindow (-1);}
	public void changeWindow_next(){changeWindow (1);}

	public void changeWindow(int inc){
		curWindow += inc;
		if (curWindow < 0) 				curWindow = WINDOW_MAX;
		if (curWindow > WINDOW_MAX) 	curWindow = 0;

		go_skins.SetActive (false);
		go_units.SetActive (false);
		go_spells.SetActive (false);
		//go_buyCrystals.SetActive (false);

		switch(curWindow){
			case 0: go_skins.SetActive (true); t_selWindow.text = "Skins"; break;
			case 1: go_units.SetActive (true); t_selWindow.text = "Units"; break;
			case 2: go_spells.SetActive (true); t_selWindow.text = "Spells"; break;
			//case 3: go_buyCrystals.SetActive (true); t_selWindow.text = "Buy Crystals"; break;
		}
	}

	#region "General"
	public Text t_crystals;
	public int crystals;

	public void loseCrystals(int cost){
		crystals -= cost;
		if (crystals < 0)  crystals = 0;
		t_crystals.text = crystals.ToString ();

		ZPlayerPrefs.SetInt ("crystals", crystals);
		MainMenu.I.updateCrystals ();
	}

	public void gainCrystals(int cost){
		crystals += cost;
		t_crystals.text = crystals.ToString ();

		ZPlayerPrefs.SetInt ("crystals", crystals);
		MainMenu.I.updateCrystals ();
	}

	// Used by outside source (daily rewards)
	public void updateCrystals(){
		crystals = ZPlayerPrefs.GetInt ("crystals");
		int crys = ZPlayerPrefs.GetInt ("crystals");
		t_crystals.text = crys.ToString ();
	}
	#endregion

	#region "Skins"
	public Sprite spr_dummy, spr_crystal, spr_check;

	public GameObject go_skins;
	public Image heroSelectCursor;
	public List<Button> btn_heroes, btn_buy;
	public List<GameObject> btn_heroesGo, btn_buyGo, skinPanel;
	public List<Image> i_skinSprites, i_skinStatus;
	public List<Text> t_skinName, t_skinPrice, t_skinBtn;
	public Text t_heroPageNum, t_skinPageNum;

	public int hero_selNum, hero_page, skin_allHeroSkinCount, skin_page, skin_pageMax, skin_curEquipped;
	public int HERO_PAGE_MAX, HERO_NUM_PER_PAGE, SKINS_PER_PAGE;
	public string selHeroName;

	public List<string> heroList, skinList, skinStatusList;
	public List<int> skinPrice;

	public void start_Skins(){
		// Variables
		HERO_PAGE_MAX = 3;
		HERO_NUM_PER_PAGE = 12;
		SKINS_PER_PAGE = 8;
		hero_page = 1;
		skin_page = 1;

		// UI Elements
		string sNum = "";
		for (int l = 1; l <= HERO_NUM_PER_PAGE; l++) {
			sNum = ((l < 10) ? "0" : "");
			btn_heroes.Add (GameObject.Find ("BTN_SkinHeroList_Hero0" + sNum + l.ToString ()).GetComponent<Button>());
			btn_heroesGo.Add (GameObject.Find ("BTN_SkinHeroList_Hero0" + sNum + l.ToString ()));
		}
		for (int l = 1; l <= SKINS_PER_PAGE; l++) {
			sNum = ((l < 10) ? "0" : "");
			btn_buy.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString () + "_Btn").GetComponent<Button>());
			i_skinSprites.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString () + "_Img").GetComponent<Image>());
			t_skinName.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString () + "_Txt").GetComponent<Text>());
			t_skinPrice.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString () + "_TxtCost").GetComponent<Text>());
			t_skinBtn.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString () + "_BtnTxt").GetComponent<Text>());
			btn_buyGo.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString () + "_Btn"));
			i_skinStatus.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString () + "_StatusImg").GetComponent<Image>());
			skinPanel.Add (GameObject.Find ("P_SkinList_" + sNum + l.ToString ()));
		}
		t_heroPageNum.text = "1/" + HERO_PAGE_MAX.ToString ();

		// Lists
		heroList = new List<string>();
		skinList = new List<string>();
		skinStatusList = new List<string>();
		for (int i = 0; i < HERO_NUM_PER_PAGE; i++)
			heroList.Add ("");
		for (int i = 0; i < SKINS_PER_PAGE; i++) {
			skinList.Add ("");
			skinPrice.Add (0);
			skinStatusList.Add ("0");
		}

		heroUpdateList ();
		_selectHero (0);
	}

	public void _selectHero(int selectNum){
		int selectPos = selectNum + 1;
		float posX = ((selectPos - 1) % 3) + 1;
		float posY = Mathf.Floor ((selectPos-1) / 3) + 1;
		float actPosX = -141.8f + 71.4f * posX;
		float actPosY = 192.55f - 69f * posY;
		heroSelectCursor.transform.localPosition = new Vector2(actPosX, actPosY);
		hero_selNum = selectNum + ((hero_page - 1) * HERO_NUM_PER_PAGE);

		selHeroName = MM_CrystalShop_DB.I.heroList_inCodeName (hero_selNum);
		skin_allHeroSkinCount = MM_CrystalShop_DB.I.skinList_skinCount (selHeroName);
		skin_pageMax = Mathf.RoundToInt(Mathf.Floor ((skin_allHeroSkinCount-1) / 6) + 1);

		skinUpdateList ();
	}

	public void _heroChangePage_Prev(){ heroChangePage (-1); }
	public void _heroChangePage_Next(){ heroChangePage (1); }

	public void heroChangePage(int incerement){
		hero_page += incerement;
		if (hero_page > HERO_PAGE_MAX)  hero_page = 1;
		if (hero_page <= 0)  hero_page = HERO_PAGE_MAX;

		t_heroPageNum.text = hero_page.ToString() + "/" + HERO_PAGE_MAX.ToString ();

		heroUpdateList ();

		_selectHero (0);
	}

	public void heroUpdateList(){
		for (int l = 0; l < btn_heroes.Count; l++) {
			heroList [l] = MM_CrystalShop_DB.I.heroList_inCodeName (l + ((hero_page - 1) * HERO_NUM_PER_PAGE));
			if (heroList [l] == "NONE") {
				btn_heroesGo [l].SetActive (false);
			} else {
				btn_heroesGo [l].SetActive (true);
				btn_heroes [l].image.sprite = MM_CrystalShop_DB.I.heroList_portrait (l + ((hero_page - 1) * HERO_NUM_PER_PAGE));
			}
		}
	}

	public void _skinChangePage_Prev(){ skinChangePage (-1); }
	public void _skinChangePage_Next(){ skinChangePage (1); }

	public void skinChangePage(int incerement){
		skin_page += incerement;
		if (skin_page > skin_pageMax)  skin_page = 1;
		if (skin_page <= 0)  hero_page = skin_pageMax;

		t_skinPageNum.text = hero_page.ToString() + "/" + skin_pageMax.ToString ();

		skinUpdateList ();
	}

	public void skinUpdateList(){
		string skinName = "";
		string skinStatus = "";

		string skinStatus_string = ZPlayerPrefs.GetString ("skinsUnlocked_" + selHeroName);
		skin_curEquipped = ZPlayerPrefs.GetInt ("skinEquipped_" + selHeroName);
		int skinIndex = 0;

		for (int i = 0; i < SKINS_PER_PAGE; i++) {
			skinName = MM_CrystalShop_DB.I.skinList_skinName (selHeroName, i);
			skinList [i] = skinName;
			skinIndex = i + ((skin_page - 1) * SKINS_PER_PAGE);
			skinStatus += skinStatus_string [skinIndex];

			if (skinName == "NONE") {
				skinPanel [i].SetActive (false);
				skinStatusList [i] = "NONE";
			} else {
				skinPanel [i].SetActive (true);
				t_skinName [i].text = skinName;
				i_skinSprites [i].sprite = MM_CrystalShop_DB.I.skinList_skinSprite(selHeroName, i);
				i_skinSprites [i].rectTransform.sizeDelta = MM_CrystalShop_DB.I.skinList_skinDimensions(selHeroName, i);
				i_skinSprites [i].rectTransform.localPosition = MM_CrystalShop_DB.I.skinList_skinPosition(selHeroName, i);

				if (skinStatus [i] == '0') {
					// Skin still locked
					skinPrice [i] = MM_CrystalShop_DB.I.skinList_skinPrice (selHeroName, i);
					t_skinPrice [i].text = skinPrice [i].ToString ();
					btn_buyGo [i].SetActive (true);
					t_skinBtn [i].text = "Buy";
					skinStatusList [i] = "Buy";
					i_skinStatus [i].sprite = spr_crystal;
				}else if(skinStatus [i] == '1'){
					// Skin unlocked
					if(skin_curEquipped == skinIndex){
						// Skin is equipped
						t_skinName [i].text = skinName;
						skinPrice [i] = 0;
						t_skinPrice [i].text = "Equipped";
						btn_buyGo [i].SetActive (false);
						skinStatusList [i] = "Equipped";
						i_skinStatus [i].sprite = spr_check;
					}else{
						// Skin is not equipped
						t_skinName [i].text = skinName;
						skinPrice [i] = 0;
						t_skinPrice [i].text = "Owned";
						btn_buyGo [i].SetActive (true);
						t_skinBtn [i].text = "Equip";
						skinStatusList [i] = "Owned";
						i_skinStatus [i].sprite = spr_dummy;
					}
				}
			}
		}
	}

	public void buySkin(int skinNum) {
		int skinIndex = skinNum + ((skin_page - 1) * SKINS_PER_PAGE);

		if (skinStatusList [skinNum] == "Owned") {
			skin_curEquipped = skinIndex;
			if (selHeroName == "Point" || selHeroName == "Camp" ) {
				ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName, skinIndex);
				ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName + "Blue" , skinIndex);
				ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName + "Red", skinIndex);
			} else {
				ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName, skinIndex);
			}

			MM_Popups.I.show_ok ("Skin equipped.");
			skinUpdateList ();
		}else if (skinStatusList [skinNum] == "Buy") {
			if (skinPrice [skinNum] <= crystals) {
				MM_Sounds.I._playSound(4, 0, 0, false);

				loseCrystals (skinPrice [skinNum]);
				string skinStatus_string = ZPlayerPrefs.GetString ("skinsUnlocked_" + selHeroName);
				string newString = "";
				for (int i = 0; i < skinStatus_string.Length; i++) {
					if (i == skinNum) {
						newString += "1";
					} else {
						newString += skinStatus_string [i];
					}
				}
				ZPlayerPrefs.SetString ("skinsUnlocked_" + selHeroName, newString);

				skin_curEquipped = skinIndex;
				if (selHeroName == "Point" || selHeroName == "Camp" ) {
					ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName, skinIndex);
					ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName + "Blue" , skinIndex);
					ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName + "Red", skinIndex);
				} else {
					ZPlayerPrefs.SetInt ("skinEquipped_" + selHeroName, skinIndex);
				}

				skinUpdateList ();
				MM_Popups.I.show_ok ("Skin purchased and equipped.");

				// Steam Achievement - Costume Party
				MM_Steam.I.unlockAchievement ("COSTUME_PARTY_1_8");
			} else {
				MM_Popups.I.show_ok ("Not enough crystals.");
			}
		}
	}
	#endregion

	#region "Units"
	public GameObject go_units;

	public Image unitSelectCursor, unitPortrait;
	public List<Button> btn_units;
	public List<GameObject> btn_unitsGo;
	public Text t_unitPageNum, t_unitName, t_unitCost, t_unitDesc, t_unitBuy;
	public Button btn_buyUnit;

	public int unit_selNum, unit_page;
	public int UNIT_PAGE_MAX, UNIT_NUM_PER_PAGE;
	public string selUnitName;

	public List<string> unitList, unitStatusList;
	public List<int> unitPrice;

	public void start_Units(){
		// Variables
		UNIT_PAGE_MAX = 1;
		UNIT_NUM_PER_PAGE = 16;
		unit_page = 1;
		unit_selNum = 0;

		// UI Elements
		string sNum = "";
		for (int l = 1; l <= UNIT_NUM_PER_PAGE; l++) {
			sNum = ((l < 10) ? "0" : "");
			btn_units.Add (GameObject.Find ("BTN_UnitList_Unit" + sNum + l.ToString ()).GetComponent<Button>());
			btn_unitsGo.Add (GameObject.Find ("BTN_UnitList_Unit" + sNum + l.ToString ()));
		}
		t_unitPageNum.text = "1/" + UNIT_PAGE_MAX.ToString ();

		// Lists
		unitList = new List<string>();
		unitStatusList = new List<string>();
		for (int i = 0; i < UNIT_NUM_PER_PAGE; i++) {
			unitList.Add ("");
			unitStatusList.Add ("0");
		}

		unitUpdateList ();
		_selectUnit (0);
	}

	public void _selectUnit(int selectNum){
		int selectPos = selectNum + 1;
		float posX = ((selectPos - 1) % 8) + 1;
		float posY = Mathf.Floor ((selectPos-1) / 8) + 1;
		float actPosX = -316.9f + 71.4f * posX;
		float actPosY = 202.55f - 79f * posY;
		unitSelectCursor.transform.localPosition = new Vector2(actPosX, actPosY);
		unit_selNum = selectNum + ((unit_page - 1) * UNIT_NUM_PER_PAGE);

		selUnitName = MM_CrystalShop_DB.I.unitList_inCodeName (unit_selNum);
		t_unitName.text = MM_CrystalShop_DB.I.unitList_Name (unit_selNum);
		t_unitDesc.text = MM_CrystalShop_DB.I.unitList_Desc (unit_selNum);
		t_unitCost.text = "Cost: " + MM_CrystalShop_DB.I.unitList_Cost (unit_selNum).ToString();
		unitPortrait.sprite = MM_CrystalShop_DB.I.unitList_portrait (unit_selNum);

		if (ZPlayerPrefs.GetInt ("unitUnlocked_" + unitList [unit_selNum]) == 0) {
			btn_buyUnit.interactable = true;
			t_unitBuy.text = "Unlock";
		} else {
			btn_buyUnit.interactable = false;
			t_unitBuy.text = "Unlocked";
		}
	}

	public void unitUpdateList(){
		int isUnlocked = 0;
		ColorBlock cb;
		for (int l = 0; l < btn_units.Count; l++) {
			unitList [l] = MM_CrystalShop_DB.I.unitList_inCodeName (l + ((unit_page - 1) * UNIT_NUM_PER_PAGE));
			Debug.Log (unitList [l]);
			if (unitList [l] == "NONE") {
				btn_unitsGo [l].SetActive (false);
			} else {
				btn_unitsGo [l].SetActive (true);
				isUnlocked = ZPlayerPrefs.GetInt ("unitUnlocked_" + unitList [l]);
				cb = btn_units [l].colors;
				cb.normalColor = (isUnlocked == 1) ? new Color(1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f);
				btn_units [l].colors = cb;
				btn_units [l].image.sprite = MM_CrystalShop_DB.I.unitList_portrait (l + ((unit_page - 1) * UNIT_NUM_PER_PAGE));
			}
		}
	}

	public void buyUnit() {
		if (MM_CrystalShop_DB.I.unitList_Cost (unit_selNum) <= crystals) {
			loseCrystals (MM_CrystalShop_DB.I.unitList_Cost (unit_selNum));
			ZPlayerPrefs.SetInt ("unitUnlocked_" + selUnitName, 1);
			MM_Popups.I.show_ok ("Unit unlocked.");

			unitUpdateList ();
		} else {
			MM_Popups.I.show_ok ("Not enough crystals.");
		}
	}
	#endregion

	#region "Spells"
	public GameObject go_spells;

	public Image spellSelectCursor, spellPortrait;
	public List<Button> btn_spells;
	public List<GameObject> btn_spellsGo;
	public Text t_spellPageNum, t_spellName, t_spellCost, t_spellDesc, t_spellBuy;
	public Button btn_buySpell;

	public int spell_selNum, spell_page;
	public int SPELL_PAGE_MAX, SPELL_NUM_PER_PAGE;
	public string selSpellName;

	public List<string> spellList, spellStatusList;
	public List<int> spellPrice;

	public void start_spells(){
		// Variables
		SPELL_PAGE_MAX = 1;
		SPELL_NUM_PER_PAGE = 8;
		spell_page = 1;
		spell_selNum = 0;

		// UI Elements
		string sNum = "";
		for (int l = 1; l <= SPELL_NUM_PER_PAGE; l++) {
			sNum = ((l < 10) ? "0" : "");
			btn_spells.Add (GameObject.Find ("BTN_SpellsList_Spell" + sNum + l.ToString ()).GetComponent<Button>());
			btn_spellsGo.Add (GameObject.Find ("BTN_SpellsList_Spell" + sNum + l.ToString ()));
		}
		t_spellPageNum.text = "1/" + SPELL_PAGE_MAX.ToString ();

		// Lists
		spellList = new List<string>();
		spellStatusList = new List<string>();
		for (int i = 0; i < SPELL_NUM_PER_PAGE; i++) {
			spellList.Add ("");
			spellStatusList.Add ("0");
		}

		spellUpdateList ();
		_selectSpell (0);
	}

	public void _selectSpell(int selectNum){
		int selectPos = selectNum + 1;
		float posX = ((selectPos - 1) % 8) + 1;
		float posY = Mathf.Floor ((selectPos-1) / 8) + 1;
		float actPosX = -316.9f + 71.4f * posX;
		float actPosY = 192.55f - 69f * posY;
		spellSelectCursor.transform.localPosition = new Vector2(actPosX, actPosY);
		spell_selNum = selectNum + ((spell_page - 1) * SPELL_NUM_PER_PAGE);

		selSpellName = MM_CrystalShop_DB.I.spellList_inCodeName (spell_selNum);
		t_spellName.text = MM_CrystalShop_DB.I.spellList_Name (spell_selNum);
		t_spellDesc.text = MM_CrystalShop_DB.I.spellList_Desc (spell_selNum);
		t_spellCost.text = "Cost: " + MM_CrystalShop_DB.I.spellList_Cost (spell_selNum).ToString();
		spellPortrait.sprite = MM_CrystalShop_DB.I.spellList_portrait (spell_selNum);

		if (ZPlayerPrefs.GetInt ("spellUnlocked_" + spellList [spell_selNum]) == 0) {
			btn_buySpell.interactable = true;
			t_spellBuy.text = "Unlock";
		} else {
			btn_buySpell.interactable = false;
			t_spellBuy.text = "Unlocked";
		}
	}

	public void spellUpdateList(){
		int isUnlocked = 0;
		ColorBlock cb;
		for (int l = 0; l < btn_spells.Count; l++) {
			spellList [l] = MM_CrystalShop_DB.I.spellList_inCodeName (l + ((spell_page - 1) * SPELL_NUM_PER_PAGE));
			Debug.Log (spellList [l]);
			if (spellList [l] == "NONE") {
				btn_spellsGo [l].SetActive (false);
			} else {
				btn_spellsGo [l].SetActive (true);
				isUnlocked = ZPlayerPrefs.GetInt ("spellUnlocked_" + spellList [l]);
				cb = btn_spells [l].colors;
				cb.normalColor = (isUnlocked == 1) ? new Color(1f, 1f, 1f) : new Color(0.5f, 0.5f, 0.5f);
				btn_spells [l].colors = cb;
				btn_spells [l].image.sprite = MM_CrystalShop_DB.I.spellList_portrait (l + ((spell_page - 1) * SPELL_NUM_PER_PAGE));
			}
		}
	}

	public void buySpell() {
		if (MM_CrystalShop_DB.I.spellList_Cost (spell_selNum) <= crystals) {
			loseCrystals (MM_CrystalShop_DB.I.spellList_Cost (spell_selNum));
			ZPlayerPrefs.SetInt ("spellUnlocked_" + selSpellName, 1);
			MM_Popups.I.show_ok ("Spell unlocked.");

			unitUpdateList ();
		} else {
			MM_Popups.I.show_ok ("Not enough crystals.");
		}
	}
	#endregion

	#region "Buy Crystals"
	public GameObject go_buyCrystals;

	public void start_buyCrystals(){
		
	}

	public void btn_buyCrystal(int selectNum){
		// Buy function is at Purchaser.cs
	}
	#endregion
}
