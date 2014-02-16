using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentData : MonoBehaviour {
	public static Dictionary<GameColor, int> FinalScores;

	public static int WinningTeam = 1;

	public static int GetTeamScore( int _team) {
		int totalTeamScore = 0;
		List<GameColor> teamColors = new List<GameColor>();
		if(_team == 1)
			teamColors.AddRange(ColoredObject.TEAM_1_COLORS);
		else
			teamColors.AddRange(ColoredObject.TEAM_2_COLORS);
		
		foreach (var col in teamColors)
			totalTeamScore += GetScore(col);

		return totalTeamScore;
	}

	public static int GetScore(GameColor _color) {
		if (!FinalScores.ContainsKey (_color))
			return 0;
		return FinalScores[_color];
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
