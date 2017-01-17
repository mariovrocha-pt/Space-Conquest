using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstLevelManager : MonoBehaviour {

	public GameObject playerShip;
	public GameObject hud;
	public GameObject pauseButton;
	public GameObject pauseCanvas;
	public GameObject instructions;
	public GameObject gameOverCanvas;
	public GameObject winnerCanvas;
	public AudioSource backgroundMusic;
	public ScoreManager score;

	public enum GameManagerState
	{
		Gameplay,
		GameOver,
		Paused,
		Winner
	}

	GameManagerState GMState;

	void Start () 
	{
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState ();
		instructions.SetActive (true);
		StartCoroutine (Instructions ());
	}

	void UpdateGameManagerState()
	{
		switch (GMState) 
		{
		case GameManagerState.Gameplay:
			Cursor.visible = false;
			pauseButton.SetActive (true);
			hud.SetActive (true);
			pauseCanvas.SetActive (false);
			playerShip.SetActive (true);
			playerShip.GetComponent<PlayerControl> ().Init ();
			break;

		case GameManagerState.Paused:
			Cursor.visible = true;
			pauseButton.SetActive (true);
			instructions.SetActive (false);
			hud.SetActive (true);
			pauseCanvas.SetActive (true);
			playerShip.GetComponent<PlayerControl> ().Init();
			break;

		case GameManagerState.GameOver:
			Cursor.visible = true;
			pauseButton.SetActive (false);
			hud.SetActive (false);
			pauseCanvas.SetActive (false);
			pauseButton.SetActive (false);
			instructions.SetActive (false);
			playerShip.SetActive (false);
			Time.timeScale = 0.5f;
			gameOverCanvas.SetActive (true);
			break;

		case GameManagerState.Winner:
			Cursor.visible = true;
			pauseButton.SetActive (false);
			hud.SetActive (false);
			playerShip.SetActive (false);
			pauseCanvas.SetActive (false);
			pauseButton.SetActive (false);
			instructions.SetActive (false);
			Time.timeScale = 0.5f;
			gameOverCanvas.SetActive (false);
			winnerCanvas.SetActive (true);
			backgroundMusic.volume = 0.01f;
			break;
		}
	}

	public void PauseUnpauseGame()
	{
		if (Time.timeScale == 1) {
			Time.timeScale = 0;
			GMState = GameManagerState.Paused;
			UpdateGameManagerState ();

		} else {
			Time.timeScale = 1;
			GMState = GameManagerState.Gameplay;
			UpdateGameManagerState ();
		}
	}

	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}

	public void StartGamePlay()
	{
		playerShip.GetComponent<PlayerHealth> ().lives = 2;
		playerShip.GetComponent<PlayerHealth> ().maxHealth = 100f;
		playerShip.GetComponent<PlayerHealth> ().currentHealth = playerShip.GetComponent<PlayerHealth> ().maxHealth;
		score.GetComponent<ScoreManager>().score = 0;
		winnerCanvas.SetActive (false);
		gameOverCanvas.SetActive (false);
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState ();
		if (Time.timeScale == 0 || Time.timeScale == 0.5f) {
			Time.timeScale = 1;
		}
		else
			Time.timeScale = 1;
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("startMenu");
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
		}
		else
			Time.timeScale = 1;
	}

	public void NextLevel()
	{
		SceneManager.LoadScene("SecondLevel");
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
		}
		else
			Time.timeScale = 1;
	}


	public void QuitGame(){
		Application.Quit();
	}

	IEnumerator Instructions(){
		yield return new WaitForSeconds (5f);
		instructions.SetActive (false);
	}
		
}