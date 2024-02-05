using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_CALC_Distance : MonoBehaviour {
	public static MG_CALC_Distance I;
	public void Awake(){ I = this; }

	public int _distUnits(MG_ClassUnit unit1, MG_ClassUnit unit2){
		//Temporary Variables
		int u1_posX = unit1.posX, u2_posX = unit2.posX, u1_posY = unit1.posY, u2_posY = unit2.posY;

		int distY = Mathf.Abs(u1_posY - u2_posY);
		int distX = Mathf.Abs(u1_posX - u2_posX);
		int dist = distX + distY;
		return dist;
	}

	public int _distBetweenPoints(int u1_posX, int u1_posY, int u2_posX, int u2_posY){
		int distY = Mathf.Abs(u1_posY - u2_posY);
		int distX = Mathf.Abs(u1_posX - u2_posX);
		int dist = distX + distY;
		return dist;
	}

	public int _distUnitAndTargeter(MG_ClassUnit unit1, MG_ClassTargeter obj1){
		//Temporary Variables
		int u1_posX = unit1.posX, u2_posX = obj1.posX, u1_posY = unit1.posY, u2_posY = obj1.posY;
		//Pythagorian method
		/*float distX = u1_posX - u2_posX;
		float distY = u1_posY - u2_posY;
		double dist = (double)Mathf.Sqrt(distX * distX + distY * distY);
		return (int)dist;*/
		//Manual distance method
		int distY = Mathf.Abs(u1_posY - u2_posY);
		int distX = Mathf.Abs(u1_posX - u2_posX);
		int dist = distX + distY;
		return dist;
	}
}
