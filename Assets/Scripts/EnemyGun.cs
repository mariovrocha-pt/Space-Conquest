using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

	public GameObject enemyBullet;
	public float fireRate;
	float cooldownTimer = 0;

	void Update () {
		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0) 
		{
			cooldownTimer = fireRate;
			FireEnemyGun ();
		}

	}

	void FireEnemyGun(){
		GameObject playerShip = GameObject.Find ("PlayerShip");

		if (playerShip != null) {

			GameObject bullet = (GameObject)Instantiate (enemyBullet);
			bullet.transform.position = transform.position;
			Vector2 direction = playerShip.transform.position - bullet.transform.position;
			bullet.GetComponent<EnemyBullet>().SetDirection (direction);

		}
	}
}
