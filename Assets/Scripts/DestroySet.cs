using UnityEngine;
using System.Collections;

public class DestroySet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		if (transform.position.y < min.y) {
			Destroy (gameObject);
		}
	}
}
