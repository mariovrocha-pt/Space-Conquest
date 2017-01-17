using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	public float shotSpeed;
	
	void Update () 
	{
		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3 (0, shotSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		transform.position = pos;
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		if (transform.position.y > max.y) 
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ((col.tag == "EnemyShip") || (col.tag == "WorldHazard")) 
		{
			Destroy(gameObject);
		}
	}
}