using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_UIControl_Buffs : MonoBehaviour {
	public static MG_UIControl_Buffs I;
	public void Awake(){ I = this; }

	private int MAX_NUMBER_OF_BUFFS_SHOWN;

	public List<Image> i_buffs;
	public List<Text> t_buffStack;

	public void _start(){
		MAX_NUMBER_OF_BUFFS_SHOWN = 10;

		i_buffs = new List<Image> ();

		for (int i = 1; i <= MAX_NUMBER_OF_BUFFS_SHOWN; i++) {
			i_buffs.Add (GameObject.Find (((i < MAX_NUMBER_OF_BUFFS_SHOWN) ? "I_UData_Buff0" : "I_UData_Buff") + i.ToString ()).GetComponent<Image> ());
			t_buffStack.Add (GameObject.Find (((i < MAX_NUMBER_OF_BUFFS_SHOWN) ? "T_UData_BuffStack_0" : "T_UData_BuffStack_") + i.ToString ()).GetComponent<Text> ());
		}
	}

	public void _updateBuffs(int unitId = -1){
		bool hasUnit = MG_GetUnit.I._checkIfUnitWithIDExists(unitId);

		if (hasUnit) {
			MG_ClassUnit unit = MG_GetUnit.I._getUnitWithID (unitId);
			int buffCount = unit.buffs.Count;

			for (int i = 0; i < MAX_NUMBER_OF_BUFFS_SHOWN; i++) {
				if (i < buffCount) {
					i_buffs[i].sprite = MG_DB_Buffs.I._getSprite (unit.buffs[i].type);
					if (unit.buffs [i].stackable) {
						t_buffStack [i].text = unit.buffs [i].stack.ToString();
					} else {
						t_buffStack [i].text = "";
					}
				} else {
					i_buffs[i].sprite = MG_DB_Buffs.I._getSprite ("none");
					t_buffStack [i].text = "";
				}
			}
		} else {
			foreach (Image iL in i_buffs) {
				iL.sprite = MG_DB_Buffs.I._getSprite ("none");
			}
			foreach (Text tB in t_buffStack) {
				tB.text = "";
			}
		}
	}
}
