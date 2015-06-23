using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
	public GameObject target;
	public float xOffset = 0;
	public float yOffset = 0;
	private Vector2 velocitytarget;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	void LateUpdate ()
	{
		Rigidbody2D rigbody = target.GetComponent<Rigidbody2D> ();
		Vector2 velocity = rigbody.velocity;
		Camera.main.orthographicSize = Mathf.Lerp (10, 20, Mathf.InverseLerp (0, 20, rigbody.velocity.magnitude));
		gameObject.transform.position = new Vector3 (target.transform.position.x + xOffset,
		                                      target.transform.position.y + yOffset, gameObject.transform.position.z);
	}
}
