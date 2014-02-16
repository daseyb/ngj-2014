using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blocks3D : MonoBehaviour 
{
    public int width, height, depth;
   
    private Dictionary<Transform, bool> blocks3D = new Dictionary<Transform,bool>();

    private Vector3 rotation;
    public bool Rotate = true;

	public bool IsFull { get; private set; }

	void Start () 
    {
        rotation = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        // Keep trach of all the scoring blocks and if it is active
        //Debug.Log(transform.childCount);
        for (int i = 0; i < transform.childCount; ++i)
        {
            blocks3D.Add(transform.GetChild(i), false);
        }

        StartCoroutine("RotationUpdate");
	}

	public void Update () 
    {
        if(Random.Range(0.0f, 1.0f) > 0.99f)
        {
            ChangeRotation();
        }
	}

    IEnumerator RotationUpdate()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (Rotate)
                transform.Rotate(rotation);

            yield return new WaitForEndOfFrame();
        }
    }

    public void ChangeRotation()
    {
        rotation = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public void ActivateBlockAt(Vector3 pos, GameColor _color)
    {
		IsFull = true;
        foreach (var block in blocks3D)
        {
            if (block.Value == false)
            {
                blocks3D[block.Key] = true;
                Block3D activatingBlock = (Block3D)block.Key.GetComponent<Block3D>();
				activatingBlock.GetComponent<ColoredBlock>().SetColor(_color);
                //Rotate = false;
                activatingBlock.DoActivation(pos);
				IsFull = false;
                break;
            }
        }

    }
}
