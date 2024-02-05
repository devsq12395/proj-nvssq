using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Campaigns : MonoBehaviour {
	public Image i, i_port;
	public Text t_name, t_desc, t_desc2;

	public GameObject c_campaign;
	public int campNum = 0;

	public List<Text> t_hs;

    void Start() {
		c_campaign.SetActive (false);

		// Add high score text here
		t_hs.Add (GameObject.Find("T_Camp01_Score").GetComponent<Text>());
		t_hs.Add (GameObject.Find("T_Camp02_Score").GetComponent<Text>());
		t_hs.Add (GameObject.Find("T_Camp03_Score").GetComponent<Text>());

		// Define high scores
		for(int i = 0; i < 3; i++){
			t_hs[i].text = "High Score: " + PlayerPrefs.GetInt ("camp_highScore_" + (i+1).ToString()).ToString();
			Debug.Log (t_hs [i].text);
		}
    }

    void Update() {
        
    }

	public void setCampaign(int newNum){
		campNum = newNum;

		c_campaign.SetActive (true);

		i_port.sprite = C_DB.I.getPortrait(campNum);
		t_name.text = C_DB.I.getName(campNum);
		int score = PlayerPrefs.GetInt ("camp_highScore_" + (newNum+1).ToString ());
		t_desc.text = C_DB.I.getDesc(campNum) + "\n\nHigh Score: " + score.ToString ();
	}

	public void BTNNext(){
		int newCampNum = campNum + 1;

		PlayerPrefs.SetInt ("camp_Number", campNum + 1);
		PlayerPrefs.SetInt ("camp_mapNum", 1);
		PlayerPrefs.SetInt ("camp_mapNum_Max", 1); // C_DB.I.getMaxMapNum(campNum)
		PlayerPrefs.SetInt ("camp_score_" + newCampNum.ToString(), 0);
		PlayerPrefs.SetInt ("camp_highScore_" + newCampNum.ToString(), 0);
		PlayerPrefs.SetInt ("camp_heroKills", 0);
		PlayerPrefs.SetInt ("camp_unitKills", 0);
		PlayerPrefs.SetInt ("camp_heroDeaths", 0);
		PlayerPrefs.SetInt ("camp_curGold", 10);

		PlayerPrefs.SetString("game_mapName", C_DB.I.getMap(campNum));

		PlayerPrefs.SetInt ("camp_mapNum", 1);

		PlayerPrefs.SetInt("isMultiplayer", 2);
		SceneManager.LoadScene ("HeroSelect");
	}

	public void BTNPrev(){
		c_campaign.SetActive (false);
	}

	public void BTNBack(){
		SceneManager.LoadScene ("MainMenu");
	}
}
