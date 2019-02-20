using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Moveable ) )]
public class MockAnimator : MonoBehaviour {

	private Moveable _moveable;

	private void Awake () {

		_moveable = GetComponent<Moveable>();

		_moveable.OnMoveSuccess += MoveSuccess;
		_moveable.OnPushSuccess += PushSuccess;
		_moveable.OnMoveFail += MoveFail;
	}

	private void OnDestroy () {

		_moveable.OnMoveSuccess -= MoveSuccess;
		_moveable.OnPushSuccess -= PushSuccess;
		_moveable.OnMoveFail -= MoveFail;
	}

	private void MoveSuccess ( Directions dir ) {
		// Debug.Log( $"{name} move success {dir.ToString()}" );
	}

	private void PushSuccess ( Directions dir ) {
		// Debug.Log( $"{name} push success {dir.ToString()}" );
	}

	private void MoveFail ( Directions dir ) {
		// Debug.Log( $"{name} move fail {dir.ToString()}" );
	}
}
