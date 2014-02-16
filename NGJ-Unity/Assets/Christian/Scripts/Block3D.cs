using UnityEngine;
using System.Collections;

public class Block3D : MonoBehaviour 
{
    public float endScale = 0.3f;
    public float growDuration = 0.01f;
    public float shrinkDuration = 0.01f;
    public float moveDuration = 0.01f;
    public float rotationDuration = 0.01f;
    public float initialScale = 0.0f;

    //private float speed = 1;
    private Vector3 startPosition;
    private Vector3 activationPosition;
    private Quaternion startRotation;
    private Quaternion activationRotation;

    public void Start()
    {
        
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
	
    public void DoActivation (Vector3 pos)
    {
        startPosition = transform.localPosition;
        //startRotation = transform.rotation;

        activationPosition = transform.parent.InverseTransformPoint(pos);
        //activationRotation = transform.rotation;

        transform.localPosition = activationPosition;
        //transform.rotation = Quaternion.Euler(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        StartCoroutine("GrowScale");
        StartCoroutine("MoveToStartPosition");
        //StartCoroutine("RotateToStartRotation");
    }

    public void DoReset ()
    {
        StartCoroutine("ShrinkScale");
    }
    
	public void Update () 
    {
        //Manually update location
        //if (Activated)
        //{
        //    transform.position = Vector3.Slerp(transform.position, startPosition, speed * Time.deltaTime);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, speed * Time.deltaTime);
        //    speed = 1 / Mathf.Ceil(Vector3.Distance(startPosition, transform.position));
        //}
	}

    IEnumerator MoveToStartPosition()
    {
        float t = 0;
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime / moveDuration;

            transform.localPosition = Vector3.Lerp(activationPosition, startPosition, t);
        }
        transform.localPosition = startPosition;
    }


    IEnumerator GrowScale()
    {
        transform.renderer.enabled = true;

        float t = 0;
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime / growDuration;

            float currentScale = Mathf.Lerp(initialScale, endScale, t);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        transform.localScale = new Vector3(endScale, endScale, endScale);

    }

    IEnumerator ShrinkScale()
    {
        float scale = transform.localScale.x;

        float t = 0;
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime / shrinkDuration;

            float currentScale = Mathf.Lerp(scale, initialScale, t);
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        transform.renderer.enabled = false;
    }
}
