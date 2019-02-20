using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Sensor ) )]
public class PressurePlateAnimator : MonoBehaviour {

	[SerializeField] private string _activationAnimationKey;
	[SerializeField] private string _deactivationAnimationKey;

	private Sensor _sensor;

	private void Awake () {

		_sensor = GetComponent<Sensor>();
		_sensor.OnActivated += Activated;
		_sensor.OnDeactivated += Deactivated;
	}

	private void Activated () {

		// Debug.Log( $"Playing animtion:{_activationAnimationKey}" );
	}
	private void Deactivated () {

		// Debug.Log( $"Playing animtion:{_deactivationAnimationKey}" );
	}
}