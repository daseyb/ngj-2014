using UnityEngine;
using System.Collections;

public class Block2D : MonoBehaviour 
{
	public Transform center;
    public float destroyDuration = 0.001f;

	public void Start()
	{
		rigidbody2D.gravityScale = 0; //Disable gravity
	}

	public void FixedUpdate()
	{
		Vector3 direction = -(transform.position - center.position).normalized;
		rigidbody2D.AddForce (new Vector2 (direction.x, direction.y) * 5);
	}
	
	public void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 direction = (transform.position - center.position).normalized;
			rigidbody2D.AddForce(new Vector2(direction.x * 100, direction.y * 100)); 
		}
	}

    public void DestroyAndCreate()
    {
        StartCoroutine("DestroyAndCreateCoroutine");    
    }

    IEnumerator DestroyAndCreateCoroutine()
    { 
        float startTime = Time.time;
        float initialScale = this.transform.localScale.x;

        while (Time.time < startTime + destroyDuration)
        {
            float scale = Mathf.Lerp(initialScale, 0.0f, destroyDuration);
            this.transform.localScale = new Vector3(scale, scale, 0.0f);
            yield return new WaitForEndOfFrame();
        }
        
        Destroy(gameObject);
    }
}
