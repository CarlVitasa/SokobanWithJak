using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Collections;

public class Tweening : MonoBehaviour {

	public static Tweening New () {

		var go = new GameObject( "Tweening" );
		var tweening = go.AddComponent<Tweening>();
		return tweening;
	}
}

public class Tween {


	//******************** Constructors ******************

	private void Reformat ( float maxTime, Action<float> setProgress ) {

		_time = 0f;
		_maxTime = maxTime;

		_loop = _tweenObject.StartCoroutine(

			TweenLoop( setProgress )
		);
	}


	//******************** Public ******************

	private static List<Tween> _pool = new List<Tween>();
	private static List<Tween> _tweens = new List<Tween>();

	private static Tween GetTweenFromPool () {

		var tween = _pool.Count > 0 ? _pool[0] : new Tween();
		_tweens.Add( tween );
		return tween;
	}
	private static void ReturnTweenToPool ( Tween tween ) {

		_tweens.Remove( tween );
		// _pool.Add( tween ); // These currently cannot go back to the pool because there is no way to remove old references
	}


	// Static Constructors
	public static Tween Float ( Action<float> setValue, float startValue, float targetValue, float time ) {

		var tween = GetTweenFromPool();

		tween.Reformat(
			time, progress => setValue(
				Mathf.LerpUnclamped(
					startValue,
					targetValue,
					progress
				)
			)
		);

		return tween;
	}
	public static Tween Vector3 ( Action<Vector3> setValue, Vector3 startValue, Vector3 targetValue, float time ) {

		var tween = GetTweenFromPool();

		tween.Reformat(
			time, progress => setValue(
				UnityEngine.Vector3.LerpUnclamped(
					startValue,
					targetValue,
					progress
				)
			)
		);

		return tween;
	}
	public static Tween Quaternion ( Action<Quaternion> setValue, Quaternion startValue, Quaternion targetValue, float time ) {

		var tween = GetTweenFromPool();

		tween.Reformat(
			time, progress => setValue(
				UnityEngine.Quaternion.SlerpUnclamped(
					startValue,
					targetValue,
					progress
				)
			)
		);

		return tween;
	}


	// Actions
	public void Play () {

		_paused = false;
	}
	public void Pause () {

		_paused = true;
	}
	public void Restart () {

		_time = 0f;
	}
	public void Kill () {

		if ( _loop != null ) {
			_tweenObject.StopCoroutine( _loop );
		}

		_isComplete = true;
	}


	// Properties
	public Tween Curve ( AnimationCurve curve ) {

		_curve = curve;
		return this;
	}
	public Tween OnComplete ( Action onComplete ) {

		_onComplete = onComplete;
		return this;
	}


	public bool IsComplete {
		get { return _isComplete; }
	}
	public bool IsPaused {
		get { return _paused; }
	}
	public float Time {
		get { return _maxTime; }
	}


	//******************** Private ******************


	private Coroutine _loop;
	private Action _onComplete;
	private AnimationCurve _curve;
	private float _time;
	private float _maxTime;
	private bool _paused;
	private bool _isComplete;


	private static Tweening __tweenObject;
	private static Tweening _tweenObject {
		get { if ( __tweenObject == null ) { __tweenObject = Tweening.New(); } return __tweenObject; }
	}


	private IEnumerator TweenLoop ( Action<float> tweenAction ) {

		while ( _time < _maxTime ) {

			// If paused skip
			if ( _paused ) { yield return null; }


			// add to time
			_time += UnityEngine.Time.deltaTime;


			// evalutate from curve
			var progress = _time / _maxTime;
			if ( _curve != null ) { progress = _curve.Evaluate( progress ); }


			// tween the action with progress
			tweenAction( progress );
			yield return null;
		}


		// tween the action with final point
		tweenAction( 1 );


		// on complete
		if ( _onComplete != null ) {
			_onComplete();
		}

		// set is complete
		_isComplete = true;
		ReturnTweenToPool( this );
	}
}
