using UnityEngine;
using System.Collections;

public class MenuEnterLevel : MonoBehaviour {
	public string LevelToLoad;

	public void Execute() {
		Application.LoadLevel (LevelToLoad);
		Time.timeScale = 1;
	}

	void OnClick() {
		Execute();
	}
}
