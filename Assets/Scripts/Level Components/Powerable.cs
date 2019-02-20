using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerStates {
	Off, On
}

public class Powerable : MonoBehaviour {

	// ********** Private **********


	[SerializeField] private bool _powered;

	public bool Powered {
		get {
			return _powered;
		}
		set {
			_powered = value;
		}
	}

}
