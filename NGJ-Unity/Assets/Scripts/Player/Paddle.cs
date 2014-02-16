using UnityEngine;
using System.Collections;

public class Paddle : CirclePart {
	public UIManager UIManager;
	public int PlayerIndex;
	public Color PlayerColor;
	public PrivacyScript Privacy;
	public float DistanceOffset = 0.1f;
	public float WidthMultiplier = 1.2f;
	public float MaxSpeed = 2.0f;
	public bool MirrorSpeed = false;
	public bool Centered = true;

	private LineRenderer lineRenderer;
	private int vertexCount = 5;

	float speed = 0;

	public float Width { get { return Privacy.circleWidth * WidthMultiplier; } }

	public float GetRadius(float _angle) {
		return Privacy.circleRadius + DistanceOffset + Privacy.ac.Evaluate(_angle/(Mathf.PI * 2)) + (Privacy.circleWidth * WidthMultiplier)/2; 
	}

	// Use this for initialization
	void Start () {
		PlayerColor = COLOR_MAP [ObjectColor];
		lineRenderer = GetComponent<LineRenderer>();
	}

	public static float WrapAngle(float _angle) {
		_angle = (Mathf.Abs (_angle) % (Mathf.PI * 2)) * Mathf.Sign (_angle);
		if(_angle < 0)
			_angle = Mathf.PI * 2 + _angle;
		return _angle;
	}

	void UpdateLineRendering() {
		vertexCount = (int)((Size / (Mathf.PI * 2)) * Privacy.circleVertexCount);
		lineRenderer.SetColors(PlayerColor, PlayerColor);
		lineRenderer.SetWidth(Width, Width);
		lineRenderer.SetVertexCount (vertexCount + 1);

		
		for (var i = 0; i < vertexCount + 1; i++)
		{
			float angle = ((float)i / (float)vertexCount) * Size - (Centered ? Size/2 : 0) + position;
			angle = WrapAngle (angle);
			
			float radius = GetRadius(angle);
			float x = radius * Mathf.Cos(angle);
			float y = radius * Mathf.Sin(angle);
			
			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
		}
	}

	// Update is called once per frame
	void Update () {
		if(UIManager) {
			float acc = UIManager.GetPaddleAcceleration (PlayerIndex);
			if (Mathf.Sign (acc) != Mathf.Sign (speed))
				speed = 0;

			speed = acc * MaxSpeed;
			if(acc == 0)
				speed = 0;

			speed = Mathf.Clamp (speed, -MaxSpeed, MaxSpeed);
			
			position += (MirrorSpeed ? -1 : 1) * speed * Time.deltaTime;
			position = WrapAngle (position);
		}

		UpdateLineRendering ();
	}
}
