using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_KillFeed : MonoBehaviour {
	public static MG_UIControl_KillFeed I;
	public void Awake(){ I = this; }

	public GameObject go_killFeed;
	public List<GameObject> cList;
	public List<Image> iList_killer, iList_victim;

	public int shownKills, shownKills_max;

	public void _start(){
		// Enable Parent Game Object
		go_killFeed.SetActive(true);

		// Defaults
		shownKills_max				= 9;

		// UI Elements
		cList = new List<GameObject>();
		iList_killer = new List<Image> ();
		iList_victim = new List<Image> ();
		for (int i = 1; i <= shownKills_max; i++) {
			cList.Add (GameObject.Find ("C_KillFeed_" + i.ToString ()));
			iList_killer.Add (GameObject.Find ("I_KillFeed" + i.ToString () + "_PortKiller").GetComponent<Image> ());
			iList_victim.Add (GameObject.Find ("I_KillFeed" + i.ToString () + "_PortVictim").GetComponent<Image> ());
		}
	
		// Disable canvases while not in use
		foreach(GameObject c in cList){
			c.SetActive (false);
		}
	}

	#region "Show Kill"
	public void _showKill(string killerName, string victimName){
		cList [shownKills].SetActive (true);
		iList_killer [shownKills].sprite = MG_DB_Units.I._getPortrait (killerName);
		iList_victim [shownKills].sprite = MG_DB_Units.I._getPortrait (victimName);

		shownKills++;
		if(shownKills > shownKills_max)		shownKills = shownKills_max;
	}
	#endregion

	#region "Reset Kill Feed"
	public void _resetKillFeed(){
		foreach(GameObject c in cList){
			c.SetActive (false);
		}
		for (int i = 0; i < shownKills_max; i++) {
			cList [i].SetActive (false);
		}

		shownKills = 0;
	}
	#endregion
}
