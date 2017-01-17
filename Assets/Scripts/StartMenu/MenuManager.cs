using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject startMenuCanvas;
	public GameObject levelMenuCanvas;
	public GameObject firstLevelDetails;
	public GameObject secondLevelDetails;

	public enum GameManagerState
	{
		Opening,
		LevelMenu,
		FirstLevel,
		SecondLevel
	}

	GameManagerState GMState;

	void Start () 
	{
		GMState = GameManagerState.Opening;
		UpdateGameManagerState ();
	}

	void UpdateGameManagerState()
	{
		switch (GMState) {
		case GameManagerState.Opening:
			startMenuCanvas.SetActive (true);
			levelMenuCanvas.SetActive (false);
			firstLevelDetails.SetActive (false);
			secondLevelDetails.SetActive (false);
			break;

		case GameManagerState.LevelMenu:
			startMenuCanvas.SetActive (false);
			levelMenuCanvas.SetActive (true);
			firstLevelDetails.SetActive (false);
			secondLevelDetails.SetActive (false);
			break;

		case GameManagerState.FirstLevel:
			startMenuCanvas.SetActive (false);
			levelMenuCanvas.SetActive (false);
			firstLevelDetails.SetActive (true);
			secondLevelDetails.SetActive (false);
			break;

		case GameManagerState.SecondLevel:
			startMenuCanvas.SetActive (false);
			levelMenuCanvas.SetActive (false);
			secondLevelDetails.SetActive (true);
			firstLevelDetails.SetActive (false);
			break;
		}
	}

	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState ();
	}


	public void QuitGame(){
		Application.Quit();
	}

	public void FirstLevelPlayButton(){
		GMState = GameManagerState.FirstLevel;
		UpdateGameManagerState ();
	}

	public void SecondLevelPlayButton(){
		GMState = GameManagerState.SecondLevel;
		UpdateGameManagerState ();
	}
		
	public void startFirstLevel(){
		SceneManager.LoadScene ("FirstLevel");
	}

	public void GoToBegin(){
		GMState = GameManagerState.Opening;
		UpdateGameManagerState ();
	}

	public void startSecondLevel(){
		SceneManager.LoadScene ("SecondLevel");
	}

	public void NewGameButton(){
		GMState = GameManagerState.LevelMenu;
		UpdateGameManagerState ();
	}
}