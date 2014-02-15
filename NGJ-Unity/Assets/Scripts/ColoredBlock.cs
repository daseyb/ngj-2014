using UnityEngine;
using System.Collections;

public class ColoredBlock : ColoredObject {

	// Use this for initialization
	void Start () {
	
	}
	
	public void SetColor(GameColor _color) {
		ObjectColor = _color;
		renderer.material.color = COLOR_MAP [_color];
	}
}
