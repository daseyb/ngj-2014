using UnityEngine;
using System.Collections;

public class UIPaddleController : MonoBehaviour {
	UISlider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<UISlider> ();
		slider.onDragFinished += DragFinished;
	}

	void DragFinished() {
		slider.value = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public float GetPaddlePosition() {
		return (slider.value - 0.5f) * -2; 
	}
}
