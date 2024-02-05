using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Tools : MonoBehaviour {
	public static MG_Tools I;
	public void Awake(){ I = this; }

	#region "STRING MODIFY - First Letter To Upper"
	public string FirstLetterToUpper(string str) {
		if (str == null)
			return null;

		if (str.Length > 1)
			return char.ToUpper(str[0]) + str.Substring(1);

		return str.ToUpper();
	}
	#endregion
}
