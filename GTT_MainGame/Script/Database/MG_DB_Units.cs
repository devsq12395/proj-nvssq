using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_DB_Units : MonoBehaviour {
	public static MG_DB_Units I;
	public void Awake(){ I = this; }

	// PORTRAITS
	public Sprite port_dummy, port_dead;

	// SPRITES
	public GameObject dummy;

	#region "Robin"
	public GameObject robin_p2_idle_left, robin_p2_idle_right;
	public GameObject robin1_p1_idle_left, robin1_p1_idle_right, robin1_p2_idle_left, robin1_p2_idle_right;
   	#endregion
	
	// Camp
	public GameObject camp_p1_idle, camp_p2_idle;
	
	// Farm
	public GameObject farm_p1_idle, farm_p2_idle;
	
	//  Infantry
	public Sprite port_Infantry;
	public GameObject Infantry_p1_idle_left, Infantry_p1_idle_right;
	public GameObject Infantry_p2_idle_left, Infantry_p2_idle_right;
	
	//  Infantry (Bayonet)
	public GameObject InfantryBayonet_p1_idle_left, InfantryBayonet_p1_idle_right;
	public GameObject InfantryBayonet_p2_idle_left, InfantryBayonet_p2_idle_right;
	
	//  Cavalry
	public GameObject Cavalry_p1_idle_left, Cavalry_p1_idle_right;
	public GameObject Cavalry_p2_idle_left, Cavalry_p2_idle_right;
	
	//  Skirmisher
	public GameObject Skirmisher_p1_idle_left, Skirmisher_p1_idle_right;
	public GameObject Skirmisher_p2_idle_left, Skirmisher_p2_idle_right;
	
	//  Artillery
	public GameObject Artillery_p1_idle_left, Artillery_p1_idle_right;
	public GameObject Artillery_p2_idle_left, Artillery_p2_idle_right;

	#region "Portrait Sprites"
	public Sprite _getPortrait(string newName){
		Sprite retVal = port_dummy;

		switch (newName) {
			
			case "Infantry": 								retVal = port_Infantry; break;
		}

		return retVal;
	}
	#endregion

	#region "Character Sprites"
	// pattern: spriteName + "_p" + playerNum + "_" + animation + "_" + facing
	// sample: testUnit001_p1_move_down
	public GameObject _getSprite(string newSpriteName){
		GameObject returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		Destroy(returnValue);
		switch (newSpriteName) {
			//  Camp
			case "Camp_p1_idle": returnValue = GameObject.Instantiate(camp_p1_idle, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Camp_p2_idle": returnValue = GameObject.Instantiate(camp_p1_idle, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			
			//  Farm
			case "Farm_p1_idle": returnValue = GameObject.Instantiate(farm_p1_idle, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Farm_p2_idle": returnValue = GameObject.Instantiate(farm_p1_idle, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			
			//  Infantry
			case "Infantry_p1_idle_left": returnValue = GameObject.Instantiate(Infantry_p1_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Infantry_p1_idle_right": returnValue = GameObject.Instantiate(Infantry_p1_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Infantry_p2_idle_left": returnValue = GameObject.Instantiate(Infantry_p2_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Infantry_p2_idle_right": returnValue = GameObject.Instantiate(Infantry_p2_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			
			//  Infantry Bayonet
			case "InfantryBayonet_p1_idle_left": returnValue = GameObject.Instantiate(InfantryBayonet_p1_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "InfantryBayonet_p1_idle_right": returnValue = GameObject.Instantiate(InfantryBayonet_p1_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "InfantryBayonet_p2_idle_left": returnValue = GameObject.Instantiate(InfantryBayonet_p2_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "InfantryBayonet_p2_idle_right": returnValue = GameObject.Instantiate(InfantryBayonet_p2_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			
			//  Cavalry
			case "Cavalry_p1_idle_left": returnValue = GameObject.Instantiate(Cavalry_p1_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Cavalry_p1_idle_right": returnValue = GameObject.Instantiate(Cavalry_p1_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Cavalry_p2_idle_left": returnValue = GameObject.Instantiate(Cavalry_p2_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Cavalry_p2_idle_right": returnValue = GameObject.Instantiate(Cavalry_p2_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			
			//  Skirmisher
			case "Skirmisher_p1_idle_left": returnValue = GameObject.Instantiate(Skirmisher_p1_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Skirmisher_p1_idle_right": returnValue = GameObject.Instantiate(Skirmisher_p1_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Skirmisher_p2_idle_left": returnValue = GameObject.Instantiate(Skirmisher_p2_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Skirmisher_p2_idle_right": returnValue = GameObject.Instantiate(Skirmisher_p2_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			
			//  Artillery
			case "Artillery_p1_idle_left": returnValue = GameObject.Instantiate(Artillery_p1_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Artillery_p1_idle_right": returnValue = GameObject.Instantiate(Artillery_p1_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Artillery_p2_idle_left": returnValue = GameObject.Instantiate(Artillery_p2_idle_left, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "Artillery_p2_idle_right": returnValue = GameObject.Instantiate(Artillery_p2_idle_right, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			#region "Default"
			default:returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;break;
			#endregion
		}

		return returnValue;
	}
	#endregion
}

