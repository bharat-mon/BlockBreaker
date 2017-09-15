using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	private int timesHit;
	private LevelManager levelManager;
	private bool isBreakable;
	public Sprite[] hitSprites;
	public static int brickCount = 0;
	public AudioClip crack;
	public GameObject smoke;

	// Use this for initialization
	void Start () {
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		isBreakable = (this.tag == "Breakable");
		if (isBreakable) {
			brickCount++;
		}
		Debug.Log("Bricks in level: " + brickCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
	AudioSource.PlayClipAtPoint (crack, transform.position);
		if (isBreakable) {
			HandleHits();
		}
	}
	
	void HandleHits () {
		timesHit++;
		int maxHits = hitSprites.Length + 1;
		if (timesHit >= maxHits) {
			brickCount--;
			Debug.Log("Bricks left: " + brickCount);
			levelManager.BricksDestroyed();
			SmokeParticles();
			Destroy(gameObject);
		} else {
			LoadSprites();
		}
	}
	
	void SmokeParticles () {
		GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
		smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites () {
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex]) {
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		} else {
			Debug.LogError("Brick sprite missing");
		}
	}
	
	// TODO Remove this method once a proper win condition is set
	void SimulateWin () {
		levelManager.LoadNextLevel();
	}
}
