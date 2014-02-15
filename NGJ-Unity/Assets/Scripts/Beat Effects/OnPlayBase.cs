using UnityEngine;
using System.Collections;

public class OnPlayBase : MonoBehaviour {
	public SamplePlayer Player;
	void Start () {
		Player.OnPlay += OnPlay;
		OnStart ();
	}

	void OnDisable() {
		Player.OnPlay -= OnPlay;
	}

	protected virtual void OnStart() {
		
	}

	protected virtual void OnPlay() {
		
	}
}
