using UnityEngine;
using System.Collections;

public class PrivacyScript : MonoBehaviour
{
    public Color circleColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float circleRadius = 1.0f;
    public int circleVertexCount = 20;
    public float circleWidth = 0.05f;

    public AnimationCurve ac;

    private LineRenderer lineRenderer;
    
    public void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Diffuse"));
        lineRenderer.SetVertexCount(circleVertexCount + 1);
		lineRenderer.useWorldSpace = false;
        Debug.Log(ac.length);
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


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Block"))
        {
            Destroy(other.gameObject);
        }
    }
}
