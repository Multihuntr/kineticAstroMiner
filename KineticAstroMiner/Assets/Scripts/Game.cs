using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
	public static bool Paused = false;

	public GUISkin guiSkin;

	private static float prevTimeScale;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Return)) {
			if (Game.Paused) {
				unpause ();
			} else {
				pause ();
			}
		}
	}

	void OnApplicationPause ()
	{
		pause ();
	}

	void pause ()
	{
		prevTimeScale = Time.timeScale;
		Time.timeScale = 0;
		Game.Paused = true;
	}

	void unpause ()
	{
		Time.timeScale = prevTimeScale;
		Game.Paused = false;
	}

	void OnGUI ()
	{
		if (Game.Paused) {
			GUI.skin = guiSkin;
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "Paused");
		}
	}
}