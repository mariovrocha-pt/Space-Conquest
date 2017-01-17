using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject[] enemies;
	public int wavesCount;
	public float spawnWait;
	public float nextSpawnIn;
	public GameObject GameManagerGO;
	public bool stopSpawning = false;

	public void Start () {
		if (stopSpawning == false) {
			StartCoroutine (SpawnEnemy ());
		}
	}

	IEnumerator SpawnEnemy(){
		yield return new WaitForSeconds (nextSpawnIn);
		while (wavesCount != 0) {
			for (int i = 0; i < wavesCount; i++) {
				Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
				Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

				int enemiesNumber = enemies.Length;
				GameObject enemy = (GameObject)Instantiate (enemies [Random.Range (0, enemiesNumber)]);
				enemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

				yield return new WaitForSeconds (spawnWait);
				wavesCount--;
			}
		}
		yield return new WaitForSeconds (spawnWait);
		if (Application.loadedLevel == 1) {
			GameManagerGO.GetComponent<FirstLevelManager> ().SetGameManagerState (FirstLevelManager.GameManagerState.Winner);
		} else if (Application.loadedLevel == 2) {
			GameManagerGO.GetComponent<SecondLevelManager> ().SetGameManagerState (SecondLevelManager.GameManagerState.Winner);
		}
	}

}
