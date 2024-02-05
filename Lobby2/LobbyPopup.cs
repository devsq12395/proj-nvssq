using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyPopup : MonoBehaviour {
	public static LobbyPopup I;
	private void Awake() {I = this;}

	public GameObject go, btn_ok;
	public Text t;

	public Sprite spr_crystals, spr_rank, spr_key;

	public bool enemyDisc = false;

	public void _start (){
		go.SetActive (false);
		go_im.SetActive (false);
	}

	public void _show(string msg, bool disc = false){
		t.text = msg;
		go.SetActive (true);

		btn_ok.SetActive (false);

		enemyDisc = disc;
	}

	public void _hide(){
		go.SetActive (false);

		if (enemyDisc) {
			PhotonRoom.room.disconnect ();
			SceneManager.LoadScene ("Lobby2");
		}
	}

	#region "POPUP - Image"
	public GameObject go_im;
	public Text t_imTitle, t_imContent;
	public Image i_im;

	public void show_im(string title, string content, int imageId = 0){
		t_imTitle.text = title;
		t_imContent.text = content;
		go_im.SetActive (true);

		switch (imageId) {
			case 0: i_im.sprite = spr_crystals; break;
			case 1: i_im.sprite = spr_rank; break;
		}
	}

	public void hide_im(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		go_im.SetActive (false);
	}
	#endregion
}
