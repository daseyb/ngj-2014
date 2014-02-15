using UnityEngine;
using System.Collections;

public class MakePersistent : MonoBehaviour {
	void Start () {
		if(GameObject.Find(gameObject.name) != gameObject)
			Destroy(gameObject);
		else
			DontDestroyOnLoad (gameObject);
	}
}
