using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {


	// ********** Public **********

	public bool IsActivated { get; private set; }

	public delegate void SensorEvent ();
	public event SensorEvent OnActivated;
	public event SensorEvent OnDeactivated;


	// ********** Private **********

	private float _startingHeight = 2.0f;
	private bool _objectDetectedLastFrame;

	private bool __objectDetected;
	private bool _objectDetected {
		get {
			return __objectDetected;
		}
		set {
			if ( __objectDetected != value ) {
				if ( value ) {
					IsActivated = true;
					if ( OnActivated != null ) {
						OnActivated();
					}
				} else {
					IsActivated = false;
					if ( OnDeactivated != null ) {
						OnDeactivated();
					}
				}
				__objectDetected = value;
			}
		}
	}

	[SerializeField] private LayerMask _activators;

	private void Update () {

		ScanForObject();
	}

	private void ScanForObject () {

		_objectDetected = ObjectDetected();
	}

	private bool ObjectDetected () {

		var dir = transform.TransformDirection( Vector3.down );
		Vector3 startPoint = transform.position + ( Vector3.up * _startingHeight );
		RaycastHit hit;

		if ( Physics.Raycast( startPoint, dir, out hit, _startingHeight, _activators ) ) {
			return true;
		}
		return false;
	}
}
