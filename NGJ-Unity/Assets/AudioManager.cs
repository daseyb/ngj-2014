using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LoopType {
	Beat,
	Bass,
	Lead
}

public class AudioManager : MonoBehaviour {
	public AudioClip[] RedLoops;
	public AudioClip[] GreenLoops;
	public AudioClip[] YellowLoops;
	public AudioClip[] BlueLoops;

	public SamplePlayer LeadPlayer;
	public SamplePlayer BassPlayer;
	public SamplePlayer BeatPlayer;

	public Dictionary<GameColor, Dictionary<LoopType, AudioClip>> AudioClips = new Dictionary<GameColor, Dictionary<LoopType, AudioClip>> ();
	public Dictionary<LoopType, SamplePlayer> Players = new Dictionary<LoopType, SamplePlayer> ();

	
	void Start() {
		AudioClips [GameColor.Red] = new Dictionary<LoopType, AudioClip> () 
		{
			{ LoopType.Beat, RedLoops[0] },
			{ LoopType.Bass, RedLoops[1] },
			{ LoopType.Lead, RedLoops[2] }
		};

		AudioClips [GameColor.Green] = new Dictionary<LoopType, AudioClip> () 
		{
			{ LoopType.Beat, GreenLoops[0] },
			{ LoopType.Bass, GreenLoops[1] },
			{ LoopType.Lead, GreenLoops[2] }
		};

		AudioClips [GameColor.Yellow] = new Dictionary<LoopType, AudioClip> () 
		{
			{ LoopType.Beat, YellowLoops[0] },
			{ LoopType.Bass, YellowLoops[1] },
			{ LoopType.Lead, YellowLoops[2] }
		};

		AudioClips [GameColor.Blue] = new Dictionary<LoopType, AudioClip> () 
		{
			{ LoopType.Beat, BlueLoops[0] },
			{ LoopType.Bass, BlueLoops[1] },
			{ LoopType.Lead, BlueLoops[2] }
		};

		Players = new Dictionary<LoopType, SamplePlayer> () 
		{
			{ LoopType.Beat, BeatPlayer },
			{ LoopType.Bass, BassPlayer },
			{ LoopType.Lead, LeadPlayer }
		};
	}

	public void Play( GameColor _color, LoopType _type) {
		Players [_type].SampleClip = AudioClips [_color] [_type];
	}

}
