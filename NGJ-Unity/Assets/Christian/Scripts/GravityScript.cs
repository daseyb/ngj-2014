using UnityEngine;
using System.Collections;

public class GravityScript : MonoBehaviour 
{
	public Transform center;

	public void Start()
	{
		rigidbody2D.gravityScale = 0; //Disable gravity
	}

	public void FixedUpdate()
	{
		Vector3 direction = -(transform.position - center.position).normalized;
		rigidbody2D.AddForce (new Vector2 (direction.x, direction.y));
	}
	
	public void Update () 
	{
		//Manually update location
		//transform.position = Vector3.Slerp(transform.position, center.position, speed * Time.deltaTime);
		//speed = 1/Mathf.Ceil(Vector3.Distance(center.position, transform.position));

		if (Input.GetMouseButtonDown (0)) 
		{
			Vector3 direction = (transform.position - center.position).normalized;
			rigidbody2D.AddForce(new Vector2(direction.x * 100, direction.y * 100)); 
		}
	}
}
