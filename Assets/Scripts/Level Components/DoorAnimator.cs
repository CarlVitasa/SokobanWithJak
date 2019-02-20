using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Door ) )]
public class DoorAnimator : MonoBehaviour {

	[SerializeField] private AnimationCurve _animationCurve;
	[SerializeField] private float _tweenTime = 0.25f;
	[SerializeField] private Vector3 _openPosition;

	private Door _door;
	private Vector3 _closedPosition;
	private Tween _movementTween;

	void Awake () {

		_closedPosition = transform.position;

		_door = GetComponent<Door>();
		_door.OnOpen += Open;
		_door.OnClose += Close;
	}

	public void Open () {

		_movementTween?.Kill();

		_movementTween = Tween.Vector3(
			input => transform.position = input,
			transform.position,
			transform.position + _openPosition,
			_tweenTime
		)
		.Curve( _animationCurve );
	}

	public void Close () {

		_movementTween?.Kill();

		_movementTween = Tween.Vector3(
			input => transform.position = input,
			transform.position,
			_closedPosition,
			_tweenTime
		)
		.Curve( _animationCurve );
	}
}
