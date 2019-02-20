using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	private enum TileType {
		Invalid = -1,
		Wall,
		Floor
	}

	[SerializeField] private GameObject _wallObj;
	[SerializeField] private float _spacing = 1f;

	// public TileInfo GetInfoForTile( int x, int y )
	// public TileInfo GetInfoForTile( TileInfo tile, Direction direction )

	private char[,] _data = new char[,] {
		{ 'x','x','x','x','x' },
		{ 'x','o','o','o','x' },
		{ 'x','o','p','o','o' },
		{ 'x','o','o','o','x' },
		{ 'x','o','o','o','x' },
		{ 'x','x','x','x','x' }
	};

	void Awake () {

		SpawnGrid();
	}

	void SpawnGrid () {

		// what does it mean to spawn a grid
		// I want tiles at a certain position based on input data

		// read from `data` at coordinate
		// get prefab for above
		// spawn at some world location

		for ( int col = 0; col < _data.GetLength( 1 ); col++ ) {
			for ( int row = 0; row < _data.GetLength( 0 ); row++ ) {
				var tileType = ReadData( _data, row, col );
				var tile = GetObject( tileType );
				PlaceTile( tile, row, col );
			}
		}

		// for ( int x = 0; x < _data.GetLength( 1 ); x++ ) {
		// 	for ( int z = 0; z < _data.GetLength( 0 ); z++ ) {
		// 		var c = _data[z, x];
		// 		if ( c == 'x' ) {
		// 			GameObject.Instantiate( _wallObj, new Vector3(
		// 				transform.position.x + x,
		// 				0,
		// 				transform.position.z - z ),
		// 			Quaternion.identity );
		// 		}
		// 	}
		// }
	}

	private TileType ReadData ( char[,] data, int row, int column ) {
		switch ( data[row, column] ) {
			case 'x':
				return TileType.Wall;
			case 'o':
				return TileType.Floor;
			default:
				return TileType.Invalid;
		}
	}

	private GameObject GetObject ( TileType tileType ) {
		switch ( tileType ) {
			case TileType.Wall:
				return Instantiate( _wallObj );
			default:
				return null;
		}
	}

	private void PlaceTile ( GameObject tile, int row, int column ) {

		if ( tile == null ) {
			return;
		}

		tile.transform.position = GetPosition( row, column );
	}

	private Vector3 GetPosition ( int row, int column ) {

		return new Vector3( column * _spacing + Random.Range( 0.0f, 0.1f ), 0f, -row * _spacing + Random.Range( 0.0f, 0.1f ) );
	}
}

// generate cubes based on grid
// serialize field type of blocks

// determine colors based on characters