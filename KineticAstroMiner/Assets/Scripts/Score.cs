using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public GUISkin guiSkin;

	// v for value
	private static int v;

	void Start ()
	{
		v = 0;
	}

	public static void addScore (int add)
	{
		v += add;
	}

	void FixedUpdate ()
	{
		if (!Game.Paused) {
			v++;
		}
	}

	void OnGUI ()
	{
		GUI.skin = guiSkin;
		GUI.Label (new Rect (Screen.width - 116, 10, 106, 30), v.ToString ("000000000"));
	}
}
