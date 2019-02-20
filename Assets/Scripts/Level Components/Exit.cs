using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Sensor ) )]
public class Exit : MonoBehaviour {

	private Sensor _sensor;

	private void Awake () {

		_sensor = GetComponent<Sensor>();
		_sensor.OnActivated += Activated;
	}

	private void Activated () {

		Debug.Log( "Level Complete" );
	}
}
