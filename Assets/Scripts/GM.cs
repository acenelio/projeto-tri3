using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public static GM instance = null;

	public float yLive = -10;

	PlayerCtrl player;

	public float timeToRespawn = 2f;
	public float timeToKill = 1.5f;
	public Transform spawnPoint;
	public GameObject playerPrefab;

	public float maxTime = 120f;
	bool timerOn = true;
	float timeLeft;

	public UI ui;

	GameData data = new GameData();

	void Awake() {
		if (instance == null) {
			instance = this;
		}
	}
	
	void Start() {
		if (player == null) {
			RespawnPlayer();
		}
		timeLeft = maxTime;
	}

	// Update is called once per frame
	void Update () {
		if (player == null) {
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if (obj != null) {
				player = obj.GetComponent<PlayerCtrl>();
			}
		}
		UpdateTimer();
		DisplayHudData();
	}

	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ExitToMainMenu() {
		LoadScene("MainMenu");
	}

	public void CloseApp() {
		Application.Quit();
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	void UpdateTimer() {
		if (timerOn) {
			timeLeft = timeLeft - Time.deltaTime;
			if (timeLeft <= 0f) {
				timeLeft = 0;
				ExpirePlayer();
			}
		}
	}

	void DisplayHudData() {
		ui.hud.txtCoinCount.text = "x " + data.coinCount;
		ui.hud.txtLifeCount.text = "x " + data.lifeCount;
		ui.hud.txtTimer.text = timeLeft.ToString("F1");
	} 

	public void IncrementCoinCount() {
		data.coinCount++;
	}

	public void DecrementLives() {
		data.lifeCount--;
	}

	public void KillPlayer() {
		if (player != null) {
			Destroy(player.gameObject);
			DecrementLives();
			if (data.lifeCount > 0) {
				Invoke("RespawnPlayer", timeToRespawn);
			}
			else {
				GameOver();
			}
		}
	}

	public void ExpirePlayer() {
		if (player != null) {
			Destroy(player.gameObject);
		}
		GameOver();
	}

	void GameOver() {
		timerOn = false;
		ui.gameOver.txtCoinCount.text = "Coins: " + data.coinCount;
		ui.gameOver.txtTimer.text = "Timer: " + timeLeft.ToString("F1");
		ui.gameOver.gameOverPanel.SetActive(true);
	}

	public void RespawnPlayer() {
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
	}

	public void LevelComplete() {
		Destroy(player.gameObject);
		timerOn = false;
		ui.levelComplete.txtCoinCount.text = "Coins: " + data.coinCount;
		ui.levelComplete.txtTimer.text = "Timer: " + timeLeft.ToString("F1");
		ui.levelComplete.levelCompletePanel.SetActive(true);
	}


	public void HurtPlayer() {
		if (player != null) {
			StartCoroutine(MuteMusic(true, 0f));
			AudioManager.instance.PlayFailSound(player.gameObject);
			DisableAndPushPlayer();
			Destroy(player.gameObject, timeToKill);
			DecrementLives();
			if (data.lifeCount > 0) {
				StartCoroutine(MuteMusic(false, timeToKill + timeToRespawn));
				Invoke("RespawnPlayer", timeToKill + timeToRespawn);
			}
			else {
				StartCoroutine(MuteMusic(false, timeToKill + timeToRespawn));
				GameOver();
			}
		}
	}

	IEnumerator MuteMusic(bool value, float delay) {
		yield return new WaitForSeconds(delay);
		Camera.main.GetComponentInChildren<AudioSource>().mute = value;
	}

	void DisableAndPushPlayer() {
		player.transform.GetComponent<PlayerCtrl>().enabled = false;
		foreach (Collider2D c2d in player.transform.GetComponents<Collider2D>()) {
			c2d.enabled = false;
		}
		foreach (Transform child in player.transform) {
			child.gameObject.SetActive(false);
		}
		Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
		rb.velocity = Vector2.zero;
		rb.AddForce(new Vector2(-150.0f, 400f));
	}

}
