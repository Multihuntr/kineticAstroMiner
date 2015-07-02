using UnityEngine;
using System.Collections;

public class Hull : MonoBehaviour
{
	public float maxhealth;
	private float currenthealth;
	// Use this for initialization
	public void damage (int attackvalue)
	{
		currenthealth -= attackvalue;
		if (currenthealth < 1) {
			death ();
		}
		Debug.Log (currenthealth);
	}
	public void repair ()
	{
		if (currenthealth < 10000) {
			currenthealth += 15;
		}
	}
	void death ()
	{
		Destroy (gameObject);
	}
	void Start ()
	{
		currenthealth = maxhealth;
	}
	// Update is called once per frame
	void Update ()
	{
		
	}
}
