using UnityEngine;
using System.Collections;

public class PrivacyScript : MonoBehaviour
{
	public float radius = 1.0f;
    public int vertexCount = 20;

    private LineRenderer lineRenderer;

    public void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    public void OnUpdate()
    {
        Color c1 = new Color(0.5f, 0.5f, 0.5f, 1f);
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetVertexCount(vertexCount + 1);

        for (var i = 0; i < vertexCount + 1; i++)
        {
            float angle = ((float)i / (float)vertexCount) * Mathf.PI * 2;

            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            //Vector3 pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }
    
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.tag.Equals("Block"))
        //{
            Destroy(other);
        //}
    }
}
