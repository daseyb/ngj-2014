using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreBar : MonoBehaviour {
	public Paddle BottomBar;
	public Paddle MiddleBar;
	public Paddle TopBar;

	public int TeamIndex = 1;
	public int Direction = 1;

	// Use this for initialization
	void Start () {

		int thisTeamScore = PersistentData.GetTeamScore (TeamIndex);
		int otherTeamScore = PersistentData.GetTeamScore (TeamIndex == 1 ? 2 : 1);
		
		int bottomScore = PersistentData.FinalScores[BottomBar.ObjectColor];
		int topScore = PersistentData.FinalScores[TopBar.ObjectColor];

		List<GameColor> teamColors = new List<GameColor>();
		if(TeamIndex == 1)
			teamColors.AddRange(ColoredObject.TEAM_1_COLORS);
		else
			teamColors.AddRange(ColoredObject.TEAM_2_COLORS);

		teamColors.Remove (BottomBar.ObjectColor);
		teamColors.Remove (TopBar.ObjectColor);

		int middleScore = PersistentData.FinalScores [teamColors [0]];

		float bottomPercentage = (float)bottomScore / thisTeamScore;
		float topPercentage = (float)topScore / thisTeamScore;
		float middlePercentage = (float)middleScore / thisTeamScore;
		
		float maxHeight = Mathf.PI;
		if (thisTeamScore < otherTeamScore) {
			maxHeight *= (float)thisTeamScore/otherTeamScore;
		}

		BottomBar.position = Direction * bottomPercentage * maxHeight / 2;
		BottomBar.Size = bottomPercentage * maxHeight;

		MiddleBar.position = Direction * (BottomBar.position + middlePercentage * maxHeight / 2);
		MiddleBar.Size = middlePercentage * maxHeight;

		TopBar.position = Direction * (MiddleBar.position + topPercentage * maxHeight / 2);
		TopBar.Size = topPercentage * maxHeight;
	}
}
