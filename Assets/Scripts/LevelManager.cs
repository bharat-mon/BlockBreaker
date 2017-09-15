using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel (string name) {
		Debug.Log ("Level load requested for: " + name);
		Brick.brickCount = 0;
		Application.LoadLevel(name);
	}
	
	public void QuitRequest () {
		Debug.Log ("Quit game requested");
		Application.Quit();
	}
	
	public void LoadNextLevel() {
		Brick.brickCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
	
	public void BricksDestroyed () {
		if (Brick.brickCount <= 0) {
			LoadNextLevel();
		}
	}
}	
