using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public float speed;
	Vector2 min;
	Vector2 max;
	public GameObject powerUpSound;

	void Awake () {
		min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));

		max.y = max.y + GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;
		min.y = min.y - GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;

	}
	
	void Update () {
		
		Vector2 position = transform.position;
		position = new Vector2 (position.x, position.y + - speed * Time.deltaTime);
		transform.position = position;

		if (transform.position.y < min.y) 
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "PlayerShip"))
		{
			PowerUpSound ();
			Destroy (gameObject);
		}
	}

	void PowerUpSound()
	{
		StartCoroutine (PlaySounds ());
	}

	IEnumerator PlaySounds()
	{
		powerUpSound.SetActive (true);
		yield return new WaitForSeconds (0.1f);
		powerUpSound.SetActive (false);
	}
}
