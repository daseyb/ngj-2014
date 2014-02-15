using UnityEngine;
using System.Collections;

public class Block2D : MonoBehaviour 
{
	public Transform center;
    public float destroyDuration = 0.1f;
    public bool applyForce = true;

	public void Start()
	{
		rigidbody2D.gravityScale = 0; //Disable gravity
        rigidbody2D.AddTorque(0.5f);
	}

	public void FixedUpdate()
	{
        if (applyForce)
        {
            Vector3 direction = -(transform.position - center.position).normalized;
            rigidbody2D.AddForce(new Vector2(direction.x, direction.y) * 2);
        }
	}
	
	public void Update () 
	{
		/*if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 direction = (transform.position - center.position).normalized;
			rigidbody2D.AddForce(new Vector2(direction.x * 100, direction.y * 100)); 
		}*/
	}

    public void DestroyAndCreate()
    {
        applyForce = false;
        StartCoroutine("DestroyAndCreateCoroutine");    
    }

    IEnumerator DestroyAndCreateCoroutine()
    { 
        float startTime = Time.time;
        float initialScale = this.transform.localScale.x;

        rigidbody2D.velocity = rigidbody2D.velocity.normalized;

         float t = 0;
         while (t < 1)
         {
             yield return new WaitForEndOfFrame();
             t += Time.deltaTime / destroyDuration;
             float scale = Mathf.Lerp(initialScale, 0.0f, t);
             this.transform.localScale = new Vector3(scale, scale, 0.0f);
         }

        Destroy(gameObject);
    }
}
