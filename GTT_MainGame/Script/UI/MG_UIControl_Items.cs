using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Items : MonoBehaviour {
	public static MG_UIControl_Items I;
	public void Awake(){ I = this; }

	public List<Image> i_itemImg_p1, i_itemImg_p2;
	public List<Text> t_item_p1, t_item_p2;
	public List<Text> t_itemBtn_p1, t_itemBtn_p2;

	public void _start() {
		i_itemImg_p1 				= new List<Image> ();
		i_itemImg_p2 				= new List<Image> ();
		t_item_p1 					= new List<Text> ();
		t_item_p2 					= new List<Text> ();
		t_itemBtn_p1 				= new List<Text> ();
		t_itemBtn_p2 				= new List<Text> ();

		// Set
		MG_UIControl_TopBar.I.c_item.SetActive (true);
		for(int i = 1; i <= 6; i++){
			i_itemImg_p1.Add (GameObject.Find ("I_ItBox_ItemImg_P1_0" + i.ToString ()).GetComponent<Image> ());
			t_item_p1.Add (GameObject.Find ("TXT_ItBox_name_P1_0" + i.ToString ()).GetComponent<Text> ());
			t_itemBtn_p1.Add (GameObject.Find ("TXT_ItBox_check_P1_0" + i.ToString ()).GetComponent<Text> ());

			i_itemImg_p2.Add (GameObject.Find ("I_ItBox_ItemImg_P2_0" + i.ToString ()).GetComponent<Image> ());
			t_item_p2.Add (GameObject.Find ("TXT_ItBox_name_P2_0" + i.ToString ()).GetComponent<Text> ());
			t_itemBtn_p2.Add (GameObject.Find ("TXT_ItBox_check_P2_0" + i.ToString ()).GetComponent<Text> ());
		}
		MG_UIControl_TopBar.I.c_item.SetActive (false);
	}

	// Show and Hide window is handled at MG_UIControl_TopBar.cs

	#region "Update Items UI"
	public void _updateItemsUI(){
		for (int i = 0; i <= 5; i++) {
			// Player 1
			i_itemImg_p1 [i].sprite = MG_DB_Items.I._getSprite (MG_Globals.I.players [1].items [i]);

			if (MG_Globals.I.players [1].items [i] == "NONE" || MG_Globals.I.players [1].items [i] == "") {
				if(MG_Globals.I.curPlayerNum == 1) 	t_itemBtn_p1 [i].text = "Buy";
				else 								t_itemBtn_p1 [i].text = "Check";
				t_item_p1 [i].text = "";
			} else {
				t_itemBtn_p1 [i].text = "Check";
				t_item_p1 [i].text = MG_Globals.I.players [1].items [i];
			}

			// Player 2
			i_itemImg_p2 [i].sprite = MG_DB_Items.I._getSprite (MG_Globals.I.players [2].items [i]);

			if (MG_Globals.I.players [2].items [i] == "NONE" || MG_Globals.I.players [1].items [i] == "") {
				if(MG_Globals.I.curPlayerNum == 2) 	t_itemBtn_p2 [i].text = "Buy";
				else 								t_itemBtn_p2 [i].text = "Check";
				t_item_p2 [i].text = "";
			} else{
				t_itemBtn_p2 [i].text = "Check";
				t_item_p2 [i].text = MG_Globals.I.players [2].items [i];
			}
		}
	}
	#endregion

	#region "Button Interact"
	public void _buttonInteract_p1_1(){_buttonInteract (1, 0);}
	public void _buttonInteract_p1_2(){_buttonInteract (1, 1);}
	public void _buttonInteract_p1_3(){_buttonInteract (1, 2);}
	public void _buttonInteract_p1_4(){_buttonInteract (1, 3);}
	public void _buttonInteract_p1_5(){_buttonInteract (1, 4);}
	public void _buttonInteract_p1_6(){_buttonInteract (1, 5);}

	public void _buttonInteract_p2_1(){_buttonInteract (2, 0);}
	public void _buttonInteract_p2_2(){_buttonInteract (2, 1);}
	public void _buttonInteract_p2_3(){_buttonInteract (2, 2);}
	public void _buttonInteract_p2_4(){_buttonInteract (2, 3);}
	public void _buttonInteract_p2_5(){_buttonInteract (2, 4);}
	public void _buttonInteract_p2_6(){_buttonInteract (2, 5);}

	public void _buttonInteract(int playerNum, int buttonNum){
		bool isBuy = false;
		string itemName = "NONE";

		if (playerNum == 1) {
			if (t_itemBtn_p1 [buttonNum].text == "Buy") 	isBuy = true;
			else 											itemName = t_item_p1 [buttonNum].text;
		} else {
			if (t_itemBtn_p2 [buttonNum].text == "Buy") 	isBuy = true;
			else 											itemName = t_item_p2 [buttonNum].text;
		}

		if (isBuy) {
			MG_UIControl_Shop.I._showWindow (playerNum, buttonNum);
		} else {
			if (itemName != "NONE" && itemName != "") {
				MG_UIControl_ItemCheck.I._showWindow (itemName, false, buttonNum);
			}
		}
	}
	#endregion
}
