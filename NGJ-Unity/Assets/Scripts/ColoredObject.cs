using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColoredObject : MonoBehaviour {
	public GameColor ObjectColor;

	public static readonly Dictionary<GameColor, Color> COLOR_MAP = new Dictionary<GameColor, Color>() {
		{ GameColor.Blue, Color.blue },
		{ GameColor.Green, Color.green },
		{ GameColor.Orange, new Color(1, 0.5f, 0) },
		{ GameColor.Red, Color.red },
		{ GameColor.Teal, new Color(0, 1, 1) },
		{ GameColor.Yellow, new Color(1, 1, 0) },
	};

	public static readonly List<GameColor> TEAM_1_COLORS = new List<GameColor>() {
		GameColor.Red, GameColor.Yellow, GameColor.Orange 
	};

	public static readonly List<GameColor> TEAM_2_COLORS = new List<GameColor>() {
		GameColor.Blue, GameColor.Green, GameColor.Teal 
	};
}
