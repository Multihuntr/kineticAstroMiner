using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{

	// v for value
	private static int v;

	// Use this for initialization
	void Start ()
	{
		v = 0;
	}

	public void addScore (int add)
	{
		v += add;
	}
}
