using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	public bool IsDown = false;

	void OnPress(bool _down) {
		IsDown = _down;
	}

}
