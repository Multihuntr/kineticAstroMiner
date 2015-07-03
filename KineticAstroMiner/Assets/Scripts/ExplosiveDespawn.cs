using UnityEngine;
using System.Collections;

public class ExplosiveDespawn : MonoBehaviour
{
	public float lifetime;
	private float count;
	// Use this for initialization
	void Start ()
	{
		count = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Game.Paused) {
			return;
		}
		if (count < lifetime) {
			count += 1;
		} else {
			Destroy (gameObject);
		}
	}
}
