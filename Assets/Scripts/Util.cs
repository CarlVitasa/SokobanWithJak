using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

	public static float GRID_UNITS = 1;

	public static Vector3 DirectionToVector ( Directions dir ) {

		switch ( dir ) {
			case Directions.Up:
				return Vector3.forward;
			case Directions.Down:
				return Vector3.back;
			case Directions.Left:
				return Vector3.left;
			case Directions.Right:
				return Vector3.right;
			default:
				return new Vector3( 0, 0, 0 );
		}
	}
}

public enum Directions {
	Up,
	Down,
	Left,
	Right
}

