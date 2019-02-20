using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {

	public struct HitInfo {

		public TileTypes TileType;
		public bool Powered;

		public HitInfo ( TileTypes tile, bool powered ) {

			TileType = tile;
			Powered = powered;
		}
	}

	private const float RAYCAST_DISTANCE = 1f;

	public HitInfo GetTile ( Directions direction ) {

		Vector3 castDirection = Util.DirectionToVector( direction );
		RaycastHit hit;
		HitInfo defaultHitInfo = new HitInfo( TileTypes.Empty, false );

		if ( Physics.Raycast( transform.position, transform.TransformDirection( castDirection ), out hit, RAYCAST_DISTANCE ) ) {

			var hitObj = hit.transform.gameObject;
			var moveable = hitObj.GetComponent<Moveable>();
			var powerable = hitObj.GetComponent<Powerable>();

			TileTypes tileType;
			bool powered;

			// powered
			if ( powerable != null ) {
				powered = powerable.Powered;
			} else {
				powered = false;
			}

			// tile type
			if ( moveable != null ) {
				tileType = TileTypes.Movable;
			} else {
				tileType = TileTypes.Immovable;
			}

			return new HitInfo( tileType, powered );
		}
		return defaultHitInfo;
	}
}

