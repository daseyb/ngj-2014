using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LoopType {
	Beat,
	Bass,
	Lead
}

public class AudioManager : MonoBehaviour {
	public SamplePlayer[] RedLoops;
	public SamplePlayer[] GreenLoops;
	public SamplePlayer[] YellowLoops;
	public SamplePlayer[] BlueLoops;

	public Dictionary<GameColor, Dictionary<LoopType, SamplePlayer>> Players = new Dictionary<GameColor, Dictionary<LoopType, SamplePlayer>> ();
	
	void Start() {
		Players [GameColor.Red] = new Dictionary<LoopType, SamplePlayer> () 
		{
			{ LoopType.Beat, RedLoops[0] },
			{ LoopType.Bass, RedLoops[1] },
			{ LoopType.Lead, RedLoops[2] }
		};

		Players [GameColor.Green] = new Dictionary<LoopType, SamplePlayer> () 
		{
			{ LoopType.Beat, GreenLoops[0] },
			{ LoopType.Bass, GreenLoops[1] },
			{ LoopType.Lead, GreenLoops[2] }
		};

		Players [GameColor.Yellow] = new Dictionary<LoopType, SamplePlayer> () 
		{
			{ LoopType.Beat, YellowLoops[0] },
			{ LoopType.Bass, YellowLoops[1] },
			{ LoopType.Lead, YellowLoops[2] }
		};

		Players [GameColor.Blue] = new Dictionary<LoopType, SamplePlayer> () 
		{
			{ LoopType.Beat, BlueLoops[0] },
			{ LoopType.Bass, BlueLoops[1] },
			{ LoopType.Lead, BlueLoops[2] }
		};
	}

	public void Play( GameColor _color, LoopType _type) {
		Players [GameColor.Red] [_type].NextBeatVolume = 0;
		Players [GameColor.Green] [_type].NextBeatVolume = 0;
		Players [GameColor.Yellow] [_type].NextBeatVolume = 0;
		Players [GameColor.Blue] [_type].NextBeatVolume = 0;
		
		Players [_color] [_type].NextBeatVolume = 1;

	}

}
