using UnityEngine;
using System;
using System.Collections;

//Used to synchronize samples
public class AudioSyncher : MonoBehaviour
{
	public static event Action<double> OnBeat;
	//Time buffer to give the audio engine some room
	const double TIME_BUFFER = 0.05;


	public int BPM;

	double next;
	double now;
	double secondsPerBeat;

	// Use this for initialization
	void Start () {
		//Beats are in a 1/16th resolution
		secondsPerBeat = 60.0 / (BPM * 16); 
		now = AudioSettings.dspTime;
		next = now + secondsPerBeat;
	}

	// Update is called once per frame
	void Update () {
		now = AudioSettings.dspTime;
		if (now + TIME_BUFFER >= next) {
			NextBeat();
		}
	}

	void NextBeat() {
		next += secondsPerBeat;
		if(OnBeat != null)
			OnBeat(next - now);
	}
}

