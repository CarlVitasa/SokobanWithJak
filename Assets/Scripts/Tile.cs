using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	public static bool IsMovable ( TileTypes type ) {

		switch ( type ) {

			case TileTypes.Player:
				return true;
			case TileTypes.Wall:
				return false;
			case TileTypes.Floor:
				return false;
			default:
				return false;
		}
	}

	public static bool IsPowerable ( TileTypes tile ) {

		switch ( tile ) {

			case TileTypes.Player:
				return false;
			case TileTypes.Wall:
				return false;
			case TileTypes.Floor:
				return false;
			default:
				return false;
		}
	}
}
public enum TileTypes {
	Empty,
	Movable,
	Immovable,
	Invalid = -1,
	Player,
	Block,
	Floor,
	Wall,
}

