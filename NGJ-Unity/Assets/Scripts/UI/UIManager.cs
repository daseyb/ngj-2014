using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public UIPaddleController[] PaddleControllers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetPaddleAcceleration(int _playerIndex) {
		return PaddleControllers [_playerIndex].GetPaddlePosition ();
	}
}
