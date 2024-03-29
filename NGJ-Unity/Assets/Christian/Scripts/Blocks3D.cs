﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blocks3D : MonoBehaviour 
{
    public bool doRotate = true;

    public PrivacyScript priv;

    //private Dictionary<Transform, bool> blocks3D = new Dictionary<Transform,bool>();
    private List<Transform> blocks3D = new List<Transform>();
    private Vector3 rotation;

    private bool resetRound = false;
    //private bool roundFinished = false;

	public bool IsFull { get; private set; }

	void Start () 
    {
        rotation = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        // Keep trach of all the scoring blocks and if it is active
        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = transform.GetChild(i);
            child.renderer.enabled = false;
            blocks3D.Add(child);
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
                Block3D deactivatingBlock = (Block3D)block.GetComponent<Block3D>();
                deactivatingBlock.DoReset();
            }
            resetRound = false;
        }

	}

    IEnumerator RotationUpdate()
    {
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (doRotate)
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
		IsFull = true;
        
        foreach (Transform block in blocks3D)
        {
            if (block.renderer.enabled == false)
            {
                block.renderer.enabled = true;
                Block3D activatingBlock = (Block3D)block.GetComponent<Block3D>();
                activatingBlock.GetComponent<ColoredBlock>().SetColor(_color);
                activatingBlock.DoActivation(pos);
                break;
            }
        }

        foreach (Transform block in blocks3D)
            if (!block.renderer.enabled) IsFull = false;
        
        if (IsFull)
        {
            StartCoroutine("EndRound");
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
        transform.localScale = new Vector3(endScale, endScale, endScale);

        yield return new WaitForSeconds(1.0f);
        
        resetRound = true;
        
        t = 0;
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime / duration;
            float currentScale = Mathf.Lerp(endScale, initialScale, t);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
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
