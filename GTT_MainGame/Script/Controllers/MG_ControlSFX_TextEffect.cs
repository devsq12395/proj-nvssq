using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;

public class MG_ControlSFX_TextEffect : MonoBehaviour {
	public static MG_ControlSFX_TextEffect I;
	public void Awake(){ I = this; }

	public float yInc;
	public int tweenNum = 0;
	public GameObject ct_parent, un_parent;
	
	public List<MG_ClassSFX> unitNums;
	
	public void init(){
		unitNums = new List<MG_ClassSFX> ();
	}
	
	public void create_unit_num_for_all_units (){
		if (un_parent != null) {
			foreach(Transform child in un_parent.transform){
				if(MG_GetSFX.I._getSFXfromGameObject(child.gameObject) != null){
					MG_ControlSFX.I._addToDestroyList(MG_GetSFX.I._getSFXfromGameObject(child.gameObject));
				}
			}
		}
		Destroy (un_parent);
		un_parent = null;
		unitNums = new List<MG_ClassSFX> ();
		
		un_parent = GameObject.Instantiate (ct_parent, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject; 
		
		/*foreach (MG_ClassUnit _unit in MG_Globals.I.units) {
			if (_unit.isAlive) {
				create_unit_num (_unit);
			}
		}
		foreach (MG_ClassUnit _unit in MG_Globals.I.unitsTemp) {
			if (_unit.isAlive) {
				create_unit_num (_unit);
			}
		}*/
	}
	
	public void create_unit_num(MG_ClassUnit _unit){
		foreach(MG_ClassSFX _go in _unit.hpTexts){
			MG_ControlSFX.I._addToDestroyList(_go);
		}
		_unit.hpTexts = new List<MG_ClassSFX> ();
		
		if (!_unit.showHealthBar) return;
		
		float posX = _unit.sprite.transform.position.x - 0.15f;
		float posY = _unit.sprite.transform.position.y + 0.3f;
		
		bool hasValue_10000 = false;
		bool hasValue_1000 = false;
		bool hasValue_100 = false;
		bool hasValue_10 = false; 
		bool hasValue_1 = false;
		_processValue(_unit.HP);
		
		MG_ClassSFX _sfx = null;

		if(value_10000 > 0){
			hasValue_10000 = true;
			_sfx = _createSFX("b" + value_10000.ToString(), posX, posY, null);
			_unit.hpTexts.Add (_sfx);
			posX += 0.1f;
		}
		if(value_1000 > 0 || hasValue_10000){
			hasValue_1000 = true;
			_sfx = _createSFX("b" + value_1000.ToString(), posX, posY, null);
			_unit.hpTexts.Add (_sfx);
			posX += 0.1f;
		}
		if(value_100 > 0 || hasValue_1000){
			hasValue_100 = true;
			_sfx = _createSFX("b" + value_100.ToString(), posX, posY, null);
			_unit.hpTexts.Add (_sfx);
			posX += 0.1f;
		}
		if(value_10 > 0 || hasValue_100){
			hasValue_10 = true;
			_sfx = _createSFX("b" + value_10.ToString(), posX, posY, null);
			_unit.hpTexts.Add (_sfx);
			posX += 0.1f;
		}
		if(value_1 > 0 || hasValue_10){
			hasValue_1 = true;
			_sfx = _createSFX("b" + value_1.ToString(), posX, posY, null);
			_unit.hpTexts.Add (_sfx);
			posX += 0.1f;
		}

		yInc += 0f;
		
		unitNums.Add (_sfx);
	}

	public void _createComboIndic(int damageDealt, float posX, float posY, bool isResist = false){
		bool hasValue_10000 = false;
		bool hasValue_1000 = false;
		bool hasValue_100 = false;
		bool hasValue_10 = false;
		bool hasValue_1 = false;
		posY += 0.3f + yInc;
		posX += 0.15f;
		_processValue(damageDealt);

		GameObject parent = GameObject.Instantiate (ct_parent, new Vector3 (posX, posY, 0), Quaternion.identity) as GameObject;

		if(value_10000 > 0){
			hasValue_10000 = true;
			//MG_ControlSFX.I._createSFX_float("o" + value_10000.ToString(), posX, posY);
			_createSFX("o" + value_10000.ToString(), posX, posY, parent);
			posX += 0.25f;
		}
		if(value_1000 > 0 || hasValue_10000){
			hasValue_1000 = true;
			//MG_ControlSFX.I._createSFX_float("o" + value_1000.ToString(), posX, posY);
			_createSFX("o" + value_1000.ToString(), posX, posY, parent);
			posX += 0.25f;
		}
		if(value_100 > 0 || hasValue_1000){
			hasValue_100 = true;
			//MG_ControlSFX.I._createSFX_float("o" + value_100.ToString(), posX, posY);
			_createSFX("o" + value_100.ToString(), posX, posY, parent);
			posX += 0.25f;
		}
		if(value_10 > 0 || hasValue_100){
			hasValue_10 = true;
			//MG_ControlSFX.I._createSFX_float("o" + value_10.ToString(), posX, posY);
			_createSFX("o" + value_10.ToString(), posX, posY, parent);
			posX += 0.25f;
		}
		if(value_1 > 0 || hasValue_10){
			hasValue_1 = true;
			//MG_ControlSFX.I._createSFX_float("o" + value_1.ToString(), posX, posY);
			_createSFX("o" + value_1.ToString(), posX, posY, parent);
			posX += 0.25f;
		}
		if(damageDealt <= 0){
			//MG_ControlSFX.I._createSFX_float("Miss", posX, posY);
			_createSFX((isResist) ? "Resist" : "Miss", posX, posY, parent);
		}

		yInc += 0f;

		// Start SFX Tween
		float 	pPosX = parent.transform.position.x,
				pPosY = parent.transform.position.y;
		Vector3 firstPos = parent.transform.position;
		Vector3 targetPos = new Vector3 (pPosX, pPosY + 0.25f, parent.transform.position.z);
		float duration = 1f;
		tweenNum++;

		gameObject.Tween("sfxMove_sfxName" + tweenNum.ToString(), firstPos, targetPos, duration, TweenScaleFunctions.Linear, (t) =>{
			// progress
			parent.transform.position = t.CurrentValue;
		}, (t) =>{
			// completion
			foreach(Transform child in parent.transform){
				if(MG_GetSFX.I._getSFXfromGameObject(child.gameObject) != null){
					MG_ControlSFX.I._addToDestroyList(MG_GetSFX.I._getSFXfromGameObject(child.gameObject));
				}
			}
			Destroy(parent);
		});
	}

	public MG_ClassSFX _createSFX(string name, float posX, float posY, GameObject parent){
		MG_ClassSFX sfx = MG_ControlSFX.I._createSFX_float(name, posX*2, posY*2);
		if (parent != null)
			sfx.sprite.transform.SetParent (parent.transform);
		
		return sfx;
	}

	public int value_10000, value_1000, value_100, value_10, value_1;
	public int valueCon_10000, valueCon_1000, valueCon_100, valueCon_10, valueCon_1;
	public int valueBon_10000, valueBon_1000, valueBon_100, valueBon_10, valueBon_1;

	public void _processValue(int value){
		if(value >= 90000){ value_10000 = 9; }
		else if(value >= 80000){ value_10000 = 8; }
		else if(value >= 70000){ value_10000 = 7; }
		else if(value >= 60000){ value_10000 = 6; }
		else if(value >= 50000){ value_10000 = 5; }
		else if(value >= 40000){ value_10000 = 4; }
		else if(value >= 30000){ value_10000 = 3; }
		else if(value >= 20000){ value_10000 = 2; }
		else if(value >= 10000){ value_10000 = 1; }
		else{ value_10000 = 0; }

		value -= value_10000 * 10000;

		if(value >= 9000){ value_1000 = 9; }
		else if(value >= 8000){ value_1000 = 8; }
		else if(value >= 7000){ value_1000 = 7; }
		else if(value >= 6000){ value_1000 = 6; }
		else if(value >= 5000){ value_1000 = 5; }
		else if(value >= 4000){ value_1000 = 4; }
		else if(value >= 3000){ value_1000 = 3; }
		else if(value >= 2000){ value_1000 = 2; }
		else if(value >= 1000){ value_1000 = 1; }
		else{ value_1000 = 0; }

		value -= value_1000 * 1000;

		if(value >= 900){ value_100 = 9; }
		else if(value >= 800){ value_100 = 8; }
		else if(value >= 700){ value_100 = 7; }
		else if(value >= 600){ value_100 = 6; }
		else if(value >= 500){ value_100 = 5; }
		else if(value >= 400){ value_100 = 4; }
		else if(value >= 300){ value_100 = 3; }
		else if(value >= 200){ value_100 = 2; }
		else if(value >= 100){ value_100 = 1; }
		else{ value_100 = 0; }

		value -= value_100 * 100;

		if(value >= 90){ value_10 = 9; }
		else if(value >= 80){ value_10 = 8; }
		else if(value >= 70){ value_10 = 7; }
		else if(value >= 60){ value_10 = 6; }
		else if(value >= 50){ value_10 = 5; }
		else if(value >= 40){ value_10 = 4; }
		else if(value >= 30){ value_10 = 3; }
		else if(value >= 20){ value_10 = 2; }
		else if(value >= 10){ value_10 = 1; }
		else{ value_10 = 0; }

		value -= value_10 * 10;

		if(value >= 9){ value_1 = 9; }
		else if(value >= 8){ value_1 = 8; }
		else if(value >= 7){ value_1 = 7; }
		else if(value >= 6){ value_1 = 6; }
		else if(value >= 5){ value_1 = 5; }
		else if(value >= 4){ value_1 = 4; }
		else if(value >= 3){ value_1 = 3; }
		else if(value >= 2){ value_1 = 2; }
		else if(value >= 1){ value_1 = 1; }
		else{ value_1 = 0; }
	}
}
