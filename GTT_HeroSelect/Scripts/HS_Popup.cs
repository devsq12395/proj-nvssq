using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class HS_Popup : MonoBehaviour {
	public static HS_Popup I;
	private void Awake() {I = this;}

	public GameObject go, btn_ok, btn_yes, btn_no;
	public Text t;

	public bool enemyDisc = false;

	#region "Basic popup"
	public void _show(string msg, bool isOption = false, bool disc = false){
		t.text = msg;
		go.SetActive (true);

		if (isOption) {
			btn_ok.SetActive (false);
			btn_yes.SetActive (true);
			btn_no.SetActive (true);
		} else {
			btn_ok.SetActive (true);
			btn_yes.SetActive (false);
			btn_no.SetActive (false);
		}

		enemyDisc = disc;
	}

	public void _hide(){
		HH_Sounds.I._playSound(2, 0, 0, false);
		go.SetActive (false);

		if (enemyDisc) {
			PhotonRoom.room.disconnect ();
			SceneManager.LoadScene ("Lobby2");
		}
	}

	public void _yes(){
		HH_Sounds.I._playSound(2, 0, 0, false);
		PhotonRoom.room.disconnect ();
		SceneManager.LoadScene ("Lobby2");
	}
	#endregion

	#region "Save - Integrated saving method"
	public GameObject go_save;
	public InputField if_save;
	public int slotNum;

	public void _show_save(){
		// Get empty slot num
		slotNum = -1;
		for (int i = 1; i <= 10; i++) {
			if(PlayerPrefs.GetInt("preset_slot" + i.ToString() + "_occupied") == 0){
				slotNum = i;
				break;
			}
		}
		if (slotNum == -1) {
			_show ("Save slots are full!");
			return;
		}

		go_save.SetActive (true);
	}

	public void _hide_save(){
		go_save.SetActive (false);
	}

	public void _save(){
		if (if_save.text.Length <= 2) {
			_show ("Minimum of 3 characters");
			return;
		}

		_hide_save ();
		_show ("Team saved successfully");
	}
	#endregion
	#region "Save - Windows save API method"
	/*public void _show_save(){
		var path = EditorUtility.SaveFilePanel(
			"Save Team file",
			"",
			texture.name + ".txt",
			"txt"
		);
	}*/
	#endregion

	#region "Load - Integrated saving method"
	public GameObject go_load;
	public List<Text> t_loadName;

	public void _start_load(){
		go_load.SetActive (true);
		for (int i = 1; i <= 10; i++) {
			t_loadName.Add (GameObject.Find("T_Name_LoadTeam" + i.ToString()).GetComponent<Text>());
		}
		go_load.SetActive (false);
	}

	public void _show_load(){
		// Get empty slot num
		slotNum = -1;
		for (int i = 1; i <= 10; i++) {
			if (PlayerPrefs.GetInt ("preset_slot" + i.ToString () + "_occupied") == 1) {
				t_loadName [i - 1].text = PlayerPrefs.GetString ("preset_slot" + i.ToString () + "_name");
			} else {
				t_loadName [i - 1].text = "";
			}
		}

		go_load.SetActive (true);
	}

	public void _hide_load(){
		go_load.SetActive (false);
	}

	public void _load(int slotNum){
		if (PlayerPrefs.GetInt ("preset_slot" + slotNum.ToString () + "_occupied") == 0) {
			_hide_load ();
			_show ("Slot is empty.");
			return;
		}

		// Load
		string name = "";
		int index = -1;

		for (int i = 0; i < 24; i++) {
			name = PlayerPrefs.GetString ("preset_slot" + slotNum.ToString () + "_card" + (i+1).ToString ());
			
			Debug.Log(i+ ", " + name);
			Debug.Log("placing at " + i.ToString() + ", " + (i%12).ToString());
			DeckBuilder.I.changeCard (i, i % 12, DB_Database.I.getIndex(name), false);
		}

		_hide_load ();
		_show ("Loaded " + PlayerPrefs.GetString ("preset_slot" + slotNum.ToString () + "_name"));
	}

	public void _delete(int slotNum){
		if (PlayerPrefs.GetInt ("preset_slot" + slotNum.ToString () + "_occupied") == 0) {
			_hide_load ();
			_show ("Slot is empty.");
			return;
		}

		PlayerPrefs.SetInt ("preset_slot" + slotNum.ToString () + "_occupied", 0);
		_show_load ();

		_show ("Deleted successfully.");
	}
	#endregion
}
