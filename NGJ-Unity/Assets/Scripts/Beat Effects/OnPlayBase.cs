using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SamplePlayer))]
public class OnPlayBase : MonoBehaviour {
	SamplePlayer player;
	void Start () {
		player = GetComponent<SamplePlayer> ();
		player.OnPlay += OnPlay;
		OnStart ();
	}

	protected virtual void OnStart() {
		
	}

	protected virtual void OnPlay() {
		
	}
}
