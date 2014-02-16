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
    public Blocks3D[] blocks3D;
	public ScoreSystem scoreSystem;

    public int currentBlocksIndex = 0;

    public Color circleColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float circleRadius = 1.0f;
    public int circleVertexCount = 20;
    public float circleWidth = 0.05f;

	public int MaxMissedBlocksAllowed = 30;

    public AnimationCurve ac;

	public CirclePart[] CircleParts;

    private LineRenderer lineRenderer;

	private int team1MissedBlocks = 0;
	private int team2MissedBlocks = 0;
    
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

	public void Win(int _winningTeam) {
		scoreSystem.OnWin ();
		PersistentData.WinningTeam = _winningTeam;
		Application.LoadLevel ("ScoreScreen");
	}

	public void EndRound() {
		scoreSystem.EndRound ();
		team1MissedBlocks = 0;
		team2MissedBlocks = 0;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Block"))
        {
            go.GetComponent<Block2D>().PushBack();
        }

        currentBlocksIndex++;
        if (currentBlocksIndex >= blocks3D.Length)
            --currentBlocksIndex;
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (currentBlocksIndex < 0 || currentBlocksIndex >= blocks3D.Length)
        { return; }

		ColoredObject obj = other.GetComponent<ColoredObject> ();

        if(other.tag.Equals("Block"))
        {
			Block2D block2D = other.GetComponent<Block2D>();
			
			if(true || TestHit(obj.transform.position - transform.position, obj.ObjectColor)) {
	            other.tag = "Untagged";
                blocks3D[currentBlocksIndex].ActivateBlockAt(other.transform.position, obj.ObjectColor);
                block2D.StartDestroy();
				scoreSystem.AddScore(obj.ObjectColor);
				if(blocks3D[currentBlocksIndex].IsFull) {
					EndRound();
				}

				//Regenerate health
				if(ColoredObject.TEAM_1_COLORS.Contains(obj.ObjectColor)) {
					team1MissedBlocks--;
					if(team1MissedBlocks < 0)
						team1MissedBlocks = 0;
				} else {
					team2MissedBlocks--;
					if(team2MissedBlocks < 0)
						team2MissedBlocks = 0;
				}

			} else {

				//Deduct health
				if(ColoredObject.TEAM_1_COLORS.Contains(obj.ObjectColor)) {
					team1MissedBlocks++;
					if(team1MissedBlocks == MaxMissedBlocksAllowed)
						Win(2);
				} else {
					team2MissedBlocks++;
					if(team2MissedBlocks == MaxMissedBlocksAllowed)
						Win(1);
				}
				block2D.DestroyAndLoose();
			}

        }
    }
}
