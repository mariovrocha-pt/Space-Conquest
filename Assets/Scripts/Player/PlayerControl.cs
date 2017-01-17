using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public GameObject GameManagerGO;
	public GameObject explosionPrefab;
	public GameObject PlayerBulletBlue;
	public GameObject PlayerBulletRed;
	public Transform firePoint01;
	public Transform firePoint02;
	public Transform firePoint03;
	public GameObject hitSound;
	int weaponID = 1;
	private float fireRate = 6f;
	float timeToFire = 0f;

	public AudioSource laserSound;

	float powerUpTimer;

	public float moveSpeed;

	public void Init()
	{
		gameObject.SetActive (true);
		transform.position = new Vector2 (0, -3.5f);
	}
	
	void Update () 
	{
		powerUpTimer -= Time.deltaTime;
		if (powerUpTimer < 0) {
			powerUpTimer = 0;
			ResetWeapon ();
		}

#if !MOBILE_INPUT
		{
			var pos = Input.mousePosition;
			pos.z = transform.position.z - Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			transform.position = Vector3.Lerp(transform.position, pos, moveSpeed * Time.deltaTime);
		}
#else
		if(Input.touchCount > 0) {
			Vector2  touchDeltaPosition =  Input.GetTouch(0).deltaPosition/30;
			transform.Translate (touchDeltaPosition.x * moveSpeed * Time.deltaTime, touchDeltaPosition.y * moveSpeed * Time.deltaTime, 0);
		}
#endif

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1,1));
		
		max.x = max.x - 0.225f; 
		min.x = min.x + 0.225f; 
		
		max.y = max.y - 0.285f; 
		min.y = min.y + 0.285f; 
		GetComponent<Rigidbody2D>().position = new Vector2 
			(
				Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, min.x, max.x),
				Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, min.y, max.y)	
			);
		if (fireRate == 0f) {
			if (Input.GetMouseButton (0)) {
				FireWeapon ();
			}
		} else {
			if (Input.GetMouseButton (0) && Time.time > timeToFire) {
				timeToFire = Time.time + 1f / fireRate;
				FireWeapon ();
			}
		}
	}

	void FireWeapon ()
	{
		laserSound.Play();
		switch (weaponID)
		{
		case 1:
			Instantiate(PlayerBulletBlue, firePoint01.position, firePoint01.rotation);
			break;
		case 2:
			Instantiate(PlayerBulletBlue, firePoint02.position, firePoint02.rotation);
			Instantiate(PlayerBulletBlue, firePoint03.position, firePoint03.rotation);
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "EnemyShip") || (col.tag == "EnemyShipBullet")) 
		{
			GetComponent<PlayerHealth>().TakeDamage(15f);
			PlayHitSound();

			if(GetComponent<PlayerHealth>().currentHealth <= 0)
			{
				PlayExplosion();
				GetComponent<PlayerHealth>().lives--;
				GetComponent<PlayerHealth> ().currentHealth = 100f;

				if(GetComponent<PlayerHealth>().lives <= 0){
					Time.timeScale = 0f;
					gameObject.SetActive(false);
					if (Application.loadedLevel == 1) {
						GameManagerGO.GetComponent<FirstLevelManager> ().SetGameManagerState (FirstLevelManager.GameManagerState.GameOver);

					} else if(Application.loadedLevel == 2){
						GameManagerGO.GetComponent<SecondLevelManager> ().SetGameManagerState (SecondLevelManager.GameManagerState.GameOver);
					}
				}
			}
		}

		else if ((col.tag == "2LineShotPowerUp"))
		{
			powerUpTimer = 8f;
			weaponID = 2;
		}
		else if ((col.tag == "LivesPowerUp"))
		{
			if (GetComponent<PlayerHealth> ().lives < 1) {
				GetComponent<PlayerHealth> ().lives ++;
			}
		}
		else if ((col.tag == "RefillLifePowerUp"))
		{
			GetComponent<PlayerHealth>().GiveHealth(50f);

			if (GetComponent<PlayerHealth>().currentHealth > 100f) {
				GetComponent<PlayerHealth>().currentHealth = 100f;
			}
		}
	}
		
	void ResetWeapon (){ weaponID = 1; }

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