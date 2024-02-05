using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_DB_UnitShop : MonoBehaviour {
	public static MG_DB_UnitShop I;
	public void Awake(){ I = this; }
	
	public Sprite img_dummy, img_test;
	
	public List<string> catalog;
	public List<int> catalog_cost, catalog_cost2;
	
	public void start (){
		catalog = new List<string>();
		catalog_cost = new List<int>();
		catalog_cost2 = new List<int>();
	}
	
	public Sprite get_port (string portName) {
		Sprite retVal = img_dummy;
		switch (portName) {
			case "test": retVal = img_test; break;
		}
		
		return retVal;
	}
	
	public void set_catalog (int catalogIndex) {
		catalog.Clear();
		
		switch (catalogIndex) {
			case 1:
				// Infantry - USA
				catalog.Add ("RifleInfantry");
				catalog.Add ("AssaultInfantry");
				catalog.Add ("AntiTankInfantry");
				
				catalog_cost.Add (100);
				catalog_cost.Add (150);
				catalog_cost.Add (200);
				
				catalog_cost2.Add (8);
				catalog_cost2.Add (8);
				catalog_cost2.Add (8);
			break;
		}
	}
}