using UnityEngine;
using System.Collections;

public class DestroySamples : MonoBehaviour {

	void Start () {
        GameObject samplesObj = GameObject.Find("Samples");
        if (samplesObj)
            GameObject.Destroy(samplesObj);
	}
	
}
