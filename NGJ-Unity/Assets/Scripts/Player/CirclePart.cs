using UnityEngine;
using System.Collections;

public class CirclePart : ColoredObject {
	public float position = 0;
	public float Size;

	public bool Covers(float _angle) {
		if(Size == 0)
			return false;

		float left = position - Size / 2;
		float right = position + Size / 2;

		if( right > Mathf.PI * 2) {
			right -= Mathf.PI * 2;
			left -= Mathf.PI * 2;
			_angle -= Mathf.PI * 2;
		}

		right -= left;
		_angle -= left;

		return 0 <= _angle && _angle <= right;
	}
}
