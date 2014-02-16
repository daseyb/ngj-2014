using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blocks3D : MonoBehaviour 
{
    public bool Rotate = true;

    public PrivacyScript priv;

    private Dictionary<Transform, bool> blocks3D = new Dictionary<Transform,bool>();
    private Vector3 rotation;
    public bool roundFinished = false;
    public bool resetRound = false;
    void Start () 
    {
        rotation = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        // Keep trach of all the scoring blocks and if it is active
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
        
        if(resetRound)
        {
            foreach (var block in blocks3D)
            {
                Block3D deActivatingBlock = (Block3D)block.Key.GetComponent<Block3D>();
                deActivatingBlock.DoReset();
                blocks3D[block.Key] = false;
            }
            priv.NextRound();
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

    //public void StartNewRound()
    //{
    //    foreach (var block in blocks3D)
    //    {
    //        blocks3D[block.Key] = false;
    //        block.Key.gameObject.SetActive(true);
    //    }
    //    roundFinished = false;
    //}

    public void ActivateBlockAt(Vector3 pos, GameColor _color)
    {
        if (!roundFinished)
        {
            foreach (var block in blocks3D)
            {
                if (block.Value == false)
                {
                    blocks3D[block.Key] = true;
                    Block3D activatingBlock = (Block3D)block.Key.GetComponent<Block3D>();
                    activatingBlock.GetComponent<ColoredBlock>().SetColor(_color);
                    activatingBlock.DoActivation(pos);
                    break;
                }
            }

            bool finished = true;
            foreach (bool b in blocks3D.Values)
            {
                if (b == false)
                    finished = false;
            }

            if (finished)
            {
                roundFinished = true;
                StartCoroutine("EndRound");
               
            }
        }
    }

    IEnumerator EndRound()
    {
        yield return new WaitForSeconds(2.0f);

        float initialScale = transform.localScale.x;
        float endScale = 2.5f;

        float duration = 1.0f;
        float t = 0;
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime / duration;
            float currentScale = Mathf.Lerp(initialScale, endScale, t);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        
        yield return new WaitForSeconds(2.0f);
        
        resetRound = true;
    }

    //IEnumerator RemoveBlock(Transform block)
    //{
    //    float duration = 1.0f;
    //    float initialScale = block.transform.localScale.x;

    //    float t = 0;
    //    while (t < 1)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        t += Time.deltaTime / duration;
    //        float currentScale = Mathf.Lerp(initialScale, 0.0f, t);
    //        block.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
    //    }

    //    block.transform.localScale = Vector3.zero;
    //}

}
