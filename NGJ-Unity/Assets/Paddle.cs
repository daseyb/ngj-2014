using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	public UIManager UIManager;
	public int PlayerIndex;
	public Color PlayerColor;
	public PrivacyScript Privacy;
	public float DistanceOffset = 0.1f;
	public float Size = 0.2f;
	public float WidthMultiplier = 1.2f;
	public float MaxSpeed = 2.0f;
	public bool MirrorSpeed = false;

	private LineRenderer lineRenderer;
	private int vertexCount = 5;

	public float position = 0;

	float speed = 0;
	
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
	}

	float WrapAngle(float _angle) {
		_angle = (Mathf.Abs (_angle) % (Mathf.PI * 2)) * Mathf.Sign (_angle);
		if(_angle < 0)
			_angle = Mathf.PI * 2 + _angle;
		return _angle;
	}

	void UpdateLineRendering() {
		vertexCount = (int)((Size / (Mathf.PI * 2)) * Privacy.circleVertexCount);
		lineRenderer.SetColors(PlayerColor, PlayerColor);
		lineRenderer.SetWidth(Privacy.circleWidth * WidthMultiplier, Privacy.circleWidth * WidthMultiplier);
		lineRenderer.SetVertexCount (vertexCount + 1);

		
		for (var i = 0; i < vertexCount + 1; i++)
		{
			float angle = ((float)i / (float)vertexCount) * Size - Size/2 + position;
			angle = WrapAngle (angle);
			
			float acV = Privacy.ac.Evaluate(angle/(Mathf.PI * 2));
			
			float radius = Privacy.circleRadius + DistanceOffset + acV;
			float x = radius * Mathf.Cos(angle);
			float y = radius * Mathf.Sin(angle);
			
			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);
		}
	}

	// Update is called once per frame
	void Update () {
		
		float acc = UIManager.GetPaddleAcceleration (PlayerIndex);
		if (Mathf.Sign (acc) != Mathf.Sign (speed))
			speed = 0;

		speed += acc * 200 * Time.deltaTime;
		if(acc == 0)
			speed = 0;

		speed = Mathf.Clamp (speed, -MaxSpeed, MaxSpeed);
		
		position += (MirrorSpeed ? -1 : 1) * speed * Time.deltaTime;
		position = WrapAngle (position);

		UpdateLineRendering ();
	}
}
