using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{

	Rigidbody2D rb;
	Vector2 aimAt;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		Vector2 twoDPos = gameObject.transform.position;
		float forceFactor = UnityEngine.Random.value * 10 + 10;
		rb.AddForce (forceFactor * (aimAt - twoDPos));
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void startRandom ()
	{
		// Pick random point in play area
		float x = UnityEngine.Random.value;
		float y = UnityEngine.Random.value;
		aimAt = Camera.main.ViewportToWorldPoint (new Vector2 (x, y));
	}
}
