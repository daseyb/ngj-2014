using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreSystem : MonoBehaviour {

	public float TakeoverPercentage = 0.1f;

	public Animation CircleAnimation;

	public AudioManager AudioManager;

	public Dictionary<GameColor, int> Scores = new Dictionary<GameColor, int>();
	public Dictionary<GameColor, int> RoundScores = new Dictionary<GameColor, int>();
	

	class ScoreComparer : IComparer<KeyValuePair<GameColor, int>>
	{
		public int Compare (KeyValuePair<GameColor, int> x, KeyValuePair<GameColor, int> y)
		{
			return x.Value.CompareTo (y.Value);
		}
	}

	public void EndRound() {
		List<KeyValuePair<GameColor, int>> scores = new List<KeyValuePair<GameColor, int>> ();
		foreach(var score in RoundScores) {
			scores.Add(score);
		}
		scores.Sort (new ScoreComparer ());

		RoundScores.Clear ();
	}

	public void AddScore(GameColor _color) {

		if (_color == GameColor.Teal) {
			AddScore(GameColor.Blue);
			AddScore(GameColor.Green);
			return;
		} else if(_color == GameColor.Orange) {
			AddScore(GameColor.Red);
			AddScore(GameColor.Yellow);
			return;
		}

		CircleAnimation.enabled = true;

		if(!Scores.ContainsKey(_color))
			Scores[_color] = 0;
		Scores [_color]++;

		if(!RoundScores.ContainsKey(_color))
			RoundScores[_color] = 0;
		RoundScores [_color]++;

		UpdateMusic ();
	}

	void UpdateMusic() {
		List<KeyValuePair<GameColor, int>> scores = new List<KeyValuePair<GameColor, int>> ();
		foreach(var score in RoundScores) {
			scores.Add(score);
		}

		scores.Sort(new ScoreComparer());

		switch (scores.Count) {
		case 1: AudioManager.Play (scores [0].Key, LoopType.Beat); break;
		case 2: AudioManager.Play (scores [1].Key, LoopType.Bass); break;
		case 3: AudioManager.Play (scores [2].Key, LoopType.Lead); break;
		default:
			AudioManager.Play (scores [0].Key, LoopType.Lead);
			if(scores[0].Value * ( 1.0f - TakeoverPercentage) >= scores[1].Value)
				AudioManager.Play (scores [0].Key, LoopType.Bass);
			else
				AudioManager.Play (scores [1].Key, LoopType.Bass);

			AudioManager.Play (scores [2].Key, LoopType.Bass);
			break;
		}
	}

		  
}
