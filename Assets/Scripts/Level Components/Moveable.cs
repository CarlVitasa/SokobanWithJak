using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent( typeof( Scanner ) )]
public class Moveable : MonoBehaviour {


	// ********** Public Interface **********

	public delegate void OnMoveEvent ( Directions dir );
	public event OnMoveEvent OnMoveSuccess;
	public event OnMoveEvent OnPushSuccess;
	public event OnMoveEvent OnMoveFail;

	public void Move ( Directions dir ) {

		if ( !CanAnimate() ) {
			return;
		}

		bool canMove;
		var tileInfo = _scanner.GetTile( dir );

		var stickable = GetComponent<Stickable>();
		if ( stickable == null ) {
			canMove = CanMove( dir );
			if ( CanMove( dir ) ) {
				MovePosition( dir );
			}
		} else {
			canMove = CanMoveGroup( dir );
			if ( CanMoveGroup( dir ) ) {
				MovePositionGroup( dir );
			}
		}

		// Move normally
		if ( canMove && tileInfo.TileType == TileTypes.Empty ) {
			if ( OnMoveSuccess != null ) {
				OnMoveSuccess( dir );
			}
		}

		// Push moveable object
		if ( canMove && tileInfo.TileType == TileTypes.Movable ) {
			if ( OnPushSuccess != null ) {
				OnPushSuccess( dir );
			}
		}

		// Push against wall
		if ( !canMove ) {
			// couldn't move
			if ( OnMoveFail != null ) {
				OnMoveFail( dir );
			}
		}
	}


	// ********** Private Interface **********

	// internal data types
	private struct TileInfo {

		public bool Valid;
		public TileTypes TileType;

		public TileInfo ( bool valid, TileTypes tile ) {
			Valid = valid;
			TileType = tile;
		}
	}

	[Header( "Movable Properties" )]
	[SerializeField] private AnimationCurve _animationCurve;
	[SerializeField] private float _tweenTime = 0.15f;

	// consts
	private const float RAYCAST_DISTANCE = 1f;

	// data
	private Tween _movementTween;

	private Scanner _scanner;

	// logic
	private void Awake () {

		_scanner = GetComponent<Scanner>();
	}

	private bool CanAnimate () {

		return _movementTween == null;
	}

	private bool CanMove ( Directions direction ) {

		bool canMove = false;
		Vector3 castDirection = Util.DirectionToVector( direction );
		RaycastHit hit;
		var stickable = GetComponent<Stickable>();

		// is a hit detected
		if ( Physics.Raycast( transform.position, transform.TransformDirection( castDirection ), out hit, RAYCAST_DISTANCE ) ) {
			var neighborMoveable = hit.transform.gameObject.GetComponent<Moveable>();
			var neighborStickable = hit.transform.gameObject.GetComponent<Stickable>();

			// Am I not Stickable
			if ( stickable == null ) {
				// did I hit a stickable
				if ( neighborStickable != null ) {
					canMove = neighborMoveable.CanMoveGroup( direction );
					return canMove;
				}
			}

			// is there a Moveable component on the object I hit
			if ( neighborMoveable != null ) {
				// can the object I hit move in the same direction I'm going
				var neighborCanMove = neighborMoveable.CanMove( direction );
				canMove = neighborCanMove;
			}
		} else {
			canMove = true;
		}
		return canMove;
	}

	private void MovePosition ( Directions direction ) {

		var dirVector = Util.DirectionToVector( direction );
		var destination = dirVector * Util.GRID_UNITS;

		if ( _movementTween == null ) {
			_movementTween = Tween.Vector3(
				input => transform.position = input,
				transform.position,
				transform.position + destination,
				_tweenTime
			)
			.Curve( _animationCurve )
			.OnComplete( () => _movementTween = null );
		}

		RaycastHit hit;
		if ( Physics.Raycast( transform.position, transform.TransformDirection( dirVector ), out hit, RAYCAST_DISTANCE ) ) {

			var moveable = hit.transform.gameObject.GetComponent<Moveable>();
			moveable?.Move( direction );
		}
	}

	private bool CanMoveGroup ( Directions direction ) {

		bool canMoveGroup = true;
		Vector3 castDirection = Util.DirectionToVector( direction );
		var stickable = GetComponent<Stickable>();
		var stickables = stickable.GetStickableChain();

		if ( stickable != null ) {
			foreach ( Stickable s in stickables ) {
				var stickableCanMove = s.GetComponent<Moveable>().CanMove( direction );
				if ( !stickableCanMove ) {
					canMoveGroup = false;
					break;
				}
			}
		}
		return canMoveGroup;
	}

	private void MovePositionGroup ( Directions direction ) {

		var stickable = GetComponent<Stickable>();
		var stickables = stickable.GetStickableChain();
		foreach ( Stickable s in stickables ) {
			var m = s.GetComponent<Moveable>();
			m.MovePosition( direction );
		}
	}
}
