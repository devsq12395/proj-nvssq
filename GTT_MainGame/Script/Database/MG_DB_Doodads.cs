using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_DB_Doodads : MonoBehaviour {
	public static MG_DB_Doodads I;
	public void Awake(){ I = this; }

	public GameObject dummy;
	public GameObject treeSummer_001, treeSummer_002;
	public GameObject treeWinter_001, treeWinter_002;
	public GameObject treeSwamp_001, treeSwamp_002;
	public GameObject grassSummer_001, grassSummer_002, grassSummer_003, flowerSummer_001, flowerSummer_002;
	public GameObject grassWinter_001, grassWinter_002, grassWinter_003, flowerWinter_001, flowerWinter_002;
	public GameObject grassSwamp_001, grassSwamp_002, grassSwamp_003;

	public GameObject dood_carFront, dood_lampPost, dood_lampPost2;
	public GameObject dood_bed, dood_box, dood_box2, dood_cabinet, dood_chair, dood_painting, dood_pot, dood_pot2, dood_pottedPlant, dood_table, dood_window, dood_crimsonGate1, dood_crimsonGate2, dood_crimsonGate3, dood_crimsonGate4,
		dood_swampProp1, dood_swampProp2, dood_swampProp3, dood_swampProp4;

	// Values
	public float xOffset, yOffset, armorBon;
	public bool isBlocker, isSightBlocker;

	public GameObject _getSprite(string newSpriteName){
		GameObject returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
		Destroy(returnValue);
		switch (newSpriteName) {
			case "treeSummer_001": returnValue = GameObject.Instantiate(treeSummer_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "treeSummer_002": returnValue = GameObject.Instantiate(treeSummer_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "treeWinter_001": returnValue = GameObject.Instantiate(treeWinter_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "treeWinter_002": returnValue = GameObject.Instantiate(treeWinter_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "treeSwamp_001": returnValue = GameObject.Instantiate(treeSwamp_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "treeSwamp_002": returnValue = GameObject.Instantiate(treeSwamp_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			case "grassSummer_001": returnValue = GameObject.Instantiate(grassSummer_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassSummer_002": returnValue = GameObject.Instantiate(grassSummer_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassSummer_003": returnValue = GameObject.Instantiate(grassSwamp_003, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassSwamp_003": returnValue = GameObject.Instantiate(grassSummer_003, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassSwamp_001": returnValue = GameObject.Instantiate(grassSwamp_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassSwamp_002": returnValue = GameObject.Instantiate(grassSwamp_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "flowerSummer_001": returnValue = GameObject.Instantiate(flowerSummer_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "flowerSummer_002": returnValue = GameObject.Instantiate(flowerSummer_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassWinter_001": returnValue = GameObject.Instantiate(grassWinter_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassWinter_002": returnValue = GameObject.Instantiate(grassWinter_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "grassWinter_003": returnValue = GameObject.Instantiate(grassWinter_003, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "flowerWinter_001": returnValue = GameObject.Instantiate(flowerWinter_001, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "flowerWinter_002": returnValue = GameObject.Instantiate(flowerWinter_002, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			case "dood_carFront": returnValue = GameObject.Instantiate(dood_carFront, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_lampPost": returnValue = GameObject.Instantiate(dood_lampPost, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_lampPost2": returnValue = GameObject.Instantiate(dood_lampPost2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_bed": returnValue = GameObject.Instantiate(dood_bed, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_box": returnValue = GameObject.Instantiate(dood_box, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_box2": returnValue = GameObject.Instantiate(dood_box2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_cabinet": returnValue = GameObject.Instantiate(dood_cabinet, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_chair": returnValue = GameObject.Instantiate(dood_chair, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_painting": returnValue = GameObject.Instantiate(dood_painting, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_pot": returnValue = GameObject.Instantiate(dood_pot, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_pot2": returnValue = GameObject.Instantiate(dood_pot2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_pottedPlant": returnValue = GameObject.Instantiate(dood_pottedPlant, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_table": returnValue = GameObject.Instantiate(dood_table, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_window": returnValue = GameObject.Instantiate(dood_window, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_swampProp1": returnValue = GameObject.Instantiate(dood_swampProp1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_swampProp2": returnValue = GameObject.Instantiate(dood_swampProp2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_swampProp3": returnValue = GameObject.Instantiate(dood_swampProp3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_swampProp4": returnValue = GameObject.Instantiate(dood_swampProp4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			case "dood_crimsonGate1": returnValue = GameObject.Instantiate(dood_crimsonGate1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_crimsonGate2": returnValue = GameObject.Instantiate(dood_crimsonGate2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_crimsonGate3": returnValue = GameObject.Instantiate(dood_crimsonGate3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
			case "dood_crimsonGate4": returnValue = GameObject.Instantiate(dood_crimsonGate4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;

			default: returnValue = GameObject.Instantiate(dummy, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
		}

		return returnValue;
	}

	public void _setValues(string doodType){
		#region "Defaults"
		isBlocker = false;
		isSightBlocker = false;
		armorBon = 1;
		xOffset = 0;
		yOffset = 0;
		#endregion
		
		switch (doodType) {
			#region "Proj NVS Terrains"
			case "forest":
				armorBon = 0.9f;
			break;
			case "hills":
				armorBon = 0.8f;
			break;
			case "water":
				armorBon = 1.1f;
			break;
			case "building":
				armorBon = 0.85f;
			break;
			#endregion
			
			#region "treeSummer_001, treeSummer_002"
			case "treeSummer_001":case "treeSummer_002":case "treeSwamp_001":case "treeSwamp_002":
				/*offset*/
				xOffset = 0;
				yOffset = 0.15f;
				isBlocker = true;
				isSightBlocker = true;
			break;
			#endregion
			#region "BLOCKER Collision doodads"
			case "dood_lampPost":case "dood_lampPost2":case "dood_carFront":
				/*offset*/
				isBlocker = true;
			break;
			#endregion
			#region "treeWinter"
			case "treeWinter_001":case "treeWinter_002":
				/*offset*/
				xOffset = 0;
				yOffset = 0.15f;
				isBlocker = true;
				isSightBlocker = true;
			break;
			#endregion
			#region "pathBlocker"
			case "pathBlocker":
				/*offset*/
				xOffset = 0;
				yOffset = 0;
				isBlocker = true;
			break;
			#endregion
			#region "pathSightBlocker"
			case "pathSightBlocker":
				/*offset*/
				xOffset = 0;
				yOffset = 0;
				isBlocker = true;
				isSightBlocker = true;
			break;
			#endregion
		}
	}
}
