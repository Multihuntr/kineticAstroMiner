using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
	public GameObject target;
	public float minZoom;
	public float maxZoom;

	private float xOffset = 0;
	private float yOffset = 0;
	private Vector2 velocitytarget;

	void Start ()
	{
	}
	
	void Update ()
	{

	}
	void LateUpdate ()
	{
		Rigidbody2D rigbody = target.GetComponent<Rigidbody2D> ();
		Vector2 velocity = rigbody.velocity;
		Camera.main.orthographicSize = Mathf.Lerp (minZoom, maxZoom, Mathf.InverseLerp (0, 20, velocity.magnitude));
		gameObject.transform.position = new Vector3 (target.transform.position.x + xOffset,
		                                      target.transform.position.y + yOffset, gameObject.transform.position.z);
	}
}
