using UnityEngine;
using System.Collections;

//Plays a sample in time
[RequireComponent(typeof(AudioSource))]
public class SamplePlayer : MonoBehaviour {
	//The clip that will be played;
	public AudioClip SampleClip;

	//Number of beats after the sample loops
	public int LoopLength;

	public int Offset;

	[Range(0,1)]
	public float Volume = 1;
	public float Pitch = 1;
	
	//To sources, to switch in between as PlayScheduled stops playback.
	AudioSource PrimarySource;
	AudioSource SecondarySource;

	int currentBeat = -1;

	// Use this for initialization
	void Start () {
		AudioSource[] sources = GetComponents<AudioSource> ();
		PrimarySource = sources [0];
		SecondarySource = sources [1];

		PrimarySource.clip = SampleClip;
		SecondarySource.clip = SampleClip;
	}

	void OnEnable() {
		AudioSyncher.OnBeat += OnBeat;
		currentBeat = (-1 + Offset) % LoopLength;
	}

	void OnDisable() {
		AudioSyncher.OnBeat -= OnBeat;
	}

	void OnBeat(double _delay) {
		PrimarySource.volume = SecondarySource.volume = Volume;
		PrimarySource.pitch = SecondarySource.pitch = Pitch;

		currentBeat = (currentBeat + 1) % LoopLength;
		if (currentBeat == 0) {
			SecondarySource.PlayScheduled (_delay);
			AudioSource temp = SecondarySource;
			SecondarySource = PrimarySource;
			PrimarySource = temp;
		}
	}
}
