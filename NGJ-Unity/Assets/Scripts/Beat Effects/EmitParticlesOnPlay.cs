using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class EmitParticlesOnPlay : OnPlayBase {
	public int EmitCount = 10;
	ParticleSystem system;

	protected override void OnStart ()
	{
		system = GetComponent<ParticleSystem> ();
	}

	protected override void OnPlay ()
	{
		system.Emit (EmitCount);
	}
}
