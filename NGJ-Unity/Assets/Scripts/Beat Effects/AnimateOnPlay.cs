using UnityEngine;
using System.Collections;

[RequireComponent( typeof(Animation) )]
public class AnimateOnPlay : OnPlayBase {
	Animation anim;
	protected override void OnStart () {
		anim = GetComponent<Animation> ();
	}

	protected override void OnPlay() {
		anim.Rewind ();
		anim.Play ();
	}
}
