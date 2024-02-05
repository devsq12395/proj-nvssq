using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DigitalRuby.Tween;

public class MM_Popups : MonoBehaviour {
	public static MM_Popups I;
	public void Awake(){ I = this; }

	public Sprite spr_crystals, spr_rank, spr_key;

	public void _start(){
		go_ifName.SetActive (false);
		go_ok.SetActive (false);
		go_im.SetActive (false);
		go_dr.SetActive (false);
	}

	void Update(){
		
	}

	#region "INPUT FIELD - Name"
	public GameObject go_ifName;
	public InputField if_ifName;
	public RectTransform rt_ifName;
	public Text t_if;
	public bool isEditName = false;

	public void editName(){
		t_if.text = "Edit your name:";
		isEditName = true;
		go_ifName.SetActive(true);
	}

	public void BTN_IFName_OK(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		if (if_ifName.text.Length <= 2) {
			show_ok ("Minimum of 3 characters");
			return;
		}

		PlayerPrefs.SetString ("playerName", if_ifName.text);
		if (isEditName) {
			go_ifName.SetActive(false);
			MainMenu.I.t_name.text = if_ifName.text;
		} else {
			tweenOut_IFName ();
		}
	}

	public void tweenOut_IFName(){
		float firstPos = 6;
		float targetPos = -500;
		float duration = 1f;

		gameObject.Tween("uiM_mainMenu_IFName", firstPos, targetPos, duration, TweenScaleFunctions.CubicEaseOut, (t) =>{
			// progress
			rt_ifName.localPosition = new Vector3(rt_ifName.localPosition.x, t.CurrentValue, rt_ifName.localPosition.z);
		}, (t) =>{
			// completion
			rt_ifName.localPosition = new Vector3(rt_ifName.localPosition.x, t.CurrentValue, rt_ifName.localPosition.z);
			go_ifName.SetActive(false);
			MainMenu.I._setupMainMenu ();
			MainMenu.I._startTween_title();

			//MainMenu.I.dailyReward ();
		});
	}
	#endregion

	#region "POPUP - Ok"
	public GameObject go_ok;
	public Text t_ok;

	public void show_ok(string msg){
		t_ok.text = msg;
		go_ok.SetActive (true);
	}

	public void hide_ok(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		go_ok.SetActive (false);
	}
	#endregion

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
			case 2: i_im.sprite = spr_key; break;
		}
	}

	public void hide_im(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		go_im.SetActive (false);
	}
	#endregion

	#region "POPUP - Daily Reward"
	public GameObject go_dr;
	public Text t_drDay, t_drReward;
	public Image i_dr;

	public void show_dr(string day, string reward, int imageId = 0){
		t_drDay.text = day;
		t_drReward.text = reward;
		go_dr.SetActive (true);

		switch (imageId) {
			case 0: i_dr.sprite = spr_crystals; break;
			case 1: i_dr.sprite = spr_key; break;
			case 2: i_dr.sprite = spr_rank; break;
		}
	}

	public void hide_dr(){
		MM_Sounds.I._playSound(2, 0, 0, false);
		go_dr.SetActive (false);
	}
	#endregion
}
