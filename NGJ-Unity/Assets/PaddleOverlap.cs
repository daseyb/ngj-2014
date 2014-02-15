using UnityEngine;
using System.Collections;

public class PaddleOverlap : MonoBehaviour {
	public Paddle Paddle01;
	public Paddle Paddle02;
	public Color OverlapColor;

	float size = 0;

	LineRenderer lineRenderer;
	int vertexCount = 0;
	float position = 0;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void UpdateLineRendering() {
		vertexCount = (int)((size / (Mathf.PI * 2)) * Paddle01.Privacy.circleVertexCount);
		lineRenderer.SetColors(OverlapColor, OverlapColor);
		lineRenderer.SetWidth(Paddle01.Width, Paddle01.Width);
		lineRenderer.SetVertexCount (vertexCount + 1);
		
		
		for (var i = 0; i < vertexCount + 1; i++)
		{
			float angle = ((float)i / (float)vertexCount) * size - size/2 + position;
			angle = Paddle.WrapAngle (angle);
			
			float radius = Paddle01.GetRadius(angle);
			float x = radius * Mathf.Cos(angle);
			float y = radius * Mathf.Sin(angle);
			
			Vector3 pos = new Vector3(x, y, -0.01f);
			lineRenderer.SetPosition(i, pos);
		}
	}

	// Update is called once per frame
	void Update () {
		float smaller = Paddle01.position;
		float larger = Paddle02.position;
		
		float smallerSize = Paddle01.Size;
		float largerSize = Paddle02.Size;
		
		if(smaller > larger) {
			float temp = larger;
			larger = smaller;
			smaller = temp;
			
			temp = largerSize;
			largerSize = smallerSize;
			smallerSize = temp;
		}
		
		bool cw = larger - smaller < smaller + Mathf.PI * 2 - larger;

		float angleDist = 0;

		if(cw) {
			angleDist = larger - smaller;
			position = smaller + angleDist * smallerSize/(smallerSize + largerSize);
		} else {
			angleDist = smaller + Mathf.PI * 2 - larger;
			position = larger + angleDist  * largerSize/(smallerSize + largerSize);
		}

		size = Paddle01.Size / 2 + Paddle02.Size / 2 - angleDist;
		if (size > 0) {
			lineRenderer.enabled = true;

			position = Paddle.WrapAngle(position);
			UpdateLineRendering();
		} else {
			lineRenderer.enabled = false;
		}
	}
}
