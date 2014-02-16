using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreBar : MonoBehaviour {
	public Paddle BottomBar;
	public Paddle MiddleBar;
	public Paddle TopBar;

	public int TeamIndex = 1;
	public int Direction = 1;

	HeightValueAnim hightValue;
	// Use this for initialization
	void Start () {
		hightValue = GetComponent<HeightValueAnim> ();
	}

	void Update() {
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
		
		float maxHeight = Mathf.PI * hightValue.HeightModifier;
		if (thisTeamScore < otherTeamScore) {
			maxHeight *= (float)thisTeamScore/otherTeamScore;
		}
		
		if(Direction > 0) {
			BottomBar.position = 0;
			BottomBar.Size = bottomPercentage * maxHeight;
			
			MiddleBar.position = BottomBar.Size;
			MiddleBar.Size = middlePercentage * maxHeight;
			
			TopBar.position = MiddleBar.position + MiddleBar.Size;
			TopBar.Size = topPercentage * maxHeight;
		} else {
			BottomBar.position = Mathf.PI * 2 - bottomPercentage * maxHeight;
			BottomBar.Size = bottomPercentage * maxHeight;
			
			MiddleBar.position = Mathf.PI * 2 - (bottomPercentage * maxHeight + middlePercentage * maxHeight);
			MiddleBar.Size = middlePercentage * maxHeight;
			
			TopBar.position = Mathf.PI * 2 - (bottomPercentage * maxHeight + middlePercentage * maxHeight + topPercentage * maxHeight);
			TopBar.Size = topPercentage * maxHeight;
		}
	}
}
