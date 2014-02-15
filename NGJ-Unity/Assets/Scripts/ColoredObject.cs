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
}
