using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameColor 
{
	Red,
	Yellow,
	Green,
	Blue,
	Teal,
	Orange
}

public class PrivacyScript : MonoBehaviour
{
    public Color circleColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float circleRadius = 1.0f;
    public int circleVertexCount = 20;
    public float circleWidth = 0.05f;

    public AnimationCurve ac;

	public CirclePart[] CircleParts;

    private LineRenderer lineRenderer;
    
    public void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Diffuse"));
        lineRenderer.SetVertexCount(circleVertexCount + 1);
		lineRenderer.useWorldSpace = false;
    }

    public void Update()
    {        
        lineRenderer.SetColors(circleColor, circleColor);
        lineRenderer.SetWidth(circleWidth, circleWidth);
		lineRenderer.SetVertexCount (circleVertexCount + 1);
		
        for (var i = 0; i < circleVertexCount + 1; i++)
        {

            float angle = ((float)i / (float)circleVertexCount) * Mathf.PI * 2;

            float acV = ac.Evaluate(((float)i / (float)circleVertexCount));

			float x = (circleRadius + acV) * Mathf.Cos(angle);
			float y = (circleRadius + acV) * Mathf.Sin(angle);

            Vector3 pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }
    }

	public bool TestHit(Vector2 _dir, GameColor _testColor) {
		float angle = Vector2.Angle(Vector2.right, _dir);
		Vector3 cross = Vector3.Cross(Vector2.right, _dir);
		
		if (cross.z > 0)
			angle = 360 - angle;

		angle *= Mathf.Deg2Rad;
		angle = Mathf.PI * 2 - angle;

		List<CirclePart> hitParts = new List<CirclePart> ();
		foreach (var circlePart in CircleParts) {
			if(circlePart.ObjectColor == _testColor && circlePart.Covers(angle))
				hitParts.Add(circlePart);
		}

		return hitParts.Count > 0;
	}


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
		ColoredObject obj = other.GetComponent<ColoredObject> ();

        if(other.tag.Equals("Block"))
        {
			if(TestHit(obj.transform.position - transform.position, obj.ObjectColor)) {
				Debug.Log("Hit color: " + obj.ObjectColor.ToString());
			}
            Destroy(other.gameObject);
        }
    }
}
