using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampaignMap : MonoBehaviour {

	public GameObject go_briefing;
	public RectTransform rect_campaignMap;
	public MG_ButtonHold btnNext, btnPrev;
	public Text t_missionName, t_missionDesc, t_heroDialog;
	public Image i_heroPort;

	public int campaignNum;
	public float mapPosX, mapPosX_min, mapPosX_speed;

	void Start () {
		DB_Campaign.I._start ();

		mapPosX_min = -1000f;
		mapPosX_speed = 10f;
	}

	void Update () {
		if (btnNext.isPressed) 		_scrollPrev ();
		if (btnPrev.isPressed) 		_scrollNext ();
	}

	#region "BUTTON - Select Campaign"
	public void _btn_selectCampaign_main_001(){ _btn_selectCampaign (1);}

	public void _btn_selectCampaign(int campNumber){
		go_briefing.SetActive (true);

		campaignNum = campNumber;

		DB_Campaign.I._setValues (campNumber);
		t_missionName.text = DB_Campaign.I.name;
		t_missionDesc.text = DB_Campaign.I.desc;
		t_heroDialog.text = DB_Campaign.I.heroDialog;
		i_heroPort.sprite = DB_Campaign.I.sprite;
	}
	#endregion
	#region "BUTTON - Back"
	public void _btn_back(){
		
	}
	#endregion

	#region "BUTTON - Scroll Prev"
	public void _scrollPrev(){
		mapPosX -= mapPosX_speed;
		if(mapPosX < mapPosX_min)	mapPosX = mapPosX_min;

		rect_campaignMap.localPosition = new Vector2 (mapPosX, 0);
	}
	#endregion
	#region "BUTTON - Scroll Next"
	public void _scrollNext(){
		mapPosX += mapPosX_speed;
		if(mapPosX > 0)	mapPosX = 0;

		rect_campaignMap.localPosition = new Vector2 (mapPosX, 0);
	}
	#endregion

	#region "BUTTON - Choose"
	public void _btn_choose(){
		
	}
	#endregion
	#region "BUTTON - Cancel"
	public void _btn_cancel(){
		go_briefing.SetActive (false);
	}
	#endregion
}
