using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	public int score;
	public int highScore = 0;
	public Text scoreText;
	public Text highScoreText;

	void Awake () {
		if (Application.loadedLevel == 1) {
			highScore = PlayerPrefs.GetInt ("bestScore");
			score = 0;
		}
		if (Application.loadedLevel == 2) {
			highScore = PlayerPrefs.GetInt ("bestScore2");
			score = 0;
		}
	}

	void Update () {
		if (Application.loadedLevel == 1) {
			scoreText.text = string.Format ("{0:0000000}", score);
			highScoreText.text = string.Format ("{0:0000000}", PlayerPrefs.GetInt ("bestScore"));

			if (score > highScore) {
				highScore = score;
				PlayerPrefs.SetInt ("bestScore", highScore);
			}
		}

		if (Application.loadedLevel == 2) {
			scoreText.text = string.Format ("{0:0000000}", score);
			highScoreText.text = string.Format ("{0:0000000}", PlayerPrefs.GetInt ("bestScore2"));

			if (score > highScore) {
				highScore = score;
				PlayerPrefs.SetInt ("bestScore2", highScore);
			}
		}

	}

	void OnDestroy(){
		if (Application.loadedLevel == 1) {
			PlayerPrefs.SetInt ("bestScore", highScore);
			PlayerPrefs.Save ();
		}

		if (Application.loadedLevel == 2) {
			PlayerPrefs.SetInt ("bestScore2", highScore);
			PlayerPrefs.Save ();
		}
	}
}
