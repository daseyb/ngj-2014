using UnityEngine;
using System.Collections;

public class CirclePart : ColoredObject {
	public float position = 0;
	public float Size;

	public bool Covers(float _angle) {
		if(Size == 0)
			return false;

		float smaller = _angle;
		float larger = position;
		
		if(smaller > larger) {
			float temp = larger;
			larger = smaller;
			smaller = temp;
		}
		
		bool cw = larger - smaller < smaller + Mathf.PI * 2 - larger;
		
		float angleDist = 0;
		
		if(cw) {
			angleDist = larger - smaller;
		} else {
			angleDist = smaller + Mathf.PI * 2 - larger;
		}
		return angleDist < Size/2;
	}
}
