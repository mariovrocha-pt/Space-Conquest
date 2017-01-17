using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour {

	public GameObject[] powerUps;
	public int wavesCount;
	public float spawnWait;
	public float nextSpawnIn;
	public GameObject GameManagerGO;

	public void Start () {
		StartCoroutine(SpawnPowerUp ());
	}

	IEnumerator SpawnPowerUp(){
		yield return new WaitForSeconds (nextSpawnIn);
		while (wavesCount != 0) {
			for (int i = 0; i < wavesCount; i++) {
				Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
				Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

				int powerUpNumber = powerUps.Length;
				GameObject powerup = (GameObject)Instantiate (powerUps [Random.Range (0, powerUpNumber)]);
				powerup.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

				yield return new WaitForSeconds (spawnWait);
				wavesCount--;
			}
		}
		yield return new WaitForSeconds (spawnWait);
	}

}
