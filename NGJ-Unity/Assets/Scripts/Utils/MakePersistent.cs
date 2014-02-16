using UnityEngine;
using System.Collections;

public class MakePersistent : MonoBehaviour {
	public bool DestroyThisOnDuplicate = true;
	void Start () {
		GameObject otherObj = GameObject.Find (gameObject.name);
		if(otherObj != gameObject)  {
			if(DestroyThisOnDuplicate)
				Destroy(gameObject);
			else {
				DontDestroyOnLoad (gameObject);
				Destroy(otherObj);
			}
		}
		else
			DontDestroyOnLoad (gameObject);
	}
}
