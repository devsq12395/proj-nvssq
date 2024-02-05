using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class EditorMapWriter : MonoBehaviour {

	[MenuItem ("Map Editor/Write Map")]
	static void _writeDoodads(){
		using(StreamWriter sw = new StreamWriter("NewMap.txt")){
			// Write terrain
			// Don't need terrain anymore so this is commented out
			/*MG_ClassTerrain t = MG_Globals.I.terrains[0];
			int tCount = MG_Globals.I.terrains.Count - 1;

			for(int lp = 0; lp <= tCount; lp++){
				t = MG_Globals.I.terrains [lp];

				sw.WriteLine ("MG_ControlTerrain.I._createTerrain((***" + t.spriteName + "***, " + t.posX.ToString () + ", " + t.posY.ToString () + ", ***" + t.type + "***);");
			}*/

			// Write new terrain
			GameObject terrain = GameObject.FindGameObjectWithTag("GridMap");
			sw.WriteLine ("MG_DB_Maps.I._createGridMap(***" + terrain.name + "***);");
			//sw.WriteLine ("MG_ControlTerrain.I._createTerrain((***" + t.spriteName + "***, " + t.posX.ToString () + ", " + t.posY.ToString () + ", ***" + t.type + "***);");
			Debug.Log("Successfully written all terrains!");

			// Write border 1
			GameObject tableTop1 = GameObject.Find("TableTop");
			Vector3 tableTopPos_1 = tableTop1.transform.position;
			Vector3 tabelTopScale_1 = tableTop1.transform.localScale;
			sw.WriteLine ("GameObject.Find(***TableTop***).transform.position = new Vector3(" + tableTopPos_1.x + "f, " + tableTopPos_1.y + "f, " + tableTopPos_1.z + "f);");
			sw.WriteLine ("GameObject.Find(***TableTop***).transform.localScale = new Vector3(" + tabelTopScale_1.x + "f, " + tabelTopScale_1.y + "f, " + tabelTopScale_1.z + "f);");

			// Write border 2
			GameObject tableTop2 = GameObject.Find("TableTop2");
			Vector3 tableTopPos_2 = tableTop2.transform.position;
			Vector3 tabelTopScale_2 = tableTop2.transform.localScale;
			sw.WriteLine ("GameObject.Find(***TableTop2***).transform.position = new Vector3(" + tableTopPos_2.x + "f, " + tableTopPos_2.y + "f, " + tableTopPos_2.z + "f);");
			sw.WriteLine ("GameObject.Find(***TableTop2***).transform.localScale = new Vector3(" + tabelTopScale_2.x + "f, " + tabelTopScale_2.y + "f, " + tabelTopScale_2.z + "f);");

			// Write border 3
			GameObject tableTop3 = GameObject.Find("TableTop3");
			Vector3 tableTopPos_3 = tableTop3.transform.position;
			Vector3 tabelTopScale_3 = tableTop3.transform.localScale;
			sw.WriteLine ("GameObject.Find(***TableTop3***).transform.position = new Vector3(" + tableTopPos_3.x + "f, " + tableTopPos_3.y + "f, " + tableTopPos_3.z + "f);");
			sw.WriteLine ("GameObject.Find(***TableTop3***).transform.localScale = new Vector3(" + tabelTopScale_3.x + "f, " + tabelTopScale_3.y + "f, " + tabelTopScale_3.z + "f);");

			// Write border 4
			GameObject tableTop4 = GameObject.Find("TableTop4");
			Vector3 tableTopPos_4 = tableTop4.transform.position;
			Vector3 tabelTopScale_4 = tableTop4.transform.localScale;
			sw.WriteLine ("GameObject.Find(***TableTop4***).transform.position = new Vector3(" + tableTopPos_4.x + "f, " + tableTopPos_4.y + "f, " + tableTopPos_4.z + "f);");
			sw.WriteLine ("GameObject.Find(***TableTop4***).transform.localScale = new Vector3(" + tabelTopScale_4.x + "f, " + tabelTopScale_4.y + "f, " + tabelTopScale_4.z + "f);");

			Debug.Log("Successfully written borders!");

			// Write units - temporarily removed
			// Add hero points here too, and chosen heroes should not be picked by this system
//			MG_ClassUnit u = MG_Globals.I.units[0];
//			int uCount = MG_Globals.I.units.Count - 1;
//			for(int lp = 0; lp <= uCount; lp++){
//				u = MG_Globals.I.units [lp];
//
//				sw.WriteLine ("MG_ControlUnits.I._createUnit(***" + u.name + "***, " + u.posX.ToString () + ", " + u.posY.ToString () + ");");
//			}
//			Debug.Log("Successfully written all units!");

			// Write doodads
			GameObject parent = GameObject.Find("_DOODADS");
			Transform child;
			int childCount = parent.transform.childCount - 1;
			for(int lp = 0; lp <= childCount; lp++){
				child = parent.transform.GetChild(lp);
				MG_ClassDoodad dood = MG_GetDoodad.I._getDoodadWithGameObject (child.gameObject);

				if (dood == null) continue;

				sw.WriteLine("MG_ControlDoodad.I._createDoodad(***" + child.name + "***, "
					+ dood.posX + ", " + dood.posY + ", " +
					((child.name == "pathBlocker" || child.name == "pathSightBlocker") ?
						("300f, 300f);")
						: ((child.transform.position.x).ToString() + "f, " + (child.transform.position.y).ToString() + "f);")
					)
				);
			}
			Debug.Log("Successfully written all doodads!");
		}
	}
}
