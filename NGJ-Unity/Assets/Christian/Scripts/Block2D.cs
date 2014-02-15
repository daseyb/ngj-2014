using UnityEngine;
using System.Collections;

public class Block2D : OnPlayBase 
{
	public Transform center;
    public float destroyDuration = 0.1f;
    public bool applyForce = true;


	int playCount = 0;

	protected override void OnStart()
	{
		rigidbody2D.gravityScale = 0; //Disable gravity
        rigidbody2D.AddTorque(0.5f);
	}

	protected override void OnPlay ()
	{
		base.OnPlay ();
		playCount++;
		if (playCount > 1) {
			Vector3 direction = -(transform.position - center.position).normalized;
			rigidbody2D.velocity = new Vector2(direction.x, direction.y) * 5;
		}
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
