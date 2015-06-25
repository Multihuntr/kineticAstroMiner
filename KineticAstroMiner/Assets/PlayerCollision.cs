using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{


	void OnCollisionEnter2D (Collision2D other)
	{
		Vector3 targetDir = other.transform.position - gameObject.transform.position;
		Vector3 forward = transform.up;
		float angle = Vector3.Angle (targetDir, forward);
		Debug.Log (angle);
		if (angle > 0 && angle < 20) {
			Destroy (other.gameObject);
		}
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
