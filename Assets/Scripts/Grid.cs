using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	[SerializeField] private GameObject _wallObj;
	[SerializeField] private GameObject _playerObj;
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

	private Tile tile;

	void Awake () {
		SpawnGrid();
	}

	void SpawnGrid () {

		// what does it mean to spawn a grid
		// I want tiles at a certain position based on input data

		// read from `data` at coordinate
		// get prefab for above
		// spawn at some world location
		for ( int row = 0; row < _data.GetLength( 0 ); row++ ) {
			for ( int col = 0; col < _data.GetLength( 1 ); col++ ) {
				var tileType = ReadData( _data, row, col );
				var tile = GetObject( tileType );
				PlaceTile( tile, row, col );
				// Debug.Log( TileAtDirection( data, 2, 3, Directions.Left ) );
			}
		}
	}
	private TileTypes ReadData ( char[,] data, int row, int column ) {

		switch ( data[row, column] ) {
			case 'p':
				return TileTypes.Player;
			case 'x':
				return TileTypes.Wall;
			case 'o':
				return TileTypes.Floor;
			default:
				return TileTypes.Invalid;
		}
	}

	private GameObject GetObject ( TileTypes tileType ) {

		switch ( tileType ) {
			case TileTypes.Wall:
				return Instantiate( _wallObj );
			case TileTypes.Player:
				return Instantiate( _playerObj );
			default:
				return null;
		}
	}

	private Vector3 GetPosition ( int row, int column ) {

		return new Vector3( column * ( _spacing + 1.0f ), 0f, -row * ( _spacing + 1.0f ) );
		// return new Vector3( column * _spacing + Random.Range( 0.0f, 0.1f ), 0f, -row * _spacing + Random.Range( 0.0f, 0.1f ) );
	}

	private void PlaceTile ( GameObject tile, int row, int column ) {

		if ( tile == null ) {
			return;
		}

		tile.transform.position = GetPosition( row, column );
	}



	public TileTypes TileAtDirection ( int row, int col, Directions dir ) {

		TileTypes tile = TileTypes.Floor;

		switch ( dir ) {
			case Directions.Up:
				tile = ReadData( _data, row - 1, col );
				break;
			case Directions.Down:
				tile = ReadData( _data, row + 1, col );
				break;
			case Directions.Left:
				tile = ReadData( _data, row, col - 1 );
				break;
			case Directions.Right:
				tile = ReadData( _data, row, col + 1 );
				break;
		}
		return tile;
	}
}

