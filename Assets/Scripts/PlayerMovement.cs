using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Moveable ) )]
public class PlayerMovement : MonoBehaviour {
	[SerializeField] private Moveable _moveable;

	void Update () {
		MovePlayer();
	}

	// TODO: Player input manager using events in preperation for pause menu
	void MovePlayer () {

		if ( Input.GetKey( KeyCode.UpArrow ) ) {
			_moveable.Move( Directions.Up );
		}

		if ( Input.GetKey( KeyCode.DownArrow ) ) {
			_moveable.Move( Directions.Down );
		}

		if ( Input.GetKey( KeyCode.LeftArrow ) ) {
			_moveable.Move( Directions.Left );
		}

		if ( Input.GetKey( KeyCode.RightArrow ) ) {
			_moveable.Move( Directions.Right );
		}
	}
}
