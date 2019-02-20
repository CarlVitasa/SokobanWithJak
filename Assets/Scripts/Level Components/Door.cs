using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * what is the public interface of door -> what are the things other people should know about the door e.g. open
 * what private functions are required
 * hook up door animator to door
 */

public class Door : MonoBehaviour {

	// ********** Public **********

	public delegate void DoorEvent ();
	public event DoorEvent OnOpen;
	public event DoorEvent OnClose;


	// ********** private **********

	private enum ActivationConditions {
		One,
		All
	}

	[Header( "Door Properties" )]
	[SerializeField] private ActivationConditions _activationConditions;
	[SerializeField] private List<Sensor> _sensors;

	private bool _wasOpen;

	private void Update () {

		bool isOpen;

		switch ( _activationConditions ) {

			case ActivationConditions.One:
				isOpen = IsOneSensorOn();
				break;

			case ActivationConditions.All:
				isOpen = AreAllSensorsOn();
				break;

			default:
				isOpen = false;
				break;
		}

		if ( !_wasOpen && isOpen ) {
			Open();
		} else if ( _wasOpen && !isOpen ) {
			Close();
		}

		_wasOpen = isOpen;
	}

	private bool AreAllSensorsOn () {

		foreach ( Sensor sensor in _sensors ) {
			if ( !sensor.IsActivated ) {
				return false;
			}
		}
		return true;
	}
	private bool IsOneSensorOn () {

		foreach ( Sensor sensor in _sensors ) {
			if ( sensor.IsActivated ) {
				return true;
			}
		}
		return false;
	}

	private void Open () {
		if ( OnOpen != null ) {
			OnOpen();
		}
	}
	private void Close () {
		if ( OnClose != null ) {
			OnClose();
		}
	}
}
