using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	float speed;
	public GameObject explosionPrefab;
	ScoreManager scoreM;

	public int points;
	public int maxHealth = 100;
	public int health; 
	public GameObject hitSound;


	void Start () {
		speed = 2f;
		health = maxHealth;
		scoreM = FindObjectOfType<ScoreManager> ();
	}
	
	void Update () {

		Vector2 position = transform.position;
		position = new Vector2 (position.x, position.y - speed * Time.deltaTime);
		transform.position = position;

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		if (transform.position.y < min.y) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "PlayerShip") || (col.tag == "PlayerShipBullet")) {
			health -= 35;

			if (health > 0) { 
				PlayHitSound ();
			} 
			else if (health <= 0) {
				PlayExplosion ();
				scoreM.score += points;
				Destroy (gameObject);
			}
		}
	}

	void PlayExplosion(){
		GameObject explosion = (GameObject)Instantiate (explosionPrefab);
		explosion.transform.position = transform.position;
	}


	void PlayHitSound()
	{
		StartCoroutine (PlayHits ());
	}

	IEnumerator PlayHits()
	{
		hitSound.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		hitSound.SetActive (false);
	}
}
