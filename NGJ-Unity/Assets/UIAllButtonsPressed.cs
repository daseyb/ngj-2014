using UnityEngine;
using System.Collections;

public class UIAllButtonsPressed : MonoBehaviour {
	public ButtonScript[] Buttons;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		bool allPressed = true;
		foreach (var bt in Buttons) {
			if(!bt.IsDown)
				allPressed = false;
		}

		if(allPressed)
			Application.LoadLevel("Game");
	}
}
