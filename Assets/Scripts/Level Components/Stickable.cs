using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Scanner ) )]
[RequireComponent( typeof( Powerable ) )]
public class Stickable : MonoBehaviour {


	// Public interface

	public List<T> GetStickableChainAs<T> () where T : MonoBehaviour {

		var list = new List<T>();
		var sticakbles = GetStickableChain();
		foreach ( Stickable s in sticakbles ) {
			var component = s.GetComponent<T>();
			if ( component != null ) {
				list.Add( component );
			}
		}
		return list;
	}
	public List<Stickable> GetStickableChain () {

		return ProcessChain();
	}


	// Private

	private const float RAYCAST_DISTANCE = 1;

	private List<Stickable> ProcessChain ( List<Stickable> chain = null ) {

		if ( chain == null ) {
			chain = new List<Stickable>();
		}

		// add this component to chain
		chain.Add( this );

		// check each direction for stickables
		var directions = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
		foreach ( Vector3 direction in directions ) {
			var s = CheckStickable( direction );
			if ( s != null && !chain.Contains( s ) ) {
				// add new stickable to chain
				s.ProcessChain( chain );
			}
		}

		return chain;
	}
	private Stickable CheckStickable ( Vector3 direction ) {

		RaycastHit hit;
		Stickable stickable = null;
		if ( Physics.Raycast( transform.position, transform.TransformDirection( direction ), out hit, RAYCAST_DISTANCE ) ) {
			stickable = hit.transform.gameObject.GetComponent<Stickable>();
		}
		return stickable;
	}
}
