using UnityEngine;
using System.Collections;

public class PrivacyScript : MonoBehaviour
{
    public Blocks3D blocks3D;
    public Color circleColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float circleRadius = 1.0f;
    public int circleVertexCount = 20;
    public float circleWidth = 0.05f;

    public AnimationCurve ac;

    private LineRenderer lineRenderer;
    
    public void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetVertexCount(circleVertexCount + 1);
    }

    public void Update()
    {        
        lineRenderer.SetColors(circleColor, circleColor);
        lineRenderer.SetWidth(circleWidth, circleWidth);

        for (var i = 0; i < circleVertexCount + 1; i++)
        {
            float angle = ((float)i / (float)circleVertexCount) * Mathf.PI * 2;

            float acV = ac.Evaluate((float)i / (float)circleVertexCount) - 0.5f;

            float x = (circleRadius * Mathf.Cos(angle)) + acV;
            float y = (circleRadius * Mathf.Sin(angle)) + acV;

            Vector3 pos = new Vector3(x, y, 0.0f);
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
            other.tag = "Untagged";
            //Debug.Log("Trigger!");

            //Vector3 pos = Camera.main.ScreenToWorldPoint(other.rigidbody2D.transform.position);
            //blocks3D.ActivateBlockAt(new Vector3(pos.x, pos.y, 0));

            blocks3D.ActivateBlockAt(other.transform.position);

            Block2D otherBlock = other.GetComponent<Block2D>();
            otherBlock.DestroyAndCreate();
        }
    }
}
