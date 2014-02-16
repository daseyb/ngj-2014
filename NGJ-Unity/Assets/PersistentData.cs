using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentData : MonoBehaviour {
	public static Dictionary<GameColor, int> FinalScores = new Dictionary<GameColor, int>() {
		{GameColor.Blue, 	2},
		{GameColor.Green, 	6},
		{GameColor.Orange, 	3},
		{GameColor.Red, 	5},
		{GameColor.Teal, 	5},
		{GameColor.Yellow, 	4},
	};

	public static int WinningTeam = 1;

	public static int GetTeamScore( int _team) {
		int totalTeamScore = 0;
		List<GameColor> teamColors = new List<GameColor>();
		if(_team == 1)
			teamColors.AddRange(ColoredObject.TEAM_1_COLORS);
		else
			teamColors.AddRange(ColoredObject.TEAM_2_COLORS);
		
		foreach (var col in teamColors)
			totalTeamScore += PersistentData.FinalScores[col];

		return totalTeamScore;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
