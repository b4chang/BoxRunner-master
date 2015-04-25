using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

	private bool paused = false;

	private static GameStateManager instance; //a singleton - is a way of accessing one class from all other classes

	public static GameStateManager Instance {
		get {
			if (instance == null){
				instance = GameObject.FindObjectOfType<GameStateManager>();
			}
			return GameStateManager.instance;
		}

	}

	public bool Paused {
		get {
			return paused;
		}

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			Instance.PauseGame ();
		}
	}

	public void PauseGame()
	{
		paused = !paused;
	}
}
